using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Networking
{
	// Token: 0x02000255 RID: 597
	internal sealed class HostTopologyInternal : IDisposable
	{
		// Token: 0x060023D1 RID: 9169 RVA: 0x0002D8D0 File Offset: 0x0002BAD0
		public HostTopologyInternal(HostTopology topology)
		{
			ConnectionConfigInternal config = new ConnectionConfigInternal(topology.DefaultConfig);
			this.InitWrapper(config, topology.MaxDefaultConnections);
			for (int i = 1; i <= topology.SpecialConnectionConfigsCount; i++)
			{
				ConnectionConfig specialConnectionConfig = topology.GetSpecialConnectionConfig(i);
				ConnectionConfigInternal config2 = new ConnectionConfigInternal(specialConnectionConfig);
				this.AddSpecialConnectionConfig(config2);
			}
			this.InitOtherParameters(topology);
		}

		// Token: 0x060023D2 RID: 9170
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitWrapper(ConnectionConfigInternal config, int maxDefaultConnections);

		// Token: 0x060023D3 RID: 9171 RVA: 0x0002D934 File Offset: 0x0002BB34
		private int AddSpecialConnectionConfig(ConnectionConfigInternal config)
		{
			return this.AddSpecialConnectionConfigWrapper(config);
		}

		// Token: 0x060023D4 RID: 9172
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int AddSpecialConnectionConfigWrapper(ConnectionConfigInternal config);

		// Token: 0x060023D5 RID: 9173 RVA: 0x0002D940 File Offset: 0x0002BB40
		private void InitOtherParameters(HostTopology topology)
		{
			this.InitReceivedPoolSize(topology.ReceivedMessagePoolSize);
			this.InitSentMessagePoolSize(topology.SentMessagePoolSize);
			this.InitMessagePoolSizeGrowthFactor(topology.MessagePoolSizeGrowthFactor);
		}

		// Token: 0x060023D6 RID: 9174
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitReceivedPoolSize(ushort pool);

		// Token: 0x060023D7 RID: 9175
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitSentMessagePoolSize(ushort pool);

		// Token: 0x060023D8 RID: 9176
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitMessagePoolSizeGrowthFactor(float factor);

		// Token: 0x060023D9 RID: 9177
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Dispose();

		// Token: 0x060023DA RID: 9178 RVA: 0x0002D974 File Offset: 0x0002BB74
		~HostTopologyInternal()
		{
			this.Dispose();
		}

		// Token: 0x0400098B RID: 2443
		internal IntPtr m_Ptr;
	}
}
