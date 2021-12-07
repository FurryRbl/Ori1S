using System;
using System.Collections;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x0200002B RID: 43
	internal class XsdAnyValidationState : XsdValidationState
	{
		// Token: 0x0600010F RID: 271 RVA: 0x000098B4 File Offset: 0x00007AB4
		public XsdAnyValidationState(XmlSchemaAny any, XsdParticleStateManager manager) : base(manager)
		{
			this.any = any;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000098C4 File Offset: 0x00007AC4
		public override void GetExpectedParticles(ArrayList al)
		{
			al.Add(this.any);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000098D4 File Offset: 0x00007AD4
		public override XsdValidationState EvaluateStartElement(string name, string ns)
		{
			if (!this.MatchesNamespace(ns))
			{
				return XsdValidationState.Invalid;
			}
			base.OccuredInternal++;
			base.Manager.SetProcessContents(this.any.ResolvedProcessContents);
			if (base.Occured > this.any.ValidatedMaxOccurs)
			{
				return XsdValidationState.Invalid;
			}
			if (base.Occured == this.any.ValidatedMaxOccurs)
			{
				return base.Manager.Create(XmlSchemaParticle.Empty);
			}
			return this;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00009970 File Offset: 0x00007B70
		private bool MatchesNamespace(string ns)
		{
			if (this.any.HasValueAny)
			{
				return true;
			}
			if (this.any.HasValueLocal && ns == string.Empty)
			{
				return true;
			}
			if (this.any.HasValueOther && (this.any.TargetNamespace == string.Empty || this.any.TargetNamespace != ns))
			{
				return true;
			}
			if (this.any.HasValueTargetNamespace && this.any.TargetNamespace == ns)
			{
				return true;
			}
			for (int i = 0; i < this.any.ResolvedNamespaces.Count; i++)
			{
				if (this.any.ResolvedNamespaces[i] == ns)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00009A5C File Offset: 0x00007C5C
		public override bool EvaluateEndElement()
		{
			return this.EvaluateIsEmptiable();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00009A64 File Offset: 0x00007C64
		internal override bool EvaluateIsEmptiable()
		{
			return this.any.ValidatedMinOccurs <= base.Occured && this.any.ValidatedMaxOccurs >= base.Occured;
		}

		// Token: 0x04000128 RID: 296
		private readonly XmlSchemaAny any;
	}
}
