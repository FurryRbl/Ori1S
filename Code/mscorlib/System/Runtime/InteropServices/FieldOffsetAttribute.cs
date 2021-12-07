using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates the physical position of fields within the unmanaged representation of a class or structure.</summary>
	// Token: 0x02000059 RID: 89
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	public sealed class FieldOffsetAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.FieldOffsetAttribute" /> class with the offset in the structure to the beginning of the field.</summary>
		/// <param name="offset">The offset, in bytes, from the beginning of the structure to the beginning of the field. </param>
		// Token: 0x0600068D RID: 1677 RVA: 0x00014D68 File Offset: 0x00012F68
		public FieldOffsetAttribute(int offset)
		{
			this.val = offset;
		}

		/// <summary>Gets the offset from the beginning of the structure to the beginning of the field.</summary>
		/// <returns>The offset from the beginning of the structure to the beginning of the field.</returns>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00014D78 File Offset: 0x00012F78
		public int Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x040000B2 RID: 178
		private int val;
	}
}
