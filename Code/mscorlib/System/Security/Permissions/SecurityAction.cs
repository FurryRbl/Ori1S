using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the security actions that can be performed using declarative security.</summary>
	// Token: 0x0200061A RID: 1562
	[ComVisible(true)]
	[Serializable]
	public enum SecurityAction
	{
		/// <summary>All callers higher in the call stack are required to have been granted the permission specified by the current permission object (see Security Demands).</summary>
		// Token: 0x040019E2 RID: 6626
		Demand = 2,
		/// <summary>The calling code can access the resource identified by the current permission object, even if callers higher in the stack have not been granted permission to access the resource (see Using the Assert Method).</summary>
		// Token: 0x040019E3 RID: 6627
		Assert,
		/// <summary>The ability to access the resource specified by the current permission object is denied to callers, even if they have been granted permission to access it (see Using the Deny Method).</summary>
		// Token: 0x040019E4 RID: 6628
		Deny,
		/// <summary>Only the resources specified by this permission object can be accessed, even if the code has been granted permission to access other resources (see Using the PermitOnly Method).</summary>
		// Token: 0x040019E5 RID: 6629
		PermitOnly,
		/// <summary>The immediate caller is required to have been granted the specified permission.</summary>
		// Token: 0x040019E6 RID: 6630
		LinkDemand,
		/// <summary>The derived class inheriting the class or overriding a method is required to have been granted the specified permission. For more information, see Inheritance Demands.</summary>
		// Token: 0x040019E7 RID: 6631
		InheritanceDemand,
		/// <summary>The request for the minimum permissions required for code to run. This action can only be used within the scope of the assembly.</summary>
		// Token: 0x040019E8 RID: 6632
		RequestMinimum,
		/// <summary>The request for additional permissions that are optional (not required to run). This request implicitly refuses all other permissions not specifically requested. This action can only be used within the scope of the assembly. </summary>
		// Token: 0x040019E9 RID: 6633
		RequestOptional,
		/// <summary>The request that permissions that might be misused will not be granted to the calling code. This action can only be used within the scope of the assembly.</summary>
		// Token: 0x040019EA RID: 6634
		RequestRefuse
	}
}
