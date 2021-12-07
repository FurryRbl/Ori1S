using System;

namespace System.Net
{
	// Token: 0x02000303 RID: 771
	internal class FileWebRequestCreator : IWebRequestCreate
	{
		// Token: 0x06001A77 RID: 6775 RVA: 0x0004A8F4 File Offset: 0x00048AF4
		internal FileWebRequestCreator()
		{
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x0004A8FC File Offset: 0x00048AFC
		public WebRequest Create(System.Uri uri)
		{
			return new FileWebRequest(uri);
		}
	}
}
