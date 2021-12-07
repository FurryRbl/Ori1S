using System;

namespace System.Collections.Specialized
{
	/// <summary>Supports a simple iteration over a <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
	// Token: 0x020000C2 RID: 194
	public class StringEnumerator
	{
		// Token: 0x06000878 RID: 2168 RVA: 0x000195E0 File Offset: 0x000177E0
		internal StringEnumerator(StringCollection coll)
		{
			this.enumerable = ((IEnumerable)coll).GetEnumerator();
		}

		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current element in the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x000195F4 File Offset: 0x000177F4
		public string Current
		{
			get
			{
				return (string)this.enumerable.Current;
			}
		}

		/// <summary>Advances the enumerator to the next element of the collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x0600087A RID: 2170 RVA: 0x00019608 File Offset: 0x00017808
		public bool MoveNext()
		{
			return this.enumerable.MoveNext();
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x0600087B RID: 2171 RVA: 0x00019618 File Offset: 0x00017818
		public void Reset()
		{
			this.enumerable.Reset();
		}

		// Token: 0x04000235 RID: 565
		private IEnumerator enumerable;
	}
}
