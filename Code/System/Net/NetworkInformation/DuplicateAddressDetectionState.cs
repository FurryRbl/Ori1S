using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the current state of an IP address.</summary>
	// Token: 0x0200035A RID: 858
	public enum DuplicateAddressDetectionState
	{
		/// <summary>The address is not valid. A nonvalid address is expired and no longer assigned to an interface; applications should not send data packets to it.</summary>
		// Token: 0x040012D8 RID: 4824
		Invalid,
		/// <summary>The duplicate address detection procedure's evaluation of the address has not completed successfully. Applications should not use the address because it is not yet valid and packets sent to it are discarded.</summary>
		// Token: 0x040012D9 RID: 4825
		Tentative,
		/// <summary>The address is not unique. This address should not be assigned to the network interface.</summary>
		// Token: 0x040012DA RID: 4826
		Duplicate,
		/// <summary>The address is valid, but it is nearing its lease lifetime and should not be used by applications.</summary>
		// Token: 0x040012DB RID: 4827
		Deprecated,
		/// <summary>The address is valid and its use is unrestricted.</summary>
		// Token: 0x040012DC RID: 4828
		Preferred
	}
}
