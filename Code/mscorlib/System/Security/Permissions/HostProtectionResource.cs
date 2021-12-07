using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies categories of functionality potentially harmful to the host if invoked by a method or class.</summary>
	// Token: 0x020005FE RID: 1534
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum HostProtectionResource
	{
		/// <summary>Exposes no host resources.</summary>
		// Token: 0x0400195F RID: 6495
		None = 0,
		/// <summary>Exposes synchronization.</summary>
		// Token: 0x04001960 RID: 6496
		Synchronization = 1,
		/// <summary>Exposes state that might be shared between threads.</summary>
		// Token: 0x04001961 RID: 6497
		SharedState = 2,
		/// <summary>Might create or destroy other processes.</summary>
		// Token: 0x04001962 RID: 6498
		ExternalProcessMgmt = 4,
		/// <summary>Might exit the current process, terminating the server.</summary>
		// Token: 0x04001963 RID: 6499
		SelfAffectingProcessMgmt = 8,
		/// <summary>Creates or manipulates threads other than its own, which might be harmful to the host.</summary>
		// Token: 0x04001964 RID: 6500
		ExternalThreading = 16,
		/// <summary>Manipulates threads in a way that only affects user code.</summary>
		// Token: 0x04001965 RID: 6501
		SelfAffectingThreading = 32,
		/// <summary>Exposes the security infrastructure.</summary>
		// Token: 0x04001966 RID: 6502
		SecurityInfrastructure = 64,
		/// <summary>Exposes the user interface.</summary>
		// Token: 0x04001967 RID: 6503
		UI = 128,
		/// <summary>Might cause a resource leak on termination, if not protected by a safe handle or some other means of ensuring the release of resources.</summary>
		// Token: 0x04001968 RID: 6504
		MayLeakOnAbort = 256,
		/// <summary>Exposes all host resources.</summary>
		// Token: 0x04001969 RID: 6505
		All = 511
	}
}
