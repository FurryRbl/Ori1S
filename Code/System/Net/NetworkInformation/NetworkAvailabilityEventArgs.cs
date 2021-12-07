using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides data for the <see cref="E:System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged" /> event.</summary>
	// Token: 0x020003A2 RID: 930
	public class NetworkAvailabilityEventArgs : EventArgs
	{
		// Token: 0x06002084 RID: 8324 RVA: 0x0005FD94 File Offset: 0x0005DF94
		internal NetworkAvailabilityEventArgs(bool available)
		{
			this.available = available;
		}

		/// <summary>Gets the current status of the network connection.</summary>
		/// <returns>true if the network is available; otherwise, false.</returns>
		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06002085 RID: 8325 RVA: 0x0005FDA4 File Offset: 0x0005DFA4
		public bool IsAvailable
		{
			get
			{
				return this.available;
			}
		}

		// Token: 0x040013CF RID: 5071
		private bool available;
	}
}
