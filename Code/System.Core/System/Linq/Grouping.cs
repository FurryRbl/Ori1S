using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000024 RID: 36
	internal class Grouping<K, T> : IEnumerable, IEnumerable<T>, IGrouping<K, T>
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0000C564 File Offset: 0x0000A764
		public Grouping(K key, IEnumerable<T> group)
		{
			this.group = group;
			this.key = key;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000C57C File Offset: 0x0000A77C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.group.GetEnumerator();
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000C58C File Offset: 0x0000A78C
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x0000C594 File Offset: 0x0000A794
		public K Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
		public IEnumerator<T> GetEnumerator()
		{
			return this.group.GetEnumerator();
		}

		// Token: 0x0400009F RID: 159
		private K key;

		// Token: 0x040000A0 RID: 160
		private IEnumerable<T> group;
	}
}
