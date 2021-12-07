using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x0200002B RID: 43
	public class Lookup<TKey, TElement> : IEnumerable, IEnumerable<IGrouping<TKey, TElement>>, ILookup<TKey, TElement>
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x0000C5B0 File Offset: 0x0000A7B0
		internal Lookup(Dictionary<TKey, List<TElement>> lookup, IEnumerable<TElement> nullKeyElements)
		{
			this.groups = new Dictionary<TKey, IGrouping<TKey, TElement>>(lookup.Comparer);
			foreach (KeyValuePair<TKey, List<TElement>> keyValuePair in lookup)
			{
				this.groups.Add(keyValuePair.Key, new Grouping<TKey, TElement>(keyValuePair.Key, keyValuePair.Value));
			}
			if (nullKeyElements != null)
			{
				this.nullGrouping = new Grouping<TKey, TElement>(default(TKey), nullKeyElements);
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000C664 File Offset: 0x0000A864
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000C66C File Offset: 0x0000A86C
		public int Count
		{
			get
			{
				return (this.nullGrouping != null) ? (this.groups.Count + 1) : this.groups.Count;
			}
		}

		// Token: 0x1700002B RID: 43
		public IEnumerable<TElement> this[TKey key]
		{
			get
			{
				if (key == null && this.nullGrouping != null)
				{
					return this.nullGrouping;
				}
				IGrouping<TKey, TElement> result;
				if (key != null && this.groups.TryGetValue(key, out result))
				{
					return result;
				}
				return new TElement[0];
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000C6F4 File Offset: 0x0000A8F4
		public IEnumerable<TResult> ApplyResultSelector<TResult>(Func<TKey, IEnumerable<TElement>, TResult> selector)
		{
			if (this.nullGrouping != null)
			{
				yield return selector(this.nullGrouping.Key, this.nullGrouping);
			}
			foreach (IGrouping<TKey, TElement> group in this.groups.Values)
			{
				yield return selector(group.Key, group);
			}
			yield break;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000C728 File Offset: 0x0000A928
		public bool Contains(TKey key)
		{
			return (key == null) ? (this.nullGrouping != null) : this.groups.ContainsKey(key);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000C760 File Offset: 0x0000A960
		public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
		{
			if (this.nullGrouping != null)
			{
				yield return this.nullGrouping;
			}
			foreach (IGrouping<TKey, TElement> g in this.groups.Values)
			{
				yield return g;
			}
			yield break;
		}

		// Token: 0x040000A1 RID: 161
		private IGrouping<TKey, TElement> nullGrouping;

		// Token: 0x040000A2 RID: 162
		private Dictionary<TKey, IGrouping<TKey, TElement>> groups;
	}
}
