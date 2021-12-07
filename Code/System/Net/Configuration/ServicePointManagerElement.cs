using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the default settings used to create connections to a remote computer. This class cannot be inherited.</summary>
	// Token: 0x020002E3 RID: 739
	public sealed class ServicePointManagerElement : ConfigurationElement
	{
		// Token: 0x06001930 RID: 6448 RVA: 0x00045644 File Offset: 0x00043844
		static ServicePointManagerElement()
		{
			ServicePointManagerElement.properties = new ConfigurationPropertyCollection();
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.checkCertificateNameProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.checkCertificateRevocationListProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.dnsRefreshTimeoutProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.enableDnsRoundRobinProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.expect100ContinueProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.useNagleAlgorithmProp);
		}

		/// <summary>Gets or sets a Boolean value that controls checking host name information in an X509 certificate.</summary>
		/// <returns>true to specify host name checking; otherwise, false. </returns>
		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001931 RID: 6449 RVA: 0x00045774 File Offset: 0x00043974
		// (set) Token: 0x06001932 RID: 6450 RVA: 0x00045788 File Offset: 0x00043988
		[ConfigurationProperty("checkCertificateName", DefaultValue = "True")]
		public bool CheckCertificateName
		{
			get
			{
				return (bool)base[ServicePointManagerElement.checkCertificateNameProp];
			}
			set
			{
				base[ServicePointManagerElement.checkCertificateNameProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether the certificate is checked against the certificate authority revocation list.</summary>
		/// <returns>true if the certificate revocation list is checked; otherwise, false.The default value is false.</returns>
		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x0004579C File Offset: 0x0004399C
		// (set) Token: 0x06001934 RID: 6452 RVA: 0x000457B0 File Offset: 0x000439B0
		[ConfigurationProperty("checkCertificateRevocationList", DefaultValue = "False")]
		public bool CheckCertificateRevocationList
		{
			get
			{
				return (bool)base[ServicePointManagerElement.checkCertificateRevocationListProp];
			}
			set
			{
				base[ServicePointManagerElement.checkCertificateRevocationListProp] = value;
			}
		}

		/// <summary>Gets or sets the amount of time after which address information is refreshed.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that specifies when addresses are resolved using DNS.</returns>
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x000457C4 File Offset: 0x000439C4
		// (set) Token: 0x06001936 RID: 6454 RVA: 0x000457D8 File Offset: 0x000439D8
		[ConfigurationProperty("dnsRefreshTimeout", DefaultValue = "120000")]
		public int DnsRefreshTimeout
		{
			get
			{
				return (int)base[ServicePointManagerElement.dnsRefreshTimeoutProp];
			}
			set
			{
				base[ServicePointManagerElement.dnsRefreshTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that controls using different IP addresses on connections to the same server.</summary>
		/// <returns>true to enable DNS round-robin behavior; otherwise, false.</returns>
		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001937 RID: 6455 RVA: 0x000457EC File Offset: 0x000439EC
		// (set) Token: 0x06001938 RID: 6456 RVA: 0x00045800 File Offset: 0x00043A00
		[ConfigurationProperty("enableDnsRoundRobin", DefaultValue = "False")]
		public bool EnableDnsRoundRobin
		{
			get
			{
				return (bool)base[ServicePointManagerElement.enableDnsRoundRobinProp];
			}
			set
			{
				base[ServicePointManagerElement.enableDnsRoundRobinProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that determines whether 100-Continue behavior is used.</summary>
		/// <returns>true to expect 100-Continue responses for POST requests; otherwise, false. The default value is true.</returns>
		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x00045814 File Offset: 0x00043A14
		// (set) Token: 0x0600193A RID: 6458 RVA: 0x00045828 File Offset: 0x00043A28
		[ConfigurationProperty("expect100Continue", DefaultValue = "True")]
		public bool Expect100Continue
		{
			get
			{
				return (bool)base[ServicePointManagerElement.expect100ContinueProp];
			}
			set
			{
				base[ServicePointManagerElement.expect100ContinueProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that determines whether the Nagle algorithm is used.</summary>
		/// <returns>true to use the Nagle algorithm; otherwise, false. The default value is true.</returns>
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x0600193B RID: 6459 RVA: 0x0004583C File Offset: 0x00043A3C
		// (set) Token: 0x0600193C RID: 6460 RVA: 0x00045850 File Offset: 0x00043A50
		[ConfigurationProperty("useNagleAlgorithm", DefaultValue = "True")]
		public bool UseNagleAlgorithm
		{
			get
			{
				return (bool)base[ServicePointManagerElement.useNagleAlgorithmProp];
			}
			set
			{
				base[ServicePointManagerElement.useNagleAlgorithmProp] = value;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x0600193D RID: 6461 RVA: 0x00045864 File Offset: 0x00043A64
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ServicePointManagerElement.properties;
			}
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x0004586C File Offset: 0x00043A6C
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		// Token: 0x04000FEF RID: 4079
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04000FF0 RID: 4080
		private static ConfigurationProperty checkCertificateNameProp = new ConfigurationProperty("checkCertificateName", typeof(bool), true);

		// Token: 0x04000FF1 RID: 4081
		private static ConfigurationProperty checkCertificateRevocationListProp = new ConfigurationProperty("checkCertificateRevocationList", typeof(bool), false);

		// Token: 0x04000FF2 RID: 4082
		private static ConfigurationProperty dnsRefreshTimeoutProp = new ConfigurationProperty("dnsRefreshTimeout", typeof(int), 120000);

		// Token: 0x04000FF3 RID: 4083
		private static ConfigurationProperty enableDnsRoundRobinProp = new ConfigurationProperty("enableDnsRoundRobin", typeof(bool), false);

		// Token: 0x04000FF4 RID: 4084
		private static ConfigurationProperty expect100ContinueProp = new ConfigurationProperty("expect100Continue", typeof(bool), true);

		// Token: 0x04000FF5 RID: 4085
		private static ConfigurationProperty useNagleAlgorithmProp = new ConfigurationProperty("useNagleAlgorithm", typeof(bool), true);
	}
}
