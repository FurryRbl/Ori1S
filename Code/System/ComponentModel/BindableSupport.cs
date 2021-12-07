using System;

namespace System.ComponentModel
{
	/// <summary>Specifies values to indicate whether a property can be bound to a data element or another property.</summary>
	// Token: 0x020000CF RID: 207
	public enum BindableSupport
	{
		/// <summary>The property is not bindable at design time.</summary>
		// Token: 0x04000252 RID: 594
		No,
		/// <summary>The property is bindable at design time.</summary>
		// Token: 0x04000253 RID: 595
		Yes,
		/// <summary>The property is set to the default.</summary>
		// Token: 0x04000254 RID: 596
		Default
	}
}
