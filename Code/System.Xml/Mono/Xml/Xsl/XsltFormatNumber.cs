using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl
{
	// Token: 0x020000A8 RID: 168
	internal class XsltFormatNumber : XPathFunction
	{
		// Token: 0x060005BE RID: 1470 RVA: 0x00023884 File Offset: 0x00021A84
		public XsltFormatNumber(FunctionArguments args, IStaticXsltContext ctx) : base(args)
		{
			if (args == null || args.Tail == null || (args.Tail.Tail != null && args.Tail.Tail.Tail != null))
			{
				throw new XPathException("format-number takes 2 or 3 args");
			}
			this.arg0 = args.Arg;
			this.arg1 = args.Tail.Arg;
			if (args.Tail.Tail != null)
			{
				this.arg2 = args.Tail.Tail.Arg;
				this.ctx = ctx;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x00023924 File Offset: 0x00021B24
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.String;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00023928 File Offset: 0x00021B28
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer && this.arg1.Peer && (this.arg2 == null || this.arg2.Peer);
			}
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00023974 File Offset: 0x00021B74
		public override object Evaluate(BaseIterator iter)
		{
			double number = this.arg0.EvaluateNumber(iter);
			string pattern = this.arg1.EvaluateString(iter);
			XmlQualifiedName name = XmlQualifiedName.Empty;
			if (this.arg2 != null)
			{
				name = XslNameUtil.FromString(this.arg2.EvaluateString(iter), this.ctx);
			}
			object result;
			try
			{
				result = ((XsltCompiledContext)iter.NamespaceManager).Processor.CompiledStyle.LookupDecimalFormat(name).FormatNumber(number, pattern);
			}
			catch (ArgumentException ex)
			{
				throw new XsltException(ex.Message, ex, iter.Current);
			}
			return result;
		}

		// Token: 0x040003A7 RID: 935
		private Expression arg0;

		// Token: 0x040003A8 RID: 936
		private Expression arg1;

		// Token: 0x040003A9 RID: 937
		private Expression arg2;

		// Token: 0x040003AA RID: 938
		private IStaticXsltContext ctx;
	}
}
