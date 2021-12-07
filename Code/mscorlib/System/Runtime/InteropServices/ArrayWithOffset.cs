using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Encapsulates an array and an offset within the specified array.</summary>
	// Token: 0x0200036A RID: 874
	[ComVisible(true)]
	[Serializable]
	public struct ArrayWithOffset
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> structure.</summary>
		/// <param name="array">A managed array. </param>
		/// <param name="offset">The offset in bytes, of the element to be passed through platform invoke. </param>
		// Token: 0x06002A0F RID: 10767 RVA: 0x000920B0 File Offset: 0x000902B0
		public ArrayWithOffset(object array, int offset)
		{
			this.array = array;
			this.offset = offset;
		}

		/// <summary>Indicates whether the specified object matches the current <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object.</summary>
		/// <returns>true if the object matches this <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />; otherwise, false.</returns>
		/// <param name="obj">Object to compare with this instance. </param>
		// Token: 0x06002A10 RID: 10768 RVA: 0x000920C0 File Offset: 0x000902C0
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (!(obj is ArrayWithOffset))
			{
				return false;
			}
			ArrayWithOffset arrayWithOffset = (ArrayWithOffset)obj;
			return arrayWithOffset.array == this.array && arrayWithOffset.offset == this.offset;
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object matches the current instance.</summary>
		/// <returns>true if the specified <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object matches the current instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object to compare with this instance.</param>
		// Token: 0x06002A11 RID: 10769 RVA: 0x00092110 File Offset: 0x00090310
		public bool Equals(ArrayWithOffset obj)
		{
			return obj.array == this.array && obj.offset == this.offset;
		}

		/// <summary>Returns a hash code for this value type.</summary>
		/// <returns>The hash code for this instance.</returns>
		// Token: 0x06002A12 RID: 10770 RVA: 0x00092144 File Offset: 0x00090344
		public override int GetHashCode()
		{
			return this.offset;
		}

		/// <summary>Returns the managed array referenced by this <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" />.</summary>
		/// <returns>The managed array this instance references.</returns>
		// Token: 0x06002A13 RID: 10771 RVA: 0x0009214C File Offset: 0x0009034C
		public object GetArray()
		{
			return this.array;
		}

		/// <summary>Returns the offset provided when this <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> was constructed.</summary>
		/// <returns>The offset for this instance.</returns>
		// Token: 0x06002A14 RID: 10772 RVA: 0x00092154 File Offset: 0x00090354
		public int GetOffset()
		{
			return this.offset;
		}

		/// <summary>Determines whether two specified <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> objects have the same value.</summary>
		/// <returns>true if the value of <paramref name="a" /> is the same as the value of <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">An <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object to compare with the <paramref name="b" /> parameter. </param>
		/// <param name="b">An <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object to compare with the <paramref name="a" /> parameter.</param>
		// Token: 0x06002A15 RID: 10773 RVA: 0x0009215C File Offset: 0x0009035C
		public static bool operator ==(ArrayWithOffset a, ArrayWithOffset b)
		{
			return a.Equals(b);
		}

		/// <summary>Determines whether two specified <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> objects no not have the same value.</summary>
		/// <returns>true if the value of <paramref name="a" /> is not the same as the value of <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">An <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object to compare with the <paramref name="b" /> parameter. </param>
		/// <param name="b">An <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object to compare with the <paramref name="a" /> parameter.</param>
		// Token: 0x06002A16 RID: 10774 RVA: 0x00092168 File Offset: 0x00090368
		public static bool operator !=(ArrayWithOffset a, ArrayWithOffset b)
		{
			return !a.Equals(b);
		}

		// Token: 0x040010AB RID: 4267
		private object array;

		// Token: 0x040010AC RID: 4268
		private int offset;
	}
}
