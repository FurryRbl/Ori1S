using System;
using System.Runtime.InteropServices;

namespace UnityEngine.Cloud.Service
{
	// Token: 0x0200026A RID: 618
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class CloudServiceConfig
	{
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060024A5 RID: 9381 RVA: 0x0002FF94 File Offset: 0x0002E194
		// (set) Token: 0x060024A6 RID: 9382 RVA: 0x0002FF9C File Offset: 0x0002E19C
		public int maxNumberOfEventInGroup
		{
			get
			{
				return this.m_MaxNumberOfEventInGroup;
			}
			set
			{
				this.m_MaxNumberOfEventInGroup = value;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060024A7 RID: 9383 RVA: 0x0002FFA8 File Offset: 0x0002E1A8
		// (set) Token: 0x060024A8 RID: 9384 RVA: 0x0002FFB0 File Offset: 0x0002E1B0
		public int archivedSessionExpiryTimeInSec
		{
			get
			{
				return this.m_ArchivedSessionExpiryTimeInSec;
			}
			set
			{
				this.m_ArchivedSessionExpiryTimeInSec = value;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060024A9 RID: 9385 RVA: 0x0002FFBC File Offset: 0x0002E1BC
		// (set) Token: 0x060024AA RID: 9386 RVA: 0x0002FFC4 File Offset: 0x0002E1C4
		public int maxContinuousRequest
		{
			get
			{
				return this.m_MaxContinuousRequest;
			}
			set
			{
				this.m_MaxContinuousRequest = value;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060024AB RID: 9387 RVA: 0x0002FFD0 File Offset: 0x0002E1D0
		// (set) Token: 0x060024AC RID: 9388 RVA: 0x0002FFD8 File Offset: 0x0002E1D8
		public int maxContinuousRequestTimeoutInSec
		{
			get
			{
				return this.m_MaxContinuousRequestTimeoutInSec;
			}
			set
			{
				this.m_MaxContinuousRequestTimeoutInSec = value;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060024AD RID: 9389 RVA: 0x0002FFE4 File Offset: 0x0002E1E4
		// (set) Token: 0x060024AE RID: 9390 RVA: 0x0002FFEC File Offset: 0x0002E1EC
		public string sessionHeaderName
		{
			get
			{
				return this.m_SessionHeaderName;
			}
			set
			{
				this.m_SessionHeaderName = value;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060024AF RID: 9391 RVA: 0x0002FFF8 File Offset: 0x0002E1F8
		// (set) Token: 0x060024B0 RID: 9392 RVA: 0x00030000 File Offset: 0x0002E200
		public string eventsHeaderName
		{
			get
			{
				return this.m_EventsHeaderName;
			}
			set
			{
				this.m_EventsHeaderName = value;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x060024B1 RID: 9393 RVA: 0x0003000C File Offset: 0x0002E20C
		// (set) Token: 0x060024B2 RID: 9394 RVA: 0x00030014 File Offset: 0x0002E214
		public string eventsEndPoint
		{
			get
			{
				return this.m_EventsEndPoint;
			}
			set
			{
				this.m_EventsEndPoint = value;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x060024B3 RID: 9395 RVA: 0x00030020 File Offset: 0x0002E220
		// (set) Token: 0x060024B4 RID: 9396 RVA: 0x00030028 File Offset: 0x0002E228
		public int[] networkFailureRetryTimeoutInSec
		{
			get
			{
				return this.m_NetworkFailureRetryTimeoutInSec;
			}
			set
			{
				this.m_NetworkFailureRetryTimeoutInSec = value;
			}
		}

		// Token: 0x040009C0 RID: 2496
		private int m_MaxNumberOfEventInGroup;

		// Token: 0x040009C1 RID: 2497
		private int m_ArchivedSessionExpiryTimeInSec;

		// Token: 0x040009C2 RID: 2498
		private int m_MaxContinuousRequest;

		// Token: 0x040009C3 RID: 2499
		private int m_MaxContinuousRequestTimeoutInSec;

		// Token: 0x040009C4 RID: 2500
		private string m_SessionHeaderName;

		// Token: 0x040009C5 RID: 2501
		private string m_EventsHeaderName;

		// Token: 0x040009C6 RID: 2502
		private string m_EventsEndPoint;

		// Token: 0x040009C7 RID: 2503
		private int[] m_NetworkFailureRetryTimeoutInSec;
	}
}
