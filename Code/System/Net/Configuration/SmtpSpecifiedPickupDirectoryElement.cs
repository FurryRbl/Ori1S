using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents an SMTP pickup directory configuration element.</summary>
	// Token: 0x020002E7 RID: 743
	public sealed class SmtpSpecifiedPickupDirectoryElement : ConfigurationElement
	{
		// Token: 0x0600195E RID: 6494 RVA: 0x00045B5C File Offset: 0x00043D5C
		static SmtpSpecifiedPickupDirectoryElement()
		{
			SmtpSpecifiedPickupDirectoryElement.properties.Add(SmtpSpecifiedPickupDirectoryElement.pickupDirectoryLocationProp);
		}

		/// <summary>Gets or sets the folder where applications save mail messages to be processed by the SMTP server.</summary>
		/// <returns>A string that specifies the pickup directory for e-mail messages.</returns>
		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x00045B9C File Offset: 0x00043D9C
		// (set) Token: 0x06001960 RID: 6496 RVA: 0x00045BB0 File Offset: 0x00043DB0
		[ConfigurationProperty("pickupDirectoryLocation")]
		public string PickupDirectoryLocation
		{
			get
			{
				return (string)base[SmtpSpecifiedPickupDirectoryElement.pickupDirectoryLocationProp];
			}
			set
			{
				base[SmtpSpecifiedPickupDirectoryElement.pickupDirectoryLocationProp] = value;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x00045BC0 File Offset: 0x00043DC0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SmtpSpecifiedPickupDirectoryElement.properties;
			}
		}

		// Token: 0x04000FFD RID: 4093
		private static ConfigurationProperty pickupDirectoryLocationProp = new ConfigurationProperty("pickupDirectoryLocation", typeof(string));

		// Token: 0x04000FFE RID: 4094
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
