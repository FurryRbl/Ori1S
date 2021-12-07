﻿using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies whether a <see cref="T:System.DateTime" /> object represents a local time, a Coordinated Universal Time (UTC), or is not specified as either local time or UTC.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000120 RID: 288
	[ComVisible(true)]
	[Serializable]
	public enum DateTimeKind
	{
		/// <summary>The time represented is not specified as either local time or Coordinated Universal Time (UTC).</summary>
		// Token: 0x040004B3 RID: 1203
		Unspecified,
		/// <summary>The time represented is UTC.</summary>
		// Token: 0x040004B4 RID: 1204
		Utc,
		/// <summary>The time represented is local time.</summary>
		// Token: 0x040004B5 RID: 1205
		Local
	}
}
