using System;

namespace Mono.Xml
{
	// Token: 0x020000C3 RID: 195
	internal class DTDEntityDeclarationCollection : DTDCollectionBase
	{
		// Token: 0x060006B8 RID: 1720 RVA: 0x00025EFC File Offset: 0x000240FC
		public DTDEntityDeclarationCollection(DTDObjectModel root) : base(root)
		{
		}

		// Token: 0x170001B9 RID: 441
		public DTDEntityDeclaration this[string name]
		{
			get
			{
				return base.BaseGet(name) as DTDEntityDeclaration;
			}
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00025F18 File Offset: 0x00024118
		public void Add(string name, DTDEntityDeclaration decl)
		{
			if (base.Contains(name))
			{
				throw new InvalidOperationException(string.Format("Entity declaration for {0} was already added.", name));
			}
			decl.SetRoot(base.Root);
			base.BaseAdd(name, decl);
		}
	}
}
