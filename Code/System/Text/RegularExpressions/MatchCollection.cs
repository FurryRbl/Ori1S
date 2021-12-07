using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the set of successful matches found by iteratively applying a regular expression pattern to the input string.</summary>
	// Token: 0x02000484 RID: 1156
	[Serializable]
	public class MatchCollection : ICollection, IEnumerable
	{
		// Token: 0x0600294F RID: 10575 RVA: 0x00088620 File Offset: 0x00086820
		internal MatchCollection(Match start)
		{
			this.current = start;
			this.list = new ArrayList();
		}

		/// <summary>Gets the number of matches.</summary>
		/// <returns>The number of matches.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06002950 RID: 10576 RVA: 0x0008863C File Offset: 0x0008683C
		public int Count
		{
			get
			{
				return this.FullList.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the collection is read only.</summary>
		/// <returns>This value of this property is always true.</returns>
		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06002951 RID: 10577 RVA: 0x0008864C File Offset: 0x0008684C
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>The value of this property is always false.</returns>
		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06002952 RID: 10578 RVA: 0x00088650 File Offset: 0x00086850
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an individual member of the collection.</summary>
		/// <returns>The captured substring at position <paramref name="i" /> in the collection.</returns>
		/// <param name="i">Index into the Match collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="i" /> is less than 0 or greater than or equal to <see cref="P:System.Text.RegularExpressions.MatchCollection.Count" />. </exception>
		// Token: 0x17000B80 RID: 2944
		public virtual Match this[int i]
		{
			get
			{
				if (i < 0 || !this.TryToGet(i))
				{
					throw new ArgumentOutOfRangeException("i");
				}
				return (i >= this.list.Count) ? this.current : ((Match)this.list[i]);
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection. This property always returns the object itself.</returns>
		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06002954 RID: 10580 RVA: 0x000886AC File Offset: 0x000868AC
		public object SyncRoot
		{
			get
			{
				return this.list;
			}
		}

		/// <summary>Copies all the elements of the collection to the given array starting at the given index.</summary>
		/// <param name="array">The array the collection is to be copied into. </param>
		/// <param name="arrayIndex">The position in the array where copying is to begin. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is a multi-dimensional array.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="arrayIndex" /> is outside the bounds of <paramref name="array" />.-or-<paramref name="arrayIndex" /> plus <see cref="P:System.Text.RegularExpressions.GroupCollection.Count" /> is outside the bounds of <paramref name="array" />.</exception>
		// Token: 0x06002955 RID: 10581 RVA: 0x000886B4 File Offset: 0x000868B4
		public void CopyTo(Array array, int index)
		{
			this.FullList.CopyTo(array, index);
		}

		/// <summary>Provides an enumerator in the same order as <see cref="P:System.Text.RegularExpressions.MatchCollection.Item(System.Int32)" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that contains all Match objects within the MatchCollection.</returns>
		// Token: 0x06002956 RID: 10582 RVA: 0x000886C4 File Offset: 0x000868C4
		public IEnumerator GetEnumerator()
		{
			IEnumerator result;
			if (this.current.Success)
			{
				IEnumerator enumerator = new MatchCollection.Enumerator(this);
				result = enumerator;
			}
			else
			{
				result = this.list.GetEnumerator();
			}
			return result;
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x000886FC File Offset: 0x000868FC
		private bool TryToGet(int i)
		{
			while (i > this.list.Count && this.current.Success)
			{
				this.list.Add(this.current);
				this.current = this.current.NextMatch();
			}
			return i < this.list.Count || this.current.Success;
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06002958 RID: 10584 RVA: 0x00088774 File Offset: 0x00086974
		private ICollection FullList
		{
			get
			{
				if (this.TryToGet(2147483647))
				{
					throw new SystemException("too many matches");
				}
				return this.list;
			}
		}

		// Token: 0x040019F3 RID: 6643
		private Match current;

		// Token: 0x040019F4 RID: 6644
		private ArrayList list;

		// Token: 0x02000485 RID: 1157
		private class Enumerator : IEnumerator
		{
			// Token: 0x06002959 RID: 10585 RVA: 0x00088798 File Offset: 0x00086998
			internal Enumerator(MatchCollection coll)
			{
				this.coll = coll;
				this.index = -1;
			}

			// Token: 0x0600295A RID: 10586 RVA: 0x000887B0 File Offset: 0x000869B0
			void IEnumerator.Reset()
			{
				this.index = -1;
			}

			// Token: 0x17000B83 RID: 2947
			// (get) Token: 0x0600295B RID: 10587 RVA: 0x000887BC File Offset: 0x000869BC
			object IEnumerator.Current
			{
				get
				{
					if (this.index < 0)
					{
						throw new InvalidOperationException("'Current' called before 'MoveNext()'");
					}
					if (this.index > this.coll.list.Count)
					{
						throw new SystemException("MatchCollection in invalid state");
					}
					if (this.index == this.coll.list.Count && !this.coll.current.Success)
					{
						throw new InvalidOperationException("'Current' called after 'MoveNext()' returned false");
					}
					return (this.index >= this.coll.list.Count) ? this.coll.current : this.coll.list[this.index];
				}
			}

			// Token: 0x0600295C RID: 10588 RVA: 0x00088884 File Offset: 0x00086A84
			bool IEnumerator.MoveNext()
			{
				if (this.index > this.coll.list.Count)
				{
					throw new SystemException("MatchCollection in invalid state");
				}
				return (this.index != this.coll.list.Count || this.coll.current.Success) && this.coll.TryToGet(++this.index);
			}

			// Token: 0x040019F5 RID: 6645
			private int index;

			// Token: 0x040019F6 RID: 6646
			private MatchCollection coll;
		}
	}
}
