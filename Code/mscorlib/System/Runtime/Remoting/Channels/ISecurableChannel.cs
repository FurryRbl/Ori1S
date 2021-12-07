using System;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>The <see cref="T:System.Runtime.Remoting.Channels.ISecurableChannel" /> contains one property, <see cref="P:System.Runtime.Remoting.Channels.ISecurableChannel.IsSecured" />, which gets or sets a Boolean value that indicates whether the current channel is secure.</summary>
	// Token: 0x0200045B RID: 1115
	public interface ISecurableChannel
	{
		/// <summary>Gets or sets a Boolean value that indicates whether the current channel is secure.</summary>
		/// <returns>A Boolean value that indicates whether the current channel is secure.</returns>
		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06002E87 RID: 11911
		// (set) Token: 0x06002E88 RID: 11912
		bool IsSecured { get; set; }
	}
}
