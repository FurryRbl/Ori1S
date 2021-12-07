using System;

namespace Mono.Xml.Xsl.yyParser
{
	// Token: 0x02000011 RID: 17
	internal interface yyInput
	{
		// Token: 0x0600005A RID: 90
		bool advance();

		// Token: 0x0600005B RID: 91
		int token();

		// Token: 0x0600005C RID: 92
		object value();
	}
}
