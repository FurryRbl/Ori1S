using System;

namespace System.Xml.XPath
{
	// Token: 0x02000148 RID: 328
	internal class XPathFunctionPosition : XPathFunction
	{
		// Token: 0x06000F39 RID: 3897 RVA: 0x0004980C File Offset: 0x00047A0C
		public XPathFunctionPosition(FunctionArguments args) : base(args)
		{
			if (args != null)
			{
				throw new XPathException("position takes 0 args");
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x00049828 File Offset: 0x00047A28
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Number;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x0004982C File Offset: 0x00047A2C
		internal override bool Peer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00049830 File Offset: 0x00047A30
		public override object Evaluate(BaseIterator iter)
		{
			return (double)iter.CurrentPosition;
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00049840 File Offset: 0x00047A40
		public override string ToString()
		{
			return "position()";
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00049848 File Offset: 0x00047A48
		internal override bool IsPositional
		{
			get
			{
				return true;
			}
		}
	}
}
