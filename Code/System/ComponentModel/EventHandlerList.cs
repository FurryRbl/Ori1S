using System;

namespace System.ComponentModel
{
	/// <summary>Provides a simple list of delegates. This class cannot be inherited.</summary>
	// Token: 0x0200014A RID: 330
	public sealed class EventHandlerList : IDisposable
	{
		/// <summary>Gets or sets the delegate for the specified object.</summary>
		/// <returns>The delegate for the specified key, or null if a delegate does not exist.</returns>
		/// <param name="key">An object to find in the list. </param>
		// Token: 0x170002BF RID: 703
		public Delegate this[object key]
		{
			get
			{
				if (key == null)
				{
					return this.null_entry;
				}
				ListEntry listEntry = this.FindEntry(key);
				if (listEntry != null)
				{
					return listEntry.value;
				}
				return null;
			}
			set
			{
				this.AddHandler(key, value);
			}
		}

		/// <summary>Adds a delegate to the list.</summary>
		/// <param name="key">The object that owns the event. </param>
		/// <param name="value">The delegate to add to the list. </param>
		// Token: 0x06000C33 RID: 3123 RVA: 0x0001FD98 File Offset: 0x0001DF98
		public void AddHandler(object key, Delegate value)
		{
			if (key == null)
			{
				this.null_entry = Delegate.Combine(this.null_entry, value);
				return;
			}
			ListEntry listEntry = this.FindEntry(key);
			if (listEntry == null)
			{
				listEntry = new ListEntry();
				listEntry.key = key;
				listEntry.value = null;
				listEntry.next = this.entries;
				this.entries = listEntry;
			}
			listEntry.value = Delegate.Combine(listEntry.value, value);
		}

		/// <summary>Adds a list of delegates to the current list.</summary>
		/// <param name="listToAddFrom">The list to add.</param>
		// Token: 0x06000C34 RID: 3124 RVA: 0x0001FE08 File Offset: 0x0001E008
		public void AddHandlers(EventHandlerList listToAddFrom)
		{
			if (listToAddFrom == null)
			{
				return;
			}
			for (ListEntry next = listToAddFrom.entries; next != null; next = next.next)
			{
				this.AddHandler(next.key, next.value);
			}
		}

		/// <summary>Removes a delegate from the list.</summary>
		/// <param name="key">The object that owns the event. </param>
		/// <param name="value">The delegate to remove from the list. </param>
		// Token: 0x06000C35 RID: 3125 RVA: 0x0001FE48 File Offset: 0x0001E048
		public void RemoveHandler(object key, Delegate value)
		{
			if (key == null)
			{
				this.null_entry = Delegate.Remove(this.null_entry, value);
				return;
			}
			ListEntry listEntry = this.FindEntry(key);
			if (listEntry == null)
			{
				return;
			}
			listEntry.value = Delegate.Remove(listEntry.value, value);
		}

		/// <summary>Disposes the delegate list.</summary>
		// Token: 0x06000C36 RID: 3126 RVA: 0x0001FE90 File Offset: 0x0001E090
		public void Dispose()
		{
			this.entries = null;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0001FE9C File Offset: 0x0001E09C
		private ListEntry FindEntry(object key)
		{
			for (ListEntry next = this.entries; next != null; next = next.next)
			{
				if (next.key == key)
				{
					return next;
				}
			}
			return null;
		}

		// Token: 0x0400036D RID: 877
		private ListEntry entries;

		// Token: 0x0400036E RID: 878
		private Delegate null_entry;
	}
}
