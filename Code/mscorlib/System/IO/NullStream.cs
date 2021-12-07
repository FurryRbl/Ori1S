using System;

namespace System.IO
{
	// Token: 0x02000254 RID: 596
	internal class NullStream : Stream
	{
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001EDA RID: 7898 RVA: 0x000724D4 File Offset: 0x000706D4
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x000724D8 File Offset: 0x000706D8
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x000724DC File Offset: 0x000706DC
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x000724E0 File Offset: 0x000706E0
		public override long Length
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001EDE RID: 7902 RVA: 0x000724E4 File Offset: 0x000706E4
		// (set) Token: 0x06001EDF RID: 7903 RVA: 0x000724E8 File Offset: 0x000706E8
		public override long Position
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x000724EC File Offset: 0x000706EC
		public override void Flush()
		{
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x000724F0 File Offset: 0x000706F0
		public override int Read(byte[] buffer, int offset, int count)
		{
			return 0;
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x000724F4 File Offset: 0x000706F4
		public override int ReadByte()
		{
			return -1;
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x000724F8 File Offset: 0x000706F8
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x000724FC File Offset: 0x000706FC
		public override void SetLength(long value)
		{
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x00072500 File Offset: 0x00070700
		public override void Write(byte[] buffer, int offset, int count)
		{
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x00072504 File Offset: 0x00070704
		public override void WriteByte(byte value)
		{
		}
	}
}
