using System;
using System.Collections;
using System.IO;

namespace System.Globalization
{
	// Token: 0x02000205 RID: 517
	[Serializable]
	internal class CCGregorianEraHandler
	{
		// Token: 0x0600199E RID: 6558 RVA: 0x0005F5EC File Offset: 0x0005D7EC
		public CCGregorianEraHandler()
		{
			this._Eras = new SortedList();
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x0600199F RID: 6559 RVA: 0x0005F600 File Offset: 0x0005D800
		public int[] Eras
		{
			get
			{
				int[] array = new int[this._Eras.Count];
				for (int i = 0; i < this._Eras.Count; i++)
				{
					array[i] = ((CCGregorianEraHandler.Era)this._Eras.GetByIndex(i)).Nr;
				}
				return array;
			}
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x0005F658 File Offset: 0x0005D858
		public void appendEra(int nr, int rd_start, int rd_end)
		{
			CCGregorianEraHandler.Era era = new CCGregorianEraHandler.Era(nr, rd_start, rd_end);
			this._Eras[nr] = era;
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x0005F688 File Offset: 0x0005D888
		public void appendEra(int nr, int rd_start)
		{
			this.appendEra(nr, rd_start, CCFixed.FromDateTime(DateTime.MaxValue));
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x0005F69C File Offset: 0x0005D89C
		public int GregorianYear(int year, int era)
		{
			return ((CCGregorianEraHandler.Era)this._Eras[era]).GregorianYear(year);
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0005F6C8 File Offset: 0x0005D8C8
		public int EraYear(out int era, int date)
		{
			IList valueList = this._Eras.GetValueList();
			foreach (object obj in valueList)
			{
				CCGregorianEraHandler.Era era2 = (CCGregorianEraHandler.Era)obj;
				if (era2.Covers(date))
				{
					return era2.EraYear(out era, date);
				}
			}
			throw new ArgumentOutOfRangeException("date", "Time value was out of era range.");
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0005F768 File Offset: 0x0005D968
		public void CheckDateTime(DateTime time)
		{
			int date = CCFixed.FromDateTime(time);
			if (!this.ValidDate(date))
			{
				throw new ArgumentOutOfRangeException("time", "Time value was out of era range.");
			}
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0005F798 File Offset: 0x0005D998
		public bool ValidDate(int date)
		{
			IList valueList = this._Eras.GetValueList();
			foreach (object obj in valueList)
			{
				if (((CCGregorianEraHandler.Era)obj).Covers(date))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0005F824 File Offset: 0x0005DA24
		public bool ValidEra(int era)
		{
			return this._Eras.Contains(era);
		}

		// Token: 0x04000966 RID: 2406
		private SortedList _Eras;

		// Token: 0x02000206 RID: 518
		[Serializable]
		private struct Era
		{
			// Token: 0x060019A7 RID: 6567 RVA: 0x0005F838 File Offset: 0x0005DA38
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

			// Token: 0x17000412 RID: 1042
			// (get) Token: 0x060019A8 RID: 6568 RVA: 0x0005F8AC File Offset: 0x0005DAAC
			public int Nr
			{
				get
				{
					return this._nr;
				}
			}

			// Token: 0x060019A9 RID: 6569 RVA: 0x0005F8B4 File Offset: 0x0005DAB4
			public int GregorianYear(int year)
			{
				if (year < 1 || year > this._maxYear)
				{
					StringWriter stringWriter = new StringWriter();
					stringWriter.Write("Valid Values are between {0} and {1}, inclusive.", 1, this._maxYear);
					throw new ArgumentOutOfRangeException("year", stringWriter.ToString());
				}
				return year + this._gregorianYearStart - 1;
			}

			// Token: 0x060019AA RID: 6570 RVA: 0x0005F914 File Offset: 0x0005DB14
			public bool Covers(int date)
			{
				return this._start <= date && date <= this._end;
			}

			// Token: 0x060019AB RID: 6571 RVA: 0x0005F934 File Offset: 0x0005DB34
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

			// Token: 0x04000967 RID: 2407
			private int _nr;

			// Token: 0x04000968 RID: 2408
			private int _start;

			// Token: 0x04000969 RID: 2409
			private int _gregorianYearStart;

			// Token: 0x0400096A RID: 2410
			private int _end;

			// Token: 0x0400096B RID: 2411
			private int _maxYear;
		}
	}
}
