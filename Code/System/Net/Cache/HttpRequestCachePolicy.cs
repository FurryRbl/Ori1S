using System;

namespace System.Net.Cache
{
	/// <summary>Defines an application's caching requirements for resources obtained by using <see cref="T:System.Net.HttpWebRequest" /> objects.</summary>
	// Token: 0x020002BF RID: 703
	public class HttpRequestCachePolicy : RequestCachePolicy
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class. </summary>
		// Token: 0x06001847 RID: 6215 RVA: 0x00042C54 File Offset: 0x00040E54
		public HttpRequestCachePolicy()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class using the specified cache synchronization date.</summary>
		/// <param name="cacheSyncDate">A <see cref="T:System.DateTime" /> object that specifies the time when resources stored in the cache must be revalidated.</param>
		// Token: 0x06001848 RID: 6216 RVA: 0x00042C5C File Offset: 0x00040E5C
		public HttpRequestCachePolicy(DateTime cacheSyncDate)
		{
			this.cacheSyncDate = cacheSyncDate;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class using the specified cache policy.</summary>
		/// <param name="level">An <see cref="T:System.Net.Cache.HttpRequestCacheLevel" /> value. </param>
		// Token: 0x06001849 RID: 6217 RVA: 0x00042C6C File Offset: 0x00040E6C
		public HttpRequestCachePolicy(HttpRequestCacheLevel level)
		{
			this.level = level;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class using the specified age control and time values. </summary>
		/// <param name="cacheAgeControl">One of the following <see cref="T:System.Net.Cache.HttpCacheAgeControl" /> enumeration values: <see cref="F:System.Net.Cache.HttpCacheAgeControl.MaxAge" />, <see cref="F:System.Net.Cache.HttpCacheAgeControl.MaxStale" />, or <see cref="F:System.Net.Cache.HttpCacheAgeControl.MinFresh" />.</param>
		/// <param name="ageOrFreshOrStale">A <see cref="T:System.TimeSpan" /> value that specifies an amount of time. See the Remarks section for more information. </param>
		/// <exception cref="T:System.ArgumentException">The value specified for the <paramref name="cacheAgeControl" /> parameter cannot be used with this constructor.</exception>
		// Token: 0x0600184A RID: 6218 RVA: 0x00042C7C File Offset: 0x00040E7C
		public HttpRequestCachePolicy(HttpCacheAgeControl cacheAgeControl, TimeSpan ageOrFreshOrStale)
		{
			switch (cacheAgeControl)
			{
			case HttpCacheAgeControl.MinFresh:
				this.minFresh = ageOrFreshOrStale;
				return;
			case HttpCacheAgeControl.MaxAge:
				this.maxAge = ageOrFreshOrStale;
				return;
			case HttpCacheAgeControl.MaxStale:
				this.maxStale = ageOrFreshOrStale;
				return;
			}
			throw new ArgumentException("ageOrFreshOrStale");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class using the specified maximum age, age control value, and time value.</summary>
		/// <param name="cacheAgeControl">An <see cref="T:System.Net.Cache.HttpCacheAgeControl" /> value. </param>
		/// <param name="maxAge">A <see cref="T:System.TimeSpan" /> value that specifies the maximum age for resources.</param>
		/// <param name="freshOrStale">A <see cref="T:System.TimeSpan" /> value that specifies an amount of time. See the Remarks section for more information.  </param>
		/// <exception cref="T:System.ArgumentException">The value specified for the <paramref name="cacheAgeControl" /> parameter is not valid.</exception>
		// Token: 0x0600184B RID: 6219 RVA: 0x00042CE0 File Offset: 0x00040EE0
		public HttpRequestCachePolicy(HttpCacheAgeControl cacheAgeControl, TimeSpan maxAge, TimeSpan freshOrStale)
		{
			this.maxAge = maxAge;
			switch (cacheAgeControl)
			{
			case HttpCacheAgeControl.MinFresh:
				this.minFresh = freshOrStale;
				return;
			case HttpCacheAgeControl.MaxStale:
				this.maxStale = freshOrStale;
				return;
			}
			throw new ArgumentException("freshOrStale");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class using the specified maximum age, age control value, time value, and cache synchronization date.</summary>
		/// <param name="cacheAgeControl">An <see cref="T:System.Net.Cache.HttpCacheAgeControl" /> value. </param>
		/// <param name="maxAge">A <see cref="T:System.TimeSpan" /> value that specifies the maximum age for resources.</param>
		/// <param name="freshOrStale">A <see cref="T:System.TimeSpan" /> value that specifies an amount of time. See the Remarks section for more information.  </param>
		/// <param name="cacheSyncDate">A <see cref="T:System.DateTime" /> object that specifies the time when resources stored in the cache must be revalidated.</param>
		// Token: 0x0600184C RID: 6220 RVA: 0x00042D3C File Offset: 0x00040F3C
		public HttpRequestCachePolicy(HttpCacheAgeControl cacheAgeControl, TimeSpan maxAge, TimeSpan freshOrStale, DateTime cacheSyncDate) : this(cacheAgeControl, maxAge, freshOrStale)
		{
			this.cacheSyncDate = cacheSyncDate;
		}

		/// <summary>Gets the cache synchronization date for this instance.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value set to the date specified when this instance was created. If no date was specified, this property's value is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x00042D50 File Offset: 0x00040F50
		public DateTime CacheSyncDate
		{
			get
			{
				return this.cacheSyncDate;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.Cache.HttpRequestCacheLevel" /> value that was specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.HttpRequestCacheLevel" /> value that specifies the cache behavior for resources that were obtained using <see cref="T:System.Net.HttpWebRequest" /> objects.</returns>
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x00042D58 File Offset: 0x00040F58
		public new HttpRequestCacheLevel Level
		{
			get
			{
				return this.level;
			}
		}

		/// <summary>Gets the maximum age permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that is set to the maximum age value specified when this instance was created. If no date was specified, this property's value is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x00042D60 File Offset: 0x00040F60
		public TimeSpan MaxAge
		{
			get
			{
				return this.maxAge;
			}
		}

		/// <summary>Gets the maximum staleness value that is permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that is set to the maximum staleness value specified when this instance was created. If no date was specified, this property's value is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001850 RID: 6224 RVA: 0x00042D68 File Offset: 0x00040F68
		public TimeSpan MaxStale
		{
			get
			{
				return this.maxStale;
			}
		}

		/// <summary>Gets the minimum freshness that is permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the minimum freshness specified when this instance was created. If no date was specified, this property's value is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x00042D70 File Offset: 0x00040F70
		public TimeSpan MinFresh
		{
			get
			{
				return this.minFresh;
			}
		}

		/// <summary>Returns a string representation of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the property values for this instance.</returns>
		// Token: 0x06001852 RID: 6226 RVA: 0x00042D78 File Offset: 0x00040F78
		[MonoTODO]
		public override string ToString()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000F8A RID: 3978
		private DateTime cacheSyncDate;

		// Token: 0x04000F8B RID: 3979
		private HttpRequestCacheLevel level;

		// Token: 0x04000F8C RID: 3980
		private TimeSpan maxAge;

		// Token: 0x04000F8D RID: 3981
		private TimeSpan maxStale;

		// Token: 0x04000F8E RID: 3982
		private TimeSpan minFresh;
	}
}
