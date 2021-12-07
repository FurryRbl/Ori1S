using System;
using System.Collections;

namespace System.Globalization
{
	// Token: 0x02000207 RID: 519
	[Serializable]
	internal class CCEastAsianLunisolarEraHandler
	{
		// Token: 0x060019AC RID: 6572 RVA: 0x0005F978 File Offset: 0x0005DB78
		public CCEastAsianLunisolarEraHandler()
		{
			this._Eras = new SortedList();
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x0005F98C File Offset: 0x0005DB8C
		public int[] Eras
		{
			get
			{
				int[] array = new int[this._Eras.Count];
				for (int i = 0; i < this._Eras.Count; i++)
				{
					array[i] = ((CCEastAsianLunisolarEraHandler.Era)this._Eras.GetByIndex(i)).Nr;
				}
				return array;
			}
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x0005F9E4 File Offset: 0x0005DBE4
		public void appendEra(int nr, int rd_start, int rd_end)
		{
			CCEastAsianLunisolarEraHandler.Era era = new CCEastAsianLunisolarEraHandler.Era(nr, rd_start, rd_end);
			this._Eras[nr] = era;
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x0005FA14 File Offset: 0x0005DC14
		public void appendEra(int nr, int rd_start)
		{
			this.appendEra(nr, rd_start, CCFixed.FromDateTime(DateTime.MaxValue));
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x0005FA28 File Offset: 0x0005DC28
		public int GregorianYear(int year, int era)
		{
			return ((CCEastAsianLunisolarEraHandler.Era)this._Eras[era]).GregorianYear(year);
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0005FA54 File Offset: 0x0005DC54
		public int EraYear(out int era, int date)
		{
			foreach (object obj in this._Eras.Values)
			{
				CCEastAsianLunisolarEraHandler.Era era2 = (CCEastAsianLunisolarEraHandler.Era)obj;
				if (era2.Covers(date))
				{
					return era2.EraYear(out era, date);
				}
			}
			throw new ArgumentOutOfRangeException("date", "Time value was out of era range.");
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x0005FAF0 File Offset: 0x0005DCF0
		public void CheckDateTime(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			if (!this.ValidDate(date))
			{
				throw new ArgumentOutOfRangeException("time", "Time value was out of era range.");
			}
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x0005FB20 File Offset: 0x0005DD20
		public bool ValidDate(int date)
		{
			foreach (object obj in this._Eras.Values)
			{
				if (((CCEastAsianLunisolarEraHandler.Era)obj).Covers(date))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x0005FBA4 File Offset: 0x0005DDA4
		public bool ValidEra(int era)
		{
			return this._Eras.Contains(era);
		}

		// Token: 0x0400096C RID: 2412
		private SortedList _Eras;

		// Token: 0x02000208 RID: 520
		[Serializable]
		private struct Era
		{
			// Token: 0x060019B5 RID: 6581 RVA: 0x0005FBB8 File Offset: 0x0005DDB8
			public Era(int nr, int start, int end)
			{
				if (nr == 0)
				{
					throw new ArgumentException("Era number shouldn't be zero.");
				}
				this._nr = nr;
				if (start > end)
				{
					throw new ArgumentException("Era should start before end.");
				}
				this._start = start;
				this._end = end;
				this._gregorianYearStart = CCGregorianCalendar.year_from_fixed(this._start);
				int num = CCGregorianCalendar.year_from_fixed(this._end);
				this._maxYear = num - this._gregorianYearStart + 1;
			}

			// Token: 0x17000414 RID: 1044
			// (get) Token: 0x060019B6 RID: 6582 RVA: 0x0005FC2C File Offset: 0x0005DE2C
			public int Nr
			{
				get
				{
					return this._nr;
				}
			}

			// Token: 0x060019B7 RID: 6583 RVA: 0x0005FC34 File Offset: 0x0005DE34
			public int GregorianYear(int year)
			{
				if (year < 1 || year > this._maxYear)
				{
					throw new ArgumentOutOfRangeException("year", string.Format("Valid Values are between {0} and {1}, inclusive.", 1, this._maxYear));
				}
				return year + this._gregorianYearStart - 1;
			}

			// Token: 0x060019B8 RID: 6584 RVA: 0x0005FC84 File Offset: 0x0005DE84
			public bool Covers(int date)
			{
				return this._start <= date && date <= this._end;
			}

			// Token: 0x060019B9 RID: 6585 RVA: 0x0005FCA4 File Offset: 0x0005DEA4
			public int EraYear(out int era, int date)
			{
				if (!this.Covers(date))
				{
					throw new ArgumentOutOfRangeException("date", "Time was out of Era range.");
				}
				int num = CCGregorianCalendar.year_from_fixed(date);
				era = this._nr;
				return num - this._gregorianYearStart + 1;
			}

			// Token: 0x0400096D RID: 2413
			private int _nr;

			// Token: 0x0400096E RID: 2414
			private int _start;

			// Token: 0x0400096F RID: 2415
			private int _gregorianYearStart;

			// Token: 0x04000970 RID: 2416
			private int _end;

			// Token: 0x04000971 RID: 2417
			private int _maxYear;
		}
	}
}
