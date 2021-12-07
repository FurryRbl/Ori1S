using System;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200044D RID: 1101
	internal class ChanelSinkStackEntry
	{
		// Token: 0x06002E62 RID: 11874 RVA: 0x0009A908 File Offset: 0x00098B08
		public ChanelSinkStackEntry(IChannelSinkBase sink, object state, ChanelSinkStackEntry next)
		{
			this.Sink = sink;
			this.State = state;
			this.Next = next;
		}

		// Token: 0x040013D7 RID: 5079
		public IChannelSinkBase Sink;

		// Token: 0x040013D8 RID: 5080
		public object State;

		// Token: 0x040013D9 RID: 5081
		public ChanelSinkStackEntry Next;
	}
}
