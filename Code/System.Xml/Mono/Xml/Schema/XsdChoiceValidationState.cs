using System;
using System.Collections;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x02000029 RID: 41
	internal class XsdChoiceValidationState : XsdValidationState
	{
		// Token: 0x06000105 RID: 261 RVA: 0x0000923C File Offset: 0x0000743C
		public XsdChoiceValidationState(XmlSchemaChoice choice, XsdParticleStateManager manager) : base(manager)
		{
			this.choice = choice;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000924C File Offset: 0x0000744C
		public override void GetExpectedParticles(ArrayList al)
		{
			if (base.Occured < this.choice.ValidatedMaxOccurs)
			{
				foreach (XmlSchemaObject xmlSchemaObject in this.choice.CompiledItems)
				{
					XmlSchemaParticle value = (XmlSchemaParticle)xmlSchemaObject;
					al.Add(value);
				}
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000092E4 File Offset: 0x000074E4
		public override XsdValidationState EvaluateStartElement(string localName, string ns)
		{
			this.emptiableComputed = false;
			bool flag = true;
			int i = 0;
			while (i < this.choice.CompiledItems.Count)
			{
				XmlSchemaParticle xsobj = (XmlSchemaParticle)this.choice.CompiledItems[i];
				XsdValidationState xsdValidationState = base.Manager.Create(xsobj);
				XsdValidationState xsdValidationState2 = xsdValidationState.EvaluateStartElement(localName, ns);
				if (xsdValidationState2 != XsdValidationState.Invalid)
				{
					base.OccuredInternal++;
					if (base.Occured > this.choice.ValidatedMaxOccurs)
					{
						return XsdValidationState.Invalid;
					}
					if (base.Occured == this.choice.ValidatedMaxOccurs)
					{
						return xsdValidationState2;
					}
					return base.Manager.MakeSequence(xsdValidationState2, this);
				}
				else
				{
					if (!this.emptiableComputed)
					{
						flag &= xsdValidationState.EvaluateIsEmptiable();
					}
					i++;
				}
			}
			if (!this.emptiableComputed)
			{
				if (flag)
				{
					this.emptiable = true;
				}
				if (!this.emptiable)
				{
					this.emptiable = (this.choice.ValidatedMinOccurs <= base.Occured);
				}
				this.emptiableComputed = true;
			}
			return XsdValidationState.Invalid;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00009420 File Offset: 0x00007620
		public override bool EvaluateEndElement()
		{
			this.emptiableComputed = false;
			if (this.choice.ValidatedMinOccurs > base.Occured + 1)
			{
				return false;
			}
			if (this.choice.ValidatedMinOccurs <= base.Occured)
			{
				return true;
			}
			for (int i = 0; i < this.choice.CompiledItems.Count; i++)
			{
				XmlSchemaParticle xsobj = (XmlSchemaParticle)this.choice.CompiledItems[i];
				if (base.Manager.Create(xsobj).EvaluateIsEmptiable())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000094CC File Offset: 0x000076CC
		internal override bool EvaluateIsEmptiable()
		{
			if (this.emptiableComputed)
			{
				return this.emptiable;
			}
			if (this.choice.ValidatedMaxOccurs < base.Occured)
			{
				return false;
			}
			if (this.choice.ValidatedMinOccurs > base.Occured + 1)
			{
				return false;
			}
			int num = base.Occured;
			while (num < this.choice.ValidatedMinOccurs)
			{
				bool flag = false;
				for (int i = 0; i < this.choice.CompiledItems.Count; i++)
				{
					XmlSchemaParticle xsobj = (XmlSchemaParticle)this.choice.CompiledItems[i];
					if (base.Manager.Create(xsobj).EvaluateIsEmptiable())
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x04000123 RID: 291
		private readonly XmlSchemaChoice choice;

		// Token: 0x04000124 RID: 292
		private bool emptiable;

		// Token: 0x04000125 RID: 293
		private bool emptiableComputed;
	}
}
