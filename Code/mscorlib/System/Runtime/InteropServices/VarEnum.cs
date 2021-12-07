using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates how to marshal the array elements when an array is marshaled from managed to unmanaged code as a <see cref="F:System.Runtime.InteropServices.UnmanagedType.SafeArray" />.</summary>
	// Token: 0x020003E2 RID: 994
	[ComVisible(true)]
	[Serializable]
	public enum VarEnum
	{
		/// <summary>Indicates that a value was not specified.</summary>
		// Token: 0x04001249 RID: 4681
		VT_EMPTY,
		/// <summary>Indicates a null value, similar to a null value in SQL.</summary>
		// Token: 0x0400124A RID: 4682
		VT_NULL,
		/// <summary>Indicates a short integer.</summary>
		// Token: 0x0400124B RID: 4683
		VT_I2,
		/// <summary>Indicates a long integer.</summary>
		// Token: 0x0400124C RID: 4684
		VT_I4,
		/// <summary>Indicates a float value.</summary>
		// Token: 0x0400124D RID: 4685
		VT_R4,
		/// <summary>Indicates a double value.</summary>
		// Token: 0x0400124E RID: 4686
		VT_R8,
		/// <summary>Indicates a currency value.</summary>
		// Token: 0x0400124F RID: 4687
		VT_CY,
		/// <summary>Indicates a DATE value.</summary>
		// Token: 0x04001250 RID: 4688
		VT_DATE,
		/// <summary>Indicates a BSTR string.</summary>
		// Token: 0x04001251 RID: 4689
		VT_BSTR,
		/// <summary>Indicates an IDispatch pointer.</summary>
		// Token: 0x04001252 RID: 4690
		VT_DISPATCH,
		/// <summary>Indicates an SCODE.</summary>
		// Token: 0x04001253 RID: 4691
		VT_ERROR,
		/// <summary>Indicates a Boolean value.</summary>
		// Token: 0x04001254 RID: 4692
		VT_BOOL,
		/// <summary>Indicates a VARIANT far pointer.</summary>
		// Token: 0x04001255 RID: 4693
		VT_VARIANT,
		/// <summary>Indicates an IUnknown pointer.</summary>
		// Token: 0x04001256 RID: 4694
		VT_UNKNOWN,
		/// <summary>Indicates a decimal value.</summary>
		// Token: 0x04001257 RID: 4695
		VT_DECIMAL,
		/// <summary>Indicates a char value.</summary>
		// Token: 0x04001258 RID: 4696
		VT_I1 = 16,
		/// <summary>Indicates a byte.</summary>
		// Token: 0x04001259 RID: 4697
		VT_UI1,
		/// <summary>Indicates an unsignedshort.</summary>
		// Token: 0x0400125A RID: 4698
		VT_UI2,
		/// <summary>Indicates an unsignedlong.</summary>
		// Token: 0x0400125B RID: 4699
		VT_UI4,
		/// <summary>Indicates a 64-bit integer.</summary>
		// Token: 0x0400125C RID: 4700
		VT_I8,
		/// <summary>Indicates an 64-bit unsigned integer.</summary>
		// Token: 0x0400125D RID: 4701
		VT_UI8,
		/// <summary>Indicates an integer value.</summary>
		// Token: 0x0400125E RID: 4702
		VT_INT,
		/// <summary>Indicates an unsigned integer value.</summary>
		// Token: 0x0400125F RID: 4703
		VT_UINT,
		/// <summary>Indicates a C style void.</summary>
		// Token: 0x04001260 RID: 4704
		VT_VOID,
		/// <summary>Indicates an HRESULT.</summary>
		// Token: 0x04001261 RID: 4705
		VT_HRESULT,
		/// <summary>Indicates a pointer type.</summary>
		// Token: 0x04001262 RID: 4706
		VT_PTR,
		/// <summary>Indicates a SAFEARRAY. Not valid in a VARIANT.</summary>
		// Token: 0x04001263 RID: 4707
		VT_SAFEARRAY,
		/// <summary>Indicates a C style array.</summary>
		// Token: 0x04001264 RID: 4708
		VT_CARRAY,
		/// <summary>Indicates a user defined type.</summary>
		// Token: 0x04001265 RID: 4709
		VT_USERDEFINED,
		/// <summary>Indicates a null-terminated string.</summary>
		// Token: 0x04001266 RID: 4710
		VT_LPSTR,
		/// <summary>Indicates a wide string terminated by null.</summary>
		// Token: 0x04001267 RID: 4711
		VT_LPWSTR,
		/// <summary>Indicates a user defined type.</summary>
		// Token: 0x04001268 RID: 4712
		VT_RECORD = 36,
		/// <summary>Indicates a FILETIME value.</summary>
		// Token: 0x04001269 RID: 4713
		VT_FILETIME = 64,
		/// <summary>Indicates length prefixed bytes.</summary>
		// Token: 0x0400126A RID: 4714
		VT_BLOB,
		/// <summary>Indicates that the name of a stream follows.</summary>
		// Token: 0x0400126B RID: 4715
		VT_STREAM,
		/// <summary>Indicates that the name of a storage follows.</summary>
		// Token: 0x0400126C RID: 4716
		VT_STORAGE,
		/// <summary>Indicates that a stream contains an object.</summary>
		// Token: 0x0400126D RID: 4717
		VT_STREAMED_OBJECT,
		/// <summary>Indicates that a storage contains an object.</summary>
		// Token: 0x0400126E RID: 4718
		VT_STORED_OBJECT,
		/// <summary>Indicates that a blob contains an object.</summary>
		// Token: 0x0400126F RID: 4719
		VT_BLOB_OBJECT,
		/// <summary>Indicates the clipboard format.</summary>
		// Token: 0x04001270 RID: 4720
		VT_CF,
		/// <summary>Indicates a class ID.</summary>
		// Token: 0x04001271 RID: 4721
		VT_CLSID,
		/// <summary>Indicates a simple, counted array.</summary>
		// Token: 0x04001272 RID: 4722
		VT_VECTOR = 4096,
		/// <summary>Indicates a SAFEARRAY pointer.</summary>
		// Token: 0x04001273 RID: 4723
		VT_ARRAY = 8192,
		/// <summary>Indicates that a value is a reference.</summary>
		// Token: 0x04001274 RID: 4724
		VT_BYREF = 16384
	}
}
