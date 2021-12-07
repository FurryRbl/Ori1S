using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Used to control how <see cref="T:System.Net.NetworkInformation.Ping" /> data packets are transmitted.</summary>
	// Token: 0x020003B7 RID: 951
	public class PingOptions
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingOptions" /> class.</summary>
		// Token: 0x06002118 RID: 8472 RVA: 0x00061FA0 File Offset: 0x000601A0
		public PingOptions()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingOptions" /> class and sets the Time to Live and fragmentation values.</summary>
		/// <param name="ttl">An <see cref="T:System.Int32" /> value greater than zero that specifies the number of times that the <see cref="T:System.Net.NetworkInformation.Ping" /> data packets can be forwarded.</param>
		/// <param name="dontFragment">true to prevent data sent to the remote host from being fragmented; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="ttl " />is less than or equal to zero.</exception>
		// Token: 0x06002119 RID: 8473 RVA: 0x00061FB4 File Offset: 0x000601B4
		public PingOptions(int ttl, bool dontFragment)
		{
			if (ttl <= 0)
			{
				throw new ArgumentOutOfRangeException("Must be greater than zero.", "ttl");
			}
			this.ttl = ttl;
			this.dont_fragment = dontFragment;
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls fragmentation of the data sent to the remote host.</summary>
		/// <returns>true if the data cannot be sent in multiple packets; otherwise false. The default is false.</returns>
		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x00061FF8 File Offset: 0x000601F8
		// (set) Token: 0x0600211B RID: 8475 RVA: 0x00062000 File Offset: 0x00060200
		public bool DontFragment
		{
			get
			{
				return this.dont_fragment;
			}
			set
			{
				this.dont_fragment = value;
			}
		}

		/// <summary>Gets or sets the number of routing nodes that can forward the <see cref="T:System.Net.NetworkInformation.Ping" /> data before it is discarded.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that specifies the number of times the <see cref="T:System.Net.NetworkInformation.Ping" /> data packets can be forwarded. The default is 128.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than or equal to zero.</exception>
		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x0006200C File Offset: 0x0006020C
		// (set) Token: 0x0600211D RID: 8477 RVA: 0x00062014 File Offset: 0x00060214
		public int Ttl
		{
			get
			{
				return this.ttl;
			}
			set
			{
				this.ttl = value;
			}
		}

		// Token: 0x0400142D RID: 5165
		private int ttl = 128;

		// Token: 0x0400142E RID: 5166
		private bool dont_fragment;
	}
}
