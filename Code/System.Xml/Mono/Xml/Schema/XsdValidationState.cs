using System;
using System.Collections;

namespace Mono.Xml.Schema
{
	// Token: 0x02000026 RID: 38
	internal abstract class XsdValidationState
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00008994 File Offset: 0x00006B94
		public XsdValidationState(XsdParticleStateManager manager)
		{
			this.manager = manager;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000089B4 File Offset: 0x00006BB4
		public static XsdInvalidValidationState Invalid
		{
			get
			{
				return XsdValidationState.invalid;
			}
		}

		// Token: 0x060000F0 RID: 240
		public abstract XsdValidationState EvaluateStartElement(string localName, string ns);

		// Token: 0x060000F1 RID: 241
		public abstract bool EvaluateEndElement();

		// Token: 0x060000F2 RID: 242
		internal abstract bool EvaluateIsEmptiable();

		// Token: 0x060000F3 RID: 243
		public abstract void GetExpectedParticles(ArrayList al);

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000089BC File Offset: 0x00006BBC
		public XsdParticleStateManager Manager
		{
			get
			{
				return this.manager;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000089C4 File Offset: 0x00006BC4
		public int Occured
		{
			get
			{
				return this.occured;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000089CC File Offset: 0x00006BCC
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x000089D4 File Offset: 0x00006BD4
		internal int OccuredInternal
		{
			get
			{
				return this.occured;
			}
			set
			{
				this.occured = value;
			}
		}

		// Token: 0x0400011B RID: 283
		private static XsdInvalidValidationState invalid = new XsdInvalidValidationState(null);

		// Token: 0x0400011C RID: 284
		private int occured;

		// Token: 0x0400011D RID: 285
		private readonly XsdParticleStateManager manager;
	}
}
