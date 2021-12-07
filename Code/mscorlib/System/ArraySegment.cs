using System;

namespace System
{
	/// <summary>Delimits a section of a one-dimensional array.</summary>
	/// <typeparam name="T">The type of the elements in the array segment.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006D3 RID: 1747
	[Serializable]
	public struct ArraySegment<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArraySegment`1" /> structure that delimits the specified range of the elements in the specified array.</summary>
		/// <param name="array">The array containing the range of elements to delimit.</param>
		/// <param name="offset">The zero-based index of the first element in the range.</param>
		/// <param name="count">The number of elements in the range.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> and <paramref name="count" /> do not specify a valid range in <paramref name="array" />.</exception>
		// Token: 0x0600433C RID: 17212 RVA: 0x000E5A00 File Offset: 0x000E3C00
		public ArraySegment(T[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (offset > array.Length)
			{
				throw new ArgumentException("out of bounds");
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException("out of bounds", "offset");
			}
			this.array = array;
			this.offset = offset;
			this.count = count;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArraySegment`1" /> structure that delimits all the elements in the specified array.</summary>
		/// <param name="array">The array to wrap.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		// Token: 0x0600433D RID: 17213 RVA: 0x000E5A90 File Offset: 0x000E3C90
		public ArraySegment(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			this.array = array;
			this.offset = 0;
			this.count = array.Length;
		}

		/// <summary>Gets the original array containing the range of elements that the array segment delimits.</summary>
		/// <returns>The original array that was passed to the constructor, and that contains the range delimited by the <see cref="T:System.ArraySegment`1" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x0600433E RID: 17214 RVA: 0x000E5AC8 File Offset: 0x000E3CC8
		public T[] Array
		{
			get
			{
				return this.array;
			}
		}

		/// <summary>Gets the position of the first element in the range delimited by the array segment, relative to the start of the original array.</summary>
		/// <returns>The position of the first element in the range delimited by the <see cref="T:System.ArraySegment`1" />, relative to the start of the original array.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x0600433F RID: 17215 RVA: 0x000E5AD0 File Offset: 0x000E3CD0
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}

		/// <summary>Gets the number of elements in the range delimited by the array segment.</summary>
		/// <returns>The number of elements in the range delimited by the <see cref="T:System.ArraySegment`1" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06004340 RID: 17216 RVA: 0x000E5AD8 File Offset: 0x000E3CD8
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>Determines whether the specified object is equal to the current instance.</summary>
		/// <returns>true if the specified object is a <see cref="T:System.ArraySegment`1" /> structure and is equal to the current instance; otherwise, false.</returns>
		/// <param name="obj">The object to be compared with the current instance.</param>
		// Token: 0x06004341 RID: 17217 RVA: 0x000E5AE0 File Offset: 0x000E3CE0
		public override bool Equals(object obj)
		{
			return obj is ArraySegment<T> && this.Equals((ArraySegment<T>)obj);
		}

		/// <summary>Determines whether the specified <see cref="T:System.ArraySegment`1" /> structure is equal to the current instance.</summary>
		/// <returns>true if the specified <see cref="T:System.ArraySegment`1" /> structure is equal to the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.ArraySegment`1" /> structure to be compared with the current instance.</param>
		// Token: 0x06004342 RID: 17218 RVA: 0x000E5AFC File Offset: 0x000E3CFC
		public bool Equals(ArraySegment<T> obj)
		{
			return this.array == obj.Array && this.offset == obj.Offset && this.count == obj.Count;
		}

		/// <summary>Returns the hash code for the current instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06004343 RID: 17219 RVA: 0x000E5B38 File Offset: 0x000E3D38
		public override int GetHashCode()
		{
			return this.array.GetHashCode() ^ this.offset ^ this.count;
		}

		/// <summary>Indicates whether two <see cref="T:System.ArraySegment`1" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.ArraySegment`1" /> structure on the left side of the equality operator.</param>
		/// <param name="b">The <see cref="T:System.ArraySegment`1" /> structure on the right side of the equality operator.</param>
		// Token: 0x06004344 RID: 17220 RVA: 0x000E5B54 File Offset: 0x000E3D54
		public static bool operator ==(ArraySegment<T> a, ArraySegment<T> b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether two <see cref="T:System.ArraySegment`1" /> structures are unequal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.ArraySegment`1" /> structure on the left side of the inequality operator.</param>
		/// <param name="b">The <see cref="T:System.ArraySegment`1" /> structure on the right side of the inequality operator.</param>
		// Token: 0x06004345 RID: 17221 RVA: 0x000E5B60 File Offset: 0x000E3D60
		public static bool operator !=(ArraySegment<T> a, ArraySegment<T> b)
		{
			return !a.Equals(b);
		}

		// Token: 0x04001C59 RID: 7257
		private T[] array;

		// Token: 0x04001C5A RID: 7258
		private int offset;

		// Token: 0x04001C5B RID: 7259
		private int count;
	}
}
