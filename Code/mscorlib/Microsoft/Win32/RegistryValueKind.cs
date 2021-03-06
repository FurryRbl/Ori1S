using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	/// <summary>Specifies the data types to use when storing values in the registry, or identifies the data type of a value in the registry.</summary>
	// Token: 0x0200006B RID: 107
	[ComVisible(true)]
	public enum RegistryValueKind
	{
		/// <summary>Indicates an unsupported registry data type. For example, the Microsoft Win32 API registry data type REG_RESOURCE_LIST is unsupported. Use this value to specify that the <see cref="M:Microsoft.Win32.RegistryKey.SetValue(System.String,System.Object)" /> method should determine the appropriate registry data type when storing a name/value pair.</summary>
		// Token: 0x040000F6 RID: 246
		Unknown,
		/// <summary>Specifies a null-terminated string. This value is equivalent to the Win32 API registry data type REG_SZ.</summary>
		// Token: 0x040000F7 RID: 247
		String,
		/// <summary>Specifies a null-terminated string that contains unexpanded references to environment variables, such as %PATH%, that are expanded when the value is retrieved. This value is equivalent to the Win32 API registry data type REG_EXPAND_SZ.</summary>
		// Token: 0x040000F8 RID: 248
		ExpandString,
		/// <summary>Specifies binary data in any form. This value is equivalent to the Win32 API registry data type REG_BINARY.</summary>
		// Token: 0x040000F9 RID: 249
		Binary,
		/// <summary>Specifies a 32-bit binary number. This value is equivalent to the Win32 API registry data type REG_DWORD.</summary>
		// Token: 0x040000FA RID: 250
		DWord,
		/// <summary>Specifies an array of null-terminated strings, terminated by two null characters. This value is equivalent to the Win32 API registry data type REG_MULTI_SZ.</summary>
		// Token: 0x040000FB RID: 251
		MultiString = 7,
		/// <summary>Specifies a 64-bit binary number. This value is equivalent to the Win32 API registry data type REG_QWORD.</summary>
		// Token: 0x040000FC RID: 252
		QWord = 11
	}
}
