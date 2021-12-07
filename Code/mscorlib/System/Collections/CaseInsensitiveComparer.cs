using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Compares two objects for equivalence, ignoring the case of strings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B3 RID: 435
	[ComVisible(true)]
	[Serializable]
	public class CaseInsensitiveComparer : IComparer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.CaseInsensitiveComparer" /> class using the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread.</summary>
		// Token: 0x060016A9 RID: 5801 RVA: 0x000584E0 File Offset: 0x000566E0
		public CaseInsensitiveComparer()
		{
			this.culture = CultureInfo.CurrentCulture;
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x000584F4 File Offset: 0x000566F4
		private CaseInsensitiveComparer(bool invariant)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.CaseInsensitiveComparer" /> class using the specified <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use for the new <see cref="T:System.Collections.CaseInsensitiveComparer" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		// Token: 0x060016AB RID: 5803 RVA: 0x000584FC File Offset: 0x000566FC
		public CaseInsensitiveComparer(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			if (culture.LCID != CultureInfo.InvariantCulture.LCID)
			{
				this.culture = culture;
			}
		}

		/// <summary>Gets an instance of <see cref="T:System.Collections.CaseInsensitiveComparer" /> that is associated with the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread and that is always available.</summary>
		/// <returns>An instance of <see cref="T:System.Collections.CaseInsensitiveComparer" /> that is associated with the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x00058554 File Offset: 0x00056754
		public static CaseInsensitiveComparer Default
		{
			get
			{
				return CaseInsensitiveComparer.defaultComparer;
			}
		}

		/// <summary>Gets an instance of <see cref="T:System.Collections.CaseInsensitiveComparer" /> that is associated with <see cref="P:System.Globalization.CultureInfo.InvariantCulture" /> and that is always available.</summary>
		/// <returns>An instance of <see cref="T:System.Collections.CaseInsensitiveComparer" /> that is associated with <see cref="P:System.Globalization.CultureInfo.InvariantCulture" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x0005855C File Offset: 0x0005675C
		public static CaseInsensitiveComparer DefaultInvariant
		{
			get
			{
				return CaseInsensitiveComparer.defaultInvariantComparer;
			}
		}

		/// <summary>Performs a case-insensitive comparison of two objects of the same type and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
		/// <returns>Value Condition Less than zero <paramref name="a" /> is less than <paramref name="b" />, with casing ignored. Zero <paramref name="a" /> equals <paramref name="b" />, with casing ignored. Greater than zero <paramref name="a" /> is greater than <paramref name="b" />, with casing ignored. </returns>
		/// <param name="a">The first object to compare. </param>
		/// <param name="b">The second object to compare. </param>
		/// <exception cref="T:System.ArgumentException">Neither <paramref name="a" /> nor <paramref name="b" /> implements the <see cref="T:System.IComparable" /> interface.-or- <paramref name="a" /> and <paramref name="b" /> are of different types. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060016AF RID: 5807 RVA: 0x00058564 File Offset: 0x00056764
		public int Compare(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text == null || text2 == null)
			{
				return Comparer.Default.Compare(a, b);
			}
			if (this.culture != null)
			{
				return this.culture.CompareInfo.Compare(text, text2, CompareOptions.IgnoreCase);
			}
			return CultureInfo.InvariantCulture.CompareInfo.Compare(text, text2, CompareOptions.IgnoreCase);
		}

		// Token: 0x0400086C RID: 2156
		private static CaseInsensitiveComparer defaultComparer = new CaseInsensitiveComparer();

		// Token: 0x0400086D RID: 2157
		private static CaseInsensitiveComparer defaultInvariantComparer = new CaseInsensitiveComparer(true);

		// Token: 0x0400086E RID: 2158
		private CultureInfo culture;
	}
}
