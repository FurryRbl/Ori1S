using System;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000441 RID: 1089
	internal class RemoteActivator : MarshalByRefObject, IActivator
	{
		// Token: 0x06002E03 RID: 11779 RVA: 0x000993A8 File Offset: 0x000975A8
		public IConstructionReturnMessage Activate(IConstructionCallMessage msg)
		{
			if (!RemotingConfiguration.IsActivationAllowed(msg.ActivationType))
			{
				throw new RemotingException("The type " + msg.ActivationTypeName + " is not allowed to be client activated");
			}
			object[] activationAttributes = new object[]
			{
				new RemoteActivationAttribute(msg.ContextProperties)
			};
			MarshalByRefObject obj = (MarshalByRefObject)Activator.CreateInstance(msg.ActivationType, msg.Args, activationAttributes);
			ObjRef resultObject = RemotingServices.Marshal(obj);
			return new ConstructionResponse(resultObject, null, msg);
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x0009941C File Offset: 0x0009761C
		public override object InitializeLifetimeService()
		{
			ILease lease = (ILease)base.InitializeLifetimeService();
			if (lease.CurrentState == LeaseState.Initial)
			{
				lease.InitialLeaseTime = TimeSpan.FromMinutes(30.0);
				lease.SponsorshipTimeout = TimeSpan.FromMinutes(1.0);
				lease.RenewOnCallTime = TimeSpan.FromMinutes(10.0);
			}
			return lease;
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06002E05 RID: 11781 RVA: 0x00099480 File Offset: 0x00097680
		public ActivatorLevel Level
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06002E06 RID: 11782 RVA: 0x00099488 File Offset: 0x00097688
		// (set) Token: 0x06002E07 RID: 11783 RVA: 0x00099490 File Offset: 0x00097690
		public IActivator NextActivator
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
