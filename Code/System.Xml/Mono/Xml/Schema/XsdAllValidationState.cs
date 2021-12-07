using System;
using System.Collections;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x0200002A RID: 42
	internal class XsdAllValidationState : XsdValidationState
	{
		// Token: 0x0600010A RID: 266 RVA: 0x000095BC File Offset: 0x000077BC
		public XsdAllValidationState(XmlSchemaAll all, XsdParticleStateManager manager) : base(manager)
		{
			this.all = all;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000095D8 File Offset: 0x000077D8
		public override void GetExpectedParticles(ArrayList al)
		{
			foreach (XmlSchemaObject xmlSchemaObject in this.all.CompiledItems)
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)xmlSchemaObject;
				if (!this.consumed.Contains(xmlSchemaParticle))
				{
					al.Add(xmlSchemaParticle);
				}
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00009660 File Offset: 0x00007860
		public override XsdValidationState EvaluateStartElement(string localName, string ns)
		{
			if (this.all.CompiledItems.Count == 0)
			{
				return XsdValidationState.Invalid;
			}
			int i = 0;
			while (i < this.all.CompiledItems.Count)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)this.all.CompiledItems[i];
				if (xmlSchemaElement.QualifiedName.Name == localName && xmlSchemaElement.QualifiedName.Namespace == ns)
				{
					if (this.consumed.Contains(xmlSchemaElement))
					{
						return XsdValidationState.Invalid;
					}
					this.consumed.Add(xmlSchemaElement);
					base.Manager.CurrentElement = xmlSchemaElement;
					base.OccuredInternal = 1;
					return this;
				}
				else
				{
					i++;
				}
			}
			return XsdValidationState.Invalid;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000972C File Offset: 0x0000792C
		public override bool EvaluateEndElement()
		{
			if (this.all.Emptiable || this.all.ValidatedMinOccurs == 0m)
			{
				return true;
			}
			if (this.all.ValidatedMinOccurs > 0m && this.consumed.Count == 0)
			{
				return false;
			}
			if (this.all.CompiledItems.Count == this.consumed.Count)
			{
				return true;
			}
			for (int i = 0; i < this.all.CompiledItems.Count; i++)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)this.all.CompiledItems[i];
				if (xmlSchemaElement.ValidatedMinOccurs > 0m && !this.consumed.Contains(xmlSchemaElement))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00009818 File Offset: 0x00007A18
		internal override bool EvaluateIsEmptiable()
		{
			if (this.all.Emptiable || this.all.ValidatedMinOccurs == 0m)
			{
				return true;
			}
			for (int i = 0; i < this.all.CompiledItems.Count; i++)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)this.all.CompiledItems[i];
				if (xmlSchemaElement.ValidatedMinOccurs > 0m && !this.consumed.Contains(xmlSchemaElement))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000126 RID: 294
		private readonly XmlSchemaAll all;

		// Token: 0x04000127 RID: 295
		private ArrayList consumed = new ArrayList();
	}
}
