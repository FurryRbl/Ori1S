using System;

namespace Mono.Xml.Xsl.yydebug
{
	// Token: 0x0200000C RID: 12
	internal interface yyDebug
	{
		// Token: 0x06000040 RID: 64
		void push(int state, object value);

		// Token: 0x06000041 RID: 65
		void lex(int state, int token, string name, object value);

		// Token: 0x06000042 RID: 66
		void shift(int from, int to, int errorFlag);

		// Token: 0x06000043 RID: 67
		void pop(int state);

		// Token: 0x06000044 RID: 68
		void discard(int state, int token, string name, object value);

		// Token: 0x06000045 RID: 69
		void reduce(int from, int to, int rule, string text, int len);

		// Token: 0x06000046 RID: 70
		void shift(int from, int to);

		// Token: 0x06000047 RID: 71
		void accept(object value);

		// Token: 0x06000048 RID: 72
		void error(string message);

		// Token: 0x06000049 RID: 73
		void reject();
	}
}
