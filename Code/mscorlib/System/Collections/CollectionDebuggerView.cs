using System;
using System.Diagnostics;

namespace System.Collections
{
	// Token: 0x020001B7 RID: 439
	internal sealed class CollectionDebuggerView
	{
		// Token: 0x060016DC RID: 5852 RVA: 0x00058CC0 File Offset: 0x00056EC0
		public CollectionDebuggerView(ICollection col)
		{
			this.c = col;
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x00058CD0 File Offset: 0x00056ED0
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public object[] Items
		{
			get
			{
				object[] array = new object[Math.Min(this.c.Count, 1024)];
				this.c.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04000877 RID: 2167
		private readonly ICollection c;
	}
}
