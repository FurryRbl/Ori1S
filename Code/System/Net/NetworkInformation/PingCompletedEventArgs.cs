using System;
using System.ComponentModel;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides data for the <see cref="E:System.Net.NetworkInformation.Ping.PingCompleted" /> event.</summary>
	// Token: 0x020003B1 RID: 945
	public class PingCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
	{
		// Token: 0x060020EC RID: 8428 RVA: 0x000613F4 File Offset: 0x0005F5F4
		internal PingCompletedEventArgs(Exception ex, bool cancelled, object userState, PingReply reply) : base(ex, cancelled, userState)
		{
			this.reply = reply;
		}

		/// <summary>Gets an object that contains data that describes an attempt to send an Internet Control Message Protocol (ICMP) echo request message and receive a corresponding ICMP echo reply message.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that describes the results of the ICMP echo request.</returns>
		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x060020ED RID: 8429 RVA: 0x00061408 File Offset: 0x0005F608
		public PingReply Reply
		{
			get
			{
				return this.reply;
			}
		}

		// Token: 0x0400141C RID: 5148
		private PingReply reply;
	}
}
