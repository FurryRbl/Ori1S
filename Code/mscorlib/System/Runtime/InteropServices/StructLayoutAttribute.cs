using System;

namespace System.Runtime.InteropServices
{
	/// <summary>The StructLayoutAttribute class allows the user to control the physical layout of the data fields of a class or structure.</summary>
	// Token: 0x02000058 RID: 88
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	public sealed class StructLayoutAttribute : Attribute
	{
		/// <summary>Initalizes a new instance of the <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.LayoutKind" /> enumeration member.</summary>
		/// <param name="layoutKind">One of the <see cref="T:System.Runtime.InteropServices.LayoutKind" /> values that specifes how the class or structure should be arranged. </param>
		// Token: 0x0600068A RID: 1674 RVA: 0x00014D20 File Offset: 0x00012F20
		public StructLayoutAttribute(short layoutKind)
		{
			this.lkind = (LayoutKind)layoutKind;
		}

		/// <summary>Initalizes a new instance of the <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.LayoutKind" /> enumeration member.</summary>
		/// <param name="layoutKind">One of the <see cref="T:System.Runtime.InteropServices.LayoutKind" /> values that specifes how the class or structure should be arranged. </param>
		// Token: 0x0600068B RID: 1675 RVA: 0x00014D40 File Offset: 0x00012F40
		public StructLayoutAttribute(LayoutKind layoutKind)
		{
			this.lkind = layoutKind;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.LayoutKind" /> value that specifies how the class or structure is arranged.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.LayoutKind" /> value that specifies how the class or structure is arranged.</returns>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00014D60 File Offset: 0x00012F60
		public LayoutKind Value
		{
			get
			{
				return this.lkind;
			}
		}

		/// <summary>Indicates how string data fields within the class should be marshaled as LPWSTR or LPSTR by default.</summary>
		// Token: 0x040000AE RID: 174
		public CharSet CharSet = CharSet.Auto;

		/// <summary>Controls the alignment of data fields of a class or structure in memory.</summary>
		// Token: 0x040000AF RID: 175
		public int Pack = 8;

		/// <summary>Indicates the absolute size of the class or structure.</summary>
		// Token: 0x040000B0 RID: 176
		public int Size;

		// Token: 0x040000B1 RID: 177
		private LayoutKind lkind;
	}
}
