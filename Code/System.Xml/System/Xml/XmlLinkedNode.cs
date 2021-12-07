using System;

namespace System.Xml
{
	/// <summary>Gets the node immediately preceding or following this node.</summary>
	// Token: 0x020000FA RID: 250
	public abstract class XmlLinkedNode : XmlNode
	{
		// Token: 0x06000A02 RID: 2562 RVA: 0x00035370 File Offset: 0x00033570
		internal XmlLinkedNode(XmlDocument doc) : base(doc)
		{
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0003537C File Offset: 0x0003357C
		internal bool IsRooted
		{
			get
			{
				for (XmlNode parentNode = this.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
				{
					if (parentNode.NodeType == XmlNodeType.Document)
					{
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>Gets the node immediately following this node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> immediately following this node or null if one does not exist.</returns>
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x000353B4 File Offset: 0x000335B4
		public override XmlNode NextSibling
		{
			get
			{
				return (this.ParentNode != null && this.ParentNode.LastChild != this) ? this.nextSibling : null;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x000353EC File Offset: 0x000335EC
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x000353F4 File Offset: 0x000335F4
		internal XmlLinkedNode NextLinkedSibling
		{
			get
			{
				return this.nextSibling;
			}
			set
			{
				this.nextSibling = value;
			}
		}

		/// <summary>Gets the node immediately preceding this node.</summary>
		/// <returns>The preceding <see cref="T:System.Xml.XmlNode" /> or null if one does not exist.</returns>
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00035400 File Offset: 0x00033600
		public override XmlNode PreviousSibling
		{
			get
			{
				if (this.ParentNode != null)
				{
					XmlNode firstChild = this.ParentNode.FirstChild;
					if (firstChild != this)
					{
						while (firstChild.NextSibling != this)
						{
							if ((firstChild = firstChild.NextSibling) == null)
							{
								goto IL_39;
							}
						}
						return firstChild;
					}
				}
				IL_39:
				return null;
			}
		}

		// Token: 0x04000503 RID: 1283
		private XmlLinkedNode nextSibling;
	}
}
