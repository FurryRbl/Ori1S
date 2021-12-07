using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that a field should be treated as containing a fixed number of elements of the specified primitive type. This class cannot be inherited. </summary>
	// Token: 0x02000050 RID: 80
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	public sealed class FixedBufferAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.FixedBufferAttribute" /> class. </summary>
		/// <param name="elementType">The type of the elements contained in the buffer.</param>
		/// <param name="length">The number of elements in the buffer.</param>
		// Token: 0x06000678 RID: 1656 RVA: 0x00014BD0 File Offset: 0x00012DD0
		public FixedBufferAttribute(Type elementType, int length)
		{
			this.elementType = elementType;
			this.length = length;
		}

		/// <summary>Gets the type of the elements contained in the fixed buffer. </summary>
		/// <returns>The type of the elements.</returns>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00014BE8 File Offset: 0x00012DE8
		public Type ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		/// <summary>Gets the number of elements in the fixed buffer. </summary>
		/// <returns>The number of elements in the fixed buffer.</returns>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00014BF0 File Offset: 0x00012DF0
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x040000A3 RID: 163
		private Type elementType;

		// Token: 0x040000A4 RID: 164
		private int length;
	}
}
