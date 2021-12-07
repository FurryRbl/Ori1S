using System;
using System.Text;

namespace System.IO
{
	// Token: 0x0200010E RID: 270
	internal class CStreamWriter : StreamWriter
	{
		// Token: 0x06000DD0 RID: 3536 RVA: 0x0003BC70 File Offset: 0x00039E70
		public CStreamWriter(Stream stream, Encoding encoding) : base(stream, encoding)
		{
			this.driver = (TermInfoDriver)ConsoleDriver.driver;
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0003BC8C File Offset: 0x00039E8C
		public override void Write(char[] buffer, int index, int count)
		{
			if (count <= 0)
			{
				return;
			}
			if (!this.driver.Initialized)
			{
				try
				{
					base.Write(buffer, index, count);
				}
				catch (IOException)
				{
				}
				return;
			}
			lock (this)
			{
				int num = index + count;
				int num2 = index;
				int num3 = 0;
				do
				{
					char c = buffer[num2++];
					if (this.driver.IsSpecialKey(c))
					{
						if (num3 > 0)
						{
							try
							{
								base.Write(buffer, index, num3);
							}
							catch (IOException)
							{
							}
							num3 = 0;
						}
						this.driver.WriteSpecialKey(c);
						index = num2;
					}
					else
					{
						num3++;
					}
				}
				while (num2 < num);
				if (num3 > 0)
				{
					try
					{
						base.Write(buffer, index, num3);
					}
					catch (IOException)
					{
					}
				}
			}
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0003BDBC File Offset: 0x00039FBC
		public override void Write(char val)
		{
			lock (this)
			{
				try
				{
					if (this.driver.IsSpecialKey(val))
					{
						this.driver.WriteSpecialKey(val);
					}
					else
					{
						this.InternalWriteChar(val);
					}
				}
				catch (IOException)
				{
				}
			}
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0003BE48 File Offset: 0x0003A048
		public void WriteKey(ConsoleKeyInfo key)
		{
			lock (this)
			{
				ConsoleKeyInfo key2 = new ConsoleKeyInfo(key);
				if (this.driver.IsSpecialKey(key2))
				{
					this.driver.WriteSpecialKey(key2);
				}
				else
				{
					this.InternalWriteChar(key2.KeyChar);
				}
			}
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0003BEBC File Offset: 0x0003A0BC
		public void InternalWriteString(string val)
		{
			try
			{
				base.Write(val);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0003BEF8 File Offset: 0x0003A0F8
		public void InternalWriteChar(char val)
		{
			try
			{
				base.Write(val);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0003BF34 File Offset: 0x0003A134
		public void InternalWriteChars(char[] buffer, int n)
		{
			try
			{
				base.Write(buffer, 0, n);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0003BF74 File Offset: 0x0003A174
		public override void Write(char[] val)
		{
			this.Write(val, 0, val.Length);
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0003BF84 File Offset: 0x0003A184
		public override void Write(string val)
		{
			if (val == null)
			{
				return;
			}
			if (this.driver.Initialized)
			{
				this.Write(val.ToCharArray());
			}
			else
			{
				try
				{
					base.Write(val);
				}
				catch (IOException)
				{
				}
			}
		}

		// Token: 0x040003C4 RID: 964
		private TermInfoDriver driver;
	}
}
