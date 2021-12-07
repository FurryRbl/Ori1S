﻿using System;

namespace System.Net
{
	/// <summary>The HTTP headers that may be specified in a client request.</summary>
	// Token: 0x0200031C RID: 796
	public enum HttpRequestHeader
	{
		/// <summary>The Cache-Control header, which specifies directives that must be obeyed by all cache control mechanisms along the request/response chain.</summary>
		// Token: 0x0400113C RID: 4412
		CacheControl,
		/// <summary>The Connection header, which specifies options that are desired for a particular connection.</summary>
		// Token: 0x0400113D RID: 4413
		Connection,
		/// <summary>The Date header, which specifies the date and time at which the request originated.</summary>
		// Token: 0x0400113E RID: 4414
		Date,
		/// <summary>The Keep-Alive header, which specifies a parameter used into order to maintain a persistent connection.</summary>
		// Token: 0x0400113F RID: 4415
		KeepAlive,
		/// <summary>The Pragma header, which specifies implementation-specific directives that might apply to any agent along the request/response chain.</summary>
		// Token: 0x04001140 RID: 4416
		Pragma,
		/// <summary>The Trailer header, which specifies the header fields present in the trailer of a message encoded with chunked transfer-coding.</summary>
		// Token: 0x04001141 RID: 4417
		Trailer,
		/// <summary>The Transfer-Encoding header, which specifies what (if any) type of transformation that has been applied to the message body.</summary>
		// Token: 0x04001142 RID: 4418
		TransferEncoding,
		/// <summary>The Upgrade header, which specifies additional communications protocols that the client supports.</summary>
		// Token: 0x04001143 RID: 4419
		Upgrade,
		/// <summary>The Via header, which specifies intermediate protocols to be used by gateway and proxy agents.</summary>
		// Token: 0x04001144 RID: 4420
		Via,
		/// <summary>The Warning header, which specifies additional information about that status or transformation of a message that might not be reflected in the message.</summary>
		// Token: 0x04001145 RID: 4421
		Warning,
		/// <summary>The Allow header, which specifies the set of HTTP methods supported.</summary>
		// Token: 0x04001146 RID: 4422
		Allow,
		/// <summary>The Content-Length header, which specifies the length, in bytes, of the accompanying body data.</summary>
		// Token: 0x04001147 RID: 4423
		ContentLength,
		/// <summary>The Content-Type header, which specifies the MIME type of the accompanying body data.</summary>
		// Token: 0x04001148 RID: 4424
		ContentType,
		/// <summary>The Content-Encoding header, which specifies the encodings that have been applied to the accompanying body data.</summary>
		// Token: 0x04001149 RID: 4425
		ContentEncoding,
		/// <summary>The Content-Langauge header, which specifies the natural language(s) of the accompanying body data.</summary>
		// Token: 0x0400114A RID: 4426
		ContentLanguage,
		/// <summary>The Content-Location header, which specifies a URI from which the accompanying body may be obtained.</summary>
		// Token: 0x0400114B RID: 4427
		ContentLocation,
		/// <summary>The Content-MD5 header, which specifies the MD5 digest of the accompanying body data, for the purpose of providing an end-to-end message integrity check.</summary>
		// Token: 0x0400114C RID: 4428
		ContentMd5,
		/// <summary>The Content-Range header, which specifies where in the full body the accompanying partial body data should be applied.</summary>
		// Token: 0x0400114D RID: 4429
		ContentRange,
		/// <summary>The Expires header, which specifies the date and time after which the accompanying body data should be considered stale.</summary>
		// Token: 0x0400114E RID: 4430
		Expires,
		/// <summary>The Last-Modified header, which specifies the date and time at which the accompanying body data was last modified.</summary>
		// Token: 0x0400114F RID: 4431
		LastModified,
		/// <summary>The Accept header, which specifies the MIME types that are acceptable for the response.</summary>
		// Token: 0x04001150 RID: 4432
		Accept,
		/// <summary>The Accept-Charset header, which specifies the character sets that are acceptable for the response.</summary>
		// Token: 0x04001151 RID: 4433
		AcceptCharset,
		/// <summary>The Accept-Encoding header, which specifies the content encodings that are acceptable for the response.</summary>
		// Token: 0x04001152 RID: 4434
		AcceptEncoding,
		/// <summary>The Accept-Langauge header, which specifies that natural languages that are preferred for the response.</summary>
		// Token: 0x04001153 RID: 4435
		AcceptLanguage,
		/// <summary>The Authorization header, which specifies the credentials that the client presents in order to authenticate itself to the server.</summary>
		// Token: 0x04001154 RID: 4436
		Authorization,
		/// <summary>The Cookie header, which specifies cookie data presented to the server.</summary>
		// Token: 0x04001155 RID: 4437
		Cookie,
		/// <summary>The Expect header, which specifies particular server behaviors that are required by the client.</summary>
		// Token: 0x04001156 RID: 4438
		Expect,
		/// <summary>The From header, which specifies an Internet E-mail address for the human user who controls the requesting user agent.</summary>
		// Token: 0x04001157 RID: 4439
		From,
		/// <summary>The Host header, which specifies the host name and port number of the resource being requested.</summary>
		// Token: 0x04001158 RID: 4440
		Host,
		/// <summary>The If-Match header, which specifies that the requested operation should be performed only if the client's cached copy of the indicated resource is current.</summary>
		// Token: 0x04001159 RID: 4441
		IfMatch,
		/// <summary>The If-Modified-Since header, which specifies that the requested operation should be performed only if the requested resource has been modified since the indicated data and time.</summary>
		// Token: 0x0400115A RID: 4442
		IfModifiedSince,
		/// <summary>The If-None-Match header, which specifies that the requested operation should be performed only if none of client's cached copies of the indicated resources are current.</summary>
		// Token: 0x0400115B RID: 4443
		IfNoneMatch,
		/// <summary>The If-Range header, which specifies that only the specified range of the requested resource should be sent, if the client's cached copy is current.</summary>
		// Token: 0x0400115C RID: 4444
		IfRange,
		/// <summary>The If-Unmodified-Since header, which specifies that the requested operation should be performed only if the requested resource has not been modified since the indicated date and time.</summary>
		// Token: 0x0400115D RID: 4445
		IfUnmodifiedSince,
		/// <summary>The Max-Forwards header, which specifies an integer indicating the remaining number of times that this request may be forwarded.</summary>
		// Token: 0x0400115E RID: 4446
		MaxForwards,
		/// <summary>The Proxy-Authorization header, which specifies the credentials that the client presents in order to authenticate itself to a proxy.</summary>
		// Token: 0x0400115F RID: 4447
		ProxyAuthorization,
		/// <summary>The Referer header, which specifies the URI of the resource from which the request URI was obtained.</summary>
		// Token: 0x04001160 RID: 4448
		Referer,
		/// <summary>The Range header, which specifies the the sub-range(s) of the response that the client requests be returned in lieu of the entire response.</summary>
		// Token: 0x04001161 RID: 4449
		Range,
		/// <summary>The TE header, which specifies the transfer encodings that are acceptable for the response.</summary>
		// Token: 0x04001162 RID: 4450
		Te,
		/// <summary>The Translate header, a Microsoft extension to the HTTP specification used in conjunction with WebDAV functionality.</summary>
		// Token: 0x04001163 RID: 4451
		Translate,
		/// <summary>The User-Agent header, which specifies information about the client agent.</summary>
		// Token: 0x04001164 RID: 4452
		UserAgent
	}
}
