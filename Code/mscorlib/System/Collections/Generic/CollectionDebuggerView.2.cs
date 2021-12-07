using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020006CF RID: 1743
	internal sealed class CollectionDebuggerView<T, U>
	{
		// Token: 0x060042E6 RID: 17126 RVA: 0x000E50F8 File Offset: 0x000E32F8
		public CollectionDebuggerView(ICollection<KeyValuePair<T, U>> col)
		{
			this.c = col;
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x060042E7 RID: 17127 RVA: 0x000E5108 File Offset: 0x000E3308
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<T, U>[] Items
		{
			get
			{
				KeyValuePair<T, U>[] array = new KeyValuePair<T, U>[this.c.Count];
				this.c.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04001C52 RID: 7250
		private readonly ICollection<KeyValuePair<T, U>> c;
	}
}
