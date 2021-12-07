using System;
using System.Collections;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000445 RID: 1093
	internal class AggregateEnumerator : IEnumerator, IDictionaryEnumerator
	{
		// Token: 0x06002E23 RID: 11811 RVA: 0x000997CC File Offset: 0x000979CC
		public AggregateEnumerator(IDictionary[] dics)
		{
			this.dictionaries = dics;
			this.Reset();
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06002E24 RID: 11812 RVA: 0x000997E4 File Offset: 0x000979E4
		public DictionaryEntry Entry
		{
			get
			{
				return this.currente.Entry;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06002E25 RID: 11813 RVA: 0x000997F4 File Offset: 0x000979F4
		public object Key
		{
			get
			{
				return this.currente.Key;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06002E26 RID: 11814 RVA: 0x00099804 File Offset: 0x00097A04
		public object Value
		{
			get
			{
				return this.currente.Value;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06002E27 RID: 11815 RVA: 0x00099814 File Offset: 0x00097A14
		public object Current
		{
			get
			{
				return this.currente.Current;
			}
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x00099824 File Offset: 0x00097A24
		public bool MoveNext()
		{
			if (this.pos >= this.dictionaries.Length)
			{
				return false;
			}
			if (this.currente.MoveNext())
			{
				return true;
			}
			this.pos++;
			if (this.pos >= this.dictionaries.Length)
			{
				return false;
			}
			this.currente = this.dictionaries[this.pos].GetEnumerator();
			return this.MoveNext();
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x0009989C File Offset: 0x00097A9C
		public void Reset()
		{
			this.pos = 0;
			if (this.dictionaries.Length > 0)
			{
				this.currente = this.dictionaries[0].GetEnumerator();
			}
		}

		// Token: 0x040013C8 RID: 5064
		private IDictionary[] dictionaries;

		// Token: 0x040013C9 RID: 5065
		private int pos;

		// Token: 0x040013CA RID: 5066
		private IDictionaryEnumerator currente;
	}
}
