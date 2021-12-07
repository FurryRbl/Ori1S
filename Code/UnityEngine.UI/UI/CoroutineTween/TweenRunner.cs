using System;
using System.Collections;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x02000036 RID: 54
	internal class TweenRunner<T> where T : struct, ITweenValue
	{
		// Token: 0x06000158 RID: 344 RVA: 0x00005A64 File Offset: 0x00003C64
		private static IEnumerator Start(T tweenInfo)
		{
			if (!tweenInfo.ValidTarget())
			{
				yield break;
			}
			float elapsedTime = 0f;
			while (elapsedTime < tweenInfo.duration)
			{
				elapsedTime += ((!tweenInfo.ignoreTimeScale) ? Time.deltaTime : Time.unscaledDeltaTime);
				float percentage = Mathf.Clamp01(elapsedTime / tweenInfo.duration);
				tweenInfo.TweenValue(percentage);
				yield return null;
			}
			tweenInfo.TweenValue(1f);
			yield break;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005A88 File Offset: 0x00003C88
		public void Init(MonoBehaviour coroutineContainer)
		{
			this.m_CoroutineContainer = coroutineContainer;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005A94 File Offset: 0x00003C94
		public void StartTween(T info)
		{
			if (this.m_CoroutineContainer == null)
			{
				Debug.LogWarning("Coroutine container not configured... did you forget to call Init?");
				return;
			}
			if (this.m_Tween != null)
			{
				this.m_CoroutineContainer.StopCoroutine(this.m_Tween);
				this.m_Tween = null;
			}
			if (!this.m_CoroutineContainer.gameObject.activeInHierarchy)
			{
				info.TweenValue(1f);
				return;
			}
			this.m_Tween = TweenRunner<T>.Start(info);
			this.m_CoroutineContainer.StartCoroutine(this.m_Tween);
		}

		// Token: 0x0400009F RID: 159
		protected MonoBehaviour m_CoroutineContainer;

		// Token: 0x040000A0 RID: 160
		protected IEnumerator m_Tween;
	}
}
