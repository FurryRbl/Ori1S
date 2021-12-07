using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	/// <summary>Represents a writer that can write a sequential series of characters. This class is abstract.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200025E RID: 606
	[ComVisible(true)]
	[Serializable]
	public abstract class TextWriter : MarshalByRefObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.TextWriter" /> class.</summary>
		// Token: 0x06001F5A RID: 8026 RVA: 0x00073F74 File Offset: 0x00072174
		protected TextWriter()
		{
			this.CoreNewLine = Environment.NewLine.ToCharArray();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.TextWriter" /> class with the specified format provider.</summary>
		/// <param name="formatProvider">An <see cref="T:System.IFormatProvider" /> object that controls formatting. </param>
		// Token: 0x06001F5B RID: 8027 RVA: 0x00073F8C File Offset: 0x0007218C
		protected TextWriter(IFormatProvider formatProvider)
		{
			this.CoreNewLine = Environment.NewLine.ToCharArray();
			this.internalFormatProvider = formatProvider;
		}

		/// <summary>When overridden in a derived class, returns the <see cref="T:System.Text.Encoding" /> in which the output is written.</summary>
		/// <returns>The Encoding in which the output is written.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001F5D RID: 8029
		public abstract Encoding Encoding { get; }

		/// <summary>Gets an object that controls formatting.</summary>
		/// <returns>An <see cref="T:System.IFormatProvider" /> object for a specific culture, or the formatting of the current culture if no other culture is specified.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001F5E RID: 8030 RVA: 0x00073FB8 File Offset: 0x000721B8
		public virtual IFormatProvider FormatProvider
		{
			get
			{
				return this.internalFormatProvider;
			}
		}

		/// <summary>Gets or sets the line terminator string used by the current TextWriter.</summary>
		/// <returns>The line terminator string for the current TextWriter.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x00073FC0 File Offset: 0x000721C0
		// (set) Token: 0x06001F60 RID: 8032 RVA: 0x00073FD0 File Offset: 0x000721D0
		public virtual string NewLine
		{
			get
			{
				return new string(this.CoreNewLine);
			}
			set
			{
				if (value == null)
				{
					value = Environment.NewLine;
				}
				this.CoreNewLine = value.ToCharArray();
			}
		}

		/// <summary>Closes the current writer and releases any system resources associated with the writer.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F61 RID: 8033 RVA: 0x00073FEC File Offset: 0x000721EC
		public virtual void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.TextWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001F62 RID: 8034 RVA: 0x00073FF8 File Offset: 0x000721F8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.IO.TextWriter" /> object.</summary>
		// Token: 0x06001F63 RID: 8035 RVA: 0x00074008 File Offset: 0x00072208
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Clears all buffers for the current writer and causes any buffered data to be written to the underlying device.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F64 RID: 8036 RVA: 0x00074018 File Offset: 0x00072218
		public virtual void Flush()
		{
		}

		/// <summary>Creates a thread-safe wrapper around the specified TextWriter.</summary>
		/// <returns>A thread-safe wrapper.</returns>
		/// <param name="writer">The TextWriter to synchronize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001F65 RID: 8037 RVA: 0x0007401C File Offset: 0x0007221C
		public static TextWriter Synchronized(TextWriter writer)
		{
			return TextWriter.Synchronized(writer, false);
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x00074028 File Offset: 0x00072228
		internal static TextWriter Synchronized(TextWriter writer, bool neverClose)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer is null");
			}
			if (writer is SynchronizedWriter)
			{
				return writer;
			}
			return new SynchronizedWriter(writer, neverClose);
		}

		/// <summary>Writes the text representation of a Boolean value to the text stream.</summary>
		/// <param name="value">The Boolean to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F67 RID: 8039 RVA: 0x00074050 File Offset: 0x00072250
		public virtual void Write(bool value)
		{
			this.Write(value.ToString());
		}

		/// <summary>Writes a character to the text stream.</summary>
		/// <param name="value">The character to write to the text stream. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F68 RID: 8040 RVA: 0x00074060 File Offset: 0x00072260
		public virtual void Write(char value)
		{
		}

		/// <summary>Writes a character array to the text stream.</summary>
		/// <param name="buffer">The character array to write to the text stream. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F69 RID: 8041 RVA: 0x00074064 File Offset: 0x00072264
		public virtual void Write(char[] buffer)
		{
			if (buffer == null)
			{
				return;
			}
			this.Write(buffer, 0, buffer.Length);
		}

		/// <summary>Writes the text representation of a decimal value to the text stream.</summary>
		/// <param name="value">The decimal value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F6A RID: 8042 RVA: 0x00074078 File Offset: 0x00072278
		public virtual void Write(decimal value)
		{
			this.Write(value.ToString(this.internalFormatProvider));
		}

		/// <summary>Writes the text representation of an 8-byte floating-point value to the text stream.</summary>
		/// <param name="value">The 8-byte floating-point value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F6B RID: 8043 RVA: 0x00074090 File Offset: 0x00072290
		public virtual void Write(double value)
		{
			this.Write(value.ToString(this.internalFormatProvider));
		}

		/// <summary>Writes the text representation of a 4-byte signed integer to the text stream.</summary>
		/// <param name="value">The 4-byte signed integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F6C RID: 8044 RVA: 0x000740A8 File Offset: 0x000722A8
		public virtual void Write(int value)
		{
			this.Write(value.ToString(this.internalFormatProvider));
		}

		/// <summary>Writes the text representation of an 8-byte signed integer to the text stream.</summary>
		/// <param name="value">The 8-byte signed integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F6D RID: 8045 RVA: 0x000740C0 File Offset: 0x000722C0
		public virtual void Write(long value)
		{
			this.Write(value.ToString(this.internalFormatProvider));
		}

		/// <summary>Writes the text representation of an object to the text stream by calling ToString on that object.</summary>
		/// <param name="value">The object to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F6E RID: 8046 RVA: 0x000740D8 File Offset: 0x000722D8
		public virtual void Write(object value)
		{
			if (value != null)
			{
				this.Write(value.ToString());
			}
		}

		/// <summary>Writes the text representation of a 4-byte floating-point value to the text stream.</summary>
		/// <param name="value">The 4-byte floating-point value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F6F RID: 8047 RVA: 0x000740EC File Offset: 0x000722EC
		public virtual void Write(float value)
		{
			this.Write(value.ToString(this.internalFormatProvider));
		}

		/// <summary>Writes a string to the text stream.</summary>
		/// <param name="value">The string to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F70 RID: 8048 RVA: 0x00074104 File Offset: 0x00072304
		public virtual void Write(string value)
		{
			if (value != null)
			{
				this.Write(value.ToCharArray());
			}
		}

		/// <summary>Writes the text representation of a 4-byte unsigned integer to the text stream.</summary>
		/// <param name="value">The 4-byte unsigned integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F71 RID: 8049 RVA: 0x00074118 File Offset: 0x00072318
		[CLSCompliant(false)]
		public virtual void Write(uint value)
		{
			this.Write(value.ToString(this.internalFormatProvider));
		}

		/// <summary>Writes the text representation of an 8-byte unsigned integer to the text stream.</summary>
		/// <param name="value">The 8-byte unsigned integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F72 RID: 8050 RVA: 0x00074130 File Offset: 0x00072330
		[CLSCompliant(false)]
		public virtual void Write(ulong value)
		{
			this.Write(value.ToString(this.internalFormatProvider));
		}

		/// <summary>Writes out a formatted string, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.</summary>
		/// <param name="format">The formatting string. </param>
		/// <param name="arg0">An object to write into the formatted string. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to the number of provided objects to be formatted. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F73 RID: 8051 RVA: 0x00074148 File Offset: 0x00072348
		public virtual void Write(string format, object arg0)
		{
			this.Write(string.Format(format, arg0));
		}

		/// <summary>Writes out a formatted string, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.</summary>
		/// <param name="format">The formatting string. </param>
		/// <param name="arg">The object array to write into the formatted string. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="arg" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to <paramref name="arg" />. Length. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F74 RID: 8052 RVA: 0x00074158 File Offset: 0x00072358
		public virtual void Write(string format, params object[] arg)
		{
			this.Write(string.Format(format, arg));
		}

		/// <summary>Writes a subarray of characters to the text stream.</summary>
		/// <param name="buffer">The character array to write data from. </param>
		/// <param name="index">Starting index in the buffer. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F75 RID: 8053 RVA: 0x00074168 File Offset: 0x00072368
		public virtual void Write(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0 || index > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0 || index > buffer.Length - count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			while (count > 0)
			{
				this.Write(buffer[index]);
				count--;
				index++;
			}
		}

		/// <summary>Writes out a formatted string, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.</summary>
		/// <param name="format">The formatting string. </param>
		/// <param name="arg0">An object to write into the formatted string. </param>
		/// <param name="arg1">An object to write into the formatted string. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to the number of provided objects to be formatted. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F76 RID: 8054 RVA: 0x000741E0 File Offset: 0x000723E0
		public virtual void Write(string format, object arg0, object arg1)
		{
			this.Write(string.Format(format, arg0, arg1));
		}

		/// <summary>Writes out a formatted string, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.</summary>
		/// <param name="format">The formatting string. </param>
		/// <param name="arg0">An object to write into the formatted string. </param>
		/// <param name="arg1">An object to write into the formatted string. </param>
		/// <param name="arg2">An object to write into the formatted string. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to the number of provided objects to be formatted. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F77 RID: 8055 RVA: 0x000741F0 File Offset: 0x000723F0
		public virtual void Write(string format, object arg0, object arg1, object arg2)
		{
			this.Write(string.Format(format, arg0, arg1, arg2));
		}

		/// <summary>Writes a line terminator to the text stream.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F78 RID: 8056 RVA: 0x00074204 File Offset: 0x00072404
		public virtual void WriteLine()
		{
			this.Write(this.CoreNewLine);
		}

		/// <summary>Writes the text representation of a Boolean followed by a line terminator to the text stream.</summary>
		/// <param name="value">The Boolean to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F79 RID: 8057 RVA: 0x00074214 File Offset: 0x00072414
		public virtual void WriteLine(bool value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes a character followed by a line terminator to the text stream.</summary>
		/// <param name="value">The character to write to the text stream. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F7A RID: 8058 RVA: 0x00074224 File Offset: 0x00072424
		public virtual void WriteLine(char value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes an array of characters followed by a line terminator to the text stream.</summary>
		/// <param name="buffer">The character array from which data is read. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F7B RID: 8059 RVA: 0x00074234 File Offset: 0x00072434
		public virtual void WriteLine(char[] buffer)
		{
			this.Write(buffer);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a decimal value followed by a line terminator to the text stream.</summary>
		/// <param name="value">The decimal value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F7C RID: 8060 RVA: 0x00074244 File Offset: 0x00072444
		public virtual void WriteLine(decimal value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a 8-byte floating-point value followed by a line terminator to the text stream.</summary>
		/// <param name="value">The 8-byte floating-point value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F7D RID: 8061 RVA: 0x00074254 File Offset: 0x00072454
		public virtual void WriteLine(double value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a 4-byte signed integer followed by a line terminator to the text stream.</summary>
		/// <param name="value">The 4-byte signed integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F7E RID: 8062 RVA: 0x00074264 File Offset: 0x00072464
		public virtual void WriteLine(int value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of an 8-byte signed integer followed by a line terminator to the text stream.</summary>
		/// <param name="value">The 8-byte signed integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F7F RID: 8063 RVA: 0x00074274 File Offset: 0x00072474
		public virtual void WriteLine(long value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of an object by calling ToString on this object, followed by a line terminator to the text stream.</summary>
		/// <param name="value">The object to write. If <paramref name="value" /> is null, only the line termination characters are written. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F80 RID: 8064 RVA: 0x00074284 File Offset: 0x00072484
		public virtual void WriteLine(object value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a 4-byte floating-point value followed by a line terminator to the text stream.</summary>
		/// <param name="value">The 4-byte floating-point value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F81 RID: 8065 RVA: 0x00074294 File Offset: 0x00072494
		public virtual void WriteLine(float value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes a string followed by a line terminator to the text stream.</summary>
		/// <param name="value">The string to write. If <paramref name="value" /> is null, only the line termination characters are written. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F82 RID: 8066 RVA: 0x000742A4 File Offset: 0x000724A4
		public virtual void WriteLine(string value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a 4-byte unsigned integer followed by a line terminator to the text stream.</summary>
		/// <param name="value">The 4-byte unsigned integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F83 RID: 8067 RVA: 0x000742B4 File Offset: 0x000724B4
		[CLSCompliant(false)]
		public virtual void WriteLine(uint value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of an 8-byte unsigned integer followed by a line terminator to the text stream.</summary>
		/// <param name="value">The 8-byte unsigned integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F84 RID: 8068 RVA: 0x000742C4 File Offset: 0x000724C4
		[CLSCompliant(false)]
		public virtual void WriteLine(ulong value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.</summary>
		/// <param name="format">The formatted string. </param>
		/// <param name="arg0">The object to write into the formatted string. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to the number of provided objects to be formatted. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F85 RID: 8069 RVA: 0x000742D4 File Offset: 0x000724D4
		public virtual void WriteLine(string format, object arg0)
		{
			this.Write(format, arg0);
			this.WriteLine();
		}

		/// <summary>Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.</summary>
		/// <param name="format">The formatting string. </param>
		/// <param name="arg">The object array to write into format string. </param>
		/// <exception cref="T:System.ArgumentNullException">A string or object is passed in as null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to arg.Length. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F86 RID: 8070 RVA: 0x000742E4 File Offset: 0x000724E4
		public virtual void WriteLine(string format, params object[] arg)
		{
			this.Write(format, arg);
			this.WriteLine();
		}

		/// <summary>Writes a subarray of characters followed by a line terminator to the text stream.</summary>
		/// <param name="buffer">The character array from which data is read. </param>
		/// <param name="index">The index into <paramref name="buffer" /> at which to begin reading. </param>
		/// <param name="count">The maximum number of characters to write. </param>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F87 RID: 8071 RVA: 0x000742F4 File Offset: 0x000724F4
		public virtual void WriteLine(char[] buffer, int index, int count)
		{
			this.Write(buffer, index, count);
			this.WriteLine();
		}

		/// <summary>Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.</summary>
		/// <param name="format">The formatting string. </param>
		/// <param name="arg0">The object to write into the format string. </param>
		/// <param name="arg1">The object to write into the format string. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to the number of provided objects to be formatted. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F88 RID: 8072 RVA: 0x00074308 File Offset: 0x00072508
		public virtual void WriteLine(string format, object arg0, object arg1)
		{
			this.Write(format, arg0, arg1);
			this.WriteLine();
		}

		/// <summary>Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.</summary>
		/// <param name="format">The formatting string. </param>
		/// <param name="arg0">The object to write into the format string. </param>
		/// <param name="arg1">The object to write into the format string. </param>
		/// <param name="arg2">The object to write into the format string. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to the number of provided objects to be formatted. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001F89 RID: 8073 RVA: 0x0007431C File Offset: 0x0007251C
		public virtual void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			this.Write(format, arg0, arg1, arg2);
			this.WriteLine();
		}

		/// <summary>Stores the new line characters used for this TextWriter.</summary>
		// Token: 0x04000BCC RID: 3020
		protected char[] CoreNewLine;

		// Token: 0x04000BCD RID: 3021
		internal IFormatProvider internalFormatProvider;

		/// <summary>Provides a TextWriter with no backing store that can be written to, but not read from.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000BCE RID: 3022
		public static readonly TextWriter Null = new TextWriter.NullTextWriter();

		// Token: 0x0200025F RID: 607
		private sealed class NullTextWriter : TextWriter
		{
			// Token: 0x17000544 RID: 1348
			// (get) Token: 0x06001F8B RID: 8075 RVA: 0x00074338 File Offset: 0x00072538
			public override Encoding Encoding
			{
				get
				{
					return Encoding.Default;
				}
			}

			// Token: 0x06001F8C RID: 8076 RVA: 0x00074340 File Offset: 0x00072540
			public override void Write(string s)
			{
			}

			// Token: 0x06001F8D RID: 8077 RVA: 0x00074344 File Offset: 0x00072544
			public override void Write(char value)
			{
			}

			// Token: 0x06001F8E RID: 8078 RVA: 0x00074348 File Offset: 0x00072548
			public override void Write(char[] value, int index, int count)
			{
			}
		}
	}
}
