using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	/// <summary>Provides a default implementation for a lifetime sponsor class.</summary>
	// Token: 0x02000484 RID: 1156
	[ComVisible(true)]
	public class ClientSponsor : MarshalByRefObject, ISponsor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> class with default values.</summary>
		// Token: 0x06002F3E RID: 12094 RVA: 0x0009C7F4 File Offset: 0x0009A9F4
		public ClientSponsor()
		{
			this.renewal_time = new TimeSpan(0, 2, 0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> class with the renewal time of the sponsored object.</summary>
		/// <param name="renewalTime">The <see cref="T:System.TimeSpan" /> by which to increase the lifetime of the sponsored objects when renewal is requested. </param>
		// Token: 0x06002F3F RID: 12095 RVA: 0x0009C818 File Offset: 0x0009AA18
		public ClientSponsor(TimeSpan renewalTime)
		{
			this.renewal_time = renewalTime;
		}

		/// <summary>Gets or sets the <see cref="T:System.TimeSpan" /> by which to increase the lifetime of the sponsored objects when renewal is requested.</summary>
		/// <returns>The <see cref="T:System.TimeSpan" /> by which to increase the lifetime of the sponsored objects when renewal is requested.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06002F40 RID: 12096 RVA: 0x0009C834 File Offset: 0x0009AA34
		// (set) Token: 0x06002F41 RID: 12097 RVA: 0x0009C83C File Offset: 0x0009AA3C
		public TimeSpan RenewalTime
		{
			get
			{
				return this.renewal_time;
			}
			set
			{
				this.renewal_time = value;
			}
		}

		/// <summary>Empties the list objects registered with the current <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002F42 RID: 12098 RVA: 0x0009C848 File Offset: 0x0009AA48
		public void Close()
		{
			foreach (object obj in this.registered_objects.Values)
			{
				MarshalByRefObject marshalByRefObject = (MarshalByRefObject)obj;
				ILease lease = marshalByRefObject.GetLifetimeService() as ILease;
				lease.Unregister(this);
			}
			this.registered_objects.Clear();
		}

		/// <summary>Frees the resources of the current <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> before the garbage collector reclaims them.</summary>
		// Token: 0x06002F43 RID: 12099 RVA: 0x0009C8D4 File Offset: 0x0009AAD4
		~ClientSponsor()
		{
			this.Close();
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />, providing a lease for the current object.</summary>
		/// <returns>An <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> for the current object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002F44 RID: 12100 RVA: 0x0009C910 File Offset: 0x0009AB10
		public override object InitializeLifetimeService()
		{
			return base.InitializeLifetimeService();
		}

		/// <summary>Registers the specified <see cref="T:System.MarshalByRefObject" /> for sponsorship.</summary>
		/// <returns>true if registration succeeded; otherwise, false.</returns>
		/// <param name="obj">The object to register for sponsorship with the <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002F45 RID: 12101 RVA: 0x0009C918 File Offset: 0x0009AB18
		public bool Register(MarshalByRefObject obj)
		{
			if (this.registered_objects.ContainsKey(obj))
			{
				return false;
			}
			ILease lease = obj.GetLifetimeService() as ILease;
			if (lease == null)
			{
				return false;
			}
			lease.Register(this);
			this.registered_objects.Add(obj, obj);
			return true;
		}

		/// <summary>Requests a sponsoring client to renew the lease for the specified object.</summary>
		/// <returns>The additional lease time for the specified object.</returns>
		/// <param name="lease">The lifetime lease of the object that requires lease renewal. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002F46 RID: 12102 RVA: 0x0009C964 File Offset: 0x0009AB64
		public TimeSpan Renewal(ILease lease)
		{
			return this.renewal_time;
		}

		/// <summary>Unregisters the specified <see cref="T:System.MarshalByRefObject" /> from the list of objects sponsored by the current <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />.</summary>
		/// <param name="obj">The object to unregister. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002F47 RID: 12103 RVA: 0x0009C96C File Offset: 0x0009AB6C
		public void Unregister(MarshalByRefObject obj)
		{
			if (!this.registered_objects.ContainsKey(obj))
			{
				return;
			}
			ILease lease = obj.GetLifetimeService() as ILease;
			lease.Unregister(this);
			this.registered_objects.Remove(obj);
		}

		// Token: 0x0400141A RID: 5146
		private TimeSpan renewal_time;

		// Token: 0x0400141B RID: 5147
		private Hashtable registered_objects = new Hashtable();
	}
}
