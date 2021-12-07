using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Specifies the scheduling priority of a <see cref="T:System.Threading.Thread" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020006B2 RID: 1714
	[ComVisible(true)]
	[Serializable]
	public enum ThreadPriority
	{
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with any other priority.</summary>
		// Token: 0x04001C0B RID: 7179
		Lowest,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with Normal priority and before those with Lowest priority.</summary>
		// Token: 0x04001C0C RID: 7180
		BelowNormal,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with AboveNormal priority and before those with BelowNormal priority. Threads have Normal priority by default.</summary>
		// Token: 0x04001C0D RID: 7181
		Normal,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with Highest priority and before those with Normal priority.</summary>
		// Token: 0x04001C0E RID: 7182
		AboveNormal,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled before threads with any other priority.</summary>
		// Token: 0x04001C0F RID: 7183
		Highest
	}
}
