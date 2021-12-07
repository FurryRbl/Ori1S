using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200005D RID: 93
	public sealed class SHA256Cng : SHA256
	{
		// Token: 0x06000538 RID: 1336 RVA: 0x00018364 File Offset: 0x00016564
		[SecurityCritical]
		public SHA256Cng()
		{
			this.hash = new SHA256Managed();
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00018388 File Offset: 0x00016588
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00018398 File Offset: 0x00016598
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000183AC File Offset: 0x000165AC
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA256Cng.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000183E4 File Offset: 0x000165E4
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x04000158 RID: 344
		private static byte[] Empty = new byte[0];

		// Token: 0x04000159 RID: 345
		private SHA256 hash;
	}
}
