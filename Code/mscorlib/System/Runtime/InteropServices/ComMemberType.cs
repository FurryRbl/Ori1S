using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Describes the type of a COM member.</summary>
	// Token: 0x0200037D RID: 893
	[ComVisible(true)]
	[Serializable]
	public enum ComMemberType
	{
		/// <summary>The member is a normal method.</summary>
		// Token: 0x040010E3 RID: 4323
		Method,
		/// <summary>The member gets properties.</summary>
		// Token: 0x040010E4 RID: 4324
		PropGet,
		/// <summary>The member sets properties.</summary>
		// Token: 0x040010E5 RID: 4325
		PropSet
	}
}
