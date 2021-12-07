using System;

namespace System.Configuration
{
	/// <summary>Represents the Uri section within a configuration file.</summary>
	// Token: 0x02000208 RID: 520
	public sealed class UriSection : ConfigurationSection
	{
		// Token: 0x06001185 RID: 4485 RVA: 0x0002EAD0 File Offset: 0x0002CCD0
		static UriSection()
		{
			UriSection.properties = new ConfigurationPropertyCollection();
			UriSection.properties.Add(UriSection.idn_prop);
			UriSection.properties.Add(UriSection.iriParsing_prop);
		}

		/// <summary>Gets an <see cref="T:System.Configuration.IdnElement" /> object that contains the configuration setting for International Domain Name (IDN) processing in the <see cref="T:System.Uri" /> class.</summary>
		/// <returns>The configuration setting for International Domain Name (IDN) processing in the <see cref="T:System.Uri" /> class.</returns>
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x0002EB3C File Offset: 0x0002CD3C
		[ConfigurationProperty("idn")]
		public IdnElement Idn
		{
			get
			{
				return (IdnElement)base[UriSection.idn_prop];
			}
		}

		/// <summary>Gets an <see cref="T:System.Configuration.IriParsingElement" /> object that contains the configuration setting for International Resource Identifiers (IRI) parsing in the <see cref="T:System.Uri" /> class.</summary>
		/// <returns>The configuration setting for International Resource Identifiers (IRI) parsing in the <see cref="T:System.Uri" /> class.</returns>
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x0002EB50 File Offset: 0x0002CD50
		[ConfigurationProperty("iriParsing")]
		public IriParsingElement IriParsing
		{
			get
			{
				return (IriParsingElement)base[UriSection.iriParsing_prop];
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x0002EB64 File Offset: 0x0002CD64
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return UriSection.properties;
			}
		}

		// Token: 0x04000506 RID: 1286
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04000507 RID: 1287
		private static ConfigurationProperty idn_prop = new ConfigurationProperty("idn", typeof(IdnElement), null);

		// Token: 0x04000508 RID: 1288
		private static ConfigurationProperty iriParsing_prop = new ConfigurationProperty("iriParsing", typeof(IriParsingElement), null);
	}
}
