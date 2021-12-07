using System;

namespace System.Net.Sockets
{
	/// <summary>Contains option values for joining an IPv6 multicast group.</summary>
	// Token: 0x020003ED RID: 1005
	public class IPv6MulticastOption
	{
		/// <summary>Initializes a new version of the <see cref="T:System.Net.Sockets.IPv6MulticastOption" /> class for the specified IP multicast group.</summary>
		/// <param name="group">The <see cref="T:System.Net.IPAddress" /> of the multicast group. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is null. </exception>
		// Token: 0x060022BF RID: 8895 RVA: 0x00066040 File Offset: 0x00064240
		public IPv6MulticastOption(IPAddress group) : this(group, 0L)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.IPv6MulticastOption" /> class with the specified IP multicast group and the local interface address.</summary>
		/// <param name="group">The group <see cref="T:System.Net.IPAddress" />. </param>
		/// <param name="ifindex">The local interface address. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="ifindex" /> is less than 0.-or- <paramref name="ifindex" /> is greater than 0x00000000FFFFFFFF. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is null. </exception>
		// Token: 0x060022C0 RID: 8896 RVA: 0x0006604C File Offset: 0x0006424C
		public IPv6MulticastOption(IPAddress group, long ifindex)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (ifindex < 0L || ifindex > (long)((ulong)-1))
			{
				throw new ArgumentOutOfRangeException("ifindex");
			}
			this.group = group;
			this.ifIndex = ifindex;
		}

		/// <summary>Gets or sets the IP address of a multicast group.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> that contains the Internet address of a multicast group.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is null. </exception>
		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x0006609C File Offset: 0x0006429C
		// (set) Token: 0x060022C2 RID: 8898 RVA: 0x000660A4 File Offset: 0x000642A4
		public IPAddress Group
		{
			get
			{
				return this.group;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.group = value;
			}
		}

		/// <summary>Gets or sets the interface index that is associated with a multicast group.</summary>
		/// <returns>A <see cref="T:System.UInt64" /> value that specifies the address of the interface.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value that is specified for a set operation is less than 0 or greater than 0x00000000FFFFFFFF. </exception>
		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x000660C0 File Offset: 0x000642C0
		// (set) Token: 0x060022C4 RID: 8900 RVA: 0x000660C8 File Offset: 0x000642C8
		public long InterfaceIndex
		{
			get
			{
				return this.ifIndex;
			}
			set
			{
				if (value < 0L || value > (long)((ulong)-1))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ifIndex = value;
			}
		}

		// Token: 0x0400157B RID: 5499
		private IPAddress group;

		// Token: 0x0400157C RID: 5500
		private long ifIndex;
	}
}
