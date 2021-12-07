using System;
using System.Collections.Generic;

namespace Mono.Xml
{
	// Token: 0x020000C0 RID: 192
	internal class DTDCollectionBase : DictionaryBase
	{
		// Token: 0x060006AB RID: 1707 RVA: 0x00025CA4 File Offset: 0x00023EA4
		protected DTDCollectionBase(DTDObjectModel root)
		{
			this.root = root;
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x00025CB4 File Offset: 0x00023EB4
		protected DTDObjectModel Root
		{
			get
			{
				return this.root;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x00025CBC File Offset: 0x00023EBC
		public DictionaryBase InnerHashtable
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00025CC0 File Offset: 0x00023EC0
		protected void BaseAdd(string name, DTDNode value)
		{
			base.Add(new KeyValuePair<string, DTDNode>(name, value));
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00025CD0 File Offset: 0x00023ED0
		public bool Contains(string key)
		{
			foreach (KeyValuePair<string, DTDNode> keyValuePair in this)
			{
				if (keyValuePair.Key == key)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00025D48 File Offset: 0x00023F48
		protected object BaseGet(string name)
		{
			foreach (KeyValuePair<string, DTDNode> keyValuePair in this)
			{
				if (keyValuePair.Key == name)
				{
					return keyValuePair.Value;
				}
			}
			return null;
		}

		// Token: 0x040003F2 RID: 1010
		private DTDObjectModel root;
	}
}
