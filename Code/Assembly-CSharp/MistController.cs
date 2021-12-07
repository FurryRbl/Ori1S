using System;
using Sein.World;
using UnityEngine;

// Token: 0x020009D5 RID: 2517
public class MistController : MonoBehaviour
{
	// Token: 0x060036C6 RID: 14022 RVA: 0x000E5F3C File Offset: 0x000E413C
	public void Awake()
	{
		if (Events.MistLifted)
		{
			this.MistAnimator.StopAndSampleAtEnd();
		}
		else
		{
			this.MistAnimator.StopAndSampleAtStart();
		}
	}

	// Token: 0x060036C7 RID: 14023 RVA: 0x000E5F64 File Offset: 0x000E4164
	public void FixedUpdate()
	{
		if (Events.MistLifted && this.MistAnimator.AtStart)
		{
			this.MistAnimator.ContinueForward();
		}
		if (!Events.MistLifted && this.MistAnimator.AtEnd)
		{
			this.MistAnimator.ContinueBackward();
		}
	}

	// Token: 0x040031AE RID: 12718
	public LegacyTransparancyAnimator MistAnimator;
}
