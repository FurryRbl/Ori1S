using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	/// <summary>Represents the default HTTP cache policy for network resources. This class cannot be inherited.</summary>
	// Token: 0x020002D5 RID: 725
	public sealed class HttpCachePolicyElement : ConfigurationElement
	{
		// Token: 0x060018DC RID: 6364 RVA: 0x000448FC File Offset: 0x00042AFC
		static HttpCachePolicyElement()
		{
			HttpCachePolicyElement.properties.Add(HttpCachePolicyElement.maximumAgeProp);
			HttpCachePolicyElement.properties.Add(HttpCachePolicyElement.maximumStaleProp);
			HttpCachePolicyElement.properties.Add(HttpCachePolicyElement.minimumFreshProp);
			HttpCachePolicyElement.properties.Add(HttpCachePolicyElement.policyLevelProp);
		}

		/// <summary>Gets or sets the maximum age permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the maximum age for cached resources specified in the configuration file.</returns>
		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x000449D8 File Offset: 0x00042BD8
		// (set) Token: 0x060018DE RID: 6366 RVA: 0x000449EC File Offset: 0x00042BEC
		[ConfigurationProperty("maximumAge", DefaultValue = "10675199.02:48:05.4775807")]
		public TimeSpan MaximumAge
		{
			get
			{
				return (TimeSpan)base[HttpCachePolicyElement.maximumAgeProp];
			}
			set
			{
				base[HttpCachePolicyElement.maximumAgeProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum staleness value permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that is set to the maximum staleness value specified in the configuration file.</returns>
		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x00044A00 File Offset: 0x00042C00
		// (set) Token: 0x060018E0 RID: 6368 RVA: 0x00044A14 File Offset: 0x00042C14
		[ConfigurationProperty("maximumStale", DefaultValue = "-10675199.02:48:05.4775808")]
		public TimeSpan MaximumStale
		{
			get
			{
				return (TimeSpan)base[HttpCachePolicyElement.maximumStaleProp];
			}
			set
			{
				base[HttpCachePolicyElement.maximumStaleProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum freshness permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the minimum freshness specified in the configuration file.</returns>
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060018E1 RID: 6369 RVA: 0x00044A28 File Offset: 0x00042C28
		// (set) Token: 0x060018E2 RID: 6370 RVA: 0x00044A3C File Offset: 0x00042C3C
		[ConfigurationProperty("minimumFresh", DefaultValue = "-10675199.02:48:05.4775808")]
		public TimeSpan MinimumFresh
		{
			get
			{
				return (TimeSpan)base[HttpCachePolicyElement.minimumFreshProp];
			}
			set
			{
				base[HttpCachePolicyElement.minimumFreshProp] = value;
			}
		}

		/// <summary>Gets or sets HTTP caching behavior for the local machine.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.HttpRequestCacheLevel" /> value that specifies the cache behavior.</returns>
		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x00044A50 File Offset: 0x00042C50
		// (set) Token: 0x060018E4 RID: 6372 RVA: 0x00044A64 File Offset: 0x00042C64
		[ConfigurationProperty("policyLevel", DefaultValue = "Default", Options = ConfigurationPropertyOptions.IsRequired)]
		public System.Net.Cache.HttpRequestCacheLevel PolicyLevel
		{
			get
			{
				return (System.Net.Cache.HttpRequestCacheLevel)((int)base[HttpCachePolicyElement.policyLevelProp]);
			}
			set
			{
				base[HttpCachePolicyElement.policyLevelProp] = value;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060018E5 RID: 6373 RVA: 0x00044A78 File Offset: 0x00042C78
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpCachePolicyElement.properties;
			}
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x00044A80 File Offset: 0x00042C80
		[MonoTODO]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x00044A88 File Offset: 0x00042C88
		[MonoTODO]
		protected override void Reset(ConfigurationElement parentElement)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000FC6 RID: 4038
		private static ConfigurationProperty maximumAgeProp = new ConfigurationProperty("maximumAge", typeof(TimeSpan), TimeSpan.MaxValue);

		// Token: 0x04000FC7 RID: 4039
		private static ConfigurationProperty maximumStaleProp = new ConfigurationProperty("maximumStale", typeof(TimeSpan), TimeSpan.MinValue);

		// Token: 0x04000FC8 RID: 4040
		private static ConfigurationProperty minimumFreshProp = new ConfigurationProperty("minimumFresh", typeof(TimeSpan), TimeSpan.MinValue);

		// Token: 0x04000FC9 RID: 4041
		private static ConfigurationProperty policyLevelProp = new ConfigurationProperty("policyLevel", typeof(System.Net.Cache.HttpRequestCacheLevel), System.Net.Cache.HttpRequestCacheLevel.Default, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04000FCA RID: 4042
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
