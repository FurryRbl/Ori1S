using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a set of access rights allowed or denied for a user or group. This class cannot be inherited.</summary>
	// Token: 0x02000426 RID: 1062
	[ComVisible(false)]
	public sealed class SemaphoreAccessRule : AccessRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> class, specifying the user or group the rule applies to, the access rights, and whether the specified access rights are allowed or denied.</summary>
		/// <param name="identity">The user or group the rule applies to. Must be of type <see cref="T:System.Security.Principal.SecurityIdentifier" /> or a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="eventRights">A bitwise combination of <see cref="T:System.Security.AccessControl.SemaphoreRights" /> values specifying the rights allowed or denied.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values specifying whether the rights are allowed or denied.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="eventRights" /> specifies an invalid value.-or-<paramref name="type" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null. -or-<paramref name="eventRights" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identity" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" /> nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x0600266D RID: 9837 RVA: 0x00077460 File Offset: 0x00075660
		public SemaphoreAccessRule(IdentityReference identity, SemaphoreRights semaphoreRights, AccessControlType type) : base(identity, 0, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
			this.semaphoreRights = semaphoreRights;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> class, specifying the name of the user or group the rule applies to, the access rights, and whether the specified access rights are allowed or denied.</summary>
		/// <param name="identity">The name of the user or group the rule applies to.</param>
		/// <param name="eventRights">A bitwise combination of <see cref="T:System.Security.AccessControl.SemaphoreRights" /> values specifying the rights allowed or denied.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values specifying whether the rights are allowed or denied.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="eventRights" /> specifies an invalid value.-or-<paramref name="type" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="eventRights" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identity" /> is null.-or-<paramref name="identity" /> is a zero-length string.-or-<paramref name="identity" /> is longer than 512 characters.</exception>
		// Token: 0x0600266E RID: 9838 RVA: 0x00077478 File Offset: 0x00075678
		public SemaphoreAccessRule(string identity, SemaphoreRights semaphoreRights, AccessControlType type) : base(null, 0, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
			this.semaphoreRights = semaphoreRights;
		}

		/// <summary>Gets the rights allowed or denied by the access rule.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Security.AccessControl.SemaphoreRights" /> values indicating the rights allowed or denied by the access rule.</returns>
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x0600266F RID: 9839 RVA: 0x00077490 File Offset: 0x00075690
		public SemaphoreRights SemaphoreRights
		{
			get
			{
				return this.semaphoreRights;
			}
		}

		// Token: 0x04001795 RID: 6037
		private SemaphoreRights semaphoreRights;
	}
}
