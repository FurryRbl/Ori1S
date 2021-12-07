using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	public sealed class TimeZoneInfo : ISerializable, IDeserializationCallback, IEquatable<TimeZoneInfo>
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00006810 File Offset: 0x00004A10
		private TimeZoneInfo(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id == string.Empty)
			{
				throw new ArgumentException("id parameter is an empty string");
			}
			if (baseUtcOffset.Ticks % 600000000L != 0L)
			{
				throw new ArgumentException("baseUtcOffset parameter does not represent a whole number of minutes");
			}
			if (baseUtcOffset > new TimeSpan(14, 0, 0) || baseUtcOffset < new TimeSpan(-14, 0, 0))
			{
				throw new ArgumentOutOfRangeException("baseUtcOffset parameter is greater than 14 hours or less than -14 hours");
			}
			if (adjustmentRules != null && adjustmentRules.Length != 0)
			{
				TimeZoneInfo.AdjustmentRule adjustmentRule = null;
				foreach (TimeZoneInfo.AdjustmentRule adjustmentRule2 in adjustmentRules)
				{
					if (adjustmentRule2 == null)
					{
						throw new InvalidTimeZoneException("one or more elements in adjustmentRules are null");
					}
					if (baseUtcOffset + adjustmentRule2.DaylightDelta < new TimeSpan(-14, 0, 0) || baseUtcOffset + adjustmentRule2.DaylightDelta > new TimeSpan(14, 0, 0))
					{
						throw new InvalidTimeZoneException("Sum of baseUtcOffset and DaylightDelta of one or more object in adjustmentRules array is greater than 14 or less than -14 hours;");
					}
					if (adjustmentRule != null && adjustmentRule.DateStart > adjustmentRule2.DateStart)
					{
						throw new InvalidTimeZoneException("adjustment rules specified in adjustmentRules parameter are not in chronological order");
					}
					if (adjustmentRule != null && adjustmentRule.DateEnd > adjustmentRule2.DateStart)
					{
						throw new InvalidTimeZoneException("some adjustment rules in the adjustmentRules parameter overlap");
					}
					if (adjustmentRule != null && adjustmentRule.DateEnd == adjustmentRule2.DateStart)
					{
						throw new InvalidTimeZoneException("a date can have multiple adjustment rules applied to it");
					}
					adjustmentRule = adjustmentRule2;
				}
			}
			this.id = id;
			this.baseUtcOffset = baseUtcOffset;
			this.displayName = (displayName ?? id);
			this.standardDisplayName = (standardDisplayName ?? id);
			this.daylightDisplayName = daylightDisplayName;
			this.disableDaylightSavingTime = disableDaylightSavingTime;
			this.adjustmentRules = adjustmentRules;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000069E8 File Offset: 0x00004BE8
		public TimeSpan BaseUtcOffset
		{
			get
			{
				return this.baseUtcOffset;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000069F0 File Offset: 0x00004BF0
		public string DaylightName
		{
			get
			{
				if (this.disableDaylightSavingTime)
				{
					return string.Empty;
				}
				return this.daylightDisplayName;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00006A0C File Offset: 0x00004C0C
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00006A14 File Offset: 0x00004C14
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00006A1C File Offset: 0x00004C1C
		public static TimeZoneInfo Local
		{
			get
			{
				if (TimeZoneInfo.local == null)
				{
					try
					{
						TimeZoneInfo.local = TimeZoneInfo.FindSystemTimeZoneByFileName("Local", "/etc/localtime");
					}
					catch
					{
						try
						{
							TimeZoneInfo.local = TimeZoneInfo.FindSystemTimeZoneByFileName("Local", Path.Combine(TimeZoneInfo.TimeZoneDirectory, "localtime"));
						}
						catch
						{
							throw new TimeZoneNotFoundException();
						}
					}
				}
				return TimeZoneInfo.local;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00006ABC File Offset: 0x00004CBC
		public string StandardName
		{
			get
			{
				return this.standardDisplayName;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00006AC4 File Offset: 0x00004CC4
		public bool SupportsDaylightSavingTime
		{
			get
			{
				return !this.disableDaylightSavingTime;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00006AD0 File Offset: 0x00004CD0
		public static TimeZoneInfo Utc
		{
			get
			{
				if (TimeZoneInfo.utc == null)
				{
					TimeZoneInfo.utc = TimeZoneInfo.CreateCustomTimeZone("UTC", new TimeSpan(0L), "UTC", "UTC");
				}
				return TimeZoneInfo.utc;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006B04 File Offset: 0x00004D04
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00006B20 File Offset: 0x00004D20
		private static string TimeZoneDirectory
		{
			get
			{
				if (TimeZoneInfo.timeZoneDirectory == null)
				{
					TimeZoneInfo.timeZoneDirectory = "/usr/share/zoneinfo";
				}
				return TimeZoneInfo.timeZoneDirectory;
			}
			set
			{
				TimeZoneInfo.ClearCachedData();
				TimeZoneInfo.timeZoneDirectory = value;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006B30 File Offset: 0x00004D30
		public static void ClearCachedData()
		{
			TimeZoneInfo.local = null;
			TimeZoneInfo.utc = null;
			TimeZoneInfo.systemTimeZones = null;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006B44 File Offset: 0x00004D44
		public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo destinationTimeZone)
		{
			return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local, destinationTimeZone);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006B54 File Offset: 0x00004D54
		public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
		{
			if (dateTime.Kind == DateTimeKind.Local && sourceTimeZone != TimeZoneInfo.Local)
			{
				throw new ArgumentException("Kind propery of dateTime is Local but the sourceTimeZone does not equal TimeZoneInfo.Local");
			}
			if (dateTime.Kind == DateTimeKind.Utc && sourceTimeZone != TimeZoneInfo.Utc)
			{
				throw new ArgumentException("Kind propery of dateTime is Utc but the sourceTimeZone does not equal TimeZoneInfo.Utc");
			}
			if (sourceTimeZone.IsInvalidTime(dateTime))
			{
				throw new ArgumentException("dateTime parameter is an invalid time");
			}
			if (sourceTimeZone == null)
			{
				throw new ArgumentNullException("sourceTimeZone");
			}
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			if (dateTime.Kind == DateTimeKind.Local && sourceTimeZone == TimeZoneInfo.Local && destinationTimeZone == TimeZoneInfo.Local)
			{
				return dateTime;
			}
			DateTime dateTime2 = TimeZoneInfo.ConvertTimeToUtc(dateTime);
			if (destinationTimeZone == TimeZoneInfo.Utc)
			{
				return dateTime2;
			}
			return TimeZoneInfo.ConvertTimeFromUtc(dateTime2, destinationTimeZone);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006C20 File Offset: 0x00004E20
		public static DateTimeOffset ConvertTime(DateTimeOffset dateTimeOffset, TimeZoneInfo destinationTimeZone)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006C28 File Offset: 0x00004E28
		public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string destinationTimeZoneId)
		{
			return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006C38 File Offset: 0x00004E38
		public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string sourceTimeZoneId, string destinationTimeZoneId)
		{
			return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(sourceTimeZoneId), TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006C4C File Offset: 0x00004E4C
		public static DateTimeOffset ConvertTimeBySystemTimeZoneId(DateTimeOffset dateTimeOffset, string destinationTimeZoneId)
		{
			return TimeZoneInfo.ConvertTime(dateTimeOffset, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006C5C File Offset: 0x00004E5C
		private DateTime ConvertTimeFromUtc(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Local)
			{
				throw new ArgumentException("Kind property of dateTime is Local");
			}
			if (this == TimeZoneInfo.Utc)
			{
				return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
			}
			if (this == TimeZoneInfo.Local)
			{
				return DateTime.SpecifyKind(dateTime.ToLocalTime(), DateTimeKind.Unspecified);
			}
			TimeZoneInfo.AdjustmentRule applicableRule = this.GetApplicableRule(dateTime);
			if (this.IsDaylightSavingTime(DateTime.SpecifyKind(dateTime, DateTimeKind.Utc)))
			{
				return DateTime.SpecifyKind(dateTime + this.BaseUtcOffset + applicableRule.DaylightDelta, DateTimeKind.Unspecified);
			}
			return DateTime.SpecifyKind(dateTime + this.BaseUtcOffset, DateTimeKind.Unspecified);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006CF8 File Offset: 0x00004EF8
		public static DateTime ConvertTimeFromUtc(DateTime dateTime, TimeZoneInfo destinationTimeZone)
		{
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			return destinationTimeZone.ConvertTimeFromUtc(dateTime);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006D14 File Offset: 0x00004F14
		public static DateTime ConvertTimeToUtc(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime;
			}
			return DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Utc);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006D34 File Offset: 0x00004F34
		public static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfo sourceTimeZone)
		{
			if (sourceTimeZone == null)
			{
				throw new ArgumentNullException("sourceTimeZone");
			}
			if (dateTime.Kind == DateTimeKind.Utc && sourceTimeZone != TimeZoneInfo.Utc)
			{
				throw new ArgumentException("Kind propery of dateTime is Utc but the sourceTimeZone does not equal TimeZoneInfo.Utc");
			}
			if (dateTime.Kind == DateTimeKind.Local && sourceTimeZone != TimeZoneInfo.Local)
			{
				throw new ArgumentException("Kind propery of dateTime is Local but the sourceTimeZone does not equal TimeZoneInfo.Local");
			}
			if (sourceTimeZone.IsInvalidTime(dateTime))
			{
				throw new ArgumentException("dateTime parameter is an invalid time");
			}
			if (dateTime.Kind == DateTimeKind.Utc && sourceTimeZone == TimeZoneInfo.Utc)
			{
				return dateTime;
			}
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime;
			}
			if (dateTime.Kind == DateTimeKind.Local)
			{
				return TimeZoneInfo.ConvertTimeToUtc(dateTime);
			}
			if (sourceTimeZone.IsAmbiguousTime(dateTime) || !sourceTimeZone.IsDaylightSavingTime(dateTime))
			{
				return DateTime.SpecifyKind(dateTime - sourceTimeZone.BaseUtcOffset, DateTimeKind.Utc);
			}
			TimeZoneInfo.AdjustmentRule applicableRule = sourceTimeZone.GetApplicableRule(dateTime);
			return DateTime.SpecifyKind(dateTime - sourceTimeZone.BaseUtcOffset - applicableRule.DaylightDelta, DateTimeKind.Utc);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006E3C File Offset: 0x0000503C
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName)
		{
			return TimeZoneInfo.CreateCustomTimeZone(id, baseUtcOffset, displayName, standardDisplayName, null, null, true);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006E4C File Offset: 0x0000504C
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules)
		{
			return TimeZoneInfo.CreateCustomTimeZone(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, false);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006E5C File Offset: 0x0000505C
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
		{
			return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, disableDaylightSavingTime);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006E70 File Offset: 0x00005070
		public bool Equals(TimeZoneInfo other)
		{
			return other != null && other.Id == this.Id && this.HasSameRules(other);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006EA8 File Offset: 0x000050A8
		public static TimeZoneInfo FindSystemTimeZoneById(string id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			string filepath = Path.Combine(TimeZoneInfo.TimeZoneDirectory, id);
			return TimeZoneInfo.FindSystemTimeZoneByFileName(id, filepath);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006EDC File Offset: 0x000050DC
		private static TimeZoneInfo FindSystemTimeZoneByFileName(string id, string filepath)
		{
			if (!File.Exists(filepath))
			{
				throw new TimeZoneNotFoundException();
			}
			byte[] array = new byte[16384];
			int length;
			using (FileStream fileStream = File.OpenRead(filepath))
			{
				length = fileStream.Read(array, 0, 16384);
			}
			if (!TimeZoneInfo.ValidTZFile(array, length))
			{
				throw new InvalidTimeZoneException("TZ file too big for the buffer");
			}
			TimeZoneInfo result;
			try
			{
				result = TimeZoneInfo.ParseTZBuffer(id, array, length);
			}
			catch (Exception ex)
			{
				throw new InvalidTimeZoneException(ex.Message);
			}
			return result;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006FA0 File Offset: 0x000051A0
		public static TimeZoneInfo FromSerializedString(string source)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006FA8 File Offset: 0x000051A8
		public TimeZoneInfo.AdjustmentRule[] GetAdjustmentRules()
		{
			if (this.disableDaylightSavingTime)
			{
				return new TimeZoneInfo.AdjustmentRule[0];
			}
			return (TimeZoneInfo.AdjustmentRule[])this.adjustmentRules.Clone();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006FD8 File Offset: 0x000051D8
		public TimeSpan[] GetAmbiguousTimeOffsets(DateTime dateTime)
		{
			if (!this.IsAmbiguousTime(dateTime))
			{
				throw new ArgumentException("dateTime is not an ambiguous time");
			}
			TimeZoneInfo.AdjustmentRule applicableRule = this.GetApplicableRule(dateTime);
			return new TimeSpan[]
			{
				this.baseUtcOffset,
				this.baseUtcOffset + applicableRule.DaylightDelta
			};
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000703C File Offset: 0x0000523C
		public TimeSpan[] GetAmbiguousTimeOffsets(DateTimeOffset dateTimeOffset)
		{
			if (!this.IsAmbiguousTime(dateTimeOffset))
			{
				throw new ArgumentException("dateTimeOffset is not an ambiguous time");
			}
			throw new NotImplementedException();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000705C File Offset: 0x0000525C
		public override int GetHashCode()
		{
			int num = this.Id.GetHashCode();
			foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in this.GetAdjustmentRules())
			{
				num ^= adjustmentRule.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000070A0 File Offset: 0x000052A0
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000070A8 File Offset: 0x000052A8
		public static ReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones()
		{
			if (TimeZoneInfo.systemTimeZones == null)
			{
				TimeZoneInfo.systemTimeZones = new List<TimeZoneInfo>();
				string[] array = new string[]
				{
					"Africa",
					"America",
					"Antarctica",
					"Arctic",
					"Asia",
					"Atlantic",
					"Brazil",
					"Canada",
					"Chile",
					"Europe",
					"Indian",
					"Mexico",
					"Mideast",
					"Pacific",
					"US"
				};
				foreach (string text in array)
				{
					try
					{
						foreach (string path in Directory.GetFiles(Path.Combine(TimeZoneInfo.TimeZoneDirectory, text)))
						{
							try
							{
								string text2 = string.Format("{0}/{1}", text, Path.GetFileName(path));
								TimeZoneInfo.systemTimeZones.Add(TimeZoneInfo.FindSystemTimeZoneById(text2));
							}
							catch (ArgumentNullException)
							{
							}
							catch (TimeZoneNotFoundException)
							{
							}
							catch (InvalidTimeZoneException)
							{
							}
							catch (Exception)
							{
								throw;
							}
						}
					}
					catch
					{
					}
				}
			}
			return new ReadOnlyCollection<TimeZoneInfo>(TimeZoneInfo.systemTimeZones);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00007270 File Offset: 0x00005470
		public TimeSpan GetUtcOffset(DateTime dateTime)
		{
			if (this.IsDaylightSavingTime(dateTime))
			{
				TimeZoneInfo.AdjustmentRule applicableRule = this.GetApplicableRule(dateTime);
				return this.BaseUtcOffset + applicableRule.DaylightDelta;
			}
			return this.BaseUtcOffset;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000072AC File Offset: 0x000054AC
		public TimeSpan GetUtcOffset(DateTimeOffset dateTimeOffset)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000072B4 File Offset: 0x000054B4
		public bool HasSameRules(TimeZoneInfo other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.adjustmentRules == null != (other.adjustmentRules == null))
			{
				return false;
			}
			if (this.adjustmentRules == null)
			{
				return true;
			}
			if (this.BaseUtcOffset != other.BaseUtcOffset)
			{
				return false;
			}
			if (this.adjustmentRules.Length != other.adjustmentRules.Length)
			{
				return false;
			}
			for (int i = 0; i < this.adjustmentRules.Length; i++)
			{
				if (!this.adjustmentRules[i].Equals(other.adjustmentRules[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00007360 File Offset: 0x00005560
		public bool IsAmbiguousTime(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Local && this.IsInvalidTime(dateTime))
			{
				throw new ArgumentException("Kind is Local and time is Invalid");
			}
			if (this == TimeZoneInfo.Utc)
			{
				return false;
			}
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				dateTime = this.ConvertTimeFromUtc(dateTime);
			}
			if (dateTime.Kind == DateTimeKind.Local && this != TimeZoneInfo.Local)
			{
				dateTime = TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local, this);
			}
			TimeZoneInfo.AdjustmentRule applicableRule = this.GetApplicableRule(dateTime);
			DateTime dateTime2 = TimeZoneInfo.TransitionPoint(applicableRule.DaylightTransitionEnd, dateTime.Year);
			return dateTime > dateTime2 - applicableRule.DaylightDelta && dateTime <= dateTime2;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000741C File Offset: 0x0000561C
		public bool IsAmbiguousTime(DateTimeOffset dateTimeOffset)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00007424 File Offset: 0x00005624
		public bool IsDaylightSavingTime(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Local && this.IsInvalidTime(dateTime))
			{
				throw new ArgumentException("dateTime is invalid and Kind is Local");
			}
			if (this == TimeZoneInfo.Utc)
			{
				return false;
			}
			if (!this.SupportsDaylightSavingTime)
			{
				return false;
			}
			if ((dateTime.Kind == DateTimeKind.Local || dateTime.Kind == DateTimeKind.Unspecified) && this == TimeZoneInfo.Local)
			{
				return dateTime.IsDaylightSavingTime();
			}
			if (dateTime.Kind == DateTimeKind.Local && this != TimeZoneInfo.Utc)
			{
				return this.IsDaylightSavingTime(DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Utc));
			}
			TimeZoneInfo.AdjustmentRule applicableRule = this.GetApplicableRule(dateTime.Date);
			if (applicableRule == null)
			{
				return false;
			}
			DateTime dateTime2 = TimeZoneInfo.TransitionPoint(applicableRule.DaylightTransitionStart, dateTime.Year);
			DateTime dateTime3 = TimeZoneInfo.TransitionPoint(applicableRule.DaylightTransitionEnd, dateTime.Year + ((applicableRule.DaylightTransitionStart.Month >= applicableRule.DaylightTransitionEnd.Month) ? 1 : 0));
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				dateTime2 -= this.BaseUtcOffset;
				dateTime3 -= this.BaseUtcOffset + applicableRule.DaylightDelta;
			}
			return dateTime >= dateTime2 && dateTime < dateTime3;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00007578 File Offset: 0x00005778
		public bool IsDaylightSavingTime(DateTimeOffset dateTimeOffset)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00007580 File Offset: 0x00005780
		public bool IsInvalidTime(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return false;
			}
			if (dateTime.Kind == DateTimeKind.Local && this != TimeZoneInfo.Local)
			{
				return false;
			}
			TimeZoneInfo.AdjustmentRule applicableRule = this.GetApplicableRule(dateTime);
			DateTime dateTime2 = TimeZoneInfo.TransitionPoint(applicableRule.DaylightTransitionStart, dateTime.Year);
			return dateTime >= dateTime2 && dateTime < dateTime2 + applicableRule.DaylightDelta;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000075F8 File Offset: 0x000057F8
		public void OnDeserialization(object sender)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007600 File Offset: 0x00005800
		public string ToSerializedString()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00007608 File Offset: 0x00005808
		public override string ToString()
		{
			return this.DisplayName;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00007610 File Offset: 0x00005810
		private TimeZoneInfo.AdjustmentRule GetApplicableRule(DateTime dateTime)
		{
			DateTime d = dateTime;
			if (dateTime.Kind == DateTimeKind.Local && this != TimeZoneInfo.Local)
			{
				d = d.ToUniversalTime() + this.BaseUtcOffset;
			}
			if (dateTime.Kind == DateTimeKind.Utc && this != TimeZoneInfo.Utc)
			{
				d += this.BaseUtcOffset;
			}
			foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in this.adjustmentRules)
			{
				if (adjustmentRule.DateStart > d.Date)
				{
					return null;
				}
				if (!(adjustmentRule.DateEnd < d.Date))
				{
					return adjustmentRule;
				}
			}
			return null;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000076C8 File Offset: 0x000058C8
		private static DateTime TransitionPoint(TimeZoneInfo.TransitionTime transition, int year)
		{
			if (transition.IsFixedDateRule)
			{
				return new DateTime(year, transition.Month, transition.Day) + transition.TimeOfDay.TimeOfDay;
			}
			DateTime dateTime = new DateTime(year, transition.Month, 1);
			DayOfWeek dayOfWeek = dateTime.DayOfWeek;
			int num = 1 + (transition.Week - 1) * 7 + (transition.DayOfWeek - dayOfWeek) % 7;
			if (num > DateTime.DaysInMonth(year, transition.Month))
			{
				num -= 7;
			}
			return new DateTime(year, transition.Month, num) + transition.TimeOfDay.TimeOfDay;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007778 File Offset: 0x00005978
		private static bool ValidTZFile(byte[] buffer, int length)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 4; i++)
			{
				stringBuilder.Append((char)buffer[i]);
			}
			return !(stringBuilder.ToString() != "TZif") && length < 16384;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000077D0 File Offset: 0x000059D0
		private static int SwapInt32(int i)
		{
			return (i >> 24 & 255) | (i >> 8 & 65280) | (i << 8 & 16711680) | i << 24;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000077F8 File Offset: 0x000059F8
		private static int ReadBigEndianInt32(byte[] buffer, int start)
		{
			int num = BitConverter.ToInt32(buffer, start);
			if (!BitConverter.IsLittleEndian)
			{
				return num;
			}
			return TimeZoneInfo.SwapInt32(num);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00007820 File Offset: 0x00005A20
		private static TimeZoneInfo ParseTZBuffer(string id, byte[] buffer, int length)
		{
			int num = TimeZoneInfo.ReadBigEndianInt32(buffer, 20);
			int num2 = TimeZoneInfo.ReadBigEndianInt32(buffer, 24);
			int num3 = TimeZoneInfo.ReadBigEndianInt32(buffer, 28);
			int num4 = TimeZoneInfo.ReadBigEndianInt32(buffer, 32);
			int num5 = TimeZoneInfo.ReadBigEndianInt32(buffer, 36);
			int num6 = TimeZoneInfo.ReadBigEndianInt32(buffer, 40);
			if (length < 44 + num4 * 5 + num5 * 6 + num6 + num3 * 8 + num2 + num)
			{
				throw new InvalidTimeZoneException();
			}
			Dictionary<int, string> abbreviations = TimeZoneInfo.ParseAbbreviations(buffer, 44 + 4 * num4 + num4 + 6 * num5, num6);
			Dictionary<int, TimeZoneInfo.TimeType> dictionary = TimeZoneInfo.ParseTimesTypes(buffer, 44 + 4 * num4 + num4, num5, abbreviations);
			List<KeyValuePair<DateTime, TimeZoneInfo.TimeType>> list = TimeZoneInfo.ParseTransitions(buffer, 44, num4, dictionary);
			if (dictionary.Count == 0)
			{
				throw new InvalidTimeZoneException();
			}
			if (dictionary.Count == 1 && dictionary[0].IsDst)
			{
				throw new InvalidTimeZoneException();
			}
			TimeSpan timeSpan = new TimeSpan(0L);
			TimeSpan timeSpan2 = new TimeSpan(0L);
			string text = null;
			string a = null;
			bool flag = false;
			DateTime d = DateTime.MinValue;
			List<TimeZoneInfo.AdjustmentRule> list2 = new List<TimeZoneInfo.AdjustmentRule>();
			for (int i = 0; i < list.Count; i++)
			{
				KeyValuePair<DateTime, TimeZoneInfo.TimeType> keyValuePair = list[i];
				DateTime key = keyValuePair.Key;
				TimeZoneInfo.TimeType value = keyValuePair.Value;
				if (!value.IsDst)
				{
					if (text != value.Name || timeSpan.TotalSeconds != (double)value.Offset)
					{
						text = value.Name;
						a = null;
						timeSpan = new TimeSpan(0, 0, value.Offset);
						list2 = new List<TimeZoneInfo.AdjustmentRule>();
						flag = false;
					}
					if (flag)
					{
						d += timeSpan;
						DateTime d2 = key + timeSpan + timeSpan2;
						if (d2.Date == new DateTime(d2.Year, 1, 1) && d2.Year > d.Year)
						{
							d2 -= new TimeSpan(24, 0, 0);
						}
						DateTime dateStart;
						if (d.Month < 7)
						{
							dateStart = new DateTime(d.Year, 1, 1);
						}
						else
						{
							dateStart = new DateTime(d.Year, 7, 1);
						}
						DateTime dateEnd;
						if (d2.Month >= 7)
						{
							dateEnd = new DateTime(d2.Year, 12, 31);
						}
						else
						{
							dateEnd = new DateTime(d2.Year, 6, 30);
						}
						TimeZoneInfo.TransitionTime transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1) + d.TimeOfDay, d.Month, d.Day);
						TimeZoneInfo.TransitionTime transitionTime2 = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1) + d2.TimeOfDay, d2.Month, d2.Day);
						if (transitionTime != transitionTime2)
						{
							list2.Add(TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(dateStart, dateEnd, timeSpan2, transitionTime, transitionTime2));
						}
					}
					flag = false;
				}
				else
				{
					if (a != value.Name || timeSpan2.TotalSeconds != (double)value.Offset - timeSpan.TotalSeconds)
					{
						a = value.Name;
						timeSpan2 = new TimeSpan(0, 0, value.Offset) - timeSpan;
					}
					d = key;
					flag = true;
				}
			}
			if (list2.Count == 0)
			{
				TimeZoneInfo.TimeType timeType = dictionary[0];
				if (text == null)
				{
					text = timeType.Name;
					timeSpan = new TimeSpan(0, 0, timeType.Offset);
				}
				return TimeZoneInfo.CreateCustomTimeZone(id, timeSpan, id, text);
			}
			return TimeZoneInfo.CreateCustomTimeZone(id, timeSpan, id, text, a, TimeZoneInfo.ValidateRules(list2).ToArray());
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00007BB4 File Offset: 0x00005DB4
		private static List<TimeZoneInfo.AdjustmentRule> ValidateRules(List<TimeZoneInfo.AdjustmentRule> adjustmentRules)
		{
			TimeZoneInfo.AdjustmentRule adjustmentRule = null;
			foreach (TimeZoneInfo.AdjustmentRule adjustmentRule2 in adjustmentRules.ToArray())
			{
				if (adjustmentRule != null && adjustmentRule.DateEnd > adjustmentRule2.DateStart)
				{
					adjustmentRules.Remove(adjustmentRule2);
				}
				adjustmentRule = adjustmentRule2;
			}
			return adjustmentRules;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00007C0C File Offset: 0x00005E0C
		private static Dictionary<int, string> ParseAbbreviations(byte[] buffer, int index, int count)
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < count; i++)
			{
				char c = (char)buffer[index + i];
				if (c != '\0')
				{
					stringBuilder.Append(c);
				}
				else
				{
					dictionary.Add(num, stringBuilder.ToString());
					for (int j = 1; j < stringBuilder.Length; j++)
					{
						dictionary.Add(num + j, stringBuilder.ToString(j, stringBuilder.Length - j));
					}
					num = i + 1;
					stringBuilder = new StringBuilder();
				}
			}
			return dictionary;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007CA4 File Offset: 0x00005EA4
		private static Dictionary<int, TimeZoneInfo.TimeType> ParseTimesTypes(byte[] buffer, int index, int count, Dictionary<int, string> abbreviations)
		{
			Dictionary<int, TimeZoneInfo.TimeType> dictionary = new Dictionary<int, TimeZoneInfo.TimeType>(count);
			for (int i = 0; i < count; i++)
			{
				int offset = TimeZoneInfo.ReadBigEndianInt32(buffer, index + 6 * i);
				byte b = buffer[index + 6 * i + 4];
				byte key = buffer[index + 6 * i + 5];
				dictionary.Add(i, new TimeZoneInfo.TimeType(offset, b != 0, abbreviations[(int)key]));
			}
			return dictionary;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007D08 File Offset: 0x00005F08
		private static List<KeyValuePair<DateTime, TimeZoneInfo.TimeType>> ParseTransitions(byte[] buffer, int index, int count, Dictionary<int, TimeZoneInfo.TimeType> time_types)
		{
			List<KeyValuePair<DateTime, TimeZoneInfo.TimeType>> list = new List<KeyValuePair<DateTime, TimeZoneInfo.TimeType>>(count);
			for (int i = 0; i < count; i++)
			{
				int num = TimeZoneInfo.ReadBigEndianInt32(buffer, index + 4 * i);
				DateTime key = TimeZoneInfo.DateTimeFromUnixTime((long)num);
				byte key2 = buffer[index + 4 * count + i];
				list.Add(new KeyValuePair<DateTime, TimeZoneInfo.TimeType>(key, time_types[(int)key2]));
			}
			return list;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007D64 File Offset: 0x00005F64
		private static DateTime DateTimeFromUnixTime(long unix_time)
		{
			DateTime dateTime = new DateTime(1970, 1, 1);
			return dateTime.AddSeconds((double)unix_time);
		}

		// Token: 0x04000043 RID: 67
		private const int BUFFER_SIZE = 16384;

		// Token: 0x04000044 RID: 68
		private TimeSpan baseUtcOffset;

		// Token: 0x04000045 RID: 69
		private string daylightDisplayName;

		// Token: 0x04000046 RID: 70
		private string displayName;

		// Token: 0x04000047 RID: 71
		private string id;

		// Token: 0x04000048 RID: 72
		private static TimeZoneInfo local;

		// Token: 0x04000049 RID: 73
		private string standardDisplayName;

		// Token: 0x0400004A RID: 74
		private bool disableDaylightSavingTime;

		// Token: 0x0400004B RID: 75
		private static TimeZoneInfo utc;

		// Token: 0x0400004C RID: 76
		private static string timeZoneDirectory;

		// Token: 0x0400004D RID: 77
		private TimeZoneInfo.AdjustmentRule[] adjustmentRules;

		// Token: 0x0400004E RID: 78
		private static List<TimeZoneInfo> systemTimeZones;

		// Token: 0x02000013 RID: 19
		[Serializable]
		public sealed class AdjustmentRule : ISerializable, IDeserializationCallback, IEquatable<TimeZoneInfo.AdjustmentRule>
		{
			// Token: 0x06000139 RID: 313 RVA: 0x00007D88 File Offset: 0x00005F88
			private AdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd)
			{
				if (dateStart.Kind != DateTimeKind.Unspecified || dateEnd.Kind != DateTimeKind.Unspecified)
				{
					throw new ArgumentException("the Kind property of dateStart or dateEnd parameter does not equal DateTimeKind.Unspecified");
				}
				if (daylightTransitionStart == daylightTransitionEnd)
				{
					throw new ArgumentException("daylightTransitionStart parameter cannot equal daylightTransitionEnd parameter");
				}
				if (dateStart.Ticks % 864000000000L != 0L || dateEnd.Ticks % 864000000000L != 0L)
				{
					throw new ArgumentException("dateStart or dateEnd parameter includes a time of day value");
				}
				if (dateEnd < dateStart)
				{
					throw new ArgumentOutOfRangeException("dateEnd is earlier than dateStart");
				}
				if (daylightDelta > new TimeSpan(14, 0, 0) || daylightDelta < new TimeSpan(-14, 0, 0))
				{
					throw new ArgumentOutOfRangeException("daylightDelta is less than -14 or greater than 14 hours");
				}
				if (daylightDelta.Ticks % 10000000L != 0L)
				{
					throw new ArgumentOutOfRangeException("daylightDelta parameter does not represent a whole number of seconds");
				}
				this.dateStart = dateStart;
				this.dateEnd = dateEnd;
				this.daylightDelta = daylightDelta;
				this.daylightTransitionStart = daylightTransitionStart;
				this.daylightTransitionEnd = daylightTransitionEnd;
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x0600013A RID: 314 RVA: 0x00007E9C File Offset: 0x0000609C
			public DateTime DateEnd
			{
				get
				{
					return this.dateEnd;
				}
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x0600013B RID: 315 RVA: 0x00007EA4 File Offset: 0x000060A4
			public DateTime DateStart
			{
				get
				{
					return this.dateStart;
				}
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x0600013C RID: 316 RVA: 0x00007EAC File Offset: 0x000060AC
			public TimeSpan DaylightDelta
			{
				get
				{
					return this.daylightDelta;
				}
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x0600013D RID: 317 RVA: 0x00007EB4 File Offset: 0x000060B4
			public TimeZoneInfo.TransitionTime DaylightTransitionEnd
			{
				get
				{
					return this.daylightTransitionEnd;
				}
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x0600013E RID: 318 RVA: 0x00007EBC File Offset: 0x000060BC
			public TimeZoneInfo.TransitionTime DaylightTransitionStart
			{
				get
				{
					return this.daylightTransitionStart;
				}
			}

			// Token: 0x0600013F RID: 319 RVA: 0x00007EC4 File Offset: 0x000060C4
			public static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd)
			{
				return new TimeZoneInfo.AdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd);
			}

			// Token: 0x06000140 RID: 320 RVA: 0x00007ED4 File Offset: 0x000060D4
			public bool Equals(TimeZoneInfo.AdjustmentRule other)
			{
				return this.dateStart == other.dateStart && this.dateEnd == other.dateEnd && this.daylightDelta == other.daylightDelta && this.daylightTransitionStart == other.daylightTransitionStart && this.daylightTransitionEnd == other.daylightTransitionEnd;
			}

			// Token: 0x06000141 RID: 321 RVA: 0x00007F50 File Offset: 0x00006150
			public override int GetHashCode()
			{
				return this.dateStart.GetHashCode() ^ this.dateEnd.GetHashCode() ^ this.daylightDelta.GetHashCode() ^ this.daylightTransitionStart.GetHashCode() ^ this.daylightTransitionEnd.GetHashCode();
			}

			// Token: 0x06000142 RID: 322 RVA: 0x00007F98 File Offset: 0x00006198
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000143 RID: 323 RVA: 0x00007FA0 File Offset: 0x000061A0
			public void OnDeserialization(object sender)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0400004F RID: 79
			private DateTime dateEnd;

			// Token: 0x04000050 RID: 80
			private DateTime dateStart;

			// Token: 0x04000051 RID: 81
			private TimeSpan daylightDelta;

			// Token: 0x04000052 RID: 82
			private TimeZoneInfo.TransitionTime daylightTransitionEnd;

			// Token: 0x04000053 RID: 83
			private TimeZoneInfo.TransitionTime daylightTransitionStart;
		}

		// Token: 0x02000014 RID: 20
		private struct TimeType
		{
			// Token: 0x06000144 RID: 324 RVA: 0x00007FA8 File Offset: 0x000061A8
			public TimeType(int offset, bool is_dst, string abbrev)
			{
				this.Offset = offset;
				this.IsDst = is_dst;
				this.Name = abbrev;
			}

			// Token: 0x06000145 RID: 325 RVA: 0x00007FC0 File Offset: 0x000061C0
			public override string ToString()
			{
				return string.Concat(new object[]
				{
					"offset: ",
					this.Offset,
					"s, is_dst: ",
					this.IsDst,
					", zone name: ",
					this.Name
				});
			}

			// Token: 0x04000054 RID: 84
			public readonly int Offset;

			// Token: 0x04000055 RID: 85
			public readonly bool IsDst;

			// Token: 0x04000056 RID: 86
			public string Name;
		}

		// Token: 0x02000015 RID: 21
		[Serializable]
		public struct TransitionTime : ISerializable, IDeserializationCallback, IEquatable<TimeZoneInfo.TransitionTime>
		{
			// Token: 0x06000146 RID: 326 RVA: 0x00008018 File Offset: 0x00006218
			private TransitionTime(DateTime timeOfDay, int month, int day)
			{
				this = new TimeZoneInfo.TransitionTime(timeOfDay, month);
				if (day < 1 || day > 31)
				{
					throw new ArgumentOutOfRangeException("day parameter is less than 1 or greater than 31");
				}
				this.day = day;
				this.isFixedDateRule = true;
			}

			// Token: 0x06000147 RID: 327 RVA: 0x00008058 File Offset: 0x00006258
			private TransitionTime(DateTime timeOfDay, int month, int week, DayOfWeek dayOfWeek)
			{
				this = new TimeZoneInfo.TransitionTime(timeOfDay, month);
				if (week < 1 || week > 5)
				{
					throw new ArgumentOutOfRangeException("week parameter is less than 1 or greater than 5");
				}
				if (dayOfWeek != DayOfWeek.Sunday && dayOfWeek != DayOfWeek.Monday && dayOfWeek != DayOfWeek.Tuesday && dayOfWeek != DayOfWeek.Wednesday && dayOfWeek != DayOfWeek.Thursday && dayOfWeek != DayOfWeek.Friday && dayOfWeek != DayOfWeek.Saturday)
				{
					throw new ArgumentOutOfRangeException("dayOfWeek parameter is not a member od DayOfWeek enumeration");
				}
				this.week = week;
				this.dayOfWeek = dayOfWeek;
				this.isFixedDateRule = false;
			}

			// Token: 0x06000148 RID: 328 RVA: 0x000080E0 File Offset: 0x000062E0
			private TransitionTime(DateTime timeOfDay, int month)
			{
				if (timeOfDay.Year != 1 || timeOfDay.Month != 1 || timeOfDay.Day != 1)
				{
					throw new ArgumentException("timeOfDay parameter has a non-default date component");
				}
				if (timeOfDay.Kind != DateTimeKind.Unspecified)
				{
					throw new ArgumentException("timeOfDay parameter Kind's property is not DateTimeKind.Unspecified");
				}
				if (timeOfDay.Ticks % 10000L != 0L)
				{
					throw new ArgumentException("timeOfDay parameter does not represent a whole number of milliseconds");
				}
				if (month < 1 || month > 12)
				{
					throw new ArgumentOutOfRangeException("month parameter is less than 1 or greater than 12");
				}
				this.timeOfDay = timeOfDay;
				this.month = month;
				this.week = -1;
				this.dayOfWeek = (DayOfWeek)(-1);
				this.day = -1;
				this.isFixedDateRule = false;
			}

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x06000149 RID: 329 RVA: 0x00008198 File Offset: 0x00006398
			public DateTime TimeOfDay
			{
				get
				{
					return this.timeOfDay;
				}
			}

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x0600014A RID: 330 RVA: 0x000081A0 File Offset: 0x000063A0
			public int Month
			{
				get
				{
					return this.month;
				}
			}

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x0600014B RID: 331 RVA: 0x000081A8 File Offset: 0x000063A8
			public int Day
			{
				get
				{
					return this.day;
				}
			}

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x0600014C RID: 332 RVA: 0x000081B0 File Offset: 0x000063B0
			public int Week
			{
				get
				{
					return this.week;
				}
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x0600014D RID: 333 RVA: 0x000081B8 File Offset: 0x000063B8
			public DayOfWeek DayOfWeek
			{
				get
				{
					return this.dayOfWeek;
				}
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x0600014E RID: 334 RVA: 0x000081C0 File Offset: 0x000063C0
			public bool IsFixedDateRule
			{
				get
				{
					return this.isFixedDateRule;
				}
			}

			// Token: 0x0600014F RID: 335 RVA: 0x000081C8 File Offset: 0x000063C8
			public static TimeZoneInfo.TransitionTime CreateFixedDateRule(DateTime timeOfDay, int month, int day)
			{
				return new TimeZoneInfo.TransitionTime(timeOfDay, month, day);
			}

			// Token: 0x06000150 RID: 336 RVA: 0x000081D4 File Offset: 0x000063D4
			public static TimeZoneInfo.TransitionTime CreateFloatingDateRule(DateTime timeOfDay, int month, int week, DayOfWeek dayOfWeek)
			{
				return new TimeZoneInfo.TransitionTime(timeOfDay, month, week, dayOfWeek);
			}

			// Token: 0x06000151 RID: 337 RVA: 0x000081E0 File Offset: 0x000063E0
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000152 RID: 338 RVA: 0x000081E8 File Offset: 0x000063E8
			public override bool Equals(object other)
			{
				return other is TimeZoneInfo.TransitionTime && this == (TimeZoneInfo.TransitionTime)other;
			}

			// Token: 0x06000153 RID: 339 RVA: 0x00008208 File Offset: 0x00006408
			public bool Equals(TimeZoneInfo.TransitionTime other)
			{
				return this == other;
			}

			// Token: 0x06000154 RID: 340 RVA: 0x00008218 File Offset: 0x00006418
			public override int GetHashCode()
			{
				return this.day ^ (int)this.dayOfWeek ^ this.month ^ (int)this.timeOfDay.Ticks ^ this.week;
			}

			// Token: 0x06000155 RID: 341 RVA: 0x00008250 File Offset: 0x00006450
			public void OnDeserialization(object sender)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000156 RID: 342 RVA: 0x00008258 File Offset: 0x00006458
			public static bool operator ==(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
			{
				return t1.day == t2.day && t1.dayOfWeek == t2.dayOfWeek && t1.isFixedDateRule == t2.isFixedDateRule && t1.month == t2.month && t1.timeOfDay == t2.timeOfDay && t1.week == t2.week;
			}

			// Token: 0x06000157 RID: 343 RVA: 0x000082DC File Offset: 0x000064DC
			public static bool operator !=(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
			{
				return !(t1 == t2);
			}

			// Token: 0x04000057 RID: 87
			private DateTime timeOfDay;

			// Token: 0x04000058 RID: 88
			private int month;

			// Token: 0x04000059 RID: 89
			private int day;

			// Token: 0x0400005A RID: 90
			private int week;

			// Token: 0x0400005B RID: 91
			private DayOfWeek dayOfWeek;

			// Token: 0x0400005C RID: 92
			private bool isFixedDateRule;
		}
	}
}
