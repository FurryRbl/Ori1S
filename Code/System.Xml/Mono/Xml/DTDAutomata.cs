using System;

namespace Mono.Xml
{
	// Token: 0x020000B6 RID: 182
	internal abstract class DTDAutomata
	{
		// Token: 0x0600065C RID: 1628 RVA: 0x00025418 File Offset: 0x00023618
		public DTDAutomata(DTDObjectModel root)
		{
			this.root = root;
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00025428 File Offset: 0x00023628
		public DTDObjectModel Root
		{
			get
			{
				return this.root;
			}
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00025430 File Offset: 0x00023630
		public DTDAutomata MakeChoice(DTDAutomata other)
		{
			if (this == this.Root.Invalid)
			{
				return other;
			}
			if (other == this.Root.Invalid)
			{
				return this;
			}
			if (this == this.Root.Empty && other == this.Root.Empty)
			{
				return this;
			}
			if (this == this.Root.Any && other == this.Root.Any)
			{
				return this;
			}
			if (other == this.Root.Empty)
			{
				return this.Root.Factory.Choice(other, this);
			}
			return this.Root.Factory.Choice(this, other);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x000254E4 File Offset: 0x000236E4
		public DTDAutomata MakeSequence(DTDAutomata other)
		{
			if (this == this.Root.Invalid || other == this.Root.Invalid)
			{
				return this.Root.Invalid;
			}
			if (this == this.Root.Empty)
			{
				return other;
			}
			if (other == this.Root.Empty)
			{
				return this;
			}
			return this.Root.Factory.Sequence(this, other);
		}

		// Token: 0x06000660 RID: 1632
		public abstract DTDAutomata TryStartElement(string name);

		// Token: 0x06000661 RID: 1633 RVA: 0x00025558 File Offset: 0x00023758
		public virtual DTDAutomata TryEndElement()
		{
			return this.Root.Invalid;
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x00025568 File Offset: 0x00023768
		public virtual bool Emptiable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040003CF RID: 975
		private DTDObjectModel root;
	}
}
