using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200001A RID: 26
	public sealed class CrashReport
	{
		// Token: 0x060000AC RID: 172 RVA: 0x000025F0 File Offset: 0x000007F0
		private CrashReport(string id, DateTime time, string text)
		{
			this.id = id;
			this.time = time;
			this.text = text;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000261C File Offset: 0x0000081C
		private static int Compare(CrashReport c1, CrashReport c2)
		{
			long ticks = c1.time.Ticks;
			long ticks2 = c2.time.Ticks;
			if (ticks > ticks2)
			{
				return 1;
			}
			if (ticks < ticks2)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000265C File Offset: 0x0000085C
		private static void PopulateReports()
		{
			object obj = CrashReport.reportsLock;
			lock (obj)
			{
				if (CrashReport.internalReports == null)
				{
					string[] reports = CrashReport.GetReports();
					CrashReport.internalReports = new List<CrashReport>(reports.Length);
					foreach (string text in reports)
					{
						double value;
						string text2;
						CrashReport.GetReportData(text, out value, out text2);
						DateTime dateTime = new DateTime(1970, 1, 1);
						DateTime dateTime2 = dateTime.AddSeconds(value);
						CrashReport.internalReports.Add(new CrashReport(text, dateTime2, text2));
					}
					CrashReport.internalReports.Sort(new Comparison<CrashReport>(CrashReport.Compare));
				}
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000272C File Offset: 0x0000092C
		public static CrashReport[] reports
		{
			get
			{
				CrashReport.PopulateReports();
				object obj = CrashReport.reportsLock;
				CrashReport[] result;
				lock (obj)
				{
					result = CrashReport.internalReports.ToArray();
				}
				return result;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00002784 File Offset: 0x00000984
		public static CrashReport lastReport
		{
			get
			{
				CrashReport.PopulateReports();
				object obj = CrashReport.reportsLock;
				lock (obj)
				{
					if (CrashReport.internalReports.Count > 0)
					{
						return CrashReport.internalReports[CrashReport.internalReports.Count - 1];
					}
				}
				return null;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000027FC File Offset: 0x000009FC
		public static void RemoveAll()
		{
			foreach (CrashReport crashReport in CrashReport.reports)
			{
				crashReport.Remove();
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00002830 File Offset: 0x00000A30
		public void Remove()
		{
			if (CrashReport.RemoveReport(this.id))
			{
				object obj = CrashReport.reportsLock;
				lock (obj)
				{
					CrashReport.internalReports.Remove(this);
				}
			}
		}

		// Token: 0x060000B4 RID: 180
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetReports();

		// Token: 0x060000B5 RID: 181
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetReportData(string id, out double secondsSinceUnixEpoch, out string text);

		// Token: 0x060000B6 RID: 182
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RemoveReport(string id);

		// Token: 0x04000074 RID: 116
		private static List<CrashReport> internalReports;

		// Token: 0x04000075 RID: 117
		private static object reportsLock = new object();

		// Token: 0x04000076 RID: 118
		private readonly string id;

		// Token: 0x04000077 RID: 119
		public readonly DateTime time;

		// Token: 0x04000078 RID: 120
		public readonly string text;
	}
}
