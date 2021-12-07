using System;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Director;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001B1 RID: 433
	[UsedByNativeCode]
	public sealed class Animator : DirectorPlayer, IAnimatorControllerPlayable
	{
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001A00 RID: 6656
		public extern bool isOptimizable { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001A01 RID: 6657
		public extern bool isHuman { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001A02 RID: 6658
		public extern bool hasRootMotion { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001A03 RID: 6659
		internal extern bool isRootPositionOrRotationControlledByCurves { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001A04 RID: 6660
		public extern float humanScale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001A05 RID: 6661
		public extern bool isInitialized { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001A06 RID: 6662 RVA: 0x000191BC File Offset: 0x000173BC
		public float GetFloat(string name)
		{
			return this.GetFloatString(name);
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x000191C8 File Offset: 0x000173C8
		public float GetFloat(int id)
		{
			return this.GetFloatID(id);
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x000191D4 File Offset: 0x000173D4
		public void SetFloat(string name, float value)
		{
			this.SetFloatString(name, value);
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x000191E0 File Offset: 0x000173E0
		public void SetFloat(string name, float value, float dampTime, float deltaTime)
		{
			this.SetFloatStringDamp(name, value, dampTime, deltaTime);
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x000191F0 File Offset: 0x000173F0
		public void SetFloat(int id, float value)
		{
			this.SetFloatID(id, value);
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x000191FC File Offset: 0x000173FC
		public void SetFloat(int id, float value, float dampTime, float deltaTime)
		{
			this.SetFloatIDDamp(id, value, dampTime, deltaTime);
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x0001920C File Offset: 0x0001740C
		public bool GetBool(string name)
		{
			return this.GetBoolString(name);
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00019218 File Offset: 0x00017418
		public bool GetBool(int id)
		{
			return this.GetBoolID(id);
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x00019224 File Offset: 0x00017424
		public void SetBool(string name, bool value)
		{
			this.SetBoolString(name, value);
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x00019230 File Offset: 0x00017430
		public void SetBool(int id, bool value)
		{
			this.SetBoolID(id, value);
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x0001923C File Offset: 0x0001743C
		public int GetInteger(string name)
		{
			return this.GetIntegerString(name);
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x00019248 File Offset: 0x00017448
		public int GetInteger(int id)
		{
			return this.GetIntegerID(id);
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x00019254 File Offset: 0x00017454
		public void SetInteger(string name, int value)
		{
			this.SetIntegerString(name, value);
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x00019260 File Offset: 0x00017460
		public void SetInteger(int id, int value)
		{
			this.SetIntegerID(id, value);
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x0001926C File Offset: 0x0001746C
		public void SetTrigger(string name)
		{
			this.SetTriggerString(name);
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x00019278 File Offset: 0x00017478
		public void SetTrigger(int id)
		{
			this.SetTriggerID(id);
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00019284 File Offset: 0x00017484
		public void ResetTrigger(string name)
		{
			this.ResetTriggerString(name);
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x00019290 File Offset: 0x00017490
		public void ResetTrigger(int id)
		{
			this.ResetTriggerID(id);
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0001929C File Offset: 0x0001749C
		public bool IsParameterControlledByCurve(string name)
		{
			return this.IsParameterControlledByCurveString(name);
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x000192A8 File Offset: 0x000174A8
		public bool IsParameterControlledByCurve(int id)
		{
			return this.IsParameterControlledByCurveID(id);
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x000192B4 File Offset: 0x000174B4
		public Vector3 deltaPosition
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_deltaPosition(out result);
				return result;
			}
		}

		// Token: 0x06001A1B RID: 6683
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_deltaPosition(out Vector3 value);

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x000192CC File Offset: 0x000174CC
		public Quaternion deltaRotation
		{
			get
			{
				Quaternion result;
				this.INTERNAL_get_deltaRotation(out result);
				return result;
			}
		}

		// Token: 0x06001A1D RID: 6685
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_deltaRotation(out Quaternion value);

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001A1E RID: 6686 RVA: 0x000192E4 File Offset: 0x000174E4
		public Vector3 velocity
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_velocity(out result);
				return result;
			}
		}

		// Token: 0x06001A1F RID: 6687
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_velocity(out Vector3 value);

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001A20 RID: 6688 RVA: 0x000192FC File Offset: 0x000174FC
		public Vector3 angularVelocity
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_angularVelocity(out result);
				return result;
			}
		}

		// Token: 0x06001A21 RID: 6689
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_angularVelocity(out Vector3 value);

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x00019314 File Offset: 0x00017514
		// (set) Token: 0x06001A23 RID: 6691 RVA: 0x0001932C File Offset: 0x0001752C
		public Vector3 rootPosition
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_rootPosition(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_rootPosition(ref value);
			}
		}

		// Token: 0x06001A24 RID: 6692
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rootPosition(out Vector3 value);

		// Token: 0x06001A25 RID: 6693
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_rootPosition(ref Vector3 value);

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x00019338 File Offset: 0x00017538
		// (set) Token: 0x06001A27 RID: 6695 RVA: 0x00019350 File Offset: 0x00017550
		public Quaternion rootRotation
		{
			get
			{
				Quaternion result;
				this.INTERNAL_get_rootRotation(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_rootRotation(ref value);
			}
		}

		// Token: 0x06001A28 RID: 6696
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rootRotation(out Quaternion value);

		// Token: 0x06001A29 RID: 6697
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_rootRotation(ref Quaternion value);

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001A2A RID: 6698
		// (set) Token: 0x06001A2B RID: 6699
		public extern bool applyRootMotion { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001A2C RID: 6700
		// (set) Token: 0x06001A2D RID: 6701
		public extern bool linearVelocityBlending { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001A2E RID: 6702 RVA: 0x0001935C File Offset: 0x0001755C
		// (set) Token: 0x06001A2F RID: 6703 RVA: 0x00019368 File Offset: 0x00017568
		[Obsolete("Use Animator.updateMode instead")]
		public bool animatePhysics
		{
			get
			{
				return this.updateMode == AnimatorUpdateMode.AnimatePhysics;
			}
			set
			{
				this.updateMode = ((!value) ? AnimatorUpdateMode.Normal : AnimatorUpdateMode.AnimatePhysics);
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001A30 RID: 6704
		// (set) Token: 0x06001A31 RID: 6705
		public extern AnimatorUpdateMode updateMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001A32 RID: 6706
		public extern bool hasTransformHierarchy { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001A33 RID: 6707
		// (set) Token: 0x06001A34 RID: 6708
		internal extern bool allowConstantClipSamplingOptimization { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001A35 RID: 6709
		public extern float gravityWeight { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x00019380 File Offset: 0x00017580
		// (set) Token: 0x06001A37 RID: 6711 RVA: 0x00019398 File Offset: 0x00017598
		public Vector3 bodyPosition
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_bodyPosition(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_bodyPosition(ref value);
			}
		}

		// Token: 0x06001A38 RID: 6712
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_bodyPosition(out Vector3 value);

		// Token: 0x06001A39 RID: 6713
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_bodyPosition(ref Vector3 value);

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x000193A4 File Offset: 0x000175A4
		// (set) Token: 0x06001A3B RID: 6715 RVA: 0x000193BC File Offset: 0x000175BC
		public Quaternion bodyRotation
		{
			get
			{
				Quaternion result;
				this.INTERNAL_get_bodyRotation(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_bodyRotation(ref value);
			}
		}

		// Token: 0x06001A3C RID: 6716
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_bodyRotation(out Quaternion value);

		// Token: 0x06001A3D RID: 6717
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_bodyRotation(ref Quaternion value);

		// Token: 0x06001A3E RID: 6718 RVA: 0x000193C8 File Offset: 0x000175C8
		public Vector3 GetIKPosition(AvatarIKGoal goal)
		{
			this.CheckIfInIKPass();
			return this.GetIKPositionInternal(goal);
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x000193D8 File Offset: 0x000175D8
		internal Vector3 GetIKPositionInternal(AvatarIKGoal goal)
		{
			Vector3 result;
			Animator.INTERNAL_CALL_GetIKPositionInternal(this, goal, out result);
			return result;
		}

		// Token: 0x06001A40 RID: 6720
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetIKPositionInternal(Animator self, AvatarIKGoal goal, out Vector3 value);

		// Token: 0x06001A41 RID: 6721 RVA: 0x000193F0 File Offset: 0x000175F0
		public void SetIKPosition(AvatarIKGoal goal, Vector3 goalPosition)
		{
			this.CheckIfInIKPass();
			this.SetIKPositionInternal(goal, goalPosition);
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x00019400 File Offset: 0x00017600
		internal void SetIKPositionInternal(AvatarIKGoal goal, Vector3 goalPosition)
		{
			Animator.INTERNAL_CALL_SetIKPositionInternal(this, goal, ref goalPosition);
		}

		// Token: 0x06001A43 RID: 6723
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetIKPositionInternal(Animator self, AvatarIKGoal goal, ref Vector3 goalPosition);

		// Token: 0x06001A44 RID: 6724 RVA: 0x0001940C File Offset: 0x0001760C
		public Quaternion GetIKRotation(AvatarIKGoal goal)
		{
			this.CheckIfInIKPass();
			return this.GetIKRotationInternal(goal);
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x0001941C File Offset: 0x0001761C
		internal Quaternion GetIKRotationInternal(AvatarIKGoal goal)
		{
			Quaternion result;
			Animator.INTERNAL_CALL_GetIKRotationInternal(this, goal, out result);
			return result;
		}

		// Token: 0x06001A46 RID: 6726
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetIKRotationInternal(Animator self, AvatarIKGoal goal, out Quaternion value);

		// Token: 0x06001A47 RID: 6727 RVA: 0x00019434 File Offset: 0x00017634
		public void SetIKRotation(AvatarIKGoal goal, Quaternion goalRotation)
		{
			this.CheckIfInIKPass();
			this.SetIKRotationInternal(goal, goalRotation);
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x00019444 File Offset: 0x00017644
		internal void SetIKRotationInternal(AvatarIKGoal goal, Quaternion goalRotation)
		{
			Animator.INTERNAL_CALL_SetIKRotationInternal(this, goal, ref goalRotation);
		}

		// Token: 0x06001A49 RID: 6729
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetIKRotationInternal(Animator self, AvatarIKGoal goal, ref Quaternion goalRotation);

		// Token: 0x06001A4A RID: 6730 RVA: 0x00019450 File Offset: 0x00017650
		public float GetIKPositionWeight(AvatarIKGoal goal)
		{
			this.CheckIfInIKPass();
			return this.GetIKPositionWeightInternal(goal);
		}

		// Token: 0x06001A4B RID: 6731
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern float GetIKPositionWeightInternal(AvatarIKGoal goal);

		// Token: 0x06001A4C RID: 6732 RVA: 0x00019460 File Offset: 0x00017660
		public void SetIKPositionWeight(AvatarIKGoal goal, float value)
		{
			this.CheckIfInIKPass();
			this.SetIKPositionWeightInternal(goal, value);
		}

		// Token: 0x06001A4D RID: 6733
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetIKPositionWeightInternal(AvatarIKGoal goal, float value);

		// Token: 0x06001A4E RID: 6734 RVA: 0x00019470 File Offset: 0x00017670
		public float GetIKRotationWeight(AvatarIKGoal goal)
		{
			this.CheckIfInIKPass();
			return this.GetIKRotationWeightInternal(goal);
		}

		// Token: 0x06001A4F RID: 6735
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern float GetIKRotationWeightInternal(AvatarIKGoal goal);

		// Token: 0x06001A50 RID: 6736 RVA: 0x00019480 File Offset: 0x00017680
		public void SetIKRotationWeight(AvatarIKGoal goal, float value)
		{
			this.CheckIfInIKPass();
			this.SetIKRotationWeightInternal(goal, value);
		}

		// Token: 0x06001A51 RID: 6737
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetIKRotationWeightInternal(AvatarIKGoal goal, float value);

		// Token: 0x06001A52 RID: 6738 RVA: 0x00019490 File Offset: 0x00017690
		public Vector3 GetIKHintPosition(AvatarIKHint hint)
		{
			this.CheckIfInIKPass();
			return this.GetIKHintPositionInternal(hint);
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x000194A0 File Offset: 0x000176A0
		internal Vector3 GetIKHintPositionInternal(AvatarIKHint hint)
		{
			Vector3 result;
			Animator.INTERNAL_CALL_GetIKHintPositionInternal(this, hint, out result);
			return result;
		}

		// Token: 0x06001A54 RID: 6740
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetIKHintPositionInternal(Animator self, AvatarIKHint hint, out Vector3 value);

		// Token: 0x06001A55 RID: 6741 RVA: 0x000194B8 File Offset: 0x000176B8
		public void SetIKHintPosition(AvatarIKHint hint, Vector3 hintPosition)
		{
			this.CheckIfInIKPass();
			this.SetIKHintPositionInternal(hint, hintPosition);
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x000194C8 File Offset: 0x000176C8
		internal void SetIKHintPositionInternal(AvatarIKHint hint, Vector3 hintPosition)
		{
			Animator.INTERNAL_CALL_SetIKHintPositionInternal(this, hint, ref hintPosition);
		}

		// Token: 0x06001A57 RID: 6743
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetIKHintPositionInternal(Animator self, AvatarIKHint hint, ref Vector3 hintPosition);

		// Token: 0x06001A58 RID: 6744 RVA: 0x000194D4 File Offset: 0x000176D4
		public float GetIKHintPositionWeight(AvatarIKHint hint)
		{
			this.CheckIfInIKPass();
			return this.GetHintWeightPositionInternal(hint);
		}

		// Token: 0x06001A59 RID: 6745
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern float GetHintWeightPositionInternal(AvatarIKHint hint);

		// Token: 0x06001A5A RID: 6746 RVA: 0x000194E4 File Offset: 0x000176E4
		public void SetIKHintPositionWeight(AvatarIKHint hint, float value)
		{
			this.CheckIfInIKPass();
			this.SetIKHintPositionWeightInternal(hint, value);
		}

		// Token: 0x06001A5B RID: 6747
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetIKHintPositionWeightInternal(AvatarIKHint hint, float value);

		// Token: 0x06001A5C RID: 6748 RVA: 0x000194F4 File Offset: 0x000176F4
		public void SetLookAtPosition(Vector3 lookAtPosition)
		{
			this.CheckIfInIKPass();
			this.SetLookAtPositionInternal(lookAtPosition);
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00019504 File Offset: 0x00017704
		internal void SetLookAtPositionInternal(Vector3 lookAtPosition)
		{
			Animator.INTERNAL_CALL_SetLookAtPositionInternal(this, ref lookAtPosition);
		}

		// Token: 0x06001A5E RID: 6750
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetLookAtPositionInternal(Animator self, ref Vector3 lookAtPosition);

		// Token: 0x06001A5F RID: 6751 RVA: 0x00019510 File Offset: 0x00017710
		[ExcludeFromDocs]
		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight, float eyesWeight)
		{
			float clampWeight = 0.5f;
			this.SetLookAtWeight(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00019530 File Offset: 0x00017730
		[ExcludeFromDocs]
		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight)
		{
			float clampWeight = 0.5f;
			float eyesWeight = 0f;
			this.SetLookAtWeight(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00019554 File Offset: 0x00017754
		[ExcludeFromDocs]
		public void SetLookAtWeight(float weight, float bodyWeight)
		{
			float clampWeight = 0.5f;
			float eyesWeight = 0f;
			float headWeight = 1f;
			this.SetLookAtWeight(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00019580 File Offset: 0x00017780
		[ExcludeFromDocs]
		public void SetLookAtWeight(float weight)
		{
			float clampWeight = 0.5f;
			float eyesWeight = 0f;
			float headWeight = 1f;
			float bodyWeight = 0f;
			this.SetLookAtWeight(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x000195B0 File Offset: 0x000177B0
		public void SetLookAtWeight(float weight, [DefaultValue("0.00f")] float bodyWeight, [DefaultValue("1.00f")] float headWeight, [DefaultValue("0.00f")] float eyesWeight, [DefaultValue("0.50f")] float clampWeight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
		}

		// Token: 0x06001A64 RID: 6756
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetLookAtWeightInternal(float weight, [DefaultValue("0.00f")] float bodyWeight, [DefaultValue("1.00f")] float headWeight, [DefaultValue("0.00f")] float eyesWeight, [DefaultValue("0.50f")] float clampWeight);

		// Token: 0x06001A65 RID: 6757 RVA: 0x000195D0 File Offset: 0x000177D0
		[ExcludeFromDocs]
		internal void SetLookAtWeightInternal(float weight, float bodyWeight, float headWeight, float eyesWeight)
		{
			float clampWeight = 0.5f;
			this.SetLookAtWeightInternal(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x000195F0 File Offset: 0x000177F0
		[ExcludeFromDocs]
		internal void SetLookAtWeightInternal(float weight, float bodyWeight, float headWeight)
		{
			float clampWeight = 0.5f;
			float eyesWeight = 0f;
			this.SetLookAtWeightInternal(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00019614 File Offset: 0x00017814
		[ExcludeFromDocs]
		internal void SetLookAtWeightInternal(float weight, float bodyWeight)
		{
			float clampWeight = 0.5f;
			float eyesWeight = 0f;
			float headWeight = 1f;
			this.SetLookAtWeightInternal(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00019640 File Offset: 0x00017840
		[ExcludeFromDocs]
		internal void SetLookAtWeightInternal(float weight)
		{
			float clampWeight = 0.5f;
			float eyesWeight = 0f;
			float headWeight = 1f;
			float bodyWeight = 0f;
			this.SetLookAtWeightInternal(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00019670 File Offset: 0x00017870
		public void SetBoneLocalRotation(HumanBodyBones humanBoneId, Quaternion rotation)
		{
			this.CheckIfInIKPass();
			this.SetBoneLocalRotationInternal(humanBoneId, rotation);
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00019680 File Offset: 0x00017880
		internal void SetBoneLocalRotationInternal(HumanBodyBones humanBoneId, Quaternion rotation)
		{
			Animator.INTERNAL_CALL_SetBoneLocalRotationInternal(this, humanBoneId, ref rotation);
		}

		// Token: 0x06001A6B RID: 6763
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetBoneLocalRotationInternal(Animator self, HumanBodyBones humanBoneId, ref Quaternion rotation);

		// Token: 0x06001A6C RID: 6764
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern ScriptableObject GetBehaviour(Type type);

		// Token: 0x06001A6D RID: 6765 RVA: 0x0001968C File Offset: 0x0001788C
		public T GetBehaviour<T>() where T : StateMachineBehaviour
		{
			return this.GetBehaviour(typeof(T)) as T;
		}

		// Token: 0x06001A6E RID: 6766
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern ScriptableObject[] GetBehaviours(Type type);

		// Token: 0x06001A6F RID: 6767 RVA: 0x000196A8 File Offset: 0x000178A8
		internal static T[] ConvertStateMachineBehaviour<T>(ScriptableObject[] rawObjects) where T : StateMachineBehaviour
		{
			if (rawObjects == null)
			{
				return null;
			}
			T[] array = new T[rawObjects.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (T)((object)rawObjects[i]);
			}
			return array;
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x000196EC File Offset: 0x000178EC
		public T[] GetBehaviours<T>() where T : StateMachineBehaviour
		{
			return Animator.ConvertStateMachineBehaviour<T>(this.GetBehaviours(typeof(T)));
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001A71 RID: 6769
		// (set) Token: 0x06001A72 RID: 6770
		public extern bool stabilizeFeet { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001A73 RID: 6771
		public extern int layerCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001A74 RID: 6772
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetLayerName(int layerIndex);

		// Token: 0x06001A75 RID: 6773
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetLayerIndex(string layerName);

		// Token: 0x06001A76 RID: 6774
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetLayerWeight(int layerIndex);

		// Token: 0x06001A77 RID: 6775
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetLayerWeight(int layerIndex, float weight);

		// Token: 0x06001A78 RID: 6776
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex);

		// Token: 0x06001A79 RID: 6777
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimatorStateInfo GetNextAnimatorStateInfo(int layerIndex);

		// Token: 0x06001A7A RID: 6778
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimatorTransitionInfo GetAnimatorTransitionInfo(int layerIndex);

		// Token: 0x06001A7B RID: 6779
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimatorClipInfo[] GetCurrentAnimatorClipInfo(int layerIndex);

		// Token: 0x06001A7C RID: 6780
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimatorClipInfo[] GetNextAnimatorClipInfo(int layerIndex);

		// Token: 0x06001A7D RID: 6781
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsInTransition(int layerIndex);

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001A7E RID: 6782
		public extern AnimatorControllerParameter[] parameters { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001A7F RID: 6783
		public extern int parameterCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001A80 RID: 6784 RVA: 0x00019704 File Offset: 0x00017904
		public AnimatorControllerParameter GetParameter(int index)
		{
			AnimatorControllerParameter[] parameters = this.parameters;
			if (index < 0 && index >= this.parameters.Length)
			{
				throw new IndexOutOfRangeException("index");
			}
			return parameters[index];
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001A81 RID: 6785
		// (set) Token: 0x06001A82 RID: 6786
		public extern float feetPivotActive { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001A83 RID: 6787
		public extern float pivotWeight { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x0001973C File Offset: 0x0001793C
		public Vector3 pivotPosition
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_pivotPosition(out result);
				return result;
			}
		}

		// Token: 0x06001A85 RID: 6789
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_pivotPosition(out Vector3 value);

		// Token: 0x06001A86 RID: 6790 RVA: 0x00019754 File Offset: 0x00017954
		public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime, [DefaultValue("1")] float targetNormalizedTime)
		{
			Animator.INTERNAL_CALL_MatchTarget(this, ref matchPosition, ref matchRotation, targetBodyPart, ref weightMask, startNormalizedTime, targetNormalizedTime);
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x00019768 File Offset: 0x00017968
		[ExcludeFromDocs]
		public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime)
		{
			float targetNormalizedTime = 1f;
			Animator.INTERNAL_CALL_MatchTarget(this, ref matchPosition, ref matchRotation, targetBodyPart, ref weightMask, startNormalizedTime, targetNormalizedTime);
		}

		// Token: 0x06001A88 RID: 6792
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_MatchTarget(Animator self, ref Vector3 matchPosition, ref Quaternion matchRotation, AvatarTarget targetBodyPart, ref MatchTargetWeightMask weightMask, float startNormalizedTime, float targetNormalizedTime);

		// Token: 0x06001A89 RID: 6793
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InterruptMatchTarget([DefaultValue("true")] bool completeMatch);

		// Token: 0x06001A8A RID: 6794 RVA: 0x0001978C File Offset: 0x0001798C
		[ExcludeFromDocs]
		public void InterruptMatchTarget()
		{
			bool completeMatch = true;
			this.InterruptMatchTarget(completeMatch);
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001A8B RID: 6795
		public extern bool isMatchingTarget { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001A8C RID: 6796
		// (set) Token: 0x06001A8D RID: 6797
		public extern float speed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001A8E RID: 6798 RVA: 0x000197A4 File Offset: 0x000179A4
		[Obsolete("ForceStateNormalizedTime is deprecated. Please use Play or CrossFade instead.")]
		public void ForceStateNormalizedTime(float normalizedTime)
		{
			this.Play(0, 0, normalizedTime);
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x000197B0 File Offset: 0x000179B0
		[ExcludeFromDocs]
		public void CrossFadeInFixedTime(string stateName, float transitionDuration, int layer)
		{
			float fixedTime = 0f;
			this.CrossFadeInFixedTime(stateName, transitionDuration, layer, fixedTime);
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x000197D0 File Offset: 0x000179D0
		[ExcludeFromDocs]
		public void CrossFadeInFixedTime(string stateName, float transitionDuration)
		{
			float fixedTime = 0f;
			int layer = -1;
			this.CrossFadeInFixedTime(stateName, transitionDuration, layer, fixedTime);
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x000197F0 File Offset: 0x000179F0
		public void CrossFadeInFixedTime(string stateName, float transitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("0.0f")] float fixedTime)
		{
			this.CrossFadeInFixedTime(Animator.StringToHash(stateName), transitionDuration, layer, fixedTime);
		}

		// Token: 0x06001A92 RID: 6802
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CrossFadeInFixedTime(int stateNameHash, float transitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("0.0f")] float fixedTime);

		// Token: 0x06001A93 RID: 6803 RVA: 0x00019804 File Offset: 0x00017A04
		[ExcludeFromDocs]
		public void CrossFadeInFixedTime(int stateNameHash, float transitionDuration, int layer)
		{
			float fixedTime = 0f;
			this.CrossFadeInFixedTime(stateNameHash, transitionDuration, layer, fixedTime);
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x00019824 File Offset: 0x00017A24
		[ExcludeFromDocs]
		public void CrossFadeInFixedTime(int stateNameHash, float transitionDuration)
		{
			float fixedTime = 0f;
			int layer = -1;
			this.CrossFadeInFixedTime(stateNameHash, transitionDuration, layer, fixedTime);
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x00019844 File Offset: 0x00017A44
		[ExcludeFromDocs]
		public void CrossFade(string stateName, float transitionDuration, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.CrossFade(stateName, transitionDuration, layer, negativeInfinity);
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x00019864 File Offset: 0x00017A64
		[ExcludeFromDocs]
		public void CrossFade(string stateName, float transitionDuration)
		{
			float negativeInfinity = float.NegativeInfinity;
			int layer = -1;
			this.CrossFade(stateName, transitionDuration, layer, negativeInfinity);
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00019884 File Offset: 0x00017A84
		public void CrossFade(string stateName, float transitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			this.CrossFade(Animator.StringToHash(stateName), transitionDuration, layer, normalizedTime);
		}

		// Token: 0x06001A98 RID: 6808
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CrossFade(int stateNameHash, float transitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime);

		// Token: 0x06001A99 RID: 6809 RVA: 0x00019898 File Offset: 0x00017A98
		[ExcludeFromDocs]
		public void CrossFade(int stateNameHash, float transitionDuration, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.CrossFade(stateNameHash, transitionDuration, layer, negativeInfinity);
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x000198B8 File Offset: 0x00017AB8
		[ExcludeFromDocs]
		public void CrossFade(int stateNameHash, float transitionDuration)
		{
			float negativeInfinity = float.NegativeInfinity;
			int layer = -1;
			this.CrossFade(stateNameHash, transitionDuration, layer, negativeInfinity);
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x000198D8 File Offset: 0x00017AD8
		[ExcludeFromDocs]
		public void PlayInFixedTime(string stateName, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.PlayInFixedTime(stateName, layer, negativeInfinity);
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x000198F4 File Offset: 0x00017AF4
		[ExcludeFromDocs]
		public void PlayInFixedTime(string stateName)
		{
			float negativeInfinity = float.NegativeInfinity;
			int layer = -1;
			this.PlayInFixedTime(stateName, layer, negativeInfinity);
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x00019914 File Offset: 0x00017B14
		public void PlayInFixedTime(string stateName, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float fixedTime)
		{
			this.PlayInFixedTime(Animator.StringToHash(stateName), layer, fixedTime);
		}

		// Token: 0x06001A9E RID: 6814
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void PlayInFixedTime(int stateNameHash, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float fixedTime);

		// Token: 0x06001A9F RID: 6815 RVA: 0x00019924 File Offset: 0x00017B24
		[ExcludeFromDocs]
		public void PlayInFixedTime(int stateNameHash, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.PlayInFixedTime(stateNameHash, layer, negativeInfinity);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00019940 File Offset: 0x00017B40
		[ExcludeFromDocs]
		public void PlayInFixedTime(int stateNameHash)
		{
			float negativeInfinity = float.NegativeInfinity;
			int layer = -1;
			this.PlayInFixedTime(stateNameHash, layer, negativeInfinity);
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x00019960 File Offset: 0x00017B60
		[ExcludeFromDocs]
		public void Play(string stateName, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.Play(stateName, layer, negativeInfinity);
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x0001997C File Offset: 0x00017B7C
		[ExcludeFromDocs]
		public void Play(string stateName)
		{
			float negativeInfinity = float.NegativeInfinity;
			int layer = -1;
			this.Play(stateName, layer, negativeInfinity);
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x0001999C File Offset: 0x00017B9C
		public void Play(string stateName, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			this.Play(Animator.StringToHash(stateName), layer, normalizedTime);
		}

		// Token: 0x06001AA4 RID: 6820
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Play(int stateNameHash, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime);

		// Token: 0x06001AA5 RID: 6821 RVA: 0x000199AC File Offset: 0x00017BAC
		[ExcludeFromDocs]
		public void Play(int stateNameHash, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.Play(stateNameHash, layer, negativeInfinity);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x000199C8 File Offset: 0x00017BC8
		[ExcludeFromDocs]
		public void Play(int stateNameHash)
		{
			float negativeInfinity = float.NegativeInfinity;
			int layer = -1;
			this.Play(stateNameHash, layer, negativeInfinity);
		}

		// Token: 0x06001AA7 RID: 6823
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTarget(AvatarTarget targetIndex, float targetNormalizedTime);

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x000199E8 File Offset: 0x00017BE8
		public Vector3 targetPosition
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_targetPosition(out result);
				return result;
			}
		}

		// Token: 0x06001AA9 RID: 6825
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_targetPosition(out Vector3 value);

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x00019A00 File Offset: 0x00017C00
		public Quaternion targetRotation
		{
			get
			{
				Quaternion result;
				this.INTERNAL_get_targetRotation(out result);
				return result;
			}
		}

		// Token: 0x06001AAB RID: 6827
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_targetRotation(out Quaternion value);

		// Token: 0x06001AAC RID: 6828
		[Obsolete("use mask and layers to control subset of transfroms in a skeleton", true)]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsControlled(Transform transform);

		// Token: 0x06001AAD RID: 6829
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsBoneTransform(Transform transform);

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001AAE RID: 6830
		internal extern Transform avatarRoot { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001AAF RID: 6831
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Transform GetBoneTransform(HumanBodyBones humanBoneId);

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001AB0 RID: 6832
		// (set) Token: 0x06001AB1 RID: 6833
		public extern AnimatorCullingMode cullingMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001AB2 RID: 6834
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StartPlayback();

		// Token: 0x06001AB3 RID: 6835
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopPlayback();

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001AB4 RID: 6836
		// (set) Token: 0x06001AB5 RID: 6837
		public extern float playbackTime { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001AB6 RID: 6838
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StartRecording(int frameCount);

		// Token: 0x06001AB7 RID: 6839
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopRecording();

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001AB8 RID: 6840
		// (set) Token: 0x06001AB9 RID: 6841
		public extern float recorderStartTime { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001ABA RID: 6842
		// (set) Token: 0x06001ABB RID: 6843
		public extern float recorderStopTime { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001ABC RID: 6844
		public extern AnimatorRecorderMode recorderMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001ABD RID: 6845
		// (set) Token: 0x06001ABE RID: 6846
		public extern RuntimeAnimatorController runtimeAnimatorController { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001ABF RID: 6847
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasState(int layerIndex, int stateID);

		// Token: 0x06001AC0 RID: 6848
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int StringToHash(string name);

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001AC1 RID: 6849
		// (set) Token: 0x06001AC2 RID: 6850
		public extern Avatar avatar { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001AC3 RID: 6851
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string GetStats();

		// Token: 0x06001AC4 RID: 6852 RVA: 0x00019A18 File Offset: 0x00017C18
		private void CheckIfInIKPass()
		{
			if (this.logWarnings && !this.CheckIfInIKPassInternal())
			{
				Debug.LogWarning("Setting and getting IK Goals, Lookat and BoneLocalRotation should only be done in OnAnimatorIK or OnStateIK");
			}
		}

		// Token: 0x06001AC5 RID: 6853
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool CheckIfInIKPassInternal();

		// Token: 0x06001AC6 RID: 6854
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatString(string name, float value);

		// Token: 0x06001AC7 RID: 6855
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatID(int id, float value);

		// Token: 0x06001AC8 RID: 6856
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetFloatString(string name);

		// Token: 0x06001AC9 RID: 6857
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetFloatID(int id);

		// Token: 0x06001ACA RID: 6858
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBoolString(string name, bool value);

		// Token: 0x06001ACB RID: 6859
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBoolID(int id, bool value);

		// Token: 0x06001ACC RID: 6860
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetBoolString(string name);

		// Token: 0x06001ACD RID: 6861
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetBoolID(int id);

		// Token: 0x06001ACE RID: 6862
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIntegerString(string name, int value);

		// Token: 0x06001ACF RID: 6863
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIntegerID(int id, int value);

		// Token: 0x06001AD0 RID: 6864
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetIntegerString(string name);

		// Token: 0x06001AD1 RID: 6865
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetIntegerID(int id);

		// Token: 0x06001AD2 RID: 6866
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTriggerString(string name);

		// Token: 0x06001AD3 RID: 6867
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTriggerID(int id);

		// Token: 0x06001AD4 RID: 6868
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResetTriggerString(string name);

		// Token: 0x06001AD5 RID: 6869
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResetTriggerID(int id);

		// Token: 0x06001AD6 RID: 6870
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsParameterControlledByCurveString(string name);

		// Token: 0x06001AD7 RID: 6871
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsParameterControlledByCurveID(int id);

		// Token: 0x06001AD8 RID: 6872
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatStringDamp(string name, float value, float dampTime, float deltaTime);

		// Token: 0x06001AD9 RID: 6873
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatIDDamp(int id, float value, float dampTime, float deltaTime);

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001ADA RID: 6874
		// (set) Token: 0x06001ADB RID: 6875
		public extern bool layersAffectMassCenter { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001ADC RID: 6876
		public extern float leftFeetBottomHeight { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001ADD RID: 6877
		public extern float rightFeetBottomHeight { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001ADE RID: 6878
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Update(float deltaTime);

		// Token: 0x06001ADF RID: 6879
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Rebind();

		// Token: 0x06001AE0 RID: 6880
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ApplyBuiltinRootMotion();

		// Token: 0x06001AE1 RID: 6881
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string ResolveHash(int hash);

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001AE2 RID: 6882
		// (set) Token: 0x06001AE3 RID: 6883
		public extern bool logWarnings { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001AE4 RID: 6884
		// (set) Token: 0x06001AE5 RID: 6885
		public extern bool fireEvents { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001AE6 RID: 6886 RVA: 0x00019A48 File Offset: 0x00017C48
		[Obsolete("GetVector is deprecated.")]
		public Vector3 GetVector(string name)
		{
			return Vector3.zero;
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00019A50 File Offset: 0x00017C50
		[Obsolete("GetVector is deprecated.")]
		public Vector3 GetVector(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x00019A58 File Offset: 0x00017C58
		[Obsolete("SetVector is deprecated.")]
		public void SetVector(string name, Vector3 value)
		{
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00019A5C File Offset: 0x00017C5C
		[Obsolete("SetVector is deprecated.")]
		public void SetVector(int id, Vector3 value)
		{
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x00019A60 File Offset: 0x00017C60
		[Obsolete("GetQuaternion is deprecated.")]
		public Quaternion GetQuaternion(string name)
		{
			return Quaternion.identity;
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x00019A68 File Offset: 0x00017C68
		[Obsolete("GetQuaternion is deprecated.")]
		public Quaternion GetQuaternion(int id)
		{
			return Quaternion.identity;
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x00019A70 File Offset: 0x00017C70
		[Obsolete("SetQuaternion is deprecated.")]
		public void SetQuaternion(string name, Quaternion value)
		{
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00019A74 File Offset: 0x00017C74
		[Obsolete("SetQuaternion is deprecated.")]
		public void SetQuaternion(int id, Quaternion value)
		{
		}
	}
}
