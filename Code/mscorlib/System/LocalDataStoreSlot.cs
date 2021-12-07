using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	/// <summary>Encapsulates a memory slot to store local data. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200014C RID: 332
	[ComVisible(true)]
	public sealed class LocalDataStoreSlot
	{
		// Token: 0x060011CD RID: 4557 RVA: 0x00047244 File Offset: 0x00045444
		internal LocalDataStoreSlot(bool in_thread)
		{
			this.thread_local = in_thread;
			object obj = LocalDataStoreSlot.lock_obj;
			lock (obj)
			{
				bool[] array;
				if (in_thread)
				{
					array = LocalDataStoreSlot.slot_bitmap_thread;
				}
				else
				{
					array = LocalDataStoreSlot.slot_bitmap_context;
				}
				int i;
				if (array != null)
				{
					for (i = 0; i < array.Length; i++)
					{
						if (!array[i])
						{
							this.slot = i;
							array[i] = true;
							return;
						}
					}
					bool[] array2 = new bool[i + 2];
					array.CopyTo(array2, 0);
					array = array2;
				}
				else
				{
					array = new bool[2];
					i = 0;
				}
				array[i] = true;
				this.slot = i;
				if (in_thread)
				{
					LocalDataStoreSlot.slot_bitmap_thread = array;
				}
				else
				{
					LocalDataStoreSlot.slot_bitmap_context = array;
				}
			}
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0004732C File Offset: 0x0004552C
		protected override void Finalize()
		{
			try
			{
				Thread.FreeLocalSlotValues(this.slot, this.thread_local);
				object obj = LocalDataStoreSlot.lock_obj;
				lock (obj)
				{
					if (this.thread_local)
					{
						LocalDataStoreSlot.slot_bitmap_thread[this.slot] = false;
					}
					else
					{
						LocalDataStoreSlot.slot_bitmap_context[this.slot] = false;
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x04000526 RID: 1318
		internal int slot;

		// Token: 0x04000527 RID: 1319
		internal bool thread_local;

		// Token: 0x04000528 RID: 1320
		private static object lock_obj = new object();

		// Token: 0x04000529 RID: 1321
		private static bool[] slot_bitmap_thread;

		// Token: 0x0400052A RID: 1322
		private static bool[] slot_bitmap_context;
	}
}
