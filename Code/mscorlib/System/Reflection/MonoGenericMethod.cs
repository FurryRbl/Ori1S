using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020002A2 RID: 674
	[Serializable]
	internal class MonoGenericMethod : MonoMethod
	{
		// Token: 0x06002264 RID: 8804 RVA: 0x0007C580 File Offset: 0x0007A780
		internal MonoGenericMethod()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06002265 RID: 8805
		public override extern Type ReflectedType { [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
