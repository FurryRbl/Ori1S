using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Defines different rules for determining the first week of the year.</summary>
	// Token: 0x020001F9 RID: 505
	[ComVisible(true)]
	[Serializable]
	public enum CalendarWeekRule
	{
		/// <summary>Indicates that the first week of the year starts on the first day of the year and ends before the following designated first day of the week. The value is 0.</summary>
		// Token: 0x04000928 RID: 2344
		FirstDay,
		/// <summary>Indicates that the first week of the year begins on the first occurrence of the designated first day of the week on or after the first day of the year. The value is 1.</summary>
		// Token: 0x04000929 RID: 2345
		FirstFullWeek,
		/// <summary>Indicates that the first week of the year is the first week with four or more days before the designated first day of the week. The value is 2.</summary>
		// Token: 0x0400092A RID: 2346
		FirstFourDayWeek
	}
}
