using System;

namespace System.Diagnostics
{
	/// <summary>Provides data for the <see cref="E:System.Diagnostics.EventLog.EntryWritten" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200021B RID: 539
	public class EntryWrittenEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EntryWrittenEventArgs" /> class.</summary>
		// Token: 0x06001227 RID: 4647 RVA: 0x00030F50 File Offset: 0x0002F150
		public EntryWrittenEventArgs() : this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EntryWrittenEventArgs" /> class with the specified event log entry.</summary>
		/// <param name="entry">An <see cref="T:System.Diagnostics.EventLogEntry" /> that represents the entry that was written. </param>
		// Token: 0x06001228 RID: 4648 RVA: 0x00030F5C File Offset: 0x0002F15C
		public EntryWrittenEventArgs(EventLogEntry entry)
		{
			this.entry = entry;
		}

		/// <summary>Gets the event log entry that was written to the log.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.EventLogEntry" /> that represents the entry that was written to the event log.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x00030F6C File Offset: 0x0002F16C
		public EventLogEntry Entry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x0400052E RID: 1326
		private EventLogEntry entry;
	}
}
