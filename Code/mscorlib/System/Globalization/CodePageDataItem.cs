using System;

namespace System.Globalization
{
	// Token: 0x0200020B RID: 523
	[Serializable]
	internal sealed class CodePageDataItem
	{
		// Token: 0x060019C9 RID: 6601 RVA: 0x00060604 File Offset: 0x0005E804
		private CodePageDataItem()
		{
		}

		// Token: 0x04000976 RID: 2422
		private string m_bodyName;

		// Token: 0x04000977 RID: 2423
		private int m_codePage;

		// Token: 0x04000978 RID: 2424
		private int m_dataIndex;

		// Token: 0x04000979 RID: 2425
		private string m_description;

		// Token: 0x0400097A RID: 2426
		private uint m_flags;

		// Token: 0x0400097B RID: 2427
		private string m_headerName;

		// Token: 0x0400097C RID: 2428
		private int m_uiFamilyCodePage;

		// Token: 0x0400097D RID: 2429
		private string m_webName;
	}
}
