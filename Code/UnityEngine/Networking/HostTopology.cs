using System;
using System.Collections.Generic;

namespace UnityEngine.Networking
{
	// Token: 0x02000251 RID: 593
	[Serializable]
	public class HostTopology
	{
		// Token: 0x0600239B RID: 9115 RVA: 0x0002D4A8 File Offset: 0x0002B6A8
		public HostTopology(ConnectionConfig defaultConfig, int maxDefaultConnections)
		{
			if (defaultConfig == null)
			{
				throw new NullReferenceException("config is not defined");
			}
			if (maxDefaultConnections <= 0)
			{
				throw new ArgumentOutOfRangeException("maxDefaultConnections", "count connection should be > 0");
			}
			if (maxDefaultConnections > 65535)
			{
				throw new ArgumentOutOfRangeException("maxDefaultConnections", "count connection should be < 65535");
			}
			ConnectionConfig.Validate(defaultConfig);
			this.m_DefConfig = new ConnectionConfig(defaultConfig);
			this.m_MaxDefConnections = maxDefaultConnections;
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x0002D544 File Offset: 0x0002B744
		private HostTopology()
		{
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x0600239D RID: 9117 RVA: 0x0002D584 File Offset: 0x0002B784
		public ConnectionConfig DefaultConfig
		{
			get
			{
				return this.m_DefConfig;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x0600239E RID: 9118 RVA: 0x0002D58C File Offset: 0x0002B78C
		public int MaxDefaultConnections
		{
			get
			{
				return this.m_MaxDefConnections;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x0600239F RID: 9119 RVA: 0x0002D594 File Offset: 0x0002B794
		public int SpecialConnectionConfigsCount
		{
			get
			{
				return this.m_SpecialConnections.Count;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x060023A0 RID: 9120 RVA: 0x0002D5A4 File Offset: 0x0002B7A4
		public List<ConnectionConfig> SpecialConnectionConfigs
		{
			get
			{
				return this.m_SpecialConnections;
			}
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x0002D5AC File Offset: 0x0002B7AC
		public ConnectionConfig GetSpecialConnectionConfig(int i)
		{
			if (i > this.m_SpecialConnections.Count || i == 0)
			{
				throw new ArgumentException("special configuration index is out of valid range");
			}
			return this.m_SpecialConnections[i - 1];
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x060023A2 RID: 9122 RVA: 0x0002D5EC File Offset: 0x0002B7EC
		// (set) Token: 0x060023A3 RID: 9123 RVA: 0x0002D5F4 File Offset: 0x0002B7F4
		public ushort ReceivedMessagePoolSize
		{
			get
			{
				return this.m_ReceivedMessagePoolSize;
			}
			set
			{
				this.m_ReceivedMessagePoolSize = value;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x060023A4 RID: 9124 RVA: 0x0002D600 File Offset: 0x0002B800
		// (set) Token: 0x060023A5 RID: 9125 RVA: 0x0002D608 File Offset: 0x0002B808
		public ushort SentMessagePoolSize
		{
			get
			{
				return this.m_SentMessagePoolSize;
			}
			set
			{
				this.m_SentMessagePoolSize = value;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x0002D614 File Offset: 0x0002B814
		// (set) Token: 0x060023A7 RID: 9127 RVA: 0x0002D61C File Offset: 0x0002B81C
		public float MessagePoolSizeGrowthFactor
		{
			get
			{
				return this.m_MessagePoolSizeGrowthFactor;
			}
			set
			{
				if ((double)value <= 0.5 || (double)value > 1.0)
				{
					throw new ArgumentException("pool growth factor should be varied between 0.5 and 1.0");
				}
				this.m_MessagePoolSizeGrowthFactor = value;
			}
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x0002D65C File Offset: 0x0002B85C
		public int AddSpecialConnectionConfig(ConnectionConfig config)
		{
			this.m_SpecialConnections.Add(new ConnectionConfig(config));
			return this.m_SpecialConnections.Count - 1;
		}

		// Token: 0x0400097E RID: 2430
		[SerializeField]
		private ConnectionConfig m_DefConfig;

		// Token: 0x0400097F RID: 2431
		[SerializeField]
		private int m_MaxDefConnections;

		// Token: 0x04000980 RID: 2432
		[SerializeField]
		private List<ConnectionConfig> m_SpecialConnections = new List<ConnectionConfig>();

		// Token: 0x04000981 RID: 2433
		[SerializeField]
		private ushort m_ReceivedMessagePoolSize = 128;

		// Token: 0x04000982 RID: 2434
		[SerializeField]
		private ushort m_SentMessagePoolSize = 128;

		// Token: 0x04000983 RID: 2435
		[SerializeField]
		private float m_MessagePoolSizeGrowthFactor = 0.75f;
	}
}
