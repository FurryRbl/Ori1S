using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents information used to configure Web proxy scripts. This class cannot be inherited.</summary>
	// Token: 0x020002E9 RID: 745
	public sealed class WebProxyScriptElement : ConfigurationElement
	{
		// Token: 0x0600196A RID: 6506 RVA: 0x00045CA8 File Offset: 0x00043EA8
		static WebProxyScriptElement()
		{
			WebProxyScriptElement.properties.Add(WebProxyScriptElement.downloadTimeoutProp);
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x00045CF8 File Offset: 0x00043EF8
		protected override void PostDeserialize()
		{
		}

		/// <summary>Gets or sets the Web proxy script download timeout using the format hours:minutes:seconds.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> object that contains the timeout value. The default download timeout is one minute.</returns>
		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x0600196C RID: 6508 RVA: 0x00045CFC File Offset: 0x00043EFC
		// (set) Token: 0x0600196D RID: 6509 RVA: 0x00045D10 File Offset: 0x00043F10
		[ConfigurationProperty("downloadTimeout", DefaultValue = "00:02:00")]
		public TimeSpan DownloadTimeout
		{
			get
			{
				return (TimeSpan)base[WebProxyScriptElement.downloadTimeoutProp];
			}
			set
			{
				base[WebProxyScriptElement.downloadTimeoutProp] = value;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x00045D24 File Offset: 0x00043F24
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebProxyScriptElement.properties;
			}
		}

		// Token: 0x04001002 RID: 4098
		private static ConfigurationProperty downloadTimeoutProp = new ConfigurationProperty("downloadTimeout", typeof(TimeSpan), new TimeSpan(0, 0, 2, 0));

		// Token: 0x04001003 RID: 4099
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
