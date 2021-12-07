using System;
using System.Collections;
using System.IO;
using System.Text;

namespace System.Resources
{
	// Token: 0x0200031A RID: 794
	internal class Win32ResFileReader
	{
		// Token: 0x06002883 RID: 10371 RVA: 0x00091874 File Offset: 0x0008FA74
		public Win32ResFileReader(Stream s)
		{
			this.res_file = s;
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x00091884 File Offset: 0x0008FA84
		private int read_int16()
		{
			int num = this.res_file.ReadByte();
			if (num == -1)
			{
				return -1;
			}
			int num2 = this.res_file.ReadByte();
			if (num2 == -1)
			{
				return -1;
			}
			return num | num2 << 8;
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x000918C0 File Offset: 0x0008FAC0
		private int read_int32()
		{
			int num = this.read_int16();
			if (num == -1)
			{
				return -1;
			}
			int num2 = this.read_int16();
			if (num2 == -1)
			{
				return -1;
			}
			return num | num2 << 16;
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x000918F4 File Offset: 0x0008FAF4
		private void read_padding()
		{
			while (this.res_file.Position % 4L != 0L)
			{
				this.read_int16();
			}
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x00091918 File Offset: 0x0008FB18
		private NameOrId read_ordinal()
		{
			int num = this.read_int16();
			if ((num & 65535) != 0)
			{
				int id = this.read_int16();
				return new NameOrId(id);
			}
			byte[] array = new byte[16];
			int num2 = 0;
			for (;;)
			{
				int num3 = this.read_int16();
				if (num3 == 0)
				{
					break;
				}
				if (num2 == array.Length)
				{
					byte[] array2 = new byte[array.Length * 2];
					Array.Copy(array, array2, array.Length);
					array = array2;
				}
				array[num2] = (byte)(num3 >> 8);
				array[num2 + 1] = (byte)(num3 & 255);
				num2 += 2;
			}
			return new NameOrId(new string(Encoding.Unicode.GetChars(array, 0, num2)));
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x000919C0 File Offset: 0x0008FBC0
		public ICollection ReadResources()
		{
			ArrayList arrayList = new ArrayList();
			for (;;)
			{
				this.read_padding();
				int num = this.read_int32();
				if (num == -1)
				{
					break;
				}
				this.read_int32();
				NameOrId type = this.read_ordinal();
				NameOrId name = this.read_ordinal();
				this.read_padding();
				this.read_int32();
				this.read_int16();
				int language = this.read_int16();
				this.read_int32();
				this.read_int32();
				if (num != 0)
				{
					byte[] array = new byte[num];
					this.res_file.Read(array, 0, num);
					arrayList.Add(new Win32EncodedResource(type, name, language, array));
				}
			}
			return arrayList;
		}

		// Token: 0x04001074 RID: 4212
		private Stream res_file;
	}
}
