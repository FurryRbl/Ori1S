using System;
using System.IO;

namespace System.Net
{
	// Token: 0x02000414 RID: 1044
	internal class WebConnectionData
	{
		// Token: 0x06002572 RID: 9586 RVA: 0x00073534 File Offset: 0x00071734
		public void Init()
		{
			this.request = null;
			this.StatusCode = 0;
			this.StatusDescription = null;
			this.Headers = null;
			this.stream = null;
		}

		// Token: 0x04001727 RID: 5927
		public HttpWebRequest request;

		// Token: 0x04001728 RID: 5928
		public int StatusCode;

		// Token: 0x04001729 RID: 5929
		public string StatusDescription;

		// Token: 0x0400172A RID: 5930
		public WebHeaderCollection Headers;

		// Token: 0x0400172B RID: 5931
		public Version Version;

		// Token: 0x0400172C RID: 5932
		public Stream stream;

		// Token: 0x0400172D RID: 5933
		public string Challenge;
	}
}
