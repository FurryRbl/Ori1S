using System;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x02000030 RID: 48
	internal interface ITweenValue
	{
		// Token: 0x06000135 RID: 309
		void TweenValue(float floatPercentage);

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000136 RID: 310
		bool ignoreTimeScale { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000137 RID: 311
		float duration { get; }

		// Token: 0x06000138 RID: 312
		bool ValidTarget();
	}
}
