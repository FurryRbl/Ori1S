using System;

namespace Mono.Xml
{
	// Token: 0x020000F1 RID: 241
	internal class SmallXmlParserException : SystemException
	{
		// Token: 0x06000C4D RID: 3149 RVA: 0x00038348 File Offset: 0x00036548
		public SmallXmlParserException(string msg, int line, int column) : base(string.Format("{0}. At ({1},{2})", msg, line, column))
		{
			this.line = line;
			this.column = column;
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x00038378 File Offset: 0x00036578
		public int Line
		{
			get
			{
				return this.line;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00038380 File Offset: 0x00036580
		public int Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x04000357 RID: 855
		private int line;

		// Token: 0x04000358 RID: 856
		private int column;
	}
}
