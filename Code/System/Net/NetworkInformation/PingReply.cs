using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about the status and data resulting from a <see cref="Overload:System.Net.NetworkInformation.Ping.Send" /> or <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> operation.</summary>
	// Token: 0x020003B8 RID: 952
	public class PingReply
	{
		// Token: 0x0600211E RID: 8478 RVA: 0x00062020 File Offset: 0x00060220
		internal PingReply(IPAddress address, byte[] buffer, PingOptions options, long roundtripTime, IPStatus status)
		{
			this.address = address;
			this.buffer = buffer;
			this.options = options;
			this.rtt = roundtripTime;
			this.status = status;
		}

		/// <summary>Gets the address of the host that sends the Internet Control Message Protocol (ICMP) echo reply.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> containing the destination for the ICMP echo message.</returns>
		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x0600211F RID: 8479 RVA: 0x00062050 File Offset: 0x00060250
		public IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		/// <summary>Gets the buffer of data received in an Internet Control Message Protocol (ICMP) echo reply message.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the data received in an ICMP echo reply message, or an empty array, if no reply was received.</returns>
		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06002120 RID: 8480 RVA: 0x00062058 File Offset: 0x00060258
		public byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		/// <summary>Gets the options used to transmit the reply to an Internet Control Message Protocol (ICMP) echo request.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingOptions" /> object that contains the Time to Live (TTL) and the fragmentation directive used for transmitting the reply if <see cref="P:System.Net.NetworkInformation.PingReply.Status" /> is <see cref="F:System.Net.NetworkInformation.IPStatus.Success" />; otherwise, null.</returns>
		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06002121 RID: 8481 RVA: 0x00062060 File Offset: 0x00060260
		public PingOptions Options
		{
			get
			{
				return this.options;
			}
		}

		/// <summary>Gets the number of milliseconds taken to send an Internet Control Message Protocol (ICMP) echo request and receive the corresponding ICMP echo reply message.</summary>
		/// <returns>An <see cref="T:System.Int64" /> that specifies the round trip time, in milliseconds. </returns>
		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06002122 RID: 8482 RVA: 0x00062068 File Offset: 0x00060268
		public long RoundtripTime
		{
			get
			{
				return this.rtt;
			}
		}

		/// <summary>Gets the status of an attempt to send an Internet Control Message Protocol (ICMP) echo request and receive the corresponding ICMP echo reply message.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPStatus" /> value indicating the result of the request.</returns>
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06002123 RID: 8483 RVA: 0x00062070 File Offset: 0x00060270
		public IPStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x0400142F RID: 5167
		private IPAddress address;

		// Token: 0x04001430 RID: 5168
		private byte[] buffer;

		// Token: 0x04001431 RID: 5169
		private PingOptions options;

		// Token: 0x04001432 RID: 5170
		private long rtt;

		// Token: 0x04001433 RID: 5171
		private IPStatus status;
	}
}
