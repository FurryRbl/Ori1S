using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates how to marshal the data between managed and unmanaged code.</summary>
	// Token: 0x02000042 RID: 66
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	public sealed class MarshalAsAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.MarshalAsAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> value.</summary>
		/// <param name="unmanagedType">The <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> value the data is to be marshaled as. </param>
		// Token: 0x0600065B RID: 1627 RVA: 0x00014A98 File Offset: 0x00012C98
		public MarshalAsAttribute(short unmanagedType)
		{
			this.utype = (UnmanagedType)unmanagedType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.MarshalAsAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> enumeration member.</summary>
		/// <param name="unmanagedType">The <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> value the data is to be marshaled as. </param>
		// Token: 0x0600065C RID: 1628 RVA: 0x00014AA8 File Offset: 0x00012CA8
		public MarshalAsAttribute(UnmanagedType unmanagedType)
		{
			this.utype = unmanagedType;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> value the data is to be marshaled as.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> value the data is to be marshaled as.</returns>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00014AB8 File Offset: 0x00012CB8
		public UnmanagedType Value
		{
			get
			{
				return this.utype;
			}
		}

		// Token: 0x04000090 RID: 144
		private UnmanagedType utype;

		/// <summary>Specifies the element type of the unmanaged <see cref="F:System.Runtime.InteropServices.UnmanagedType.LPArray" /> or <see cref="F:System.Runtime.InteropServices.UnmanagedType.ByValArray" />.</summary>
		// Token: 0x04000091 RID: 145
		public UnmanagedType ArraySubType;

		/// <summary>Provides additional information to a custom marshaler.</summary>
		// Token: 0x04000092 RID: 146
		public string MarshalCookie;

		/// <summary>Specifies the fully qualified name of a custom marshaler.</summary>
		// Token: 0x04000093 RID: 147
		[ComVisible(true)]
		public string MarshalType;

		/// <summary>Implements <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.MarshalType" /> as a type.</summary>
		// Token: 0x04000094 RID: 148
		[ComVisible(true)]
		public Type MarshalTypeRef;

		/// <summary>Indicates the element type of the <see cref="F:System.Runtime.InteropServices.UnmanagedType.SafeArray" />.</summary>
		// Token: 0x04000095 RID: 149
		public VarEnum SafeArraySubType;

		/// <summary>Indicates the number of elements in the fixed-length array or the number of characters (not bytes) in a string to import.</summary>
		// Token: 0x04000096 RID: 150
		public int SizeConst;

		/// <summary>Indicates which parameter contains the count of array elements, much like size_is in COM, and is zero-based.</summary>
		// Token: 0x04000097 RID: 151
		public short SizeParamIndex;

		/// <summary>Indicates the user-defined element type of the <see cref="F:System.Runtime.InteropServices.UnmanagedType.SafeArray" />.</summary>
		// Token: 0x04000098 RID: 152
		public Type SafeArrayUserDefinedSubType;

		/// <summary>Specifies the parameter index of the unmanaged iid_is attribute used by COM.</summary>
		// Token: 0x04000099 RID: 153
		public int IidParameterIndex;
	}
}
