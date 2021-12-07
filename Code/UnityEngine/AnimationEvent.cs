using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200019B RID: 411
	[RequiredByNativeCode]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AnimationEvent
	{
		// Token: 0x06001952 RID: 6482 RVA: 0x00018B58 File Offset: 0x00016D58
		public AnimationEvent()
		{
			this.m_Time = 0f;
			this.m_FunctionName = string.Empty;
			this.m_StringParameter = string.Empty;
			this.m_ObjectReferenceParameter = null;
			this.m_FloatParameter = 0f;
			this.m_IntParameter = 0;
			this.m_MessageOptions = 0;
			this.m_Source = AnimationEventSource.NoSource;
			this.m_StateSender = null;
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x00018BBC File Offset: 0x00016DBC
		// (set) Token: 0x06001954 RID: 6484 RVA: 0x00018BC4 File Offset: 0x00016DC4
		[Obsolete("Use stringParameter instead")]
		public string data
		{
			get
			{
				return this.m_StringParameter;
			}
			set
			{
				this.m_StringParameter = value;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x00018BD0 File Offset: 0x00016DD0
		// (set) Token: 0x06001956 RID: 6486 RVA: 0x00018BD8 File Offset: 0x00016DD8
		public string stringParameter
		{
			get
			{
				return this.m_StringParameter;
			}
			set
			{
				this.m_StringParameter = value;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x00018BE4 File Offset: 0x00016DE4
		// (set) Token: 0x06001958 RID: 6488 RVA: 0x00018BEC File Offset: 0x00016DEC
		public float floatParameter
		{
			get
			{
				return this.m_FloatParameter;
			}
			set
			{
				this.m_FloatParameter = value;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001959 RID: 6489 RVA: 0x00018BF8 File Offset: 0x00016DF8
		// (set) Token: 0x0600195A RID: 6490 RVA: 0x00018C00 File Offset: 0x00016E00
		public int intParameter
		{
			get
			{
				return this.m_IntParameter;
			}
			set
			{
				this.m_IntParameter = value;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x00018C0C File Offset: 0x00016E0C
		// (set) Token: 0x0600195C RID: 6492 RVA: 0x00018C14 File Offset: 0x00016E14
		public Object objectReferenceParameter
		{
			get
			{
				return this.m_ObjectReferenceParameter;
			}
			set
			{
				this.m_ObjectReferenceParameter = value;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x00018C20 File Offset: 0x00016E20
		// (set) Token: 0x0600195E RID: 6494 RVA: 0x00018C28 File Offset: 0x00016E28
		public string functionName
		{
			get
			{
				return this.m_FunctionName;
			}
			set
			{
				this.m_FunctionName = value;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x00018C34 File Offset: 0x00016E34
		// (set) Token: 0x06001960 RID: 6496 RVA: 0x00018C3C File Offset: 0x00016E3C
		public float time
		{
			get
			{
				return this.m_Time;
			}
			set
			{
				this.m_Time = value;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x00018C48 File Offset: 0x00016E48
		// (set) Token: 0x06001962 RID: 6498 RVA: 0x00018C50 File Offset: 0x00016E50
		public SendMessageOptions messageOptions
		{
			get
			{
				return (SendMessageOptions)this.m_MessageOptions;
			}
			set
			{
				this.m_MessageOptions = (int)value;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x00018C5C File Offset: 0x00016E5C
		public bool isFiredByLegacy
		{
			get
			{
				return this.m_Source == AnimationEventSource.Legacy;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x00018C68 File Offset: 0x00016E68
		public bool isFiredByAnimator
		{
			get
			{
				return this.m_Source == AnimationEventSource.Animator;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001965 RID: 6501 RVA: 0x00018C74 File Offset: 0x00016E74
		public AnimationState animationState
		{
			get
			{
				if (!this.isFiredByLegacy)
				{
					Debug.LogError("AnimationEvent was not fired by Animation component, you shouldn't use AnimationEvent.animationState");
				}
				return this.m_StateSender;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x00018C94 File Offset: 0x00016E94
		public AnimatorStateInfo animatorStateInfo
		{
			get
			{
				if (!this.isFiredByAnimator)
				{
					Debug.LogError("AnimationEvent was not fired by Animator component, you shouldn't use AnimationEvent.animatorStateInfo");
				}
				return this.m_AnimatorStateInfo;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x00018CB4 File Offset: 0x00016EB4
		public AnimatorClipInfo animatorClipInfo
		{
			get
			{
				if (!this.isFiredByAnimator)
				{
					Debug.LogError("AnimationEvent was not fired by Animator component, you shouldn't use AnimationEvent.animatorClipInfo");
				}
				return this.m_AnimatorClipInfo;
			}
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00018CD4 File Offset: 0x00016ED4
		internal int GetHash()
		{
			int hashCode = this.functionName.GetHashCode();
			return 33 * hashCode + this.time.GetHashCode();
		}

		// Token: 0x0400048B RID: 1163
		internal float m_Time;

		// Token: 0x0400048C RID: 1164
		internal string m_FunctionName;

		// Token: 0x0400048D RID: 1165
		internal string m_StringParameter;

		// Token: 0x0400048E RID: 1166
		internal Object m_ObjectReferenceParameter;

		// Token: 0x0400048F RID: 1167
		internal float m_FloatParameter;

		// Token: 0x04000490 RID: 1168
		internal int m_IntParameter;

		// Token: 0x04000491 RID: 1169
		internal int m_MessageOptions;

		// Token: 0x04000492 RID: 1170
		internal AnimationEventSource m_Source;

		// Token: 0x04000493 RID: 1171
		internal AnimationState m_StateSender;

		// Token: 0x04000494 RID: 1172
		internal AnimatorStateInfo m_AnimatorStateInfo;

		// Token: 0x04000495 RID: 1173
		internal AnimatorClipInfo m_AnimatorClipInfo;
	}
}
