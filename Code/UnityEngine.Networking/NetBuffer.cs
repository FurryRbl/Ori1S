using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000035 RID: 53
	internal class NetBuffer
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00006B9C File Offset: 0x00004D9C
		public NetBuffer()
		{
			this.m_Buffer = new byte[64];
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006BB4 File Offset: 0x00004DB4
		public NetBuffer(byte[] buffer)
		{
			this.m_Buffer = buffer;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00006BC4 File Offset: 0x00004DC4
		public uint Position
		{
			get
			{
				return this.m_Pos;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006BCC File Offset: 0x00004DCC
		public byte ReadByte()
		{
			if ((ulong)this.m_Pos >= (ulong)((long)this.m_Buffer.Length))
			{
				throw new IndexOutOfRangeException("NetworkReader:ReadByte out of range:" + this.ToString());
			}
			return this.m_Buffer[(int)((UIntPtr)(this.m_Pos++))];
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006C20 File Offset: 0x00004E20
		public void ReadBytes(byte[] buffer, uint count)
		{
			if ((ulong)(this.m_Pos + count) > (ulong)((long)this.m_Buffer.Length))
			{
				throw new IndexOutOfRangeException(string.Concat(new object[]
				{
					"NetworkReader:ReadBytes out of range: (",
					count,
					") ",
					this.ToString()
				}));
			}
			ushort num = 0;
			while ((uint)num < count)
			{
				buffer[(int)num] = this.m_Buffer[(int)((UIntPtr)(this.m_Pos + (uint)num))];
				num += 1;
			}
			this.m_Pos += count;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006CAC File Offset: 0x00004EAC
		public void ReadChars(char[] buffer, uint count)
		{
			if ((ulong)(this.m_Pos + count) > (ulong)((long)this.m_Buffer.Length))
			{
				throw new IndexOutOfRangeException(string.Concat(new object[]
				{
					"NetworkReader:ReadChars out of range: (",
					count,
					") ",
					this.ToString()
				}));
			}
			ushort num = 0;
			while ((uint)num < count)
			{
				buffer[(int)num] = (char)this.m_Buffer[(int)((UIntPtr)(this.m_Pos + (uint)num))];
				num += 1;
			}
			this.m_Pos += count;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006D38 File Offset: 0x00004F38
		internal ArraySegment<byte> AsArraySegment()
		{
			return new ArraySegment<byte>(this.m_Buffer, 0, (int)this.m_Pos);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006D4C File Offset: 0x00004F4C
		public void WriteByte(byte value)
		{
			this.WriteCheckForSpace(1);
			this.m_Buffer[(int)((UIntPtr)this.m_Pos)] = value;
			this.m_Pos += 1U;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006D80 File Offset: 0x00004F80
		public void WriteByte2(byte value0, byte value1)
		{
			this.WriteCheckForSpace(2);
			this.m_Buffer[(int)((UIntPtr)this.m_Pos)] = value0;
			this.m_Buffer[(int)((UIntPtr)(this.m_Pos + 1U))] = value1;
			this.m_Pos += 2U;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006DB8 File Offset: 0x00004FB8
		public void WriteByte4(byte value0, byte value1, byte value2, byte value3)
		{
			this.WriteCheckForSpace(4);
			this.m_Buffer[(int)((UIntPtr)this.m_Pos)] = value0;
			this.m_Buffer[(int)((UIntPtr)(this.m_Pos + 1U))] = value1;
			this.m_Buffer[(int)((UIntPtr)(this.m_Pos + 2U))] = value2;
			this.m_Buffer[(int)((UIntPtr)(this.m_Pos + 3U))] = value3;
			this.m_Pos += 4U;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006E20 File Offset: 0x00005020
		public void WriteByte8(byte value0, byte value1, byte value2, byte value3, byte value4, byte value5, byte value6, byte value7)
		{
			this.WriteCheckForSpace(8);
			this.m_Buffer[(int)((UIntPtr)this.m_Pos)] = value0;
			this.m_Buffer[(int)((UIntPtr)(this.m_Pos + 1U))] = value1;
			this.m_Buffer[(int)((UIntPtr)(this.m_Pos + 2U))] = value2;
			this.m_Buffer[(int)((UIntPtr)(this.m_Pos + 3U))] = value3;
			this.m_Buffer[(int)((UIntPtr)(this.m_Pos + 4U))] = value4;
			this.m_Buffer[(int)((UIntPtr)(this.m_Pos + 5U))] = value5;
			this.m_Buffer[(int)((UIntPtr)(this.m_Pos + 6U))] = value6;
			this.m_Buffer[(int)((UIntPtr)(this.m_Pos + 7U))] = value7;
			this.m_Pos += 8U;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006ED0 File Offset: 0x000050D0
		public void WriteBytesAtOffset(byte[] buffer, ushort targetOffset, ushort count)
		{
			uint num = (uint)(count + targetOffset);
			this.WriteCheckForSpace((ushort)num);
			if (targetOffset == 0 && (int)count == buffer.Length)
			{
				buffer.CopyTo(this.m_Buffer, (long)((ulong)this.m_Pos));
			}
			else
			{
				for (int i = 0; i < (int)count; i++)
				{
					this.m_Buffer[(int)targetOffset + i] = buffer[i];
				}
			}
			if (num > this.m_Pos)
			{
				this.m_Pos = num;
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006F44 File Offset: 0x00005144
		public void WriteBytes(byte[] buffer, ushort count)
		{
			this.WriteCheckForSpace(count);
			if ((int)count == buffer.Length)
			{
				buffer.CopyTo(this.m_Buffer, (long)((ulong)this.m_Pos));
			}
			else
			{
				for (int i = 0; i < (int)count; i++)
				{
					this.m_Buffer[(int)(checked((IntPtr)(unchecked((ulong)this.m_Pos + (ulong)((long)i)))))] = buffer[i];
				}
			}
			this.m_Pos += (uint)count;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006FB0 File Offset: 0x000051B0
		private void WriteCheckForSpace(ushort count)
		{
			if ((ulong)(this.m_Pos + (uint)count) < (ulong)((long)this.m_Buffer.Length))
			{
				return;
			}
			int num = (int)((float)this.m_Buffer.Length * 1.5f);
			while ((ulong)(this.m_Pos + (uint)count) >= (ulong)((long)num))
			{
				num = (int)((float)num * 1.5f);
				if (num > 134217728)
				{
					Debug.LogWarning("NetworkBuffer size is " + num + " bytes!");
				}
			}
			byte[] array = new byte[num];
			this.m_Buffer.CopyTo(array, 0);
			this.m_Buffer = array;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007048 File Offset: 0x00005248
		public void FinishMessage()
		{
			ushort num = (ushort)(this.m_Pos - 4U);
			this.m_Buffer[0] = (byte)(num & 255);
			this.m_Buffer[1] = (byte)(num >> 8 & 255);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007084 File Offset: 0x00005284
		public void SeekZero()
		{
			this.m_Pos = 0U;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007090 File Offset: 0x00005290
		public void Replace(byte[] buffer)
		{
			this.m_Buffer = buffer;
			this.m_Pos = 0U;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000070A0 File Offset: 0x000052A0
		public override string ToString()
		{
			return string.Format("NetBuf sz:{0} pos:{1}", this.m_Buffer.Length, this.m_Pos);
		}

		// Token: 0x040000A7 RID: 167
		private const int k_InitialSize = 64;

		// Token: 0x040000A8 RID: 168
		private const float k_GrowthFactor = 1.5f;

		// Token: 0x040000A9 RID: 169
		private const int k_BufferSizeWarning = 134217728;

		// Token: 0x040000AA RID: 170
		private byte[] m_Buffer;

		// Token: 0x040000AB RID: 171
		private uint m_Pos;
	}
}
