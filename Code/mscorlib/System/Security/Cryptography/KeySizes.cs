using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Determines the set of valid key sizes for the symmetric cryptographic algorithms.</summary>
	// Token: 0x020005BC RID: 1468
	[ComVisible(true)]
	public sealed class KeySizes
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.KeySizes" /> class with the specified key values.</summary>
		/// <param name="minSize">The minimum valid key size. </param>
		/// <param name="maxSize">The maximum valid key size. </param>
		/// <param name="skipSize">The interval between valid key sizes. </param>
		// Token: 0x0600384A RID: 14410 RVA: 0x000B69DC File Offset: 0x000B4BDC
		public KeySizes(int minSize, int maxSize, int skipSize)
		{
			this._maxSize = maxSize;
			this._minSize = minSize;
			this._skipSize = skipSize;
		}

		/// <summary>Specifies the maximum key size in bits.</summary>
		/// <returns>The maximum key size in bits.</returns>
		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x0600384B RID: 14411 RVA: 0x000B69FC File Offset: 0x000B4BFC
		public int MaxSize
		{
			get
			{
				return this._maxSize;
			}
		}

		/// <summary>Specifies the minimum key size in bits.</summary>
		/// <returns>The minimum key size in bits.</returns>
		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x0600384C RID: 14412 RVA: 0x000B6A04 File Offset: 0x000B4C04
		public int MinSize
		{
			get
			{
				return this._minSize;
			}
		}

		/// <summary>Specifies the interval between valid key sizes in bits.</summary>
		/// <returns>The interval between valid key sizes in bits.</returns>
		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x0600384D RID: 14413 RVA: 0x000B6A0C File Offset: 0x000B4C0C
		public int SkipSize
		{
			get
			{
				return this._skipSize;
			}
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x000B6A14 File Offset: 0x000B4C14
		internal bool IsLegal(int keySize)
		{
			int num = keySize - this.MinSize;
			bool flag = num >= 0 && keySize <= this.MaxSize;
			return (this.SkipSize != 0) ? (flag && num % this.SkipSize == 0) : flag;
		}

		// Token: 0x0600384F RID: 14415 RVA: 0x000B6A68 File Offset: 0x000B4C68
		internal static bool IsLegalKeySize(KeySizes[] legalKeys, int size)
		{
			foreach (KeySizes keySizes in legalKeys)
			{
				if (keySizes.IsLegal(size))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001869 RID: 6249
		private int _maxSize;

		// Token: 0x0400186A RID: 6250
		private int _minSize;

		// Token: 0x0400186B RID: 6251
		private int _skipSize;
	}
}
