using System;

namespace System.Collections.Specialized
{
	/// <summary>Creates collections that ignore the case in strings.</summary>
	// Token: 0x020000AE RID: 174
	public class CollectionsUtil
	{
		/// <summary>Creates a new case-insensitive instance of the <see cref="T:System.Collections.Hashtable" /> class with the default initial capacity.</summary>
		/// <returns>A new case-insensitive instance of the <see cref="T:System.Collections.Hashtable" /> class with the default initial capacity.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000777 RID: 1911 RVA: 0x00016FF4 File Offset: 0x000151F4
		public static Hashtable CreateCaseInsensitiveHashtable()
		{
			return new Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
		}

		/// <summary>Copies the entries from the specified dictionary to a new case-insensitive instance of the <see cref="T:System.Collections.Hashtable" /> class with the same initial capacity as the number of entries copied.</summary>
		/// <returns>A new case-insensitive instance of the <see cref="T:System.Collections.Hashtable" /> class containing the entries from the specified <see cref="T:System.Collections.IDictionary" />.</returns>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> to copy to a new case-insensitive <see cref="T:System.Collections.Hashtable" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		// Token: 0x06000778 RID: 1912 RVA: 0x00017008 File Offset: 0x00015208
		public static Hashtable CreateCaseInsensitiveHashtable(IDictionary d)
		{
			return new Hashtable(d, CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
		}

		/// <summary>Creates a new case-insensitive instance of the <see cref="T:System.Collections.Hashtable" /> class with the specified initial capacity.</summary>
		/// <returns>A new case-insensitive instance of the <see cref="T:System.Collections.Hashtable" /> class with the specified initial capacity.</returns>
		/// <param name="capacity">The approximate number of entries that the <see cref="T:System.Collections.Hashtable" /> can initially contain. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x06000779 RID: 1913 RVA: 0x0001701C File Offset: 0x0001521C
		public static Hashtable CreateCaseInsensitiveHashtable(int capacity)
		{
			return new Hashtable(capacity, CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Collections.SortedList" /> class that ignores the case of strings.</summary>
		/// <returns>A new instance of the <see cref="T:System.Collections.SortedList" /> class that ignores the case of strings.</returns>
		// Token: 0x0600077A RID: 1914 RVA: 0x00017030 File Offset: 0x00015230
		public static SortedList CreateCaseInsensitiveSortedList()
		{
			return new SortedList(CaseInsensitiveComparer.Default);
		}
	}
}
