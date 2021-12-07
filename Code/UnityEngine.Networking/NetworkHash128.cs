using System;

namespace UnityEngine.Networking
{
	// Token: 0x0200003F RID: 63
	[Serializable]
	public struct NetworkHash128
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x00009FFC File Offset: 0x000081FC
		public void Reset()
		{
			this.i0 = 0;
			this.i1 = 0;
			this.i2 = 0;
			this.i3 = 0;
			this.i4 = 0;
			this.i5 = 0;
			this.i6 = 0;
			this.i7 = 0;
			this.i8 = 0;
			this.i9 = 0;
			this.i10 = 0;
			this.i11 = 0;
			this.i12 = 0;
			this.i13 = 0;
			this.i14 = 0;
			this.i15 = 0;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000A07C File Offset: 0x0000827C
		public bool IsValid()
		{
			return (this.i0 | this.i1 | this.i2 | this.i3 | this.i4 | this.i5 | this.i6 | this.i7 | this.i8 | this.i9 | this.i10 | this.i11 | this.i12 | this.i13 | this.i14 | this.i15) != 0;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000A100 File Offset: 0x00008300
		private static int HexToNumber(char c)
		{
			if (c >= '0' && c <= '9')
			{
				return (int)(c - '0');
			}
			if (c >= 'a' && c <= 'f')
			{
				return (int)(c - 'a' + '\n');
			}
			if (c >= 'A' && c <= 'F')
			{
				return (int)(c - 'A' + '\n');
			}
			return 0;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000A154 File Offset: 0x00008354
		public static NetworkHash128 Parse(string text)
		{
			int length = text.Length;
			if (length < 32)
			{
				string str = string.Empty;
				for (int i = 0; i < 32 - length; i++)
				{
					str += "0";
				}
				text = str + text;
			}
			NetworkHash128 result;
			result.i0 = (byte)(NetworkHash128.HexToNumber(text[0]) * 16 + NetworkHash128.HexToNumber(text[1]));
			result.i1 = (byte)(NetworkHash128.HexToNumber(text[2]) * 16 + NetworkHash128.HexToNumber(text[3]));
			result.i2 = (byte)(NetworkHash128.HexToNumber(text[4]) * 16 + NetworkHash128.HexToNumber(text[5]));
			result.i3 = (byte)(NetworkHash128.HexToNumber(text[6]) * 16 + NetworkHash128.HexToNumber(text[7]));
			result.i4 = (byte)(NetworkHash128.HexToNumber(text[8]) * 16 + NetworkHash128.HexToNumber(text[9]));
			result.i5 = (byte)(NetworkHash128.HexToNumber(text[10]) * 16 + NetworkHash128.HexToNumber(text[11]));
			result.i6 = (byte)(NetworkHash128.HexToNumber(text[12]) * 16 + NetworkHash128.HexToNumber(text[13]));
			result.i7 = (byte)(NetworkHash128.HexToNumber(text[14]) * 16 + NetworkHash128.HexToNumber(text[15]));
			result.i8 = (byte)(NetworkHash128.HexToNumber(text[16]) * 16 + NetworkHash128.HexToNumber(text[17]));
			result.i9 = (byte)(NetworkHash128.HexToNumber(text[18]) * 16 + NetworkHash128.HexToNumber(text[19]));
			result.i10 = (byte)(NetworkHash128.HexToNumber(text[20]) * 16 + NetworkHash128.HexToNumber(text[21]));
			result.i11 = (byte)(NetworkHash128.HexToNumber(text[22]) * 16 + NetworkHash128.HexToNumber(text[23]));
			result.i12 = (byte)(NetworkHash128.HexToNumber(text[24]) * 16 + NetworkHash128.HexToNumber(text[25]));
			result.i13 = (byte)(NetworkHash128.HexToNumber(text[26]) * 16 + NetworkHash128.HexToNumber(text[27]));
			result.i14 = (byte)(NetworkHash128.HexToNumber(text[28]) * 16 + NetworkHash128.HexToNumber(text[29]));
			result.i15 = (byte)(NetworkHash128.HexToNumber(text[30]) * 16 + NetworkHash128.HexToNumber(text[31]));
			return result;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000A3F8 File Offset: 0x000085F8
		public override string ToString()
		{
			return string.Format("{0:x2}{1:x2}{2:x2}{3:x2}{4:x2}{5:x2}{6:x2}{7:x2}{8:x2}{9:x2}{10:x2}{11:x2}{12:x2}{13:x2}{14:x2}{15:x2}", new object[]
			{
				this.i0,
				this.i1,
				this.i2,
				this.i3,
				this.i4,
				this.i5,
				this.i6,
				this.i7,
				this.i8,
				this.i9,
				this.i10,
				this.i11,
				this.i12,
				this.i13,
				this.i14,
				this.i15
			});
		}

		// Token: 0x040000FA RID: 250
		public byte i0;

		// Token: 0x040000FB RID: 251
		public byte i1;

		// Token: 0x040000FC RID: 252
		public byte i2;

		// Token: 0x040000FD RID: 253
		public byte i3;

		// Token: 0x040000FE RID: 254
		public byte i4;

		// Token: 0x040000FF RID: 255
		public byte i5;

		// Token: 0x04000100 RID: 256
		public byte i6;

		// Token: 0x04000101 RID: 257
		public byte i7;

		// Token: 0x04000102 RID: 258
		public byte i8;

		// Token: 0x04000103 RID: 259
		public byte i9;

		// Token: 0x04000104 RID: 260
		public byte i10;

		// Token: 0x04000105 RID: 261
		public byte i11;

		// Token: 0x04000106 RID: 262
		public byte i12;

		// Token: 0x04000107 RID: 263
		public byte i13;

		// Token: 0x04000108 RID: 264
		public byte i14;

		// Token: 0x04000109 RID: 265
		public byte i15;
	}
}
