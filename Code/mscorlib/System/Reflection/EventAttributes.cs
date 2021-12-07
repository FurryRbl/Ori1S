using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies the attributes of an event.</summary>
	// Token: 0x0200028B RID: 651
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum EventAttributes
	{
		/// <summary>Specifies that the event has no attributes.</summary>
		// Token: 0x04000C48 RID: 3144
		None = 0,
		/// <summary>Specifies that the event is special in a way described by the name.</summary>
		// Token: 0x04000C49 RID: 3145
		SpecialName = 512,
		/// <summary>Specifies a reserved flag for common language runtime use only.</summary>
		// Token: 0x04000C4A RID: 3146
		ReservedMask = 1024,
		/// <summary>Specifies that the common language runtime should check name encoding.</summary>
		// Token: 0x04000C4B RID: 3147
		RTSpecialName = 1024
	}
}
