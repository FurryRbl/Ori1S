using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>
	///   <see cref="T:System.EventArgs" /> is the base class for classes containing event data. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000135 RID: 309
	[ComVisible(true)]
	[Serializable]
	public class EventArgs
	{
		/// <summary>Represents an event with no event data.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040004FD RID: 1277
		public static readonly EventArgs Empty = new EventArgs();
	}
}
