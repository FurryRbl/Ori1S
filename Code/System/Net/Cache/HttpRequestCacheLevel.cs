﻿using System;

namespace System.Net.Cache
{
	/// <summary>Specifies caching behavior for resources obtained using the Hypertext Transfer protocol (HTTP).</summary>
	// Token: 0x020002BE RID: 702
	public enum HttpRequestCacheLevel
	{
		/// <summary>Satisfies a request for a resource either by using the cached copy of the resource or by sending a request for the resource to the server. The action taken is determined by the current cache policy and the age of the content in the cache. This is the cache level that should be used by most applications.</summary>
		// Token: 0x04000F81 RID: 3969
		Default,
		/// <summary>Satisfies a request by using the server. No entries are taken from caches, added to caches, or removed from caches between the client and server. This is the default cache behavior specified in the machine configuration file that ships with the .NET Framework.</summary>
		// Token: 0x04000F82 RID: 3970
		BypassCache,
		/// <summary>Satisfies a request using the locally cached resource; does not send a request for an item that is not in the cache. When this cache policy level is specified, a <see cref="T:System.Net.WebException" /> exception is thrown if the item is not in the client cache.</summary>
		// Token: 0x04000F83 RID: 3971
		CacheOnly,
		/// <summary>Satisfies a request for a resource from the cache if the resource is available; otherwise, sends a request for a resource to the server. If the requested item is available in any cache between the client and the server, the request might be satisfied by the intermediate cache.</summary>
		// Token: 0x04000F84 RID: 3972
		CacheIfAvailable,
		/// <summary>Compares the copy of the resource in the cache with the copy on the server. If the copy on the server is newer, it is used to satisfy the request and replaces the copy in the cache. If the copy in the cache is the same as the server copy, the cached copy is used. In the HTTP caching protocol, this is achieved using a conditional request.</summary>
		// Token: 0x04000F85 RID: 3973
		Revalidate,
		/// <summary>Satisfies a request by using the server. The response might be saved in the cache. In the HTTP caching protocol, this is achieved using the no-cache cache control directive and the no-cache Pragma header.</summary>
		// Token: 0x04000F86 RID: 3974
		Reload,
		/// <summary>Never satisfies a request by using resources from the cache and does not cache resources. If the resource is present in the local cache, it is removed. This policy level indicates to intermediate caches that they should remove the resource. In the HTTP caching protocol, this is achieved using the no-cache cache control directive.</summary>
		// Token: 0x04000F87 RID: 3975
		NoCacheNoStore,
		/// <summary>Satisfies a request for a resource either from the local computer's cache or a remote cache on the local area network. If the request cannot be satisfied, a <see cref="T:System.Net.WebException" /> exception is thrown. In the HTTP caching protocol, this is achieved using the only-if-cached cache control directive.</summary>
		// Token: 0x04000F88 RID: 3976
		CacheOrNextCacheOnly,
		/// <summary>Satisfies a request by using the server or a cache other than the local cache. Before the request can be satisfied by an intermediate cache, that cache must revalidate its cached entry with the server. In the HTTP caching protocol, this is achieved using the max-age = 0 cache control directive and the no-cache Pragma header.</summary>
		// Token: 0x04000F89 RID: 3977
		Refresh
	}
}
