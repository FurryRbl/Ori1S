using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a set of access rights to be audited for a user or group. This class cannot be inherited. </summary>
	// Token: 0x02000571 RID: 1393
	public sealed class EventWaitHandleAuditRule : AuditRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.EventWaitHandleAuditRule" /> class, specifying the user or group to audit, the rights to audit, and whether to audit success, failure, or both.</summary>
		/// <param name="identity">The user or group the rule applies to. Must be of type <see cref="T:System.Security.Principal.SecurityIdentifier" /> or a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="eventRights">A bitwise combination of <see cref="T:System.Security.AccessControl.EventWaitHandleRights" /> values specifying the kinds of access to audit.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Security.AccessControl.AuditFlags" /> values specifying whether to audit success, failure, or both.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="eventRights" /> specifies an invalid value.-or-<paramref name="flags" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null. -or-<paramref name="eventRights" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flags" /> is <see cref="F:System.Security.AccessControl.AuditFlags.None" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identity" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" /> nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x06003626 RID: 13862 RVA: 0x000B1CE0 File Offset: 0x000AFEE0
		public EventWaitHandleAuditRule(IdentityReference identity, EventWaitHandleRights eventRights, AuditFlags flags) : base(identity, 0, false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
			if (eventRights < EventWaitHandleRights.Modify || eventRights > EventWaitHandleRights.FullControl)
			{
				throw new ArgumentOutOfRangeException("eventRights");
			}
			if (flags < AuditFlags.None || flags > AuditFlags.Failure)
			{
				throw new ArgumentOutOfRangeException("flags");
			}
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (eventRights == (EventWaitHandleRights)0)
			{
				throw new ArgumentNullException("eventRights");
			}
			if (flags == AuditFlags.None)
			{
				throw new ArgumentException("flags");
			}
			if (!(identity is SecurityIdentifier))
			{
				throw new ArgumentException("identity");
			}
			this.rights = eventRights;
		}

		/// <summary>Gets the access rights affected by the audit rule.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Security.AccessControl.EventWaitHandleRights" /> values that indicates the rights affected by the audit rule.</returns>
		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06003627 RID: 13863 RVA: 0x000B1D88 File Offset: 0x000AFF88
		public EventWaitHandleRights EventWaitHandleRights
		{
			get
			{
				return this.rights;
			}
		}

		// Token: 0x040016F6 RID: 5878
		private EventWaitHandleRights rights;
	}
}
