using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	/// <summary>Defines a lifetime lease object that is used by the remoting lifetime service.</summary>
	// Token: 0x02000485 RID: 1157
	[ComVisible(true)]
	public interface ILease
	{
		/// <summary>Gets the amount of time remaining on the lease.</summary>
		/// <returns>The amount of time remaining on the lease.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06002F48 RID: 12104
		TimeSpan CurrentLeaseTime { get; }

		/// <summary>Gets the current <see cref="T:System.Runtime.Remoting.Lifetime.LeaseState" /> of the lease.</summary>
		/// <returns>The current <see cref="T:System.Runtime.Remoting.Lifetime.LeaseState" /> of the lease.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06002F49 RID: 12105
		LeaseState CurrentState { get; }

		/// <summary>Gets or sets the initial time for the lease.</summary>
		/// <returns>The initial time for the lease.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06002F4A RID: 12106
		// (set) Token: 0x06002F4B RID: 12107
		TimeSpan InitialLeaseTime { get; set; }

		/// <summary>Gets or sets the amount of time by which a call to the remote object renews the <see cref="P:System.Runtime.Remoting.Lifetime.ILease.CurrentLeaseTime" />.</summary>
		/// <returns>The amount of time by which a call to the remote object renews the <see cref="P:System.Runtime.Remoting.Lifetime.ILease.CurrentLeaseTime" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06002F4C RID: 12108
		// (set) Token: 0x06002F4D RID: 12109
		TimeSpan RenewOnCallTime { get; set; }

		/// <summary>Gets or sets the amount of time to wait for a sponsor to return with a lease renewal time.</summary>
		/// <returns>The amount of time to wait for a sponsor to return with a lease renewal time.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06002F4E RID: 12110
		// (set) Token: 0x06002F4F RID: 12111
		TimeSpan SponsorshipTimeout { get; set; }

		/// <summary>Registers a sponsor for the lease without renewing the lease.</summary>
		/// <param name="obj">The callback object of the sponsor. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002F50 RID: 12112
		void Register(ISponsor obj);

		/// <summary>Registers a sponsor for the lease, and renews it by the specified <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="obj">The callback object of the sponsor. </param>
		/// <param name="renewalTime">The length of time to renew the lease by. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		// Token: 0x06002F51 RID: 12113
		void Register(ISponsor obj, TimeSpan renewalTime);

		/// <summary>Renews a lease for the specified time.</summary>
		/// <returns>The new expiration time of the lease.</returns>
		/// <param name="renewalTime">The length of time to renew the lease by. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002F52 RID: 12114
		TimeSpan Renew(TimeSpan renewalTime);

		/// <summary>Removes a sponsor from the sponsor list.</summary>
		/// <param name="obj">The lease sponsor to unregister. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002F53 RID: 12115
		void Unregister(ISponsor obj);
	}
}
