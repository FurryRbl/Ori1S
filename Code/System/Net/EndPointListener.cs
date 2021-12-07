using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Authenticode;

namespace System.Net
{
	// Token: 0x02000300 RID: 768
	internal sealed class EndPointListener
	{
		// Token: 0x06001A50 RID: 6736 RVA: 0x00049400 File Offset: 0x00047600
		public EndPointListener(IPAddress addr, int port, bool secure)
		{
			if (secure)
			{
				this.secure = secure;
				this.LoadCertificateAndKey(addr, port);
			}
			this.endpoint = new IPEndPoint(addr, port);
			this.sock = new System.Net.Sockets.Socket(addr.AddressFamily, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
			this.sock.Bind(this.endpoint);
			this.sock.Listen(500);
			this.sock.BeginAccept(new AsyncCallback(EndPointListener.OnAccept), this);
			this.prefixes = new Hashtable();
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00049490 File Offset: 0x00047690
		private void LoadCertificateAndKey(IPAddress addr, int port)
		{
			try
			{
				string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				string path = Path.Combine(folderPath, ".mono");
				path = Path.Combine(path, "httplistener");
				string fileName = Path.Combine(path, string.Format("{0}.cer", port));
				string filename = Path.Combine(path, string.Format("{0}.pvk", port));
				this.cert = new System.Security.Cryptography.X509Certificates.X509Certificate2(fileName);
				this.key = PrivateKey.CreateFromFile(filename).RSA;
			}
			catch
			{
			}
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00049530 File Offset: 0x00047730
		private static void OnAccept(IAsyncResult ares)
		{
			EndPointListener endPointListener = (EndPointListener)ares.AsyncState;
			System.Net.Sockets.Socket socket = null;
			try
			{
				socket = endPointListener.sock.EndAccept(ares);
			}
			catch
			{
			}
			finally
			{
				try
				{
					endPointListener.sock.BeginAccept(new AsyncCallback(EndPointListener.OnAccept), endPointListener);
				}
				catch
				{
					if (socket != null)
					{
						try
						{
							socket.Close();
						}
						catch
						{
						}
						socket = null;
					}
				}
			}
			if (socket == null)
			{
				return;
			}
			if (endPointListener.secure && (endPointListener.cert == null || endPointListener.key == null))
			{
				socket.Close();
				return;
			}
			HttpConnection httpConnection = new HttpConnection(socket, endPointListener, endPointListener.secure, endPointListener.cert, endPointListener.key);
			httpConnection.BeginReadRequest();
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x00049654 File Offset: 0x00047854
		public bool BindContext(HttpListenerContext context)
		{
			HttpListenerRequest request = context.Request;
			ListenerPrefix prefix;
			HttpListener httpListener = this.SearchListener(request.UserHostName, request.Url, out prefix);
			if (httpListener == null)
			{
				return false;
			}
			context.Listener = httpListener;
			context.Connection.Prefix = prefix;
			httpListener.RegisterContext(context);
			return true;
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x000496A0 File Offset: 0x000478A0
		public void UnbindContext(HttpListenerContext context)
		{
			if (context == null || context.Request == null)
			{
				return;
			}
			HttpListenerRequest request = context.Request;
			ListenerPrefix listenerPrefix;
			HttpListener httpListener = this.SearchListener(request.UserHostName, request.Url, out listenerPrefix);
			if (httpListener != null)
			{
				httpListener.UnregisterContext(context);
			}
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x000496E8 File Offset: 0x000478E8
		private HttpListener SearchListener(string host, System.Uri uri, out ListenerPrefix prefix)
		{
			prefix = null;
			if (uri == null)
			{
				return null;
			}
			if (host != null)
			{
				int num = host.IndexOf(':');
				if (num >= 0)
				{
					host = host.Substring(0, num);
				}
			}
			string text = HttpUtility.UrlDecode(uri.AbsolutePath);
			string text2 = (text[text.Length - 1] != '/') ? (text + "/") : text;
			HttpListener httpListener = null;
			int num2 = -1;
			Hashtable obj = this.prefixes;
			lock (obj)
			{
				if (host != null && host != string.Empty)
				{
					foreach (object obj2 in this.prefixes.Keys)
					{
						ListenerPrefix listenerPrefix = (ListenerPrefix)obj2;
						string path = listenerPrefix.Path;
						if (path.Length >= num2)
						{
							if (listenerPrefix.Host == host && (text.StartsWith(path) || text2.StartsWith(path)))
							{
								num2 = path.Length;
								httpListener = (HttpListener)this.prefixes[listenerPrefix];
								prefix = listenerPrefix;
							}
						}
					}
					if (num2 != -1)
					{
						return httpListener;
					}
				}
				httpListener = this.MatchFromList(host, text, this.unhandled, out prefix);
				if (httpListener != null)
				{
					return httpListener;
				}
				httpListener = this.MatchFromList(host, text, this.all, out prefix);
				if (httpListener != null)
				{
					return httpListener;
				}
			}
			return null;
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x000498C8 File Offset: 0x00047AC8
		private HttpListener MatchFromList(string host, string path, ArrayList list, out ListenerPrefix prefix)
		{
			prefix = null;
			if (list == null)
			{
				return null;
			}
			HttpListener result = null;
			int num = -1;
			foreach (object obj in list)
			{
				ListenerPrefix listenerPrefix = (ListenerPrefix)obj;
				string path2 = listenerPrefix.Path;
				if (path2.Length >= num)
				{
					if (path.StartsWith(path2))
					{
						num = path2.Length;
						result = listenerPrefix.Listener;
						prefix = listenerPrefix;
					}
				}
			}
			return result;
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x0004997C File Offset: 0x00047B7C
		private void AddSpecial(ArrayList coll, ListenerPrefix prefix)
		{
			if (coll == null)
			{
				return;
			}
			foreach (object obj in coll)
			{
				ListenerPrefix listenerPrefix = (ListenerPrefix)obj;
				if (listenerPrefix.Path == prefix.Path)
				{
					throw new HttpListenerException(400, "Prefix already in use.");
				}
			}
			coll.Add(prefix);
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00049A14 File Offset: 0x00047C14
		private void RemoveSpecial(ArrayList coll, ListenerPrefix prefix)
		{
			if (coll == null)
			{
				return;
			}
			int count = coll.Count;
			for (int i = 0; i < count; i++)
			{
				ListenerPrefix listenerPrefix = (ListenerPrefix)coll[i];
				if (listenerPrefix.Path == prefix.Path)
				{
					coll.RemoveAt(i);
					this.CheckIfRemove();
					return;
				}
			}
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x00049A74 File Offset: 0x00047C74
		private void CheckIfRemove()
		{
			if (this.prefixes.Count > 0)
			{
				return;
			}
			if (this.unhandled != null && this.unhandled.Count > 0)
			{
				return;
			}
			if (this.all != null && this.all.Count > 0)
			{
				return;
			}
			EndPointManager.RemoveEndPoint(this, this.endpoint);
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00049ADC File Offset: 0x00047CDC
		public void Close()
		{
			this.sock.Close();
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x00049AEC File Offset: 0x00047CEC
		public void AddPrefix(ListenerPrefix prefix, HttpListener listener)
		{
			Hashtable obj = this.prefixes;
			lock (obj)
			{
				if (prefix.Host == "*")
				{
					if (this.unhandled == null)
					{
						this.unhandled = new ArrayList();
					}
					prefix.Listener = listener;
					this.AddSpecial(this.unhandled, prefix);
				}
				else if (prefix.Host == "+")
				{
					if (this.all == null)
					{
						this.all = new ArrayList();
					}
					prefix.Listener = listener;
					this.AddSpecial(this.all, prefix);
				}
				else if (this.prefixes.ContainsKey(prefix))
				{
					HttpListener httpListener = (HttpListener)this.prefixes[prefix];
					if (httpListener != listener)
					{
						throw new HttpListenerException(400, "There's another listener for " + prefix);
					}
				}
				else
				{
					this.prefixes[prefix] = listener;
				}
			}
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00049C08 File Offset: 0x00047E08
		public void RemovePrefix(ListenerPrefix prefix, HttpListener listener)
		{
			Hashtable obj = this.prefixes;
			lock (obj)
			{
				if (prefix.Host == "*")
				{
					this.RemoveSpecial(this.unhandled, prefix);
				}
				else if (prefix.Host == "+")
				{
					this.RemoveSpecial(this.all, prefix);
				}
				else if (this.prefixes.ContainsKey(prefix))
				{
					this.prefixes.Remove(prefix);
					this.CheckIfRemove();
				}
			}
		}

		// Token: 0x04001048 RID: 4168
		private IPEndPoint endpoint;

		// Token: 0x04001049 RID: 4169
		private System.Net.Sockets.Socket sock;

		// Token: 0x0400104A RID: 4170
		private Hashtable prefixes;

		// Token: 0x0400104B RID: 4171
		private ArrayList unhandled;

		// Token: 0x0400104C RID: 4172
		private ArrayList all;

		// Token: 0x0400104D RID: 4173
		private System.Security.Cryptography.X509Certificates.X509Certificate2 cert;

		// Token: 0x0400104E RID: 4174
		private AsymmetricAlgorithm key;

		// Token: 0x0400104F RID: 4175
		private bool secure;
	}
}
