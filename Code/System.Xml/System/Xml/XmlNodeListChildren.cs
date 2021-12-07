using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x02000108 RID: 264
	internal class XmlNodeListChildren : XmlNodeList
	{
		// Token: 0x06000A96 RID: 2710 RVA: 0x00037C28 File Offset: 0x00035E28
		public XmlNodeListChildren(IHasXmlChildNode parent)
		{
			this.parent = parent;
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x00037C38 File Offset: 0x00035E38
		public override int Count
		{
			get
			{
				int num = 0;
				if (this.parent.LastLinkedChild != null)
				{
					XmlLinkedNode nextLinkedSibling = this.parent.LastLinkedChild.NextLinkedSibling;
					num = 1;
					while (!object.ReferenceEquals(nextLinkedSibling, this.parent.LastLinkedChild))
					{
						nextLinkedSibling = nextLinkedSibling.NextLinkedSibling;
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00037C94 File Offset: 0x00035E94
		public override IEnumerator GetEnumerator()
		{
			return new XmlNodeListChildren.Enumerator(this.parent);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00037CA4 File Offset: 0x00035EA4
		public override XmlNode Item(int index)
		{
			XmlNode result = null;
			if (this.Count <= index)
			{
				return null;
			}
			if (index >= 0 && this.parent.LastLinkedChild != null)
			{
				XmlLinkedNode nextLinkedSibling = this.parent.LastLinkedChild.NextLinkedSibling;
				int num = 0;
				while (num < index && !object.ReferenceEquals(nextLinkedSibling, this.parent.LastLinkedChild))
				{
					nextLinkedSibling = nextLinkedSibling.NextLinkedSibling;
					num++;
				}
				if (num == index)
				{
					result = nextLinkedSibling;
				}
			}
			return result;
		}

		// Token: 0x04000538 RID: 1336
		private IHasXmlChildNode parent;

		// Token: 0x02000109 RID: 265
		private class Enumerator : IEnumerator
		{
			// Token: 0x06000A9A RID: 2714 RVA: 0x00037D24 File Offset: 0x00035F24
			internal Enumerator(IHasXmlChildNode parent)
			{
				this.currentChild = null;
				this.parent = parent;
				this.passedLastNode = false;
			}

			// Token: 0x170002FB RID: 763
			// (get) Token: 0x06000A9B RID: 2715 RVA: 0x00037D44 File Offset: 0x00035F44
			public virtual object Current
			{
				get
				{
					if (this.currentChild == null || this.parent.LastLinkedChild == null || this.passedLastNode)
					{
						throw new InvalidOperationException();
					}
					return this.currentChild;
				}
			}

			// Token: 0x06000A9C RID: 2716 RVA: 0x00037D84 File Offset: 0x00035F84
			public virtual bool MoveNext()
			{
				bool result = true;
				if (this.parent.LastLinkedChild == null)
				{
					result = false;
				}
				else if (this.currentChild == null)
				{
					this.currentChild = this.parent.LastLinkedChild.NextLinkedSibling;
				}
				else if (object.ReferenceEquals(this.currentChild, this.parent.LastLinkedChild))
				{
					result = false;
					this.passedLastNode = true;
				}
				else
				{
					this.currentChild = this.currentChild.NextLinkedSibling;
				}
				return result;
			}

			// Token: 0x06000A9D RID: 2717 RVA: 0x00037E0C File Offset: 0x0003600C
			public virtual void Reset()
			{
				this.currentChild = null;
			}

			// Token: 0x04000539 RID: 1337
			private IHasXmlChildNode parent;

			// Token: 0x0400053A RID: 1338
			private XmlLinkedNode currentChild;

			// Token: 0x0400053B RID: 1339
			private bool passedLastNode;
		}
	}
}
