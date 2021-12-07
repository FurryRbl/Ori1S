using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Mono.Security.Protocol.Tls;

namespace System.Net
{
	// Token: 0x02000412 RID: 1042
	internal class WebConnection
	{
		// Token: 0x06002544 RID: 9540 RVA: 0x00071170 File Offset: 0x0006F370
		public WebConnection(WebConnectionGroup group, ServicePoint sPoint)
		{
			this.sPoint = sPoint;
			this.buffer = new byte[4096];
			this.readState = ReadState.None;
			this.Data = new WebConnectionData();
			this.initConn = new WaitCallback(this.InitConnection);
			this.queue = group.Queue;
			this.abortHelper = new WebConnection.AbortHelper();
			this.abortHelper.Connection = this;
			this.abortHandler = new EventHandler(this.abortHelper.Abort);
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x00071224 File Offset: 0x0006F424
		private bool CanReuse()
		{
			return !this.socket.Poll(0, System.Net.Sockets.SelectMode.SelectRead);
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x00071238 File Offset: 0x0006F438
		private void LoggedThrow(Exception e)
		{
			Console.WriteLine("Throwing this exception: " + e);
			throw e;
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x0007124C File Offset: 0x0006F44C
		internal static Stream DownloadPolicy(string url, string proxy)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			if (proxy != null)
			{
				httpWebRequest.Proxy = new WebProxy(proxy);
			}
			return httpWebRequest.GetResponse().GetResponseStream();
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x00071284 File Offset: 0x0006F484
		private void CheckUnityWebSecurity(HttpWebRequest request)
		{
			if (!Environment.SocketSecurityEnabled)
			{
				return;
			}
			Console.WriteLine("CheckingSecurityForUrl: " + request.RequestUri.AbsoluteUri);
			System.Uri requestUri = request.RequestUri;
			string text = string.Empty;
			if (!requestUri.IsDefaultPort)
			{
				text = ":" + requestUri.Port;
			}
			if (requestUri.ToString() == string.Concat(new string[]
			{
				requestUri.Scheme,
				"://",
				requestUri.Host,
				text,
				"/crossdomain.xml"
			}))
			{
				return;
			}
			try
			{
				if (WebConnection.method_GetSecurityPolicyFromNonMainThread == null)
				{
					Type type = Type.GetType("UnityEngine.UnityCrossDomainHelper, CrossDomainPolicyParser, Version=1.0.0.0, Culture=neutral");
					if (type == null)
					{
						this.LoggedThrow(new SecurityException("Cant find type UnityCrossDomainHelper"));
					}
					WebConnection.method_GetSecurityPolicyFromNonMainThread = type.GetMethod("GetSecurityPolicyForDotNetWebRequest");
					if (WebConnection.method_GetSecurityPolicyFromNonMainThread == null)
					{
						this.LoggedThrow(new SecurityException("Cant find GetSecurityPolicyFromNonMainThread"));
					}
				}
				MethodInfo method = typeof(WebConnection).GetMethod("DownloadPolicy", BindingFlags.Static | BindingFlags.NonPublic);
				if (method == null)
				{
					this.LoggedThrow(new SecurityException("Cannot find method DownloadPolicy"));
				}
				if (!(bool)WebConnection.method_GetSecurityPolicyFromNonMainThread.Invoke(null, new object[]
				{
					request.RequestUri.ToString(),
					method
				}))
				{
					this.LoggedThrow(new SecurityException("Webrequest was denied"));
				}
			}
			catch (Exception arg)
			{
				this.LoggedThrow(new SecurityException("Unexpected error while trying to call method_GetSecurityPolicyBlocking : " + arg));
			}
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x00071428 File Offset: 0x0006F628
		private void Connect(HttpWebRequest request)
		{
			object obj = this.socketLock;
			lock (obj)
			{
				if (this.socket != null && this.socket.Connected && this.status == WebExceptionStatus.Success && this.CanReuse() && this.CompleteChunkedRead())
				{
					this.reused = true;
				}
				else
				{
					this.reused = false;
					if (this.socket != null)
					{
						this.socket.Close();
						this.socket = null;
					}
					this.chunkStream = null;
					IPHostEntry hostEntry = this.sPoint.HostEntry;
					if (hostEntry == null)
					{
						this.status = ((!this.sPoint.UsesProxy) ? WebExceptionStatus.NameResolutionFailure : WebExceptionStatus.ProxyNameResolutionFailure);
					}
					else
					{
						WebConnectionData data = this.Data;
						foreach (IPAddress ipaddress in hostEntry.AddressList)
						{
							this.socket = new System.Net.Sockets.Socket(ipaddress.AddressFamily, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
							IPEndPoint ipendPoint = new IPEndPoint(ipaddress, this.sPoint.Address.Port);
							this.socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel.Tcp, System.Net.Sockets.SocketOptionName.Debug, (!this.sPoint.UseNagleAlgorithm) ? 1 : 0);
							this.socket.NoDelay = !this.sPoint.UseNagleAlgorithm;
							if (!this.sPoint.CallEndPointDelegate(this.socket, ipendPoint))
							{
								this.socket.Close();
								this.socket = null;
								this.status = WebExceptionStatus.ConnectFailure;
							}
							else
							{
								try
								{
									if (request.Aborted)
									{
										break;
									}
									this.CheckUnityWebSecurity(request);
									this.socket.Connect(ipendPoint, false);
									this.status = WebExceptionStatus.Success;
									break;
								}
								catch (ThreadAbortException)
								{
									System.Net.Sockets.Socket socket = this.socket;
									this.socket = null;
									if (socket != null)
									{
										socket.Close();
									}
									break;
								}
								catch (ObjectDisposedException ex)
								{
									break;
								}
								catch (Exception ex2)
								{
									System.Net.Sockets.Socket socket2 = this.socket;
									this.socket = null;
									if (socket2 != null)
									{
										socket2.Close();
									}
									if (!request.Aborted)
									{
										this.status = WebExceptionStatus.ConnectFailure;
									}
									this.connect_exception = ex2;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x000716D0 File Offset: 0x0006F8D0
		private static void EnsureSSLStreamAvailable()
		{
			object obj = WebConnection.classLock;
			lock (obj)
			{
				if (WebConnection.sslStream == null)
				{
					WebConnection.sslStream = Type.GetType("Mono.Security.Protocol.Tls.HttpsClientStream, Mono.Security, Version=2.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756", false);
					if (WebConnection.sslStream == null)
					{
						string message = "Missing Mono.Security.dll assembly. Support for SSL/TLS is unavailable.";
						throw new NotSupportedException(message);
					}
					WebConnection.piClient = WebConnection.sslStream.GetProperty("SelectedClientCertificate");
					WebConnection.piServer = WebConnection.sslStream.GetProperty("ServerCertificate");
					WebConnection.piTrustFailure = WebConnection.sslStream.GetProperty("TrustFailure");
				}
			}
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x00071784 File Offset: 0x0006F984
		private bool CreateTunnel(HttpWebRequest request, Stream stream, out byte[] buffer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CONNECT ");
			stringBuilder.Append(request.Address.Host);
			stringBuilder.Append(':');
			stringBuilder.Append(request.Address.Port);
			stringBuilder.Append(" HTTP/");
			if (request.ServicePoint.ProtocolVersion == HttpVersion.Version11)
			{
				stringBuilder.Append("1.1");
			}
			else
			{
				stringBuilder.Append("1.0");
			}
			stringBuilder.Append("\r\nHost: ");
			stringBuilder.Append(request.Address.Authority);
			string challenge = this.Data.Challenge;
			this.Data.Challenge = null;
			bool flag = request.Headers["Proxy-Authorization"] != null;
			if (flag)
			{
				stringBuilder.Append("\r\nProxy-Authorization: ");
				stringBuilder.Append(request.Headers["Proxy-Authorization"]);
			}
			else if (challenge != null && this.Data.StatusCode == 407)
			{
				flag = true;
				ICredentials credentials = request.Proxy.Credentials;
				Authorization authorization = AuthenticationManager.Authenticate(challenge, request, credentials);
				if (authorization != null)
				{
					stringBuilder.Append("\r\nProxy-Authorization: ");
					stringBuilder.Append(authorization.Message);
				}
			}
			stringBuilder.Append("\r\n\r\n");
			this.Data.StatusCode = 0;
			byte[] bytes = Encoding.Default.GetBytes(stringBuilder.ToString());
			stream.Write(bytes, 0, bytes.Length);
			int num;
			WebHeaderCollection webHeaderCollection = this.ReadHeaders(request, stream, out buffer, out num);
			if (!flag && webHeaderCollection != null && num == 407)
			{
				this.Data.StatusCode = num;
				this.Data.Challenge = webHeaderCollection["Proxy-Authenticate"];
				return false;
			}
			if (num != 200)
			{
				string where = string.Format("The remote server returned a {0} status code.", num);
				this.HandleError(WebExceptionStatus.SecureChannelFailure, null, where);
				return false;
			}
			return webHeaderCollection != null;
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x0007199C File Offset: 0x0006FB9C
		private WebHeaderCollection ReadHeaders(HttpWebRequest request, Stream stream, out byte[] retBuffer, out int status)
		{
			retBuffer = null;
			status = 200;
			byte[] array = new byte[1024];
			MemoryStream memoryStream = new MemoryStream();
			bool flag = false;
			int num2;
			WebHeaderCollection webHeaderCollection;
			for (;;)
			{
				int num = stream.Read(array, 0, 1024);
				if (num == 0)
				{
					break;
				}
				memoryStream.Write(array, 0, num);
				num2 = 0;
				string text = null;
				webHeaderCollection = new WebHeaderCollection();
				while (WebConnection.ReadLine(memoryStream.GetBuffer(), ref num2, (int)memoryStream.Length, ref text))
				{
					if (text == null)
					{
						goto Block_2;
					}
					if (flag)
					{
						webHeaderCollection.Add(text);
					}
					else
					{
						int num3 = text.IndexOf(' ');
						if (num3 == -1)
						{
							goto Block_5;
						}
						status = (int)uint.Parse(text.Substring(num3 + 1, 3));
						flag = true;
					}
				}
			}
			this.HandleError(WebExceptionStatus.ServerProtocolViolation, null, "ReadHeaders");
			return null;
			Block_2:
			if (memoryStream.Length - (long)num2 > 0L)
			{
				retBuffer = new byte[memoryStream.Length - (long)num2];
				Buffer.BlockCopy(memoryStream.GetBuffer(), num2, retBuffer, 0, retBuffer.Length);
			}
			return webHeaderCollection;
			Block_5:
			this.HandleError(WebExceptionStatus.ServerProtocolViolation, null, "ReadHeaders2");
			return null;
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x00071AB8 File Offset: 0x0006FCB8
		private bool CreateStream(HttpWebRequest request)
		{
			try
			{
				System.Net.Sockets.NetworkStream networkStream = new System.Net.Sockets.NetworkStream(this.socket, false);
				if (request.Address.Scheme == System.Uri.UriSchemeHttps)
				{
					this.ssl = true;
					WebConnection.EnsureSSLStreamAvailable();
					if (!this.reused || this.nstream == null || this.nstream.GetType() != WebConnection.sslStream)
					{
						byte[] array = null;
						if (this.sPoint.UseConnect && !this.CreateTunnel(request, networkStream, out array))
						{
							return false;
						}
						object[] args = new object[]
						{
							networkStream,
							request.ClientCertificates,
							request,
							array
						};
						this.nstream = (Stream)Activator.CreateInstance(WebConnection.sslStream, args);
						SslClientStream sslClientStream = (SslClientStream)this.nstream;
						ServicePointManager.ChainValidationHelper @object = new ServicePointManager.ChainValidationHelper(request);
						sslClientStream.ServerCertValidation2 += @object.ValidateChain;
						this.certsAvailable = false;
					}
				}
				else
				{
					this.ssl = false;
					this.nstream = networkStream;
				}
			}
			catch (Exception)
			{
				if (!request.Aborted)
				{
					this.status = WebExceptionStatus.ConnectFailure;
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x00071C08 File Offset: 0x0006FE08
		private void HandleError(WebExceptionStatus st, Exception e, string where)
		{
			this.status = st;
			lock (this)
			{
				if (st == WebExceptionStatus.RequestCanceled)
				{
					this.Data = new WebConnectionData();
				}
			}
			if (e == null)
			{
				try
				{
					throw new Exception(new StackTrace().ToString());
				}
				catch (Exception ex)
				{
					e = ex;
				}
			}
			HttpWebRequest httpWebRequest = null;
			if (this.Data != null && this.Data.request != null)
			{
				httpWebRequest = this.Data.request;
			}
			this.Close(true);
			if (httpWebRequest != null)
			{
				httpWebRequest.FinishedReading = true;
				httpWebRequest.SetResponseError(st, e, where);
			}
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x00071CE0 File Offset: 0x0006FEE0
		private static void ReadDone(IAsyncResult result)
		{
			WebConnection webConnection = (WebConnection)result.AsyncState;
			WebConnectionData data = webConnection.Data;
			Stream stream = webConnection.nstream;
			if (stream == null)
			{
				webConnection.Close(true);
				return;
			}
			int num = -1;
			try
			{
				num = stream.EndRead(result);
			}
			catch (Exception e)
			{
				webConnection.HandleError(WebExceptionStatus.ReceiveFailure, e, "ReadDone1");
				return;
			}
			if (num == 0)
			{
				webConnection.HandleError(WebExceptionStatus.ReceiveFailure, null, "ReadDone2");
				return;
			}
			if (num < 0)
			{
				webConnection.HandleError(WebExceptionStatus.ServerProtocolViolation, null, "ReadDone3");
				return;
			}
			int num2 = -1;
			num += webConnection.position;
			if (webConnection.readState == ReadState.None)
			{
				Exception ex = null;
				try
				{
					num2 = webConnection.GetResponse(webConnection.buffer, num);
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
				if (ex != null)
				{
					webConnection.HandleError(WebExceptionStatus.ServerProtocolViolation, ex, "ReadDone4");
					return;
				}
			}
			if (webConnection.readState != ReadState.Content)
			{
				int num3 = num * 2;
				int num4 = (num3 >= webConnection.buffer.Length) ? num3 : webConnection.buffer.Length;
				byte[] dst = new byte[num4];
				Buffer.BlockCopy(webConnection.buffer, 0, dst, 0, num);
				webConnection.buffer = dst;
				webConnection.position = num;
				webConnection.readState = ReadState.None;
				WebConnection.InitRead(webConnection);
				return;
			}
			webConnection.position = 0;
			WebConnectionStream webConnectionStream = new WebConnectionStream(webConnection);
			string text = data.Headers["Transfer-Encoding"];
			webConnection.chunkedRead = (text != null && text.ToLower().IndexOf("chunked") != -1);
			if (!webConnection.chunkedRead)
			{
				webConnectionStream.ReadBuffer = webConnection.buffer;
				webConnectionStream.ReadBufferOffset = num2;
				webConnectionStream.ReadBufferSize = num;
				webConnectionStream.CheckResponseInBuffer();
			}
			else if (webConnection.chunkStream == null)
			{
				try
				{
					webConnection.chunkStream = new ChunkStream(webConnection.buffer, num2, num, data.Headers);
				}
				catch (Exception e2)
				{
					webConnection.HandleError(WebExceptionStatus.ServerProtocolViolation, e2, "ReadDone5");
					return;
				}
			}
			else
			{
				webConnection.chunkStream.ResetBuffer();
				try
				{
					webConnection.chunkStream.Write(webConnection.buffer, num2, num);
				}
				catch (Exception e3)
				{
					webConnection.HandleError(WebExceptionStatus.ServerProtocolViolation, e3, "ReadDone6");
					return;
				}
			}
			data.stream = webConnectionStream;
			if (!WebConnection.ExpectContent(data.StatusCode) || data.request.Method == "HEAD")
			{
				webConnectionStream.ForceCompletion();
			}
			data.request.SetResponseData(data);
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x00071FD0 File Offset: 0x000701D0
		private static bool ExpectContent(int statusCode)
		{
			return statusCode >= 200 && statusCode != 204 && statusCode != 304;
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x00072004 File Offset: 0x00070204
		internal void GetCertificates()
		{
			X509Certificate client = (X509Certificate)WebConnection.piClient.GetValue(this.nstream, null);
			X509Certificate x509Certificate = (X509Certificate)WebConnection.piServer.GetValue(this.nstream, null);
			this.sPoint.SetCertificates(client, x509Certificate);
			this.certsAvailable = (x509Certificate != null);
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x0007205C File Offset: 0x0007025C
		internal static void InitRead(object state)
		{
			WebConnection webConnection = (WebConnection)state;
			Stream stream = webConnection.nstream;
			try
			{
				int count = webConnection.buffer.Length - webConnection.position;
				stream.BeginRead(webConnection.buffer, webConnection.position, count, WebConnection.readDoneDelegate, webConnection);
			}
			catch (Exception e)
			{
				webConnection.HandleError(WebExceptionStatus.ReceiveFailure, e, "InitRead");
			}
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x000720D8 File Offset: 0x000702D8
		private int GetResponse(byte[] buffer, int max)
		{
			int num = 0;
			string text = null;
			bool flag = false;
			bool flag2 = false;
			for (;;)
			{
				if (this.readState != ReadState.None)
				{
					goto IL_114;
				}
				if (!WebConnection.ReadLine(buffer, ref num, max, ref text))
				{
					break;
				}
				if (text == null)
				{
					flag2 = true;
				}
				else
				{
					flag2 = false;
					this.readState = ReadState.Status;
					string[] array = text.Split(new char[]
					{
						' '
					});
					if (array.Length < 2)
					{
						return -1;
					}
					if (string.Compare(array[0], "HTTP/1.1", true) == 0)
					{
						this.Data.Version = HttpVersion.Version11;
						this.sPoint.SetVersion(HttpVersion.Version11);
					}
					else
					{
						this.Data.Version = HttpVersion.Version10;
						this.sPoint.SetVersion(HttpVersion.Version10);
					}
					this.Data.StatusCode = (int)uint.Parse(array[1]);
					if (array.Length >= 3)
					{
						this.Data.StatusDescription = string.Join(" ", array, 2, array.Length - 2);
					}
					else
					{
						this.Data.StatusDescription = string.Empty;
					}
					if (num >= max)
					{
						return num;
					}
					goto IL_114;
				}
				IL_2CA:
				if (!flag2 && !flag)
				{
					return -1;
				}
				continue;
				IL_114:
				flag2 = false;
				if (this.readState != ReadState.Status)
				{
					goto IL_2CA;
				}
				this.readState = ReadState.Headers;
				this.Data.Headers = new WebHeaderCollection();
				ArrayList arrayList = new ArrayList();
				bool flag3 = false;
				while (!flag3)
				{
					if (!WebConnection.ReadLine(buffer, ref num, max, ref text))
					{
						break;
					}
					if (text == null)
					{
						flag3 = true;
					}
					else if (text.Length > 0 && (text[0] == ' ' || text[0] == '\t'))
					{
						int num2 = arrayList.Count - 1;
						if (num2 < 0)
						{
							break;
						}
						string value = (string)arrayList[num2] + text;
						arrayList[num2] = value;
					}
					else
					{
						arrayList.Add(text);
					}
				}
				if (!flag3)
				{
					return -1;
				}
				foreach (object obj in arrayList)
				{
					string @internal = (string)obj;
					this.Data.Headers.SetInternal(@internal);
				}
				if (this.Data.StatusCode != 100)
				{
					goto IL_2C1;
				}
				this.sPoint.SendContinue = true;
				if (num >= max)
				{
					return num;
				}
				if (this.Data.request.ExpectContinue)
				{
					this.Data.request.DoContinueDelegate(this.Data.StatusCode, this.Data.Headers);
					this.Data.request.ExpectContinue = false;
				}
				this.readState = ReadState.None;
				flag = true;
				goto IL_2CA;
			}
			return -1;
			IL_2C1:
			this.readState = ReadState.Content;
			return num;
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x000723DC File Offset: 0x000705DC
		private void InitConnection(object state)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)state;
			httpWebRequest.WebConnection = this;
			if (httpWebRequest.Aborted)
			{
				return;
			}
			this.keepAlive = httpWebRequest.KeepAlive;
			this.Data = new WebConnectionData();
			this.Data.request = httpWebRequest;
			WebExceptionStatus webExceptionStatus;
			for (;;)
			{
				this.Connect(httpWebRequest);
				if (httpWebRequest.Aborted)
				{
					break;
				}
				if (this.status != WebExceptionStatus.Success)
				{
					goto Block_3;
				}
				if (this.CreateStream(httpWebRequest))
				{
					goto IL_D2;
				}
				if (httpWebRequest.Aborted)
				{
					return;
				}
				webExceptionStatus = this.status;
				if (this.Data.Challenge == null)
				{
					goto IL_B4;
				}
			}
			return;
			Block_3:
			if (!httpWebRequest.Aborted)
			{
				httpWebRequest.SetWriteStreamError(this.status, this.connect_exception);
				this.Close(true);
			}
			return;
			IL_B4:
			Exception exc = this.connect_exception;
			this.connect_exception = null;
			httpWebRequest.SetWriteStreamError(webExceptionStatus, exc);
			this.Close(true);
			return;
			IL_D2:
			this.readState = ReadState.None;
			httpWebRequest.SetWriteStream(new WebConnectionStream(this, httpWebRequest));
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x000724D0 File Offset: 0x000706D0
		internal EventHandler SendRequest(HttpWebRequest request)
		{
			if (request.Aborted)
			{
				return null;
			}
			lock (this)
			{
				if (!this.busy)
				{
					this.busy = true;
					this.status = WebExceptionStatus.Success;
					ThreadPool.QueueUserWorkItem(this.initConn, request);
				}
				else
				{
					Queue obj = this.queue;
					lock (obj)
					{
						this.queue.Enqueue(request);
					}
				}
			}
			return this.abortHandler;
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x00072588 File Offset: 0x00070788
		private void SendNext()
		{
			Queue obj = this.queue;
			lock (obj)
			{
				if (this.queue.Count > 0)
				{
					this.SendRequest((HttpWebRequest)this.queue.Dequeue());
				}
			}
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x000725F4 File Offset: 0x000707F4
		internal void NextRead()
		{
			lock (this)
			{
				this.Data.request.FinishedReading = true;
				string name = (!this.sPoint.UsesProxy) ? "Connection" : "Proxy-Connection";
				string text = (this.Data.Headers == null) ? null : this.Data.Headers[name];
				bool flag = this.Data.Version == HttpVersion.Version11 && this.keepAlive;
				if (text != null)
				{
					text = text.ToLower();
					flag = (this.keepAlive && text.IndexOf("keep-alive") != -1);
				}
				if ((this.socket != null && !this.socket.Connected) || !flag || (text != null && text.IndexOf("close") != -1))
				{
					this.Close(false);
				}
				this.busy = false;
				if (this.priority_request != null)
				{
					this.SendRequest(this.priority_request);
					this.priority_request = null;
				}
				else
				{
					this.SendNext();
				}
			}
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x0007274C File Offset: 0x0007094C
		private static bool ReadLine(byte[] buffer, ref int start, int max, ref string output)
		{
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (start < max)
			{
				num = (int)buffer[start++];
				if (num == 10)
				{
					if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '\r')
					{
						stringBuilder.Length--;
					}
					flag = false;
					break;
				}
				if (flag)
				{
					stringBuilder.Length--;
					break;
				}
				if (num == 13)
				{
					flag = true;
				}
				stringBuilder.Append((char)num);
			}
			if (num != 10 && num != 13)
			{
				return false;
			}
			if (stringBuilder.Length == 0)
			{
				output = null;
				return num == 10 || num == 13;
			}
			if (flag)
			{
				stringBuilder.Length--;
			}
			output = stringBuilder.ToString();
			return true;
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x00072834 File Offset: 0x00070A34
		internal IAsyncResult BeginRead(HttpWebRequest request, byte[] buffer, int offset, int size, AsyncCallback cb, object state)
		{
			lock (this)
			{
				if (this.Data.request != request)
				{
					throw new ObjectDisposedException(typeof(System.Net.Sockets.NetworkStream).FullName);
				}
				if (this.nstream == null)
				{
					return null;
				}
			}
			IAsyncResult asyncResult = null;
			if (this.chunkedRead)
			{
				if (!this.chunkStream.WantMore)
				{
					goto IL_9A;
				}
			}
			try
			{
				asyncResult = this.nstream.BeginRead(buffer, offset, size, cb, state);
				cb = null;
			}
			catch (Exception)
			{
				this.HandleError(WebExceptionStatus.ReceiveFailure, null, "chunked BeginRead");
				throw;
			}
			IL_9A:
			if (this.chunkedRead)
			{
				WebAsyncResult webAsyncResult = new WebAsyncResult(cb, state, buffer, offset, size);
				webAsyncResult.InnerAsyncResult = asyncResult;
				if (asyncResult == null)
				{
					webAsyncResult.SetCompleted(true, null);
					webAsyncResult.DoCallback();
				}
				return webAsyncResult;
			}
			return asyncResult;
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x00072948 File Offset: 0x00070B48
		internal int EndRead(HttpWebRequest request, IAsyncResult result)
		{
			lock (this)
			{
				if (this.Data.request != request)
				{
					throw new ObjectDisposedException(typeof(System.Net.Sockets.NetworkStream).FullName);
				}
				if (this.nstream == null)
				{
					throw new ObjectDisposedException(typeof(System.Net.Sockets.NetworkStream).FullName);
				}
			}
			int num = 0;
			WebAsyncResult webAsyncResult = null;
			IAsyncResult innerAsyncResult = ((WebAsyncResult)result).InnerAsyncResult;
			if (this.chunkedRead && innerAsyncResult is WebAsyncResult)
			{
				webAsyncResult = (WebAsyncResult)innerAsyncResult;
				IAsyncResult innerAsyncResult2 = webAsyncResult.InnerAsyncResult;
				if (innerAsyncResult2 != null && !(innerAsyncResult2 is WebAsyncResult))
				{
					num = this.nstream.EndRead(innerAsyncResult2);
				}
			}
			else if (!(innerAsyncResult is WebAsyncResult))
			{
				num = this.nstream.EndRead(innerAsyncResult);
				webAsyncResult = (WebAsyncResult)result;
			}
			if (this.chunkedRead)
			{
				bool flag = num == 0;
				try
				{
					this.chunkStream.WriteAndReadBack(webAsyncResult.Buffer, webAsyncResult.Offset, webAsyncResult.Size, ref num);
					if (!flag && num == 0 && this.chunkStream.WantMore)
					{
						num = this.EnsureRead(webAsyncResult.Buffer, webAsyncResult.Offset, webAsyncResult.Size);
					}
				}
				catch (Exception ex)
				{
					if (ex is WebException)
					{
						throw ex;
					}
					throw new WebException("Invalid chunked data.", ex, WebExceptionStatus.ServerProtocolViolation, null);
				}
				if ((flag || num == 0) && this.chunkStream.ChunkLeft != 0)
				{
					this.HandleError(WebExceptionStatus.ReceiveFailure, null, "chunked EndRead");
					throw new WebException("Read error", null, WebExceptionStatus.ReceiveFailure, null);
				}
			}
			return (num == 0) ? -1 : num;
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x00072B34 File Offset: 0x00070D34
		private int EnsureRead(byte[] buffer, int offset, int size)
		{
			byte[] array = null;
			int num = 0;
			while (num == 0 && this.chunkStream.WantMore)
			{
				int num2 = this.chunkStream.ChunkLeft;
				if (num2 <= 0)
				{
					num2 = 1024;
				}
				else if (num2 > 16384)
				{
					num2 = 16384;
				}
				if (array == null || array.Length < num2)
				{
					array = new byte[num2];
				}
				int num3 = this.nstream.Read(array, 0, num2);
				if (num3 <= 0)
				{
					return 0;
				}
				this.chunkStream.Write(array, 0, num3);
				num += this.chunkStream.Read(buffer, offset + num, size - num);
			}
			return num;
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x00072BE4 File Offset: 0x00070DE4
		private bool CompleteChunkedRead()
		{
			if (!this.chunkedRead || this.chunkStream == null)
			{
				return true;
			}
			while (this.chunkStream.WantMore)
			{
				int num = this.nstream.Read(this.buffer, 0, this.buffer.Length);
				if (num <= 0)
				{
					return false;
				}
				this.chunkStream.Write(this.buffer, 0, num);
			}
			return true;
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x00072C58 File Offset: 0x00070E58
		internal IAsyncResult BeginWrite(HttpWebRequest request, byte[] buffer, int offset, int size, AsyncCallback cb, object state)
		{
			lock (this)
			{
				if (this.Data.request != request)
				{
					throw new ObjectDisposedException(typeof(System.Net.Sockets.NetworkStream).FullName);
				}
				if (this.nstream == null)
				{
					return null;
				}
			}
			IAsyncResult result = null;
			try
			{
				result = this.nstream.BeginWrite(buffer, offset, size, cb, state);
			}
			catch (Exception)
			{
				this.status = WebExceptionStatus.SendFailure;
				throw;
			}
			return result;
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x00072D14 File Offset: 0x00070F14
		internal void EndWrite2(HttpWebRequest request, IAsyncResult result)
		{
			if (request.FinishedReading)
			{
				return;
			}
			lock (this)
			{
				if (this.Data.request != request)
				{
					throw new ObjectDisposedException(typeof(System.Net.Sockets.NetworkStream).FullName);
				}
				if (this.nstream == null)
				{
					throw new ObjectDisposedException(typeof(System.Net.Sockets.NetworkStream).FullName);
				}
			}
			try
			{
				this.nstream.EndWrite(result);
			}
			catch (Exception ex)
			{
				this.status = WebExceptionStatus.SendFailure;
				if (ex.InnerException != null)
				{
					throw ex.InnerException;
				}
				throw;
			}
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x00072DF0 File Offset: 0x00070FF0
		internal bool EndWrite(HttpWebRequest request, IAsyncResult result)
		{
			if (request.FinishedReading)
			{
				return true;
			}
			lock (this)
			{
				if (this.Data.request != request)
				{
					throw new ObjectDisposedException(typeof(System.Net.Sockets.NetworkStream).FullName);
				}
				if (this.nstream == null)
				{
					throw new ObjectDisposedException(typeof(System.Net.Sockets.NetworkStream).FullName);
				}
			}
			bool result2;
			try
			{
				this.nstream.EndWrite(result);
				result2 = true;
			}
			catch
			{
				this.status = WebExceptionStatus.SendFailure;
				result2 = false;
			}
			return result2;
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x00072EC8 File Offset: 0x000710C8
		internal int Read(HttpWebRequest request, byte[] buffer, int offset, int size)
		{
			lock (this)
			{
				if (this.Data.request != request)
				{
					throw new ObjectDisposedException(typeof(System.Net.Sockets.NetworkStream).FullName);
				}
				if (this.nstream == null)
				{
					return 0;
				}
			}
			int num = 0;
			try
			{
				bool flag = false;
				if (!this.chunkedRead)
				{
					num = this.nstream.Read(buffer, offset, size);
					flag = (num == 0);
				}
				if (this.chunkedRead)
				{
					try
					{
						this.chunkStream.WriteAndReadBack(buffer, offset, size, ref num);
						if (!flag && num == 0 && this.chunkStream.WantMore)
						{
							num = this.EnsureRead(buffer, offset, size);
						}
					}
					catch (Exception e)
					{
						this.HandleError(WebExceptionStatus.ReceiveFailure, e, "chunked Read1");
						throw;
					}
					if ((flag || num == 0) && this.chunkStream.WantMore)
					{
						this.HandleError(WebExceptionStatus.ReceiveFailure, null, "chunked Read2");
						throw new WebException("Read error", null, WebExceptionStatus.ReceiveFailure, null);
					}
				}
			}
			catch (Exception e2)
			{
				this.HandleError(WebExceptionStatus.ReceiveFailure, e2, "Read");
			}
			return num;
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x00073044 File Offset: 0x00071244
		internal bool Write(HttpWebRequest request, byte[] buffer, int offset, int size, ref string err_msg)
		{
			err_msg = null;
			lock (this)
			{
				if (this.Data.request != request)
				{
					throw new ObjectDisposedException(typeof(System.Net.Sockets.NetworkStream).FullName);
				}
				if (this.nstream == null)
				{
					return false;
				}
			}
			try
			{
				this.nstream.Write(buffer, offset, size);
				if (this.ssl && !this.certsAvailable)
				{
					this.GetCertificates();
				}
			}
			catch (Exception ex)
			{
				err_msg = ex.Message;
				WebExceptionStatus st = WebExceptionStatus.SendFailure;
				string where = "Write: " + err_msg;
				if (ex is WebException)
				{
					this.HandleError(st, ex, where);
					return false;
				}
				if (this.ssl && (bool)WebConnection.piTrustFailure.GetValue(this.nstream, null))
				{
					st = WebExceptionStatus.TrustFailure;
					where = "Trust failure";
				}
				this.HandleError(st, ex, where);
				return false;
			}
			return true;
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x00073188 File Offset: 0x00071388
		internal void Close(bool sendNext)
		{
			lock (this)
			{
				if (this.nstream != null)
				{
					try
					{
						this.nstream.Close();
					}
					catch
					{
					}
					this.nstream = null;
				}
				if (this.socket != null)
				{
					try
					{
						this.socket.Close();
					}
					catch
					{
					}
					this.socket = null;
				}
				this.busy = false;
				this.Data = new WebConnectionData();
				if (sendNext)
				{
					this.SendNext();
				}
			}
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x00073264 File Offset: 0x00071464
		private void Abort(object sender, EventArgs args)
		{
			lock (this)
			{
				Queue obj = this.queue;
				lock (obj)
				{
					HttpWebRequest httpWebRequest = (HttpWebRequest)sender;
					if (this.Data.request == httpWebRequest)
					{
						if (!httpWebRequest.FinishedReading)
						{
							this.status = WebExceptionStatus.RequestCanceled;
							this.Close(false);
							if (this.queue.Count > 0)
							{
								this.Data.request = (HttpWebRequest)this.queue.Dequeue();
								this.SendRequest(this.Data.request);
							}
						}
					}
					else
					{
						httpWebRequest.FinishedReading = true;
						httpWebRequest.SetResponseError(WebExceptionStatus.RequestCanceled, null, "User aborted");
						if (this.queue.Count > 0 && this.queue.Peek() == sender)
						{
							this.queue.Dequeue();
						}
						else if (this.queue.Count > 0)
						{
							object[] array = this.queue.ToArray();
							this.queue.Clear();
							for (int i = array.Length - 1; i >= 0; i--)
							{
								if (array[i] != sender)
								{
									this.queue.Enqueue(array[i]);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x000733E8 File Offset: 0x000715E8
		internal void ResetNtlm()
		{
			this.ntlm_authenticated = false;
			this.ntlm_credentials = null;
			this.unsafe_sharing = false;
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x00073400 File Offset: 0x00071600
		internal bool Busy
		{
			get
			{
				bool result;
				lock (this)
				{
					result = this.busy;
				}
				return result;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06002567 RID: 9575 RVA: 0x0007344C File Offset: 0x0007164C
		internal bool Connected
		{
			get
			{
				bool result;
				lock (this)
				{
					result = (this.socket != null && this.socket.Connected);
				}
				return result;
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (set) Token: 0x06002568 RID: 9576 RVA: 0x000734AC File Offset: 0x000716AC
		internal HttpWebRequest PriorityRequest
		{
			set
			{
				this.priority_request = value;
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06002569 RID: 9577 RVA: 0x000734B8 File Offset: 0x000716B8
		// (set) Token: 0x0600256A RID: 9578 RVA: 0x000734C0 File Offset: 0x000716C0
		internal bool NtlmAuthenticated
		{
			get
			{
				return this.ntlm_authenticated;
			}
			set
			{
				this.ntlm_authenticated = value;
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x0600256B RID: 9579 RVA: 0x000734CC File Offset: 0x000716CC
		// (set) Token: 0x0600256C RID: 9580 RVA: 0x000734D4 File Offset: 0x000716D4
		internal NetworkCredential NtlmCredential
		{
			get
			{
				return this.ntlm_credentials;
			}
			set
			{
				this.ntlm_credentials = value;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x0600256D RID: 9581 RVA: 0x000734E0 File Offset: 0x000716E0
		// (set) Token: 0x0600256E RID: 9582 RVA: 0x000734E8 File Offset: 0x000716E8
		internal bool UnsafeAuthenticatedConnectionSharing
		{
			get
			{
				return this.unsafe_sharing;
			}
			set
			{
				this.unsafe_sharing = value;
			}
		}

		// Token: 0x04001706 RID: 5894
		private ServicePoint sPoint;

		// Token: 0x04001707 RID: 5895
		private Stream nstream;

		// Token: 0x04001708 RID: 5896
		private System.Net.Sockets.Socket socket;

		// Token: 0x04001709 RID: 5897
		private object socketLock = new object();

		// Token: 0x0400170A RID: 5898
		private WebExceptionStatus status;

		// Token: 0x0400170B RID: 5899
		private WaitCallback initConn;

		// Token: 0x0400170C RID: 5900
		private bool keepAlive;

		// Token: 0x0400170D RID: 5901
		private byte[] buffer;

		// Token: 0x0400170E RID: 5902
		private static AsyncCallback readDoneDelegate = new AsyncCallback(WebConnection.ReadDone);

		// Token: 0x0400170F RID: 5903
		private EventHandler abortHandler;

		// Token: 0x04001710 RID: 5904
		private WebConnection.AbortHelper abortHelper;

		// Token: 0x04001711 RID: 5905
		private ReadState readState;

		// Token: 0x04001712 RID: 5906
		internal WebConnectionData Data;

		// Token: 0x04001713 RID: 5907
		private bool chunkedRead;

		// Token: 0x04001714 RID: 5908
		private ChunkStream chunkStream;

		// Token: 0x04001715 RID: 5909
		private Queue queue;

		// Token: 0x04001716 RID: 5910
		private bool reused;

		// Token: 0x04001717 RID: 5911
		private int position;

		// Token: 0x04001718 RID: 5912
		private bool busy;

		// Token: 0x04001719 RID: 5913
		private HttpWebRequest priority_request;

		// Token: 0x0400171A RID: 5914
		private NetworkCredential ntlm_credentials;

		// Token: 0x0400171B RID: 5915
		private bool ntlm_authenticated;

		// Token: 0x0400171C RID: 5916
		private bool unsafe_sharing;

		// Token: 0x0400171D RID: 5917
		private bool ssl;

		// Token: 0x0400171E RID: 5918
		private bool certsAvailable;

		// Token: 0x0400171F RID: 5919
		private Exception connect_exception;

		// Token: 0x04001720 RID: 5920
		private static object classLock = new object();

		// Token: 0x04001721 RID: 5921
		private static Type sslStream;

		// Token: 0x04001722 RID: 5922
		private static PropertyInfo piClient;

		// Token: 0x04001723 RID: 5923
		private static PropertyInfo piServer;

		// Token: 0x04001724 RID: 5924
		private static PropertyInfo piTrustFailure;

		// Token: 0x04001725 RID: 5925
		private static MethodInfo method_GetSecurityPolicyFromNonMainThread;

		// Token: 0x02000413 RID: 1043
		private class AbortHelper
		{
			// Token: 0x06002570 RID: 9584 RVA: 0x000734FC File Offset: 0x000716FC
			public void Abort(object sender, EventArgs args)
			{
				WebConnection webConnection = ((HttpWebRequest)sender).WebConnection;
				if (webConnection == null)
				{
					webConnection = this.Connection;
				}
				webConnection.Abort(sender, args);
			}

			// Token: 0x04001726 RID: 5926
			public WebConnection Connection;
		}
	}
}
