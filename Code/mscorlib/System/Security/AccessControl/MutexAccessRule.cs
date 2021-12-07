using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a set of access rights allowed or denied for a user or group. This class cannot be inherited.</summary>
	// Token: 0x0200057E RID: 1406
	public sealed class MutexAccessRule : AccessRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.MutexAccessRule" /> class, specifying the user or group the rule applies to, the access rights, and whether the specified access rights are allowed or denied.</summary>
		/// <param name="identity">The user or group the rule applies to. Must be of type <see cref="T:System.Security.Principal.SecurityIdentifier" /> or a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="eventRights">A bitwise combination of <see cref="T:System.Security.AccessControl.MutexRights" /> values specifying the rights allowed or denied.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values specifying whether the rights are allowed or denied.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="eventRights" /> specifies an invalid value.-or-<paramref name="type" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null. -or-<paramref name="eventRights" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identity" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" /> nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x06003686 RID: 13958 RVA: 0x000B21AC File Offset: 0x000B03AC
		public MutexAccessRule(IdentityReference identity, MutexRights eventRights, AccessControlType type) : base(identity, 0, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
			this.rights = eventRights;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.MutexAccessRule" /> class, specifying the name of the user or group the rule applies to, the access rights, and whether the specified access rights are allowed or denied.</summary>
		/// <param name="identity">The name of the user or group the rule applies to.</param>
		/// <param name="eventRights">A bitwise combination of <see cref="T:System.Security.AccessControl.MutexRights" /> values specifying the rights allowed or denied.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values specifying whether the rights are allowed or denied.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="eventRights" /> specifies an invalid value.-or-<paramref name="type" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="eventRights" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identity" /> is null.-or-<paramref name="identity" /> is a zero-length string.-or-<paramref name="identity" /> is longer than 512 characters.</exception>
		// Token: 0x06003687 RID: 13959 RVA: 0x000B21C4 File Offset: 0x000B03C4
		public MutexAccessRule(string identity, MutexRights eventRights, AccessControlType type) : this(new SecurityIdentifier(identity), eventRights, type)
		{
		}

		/// <summary>Gets the rights allowed or denied by the access rule.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Security.AccessControl.MutexRights" /> values indicating the rights allowed or denied by the access rule.</returns>
		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06003688 RID: 13960 RVA: 0x000B21D4 File Offset: 0x000B03D4
		public MutexRights MutexRights
		{
			get
			{
				return this.rights;
			}
		}

		// Token: 0x04001726 RID: 5926
		private MutexRights rights;
	}
}
