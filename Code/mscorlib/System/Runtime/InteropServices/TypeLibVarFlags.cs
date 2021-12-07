using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Describes the original settings of the <see cref="T:System.Runtime.InteropServices.VARFLAGS" /> in the COM type library from which the variable was imported.</summary>
	// Token: 0x020003CC RID: 972
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum TypeLibVarFlags
	{
		/// <summary>Assignment to the variable should not be allowed.</summary>
		// Token: 0x04001208 RID: 4616
		FReadOnly = 1,
		/// <summary>The variable returns an object that is a source of events.</summary>
		// Token: 0x04001209 RID: 4617
		FSource = 2,
		/// <summary>The variable supports data binding.</summary>
		// Token: 0x0400120A RID: 4618
		FBindable = 4,
		/// <summary>Indicates that the property supports the COM OnRequestEdit notification.</summary>
		// Token: 0x0400120B RID: 4619
		FRequestEdit = 8,
		/// <summary>The variable is displayed as bindable. <see cref="F:System.Runtime.InteropServices.TypeLibVarFlags.FBindable" /> must also be set.</summary>
		// Token: 0x0400120C RID: 4620
		FDisplayBind = 16,
		/// <summary>The variable is the single property that best represents the object. Only one variable in a type info can have this value.</summary>
		// Token: 0x0400120D RID: 4621
		FDefaultBind = 32,
		/// <summary>The variable should not be displayed in a browser, though it exists and is bindable.</summary>
		// Token: 0x0400120E RID: 4622
		FHidden = 64,
		/// <summary>This flag is intended for system-level functions or functions that type browsers should not display.</summary>
		// Token: 0x0400120F RID: 4623
		FRestricted = 128,
		/// <summary>Permits an optimization in which the compiler looks for a member named "xyz" on the type "abc". If such a member is found and is flagged as an accessor function for an element of the default collection, then a call is generated to that member function.</summary>
		// Token: 0x04001210 RID: 4624
		FDefaultCollelem = 256,
		/// <summary>The default display in the user interface.</summary>
		// Token: 0x04001211 RID: 4625
		FUiDefault = 512,
		/// <summary>The variable appears in an object browser, but not in a properties browser.</summary>
		// Token: 0x04001212 RID: 4626
		FNonBrowsable = 1024,
		/// <summary>Tags the interface as having default behaviors.</summary>
		// Token: 0x04001213 RID: 4627
		FReplaceable = 2048,
		/// <summary>The variable is mapped as individual bindable properties.</summary>
		// Token: 0x04001214 RID: 4628
		FImmediateBind = 4096
	}
}
