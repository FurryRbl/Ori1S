using System;
using System.IO;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x020000A3 RID: 163
	internal class TlsStream : Stream
	{
		// Token: 0x06000636 RID: 1590 RVA: 0x0002306C File Offset: 0x0002126C
		public TlsStream()
		{
			this.buffer = new MemoryStream(0);
			this.canRead = false;
			this.canWrite = true;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0002309C File Offset: 0x0002129C
		public TlsStream(byte[] data)
		{
			if (data != null)
			{
				this.buffer = new MemoryStream(data);
			}
			else
			{
				this.buffer = new MemoryStream();
			}
			this.canRead = true;
			this.canWrite = false;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x000230E0 File Offset: 0x000212E0
		public bool EOF
		{
			get
			{
				return this.Position >= this.Length;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x000230F8 File Offset: 0x000212F8
		public override bool CanWrite
		{
			get
			{
				return this.canWrite;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00023100 File Offset: 0x00021300
		public override bool CanRead
		{
			get
			{
				return this.canRead;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00023108 File Offset: 0x00021308
		public override bool CanSeek
		{
			get
			{
				return this.buffer.CanSeek;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x00023118 File Offset: 0x00021318
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x00023128 File Offset: 0x00021328
		public override long Position
		{
			get
			{
				return this.buffer.Position;
			}
			set
			{
				this.buffer.Position = value;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00023138 File Offset: 0x00021338
		public override long Length
		{
			get
			{
				return this.buffer.Length;
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00023148 File Offset: 0x00021348
		private byte[] ReadSmallValue(int length)
		{
			if (length > 4)
			{
				throw new ArgumentException("8 bytes maximum");
			}
			if (this.temp == null)
			{
				this.temp = new byte[4];
			}
			if (this.Read(this.temp, 0, length) != length)
			{
				throw new TlsException(string.Format("buffer underrun", new object[0]));
			}
			return this.temp;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x000231B0 File Offset: 0x000213B0
		public new byte ReadByte()
		{
			byte[] array = this.ReadSmallValue(1);
			return array[0];
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x000231C8 File Offset: 0x000213C8
		public short ReadInt16()
		{
			byte[] array = this.ReadSmallValue(2);
			return (short)((int)array[0] << 8 | (int)array[1]);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x000231E8 File Offset: 0x000213E8
		public int ReadInt24()
		{
			byte[] array = this.ReadSmallValue(3);
			return (int)array[0] << 16 | (int)array[1] << 8 | (int)array[2];
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00023210 File Offset: 0x00021410
		public int ReadInt32()
		{
			byte[] array = this.ReadSmallValue(4);
			return (int)array[0] << 24 | (int)array[1] << 16 | (int)array[2] << 8 | (int)array[3];
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0002323C File Offset: 0x0002143C
		public byte[] ReadBytes(int count)
		{
			byte[] result = new byte[count];
			if (this.Read(result, 0, count) != count)
			{
				throw new TlsException("buffer underrun");
			}
			return result;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0002326C File Offset: 0x0002146C
		public void Write(byte value)
		{
			if (this.temp == null)
			{
				this.temp = new byte[4];
			}
			this.temp[0] = value;
			this.Write(this.temp, 0, 1);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000232A8 File Offset: 0x000214A8
		public void Write(short value)
		{
			if (this.temp == null)
			{
				this.temp = new byte[4];
			}
			this.temp[0] = (byte)(value >> 8);
			this.temp[1] = (byte)value;
			this.Write(this.temp, 0, 2);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x000232E8 File Offset: 0x000214E8
		public void WriteInt24(int value)
		{
			if (this.temp == null)
			{
				this.temp = new byte[4];
			}
			this.temp[0] = (byte)(value >> 16);
			this.temp[1] = (byte)(value >> 8);
			this.temp[2] = (byte)value;
			this.Write(this.temp, 0, 3);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00023340 File Offset: 0x00021540
		public void Write(int value)
		{
			if (this.temp == null)
			{
				this.temp = new byte[4];
			}
			this.temp[0] = (byte)(value >> 24);
			this.temp[1] = (byte)(value >> 16);
			this.temp[2] = (byte)(value >> 8);
			this.temp[3] = (byte)value;
			this.Write(this.temp, 0, 4);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000233A4 File Offset: 0x000215A4
		public void Write(ulong value)
		{
			this.Write((int)(value >> 32));
			this.Write((int)value);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x000233BC File Offset: 0x000215BC
		public void Write(byte[] buffer)
		{
			this.Write(buffer, 0, buffer.Length);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x000233CC File Offset: 0x000215CC
		public void Reset()
		{
			this.buffer.SetLength(0L);
			this.buffer.Position = 0L;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x000233E8 File Offset: 0x000215E8
		public byte[] ToArray()
		{
			return this.buffer.ToArray();
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000233F8 File Offset: 0x000215F8
		public override void Flush()
		{
			this.buffer.Flush();
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00023408 File Offset: 0x00021608
		public override void SetLength(long length)
		{
			this.buffer.SetLength(length);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00023418 File Offset: 0x00021618
		public override long Seek(long offset, SeekOrigin loc)
		{
			return this.buffer.Seek(offset, loc);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00023428 File Offset: 0x00021628
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.canRead)
			{
				return this.buffer.Read(buffer, offset, count);
			}
			throw new InvalidOperationException("Read operations are not allowed by this stream");
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0002345C File Offset: 0x0002165C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.canWrite)
			{
				this.buffer.Write(buffer, offset, count);
				return;
			}
			throw new InvalidOperationException("Write operations are not allowed by this stream");
		}

		// Token: 0x04000300 RID: 768
		private const int temp_size = 4;

		// Token: 0x04000301 RID: 769
		private bool canRead;

		// Token: 0x04000302 RID: 770
		private bool canWrite;

		// Token: 0x04000303 RID: 771
		private MemoryStream buffer;

		// Token: 0x04000304 RID: 772
		private byte[] temp;
	}
}
