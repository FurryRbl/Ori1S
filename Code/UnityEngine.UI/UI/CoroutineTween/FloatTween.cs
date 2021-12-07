using System;
using UnityEngine.Events;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x02000034 RID: 52
	internal struct FloatTween : ITweenValue
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000597C File Offset: 0x00003B7C
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00005984 File Offset: 0x00003B84
		public float startValue
		{
			get
			{
				return this.m_StartValue;
			}
			set
			{
				this.m_StartValue = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00005990 File Offset: 0x00003B90
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00005998 File Offset: 0x00003B98
		public float targetValue
		{
			get
			{
				return this.m_TargetValue;
			}
			set
			{
				this.m_TargetValue = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000059A4 File Offset: 0x00003BA4
		// (set) Token: 0x0600014E RID: 334 RVA: 0x000059AC File Offset: 0x00003BAC
		public float duration
		{
			get
			{
				return this.m_Duration;
			}
			set
			{
				this.m_Duration = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000059B8 File Offset: 0x00003BB8
		// (set) Token: 0x06000150 RID: 336 RVA: 0x000059C0 File Offset: 0x00003BC0
		public bool ignoreTimeScale
		{
			get
			{
				return this.m_IgnoreTimeScale;
			}
			set
			{
				this.m_IgnoreTimeScale = value;
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000059CC File Offset: 0x00003BCC
		public void TweenValue(float floatPercentage)
		{
			if (!this.ValidTarget())
			{
				return;
			}
			float arg = Mathf.Lerp(this.m_StartValue, this.m_TargetValue, floatPercentage);
			this.m_Target.Invoke(arg);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005A04 File Offset: 0x00003C04
		public void AddOnChangedCallback(UnityAction<float> callback)
		{
			if (this.m_Target == null)
			{
				this.m_Target = new FloatTween.FloatTweenCallback();
			}
			this.m_Target.AddListener(callback);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005A34 File Offset: 0x00003C34
		public bool GetIgnoreTimescale()
		{
			return this.m_IgnoreTimeScale;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005A3C File Offset: 0x00003C3C
		public float GetDuration()
		{
			return this.m_Duration;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005A44 File Offset: 0x00003C44
		public bool ValidTarget()
		{
			return this.m_Target != null;
		}

		// Token: 0x0400009A RID: 154
		private FloatTween.FloatTweenCallback m_Target;

		// Token: 0x0400009B RID: 155
		private float m_StartValue;

		// Token: 0x0400009C RID: 156
		private float m_TargetValue;

		// Token: 0x0400009D RID: 157
		private float m_Duration;

		// Token: 0x0400009E RID: 158
		private bool m_IgnoreTimeScale;

		// Token: 0x02000035 RID: 53
		public class FloatTweenCallback : UnityEvent<float>
		{
		}
	}
}
