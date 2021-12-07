using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Director
{
	// Token: 0x020001CB RID: 459
	[UsedByNativeCode]
	public sealed class AnimatorControllerPlayable : AnimationPlayable, IAnimatorControllerPlayable
	{
		// Token: 0x06001B8A RID: 7050 RVA: 0x0001A268 File Offset: 0x00018468
		public AnimatorControllerPlayable(RuntimeAnimatorController controller) : base(false)
		{
			this.m_Ptr = IntPtr.Zero;
			this.InstantiateEnginePlayable(controller);
		}

		// Token: 0x06001B8B RID: 7051
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InstantiateEnginePlayable(RuntimeAnimatorController controller);

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001B8C RID: 7052
		public extern RuntimeAnimatorController animatorController { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001B8D RID: 7053 RVA: 0x0001A284 File Offset: 0x00018484
		public override int AddInput(AnimationPlayable source)
		{
			Debug.LogError("AnimationClipPlayable doesn't support adding inputs");
			return -1;
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x0001A294 File Offset: 0x00018494
		public override bool SetInput(AnimationPlayable source, int index)
		{
			Debug.LogError("AnimationClipPlayable doesn't support setting inputs");
			return false;
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x0001A2A4 File Offset: 0x000184A4
		public override bool SetInputs(IEnumerable<AnimationPlayable> sources)
		{
			Debug.LogError("AnimationClipPlayable doesn't support setting inputs");
			return false;
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x0001A2B4 File Offset: 0x000184B4
		public override bool RemoveInput(int index)
		{
			Debug.LogError("AnimationClipPlayable doesn't support removing inputs");
			return false;
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x0001A2C4 File Offset: 0x000184C4
		public override bool RemoveInput(AnimationPlayable playable)
		{
			Debug.LogError("AnimationClipPlayable doesn't support removing inputs");
			return false;
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x0001A2D4 File Offset: 0x000184D4
		public override bool RemoveAllInputs()
		{
			Debug.LogError("AnimationClipPlayable doesn't support removing inputs");
			return false;
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x0001A2E4 File Offset: 0x000184E4
		public float GetFloat(string name)
		{
			return this.GetFloatString(name);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x0001A2F0 File Offset: 0x000184F0
		public float GetFloat(int id)
		{
			return this.GetFloatID(id);
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x0001A2FC File Offset: 0x000184FC
		public void SetFloat(string name, float value)
		{
			this.SetFloatString(name, value);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x0001A308 File Offset: 0x00018508
		public void SetFloat(int id, float value)
		{
			this.SetFloatID(id, value);
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x0001A314 File Offset: 0x00018514
		public bool GetBool(string name)
		{
			return this.GetBoolString(name);
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x0001A320 File Offset: 0x00018520
		public bool GetBool(int id)
		{
			return this.GetBoolID(id);
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x0001A32C File Offset: 0x0001852C
		public void SetBool(string name, bool value)
		{
			this.SetBoolString(name, value);
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x0001A338 File Offset: 0x00018538
		public void SetBool(int id, bool value)
		{
			this.SetBoolID(id, value);
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x0001A344 File Offset: 0x00018544
		public int GetInteger(string name)
		{
			return this.GetIntegerString(name);
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x0001A350 File Offset: 0x00018550
		public int GetInteger(int id)
		{
			return this.GetIntegerID(id);
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x0001A35C File Offset: 0x0001855C
		public void SetInteger(string name, int value)
		{
			this.SetIntegerString(name, value);
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x0001A368 File Offset: 0x00018568
		public void SetInteger(int id, int value)
		{
			this.SetIntegerID(id, value);
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x0001A374 File Offset: 0x00018574
		public void SetTrigger(string name)
		{
			this.SetTriggerString(name);
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0001A380 File Offset: 0x00018580
		public void SetTrigger(int id)
		{
			this.SetTriggerID(id);
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x0001A38C File Offset: 0x0001858C
		public void ResetTrigger(string name)
		{
			this.ResetTriggerString(name);
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0001A398 File Offset: 0x00018598
		public void ResetTrigger(int id)
		{
			this.ResetTriggerID(id);
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x0001A3A4 File Offset: 0x000185A4
		public bool IsParameterControlledByCurve(string name)
		{
			return this.IsParameterControlledByCurveString(name);
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x0001A3B0 File Offset: 0x000185B0
		public bool IsParameterControlledByCurve(int id)
		{
			return this.IsParameterControlledByCurveID(id);
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001BA5 RID: 7077
		public extern int layerCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001BA6 RID: 7078
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetLayerName(int layerIndex);

		// Token: 0x06001BA7 RID: 7079
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetLayerIndex(string layerName);

		// Token: 0x06001BA8 RID: 7080
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetLayerWeight(int layerIndex);

		// Token: 0x06001BA9 RID: 7081
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetLayerWeight(int layerIndex, float weight);

		// Token: 0x06001BAA RID: 7082
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex);

		// Token: 0x06001BAB RID: 7083
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimatorStateInfo GetNextAnimatorStateInfo(int layerIndex);

		// Token: 0x06001BAC RID: 7084
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimatorTransitionInfo GetAnimatorTransitionInfo(int layerIndex);

		// Token: 0x06001BAD RID: 7085
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimatorClipInfo[] GetCurrentAnimatorClipInfo(int layerIndex);

		// Token: 0x06001BAE RID: 7086
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AnimatorClipInfo[] GetNextAnimatorClipInfo(int layerIndex);

		// Token: 0x06001BAF RID: 7087
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string ResolveHash(int hash);

		// Token: 0x06001BB0 RID: 7088
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsInTransition(int layerIndex);

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001BB1 RID: 7089
		public extern int parameterCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001BB2 RID: 7090
		private extern AnimatorControllerParameter[] parameters { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001BB3 RID: 7091 RVA: 0x0001A3BC File Offset: 0x000185BC
		public AnimatorControllerParameter GetParameter(int index)
		{
			AnimatorControllerParameter[] parameters = this.parameters;
			if (index < 0 && index >= this.parameters.Length)
			{
				throw new IndexOutOfRangeException("index");
			}
			return parameters[index];
		}

		// Token: 0x06001BB4 RID: 7092
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int StringToHash(string name);

		// Token: 0x06001BB5 RID: 7093 RVA: 0x0001A3F4 File Offset: 0x000185F4
		[ExcludeFromDocs]
		public void CrossFadeInFixedTime(string stateName, float transitionDuration, int layer)
		{
			float fixedTime = 0f;
			this.CrossFadeInFixedTime(stateName, transitionDuration, layer, fixedTime);
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x0001A414 File Offset: 0x00018614
		[ExcludeFromDocs]
		public void CrossFadeInFixedTime(string stateName, float transitionDuration)
		{
			float fixedTime = 0f;
			int layer = -1;
			this.CrossFadeInFixedTime(stateName, transitionDuration, layer, fixedTime);
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x0001A434 File Offset: 0x00018634
		public void CrossFadeInFixedTime(string stateName, float transitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("0.0f")] float fixedTime)
		{
			this.CrossFadeInFixedTime(AnimatorControllerPlayable.StringToHash(stateName), transitionDuration, layer, fixedTime);
		}

		// Token: 0x06001BB8 RID: 7096
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CrossFadeInFixedTime(int stateNameHash, float transitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("0.0f")] float fixedTime);

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0001A448 File Offset: 0x00018648
		[ExcludeFromDocs]
		public void CrossFadeInFixedTime(int stateNameHash, float transitionDuration, int layer)
		{
			float fixedTime = 0f;
			this.CrossFadeInFixedTime(stateNameHash, transitionDuration, layer, fixedTime);
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x0001A468 File Offset: 0x00018668
		[ExcludeFromDocs]
		public void CrossFadeInFixedTime(int stateNameHash, float transitionDuration)
		{
			float fixedTime = 0f;
			int layer = -1;
			this.CrossFadeInFixedTime(stateNameHash, transitionDuration, layer, fixedTime);
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0001A488 File Offset: 0x00018688
		[ExcludeFromDocs]
		public void CrossFade(string stateName, float transitionDuration, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.CrossFade(stateName, transitionDuration, layer, negativeInfinity);
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x0001A4A8 File Offset: 0x000186A8
		[ExcludeFromDocs]
		public void CrossFade(string stateName, float transitionDuration)
		{
			float negativeInfinity = float.NegativeInfinity;
			int layer = -1;
			this.CrossFade(stateName, transitionDuration, layer, negativeInfinity);
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0001A4C8 File Offset: 0x000186C8
		public void CrossFade(string stateName, float transitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			this.CrossFade(AnimatorControllerPlayable.StringToHash(stateName), transitionDuration, layer, normalizedTime);
		}

		// Token: 0x06001BBE RID: 7102
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CrossFade(int stateNameHash, float transitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime);

		// Token: 0x06001BBF RID: 7103 RVA: 0x0001A4DC File Offset: 0x000186DC
		[ExcludeFromDocs]
		public void CrossFade(int stateNameHash, float transitionDuration, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.CrossFade(stateNameHash, transitionDuration, layer, negativeInfinity);
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x0001A4FC File Offset: 0x000186FC
		[ExcludeFromDocs]
		public void CrossFade(int stateNameHash, float transitionDuration)
		{
			float negativeInfinity = float.NegativeInfinity;
			int layer = -1;
			this.CrossFade(stateNameHash, transitionDuration, layer, negativeInfinity);
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x0001A51C File Offset: 0x0001871C
		[ExcludeFromDocs]
		public void PlayInFixedTime(string stateName, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.PlayInFixedTime(stateName, layer, negativeInfinity);
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x0001A538 File Offset: 0x00018738
		[ExcludeFromDocs]
		public void PlayInFixedTime(string stateName)
		{
			float negativeInfinity = float.NegativeInfinity;
			int layer = -1;
			this.PlayInFixedTime(stateName, layer, negativeInfinity);
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0001A558 File Offset: 0x00018758
		public void PlayInFixedTime(string stateName, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float fixedTime)
		{
			this.PlayInFixedTime(AnimatorControllerPlayable.StringToHash(stateName), layer, fixedTime);
		}

		// Token: 0x06001BC4 RID: 7108
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void PlayInFixedTime(int stateNameHash, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float fixedTime);

		// Token: 0x06001BC5 RID: 7109 RVA: 0x0001A568 File Offset: 0x00018768
		[ExcludeFromDocs]
		public void PlayInFixedTime(int stateNameHash, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.PlayInFixedTime(stateNameHash, layer, negativeInfinity);
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x0001A584 File Offset: 0x00018784
		[ExcludeFromDocs]
		public void PlayInFixedTime(int stateNameHash)
		{
			float negativeInfinity = float.NegativeInfinity;
			int layer = -1;
			this.PlayInFixedTime(stateNameHash, layer, negativeInfinity);
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x0001A5A4 File Offset: 0x000187A4
		[ExcludeFromDocs]
		public void Play(string stateName, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.Play(stateName, layer, negativeInfinity);
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x0001A5C0 File Offset: 0x000187C0
		[ExcludeFromDocs]
		public void Play(string stateName)
		{
			float negativeInfinity = float.NegativeInfinity;
			int layer = -1;
			this.Play(stateName, layer, negativeInfinity);
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x0001A5E0 File Offset: 0x000187E0
		public void Play(string stateName, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			this.Play(AnimatorControllerPlayable.StringToHash(stateName), layer, normalizedTime);
		}

		// Token: 0x06001BCA RID: 7114
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Play(int stateNameHash, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime);

		// Token: 0x06001BCB RID: 7115 RVA: 0x0001A5F0 File Offset: 0x000187F0
		[ExcludeFromDocs]
		public void Play(int stateNameHash, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.Play(stateNameHash, layer, negativeInfinity);
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x0001A60C File Offset: 0x0001880C
		[ExcludeFromDocs]
		public void Play(int stateNameHash)
		{
			float negativeInfinity = float.NegativeInfinity;
			int layer = -1;
			this.Play(stateNameHash, layer, negativeInfinity);
		}

		// Token: 0x06001BCD RID: 7117
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasState(int layerIndex, int stateID);

		// Token: 0x06001BCE RID: 7118
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatString(string name, float value);

		// Token: 0x06001BCF RID: 7119
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatID(int id, float value);

		// Token: 0x06001BD0 RID: 7120
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetFloatString(string name);

		// Token: 0x06001BD1 RID: 7121
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetFloatID(int id);

		// Token: 0x06001BD2 RID: 7122
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBoolString(string name, bool value);

		// Token: 0x06001BD3 RID: 7123
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBoolID(int id, bool value);

		// Token: 0x06001BD4 RID: 7124
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetBoolString(string name);

		// Token: 0x06001BD5 RID: 7125
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetBoolID(int id);

		// Token: 0x06001BD6 RID: 7126
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIntegerString(string name, int value);

		// Token: 0x06001BD7 RID: 7127
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIntegerID(int id, int value);

		// Token: 0x06001BD8 RID: 7128
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetIntegerString(string name);

		// Token: 0x06001BD9 RID: 7129
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetIntegerID(int id);

		// Token: 0x06001BDA RID: 7130
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTriggerString(string name);

		// Token: 0x06001BDB RID: 7131
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTriggerID(int id);

		// Token: 0x06001BDC RID: 7132
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResetTriggerString(string name);

		// Token: 0x06001BDD RID: 7133
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResetTriggerID(int id);

		// Token: 0x06001BDE RID: 7134
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsParameterControlledByCurveString(string name);

		// Token: 0x06001BDF RID: 7135
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsParameterControlledByCurveID(int id);
	}
}
