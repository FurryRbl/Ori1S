using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001C6 RID: 454
	public sealed class HumanPoseHandler : IDisposable
	{
		// Token: 0x06001B41 RID: 6977 RVA: 0x00019E3C File Offset: 0x0001803C
		public HumanPoseHandler(Avatar avatar, Transform root)
		{
			this.m_Ptr = IntPtr.Zero;
			if (root == null)
			{
				throw new ArgumentNullException("HumanPoseHandler root Transform is null");
			}
			if (avatar == null)
			{
				throw new ArgumentNullException("HumanPoseHandler avatar is null");
			}
			if (!avatar.isValid)
			{
				throw new ArgumentException("HumanPoseHandler avatar is invalid");
			}
			if (!avatar.isHuman)
			{
				throw new ArgumentException("HumanPoseHandler avatar is not human");
			}
			this.Internal_HumanPoseHandler(avatar, root);
		}

		// Token: 0x06001B42 RID: 6978
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Dispose();

		// Token: 0x06001B43 RID: 6979
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_HumanPoseHandler(Avatar avatar, Transform root);

		// Token: 0x06001B44 RID: 6980 RVA: 0x00019EBC File Offset: 0x000180BC
		private bool Internal_GetHumanPose(ref Vector3 bodyPosition, ref Quaternion bodyRotation, float[] muscles)
		{
			return HumanPoseHandler.INTERNAL_CALL_Internal_GetHumanPose(this, ref bodyPosition, ref bodyRotation, muscles);
		}

		// Token: 0x06001B45 RID: 6981
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Internal_GetHumanPose(HumanPoseHandler self, ref Vector3 bodyPosition, ref Quaternion bodyRotation, float[] muscles);

		// Token: 0x06001B46 RID: 6982 RVA: 0x00019EC8 File Offset: 0x000180C8
		public void GetHumanPose(ref HumanPose humanPose)
		{
			humanPose.Init();
			if (!this.Internal_GetHumanPose(ref humanPose.bodyPosition, ref humanPose.bodyRotation, humanPose.muscles))
			{
				Debug.LogWarning("HumanPoseHandler is not initialized properly");
			}
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x00019EF8 File Offset: 0x000180F8
		private bool Internal_SetHumanPose(ref Vector3 bodyPosition, ref Quaternion bodyRotation, float[] muscles)
		{
			return HumanPoseHandler.INTERNAL_CALL_Internal_SetHumanPose(this, ref bodyPosition, ref bodyRotation, muscles);
		}

		// Token: 0x06001B48 RID: 6984
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Internal_SetHumanPose(HumanPoseHandler self, ref Vector3 bodyPosition, ref Quaternion bodyRotation, float[] muscles);

		// Token: 0x06001B49 RID: 6985 RVA: 0x00019F04 File Offset: 0x00018104
		public void SetHumanPose(ref HumanPose humanPose)
		{
			humanPose.Init();
			if (!this.Internal_SetHumanPose(ref humanPose.bodyPosition, ref humanPose.bodyRotation, humanPose.muscles))
			{
				Debug.LogWarning("HumanPoseHandler is not initialized properly");
			}
		}

		// Token: 0x0400058A RID: 1418
		internal IntPtr m_Ptr;
	}
}
