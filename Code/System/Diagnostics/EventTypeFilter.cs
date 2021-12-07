using System;

namespace System.Diagnostics
{
	/// <summary>Indicates whether a listener should trace based on the event type.</summary>
	// Token: 0x0200022B RID: 555
	public class EventTypeFilter : TraceFilter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventTypeFilter" /> class. </summary>
		/// <param name="level">A bitwise combination of the <see cref="T:System.Diagnostics.SourceLevels" /> values that specifies the event type of the messages to trace. </param>
		// Token: 0x060012F8 RID: 4856 RVA: 0x00032BE8 File Offset: 0x00030DE8
		public EventTypeFilter(SourceLevels eventType)
		{
			this.event_type = eventType;
		}

		/// <summary>Gets or sets the event type of the messages to trace.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.SourceLevels" /> values.</returns>
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x00032BF8 File Offset: 0x00030DF8
		// (set) Token: 0x060012FA RID: 4858 RVA: 0x00032C00 File Offset: 0x00030E00
		public SourceLevels EventType
		{
			get
			{
				return this.event_type;
			}
			set
			{
				this.event_type = value;
			}
		}

		/// <summary>Determines whether the trace listener should trace the event. </summary>
		/// <returns>trueif the trace should be produced; otherwise, false.</returns>
		/// <param name="cache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that represents the information cache for the trace event.</param>
		/// <param name="source">The name of the source.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values. </param>
		/// <param name="id">A trace identifier number.</param>
		/// <param name="formatOrMessage">The format to use for writing an array of arguments, or a message to write.</param>
		/// <param name="args">An array of argument objects.</param>
		/// <param name="data1">A trace data object.</param>
		/// <param name="data">An array of trace data objects.</param>
		// Token: 0x060012FB RID: 4859 RVA: 0x00032C0C File Offset: 0x00030E0C
		public override bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1, object[] data)
		{
			switch (eventType)
			{
			case TraceEventType.Critical:
				return (this.event_type & SourceLevels.Critical) != SourceLevels.Off;
			case TraceEventType.Error:
				return (this.event_type & SourceLevels.Error) != SourceLevels.Off;
			default:
				if (eventType == TraceEventType.Verbose)
				{
					return (this.event_type & SourceLevels.Verbose) != SourceLevels.Off;
				}
				if (eventType != TraceEventType.Start && eventType != TraceEventType.Stop && eventType != TraceEventType.Suspend && eventType != TraceEventType.Resume && eventType != TraceEventType.Transfer)
				{
					return this.event_type != SourceLevels.Off;
				}
				return (this.event_type & SourceLevels.ActivityTracing) != SourceLevels.Off;
			case TraceEventType.Warning:
				return (this.event_type & SourceLevels.Warning) != SourceLevels.Off;
			case TraceEventType.Information:
				return (this.event_type & SourceLevels.Information) != SourceLevels.Off;
			}
		}

		// Token: 0x0400056D RID: 1389
		private SourceLevels event_type;
	}
}
