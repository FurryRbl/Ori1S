﻿using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Represents language-neutral information for an event log entry.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200021C RID: 540
	public class EventInstance
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventInstance" /> class using the specified resource identifiers for the localized message and category text of the event entry.</summary>
		/// <param name="instanceId">A resource identifier that corresponds to a string defined in the message resource file of the event source.</param>
		/// <param name="categoryId">A resource identifier that corresponds to a string defined in the category resource file of the event source, or zero to specify no category for the event. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="instanceId" /> parameter is a negative value or a value larger than <see cref="F:System.UInt32.MaxValue" />.-or- The <paramref name="categoryId" /> parameter is a negative value or a value larger than <see cref="F:System.UInt16.MaxValue" />. </exception>
		// Token: 0x0600122A RID: 4650 RVA: 0x00030F74 File Offset: 0x0002F174
		public EventInstance(long instanceId, int categoryId) : this(instanceId, categoryId, EventLogEntryType.Information)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventInstance" /> class using the specified resource identifiers for the localized message and category text of the event entry and the specified event log entry type.</summary>
		/// <param name="instanceId">A resource identifier that corresponds to a string defined in the message resource file of the event source. </param>
		/// <param name="categoryId">A resource identifier that corresponds to a string defined in the category resource file of the event source, or zero to specify no category for the event. </param>
		/// <param name="entryType">An <see cref="T:System.Diagnostics.EventLogEntryType" /> value that indicates the event type. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="entryType" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" /> value. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="instanceId" /> is a negative value or a value larger than <see cref="F:System.UInt32.MaxValue" />.-or- <paramref name="categoryId" /> is a negative value or a value larger than <see cref="F:System.UInt16.MaxValue" />. </exception>
		// Token: 0x0600122B RID: 4651 RVA: 0x00030F80 File Offset: 0x0002F180
		public EventInstance(long instanceId, int categoryId, EventLogEntryType entryType)
		{
			this.InstanceId = instanceId;
			this.CategoryId = categoryId;
			this.EntryType = entryType;
		}

		/// <summary>Gets or sets the resource identifier that specifies the application-defined category of the event entry.</summary>
		/// <returns>A numeric category value or resource identifier that corresponds to a string defined in the category resource file of the event source. The default is zero, which signifies that no category will be displayed for the event entry.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to a negative value or to a value larger than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x00030FA8 File Offset: 0x0002F1A8
		// (set) Token: 0x0600122D RID: 4653 RVA: 0x00030FB0 File Offset: 0x0002F1B0
		public int CategoryId
		{
			get
			{
				return this._categoryId;
			}
			set
			{
				if (value < 0 || value > 65535)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._categoryId = value;
			}
		}

		/// <summary>Gets or sets the event type of the event log entry.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.EventLogEntryType" /> value that indicates the event entry type. The default value is <see cref="F:System.Diagnostics.EventLogEntryType.Information" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property is not set to a valid <see cref="T:System.Diagnostics.EventLogEntryType" /> value. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x00030FE4 File Offset: 0x0002F1E4
		// (set) Token: 0x0600122F RID: 4655 RVA: 0x00030FEC File Offset: 0x0002F1EC
		public EventLogEntryType EntryType
		{
			get
			{
				return this._entryType;
			}
			set
			{
				if (!Enum.IsDefined(typeof(EventLogEntryType), value))
				{
					throw new System.ComponentModel.InvalidEnumArgumentException("value", (int)value, typeof(EventLogEntryType));
				}
				this._entryType = value;
			}
		}

		/// <summary>Gets or sets the resource identifier that designates the message text of the event entry.</summary>
		/// <returns>A resource identifier that corresponds to a string defined in the message resource file of the event source.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to a negative value or to a value larger than <see cref="F:System.UInt32.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x00031028 File Offset: 0x0002F228
		// (set) Token: 0x06001231 RID: 4657 RVA: 0x00031030 File Offset: 0x0002F230
		public long InstanceId
		{
			get
			{
				return this._instanceId;
			}
			set
			{
				if (value < 0L || value > (long)((ulong)-1))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._instanceId = value;
			}
		}

		// Token: 0x0400052F RID: 1327
		private int _categoryId;

		// Token: 0x04000530 RID: 1328
		private EventLogEntryType _entryType;

		// Token: 0x04000531 RID: 1329
		private long _instanceId;
	}
}
