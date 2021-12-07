using System;

namespace System.Net
{
	/// <summary>Defines status codes for the <see cref="T:System.Net.WebException" /> class.</summary>
	// Token: 0x02000418 RID: 1048
	public enum WebExceptionStatus
	{
		/// <summary>No error was encountered.</summary>
		// Token: 0x04001751 RID: 5969
		Success,
		/// <summary>The name resolver service could not resolve the host name.</summary>
		// Token: 0x04001752 RID: 5970
		NameResolutionFailure,
		/// <summary>The remote service point could not be contacted at the transport level.</summary>
		// Token: 0x04001753 RID: 5971
		ConnectFailure,
		/// <summary>A complete response was not received from the remote server.</summary>
		// Token: 0x04001754 RID: 5972
		ReceiveFailure,
		/// <summary>A complete request could not be sent to the remote server.</summary>
		// Token: 0x04001755 RID: 5973
		SendFailure,
		/// <summary>The request was a piplined request and the connection was closed before the response was received.</summary>
		// Token: 0x04001756 RID: 5974
		PipelineFailure,
		/// <summary>The request was canceled, the <see cref="M:System.Net.WebRequest.Abort" /> method was called, or an unclassifiable error occurred. This is the default value for <see cref="P:System.Net.WebException.Status" />.</summary>
		// Token: 0x04001757 RID: 5975
		RequestCanceled,
		/// <summary>The response received from the server was complete but indicated a protocol-level error. For example, an HTTP protocol error such as 401 Access Denied would use this status.</summary>
		// Token: 0x04001758 RID: 5976
		ProtocolError,
		/// <summary>The connection was prematurely closed.</summary>
		// Token: 0x04001759 RID: 5977
		ConnectionClosed,
		/// <summary>A server certificate could not be validated.</summary>
		// Token: 0x0400175A RID: 5978
		TrustFailure,
		/// <summary>An error occurred while establishing a connection using SSL.</summary>
		// Token: 0x0400175B RID: 5979
		SecureChannelFailure,
		/// <summary>The server response was not a valid HTTP response.</summary>
		// Token: 0x0400175C RID: 5980
		ServerProtocolViolation,
		/// <summary>The connection for a request that specifies the Keep-alive header was closed unexpectedly.</summary>
		// Token: 0x0400175D RID: 5981
		KeepAliveFailure,
		/// <summary>An internal asynchronous request is pending.</summary>
		// Token: 0x0400175E RID: 5982
		Pending,
		/// <summary>No response was received during the time-out period for a request.</summary>
		// Token: 0x0400175F RID: 5983
		Timeout,
		/// <summary>The name resolver service could not resolve the proxy host name.</summary>
		// Token: 0x04001760 RID: 5984
		ProxyNameResolutionFailure,
		/// <summary>An exception of unknown type has occurred.</summary>
		// Token: 0x04001761 RID: 5985
		UnknownError,
		/// <summary>A message was received that exceeded the specified limit when sending a request or receiving a response from the server.</summary>
		// Token: 0x04001762 RID: 5986
		MessageLengthLimitExceeded,
		/// <summary>The specified cache entry was not found.</summary>
		// Token: 0x04001763 RID: 5987
		CacheEntryNotFound,
		/// <summary>The request was not permitted by the cache policy. In general, this occurs when a request is not cacheable and the effective policy prohibits sending the request to the server. You might receive this status if a request method implies the presence of a request body, a request method requires direct interaction with the server, or a request contains a conditional header.</summary>
		// Token: 0x04001764 RID: 5988
		RequestProhibitedByCachePolicy,
		/// <summary>This request was not permitted by the proxy.</summary>
		// Token: 0x04001765 RID: 5989
		RequestProhibitedByProxy
	}
}
