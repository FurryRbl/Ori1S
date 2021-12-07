using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000061 RID: 97
	public sealed class SHA512Cng : SHA512
	{
		// Token: 0x06000550 RID: 1360 RVA: 0x000185B4 File Offset: 0x000167B4
		[SecurityCritical]
		public SHA512Cng()
		{
			this.hash = new SHA512Managed();
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x000185D8 File Offset: 0x000167D8
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000185E8 File Offset: 0x000167E8
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000185FC File Offset: 0x000167FC
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA512Cng.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00018634 File Offset: 0x00016834
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x04000160 RID: 352
		private static byte[] Empty = new byte[0];

		// Token: 0x04000161 RID: 353
		private SHA512 hash;
	}
}
