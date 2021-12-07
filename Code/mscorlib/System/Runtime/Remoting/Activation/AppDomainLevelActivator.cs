using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200043B RID: 1083
	internal class AppDomainLevelActivator : IActivator
	{
		// Token: 0x06002DE9 RID: 11753 RVA: 0x000991CC File Offset: 0x000973CC
		public AppDomainLevelActivator(string activationUrl, IActivator next)
		{
			this._activationUrl = activationUrl;
			this._next = next;
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06002DEA RID: 11754 RVA: 0x000991E4 File Offset: 0x000973E4
		public ActivatorLevel Level
		{
			get
			{
				return ActivatorLevel.AppDomain;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06002DEB RID: 11755 RVA: 0x000991E8 File Offset: 0x000973E8
		// (set) Token: 0x06002DEC RID: 11756 RVA: 0x000991F0 File Offset: 0x000973F0
		public IActivator NextActivator
		{
			get
			{
				return this._next;
			}
			set
			{
				this._next = value;
			}
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x000991FC File Offset: 0x000973FC
		public IConstructionReturnMessage Activate(IConstructionCallMessage ctorCall)
		{
			IActivator activator = (IActivator)RemotingServices.Connect(typeof(IActivator), this._activationUrl);
			ctorCall.Activator = ctorCall.Activator.NextActivator;
			IConstructionReturnMessage constructionReturnMessage;
			try
			{
				constructionReturnMessage = activator.Activate(ctorCall);
			}
			catch (Exception e)
			{
				return new ConstructionResponse(e, ctorCall);
			}
			ObjRef objRef = (ObjRef)constructionReturnMessage.ReturnValue;
			if (RemotingServices.GetIdentityForUri(objRef.URI) != null)
			{
				throw new RemotingException("Inconsistent state during activation; there may be two proxies for the same object");
			}
			object obj;
			Identity orCreateClientIdentity = RemotingServices.GetOrCreateClientIdentity(objRef, null, out obj);
			RemotingServices.SetMessageTargetIdentity(ctorCall, orCreateClientIdentity);
			return constructionReturnMessage;
		}

		// Token: 0x040013C0 RID: 5056
		private string _activationUrl;

		// Token: 0x040013C1 RID: 5057
		private IActivator _next;
	}
}
