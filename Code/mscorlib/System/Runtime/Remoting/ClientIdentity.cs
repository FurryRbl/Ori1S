using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x0200041D RID: 1053
	internal class ClientIdentity : Identity
	{
		// Token: 0x06002CD0 RID: 11472 RVA: 0x00094028 File Offset: 0x00092228
		public ClientIdentity(string objectUri, ObjRef objRef) : base(objectUri)
		{
			this._objRef = objRef;
			IMessageSink envoySink;
			if (this._objRef.EnvoyInfo != null)
			{
				IMessageSink envoySinks = this._objRef.EnvoyInfo.EnvoySinks;
				envoySink = envoySinks;
			}
			else
			{
				envoySink = null;
			}
			this._envoySink = envoySink;
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06002CD1 RID: 11473 RVA: 0x00094074 File Offset: 0x00092274
		// (set) Token: 0x06002CD2 RID: 11474 RVA: 0x00094088 File Offset: 0x00092288
		public MarshalByRefObject ClientProxy
		{
			get
			{
				return (MarshalByRefObject)this._proxyReference.Target;
			}
			set
			{
				this._proxyReference = new WeakReference(value);
			}
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x00094098 File Offset: 0x00092298
		public override ObjRef CreateObjRef(Type requestedType)
		{
			return this._objRef;
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06002CD4 RID: 11476 RVA: 0x000940A0 File Offset: 0x000922A0
		public string TargetUri
		{
			get
			{
				return this._objRef.URI;
			}
		}

		// Token: 0x04001363 RID: 4963
		private WeakReference _proxyReference;
	}
}
