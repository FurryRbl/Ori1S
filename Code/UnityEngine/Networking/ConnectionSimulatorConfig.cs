using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Networking
{
	// Token: 0x02000253 RID: 595
	public sealed class ConnectionSimulatorConfig : IDisposable
	{
		// Token: 0x060023B4 RID: 9140
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ConnectionSimulatorConfig(int outMinDelay, int outAvgDelay, int inMinDelay, int inAvgDelay, float packetLossPercentage);

		// Token: 0x060023B5 RID: 9141
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Dispose();

		// Token: 0x060023B6 RID: 9142 RVA: 0x0002D728 File Offset: 0x0002B928
		~ConnectionSimulatorConfig()
		{
			this.Dispose();
		}

		// Token: 0x04000989 RID: 2441
		internal IntPtr m_Ptr;
	}
}
