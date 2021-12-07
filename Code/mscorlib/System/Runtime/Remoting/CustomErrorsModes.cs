using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Specifies how custom errors are handled.</summary>
	// Token: 0x02000418 RID: 1048
	[ComVisible(true)]
	public enum CustomErrorsModes
	{
		/// <summary>All callers receive filtered exception information.</summary>
		// Token: 0x04001358 RID: 4952
		On,
		/// <summary>All callers receive complete exception information.</summary>
		// Token: 0x04001359 RID: 4953
		Off,
		/// <summary>Local callers receive complete exception information; remote callers receive filtered exception information.</summary>
		// Token: 0x0400135A RID: 4954
		RemoteOnly
	}
}
