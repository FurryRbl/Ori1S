using System;
using System.Xml.Schema;

namespace Mono.Xml
{
	// Token: 0x020000C1 RID: 193
	internal class DTDElementDeclarationCollection : DTDCollectionBase
	{
		// Token: 0x060006B1 RID: 1713 RVA: 0x00025DC4 File Offset: 0x00023FC4
		public DTDElementDeclarationCollection(DTDObjectModel root) : base(root)
		{
		}

		// Token: 0x170001B7 RID: 439
		public DTDElementDeclaration this[string name]
		{
			get
			{
				return this.Get(name);
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00025DDC File Offset: 0x00023FDC
		public DTDElementDeclaration Get(string name)
		{
			return base.BaseGet(name) as DTDElementDeclaration;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00025DEC File Offset: 0x00023FEC
		public void Add(string name, DTDElementDeclaration decl)
		{
			if (base.Contains(name))
			{
				base.Root.AddError(new XmlSchemaException(string.Format("Element declaration for {0} was already added.", name), null));
				return;
			}
			decl.SetRoot(base.Root);
			base.BaseAdd(name, decl);
		}
	}
}
