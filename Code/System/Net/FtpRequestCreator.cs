using System;

namespace System.Net
{
	// Token: 0x02000309 RID: 777
	internal class FtpRequestCreator : IWebRequestCreate
	{
		// Token: 0x06001AD7 RID: 6871 RVA: 0x0004BA90 File Offset: 0x00049C90
		public WebRequest Create(System.Uri uri)
		{
			return new FtpWebRequest(uri);
		}
	}
}
