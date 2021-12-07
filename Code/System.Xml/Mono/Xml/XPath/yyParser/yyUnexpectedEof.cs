using System;

namespace Mono.Xml.XPath.yyParser
{
	// Token: 0x02000008 RID: 8
	internal class yyUnexpectedEof : yyException
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00003F38 File Offset: 0x00002138
		public yyUnexpectedEof(string message) : base(message)
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003F44 File Offset: 0x00002144
		public yyUnexpectedEof() : base(string.Empty)
		{
		}
	}
}
