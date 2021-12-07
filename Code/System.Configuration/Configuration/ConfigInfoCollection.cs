using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Configuration
{
	// Token: 0x0200006E RID: 110
	internal class ConfigInfoCollection : NameObjectCollectionBase
	{
		// Token: 0x060003CB RID: 971 RVA: 0x0000B4B8 File Offset: 0x000096B8
		public ConfigInfoCollection() : base(StringComparer.Ordinal)
		{
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000B4C8 File Offset: 0x000096C8
		public ICollection AllKeys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x17000116 RID: 278
		public ConfigInfo this[string name]
		{
			get
			{
				return (ConfigInfo)base.BaseGet(name);
			}
			set
			{
				base.BaseSet(name, value);
			}
		}

		// Token: 0x17000117 RID: 279
		public ConfigInfo this[int index]
		{
			get
			{
				return (ConfigInfo)base.BaseGet(index);
			}
			set
			{
				base.BaseSet(index, value);
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000B508 File Offset: 0x00009708
		public void Add(string name, ConfigInfo config)
		{
			base.BaseAdd(name, config);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000B514 File Offset: 0x00009714
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000B51C File Offset: 0x0000971C
		public string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000B528 File Offset: 0x00009728
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000B534 File Offset: 0x00009734
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
