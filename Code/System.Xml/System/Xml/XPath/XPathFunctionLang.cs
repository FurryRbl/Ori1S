using System;
using System.Globalization;

namespace System.Xml.XPath
{
	// Token: 0x0200015D RID: 349
	internal class XPathFunctionLang : XPathFunction
	{
		// Token: 0x06000FA4 RID: 4004 RVA: 0x0004AB78 File Offset: 0x00048D78
		public XPathFunctionLang(FunctionArguments args) : base(args)
		{
			if (args == null || args.Tail != null)
			{
				throw new XPathException("lang takes one arg");
			}
			this.arg0 = args.Arg;
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x0004ABAC File Offset: 0x00048DAC
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x0004ABB0 File Offset: 0x00048DB0
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer;
			}
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0004ABC0 File Offset: 0x00048DC0
		public override object Evaluate(BaseIterator iter)
		{
			return this.EvaluateBoolean(iter);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0004ABD0 File Offset: 0x00048DD0
		public override bool EvaluateBoolean(BaseIterator iter)
		{
			string a = this.arg0.EvaluateString(iter).ToLower(CultureInfo.InvariantCulture);
			string text = iter.Current.XmlLang.ToLower(CultureInfo.InvariantCulture);
			return a == text || a == text.Split(new char[]
			{
				'-'
			})[0];
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0004AC34 File Offset: 0x00048E34
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"lang(",
				this.arg0.ToString(),
				")"
			});
		}

		// Token: 0x040006C9 RID: 1737
		private Expression arg0;
	}
}
