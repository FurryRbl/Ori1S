using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Specifies whether a calendar is solar-based, lunar-based, or lunisolar-based.</summary>
	// Token: 0x020001F8 RID: 504
	[ComVisible(true)]
	public enum CalendarAlgorithmType
	{
		/// <summary>An unknown calendar basis.</summary>
		// Token: 0x04000923 RID: 2339
		Unknown,
		/// <summary>A solar-based calendar.</summary>
		// Token: 0x04000924 RID: 2340
		SolarCalendar,
		/// <summary>A lunar-based calendar.</summary>
		// Token: 0x04000925 RID: 2341
		LunarCalendar,
		/// <summary>A lunisolar-based calendar.</summary>
		// Token: 0x04000926 RID: 2342
		LunisolarCalendar
	}
}
