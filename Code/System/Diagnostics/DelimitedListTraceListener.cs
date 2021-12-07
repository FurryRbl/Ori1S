using System;
using System.IO;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Directs tracing or debugging output to a text writer, such as a stream writer, or to a stream, such as a file stream.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000218 RID: 536
	public class DelimitedListTraceListener : TextWriterTraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DelimitedListTraceListener" /> class that writes to the specified file.  </summary>
		/// <param name="fileName">The name of the file to receive the output.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null. </exception>
		// Token: 0x06001200 RID: 4608 RVA: 0x0002FB78 File Offset: 0x0002DD78
		public DelimitedListTraceListener(string fileName) : base(fileName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DelimitedListTraceListener" /> class that writes to the specified file and has the specified name. </summary>
		/// <param name="fileName">The name of the file to receive the output. </param>
		/// <param name="name">The name of the new instance of the trace listener. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null. </exception>
		// Token: 0x06001201 RID: 4609 RVA: 0x0002FB8C File Offset: 0x0002DD8C
		public DelimitedListTraceListener(string fileName, string name) : base(fileName, name)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DelimitedListTraceListener" /> class that writes to the specified output stream. </summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> to receive the output.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x06001202 RID: 4610 RVA: 0x0002FBA4 File Offset: 0x0002DDA4
		public DelimitedListTraceListener(Stream stream) : base(stream)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DelimitedListTraceListener" /> class that writes to the specified output stream and has the specified name. </summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> to receive the output.</param>
		/// <param name="name">The name of the new instance of the trace listener. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x06001203 RID: 4611 RVA: 0x0002FBB8 File Offset: 0x0002DDB8
		public DelimitedListTraceListener(Stream stream, string name) : base(stream, name)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DelimitedListTraceListener" /> class that writes to the specified text writer. </summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to receive the output.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is null. </exception>
		// Token: 0x06001204 RID: 4612 RVA: 0x0002FBD0 File Offset: 0x0002DDD0
		public DelimitedListTraceListener(TextWriter writer) : base(writer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DelimitedListTraceListener" /> class that writes to the specified text writer and has the specified name. </summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to receive the output.</param>
		/// <param name="name">The name of the new instance of the trace listener. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is null. </exception>
		// Token: 0x06001205 RID: 4613 RVA: 0x0002FBE4 File Offset: 0x0002DDE4
		public DelimitedListTraceListener(TextWriter writer, string name) : base(writer, name)
		{
		}

		/// <summary>Gets or sets the delimiter for the delimited list.</summary>
		/// <returns>The delimiter for the delimited list.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.DelimitedListTraceListener.Delimiter" /> is set to null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Diagnostics.DelimitedListTraceListener.Delimiter" /> is set to an empty string ("").</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x0002FC14 File Offset: 0x0002DE14
		// (set) Token: 0x06001208 RID: 4616 RVA: 0x0002FC1C File Offset: 0x0002DE1C
		public string Delimiter
		{
			get
			{
				return this.delimiter;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.delimiter = value;
			}
		}

		/// <summary>Returns the custom configuration file attribute supported by the delimited trace listener.</summary>
		/// <returns>A string array that contains the single value "delimiter".</returns>
		// Token: 0x06001209 RID: 4617 RVA: 0x0002FC38 File Offset: 0x0002DE38
		protected internal override string[] GetSupportedAttributes()
		{
			return DelimitedListTraceListener.attributes;
		}

		/// <summary>Writes trace information, a data object, and event information to the output file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">A data object to write to the output file or stream.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600120A RID: 4618 RVA: 0x0002FC40 File Offset: 0x0002DE40
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			this.TraceCore(eventCache, source, eventType, id, null, new object[]
			{
				data
			});
		}

		/// <summary>Writes trace information, an array of data objects, and event information to the output file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">An array of data objects to write to the output file or stream.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600120B RID: 4619 RVA: 0x0002FC64 File Offset: 0x0002DE64
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			this.TraceCore(eventCache, source, eventType, id, null, data);
		}

		/// <summary>Writes trace information, a message, and event information to the output file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">The trace message to write to the output file or stream.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600120C RID: 4620 RVA: 0x0002FC74 File Offset: 0x0002DE74
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			this.TraceCore(eventCache, source, eventType, id, message, new object[0]);
		}

		/// <summary>Writes trace information, a formatted array of objects, and event information to the output file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="format">A format string that contains zero or more format items that correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An array containing zero or more objects to format.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600120D RID: 4621 RVA: 0x0002FC8C File Offset: 0x0002DE8C
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			this.TraceCore(eventCache, source, eventType, id, string.Format(format, args), new object[0]);
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0002FCB4 File Offset: 0x0002DEB4
		private void TraceCore(TraceEventCache c, string source, TraceEventType eventType, int id, string message, params object[] data)
		{
			this.Write(string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{12}", new object[]
			{
				this.delimiter,
				(source == null) ? null : ("\"" + source.Replace("\"", "\"\"") + "\""),
				eventType,
				id,
				(message == null) ? null : ("\"" + message.Replace("\"", "\"\"") + "\""),
				this.FormatData(data),
				(!this.IsTarget(c, TraceOptions.ProcessId)) ? null : c.ProcessId.ToString(),
				(!this.IsTarget(c, TraceOptions.LogicalOperationStack)) ? null : TraceListener.FormatArray(c.LogicalOperationStack, ", "),
				(!this.IsTarget(c, TraceOptions.ThreadId)) ? null : c.ThreadId,
				(!this.IsTarget(c, TraceOptions.DateTime)) ? null : c.DateTime.ToString("o"),
				(!this.IsTarget(c, TraceOptions.Timestamp)) ? null : c.Timestamp.ToString(),
				(!this.IsTarget(c, TraceOptions.Callstack)) ? null : c.Callstack,
				Environment.NewLine
			}));
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0002FE3C File Offset: 0x0002E03C
		private bool IsTarget(TraceEventCache c, TraceOptions opt)
		{
			return c != null && (base.TraceOutputOptions & opt) != TraceOptions.None;
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0002FE58 File Offset: 0x0002E058
		private string FormatData(object[] data)
		{
			if (data == null || data.Length == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				if (data[i] != null)
				{
					stringBuilder.Append('"').Append(data[i].ToString().Replace("\"", "\"\"")).Append('"');
				}
				if (i + 1 < data.Length)
				{
					stringBuilder.Append(',');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000526 RID: 1318
		private static readonly string[] attributes = new string[]
		{
			"delimiter"
		};

		// Token: 0x04000527 RID: 1319
		private string delimiter = ";";
	}
}
