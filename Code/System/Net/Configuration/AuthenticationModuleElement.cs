using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the type information for an authentication module. This class cannot be inherited.</summary>
	// Token: 0x020002C8 RID: 712
	public sealed class AuthenticationModuleElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> class. </summary>
		// Token: 0x0600187F RID: 6271 RVA: 0x000439BC File Offset: 0x00041BBC
		public AuthenticationModuleElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> class with the specified type information.</summary>
		/// <param name="typeName">A string that identifies the type and the assembly that contains it.</param>
		// Token: 0x06001880 RID: 6272 RVA: 0x000439C4 File Offset: 0x00041BC4
		public AuthenticationModuleElement(string typeName)
		{
			this.Type = typeName;
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x000439D4 File Offset: 0x00041BD4
		static AuthenticationModuleElement()
		{
			AuthenticationModuleElement.properties = new ConfigurationPropertyCollection();
			AuthenticationModuleElement.properties.Add(AuthenticationModuleElement.typeProp);
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x00043A18 File Offset: 0x00041C18
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthenticationModuleElement.properties;
			}
		}

		/// <summary>Gets or sets the type and assembly information for the current instance.</summary>
		/// <returns>A string that identifies a type that implements an authentication module or null if no value has been specified.</returns>
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x00043A20 File Offset: 0x00041C20
		// (set) Token: 0x06001884 RID: 6276 RVA: 0x00043A34 File Offset: 0x00041C34
		[ConfigurationProperty("type", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		public string Type
		{
			get
			{
				return (string)base[AuthenticationModuleElement.typeProp];
			}
			set
			{
				base[AuthenticationModuleElement.typeProp] = value;
			}
		}

		// Token: 0x04000FB1 RID: 4017
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04000FB2 RID: 4018
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}
