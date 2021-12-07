using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Enforces a synchronization domain for the current context and all contexts that share the same instance.</summary>
	// Token: 0x02000480 RID: 1152
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class)]
	[Serializable]
	public class SynchronizationAttribute : ContextAttribute, IContributeClientContextSink, IContributeServerContextSink
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> class with default values.</summary>
		// Token: 0x06002F23 RID: 12067 RVA: 0x0009C250 File Offset: 0x0009A450
		public SynchronizationAttribute() : this(8, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> class with a Boolean value indicating whether reentry is required.</summary>
		/// <param name="reEntrant">A Boolean value indicating whether reentry is required. </param>
		// Token: 0x06002F24 RID: 12068 RVA: 0x0009C25C File Offset: 0x0009A45C
		public SynchronizationAttribute(bool reEntrant) : this(8, reEntrant)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> class with a flag indicating the behavior of the object to which this attribute is applied.</summary>
		/// <param name="flag">An integer value indicating the behavior of the object to which this attribute is applied. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter was not one of the defined flags. </exception>
		// Token: 0x06002F25 RID: 12069 RVA: 0x0009C268 File Offset: 0x0009A468
		public SynchronizationAttribute(int flag) : this(flag, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> class with a flag indicating the behavior of the object to which this attribute is applied, and a Boolean value indicating whether reentry is required.</summary>
		/// <param name="flag">An integer value indicating the behavior of the object to which this attribute is applied. </param>
		/// <param name="reEntrant">true if reentry is required, and callouts must be intercepted and serialized; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter was not one of the defined flags. </exception>
		// Token: 0x06002F26 RID: 12070 RVA: 0x0009C274 File Offset: 0x0009A474
		public SynchronizationAttribute(int flag, bool reEntrant) : base("Synchronization")
		{
			if (flag != 1 && flag != 4 && flag != 8 && flag != 2)
			{
				throw new ArgumentException("flag");
			}
			this._bReEntrant = reEntrant;
			this._flavor = flag;
		}

		/// <summary>Gets or sets a Boolean value indicating whether reentry is required.</summary>
		/// <returns>A Boolean value indicating whether reentry is required.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06002F27 RID: 12071 RVA: 0x0009C2D0 File Offset: 0x0009A4D0
		public virtual bool IsReEntrant
		{
			get
			{
				return this._bReEntrant;
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether the <see cref="T:System.Runtime.Remoting.Contexts.Context" /> implementing this instance of <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> is locked.</summary>
		/// <returns>A Boolean value indicating whether the <see cref="T:System.Runtime.Remoting.Contexts.Context" /> implementing this instance of <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> is locked.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06002F28 RID: 12072 RVA: 0x0009C2D8 File Offset: 0x0009A4D8
		// (set) Token: 0x06002F29 RID: 12073 RVA: 0x0009C2E0 File Offset: 0x0009A4E0
		public virtual bool Locked
		{
			get
			{
				return this._locked;
			}
			set
			{
				if (value)
				{
					this._mutex.WaitOne();
					lock (this)
					{
						this._lockCount++;
						if (this._lockCount > 1)
						{
							this.ReleaseLock();
						}
						this._ownerThread = Thread.CurrentThread;
					}
				}
				else
				{
					lock (this)
					{
						while (this._lockCount > 0 && this._ownerThread == Thread.CurrentThread)
						{
							this._lockCount--;
							this._mutex.ReleaseMutex();
							this._ownerThread = null;
						}
					}
				}
			}
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x0009C3CC File Offset: 0x0009A5CC
		internal void AcquireLock()
		{
			this._mutex.WaitOne();
			lock (this)
			{
				this._ownerThread = Thread.CurrentThread;
				this._lockCount++;
			}
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x0009C430 File Offset: 0x0009A630
		internal void ReleaseLock()
		{
			lock (this)
			{
				if (this._lockCount > 0 && this._ownerThread == Thread.CurrentThread)
				{
					this._lockCount--;
					this._mutex.ReleaseMutex();
					this._ownerThread = null;
				}
			}
		}

		/// <summary>Adds the Synchronized context property to the specified <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />.</summary>
		/// <param name="ctorMsg">The <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> to which to add the property. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002F2C RID: 12076 RVA: 0x0009C4AC File Offset: 0x0009A6AC
		[ComVisible(true)]
		public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			if (this._flavor != 1)
			{
				ctorMsg.ContextProperties.Add(this);
			}
		}

		/// <summary>Creates a CallOut sink and chains it in front of the provided chain of sinks at the context boundary on the client end of a remoting call.</summary>
		/// <returns>The composite sink chain with the new CallOut sink.</returns>
		/// <param name="nextSink">The chain of sinks composed so far. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002F2D RID: 12077 RVA: 0x0009C4C8 File Offset: 0x0009A6C8
		public virtual IMessageSink GetClientContextSink(IMessageSink nextSink)
		{
			return new SynchronizedClientContextSink(nextSink, this);
		}

		/// <summary>Creates a synchronized dispatch sink and chains it in front of the provided chain of sinks at the context boundary on the server end of a remoting call.</summary>
		/// <returns>The composite sink chain with the new synchronized dispatch sink.</returns>
		/// <param name="nextSink">The chain of sinks composed so far. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002F2E RID: 12078 RVA: 0x0009C4D4 File Offset: 0x0009A6D4
		public virtual IMessageSink GetServerContextSink(IMessageSink nextSink)
		{
			return new SynchronizedServerContextSink(nextSink, this);
		}

		/// <summary>Returns a Boolean value indicating whether the context parameter meets the context attribute's requirements.</summary>
		/// <returns>true if the passed in context is OK; otherwise, false.</returns>
		/// <param name="ctx">The context to check. </param>
		/// <param name="msg">Information gathered at construction time of the context bound object marked by this attribute. The <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> can inspect, add to, and remove properties from the context while determining if the context is acceptable to it. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="ctx" /> or <paramref name="msg" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002F2F RID: 12079 RVA: 0x0009C4E0 File Offset: 0x0009A6E0
		[ComVisible(true)]
		public override bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			SynchronizationAttribute synchronizationAttribute = ctx.GetProperty("Synchronization") as SynchronizationAttribute;
			switch (this._flavor)
			{
			case 1:
				return synchronizationAttribute == null;
			case 2:
				return true;
			case 4:
				return synchronizationAttribute != null;
			case 8:
				return false;
			}
			return false;
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x0009C544 File Offset: 0x0009A744
		internal static void ExitContext()
		{
			if (Thread.CurrentContext.IsDefaultContext)
			{
				return;
			}
			SynchronizationAttribute synchronizationAttribute = Thread.CurrentContext.GetProperty("Synchronization") as SynchronizationAttribute;
			if (synchronizationAttribute == null)
			{
				return;
			}
			synchronizationAttribute.Locked = false;
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x0009C584 File Offset: 0x0009A784
		internal static void EnterContext()
		{
			if (Thread.CurrentContext.IsDefaultContext)
			{
				return;
			}
			SynchronizationAttribute synchronizationAttribute = Thread.CurrentContext.GetProperty("Synchronization") as SynchronizationAttribute;
			if (synchronizationAttribute == null)
			{
				return;
			}
			synchronizationAttribute.Locked = true;
		}

		/// <summary>Indicates that the class to which this attribute is applied cannot be created in a context that has synchronization. This field is constant.</summary>
		// Token: 0x04001409 RID: 5129
		public const int NOT_SUPPORTED = 1;

		/// <summary>Indicates that the class to which this attribute is applied is not dependent on whether the context has synchronization. This field is constant.</summary>
		// Token: 0x0400140A RID: 5130
		public const int SUPPORTED = 2;

		/// <summary>Indicates that the class to which this attribute is applied must be created in a context that has synchronization. This field is constant.</summary>
		// Token: 0x0400140B RID: 5131
		public const int REQUIRED = 4;

		/// <summary>Indicates that the class to which this attribute is applied must be created in a context with a new instance of the synchronization property each time. This field is constant.</summary>
		// Token: 0x0400140C RID: 5132
		public const int REQUIRES_NEW = 8;

		// Token: 0x0400140D RID: 5133
		private bool _bReEntrant;

		// Token: 0x0400140E RID: 5134
		private int _flavor;

		// Token: 0x0400140F RID: 5135
		[NonSerialized]
		private bool _locked;

		// Token: 0x04001410 RID: 5136
		[NonSerialized]
		private int _lockCount;

		// Token: 0x04001411 RID: 5137
		[NonSerialized]
		private Mutex _mutex = new Mutex(false);

		// Token: 0x04001412 RID: 5138
		[NonSerialized]
		private Thread _ownerThread;
	}
}
