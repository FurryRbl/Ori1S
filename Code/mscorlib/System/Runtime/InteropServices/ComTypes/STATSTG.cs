using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains statistical information about an open storage, stream, or byte-array object.</summary>
	// Token: 0x0200040A RID: 1034
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct STATSTG
	{
		/// <summary>Represents a pointer to a null-terminated string containing the name of the object described by this structure.</summary>
		// Token: 0x040012F4 RID: 4852
		public string pwcsName;

		/// <summary>Indicates the type of storage object, which is one of the values from the STGTY enumeration.</summary>
		// Token: 0x040012F5 RID: 4853
		public int type;

		/// <summary>Specifies the size, in bytes, of the stream or byte array.</summary>
		// Token: 0x040012F6 RID: 4854
		public long cbSize;

		/// <summary>Indicates the last modification time for this storage, stream, or byte array.</summary>
		// Token: 0x040012F7 RID: 4855
		public FILETIME mtime;

		/// <summary>Indicates the creation time for this storage, stream, or byte array.</summary>
		// Token: 0x040012F8 RID: 4856
		public FILETIME ctime;

		/// <summary>Specifies the last access time for this storage, stream, or byte array. </summary>
		// Token: 0x040012F9 RID: 4857
		public FILETIME atime;

		/// <summary>Indicates the access mode that was specified when the object was opened.</summary>
		// Token: 0x040012FA RID: 4858
		public int grfMode;

		/// <summary>Indicates the types of region locking supported by the stream or byte array.</summary>
		// Token: 0x040012FB RID: 4859
		public int grfLocksSupported;

		/// <summary>Indicates the class identifier for the storage object.</summary>
		// Token: 0x040012FC RID: 4860
		public Guid clsid;

		/// <summary>Indicates the current state bits of the storage object (the value most recently set by the IStorage::SetStateBits method).</summary>
		// Token: 0x040012FD RID: 4861
		public int grfStateBits;

		/// <summary>Reserved for future use.</summary>
		// Token: 0x040012FE RID: 4862
		public int reserved;
	}
}
