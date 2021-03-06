using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Mono.Globalization.Unicode;

namespace System.Globalization
{
	/// <summary>Implements a set of methods for culture-sensitive string comparisons.</summary>
	// Token: 0x0200020C RID: 524
	[ComVisible(true)]
	[Serializable]
	public class CompareInfo : IDeserializationCallback
	{
		// Token: 0x060019CA RID: 6602 RVA: 0x0006060C File Offset: 0x0005E80C
		private CompareInfo()
		{
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x00060614 File Offset: 0x0005E814
		internal CompareInfo(CultureInfo ci)
		{
			this.culture = ci.LCID;
			if (CompareInfo.UseManagedCollation)
			{
				object obj = CompareInfo.monitor;
				lock (obj)
				{
					if (CompareInfo.collators == null)
					{
						CompareInfo.collators = new Hashtable();
					}
					this.collator = (SimpleCollator)CompareInfo.collators[ci.LCID];
					if (this.collator == null)
					{
						this.collator = new SimpleCollator(ci);
						CompareInfo.collators[ci.LCID] = this.collator;
					}
				}
			}
			else
			{
				this.icu_name = ci.IcuName;
				this.construct_compareinfo(this.icu_name);
			}
		}

		/// <summary>Runs when the entire object graph has been deserialized.</summary>
		/// <param name="sender">The object that initiated the callback. </param>
		// Token: 0x060019CD RID: 6605 RVA: 0x00060734 File Offset: 0x0005E934
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			if (CompareInfo.UseManagedCollation)
			{
				this.collator = new SimpleCollator(new CultureInfo(this.culture));
			}
			else
			{
				try
				{
					this.construct_compareinfo(this.icu_name);
				}
				catch
				{
				}
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060019CE RID: 6606 RVA: 0x0006079C File Offset: 0x0005E99C
		internal static bool UseManagedCollation
		{
			get
			{
				return CompareInfo.useManagedCollation;
			}
		}

		// Token: 0x060019CF RID: 6607
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void construct_compareinfo(string locale);

		// Token: 0x060019D0 RID: 6608
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void free_internal_collator();

		// Token: 0x060019D1 RID: 6609
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int internal_compare(string str1, int offset1, int length1, string str2, int offset2, int length2, CompareOptions options);

		// Token: 0x060019D2 RID: 6610
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void assign_sortkey(object key, string source, CompareOptions options);

		// Token: 0x060019D3 RID: 6611
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int internal_index(string source, int sindex, int count, char value, CompareOptions options, bool first);

		// Token: 0x060019D4 RID: 6612
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int internal_index(string source, int sindex, int count, string value, CompareOptions options, bool first);

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x060019D5 RID: 6613 RVA: 0x000607A4 File Offset: 0x0005E9A4
		~CompareInfo()
		{
			this.free_internal_collator();
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x000607E0 File Offset: 0x0005E9E0
		private int internal_compare_managed(string str1, int offset1, int length1, string str2, int offset2, int length2, CompareOptions options)
		{
			return this.collator.Compare(str1, offset1, length1, str2, offset2, length2, options);
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x00060804 File Offset: 0x0005EA04
		private int internal_compare_switch(string str1, int offset1, int length1, string str2, int offset2, int length2, CompareOptions options)
		{
			return (!CompareInfo.UseManagedCollation) ? this.internal_compare(str1, offset1, length1, str2, offset2, length2, options) : this.internal_compare_managed(str1, offset1, length1, str2, offset2, length2, options);
		}

		/// <summary>Compares two strings. </summary>
		/// <returns>Value Condition zero The two strings are equal. less than zero <paramref name="string1" /> is less than <paramref name="string2" />. greater than zero <paramref name="string1" /> is greater than <paramref name="string2" />. </returns>
		/// <param name="string1">The first string to compare. </param>
		/// <param name="string2">The second string to compare. </param>
		// Token: 0x060019D8 RID: 6616 RVA: 0x00060844 File Offset: 0x0005EA44
		public virtual int Compare(string string1, string string2)
		{
			return this.Compare(string1, string2, CompareOptions.None);
		}

		/// <summary>Compares two strings using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>Value Condition zero The two strings are equal. less than zero <paramref name="string1" /> is less than <paramref name="string2" />. greater than zero <paramref name="string1" /> is greater than <paramref name="string2" />. </returns>
		/// <param name="string1">The first string to compare. </param>
		/// <param name="string2">The second string to compare. </param>
		/// <param name="options">The <see cref="T:System.Globalization.CompareOptions" /> value that defines how <paramref name="string1" /> and <paramref name="string2" /> should be compared. <paramref name="options" /> is either the value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x060019D9 RID: 6617 RVA: 0x00060850 File Offset: 0x0005EA50
		public virtual int Compare(string string1, string string2, CompareOptions options)
		{
			if ((options & (CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort | CompareOptions.Ordinal | CompareOptions.OrdinalIgnoreCase)) != options)
			{
				throw new ArgumentException("options");
			}
			if (string1 == null)
			{
				if (string2 == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (string2 == null)
				{
					return 1;
				}
				if (string1.Length == 0 && string2.Length == 0)
				{
					return 0;
				}
				return this.internal_compare_switch(string1, 0, string1.Length, string2, 0, string2.Length, options);
			}
		}

		/// <summary>Compares the end section of a string with the end section of another string.</summary>
		/// <returns>Value Condition zero The two strings are equal. less than zero The specified section of <paramref name="string1" /> is less than the specified section of <paramref name="string2" />. greater than zero The specified section of <paramref name="string1" /> is greater than the specified section of <paramref name="string2" />. </returns>
		/// <param name="string1">The first string to compare. </param>
		/// <param name="offset1">The zero-based index of the character in <paramref name="string1" /> at which to start comparing. </param>
		/// <param name="string2">The second string to compare. </param>
		/// <param name="offset2">The zero-based index of the character in <paramref name="string2" /> at which to start comparing. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset1" /> or <paramref name="offset2" /> is less than zero.-or- <paramref name="offset1" /> is greater than or equal to the number of characters in <paramref name="string1" />.-or- <paramref name="offset2" /> is greater than or equal to the number of characters in <paramref name="string2" />. </exception>
		// Token: 0x060019DA RID: 6618 RVA: 0x000608BC File Offset: 0x0005EABC
		public virtual int Compare(string string1, int offset1, string string2, int offset2)
		{
			return this.Compare(string1, offset1, string2, offset2, CompareOptions.None);
		}

		/// <summary>Compares the end section of a string with the end section of another string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>Value Condition zero The two strings are equal. less than zero The specified section of <paramref name="string1" /> is less than the specified section of <paramref name="string2" />. greater than zero The specified section of <paramref name="string1" /> is greater than the specified section of <paramref name="string2" />. </returns>
		/// <param name="string1">The first string to compare. </param>
		/// <param name="offset1">The zero-based index of the character in <paramref name="string1" /> at which to start comparing. </param>
		/// <param name="string2">The second string to compare. </param>
		/// <param name="offset2">The zero-based index of the character in <paramref name="string2" /> at which to start comparing. </param>
		/// <param name="options">The <see cref="T:System.Globalization.CompareOptions" /> value that defines how <paramref name="string1" /> and <paramref name="string2" /> should be compared. <paramref name="options" /> is either the value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset1" /> or <paramref name="offset2" /> is less than zero.-or- <paramref name="offset1" /> is greater than or equal to the number of characters in <paramref name="string1" />.-or- <paramref name="offset2" /> is greater than or equal to the number of characters in <paramref name="string2" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x060019DB RID: 6619 RVA: 0x000608CC File Offset: 0x0005EACC
		public virtual int Compare(string string1, int offset1, string string2, int offset2, CompareOptions options)
		{
			if ((options & (CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort | CompareOptions.Ordinal | CompareOptions.OrdinalIgnoreCase)) != options)
			{
				throw new ArgumentException("options");
			}
			if (string1 == null)
			{
				if (string2 == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (string2 == null)
				{
					return 1;
				}
				if ((string1.Length == 0 || offset1 == string1.Length) && (string2.Length == 0 || offset2 == string2.Length))
				{
					return 0;
				}
				if (offset1 < 0 || offset2 < 0)
				{
					throw new ArgumentOutOfRangeException("Offsets must not be less than zero");
				}
				if (offset1 > string1.Length)
				{
					throw new ArgumentOutOfRangeException("Offset1 is greater than or equal to the length of string1");
				}
				if (offset2 > string2.Length)
				{
					throw new ArgumentOutOfRangeException("Offset2 is greater than or equal to the length of string2");
				}
				return this.internal_compare_switch(string1, offset1, string1.Length - offset1, string2, offset2, string2.Length - offset2, options);
			}
		}

		/// <summary>Compares a section of one string with a section of another string.</summary>
		/// <returns>Value Condition zero The two strings are equal. less than zero The specified section of <paramref name="string1" /> is less than the specified section of <paramref name="string2" />. greater than zero The specified section of <paramref name="string1" /> is greater than the specified section of <paramref name="string2" />. </returns>
		/// <param name="string1">The first string to compare. </param>
		/// <param name="offset1">The zero-based index of the character in <paramref name="string1" /> at which to start comparing. </param>
		/// <param name="length1">The number of consecutive characters in <paramref name="string1" /> to compare. </param>
		/// <param name="string2">The second string to compare. </param>
		/// <param name="offset2">The zero-based index of the character in <paramref name="string2" /> at which to start comparing. </param>
		/// <param name="length2">The number of consecutive characters in <paramref name="string2" /> to compare. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset1" /> or <paramref name="length1" /> or <paramref name="offset2" /> or <paramref name="length2" /> is less than zero.-or- <paramref name="offset1" /> is greater than or equal to the number of characters in <paramref name="string1" />.-or- <paramref name="offset2" /> is greater than or equal to the number of characters in <paramref name="string2" />.-or- <paramref name="length1" /> is greater than the number of characters from <paramref name="offset1" /> to the end of <paramref name="string1" />.-or- <paramref name="length2" /> is greater than the number of characters from <paramref name="offset2" /> to the end of <paramref name="string2" />. </exception>
		// Token: 0x060019DC RID: 6620 RVA: 0x000609A4 File Offset: 0x0005EBA4
		public virtual int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2)
		{
			return this.Compare(string1, offset1, length1, string2, offset2, length2, CompareOptions.None);
		}

		/// <summary>Compares a section of one string with a section of another string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>Value Condition zero The two strings are equal. less than zero The specified section of <paramref name="string1" /> is less than the specified section of <paramref name="string2" />. greater than zero The specified section of <paramref name="string1" /> is greater than the specified section of <paramref name="string2" />. </returns>
		/// <param name="string1">The first string to compare. </param>
		/// <param name="offset1">The zero-based index of the character in <paramref name="string1" /> at which to start comparing. </param>
		/// <param name="length1">The number of consecutive characters in <paramref name="string1" /> to compare. </param>
		/// <param name="string2">The second string to compare. </param>
		/// <param name="offset2">The zero-based index of the character in <paramref name="string2" /> at which to start comparing. </param>
		/// <param name="length2">The number of consecutive characters in <paramref name="string2" /> to compare. </param>
		/// <param name="options">The <see cref="T:System.Globalization.CompareOptions" /> value that defines how <paramref name="string1" /> and <paramref name="string2" /> should be compared. <paramref name="options" /> is either the value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset1" /> or <paramref name="length1" /> or <paramref name="offset2" /> or <paramref name="length2" /> is less than zero.-or- <paramref name="offset1" /> is greater than or equal to the number of characters in <paramref name="string1" />.-or- <paramref name="offset2" /> is greater than or equal to the number of characters in <paramref name="string2" />.-or- <paramref name="length1" /> is greater than the number of characters from <paramref name="offset1" /> to the end of <paramref name="string1" />.-or- <paramref name="length2" /> is greater than the number of characters from <paramref name="offset2" /> to the end of <paramref name="string2" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x060019DD RID: 6621 RVA: 0x000609C4 File Offset: 0x0005EBC4
		public virtual int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2, CompareOptions options)
		{
			if ((options & (CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort | CompareOptions.Ordinal | CompareOptions.OrdinalIgnoreCase)) != options)
			{
				throw new ArgumentException("options");
			}
			if (string1 == null)
			{
				if (string2 == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (string2 == null)
				{
					return 1;
				}
				if ((string1.Length == 0 || offset1 == string1.Length || length1 == 0) && (string2.Length == 0 || offset2 == string2.Length || length2 == 0))
				{
					return 0;
				}
				if (offset1 < 0 || length1 < 0 || offset2 < 0 || length2 < 0)
				{
					throw new ArgumentOutOfRangeException("Offsets and lengths must not be less than zero");
				}
				if (offset1 > string1.Length)
				{
					throw new ArgumentOutOfRangeException("Offset1 is greater than or equal to the length of string1");
				}
				if (offset2 > string2.Length)
				{
					throw new ArgumentOutOfRangeException("Offset2 is greater than or equal to the length of string2");
				}
				if (length1 > string1.Length - offset1)
				{
					throw new ArgumentOutOfRangeException("Length1 is greater than the number of characters from offset1 to the end of string1");
				}
				if (length2 > string2.Length - offset2)
				{
					throw new ArgumentOutOfRangeException("Length2 is greater than the number of characters from offset2 to the end of string2");
				}
				return this.internal_compare_switch(string1, offset1, length1, string2, offset2, length2, options);
			}
		}

		/// <summary>Determines whether the specified object is equal to the current <see cref="T:System.Globalization.CompareInfo" /> object.</summary>
		/// <returns>true if the specified object is equal to the current <see cref="T:System.Globalization.CompareInfo" />; otherwise, false.</returns>
		/// <param name="value">The object to compare with the current <see cref="T:System.Globalization.CompareInfo" />. </param>
		// Token: 0x060019DE RID: 6622 RVA: 0x00060AE4 File Offset: 0x0005ECE4
		public override bool Equals(object value)
		{
			CompareInfo compareInfo = value as CompareInfo;
			return compareInfo != null && compareInfo.culture == this.culture;
		}

		/// <summary>Initializes a new <see cref="T:System.Globalization.CompareInfo" /> object that is associated with the culture with the specified identifier.</summary>
		/// <returns>A new <see cref="T:System.Globalization.CompareInfo" /> object associated with the culture with the specified identifier and using string comparison methods in the current <see cref="T:System.Reflection.Assembly" />.</returns>
		/// <param name="culture">An integer representing the culture identifier. </param>
		// Token: 0x060019DF RID: 6623 RVA: 0x00060B10 File Offset: 0x0005ED10
		public static CompareInfo GetCompareInfo(int culture)
		{
			return new CultureInfo(culture).CompareInfo;
		}

		/// <summary>Initializes a new <see cref="T:System.Globalization.CompareInfo" /> object that is associated with the culture with the specified name.</summary>
		/// <returns>A new <see cref="T:System.Globalization.CompareInfo" /> object associated with the culture with the specified identifier and using string comparison methods in the current <see cref="T:System.Reflection.Assembly" />.</returns>
		/// <param name="name">A string representing the culture name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an invalid culture name. </exception>
		// Token: 0x060019E0 RID: 6624 RVA: 0x00060B20 File Offset: 0x0005ED20
		public static CompareInfo GetCompareInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return new CultureInfo(name).CompareInfo;
		}

		/// <summary>Initializes a new <see cref="T:System.Globalization.CompareInfo" /> object that is associated with the specified culture and that uses string comparison methods in the specified <see cref="T:System.Reflection.Assembly" />.</summary>
		/// <returns>A new <see cref="T:System.Globalization.CompareInfo" /> object associated with the culture with the specified identifier and using string comparison methods in the current <see cref="T:System.Reflection.Assembly" />.</returns>
		/// <param name="culture">An integer representing the culture identifier. </param>
		/// <param name="assembly">An <see cref="T:System.Reflection.Assembly" /> that contains the string comparison methods to use. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assembly" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assembly" /> is of an invalid type. </exception>
		// Token: 0x060019E1 RID: 6625 RVA: 0x00060B40 File Offset: 0x0005ED40
		public static CompareInfo GetCompareInfo(int culture, Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (assembly != typeof(object).Module.Assembly)
			{
				throw new ArgumentException("Assembly is an invalid type");
			}
			return CompareInfo.GetCompareInfo(culture);
		}

		/// <summary>Initializes a new <see cref="T:System.Globalization.CompareInfo" /> object that is associated with the specified culture and that uses string comparison methods in the specified <see cref="T:System.Reflection.Assembly" />.</summary>
		/// <returns>A new <see cref="T:System.Globalization.CompareInfo" /> object associated with the culture with the specified identifier and using string comparison methods in the current <see cref="T:System.Reflection.Assembly" />.</returns>
		/// <param name="name">A string representing the culture name. </param>
		/// <param name="assembly">An <see cref="T:System.Reflection.Assembly" /> that contains the string comparison methods to use. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="assembly" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an invalid culture name.-or- <paramref name="assembly" /> is of an invalid type. </exception>
		// Token: 0x060019E2 RID: 6626 RVA: 0x00060B8C File Offset: 0x0005ED8C
		public static CompareInfo GetCompareInfo(string name, Assembly assembly)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (assembly != typeof(object).Module.Assembly)
			{
				throw new ArgumentException("Assembly is an invalid type");
			}
			return CompareInfo.GetCompareInfo(name);
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.CompareInfo" /> for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.CompareInfo" />.</returns>
		// Token: 0x060019E3 RID: 6627 RVA: 0x00060BE8 File Offset: 0x0005EDE8
		public override int GetHashCode()
		{
			return this.LCID;
		}

		/// <summary>Gets the sort key for the specified string.</summary>
		/// <returns>The <see cref="T:System.Globalization.SortKey" /> object that contains the sort key for the specified string.</returns>
		/// <param name="source">The string for which a <see cref="T:System.Globalization.SortKey" /> object is obtained. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060019E4 RID: 6628 RVA: 0x00060BF0 File Offset: 0x0005EDF0
		public virtual SortKey GetSortKey(string source)
		{
			return this.GetSortKey(source, CompareOptions.None);
		}

		/// <summary>Gets a <see cref="T:System.Globalization.SortKey" /> object for the specified string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The <see cref="T:System.Globalization.SortKey" /> object that contains the sort key for the specified string.</returns>
		/// <param name="source">The string for which a <see cref="T:System.Globalization.SortKey" /> object is obtained. </param>
		/// <param name="options">The <see cref="T:System.Globalization.CompareOptions" /> value that define how the sort key is calculated. <paramref name="options" /> is a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060019E5 RID: 6629 RVA: 0x00060BFC File Offset: 0x0005EDFC
		public virtual SortKey GetSortKey(string source, CompareOptions options)
		{
			if (options == CompareOptions.OrdinalIgnoreCase || options == CompareOptions.Ordinal)
			{
				throw new ArgumentException("Now allowed CompareOptions.", "options");
			}
			if (CompareInfo.UseManagedCollation)
			{
				return this.collator.GetSortKey(source, options);
			}
			SortKey sortKey = new SortKey(this.culture, source, options);
			this.assign_sortkey(sortKey, source, options);
			return sortKey;
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the entire source string.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <paramref name="source" />, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		// Token: 0x060019E6 RID: 6630 RVA: 0x00060C68 File Offset: 0x0005EE68
		public virtual int IndexOf(string source, char value)
		{
			return this.IndexOf(source, value, 0, source.Length, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the entire source string.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <paramref name="source" />, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		// Token: 0x060019E7 RID: 6631 RVA: 0x00060C88 File Offset: 0x0005EE88
		public virtual int IndexOf(string source, string value)
		{
			return this.IndexOf(source, value, 0, source.Length, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <paramref name="source" /> using the specified <see cref="T:System.Globalization.CompareOptions" /> value, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="options">The <see cref="T:System.Globalization.CompareOptions" /> value that defines how the strings should be compared. <paramref name="options" /> is either the value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x060019E8 RID: 6632 RVA: 0x00060CA8 File Offset: 0x0005EEA8
		public virtual int IndexOf(string source, char value, CompareOptions options)
		{
			return this.IndexOf(source, value, 0, source.Length, options);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the section of the source string that extends from the specified index to the end of the string.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the section of <paramref name="source" /> that extends from <paramref name="startIndex" /> to the end of <paramref name="source" />, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />. </exception>
		// Token: 0x060019E9 RID: 6633 RVA: 0x00060CC8 File Offset: 0x0005EEC8
		public virtual int IndexOf(string source, char value, int startIndex)
		{
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <paramref name="source" /> using the specified <see cref="T:System.Globalization.CompareOptions" /> value, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="options">The <see cref="T:System.Globalization.CompareOptions" /> value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x060019EA RID: 6634 RVA: 0x00060CE8 File Offset: 0x0005EEE8
		public virtual int IndexOf(string source, string value, CompareOptions options)
		{
			return this.IndexOf(source, value, 0, source.Length, options);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the section of the source string that extends from the specified index to the end of the string.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the section of <paramref name="source" /> that extends from <paramref name="startIndex" /> to the end of <paramref name="source" />, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />. </exception>
		// Token: 0x060019EB RID: 6635 RVA: 0x00060D08 File Offset: 0x0005EF08
		public virtual int IndexOf(string source, string value, int startIndex)
		{
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the section of the source string that extends from the specified index to the end of the string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the section of <paramref name="source" /> that extends from <paramref name="startIndex" /> to the end of <paramref name="source" /> using the specified <see cref="T:System.Globalization.CompareOptions" /> value, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the search. </param>
		/// <param name="options">The <see cref="T:System.Globalization.CompareOptions" /> value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x060019EC RID: 6636 RVA: 0x00060D28 File Offset: 0x0005EF28
		public virtual int IndexOf(string source, char value, int startIndex, CompareOptions options)
		{
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, options);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the section of the source string that starts at the specified index and contains the specified number of elements.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified by <paramref name="count" />, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />. </exception>
		// Token: 0x060019ED RID: 6637 RVA: 0x00060D48 File Offset: 0x0005EF48
		public virtual int IndexOf(string source, char value, int startIndex, int count)
		{
			return this.IndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the section of the source string that extends from the specified index to the end of the string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the section of <paramref name="source" /> that extends from <paramref name="startIndex" /> to the end of <paramref name="source" /> using the specified <see cref="T:System.Globalization.CompareOptions" /> value, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the search. </param>
		/// <param name="options">The <see cref="T:System.Globalization.CompareOptions" /> value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x060019EE RID: 6638 RVA: 0x00060D58 File Offset: 0x0005EF58
		public virtual int IndexOf(string source, string value, int startIndex, CompareOptions options)
		{
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, options);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the section of the source string that starts at the specified index and contains the specified number of elements.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified by <paramref name="count" />, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />. </exception>
		// Token: 0x060019EF RID: 6639 RVA: 0x00060D78 File Offset: 0x0005EF78
		public virtual int IndexOf(string source, string value, int startIndex, int count)
		{
			return this.IndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x00060D88 File Offset: 0x0005EF88
		private int internal_index_managed(string s, int sindex, int count, char c, CompareOptions opt, bool first)
		{
			return (!first) ? this.collator.LastIndexOf(s, c, sindex, count, opt) : this.collator.IndexOf(s, c, sindex, count, opt);
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x00060DC8 File Offset: 0x0005EFC8
		private int internal_index_switch(string s, int sindex, int count, char c, CompareOptions opt, bool first)
		{
			return (!CompareInfo.UseManagedCollation || (first && opt == CompareOptions.Ordinal)) ? this.internal_index(s, sindex, count, c, opt, first) : this.internal_index_managed(s, sindex, count, c, opt, first);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the section of the source string that starts at the specified index and contains the specified number of elements using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified by <paramref name="count" />, using the specified <see cref="T:System.Globalization.CompareOptions" /> value, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <param name="options">The <see cref="T:System.Globalization.CompareOptions" /> value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x060019F2 RID: 6642 RVA: 0x00060E18 File Offset: 0x0005F018
		public virtual int IndexOf(string source, char value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (count < 0 || source.Length - startIndex < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if ((options & (CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.Ordinal | CompareOptions.OrdinalIgnoreCase)) != options)
			{
				throw new ArgumentException("options");
			}
			if (count == 0)
			{
				return -1;
			}
			if ((options & CompareOptions.Ordinal) != CompareOptions.None)
			{
				for (int i = startIndex; i < startIndex + count; i++)
				{
					if (source[i] == value)
					{
						return i;
					}
				}
				return -1;
			}
			return this.internal_index_switch(source, startIndex, count, value, options, true);
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00060ED0 File Offset: 0x0005F0D0
		private int internal_index_managed(string s1, int sindex, int count, string s2, CompareOptions opt, bool first)
		{
			return (!first) ? this.collator.LastIndexOf(s1, s2, sindex, count, opt) : this.collator.IndexOf(s1, s2, sindex, count, opt);
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x00060F10 File Offset: 0x0005F110
		private int internal_index_switch(string s1, int sindex, int count, string s2, CompareOptions opt, bool first)
		{
			return (!CompareInfo.UseManagedCollation || (first && opt == CompareOptions.Ordinal)) ? this.internal_index(s1, sindex, count, s2, opt, first) : this.internal_index_managed(s1, sindex, count, s2, opt, first);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the section of the source string that starts at the specified index and contains the specified number of elements using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified by <paramref name="count" />, using the specified <see cref="T:System.Globalization.CompareOptions" /> value, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <param name="options">The <see cref="T:System.Globalization.CompareOptions" /> value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x060019F5 RID: 6645 RVA: 0x00060F60 File Offset: 0x0005F160
		public virtual int IndexOf(string source, string value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (count < 0 || source.Length - startIndex < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if ((options & (CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.Ordinal | CompareOptions.OrdinalIgnoreCase)) != options)
			{
				throw new ArgumentException("options");
			}
			if (value.Length == 0)
			{
				return startIndex;
			}
			if (count == 0)
			{
				return -1;
			}
			return this.internal_index_switch(source, startIndex, count, value, options, true);
		}

		/// <summary>Determines whether the specified source string starts with the specified prefix.</summary>
		/// <returns>true if the length of <paramref name="prefix" /> is less than or equal to the length of <paramref name="source" /> and <paramref name="source" /> starts with <paramref name="prefix" />; otherwise, false.</returns>
		/// <param name="source">The string to search in. </param>
		/// <param name="prefix">The string to compare with the beginning of <paramref name="source" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="prefix" /> is null. </exception>
		// Token: 0x060019F6 RID: 6646 RVA: 0x00061004 File Offset: 0x0005F204
		public virtual bool IsPrefix(string source, string prefix)
		{
			return this.IsPrefix(source, prefix, CompareOptions.None);
		}

		/// <summary>Determines whether the specified source string starts with the specified prefix using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>true if the length of <paramref name="prefix" /> is less than or equal to the length of <paramref name="source" /> and <paramref name="source" /> starts with <paramref name="prefix" />; otherwise, false.</returns>
		/// <param name="source">The string to search in. </param>
		/// <param name="prefix">The string to compare with the beginning of <paramref name="source" />. </param>
		/// <param name="options">The <see cref="T:System.Globalization.CompareOptions" /> value that defines how <paramref name="source" /> and <paramref name="prefix" /> should be compared. <paramref name="options" /> is either the value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="prefix" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x060019F7 RID: 6647 RVA: 0x00061010 File Offset: 0x0005F210
		public virtual bool IsPrefix(string source, string prefix, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			if (CompareInfo.UseManagedCollation)
			{
				return this.collator.IsPrefix(source, prefix, options);
			}
			return source.Length >= prefix.Length && this.Compare(source, 0, prefix.Length, prefix, 0, prefix.Length, options) == 0;
		}

		/// <summary>Determines whether the specified source string ends with the specified suffix.</summary>
		/// <returns>true if the length of <paramref name="suffix" /> is less than or equal to the length of <paramref name="source" /> and <paramref name="source" /> ends with <paramref name="suffix" />; otherwise, false.</returns>
		/// <param name="source">The string to search in. </param>
		/// <param name="suffix">The string to compare with the end of <paramref name="source" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="suffix" /> is null. </exception>
		// Token: 0x060019F8 RID: 6648 RVA: 0x00061088 File Offset: 0x0005F288
		public virtual bool IsSuffix(string source, string suffix)
		{
			return this.IsSuffix(source, suffix, CompareOptions.None);
		}

		/// <summary>Determines whether the specified source string ends with the specified suffix using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>true if the length of <paramref name="suffix" /> is less than or equal to the length of <paramref name="source" /> and <paramref name="source" /> ends with <paramref name="suffix" />; otherwise, false.</returns>
		/// <param name="source">The string to search in. </param>
		/// <param name="suffix">The string to compare with the end of <paramref name="source" />. </param>
		/// <param name="options">The <see cref="T:System.Globalization.CompareOptions" /> value that defines how <paramref name="source" /> and <paramref name="suffix" /> should be compared. <paramref name="options" /> is either the value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="suffix" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x060019F9 RID: 6649 RVA: 0x00061094 File Offset: 0x0005F294
		public virtual bool IsSuffix(string source, string suffix, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (suffix == null)
			{
				throw new ArgumentNullException("suffix");
			}
			if (CompareInfo.UseManagedCollation)
			{
				return this.collator.IsSuffix(source, suffix, options);
			}
			return source.Length >= suffix.Length && this.Compare(source, source.Length - suffix.Length, suffix.Length, suffix, 0, suffix.Length, options) == 0;
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the entire source string.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the entire <paramref name="source" />, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		// Token: 0x060019FA RID: 6650 RVA: 0x00061118 File Offset: 0x0005F318
		public virtual int LastIndexOf(string source, char value)
		{
			return this.LastIndexOf(source, value, source.Length - 1, source.Length, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the entire source string.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the entire <paramref name="source" />, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		// Token: 0x060019FB RID: 6651 RVA: 0x0006113C File Offset: 0x0005F33C
		public virtual int LastIndexOf(string source, string value)
		{
			return this.LastIndexOf(source, value, source.Length - 1, source.Length, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the entire <paramref name="source" /> using the specified <see cref="T:System.Globalization.CompareOptions" /> value, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="options">The <see cref="T:System.Globalization.CompareOptions" /> value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x060019FC RID: 6652 RVA: 0x00061160 File Offset: 0x0005F360
		public virtual int LastIndexOf(string source, char value, CompareOptions options)
		{
			return this.LastIndexOf(source, value, source.Length - 1, source.Length, options);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the section of the source string that extends from the beginning of the string to the specified index.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the section of <paramref name="source" /> that extends from the beginning of <paramref name="source" /> to <paramref name="startIndex" />, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />. </exception>
		// Token: 0x060019FD RID: 6653 RVA: 0x00061184 File Offset: 0x0005F384
		public virtual int LastIndexOf(string source, char value, int startIndex)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the entire <paramref name="source" /> using the specified <see cref="T:System.Globalization.CompareOptions" /> value, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="options">The <see cref="T:System.Globalization.CompareOptions" /> value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x060019FE RID: 6654 RVA: 0x00061194 File Offset: 0x0005F394
		public virtual int LastIndexOf(string source, string value, CompareOptions options)
		{
			return this.LastIndexOf(source, value, source.Length - 1, source.Length, options);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the section of the source string that extends from the beginning of the string to the specified index.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the section of <paramref name="source" /> that extends from the beginning of <paramref name="source" /> to <paramref name="startIndex" />, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />. </exception>
		// Token: 0x060019FF RID: 6655 RVA: 0x000611B8 File Offset: 0x0005F3B8
		public virtual int LastIndexOf(string source, string value, int startIndex)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the section of the source string that extends from the beginning of the string to the specified index using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the section of <paramref name="source" /> that extends from the beginning of <paramref name="source" /> to <paramref name="startIndex" /> using the specified <see cref="T:System.Globalization.CompareOptions" /> value, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <param name="options">The <see cref="T:System.Globalization.CompareOptions" /> value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x06001A00 RID: 6656 RVA: 0x000611C8 File Offset: 0x0005F3C8
		public virtual int LastIndexOf(string source, char value, int startIndex, CompareOptions options)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, options);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the section of the source string that contains the specified number of elements and ends at the specified index.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the section of <paramref name="source" /> that contains the number of elements specified by <paramref name="count" /> and ends at <paramref name="startIndex" />, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />. </exception>
		// Token: 0x06001A01 RID: 6657 RVA: 0x000611D8 File Offset: 0x0005F3D8
		public virtual int LastIndexOf(string source, char value, int startIndex, int count)
		{
			return this.LastIndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the section of the source string that extends from the beginning of the string to the specified index using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the section of <paramref name="source" /> that extends from the beginning of <paramref name="source" /> to <paramref name="startIndex" /> using the specified <see cref="T:System.Globalization.CompareOptions" /> value, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <param name="options">The <see cref="T:System.Globalization.CompareOptions" /> value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x06001A02 RID: 6658 RVA: 0x000611E8 File Offset: 0x0005F3E8
		public virtual int LastIndexOf(string source, string value, int startIndex, CompareOptions options)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, options);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the section of the source string that contains the specified number of elements and ends at the specified index.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the section of <paramref name="source" /> that contains the number of elements specified by <paramref name="count" /> and ends at <paramref name="startIndex" />, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />. </exception>
		// Token: 0x06001A03 RID: 6659 RVA: 0x000611F8 File Offset: 0x0005F3F8
		public virtual int LastIndexOf(string source, string value, int startIndex, int count)
		{
			return this.LastIndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the section of the source string that contains the specified number of elements and ends at the specified index using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the section of <paramref name="source" /> that contains the number of elements specified by <paramref name="count" /> and ends at <paramref name="startIndex" /> using the specified <see cref="T:System.Globalization.CompareOptions" /> value, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <param name="options">The <see cref="T:System.Globalization.CompareOptions" /> value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x06001A04 RID: 6660 RVA: 0x00061208 File Offset: 0x0005F408
		public virtual int LastIndexOf(string source, char value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (count < 0 || startIndex - count < -1)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if ((options & (CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.Ordinal | CompareOptions.OrdinalIgnoreCase)) != options)
			{
				throw new ArgumentException("options");
			}
			if (count == 0)
			{
				return -1;
			}
			if ((options & CompareOptions.Ordinal) != CompareOptions.None)
			{
				for (int i = startIndex; i > startIndex - count; i--)
				{
					if (source[i] == value)
					{
						return i;
					}
				}
				return -1;
			}
			return this.internal_index_switch(source, startIndex, count, value, options, false);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the section of the source string that contains the specified number of elements and ends at the specified index using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the section of <paramref name="source" /> that contains the number of elements specified by <paramref name="count" /> and ends at <paramref name="startIndex" /> using the specified <see cref="T:System.Globalization.CompareOptions" /> value, if found; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <param name="options">The <see cref="T:System.Globalization.CompareOptions" /> value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x06001A05 RID: 6661 RVA: 0x000612BC File Offset: 0x0005F4BC
		public virtual int LastIndexOf(string source, string value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (count < 0 || startIndex - count < -1)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if ((options & (CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.Ordinal | CompareOptions.OrdinalIgnoreCase)) != options)
			{
				throw new ArgumentException("options");
			}
			if (count == 0)
			{
				return -1;
			}
			if (value.Length == 0)
			{
				return 0;
			}
			return this.internal_index_switch(source, startIndex, count, value, options, false);
		}

		/// <summary>Indicates whether a specified Unicode character is sortable.</summary>
		/// <returns>true if the <paramref name="ch" /> parameter is sortable; otherwise, false.</returns>
		/// <param name="ch">A Unicode character.</param>
		// Token: 0x06001A06 RID: 6662 RVA: 0x0006135C File Offset: 0x0005F55C
		[ComVisible(false)]
		public static bool IsSortable(char ch)
		{
			return MSCompatUnicodeTable.IsSortable((int)ch);
		}

		/// <summary>Indicates whether a specified Unicode string is sortable.</summary>
		/// <returns>true if the <paramref name="str" /> parameter is not an empty string ("") and all the Unicode characters in <paramref name="str" /> are sortable; otherwise, false.</returns>
		/// <param name="text">A string of zero or more Unicode characters.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null.</exception>
		// Token: 0x06001A07 RID: 6663 RVA: 0x00061364 File Offset: 0x0005F564
		[ComVisible(false)]
		public static bool IsSortable(string text)
		{
			return MSCompatUnicodeTable.IsSortable(text);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Globalization.CompareInfo" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Globalization.CompareInfo" />.</returns>
		// Token: 0x06001A08 RID: 6664 RVA: 0x0006136C File Offset: 0x0005F56C
		public override string ToString()
		{
			return "CompareInfo - " + this.culture;
		}

		/// <summary>Gets the properly formed culture identifier for the current <see cref="T:System.Globalization.CompareInfo" />.</summary>
		/// <returns>The properly formed culture identifier for the current <see cref="T:System.Globalization.CompareInfo" />.</returns>
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x00061384 File Offset: 0x0005F584
		public int LCID
		{
			get
			{
				return this.culture;
			}
		}

		/// <summary>Gets the name of the culture used for sorting operations by this <see cref="T:System.Globalization.CompareInfo" /> object.</summary>
		/// <returns>The name of a culture.</returns>
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x0006138C File Offset: 0x0005F58C
		[ComVisible(false)]
		public virtual string Name
		{
			get
			{
				return this.icu_name;
			}
		}

		// Token: 0x0400097E RID: 2430
		private const CompareOptions ValidCompareOptions_NoStringSort = CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.Ordinal | CompareOptions.OrdinalIgnoreCase;

		// Token: 0x0400097F RID: 2431
		private const CompareOptions ValidCompareOptions = CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort | CompareOptions.Ordinal | CompareOptions.OrdinalIgnoreCase;

		// Token: 0x04000980 RID: 2432
		private static readonly bool useManagedCollation = Environment.internalGetEnvironmentVariable("MONO_DISABLE_MANAGED_COLLATION") != "yes" && MSCompatUnicodeTable.IsReady;

		// Token: 0x04000981 RID: 2433
		private int culture;

		// Token: 0x04000982 RID: 2434
		[NonSerialized]
		private string icu_name;

		// Token: 0x04000983 RID: 2435
		private int win32LCID;

		// Token: 0x04000984 RID: 2436
		private string m_name;

		// Token: 0x04000985 RID: 2437
		[NonSerialized]
		private SimpleCollator collator;

		// Token: 0x04000986 RID: 2438
		private static Hashtable collators;

		// Token: 0x04000987 RID: 2439
		[NonSerialized]
		private static object monitor = new object();
	}
}
