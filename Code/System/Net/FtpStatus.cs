using System;

namespace System.Net
{
	// Token: 0x0200030D RID: 781
	internal class FtpStatus
	{
		// Token: 0x06001B30 RID: 6960 RVA: 0x0004D7C4 File Offset: 0x0004B9C4
		public FtpStatus(FtpStatusCode statusCode, string statusDescription)
		{
			this.statusCode = statusCode;
			this.statusDescription = statusDescription;
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x0004D7DC File Offset: 0x0004B9DC
		public FtpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x0004D7E4 File Offset: 0x0004B9E4
		public string StatusDescription
		{
			get
			{
				return this.statusDescription;
			}
		}

		// Token: 0x040010D7 RID: 4311
		private readonly FtpStatusCode statusCode;

		// Token: 0x040010D8 RID: 4312
		private readonly string statusDescription;
	}
}
