using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000041 RID: 65
	public class GUIElement : Behaviour
	{
		// Token: 0x0600033C RID: 828 RVA: 0x00003EAC File Offset: 0x000020AC
		public bool HitTest(Vector3 screenPosition, [DefaultValue("null")] Camera camera)
		{
			return GUIElement.INTERNAL_CALL_HitTest(this, ref screenPosition, camera);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00003EB8 File Offset: 0x000020B8
		[ExcludeFromDocs]
		public bool HitTest(Vector3 screenPosition)
		{
			Camera camera = null;
			return GUIElement.INTERNAL_CALL_HitTest(this, ref screenPosition, camera);
		}

		// Token: 0x0600033E RID: 830
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_HitTest(GUIElement self, ref Vector3 screenPosition, Camera camera);

		// Token: 0x0600033F RID: 831 RVA: 0x00003ED0 File Offset: 0x000020D0
		public Rect GetScreenRect([DefaultValue("null")] Camera camera)
		{
			Rect result;
			GUIElement.INTERNAL_CALL_GetScreenRect(this, camera, out result);
			return result;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00003EE8 File Offset: 0x000020E8
		[ExcludeFromDocs]
		public Rect GetScreenRect()
		{
			Camera camera = null;
			Rect result;
			GUIElement.INTERNAL_CALL_GetScreenRect(this, camera, out result);
			return result;
		}

		// Token: 0x06000341 RID: 833
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetScreenRect(GUIElement self, Camera camera, out Rect value);
	}
}
