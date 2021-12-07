using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Specifies the type of Windows account used.</summary>
	// Token: 0x02000669 RID: 1641
	[ComVisible(true)]
	[Serializable]
	public enum WindowsAccountType
	{
		/// <summary>A normal user account.</summary>
		// Token: 0x04001B11 RID: 6929
		Normal,
		/// <summary>A Windows guest account.</summary>
		// Token: 0x04001B12 RID: 6930
		Guest,
		/// <summary>A Windows system account.</summary>
		// Token: 0x04001B13 RID: 6931
		System,
		/// <summary>An anonymous account.</summary>
		// Token: 0x04001B14 RID: 6932
		Anonymous
	}
}
