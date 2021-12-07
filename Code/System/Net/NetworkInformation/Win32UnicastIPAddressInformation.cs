using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003C9 RID: 969
	internal class Win32UnicastIPAddressInformation : UnicastIPAddressInformation
	{
		// Token: 0x06002185 RID: 8581 RVA: 0x000625E8 File Offset: 0x000607E8
		public Win32UnicastIPAddressInformation(int ifIndex, Win32_IP_ADAPTER_UNICAST_ADDRESS info)
		{
			this.if_index = ifIndex;
			this.info = info;
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x00062600 File Offset: 0x00060800
		public override IPAddress Address
		{
			get
			{
				return this.info.Address.GetIPAddress();
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06002187 RID: 8583 RVA: 0x00062614 File Offset: 0x00060814
		public override bool IsDnsEligible
		{
			get
			{
				return this.info.LengthFlags.IsDnsEligible;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x00062628 File Offset: 0x00060828
		public override bool IsTransient
		{
			get
			{
				return this.info.LengthFlags.IsTransient;
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06002189 RID: 8585 RVA: 0x0006263C File Offset: 0x0006083C
		public override long AddressPreferredLifetime
		{
			get
			{
				return (long)((ulong)this.info.PreferredLifetime);
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x0006264C File Offset: 0x0006084C
		public override long AddressValidLifetime
		{
			get
			{
				return (long)((ulong)this.info.ValidLifetime);
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x0600218B RID: 8587 RVA: 0x0006265C File Offset: 0x0006085C
		public override long DhcpLeaseLifetime
		{
			get
			{
				return (long)((ulong)this.info.LeaseLifetime);
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x0006266C File Offset: 0x0006086C
		public override DuplicateAddressDetectionState DuplicateAddressDetectionState
		{
			get
			{
				return this.info.DadState;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x0600218D RID: 8589 RVA: 0x0006267C File Offset: 0x0006087C
		public override IPAddress IPv4Mask
		{
			get
			{
				Win32_IP_ADAPTER_INFO adapterInfoByIndex = Win32NetworkInterface2.GetAdapterInfoByIndex(this.if_index);
				if (adapterInfoByIndex == null)
				{
					throw new Exception("huh? " + this.if_index);
				}
				if (this.Address == null)
				{
					return null;
				}
				string b = this.Address.ToString();
				Win32_IP_ADDR_STRING win32_IP_ADDR_STRING = adapterInfoByIndex.IpAddressList;
				while (!(win32_IP_ADDR_STRING.IpAddress == b))
				{
					if (win32_IP_ADDR_STRING.Next == IntPtr.Zero)
					{
						return null;
					}
					win32_IP_ADDR_STRING = (Win32_IP_ADDR_STRING)Marshal.PtrToStructure(win32_IP_ADDR_STRING.Next, typeof(Win32_IP_ADDR_STRING));
				}
				return IPAddress.Parse(win32_IP_ADDR_STRING.IpMask);
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x00062734 File Offset: 0x00060934
		public override PrefixOrigin PrefixOrigin
		{
			get
			{
				return this.info.PrefixOrigin;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x0600218F RID: 8591 RVA: 0x00062744 File Offset: 0x00060944
		public override SuffixOrigin SuffixOrigin
		{
			get
			{
				return this.info.SuffixOrigin;
			}
		}

		// Token: 0x0400146D RID: 5229
		private int if_index;

		// Token: 0x0400146E RID: 5230
		private Win32_IP_ADAPTER_UNICAST_ADDRESS info;
	}
}
