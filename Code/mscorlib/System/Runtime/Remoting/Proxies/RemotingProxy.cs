using System;
using System.Reflection;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x020004E9 RID: 1257
	internal class RemotingProxy : RealProxy, IRemotingTypeInfo
	{
		// Token: 0x06003283 RID: 12931 RVA: 0x000A3D08 File Offset: 0x000A1F08
		internal RemotingProxy(Type type, ClientIdentity identity) : base(type, identity)
		{
			this._sink = identity.ChannelSink;
			this._hasEnvoySink = false;
			this._targetUri = identity.TargetUri;
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x000A3D34 File Offset: 0x000A1F34
		internal RemotingProxy(Type type, string activationUrl, object[] activationAttributes) : base(type)
		{
			this._hasEnvoySink = false;
			this._ctorCall = ActivationServices.CreateConstructionCall(type, activationUrl, activationAttributes);
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x000A3D94 File Offset: 0x000A1F94
		public override IMessage Invoke(IMessage request)
		{
			IMethodCallMessage methodCallMessage = request as IMethodCallMessage;
			if (methodCallMessage != null)
			{
				if (methodCallMessage.MethodBase == RemotingProxy._cache_GetHashCodeMethod)
				{
					return new MethodResponse(base.ObjectIdentity.GetHashCode(), null, null, methodCallMessage);
				}
				if (methodCallMessage.MethodBase == RemotingProxy._cache_GetTypeMethod)
				{
					return new MethodResponse(base.GetProxiedType(), null, null, methodCallMessage);
				}
			}
			IInternalMessage internalMessage = request as IInternalMessage;
			if (internalMessage != null)
			{
				if (internalMessage.Uri == null)
				{
					internalMessage.Uri = this._targetUri;
				}
				internalMessage.TargetIdentity = this._objectIdentity;
			}
			this._objectIdentity.NotifyClientDynamicSinks(true, request, true, false);
			IMessageSink messageSink;
			if (Thread.CurrentContext.HasExitSinks && !this._hasEnvoySink)
			{
				messageSink = Thread.CurrentContext.GetClientContextSinkChain();
			}
			else
			{
				messageSink = this._sink;
			}
			MonoMethodMessage monoMethodMessage = request as MonoMethodMessage;
			IMessage result;
			if (monoMethodMessage == null || monoMethodMessage.CallType == CallType.Sync)
			{
				result = messageSink.SyncProcessMessage(request);
			}
			else
			{
				AsyncResult asyncResult = monoMethodMessage.AsyncResult;
				IMessageCtrl messageCtrl = messageSink.AsyncProcessMessage(request, asyncResult);
				if (asyncResult != null)
				{
					asyncResult.SetMessageCtrl(messageCtrl);
				}
				result = new ReturnMessage(null, new object[0], 0, null, monoMethodMessage);
			}
			this._objectIdentity.NotifyClientDynamicSinks(false, request, true, false);
			return result;
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x000A3ED4 File Offset: 0x000A20D4
		internal void AttachIdentity(Identity identity)
		{
			this._objectIdentity = identity;
			if (identity is ClientActivatedIdentity)
			{
				ClientActivatedIdentity clientActivatedIdentity = (ClientActivatedIdentity)identity;
				this._targetContext = clientActivatedIdentity.Context;
				base.AttachServer(clientActivatedIdentity.GetServerObject());
				clientActivatedIdentity.SetClientProxy((MarshalByRefObject)this.GetTransparentProxy());
			}
			if (identity is ClientIdentity)
			{
				((ClientIdentity)identity).ClientProxy = (MarshalByRefObject)this.GetTransparentProxy();
				this._targetUri = ((ClientIdentity)identity).TargetUri;
			}
			else
			{
				this._targetUri = identity.ObjectUri;
			}
			if (this._objectIdentity.EnvoySink != null)
			{
				this._sink = this._objectIdentity.EnvoySink;
				this._hasEnvoySink = true;
			}
			else
			{
				this._sink = this._objectIdentity.ChannelSink;
			}
			this._ctorCall = null;
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000A3FAC File Offset: 0x000A21AC
		internal IMessage ActivateRemoteObject(IMethodMessage request)
		{
			if (this._ctorCall == null)
			{
				return new ConstructionResponse(this, null, (IMethodCallMessage)request);
			}
			this._ctorCall.CopyFrom(request);
			return ActivationServices.Activate(this, this._ctorCall);
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06003289 RID: 12937 RVA: 0x000A3FE0 File Offset: 0x000A21E0
		// (set) Token: 0x0600328A RID: 12938 RVA: 0x000A402C File Offset: 0x000A222C
		public string TypeName
		{
			get
			{
				if (this._objectIdentity is ClientIdentity)
				{
					ObjRef objRef = this._objectIdentity.CreateObjRef(null);
					if (objRef.TypeInfo != null)
					{
						return objRef.TypeInfo.TypeName;
					}
				}
				return base.GetProxiedType().AssemblyQualifiedName;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x000A4034 File Offset: 0x000A2234
		public bool CanCastTo(Type fromType, object o)
		{
			if (this._objectIdentity is ClientIdentity)
			{
				ObjRef objRef = this._objectIdentity.CreateObjRef(null);
				if (objRef.IsReferenceToWellKnow && (fromType.IsInterface || base.GetProxiedType() == typeof(MarshalByRefObject)))
				{
					return true;
				}
				if (objRef.TypeInfo != null)
				{
					return objRef.TypeInfo.CanCastTo(fromType, o);
				}
			}
			return fromType.IsAssignableFrom(base.GetProxiedType());
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x000A40B0 File Offset: 0x000A22B0
		~RemotingProxy()
		{
			if (this._objectIdentity != null && !(this._objectIdentity is ClientActivatedIdentity))
			{
				RemotingServices.DisposeIdentity(this._objectIdentity);
			}
		}

		// Token: 0x04001524 RID: 5412
		private static MethodInfo _cache_GetTypeMethod = typeof(object).GetMethod("GetType");

		// Token: 0x04001525 RID: 5413
		private static MethodInfo _cache_GetHashCodeMethod = typeof(object).GetMethod("GetHashCode");

		// Token: 0x04001526 RID: 5414
		private IMessageSink _sink;

		// Token: 0x04001527 RID: 5415
		private bool _hasEnvoySink;

		// Token: 0x04001528 RID: 5416
		private ConstructionCall _ctorCall;
	}
}
