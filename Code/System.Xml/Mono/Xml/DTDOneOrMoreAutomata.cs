using System;

namespace Mono.Xml
{
	// Token: 0x020000BA RID: 186
	internal class DTDOneOrMoreAutomata : DTDAutomata
	{
		// Token: 0x06000672 RID: 1650 RVA: 0x00025790 File Offset: 0x00023990
		public DTDOneOrMoreAutomata(DTDObjectModel root, DTDAutomata children) : base(root)
		{
			this.children = children;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x000257A0 File Offset: 0x000239A0
		public DTDAutomata Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x000257A8 File Offset: 0x000239A8
		public override DTDAutomata TryStartElement(string name)
		{
			DTDAutomata dtdautomata = this.children.TryStartElement(name);
			if (dtdautomata != base.Root.Invalid)
			{
				return dtdautomata.MakeSequence(base.Root.Empty.MakeChoice(this));
			}
			return base.Root.Invalid;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x000257F8 File Offset: 0x000239F8
		public override DTDAutomata TryEndElement()
		{
			return (!this.Emptiable) ? base.Root.Invalid : this.children.TryEndElement();
		}

		// Token: 0x040003D9 RID: 985
		private DTDAutomata children;
	}
}
