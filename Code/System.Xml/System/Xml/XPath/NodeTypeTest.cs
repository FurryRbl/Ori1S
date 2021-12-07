using System;

namespace System.Xml.XPath
{
	// Token: 0x02000187 RID: 391
	internal class NodeTypeTest : NodeTest
	{
		// Token: 0x06001087 RID: 4231 RVA: 0x0004D3D8 File Offset: 0x0004B5D8
		public NodeTypeTest(Axes axis) : base(axis)
		{
			this.type = this._axis.NodeType;
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0004D3F4 File Offset: 0x0004B5F4
		public NodeTypeTest(Axes axis, XPathNodeType type) : base(axis)
		{
			this.type = type;
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0004D404 File Offset: 0x0004B604
		public NodeTypeTest(Axes axis, XPathNodeType type, string param) : base(axis)
		{
			this.type = type;
			this._param = param;
			if (param != null && type != XPathNodeType.ProcessingInstruction)
			{
				throw new XPathException("No argument allowed for " + NodeTypeTest.ToString(type) + "() test");
			}
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0004D444 File Offset: 0x0004B644
		internal NodeTypeTest(NodeTypeTest other, Axes axis) : base(axis)
		{
			this.type = other.type;
			this._param = other._param;
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0004D468 File Offset: 0x0004B668
		public override string ToString()
		{
			string text = NodeTypeTest.ToString(this.type);
			if (this.type == XPathNodeType.ProcessingInstruction && this._param != null)
			{
				text = text + "('" + this._param + "')";
			}
			else
			{
				text += "()";
			}
			return this._axis.ToString() + "::" + text;
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x0004D4D8 File Offset: 0x0004B6D8
		private static string ToString(XPathNodeType type)
		{
			switch (type)
			{
			case XPathNodeType.Element:
			case XPathNodeType.Attribute:
			case XPathNodeType.Namespace:
			case XPathNodeType.All:
				return "node";
			case XPathNodeType.Text:
				return "text";
			case XPathNodeType.ProcessingInstruction:
				return "processing-instruction";
			case XPathNodeType.Comment:
				return "comment";
			}
			return "node-type [" + type.ToString() + "]";
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0004D54C File Offset: 0x0004B74C
		public override bool Match(IXmlNamespaceResolver nsm, XPathNavigator nav)
		{
			XPathNodeType nodeType = nav.NodeType;
			switch (this.type)
			{
			case XPathNodeType.Text:
				switch (nodeType)
				{
				case XPathNodeType.Text:
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
					return true;
				default:
					return false;
				}
				break;
			case XPathNodeType.ProcessingInstruction:
				return nodeType == XPathNodeType.ProcessingInstruction && (this._param == null || !(nav.Name != this._param));
			case XPathNodeType.All:
				return true;
			}
			return this.type == nodeType;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0004D5E4 File Offset: 0x0004B7E4
		public override void GetInfo(out string name, out string ns, out XPathNodeType nodetype, IXmlNamespaceResolver nsm)
		{
			name = this._param;
			ns = null;
			nodetype = this.type;
		}

		// Token: 0x040006FC RID: 1788
		public readonly XPathNodeType type;

		// Token: 0x040006FD RID: 1789
		protected string _param;
	}
}
