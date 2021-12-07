using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.ConfigurationLocationCollection" /> objects.</summary>
	// Token: 0x0200002A RID: 42
	public class ConfigurationLocationCollection : ReadOnlyCollectionBase
	{
		// Token: 0x060001AB RID: 427 RVA: 0x0000616C File Offset: 0x0000436C
		internal ConfigurationLocationCollection()
		{
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ConfigurationLocationCollection" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationLocationCollection" /> at the specified index.</returns>
		/// <param name="index">The index location of the <see cref="T:System.Configuration.ConfigurationLocationCollection" /> to return.</param>
		// Token: 0x17000072 RID: 114
		public ConfigurationLocation this[int index]
		{
			get
			{
				return base.InnerList[index] as ConfigurationLocation;
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00006188 File Offset: 0x00004388
		internal void Add(ConfigurationLocation loc)
		{
			base.InnerList.Add(loc);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00006198 File Offset: 0x00004398
		internal ConfigurationLocation Find(string location)
		{
			foreach (object obj in base.InnerList)
			{
				ConfigurationLocation configurationLocation = (ConfigurationLocation)obj;
				if (string.Compare(configurationLocation.Path, location, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return configurationLocation;
				}
			}
			return null;
		}
	}
}
