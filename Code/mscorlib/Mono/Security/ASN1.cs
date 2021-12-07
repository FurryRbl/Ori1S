using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Mono.Security
{
	// Token: 0x0200009D RID: 157
	internal class ASN1
	{
		// Token: 0x060008F7 RID: 2295 RVA: 0x000228AC File Offset: 0x00020AAC
		public ASN1() : this(0, null)
		{
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x000228B8 File Offset: 0x00020AB8
		public ASN1(byte tag) : this(tag, null)
		{
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x000228C4 File Offset: 0x00020AC4
		public ASN1(byte tag, byte[] data)
		{
			this.m_nTag = tag;
			this.m_aValue = data;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x000228DC File Offset: 0x00020ADC
		public ASN1(byte[] data)
		{
			this.m_nTag = data[0];
			int num = 0;
			int num2 = (int)data[1];
			if (num2 > 128)
			{
				num = num2 - 128;
				num2 = 0;
				for (int i = 0; i < num; i++)
				{
					num2 *= 256;
					num2 += (int)data[i + 2];
				}
			}
			else if (num2 == 128)
			{
				throw new NotSupportedException("Undefined length encoding.");
			}
			this.m_aValue = new byte[num2];
			Buffer.BlockCopy(data, 2 + num, this.m_aValue, 0, num2);
			if ((this.m_nTag & 32) == 32)
			{
				int num3 = 2 + num;
				this.Decode(data, ref num3, data.Length);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00022990 File Offset: 0x00020B90
		public int Count
		{
			get
			{
				if (this.elist == null)
				{
					return 0;
				}
				return this.elist.Count;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x000229AC File Offset: 0x00020BAC
		public byte Tag
		{
			get
			{
				return this.m_nTag;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x000229B4 File Offset: 0x00020BB4
		public int Length
		{
			get
			{
				if (this.m_aValue != null)
				{
					return this.m_aValue.Length;
				}
				return 0;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x000229CC File Offset: 0x00020BCC
		// (set) Token: 0x060008FF RID: 2303 RVA: 0x000229FC File Offset: 0x00020BFC
		public byte[] Value
		{
			get
			{
				if (this.m_aValue == null)
				{
					this.GetBytes();
				}
				return (byte[])this.m_aValue.Clone();
			}
			set
			{
				if (value != null)
				{
					this.m_aValue = (byte[])value.Clone();
				}
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00022A18 File Offset: 0x00020C18
		private bool CompareArray(byte[] array1, byte[] array2)
		{
			bool flag = array1.Length == array2.Length;
			if (flag)
			{
				for (int i = 0; i < array1.Length; i++)
				{
					if (array1[i] != array2[i])
					{
						return false;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00022A58 File Offset: 0x00020C58
		public bool Equals(byte[] asn1)
		{
			return this.CompareArray(this.GetBytes(), asn1);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00022A68 File Offset: 0x00020C68
		public bool CompareValue(byte[] value)
		{
			return this.CompareArray(this.m_aValue, value);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00022A78 File Offset: 0x00020C78
		public ASN1 Add(ASN1 asn1)
		{
			if (asn1 != null)
			{
				if (this.elist == null)
				{
					this.elist = new ArrayList();
				}
				this.elist.Add(asn1);
			}
			return asn1;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00022AB0 File Offset: 0x00020CB0
		public virtual byte[] GetBytes()
		{
			byte[] array = null;
			if (this.Count > 0)
			{
				int num = 0;
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this.elist)
				{
					ASN1 asn = (ASN1)obj;
					byte[] bytes = asn.GetBytes();
					arrayList.Add(bytes);
					num += bytes.Length;
				}
				array = new byte[num];
				int num2 = 0;
				for (int i = 0; i < this.elist.Count; i++)
				{
					byte[] array2 = (byte[])arrayList[i];
					Buffer.BlockCopy(array2, 0, array, num2, array2.Length);
					num2 += array2.Length;
				}
			}
			else if (this.m_aValue != null)
			{
				array = this.m_aValue;
			}
			int num3 = 0;
			byte[] array3;
			if (array != null)
			{
				int num4 = array.Length;
				if (num4 > 127)
				{
					if (num4 <= 255)
					{
						array3 = new byte[3 + num4];
						Buffer.BlockCopy(array, 0, array3, 3, num4);
						num3 = 129;
						array3[2] = (byte)num4;
					}
					else if (num4 <= 65535)
					{
						array3 = new byte[4 + num4];
						Buffer.BlockCopy(array, 0, array3, 4, num4);
						num3 = 130;
						array3[2] = (byte)(num4 >> 8);
						array3[3] = (byte)num4;
					}
					else if (num4 <= 16777215)
					{
						array3 = new byte[5 + num4];
						Buffer.BlockCopy(array, 0, array3, 5, num4);
						num3 = 131;
						array3[2] = (byte)(num4 >> 16);
						array3[3] = (byte)(num4 >> 8);
						array3[4] = (byte)num4;
					}
					else
					{
						array3 = new byte[6 + num4];
						Buffer.BlockCopy(array, 0, array3, 6, num4);
						num3 = 132;
						array3[2] = (byte)(num4 >> 24);
						array3[3] = (byte)(num4 >> 16);
						array3[4] = (byte)(num4 >> 8);
						array3[5] = (byte)num4;
					}
				}
				else
				{
					array3 = new byte[2 + num4];
					Buffer.BlockCopy(array, 0, array3, 2, num4);
					num3 = num4;
				}
				if (this.m_aValue == null)
				{
					this.m_aValue = array;
				}
			}
			else
			{
				array3 = new byte[2];
			}
			array3[0] = this.m_nTag;
			array3[1] = (byte)num3;
			return array3;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00022D20 File Offset: 0x00020F20
		protected void Decode(byte[] asn1, ref int anPos, int anLength)
		{
			while (anPos < anLength - 1)
			{
				byte b;
				int num;
				byte[] data;
				this.DecodeTLV(asn1, ref anPos, out b, out num, out data);
				if (b != 0)
				{
					ASN1 asn2 = this.Add(new ASN1(b, data));
					if ((b & 32) == 32)
					{
						int num2 = anPos;
						asn2.Decode(asn1, ref num2, num2 + num);
					}
					anPos += num;
				}
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00022D88 File Offset: 0x00020F88
		protected void DecodeTLV(byte[] asn1, ref int pos, out byte tag, out int length, out byte[] content)
		{
			tag = asn1[pos++];
			length = (int)asn1[pos++];
			if ((length & 128) == 128)
			{
				int num = length & 127;
				length = 0;
				for (int i = 0; i < num; i++)
				{
					length = length * 256 + (int)asn1[pos++];
				}
			}
			content = new byte[length];
			Buffer.BlockCopy(asn1, pos, content, 0, length);
		}

		// Token: 0x170000ED RID: 237
		public ASN1 this[int index]
		{
			get
			{
				ASN1 result;
				try
				{
					if (this.elist == null || index >= this.elist.Count)
					{
						result = null;
					}
					else
					{
						result = (ASN1)this.elist[index];
					}
				}
				catch (ArgumentOutOfRangeException)
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00022E8C File Offset: 0x0002108C
		public ASN1 Element(int index, byte anTag)
		{
			ASN1 result;
			try
			{
				if (this.elist == null || index >= this.elist.Count)
				{
					result = null;
				}
				else
				{
					ASN1 asn = (ASN1)this.elist[index];
					if (asn.Tag == anTag)
					{
						result = asn;
					}
					else
					{
						result = null;
					}
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00022F18 File Offset: 0x00021118
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Tag: {0} {1}", this.m_nTag.ToString("X2"), Environment.NewLine);
			stringBuilder.AppendFormat("Length: {0} {1}", this.Value.Length, Environment.NewLine);
			stringBuilder.Append("Value: ");
			stringBuilder.Append(Environment.NewLine);
			for (int i = 0; i < this.Value.Length; i++)
			{
				stringBuilder.AppendFormat("{0} ", this.Value[i].ToString("X2"));
				if ((i + 1) % 16 == 0)
				{
					stringBuilder.AppendFormat(Environment.NewLine, new object[0]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00022FE0 File Offset: 0x000211E0
		public void SaveToFile(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			using (FileStream fileStream = File.Create(filename))
			{
				byte[] bytes = this.GetBytes();
				fileStream.Write(bytes, 0, bytes.Length);
			}
		}

		// Token: 0x040001C7 RID: 455
		private byte m_nTag;

		// Token: 0x040001C8 RID: 456
		private byte[] m_aValue;

		// Token: 0x040001C9 RID: 457
		private ArrayList elist;
	}
}
