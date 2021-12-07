using System;
using System.Collections;

namespace Mono.Xml
{
	// Token: 0x020000CA RID: 202
	internal class DTDAttListDeclaration : DTDNode
	{
		// Token: 0x060006FF RID: 1791 RVA: 0x00026C78 File Offset: 0x00024E78
		internal DTDAttListDeclaration(DTDObjectModel root)
		{
			base.SetRoot(root);
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00026CA0 File Offset: 0x00024EA0
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x00026CA8 File Offset: 0x00024EA8
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		public DTDAttributeDefinition this[int i]
		{
			get
			{
				return this.Get(i);
			}
		}

		// Token: 0x170001D9 RID: 473
		public DTDAttributeDefinition this[string name]
		{
			get
			{
				return this.Get(name);
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00026CCC File Offset: 0x00024ECC
		public DTDAttributeDefinition Get(int i)
		{
			return this.attributes[i] as DTDAttributeDefinition;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00026CE0 File Offset: 0x00024EE0
		public DTDAttributeDefinition Get(string name)
		{
			object obj = this.attributeOrders[name];
			if (obj != null)
			{
				return this.attributes[(int)obj] as DTDAttributeDefinition;
			}
			return null;
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x00026D18 File Offset: 0x00024F18
		public IList Definitions
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00026D20 File Offset: 0x00024F20
		public void Add(DTDAttributeDefinition def)
		{
			if (this.attributeOrders[def.Name] != null)
			{
				throw new InvalidOperationException(string.Format("Attribute definition for {0} was already added at element {1}.", def.Name, this.Name));
			}
			def.SetRoot(base.Root);
			this.attributeOrders.Add(def.Name, this.attributes.Count);
			this.attributes.Add(def);
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x00026D9C File Offset: 0x00024F9C
		public int Count
		{
			get
			{
				return this.attributeOrders.Count;
			}
		}

		// Token: 0x0400040E RID: 1038
		private string name;

		// Token: 0x0400040F RID: 1039
		private Hashtable attributeOrders = new Hashtable();

		// Token: 0x04000410 RID: 1040
		private ArrayList attributes = new ArrayList();
	}
}
