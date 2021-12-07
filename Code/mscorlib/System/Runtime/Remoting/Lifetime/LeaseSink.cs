using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000489 RID: 1161
	internal class LeaseSink : IMessageSink
	{
		// Token: 0x06002F6D RID: 12141 RVA: 0x0009D070 File Offset: 0x0009B270
		public LeaseSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x0009D080 File Offset: 0x0009B280
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this.RenewLease(msg);
			return this._nextSink.SyncProcessMessage(msg);
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x0009D098 File Offset: 0x0009B298
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			this.RenewLease(msg);
			return this._nextSink.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x0009D0B0 File Offset: 0x0009B2B0
		private void RenewLease(IMessage msg)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			ILease lease = serverIdentity.Lease;
			if (lease != null && lease.CurrentLeaseTime < lease.RenewOnCallTime)
			{
				lease.Renew(lease.RenewOnCallTime);
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06002F71 RID: 12145 RVA: 0x0009D0FC File Offset: 0x0009B2FC
		public IMessageSink NextSink
		{
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x04001426 RID: 5158
		private IMessageSink _nextSink;
	}
}
