using System;
using System.Xml;
using System.Xml.XPath;

namespace Mono.Xml.Xsl
{
	// Token: 0x020000AD RID: 173
	internal class XsltUnparsedEntityUri : XPathFunction
	{
		// Token: 0x060005D6 RID: 1494 RVA: 0x0002405C File Offset: 0x0002225C
		public XsltUnparsedEntityUri(FunctionArguments args) : base(args)
		{
			if (args == null || args.Tail != null)
			{
				throw new XPathException("unparsed-entity-uri takes 1 arg");
			}
			this.arg0 = args.Arg;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00024090 File Offset: 0x00022290
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.String;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00024094 File Offset: 0x00022294
		internal override bool Peer
		{
			get
			{
				return this.arg0.Peer;
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000240A4 File Offset: 0x000222A4
		public override object Evaluate(BaseIterator iter)
		{
			IHasXmlNode hasXmlNode = iter.Current as IHasXmlNode;
			if (hasXmlNode == null)
			{
				return string.Empty;
			}
			XmlNode node = hasXmlNode.GetNode();
			if (node.OwnerDocument == null)
			{
				return string.Empty;
			}
			XmlDocumentType documentType = node.OwnerDocument.DocumentType;
			if (documentType == null)
			{
				return string.Empty;
			}
			XmlEntity xmlEntity = documentType.Entities.GetNamedItem(this.arg0.EvaluateString(iter)) as XmlEntity;
			if (xmlEntity == null)
			{
				return string.Empty;
			}
			return (xmlEntity.SystemId == null) ? string.Empty : xmlEntity.SystemId;
		}

		// Token: 0x040003B4 RID: 948
		private Expression arg0;
	}
}
