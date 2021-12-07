using System;

namespace System.Xml.XPath
{
	// Token: 0x0200017F RID: 383
	internal abstract class NodeSet : Expression
	{
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0004CB98 File Offset: 0x0004AD98
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001057 RID: 4183
		internal abstract bool Subtree { get; }
	}
}
