using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the network element in the SMTP configuration file. This class cannot be inherited.</summary>
	// Token: 0x020002E5 RID: 741
	public sealed class SmtpNetworkElement : ConfigurationElement
	{
		/// <summary>Determines whether or not default user credentials are used to access an SMTP server. The default value is false.</summary>
		/// <returns>true indicates that default user credentials will be used to access the SMTP server; otherwise, false.</returns>
		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001949 RID: 6473 RVA: 0x00045A08 File Offset: 0x00043C08
		// (set) Token: 0x0600194A RID: 6474 RVA: 0x00045A1C File Offset: 0x00043C1C
		[ConfigurationProperty("defaultCredentials", DefaultValue = "False")]
		public bool DefaultCredentials
		{
			get
			{
				return (bool)base["defaultCredentials"];
			}
			set
			{
				base["defaultCredentials"] = value;
			}
		}

		/// <summary>Gets or sets the name of the SMTP server.</summary>
		/// <returns>A string that represents the name of the SMTP server to connect to.</returns>
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x00045A30 File Offset: 0x00043C30
		// (set) Token: 0x0600194C RID: 6476 RVA: 0x00045A44 File Offset: 0x00043C44
		[ConfigurationProperty("host")]
		public string Host
		{
			get
			{
				return (string)base["host"];
			}
			set
			{
				base["host"] = value;
			}
		}

		/// <summary>Gets or sets the user password to use to connect to an SMTP mail server.</summary>
		/// <returns>A string that represents the password to use to connect to an SMTP mail server.</returns>
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x00045A54 File Offset: 0x00043C54
		// (set) Token: 0x0600194E RID: 6478 RVA: 0x00045A68 File Offset: 0x00043C68
		[ConfigurationProperty("password")]
		public string Password
		{
			get
			{
				return (string)base["password"];
			}
			set
			{
				base["password"] = value;
			}
		}

		/// <summary>Gets or sets the port that SMTP clients use to connect to an SMTP mail server. The default value is 25.</summary>
		/// <returns>A string that represents the port to connect to an SMTP mail server.</returns>
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x0600194F RID: 6479 RVA: 0x00045A78 File Offset: 0x00043C78
		// (set) Token: 0x06001950 RID: 6480 RVA: 0x00045A8C File Offset: 0x00043C8C
		[ConfigurationProperty("port", DefaultValue = "25")]
		public int Port
		{
			get
			{
				return (int)base["port"];
			}
			set
			{
				base["port"] = value;
			}
		}

		/// <summary>Gets or sets the user name to connect to an SMTP mail server.</summary>
		/// <returns>A string that represents the user name to connect to an SMTP mail server.</returns>
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x00045AA0 File Offset: 0x00043CA0
		// (set) Token: 0x06001952 RID: 6482 RVA: 0x00045AB4 File Offset: 0x00043CB4
		[ConfigurationProperty("userName", DefaultValue = null)]
		public string UserName
		{
			get
			{
				return (string)base["userName"];
			}
			set
			{
				base["userName"] = value;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x00045AC4 File Offset: 0x00043CC4
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return base.Properties;
			}
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x00045ACC File Offset: 0x00043CCC
		protected override void PostDeserialize()
		{
		}
	}
}
