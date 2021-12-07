using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Specifies how to invoke a function by IDispatch::Invoke.</summary>
	// Token: 0x020003FE RID: 1022
	[Flags]
	[Serializable]
	public enum INVOKEKIND
	{
		/// <summary>The member is called using a normal function invocation syntax.</summary>
		// Token: 0x040012E0 RID: 4832
		INVOKE_FUNC = 1,
		/// <summary>The function is invoked using a normal property access syntax.</summary>
		// Token: 0x040012E1 RID: 4833
		INVOKE_PROPERTYGET = 2,
		/// <summary>The function is invoked using a property value assignment syntax.</summary>
		// Token: 0x040012E2 RID: 4834
		INVOKE_PROPERTYPUT = 4,
		/// <summary>The function is invoked using a property reference assignment syntax.</summary>
		// Token: 0x040012E3 RID: 4835
		INVOKE_PROPERTYPUTREF = 8
	}
}
