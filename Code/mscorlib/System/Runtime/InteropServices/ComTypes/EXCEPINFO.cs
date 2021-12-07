using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Describes the exceptions that occur during IDispatch::Invoke.</summary>
	// Token: 0x020003ED RID: 1005
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct EXCEPINFO
	{
		/// <summary>Represents an error code identifying the error.</summary>
		// Token: 0x040012A7 RID: 4775
		public short wCode;

		/// <summary>This field is reserved; it must be set to 0.</summary>
		// Token: 0x040012A8 RID: 4776
		public short wReserved;

		/// <summary>Indicates the name of the source of the exception. Typically, this is an application name.</summary>
		// Token: 0x040012A9 RID: 4777
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrSource;

		/// <summary>Describes the error intended for the customer.</summary>
		// Token: 0x040012AA RID: 4778
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrDescription;

		/// <summary>Contains the fully-qualified drive, path, and file name of a Help file that contains more information about the error.</summary>
		// Token: 0x040012AB RID: 4779
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrHelpFile;

		/// <summary>Indicates the Help context ID of the topic within the Help file.</summary>
		// Token: 0x040012AC RID: 4780
		public int dwHelpContext;

		/// <summary>This field is reserved; it must be set to null.</summary>
		// Token: 0x040012AD RID: 4781
		public IntPtr pvReserved;

		/// <summary>Represents a pointer to a function that takes an <see cref="T:System.Runtime.InteropServices.EXCEPINFO" /> structure as an argument and returns an HRESULT value. If deferred fill-in is not desired, this field is set to null.</summary>
		// Token: 0x040012AE RID: 4782
		public IntPtr pfnDeferredFillIn;

		/// <summary>A return value describing the error.</summary>
		// Token: 0x040012AF RID: 4783
		public int scode;
	}
}
