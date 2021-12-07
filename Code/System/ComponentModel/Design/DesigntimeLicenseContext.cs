using System;
using System.Collections;
using System.Reflection;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a design-time license context that can support a license provider at design time.</summary>
	// Token: 0x02000103 RID: 259
	public class DesigntimeLicenseContext : LicenseContext
	{
		/// <summary>Gets a saved license key.</summary>
		/// <returns>The saved license key that matches the specified type.</returns>
		/// <param name="type">The type of the license key. </param>
		/// <param name="resourceAssembly">The assembly to get the key from. </param>
		// Token: 0x06000A7E RID: 2686 RVA: 0x0001D53C File Offset: 0x0001B73C
		public override string GetSavedLicenseKey(Type type, Assembly resourceAssembly)
		{
			return (string)this.keys[type];
		}

		/// <summary>Sets a saved license key.</summary>
		/// <param name="type">The type of the license key. </param>
		/// <param name="key">The license key. </param>
		// Token: 0x06000A7F RID: 2687 RVA: 0x0001D550 File Offset: 0x0001B750
		public override void SetSavedLicenseKey(Type type, string key)
		{
			this.keys[type] = key;
		}

		/// <summary>Gets the license usage mode.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.LicenseUsageMode" /> indicating the licensing mode for the context.</returns>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0001D560 File Offset: 0x0001B760
		public override LicenseUsageMode UsageMode
		{
			get
			{
				return LicenseUsageMode.Designtime;
			}
		}

		// Token: 0x040002C8 RID: 712
		internal Hashtable keys = new Hashtable();
	}
}
