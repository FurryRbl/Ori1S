using System;

namespace Microsoft.Win32
{
	/// <summary>Specifies optional behavior when retrieving name/value pairs from a registry key.</summary>
	// Token: 0x0200006C RID: 108
	[Flags]
	public enum RegistryValueOptions
	{
		/// <summary>No optional behavior is specified.</summary>
		// Token: 0x040000FE RID: 254
		None = 0,
		/// <summary>A value of type <see cref="F:Microsoft.Win32.RegistryValueKind.ExpandString" /> is retrieved without expanding its embedded environment variables. </summary>
		// Token: 0x040000FF RID: 255
		DoNotExpandEnvironmentNames = 1
	}
}
