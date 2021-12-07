using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UI.Collections
{
	// Token: 0x0200009D RID: 157
	internal class IndexedSet<T> : IList<T>, IEnumerable, ICollection<T>, IEnumerable<T>
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x00018308 File Offset: 0x00016508
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00018310 File Offset: 0x00016510
		public void Add(T item)
		{
			this.m_List.Add(item);
			this.m_Dictionary.Add(item, this.m_List.Count - 1);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00018344 File Offset: 0x00016544
		public bool AddUnique(T item)
		{
			if (this.m_Dictionary.ContainsKey(item))
			{
				return false;
			}
			this.m_List.Add(item);
			this.m_Dictionary.Add(item, this.m_List.Count - 1);
			return true;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001838C File Offset: 0x0001658C
		public bool Remove(T item)
		{
			int index = -1;
			if (!this.m_Dictionary.TryGetValue(item, out index))
			{
				return false;
			}
			this.RemoveAt(index);
			return true;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000183B8 File Offset: 0x000165B8
		public IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000183C0 File Offset: 0x000165C0
		public void Clear()
		{
			this.m_List.Clear();
			this.m_Dictionary.Clear();
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000183D8 File Offset: 0x000165D8
		public bool Contains(T item)
		{
			return this.m_Dictionary.ContainsKey(item);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x000183E8 File Offset: 0x000165E8
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.m_List.CopyTo(array, arrayIndex);
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x000183F8 File Offset: 0x000165F8
		public int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x00018408 File Offset: 0x00016608
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001840C File Offset: 0x0001660C
		public int IndexOf(T item)
		{
			int result = -1;
			this.m_Dictionary.TryGetValue(item, out result);
			return result;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001842C File Offset: 0x0001662C
		public void Insert(int index, T item)
		{
			throw new NotSupportedException("Random Insertion is semantically invalid, since this structure does not guarantee ordering.");
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00018438 File Offset: 0x00016638
		public void RemoveAt(int index)
		{
			T key = this.m_List[index];
			this.m_Dictionary.Remove(key);
			if (index == this.m_List.Count - 1)
			{
				this.m_List.RemoveAt(index);
			}
			else
			{
				int index2 = this.m_List.Count - 1;
				T t = this.m_List[index2];
				this.m_List[index] = t;
				this.m_Dictionary[t] = index;
				this.m_List.RemoveAt(index2);
			}
		}

		// Token: 0x1700017A RID: 378
		public T this[int index]
		{
			get
			{
				return this.m_List[index];
			}
			set
			{
				T key = this.m_List[index];
				this.m_Dictionary.Remove(key);
				this.m_List[index] = value;
				this.m_Dictionary.Add(key, index);
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00018518 File Offset: 0x00016718
		public void RemoveAll(Predicate<T> match)
		{
			int i = 0;
			while (i < this.m_List.Count)
			{
				T t = this.m_List[i];
				if (match(t))
				{
					this.Remove(t);
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00018568 File Offset: 0x00016768
		public void Sort(Comparison<T> sortLayoutFunction)
		{
			this.m_List.Sort(sortLayoutFunction);
			for (int i = 0; i < this.m_List.Count; i++)
			{
				T key = this.m_List[i];
				this.m_Dictionary[key] = i;
			}
		}

		// Token: 0x040002A3 RID: 675
		private readonly List<T> m_List = new List<T>();

		// Token: 0x040002A4 RID: 676
		private Dictionary<T, int> m_Dictionary = new Dictionary<T, int>();
	}
}
