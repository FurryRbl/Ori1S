using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Mono.Security
{
	// Token: 0x0200000E RID: 14
	public class ASN1
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00004CA8 File Offset: 0x00002EA8
		public ASN1() : this(0, null)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004CB4 File Offset: 0x00002EB4
		public ASN1(byte tag) : this(tag, null)
		{
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004CC0 File Offset: 0x00002EC0
		public ASN1(byte tag, byte[] data)
		{
			this.m_nTag = tag;
			this.m_aValue = data;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004CD8 File Offset: 0x00002ED8
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

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00004D8C File Offset: 0x00002F8C
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

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004DA8 File Offset: 0x00002FA8
		public byte Tag
		{
			get
			{
				return this.m_nTag;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00004DB0 File Offset: 0x00002FB0
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00004DC8 File Offset: 0x00002FC8
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00004DF8 File Offset: 0x00002FF8
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

		// Token: 0x06000082 RID: 130 RVA: 0x00004E14 File Offset: 0x00003014
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

		// Token: 0x06000083 RID: 131 RVA: 0x00004E54 File Offset: 0x00003054
		public bool Equals(byte[] asn1)
		{
			return this.CompareArray(this.GetBytes(), asn1);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004E64 File Offset: 0x00003064
		public bool CompareValue(byte[] value)
		{
			return this.CompareArray(this.m_aValue, value);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004E74 File Offset: 0x00003074
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

		// Token: 0x06000086 RID: 134 RVA: 0x00004EAC File Offset: 0x000030AC
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

		// Token: 0x06000087 RID: 135 RVA: 0x0000511C File Offset: 0x0000331C
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

		// Token: 0x06000088 RID: 136 RVA: 0x00005184 File Offset: 0x00003384
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

		// Token: 0x17000009 RID: 9
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

		// Token: 0x0600008A RID: 138 RVA: 0x00005288 File Offset: 0x00003488
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

		// Token: 0x0600008B RID: 139 RVA: 0x00005314 File Offset: 0x00003514
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

		// Token: 0x0600008C RID: 140 RVA: 0x000053DC File Offset: 0x000035DC
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

		// Token: 0x04000031 RID: 49
		private byte m_nTag;

		// Token: 0x04000032 RID: 50
		private byte[] m_aValue;

		// Token: 0x04000033 RID: 51
		private ArrayList elist;
	}
}
