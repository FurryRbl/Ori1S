using System;

namespace Mono.Xml
{
	// Token: 0x020000B9 RID: 185
	internal class DTDSequenceAutomata : DTDAutomata
	{
		// Token: 0x0600066C RID: 1644 RVA: 0x00025670 File Offset: 0x00023870
		public DTDSequenceAutomata(DTDObjectModel root, DTDAutomata left, DTDAutomata right) : base(root)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00025688 File Offset: 0x00023888
		public DTDAutomata Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x00025690 File Offset: 0x00023890
		public DTDAutomata Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00025698 File Offset: 0x00023898
		public override DTDAutomata TryStartElement(string name)
		{
			DTDAutomata dtdautomata = this.left.TryStartElement(name);
			DTDAutomata dtdautomata2 = this.right.TryStartElement(name);
			if (dtdautomata == base.Root.Invalid)
			{
				return (!this.left.Emptiable) ? dtdautomata : dtdautomata2;
			}
			DTDAutomata dtdautomata3 = dtdautomata.MakeSequence(this.right);
			if (this.left.Emptiable)
			{
				return dtdautomata2.MakeChoice(dtdautomata3);
			}
			return dtdautomata3;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00025710 File Offset: 0x00023910
		public override DTDAutomata TryEndElement()
		{
			return (!this.left.Emptiable) ? base.Root.Invalid : this.right;
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00025744 File Offset: 0x00023944
		public override bool Emptiable
		{
			get
			{
				if (!this.hasComputedEmptiable)
				{
					this.cachedEmptiable = (this.left.Emptiable && this.right.Emptiable);
					this.hasComputedEmptiable = true;
				}
				return this.cachedEmptiable;
			}
		}

		// Token: 0x040003D5 RID: 981
		private DTDAutomata left;

		// Token: 0x040003D6 RID: 982
		private DTDAutomata right;

		// Token: 0x040003D7 RID: 983
		private bool hasComputedEmptiable;

		// Token: 0x040003D8 RID: 984
		private bool cachedEmptiable;
	}
}
