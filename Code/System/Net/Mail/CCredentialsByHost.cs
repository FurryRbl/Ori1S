using System;

namespace System.Net.Mail
{
	// Token: 0x02000346 RID: 838
	internal class CCredentialsByHost : ICredentialsByHost
	{
		// Token: 0x06001DDA RID: 7642 RVA: 0x0005B7F0 File Offset: 0x000599F0
		public CCredentialsByHost(string userName, string password)
		{
			this.userName = userName;
			this.password = password;
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x0005B808 File Offset: 0x00059A08
		public NetworkCredential GetCredential(string host, int port, string authenticationType)
		{
			return new NetworkCredential(this.userName, this.password);
		}

		// Token: 0x04001286 RID: 4742
		private string userName;

		// Token: 0x04001287 RID: 4743
		private string password;
	}
}
