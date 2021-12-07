using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Represents the result of mapping a string to its sort key.</summary>
	// Token: 0x02000086 RID: 134
	[ComVisible(true)]
	[Serializable]
	public class SortKey
	{
		// Token: 0x060007F2 RID: 2034 RVA: 0x0001CA04 File Offset: 0x0001AC04
		internal SortKey(int lcid, string source, CompareOptions opt)
		{
			this.lcid = lcid;
			this.source = source;
			this.options = opt;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001CA24 File Offset: 0x0001AC24
		internal SortKey(int lcid, string source, byte[] buffer, CompareOptions opt, int lv1Length, int lv2Length, int lv3Length, int kanaSmallLength, int markTypeLength, int katakanaLength, int kanaWidthLength, int identLength)
		{
			this.lcid = lcid;
			this.source = source;
			this.key = buffer;
			this.options = opt;
		}

		/// <summary>Compares two sort keys.</summary>
		/// <returns>Value Condition Less than zero <paramref name="sortkey1" /> is less than <paramref name="sortkey2" />. Zero <paramref name="sortkey1" /> is equal to <paramref name="sortkey2" />. Greater than zero <paramref name="sortkey1" /> is greater than <paramref name="sortkey2" />. </returns>
		/// <param name="sortkey1">The first sort key to compare. </param>
		/// <param name="sortkey2">The second sort key to compare. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sortkey1" /> or <paramref name="sortkey2" /> is null.</exception>
		// Token: 0x060007F4 RID: 2036 RVA: 0x0001CA4C File Offset: 0x0001AC4C
		public static int Compare(SortKey sortkey1, SortKey sortkey2)
		{
			if (sortkey1 == null)
			{
				throw new ArgumentNullException("sortkey1");
			}
			if (sortkey2 == null)
			{
				throw new ArgumentNullException("sortkey2");
			}
			if (object.ReferenceEquals(sortkey1, sortkey2) || object.ReferenceEquals(sortkey1.OriginalString, sortkey2.OriginalString))
			{
				return 0;
			}
			byte[] keyData = sortkey1.KeyData;
			byte[] keyData2 = sortkey2.KeyData;
			int num = (keyData.Length <= keyData2.Length) ? keyData.Length : keyData2.Length;
			for (int i = 0; i < num; i++)
			{
				if (keyData[i] != keyData2[i])
				{
					return (keyData[i] >= keyData2[i]) ? 1 : -1;
				}
			}
			return (keyData.Length != keyData2.Length) ? ((keyData.Length >= keyData2.Length) ? 1 : -1) : 0;
		}

		/// <summary>Gets the original string used to create the current <see cref="T:System.Globalization.SortKey" /> object.</summary>
		/// <returns>The original string used to create the current <see cref="T:System.Globalization.SortKey" /> object.</returns>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0001CB18 File Offset: 0x0001AD18
		public virtual string OriginalString
		{
			get
			{
				return this.source;
			}
		}

		/// <summary>Gets the byte array representing the current <see cref="T:System.Globalization.SortKey" /> object.</summary>
		/// <returns>A byte array representing the current <see cref="T:System.Globalization.SortKey" /> object.</returns>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0001CB20 File Offset: 0x0001AD20
		public virtual byte[] KeyData
		{
			get
			{
				return this.key;
			}
		}

		/// <summary>Determines whether the specified object is equal to the current <see cref="T:System.Globalization.SortKey" /> object.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is equal to the current <see cref="T:System.Globalization.SortKey" /> object; otherwise, false. </returns>
		/// <param name="value">The object to compare with the current <see cref="T:System.Globalization.SortKey" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060007F7 RID: 2039 RVA: 0x0001CB28 File Offset: 0x0001AD28
		public override bool Equals(object value)
		{
			SortKey sortKey = value as SortKey;
			return sortKey != null && this.lcid == sortKey.lcid && this.options == sortKey.options && SortKey.Compare(this, sortKey) == 0;
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.SortKey" /> object that is suitable for hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.SortKey" /> object.</returns>
		// Token: 0x060007F8 RID: 2040 RVA: 0x0001CB74 File Offset: 0x0001AD74
		public override int GetHashCode()
		{
			if (this.key.Length == 0)
			{
				return 0;
			}
			int num = (int)this.key[0];
			for (int i = 1; i < this.key.Length; i++)
			{
				num ^= (int)this.key[i] << (i & 3);
			}
			return num;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Globalization.SortKey" /> object.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Globalization.SortKey" /> object.</returns>
		// Token: 0x060007F9 RID: 2041 RVA: 0x0001CBC8 File Offset: 0x0001ADC8
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"SortKey - ",
				this.lcid,
				", ",
				this.options,
				", ",
				this.source
			});
		}

		// Token: 0x0400016B RID: 363
		private readonly string source;

		// Token: 0x0400016C RID: 364
		private readonly CompareOptions options;

		// Token: 0x0400016D RID: 365
		private readonly byte[] key;

		// Token: 0x0400016E RID: 366
		private readonly int lcid;
	}
}
