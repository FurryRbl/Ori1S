using System;

namespace System.ComponentModel.Design
{
	/// <summary>Defines identifiers that indicate information about the context in which a request for Help information originated.</summary>
	// Token: 0x02000109 RID: 265
	public enum HelpContextType
	{
		/// <summary>A general context.</summary>
		// Token: 0x040002DA RID: 730
		Ambient,
		/// <summary>A window.</summary>
		// Token: 0x040002DB RID: 731
		Window,
		/// <summary>A selection.</summary>
		// Token: 0x040002DC RID: 732
		Selection,
		/// <summary>A tool window selection.</summary>
		// Token: 0x040002DD RID: 733
		ToolWindowSelection
	}
}
