using System;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000094 RID: 148
	internal class SecurityParameters
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0001F7C0 File Offset: 0x0001D9C0
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x0001F7C8 File Offset: 0x0001D9C8
		public CipherSuite Cipher
		{
			get
			{
				return this.cipher;
			}
			set
			{
				this.cipher = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0001F7D4 File Offset: 0x0001D9D4
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x0001F7DC File Offset: 0x0001D9DC
		public byte[] ClientWriteMAC
		{
			get
			{
				return this.clientWriteMAC;
			}
			set
			{
				this.clientWriteMAC = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0001F7E8 File Offset: 0x0001D9E8
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x0001F7F0 File Offset: 0x0001D9F0
		public byte[] ServerWriteMAC
		{
			get
			{
				return this.serverWriteMAC;
			}
			set
			{
				this.serverWriteMAC = value;
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001F7FC File Offset: 0x0001D9FC
		public void Clear()
		{
			this.cipher = null;
		}

		// Token: 0x040002B4 RID: 692
		private CipherSuite cipher;

		// Token: 0x040002B5 RID: 693
		private byte[] clientWriteMAC;

		// Token: 0x040002B6 RID: 694
		private byte[] serverWriteMAC;
	}
}
