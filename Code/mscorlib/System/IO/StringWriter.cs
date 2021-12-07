using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	/// <summary>Implements a <see cref="T:System.IO.TextWriter" /> for writing information to a string. The information is stored in an underlying <see cref="T:System.Text.StringBuilder" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200025A RID: 602
	[MonoTODO("Serialization format not compatible with .NET")]
	[ComVisible(true)]
	[Serializable]
	public class StringWriter : TextWriter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringWriter" /> class.</summary>
		// Token: 0x06001F38 RID: 7992 RVA: 0x00073AA8 File Offset: 0x00071CA8
		public StringWriter() : this(new StringBuilder())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringWriter" /> class with the specified format control.</summary>
		/// <param name="formatProvider">An <see cref="T:System.IFormatProvider" /> object that controls formatting. </param>
		// Token: 0x06001F39 RID: 7993 RVA: 0x00073AB8 File Offset: 0x00071CB8
		public StringWriter(IFormatProvider formatProvider) : this(new StringBuilder(), formatProvider)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringWriter" /> class that writes to the specified <see cref="T:System.Text.StringBuilder" />.</summary>
		/// <param name="sb">The StringBuilder to write to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sb" /> is null. </exception>
		// Token: 0x06001F3A RID: 7994 RVA: 0x00073AC8 File Offset: 0x00071CC8
		public StringWriter(StringBuilder sb) : this(sb, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringWriter" /> class that writes to the specified <see cref="T:System.Text.StringBuilder" /> and has the specified format provider.</summary>
		/// <param name="sb">The StringBuilder to write to. </param>
		/// <param name="formatProvider">An <see cref="T:System.IFormatProvider" /> object that controls formatting. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sb" /> is null. </exception>
		// Token: 0x06001F3B RID: 7995 RVA: 0x00073AD4 File Offset: 0x00071CD4
		public StringWriter(StringBuilder sb, IFormatProvider formatProvider)
		{
			if (sb == null)
			{
				throw new ArgumentNullException("sb");
			}
			this.internalString = sb;
			this.internalFormatProvider = formatProvider;
		}

		/// <summary>Gets the <see cref="T:System.Text.Encoding" /> in which the output is written.</summary>
		/// <returns>The Encoding in which the output is written.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001F3C RID: 7996 RVA: 0x00073AFC File Offset: 0x00071CFC
		public override Encoding Encoding
		{
			get
			{
				return Encoding.Unicode;
			}
		}

		/// <summary>Closes the current <see cref="T:System.IO.StringWriter" /> and the underlying stream.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F3D RID: 7997 RVA: 0x00073B04 File Offset: 0x00071D04
		public override void Close()
		{
			this.Dispose(true);
			this.disposed = true;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.StringWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001F3E RID: 7998 RVA: 0x00073B14 File Offset: 0x00071D14
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.disposed = true;
		}

		/// <summary>Returns the underlying <see cref="T:System.Text.StringBuilder" />.</summary>
		/// <returns>The underlying StringBuilder.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001F3F RID: 7999 RVA: 0x00073B24 File Offset: 0x00071D24
		public virtual StringBuilder GetStringBuilder()
		{
			return this.internalString;
		}

		/// <summary>Returns a string containing the characters written to the current StringWriter so far.</summary>
		/// <returns>The string containing the characters written to the current StringWriter.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001F40 RID: 8000 RVA: 0x00073B2C File Offset: 0x00071D2C
		public override string ToString()
		{
			return this.internalString.ToString();
		}

		/// <summary>Writes a character to this instance of the StringWriter.</summary>
		/// <param name="value">The character to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The writer is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001F41 RID: 8001 RVA: 0x00073B3C File Offset: 0x00071D3C
		public override void Write(char value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("StringReader", Locale.GetText("Cannot write to a closed StringWriter"));
			}
			this.internalString.Append(value);
		}

		/// <summary>Writes a string to this instance of the StringWriter.</summary>
		/// <param name="value">The string to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The writer is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001F42 RID: 8002 RVA: 0x00073B6C File Offset: 0x00071D6C
		public override void Write(string value)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("StringReader", Locale.GetText("Cannot write to a closed StringWriter"));
			}
			this.internalString.Append(value);
		}

		/// <summary>Writes the specified region of a character array to this instance of the StringWriter.</summary>
		/// <param name="buffer">The character array to read data from. </param>
		/// <param name="index">The index at which to begin reading from <paramref name="buffer" />. </param>
		/// <param name="count">The maximum number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ArgumentException">(<paramref name="index" /> + <paramref name="count" />)&gt; <paramref name="buffer" />. Length. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The writer is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001F43 RID: 8003 RVA: 0x00073B9C File Offset: 0x00071D9C
		public override void Write(char[] buffer, int index, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("StringReader", Locale.GetText("Cannot write to a closed StringWriter"));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (index > buffer.Length - count)
			{
				throw new ArgumentException("index + count > buffer.Length");
			}
			this.internalString.Append(buffer, index, count);
		}

		// Token: 0x04000BC8 RID: 3016
		private StringBuilder internalString;

		// Token: 0x04000BC9 RID: 3017
		private bool disposed;
	}
}
