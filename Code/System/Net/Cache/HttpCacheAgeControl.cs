using System;

namespace System.Net.Cache
{
	/// <summary>Specifies the meaning of time values that control caching behavior for resources obtained using <see cref="T:System.Net.HttpWebRequest" /> objects.</summary>
	// Token: 0x020002BD RID: 701
	public enum HttpCacheAgeControl
	{
		/// <summary>For internal use only. The Framework will throw an <see cref="T:System.ArgumentException" /> if you try to use this member.</summary>
		// Token: 0x04000F7A RID: 3962
		None,
		/// <summary>Content can be taken from the cache if the time remaining before expiration is greater than or equal to the time specified with this value.</summary>
		// Token: 0x04000F7B RID: 3963
		MinFresh,
		/// <summary>Content can be taken from the cache until it is older than the age specified with this value.</summary>
		// Token: 0x04000F7C RID: 3964
		MaxAge,
		/// <summary>
		///   <see cref="P:System.Net.Cache.HttpRequestCachePolicy.MaxAge" /> and <see cref="P:System.Net.Cache.HttpRequestCachePolicy.MinFresh" />.</summary>
		// Token: 0x04000F7D RID: 3965
		MaxAgeAndMinFresh,
		/// <summary>Content can be taken from the cache after it has expired, until the time specified with this value elapses.</summary>
		// Token: 0x04000F7E RID: 3966
		MaxStale,
		/// <summary>
		///   <see cref="P:System.Net.Cache.HttpRequestCachePolicy.MaxAge" /> and <see cref="P:System.Net.Cache.HttpRequestCachePolicy.MaxStale" />.</summary>
		// Token: 0x04000F7F RID: 3967
		MaxAgeAndMaxStale = 6
	}
}
