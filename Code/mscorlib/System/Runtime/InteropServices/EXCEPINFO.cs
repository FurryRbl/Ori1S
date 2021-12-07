using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.EXCEPINFO" /> instead.</summary>
	// Token: 0x0200038B RID: 907
	[Obsolete]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct EXCEPINFO
	{
		/// <summary>Represents an error code identifying the error.</summary>
		// Token: 0x040010FE RID: 4350
		public short wCode;

		/// <summary>This field is reserved; must be set to 0.</summary>
		// Token: 0x040010FF RID: 4351
		public short wReserved;

		/// <summary>Indicates the name of the source of the exception. Typically, this is an application name.</summary>
		// Token: 0x04001100 RID: 4352
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrSource;

		/// <summary>Describes the error intended for the customer.</summary>
		// Token: 0x04001101 RID: 4353
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrDescription;

		/// <summary>Contains the fully-qualified drive, path, and file name of a Help file with more information about the error.</summary>
		// Token: 0x04001102 RID: 4354
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrHelpFile;

		/// <summary>Indicates the Help context ID of the topic within the Help file.</summary>
		// Token: 0x04001103 RID: 4355
		public int dwHelpContext;

		/// <summary>This field is reserved; must be set to null.</summary>
		// Token: 0x04001104 RID: 4356
		public IntPtr pvReserved;

		/// <summary>Represents a pointer to a function that takes an <see cref="T:System.Runtime.InteropServices.EXCEPINFO" /> structure as an argument and returns an HRESULT value. If deferred fill-in is not desired, this field is set to null.</summary>
		// Token: 0x04001105 RID: 4357
		public IntPtr pfnDeferredFillIn;
	}
}
