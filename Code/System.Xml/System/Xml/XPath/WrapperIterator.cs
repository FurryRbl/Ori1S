using System;

namespace System.Xml.XPath
{
	// Token: 0x02000192 RID: 402
	internal class WrapperIterator : BaseIterator
	{
		// Token: 0x060010E4 RID: 4324 RVA: 0x0004E484 File Offset: 0x0004C684
		public WrapperIterator(XPathNodeIterator iter, IXmlNamespaceResolver nsm) : base(nsm)
		{
			this.iter = iter;
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0004E494 File Offset: 0x0004C694
		private WrapperIterator(WrapperIterator other) : base(other)
		{
			this.iter = other.iter.Clone();
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0004E4B0 File Offset: 0x0004C6B0
		public override XPathNodeIterator Clone()
		{
			return new WrapperIterator(this);
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0004E4B8 File Offset: 0x0004C6B8
		public override bool MoveNextCore()
		{
			return this.iter.MoveNext();
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x0004E4C8 File Offset: 0x0004C6C8
		public override XPathNavigator Current
		{
			get
			{
				return this.iter.Current;
			}
		}

		// Token: 0x04000710 RID: 1808
		private XPathNodeIterator iter;
	}
}
