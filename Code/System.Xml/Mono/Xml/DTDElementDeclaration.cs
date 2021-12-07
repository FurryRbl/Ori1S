using System;
using System.Xml.Schema;

namespace Mono.Xml
{
	// Token: 0x020000C8 RID: 200
	internal class DTDElementDeclaration : DTDNode
	{
		// Token: 0x060006E3 RID: 1763 RVA: 0x00026538 File Offset: 0x00024738
		internal DTDElementDeclaration(DTDObjectModel root)
		{
			this.root = root;
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00026548 File Offset: 0x00024748
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x00026550 File Offset: 0x00024750
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0002655C File Offset: 0x0002475C
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x00026564 File Offset: 0x00024764
		public bool IsEmpty
		{
			get
			{
				return this.isEmpty;
			}
			set
			{
				this.isEmpty = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x00026570 File Offset: 0x00024770
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x00026578 File Offset: 0x00024778
		public bool IsAny
		{
			get
			{
				return this.isAny;
			}
			set
			{
				this.isAny = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x00026584 File Offset: 0x00024784
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x0002658C File Offset: 0x0002478C
		public bool IsMixedContent
		{
			get
			{
				return this.isMixedContent;
			}
			set
			{
				this.isMixedContent = value;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x00026598 File Offset: 0x00024798
		public DTDContentModel ContentModel
		{
			get
			{
				if (this.contentModel == null)
				{
					this.contentModel = new DTDContentModel(this.root, this.Name);
				}
				return this.contentModel;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x000265D0 File Offset: 0x000247D0
		public DTDAttListDeclaration Attributes
		{
			get
			{
				return base.Root.AttListDecls[this.Name];
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x000265F4 File Offset: 0x000247F4
		internal XmlSchemaElement CreateXsdElement()
		{
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			base.SetLineInfo(xmlSchemaElement);
			xmlSchemaElement.Name = this.Name;
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaElement.SchemaType = xmlSchemaComplexType;
			if (this.Attributes != null)
			{
				base.SetLineInfo(xmlSchemaComplexType);
				foreach (object obj in this.Attributes.Definitions)
				{
					DTDAttributeDefinition dtdattributeDefinition = (DTDAttributeDefinition)obj;
					xmlSchemaComplexType.Attributes.Add(dtdattributeDefinition.CreateXsdAttribute());
				}
			}
			if (!this.IsEmpty)
			{
				if (this.IsAny)
				{
					xmlSchemaComplexType.Particle = new XmlSchemaAny
					{
						MinOccurs = 0m,
						MaxOccursString = "unbounded"
					};
				}
				else
				{
					if (this.IsMixedContent)
					{
						xmlSchemaComplexType.IsMixed = true;
					}
					xmlSchemaComplexType.Particle = this.ContentModel.CreateXsdParticle();
				}
			}
			return xmlSchemaElement;
		}

		// Token: 0x04000400 RID: 1024
		private DTDObjectModel root;

		// Token: 0x04000401 RID: 1025
		private DTDContentModel contentModel;

		// Token: 0x04000402 RID: 1026
		private string name;

		// Token: 0x04000403 RID: 1027
		private bool isEmpty;

		// Token: 0x04000404 RID: 1028
		private bool isAny;

		// Token: 0x04000405 RID: 1029
		private bool isMixedContent;
	}
}
