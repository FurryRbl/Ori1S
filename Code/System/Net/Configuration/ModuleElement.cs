using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the type information for a custom <see cref="T:System.Net.IWebProxy" /> module. This class cannot be inherited.</summary>
	// Token: 0x020002D9 RID: 729
	public sealed class ModuleElement : ConfigurationElement
	{
		// Token: 0x060018FC RID: 6396 RVA: 0x00044CC4 File Offset: 0x00042EC4
		static ModuleElement()
		{
			ModuleElement.properties = new ConfigurationPropertyCollection();
			ModuleElement.properties.Add(ModuleElement.typeProp);
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x00044CFC File Offset: 0x00042EFC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ModuleElement.properties;
			}
		}

		/// <summary>Gets or sets the type and assembly information for the current instance.</summary>
		/// <returns>A string that identifies a type that implements the <see cref="T:System.Net.IWebProxy" /> interface or null if no value has been specified.</returns>
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x00044D04 File Offset: 0x00042F04
		// (set) Token: 0x060018FF RID: 6399 RVA: 0x00044D18 File Offset: 0x00042F18
		[ConfigurationProperty("type")]
		public string Type
		{
			get
			{
				return (string)base[ModuleElement.typeProp];
			}
			set
			{
				base[ModuleElement.typeProp] = value;
			}
		}

		// Token: 0x04000FD2 RID: 4050
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04000FD3 RID: 4051
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), null);
	}
}
