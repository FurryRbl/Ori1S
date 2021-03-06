using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Manages the definition of the IEnumString interface.</summary>
	// Token: 0x020003FA RID: 1018
	[Guid("00000101-0000-0000-c000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IEnumString
	{
		/// <summary>Retrieves a specified number of items in the enumeration sequence.</summary>
		/// <returns>S_OK if the <paramref name="pceltFetched" /> parameter equals the <paramref name="celt" /> parameter; otherwise, S_FALSE.</returns>
		/// <param name="celt">The number of strings to return in <paramref name="rgelt" />. </param>
		/// <param name="rgelt">When this method returns, contains a reference to the enumerated strings. This parameter is passed uninitialized.</param>
		/// <param name="pceltFetched">When this method returns, contains a reference to the actual number of strings enumerated in <paramref name="rgelt" />. </param>
		// Token: 0x06002C25 RID: 11301
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeConst = 0, SizeParamIndex = 0)] [Out] string[] rgelt, IntPtr pceltFetched);

		/// <summary>Skips a specified number of items in the enumeration sequence.</summary>
		/// <returns>S_OK if the number of elements skipped equals the <paramref name="celt" /> parameter; otherwise, S_FALSE.</returns>
		/// <param name="celt">The number of elements to skip in the enumeration. </param>
		// Token: 0x06002C26 RID: 11302
		[PreserveSig]
		int Skip(int celt);

		/// <summary>Resets the enumeration sequence to the beginning.</summary>
		// Token: 0x06002C27 RID: 11303
		void Reset();

		/// <summary>Creates a new enumerator that contains the same enumeration state as the current one.</summary>
		/// <param name="ppenum">When this method returns, contains a reference to the newly created enumerator. This parameter is passed uninitialized.</param>
		// Token: 0x06002C28 RID: 11304
		void Clone(out IEnumString ppenum);
	}
}
