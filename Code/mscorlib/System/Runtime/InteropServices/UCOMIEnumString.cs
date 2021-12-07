using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.IEnumString" /> instead.</summary>
	// Token: 0x020003D4 RID: 980
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Obsolete]
	[Guid("00000101-0000-0000-c000-000000000046")]
	[ComImport]
	public interface UCOMIEnumString
	{
		/// <summary>Retrieves a specified number of items in the enumeration sequence.</summary>
		/// <returns>S_OK if the <paramref name="pceltFetched" /> parameter equals the <paramref name="celt" /> parameter; otherwise, S_FALSE.</returns>
		/// <param name="celt">The number of strings to return in <paramref name="rgelt" />. </param>
		/// <param name="rgelt">On successful return, a reference to the enumerated strings. </param>
		/// <param name="pceltFetched">On successful return, a reference to the actual number of strings enumerated in <paramref name="rgelt" />. </param>
		// Token: 0x06002BAF RID: 11183
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeConst = 0, SizeParamIndex = 0)] [Out] string[] rgelt, out int pceltFetched);

		/// <summary>Skips over a specified number of items in the enumeration sequence.</summary>
		/// <returns>S_OK if the number of elements skipped equals the <paramref name="celt" /> parameter; otherwise, S_FALSE.</returns>
		/// <param name="celt">The number of elements to skip in the enumeration. </param>
		// Token: 0x06002BB0 RID: 11184
		[PreserveSig]
		int Skip(int celt);

		/// <summary>Resets the enumeration sequence to the beginning.</summary>
		/// <returns>An HRESULT with the value S_OK.</returns>
		// Token: 0x06002BB1 RID: 11185
		[PreserveSig]
		int Reset();

		/// <summary>Creates another enumerator that contains the same enumeration state as the current one.</summary>
		/// <param name="ppenum">On successful return, a reference to the newly created enumerator. </param>
		// Token: 0x06002BB2 RID: 11186
		void Clone(out UCOMIEnumString ppenum);
	}
}
