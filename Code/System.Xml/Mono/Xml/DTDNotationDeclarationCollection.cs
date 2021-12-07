using System;

namespace Mono.Xml
{
	// Token: 0x020000C4 RID: 196
	internal class DTDNotationDeclarationCollection : DTDCollectionBase
	{
		// Token: 0x060006BB RID: 1723 RVA: 0x00025F58 File Offset: 0x00024158
		public DTDNotationDeclarationCollection(DTDObjectModel root) : base(root)
		{
		}

		// Token: 0x170001BA RID: 442
		public DTDNotationDeclaration this[string name]
		{
			get
			{
				return base.BaseGet(name) as DTDNotationDeclaration;
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00025F74 File Offset: 0x00024174
		public void Add(string name, DTDNotationDeclaration decl)
		{
			if (base.Contains(name))
			{
				throw new InvalidOperationException(string.Format("Notation declaration for {0} was already added.", name));
			}
			decl.SetRoot(base.Root);
			base.BaseAdd(name, decl);
		}
	}
}
