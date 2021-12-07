﻿using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the type of access control modification to perform. This enumeration is used by methods of the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> class and its descendents.</summary>
	// Token: 0x02000555 RID: 1365
	public enum AccessControlModification
	{
		/// <summary>Add the specified authorization rule to the access control list (ACL).</summary>
		// Token: 0x0400167D RID: 5757
		Add,
		/// <summary>Remove all authorization rules from the ACL, then add the specified authorization rule to the ACL.</summary>
		// Token: 0x0400167E RID: 5758
		Set,
		/// <summary>Remove authorization rules that contain the same SID as the specified authorization rule from the ACL, and then add the specified authorization rule to the ACL.</summary>
		// Token: 0x0400167F RID: 5759
		Reset,
		/// <summary>Remove authorization rules that contain the same security identifier (SID) and access mask as the specified authorization rule from the ACL.</summary>
		// Token: 0x04001680 RID: 5760
		Remove,
		/// <summary>Remove authorization rules that contain the same SID as the specified authorization rule from the ACL.</summary>
		// Token: 0x04001681 RID: 5761
		RemoveAll,
		/// <summary>Remove authorization rules that exactly match the specified authorization rule from the ACL.</summary>
		// Token: 0x04001682 RID: 5762
		RemoveSpecific
	}
}
