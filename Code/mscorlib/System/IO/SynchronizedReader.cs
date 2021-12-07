using System;

namespace System.IO
{
	// Token: 0x0200025D RID: 605
	[Serializable]
	internal class SynchronizedReader : TextReader
	{
		// Token: 0x06001F52 RID: 8018 RVA: 0x00073D34 File Offset: 0x00071F34
		public SynchronizedReader(TextReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x00073D44 File Offset: 0x00071F44
		public override void Close()
		{
			lock (this)
			{
				this.reader.Close();
			}
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x00073D8C File Offset: 0x00071F8C
		public override int Peek()
		{
			int result;
			lock (this)
			{
				result = this.reader.Peek();
			}
			return result;
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x00073DDC File Offset: 0x00071FDC
		public override int ReadBlock(char[] buffer, int index, int count)
		{
			int result;
			lock (this)
			{
				result = this.reader.ReadBlock(buffer, index, count);
			}
			return result;
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x00073E30 File Offset: 0x00072030
		public override string ReadLine()
		{
			string result;
			lock (this)
			{
				result = this.reader.ReadLine();
			}
			return result;
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x00073E80 File Offset: 0x00072080
		public override string ReadToEnd()
		{
			string result;
			lock (this)
			{
				result = this.reader.ReadToEnd();
			}
			return result;
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x00073ED0 File Offset: 0x000720D0
		public override int Read()
		{
			int result;
			lock (this)
			{
				result = this.reader.Read();
			}
			return result;
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x00073F20 File Offset: 0x00072120
		public override int Read(char[] buffer, int index, int count)
		{
			int result;
			lock (this)
			{
				result = this.reader.Read(buffer, index, count);
			}
			return result;
		}

		// Token: 0x04000BCB RID: 3019
		private TextReader reader;
	}
}
