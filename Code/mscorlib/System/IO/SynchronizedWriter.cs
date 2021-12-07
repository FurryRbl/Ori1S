using System;
using System.Text;

namespace System.IO
{
	// Token: 0x02000260 RID: 608
	[Serializable]
	internal class SynchronizedWriter : TextWriter
	{
		// Token: 0x06001F8F RID: 8079 RVA: 0x0007434C File Offset: 0x0007254C
		public SynchronizedWriter(TextWriter writer) : this(writer, false)
		{
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x00074358 File Offset: 0x00072558
		public SynchronizedWriter(TextWriter writer, bool neverClose)
		{
			this.writer = writer;
			this.neverClose = neverClose;
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x00074370 File Offset: 0x00072570
		public override void Close()
		{
			if (this.neverClose)
			{
				return;
			}
			lock (this)
			{
				this.writer.Close();
			}
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x000743C4 File Offset: 0x000725C4
		public override void Flush()
		{
			lock (this)
			{
				this.writer.Flush();
			}
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x0007440C File Offset: 0x0007260C
		public override void Write(bool value)
		{
			lock (this)
			{
				this.writer.Write(value);
			}
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x00074458 File Offset: 0x00072658
		public override void Write(char value)
		{
			lock (this)
			{
				this.writer.Write(value);
			}
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x000744A4 File Offset: 0x000726A4
		public override void Write(char[] value)
		{
			lock (this)
			{
				this.writer.Write(value);
			}
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x000744F0 File Offset: 0x000726F0
		public override void Write(decimal value)
		{
			lock (this)
			{
				this.writer.Write(value);
			}
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x0007453C File Offset: 0x0007273C
		public override void Write(int value)
		{
			lock (this)
			{
				this.writer.Write(value);
			}
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x00074588 File Offset: 0x00072788
		public override void Write(long value)
		{
			lock (this)
			{
				this.writer.Write(value);
			}
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x000745D4 File Offset: 0x000727D4
		public override void Write(object value)
		{
			lock (this)
			{
				this.writer.Write(value);
			}
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x00074620 File Offset: 0x00072820
		public override void Write(float value)
		{
			lock (this)
			{
				this.writer.Write(value);
			}
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x0007466C File Offset: 0x0007286C
		public override void Write(string value)
		{
			lock (this)
			{
				this.writer.Write(value);
			}
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x000746B8 File Offset: 0x000728B8
		public override void Write(uint value)
		{
			lock (this)
			{
				this.writer.Write(value);
			}
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x00074704 File Offset: 0x00072904
		public override void Write(ulong value)
		{
			lock (this)
			{
				this.writer.Write(value);
			}
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x00074750 File Offset: 0x00072950
		public override void Write(string format, object value)
		{
			lock (this)
			{
				this.writer.Write(format, value);
			}
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x0007479C File Offset: 0x0007299C
		public override void Write(string format, object[] value)
		{
			lock (this)
			{
				this.writer.Write(format, value);
			}
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x000747E8 File Offset: 0x000729E8
		public override void Write(char[] buffer, int index, int count)
		{
			lock (this)
			{
				this.writer.Write(buffer, index, count);
			}
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x00074834 File Offset: 0x00072A34
		public override void Write(string format, object arg0, object arg1)
		{
			lock (this)
			{
				this.writer.Write(format, arg0, arg1);
			}
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x00074880 File Offset: 0x00072A80
		public override void Write(string format, object arg0, object arg1, object arg2)
		{
			lock (this)
			{
				this.writer.Write(format, arg0, arg1, arg2);
			}
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x000748D0 File Offset: 0x00072AD0
		public override void WriteLine()
		{
			lock (this)
			{
				this.writer.WriteLine();
			}
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x00074918 File Offset: 0x00072B18
		public override void WriteLine(bool value)
		{
			lock (this)
			{
				this.writer.WriteLine(value);
			}
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x00074964 File Offset: 0x00072B64
		public override void WriteLine(char value)
		{
			lock (this)
			{
				this.writer.WriteLine(value);
			}
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x000749B0 File Offset: 0x00072BB0
		public override void WriteLine(char[] value)
		{
			lock (this)
			{
				this.writer.WriteLine(value);
			}
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x000749FC File Offset: 0x00072BFC
		public override void WriteLine(decimal value)
		{
			lock (this)
			{
				this.writer.WriteLine(value);
			}
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x00074A48 File Offset: 0x00072C48
		public override void WriteLine(double value)
		{
			lock (this)
			{
				this.writer.WriteLine(value);
			}
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x00074A94 File Offset: 0x00072C94
		public override void WriteLine(int value)
		{
			lock (this)
			{
				this.writer.WriteLine(value);
			}
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x00074AE0 File Offset: 0x00072CE0
		public override void WriteLine(long value)
		{
			lock (this)
			{
				this.writer.WriteLine(value);
			}
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x00074B2C File Offset: 0x00072D2C
		public override void WriteLine(object value)
		{
			lock (this)
			{
				this.writer.WriteLine(value);
			}
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x00074B78 File Offset: 0x00072D78
		public override void WriteLine(float value)
		{
			lock (this)
			{
				this.writer.WriteLine(value);
			}
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x00074BC4 File Offset: 0x00072DC4
		public override void WriteLine(string value)
		{
			lock (this)
			{
				this.writer.WriteLine(value);
			}
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x00074C10 File Offset: 0x00072E10
		public override void WriteLine(uint value)
		{
			lock (this)
			{
				this.writer.WriteLine(value);
			}
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x00074C5C File Offset: 0x00072E5C
		public override void WriteLine(ulong value)
		{
			lock (this)
			{
				this.writer.WriteLine(value);
			}
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x00074CA8 File Offset: 0x00072EA8
		public override void WriteLine(string format, object value)
		{
			lock (this)
			{
				this.writer.WriteLine(format, value);
			}
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x00074CF4 File Offset: 0x00072EF4
		public override void WriteLine(string format, object[] value)
		{
			lock (this)
			{
				this.writer.WriteLine(format, value);
			}
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x00074D40 File Offset: 0x00072F40
		public override void WriteLine(char[] buffer, int index, int count)
		{
			lock (this)
			{
				this.writer.WriteLine(buffer, index, count);
			}
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x00074D8C File Offset: 0x00072F8C
		public override void WriteLine(string format, object arg0, object arg1)
		{
			lock (this)
			{
				this.writer.WriteLine(format, arg0, arg1);
			}
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x00074DD8 File Offset: 0x00072FD8
		public override void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			lock (this)
			{
				this.writer.WriteLine(format, arg0, arg1, arg2);
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x00074E28 File Offset: 0x00073028
		public override Encoding Encoding
		{
			get
			{
				Encoding encoding;
				lock (this)
				{
					encoding = this.writer.Encoding;
				}
				return encoding;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x00074E78 File Offset: 0x00073078
		public override IFormatProvider FormatProvider
		{
			get
			{
				IFormatProvider formatProvider;
				lock (this)
				{
					formatProvider = this.writer.FormatProvider;
				}
				return formatProvider;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001FB7 RID: 8119 RVA: 0x00074EC8 File Offset: 0x000730C8
		// (set) Token: 0x06001FB8 RID: 8120 RVA: 0x00074F18 File Offset: 0x00073118
		public override string NewLine
		{
			get
			{
				string newLine;
				lock (this)
				{
					newLine = this.writer.NewLine;
				}
				return newLine;
			}
			set
			{
				lock (this)
				{
					this.writer.NewLine = value;
				}
			}
		}

		// Token: 0x04000BCF RID: 3023
		private TextWriter writer;

		// Token: 0x04000BD0 RID: 3024
		private bool neverClose;
	}
}
