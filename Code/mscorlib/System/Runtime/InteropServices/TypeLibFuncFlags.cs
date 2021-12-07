using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Describes the original settings of the FUNCFLAGS in the COM type library from where this method was imported.</summary>
	// Token: 0x020003C6 RID: 966
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibFuncFlags
	{
		/// <summary>This flag is intended for system-level functions or functions that type browsers should not display.</summary>
		// Token: 0x040011DB RID: 4571
		FRestricted = 1,
		/// <summary>The function returns an object that is a source of events.</summary>
		// Token: 0x040011DC RID: 4572
		FSource = 2,
		/// <summary>The function that supports data binding.</summary>
		// Token: 0x040011DD RID: 4573
		FBindable = 4,
		/// <summary>When set, any call to a method that sets the property results first in a call to IPropertyNotifySink::OnRequestEdit.</summary>
		// Token: 0x040011DE RID: 4574
		FRequestEdit = 8,
		/// <summary>The function that is displayed to the user as bindable. <see cref="F:System.Runtime.InteropServices.TypeLibFuncFlags.FBindable" /> must also be set.</summary>
		// Token: 0x040011DF RID: 4575
		FDisplayBind = 16,
		/// <summary>The function that best represents the object. Only one function in a type information can have this attribute.</summary>
		// Token: 0x040011E0 RID: 4576
		FDefaultBind = 32,
		/// <summary>The function should not be displayed to the user, although it exists and is bindable.</summary>
		// Token: 0x040011E1 RID: 4577
		FHidden = 64,
		/// <summary>The function supports GetLastError.</summary>
		// Token: 0x040011E2 RID: 4578
		FUsesGetLastError = 128,
		/// <summary>Permits an optimization in which the compiler looks for a member named "xyz" on the type "abc". If such a member is found and is flagged as an accessor function for an element of the default collection, then a call is generated to that member function.</summary>
		// Token: 0x040011E3 RID: 4579
		FDefaultCollelem = 256,
		/// <summary>The type information member is the default member for display in the user interface.</summary>
		// Token: 0x040011E4 RID: 4580
		FUiDefault = 512,
		/// <summary>The property appears in an object browser, but not in a properties browser.</summary>
		// Token: 0x040011E5 RID: 4581
		FNonBrowsable = 1024,
		/// <summary>Tags the interface as having default behaviors.</summary>
		// Token: 0x040011E6 RID: 4582
		FReplaceable = 2048,
		/// <summary>The function is mapped as individual bindable properties.</summary>
		// Token: 0x040011E7 RID: 4583
		FImmediateBind = 4096
	}
}
