using System;
using System.Collections;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x02000027 RID: 39
	internal class XsdElementValidationState : XsdValidationState
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x000089E0 File Offset: 0x00006BE0
		public XsdElementValidationState(XmlSchemaElement element, XsdParticleStateManager manager) : base(manager)
		{
			this.element = element;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000089F0 File Offset: 0x00006BF0
		private string Name
		{
			get
			{
				return this.element.QualifiedName.Name;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00008A04 File Offset: 0x00006C04
		private string NS
		{
			get
			{
				return this.element.QualifiedName.Namespace;
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00008A18 File Offset: 0x00006C18
		public override void GetExpectedParticles(ArrayList al)
		{
			XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)base.MemberwiseClone();
			decimal num = this.element.ValidatedMinOccurs - base.Occured;
			xmlSchemaElement.MinOccurs = ((!(num > 0m)) ? 0m : num);
			if (this.element.ValidatedMaxOccurs == 79228162514264337593543950335m)
			{
				xmlSchemaElement.MaxOccursString = "unbounded";
			}
			else
			{
				xmlSchemaElement.MaxOccurs = this.element.ValidatedMaxOccurs - base.Occured;
			}
			al.Add(xmlSchemaElement);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00008AC8 File Offset: 0x00006CC8
		public override XsdValidationState EvaluateStartElement(string name, string ns)
		{
			if (this.Name == name && this.NS == ns && !this.element.IsAbstract)
			{
				return this.CheckOccurence(this.element);
			}
			for (int i = 0; i < this.element.SubstitutingElements.Count; i++)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)this.element.SubstitutingElements[i];
				if (xmlSchemaElement.QualifiedName.Name == name && xmlSchemaElement.QualifiedName.Namespace == ns)
				{
					return this.CheckOccurence(xmlSchemaElement);
				}
			}
			return XsdValidationState.Invalid;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00008B88 File Offset: 0x00006D88
		private XsdValidationState CheckOccurence(XmlSchemaElement maybeSubstituted)
		{
			base.OccuredInternal++;
			base.Manager.CurrentElement = maybeSubstituted;
			if (base.Occured > this.element.ValidatedMaxOccurs)
			{
				return XsdValidationState.Invalid;
			}
			if (base.Occured == this.element.ValidatedMaxOccurs)
			{
				return base.Manager.Create(XmlSchemaParticle.Empty);
			}
			return this;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00008C08 File Offset: 0x00006E08
		public override bool EvaluateEndElement()
		{
			return this.EvaluateIsEmptiable();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00008C10 File Offset: 0x00006E10
		internal override bool EvaluateIsEmptiable()
		{
			return this.element.ValidatedMinOccurs <= base.Occured && this.element.ValidatedMaxOccurs >= base.Occured;
		}

		// Token: 0x0400011E RID: 286
		private readonly XmlSchemaElement element;
	}
}
