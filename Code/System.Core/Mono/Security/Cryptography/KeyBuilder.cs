using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200000E RID: 14
	public sealed class KeyBuilder
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x000058B4 File Offset: 0x00003AB4
		private KeyBuilder()
		{
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000058BC File Offset: 0x00003ABC
		private static RandomNumberGenerator Rng
		{
			get
			{
				if (KeyBuilder.rng == null)
				{
					KeyBuilder.rng = RandomNumberGenerator.Create();
				}
				return KeyBuilder.rng;
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000058D8 File Offset: 0x00003AD8
		public static byte[] Key(int size)
		{
			byte[] array = new byte[size];
			KeyBuilder.Rng.GetBytes(array);
			return array;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000058F8 File Offset: 0x00003AF8
		public static byte[] IV(int size)
		{
			byte[] array = new byte[size];
			KeyBuilder.Rng.GetBytes(array);
			return array;
		}

		// Token: 0x04000032 RID: 50
		private static RandomNumberGenerator rng;
	}
}
