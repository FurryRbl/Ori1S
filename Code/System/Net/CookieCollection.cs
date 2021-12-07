using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Net
{
	/// <summary>Provides a collection container for instances of the <see cref="T:System.Net.Cookie" /> class.</summary>
	// Token: 0x020002EF RID: 751
	[Serializable]
	public class CookieCollection : ICollection, IEnumerable
	{
		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x0004611C File Offset: 0x0004431C
		internal IList<Cookie> List
		{
			get
			{
				return this.list;
			}
		}

		/// <summary>Gets the number of cookies contained in a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <returns>The number of cookies contained in a <see cref="T:System.Net.CookieCollection" />.</returns>
		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001990 RID: 6544 RVA: 0x00046124 File Offset: 0x00044324
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to a <see cref="T:System.Net.CookieCollection" /> is thread safe.</summary>
		/// <returns>true if access to the <see cref="T:System.Net.CookieCollection" /> is thread safe; otherwise, false. The default is false.</returns>
		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x00046134 File Offset: 0x00044334
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that you can use to synchronize access to the <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <returns>An object that you can use to synchronize access to the <see cref="T:System.Net.CookieCollection" />.</returns>
		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x00046138 File Offset: 0x00044338
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the elements of a <see cref="T:System.Net.CookieCollection" /> to an instance of the <see cref="T:System.Array" /> class, starting at a particular index.</summary>
		/// <param name="array">The target <see cref="T:System.Array" /> to which the <see cref="T:System.Net.CookieCollection" /> will be copied. </param>
		/// <param name="index">The zero-based index in the target <see cref="T:System.Array" /> where copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in this <see cref="T:System.Net.CookieCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.CookieCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x06001993 RID: 6547 RVA: 0x0004613C File Offset: 0x0004433C
		public void CopyTo(Array array, int index)
		{
			((ICollection)this.list).CopyTo(array, index);
		}

		/// <summary>Copies the elements of this <see cref="T:System.Net.CookieCollection" /> to a <see cref="T:System.Net.Cookie" /> array starting at the specified index of the target array.</summary>
		/// <param name="array">The target <see cref="T:System.Net.Cookie" /> array to which the <see cref="T:System.Net.CookieCollection" /> will be copied.</param>
		/// <param name="index">The zero-based index in the target <see cref="T:System.Array" /> where copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in this <see cref="T:System.Net.CookieCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The elements in this <see cref="T:System.Net.CookieCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x06001994 RID: 6548 RVA: 0x0004614C File Offset: 0x0004434C
		public void CopyTo(Cookie[] array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Gets an enumerator that can iterate through a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <returns>An instance of an implementation of an <see cref="T:System.Collections.IEnumerator" /> interface that can iterate through a <see cref="T:System.Net.CookieCollection" />.</returns>
		// Token: 0x06001995 RID: 6549 RVA: 0x0004615C File Offset: 0x0004435C
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Net.CookieCollection" /> is read-only.</summary>
		/// <returns>true if this is a read-only <see cref="T:System.Net.CookieCollection" />; otherwise, false. The default is true.</returns>
		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x00046170 File Offset: 0x00044370
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Adds a <see cref="T:System.Net.Cookie" /> to a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <param name="cookie">The <see cref="T:System.Net.Cookie" /> to be added to a <see cref="T:System.Net.CookieCollection" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookie" /> is null. </exception>
		// Token: 0x06001997 RID: 6551 RVA: 0x00046174 File Offset: 0x00044374
		public void Add(Cookie cookie)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			int num = this.SearchCookie(cookie);
			if (num == -1)
			{
				this.list.Add(cookie);
			}
			else
			{
				this.list[num] = cookie;
			}
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x000461C0 File Offset: 0x000443C0
		internal void Sort()
		{
			if (this.list.Count > 0)
			{
				this.list.Sort(CookieCollection.Comparer);
			}
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x000461E4 File Offset: 0x000443E4
		private int SearchCookie(Cookie cookie)
		{
			string name = cookie.Name;
			string domain = cookie.Domain;
			string path = cookie.Path;
			for (int i = this.list.Count - 1; i >= 0; i--)
			{
				Cookie cookie2 = this.list[i];
				if (cookie2.Version == cookie.Version)
				{
					if (string.Compare(domain, cookie2.Domain, true, CultureInfo.InvariantCulture) == 0)
					{
						if (string.Compare(name, cookie2.Name, true, CultureInfo.InvariantCulture) == 0)
						{
							if (string.Compare(path, cookie2.Path, true, CultureInfo.InvariantCulture) == 0)
							{
								return i;
							}
						}
					}
				}
			}
			return -1;
		}

		/// <summary>Adds the contents of a <see cref="T:System.Net.CookieCollection" /> to the current instance.</summary>
		/// <param name="cookies">The <see cref="T:System.Net.CookieCollection" /> to be added. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cookies" /> is null. </exception>
		// Token: 0x0600199A RID: 6554 RVA: 0x000462A4 File Offset: 0x000444A4
		public void Add(CookieCollection cookies)
		{
			if (cookies == null)
			{
				throw new ArgumentNullException("cookies");
			}
			foreach (object obj in cookies)
			{
				Cookie cookie = (Cookie)obj;
				this.Add(cookie);
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.Cookie" /> with a specific index from a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <returns>A <see cref="T:System.Net.Cookie" /> with a specific index from a <see cref="T:System.Net.CookieCollection" />.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Net.Cookie" /> to be found. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or <paramref name="index" /> is greater than or equal to <see cref="P:System.Net.CookieCollection.Count" />. </exception>
		// Token: 0x1700062A RID: 1578
		public Cookie this[int index]
		{
			get
			{
				if (index < 0 || index >= this.list.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.list[index];
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.Cookie" /> with a specific name from a <see cref="T:System.Net.CookieCollection" />.</summary>
		/// <returns>The <see cref="T:System.Net.Cookie" /> with a specific name from a <see cref="T:System.Net.CookieCollection" />.</returns>
		/// <param name="name">The name of the <see cref="T:System.Net.Cookie" /> to be found. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x1700062B RID: 1579
		public Cookie this[string name]
		{
			get
			{
				foreach (Cookie cookie in this.list)
				{
					if (string.Compare(cookie.Name, name, true, CultureInfo.InvariantCulture) == 0)
					{
						return cookie;
					}
				}
				return null;
			}
		}

		// Token: 0x0400100E RID: 4110
		private List<Cookie> list = new List<Cookie>();

		// Token: 0x0400100F RID: 4111
		private static CookieCollection.CookieCollectionComparer Comparer = new CookieCollection.CookieCollectionComparer();

		// Token: 0x020002F0 RID: 752
		private sealed class CookieCollectionComparer : IComparer<Cookie>
		{
			// Token: 0x0600199E RID: 6558 RVA: 0x000463DC File Offset: 0x000445DC
			public int Compare(Cookie x, Cookie y)
			{
				if (x == null || y == null)
				{
					return 0;
				}
				int num = x.Name.Length + x.Value.Length;
				int num2 = y.Name.Length + y.Value.Length;
				return num - num2;
			}
		}
	}
}
