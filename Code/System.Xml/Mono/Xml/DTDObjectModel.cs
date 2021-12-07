using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Mono.Xml2;

namespace Mono.Xml
{
	// Token: 0x020000BE RID: 190
	internal class DTDObjectModel
	{
		// Token: 0x06000681 RID: 1665 RVA: 0x0002587C File Offset: 0x00023A7C
		public DTDObjectModel(XmlNameTable nameTable)
		{
			this.nameTable = nameTable;
			this.elementDecls = new DTDElementDeclarationCollection(this);
			this.attListDecls = new DTDAttListDeclarationCollection(this);
			this.entityDecls = new DTDEntityDeclarationCollection(this);
			this.peDecls = new DTDParameterEntityDeclarationCollection(this);
			this.notationDecls = new DTDNotationDeclarationCollection(this);
			this.factory = new DTDAutomataFactory(this);
			this.validationErrors = new ArrayList();
			this.externalResources = new Hashtable();
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x000258F4 File Offset: 0x00023AF4
		// (set) Token: 0x06000683 RID: 1667 RVA: 0x000258FC File Offset: 0x00023AFC
		public string BaseURI
		{
			get
			{
				return this.baseURI;
			}
			set
			{
				this.baseURI = value;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x00025908 File Offset: 0x00023B08
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x00025910 File Offset: 0x00023B10
		public bool IsStandalone
		{
			get
			{
				return this.isStandalone;
			}
			set
			{
				this.isStandalone = value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0002591C File Offset: 0x00023B1C
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x00025924 File Offset: 0x00023B24
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

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x00025930 File Offset: 0x00023B30
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00025938 File Offset: 0x00023B38
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x00025940 File Offset: 0x00023B40
		public string PublicId
		{
			get
			{
				return this.publicId;
			}
			set
			{
				this.publicId = value;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0002594C File Offset: 0x00023B4C
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x00025954 File Offset: 0x00023B54
		public string SystemId
		{
			get
			{
				return this.systemId;
			}
			set
			{
				this.systemId = value;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00025960 File Offset: 0x00023B60
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x00025968 File Offset: 0x00023B68
		public string InternalSubset
		{
			get
			{
				return this.intSubset;
			}
			set
			{
				this.intSubset = value;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00025974 File Offset: 0x00023B74
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x0002597C File Offset: 0x00023B7C
		public bool InternalSubsetHasPEReference
		{
			get
			{
				return this.intSubsetHasPERef;
			}
			set
			{
				this.intSubsetHasPERef = value;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00025988 File Offset: 0x00023B88
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x00025990 File Offset: 0x00023B90
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
			set
			{
				this.lineNumber = value;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0002599C File Offset: 0x00023B9C
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x000259A4 File Offset: 0x00023BA4
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
			set
			{
				this.linePosition = value;
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000259B0 File Offset: 0x00023BB0
		internal XmlSchema CreateXsdSchema()
		{
			XmlSchema xmlSchema = new XmlSchema();
			xmlSchema.SourceUri = this.BaseURI;
			xmlSchema.LineNumber = this.LineNumber;
			xmlSchema.LinePosition = this.LinePosition;
			foreach (DTDNode dtdnode in this.ElementDecls.Values)
			{
				DTDElementDeclaration dtdelementDeclaration = (DTDElementDeclaration)dtdnode;
				xmlSchema.Items.Add(dtdelementDeclaration.CreateXsdElement());
			}
			return xmlSchema;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00025A54 File Offset: 0x00023C54
		public string ResolveEntity(string name)
		{
			DTDEntityDeclaration dtdentityDeclaration = this.EntityDecls[name];
			if (dtdentityDeclaration == null)
			{
				this.AddError(new XmlSchemaException("Required entity was not found.", this.LineNumber, this.LinePosition, null, this.BaseURI, null));
				return " ";
			}
			return dtdentityDeclaration.EntityValue;
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00025AA4 File Offset: 0x00023CA4
		internal XmlResolver Resolver
		{
			get
			{
				return this.resolver;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x00025AAC File Offset: 0x00023CAC
		public XmlResolver XmlResolver
		{
			set
			{
				this.resolver = value;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00025AB8 File Offset: 0x00023CB8
		internal Hashtable ExternalResources
		{
			get
			{
				return this.externalResources;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00025AC0 File Offset: 0x00023CC0
		public DTDAutomataFactory Factory
		{
			get
			{
				return this.factory;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00025AC8 File Offset: 0x00023CC8
		public DTDElementDeclaration RootElement
		{
			get
			{
				return this.ElementDecls[this.Name];
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00025ADC File Offset: 0x00023CDC
		public DTDElementDeclarationCollection ElementDecls
		{
			get
			{
				return this.elementDecls;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00025AE4 File Offset: 0x00023CE4
		public DTDAttListDeclarationCollection AttListDecls
		{
			get
			{
				return this.attListDecls;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00025AEC File Offset: 0x00023CEC
		public DTDEntityDeclarationCollection EntityDecls
		{
			get
			{
				return this.entityDecls;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00025AF4 File Offset: 0x00023CF4
		public DTDParameterEntityDeclarationCollection PEDecls
		{
			get
			{
				return this.peDecls;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00025AFC File Offset: 0x00023CFC
		public DTDNotationDeclarationCollection NotationDecls
		{
			get
			{
				return this.notationDecls;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00025B04 File Offset: 0x00023D04
		public DTDAutomata RootAutomata
		{
			get
			{
				if (this.rootAutomata == null)
				{
					this.rootAutomata = new DTDElementAutomata(this, this.Name);
				}
				return this.rootAutomata;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00025B2C File Offset: 0x00023D2C
		public DTDEmptyAutomata Empty
		{
			get
			{
				if (this.emptyAutomata == null)
				{
					this.emptyAutomata = new DTDEmptyAutomata(this);
				}
				return this.emptyAutomata;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00025B4C File Offset: 0x00023D4C
		public DTDAnyAutomata Any
		{
			get
			{
				if (this.anyAutomata == null)
				{
					this.anyAutomata = new DTDAnyAutomata(this);
				}
				return this.anyAutomata;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00025B6C File Offset: 0x00023D6C
		public DTDInvalidAutomata Invalid
		{
			get
			{
				if (this.invalidAutomata == null)
				{
					this.invalidAutomata = new DTDInvalidAutomata(this);
				}
				return this.invalidAutomata;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00025B8C File Offset: 0x00023D8C
		public XmlSchemaException[] Errors
		{
			get
			{
				return this.validationErrors.ToArray(typeof(XmlSchemaException)) as XmlSchemaException[];
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00025BA8 File Offset: 0x00023DA8
		public void AddError(XmlSchemaException ex)
		{
			this.validationErrors.Add(ex);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00025BB8 File Offset: 0x00023DB8
		internal string GenerateEntityAttributeText(string entityName)
		{
			DTDEntityDeclaration dtdentityDeclaration = this.EntityDecls[entityName];
			if (dtdentityDeclaration == null)
			{
				return null;
			}
			return dtdentityDeclaration.EntityValue;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00025BE0 File Offset: 0x00023DE0
		internal Mono.Xml2.XmlTextReader GenerateEntityContentReader(string entityName, XmlParserContext context)
		{
			DTDEntityDeclaration dtdentityDeclaration = this.EntityDecls[entityName];
			if (dtdentityDeclaration == null)
			{
				return null;
			}
			if (dtdentityDeclaration.SystemId != null)
			{
				Uri baseUri = (!(dtdentityDeclaration.BaseURI == string.Empty)) ? new Uri(dtdentityDeclaration.BaseURI) : null;
				Stream xmlFragment = this.resolver.GetEntity(this.resolver.ResolveUri(baseUri, dtdentityDeclaration.SystemId), null, typeof(Stream)) as Stream;
				return new Mono.Xml2.XmlTextReader(xmlFragment, XmlNodeType.Element, context);
			}
			return new Mono.Xml2.XmlTextReader(dtdentityDeclaration.EntityValue, XmlNodeType.Element, context);
		}

		// Token: 0x040003DA RID: 986
		public const int AllowedExternalEntitiesMax = 256;

		// Token: 0x040003DB RID: 987
		private DTDAutomataFactory factory;

		// Token: 0x040003DC RID: 988
		private DTDElementAutomata rootAutomata;

		// Token: 0x040003DD RID: 989
		private DTDEmptyAutomata emptyAutomata;

		// Token: 0x040003DE RID: 990
		private DTDAnyAutomata anyAutomata;

		// Token: 0x040003DF RID: 991
		private DTDInvalidAutomata invalidAutomata;

		// Token: 0x040003E0 RID: 992
		private DTDElementDeclarationCollection elementDecls;

		// Token: 0x040003E1 RID: 993
		private DTDAttListDeclarationCollection attListDecls;

		// Token: 0x040003E2 RID: 994
		private DTDParameterEntityDeclarationCollection peDecls;

		// Token: 0x040003E3 RID: 995
		private DTDEntityDeclarationCollection entityDecls;

		// Token: 0x040003E4 RID: 996
		private DTDNotationDeclarationCollection notationDecls;

		// Token: 0x040003E5 RID: 997
		private ArrayList validationErrors;

		// Token: 0x040003E6 RID: 998
		private XmlResolver resolver;

		// Token: 0x040003E7 RID: 999
		private XmlNameTable nameTable;

		// Token: 0x040003E8 RID: 1000
		private Hashtable externalResources;

		// Token: 0x040003E9 RID: 1001
		private string baseURI;

		// Token: 0x040003EA RID: 1002
		private string name;

		// Token: 0x040003EB RID: 1003
		private string publicId;

		// Token: 0x040003EC RID: 1004
		private string systemId;

		// Token: 0x040003ED RID: 1005
		private string intSubset;

		// Token: 0x040003EE RID: 1006
		private bool intSubsetHasPERef;

		// Token: 0x040003EF RID: 1007
		private bool isStandalone;

		// Token: 0x040003F0 RID: 1008
		private int lineNumber;

		// Token: 0x040003F1 RID: 1009
		private int linePosition;
	}
}
