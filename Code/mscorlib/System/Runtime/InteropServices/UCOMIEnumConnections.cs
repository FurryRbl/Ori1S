using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.IEnumConnections" /> instead. </summary>
	// Token: 0x020003D2 RID: 978
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Obsolete]
	[Guid("b196b287-bab4-101a-b69c-00aa00341d07")]
	[ComImport]
	public interface UCOMIEnumConnections
	{
		/// <summary>Retrieves a specified number of items in the enumeration sequence.</summary>
		/// <returns>S_OK if the <paramref name="pceltFetched" /> parameter equals the <paramref name="celt" /> parameter; otherwise, S_FALSE.</returns>
		/// <param name="celt">The number of <see cref="T:System.Runtime.InteropServices.CONNECTDATA" /> structures to return in <paramref name="rgelt" />. </param>
		/// <param name="rgelt">On successful return, a reference to the enumerated connections. </param>
		/// <param name="pceltFetched">On successful return, a reference to the actual number of connections enumerated in <paramref name="rgelt" />. </param>
		// Token: 0x06002BA7 RID: 11175
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeConst = 0, SizeParamIndex = 0)] [Out] CONNECTDATA[] rgelt, out int pceltFetched);

		/// <summary>Skips over a specified number of items in the enumeration sequence.</summary>
		/// <returns>S_OK if the number of elements skipped equals the <paramref name="celt" /> parameter; otherwise, S_FALSE.</returns>
		/// <param name="celt">The number of elements to skip in the enumeration. </param>
		// Token: 0x06002BA8 RID: 11176
		[PreserveSig]
		int Skip(int celt);

		/// <summary>Resets the enumeration sequence to the beginning.</summary>
		// Token: 0x06002BA9 RID: 11177
		[PreserveSig]
		void Reset();

		/// <summary>Creates another enumerator that contains the same enumeration state as the current one.</summary>
		/// <param name="ppenum">On successful return, a reference to the newly created enumerator. </param>
		// Token: 0x06002BAA RID: 11178
		void Clone(out UCOMIEnumConnections ppenum);
	}
}
