using System;
using System.IO;
using System.Threading;
using System.Xml;

namespace System.Diagnostics
{
	/// <summary>Directs tracing or debugging output as XML-encoded data to a <see cref="T:System.IO.TextWriter" /> or to a <see cref="T:System.IO.Stream" />, such as a <see cref="T:System.IO.FileStream" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200026B RID: 619
	public class XmlWriterTraceListener : TextWriterTraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.XmlWriterTraceListener" /> class, using the specified file as the recipient of the debugging and tracing output. </summary>
		/// <param name="filename">The name of the file to write to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filename" /> is null. </exception>
		// Token: 0x06001600 RID: 5632 RVA: 0x0003AC04 File Offset: 0x00038E04
		public XmlWriterTraceListener(string filename) : this(filename, XmlWriterTraceListener.default_name)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.XmlWriterTraceListener" /> class with the specified name, using the specified file as the recipient of the debugging and tracing output.  </summary>
		/// <param name="filename">The name of the file to write to. </param>
		/// <param name="name">The name of the new instance. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x06001601 RID: 5633 RVA: 0x0003AC14 File Offset: 0x00038E14
		public XmlWriterTraceListener(string filename, string name) : this(new StreamWriter(new FileStream(filename, FileMode.Append, FileAccess.Write, FileShare.ReadWrite)), name)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.XmlWriterTraceListener" /> class, using the specified stream as the recipient of the debugging and tracing output. </summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that represents the stream the trace listener writes to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x06001602 RID: 5634 RVA: 0x0003AC2C File Offset: 0x00038E2C
		public XmlWriterTraceListener(Stream stream) : this(stream, XmlWriterTraceListener.default_name)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.XmlWriterTraceListener" /> class with the specified name, using the specified stream as the recipient of the debugging and tracing output. </summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that represents the stream the trace listener writes to. </param>
		/// <param name="name">The name of the new instance. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x06001603 RID: 5635 RVA: 0x0003AC3C File Offset: 0x00038E3C
		public XmlWriterTraceListener(Stream writer, string name) : this(new StreamWriter(writer), name)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.XmlWriterTraceListener" /> class using the specified writer as the recipient of the debugging and tracing output. </summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> that receives the output from the trace listener.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is null. </exception>
		// Token: 0x06001604 RID: 5636 RVA: 0x0003AC4C File Offset: 0x00038E4C
		public XmlWriterTraceListener(TextWriter writer) : this(writer, XmlWriterTraceListener.default_name)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.XmlWriterTraceListener" /> class with the specified name, using the specified writer as the recipient of the debugging and tracing output. </summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> that receives the output from the trace listener. </param>
		/// <param name="name">The name of the new instance. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is null. </exception>
		// Token: 0x06001605 RID: 5637 RVA: 0x0003AC5C File Offset: 0x00038E5C
		public XmlWriterTraceListener(TextWriter writer, string name) : base(name)
		{
			this.w = XmlWriter.Create(writer, new XmlWriterSettings
			{
				OmitXmlDeclaration = true
			});
		}

		/// <summary>Closes the <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" /> for this listener so that it no longer receives tracing or debugging output.</summary>
		// Token: 0x06001607 RID: 5639 RVA: 0x0003ACAC File Offset: 0x00038EAC
		public override void Close()
		{
			this.w.Close();
		}

		/// <summary>Writes trace information including an error message and a detailed error message to the file or stream.</summary>
		/// <param name="message">The error message to write.</param>
		/// <param name="detailMessage">The detailed error message to append to the error message.</param>
		// Token: 0x06001608 RID: 5640 RVA: 0x0003ACBC File Offset: 0x00038EBC
		public override void Fail(string message, string detailMessage)
		{
			this.TraceEvent(null, null, TraceEventType.Error, 0, message + " " + detailMessage);
		}

		/// <summary>Writes trace information, a data object, and event information to the file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">The source name. </param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">A data object to emit.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001609 RID: 5641 RVA: 0x0003ACE0 File Offset: 0x00038EE0
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			this.TraceCore(eventCache, source, eventType, id, false, Guid.Empty, 2, true, new object[]
			{
				data
			});
		}

		/// <summary>Writes trace information, data objects, and event information to the file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">The source name. </param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">An array of data objects to emit.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600160A RID: 5642 RVA: 0x0003AD0C File Offset: 0x00038F0C
		[MonoLimitation("level is not always correct")]
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			this.TraceCore(eventCache, source, eventType, id, false, Guid.Empty, 2, true, data);
		}

		/// <summary>Writes trace information, a message, and event information to the file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">The source name. </param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">The message to write.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600160B RID: 5643 RVA: 0x0003AD30 File Offset: 0x00038F30
		[MonoLimitation("level is not always correct")]
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			this.TraceCore(eventCache, source, TraceEventType.Transfer, id, false, Guid.Empty, 2, true, new object[]
			{
				message
			});
		}

		/// <summary>Writes trace information, a formatted message, and event information to the file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">The source name. </param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="format">A format string that contains zero or more format items that correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array containing zero or more objects to format.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600160C RID: 5644 RVA: 0x0003AD60 File Offset: 0x00038F60
		[MonoLimitation("level is not always correct")]
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			this.TraceCore(eventCache, source, TraceEventType.Transfer, id, false, Guid.Empty, 2, true, new object[]
			{
				string.Format(format, args)
			});
		}

		/// <summary>Writes trace information including the identity of a related activity, a message, and event information to the file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">The source name. </param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">A trace message to write.</param>
		/// <param name="relatedActivityId">A <see cref="T:System.Guid" /> structure that identifies a related activity.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600160D RID: 5645 RVA: 0x0003AD98 File Offset: 0x00038F98
		public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
		{
			this.TraceCore(eventCache, source, TraceEventType.Transfer, id, true, relatedActivityId, 255, true, new object[]
			{
				message
			});
		}

		/// <summary>Writes a verbatim message without any additional context information to the file or stream.</summary>
		/// <param name="message">The message to write.</param>
		// Token: 0x0600160E RID: 5646 RVA: 0x0003ADC8 File Offset: 0x00038FC8
		public override void Write(string message)
		{
			this.WriteLine(message);
		}

		/// <summary>Writes a verbatim message without any additional context information followed by the current line terminator to the file or stream.</summary>
		/// <param name="message">The message to write.</param>
		// Token: 0x0600160F RID: 5647 RVA: 0x0003ADD4 File Offset: 0x00038FD4
		[MonoLimitation("level is not always correct")]
		public override void WriteLine(string message)
		{
			this.TraceCore(null, "Trace", TraceEventType.Information, 0, false, Guid.Empty, 8, false, new object[]
			{
				message
			});
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x0003AE04 File Offset: 0x00039004
		private void TraceCore(TraceEventCache eventCache, string source, TraceEventType eventType, int id, bool hasRelatedActivity, Guid relatedActivity, int level, bool wrapData, params object[] data)
		{
			Process process = (eventCache == null) ? Process.GetCurrentProcess() : Process.GetProcessById(eventCache.ProcessId);
			this.w.WriteStartElement("E2ETraceEvent", XmlWriterTraceListener.e2e_ns);
			this.w.WriteStartElement("System", XmlWriterTraceListener.sys_ns);
			this.w.WriteStartElement("EventID", XmlWriterTraceListener.sys_ns);
			this.w.WriteString(XmlConvert.ToString(id));
			this.w.WriteEndElement();
			this.w.WriteStartElement("Type", XmlWriterTraceListener.sys_ns);
			this.w.WriteString("3");
			this.w.WriteEndElement();
			this.w.WriteStartElement("SubType", XmlWriterTraceListener.sys_ns);
			this.w.WriteAttributeString("Name", eventType.ToString());
			this.w.WriteString("0");
			this.w.WriteEndElement();
			this.w.WriteStartElement("Level", XmlWriterTraceListener.sys_ns);
			this.w.WriteString(level.ToString());
			this.w.WriteEndElement();
			this.w.WriteStartElement("TimeCreated", XmlWriterTraceListener.sys_ns);
			this.w.WriteAttributeString("SystemTime", XmlConvert.ToString((eventCache == null) ? DateTime.Now : eventCache.DateTime));
			this.w.WriteEndElement();
			this.w.WriteStartElement("Source", XmlWriterTraceListener.sys_ns);
			this.w.WriteAttributeString("Name", source);
			this.w.WriteEndElement();
			this.w.WriteStartElement("Correlation", XmlWriterTraceListener.sys_ns);
			this.w.WriteAttributeString("ActivityID", "{" + Guid.Empty + "}");
			this.w.WriteEndElement();
			this.w.WriteStartElement("Execution", XmlWriterTraceListener.sys_ns);
			this.w.WriteAttributeString("ProcessName", process.MainModule.ModuleName);
			this.w.WriteAttributeString("ProcessID", process.Id.ToString());
			this.w.WriteAttributeString("ThreadID", (eventCache == null) ? Thread.CurrentThread.ManagedThreadId.ToString() : eventCache.ThreadId);
			this.w.WriteEndElement();
			this.w.WriteStartElement("Channel", XmlWriterTraceListener.sys_ns);
			this.w.WriteEndElement();
			this.w.WriteStartElement("Computer");
			this.w.WriteString(process.MachineName);
			this.w.WriteEndElement();
			this.w.WriteEndElement();
			this.w.WriteStartElement("ApplicationData", XmlWriterTraceListener.e2e_ns);
			foreach (object obj in data)
			{
				if (wrapData)
				{
					this.w.WriteStartElement("TraceData", XmlWriterTraceListener.e2e_ns);
				}
				if (obj != null)
				{
					this.w.WriteString(obj.ToString());
				}
				if (wrapData)
				{
					this.w.WriteEndElement();
				}
			}
			this.w.WriteEndElement();
			this.w.WriteEndElement();
		}

		// Token: 0x040006D5 RID: 1749
		private static readonly string e2e_ns = "http://schemas.microsoft.com/2004/06/E2ETraceEvent";

		// Token: 0x040006D6 RID: 1750
		private static readonly string sys_ns = "http://schemas.microsoft.com/2004/06/windows/eventlog/system";

		// Token: 0x040006D7 RID: 1751
		private static readonly string default_name = "XmlWriter";

		// Token: 0x040006D8 RID: 1752
		private XmlWriter w;
	}
}
