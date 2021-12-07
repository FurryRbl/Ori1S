using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200005C RID: 92
	public sealed class SHA1Cng : SHA1
	{
		// Token: 0x06000532 RID: 1330 RVA: 0x000182D0 File Offset: 0x000164D0
		[SecurityCritical]
		public SHA1Cng()
		{
			this.hash = new SHA1Managed();
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000182F4 File Offset: 0x000164F4
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00018304 File Offset: 0x00016504
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00018318 File Offset: 0x00016518
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA1Cng.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00018350 File Offset: 0x00016550
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x04000156 RID: 342
		private static byte[] Empty = new byte[0];

		// Token: 0x04000157 RID: 343
		private SHA1 hash;
	}
}
