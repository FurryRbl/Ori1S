using System;

namespace System.IO.Ports
{
	/// <summary>Specifies the parity bit for a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
	// Token: 0x02000297 RID: 663
	public enum Parity
	{
		/// <summary>No parity check occurs.</summary>
		// Token: 0x04000E8E RID: 3726
		None,
		/// <summary>Sets the parity bit so that the count of bits set is an odd number.</summary>
		// Token: 0x04000E8F RID: 3727
		Odd,
		/// <summary>Sets the parity bit so that the count of bits set is an even number.</summary>
		// Token: 0x04000E90 RID: 3728
		Even,
		/// <summary>Leaves the parity bit set to 1.</summary>
		// Token: 0x04000E91 RID: 3729
		Mark,
		/// <summary>Leaves the parity bit set to 0.</summary>
		// Token: 0x04000E92 RID: 3730
		Space
	}
}
