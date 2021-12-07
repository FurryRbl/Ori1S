using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200005B RID: 91
	public sealed class MD5Cng : MD5
	{
		// Token: 0x0600052C RID: 1324 RVA: 0x0001823C File Offset: 0x0001643C
		[SecurityCritical]
		public MD5Cng()
		{
			this.hash = new MD5CryptoServiceProvider();
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00018260 File Offset: 0x00016460
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00018270 File Offset: 0x00016470
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00018284 File Offset: 0x00016484
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(MD5Cng.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000182BC File Offset: 0x000164BC
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x04000154 RID: 340
		private static byte[] Empty = new byte[0];

		// Token: 0x04000155 RID: 341
		private MD5 hash;
	}
}
