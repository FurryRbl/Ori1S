using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001B8 RID: 440
	public sealed class AvatarBuilder
	{
		// Token: 0x06001B1D RID: 6941 RVA: 0x00019CC8 File Offset: 0x00017EC8
		public static Avatar BuildHumanAvatar(GameObject go, HumanDescription monoHumanDescription)
		{
			if (go == null)
			{
				throw new NullReferenceException();
			}
			return AvatarBuilder.BuildHumanAvatarMono(go, monoHumanDescription);
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x00019CE4 File Offset: 0x00017EE4
		private static Avatar BuildHumanAvatarMono(GameObject go, HumanDescription monoHumanDescription)
		{
			return AvatarBuilder.INTERNAL_CALL_BuildHumanAvatarMono(go, ref monoHumanDescription);
		}

		// Token: 0x06001B1F RID: 6943
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Avatar INTERNAL_CALL_BuildHumanAvatarMono(GameObject go, ref HumanDescription monoHumanDescription);

		// Token: 0x06001B20 RID: 6944
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Avatar BuildGenericAvatar(GameObject go, string rootMotionTransformName);
	}
}
