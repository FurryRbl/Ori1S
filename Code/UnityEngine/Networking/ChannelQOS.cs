using System;

namespace UnityEngine.Networking
{
	// Token: 0x0200024F RID: 591
	[Serializable]
	public class ChannelQOS
	{
		// Token: 0x0600236C RID: 9068 RVA: 0x0002CF34 File Offset: 0x0002B134
		public ChannelQOS(QosType value)
		{
			this.m_Type = value;
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x0002CF44 File Offset: 0x0002B144
		public ChannelQOS()
		{
			this.m_Type = QosType.Unreliable;
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x0002CF54 File Offset: 0x0002B154
		public ChannelQOS(ChannelQOS channel)
		{
			if (channel == null)
			{
				throw new NullReferenceException("channel is not defined");
			}
			this.m_Type = channel.m_Type;
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x0002CF7C File Offset: 0x0002B17C
		public QosType QOS
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x04000969 RID: 2409
		[SerializeField]
		internal QosType m_Type;
	}
}
