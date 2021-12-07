using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies access flags for the security permission object.</summary>
	// Token: 0x0200061D RID: 1565
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum SecurityPermissionFlag
	{
		/// <summary>No security access.</summary>
		// Token: 0x040019EF RID: 6639
		NoFlags = 0,
		/// <summary>Ability to assert that all this code's callers have the requisite permission for the operation.</summary>
		// Token: 0x040019F0 RID: 6640
		Assertion = 1,
		/// <summary>Ability to call unmanaged code.</summary>
		// Token: 0x040019F1 RID: 6641
		UnmanagedCode = 2,
		/// <summary>Ability to skip verification of code in this assembly. Code that is unverifiable can be run if this permission is granted.</summary>
		// Token: 0x040019F2 RID: 6642
		SkipVerification = 4,
		/// <summary>Permission for the code to run. Without this permission, managed code will not be executed.</summary>
		// Token: 0x040019F3 RID: 6643
		Execution = 8,
		/// <summary>Ability to use certain advanced operations on threads.</summary>
		// Token: 0x040019F4 RID: 6644
		ControlThread = 16,
		/// <summary>Ability to provide evidence, including the ability to alter the evidence provided by the common language runtime.</summary>
		// Token: 0x040019F5 RID: 6645
		ControlEvidence = 32,
		/// <summary>Ability to view and modify policy.</summary>
		// Token: 0x040019F6 RID: 6646
		ControlPolicy = 64,
		/// <summary>Ability to provide serialization services. Used by serialization formatters.</summary>
		// Token: 0x040019F7 RID: 6647
		SerializationFormatter = 128,
		/// <summary>Ability to specify domain policy.</summary>
		// Token: 0x040019F8 RID: 6648
		ControlDomainPolicy = 256,
		/// <summary>Ability to manipulate the principal object.</summary>
		// Token: 0x040019F9 RID: 6649
		ControlPrincipal = 512,
		/// <summary>Ability to create and manipulate an <see cref="T:System.AppDomain" />.</summary>
		// Token: 0x040019FA RID: 6650
		ControlAppDomain = 1024,
		/// <summary>Permission to configure Remoting types and channels.</summary>
		// Token: 0x040019FB RID: 6651
		RemotingConfiguration = 2048,
		/// <summary>Permission to plug code into the common language runtime infrastructure, such as adding Remoting Context Sinks, Envoy Sinks and Dynamic Sinks.</summary>
		// Token: 0x040019FC RID: 6652
		Infrastructure = 4096,
		/// <summary>Permission to perform explicit binding redirection in the application configuration file. This includes redirection of .NET Framework assemblies that have been unified as well as other assemblies found outside the .NET Framework.</summary>
		// Token: 0x040019FD RID: 6653
		BindingRedirects = 8192,
		/// <summary>The unrestricted state of the permission.</summary>
		// Token: 0x040019FE RID: 6654
		AllFlags = 16383
	}
}
