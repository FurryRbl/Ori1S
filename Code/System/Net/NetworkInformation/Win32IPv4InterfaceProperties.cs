using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000387 RID: 903
	internal sealed class Win32IPv4InterfaceProperties : IPv4InterfaceProperties
	{
		// Token: 0x0600201E RID: 8222 RVA: 0x0005F7C8 File Offset: 0x0005D9C8
		public Win32IPv4InterfaceProperties(Win32_IP_ADAPTER_INFO ainfo, Win32_MIB_IFROW mib)
		{
			this.ainfo = ainfo;
			this.mib = mib;
			int num = 0;
			Win32IPv4InterfaceProperties.GetPerAdapterInfo(mib.Index, null, ref num);
			this.painfo = new Win32_IP_PER_ADAPTER_INFO();
			int perAdapterInfo = Win32IPv4InterfaceProperties.GetPerAdapterInfo(mib.Index, this.painfo, ref num);
			if (perAdapterInfo != 0)
			{
				throw new NetworkInformationException(perAdapterInfo);
			}
		}

		// Token: 0x0600201F RID: 8223
		[DllImport("iphlpapi.dll")]
		private static extern int GetPerAdapterInfo(int IfIndex, Win32_IP_PER_ADAPTER_INFO pPerAdapterInfo, ref int pOutBufLen);

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06002020 RID: 8224 RVA: 0x0005F828 File Offset: 0x0005DA28
		public override int Index
		{
			get
			{
				return this.mib.Index;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x0005F838 File Offset: 0x0005DA38
		public override bool IsAutomaticPrivateAddressingActive
		{
			get
			{
				return this.painfo.AutoconfigActive != 0U;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x0005F84C File Offset: 0x0005DA4C
		public override bool IsAutomaticPrivateAddressingEnabled
		{
			get
			{
				return this.painfo.AutoconfigEnabled != 0U;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x0005F860 File Offset: 0x0005DA60
		public override bool IsDhcpEnabled
		{
			get
			{
				return this.ainfo.DhcpEnabled != 0U;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x0005F874 File Offset: 0x0005DA74
		public override bool IsForwardingEnabled
		{
			get
			{
				return Win32_FIXED_INFO.Instance.EnableRouting != 0U;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x0005F888 File Offset: 0x0005DA88
		public override int Mtu
		{
			get
			{
				return this.mib.Mtu;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06002026 RID: 8230 RVA: 0x0005F898 File Offset: 0x0005DA98
		public override bool UsesWins
		{
			get
			{
				return this.ainfo.HaveWins;
			}
		}

		// Token: 0x04001371 RID: 4977
		private Win32_IP_ADAPTER_INFO ainfo;

		// Token: 0x04001372 RID: 4978
		private Win32_IP_PER_ADAPTER_INFO painfo;

		// Token: 0x04001373 RID: 4979
		private Win32_MIB_IFROW mib;
	}
}
