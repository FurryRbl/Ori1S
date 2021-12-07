using System;

namespace Mono.Xml.XPath.yydebug
{
	// Token: 0x02000004 RID: 4
	internal interface yyDebug
	{
		// Token: 0x06000012 RID: 18
		void push(int state, object value);

		// Token: 0x06000013 RID: 19
		void lex(int state, int token, string name, object value);

		// Token: 0x06000014 RID: 20
		void shift(int from, int to, int errorFlag);

		// Token: 0x06000015 RID: 21
		void pop(int state);

		// Token: 0x06000016 RID: 22
		void discard(int state, int token, string name, object value);

		// Token: 0x06000017 RID: 23
		void reduce(int from, int to, int rule, string text, int len);

		// Token: 0x06000018 RID: 24
		void shift(int from, int to);

		// Token: 0x06000019 RID: 25
		void accept(object value);

		// Token: 0x0600001A RID: 26
		void error(string message);

		// Token: 0x0600001B RID: 27
		void reject();
	}
}
