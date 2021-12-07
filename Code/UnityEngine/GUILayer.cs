using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000043 RID: 67
	public sealed class GUILayer : Behaviour
	{
		// Token: 0x06000350 RID: 848 RVA: 0x00003F5C File Offset: 0x0000215C
		public GUIElement HitTest(Vector3 screenPosition)
		{
			return GUILayer.INTERNAL_CALL_HitTest(this, ref screenPosition);
		}

		// Token: 0x06000351 RID: 849
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GUIElement INTERNAL_CALL_HitTest(GUILayer self, ref Vector3 screenPosition);
	}
}
