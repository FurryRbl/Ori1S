using System;

namespace Mono.Xml.Xsl.yydebug
{
	// Token: 0x0200000D RID: 13
	internal class yyDebugSimple : yyDebug
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00005AE4 File Offset: 0x00003CE4
		private void println(string s)
		{
			Console.Error.WriteLine(s);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00005AF4 File Offset: 0x00003CF4
		public void push(int state, object value)
		{
			this.println(string.Concat(new object[]
			{
				"push\tstate ",
				state,
				"\tvalue ",
				value
			}));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00005B30 File Offset: 0x00003D30
		public void lex(int state, int token, string name, object value)
		{
			this.println(string.Concat(new object[]
			{
				"lex\tstate ",
				state,
				"\treading ",
				name,
				"\tvalue ",
				value
			}));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00005B70 File Offset: 0x00003D70
		public void shift(int from, int to, int errorFlag)
		{
			switch (errorFlag)
			{
			case 0:
			case 1:
			case 2:
				this.println(string.Concat(new object[]
				{
					"shift\tfrom state ",
					from,
					" to ",
					to,
					"\t",
					errorFlag,
					" left to recover"
				}));
				break;
			case 3:
				this.println(string.Concat(new object[]
				{
					"shift\tfrom state ",
					from,
					" to ",
					to,
					"\ton error"
				}));
				break;
			default:
				this.println(string.Concat(new object[]
				{
					"shift\tfrom state ",
					from,
					" to ",
					to
				}));
				break;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00005C64 File Offset: 0x00003E64
		public void pop(int state)
		{
			this.println("pop\tstate " + state + "\ton error");
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00005C84 File Offset: 0x00003E84
		public void discard(int state, int token, string name, object value)
		{
			this.println(string.Concat(new object[]
			{
				"discard\tstate ",
				state,
				"\ttoken ",
				name,
				"\tvalue ",
				value
			}));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00005CC4 File Offset: 0x00003EC4
		public void reduce(int from, int to, int rule, string text, int len)
		{
			this.println(string.Concat(new object[]
			{
				"reduce\tstate ",
				from,
				"\tuncover ",
				to,
				"\trule (",
				rule,
				") ",
				text
			}));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00005D24 File Offset: 0x00003F24
		public void shift(int from, int to)
		{
			this.println(string.Concat(new object[]
			{
				"goto\tfrom state ",
				from,
				" to ",
				to
			}));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00005D5C File Offset: 0x00003F5C
		public void accept(object value)
		{
			this.println("accept\tvalue " + value);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00005D70 File Offset: 0x00003F70
		public void error(string message)
		{
			this.println("error\t" + message);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00005D84 File Offset: 0x00003F84
		public void reject()
		{
			this.println("reject");
		}
	}
}
