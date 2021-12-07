using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.MailSettingsSectionGroup" /> class.</summary>
	// Token: 0x020002D8 RID: 728
	public sealed class MailSettingsSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Gets the SMTP settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SmtpSection" /> object that contains configuration information for the local computer.</returns>
		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x00044CA4 File Offset: 0x00042EA4
		public SmtpSection Smtp
		{
			get
			{
				return (SmtpSection)base.Sections["smtp"];
			}
		}
	}
}
