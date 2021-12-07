using System;

namespace System.Security
{
	/// <summary>Specifies the scope of a <see cref="T:System.Security.SecurityCriticalAttribute" />.</summary>
	// Token: 0x02000542 RID: 1346
	public enum SecurityCriticalScope
	{
		/// <summary>The attribute applies only to the immediate target.</summary>
		// Token: 0x04001640 RID: 5696
		Explicit,
		/// <summary>The attribute applies to all code that follows it.</summary>
		// Token: 0x04001641 RID: 5697
		Everything
	}
}
