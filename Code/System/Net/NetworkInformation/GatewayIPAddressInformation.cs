using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Represents the IP address of the network gateway. This class cannot be instantiated.</summary>
	// Token: 0x0200035E RID: 862
	public abstract class GatewayIPAddressInformation
	{
		/// <summary>Get the IP address of the gateway.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> object that contains the IP address of the gateway.</returns>
		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001E5D RID: 7773
		public abstract IPAddress Address { get; }
	}
}
