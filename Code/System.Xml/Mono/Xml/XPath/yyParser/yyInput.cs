using System;

namespace Mono.Xml.XPath.yyParser
{
	// Token: 0x02000009 RID: 9
	internal interface yyInput
	{
		// Token: 0x0600002C RID: 44
		bool advance();

		// Token: 0x0600002D RID: 45
		int token();

		// Token: 0x0600002E RID: 46
		object value();
	}
}
