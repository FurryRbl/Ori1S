using System;
using System.Text;

namespace System.Xml.XPath
{
	// Token: 0x02000156 RID: 342
	internal class XPathFunctionNormalizeSpace : XPathFunction
	{
		// Token: 0x06000F83 RID: 3971 RVA: 0x0004A664 File Offset: 0x00048864
		public XPathFunctionNormalizeSpace(FunctionArguments args) : base(args)
		{
			if (args != null)
			{
				this.arg0 = args.Arg;
				if (args.Tail != null)
				{
					throw new XPathException("normalize-space takes 1 or zero args");
				}
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x0004A698 File Offset: 0x00048898
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.String;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0004A69C File Offset: 0x0004889C
		internal override bool Peer
		{
			get
			{
				return this.arg0 == null || this.arg0.Peer;
			}
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0004A6BC File Offset: 0x000488BC
		public override object Evaluate(BaseIterator iter)
		{
			string text;
			if (this.arg0 != null)
			{
				text = this.arg0.EvaluateString(iter);
			}
			else
			{
				text = iter.Current.Value;
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			foreach (char c in text)
			{
				if (c == ' ' || c == '\t' || c == '\r' || c == '\n')
				{
					flag = true;
				}
				else
				{
					if (flag)
					{
						flag = false;
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append(' ');
						}
					}
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0004A774 File Offset: 0x00048974
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"normalize-space(",
				(this.arg0 == null) ? string.Empty : this.arg0.ToString(),
				")"
			});
		}

		// Token: 0x040006C3 RID: 1731
		private Expression arg0;
	}
}
