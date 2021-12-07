using System;

namespace System.Net.Cache
{
	/// <summary>Defines an application's caching requirements for resources obtained by using <see cref="T:System.Net.WebRequest" /> objects.</summary>
	// Token: 0x020002C1 RID: 705
	public class RequestCachePolicy
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.RequestCachePolicy" /> class. </summary>
		// Token: 0x06001853 RID: 6227 RVA: 0x00042D80 File Offset: 0x00040F80
		public RequestCachePolicy()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.RequestCachePolicy" /> class. using the specified cache policy.</summary>
		/// <param name="level">A <see cref="T:System.Net.Cache.RequestCacheLevel" /> that specifies the cache behavior for resources obtained using <see cref="T:System.Net.WebRequest" /> objects. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">level is not a valid <see cref="T:System.Net.Cache.RequestCacheLevel" />.value.</exception>
		// Token: 0x06001854 RID: 6228 RVA: 0x00042D88 File Offset: 0x00040F88
		public RequestCachePolicy(RequestCacheLevel level)
		{
			this.level = level;
		}

		/// <summary>Gets the <see cref="T:System.Net.Cache.RequestCacheLevel" /> value specified when this instance was constructed.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCacheLevel" /> value that specifies the cache behavior for resources obtained using <see cref="T:System.Net.WebRequest" /> objects.</returns>
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x00042D98 File Offset: 0x00040F98
		public RequestCacheLevel Level
		{
			get
			{
				return this.level;
			}
		}

		/// <summary>Returns a string representation of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the <see cref="P:System.Net.Cache.RequestCachePolicy.Level" /> for this instance.</returns>
		// Token: 0x06001856 RID: 6230 RVA: 0x00042DA0 File Offset: 0x00040FA0
		[MonoTODO]
		public override string ToString()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000F97 RID: 3991
		private RequestCacheLevel level;
	}
}
