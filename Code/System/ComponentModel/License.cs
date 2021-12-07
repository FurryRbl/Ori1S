using System;

namespace System.ComponentModel
{
	/// <summary>Provides the abstract base class for all licenses. A license is granted to a specific instance of a component.</summary>
	// Token: 0x02000174 RID: 372
	public abstract class License : IDisposable
	{
		/// <summary>When overridden in a derived class, gets the license key granted to this component.</summary>
		/// <returns>A license key granted to this component.</returns>
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000CE5 RID: 3301
		public abstract string LicenseKey { get; }

		/// <summary>When overridden in a derived class, disposes of the resources used by the license.</summary>
		// Token: 0x06000CE6 RID: 3302
		public abstract void Dispose();
	}
}
