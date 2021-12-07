using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers for types of inheritance levels.</summary>
	// Token: 0x02000160 RID: 352
	public enum InheritanceLevel
	{
		/// <summary>The object is inherited.</summary>
		// Token: 0x0400037D RID: 893
		Inherited = 1,
		/// <summary>The object is inherited, but has read-only access.</summary>
		// Token: 0x0400037E RID: 894
		InheritedReadOnly,
		/// <summary>The object is not inherited.</summary>
		// Token: 0x0400037F RID: 895
		NotInherited
	}
}
