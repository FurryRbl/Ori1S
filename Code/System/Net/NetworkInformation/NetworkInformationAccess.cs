using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies permission to access information about network interfaces and traffic statistics.</summary>
	// Token: 0x020003A4 RID: 932
	[Flags]
	public enum NetworkInformationAccess
	{
		/// <summary>No access to network information.</summary>
		// Token: 0x040013D3 RID: 5075
		None = 0,
		/// <summary>Read access to network information.</summary>
		// Token: 0x040013D4 RID: 5076
		Read = 1,
		/// <summary>Ping access to network information.</summary>
		// Token: 0x040013D5 RID: 5077
		Ping = 4
	}
}
