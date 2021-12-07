using System;
using System.Reflection;

namespace System.ComponentModel
{
	/// <summary>Specifies when you can use a licensed object and provides a way of obtaining additional services needed to support licenses running within its domain.</summary>
	// Token: 0x02000173 RID: 371
	public class LicenseContext : IServiceProvider
	{
		/// <summary>When overridden in a derived class, returns a saved license key for the specified type, from the specified resource assembly.</summary>
		/// <returns>The <see cref="P:System.ComponentModel.License.LicenseKey" /> for the specified type. This method returns null unless you override it.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of component. </param>
		/// <param name="resourceAssembly">An <see cref="T:System.Reflection.Assembly" /> with the license key. </param>
		// Token: 0x06000CE0 RID: 3296 RVA: 0x00020570 File Offset: 0x0001E770
		public virtual string GetSavedLicenseKey(Type type, Assembly resourceAssembly)
		{
			return null;
		}

		/// <summary>Gets the requested service, if it is available.</summary>
		/// <returns>An instance of the service, or null if the service cannot be found.</returns>
		/// <param name="type">The type of service to retrieve. </param>
		// Token: 0x06000CE1 RID: 3297 RVA: 0x00020574 File Offset: 0x0001E774
		public virtual object GetService(Type type)
		{
			return null;
		}

		/// <summary>When overridden in a derived class, sets a license key for the specified type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the component associated with the license key. </param>
		/// <param name="key">The <see cref="P:System.ComponentModel.License.LicenseKey" /> to save for the type of component. </param>
		// Token: 0x06000CE2 RID: 3298 RVA: 0x00020578 File Offset: 0x0001E778
		public virtual void SetSavedLicenseKey(Type type, string key)
		{
		}

		/// <summary>When overridden in a derived class, gets a value that specifies when you can use a license.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.LicenseUsageMode" /> values that specifies when you can use a license. The default is <see cref="F:System.ComponentModel.LicenseUsageMode.Runtime" />.</returns>
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x0002057C File Offset: 0x0001E77C
		public virtual LicenseUsageMode UsageMode
		{
			get
			{
				return LicenseUsageMode.Runtime;
			}
		}
	}
}
