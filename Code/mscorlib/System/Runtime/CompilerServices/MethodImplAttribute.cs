using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies the details of how a method is implemented. This class cannot be inherited. </summary>
	// Token: 0x02000041 RID: 65
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class MethodImplAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.MethodImplAttribute" /> class.</summary>
		// Token: 0x06000657 RID: 1623 RVA: 0x00014A68 File Offset: 0x00012C68
		public MethodImplAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.MethodImplAttribute" /> class with the specified <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value.</summary>
		/// <param name="value">A bitmask representing the desired <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value which specifies properties of the attributed method. </param>
		// Token: 0x06000658 RID: 1624 RVA: 0x00014A70 File Offset: 0x00012C70
		public MethodImplAttribute(short value)
		{
			this._val = (MethodImplOptions)value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.MethodImplAttribute" /> class with the specified <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value.</summary>
		/// <param name="methodImplOptions">An enumeration value specifying properties of the attributed method. </param>
		// Token: 0x06000659 RID: 1625 RVA: 0x00014A80 File Offset: 0x00012C80
		public MethodImplAttribute(MethodImplOptions methodImplOptions)
		{
			this._val = methodImplOptions;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value describing the attributed method.</summary>
		/// <returns>The <see cref="T:System.Runtime.CompilerServices.MethodImplOptions" /> value describing the attributed method.</returns>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x00014A90 File Offset: 0x00012C90
		public MethodImplOptions Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x0400008E RID: 142
		private MethodImplOptions _val;

		/// <summary>A <see cref="T:System.Runtime.CompilerServices.MethodCodeType" /> value indicating what kind of implementation is provided for this method.</summary>
		// Token: 0x0400008F RID: 143
		public MethodCodeType MethodCodeType;
	}
}
