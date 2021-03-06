using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that the attributed method is exposed by an unmanaged dynamic-link library (DLL) as a static entry point.</summary>
	// Token: 0x02000040 RID: 64
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public sealed class DllImportAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.DllImportAttribute" /> class with the name of the DLL containing the method to import.</summary>
		/// <param name="dllName">The name of the DLL that contains the unmanaged method. This can include an assembly display name, if the DLL is included in an assembly.</param>
		// Token: 0x06000655 RID: 1621 RVA: 0x00014A50 File Offset: 0x00012C50
		public DllImportAttribute(string dllName)
		{
			this.Dll = dllName;
		}

		/// <summary>Gets the name of the DLL file that contains the entry point.</summary>
		/// <returns>The name of the DLL file that contains the entry point.</returns>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x00014A60 File Offset: 0x00012C60
		public string Value
		{
			get
			{
				return this.Dll;
			}
		}

		/// <summary>Indicates the calling convention of an entry point.</summary>
		// Token: 0x04000085 RID: 133
		public CallingConvention CallingConvention;

		/// <summary>Indicates how to marshal string parameters to the method and controls name mangling.</summary>
		// Token: 0x04000086 RID: 134
		public CharSet CharSet;

		// Token: 0x04000087 RID: 135
		private string Dll;

		/// <summary>Indicates the name or ordinal of the DLL entry point to be called.</summary>
		// Token: 0x04000088 RID: 136
		public string EntryPoint;

		/// <summary>Controls whether the <see cref="F:System.Runtime.InteropServices.DllImportAttribute.CharSet" /> field causes the common language runtime to search an unmanaged DLL for entry-point names other than the one specified.</summary>
		// Token: 0x04000089 RID: 137
		public bool ExactSpelling;

		/// <summary>Indicates whether unmanaged methods that have HRESULT or retval return values are directly translated or whether HRESULT or retval return values are automatically converted to exceptions.</summary>
		// Token: 0x0400008A RID: 138
		public bool PreserveSig;

		/// <summary>Indicates whether the callee calls the SetLastError Win32 API function before returning from the attributed method.</summary>
		// Token: 0x0400008B RID: 139
		public bool SetLastError;

		/// <summary>Enables or disables best-fit mapping behavior when converting Unicode characters to ANSI characters.</summary>
		// Token: 0x0400008C RID: 140
		public bool BestFitMapping;

		/// <summary>Enables or disables the throwing of an exception on an unmappable Unicode character that is converted to an ANSI "?" character.</summary>
		// Token: 0x0400008D RID: 141
		public bool ThrowOnUnmappableChar;
	}
}
