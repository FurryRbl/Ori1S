using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	// Token: 0x0200010D RID: 269
	internal class CStreamReader : StreamReader
	{
		// Token: 0x06000DCA RID: 3530 RVA: 0x0003BA98 File Offset: 0x00039C98
		public CStreamReader(Stream stream, Encoding encoding) : base(stream, encoding)
		{
			this.driver = (TermInfoDriver)ConsoleDriver.driver;
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0003BAB4 File Offset: 0x00039CB4
		public override int Peek()
		{
			try
			{
				return base.Peek();
			}
			catch (IOException)
			{
			}
			return -1;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0003BAF8 File Offset: 0x00039CF8
		public override int Read()
		{
			try
			{
				return (int)Console.ReadKey().KeyChar;
			}
			catch (IOException)
			{
			}
			return -1;
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0003BB44 File Offset: 0x00039D44
		public override int Read([In] [Out] char[] dest, int index, int count)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (index > dest.Length - count)
			{
				throw new ArgumentException("index + count > dest.Length");
			}
			try
			{
				return this.driver.Read(dest, index, count);
			}
			catch (IOException)
			{
			}
			return 0;
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0003BBE4 File Offset: 0x00039DE4
		public override string ReadLine()
		{
			try
			{
				return this.driver.ReadLine();
			}
			catch (IOException)
			{
			}
			return null;
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0003BC2C File Offset: 0x00039E2C
		public override string ReadToEnd()
		{
			try
			{
				return base.ReadToEnd();
			}
			catch (IOException)
			{
			}
			return null;
		}

		// Token: 0x040003C3 RID: 963
		private TermInfoDriver driver;
	}
}
