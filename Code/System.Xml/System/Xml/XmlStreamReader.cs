using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Xml
{
	// Token: 0x02000130 RID: 304
	internal class XmlStreamReader : NonBlockingStreamReader
	{
		// Token: 0x06000E2A RID: 3626 RVA: 0x00046654 File Offset: 0x00044854
		private XmlStreamReader(XmlInputStream input) : base(input, (input.ActualEncoding == null) ? XmlInputStream.StrictUTF8 : input.ActualEncoding)
		{
			this.input = input;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0004668C File Offset: 0x0004488C
		public XmlStreamReader(Stream input) : this(new XmlInputStream(input))
		{
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x000466B0 File Offset: 0x000448B0
		public override void Close()
		{
			this.input.Close();
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x000466C0 File Offset: 0x000448C0
		public override int Read([In] [Out] char[] dest_buffer, int index, int count)
		{
			int result;
			try
			{
				result = base.Read(dest_buffer, index, count);
			}
			catch (ArgumentException)
			{
				throw XmlStreamReader.invalidDataException;
			}
			return result;
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0004670C File Offset: 0x0004490C
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x04000663 RID: 1635
		private XmlInputStream input;

		// Token: 0x04000664 RID: 1636
		private static XmlException invalidDataException = new XmlException("invalid data.");
	}
}
