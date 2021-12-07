using System;

namespace System.Configuration
{
	/// <summary>Used to specify which configuration file is to be represented by the Configuration object.</summary>
	// Token: 0x0200003A RID: 58
	public enum ConfigurationUserLevel
	{
		/// <summary>Get the <see cref="T:System.Configuration.Configuration" /> that applies to all users.</summary>
		// Token: 0x040000C0 RID: 192
		None,
		/// <summary>Get the roaming <see cref="T:System.Configuration.Configuration" /> that applies to the current user.</summary>
		// Token: 0x040000C1 RID: 193
		PerUserRoaming = 10,
		/// <summary>Get the local <see cref="T:System.Configuration.Configuration" /> that applies to the current user.</summary>
		// Token: 0x040000C2 RID: 194
		PerUserRoamingAndLocal = 20
	}
}
