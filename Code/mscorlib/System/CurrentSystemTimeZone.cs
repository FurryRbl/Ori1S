using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000183 RID: 387
	[Serializable]
	internal class CurrentSystemTimeZone : TimeZone, IDeserializationCallback
	{
		// Token: 0x06001446 RID: 5190 RVA: 0x00051C1C File Offset: 0x0004FE1C
		internal CurrentSystemTimeZone()
		{
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x00051C30 File Offset: 0x0004FE30
		internal CurrentSystemTimeZone(long lnow)
		{
			DateTime dateTime = new DateTime(lnow);
			long[] array;
			string[] array2;
			if (!CurrentSystemTimeZone.GetTimeZoneData(dateTime.Year, out array, out array2))
			{
				throw new NotSupportedException(Locale.GetText("Can't get timezone name."));
			}
			this.m_standardName = Locale.GetText(array2[0]);
			this.m_daylightName = Locale.GetText(array2[1]);
			this.m_ticksOffset = array[2];
			DaylightTime daylightTimeFromData = this.GetDaylightTimeFromData(array);
			this.m_CachedDaylightChanges.Add(dateTime.Year, daylightTimeFromData);
			this.OnDeserialization(daylightTimeFromData);
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x00051CC8 File Offset: 0x0004FEC8
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialization(null);
		}

		// Token: 0x06001449 RID: 5193
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetTimeZoneData(int year, out long[] data, out string[] names);

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x00051CD4 File Offset: 0x0004FED4
		public override string DaylightName
		{
			get
			{
				return this.m_daylightName;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x00051CDC File Offset: 0x0004FEDC
		public override string StandardName
		{
			get
			{
				return this.m_standardName;
			}
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x00051CE4 File Offset: 0x0004FEE4
		public override DaylightTime GetDaylightChanges(int year)
		{
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", year + Locale.GetText(" is not in a range between 1 and 9999."));
			}
			if (year == CurrentSystemTimeZone.this_year)
			{
				return CurrentSystemTimeZone.this_year_dlt;
			}
			Hashtable cachedDaylightChanges = this.m_CachedDaylightChanges;
			DaylightTime result;
			lock (cachedDaylightChanges)
			{
				DaylightTime daylightTime = (DaylightTime)this.m_CachedDaylightChanges[year];
				if (daylightTime == null)
				{
					long[] data;
					string[] array;
					if (!CurrentSystemTimeZone.GetTimeZoneData(year, out data, out array))
					{
						throw new ArgumentException(Locale.GetText("Can't get timezone data for " + year));
					}
					daylightTime = this.GetDaylightTimeFromData(data);
					this.m_CachedDaylightChanges.Add(year, daylightTime);
				}
				result = daylightTime;
			}
			return result;
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x00051DD4 File Offset: 0x0004FFD4
		public override TimeSpan GetUtcOffset(DateTime time)
		{
			if (this.IsDaylightSavingTime(time))
			{
				return this.utcOffsetWithDLS;
			}
			return this.utcOffsetWithOutDLS;
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x00051DF0 File Offset: 0x0004FFF0
		private void OnDeserialization(DaylightTime dlt)
		{
			if (dlt == null)
			{
				CurrentSystemTimeZone.this_year = DateTime.Now.Year;
				long[] data;
				string[] array;
				if (!CurrentSystemTimeZone.GetTimeZoneData(CurrentSystemTimeZone.this_year, out data, out array))
				{
					throw new ArgumentException(Locale.GetText("Can't get timezone data for " + CurrentSystemTimeZone.this_year));
				}
				dlt = this.GetDaylightTimeFromData(data);
			}
			else
			{
				CurrentSystemTimeZone.this_year = dlt.Start.Year;
			}
			this.utcOffsetWithOutDLS = new TimeSpan(this.m_ticksOffset);
			this.utcOffsetWithDLS = new TimeSpan(this.m_ticksOffset + dlt.Delta.Ticks);
			CurrentSystemTimeZone.this_year_dlt = dlt;
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x00051EA0 File Offset: 0x000500A0
		private DaylightTime GetDaylightTimeFromData(long[] data)
		{
			return new DaylightTime(new DateTime(data[0]), new DateTime(data[1]), new TimeSpan(data[3]));
		}

		// Token: 0x040007D7 RID: 2007
		private string m_standardName;

		// Token: 0x040007D8 RID: 2008
		private string m_daylightName;

		// Token: 0x040007D9 RID: 2009
		private Hashtable m_CachedDaylightChanges = new Hashtable(1);

		// Token: 0x040007DA RID: 2010
		private long m_ticksOffset;

		// Token: 0x040007DB RID: 2011
		[NonSerialized]
		private TimeSpan utcOffsetWithOutDLS;

		// Token: 0x040007DC RID: 2012
		[NonSerialized]
		private TimeSpan utcOffsetWithDLS;

		// Token: 0x040007DD RID: 2013
		private static int this_year;

		// Token: 0x040007DE RID: 2014
		private static DaylightTime this_year_dlt;

		// Token: 0x02000184 RID: 388
		internal enum TimeZoneData
		{
			// Token: 0x040007E0 RID: 2016
			DaylightSavingStartIdx,
			// Token: 0x040007E1 RID: 2017
			DaylightSavingEndIdx,
			// Token: 0x040007E2 RID: 2018
			UtcOffsetIdx,
			// Token: 0x040007E3 RID: 2019
			AdditionalDaylightOffsetIdx
		}

		// Token: 0x02000185 RID: 389
		internal enum TimeZoneNames
		{
			// Token: 0x040007E5 RID: 2021
			StandardNameIdx,
			// Token: 0x040007E6 RID: 2022
			DaylightNameIdx
		}
	}
}
