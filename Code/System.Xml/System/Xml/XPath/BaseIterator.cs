using System;

namespace System.Xml.XPath
{
	// Token: 0x02000191 RID: 401
	internal abstract class BaseIterator : XPathNodeIterator
	{
		// Token: 0x060010D8 RID: 4312 RVA: 0x0004E2E4 File Offset: 0x0004C4E4
		internal BaseIterator(BaseIterator other)
		{
			this._nsm = other._nsm;
			this.position = other.position;
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0004E304 File Offset: 0x0004C504
		internal BaseIterator(IXmlNamespaceResolver nsm)
		{
			this._nsm = nsm;
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x0004E314 File Offset: 0x0004C514
		// (set) Token: 0x060010DB RID: 4315 RVA: 0x0004E31C File Offset: 0x0004C51C
		public IXmlNamespaceResolver NamespaceManager
		{
			get
			{
				return this._nsm;
			}
			set
			{
				this._nsm = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x0004E328 File Offset: 0x0004C528
		public virtual bool ReverseAxis
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x0004E32C File Offset: 0x0004C52C
		public int ComparablePosition
		{
			get
			{
				if (this.ReverseAxis)
				{
					int num = this.Count - this.CurrentPosition + 1;
					return (num >= 1) ? num : 1;
				}
				return this.CurrentPosition;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x0004E36C File Offset: 0x0004C56C
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0004E374 File Offset: 0x0004C574
		internal void SetPosition(int pos)
		{
			this.position = pos;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0004E380 File Offset: 0x0004C580
		public override bool MoveNext()
		{
			if (!this.MoveNextCore())
			{
				return false;
			}
			this.position++;
			return true;
		}

		// Token: 0x060010E1 RID: 4321
		public abstract bool MoveNextCore();

		// Token: 0x060010E2 RID: 4322 RVA: 0x0004E3A0 File Offset: 0x0004C5A0
		internal XPathNavigator PeekNext()
		{
			XPathNodeIterator xpathNodeIterator = this.Clone();
			return (!xpathNodeIterator.MoveNext()) ? null : xpathNodeIterator.Current;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0004E3CC File Offset: 0x0004C5CC
		public override string ToString()
		{
			if (this.Current != null)
			{
				return string.Concat(new object[]
				{
					this.Current.NodeType.ToString(),
					"[",
					this.CurrentPosition,
					"] : ",
					this.Current.Name,
					" = ",
					this.Current.Value
				});
			}
			return string.Concat(new object[]
			{
				base.GetType().ToString(),
				"[",
				this.CurrentPosition,
				"]"
			});
		}

		// Token: 0x0400070E RID: 1806
		private IXmlNamespaceResolver _nsm;

		// Token: 0x0400070F RID: 1807
		private int position;
	}
}
