using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for cache behavior. This class cannot be inherited.</summary>
	// Token: 0x020002E2 RID: 738
	public sealed class RequestCachingSection : ConfigurationSection
	{
		// Token: 0x06001921 RID: 6433 RVA: 0x00045430 File Offset: 0x00043630
		static RequestCachingSection()
		{
			RequestCachingSection.properties = new ConfigurationPropertyCollection();
			RequestCachingSection.properties.Add(RequestCachingSection.defaultFtpCachePolicyProp);
			RequestCachingSection.properties.Add(RequestCachingSection.defaultHttpCachePolicyProp);
			RequestCachingSection.properties.Add(RequestCachingSection.defaultPolicyLevelProp);
			RequestCachingSection.properties.Add(RequestCachingSection.disableAllCachingProp);
			RequestCachingSection.properties.Add(RequestCachingSection.isPrivateCacheProp);
			RequestCachingSection.properties.Add(RequestCachingSection.unspecifiedMaximumAgeProp);
		}

		/// <summary>Gets the default FTP caching behavior for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.FtpCachePolicyElement" /> that defines the default cache policy.</returns>
		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x00045558 File Offset: 0x00043758
		[ConfigurationProperty("defaultFtpCachePolicy")]
		public FtpCachePolicyElement DefaultFtpCachePolicy
		{
			get
			{
				return (FtpCachePolicyElement)base[RequestCachingSection.defaultFtpCachePolicyProp];
			}
		}

		/// <summary>Gets the default caching behavior for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.HttpCachePolicyElement" /> that defines the default cache policy.</returns>
		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001923 RID: 6435 RVA: 0x0004556C File Offset: 0x0004376C
		[ConfigurationProperty("defaultHttpCachePolicy")]
		public HttpCachePolicyElement DefaultHttpCachePolicy
		{
			get
			{
				return (HttpCachePolicyElement)base[RequestCachingSection.defaultHttpCachePolicyProp];
			}
		}

		/// <summary>Gets or sets the default cache policy level.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCacheLevel" /> enumeration value.</returns>
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001924 RID: 6436 RVA: 0x00045580 File Offset: 0x00043780
		// (set) Token: 0x06001925 RID: 6437 RVA: 0x00045594 File Offset: 0x00043794
		[ConfigurationProperty("defaultPolicyLevel", DefaultValue = "BypassCache")]
		public System.Net.Cache.RequestCacheLevel DefaultPolicyLevel
		{
			get
			{
				return (System.Net.Cache.RequestCacheLevel)((int)base[RequestCachingSection.defaultPolicyLevelProp]);
			}
			set
			{
				base[RequestCachingSection.defaultPolicyLevelProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that enables caching on the local computer.</summary>
		/// <returns>true if caching is disabled on the local computer; otherwise, false.</returns>
		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x000455A8 File Offset: 0x000437A8
		// (set) Token: 0x06001927 RID: 6439 RVA: 0x000455BC File Offset: 0x000437BC
		[ConfigurationProperty("disableAllCaching", DefaultValue = "False")]
		public bool DisableAllCaching
		{
			get
			{
				return (bool)base[RequestCachingSection.disableAllCachingProp];
			}
			set
			{
				base[RequestCachingSection.disableAllCachingProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether the local computer cache is private.</summary>
		/// <returns>true if the cache provides user isolation; otherwise, false.</returns>
		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x000455D0 File Offset: 0x000437D0
		// (set) Token: 0x06001929 RID: 6441 RVA: 0x000455E4 File Offset: 0x000437E4
		[ConfigurationProperty("isPrivateCache", DefaultValue = "True")]
		public bool IsPrivateCache
		{
			get
			{
				return (bool)base[RequestCachingSection.isPrivateCacheProp];
			}
			set
			{
				base[RequestCachingSection.isPrivateCacheProp] = value;
			}
		}

		/// <summary>Gets or sets a value used as the maximum age for cached resources that do not have expiration information.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that provides a default maximum age for cached resources.</returns>
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x000455F8 File Offset: 0x000437F8
		// (set) Token: 0x0600192B RID: 6443 RVA: 0x0004560C File Offset: 0x0004380C
		[ConfigurationProperty("unspecifiedMaximumAge", DefaultValue = "1.00:00:00")]
		public TimeSpan UnspecifiedMaximumAge
		{
			get
			{
				return (TimeSpan)base[RequestCachingSection.unspecifiedMaximumAgeProp];
			}
			set
			{
				base[RequestCachingSection.unspecifiedMaximumAgeProp] = value;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x00045620 File Offset: 0x00043820
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return RequestCachingSection.properties;
			}
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00045628 File Offset: 0x00043828
		[MonoTODO]
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00045630 File Offset: 0x00043830
		[MonoTODO]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			base.DeserializeElement(reader, serializeCollectionKey);
		}

		// Token: 0x04000FE8 RID: 4072
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04000FE9 RID: 4073
		private static ConfigurationProperty defaultFtpCachePolicyProp = new ConfigurationProperty("defaultFtpCachePolicy", typeof(FtpCachePolicyElement));

		// Token: 0x04000FEA RID: 4074
		private static ConfigurationProperty defaultHttpCachePolicyProp = new ConfigurationProperty("defaultHttpCachePolicy", typeof(HttpCachePolicyElement));

		// Token: 0x04000FEB RID: 4075
		private static ConfigurationProperty defaultPolicyLevelProp = new ConfigurationProperty("defaultPolicyLevel", typeof(System.Net.Cache.RequestCacheLevel), System.Net.Cache.RequestCacheLevel.BypassCache);

		// Token: 0x04000FEC RID: 4076
		private static ConfigurationProperty disableAllCachingProp = new ConfigurationProperty("disableAllCaching", typeof(bool), false);

		// Token: 0x04000FED RID: 4077
		private static ConfigurationProperty isPrivateCacheProp = new ConfigurationProperty("isPrivateCache", typeof(bool), true);

		// Token: 0x04000FEE RID: 4078
		private static ConfigurationProperty unspecifiedMaximumAgeProp = new ConfigurationProperty("unspecifiedMaximumAge", typeof(TimeSpan), new TimeSpan(1, 0, 0, 0));
	}
}
