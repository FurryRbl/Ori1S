using System;
using System.Collections;

namespace Mono.Xml
{
	// Token: 0x020000CE RID: 206
	internal class DTDParameterEntityDeclarationCollection
	{
		// Token: 0x0600072C RID: 1836 RVA: 0x000274F8 File Offset: 0x000256F8
		public DTDParameterEntityDeclarationCollection(DTDObjectModel root)
		{
			this.root = root;
		}

		// Token: 0x170001ED RID: 493
		public DTDParameterEntityDeclaration this[string name]
		{
			get
			{
				return this.peDecls[name] as DTDParameterEntityDeclaration;
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00027528 File Offset: 0x00025728
		public void Add(string name, DTDParameterEntityDeclaration decl)
		{
			if (this.peDecls[name] != null)
			{
				return;
			}
			decl.SetRoot(this.root);
			this.peDecls.Add(name, decl);
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x00027558 File Offset: 0x00025758
		public ICollection Keys
		{
			get
			{
				return this.peDecls.Keys;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x00027568 File Offset: 0x00025768
		public ICollection Values
		{
			get
			{
				return this.peDecls.Values;
			}
		}

		// Token: 0x04000426 RID: 1062
		private Hashtable peDecls = new Hashtable();

		// Token: 0x04000427 RID: 1063
		private DTDObjectModel root;
	}
}
