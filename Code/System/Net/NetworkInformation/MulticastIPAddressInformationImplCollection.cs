using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200039E RID: 926
	internal class MulticastIPAddressInformationImplCollection : MulticastIPAddressInformationCollection
	{
		// Token: 0x0600206E RID: 8302 RVA: 0x0005FC1C File Offset: 0x0005DE1C
		private MulticastIPAddressInformationImplCollection(bool isReadOnly)
		{
			this.is_readonly = isReadOnly;
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06002070 RID: 8304 RVA: 0x0005FC3C File Offset: 0x0005DE3C
		public override bool IsReadOnly
		{
			get
			{
				return this.is_readonly;
			}
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x0005FC44 File Offset: 0x0005DE44
		public static MulticastIPAddressInformationCollection Win32FromMulticast(IntPtr ptr)
		{
			MulticastIPAddressInformationImplCollection multicastIPAddressInformationImplCollection = new MulticastIPAddressInformationImplCollection(false);
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_MULTICAST_ADDRESS win32_IP_ADAPTER_MULTICAST_ADDRESS = (Win32_IP_ADAPTER_MULTICAST_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_MULTICAST_ADDRESS));
				multicastIPAddressInformationImplCollection.Add(new MulticastIPAddressInformationImpl(win32_IP_ADAPTER_MULTICAST_ADDRESS.Address.GetIPAddress(), win32_IP_ADAPTER_MULTICAST_ADDRESS.LengthFlags.IsDnsEligible, win32_IP_ADAPTER_MULTICAST_ADDRESS.LengthFlags.IsTransient));
				intPtr = win32_IP_ADAPTER_MULTICAST_ADDRESS.Next;
			}
			multicastIPAddressInformationImplCollection.is_readonly = true;
			return multicastIPAddressInformationImplCollection;
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x0005FCC4 File Offset: 0x0005DEC4
		public static MulticastIPAddressInformationImplCollection LinuxFromList(List<IPAddress> addresses)
		{
			MulticastIPAddressInformationImplCollection multicastIPAddressInformationImplCollection = new MulticastIPAddressInformationImplCollection(false);
			foreach (IPAddress address in addresses)
			{
				multicastIPAddressInformationImplCollection.Add(new MulticastIPAddressInformationImpl(address, true, false));
			}
			multicastIPAddressInformationImplCollection.is_readonly = true;
			return multicastIPAddressInformationImplCollection;
		}

		// Token: 0x040013C4 RID: 5060
		public static readonly MulticastIPAddressInformationImplCollection Empty = new MulticastIPAddressInformationImplCollection(true);

		// Token: 0x040013C5 RID: 5061
		private bool is_readonly;
	}
}
