using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000086 RID: 134
	public class TextAsset : Object
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000825 RID: 2085
		public extern string text { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000826 RID: 2086
		public extern byte[] bytes { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000827 RID: 2087 RVA: 0x0000B734 File Offset: 0x00009934
		public override string ToString()
		{
			return this.text;
		}
	}
}
