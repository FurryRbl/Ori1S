using System;

namespace System.ComponentModel
{
	/// <summary>Allows coordination of initialization for a component and its dependent properties.</summary>
	// Token: 0x0200016F RID: 367
	public interface ISupportInitializeNotification : ISupportInitialize
	{
		/// <summary>Occurs when initialization of the component is completed.</summary>
		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06000CD1 RID: 3281
		// (remove) Token: 0x06000CD2 RID: 3282
		event EventHandler Initialized;

		/// <summary>Gets a value indicating whether the component is initialized.</summary>
		/// <returns>true to indicate the component has completed initialization; otherwise, false. </returns>
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000CD3 RID: 3283
		bool IsInitialized { get; }
	}
}
