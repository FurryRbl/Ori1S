using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Threading;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000439 RID: 1081
	internal class ActivationServices
	{
		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06002DE0 RID: 11744 RVA: 0x00098D6C File Offset: 0x00096F6C
		private static IActivator ConstructionActivator
		{
			get
			{
				if (ActivationServices._constructionActivator == null)
				{
					ActivationServices._constructionActivator = new ConstructionLevelActivator();
				}
				return ActivationServices._constructionActivator;
			}
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x00098D88 File Offset: 0x00096F88
		public static IMessage Activate(RemotingProxy proxy, ConstructionCall ctorCall)
		{
			ctorCall.SourceProxy = proxy;
			IMessage message;
			if (Thread.CurrentContext.HasExitSinks && !ctorCall.IsContextOk)
			{
				message = Thread.CurrentContext.GetClientContextSinkChain().SyncProcessMessage(ctorCall);
			}
			else
			{
				message = ActivationServices.RemoteActivate(ctorCall);
			}
			if (message is IConstructionReturnMessage && ((IConstructionReturnMessage)message).Exception == null && proxy.ObjectIdentity == null)
			{
				Identity messageTargetIdentity = RemotingServices.GetMessageTargetIdentity(ctorCall);
				proxy.AttachIdentity(messageTargetIdentity);
			}
			return message;
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x00098E08 File Offset: 0x00097008
		public static IMessage RemoteActivate(IConstructionCallMessage ctorCall)
		{
			IMessage result;
			try
			{
				result = ctorCall.Activator.Activate(ctorCall);
			}
			catch (Exception e)
			{
				result = new ReturnMessage(e, ctorCall);
			}
			return result;
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x00098E5C File Offset: 0x0009705C
		public static object CreateProxyFromAttributes(Type type, object[] activationAttributes)
		{
			string text = null;
			foreach (object obj in activationAttributes)
			{
				if (!(obj is IContextAttribute))
				{
					throw new RemotingException("Activation attribute does not implement the IContextAttribute interface");
				}
				if (obj is UrlAttribute)
				{
					text = ((UrlAttribute)obj).UrlValue;
				}
			}
			if (text != null)
			{
				return RemotingServices.CreateClientProxy(type, text, activationAttributes);
			}
			ActivatedClientTypeEntry activatedClientTypeEntry = RemotingConfiguration.IsRemotelyActivatedClientType(type);
			if (activatedClientTypeEntry != null)
			{
				return RemotingServices.CreateClientProxy(activatedClientTypeEntry, activationAttributes);
			}
			if (type.IsContextful)
			{
				return RemotingServices.CreateClientProxyForContextBound(type, activationAttributes);
			}
			return null;
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x00098EF0 File Offset: 0x000970F0
		public static ConstructionCall CreateConstructionCall(Type type, string activationUrl, object[] activationAttributes)
		{
			ConstructionCall constructionCall = new ConstructionCall(type);
			if (!type.IsContextful)
			{
				constructionCall.Activator = new AppDomainLevelActivator(activationUrl, ActivationServices.ConstructionActivator);
				constructionCall.IsContextOk = false;
				return constructionCall;
			}
			IActivator activator = ActivationServices.ConstructionActivator;
			activator = new ContextLevelActivator(activator);
			ArrayList arrayList = new ArrayList();
			if (activationAttributes != null)
			{
				arrayList.AddRange(activationAttributes);
			}
			bool flag = activationUrl == ChannelServices.CrossContextUrl;
			Context currentContext = Thread.CurrentContext;
			if (flag)
			{
				foreach (object obj in arrayList)
				{
					IContextAttribute contextAttribute = (IContextAttribute)obj;
					if (!contextAttribute.IsContextOK(currentContext, constructionCall))
					{
						flag = false;
						break;
					}
				}
			}
			object[] customAttributes = type.GetCustomAttributes(true);
			foreach (object obj2 in customAttributes)
			{
				if (obj2 is IContextAttribute)
				{
					flag = (flag && ((IContextAttribute)obj2).IsContextOK(currentContext, constructionCall));
					arrayList.Add(obj2);
				}
			}
			if (!flag)
			{
				constructionCall.SetActivationAttributes(arrayList.ToArray());
				foreach (object obj3 in arrayList)
				{
					IContextAttribute contextAttribute2 = (IContextAttribute)obj3;
					contextAttribute2.GetPropertiesForNewContext(constructionCall);
				}
			}
			if (activationUrl != ChannelServices.CrossContextUrl)
			{
				activator = new AppDomainLevelActivator(activationUrl, activator);
			}
			constructionCall.Activator = activator;
			constructionCall.IsContextOk = flag;
			return constructionCall;
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x000990CC File Offset: 0x000972CC
		public static IMessage CreateInstanceFromMessage(IConstructionCallMessage ctorCall)
		{
			object obj = ActivationServices.AllocateUninitializedClassInstance(ctorCall.ActivationType);
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(ctorCall);
			serverIdentity.AttachServerObject((MarshalByRefObject)obj, Thread.CurrentContext);
			ConstructionCall constructionCall = ctorCall as ConstructionCall;
			if (ctorCall.ActivationType.IsContextful && constructionCall != null && constructionCall.SourceProxy != null)
			{
				constructionCall.SourceProxy.AttachIdentity(serverIdentity);
				MarshalByRefObject target = (MarshalByRefObject)constructionCall.SourceProxy.GetTransparentProxy();
				RemotingServices.InternalExecuteMessage(target, ctorCall);
			}
			else
			{
				ctorCall.MethodBase.Invoke(obj, ctorCall.Args);
			}
			return new ConstructionResponse(obj, null, ctorCall);
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x00099170 File Offset: 0x00097370
		public static object CreateProxyForType(Type type)
		{
			ActivatedClientTypeEntry activatedClientTypeEntry = RemotingConfiguration.IsRemotelyActivatedClientType(type);
			if (activatedClientTypeEntry != null)
			{
				return RemotingServices.CreateClientProxy(activatedClientTypeEntry, null);
			}
			WellKnownClientTypeEntry wellKnownClientTypeEntry = RemotingConfiguration.IsWellKnownClientType(type);
			if (wellKnownClientTypeEntry != null)
			{
				return RemotingServices.CreateClientProxy(wellKnownClientTypeEntry);
			}
			if (type.IsContextful)
			{
				return RemotingServices.CreateClientProxyForContextBound(type, null);
			}
			if (type.IsCOMObject)
			{
				return RemotingServices.CreateClientProxyForComInterop(type);
			}
			return null;
		}

		// Token: 0x06002DE7 RID: 11751
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object AllocateUninitializedClassInstance(Type type);

		// Token: 0x06002DE8 RID: 11752
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EnableProxyActivation(Type type, bool enable);

		// Token: 0x040013B9 RID: 5049
		private static IActivator _constructionActivator;
	}
}
