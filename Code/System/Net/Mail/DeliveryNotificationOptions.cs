using System;

namespace System.Net.Mail
{
	/// <summary>Describes the delivery notification options for e-mail.</summary>
	// Token: 0x02000339 RID: 825
	[Flags]
	public enum DeliveryNotificationOptions
	{
		/// <summary>No notification.</summary>
		// Token: 0x04001236 RID: 4662
		None = 0,
		/// <summary>Notify if the delivery is successful.</summary>
		// Token: 0x04001237 RID: 4663
		OnSuccess = 1,
		/// <summary>Notify if the delivery is unsuccessful.</summary>
		// Token: 0x04001238 RID: 4664
		OnFailure = 2,
		/// <summary>Notify if the delivery is delayed</summary>
		// Token: 0x04001239 RID: 4665
		Delay = 4,
		/// <summary>Never notify.</summary>
		// Token: 0x0400123A RID: 4666
		Never = 134217728
	}
}
