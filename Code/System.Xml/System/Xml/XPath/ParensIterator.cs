using System;

namespace System.Xml.XPath
{
	// Token: 0x02000196 RID: 406
	internal class ParensIterator : BaseIterator
	{
		// Token: 0x060010FC RID: 4348 RVA: 0x0004E694 File Offset: 0x0004C894
		public ParensIterator(BaseIterator iter) : base(iter.NamespaceManager)
		{
			this._iter = iter;
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0004E6AC File Offset: 0x0004C8AC
		private ParensIterator(ParensIterator other) : base(other)
		{
			this._iter = (BaseIterator)other._iter.Clone();
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0004E6CC File Offset: 0x0004C8CC
		public override XPathNodeIterator Clone()
		{
			return new ParensIterator(this);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0004E6D4 File Offset: 0x0004C8D4
		public override bool MoveNextCore()
		{
			return this._iter.MoveNext();
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x0004E6E4 File Offset: 0x0004C8E4
		public override XPathNavigator Current
		{
			get
			{
				return this._iter.Current;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x0004E6F4 File Offset: 0x0004C8F4
		public override int Count
		{
			get
			{
				return this._iter.Count;
			}
		}

		// Token: 0x04000714 RID: 1812
		private BaseIterator _iter;
	}
}
