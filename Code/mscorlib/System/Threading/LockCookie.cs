using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Defines the lock that implements single-writer/multiple-reader semantics. This is a value type.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006A2 RID: 1698
	[ComVisible(true)]
	public struct LockCookie
	{
		// Token: 0x060040A0 RID: 16544 RVA: 0x000DF238 File Offset: 0x000DD438
		internal LockCookie(int thread_id)
		{
			this.ThreadId = thread_id;
			this.ReaderLocks = 0;
			this.WriterLocks = 0;
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x000DF250 File Offset: 0x000DD450
		internal LockCookie(int thread_id, int reader_locks, int writer_locks)
		{
			this.ThreadId = thread_id;
			this.ReaderLocks = reader_locks;
			this.WriterLocks = writer_locks;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060040A2 RID: 16546 RVA: 0x000DF268 File Offset: 0x000DD468
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Threading.LockCookie" />.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Threading.LockCookie" /> to compare to the current instance.</param>
		// Token: 0x060040A3 RID: 16547 RVA: 0x000DF27C File Offset: 0x000DD47C
		public bool Equals(LockCookie obj)
		{
			return this.ThreadId == obj.ThreadId && this.ReaderLocks == obj.ReaderLocks && this.WriterLocks == obj.WriterLocks;
		}

		/// <summary>Indicates whether a specified object is a <see cref="T:System.Threading.LockCookie" /> and is equal to the current instance.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The object to compare to the current instance.</param>
		// Token: 0x060040A4 RID: 16548 RVA: 0x000DF2B8 File Offset: 0x000DD4B8
		public override bool Equals(object obj)
		{
			return obj is LockCookie && obj.Equals(this);
		}

		/// <summary>Indicates whether two <see cref="T:System.Threading.LockCookie" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Threading.LockCookie" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Threading.LockCookie" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060040A5 RID: 16549 RVA: 0x000DF2D8 File Offset: 0x000DD4D8
		public static bool operator ==(LockCookie a, LockCookie b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Threading.LockCookie" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Threading.LockCookie" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Threading.LockCookie" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060040A6 RID: 16550 RVA: 0x000DF2E4 File Offset: 0x000DD4E4
		public static bool operator !=(LockCookie a, LockCookie b)
		{
			return !a.Equals(b);
		}

		// Token: 0x04001BB4 RID: 7092
		internal int ThreadId;

		// Token: 0x04001BB5 RID: 7093
		internal int ReaderLocks;

		// Token: 0x04001BB6 RID: 7094
		internal int WriterLocks;
	}
}
