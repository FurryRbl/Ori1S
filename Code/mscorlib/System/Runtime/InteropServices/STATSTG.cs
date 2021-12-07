using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.STATSTG" /> instead.</summary>
	// Token: 0x020003B5 RID: 949
	[Obsolete]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct STATSTG
	{
		/// <summary>Pointer to a null-terminated string containing the name of the object described by this structure.</summary>
		// Token: 0x04001187 RID: 4487
		public string pwcsName;

		/// <summary>Indicates the type of storage object which is one of the values from the STGTY enumeration.</summary>
		// Token: 0x04001188 RID: 4488
		public int type;

		/// <summary>Specifies the size in bytes of the stream or byte array.</summary>
		// Token: 0x04001189 RID: 4489
		public long cbSize;

		/// <summary>Indicates the last modification time for this storage, stream, or byte array.</summary>
		// Token: 0x0400118A RID: 4490
		public FILETIME mtime;

		/// <summary>Indicates the creation time for this storage, stream, or byte array.</summary>
		// Token: 0x0400118B RID: 4491
		public FILETIME ctime;

		/// <summary>Indicates the last access time for this storage, stream or byte array </summary>
		// Token: 0x0400118C RID: 4492
		public FILETIME atime;

		/// <summary>Indicates the access mode that was specified when the object was opened.</summary>
		// Token: 0x0400118D RID: 4493
		public int grfMode;

		/// <summary>Indicates the types of region locking supported by the stream or byte array.</summary>
		// Token: 0x0400118E RID: 4494
		public int grfLocksSupported;

		/// <summary>Indicates the class identifier for the storage object.</summary>
		// Token: 0x0400118F RID: 4495
		public Guid clsid;

		/// <summary>Indicates the current state bits of the storage object (the value most recently set by the IStorage::SetStateBits method).</summary>
		// Token: 0x04001190 RID: 4496
		public int grfStateBits;

		/// <summary>Reserved for future use.</summary>
		// Token: 0x04001191 RID: 4497
		public int reserved;
	}
}
