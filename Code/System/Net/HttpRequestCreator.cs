using System;

namespace System.Net
{
	// Token: 0x0200031B RID: 795
	internal class HttpRequestCreator : IWebRequestCreate
	{
		// Token: 0x06001BF2 RID: 7154 RVA: 0x00050930 File Offset: 0x0004EB30
		internal HttpRequestCreator()
		{
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x00050938 File Offset: 0x0004EB38
		public WebRequest Create(System.Uri uri)
		{
			return new HttpWebRequest(uri);
		}
	}
}
