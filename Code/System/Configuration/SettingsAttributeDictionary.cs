using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Represents a collection of key/value pairs used to describe a configuration object as well as a <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
	// Token: 0x020001EF RID: 495
	[Serializable]
	public class SettingsAttributeDictionary : Hashtable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsAttributeDictionary" /> class. </summary>
		// Token: 0x060010FC RID: 4348 RVA: 0x0002DA44 File Offset: 0x0002BC44
		public SettingsAttributeDictionary()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsAttributeDictionary" /> class. </summary>
		/// <param name="attributes"></param>
		// Token: 0x060010FD RID: 4349 RVA: 0x0002DA4C File Offset: 0x0002BC4C
		public SettingsAttributeDictionary(SettingsAttributeDictionary attributes) : base(attributes)
		{
		}
	}
}
