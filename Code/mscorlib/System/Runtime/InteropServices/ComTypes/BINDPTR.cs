using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains a pointer to a bound-to <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> structure, <see cref="T:System.Runtime.InteropServices.VARDESC" /> structure, or an ITypeComp interface.</summary>
	// Token: 0x020003E5 RID: 997
	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
	public struct BINDPTR
	{
		/// <summary>Represents a pointer to a <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> structure.</summary>
		// Token: 0x04001284 RID: 4740
		[FieldOffset(0)]
		public IntPtr lpfuncdesc;

		/// <summary>Represents a pointer to an <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeComp" /> interface.</summary>
		// Token: 0x04001285 RID: 4741
		[FieldOffset(0)]
		public IntPtr lptcomp;

		/// <summary>Represents a pointer to a <see cref="T:System.Runtime.InteropServices.VARDESC" /> structure.</summary>
		// Token: 0x04001286 RID: 4742
		[FieldOffset(0)]
		public IntPtr lpvardesc;
	}
}
