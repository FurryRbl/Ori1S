using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Networking
{
	// Token: 0x02000256 RID: 598
	internal sealed class GlobalConfigInternal : IDisposable
	{
		// Token: 0x060023DB RID: 9179 RVA: 0x0002D9B0 File Offset: 0x0002BBB0
		public GlobalConfigInternal(GlobalConfig config)
		{
			this.InitWrapper();
			this.InitThreadAwakeTimeout(config.ThreadAwakeTimeout);
			this.InitReactorModel((byte)config.ReactorModel);
			this.InitReactorMaximumReceivedMessages(config.ReactorMaximumReceivedMessages);
			this.InitReactorMaximumSentMessages(config.ReactorMaximumSentMessages);
			this.InitMaxPacketSize(config.MaxPacketSize);
		}

		// Token: 0x060023DC RID: 9180
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitWrapper();

		// Token: 0x060023DD RID: 9181
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitThreadAwakeTimeout(uint ms);

		// Token: 0x060023DE RID: 9182
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitReactorModel(byte model);

		// Token: 0x060023DF RID: 9183
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitReactorMaximumReceivedMessages(ushort size);

		// Token: 0x060023E0 RID: 9184
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitReactorMaximumSentMessages(ushort size);

		// Token: 0x060023E1 RID: 9185
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitMaxPacketSize(ushort size);

		// Token: 0x060023E2 RID: 9186
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Dispose();

		// Token: 0x060023E3 RID: 9187 RVA: 0x0002DA08 File Offset: 0x0002BC08
		~GlobalConfigInternal()
		{
			this.Dispose();
		}

		// Token: 0x0400098C RID: 2444
		internal IntPtr m_Ptr;
	}
}
