using System;
using ManagedSteam.CallbackStructures;
using ManagedSteam.SteamTypes;

namespace ManagedSteam
{
	// Token: 0x02000073 RID: 115
	public interface IHTTP
	{
		// Token: 0x1400004B RID: 75
		// (add) Token: 0x0600039F RID: 927
		// (remove) Token: 0x060003A0 RID: 928
		event ResultEvent<HTTPRequestCompleted> HTTPRequestCompleted;

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x060003A1 RID: 929
		// (remove) Token: 0x060003A2 RID: 930
		event CallbackEvent<HTTPRequestHeadersReceived> HTTPRequestHeadersReceived;

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x060003A3 RID: 931
		// (remove) Token: 0x060003A4 RID: 932
		event CallbackEvent<HTTPRequestDataReceived> HTTPRequestDataReceived;

		// Token: 0x060003A5 RID: 933
		HTTPRequestHandle CreateHTTPRequest(HTTPMethod HTTPRequestMethod, string absoluteURL);

		// Token: 0x060003A6 RID: 934
		bool SetHTTPRequestContextValue(HTTPRequestHandle request, ulong contextValue);

		// Token: 0x060003A7 RID: 935
		bool SetHTTPRequestNetworkActivityTimeout(HTTPRequestHandle request, uint timeoutSeconds);

		// Token: 0x060003A8 RID: 936
		bool SetHTTPRequestHeaderValue(HTTPRequestHandle request, string headerName, string headerValue);

		// Token: 0x060003A9 RID: 937
		bool SetHTTPRequestGetOrPostParameter(HTTPRequestHandle request, string paramName, string paramValue);

		// Token: 0x060003AA RID: 938
		bool SendHTTPRequest(HTTPRequestHandle request, out SteamAPICall callHandle);

		// Token: 0x060003AB RID: 939
		HTTPSendHTTPRequest SendHTTPRequest(HTTPRequestHandle request);

		// Token: 0x060003AC RID: 940
		bool SendHTTPRequestAndStreamResponse(HTTPRequestHandle request, out SteamAPICall callHandle);

		// Token: 0x060003AD RID: 941
		HTTPSendHTTPRequestAndStreamResponse SendHTTPRequestAndStreamResponse(HTTPRequestHandle request);

		// Token: 0x060003AE RID: 942
		bool DeferHTTPRequest(HTTPRequestHandle request);

		// Token: 0x060003AF RID: 943
		bool PrioritizeHTTPRequest(HTTPRequestHandle request);

		// Token: 0x060003B0 RID: 944
		bool GetHTTPResponseHeaderSize(HTTPRequestHandle request, string headerName, out uint responseHeaderSize);

		// Token: 0x060003B1 RID: 945
		HTTPGetHTTPResponseHeaderSize GetHTTPResponseHeaderSize(HTTPRequestHandle request, string headerName);

		// Token: 0x060003B2 RID: 946
		bool GetHTTPResponseHeaderValue(HTTPRequestHandle request, string headerName, IntPtr headerValueBuffer, uint bufferSize);

		// Token: 0x060003B3 RID: 947
		bool GetHTTPResponseBodySize(HTTPRequestHandle request, out uint bodySize);

		// Token: 0x060003B4 RID: 948
		HTTPGetHTTPResponseBodySize GetHTTPResponseBodySize(HTTPRequestHandle request);

		// Token: 0x060003B5 RID: 949
		bool GetHTTPResponseBodyData(HTTPRequestHandle request, IntPtr bodyDataBuffer, uint bufferSize);

		// Token: 0x060003B6 RID: 950
		bool GetHTTPStreamingResponseBodyData(HTTPRequestHandle request, uint offset, IntPtr bodyDataBuffer, uint bufferSize);

		// Token: 0x060003B7 RID: 951
		bool ReleaseHTTPRequest(HTTPRequestHandle request);

		// Token: 0x060003B8 RID: 952
		bool GetHTTPDownloadProgressPct(HTTPRequestHandle request, out float percent);

		// Token: 0x060003B9 RID: 953
		HTTPGetHTTPDownloadProgressPct GetHTTPDownloadProgressPct(HTTPRequestHandle request);

		// Token: 0x060003BA RID: 954
		bool SetHTTPRequestRawPostBody(HTTPRequestHandle request, string contentType, IntPtr body, uint bodyLength);
	}
}
