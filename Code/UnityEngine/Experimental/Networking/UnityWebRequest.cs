using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace UnityEngine.Experimental.Networking
{
	// Token: 0x0200021C RID: 540
	[StructLayout(LayoutKind.Sequential)]
	public sealed class UnityWebRequest : IDisposable
	{
		// Token: 0x06002174 RID: 8564 RVA: 0x00029958 File Offset: 0x00027B58
		public UnityWebRequest()
		{
			this.InternalCreate();
			this.InternalSetDefaults();
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x0002996C File Offset: 0x00027B6C
		public UnityWebRequest(string url)
		{
			this.InternalCreate();
			this.InternalSetDefaults();
			this.url = url;
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x00029994 File Offset: 0x00027B94
		public UnityWebRequest(string url, string method)
		{
			this.InternalCreate();
			this.InternalSetDefaults();
			this.url = url;
			this.method = method;
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x000299C4 File Offset: 0x00027BC4
		public UnityWebRequest(string url, string method, DownloadHandler downloadHandler, UploadHandler uploadHandler)
		{
			this.InternalCreate();
			this.InternalSetDefaults();
			this.url = url;
			this.method = method;
			this.downloadHandler = downloadHandler;
			this.uploadHandler = uploadHandler;
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x00029AE8 File Offset: 0x00027CE8
		public static UnityWebRequest Get(string uri)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerBuffer(), null);
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x00029B08 File Offset: 0x00027D08
		public static UnityWebRequest Delete(string uri)
		{
			return new UnityWebRequest(uri, "DELETE");
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x00029B24 File Offset: 0x00027D24
		public static UnityWebRequest Head(string uri)
		{
			return new UnityWebRequest(uri, "HEAD");
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x00029B40 File Offset: 0x00027D40
		public static UnityWebRequest GetTexture(string uri)
		{
			return UnityWebRequest.GetTexture(uri, false);
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x00029B4C File Offset: 0x00027D4C
		public static UnityWebRequest GetTexture(string uri, bool nonReadable)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerTexture(nonReadable), null);
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x00029B70 File Offset: 0x00027D70
		public static UnityWebRequest GetAssetBundle(string uri)
		{
			return UnityWebRequest.GetAssetBundle(uri, 0U);
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x00029B7C File Offset: 0x00027D7C
		public static UnityWebRequest GetAssetBundle(string uri, uint crc)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAssetBundle(uri, crc), null);
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x00029BA0 File Offset: 0x00027DA0
		public static UnityWebRequest GetAssetBundle(string uri, uint version, uint crc)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAssetBundle(uri, version, crc), null);
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x00029BC4 File Offset: 0x00027DC4
		public static UnityWebRequest GetAssetBundle(string uri, Hash128 hash, uint crc)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerAssetBundle(uri, hash, crc), null);
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x00029BE8 File Offset: 0x00027DE8
		public static UnityWebRequest Put(string uri, byte[] bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(bodyData));
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x00029C10 File Offset: 0x00027E10
		public static UnityWebRequest Put(string uri, string bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(Encoding.UTF8.GetBytes(bodyData)));
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x00029C40 File Offset: 0x00027E40
		public static UnityWebRequest Post(string uri, string postData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			string s = WWWTranscoder.URLEncode(postData, Encoding.UTF8);
			unityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(s));
			unityWebRequest.uploadHandler.contentType = "application/x-www-form-urlencoded";
			unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
			return unityWebRequest;
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x00029C98 File Offset: 0x00027E98
		public static UnityWebRequest Post(string uri, WWWForm formData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			unityWebRequest.uploadHandler = new UploadHandlerRaw(formData.data);
			unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
			Dictionary<string, string> headers = formData.headers;
			foreach (KeyValuePair<string, string> keyValuePair in headers)
			{
				unityWebRequest.SetRequestHeader(keyValuePair.Key, keyValuePair.Value);
			}
			return unityWebRequest;
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x00029D38 File Offset: 0x00027F38
		public static UnityWebRequest Post(string uri, List<IMultipartFormSection> multipartFormSections)
		{
			byte[] boundary = UnityWebRequest.GenerateBoundary();
			return UnityWebRequest.Post(uri, multipartFormSections, boundary);
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x00029D54 File Offset: 0x00027F54
		public static UnityWebRequest Post(string uri, List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			byte[] data = UnityWebRequest.SerializeFormSections(multipartFormSections, boundary);
			unityWebRequest.uploadHandler = new UploadHandlerRaw(data)
			{
				contentType = "multipart/form-data; boundary=" + Encoding.UTF8.GetString(boundary, 0, boundary.Length)
			};
			unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
			return unityWebRequest;
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x00029DB0 File Offset: 0x00027FB0
		public static UnityWebRequest Post(string uri, Dictionary<string, string> formFields)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			byte[] data = UnityWebRequest.SerializeSimpleForm(formFields);
			unityWebRequest.uploadHandler = new UploadHandlerRaw(data)
			{
				contentType = "application/x-www-form-urlencoded"
			};
			unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
			return unityWebRequest;
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x00029DF8 File Offset: 0x00027FF8
		public static byte[] SerializeFormSections(List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			byte[] bytes = Encoding.UTF8.GetBytes("\r\n");
			int num = 0;
			foreach (IMultipartFormSection multipartFormSection in multipartFormSections)
			{
				num += 64 + multipartFormSection.sectionData.Length;
			}
			List<byte> list = new List<byte>(num);
			foreach (IMultipartFormSection multipartFormSection2 in multipartFormSections)
			{
				string str = "form-data";
				string sectionName = multipartFormSection2.sectionName;
				string fileName = multipartFormSection2.fileName;
				if (!string.IsNullOrEmpty(fileName))
				{
					str = "file";
				}
				string text = "Content-Disposition: " + str;
				if (!string.IsNullOrEmpty(sectionName))
				{
					text = text + "; name=\"" + sectionName + "\"";
				}
				if (!string.IsNullOrEmpty(fileName))
				{
					text = text + "; filename=\"" + fileName + "\"";
				}
				text += "\r\n";
				string contentType = multipartFormSection2.contentType;
				if (!string.IsNullOrEmpty(contentType))
				{
					text = text + "Content-Type: " + contentType + "\r\n";
				}
				list.AddRange(boundary);
				list.AddRange(bytes);
				list.AddRange(Encoding.UTF8.GetBytes(text));
				list.AddRange(bytes);
				list.AddRange(multipartFormSection2.sectionData);
			}
			list.TrimExcess();
			return list.ToArray();
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x00029FC4 File Offset: 0x000281C4
		public static byte[] GenerateBoundary()
		{
			byte[] array = new byte[40];
			for (int i = 0; i < 40; i++)
			{
				int num = Random.Range(48, 110);
				if (num > 57)
				{
					num += 7;
				}
				if (num > 90)
				{
					num += 6;
				}
				array[i] = (byte)num;
			}
			return array;
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x0002A014 File Offset: 0x00028214
		public static byte[] SerializeSimpleForm(Dictionary<string, string> formFields)
		{
			string text = string.Empty;
			foreach (KeyValuePair<string, string> keyValuePair in formFields)
			{
				if (text.Length > 0)
				{
					text += "&";
				}
				text = text + Uri.EscapeDataString(keyValuePair.Key) + "=" + Uri.EscapeDataString(keyValuePair.Value);
			}
			return Encoding.UTF8.GetBytes(text);
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x0002A0BC File Offset: 0x000282BC
		// (set) Token: 0x0600218D RID: 8589 RVA: 0x0002A0C4 File Offset: 0x000282C4
		public bool disposeDownloadHandlerOnDispose { get; set; }

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x0002A0D0 File Offset: 0x000282D0
		// (set) Token: 0x0600218F RID: 8591 RVA: 0x0002A0D8 File Offset: 0x000282D8
		public bool disposeUploadHandlerOnDispose { get; set; }

		// Token: 0x06002190 RID: 8592
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void InternalCreate();

		// Token: 0x06002191 RID: 8593
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void InternalDestroy();

		// Token: 0x06002192 RID: 8594 RVA: 0x0002A0E4 File Offset: 0x000282E4
		private void InternalSetDefaults()
		{
			this.disposeDownloadHandlerOnDispose = true;
			this.disposeUploadHandlerOnDispose = true;
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x0002A0F4 File Offset: 0x000282F4
		~UnityWebRequest()
		{
			this.InternalDestroy();
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x0002A130 File Offset: 0x00028330
		public void Dispose()
		{
			if (this.disposeDownloadHandlerOnDispose)
			{
				DownloadHandler downloadHandler = this.downloadHandler;
				if (downloadHandler != null)
				{
					downloadHandler.Dispose();
				}
			}
			if (this.disposeUploadHandlerOnDispose)
			{
				UploadHandler uploadHandler = this.uploadHandler;
				if (uploadHandler != null)
				{
					uploadHandler.Dispose();
				}
			}
			this.InternalDestroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002195 RID: 8597
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern AsyncOperation InternalBegin();

		// Token: 0x06002196 RID: 8598
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void InternalAbort();

		// Token: 0x06002197 RID: 8599 RVA: 0x0002A188 File Offset: 0x00028388
		public AsyncOperation Send()
		{
			return this.InternalBegin();
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x0002A190 File Offset: 0x00028390
		public void Abort()
		{
			this.InternalAbort();
		}

		// Token: 0x06002199 RID: 8601
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void InternalSetMethod(UnityWebRequest.UnityWebRequestMethod methodType);

		// Token: 0x0600219A RID: 8602
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void InternalSetCustomMethod(string customMethodName);

		// Token: 0x0600219B RID: 8603
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int InternalGetMethod();

		// Token: 0x0600219C RID: 8604
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string InternalGetCustomMethod();

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x0600219D RID: 8605 RVA: 0x0002A198 File Offset: 0x00028398
		// (set) Token: 0x0600219E RID: 8606 RVA: 0x0002A1E8 File Offset: 0x000283E8
		public string method
		{
			get
			{
				switch (this.InternalGetMethod())
				{
				case 0:
					return "GET";
				case 1:
					return "POST";
				case 2:
					return "PUT";
				case 3:
					return "HEAD";
				default:
					return this.InternalGetCustomMethod();
				}
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("Cannot set a UnityWebRequest's method to an empty or null string");
				}
				string text = value.ToUpper();
				switch (text)
				{
				case "GET":
					this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Get);
					return;
				case "POST":
					this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Post);
					return;
				case "PUT":
					this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Put);
					return;
				case "HEAD":
					this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Head);
					return;
				}
				this.InternalSetCustomMethod(value.ToUpper());
			}
		}

		// Token: 0x0600219F RID: 8607
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int InternalGetError();

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x060021A0 RID: 8608
		public extern string error { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060021A1 RID: 8609
		// (set) Token: 0x060021A2 RID: 8610
		public extern bool useHttpContinue { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x060021A3 RID: 8611 RVA: 0x0002A2D0 File Offset: 0x000284D0
		// (set) Token: 0x060021A4 RID: 8612 RVA: 0x0002A2D8 File Offset: 0x000284D8
		public string url
		{
			get
			{
				return this.InternalGetUrl();
			}
			set
			{
				string text = value;
				string uriString = "http://localhost/";
				Uri uri = new Uri(uriString);
				if (text.StartsWith("//"))
				{
					text = uri.Scheme + ":" + text;
				}
				if (text.StartsWith("/"))
				{
					text = uri.Scheme + "://" + uri.Host + text;
				}
				if (UnityWebRequest.domainRegex.IsMatch(text))
				{
					text = uri.Scheme + "://" + text;
				}
				Uri uri2 = null;
				try
				{
					uri2 = new Uri(text);
				}
				catch (FormatException ex)
				{
					try
					{
						uri2 = new Uri(uri, text);
					}
					catch (FormatException)
					{
						throw ex;
					}
				}
				this.InternalSetUrl(uri2.AbsoluteUri);
			}
		}

		// Token: 0x060021A5 RID: 8613
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string InternalGetUrl();

		// Token: 0x060021A6 RID: 8614
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetUrl(string url);

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x060021A7 RID: 8615
		public extern long responseCode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x060021A8 RID: 8616
		public extern float uploadProgress { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x060021A9 RID: 8617
		public extern bool isModifiable { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060021AA RID: 8618
		public extern bool isDone { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060021AB RID: 8619
		public extern bool isError { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060021AC RID: 8620
		public extern float downloadProgress { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x060021AD RID: 8621
		public extern ulong uploadedBytes { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x060021AE RID: 8622
		public extern ulong downloadedBytes { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x060021AF RID: 8623
		// (set) Token: 0x060021B0 RID: 8624
		public extern int redirectLimit { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x060021B1 RID: 8625
		// (set) Token: 0x060021B2 RID: 8626
		public extern bool chunkedTransfer { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060021B3 RID: 8627
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetRequestHeader(string name);

		// Token: 0x060021B4 RID: 8628
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void InternalSetRequestHeader(string name, string value);

		// Token: 0x060021B5 RID: 8629 RVA: 0x0002A3CC File Offset: 0x000285CC
		public void SetRequestHeader(string name, string value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Cannot set a Request Header with a null or empty name");
			}
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException("Cannot set a Request header with a null or empty value");
			}
			if (!UnityWebRequest.IsHeaderNameLegal(name))
			{
				throw new ArgumentException("Cannot set Request Header " + name + " - name contains illegal characters or is not user-overridable");
			}
			if (!UnityWebRequest.IsHeaderValueLegal(value))
			{
				throw new ArgumentException("Cannot set Request Header - value contains illegal characters");
			}
			this.InternalSetRequestHeader(name, value);
		}

		// Token: 0x060021B6 RID: 8630
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetResponseHeader(string name);

		// Token: 0x060021B7 RID: 8631
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string[] InternalGetResponseHeaderKeys();

		// Token: 0x060021B8 RID: 8632 RVA: 0x0002A444 File Offset: 0x00028644
		public Dictionary<string, string> GetResponseHeaders()
		{
			string[] array = this.InternalGetResponseHeaderKeys();
			if (array == null)
			{
				return null;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				string responseHeader = this.GetResponseHeader(array[i]);
				dictionary.Add(array[i], responseHeader);
			}
			return dictionary;
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x060021B9 RID: 8633
		// (set) Token: 0x060021BA RID: 8634
		public extern UploadHandler uploadHandler { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x060021BB RID: 8635
		// (set) Token: 0x060021BC RID: 8636
		public extern DownloadHandler downloadHandler { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060021BD RID: 8637 RVA: 0x0002A494 File Offset: 0x00028694
		private static bool ContainsForbiddenCharacters(string s, int firstAllowedCharCode)
		{
			foreach (char c in s)
			{
				if ((int)c < firstAllowedCharCode || c == '\u007f')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x0002A4D4 File Offset: 0x000286D4
		private static bool IsHeaderNameLegal(string headerName)
		{
			if (string.IsNullOrEmpty(headerName))
			{
				return false;
			}
			headerName = headerName.ToLower();
			if (UnityWebRequest.ContainsForbiddenCharacters(headerName, 33))
			{
				return false;
			}
			if (headerName.StartsWith("sec-") || headerName.StartsWith("proxy-"))
			{
				return false;
			}
			foreach (string b in UnityWebRequest.forbiddenHeaderKeys)
			{
				if (string.Equals(headerName, b))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x0002A554 File Offset: 0x00028754
		private static bool IsHeaderValueLegal(string headerValue)
		{
			return !string.IsNullOrEmpty(headerValue) && !UnityWebRequest.ContainsForbiddenCharacters(headerValue, 32);
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x0002A574 File Offset: 0x00028774
		private static string GetErrorDescription(UnityWebRequest.UnityWebRequestError errorCode)
		{
			switch (errorCode)
			{
			case UnityWebRequest.UnityWebRequestError.OK:
				return "No Error";
			case UnityWebRequest.UnityWebRequestError.SDKError:
				return "Internal Error With Transport Layer";
			case UnityWebRequest.UnityWebRequestError.UnsupportedProtocol:
				return "Specified Transport Protocol is Unsupported";
			case UnityWebRequest.UnityWebRequestError.MalformattedUrl:
				return "URL is Malformatted";
			case UnityWebRequest.UnityWebRequestError.CannotResolveProxy:
				return "Unable to resolve specified proxy server";
			case UnityWebRequest.UnityWebRequestError.CannotResolveHost:
				return "Unable to resolve host specified in URL";
			case UnityWebRequest.UnityWebRequestError.CannotConnectToHost:
				return "Unable to connect to host specified in URL";
			case UnityWebRequest.UnityWebRequestError.AccessDenied:
				return "Remote server denied access to the specified URL";
			case UnityWebRequest.UnityWebRequestError.GenericHTTPError:
				return "Unknown/Generic HTTP Error - Check HTTP Error code";
			case UnityWebRequest.UnityWebRequestError.WriteError:
				return "Error when transmitting request to remote server - transmission terminated prematurely";
			case UnityWebRequest.UnityWebRequestError.ReadError:
				return "Error when reading response from remote server - transmission terminated prematurely";
			case UnityWebRequest.UnityWebRequestError.OutOfMemory:
				return "Out of Memory";
			case UnityWebRequest.UnityWebRequestError.Timeout:
				return "Timeout occurred while waiting for response from remote server";
			case UnityWebRequest.UnityWebRequestError.HTTPPostError:
				return "Error while transmitting HTTP POST body data";
			case UnityWebRequest.UnityWebRequestError.SSLCannotConnect:
				return "Unable to connect to SSL server at remote host";
			case UnityWebRequest.UnityWebRequestError.Aborted:
				return "Request was manually aborted by local code";
			case UnityWebRequest.UnityWebRequestError.TooManyRedirects:
				return "Redirect limit exceeded";
			case UnityWebRequest.UnityWebRequestError.ReceivedNoData:
				return "Received an empty response from remote host";
			case UnityWebRequest.UnityWebRequestError.SSLNotSupported:
				return "SSL connections are not supported on the local machine";
			case UnityWebRequest.UnityWebRequestError.FailedToSendData:
				return "Failed to transmit body data";
			case UnityWebRequest.UnityWebRequestError.FailedToReceiveData:
				return "Failed to receive response body data";
			case UnityWebRequest.UnityWebRequestError.SSLCertificateError:
				return "Failure to authenticate SSL certificate of remote host";
			case UnityWebRequest.UnityWebRequestError.SSLCipherNotAvailable:
				return "SSL cipher received from remote host is not supported on the local machine";
			case UnityWebRequest.UnityWebRequestError.SSLCACertError:
				return "Failure to authenticate Certificate Authority of the SSL certificate received from the remote host";
			case UnityWebRequest.UnityWebRequestError.UnrecognizedContentEncoding:
				return "Remote host returned data with an unrecognized/unparseable content encoding";
			case UnityWebRequest.UnityWebRequestError.LoginFailed:
				return "HTTP authentication failed";
			case UnityWebRequest.UnityWebRequestError.SSLShutdownFailed:
				return "Failure while shutting down SSL connection";
			}
			return "Unknown error";
		}

		// Token: 0x040008A9 RID: 2217
		public const string kHttpVerbGET = "GET";

		// Token: 0x040008AA RID: 2218
		public const string kHttpVerbHEAD = "HEAD";

		// Token: 0x040008AB RID: 2219
		public const string kHttpVerbPOST = "POST";

		// Token: 0x040008AC RID: 2220
		public const string kHttpVerbPUT = "PUT";

		// Token: 0x040008AD RID: 2221
		public const string kHttpVerbCREATE = "CREATE";

		// Token: 0x040008AE RID: 2222
		public const string kHttpVerbDELETE = "DELETE";

		// Token: 0x040008AF RID: 2223
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x040008B0 RID: 2224
		private static Regex domainRegex = new Regex("^\\s*\\w+(?:\\.\\w+)+\\s*$");

		// Token: 0x040008B1 RID: 2225
		private static readonly string[] forbiddenHeaderKeys = new string[]
		{
			"accept-charset",
			"accept-encoding",
			"access-control-request-headers",
			"access-control-request-method",
			"connection",
			"content-length",
			"cookie",
			"cookie2",
			"date",
			"dnt",
			"expect",
			"host",
			"keep-alive",
			"origin",
			"referer",
			"te",
			"trailer",
			"transfer-encoding",
			"upgrade",
			"user-agent",
			"via",
			"x-unity-version"
		};

		// Token: 0x0200021D RID: 541
		internal enum UnityWebRequestMethod
		{
			// Token: 0x040008B6 RID: 2230
			Get,
			// Token: 0x040008B7 RID: 2231
			Post,
			// Token: 0x040008B8 RID: 2232
			Put,
			// Token: 0x040008B9 RID: 2233
			Head,
			// Token: 0x040008BA RID: 2234
			Custom
		}

		// Token: 0x0200021E RID: 542
		internal enum UnityWebRequestError
		{
			// Token: 0x040008BC RID: 2236
			OK,
			// Token: 0x040008BD RID: 2237
			Unknown,
			// Token: 0x040008BE RID: 2238
			SDKError,
			// Token: 0x040008BF RID: 2239
			UnsupportedProtocol,
			// Token: 0x040008C0 RID: 2240
			MalformattedUrl,
			// Token: 0x040008C1 RID: 2241
			CannotResolveProxy,
			// Token: 0x040008C2 RID: 2242
			CannotResolveHost,
			// Token: 0x040008C3 RID: 2243
			CannotConnectToHost,
			// Token: 0x040008C4 RID: 2244
			AccessDenied,
			// Token: 0x040008C5 RID: 2245
			GenericHTTPError,
			// Token: 0x040008C6 RID: 2246
			WriteError,
			// Token: 0x040008C7 RID: 2247
			ReadError,
			// Token: 0x040008C8 RID: 2248
			OutOfMemory,
			// Token: 0x040008C9 RID: 2249
			Timeout,
			// Token: 0x040008CA RID: 2250
			HTTPPostError,
			// Token: 0x040008CB RID: 2251
			SSLCannotConnect,
			// Token: 0x040008CC RID: 2252
			Aborted,
			// Token: 0x040008CD RID: 2253
			TooManyRedirects,
			// Token: 0x040008CE RID: 2254
			ReceivedNoData,
			// Token: 0x040008CF RID: 2255
			SSLNotSupported,
			// Token: 0x040008D0 RID: 2256
			FailedToSendData,
			// Token: 0x040008D1 RID: 2257
			FailedToReceiveData,
			// Token: 0x040008D2 RID: 2258
			SSLCertificateError,
			// Token: 0x040008D3 RID: 2259
			SSLCipherNotAvailable,
			// Token: 0x040008D4 RID: 2260
			SSLCACertError,
			// Token: 0x040008D5 RID: 2261
			UnrecognizedContentEncoding,
			// Token: 0x040008D6 RID: 2262
			LoginFailed,
			// Token: 0x040008D7 RID: 2263
			SSLShutdownFailed
		}
	}
}
