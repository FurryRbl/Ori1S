using System;

namespace System.Xml.XPath
{
	// Token: 0x02000147 RID: 327
	internal class XPathFunctionLast : XPathFunction
	{
		// Token: 0x06000F33 RID: 3891 RVA: 0x000497CC File Offset: 0x000479CC
		public XPathFunctionLast(FunctionArguments args) : base(args)
		{
			if (args != null)
			{
				throw new XPathException("last takes 0 args");
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x000497E8 File Offset: 0x000479E8
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Number;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x000497EC File Offset: 0x000479EC
		internal override bool Peer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x000497F0 File Offset: 0x000479F0
		public override object Evaluate(BaseIterator iter)
		{
			return (double)iter.Count;
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x00049800 File Offset: 0x00047A00
		public override string ToString()
		{
			return "last()";
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x00049808 File Offset: 0x00047A08
		internal override bool IsPositional
		{
			get
			{
				return true;
			}
		}
	}
}
