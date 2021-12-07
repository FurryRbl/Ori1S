using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000384 RID: 900
	internal abstract class UnixIPv4InterfaceProperties : IPv4InterfaceProperties
	{
		// Token: 0x06002012 RID: 8210 RVA: 0x0005F6C0 File Offset: 0x0005D8C0
		public UnixIPv4InterfaceProperties(UnixNetworkInterface iface)
		{
			this.iface = iface;
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06002013 RID: 8211 RVA: 0x0005F6D0 File Offset: 0x0005D8D0
		public override int Index
		{
			get
			{
				return UnixNetworkInterface.IfNameToIndex(this.iface.Name);
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06002014 RID: 8212 RVA: 0x0005F6E4 File Offset: 0x0005D8E4
		public override bool IsAutomaticPrivateAddressingActive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06002015 RID: 8213 RVA: 0x0005F6E8 File Offset: 0x0005D8E8
		public override bool IsAutomaticPrivateAddressingEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06002016 RID: 8214 RVA: 0x0005F6EC File Offset: 0x0005D8EC
		public override bool IsDhcpEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x0005F6F0 File Offset: 0x0005D8F0
		public override bool UsesWins
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001370 RID: 4976
		protected UnixNetworkInterface iface;
	}
}
