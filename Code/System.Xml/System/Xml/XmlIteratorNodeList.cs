using System;
using System.Collections;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x0200010B RID: 267
	internal class XmlIteratorNodeList : XmlNodeList
	{
		// Token: 0x06000AA2 RID: 2722 RVA: 0x00037E78 File Offset: 0x00036078
		public XmlIteratorNodeList(XPathNodeIterator iter)
		{
			this.source = iter;
			this.iterator = iter.Clone();
			this.list = new ArrayList();
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x00037EAC File Offset: 0x000360AC
		public override int Count
		{
			get
			{
				return this.iterator.Count;
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00037EBC File Offset: 0x000360BC
		public override IEnumerator GetEnumerator()
		{
			if (this.finished)
			{
				return this.list.GetEnumerator();
			}
			return new XmlIteratorNodeList.XPathNodeIteratorNodeListIterator(this.source);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00037EEC File Offset: 0x000360EC
		public override XmlNode Item(int index)
		{
			if (index < 0)
			{
				return null;
			}
			if (index < this.list.Count)
			{
				return (XmlNode)this.list[index];
			}
			index++;
			while (this.iterator.CurrentPosition < index)
			{
				if (!this.iterator.MoveNext())
				{
					this.finished = true;
					return null;
				}
				this.list.Add(((IHasXmlNode)this.iterator.Current).GetNode());
			}
			return (XmlNode)this.list[index - 1];
		}

		// Token: 0x0400053D RID: 1341
		private XPathNodeIterator source;

		// Token: 0x0400053E RID: 1342
		private XPathNodeIterator iterator;

		// Token: 0x0400053F RID: 1343
		private ArrayList list;

		// Token: 0x04000540 RID: 1344
		private bool finished;

		// Token: 0x0200010C RID: 268
		private class XPathNodeIteratorNodeListIterator : IEnumerator
		{
			// Token: 0x06000AA6 RID: 2726 RVA: 0x00037F90 File Offset: 0x00036190
			public XPathNodeIteratorNodeListIterator(XPathNodeIterator source)
			{
				this.source = source;
				this.Reset();
			}

			// Token: 0x06000AA7 RID: 2727 RVA: 0x00037FA8 File Offset: 0x000361A8
			public bool MoveNext()
			{
				return this.iter.MoveNext();
			}

			// Token: 0x170002FE RID: 766
			// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x00037FB8 File Offset: 0x000361B8
			public object Current
			{
				get
				{
					return ((IHasXmlNode)this.iter.Current).GetNode();
				}
			}

			// Token: 0x06000AA9 RID: 2729 RVA: 0x00037FD0 File Offset: 0x000361D0
			public void Reset()
			{
				this.iter = this.source.Clone();
			}

			// Token: 0x04000541 RID: 1345
			private XPathNodeIterator iter;

			// Token: 0x04000542 RID: 1346
			private XPathNodeIterator source;
		}
	}
}
