using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Defines an environment for the objects that are resident inside it and for which a policy can be enforced.</summary>
	// Token: 0x0200046E RID: 1134
	[ComVisible(true)]
	public class Context
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.Context" /> class.</summary>
		// Token: 0x06002ED4 RID: 11988 RVA: 0x0009B148 File Offset: 0x00099348
		public Context()
		{
			this.domain_id = Thread.GetDomainID();
			this.context_id = 1 + Context.global_count++;
		}

		/// <summary>Cleans up the backing objects for the nondefault contexts.</summary>
		// Token: 0x06002ED6 RID: 11990 RVA: 0x0009B188 File Offset: 0x00099388
		~Context()
		{
		}

		/// <summary>Gets the default context for the current application domain.</summary>
		/// <returns>The default context for the <see cref="T:System.AppDomain" /> namespace.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06002ED7 RID: 11991 RVA: 0x0009B1C0 File Offset: 0x000993C0
		public static Context DefaultContext
		{
			get
			{
				return AppDomain.InternalGetDefaultContext();
			}
		}

		/// <summary>Gets the context ID for the current context.</summary>
		/// <returns>The context ID for the current context.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06002ED8 RID: 11992 RVA: 0x0009B1C8 File Offset: 0x000993C8
		public virtual int ContextID
		{
			get
			{
				return this.context_id;
			}
		}

		/// <summary>Gets the array of the current context properties.</summary>
		/// <returns>The current context properties array; otherwise, null if the context does not have any properties attributed to it.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06002ED9 RID: 11993 RVA: 0x0009B1D0 File Offset: 0x000993D0
		public virtual IContextProperty[] ContextProperties
		{
			get
			{
				if (this.context_properties == null)
				{
					return new IContextProperty[0];
				}
				return (IContextProperty[])this.context_properties.ToArray(typeof(IContextProperty[]));
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002EDA RID: 11994 RVA: 0x0009B20C File Offset: 0x0009940C
		internal bool IsDefaultContext
		{
			get
			{
				return this.context_id == 0;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06002EDB RID: 11995 RVA: 0x0009B218 File Offset: 0x00099418
		internal bool NeedsContextSink
		{
			get
			{
				return this.context_id != 0 || (Context.global_dynamic_properties != null && Context.global_dynamic_properties.HasProperties) || (this.context_dynamic_properties != null && this.context_dynamic_properties.HasProperties);
			}
		}

		/// <summary>Registers a dynamic property implementing the <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" /> interface with the remoting service.</summary>
		/// <returns>true if the property was successfully registered; otherwise, false.</returns>
		/// <param name="prop">The dynamic property to register. </param>
		/// <param name="obj">The object/proxy for which the <paramref name="property" /> is registered. </param>
		/// <param name="ctx">The context for which the <paramref name="property" /> is registered. </param>
		/// <exception cref="T:System.ArgumentNullException">Either <paramref name="prop" /> or its name is null, or it is not dynamic (it does not implement <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" />). </exception>
		/// <exception cref="T:System.ArgumentException">Both an object as well as a context are specified (both <paramref name="obj" /> and <paramref name="ctx" /> are not null). </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002EDC RID: 11996 RVA: 0x0009B268 File Offset: 0x00099468
		public static bool RegisterDynamicProperty(IDynamicProperty prop, ContextBoundObject obj, Context ctx)
		{
			DynamicPropertyCollection dynamicPropertyCollection = Context.GetDynamicPropertyCollection(obj, ctx);
			return dynamicPropertyCollection.RegisterDynamicProperty(prop);
		}

		/// <summary>Unregisters a dynamic property implementing the <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" /> interface.</summary>
		/// <returns>true if the object was successfully unregistered; otherwise, false.</returns>
		/// <param name="name">The name of the dynamic property to unregister. </param>
		/// <param name="obj">The object/proxy for which the <paramref name="property" /> is registered. </param>
		/// <param name="ctx">The context for which the <paramref name="property" /> is registered. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">Both an object as well as a context are specified (both <paramref name="obj" /> and <paramref name="ctx" /> are not null). </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002EDD RID: 11997 RVA: 0x0009B284 File Offset: 0x00099484
		public static bool UnregisterDynamicProperty(string name, ContextBoundObject obj, Context ctx)
		{
			DynamicPropertyCollection dynamicPropertyCollection = Context.GetDynamicPropertyCollection(obj, ctx);
			return dynamicPropertyCollection.UnregisterDynamicProperty(name);
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x0009B2A0 File Offset: 0x000994A0
		private static DynamicPropertyCollection GetDynamicPropertyCollection(ContextBoundObject obj, Context ctx)
		{
			if (ctx == null && obj != null)
			{
				if (RemotingServices.IsTransparentProxy(obj))
				{
					RealProxy realProxy = RemotingServices.GetRealProxy(obj);
					return realProxy.ObjectIdentity.ClientDynamicProperties;
				}
				return obj.ObjectIdentity.ServerDynamicProperties;
			}
			else
			{
				if (ctx != null && obj == null)
				{
					if (ctx.context_dynamic_properties == null)
					{
						ctx.context_dynamic_properties = new DynamicPropertyCollection();
					}
					return ctx.context_dynamic_properties;
				}
				if (ctx == null && obj == null)
				{
					if (Context.global_dynamic_properties == null)
					{
						Context.global_dynamic_properties = new DynamicPropertyCollection();
					}
					return Context.global_dynamic_properties;
				}
				throw new ArgumentException("Either obj or ctx must be null");
			}
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x0009B33C File Offset: 0x0009953C
		internal static void NotifyGlobalDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (Context.global_dynamic_properties != null && Context.global_dynamic_properties.HasProperties)
			{
				Context.global_dynamic_properties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06002EE0 RID: 12000 RVA: 0x0009B368 File Offset: 0x00099568
		internal static bool HasGlobalDynamicSinks
		{
			get
			{
				return Context.global_dynamic_properties != null && Context.global_dynamic_properties.HasProperties;
			}
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x0009B384 File Offset: 0x00099584
		internal void NotifyDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (this.context_dynamic_properties != null && this.context_dynamic_properties.HasProperties)
			{
				this.context_dynamic_properties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06002EE2 RID: 12002 RVA: 0x0009B3B4 File Offset: 0x000995B4
		internal bool HasDynamicSinks
		{
			get
			{
				return this.context_dynamic_properties != null && this.context_dynamic_properties.HasProperties;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06002EE3 RID: 12003 RVA: 0x0009B3D0 File Offset: 0x000995D0
		internal bool HasExitSinks
		{
			get
			{
				return !(this.GetClientContextSinkChain() is ClientContextTerminatorSink) || this.HasDynamicSinks || Context.HasGlobalDynamicSinks;
			}
		}

		/// <summary>Returns a specific context property, specified by name.</summary>
		/// <returns>The specified context property.</returns>
		/// <param name="name">The name of the property. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002EE4 RID: 12004 RVA: 0x0009B3F8 File Offset: 0x000995F8
		public virtual IContextProperty GetProperty(string name)
		{
			if (this.context_properties == null)
			{
				return null;
			}
			foreach (object obj in this.context_properties)
			{
				IContextProperty contextProperty = (IContextProperty)obj;
				if (contextProperty.Name == name)
				{
					return contextProperty;
				}
			}
			return null;
		}

		/// <summary>Sets a specific context property by name.</summary>
		/// <param name="prop">The actual context property. </param>
		/// <exception cref="T:System.InvalidOperationException">There is an attempt to add properties to the default context. </exception>
		/// <exception cref="T:System.InvalidOperationException">The context is frozen. </exception>
		/// <exception cref="T:System.ArgumentNullException">The property or the property name is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002EE5 RID: 12005 RVA: 0x0009B488 File Offset: 0x00099688
		public virtual void SetProperty(IContextProperty prop)
		{
			if (prop == null)
			{
				throw new ArgumentNullException("IContextProperty");
			}
			if (this == Context.DefaultContext)
			{
				throw new InvalidOperationException("Can not add properties to default context");
			}
			if (this.frozen)
			{
				throw new InvalidOperationException("Context is Frozen");
			}
			if (this.context_properties == null)
			{
				this.context_properties = new ArrayList();
			}
			this.context_properties.Add(prop);
		}

		/// <summary>Freezes the context, making it impossible to add or remove context properties from the current context.</summary>
		/// <exception cref="T:System.InvalidOperationException">The context is already frozen. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002EE6 RID: 12006 RVA: 0x0009B4F8 File Offset: 0x000996F8
		public virtual void Freeze()
		{
			if (this.context_properties != null)
			{
				foreach (object obj in this.context_properties)
				{
					IContextProperty contextProperty = (IContextProperty)obj;
					contextProperty.Freeze(this);
				}
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> class representation of the current context.</summary>
		/// <returns>A <see cref="T:System.String" /> class representation of the current context.</returns>
		// Token: 0x06002EE7 RID: 12007 RVA: 0x0009B574 File Offset: 0x00099774
		public override string ToString()
		{
			return "ContextID: " + this.context_id;
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x0009B58C File Offset: 0x0009978C
		internal IMessageSink GetServerContextSinkChain()
		{
			if (this.server_context_sink_chain == null)
			{
				if (Context.default_server_context_sink == null)
				{
					Context.default_server_context_sink = new ServerContextTerminatorSink();
				}
				this.server_context_sink_chain = Context.default_server_context_sink;
				if (this.context_properties != null)
				{
					for (int i = this.context_properties.Count - 1; i >= 0; i--)
					{
						IContributeServerContextSink contributeServerContextSink = this.context_properties[i] as IContributeServerContextSink;
						if (contributeServerContextSink != null)
						{
							this.server_context_sink_chain = contributeServerContextSink.GetServerContextSink(this.server_context_sink_chain);
						}
					}
				}
			}
			return this.server_context_sink_chain;
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x0009B61C File Offset: 0x0009981C
		internal IMessageSink GetClientContextSinkChain()
		{
			if (this.client_context_sink_chain == null)
			{
				this.client_context_sink_chain = new ClientContextTerminatorSink(this);
				if (this.context_properties != null)
				{
					foreach (object obj in this.context_properties)
					{
						IContextProperty contextProperty = (IContextProperty)obj;
						IContributeClientContextSink contributeClientContextSink = contextProperty as IContributeClientContextSink;
						if (contributeClientContextSink != null)
						{
							this.client_context_sink_chain = contributeClientContextSink.GetClientContextSink(this.client_context_sink_chain);
						}
					}
				}
			}
			return this.client_context_sink_chain;
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x0009B6CC File Offset: 0x000998CC
		internal IMessageSink CreateServerObjectSinkChain(MarshalByRefObject obj, bool forceInternalExecute)
		{
			IMessageSink messageSink = new StackBuilderSink(obj, forceInternalExecute);
			messageSink = new ServerObjectTerminatorSink(messageSink);
			messageSink = new LeaseSink(messageSink);
			if (this.context_properties != null)
			{
				for (int i = this.context_properties.Count - 1; i >= 0; i--)
				{
					IContextProperty contextProperty = (IContextProperty)this.context_properties[i];
					IContributeObjectSink contributeObjectSink = contextProperty as IContributeObjectSink;
					if (contributeObjectSink != null)
					{
						messageSink = contributeObjectSink.GetObjectSink(obj, messageSink);
					}
				}
			}
			return messageSink;
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x0009B744 File Offset: 0x00099944
		internal IMessageSink CreateEnvoySink(MarshalByRefObject serverObject)
		{
			IMessageSink messageSink = EnvoyTerminatorSink.Instance;
			if (this.context_properties != null)
			{
				foreach (object obj in this.context_properties)
				{
					IContextProperty contextProperty = (IContextProperty)obj;
					IContributeEnvoySink contributeEnvoySink = contextProperty as IContributeEnvoySink;
					if (contributeEnvoySink != null)
					{
						messageSink = contributeEnvoySink.GetEnvoySink(serverObject, messageSink);
					}
				}
			}
			return messageSink;
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x0009B7D8 File Offset: 0x000999D8
		internal static Context SwitchToContext(Context newContext)
		{
			return AppDomain.InternalSetContext(newContext);
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x0009B7E0 File Offset: 0x000999E0
		internal static Context CreateNewContext(IConstructionCallMessage msg)
		{
			Context context = new Context();
			foreach (object obj in msg.ContextProperties)
			{
				IContextProperty contextProperty = (IContextProperty)obj;
				if (context.GetProperty(contextProperty.Name) == null)
				{
					context.SetProperty(contextProperty);
				}
			}
			context.Freeze();
			foreach (object obj2 in msg.ContextProperties)
			{
				IContextProperty contextProperty2 = (IContextProperty)obj2;
				if (!contextProperty2.IsNewContextOK(context))
				{
					throw new RemotingException("A context property did not approve the candidate context for activating the object");
				}
			}
			return context;
		}

		/// <summary>Executes code in another context.</summary>
		/// <param name="deleg">The delegate used to request the callback. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002EEE RID: 12014 RVA: 0x0009B8E8 File Offset: 0x00099AE8
		public void DoCallBack(CrossContextDelegate deleg)
		{
			lock (this)
			{
				if (this.callback_object == null)
				{
					Context newContext = Context.SwitchToContext(this);
					this.callback_object = new ContextCallbackObject();
					Context.SwitchToContext(newContext);
				}
			}
			this.callback_object.DoCallBack(deleg);
		}

		/// <summary>Allocates an unnamed data slot.</summary>
		/// <returns>A local data slot.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002EEF RID: 12015 RVA: 0x0009B958 File Offset: 0x00099B58
		public static LocalDataStoreSlot AllocateDataSlot()
		{
			return new LocalDataStoreSlot(false);
		}

		/// <summary>Allocates a named data slot.</summary>
		/// <returns>A local data slot object.</returns>
		/// <param name="name">The required name for the data slot. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002EF0 RID: 12016 RVA: 0x0009B960 File Offset: 0x00099B60
		public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
		{
			object syncRoot = Context.namedSlots.SyncRoot;
			LocalDataStoreSlot result;
			lock (syncRoot)
			{
				LocalDataStoreSlot localDataStoreSlot = Context.AllocateDataSlot();
				Context.namedSlots.Add(name, localDataStoreSlot);
				result = localDataStoreSlot;
			}
			return result;
		}

		/// <summary>Frees a named data slot on all the contexts.</summary>
		/// <param name="name">The name of the data slot to free. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002EF1 RID: 12017 RVA: 0x0009B9C0 File Offset: 0x00099BC0
		public static void FreeNamedDataSlot(string name)
		{
			object syncRoot = Context.namedSlots.SyncRoot;
			lock (syncRoot)
			{
				Context.namedSlots.Remove(name);
			}
		}

		/// <summary>Retrieves the value from the specified slot on the current context.</summary>
		/// <returns>Returns the data associated with <paramref name="slot" />. </returns>
		/// <param name="slot">The data slot that contains the data. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002EF2 RID: 12018 RVA: 0x0009BA14 File Offset: 0x00099C14
		public static object GetData(LocalDataStoreSlot slot)
		{
			Context currentContext = Thread.CurrentContext;
			Context obj = currentContext;
			object result;
			lock (obj)
			{
				if (currentContext.datastore != null && slot.slot < currentContext.datastore.Length)
				{
					result = currentContext.datastore[slot.slot];
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		/// <summary>Looks up a named data slot.</summary>
		/// <returns>Returns a local data slot.</returns>
		/// <param name="name">The data slot name. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002EF3 RID: 12019 RVA: 0x0009BA90 File Offset: 0x00099C90
		public static LocalDataStoreSlot GetNamedDataSlot(string name)
		{
			object syncRoot = Context.namedSlots.SyncRoot;
			LocalDataStoreSlot result;
			lock (syncRoot)
			{
				LocalDataStoreSlot localDataStoreSlot = Context.namedSlots[name] as LocalDataStoreSlot;
				if (localDataStoreSlot == null)
				{
					result = Context.AllocateNamedDataSlot(name);
				}
				else
				{
					result = localDataStoreSlot;
				}
			}
			return result;
		}

		/// <summary>Sets the data in the specified slot on the current context.</summary>
		/// <param name="slot">The data slot where the data is to be added. </param>
		/// <param name="data">The data that is to be added. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002EF4 RID: 12020 RVA: 0x0009BB04 File Offset: 0x00099D04
		public static void SetData(LocalDataStoreSlot slot, object data)
		{
			Context currentContext = Thread.CurrentContext;
			Context obj = currentContext;
			lock (obj)
			{
				if (currentContext.datastore == null)
				{
					currentContext.datastore = new object[slot.slot + 2];
				}
				else if (slot.slot >= currentContext.datastore.Length)
				{
					object[] array = new object[slot.slot + 2];
					currentContext.datastore.CopyTo(array, 0);
					currentContext.datastore = array;
				}
				currentContext.datastore[slot.slot] = data;
			}
		}

		// Token: 0x040013F2 RID: 5106
		private int domain_id;

		// Token: 0x040013F3 RID: 5107
		private int context_id;

		// Token: 0x040013F4 RID: 5108
		private UIntPtr static_data;

		// Token: 0x040013F5 RID: 5109
		private static IMessageSink default_server_context_sink;

		// Token: 0x040013F6 RID: 5110
		private IMessageSink server_context_sink_chain;

		// Token: 0x040013F7 RID: 5111
		private IMessageSink client_context_sink_chain;

		// Token: 0x040013F8 RID: 5112
		private object[] datastore;

		// Token: 0x040013F9 RID: 5113
		private ArrayList context_properties;

		// Token: 0x040013FA RID: 5114
		private bool frozen;

		// Token: 0x040013FB RID: 5115
		private static int global_count;

		// Token: 0x040013FC RID: 5116
		private static Hashtable namedSlots = new Hashtable();

		// Token: 0x040013FD RID: 5117
		private static DynamicPropertyCollection global_dynamic_properties;

		// Token: 0x040013FE RID: 5118
		private DynamicPropertyCollection context_dynamic_properties;

		// Token: 0x040013FF RID: 5119
		private ContextCallbackObject callback_object;
	}
}
