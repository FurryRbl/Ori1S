using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the defined native object types.</summary>
	// Token: 0x02000591 RID: 1425
	public enum ResourceType
	{
		/// <summary>An unknown object type.</summary>
		// Token: 0x0400175C RID: 5980
		Unknown,
		/// <summary>A file or directory.</summary>
		// Token: 0x0400175D RID: 5981
		FileObject,
		/// <summary>A Windows service.</summary>
		// Token: 0x0400175E RID: 5982
		Service,
		/// <summary>A printer.</summary>
		// Token: 0x0400175F RID: 5983
		Printer,
		/// <summary>A registry key.</summary>
		// Token: 0x04001760 RID: 5984
		RegistryKey,
		/// <summary>A network share.</summary>
		// Token: 0x04001761 RID: 5985
		LMShare,
		/// <summary>A local kernel object.</summary>
		// Token: 0x04001762 RID: 5986
		KernelObject,
		/// <summary>A window station or desktop object on the local computer.</summary>
		// Token: 0x04001763 RID: 5987
		WindowObject,
		/// <summary>A directory service (DS) object or a property set or property of a directory service object.</summary>
		// Token: 0x04001764 RID: 5988
		DSObject,
		/// <summary>A directory service object and all of its property sets and properties.</summary>
		// Token: 0x04001765 RID: 5989
		DSObjectAll,
		/// <summary>An object defined by a provider.</summary>
		// Token: 0x04001766 RID: 5990
		ProviderDefined,
		/// <summary>A Windows Management Instrumentation (WMI) object.</summary>
		// Token: 0x04001767 RID: 5991
		WmiGuidObject,
		/// <summary>An object for a registry entry under WOW64.</summary>
		// Token: 0x04001768 RID: 5992
		RegistryWow6432Key
	}
}
