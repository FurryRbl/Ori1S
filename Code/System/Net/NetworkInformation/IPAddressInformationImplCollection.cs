using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200036E RID: 878
	internal class IPAddressInformationImplCollection : IPAddressInformationCollection
	{
		// Token: 0x06001F36 RID: 7990 RVA: 0x0005DB00 File Offset: 0x0005BD00
		private IPAddressInformationImplCollection(bool isReadOnly)
		{
			this.is_readonly = isReadOnly;
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06001F38 RID: 7992 RVA: 0x0005DB20 File Offset: 0x0005BD20
		public override bool IsReadOnly
		{
			get
			{
				return this.is_readonly;
			}
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x0005DB28 File Offset: 0x0005BD28
		public static IPAddressInformationCollection Win32FromAnycast(IntPtr ptr)
		{
			IPAddressInformationImplCollection ipaddressInformationImplCollection = new IPAddressInformationImplCollection(false);
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_ANYCAST_ADDRESS win32_IP_ADAPTER_ANYCAST_ADDRESS = (Win32_IP_ADAPTER_ANYCAST_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_ANYCAST_ADDRESS));
				ipaddressInformationImplCollection.Add(new IPAddressInformationImpl(win32_IP_ADAPTER_ANYCAST_ADDRESS.Address.GetIPAddress(), win32_IP_ADAPTER_ANYCAST_ADDRESS.LengthFlags.IsDnsEligible, win32_IP_ADAPTER_ANYCAST_ADDRESS.LengthFlags.IsTransient));
				intPtr = win32_IP_ADAPTER_ANYCAST_ADDRESS.Next;
			}
			ipaddressInformationImplCollection.is_readonly = true;
			return ipaddressInformationImplCollection;
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x0005DBA8 File Offset: 0x0005BDA8
		public static IPAddressInformationImplCollection LinuxFromAnycast(IList<IPAddress> addresses)
		{
			IPAddressInformationImplCollection ipaddressInformationImplCollection = new IPAddressInformationImplCollection(false);
			foreach (IPAddress address in addresses)
			{
				ipaddressInformationImplCollection.Add(new IPAddressInformationImpl(address, false, false));
			}
			ipaddressInformationImplCollection.is_readonly = true;
			return ipaddressInformationImplCollection;
		}

		// Token: 0x04001310 RID: 4880
		public static readonly IPAddressInformationImplCollection Empty = new IPAddressInformationImplCollection(true);

		// Token: 0x04001311 RID: 4881
		private bool is_readonly;
	}
}
