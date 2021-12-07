using System;

namespace System.Xml.XPath
{
	// Token: 0x02000183 RID: 387
	internal class ExprRoot : NodeSet
	{
		// Token: 0x06001074 RID: 4212 RVA: 0x0004D084 File Offset: 0x0004B284
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0004D08C File Offset: 0x0004B28C
		public override object Evaluate(BaseIterator iter)
		{
			if (iter.CurrentPosition == 0)
			{
				iter = (BaseIterator)iter.Clone();
				iter.MoveNext();
			}
			XPathNavigator xpathNavigator = iter.Current.Clone();
			xpathNavigator.MoveToRoot();
			return new SelfIterator(xpathNavigator, iter.NamespaceManager);
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0004D0D8 File Offset: 0x0004B2D8
		internal override XPathNodeType EvaluatedNodeType
		{
			get
			{
				return XPathNodeType.Root;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x0004D0DC File Offset: 0x0004B2DC
		internal override bool Peer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x0004D0E0 File Offset: 0x0004B2E0
		internal override bool Subtree
		{
			get
			{
				return false;
			}
		}
	}
}
