using System;

namespace System.Net
{
	/// <summary>Provides a container class for Internet host address information.</summary>
	// Token: 0x0200032C RID: 812
	public class IPHostEntry
	{
		/// <summary>Gets or sets a list of IP addresses that are associated with a host.</summary>
		/// <returns>An array of type <see cref="T:System.Net.IPAddress" /> that contains IP addresses that resolve to the host names that are contained in the <see cref="P:System.Net.IPHostEntry.Aliases" /> property.</returns>
		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x000552F8 File Offset: 0x000534F8
		// (set) Token: 0x06001CD3 RID: 7379 RVA: 0x00055300 File Offset: 0x00053500
		public IPAddress[] AddressList
		{
			get
			{
				return this.addressList;
			}
			set
			{
				this.addressList = value;
			}
		}

		/// <summary>Gets or sets a list of aliases that are associated with a host.</summary>
		/// <returns>An array of strings that contain DNS names that resolve to the IP addresses in the <see cref="P:System.Net.IPHostEntry.AddressList" /> property.</returns>
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x0005530C File Offset: 0x0005350C
		// (set) Token: 0x06001CD5 RID: 7381 RVA: 0x00055314 File Offset: 0x00053514
		public string[] Aliases
		{
			get
			{
				return this.aliases;
			}
			set
			{
				this.aliases = value;
			}
		}

		/// <summary>Gets or sets the DNS name of the host.</summary>
		/// <returns>A string that contains the primary host name for the server.</returns>
		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x00055320 File Offset: 0x00053520
		// (set) Token: 0x06001CD7 RID: 7383 RVA: 0x00055328 File Offset: 0x00053528
		public string HostName
		{
			get
			{
				return this.hostName;
			}
			set
			{
				this.hostName = value;
			}
		}

		// Token: 0x04001214 RID: 4628
		private IPAddress[] addressList;

		// Token: 0x04001215 RID: 4629
		private string[] aliases;

		// Token: 0x04001216 RID: 4630
		private string hostName;
	}
}
