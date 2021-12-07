using System;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x020002E0 RID: 736
	public class Achievement : IAchievement
	{
		// Token: 0x0600261C RID: 9756 RVA: 0x00034BC8 File Offset: 0x00032DC8
		public Achievement(string id, double percentCompleted, bool completed, bool hidden, DateTime lastReportedDate)
		{
			this.id = id;
			this.percentCompleted = percentCompleted;
			this.m_Completed = completed;
			this.m_Hidden = hidden;
			this.m_LastReportedDate = lastReportedDate;
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x00034BF8 File Offset: 0x00032DF8
		public Achievement(string id, double percent)
		{
			this.id = id;
			this.percentCompleted = percent;
			this.m_Hidden = false;
			this.m_Completed = false;
			this.m_LastReportedDate = DateTime.MinValue;
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x00034C28 File Offset: 0x00032E28
		public Achievement() : this("unknown", 0.0)
		{
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x00034C40 File Offset: 0x00032E40
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.id,
				" - ",
				this.percentCompleted,
				" - ",
				this.completed,
				" - ",
				this.hidden,
				" - ",
				this.lastReportedDate
			});
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x00034CBC File Offset: 0x00032EBC
		public void ReportProgress(Action<bool> callback)
		{
			ActivePlatform.Instance.ReportProgress(this.id, this.percentCompleted, callback);
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x00034CE0 File Offset: 0x00032EE0
		public void SetCompleted(bool value)
		{
			this.m_Completed = value;
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x00034CEC File Offset: 0x00032EEC
		public void SetHidden(bool value)
		{
			this.m_Hidden = value;
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x00034CF8 File Offset: 0x00032EF8
		public void SetLastReportedDate(DateTime date)
		{
			this.m_LastReportedDate = date;
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002624 RID: 9764 RVA: 0x00034D04 File Offset: 0x00032F04
		// (set) Token: 0x06002625 RID: 9765 RVA: 0x00034D0C File Offset: 0x00032F0C
		public string id { get; set; }

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002626 RID: 9766 RVA: 0x00034D18 File Offset: 0x00032F18
		// (set) Token: 0x06002627 RID: 9767 RVA: 0x00034D20 File Offset: 0x00032F20
		public double percentCompleted { get; set; }

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002628 RID: 9768 RVA: 0x00034D2C File Offset: 0x00032F2C
		public bool completed
		{
			get
			{
				return this.m_Completed;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06002629 RID: 9769 RVA: 0x00034D34 File Offset: 0x00032F34
		public bool hidden
		{
			get
			{
				return this.m_Hidden;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x0600262A RID: 9770 RVA: 0x00034D3C File Offset: 0x00032F3C
		public DateTime lastReportedDate
		{
			get
			{
				return this.m_LastReportedDate;
			}
		}

		// Token: 0x04000BAD RID: 2989
		private bool m_Completed;

		// Token: 0x04000BAE RID: 2990
		private bool m_Hidden;

		// Token: 0x04000BAF RID: 2991
		private DateTime m_LastReportedDate;
	}
}
