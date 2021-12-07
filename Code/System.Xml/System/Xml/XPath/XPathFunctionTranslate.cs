using System;
using System.Text;

namespace System.Xml.XPath
{
	// Token: 0x02000157 RID: 343
	internal class XPathFunctionTranslate : XPathFunction
	{
		// Token: 0x06000F88 RID: 3976 RVA: 0x0004A7C0 File Offset: 0x000489C0
		public XPathFunctionTranslate(FunctionArguments args) : base(args)
		{
			if (args == null || args.Tail == null || args.Tail.Tail == null || args.Tail.Tail.Tail != null)
			{
				throw new XPathException("translate takes 3 args");
			}
			this.arg0 = args.Arg;
			this.arg1 = args.Tail.Arg;
			this.arg2 = args.Tail.Tail.Arg;
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0004A848 File Offset: 0x00048A48
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.String;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x0004A84C File Offset: 0x00048A4C
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer && this.arg1.Peer && this.arg2.Peer;
			}
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0004A888 File Offset: 0x00048A88
		public override object Evaluate(BaseIterator iter)
		{
			string text = this.arg0.EvaluateString(iter);
			string text2 = this.arg1.EvaluateString(iter);
			string text3 = this.arg2.EvaluateString(iter);
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			int i = 0;
			int length = text.Length;
			int length2 = text3.Length;
			while (i < length)
			{
				int num = text2.IndexOf(text[i]);
				if (num != -1)
				{
					if (num < length2)
					{
						stringBuilder.Append(text3[num]);
					}
				}
				else
				{
					stringBuilder.Append(text[i]);
				}
				i++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0004A93C File Offset: 0x00048B3C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"string-length(",
				this.arg0.ToString(),
				",",
				this.arg1.ToString(),
				",",
				this.arg2.ToString(),
				")"
			});
		}

		// Token: 0x040006C4 RID: 1732
		private Expression arg0;

		// Token: 0x040006C5 RID: 1733
		private Expression arg1;

		// Token: 0x040006C6 RID: 1734
		private Expression arg2;
	}
}
