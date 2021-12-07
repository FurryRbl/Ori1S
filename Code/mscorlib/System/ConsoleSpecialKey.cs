using System;

namespace System
{
	/// <summary>Specifies combinations of modifier and console keys that can interrupt the current process.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000117 RID: 279
	[Serializable]
	public enum ConsoleSpecialKey
	{
		/// <summary>The <see cref="F:System.ConsoleModifiers.Control" /> modifier key plus the <see cref="F:System.ConsoleKey.C" /> console key.</summary>
		// Token: 0x0400047C RID: 1148
		ControlC,
		/// <summary>The <see cref="F:System.ConsoleModifiers.Control" /> modifier key plus the BREAK console key.</summary>
		// Token: 0x0400047D RID: 1149
		ControlBreak
	}
}
