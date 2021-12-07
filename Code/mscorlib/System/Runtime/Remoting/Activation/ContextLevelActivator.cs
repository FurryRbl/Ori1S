using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200043D RID: 1085
	[Serializable]
	internal class ContextLevelActivator : IActivator
	{
		// Token: 0x06002DF3 RID: 11763 RVA: 0x000992DC File Offset: 0x000974DC
		public ContextLevelActivator(IActivator next)
		{
			this.m_NextActivator = next;
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06002DF4 RID: 11764 RVA: 0x000992EC File Offset: 0x000974EC
		public ActivatorLevel Level
		{
			get
			{
				return ActivatorLevel.Context;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06002DF5 RID: 11765 RVA: 0x000992F0 File Offset: 0x000974F0
		// (set) Token: 0x06002DF6 RID: 11766 RVA: 0x000992F8 File Offset: 0x000974F8
		public IActivator NextActivator
		{
			get
			{
				return this.m_NextActivator;
			}
			set
			{
				this.m_NextActivator = value;
			}
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x00099304 File Offset: 0x00097504
		public IConstructionReturnMessage Activate(IConstructionCallMessage ctorCall)
		{
			ServerIdentity serverIdentity = RemotingServices.CreateContextBoundObjectIdentity(ctorCall.ActivationType);
			RemotingServices.SetMessageTargetIdentity(ctorCall, serverIdentity);
			ConstructionCall constructionCall = ctorCall as ConstructionCall;
			if (constructionCall == null || !constructionCall.IsContextOk)
			{
				serverIdentity.Context = Context.CreateNewContext(ctorCall);
				Context newContext = Context.SwitchToContext(serverIdentity.Context);
				try
				{
					return this.m_NextActivator.Activate(ctorCall);
				}
				finally
				{
					Context.SwitchToContext(newContext);
				}
			}
			return this.m_NextActivator.Activate(ctorCall);
		}

		// Token: 0x040013C2 RID: 5058
		private IActivator m_NextActivator;
	}
}
