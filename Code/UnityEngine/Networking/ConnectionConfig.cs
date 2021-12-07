using System;
using System.Collections.Generic;

namespace UnityEngine.Networking
{
	// Token: 0x02000250 RID: 592
	[Serializable]
	public class ConnectionConfig
	{
		// Token: 0x06002370 RID: 9072 RVA: 0x0002CF84 File Offset: 0x0002B184
		public ConnectionConfig()
		{
			this.m_PacketSize = 1500;
			this.m_FragmentSize = 500;
			this.m_ResendTimeout = 1200U;
			this.m_DisconnectTimeout = 2000U;
			this.m_ConnectTimeout = 2000U;
			this.m_MinUpdateTimeout = 10U;
			this.m_PingTimeout = 500U;
			this.m_ReducedPingTimeout = 100U;
			this.m_AllCostTimeout = 20U;
			this.m_NetworkDropThreshold = 5;
			this.m_OverflowDropThreshold = 5;
			this.m_MaxConnectionAttempt = 10;
			this.m_AckDelay = 33U;
			this.m_MaxCombinedReliableMessageSize = 100;
			this.m_MaxCombinedReliableMessageCount = 10;
			this.m_MaxSentMessageQueueSize = 128;
			this.m_IsAcksLong = false;
			this.m_UsePlatformSpecificProtocols = false;
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x0002D044 File Offset: 0x0002B244
		public ConnectionConfig(ConnectionConfig config)
		{
			if (config == null)
			{
				throw new NullReferenceException("config is not defined");
			}
			this.m_PacketSize = config.m_PacketSize;
			this.m_FragmentSize = config.m_FragmentSize;
			this.m_ResendTimeout = config.m_ResendTimeout;
			this.m_DisconnectTimeout = config.m_DisconnectTimeout;
			this.m_ConnectTimeout = config.m_ConnectTimeout;
			this.m_MinUpdateTimeout = config.m_MinUpdateTimeout;
			this.m_PingTimeout = config.m_PingTimeout;
			this.m_ReducedPingTimeout = config.m_ReducedPingTimeout;
			this.m_AllCostTimeout = config.m_AllCostTimeout;
			this.m_NetworkDropThreshold = config.m_NetworkDropThreshold;
			this.m_OverflowDropThreshold = config.m_OverflowDropThreshold;
			this.m_MaxConnectionAttempt = config.m_MaxConnectionAttempt;
			this.m_AckDelay = config.m_AckDelay;
			this.m_MaxCombinedReliableMessageSize = config.MaxCombinedReliableMessageSize;
			this.m_MaxCombinedReliableMessageCount = config.m_MaxCombinedReliableMessageCount;
			this.m_MaxSentMessageQueueSize = config.m_MaxSentMessageQueueSize;
			this.m_IsAcksLong = config.m_IsAcksLong;
			this.m_UsePlatformSpecificProtocols = config.m_UsePlatformSpecificProtocols;
			foreach (ChannelQOS channel in config.m_Channels)
			{
				this.m_Channels.Add(new ChannelQOS(channel));
			}
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x0002D1B0 File Offset: 0x0002B3B0
		public static void Validate(ConnectionConfig config)
		{
			if (config.m_PacketSize < 128)
			{
				throw new ArgumentOutOfRangeException("PacketSize should be > " + 128.ToString());
			}
			if (config.m_FragmentSize >= config.m_PacketSize - 128)
			{
				throw new ArgumentOutOfRangeException("FragmentSize should be < PacketSize - " + 128.ToString());
			}
			if (config.m_Channels.Count > 255)
			{
				throw new ArgumentOutOfRangeException("Channels number should be less than 256");
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06002373 RID: 9075 RVA: 0x0002D240 File Offset: 0x0002B440
		// (set) Token: 0x06002374 RID: 9076 RVA: 0x0002D248 File Offset: 0x0002B448
		public ushort PacketSize
		{
			get
			{
				return this.m_PacketSize;
			}
			set
			{
				this.m_PacketSize = value;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x0002D254 File Offset: 0x0002B454
		// (set) Token: 0x06002376 RID: 9078 RVA: 0x0002D25C File Offset: 0x0002B45C
		public ushort FragmentSize
		{
			get
			{
				return this.m_FragmentSize;
			}
			set
			{
				this.m_FragmentSize = value;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002377 RID: 9079 RVA: 0x0002D268 File Offset: 0x0002B468
		// (set) Token: 0x06002378 RID: 9080 RVA: 0x0002D270 File Offset: 0x0002B470
		public uint ResendTimeout
		{
			get
			{
				return this.m_ResendTimeout;
			}
			set
			{
				this.m_ResendTimeout = value;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06002379 RID: 9081 RVA: 0x0002D27C File Offset: 0x0002B47C
		// (set) Token: 0x0600237A RID: 9082 RVA: 0x0002D284 File Offset: 0x0002B484
		public uint DisconnectTimeout
		{
			get
			{
				return this.m_DisconnectTimeout;
			}
			set
			{
				this.m_DisconnectTimeout = value;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x0002D290 File Offset: 0x0002B490
		// (set) Token: 0x0600237C RID: 9084 RVA: 0x0002D298 File Offset: 0x0002B498
		public uint ConnectTimeout
		{
			get
			{
				return this.m_ConnectTimeout;
			}
			set
			{
				this.m_ConnectTimeout = value;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x0002D2A4 File Offset: 0x0002B4A4
		// (set) Token: 0x0600237E RID: 9086 RVA: 0x0002D2AC File Offset: 0x0002B4AC
		public uint MinUpdateTimeout
		{
			get
			{
				return this.m_MinUpdateTimeout;
			}
			set
			{
				if (value == 0U)
				{
					throw new ArgumentOutOfRangeException("Minimal update timeout should be > 0");
				}
				this.m_MinUpdateTimeout = value;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x0002D2C8 File Offset: 0x0002B4C8
		// (set) Token: 0x06002380 RID: 9088 RVA: 0x0002D2D0 File Offset: 0x0002B4D0
		public uint PingTimeout
		{
			get
			{
				return this.m_PingTimeout;
			}
			set
			{
				this.m_PingTimeout = value;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06002381 RID: 9089 RVA: 0x0002D2DC File Offset: 0x0002B4DC
		// (set) Token: 0x06002382 RID: 9090 RVA: 0x0002D2E4 File Offset: 0x0002B4E4
		public uint ReducedPingTimeout
		{
			get
			{
				return this.m_ReducedPingTimeout;
			}
			set
			{
				this.m_ReducedPingTimeout = value;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06002383 RID: 9091 RVA: 0x0002D2F0 File Offset: 0x0002B4F0
		// (set) Token: 0x06002384 RID: 9092 RVA: 0x0002D2F8 File Offset: 0x0002B4F8
		public uint AllCostTimeout
		{
			get
			{
				return this.m_AllCostTimeout;
			}
			set
			{
				this.m_AllCostTimeout = value;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06002385 RID: 9093 RVA: 0x0002D304 File Offset: 0x0002B504
		// (set) Token: 0x06002386 RID: 9094 RVA: 0x0002D30C File Offset: 0x0002B50C
		public byte NetworkDropThreshold
		{
			get
			{
				return this.m_NetworkDropThreshold;
			}
			set
			{
				this.m_NetworkDropThreshold = value;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x0002D318 File Offset: 0x0002B518
		// (set) Token: 0x06002388 RID: 9096 RVA: 0x0002D320 File Offset: 0x0002B520
		public byte OverflowDropThreshold
		{
			get
			{
				return this.m_OverflowDropThreshold;
			}
			set
			{
				this.m_OverflowDropThreshold = value;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06002389 RID: 9097 RVA: 0x0002D32C File Offset: 0x0002B52C
		// (set) Token: 0x0600238A RID: 9098 RVA: 0x0002D334 File Offset: 0x0002B534
		public byte MaxConnectionAttempt
		{
			get
			{
				return this.m_MaxConnectionAttempt;
			}
			set
			{
				this.m_MaxConnectionAttempt = value;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x0002D340 File Offset: 0x0002B540
		// (set) Token: 0x0600238C RID: 9100 RVA: 0x0002D348 File Offset: 0x0002B548
		public uint AckDelay
		{
			get
			{
				return this.m_AckDelay;
			}
			set
			{
				this.m_AckDelay = value;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x0600238D RID: 9101 RVA: 0x0002D354 File Offset: 0x0002B554
		// (set) Token: 0x0600238E RID: 9102 RVA: 0x0002D35C File Offset: 0x0002B55C
		public ushort MaxCombinedReliableMessageSize
		{
			get
			{
				return this.m_MaxCombinedReliableMessageSize;
			}
			set
			{
				this.m_MaxCombinedReliableMessageSize = value;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x0600238F RID: 9103 RVA: 0x0002D368 File Offset: 0x0002B568
		// (set) Token: 0x06002390 RID: 9104 RVA: 0x0002D370 File Offset: 0x0002B570
		public ushort MaxCombinedReliableMessageCount
		{
			get
			{
				return this.m_MaxCombinedReliableMessageCount;
			}
			set
			{
				this.m_MaxCombinedReliableMessageCount = value;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x0002D37C File Offset: 0x0002B57C
		// (set) Token: 0x06002392 RID: 9106 RVA: 0x0002D384 File Offset: 0x0002B584
		public ushort MaxSentMessageQueueSize
		{
			get
			{
				return this.m_MaxSentMessageQueueSize;
			}
			set
			{
				this.m_MaxSentMessageQueueSize = value;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06002393 RID: 9107 RVA: 0x0002D390 File Offset: 0x0002B590
		// (set) Token: 0x06002394 RID: 9108 RVA: 0x0002D398 File Offset: 0x0002B598
		public bool IsAcksLong
		{
			get
			{
				return this.m_IsAcksLong;
			}
			set
			{
				this.m_IsAcksLong = value;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06002395 RID: 9109 RVA: 0x0002D3A4 File Offset: 0x0002B5A4
		// (set) Token: 0x06002396 RID: 9110 RVA: 0x0002D3AC File Offset: 0x0002B5AC
		public bool UsePlatformSpecificProtocols
		{
			get
			{
				return this.m_UsePlatformSpecificProtocols;
			}
			set
			{
				if (value && Application.platform != RuntimePlatform.PS4)
				{
					throw new ArgumentOutOfRangeException("Platform specific protocols are not supported on this platform");
				}
				this.m_UsePlatformSpecificProtocols = value;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06002397 RID: 9111 RVA: 0x0002D3E0 File Offset: 0x0002B5E0
		public int ChannelCount
		{
			get
			{
				return this.m_Channels.Count;
			}
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x0002D3F0 File Offset: 0x0002B5F0
		public byte AddChannel(QosType value)
		{
			if (this.m_Channels.Count > 255)
			{
				throw new ArgumentOutOfRangeException("Channels Count should be less than 256");
			}
			if (!Enum.IsDefined(typeof(QosType), value))
			{
				throw new ArgumentOutOfRangeException("requested qos type doesn't exist: " + (int)value);
			}
			ChannelQOS item = new ChannelQOS(value);
			this.m_Channels.Add(item);
			return (byte)(this.m_Channels.Count - 1);
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x0002D470 File Offset: 0x0002B670
		public QosType GetChannel(byte idx)
		{
			if ((int)idx >= this.m_Channels.Count)
			{
				throw new ArgumentOutOfRangeException("requested index greater than maximum channels count");
			}
			return this.m_Channels[(int)idx].QOS;
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x0600239A RID: 9114 RVA: 0x0002D4A0 File Offset: 0x0002B6A0
		public List<ChannelQOS> Channels
		{
			get
			{
				return this.m_Channels;
			}
		}

		// Token: 0x0400096A RID: 2410
		private const int g_MinPacketSize = 128;

		// Token: 0x0400096B RID: 2411
		[SerializeField]
		private ushort m_PacketSize;

		// Token: 0x0400096C RID: 2412
		[SerializeField]
		private ushort m_FragmentSize;

		// Token: 0x0400096D RID: 2413
		[SerializeField]
		private uint m_ResendTimeout;

		// Token: 0x0400096E RID: 2414
		[SerializeField]
		private uint m_DisconnectTimeout;

		// Token: 0x0400096F RID: 2415
		[SerializeField]
		private uint m_ConnectTimeout;

		// Token: 0x04000970 RID: 2416
		[SerializeField]
		private uint m_MinUpdateTimeout;

		// Token: 0x04000971 RID: 2417
		[SerializeField]
		private uint m_PingTimeout;

		// Token: 0x04000972 RID: 2418
		[SerializeField]
		private uint m_ReducedPingTimeout;

		// Token: 0x04000973 RID: 2419
		[SerializeField]
		private uint m_AllCostTimeout;

		// Token: 0x04000974 RID: 2420
		[SerializeField]
		private byte m_NetworkDropThreshold;

		// Token: 0x04000975 RID: 2421
		[SerializeField]
		private byte m_OverflowDropThreshold;

		// Token: 0x04000976 RID: 2422
		[SerializeField]
		private byte m_MaxConnectionAttempt;

		// Token: 0x04000977 RID: 2423
		[SerializeField]
		private uint m_AckDelay;

		// Token: 0x04000978 RID: 2424
		[SerializeField]
		private ushort m_MaxCombinedReliableMessageSize;

		// Token: 0x04000979 RID: 2425
		[SerializeField]
		private ushort m_MaxCombinedReliableMessageCount;

		// Token: 0x0400097A RID: 2426
		[SerializeField]
		private ushort m_MaxSentMessageQueueSize;

		// Token: 0x0400097B RID: 2427
		[SerializeField]
		private bool m_IsAcksLong;

		// Token: 0x0400097C RID: 2428
		[SerializeField]
		private bool m_UsePlatformSpecificProtocols;

		// Token: 0x0400097D RID: 2429
		[SerializeField]
		internal List<ChannelQOS> m_Channels = new List<ChannelQOS>();
	}
}
