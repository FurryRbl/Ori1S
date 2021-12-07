using System;

namespace System.Xml.XPath
{
	// Token: 0x02000186 RID: 390
	internal abstract class NodeTest : NodeSet
	{
		// Token: 0x0600107E RID: 4222 RVA: 0x0004D284 File Offset: 0x0004B484
		public NodeTest(Axes axis)
		{
			this._axis = new AxisSpecifier(axis);
		}

		// Token: 0x0600107F RID: 4223
		public abstract bool Match(IXmlNamespaceResolver nsm, XPathNavigator nav);

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x0004D298 File Offset: 0x0004B498
		public AxisSpecifier Axis
		{
			get
			{
				return this._axis;
			}
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0004D2A0 File Offset: 0x0004B4A0
		public override object Evaluate(BaseIterator iter)
		{
			BaseIterator iter2 = this._axis.Evaluate(iter);
			return new AxisIterator(iter2, this);
		}

		// Token: 0x06001082 RID: 4226
		public abstract void GetInfo(out string name, out string ns, out XPathNodeType nodetype, IXmlNamespaceResolver nsm);

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x0004D2C4 File Offset: 0x0004B4C4
		public override bool RequireSorting
		{
			get
			{
				switch (this._axis.Axis)
				{
				case Axes.Ancestor:
				case Axes.AncestorOrSelf:
				case Axes.Attribute:
				case Axes.Namespace:
				case Axes.Preceding:
				case Axes.PrecedingSibling:
					return true;
				}
				return false;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x0004D31C File Offset: 0x0004B51C
		internal override bool Peer
		{
			get
			{
				switch (this._axis.Axis)
				{
				case Axes.Ancestor:
				case Axes.AncestorOrSelf:
				case Axes.Descendant:
				case Axes.DescendantOrSelf:
				case Axes.Following:
				case Axes.Preceding:
					return false;
				}
				return true;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x0004D370 File Offset: 0x0004B570
		internal override bool Subtree
		{
			get
			{
				switch (this._axis.Axis)
				{
				case Axes.Ancestor:
				case Axes.AncestorOrSelf:
				case Axes.Following:
				case Axes.FollowingSibling:
				case Axes.Parent:
				case Axes.Preceding:
				case Axes.PrecedingSibling:
					return false;
				}
				return true;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x0004D3C8 File Offset: 0x0004B5C8
		internal override XPathNodeType EvaluatedNodeType
		{
			get
			{
				return this._axis.NodeType;
			}
		}

		// Token: 0x040006FB RID: 1787
		protected AxisSpecifier _axis;
	}
}
