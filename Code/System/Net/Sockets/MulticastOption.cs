using System;

namespace System.Net.Sockets
{
	/// <summary>Contains <see cref="T:System.Net.IPAddress" /> values used to join and drop multicast groups.</summary>
	// Token: 0x020003F0 RID: 1008
	public class MulticastOption
	{
		/// <summary>Initializes a new version of the <see cref="T:System.Net.Sockets.MulticastOption" /> class for the specified IP multicast group.</summary>
		/// <param name="group">The <see cref="T:System.Net.IPAddress" /> of the multicast group. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is null. </exception>
		// Token: 0x060022D1 RID: 8913 RVA: 0x000661D8 File Offset: 0x000643D8
		public MulticastOption(IPAddress group) : this(group, IPAddress.Any)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.MulticastOption" /> class with the specified IP multicast group address and interface index.</summary>
		/// <param name="group">The <see cref="T:System.Net.IPAddress" /> of the multicast group.</param>
		/// <param name="interfaceIndex">The index of the interface that is used to send and receive multicast packets.</param>
		// Token: 0x060022D2 RID: 8914 RVA: 0x000661E8 File Offset: 0x000643E8
		public MulticastOption(IPAddress group, int interfaceIndex)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (interfaceIndex < 0 || interfaceIndex > 16777215)
			{
				throw new ArgumentOutOfRangeException("interfaceIndex");
			}
			this.group = group;
			this.iface_index = interfaceIndex;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.MulticastOption" /> class with the specified IP multicast group address and local IP address associated with a network interface.</summary>
		/// <param name="group">The group <see cref="T:System.Net.IPAddress" />. </param>
		/// <param name="mcint">The local <see cref="T:System.Net.IPAddress" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is null.-or- <paramref name="mcint" /> is null. </exception>
		// Token: 0x060022D3 RID: 8915 RVA: 0x00066238 File Offset: 0x00064438
		public MulticastOption(IPAddress group, IPAddress mcint)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (mcint == null)
			{
				throw new ArgumentNullException("mcint");
			}
			this.group = group;
			this.local = mcint;
		}

		/// <summary>Gets or sets the IP address of a multicast group.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> that contains the Internet address of a multicast group.</returns>
		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x060022D4 RID: 8916 RVA: 0x0006627C File Offset: 0x0006447C
		// (set) Token: 0x060022D5 RID: 8917 RVA: 0x00066284 File Offset: 0x00064484
		public IPAddress Group
		{
			get
			{
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		/// <summary>Gets or sets the local address associated with a multicast group.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> that contains the local address associated with a multicast group.</returns>
		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x060022D6 RID: 8918 RVA: 0x00066290 File Offset: 0x00064490
		// (set) Token: 0x060022D7 RID: 8919 RVA: 0x00066298 File Offset: 0x00064498
		public IPAddress LocalAddress
		{
			get
			{
				return this.local;
			}
			set
			{
				this.local = value;
				this.iface_index = 0;
			}
		}

		/// <summary>Gets or sets the index of the interface that is used to send and receive multicast packets. </summary>
		/// <returns>An integer that represents the index of a <see cref="T:System.Net.NetworkInformation.NetworkInterface" /> array element.</returns>
		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x060022D8 RID: 8920 RVA: 0x000662A8 File Offset: 0x000644A8
		// (set) Token: 0x060022D9 RID: 8921 RVA: 0x000662B0 File Offset: 0x000644B0
		public int InterfaceIndex
		{
			get
			{
				return this.iface_index;
			}
			set
			{
				if (value < 0 || value > 16777215)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.iface_index = value;
				this.local = null;
			}
		}

		// Token: 0x04001581 RID: 5505
		private IPAddress group;

		// Token: 0x04001582 RID: 5506
		private IPAddress local;

		// Token: 0x04001583 RID: 5507
		private int iface_index;
	}
}
