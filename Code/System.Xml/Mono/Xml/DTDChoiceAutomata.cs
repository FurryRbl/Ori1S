using System;

namespace Mono.Xml
{
	// Token: 0x020000B8 RID: 184
	internal class DTDChoiceAutomata : DTDAutomata
	{
		// Token: 0x06000666 RID: 1638 RVA: 0x000255BC File Offset: 0x000237BC
		public DTDChoiceAutomata(DTDObjectModel root, DTDAutomata left, DTDAutomata right) : base(root)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x000255D4 File Offset: 0x000237D4
		public DTDAutomata Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x000255DC File Offset: 0x000237DC
		public DTDAutomata Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x000255E4 File Offset: 0x000237E4
		public override DTDAutomata TryStartElement(string name)
		{
			return this.left.TryStartElement(name).MakeChoice(this.right.TryStartElement(name));
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00025604 File Offset: 0x00023804
		public override DTDAutomata TryEndElement()
		{
			return this.left.TryEndElement().MakeChoice(this.right.TryEndElement());
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00025624 File Offset: 0x00023824
		public override bool Emptiable
		{
			get
			{
				if (!this.hasComputedEmptiable)
				{
					this.cachedEmptiable = (this.left.Emptiable || this.right.Emptiable);
					this.hasComputedEmptiable = true;
				}
				return this.cachedEmptiable;
			}
		}

		// Token: 0x040003D1 RID: 977
		private DTDAutomata left;

		// Token: 0x040003D2 RID: 978
		private DTDAutomata right;

		// Token: 0x040003D3 RID: 979
		private bool hasComputedEmptiable;

		// Token: 0x040003D4 RID: 980
		private bool cachedEmptiable;
	}
}
