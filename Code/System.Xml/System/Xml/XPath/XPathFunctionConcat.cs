using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace System.Xml.XPath
{
	// Token: 0x0200014F RID: 335
	internal class XPathFunctionConcat : XPathFunction
	{
		// Token: 0x06000F60 RID: 3936 RVA: 0x00049D68 File Offset: 0x00047F68
		public XPathFunctionConcat(FunctionArguments args) : base(args)
		{
			if (args == null || args.Tail == null)
			{
				throw new XPathException("concat takes 2 or more args");
			}
			args.ToArrayList(this.rgs = new ArrayList());
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x00049DAC File Offset: 0x00047FAC
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.String;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x00049DB0 File Offset: 0x00047FB0
		internal override bool Peer
		{
			get
			{
				for (int i = 0; i < this.rgs.Count; i++)
				{
					if (!((Expression)this.rgs[i]).Peer)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00049DF8 File Offset: 0x00047FF8
		public override object Evaluate(BaseIterator iter)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int count = this.rgs.Count;
			for (int i = 0; i < count; i++)
			{
				stringBuilder.Append(((Expression)this.rgs[i]).EvaluateString(iter));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x00049E50 File Offset: 0x00048050
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("concat(");
			for (int i = 0; i < this.rgs.Count - 1; i++)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}", new object[]
				{
					this.rgs[i].ToString()
				});
				stringBuilder.Append(',');
			}
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				this.rgs[this.rgs.Count - 1].ToString()
			});
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		// Token: 0x040006B6 RID: 1718
		private ArrayList rgs;
	}
}
