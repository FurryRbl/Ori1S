﻿using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.IPersistFile" /> instead.</summary>
	// Token: 0x020003D7 RID: 983
	[Obsolete]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0000010b-0000-0000-c000-000000000046")]
	[ComImport]
	public interface UCOMIPersistFile
	{
		/// <summary>Retrieves the class identifier (CLSID) of an object.</summary>
		/// <param name="pClassID">On successful return, a reference to the CLSID. </param>
		// Token: 0x06002BCB RID: 11211
		void GetClassID(out Guid pClassID);

		/// <summary>Checks an object for changes since it was last saved to its current file.</summary>
		/// <returns>S_OK if the file has changed since it was last saved; S_FALSE if the file has not changed since it was last saved.</returns>
		// Token: 0x06002BCC RID: 11212
		[PreserveSig]
		int IsDirty();

		/// <summary>Opens the specified file and initializes an object from the file contents.</summary>
		/// <param name="pszFileName">A zero-terminated string containing the absolute path of the file to open. </param>
		/// <param name="dwMode">A combination of values from the STGM enumeration to indicate the access mode in which to open <paramref name="pszFileName" />. </param>
		// Token: 0x06002BCD RID: 11213
		void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

		/// <summary>Saves a copy of the object into the specified file.</summary>
		/// <param name="pszFileName">A zero-terminated string containing the absolute path of the file to which the object is saved. </param>
		/// <param name="fRemember">Indicates whether <paramref name="pszFileName" /> is to be used as the current working file. </param>
		// Token: 0x06002BCE RID: 11214
		void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);

		/// <summary>Notifies the object that it can write to its file.</summary>
		/// <param name="pszFileName">The absolute path of the file where the object was previously saved. </param>
		// Token: 0x06002BCF RID: 11215
		void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

		/// <summary>Retrieves either the absolute path to current working file of the object, or if there is no current working file, the default filename prompt of the object.</summary>
		/// <param name="ppszFileName">The address of a pointer to a zero-terminated string containing the path for the current file, or the default filename prompt (such as *.txt). </param>
		// Token: 0x06002BD0 RID: 11216
		void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
	}
}
