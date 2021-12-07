using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Controls the layout of an object when exported to unmanaged code.</summary>
	// Token: 0x020003AB RID: 939
	[ComVisible(true)]
	[Serializable]
	public enum LayoutKind
	{
		/// <summary>The members of the object are laid out sequentially, in the order in which they appear when exported to unmanaged memory. The members are laid out according to the packing specified in <see cref="F:System.Runtime.InteropServices.StructLayoutAttribute.Pack" />, and can be noncontiguous.</summary>
		// Token: 0x0400115B RID: 4443
		Sequential,
		/// <summary>The precise position of each member of an object in unmanaged memory is explicitly controlled. Each member must use the <see cref="T:System.Runtime.InteropServices.FieldOffsetAttribute" /> to indicate the position of that field within the type.</summary>
		// Token: 0x0400115C RID: 4444
		Explicit = 2,
		/// <summary>The runtime automatically chooses an appropriate layout for the members of an object in unmanaged memory. Objects defined with this enumeration member cannot be exposed outside of managed code. Attempting to do so generates an exception.</summary>
		// Token: 0x0400115D RID: 4445
		Auto
	}
}
