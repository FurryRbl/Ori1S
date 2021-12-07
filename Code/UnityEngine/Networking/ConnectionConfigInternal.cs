using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Networking
{
	// Token: 0x02000254 RID: 596
	internal sealed class ConnectionConfigInternal : IDisposable
	{
		// Token: 0x060023B7 RID: 9143 RVA: 0x0002D764 File Offset: 0x0002B964
		private ConnectionConfigInternal()
		{
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x0002D76C File Offset: 0x0002B96C
		public ConnectionConfigInternal(ConnectionConfig config)
		{
			if (config == null)
			{
				throw new NullReferenceException("config is not defined");
			}
			this.InitWrapper();
			this.InitPacketSize(config.PacketSize);
			this.InitFragmentSize(config.FragmentSize);
			this.InitResendTimeout(config.ResendTimeout);
			this.InitDisconnectTimeout(config.DisconnectTimeout);
			this.InitConnectTimeout(config.ConnectTimeout);
			this.InitMinUpdateTimeout(config.MinUpdateTimeout);
			this.InitPingTimeout(config.PingTimeout);
			this.InitReducedPingTimeout(config.ReducedPingTimeout);
			this.InitAllCostTimeout(config.AllCostTimeout);
			this.InitNetworkDropThreshold(config.NetworkDropThreshold);
			this.InitOverflowDropThreshold(config.OverflowDropThreshold);
			this.InitMaxConnectionAttempt(config.MaxConnectionAttempt);
			this.InitAckDelay(config.AckDelay);
			this.InitMaxCombinedReliableMessageSize(config.MaxCombinedReliableMessageSize);
			this.InitMaxCombinedReliableMessageCount(config.MaxCombinedReliableMessageCount);
			this.InitMaxSentMessageQueueSize(config.MaxSentMessageQueueSize);
			this.InitIsAcksLong(config.IsAcksLong);
			this.InitUsePlatformSpecificProtocols(config.UsePlatformSpecificProtocols);
			byte b = 0;
			while ((int)b < config.ChannelCount)
			{
				this.AddChannel(config.GetChannel(b));
				b += 1;
			}
		}

		// Token: 0x060023B9 RID: 9145
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitWrapper();

		// Token: 0x060023BA RID: 9146
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern byte AddChannel(QosType value);

		// Token: 0x060023BB RID: 9147
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern QosType GetChannel(int i);

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060023BC RID: 9148
		public extern int ChannelSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060023BD RID: 9149
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitPacketSize(ushort value);

		// Token: 0x060023BE RID: 9150
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitFragmentSize(ushort value);

		// Token: 0x060023BF RID: 9151
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitResendTimeout(uint value);

		// Token: 0x060023C0 RID: 9152
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitDisconnectTimeout(uint value);

		// Token: 0x060023C1 RID: 9153
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitConnectTimeout(uint value);

		// Token: 0x060023C2 RID: 9154
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitMinUpdateTimeout(uint value);

		// Token: 0x060023C3 RID: 9155
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitPingTimeout(uint value);

		// Token: 0x060023C4 RID: 9156
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitReducedPingTimeout(uint value);

		// Token: 0x060023C5 RID: 9157
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitAllCostTimeout(uint value);

		// Token: 0x060023C6 RID: 9158
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitNetworkDropThreshold(byte value);

		// Token: 0x060023C7 RID: 9159
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitOverflowDropThreshold(byte value);

		// Token: 0x060023C8 RID: 9160
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitMaxConnectionAttempt(byte value);

		// Token: 0x060023C9 RID: 9161
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitAckDelay(uint value);

		// Token: 0x060023CA RID: 9162
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitMaxCombinedReliableMessageSize(ushort value);

		// Token: 0x060023CB RID: 9163
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitMaxCombinedReliableMessageCount(ushort value);

		// Token: 0x060023CC RID: 9164
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitMaxSentMessageQueueSize(ushort value);

		// Token: 0x060023CD RID: 9165
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitIsAcksLong(bool value);

		// Token: 0x060023CE RID: 9166
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitUsePlatformSpecificProtocols(bool value);

		// Token: 0x060023CF RID: 9167
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Dispose();

		// Token: 0x060023D0 RID: 9168 RVA: 0x0002D894 File Offset: 0x0002BA94
		~ConnectionConfigInternal()
		{
			this.Dispose();
		}

		// Token: 0x0400098A RID: 2442
		internal IntPtr m_Ptr;
	}
}
