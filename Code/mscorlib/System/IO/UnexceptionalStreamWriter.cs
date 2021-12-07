using System;
using System.Text;

namespace System.IO
{
	// Token: 0x02000262 RID: 610
	internal class UnexceptionalStreamWriter : StreamWriter
	{
		// Token: 0x06001FCA RID: 8138 RVA: 0x000752A8 File Offset: 0x000734A8
		public UnexceptionalStreamWriter(Stream stream) : base(stream)
		{
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x000752B4 File Offset: 0x000734B4
		public UnexceptionalStreamWriter(Stream stream, Encoding encoding) : base(stream, encoding)
		{
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x000752C0 File Offset: 0x000734C0
		public UnexceptionalStreamWriter(Stream stream, Encoding encoding, int bufferSize) : base(stream, encoding, bufferSize)
		{
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x000752CC File Offset: 0x000734CC
		public UnexceptionalStreamWriter(string path) : base(path)
		{
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x000752D8 File Offset: 0x000734D8
		public UnexceptionalStreamWriter(string path, bool append) : base(path, append)
		{
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x000752E4 File Offset: 0x000734E4
		public UnexceptionalStreamWriter(string path, bool append, Encoding encoding) : base(path, append, encoding)
		{
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x000752F0 File Offset: 0x000734F0
		public UnexceptionalStreamWriter(string path, bool append, Encoding encoding, int bufferSize) : base(path, append, encoding, bufferSize)
		{
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x00075300 File Offset: 0x00073500
		public override void Flush()
		{
			try
			{
				base.Flush();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x0007533C File Offset: 0x0007353C
		public override void Write(char[] buffer, int index, int count)
		{
			try
			{
				base.Write(buffer, index, count);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x0007537C File Offset: 0x0007357C
		public override void Write(char value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x000753B8 File Offset: 0x000735B8
		public override void Write(char[] value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x000753F4 File Offset: 0x000735F4
		public override void Write(string value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}
	}
}
