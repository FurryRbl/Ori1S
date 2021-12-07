using System;
using System.IO;

namespace System.Diagnostics
{
	/// <summary>Directs tracing or debugging output to a <see cref="T:System.IO.TextWriter" /> or to a <see cref="T:System.IO.Stream" />, such as <see cref="T:System.IO.FileStream" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000254 RID: 596
	public class TextWriterTraceListener : TraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class with <see cref="T:System.IO.TextWriter" /> as the output recipient.</summary>
		// Token: 0x060014F0 RID: 5360 RVA: 0x00037640 File Offset: 0x00035840
		public TextWriterTraceListener() : base("TextWriter")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class, using the stream as the recipient of the debugging and tracing output.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that represents the stream the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> writes to. </param>
		/// <exception cref="T:System.ArgumentNullException">The stream is null. </exception>
		// Token: 0x060014F1 RID: 5361 RVA: 0x00037650 File Offset: 0x00035850
		public TextWriterTraceListener(Stream stream) : this(stream, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class, using the file as the recipient of the debugging and tracing output.</summary>
		/// <param name="fileName">The name of the file the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> writes to. </param>
		/// <exception cref="T:System.ArgumentNullException">The file is null. </exception>
		// Token: 0x060014F2 RID: 5362 RVA: 0x00037660 File Offset: 0x00035860
		public TextWriterTraceListener(string fileName) : this(fileName, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class using the specified writer as recipient of the tracing or debugging output.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> that receives the output from the <see cref="T:System.Diagnostics.TextWriterTraceListener" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The writer is null. </exception>
		// Token: 0x060014F3 RID: 5363 RVA: 0x00037670 File Offset: 0x00035870
		public TextWriterTraceListener(TextWriter writer) : this(writer, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class with the specified name, using the stream as the recipient of the debugging and tracing output.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that represents the stream the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> writes to. </param>
		/// <param name="name">The name of the new instance. </param>
		/// <exception cref="T:System.ArgumentNullException">The stream is null. </exception>
		// Token: 0x060014F4 RID: 5364 RVA: 0x00037680 File Offset: 0x00035880
		public TextWriterTraceListener(Stream stream, string name) : base((name == null) ? string.Empty : name)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.writer = new StreamWriter(stream);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class with the specified name, using the file as the recipient of the debugging and tracing output.</summary>
		/// <param name="fileName">The name of the file the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> writes to. </param>
		/// <param name="name">The name of the new instance. </param>
		/// <exception cref="T:System.ArgumentNullException">The stream is null. </exception>
		// Token: 0x060014F5 RID: 5365 RVA: 0x000376C4 File Offset: 0x000358C4
		public TextWriterTraceListener(string fileName, string name) : base((name == null) ? string.Empty : name)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.writer = new StreamWriter(new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class with the specified name, using the specified writer as recipient of the tracing or debugging output.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> that receives the output from the <see cref="T:System.Diagnostics.TextWriterTraceListener" />. </param>
		/// <param name="name">The name of the new instance. </param>
		/// <exception cref="T:System.ArgumentNullException">The writer is null. </exception>
		// Token: 0x060014F6 RID: 5366 RVA: 0x00037710 File Offset: 0x00035910
		public TextWriterTraceListener(TextWriter writer, string name) : base((name == null) ? string.Empty : name)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
		}

		/// <summary>Gets or sets the text writer that receives the tracing or debugging output.</summary>
		/// <returns>A <see cref="T:System.IO.TextWriter" /> that represents the writer that receives the tracing or debugging output.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00037744 File Offset: 0x00035944
		// (set) Token: 0x060014F8 RID: 5368 RVA: 0x0003774C File Offset: 0x0003594C
		public TextWriter Writer
		{
			get
			{
				return this.writer;
			}
			set
			{
				this.writer = value;
			}
		}

		/// <summary>Closes the <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" /> so that it no longer receives tracing or debugging output.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060014F9 RID: 5369 RVA: 0x00037758 File Offset: 0x00035958
		public override void Close()
		{
			if (this.writer != null)
			{
				this.writer.Flush();
				this.writer.Close();
				this.writer = null;
			}
		}

		/// <summary>Disposes this <see cref="T:System.Diagnostics.TextWriterTraceListener" /> object.</summary>
		/// <param name="disposing">true to release managed resources; if false, <see cref="M:System.Diagnostics.TextWriterTraceListener.Dispose(System.Boolean)" /> has no effect.</param>
		// Token: 0x060014FA RID: 5370 RVA: 0x00037790 File Offset: 0x00035990
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
			base.Dispose(disposing);
		}

		/// <summary>Flushes the output buffer for the <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060014FB RID: 5371 RVA: 0x000377A8 File Offset: 0x000359A8
		public override void Flush()
		{
			if (this.writer != null)
			{
				this.writer.Flush();
			}
		}

		/// <summary>Writes a message to this instance's <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" />.</summary>
		/// <param name="message">A message to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060014FC RID: 5372 RVA: 0x000377C0 File Offset: 0x000359C0
		public override void Write(string message)
		{
			if (this.writer != null)
			{
				if (base.NeedIndent)
				{
					this.WriteIndent();
				}
				this.writer.Write(message);
			}
		}

		/// <summary>Writes a message to this instance's <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" /> followed by a line terminator. The default line terminator is a carriage return followed by a line feed (\r\n).</summary>
		/// <param name="message">A message to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060014FD RID: 5373 RVA: 0x000377F8 File Offset: 0x000359F8
		public override void WriteLine(string message)
		{
			if (this.writer != null)
			{
				if (base.NeedIndent)
				{
					this.WriteIndent();
				}
				this.writer.WriteLine(message);
				base.NeedIndent = true;
			}
		}

		// Token: 0x0400065D RID: 1629
		private TextWriter writer;
	}
}
