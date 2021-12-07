using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020006CE RID: 1742
	internal sealed class CollectionDebuggerView<T>
	{
		// Token: 0x060042E4 RID: 17124 RVA: 0x000E50BC File Offset: 0x000E32BC
		public CollectionDebuggerView(ICollection<T> col)
		{
			this.c = col;
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x060042E5 RID: 17125 RVA: 0x000E50CC File Offset: 0x000E32CC
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				T[] array = new T[this.c.Count];
				this.c.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04001C51 RID: 7249
		private readonly ICollection<T> c;
	}
}
