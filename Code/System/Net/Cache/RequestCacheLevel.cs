using System;

namespace System.Net.Cache
{
	/// <summary>Specifies caching behavior for resources obtained using <see cref="T:System.Net.WebRequest" /> and its derived classes.</summary>
	// Token: 0x020002C0 RID: 704
	public enum RequestCacheLevel
	{
		/// <summary>Satisfies a request for a resource either by using the cached copy of the resource or by sending a request for the resource to the server. The action taken is determined by the current cache policy and the age of the content in the cache. This is the cache level that should be used by most applications.</summary>
		// Token: 0x04000F90 RID: 3984
		Default,
		/// <summary>Satisfies a request by using the server. No entries are taken from caches, added to caches, or removed from caches between the client and server. This is the default cache behavior specified in the machine configuration file that ships with the .NET Framework.</summary>
		// Token: 0x04000F91 RID: 3985
		BypassCache,
		/// <summary>Satisfies a request using the locally cached resource; does not send a request for an item that is not in the cache. When this cache policy level is specified, a <see cref="T:System.Net.WebException" /> exception is thrown if the item is not in the client cache.</summary>
		// Token: 0x04000F92 RID: 3986
		CacheOnly,
		/// <summary>Satisfies a request for a resource from the cache, if the resource is available; otherwise, sends a request for a resource to the server. If the requested item is available in any cache between the client and the server, the request might be satisfied by the intermediate cache.</summary>
		// Token: 0x04000F93 RID: 3987
		CacheIfAvailable,
		/// <summary>Satisfies a request by using the cached copy of the resource if the timestamp is the same as the timestamp of the resource on the server; otherwise, the resource is downloaded from the server, presented to the caller, and stored in the cache.</summary>
		// Token: 0x04000F94 RID: 3988
		Revalidate,
		/// <summary>Satisfies a request by using the server. The response might be saved in the cache. In the HTTP caching protocol, this is achieved using the no-cache cache control directive and the no-cache Pragma header.</summary>
		// Token: 0x04000F95 RID: 3989
		Reload,
		/// <summary>Never satisfies a request by using resources from the cache and does not cache resources. If the resource is present in the local cache, it is removed. This policy level indicates to intermediate caches that they should remove the resource. In the HTTP caching protocol, this is achieved using the no-cache cache control directive.</summary>
		// Token: 0x04000F96 RID: 3990
		NoCacheNoStore
	}
}
