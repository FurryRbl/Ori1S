using System;
using System.Collections;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x02000028 RID: 40
	internal class XsdSequenceValidationState : XsdValidationState
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00008C5C File Offset: 0x00006E5C
		public XsdSequenceValidationState(XmlSchemaSequence sequence, XsdParticleStateManager manager) : base(manager)
		{
			this.seq = sequence;
			this.current = -1;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00008C74 File Offset: 0x00006E74
		public override void GetExpectedParticles(ArrayList al)
		{
			if (this.currentAutomata == null)
			{
				foreach (XmlSchemaObject xmlSchemaObject in this.seq.CompiledItems)
				{
					XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)xmlSchemaObject;
					al.Add(xmlSchemaParticle);
					if (!xmlSchemaParticle.ValidateIsEmptiable())
					{
						break;
					}
				}
				return;
			}
			if (this.currentAutomata != null)
			{
				this.currentAutomata.GetExpectedParticles(al);
				if (!this.currentAutomata.EvaluateIsEmptiable())
				{
					return;
				}
				for (int i = this.current + 1; i < this.seq.CompiledItems.Count; i++)
				{
					XmlSchemaParticle xmlSchemaParticle2 = this.seq.CompiledItems[i] as XmlSchemaParticle;
					al.Add(xmlSchemaParticle2);
					if (!xmlSchemaParticle2.ValidateIsEmptiable())
					{
						break;
					}
				}
			}
			if (base.Occured + 1 == this.seq.ValidatedMaxOccurs)
			{
				return;
			}
			for (int j = 0; j <= this.current; j++)
			{
				al.Add(this.seq.CompiledItems[j]);
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00008DE0 File Offset: 0x00006FE0
		public override XsdValidationState EvaluateStartElement(string name, string ns)
		{
			if (this.seq.CompiledItems.Count == 0)
			{
				return XsdValidationState.Invalid;
			}
			int num = (this.current >= 0) ? this.current : 0;
			XsdValidationState xsdValidationState = this.currentAutomata;
			bool flag = false;
			XsdValidationState xsdValidationState2;
			for (;;)
			{
				if (xsdValidationState == null)
				{
					xsdValidationState = base.Manager.Create(this.seq.CompiledItems[num] as XmlSchemaParticle);
					flag = true;
				}
				if (xsdValidationState is XsdEmptyValidationState && this.seq.CompiledItems.Count == num + 1 && base.Occured == this.seq.ValidatedMaxOccurs)
				{
					break;
				}
				xsdValidationState2 = xsdValidationState.EvaluateStartElement(name, ns);
				if (xsdValidationState2 != XsdValidationState.Invalid)
				{
					goto IL_E1;
				}
				if (!xsdValidationState.EvaluateIsEmptiable())
				{
					goto Block_8;
				}
				num++;
				if (num > this.current && flag && this.current >= 0)
				{
					goto Block_13;
				}
				if (this.seq.CompiledItems.Count > num)
				{
					xsdValidationState = base.Manager.Create(this.seq.CompiledItems[num] as XmlSchemaParticle);
				}
				else
				{
					if (this.current < 0)
					{
						goto Block_15;
					}
					num = 0;
					xsdValidationState = null;
				}
			}
			return XsdValidationState.Invalid;
			Block_8:
			this.emptiable = false;
			return XsdValidationState.Invalid;
			IL_E1:
			this.current = num;
			this.currentAutomata = xsdValidationState2;
			if (flag)
			{
				base.OccuredInternal++;
				if (base.Occured > this.seq.ValidatedMaxOccurs)
				{
					return XsdValidationState.Invalid;
				}
			}
			return this;
			Block_13:
			return XsdValidationState.Invalid;
			Block_15:
			return XsdValidationState.Invalid;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00008F98 File Offset: 0x00007198
		public override bool EvaluateEndElement()
		{
			if (this.seq.ValidatedMinOccurs > base.Occured + 1)
			{
				return false;
			}
			if (this.seq.CompiledItems.Count == 0)
			{
				return true;
			}
			if (this.currentAutomata == null && this.seq.ValidatedMinOccurs <= base.Occured)
			{
				return true;
			}
			int num = (this.current >= 0) ? this.current : 0;
			XsdValidationState xsdValidationState = this.currentAutomata;
			if (xsdValidationState == null)
			{
				xsdValidationState = base.Manager.Create(this.seq.CompiledItems[num] as XmlSchemaParticle);
			}
			while (xsdValidationState != null)
			{
				if (!xsdValidationState.EvaluateEndElement() && !xsdValidationState.EvaluateIsEmptiable())
				{
					return false;
				}
				num++;
				if (this.seq.CompiledItems.Count > num)
				{
					xsdValidationState = base.Manager.Create(this.seq.CompiledItems[num] as XmlSchemaParticle);
				}
				else
				{
					xsdValidationState = null;
				}
			}
			if (this.current < 0)
			{
				base.OccuredInternal++;
			}
			return this.seq.ValidatedMinOccurs <= base.Occured && this.seq.ValidatedMaxOccurs >= base.Occured;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00009114 File Offset: 0x00007314
		internal override bool EvaluateIsEmptiable()
		{
			if (this.seq.ValidatedMinOccurs > base.Occured + 1)
			{
				return false;
			}
			if (this.seq.ValidatedMinOccurs == 0m && this.currentAutomata == null)
			{
				return true;
			}
			if (this.emptiable)
			{
				return true;
			}
			if (this.seq.CompiledItems.Count == 0)
			{
				return true;
			}
			int num = (this.current >= 0) ? this.current : 0;
			XsdValidationState xsdValidationState = this.currentAutomata;
			if (xsdValidationState == null)
			{
				xsdValidationState = base.Manager.Create(this.seq.CompiledItems[num] as XmlSchemaParticle);
			}
			while (xsdValidationState != null)
			{
				if (!xsdValidationState.EvaluateIsEmptiable())
				{
					return false;
				}
				num++;
				if (this.seq.CompiledItems.Count > num)
				{
					xsdValidationState = base.Manager.Create(this.seq.CompiledItems[num] as XmlSchemaParticle);
				}
				else
				{
					xsdValidationState = null;
				}
			}
			this.emptiable = true;
			return true;
		}

		// Token: 0x0400011F RID: 287
		private readonly XmlSchemaSequence seq;

		// Token: 0x04000120 RID: 288
		private int current;

		// Token: 0x04000121 RID: 289
		private XsdValidationState currentAutomata;

		// Token: 0x04000122 RID: 290
		private bool emptiable;
	}
}
