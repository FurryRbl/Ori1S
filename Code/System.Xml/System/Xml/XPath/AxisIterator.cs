using System;

namespace System.Xml.XPath
{
	// Token: 0x020001A3 RID: 419
	internal class AxisIterator : BaseIterator
	{
		// Token: 0x0600113C RID: 4412 RVA: 0x0004F17C File Offset: 0x0004D37C
		public AxisIterator(BaseIterator iter, NodeTest test) : base(iter.NamespaceManager)
		{
			this._iter = iter;
			this._test = test;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0004F198 File Offset: 0x0004D398
		private AxisIterator(AxisIterator other) : base(other)
		{
			this._iter = (BaseIterator)other._iter.Clone();
			this._test = other._test;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0004F1C4 File Offset: 0x0004D3C4
		public override XPathNodeIterator Clone()
		{
			return new AxisIterator(this);
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0004F1CC File Offset: 0x0004D3CC
		public override bool MoveNextCore()
		{
			while (this._iter.MoveNext())
			{
				if (this._test.Match(base.NamespaceManager, this._iter.Current))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x0004F214 File Offset: 0x0004D414
		public override XPathNavigator Current
		{
			get
			{
				return (this.CurrentPosition != 0) ? this._iter.Current : null;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x0004F234 File Offset: 0x0004D434
		public override bool ReverseAxis
		{
			get
			{
				return this._iter.ReverseAxis;
			}
		}

		// Token: 0x04000728 RID: 1832
		private BaseIterator _iter;

		// Token: 0x04000729 RID: 1833
		private NodeTest _test;
	}
}
