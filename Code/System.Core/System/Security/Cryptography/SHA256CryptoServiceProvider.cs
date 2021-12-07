using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200005E RID: 94
	public sealed class SHA256CryptoServiceProvider : SHA256
	{
		// Token: 0x0600053E RID: 1342 RVA: 0x000183F8 File Offset: 0x000165F8
		[SecurityCritical]
		public SHA256CryptoServiceProvider()
		{
			this.hash = new SHA256Managed();
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001841C File Offset: 0x0001661C
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001842C File Offset: 0x0001662C
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00018440 File Offset: 0x00016640
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA256CryptoServiceProvider.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00018478 File Offset: 0x00016678
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x0400015A RID: 346
		private static byte[] Empty = new byte[0];

		// Token: 0x0400015B RID: 347
		private SHA256 hash;
	}
}
