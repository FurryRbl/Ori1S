using System;
using System.Collections.Generic;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;
using ManagedSteam.Utility;

namespace ManagedSteam.Implementations
{
	// Token: 0x02000074 RID: 116
	internal class HTTP : SteamService, IHTTP
	{
		// Token: 0x060003BB RID: 955 RVA: 0x0000706C File Offset: 0x0000526C
		public HTTP()
		{
			this.httpRequestCompleted = new List<SteamService.Result<HTTPRequestCompleted>>();
			this.httpRequestHeadersReceived = new List<HTTPRequestHeadersReceived>();
			this.httpRequestDataReceived = new List<HTTPRequestDataReceived>();
			SteamService.Results[ResultID.HTTPRequestCompleted] = delegate(IntPtr data, int dataSize, bool flag)
			{
				this.httpRequestCompleted.Add(new SteamService.Result<HTTPRequestCompleted>(ManagedSteam.CallbackStructures.HTTPRequestCompleted.Create(data, dataSize), flag));
			};
			SteamService.Callbacks[CallbackID.HTTPRequestHeadersReceived] = delegate(IntPtr data, int dataSize)
			{
				this.httpRequestHeadersReceived.Add(ManagedSteam.CallbackStructures.HTTPRequestHeadersReceived.Create(data, dataSize));
			};
			SteamService.Callbacks[CallbackID.HTTPRequestDataReceived] = delegate(IntPtr data, int dataSize)
			{
				this.httpRequestDataReceived.Add(ManagedSteam.CallbackStructures.HTTPRequestDataReceived.Create(data, dataSize));
			};
		}

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x060003BC RID: 956 RVA: 0x00007100 File Offset: 0x00005300
		// (remove) Token: 0x060003BD RID: 957 RVA: 0x00007138 File Offset: 0x00005338
		public event ResultEvent<HTTPRequestCompleted> HTTPRequestCompleted;

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x060003BE RID: 958 RVA: 0x00007170 File Offset: 0x00005370
		// (remove) Token: 0x060003BF RID: 959 RVA: 0x000071A8 File Offset: 0x000053A8
		public event CallbackEvent<HTTPRequestHeadersReceived> HTTPRequestHeadersReceived;

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x060003C0 RID: 960 RVA: 0x000071E0 File Offset: 0x000053E0
		// (remove) Token: 0x060003C1 RID: 961 RVA: 0x00007218 File Offset: 0x00005418
		public event CallbackEvent<HTTPRequestDataReceived> HTTPRequestDataReceived;

		// Token: 0x060003C2 RID: 962 RVA: 0x0000724D File Offset: 0x0000544D
		internal override void CheckIfUsableInternal()
		{
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000724F File Offset: 0x0000544F
		internal override void ReleaseManagedResources()
		{
			this.httpRequestCompleted = null;
			this.HTTPRequestCompleted = null;
			this.httpRequestHeadersReceived = null;
			this.HTTPRequestHeadersReceived = null;
			this.httpRequestDataReceived = null;
			this.HTTPRequestDataReceived = null;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000727B File Offset: 0x0000547B
		internal override void InvokeEvents()
		{
			SteamService.InvokeEvents<HTTPRequestCompleted>(this.httpRequestCompleted, this.HTTPRequestCompleted);
			SteamService.InvokeEvents<HTTPRequestHeadersReceived>(this.httpRequestHeadersReceived, this.HTTPRequestHeadersReceived);
			SteamService.InvokeEvents<HTTPRequestDataReceived>(this.httpRequestDataReceived, this.HTTPRequestDataReceived);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x000072B0 File Offset: 0x000054B0
		public HTTPRequestHandle CreateHTTPRequest(HTTPMethod HTTPRequestMethod, string absoluteURL)
		{
			base.CheckIfUsable();
			HTTPRequestHandle result;
			using (NativeString nativeString = new NativeString(absoluteURL))
			{
				result = new HTTPRequestHandle(NativeMethods.HTTP_CreateHTTPRequest((int)HTTPRequestMethod, nativeString.ToNativeAsUtf8()));
			}
			return result;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000072FC File Offset: 0x000054FC
		public bool SetHTTPRequestContextValue(HTTPRequestHandle request, ulong contextValue)
		{
			base.CheckIfUsable();
			return NativeMethods.HTTP_SetHTTPRequestContextValue(request.AsUInt32, contextValue);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00007311 File Offset: 0x00005511
		public bool SetHTTPRequestNetworkActivityTimeout(HTTPRequestHandle request, uint timeoutSeconds)
		{
			base.CheckIfUsable();
			return NativeMethods.HTTP_SetHTTPRequestNetworkActivityTimeout(request.AsUInt32, timeoutSeconds);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00007328 File Offset: 0x00005528
		public bool SetHTTPRequestHeaderValue(HTTPRequestHandle request, string headerName, string headerValue)
		{
			base.CheckIfUsable();
			bool result;
			using (NativeString nativeString = new NativeString(headerName))
			{
				using (NativeString nativeString2 = new NativeString(headerValue))
				{
					result = NativeMethods.HTTP_SetHTTPRequestHeaderValue(request.AsUInt32, nativeString.ToNativeAsUtf8(), nativeString2.ToNativeAsUtf8());
				}
			}
			return result;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00007398 File Offset: 0x00005598
		public bool SetHTTPRequestGetOrPostParameter(HTTPRequestHandle request, string paramName, string paramValue)
		{
			base.CheckIfUsable();
			bool result;
			using (NativeString nativeString = new NativeString(paramName))
			{
				using (NativeString nativeString2 = new NativeString(paramValue))
				{
					result = NativeMethods.HTTP_SetHTTPRequestGetOrPostParameter(request.AsUInt32, nativeString.ToNativeAsUtf8(), nativeString2.ToNativeAsUtf8());
				}
			}
			return result;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00007408 File Offset: 0x00005608
		public bool SendHTTPRequest(HTTPRequestHandle request, out SteamAPICall callHandle)
		{
			base.CheckIfUsable();
			ulong value = 0UL;
			bool result = NativeMethods.HTTP_SendHTTPRequest(request.AsUInt32, ref value);
			callHandle = new SteamAPICall(value);
			return result;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000743C File Offset: 0x0000563C
		public HTTPSendHTTPRequest SendHTTPRequest(HTTPRequestHandle request)
		{
			HTTPSendHTTPRequest result = default(HTTPSendHTTPRequest);
			result.Result = this.SendHTTPRequest(request, out result.CallHandle);
			return result;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00007468 File Offset: 0x00005668
		public bool SendHTTPRequestAndStreamResponse(HTTPRequestHandle request, out SteamAPICall callHandle)
		{
			base.CheckIfUsable();
			ulong value = 0UL;
			bool result = NativeMethods.HTTP_SendHTTPRequestAndStreamResponse(request.AsUInt32, ref value);
			callHandle = new SteamAPICall(value);
			return result;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000749C File Offset: 0x0000569C
		public HTTPSendHTTPRequestAndStreamResponse SendHTTPRequestAndStreamResponse(HTTPRequestHandle request)
		{
			HTTPSendHTTPRequestAndStreamResponse result = default(HTTPSendHTTPRequestAndStreamResponse);
			result.Result = this.SendHTTPRequestAndStreamResponse(request, out result.CallHandle);
			return result;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000074C7 File Offset: 0x000056C7
		public bool DeferHTTPRequest(HTTPRequestHandle request)
		{
			base.CheckIfUsable();
			return NativeMethods.HTTP_DeferHTTPRequest(request.AsUInt32);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000074DB File Offset: 0x000056DB
		public bool PrioritizeHTTPRequest(HTTPRequestHandle request)
		{
			base.CheckIfUsable();
			return NativeMethods.HTTP_PrioritizeHTTPRequest(request.AsUInt32);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000074F0 File Offset: 0x000056F0
		public bool GetHTTPResponseHeaderSize(HTTPRequestHandle request, string headerName, out uint responseHeaderSize)
		{
			base.CheckIfUsable();
			responseHeaderSize = 0U;
			bool result;
			using (NativeString nativeString = new NativeString(headerName))
			{
				result = NativeMethods.HTTP_GetHTTPResponseHeaderSize(request.AsUInt32, nativeString.ToNativeAsUtf8(), ref responseHeaderSize);
			}
			return result;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00007540 File Offset: 0x00005740
		public HTTPGetHTTPResponseHeaderSize GetHTTPResponseHeaderSize(HTTPRequestHandle request, string headerName)
		{
			HTTPGetHTTPResponseHeaderSize result = default(HTTPGetHTTPResponseHeaderSize);
			result.Result = this.GetHTTPResponseHeaderSize(request, headerName, out result.ResponseHeaderSize);
			return result;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000756C File Offset: 0x0000576C
		public bool GetHTTPResponseHeaderValue(HTTPRequestHandle request, string headerName, IntPtr headerValueBuffer, uint bufferSize)
		{
			base.CheckIfUsable();
			bool result;
			using (NativeString nativeString = new NativeString(headerName))
			{
				result = NativeMethods.HTTP_GetHTTPResponseHeaderValue(request.AsUInt32, nativeString.ToNativeAsUtf8(), headerValueBuffer, bufferSize);
			}
			return result;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000075BC File Offset: 0x000057BC
		public bool GetHTTPResponseBodySize(HTTPRequestHandle request, out uint bodySize)
		{
			base.CheckIfUsable();
			bodySize = 0U;
			return NativeMethods.HTTP_GetHTTPResponseBodySize(request.AsUInt32, ref bodySize);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000075D4 File Offset: 0x000057D4
		public HTTPGetHTTPResponseBodySize GetHTTPResponseBodySize(HTTPRequestHandle request)
		{
			HTTPGetHTTPResponseBodySize result = default(HTTPGetHTTPResponseBodySize);
			result.Result = this.GetHTTPResponseBodySize(request, out result.BodySize);
			return result;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000075FF File Offset: 0x000057FF
		public bool GetHTTPResponseBodyData(HTTPRequestHandle request, IntPtr bodyDataBuffer, uint bufferSize)
		{
			base.CheckIfUsable();
			return NativeMethods.HTTP_GetHTTPResponseBodyData(request.AsUInt32, bodyDataBuffer, bufferSize);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00007615 File Offset: 0x00005815
		public bool GetHTTPStreamingResponseBodyData(HTTPRequestHandle request, uint offset, IntPtr bodyDataBuffer, uint bufferSize)
		{
			base.CheckIfUsable();
			return NativeMethods.HTTP_GetHTTPStreamingResponseBodyData(request.AsUInt32, offset, bodyDataBuffer, bufferSize);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000762D File Offset: 0x0000582D
		public bool ReleaseHTTPRequest(HTTPRequestHandle request)
		{
			base.CheckIfUsable();
			return NativeMethods.HTTP_ReleaseHTTPRequest(request.AsUInt32);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00007641 File Offset: 0x00005841
		public bool GetHTTPDownloadProgressPct(HTTPRequestHandle request, out float percent)
		{
			base.CheckIfUsable();
			percent = 0f;
			return NativeMethods.HTTP_GetHTTPDownloadProgressPct(request.AsUInt32, ref percent);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00007660 File Offset: 0x00005860
		public HTTPGetHTTPDownloadProgressPct GetHTTPDownloadProgressPct(HTTPRequestHandle request)
		{
			HTTPGetHTTPDownloadProgressPct result = default(HTTPGetHTTPDownloadProgressPct);
			result.Result = this.GetHTTPDownloadProgressPct(request, out result.Percent);
			return result;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000768C File Offset: 0x0000588C
		public bool SetHTTPRequestRawPostBody(HTTPRequestHandle request, string contentType, IntPtr body, uint bodyLength)
		{
			base.CheckIfUsable();
			bool result;
			using (NativeString nativeString = new NativeString(contentType))
			{
				result = NativeMethods.HTTP_SetHTTPRequestRawPostBody(request.AsUInt32, nativeString.ToNativeAsUtf8(), body, bodyLength);
			}
			return result;
		}

		// Token: 0x04000201 RID: 513
		private List<SteamService.Result<HTTPRequestCompleted>> httpRequestCompleted;

		// Token: 0x04000202 RID: 514
		private List<HTTPRequestHeadersReceived> httpRequestHeadersReceived;

		// Token: 0x04000203 RID: 515
		private List<HTTPRequestDataReceived> httpRequestDataReceived;
	}
}
