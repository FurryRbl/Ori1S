using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Defines how a method is implemented.</summary>
	// Token: 0x0200033F RID: 831
	[ComVisible(true)]
	[Serializable]
	public enum MethodCodeType
	{
		/// <summary>Specifies that the method implementation is in Microsoft intermediate language (MSIL).</summary>
		// Token: 0x04001090 RID: 4240
		IL,
		/// <summary>Specifies that the method is implemented in native code.</summary>
		// Token: 0x04001091 RID: 4241
		Native,
		/// <summary>Specifies that the method implementation is in optimized intermediate language (OPTIL).</summary>
		// Token: 0x04001092 RID: 4242
		OPTIL,
		/// <summary>Specifies that the method implementation is provided by the runtime.</summary>
		// Token: 0x04001093 RID: 4243
		Runtime
	}
}
