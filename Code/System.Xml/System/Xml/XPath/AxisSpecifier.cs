using System;

namespace System.Xml.XPath
{
	// Token: 0x02000185 RID: 389
	internal class AxisSpecifier
	{
		// Token: 0x06001079 RID: 4217 RVA: 0x0004D0E4 File Offset: 0x0004B2E4
		public AxisSpecifier(Axes axis)
		{
			this._axis = axis;
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x0004D0F4 File Offset: 0x0004B2F4
		public XPathNodeType NodeType
		{
			get
			{
				Axes axis = this._axis;
				if (axis == Axes.Attribute)
				{
					return XPathNodeType.Attribute;
				}
				if (axis != Axes.Namespace)
				{
					return XPathNodeType.Element;
				}
				return XPathNodeType.Namespace;
			}
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0004D120 File Offset: 0x0004B320
		public override string ToString()
		{
			switch (this._axis)
			{
			case Axes.Ancestor:
				return "ancestor";
			case Axes.AncestorOrSelf:
				return "ancestor-or-self";
			case Axes.Attribute:
				return "attribute";
			case Axes.Child:
				return "child";
			case Axes.Descendant:
				return "descendant";
			case Axes.DescendantOrSelf:
				return "descendant-or-self";
			case Axes.Following:
				return "following";
			case Axes.FollowingSibling:
				return "following-sibling";
			case Axes.Namespace:
				return "namespace";
			case Axes.Parent:
				return "parent";
			case Axes.Preceding:
				return "preceding";
			case Axes.PrecedingSibling:
				return "preceding-sibling";
			case Axes.Self:
				return "self";
			default:
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x0004D1C8 File Offset: 0x0004B3C8
		public Axes Axis
		{
			get
			{
				return this._axis;
			}
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0004D1D0 File Offset: 0x0004B3D0
		public BaseIterator Evaluate(BaseIterator iter)
		{
			switch (this._axis)
			{
			case Axes.Ancestor:
				return new AncestorIterator(iter);
			case Axes.AncestorOrSelf:
				return new AncestorOrSelfIterator(iter);
			case Axes.Attribute:
				return new AttributeIterator(iter);
			case Axes.Child:
				return new ChildIterator(iter);
			case Axes.Descendant:
				return new DescendantIterator(iter);
			case Axes.DescendantOrSelf:
				return new DescendantOrSelfIterator(iter);
			case Axes.Following:
				return new FollowingIterator(iter);
			case Axes.FollowingSibling:
				return new FollowingSiblingIterator(iter);
			case Axes.Namespace:
				return new NamespaceIterator(iter);
			case Axes.Parent:
				return new ParentIterator(iter);
			case Axes.Preceding:
				return new PrecedingIterator(iter);
			case Axes.PrecedingSibling:
				return new PrecedingSiblingIterator(iter);
			case Axes.Self:
				return new SelfIterator(iter);
			default:
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x040006FA RID: 1786
		protected Axes _axis;
	}
}
