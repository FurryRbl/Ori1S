using System;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	// Token: 0x020005EB RID: 1515
	internal class TripleDESTransform : SymmetricTransform
	{
		// Token: 0x060039CD RID: 14797 RVA: 0x000C6348 File Offset: 0x000C4548
		public TripleDESTransform(TripleDES algo, bool encryption, byte[] key, byte[] iv) : base(algo, encryption, iv)
		{
			if (key == null)
			{
				key = TripleDESTransform.GetStrongKey();
			}
			if (TripleDES.IsWeakKey(key))
			{
				string text = Locale.GetText("This is a known weak key.");
				throw new CryptographicException(text);
			}
			byte[] array = new byte[8];
			byte[] array2 = new byte[8];
			byte[] array3 = new byte[8];
			DES symmAlgo = DES.Create();
			Buffer.BlockCopy(key, 0, array, 0, 8);
			Buffer.BlockCopy(key, 8, array2, 0, 8);
			if (key.Length == 16)
			{
				Buffer.BlockCopy(key, 0, array3, 0, 8);
			}
			else
			{
				Buffer.BlockCopy(key, 16, array3, 0, 8);
			}
			if (encryption || algo.Mode == CipherMode.CFB)
			{
				this.E1 = new DESTransform(symmAlgo, true, array, iv);
				this.D2 = new DESTransform(symmAlgo, false, array2, iv);
				this.E3 = new DESTransform(symmAlgo, true, array3, iv);
			}
			else
			{
				this.D1 = new DESTransform(symmAlgo, false, array3, iv);
				this.E2 = new DESTransform(symmAlgo, true, array2, iv);
				this.D3 = new DESTransform(symmAlgo, false, array, iv);
			}
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x000C645C File Offset: 0x000C465C
		protected override void ECB(byte[] input, byte[] output)
		{
			DESTransform.Permutation(input, output, DESTransform.ipTab, false);
			if (this.encrypt)
			{
				this.E1.ProcessBlock(output, output);
				this.D2.ProcessBlock(output, output);
				this.E3.ProcessBlock(output, output);
			}
			else
			{
				this.D1.ProcessBlock(output, output);
				this.E2.ProcessBlock(output, output);
				this.D3.ProcessBlock(output, output);
			}
			DESTransform.Permutation(output, output, DESTransform.fpTab, true);
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x000C64E4 File Offset: 0x000C46E4
		internal static byte[] GetStrongKey()
		{
			int size = DESTransform.BLOCK_BYTE_SIZE * 3;
			byte[] array = KeyBuilder.Key(size);
			while (TripleDES.IsWeakKey(array))
			{
				array = KeyBuilder.Key(size);
			}
			return array;
		}

		// Token: 0x0400190D RID: 6413
		private DESTransform E1;

		// Token: 0x0400190E RID: 6414
		private DESTransform D2;

		// Token: 0x0400190F RID: 6415
		private DESTransform E3;

		// Token: 0x04001910 RID: 6416
		private DESTransform D1;

		// Token: 0x04001911 RID: 6417
		private DESTransform E2;

		// Token: 0x04001912 RID: 6418
		private DESTransform D3;
	}
}
