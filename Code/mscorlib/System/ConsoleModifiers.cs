using System;

namespace System
{
	/// <summary>Represents the SHIFT, ALT, and CTRL modifier keys on a keyboard.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000116 RID: 278
	[Flags]
	[Serializable]
	public enum ConsoleModifiers
	{
		/// <summary>The left or right ALT modifier key.</summary>
		// Token: 0x04000478 RID: 1144
		Alt = 1,
		/// <summary>The left or right SHIFT modifier key.</summary>
		// Token: 0x04000479 RID: 1145
		Shift = 2,
		/// <summary>The left or right CTRL modifier key.</summary>
		// Token: 0x0400047A RID: 1146
		Control = 4
	}
}
