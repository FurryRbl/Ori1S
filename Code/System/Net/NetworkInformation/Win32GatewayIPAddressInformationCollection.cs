using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200035C RID: 860
	internal class Win32GatewayIPAddressInformationCollection : GatewayIPAddressInformationCollection
	{
		// Token: 0x06001E53 RID: 7763 RVA: 0x0005CC80 File Offset: 0x0005AE80
		private Win32GatewayIPAddressInformationCollection(bool isReadOnly)
		{
			this.is_readonly = isReadOnly;
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x0005CC90 File Offset: 0x0005AE90
		public Win32GatewayIPAddressInformationCollection(params Win32_IP_ADDR_STRING[] al)
		{
			foreach (Win32_IP_ADDR_STRING win32_IP_ADDR_STRING in al)
			{
				if (!string.IsNullOrEmpty(win32_IP_ADDR_STRING.IpAddress))
				{
					this.Add(new GatewayIPAddressInformationImpl(IPAddress.Parse(win32_IP_ADDR_STRING.IpAddress)));
					this.AddSubsequently(win32_IP_ADDR_STRING.Next);
				}
			}
			this.is_readonly = true;
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x0005CD18 File Offset: 0x0005AF18
		private void AddSubsequently(IntPtr head)
		{
			IntPtr intPtr = head;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADDR_STRING win32_IP_ADDR_STRING = (Win32_IP_ADDR_STRING)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADDR_STRING));
				this.Add(new GatewayIPAddressInformationImpl(IPAddress.Parse(win32_IP_ADDR_STRING.IpAddress)));
				intPtr = win32_IP_ADDR_STRING.Next;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001E57 RID: 7767 RVA: 0x0005CD74 File Offset: 0x0005AF74
		public override bool IsReadOnly
		{
			get
			{
				return this.is_readonly;
			}
		}

		// Token: 0x040012DE RID: 4830
		public static readonly Win32GatewayIPAddressInformationCollection Empty = new Win32GatewayIPAddressInformationCollection(true);

		// Token: 0x040012DF RID: 4831
		private bool is_readonly;
	}
}
