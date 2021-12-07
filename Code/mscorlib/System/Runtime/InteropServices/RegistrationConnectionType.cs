using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Defines the types of connections to a class object.</summary>
	// Token: 0x020003B3 RID: 947
	[Flags]
	public enum RegistrationConnectionType
	{
		/// <summary>Multiple applications can connect to the class object through calls to CoGetClassObject.</summary>
		// Token: 0x04001182 RID: 4482
		MultipleUse = 1,
		/// <summary>Registers separate CLSCTX_LOCAL_SERVER and CLSCTX_INPROC_SERVER class factories.</summary>
		// Token: 0x04001183 RID: 4483
		MultiSeparate = 2,
		/// <summary>Once an application is connected to a class object with CoGetClassObject, the class object is removed from public view so that no other applications can connect to it. This value is commonly used for single document interface (SDI) applications.</summary>
		// Token: 0x04001184 RID: 4484
		SingleUse = 0,
		/// <summary>Suspends registration and activation requests for the specified CLSID until there is a call to CoResumeClassObjects.</summary>
		// Token: 0x04001185 RID: 4485
		Suspended = 4,
		/// <summary>The class object is a surrogate process used to run DLL servers.</summary>
		// Token: 0x04001186 RID: 4486
		Surrogate = 8
	}
}
