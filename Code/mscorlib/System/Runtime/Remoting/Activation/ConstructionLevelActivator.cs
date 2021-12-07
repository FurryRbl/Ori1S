using System;
using System.Threading;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200043C RID: 1084
	[Serializable]
	internal class ConstructionLevelActivator : IActivator
	{
		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002DEF RID: 11759 RVA: 0x000992B8 File Offset: 0x000974B8
		public ActivatorLevel Level
		{
			get
			{
				return ActivatorLevel.Construction;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06002DF0 RID: 11760 RVA: 0x000992BC File Offset: 0x000974BC
		// (set) Token: 0x06002DF1 RID: 11761 RVA: 0x000992C0 File Offset: 0x000974C0
		public IActivator NextActivator
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000992C4 File Offset: 0x000974C4
		public IConstructionReturnMessage Activate(IConstructionCallMessage msg)
		{
			return (IConstructionReturnMessage)Thread.CurrentContext.GetServerContextSinkChain().SyncProcessMessage(msg);
		}
	}
}
