using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents information used to configure <see cref="T:System.Net.Sockets.Socket" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020002E8 RID: 744
	public sealed class SocketElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.SocketElement" /> class. </summary>
		// Token: 0x06001962 RID: 6498 RVA: 0x00045BC8 File Offset: 0x00043DC8
		public SocketElement()
		{
			SocketElement.alwaysUseCompletionPortsForAcceptProp = new ConfigurationProperty("alwaysUseCompletionPortsForAccept", typeof(bool), false);
			SocketElement.alwaysUseCompletionPortsForConnectProp = new ConfigurationProperty("alwaysUseCompletionPortsForConnect", typeof(bool), false);
			SocketElement.properties = new ConfigurationPropertyCollection();
			SocketElement.properties.Add(SocketElement.alwaysUseCompletionPortsForAcceptProp);
			SocketElement.properties.Add(SocketElement.alwaysUseCompletionPortsForConnectProp);
		}

		/// <summary>Gets or sets a Boolean value that specifies whether completion ports are used when accepting connections.</summary>
		/// <returns>true to use completion ports; otherwise, false.</returns>
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x00045C44 File Offset: 0x00043E44
		// (set) Token: 0x06001964 RID: 6500 RVA: 0x00045C58 File Offset: 0x00043E58
		[ConfigurationProperty("alwaysUseCompletionPortsForAccept", DefaultValue = "False")]
		public bool AlwaysUseCompletionPortsForAccept
		{
			get
			{
				return (bool)base[SocketElement.alwaysUseCompletionPortsForAcceptProp];
			}
			set
			{
				base[SocketElement.alwaysUseCompletionPortsForAcceptProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether completion ports are used when making connections.</summary>
		/// <returns>true to use completion ports; otherwise, false.</returns>
		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001965 RID: 6501 RVA: 0x00045C6C File Offset: 0x00043E6C
		// (set) Token: 0x06001966 RID: 6502 RVA: 0x00045C80 File Offset: 0x00043E80
		[ConfigurationProperty("alwaysUseCompletionPortsForConnect", DefaultValue = "False")]
		public bool AlwaysUseCompletionPortsForConnect
		{
			get
			{
				return (bool)base[SocketElement.alwaysUseCompletionPortsForConnectProp];
			}
			set
			{
				base[SocketElement.alwaysUseCompletionPortsForConnectProp] = value;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x00045C94 File Offset: 0x00043E94
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SocketElement.properties;
			}
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00045C9C File Offset: 0x00043E9C
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		// Token: 0x04000FFF RID: 4095
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04001000 RID: 4096
		private static ConfigurationProperty alwaysUseCompletionPortsForAcceptProp;

		// Token: 0x04001001 RID: 4097
		private static ConfigurationProperty alwaysUseCompletionPortsForConnectProp;
	}
}
