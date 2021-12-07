using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents a string comparison operation that uses specific case and culture-based or ordinal comparison rules.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000174 RID: 372
	[ComVisible(true)]
	[Serializable]
	public abstract class StringComparer : IComparer<string>, IEqualityComparer<string>, IComparer, IEqualityComparer
	{
		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-sensitive string comparison using the word comparison rules of the current culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x0004DF00 File Offset: 0x0004C100
		public static StringComparer CurrentCulture
		{
			get
			{
				return new CultureAwareComparer(CultureInfo.CurrentCulture, false);
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs case-insensitive string comparisons using the word comparison rules of the current culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x0004DF10 File Offset: 0x0004C110
		public static StringComparer CurrentCultureIgnoreCase
		{
			get
			{
				return new CultureAwareComparer(CultureInfo.CurrentCulture, true);
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-sensitive string comparison using the word comparison rules of the invariant culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x0004DF20 File Offset: 0x0004C120
		public static StringComparer InvariantCulture
		{
			get
			{
				return StringComparer.invariantCulture;
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-insensitive string comparison using the word comparison rules of the invariant culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x0004DF28 File Offset: 0x0004C128
		public static StringComparer InvariantCultureIgnoreCase
		{
			get
			{
				return StringComparer.invariantCultureIgnoreCase;
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-sensitive ordinal string comparison.</summary>
		/// <returns>A <see cref="T:System.StringComparer" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x0004DF30 File Offset: 0x0004C130
		public static StringComparer Ordinal
		{
			get
			{
				return StringComparer.ordinal;
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-insensitive ordinal string comparison.</summary>
		/// <returns>A <see cref="T:System.StringComparer" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x0004DF38 File Offset: 0x0004C138
		public static StringComparer OrdinalIgnoreCase
		{
			get
			{
				return StringComparer.ordinalIgnoreCase;
			}
		}

		/// <summary>Creates a <see cref="T:System.StringComparer" /> object that compares strings according to the rules of a specified culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object that performs string comparisons according to the comparison rules used by the <paramref name="culture" /> parameter and the case rule specified by the <paramref name="ignoreCase" /> parameter.</returns>
		/// <param name="culture">A culture whose linguistic rules are used to perform a string comparison.</param>
		/// <param name="ignoreCase">true to specify that comparison operations be case-insensitive; false to specify that comparison operations be case-sensitive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001393 RID: 5011 RVA: 0x0004DF40 File Offset: 0x0004C140
		public static StringComparer Create(CultureInfo culture, bool ignoreCase)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return new CultureAwareComparer(culture, ignoreCase);
		}

		/// <summary>When overridden in a derived class, compares two objects and returns an indication of their relative sort order.</summary>
		/// <returns>ValueMeaningLess than zero<paramref name="x" /> is less than <paramref name="y" />. -or-<paramref name="x" /> is null.Zero<paramref name="x" /> is equal to <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than <paramref name="y" />.-or-<paramref name="y" /> is null.</returns>
		/// <param name="x">An object to compare to <paramref name="y" />.</param>
		/// <param name="y">An object to compare to <paramref name="x" />.</param>
		/// <exception cref="T:System.ArgumentException">Neither <paramref name="x" /> nor <paramref name="y" /> is a <see cref="T:System.String" /> object, and neither <paramref name="x" /> nor <paramref name="y" /> implements the <see cref="T:System.IComparable" /> interface.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001394 RID: 5012 RVA: 0x0004DF5C File Offset: 0x0004C15C
		public int Compare(object x, object y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			string text = x as string;
			if (text != null)
			{
				string text2 = y as string;
				if (text2 != null)
				{
					return this.Compare(text, text2);
				}
			}
			IComparable comparable = x as IComparable;
			if (comparable == null)
			{
				throw new ArgumentException();
			}
			return comparable.CompareTo(y);
		}

		/// <summary>When overridden in a derived class, indicates whether two objects are equal.</summary>
		/// <returns>true if <paramref name="x" /> and <paramref name="y" /> refer to the same object, or <paramref name="x" /> and <paramref name="y" /> are both the same type of object and those objects are equal; otherwise, false.</returns>
		/// <param name="x">An object to compare to <paramref name="y" />.</param>
		/// <param name="y">An object to compare to <paramref name="x" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001395 RID: 5013 RVA: 0x0004DFC0 File Offset: 0x0004C1C0
		public bool Equals(object x, object y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			string text = x as string;
			if (text != null)
			{
				string text2 = y as string;
				if (text2 != null)
				{
					return this.Equals(text, text2);
				}
			}
			return x.Equals(y);
		}

		/// <summary>When overridden in a derived class, gets the hash code for the specified object.</summary>
		/// <returns>A 32-bit signed hash code calculated from the value of the <paramref name="obj" /> parameter.</returns>
		/// <param name="obj">An object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001396 RID: 5014 RVA: 0x0004E010 File Offset: 0x0004C210
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string text = obj as string;
			return (text != null) ? this.GetHashCode(text) : obj.GetHashCode();
		}

		/// <summary>When overridden in a derived class, compares two strings and returns an indication of their relative sort order.</summary>
		/// <returns>ValueMeaningLess than zero<paramref name="x" /> is less than <paramref name="y" />.-or-<paramref name="x" /> is null.Zero<paramref name="x" /> is equal to <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than <paramref name="y" />.-or-<paramref name="y" /> is null.</returns>
		/// <param name="x">A string to compare to <paramref name="y" />.</param>
		/// <param name="y">A string to compare to <paramref name="x" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001397 RID: 5015
		public abstract int Compare(string x, string y);

		/// <summary>When overridden in a derived class, indicates whether two strings are equal.</summary>
		/// <returns>true if <paramref name="x" /> and <paramref name="y" /> refer to the same object, or <paramref name="x" /> and <paramref name="y" /> are equal; otherwise, false.</returns>
		/// <param name="x">A string to compare to <paramref name="y" />.</param>
		/// <param name="y">A string to compare to <paramref name="x" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001398 RID: 5016
		public abstract bool Equals(string x, string y);

		/// <summary>When overridden in a derived class, gets the hash code for the specified string.</summary>
		/// <returns>A 32-bit signed hash code calculated from the value of the <paramref name="obj" /> parameter.</returns>
		/// <param name="obj">A string.</param>
		/// <exception cref="T:System.ArgumentException">Not enough memory is available to allocate the buffer required to compute the hash code.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001399 RID: 5017
		public abstract int GetHashCode(string obj);

		// Token: 0x040005AC RID: 1452
		private static StringComparer invariantCultureIgnoreCase = new CultureAwareComparer(CultureInfo.InvariantCulture, true);

		// Token: 0x040005AD RID: 1453
		private static StringComparer invariantCulture = new CultureAwareComparer(CultureInfo.InvariantCulture, false);

		// Token: 0x040005AE RID: 1454
		private static StringComparer ordinalIgnoreCase = new OrdinalComparer(true);

		// Token: 0x040005AF RID: 1455
		private static StringComparer ordinal = new OrdinalComparer(false);
	}
}
