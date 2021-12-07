using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x0200042E RID: 1070
	internal class ClientActivatedIdentity : ServerIdentity
	{
		// Token: 0x06002D94 RID: 11668 RVA: 0x00097C44 File Offset: 0x00095E44
		public ClientActivatedIdentity(string objectUri, Type objectType) : base(objectUri, null, objectType)
		{
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x00097C50 File Offset: 0x00095E50
		public MarshalByRefObject GetServerObject()
		{
			return this._serverObject;
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x00097C58 File Offset: 0x00095E58
		public MarshalByRefObject GetClientProxy()
		{
			return this._targetThis;
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x00097C60 File Offset: 0x00095E60
		public void SetClientProxy(MarshalByRefObject obj)
		{
			this._targetThis = obj;
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x00097C6C File Offset: 0x00095E6C
		public override void OnLifetimeExpired()
		{
			base.OnLifetimeExpired();
			RemotingServices.DisposeIdentity(this);
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x00097C7C File Offset: 0x00095E7C
		public override IMessage SyncObjectProcessMessage(IMessage msg)
		{
			if (this._serverSink == null)
			{
				bool flag = this._targetThis != null;
				this._serverSink = this._context.CreateServerObjectSinkChain((!flag) ? this._serverObject : this._targetThis, flag);
			}
			return this._serverSink.SyncProcessMessage(msg);
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x00097CD8 File Offset: 0x00095ED8
		public override IMessageCtrl AsyncObjectProcessMessage(IMessage msg, IMessageSink replySink)
		{
			if (this._serverSink == null)
			{
				bool flag = this._targetThis != null;
				this._serverSink = this._context.CreateServerObjectSinkChain((!flag) ? this._serverObject : this._targetThis, flag);
			}
			return this._serverSink.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x040013A1 RID: 5025
		private MarshalByRefObject _targetThis;
	}
}
