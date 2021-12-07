using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003CA RID: 970
	internal class LinuxUnicastIPAddressInformation : UnicastIPAddressInformation
	{
		// Token: 0x06002190 RID: 8592 RVA: 0x00062754 File Offset: 0x00060954
		public LinuxUnicastIPAddressInformation(IPAddress address)
		{
			this.address = address;
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06002191 RID: 8593 RVA: 0x00062764 File Offset: 0x00060964
		public override IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x0006276C File Offset: 0x0006096C
		public override bool IsDnsEligible
		{
			get
			{
				byte[] addressBytes = this.address.GetAddressBytes();
				return addressBytes[0] != 169 || addressBytes[1] != 254;
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06002193 RID: 8595 RVA: 0x000627A4 File Offset: 0x000609A4
		[MonoTODO("Always returns false")]
		public override bool IsTransient
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06002194 RID: 8596 RVA: 0x000627A8 File Offset: 0x000609A8
		public override long AddressPreferredLifetime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x000627B0 File Offset: 0x000609B0
		public override long AddressValidLifetime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06002196 RID: 8598 RVA: 0x000627B8 File Offset: 0x000609B8
		public override long DhcpLeaseLifetime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x000627C0 File Offset: 0x000609C0
		public override DuplicateAddressDetectionState DuplicateAddressDetectionState
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06002198 RID: 8600 RVA: 0x000627C8 File Offset: 0x000609C8
		public override IPAddress IPv4Mask
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06002199 RID: 8601 RVA: 0x000627D0 File Offset: 0x000609D0
		public override PrefixOrigin PrefixOrigin
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x0600219A RID: 8602 RVA: 0x000627D8 File Offset: 0x000609D8
		public override SuffixOrigin SuffixOrigin
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0400146F RID: 5231
		private IPAddress address;
	}
}
