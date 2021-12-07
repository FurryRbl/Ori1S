using System;

namespace Mono.Xml.Xsl.yyParser
{
	// Token: 0x02000010 RID: 16
	internal class yyUnexpectedEof : yyException
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00005DA8 File Offset: 0x00003FA8
		public yyUnexpectedEof(string message) : base(message)
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00005DB4 File Offset: 0x00003FB4
		public yyUnexpectedEof() : base(string.Empty)
		{
		}
	}
}
