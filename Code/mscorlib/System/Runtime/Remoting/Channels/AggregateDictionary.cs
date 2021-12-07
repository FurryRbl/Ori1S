using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000444 RID: 1092
	[ComVisible(true)]
	internal class AggregateDictionary : IEnumerable, ICollection, IDictionary
	{
		// Token: 0x06002E12 RID: 11794 RVA: 0x00099588 File Offset: 0x00097788
		public AggregateDictionary(IDictionary[] dics)
		{
			this.dictionaries = dics;
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x00099598 File Offset: 0x00097798
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new AggregateEnumerator(this.dictionaries);
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06002E14 RID: 11796 RVA: 0x000995A8 File Offset: 0x000977A8
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06002E15 RID: 11797 RVA: 0x000995AC File Offset: 0x000977AC
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700082F RID: 2095
		public object this[object key]
		{
			get
			{
				foreach (IDictionary dictionary in this.dictionaries)
				{
					if (dictionary.Contains(key))
					{
						return dictionary[key];
					}
				}
				return null;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06002E18 RID: 11800 RVA: 0x000995FC File Offset: 0x000977FC
		public ICollection Keys
		{
			get
			{
				if (this._keys != null)
				{
					return this._keys;
				}
				this._keys = new ArrayList();
				foreach (IDictionary dictionary in this.dictionaries)
				{
					this._keys.AddRange(dictionary.Keys);
				}
				return this._keys;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06002E19 RID: 11801 RVA: 0x0009965C File Offset: 0x0009785C
		public ICollection Values
		{
			get
			{
				if (this._values != null)
				{
					return this._values;
				}
				this._values = new ArrayList();
				foreach (IDictionary dictionary in this.dictionaries)
				{
					this._values.AddRange(dictionary.Values);
				}
				return this._values;
			}
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x000996BC File Offset: 0x000978BC
		public void Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x000996C4 File Offset: 0x000978C4
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x000996CC File Offset: 0x000978CC
		public bool Contains(object ob)
		{
			foreach (IDictionary dictionary in this.dictionaries)
			{
				if (dictionary.Contains(ob))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x00099708 File Offset: 0x00097908
		public IDictionaryEnumerator GetEnumerator()
		{
			return new AggregateEnumerator(this.dictionaries);
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x00099718 File Offset: 0x00097918
		public void Remove(object ob)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x00099720 File Offset: 0x00097920
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06002E20 RID: 11808 RVA: 0x0009978C File Offset: 0x0009798C
		public int Count
		{
			get
			{
				int num = 0;
				foreach (IDictionary dictionary in this.dictionaries)
				{
					num += dictionary.Count;
				}
				return num;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06002E21 RID: 11809 RVA: 0x000997C4 File Offset: 0x000979C4
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06002E22 RID: 11810 RVA: 0x000997C8 File Offset: 0x000979C8
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x040013C5 RID: 5061
		private IDictionary[] dictionaries;

		// Token: 0x040013C6 RID: 5062
		private ArrayList _values;

		// Token: 0x040013C7 RID: 5063
		private ArrayList _keys;
	}
}
