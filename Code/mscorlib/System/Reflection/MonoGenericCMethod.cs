using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020002A3 RID: 675
	[Serializable]
	internal class MonoGenericCMethod : MonoCMethod
	{
		// Token: 0x06002266 RID: 8806 RVA: 0x0007C590 File Offset: 0x0007A790
		internal MonoGenericCMethod()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06002267 RID: 8807
		public override extern Type ReflectedType { [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
