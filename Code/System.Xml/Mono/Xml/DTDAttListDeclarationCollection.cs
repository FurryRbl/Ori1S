using System;

namespace Mono.Xml
{
	// Token: 0x020000C2 RID: 194
	internal class DTDAttListDeclarationCollection : DTDCollectionBase
	{
		// Token: 0x060006B5 RID: 1717 RVA: 0x00025E38 File Offset: 0x00024038
		public DTDAttListDeclarationCollection(DTDObjectModel root) : base(root)
		{
		}

		// Token: 0x170001B8 RID: 440
		public DTDAttListDeclaration this[string name]
		{
			get
			{
				return base.BaseGet(name) as DTDAttListDeclaration;
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00025E54 File Offset: 0x00024054
		public void Add(string name, DTDAttListDeclaration decl)
		{
			DTDAttListDeclaration dtdattListDeclaration = this[name];
			if (dtdattListDeclaration != null)
			{
				foreach (object obj in decl.Definitions)
				{
					DTDAttributeDefinition dtdattributeDefinition = (DTDAttributeDefinition)obj;
					if (decl.Get(dtdattributeDefinition.Name) == null)
					{
						dtdattListDeclaration.Add(dtdattributeDefinition);
					}
				}
			}
			else
			{
				decl.SetRoot(base.Root);
				base.BaseAdd(name, decl);
			}
		}
	}
}
