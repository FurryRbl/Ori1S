using System;

namespace System.Diagnostics
{
	/// <summary>Indicates whether a listener should trace a message based on the source of a trace.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200024D RID: 589
	public class SourceFilter : TraceFilter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SourceFilter" /> class, specifying the name of the trace source. </summary>
		/// <param name="source">The name of the trace source.</param>
		// Token: 0x060014C3 RID: 5315 RVA: 0x0003707C File Offset: 0x0003527C
		public SourceFilter(string source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.source = source;
		}

		/// <summary>Gets or sets the name of the trace source.</summary>
		/// <returns>The name of the trace source.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x0003709C File Offset: 0x0003529C
		// (set) Token: 0x060014C5 RID: 5317 RVA: 0x000370A4 File Offset: 0x000352A4
		public string Source
		{
			get
			{
				return this.source;
			}
			set
			{
				if (this.source == null)
				{
					throw new ArgumentNullException("value");
				}
				this.source = value;
			}
		}

		/// <summary>Determines whether the trace listener should trace the event.</summary>
		/// <returns>true if the trace should be produced; otherwise, false. </returns>
		/// <param name="cache">An object that represents the information cache for the trace event.</param>
		/// <param name="source">The name of the source.</param>
		/// <param name="eventType">One of the enumeration values that identifies the event type. </param>
		/// <param name="id">A trace identifier number.</param>
		/// <param name="formatOrMessage">The format to use for writing an array of arguments or a message to write.</param>
		/// <param name="args">An array of argument objects.</param>
		/// <param name="data1">A trace data object.</param>
		/// <param name="data">An array of trace data objects.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060014C6 RID: 5318 RVA: 0x000370C4 File Offset: 0x000352C4
		public override bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1, object[] data)
		{
			return source == this.source;
		}

		// Token: 0x04000642 RID: 1602
		private string source;
	}
}
