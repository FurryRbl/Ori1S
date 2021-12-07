using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents a collection of captured groups in a single match. </summary>
	// Token: 0x0200047B RID: 1147
	[Serializable]
	public class GroupCollection : ICollection, IEnumerable
	{
		// Token: 0x060028F7 RID: 10487 RVA: 0x00085F30 File Offset: 0x00084130
		internal GroupCollection(int n, int gap)
		{
			this.list = new Group[n];
			this.gap = gap;
		}

		/// <summary>Returns the number of groups in the collection.</summary>
		/// <returns>The number of groups in the collection.</returns>
		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x060028F8 RID: 10488 RVA: 0x00085F4C File Offset: 0x0008414C
		public int Count
		{
			get
			{
				return this.list.Length;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true if GroupCollection is read-only; otherwise false.</returns>
		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x060028F9 RID: 10489 RVA: 0x00085F58 File Offset: 0x00084158
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether access to the GroupCollection is synchronized (thread-safe).</summary>
		/// <returns>true if access is synchronized; otherwise false.</returns>
		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x060028FA RID: 10490 RVA: 0x00085F5C File Offset: 0x0008415C
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Enables access to a member of the collection by integer index.</summary>
		/// <returns>The member of the collection specified by <paramref name="groupnum" />.</returns>
		/// <param name="groupnum">The zero-based index of the collection member to be retrieved. </param>
		// Token: 0x17000B63 RID: 2915
		public Group this[int i]
		{
			get
			{
				if (i >= this.gap)
				{
					Match match = (Match)this.list[0];
					i = ((match != Match.Empty) ? match.Regex.GetGroupIndex(i) : -1);
				}
				return (i >= 0) ? this.list[i] : Group.Fail;
			}
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x00085FC0 File Offset: 0x000841C0
		internal void SetValue(Group g, int i)
		{
			this.list[i] = g;
		}

		/// <summary>Enables access to a member of the collection by string index.</summary>
		/// <returns>The member of the collection specified by <paramref name="groupname" />.</returns>
		/// <param name="groupname">The name of a capturing group. </param>
		// Token: 0x17000B64 RID: 2916
		public Group this[string groupName]
		{
			get
			{
				Match match = (Match)this.list[0];
				if (match != Match.Empty)
				{
					int num = match.Regex.GroupNumberFromName(groupName);
					if (num != -1)
					{
						return this[num];
					}
				}
				return Group.Fail;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the GroupCollection.</summary>
		/// <returns>A copy of the <see cref="T:System.Text.RegularExpressions.Match" /> object to synchronize.</returns>
		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x060028FE RID: 10494 RVA: 0x00086014 File Offset: 0x00084214
		public object SyncRoot
		{
			get
			{
				return this.list;
			}
		}

		/// <summary>Copies all the elements of the collection to the given array beginning at the given index.</summary>
		/// <param name="array">The array the collection is to be copied into. </param>
		/// <param name="arrayIndex">The position in the destination array where the copying is to begin. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="arrayIndex" /> is outside the bounds of <paramref name="array" />.-or-<paramref name="arrayIndex" /> plus <see cref="P:System.Text.RegularExpressions.GroupCollection.Count" /> is outside the bounds of <paramref name="array" />.</exception>
		// Token: 0x060028FF RID: 10495 RVA: 0x0008601C File Offset: 0x0008421C
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that can iterate through the collection.</summary>
		/// <returns>An object that contains all <see cref="T:System.Text.RegularExpressions.Group" /> objects in the <see cref="T:System.Text.RegularExpressions.GroupCollection" />.</returns>
		// Token: 0x06002900 RID: 10496 RVA: 0x0008602C File Offset: 0x0008422C
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x040019CB RID: 6603
		private Group[] list;

		// Token: 0x040019CC RID: 6604
		private int gap;
	}
}
