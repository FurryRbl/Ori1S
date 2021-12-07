using System;

namespace Mono.Xml.XPath.yydebug
{
	// Token: 0x02000005 RID: 5
	internal class yyDebugSimple : yyDebug
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00003C74 File Offset: 0x00001E74
		private void println(string s)
		{
			Console.Error.WriteLine(s);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003C84 File Offset: 0x00001E84
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

		// Token: 0x0600001F RID: 31 RVA: 0x00003CC0 File Offset: 0x00001EC0
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

		// Token: 0x06000020 RID: 32 RVA: 0x00003D00 File Offset: 0x00001F00
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

		// Token: 0x06000021 RID: 33 RVA: 0x00003DF4 File Offset: 0x00001FF4
		public void pop(int state)
		{
			this.println("pop\tstate " + state + "\ton error");
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003E14 File Offset: 0x00002014
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

		// Token: 0x06000023 RID: 35 RVA: 0x00003E54 File Offset: 0x00002054
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

		// Token: 0x06000024 RID: 36 RVA: 0x00003EB4 File Offset: 0x000020B4
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

		// Token: 0x06000025 RID: 37 RVA: 0x00003EEC File Offset: 0x000020EC
		public void accept(object value)
		{
			this.println("accept\tvalue " + value);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003F00 File Offset: 0x00002100
		public void error(string message)
		{
			this.println("error\t" + message);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003F14 File Offset: 0x00002114
		public void reject()
		{
			this.println("reject");
		}
	}
}
