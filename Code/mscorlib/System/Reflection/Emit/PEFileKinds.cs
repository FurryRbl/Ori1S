using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Specifies the type of the portable executable (PE) file.</summary>
	// Token: 0x020002F6 RID: 758
	[ComVisible(true)]
	[Serializable]
	public enum PEFileKinds
	{
		/// <summary>The portable executable (PE) file is a DLL.</summary>
		// Token: 0x04000F99 RID: 3993
		Dll = 1,
		/// <summary>The application is a console (not a Windows-based) application.</summary>
		// Token: 0x04000F9A RID: 3994
		ConsoleApplication,
		/// <summary>The application is a Windows-based application.</summary>
		// Token: 0x04000F9B RID: 3995
		WindowApplication
	}
}
