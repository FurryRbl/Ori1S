using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the operational state of a network interface.</summary>
	// Token: 0x020003AF RID: 943
	public enum OperationalStatus
	{
		/// <summary>The network interface is up; it can transmit data packets.</summary>
		// Token: 0x04001412 RID: 5138
		Up = 1,
		/// <summary>The network interface is unable to transmit data packets.</summary>
		// Token: 0x04001413 RID: 5139
		Down,
		/// <summary>The network interface is running tests.</summary>
		// Token: 0x04001414 RID: 5140
		Testing,
		/// <summary>The network interface status is not known.</summary>
		// Token: 0x04001415 RID: 5141
		Unknown,
		/// <summary>The network interface is not in a condition to transmit data packets; it is waiting for an external event.</summary>
		// Token: 0x04001416 RID: 5142
		Dormant,
		/// <summary>The network interface is unable to transmit data packets because of a missing component, typically a hardware component.</summary>
		// Token: 0x04001417 RID: 5143
		NotPresent,
		/// <summary>The network interface is unable to transmit data packets because it runs on top of one or more other interfaces, and at least one of these "lower layer" interfaces is down.</summary>
		// Token: 0x04001418 RID: 5144
		LowerLayerDown
	}
}
