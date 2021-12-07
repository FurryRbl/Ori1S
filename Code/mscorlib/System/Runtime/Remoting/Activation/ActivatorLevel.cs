using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Activation
{
	/// <summary>Defines the appropriate position for a <see cref="T:System.Activator" /> in the chain of activators.</summary>
	// Token: 0x0200043A RID: 1082
	[ComVisible(true)]
	[Serializable]
	public enum ActivatorLevel
	{
		/// <summary>Constructs a blank object and runs the constructor.</summary>
		// Token: 0x040013BB RID: 5051
		Construction = 4,
		/// <summary>Finds or creates a suitable context.</summary>
		// Token: 0x040013BC RID: 5052
		Context = 8,
		/// <summary>Finds or creates a <see cref="T:System.AppDomain" />.</summary>
		// Token: 0x040013BD RID: 5053
		AppDomain = 12,
		/// <summary>Starts a process.</summary>
		// Token: 0x040013BE RID: 5054
		Process = 16,
		/// <summary>Finds a suitable computer.</summary>
		// Token: 0x040013BF RID: 5055
		Machine = 20
	}
}
