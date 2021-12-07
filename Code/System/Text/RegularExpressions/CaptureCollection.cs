using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the set of captures made by a single capturing group.</summary>
	// Token: 0x0200046C RID: 1132
	[Serializable]
	public class CaptureCollection : ICollection, IEnumerable
	{
		// Token: 0x06002867 RID: 10343 RVA: 0x00080474 File Offset: 0x0007E674
		internal CaptureCollection(int n)
		{
			this.list = new Capture[n];
		}

		/// <summary>Gets the number of substrings captured by the group.</summary>
		/// <returns>The number of items in the <see cref="T:System.Text.RegularExpressions.CaptureCollection" />.</returns>
		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06002868 RID: 10344 RVA: 0x00080488 File Offset: 0x0007E688
		public int Count
		{
			get
			{
				return this.list.Length;
			}
		}

		/// <summary>Gets a value that indicates whether the collection is read only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06002869 RID: 10345 RVA: 0x00080494 File Offset: 0x0007E694
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x0600286A RID: 10346 RVA: 0x00080498 File Offset: 0x0007E698
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an individual member of the collection.</summary>
		/// <returns>The captured substring at position <paramref name="i" /> in the collection.</returns>
		/// <param name="i">Index into the capture collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="i" /> is less than 0 or greater than <see cref="P:System.Text.RegularExpressions.CaptureCollection.Count" />. </exception>
		// Token: 0x17000B50 RID: 2896
		public Capture this[int i]
		{
			get
			{
				if (i < 0 || i >= this.Count)
				{
					throw new ArgumentOutOfRangeException("Index is out of range");
				}
				return this.list[i];
			}
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x000804D0 File Offset: 0x0007E6D0
		internal void SetValue(Capture cap, int i)
		{
			this.list[i] = cap;
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x0600286D RID: 10349 RVA: 0x000804DC File Offset: 0x0007E6DC
		public object SyncRoot
		{
			get
			{
				return this.list;
			}
		}

		/// <summary>Copies all the elements of the collection to the given array beginning at the given index.</summary>
		/// <param name="array">The array the collection is to be copied into. </param>
		/// <param name="arrayIndex">The position in the destination array where copying is to begin. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array " />is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is outside the bounds of <paramref name="array" />. -or-<paramref name="arrayIndex" /> plus <see cref="P:System.Text.RegularExpressions.CaptureCollection.Count" /> is outside the bounds of <paramref name="array" />. </exception>
		// Token: 0x0600286E RID: 10350 RVA: 0x000804E4 File Offset: 0x0007E6E4
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Provides an enumerator that iterates through the collection.</summary>
		/// <returns>An object that contains all <see cref="T:System.Text.RegularExpressions.Capture" /> objects within the <see cref="T:System.Text.RegularExpressions.CaptureCollection" />.</returns>
		// Token: 0x0600286F RID: 10351 RVA: 0x000804F4 File Offset: 0x0007E6F4
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x04001907 RID: 6407
		private Capture[] list;
	}
}
