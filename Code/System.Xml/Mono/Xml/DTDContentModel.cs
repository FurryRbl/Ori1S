using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml
{
	// Token: 0x020000C5 RID: 197
	internal class DTDContentModel : DTDNode
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x00025FB4 File Offset: 0x000241B4
		internal DTDContentModel(DTDObjectModel root, string ownerElementName)
		{
			this.root = root;
			this.ownerElementName = ownerElementName;
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00025FD8 File Offset: 0x000241D8
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x00025FE0 File Offset: 0x000241E0
		public DTDContentModelCollection ChildModels
		{
			get
			{
				return this.childModels;
			}
			set
			{
				this.childModels = value;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00025FEC File Offset: 0x000241EC
		public DTDElementDeclaration ElementDecl
		{
			get
			{
				return this.root.ElementDecls[this.ownerElementName];
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00026004 File Offset: 0x00024204
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x0002600C File Offset: 0x0002420C
		public string ElementName
		{
			get
			{
				return this.elementName;
			}
			set
			{
				this.elementName = value;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00026018 File Offset: 0x00024218
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x00026020 File Offset: 0x00024220
		public DTDOccurence Occurence
		{
			get
			{
				return this.occurence;
			}
			set
			{
				this.occurence = value;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0002602C File Offset: 0x0002422C
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x00026034 File Offset: 0x00024234
		public DTDContentOrderType OrderType
		{
			get
			{
				return this.orderType;
			}
			set
			{
				this.orderType = value;
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00026040 File Offset: 0x00024240
		public DTDAutomata GetAutomata()
		{
			if (this.compiledAutomata == null)
			{
				this.Compile();
			}
			return this.compiledAutomata;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0002605C File Offset: 0x0002425C
		public DTDAutomata Compile()
		{
			this.compiledAutomata = this.CompileInternal();
			return this.compiledAutomata;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00026070 File Offset: 0x00024270
		internal XmlSchemaParticle CreateXsdParticle()
		{
			XmlSchemaParticle xmlSchemaParticle = this.CreateXsdParticleCore();
			if (xmlSchemaParticle == null)
			{
				return null;
			}
			switch (this.Occurence)
			{
			case DTDOccurence.Optional:
				xmlSchemaParticle.MinOccurs = 0m;
				break;
			case DTDOccurence.ZeroOrMore:
				xmlSchemaParticle.MinOccurs = 0m;
				xmlSchemaParticle.MaxOccursString = "unbounded";
				break;
			case DTDOccurence.OneOrMore:
				xmlSchemaParticle.MaxOccursString = "unbounded";
				break;
			}
			return xmlSchemaParticle;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x000260EC File Offset: 0x000242EC
		private XmlSchemaParticle CreateXsdParticleCore()
		{
			if (this.ElementName != null)
			{
				XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
				base.SetLineInfo(xmlSchemaElement);
				xmlSchemaElement.RefName = new XmlQualifiedName(this.ElementName);
				return xmlSchemaElement;
			}
			if (this.ChildModels.Count == 0)
			{
				return null;
			}
			XmlSchemaGroupBase xmlSchemaGroupBase = (this.OrderType != DTDContentOrderType.Seq) ? new XmlSchemaChoice() : new XmlSchemaSequence();
			base.SetLineInfo(xmlSchemaGroupBase);
			foreach (object obj in this.ChildModels.Items)
			{
				DTDContentModel dtdcontentModel = (DTDContentModel)obj;
				XmlSchemaParticle xmlSchemaParticle = dtdcontentModel.CreateXsdParticle();
				if (xmlSchemaParticle != null)
				{
					xmlSchemaGroupBase.Items.Add(xmlSchemaParticle);
				}
			}
			return xmlSchemaGroupBase;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000261E4 File Offset: 0x000243E4
		private DTDAutomata CompileInternal()
		{
			if (this.ElementDecl.IsAny)
			{
				return this.root.Any;
			}
			if (this.ElementDecl.IsEmpty)
			{
				return this.root.Empty;
			}
			DTDAutomata basicContentAutomata = this.GetBasicContentAutomata();
			switch (this.Occurence)
			{
			case DTDOccurence.One:
				return basicContentAutomata;
			case DTDOccurence.Optional:
				return this.Choice(this.root.Empty, basicContentAutomata);
			case DTDOccurence.ZeroOrMore:
				return this.Choice(this.root.Empty, new DTDOneOrMoreAutomata(this.root, basicContentAutomata));
			case DTDOccurence.OneOrMore:
				return new DTDOneOrMoreAutomata(this.root, basicContentAutomata);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00026298 File Offset: 0x00024498
		private DTDAutomata GetBasicContentAutomata()
		{
			if (this.ElementName != null)
			{
				return new DTDElementAutomata(this.root, this.ElementName);
			}
			int count = this.ChildModels.Count;
			if (count == 0)
			{
				return this.root.Empty;
			}
			if (count == 1)
			{
				return this.ChildModels[0].GetAutomata();
			}
			int count2 = this.ChildModels.Count;
			DTDContentOrderType dtdcontentOrderType = this.OrderType;
			DTDAutomata dtdautomata;
			if (dtdcontentOrderType == DTDContentOrderType.Seq)
			{
				dtdautomata = this.Sequence(this.ChildModels[count2 - 2].GetAutomata(), this.ChildModels[count2 - 1].GetAutomata());
				for (int i = count2 - 2; i > 0; i--)
				{
					dtdautomata = this.Sequence(this.ChildModels[i - 1].GetAutomata(), dtdautomata);
				}
				return dtdautomata;
			}
			if (dtdcontentOrderType != DTDContentOrderType.Or)
			{
				throw new InvalidOperationException("Invalid pattern specification");
			}
			dtdautomata = this.Choice(this.ChildModels[count2 - 2].GetAutomata(), this.ChildModels[count2 - 1].GetAutomata());
			for (int j = count2 - 2; j > 0; j--)
			{
				dtdautomata = this.Choice(this.ChildModels[j - 1].GetAutomata(), dtdautomata);
			}
			return dtdautomata;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x000263F4 File Offset: 0x000245F4
		private DTDAutomata Sequence(DTDAutomata l, DTDAutomata r)
		{
			return this.root.Factory.Sequence(l, r);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00026408 File Offset: 0x00024608
		private DTDAutomata Choice(DTDAutomata l, DTDAutomata r)
		{
			return l.MakeChoice(r);
		}

		// Token: 0x040003F3 RID: 1011
		private DTDObjectModel root;

		// Token: 0x040003F4 RID: 1012
		private DTDAutomata compiledAutomata;

		// Token: 0x040003F5 RID: 1013
		private string ownerElementName;

		// Token: 0x040003F6 RID: 1014
		private string elementName;

		// Token: 0x040003F7 RID: 1015
		private DTDContentOrderType orderType;

		// Token: 0x040003F8 RID: 1016
		private DTDContentModelCollection childModels = new DTDContentModelCollection();

		// Token: 0x040003F9 RID: 1017
		private DTDOccurence occurence;
	}
}
