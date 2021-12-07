using System;

namespace System.Diagnostics
{
	// Token: 0x02000263 RID: 611
	internal class TraceSourceInfo
	{
		// Token: 0x060015B5 RID: 5557 RVA: 0x000396A8 File Offset: 0x000378A8
		public TraceSourceInfo(string name, SourceLevels levels)
		{
			this.name = name;
			this.levels = levels;
			this.listeners = new TraceListenerCollection();
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x000396CC File Offset: 0x000378CC
		internal TraceSourceInfo(string name, SourceLevels levels, TraceImplSettings settings)
		{
			this.name = name;
			this.levels = levels;
			this.listeners = new TraceListenerCollection(false);
			this.listeners.Add(new DefaultTraceListener(), settings);
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x00039700 File Offset: 0x00037900
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x00039708 File Offset: 0x00037908
		public SourceLevels Levels
		{
			get
			{
				return this.levels;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x00039710 File Offset: 0x00037910
		public TraceListenerCollection Listeners
		{
			get
			{
				return this.listeners;
			}
		}

		// Token: 0x040006B3 RID: 1715
		private string name;

		// Token: 0x040006B4 RID: 1716
		private SourceLevels levels;

		// Token: 0x040006B5 RID: 1717
		private TraceListenerCollection listeners;
	}
}
