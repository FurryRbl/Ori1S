using System;

namespace System
{
	/// <summary>Provides the possible values for the configuration setting of the <see cref="T:System.Configuration.IdnElement" /> in the <see cref="N:System.Configuration" /> namespace.</summary>
	// Token: 0x020004B7 RID: 1207
	public enum UriIdnScope
	{
		/// <summary>This value will not convert any Unicode domain names to use Punycode. This is the default value which is consistent with the .NET Framework 2.0 behavior.</summary>
		// Token: 0x04001B74 RID: 7028
		None,
		/// <summary>This value will convert all external Unicode domain names to use the Punycode equivalents (IDN names). In this case to handle international names on the local Intranet, the DNS servers that are used for the Intranet should support Unicode names.</summary>
		// Token: 0x04001B75 RID: 7029
		AllExceptIntranet,
		/// <summary>This value will convert any Unicode domain names to their Punycode equivalents (IDN names).</summary>
		// Token: 0x04001B76 RID: 7030
		All
	}
}
