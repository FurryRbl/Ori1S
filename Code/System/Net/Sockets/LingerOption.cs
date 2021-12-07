using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies whether a <see cref="T:System.Net.Sockets.Socket" /> will remain connected after a call to the <see cref="M:System.Net.Sockets.Socket.Close" /> or <see cref="M:System.Net.Sockets.TcpClient.Close" /> methods and the length of time it will remain connected, if data remains to be sent.</summary>
	// Token: 0x020003EF RID: 1007
	public class LingerOption
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.LingerOption" /> class.</summary>
		/// <param name="enable">true to remain connected after the <see cref="M:System.Net.Sockets.Socket.Close" /> method is called; otherwise, false. </param>
		/// <param name="seconds">The number of seconds to remain connected after the <see cref="M:System.Net.Sockets.Socket.Close" /> method is called. </param>
		// Token: 0x060022CC RID: 8908 RVA: 0x00066198 File Offset: 0x00064398
		public LingerOption(bool enable, int secs)
		{
			this.enabled = enable;
			this.seconds = secs;
		}

		/// <summary>Gets or sets a value that indicates whether to linger after the <see cref="T:System.Net.Sockets.Socket" /> is closed.</summary>
		/// <returns>true if the <see cref="T:System.Net.Sockets.Socket" /> should linger after <see cref="M:System.Net.Sockets.Socket.Close" /> is called; otherwise, false.</returns>
		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x000661B0 File Offset: 0x000643B0
		// (set) Token: 0x060022CE RID: 8910 RVA: 0x000661B8 File Offset: 0x000643B8
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		/// <summary>Gets or sets the amount of time to remain connected after calling the <see cref="M:System.Net.Sockets.Socket.Close" /> method if data remains to be sent.</summary>
		/// <returns>The amount of time, in seconds, to remain connected after calling <see cref="M:System.Net.Sockets.Socket.Close" />.</returns>
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x000661C4 File Offset: 0x000643C4
		// (set) Token: 0x060022D0 RID: 8912 RVA: 0x000661CC File Offset: 0x000643CC
		public int LingerTime
		{
			get
			{
				return this.seconds;
			}
			set
			{
				this.seconds = value;
			}
		}

		// Token: 0x0400157F RID: 5503
		private bool enabled;

		// Token: 0x04001580 RID: 5504
		private int seconds;
	}
}
