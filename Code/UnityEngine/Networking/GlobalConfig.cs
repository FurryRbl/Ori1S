using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000252 RID: 594
	[Serializable]
	public class GlobalConfig
	{
		// Token: 0x060023A9 RID: 9129 RVA: 0x0002D67C File Offset: 0x0002B87C
		public GlobalConfig()
		{
			this.m_ThreadAwakeTimeout = 1U;
			this.m_ReactorModel = ReactorModel.SelectReactor;
			this.m_ReactorMaximumReceivedMessages = 1024;
			this.m_ReactorMaximumSentMessages = 1024;
			this.m_MaxPacketSize = 2000;
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x0002D6B4 File Offset: 0x0002B8B4
		// (set) Token: 0x060023AB RID: 9131 RVA: 0x0002D6BC File Offset: 0x0002B8BC
		public uint ThreadAwakeTimeout
		{
			get
			{
				return this.m_ThreadAwakeTimeout;
			}
			set
			{
				if (value == 0U)
				{
					throw new ArgumentOutOfRangeException("Minimal thread awake timeout should be > 0");
				}
				this.m_ThreadAwakeTimeout = value;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x0002D6D8 File Offset: 0x0002B8D8
		// (set) Token: 0x060023AD RID: 9133 RVA: 0x0002D6E0 File Offset: 0x0002B8E0
		public ReactorModel ReactorModel
		{
			get
			{
				return this.m_ReactorModel;
			}
			set
			{
				this.m_ReactorModel = value;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x060023AE RID: 9134 RVA: 0x0002D6EC File Offset: 0x0002B8EC
		// (set) Token: 0x060023AF RID: 9135 RVA: 0x0002D6F4 File Offset: 0x0002B8F4
		public ushort ReactorMaximumReceivedMessages
		{
			get
			{
				return this.m_ReactorMaximumReceivedMessages;
			}
			set
			{
				this.m_ReactorMaximumReceivedMessages = value;
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x060023B0 RID: 9136 RVA: 0x0002D700 File Offset: 0x0002B900
		// (set) Token: 0x060023B1 RID: 9137 RVA: 0x0002D708 File Offset: 0x0002B908
		public ushort ReactorMaximumSentMessages
		{
			get
			{
				return this.m_ReactorMaximumSentMessages;
			}
			set
			{
				this.m_ReactorMaximumSentMessages = value;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060023B2 RID: 9138 RVA: 0x0002D714 File Offset: 0x0002B914
		// (set) Token: 0x060023B3 RID: 9139 RVA: 0x0002D71C File Offset: 0x0002B91C
		public ushort MaxPacketSize
		{
			get
			{
				return this.m_MaxPacketSize;
			}
			set
			{
				this.m_MaxPacketSize = value;
			}
		}

		// Token: 0x04000984 RID: 2436
		[SerializeField]
		private uint m_ThreadAwakeTimeout;

		// Token: 0x04000985 RID: 2437
		[SerializeField]
		private ReactorModel m_ReactorModel;

		// Token: 0x04000986 RID: 2438
		[SerializeField]
		private ushort m_ReactorMaximumReceivedMessages;

		// Token: 0x04000987 RID: 2439
		[SerializeField]
		private ushort m_ReactorMaximumSentMessages;

		// Token: 0x04000988 RID: 2440
		[SerializeField]
		private ushort m_MaxPacketSize;
	}
}
