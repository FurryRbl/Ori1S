using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x0200031C RID: 796
	internal class Win32IconFileReader
	{
		// Token: 0x0600288B RID: 10379 RVA: 0x00091AD4 File Offset: 0x0008FCD4
		public Win32IconFileReader(Stream s)
		{
			this.iconFile = s;
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x00091AE4 File Offset: 0x0008FCE4
		public ICONDIRENTRY[] ReadIcons()
		{
			ICONDIRENTRY[] result;
			using (BinaryReader binaryReader = new BinaryReader(this.iconFile))
			{
				int num = (int)binaryReader.ReadInt16();
				int num2 = (int)binaryReader.ReadInt16();
				if (num != 0 || num2 != 1)
				{
					throw new Exception("Invalid .ico file format");
				}
				long num3 = (long)binaryReader.ReadInt16();
				ICONDIRENTRY[] array = new ICONDIRENTRY[num3];
				int num4 = 0;
				while ((long)num4 < num3)
				{
					ICONDIRENTRY icondirentry = new ICONDIRENTRY();
					icondirentry.bWidth = binaryReader.ReadByte();
					icondirentry.bHeight = binaryReader.ReadByte();
					icondirentry.bColorCount = binaryReader.ReadByte();
					icondirentry.bReserved = binaryReader.ReadByte();
					icondirentry.wPlanes = binaryReader.ReadInt16();
					icondirentry.wBitCount = binaryReader.ReadInt16();
					int num5 = binaryReader.ReadInt32();
					int num6 = binaryReader.ReadInt32();
					icondirentry.image = new byte[num5];
					long position = this.iconFile.Position;
					this.iconFile.Position = (long)num6;
					this.iconFile.Read(icondirentry.image, 0, num5);
					this.iconFile.Position = position;
					if (icondirentry.wPlanes == 0)
					{
						icondirentry.wPlanes = (short)((int)icondirentry.image[12] | (int)icondirentry.image[13] << 8);
					}
					if (icondirentry.wBitCount == 0)
					{
						icondirentry.wBitCount = (short)((int)icondirentry.image[14] | (int)icondirentry.image[15] << 8);
					}
					array[num4] = icondirentry;
					num4++;
				}
				result = array;
			}
			return result;
		}

		// Token: 0x0400107E RID: 4222
		private Stream iconFile;
	}
}
