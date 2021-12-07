using System;
using System.Collections;

namespace System.Diagnostics
{
	/// <summary>Defines size and enumerators for a collection of <see cref="T:System.Diagnostics.EventLogEntry" /> instances.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200021E RID: 542
	public class EventLogEntryCollection : ICollection, IEnumerable
	{
		// Token: 0x06001275 RID: 4725 RVA: 0x00031D54 File Offset: 0x0002FF54
		internal EventLogEntryCollection(EventLogImpl impl)
		{
			this._impl = impl;
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Diagnostics.EventLogEntryCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>false if access to the collection is not synchronized (thread-safe).</returns>
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x00031D64 File Offset: 0x0002FF64
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Diagnostics.EventLogEntryCollection" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x00031D68 File Offset: 0x0002FF68
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements that are copied from the collection. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		// Token: 0x06001278 RID: 4728 RVA: 0x00031D6C File Offset: 0x0002FF6C
		void ICollection.CopyTo(Array array, int index)
		{
			EventLogEntry[] entries = this._impl.GetEntries();
			Array.Copy(entries, 0, array, index, entries.Length);
		}

		/// <summary>Gets the number of entries in the event log (that is, the number of elements in the <see cref="T:System.Diagnostics.EventLogEntry" /> collection).</summary>
		/// <returns>The number of entries currently in the event log.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x00031D94 File Offset: 0x0002FF94
		public int Count
		{
			get
			{
				return this._impl.EntryCount;
			}
		}

		/// <summary>Gets an entry in the event log, based on an index that starts at 0 (zero).</summary>
		/// <returns>The event log entry at the location that is specified by the <paramref name="index" /> parameter.</returns>
		/// <param name="index">The zero-based index that is associated with the event log entry. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700042C RID: 1068
		public virtual EventLogEntry this[int index]
		{
			get
			{
				return this._impl[index];
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Diagnostics.EventLogEntryCollection" /> to an array of <see cref="T:System.Diagnostics.EventLogEntry" /> instances, starting at a particular array index.</summary>
		/// <param name="entries">The one-dimensional array of <see cref="T:System.Diagnostics.EventLogEntry" /> instances that is the destination of the elements copied from the collection. The array must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in the array at which copying begins. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600127B RID: 4731 RVA: 0x00031DB4 File Offset: 0x0002FFB4
		public void CopyTo(EventLogEntry[] eventLogEntries, int index)
		{
			EventLogEntry[] entries = this._impl.GetEntries();
			Array.Copy(entries, 0, eventLogEntries, index, entries.Length);
		}

		/// <summary>Supports a simple iteration over the <see cref="T:System.Diagnostics.EventLogEntryCollection" /> object.</summary>
		/// <returns>An object that can be used to iterate over the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600127C RID: 4732 RVA: 0x00031DDC File Offset: 0x0002FFDC
		public IEnumerator GetEnumerator()
		{
			return new EventLogEntryCollection.EventLogEntryEnumerator(this._impl);
		}

		// Token: 0x0400053E RID: 1342
		private readonly EventLogImpl _impl;

		// Token: 0x0200021F RID: 543
		private class EventLogEntryEnumerator : IEnumerator
		{
			// Token: 0x0600127D RID: 4733 RVA: 0x00031DEC File Offset: 0x0002FFEC
			internal EventLogEntryEnumerator(EventLogImpl impl)
			{
				this._impl = impl;
			}

			// Token: 0x1700042D RID: 1069
			// (get) Token: 0x0600127E RID: 4734 RVA: 0x00031E04 File Offset: 0x00030004
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x1700042E RID: 1070
			// (get) Token: 0x0600127F RID: 4735 RVA: 0x00031E0C File Offset: 0x0003000C
			public EventLogEntry Current
			{
				get
				{
					if (this._currentEntry != null)
					{
						return this._currentEntry;
					}
					throw new InvalidOperationException("No current EventLog entry available, cursor is located before the first or after the last element of the enumeration.");
				}
			}

			// Token: 0x06001280 RID: 4736 RVA: 0x00031E2C File Offset: 0x0003002C
			public bool MoveNext()
			{
				this._currentIndex++;
				if (this._currentIndex >= this._impl.EntryCount)
				{
					this._currentEntry = null;
					return false;
				}
				this._currentEntry = this._impl[this._currentIndex];
				return true;
			}

			// Token: 0x06001281 RID: 4737 RVA: 0x00031E80 File Offset: 0x00030080
			public void Reset()
			{
				this._currentIndex = -1;
			}

			// Token: 0x0400053F RID: 1343
			private readonly EventLogImpl _impl;

			// Token: 0x04000540 RID: 1344
			private int _currentIndex = -1;

			// Token: 0x04000541 RID: 1345
			private EventLogEntry _currentEntry;
		}
	}
}
