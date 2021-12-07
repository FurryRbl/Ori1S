using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200038E RID: 910
	internal class Win32IPv6InterfaceProperties : IPv6InterfaceProperties
	{
		// Token: 0x06002060 RID: 8288 RVA: 0x0005FAEC File Offset: 0x0005DCEC
		public Win32IPv6InterfaceProperties(Win32_MIB_IFROW mib)
		{
			this.mib = mib;
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06002061 RID: 8289 RVA: 0x0005FAFC File Offset: 0x0005DCFC
		public override int Index
		{
			get
			{
				return this.mib.Index;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002062 RID: 8290 RVA: 0x0005FB0C File Offset: 0x0005DD0C
		public override int Mtu
		{
			get
			{
				return this.mib.Mtu;
			}
		}

		// Token: 0x0400137B RID: 4987
		private Win32_MIB_IFROW mib;
	}
}
