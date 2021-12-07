using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000019 RID: 25
	public class StrongBox<T> : IStrongBox
	{
		// Token: 0x06000164 RID: 356 RVA: 0x000083AC File Offset: 0x000065AC
		public StrongBox(T value)
		{
			this.Value = value;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000083BC File Offset: 0x000065BC
		// (set) Token: 0x06000166 RID: 358 RVA: 0x000083CC File Offset: 0x000065CC
		object IStrongBox.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (T)((object)value);
			}
		}

		// Token: 0x04000062 RID: 98
		public T Value;
	}
}
