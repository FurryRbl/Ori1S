using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Identifies how to marshal parameters or fields to unmanaged code.</summary>
	// Token: 0x020003DF RID: 991
	[ComVisible(true)]
	[Serializable]
	public enum UnmanagedType
	{
		/// <summary>A 4-byte Boolean value (true != 0, false = 0). This is the Win32 BOOL type.</summary>
		// Token: 0x0400121E RID: 4638
		Bool = 2,
		/// <summary>A 1-byte signed integer. You can use this member to transform a Boolean value into a 1-byte, C-style bool (true = 1, false = 0).</summary>
		// Token: 0x0400121F RID: 4639
		I1,
		/// <summary>A 1-byte unsigned integer.</summary>
		// Token: 0x04001220 RID: 4640
		U1,
		/// <summary>A 2-byte signed integer.</summary>
		// Token: 0x04001221 RID: 4641
		I2,
		/// <summary>A 2-byte unsigned integer.</summary>
		// Token: 0x04001222 RID: 4642
		U2,
		/// <summary>A 4-byte signed integer.</summary>
		// Token: 0x04001223 RID: 4643
		I4,
		/// <summary>A 4-byte unsigned integer.</summary>
		// Token: 0x04001224 RID: 4644
		U4,
		/// <summary>An 8-byte signed integer.</summary>
		// Token: 0x04001225 RID: 4645
		I8,
		/// <summary>An 8-byte unsigned integer.</summary>
		// Token: 0x04001226 RID: 4646
		U8,
		/// <summary>A 4-byte floating point number.</summary>
		// Token: 0x04001227 RID: 4647
		R4,
		/// <summary>An 8-byte floating point number.</summary>
		// Token: 0x04001228 RID: 4648
		R8,
		/// <summary>Used on a <see cref="T:System.Decimal" /> to marshal the decimal value as a COM currency type instead of as a Decimal.</summary>
		// Token: 0x04001229 RID: 4649
		Currency = 15,
		/// <summary>A Unicode character string that is a length-prefixed double byte. You can use this member, which is the default string in COM, on the <see cref="T:System.String" /> data type.</summary>
		// Token: 0x0400122A RID: 4650
		BStr = 19,
		/// <summary>A single byte, null-terminated ANSI character string. You can use this member on the <see cref="T:System.String" /> or <see cref="T:System.Text.StringBuilder" /> data types</summary>
		// Token: 0x0400122B RID: 4651
		LPStr,
		/// <summary>A 2-byte, null-terminated Unicode character string.</summary>
		// Token: 0x0400122C RID: 4652
		LPWStr,
		/// <summary>A platform-dependent character string: ANSI on Windows 98 and Unicode on Windows NT and Windows XP. This value is only supported for platform invoke, and not COM interop, because exporting a string of type LPTStr is not supported.</summary>
		// Token: 0x0400122D RID: 4653
		LPTStr,
		/// <summary>Used for in-line, fixed-length character arrays that appear within a structure. The character type used with <see cref="F:System.Runtime.InteropServices.UnmanagedType.ByValTStr" /> is determined by the <see cref="T:System.Runtime.InteropServices.CharSet" /> argument of the <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> applied to the containing structure. Always use the <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.SizeConst" /> field to indicate the size of the array.</summary>
		// Token: 0x0400122E RID: 4654
		ByValTStr,
		/// <summary>A COM IUnknown pointer. You can use this member on the <see cref="T:System.Object" /> data type.</summary>
		// Token: 0x0400122F RID: 4655
		IUnknown = 25,
		/// <summary>A COM IDispatch pointer (Object in Microsoft Visual Basic 6.0).</summary>
		// Token: 0x04001230 RID: 4656
		IDispatch,
		/// <summary>A VARIANT, which is used to marshal managed formatted classes and value types.</summary>
		// Token: 0x04001231 RID: 4657
		Struct,
		/// <summary>A COM interface pointer. The <see cref="T:System.Guid" /> of the interface is obtained from the class metadata. Use this member to specify the exact interface type or the default interface type if you apply it to a class. This member produces <see cref="F:System.Runtime.InteropServices.UnmanagedType.IUnknown" /> behavior when you apply it to the <see cref="T:System.Object" /> data type.</summary>
		// Token: 0x04001232 RID: 4658
		Interface,
		/// <summary>A SafeArray is a self-describing array that carries the type, rank, and bounds of the associated array data. You can use this member with the <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.SafeArraySubType" /> field to override the default element type.</summary>
		// Token: 0x04001233 RID: 4659
		SafeArray,
		/// <summary>When <see cref="P:System.Runtime.InteropServices.MarshalAsAttribute.Value" /> is set to ByValArray, the <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.SizeConst" /> must be set to indicate the number of elements in the array. The <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.ArraySubType" /> field can optionally contain the <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> of the array elements when it is necessary to differentiate among string types. You can only use this <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> on an array that appear as fields in a structure.</summary>
		// Token: 0x04001234 RID: 4660
		ByValArray,
		/// <summary>A platform-dependent, signed integer. 4-bytes on 32 bit Windows, 8-bytes on 64 bit Windows.</summary>
		// Token: 0x04001235 RID: 4661
		SysInt,
		/// <summary>A platform-dependent, unsigned integer. 4-bytes on 32 bit Windows, 8-bytes on 64 bit Windows.</summary>
		// Token: 0x04001236 RID: 4662
		SysUInt,
		/// <summary>Allows Visual Basic 2005 to change a string in unmanaged code, and have the results reflected in managed code. This value is only supported for platform invoke.</summary>
		// Token: 0x04001237 RID: 4663
		VBByRefStr = 34,
		/// <summary>An ANSI character string that is a length prefixed, single byte. You can use this member on the <see cref="T:System.String" /> data type.</summary>
		// Token: 0x04001238 RID: 4664
		AnsiBStr,
		/// <summary>A length-prefixed, platform-dependent char string. ANSI on Windows 98, Unicode on Windows NT. You rarely use this BSTR-like member.</summary>
		// Token: 0x04001239 RID: 4665
		TBStr,
		/// <summary>A 2-byte, OLE-defined VARIANT_BOOL type (true = -1, false = 0).</summary>
		// Token: 0x0400123A RID: 4666
		VariantBool,
		/// <summary>An integer that can be used as a C-style function pointer. You can use this member on a <see cref="T:System.Delegate" /> data type or a type that inherits from a <see cref="T:System.Delegate" />.</summary>
		// Token: 0x0400123B RID: 4667
		FunctionPtr,
		/// <summary>A dynamic type that determines the type of an object at run time and marshals the object as that type. Valid for platform invoke methods only.</summary>
		// Token: 0x0400123C RID: 4668
		AsAny = 40,
		/// <summary>A pointer to the first element of a C-style array. When marshaling from managed to unmanaged, the length of the array is determined by the length of the managed array. When marshaling from unmanaged to managed, the length of the array is determined from the <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.SizeConst" /> and the <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.SizeParamIndex" /> fields, optionally followed by the unmanaged type of the elements within the array when it is necessary to differentiate among string types.</summary>
		// Token: 0x0400123D RID: 4669
		LPArray = 42,
		/// <summary>A pointer to a C-style structure that you use to marshal managed formatted classes. Valid for platform invoke methods only.</summary>
		// Token: 0x0400123E RID: 4670
		LPStruct,
		/// <summary>Specifies the custom marshaler class when used with <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.MarshalType" /> or <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.MarshalTypeRef" />. The <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.MarshalCookie" /> field can be used to pass additional information to the custom marshaler. You can use this member on any reference type.</summary>
		// Token: 0x0400123F RID: 4671
		CustomMarshaler,
		/// <summary>This native type associated with an <see cref="F:System.Runtime.InteropServices.UnmanagedType.I4" /> or a <see cref="F:System.Runtime.InteropServices.UnmanagedType.U4" /> causes the parameter to be exported as a HRESULT in the exported type library.</summary>
		// Token: 0x04001240 RID: 4672
		Error
	}
}
