using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000902 RID: 2306
public class HighlightNearSein : MonoBehaviour, INearSeinReceiver
{
	// Token: 0x0600333C RID: 13116 RVA: 0x000D7E88 File Offset: 0x000D6088
	public void OnNearSeinEnter()
	{
		foreach (LegacyAnimator legacyAnimator in this.Animators)
		{
			legacyAnimator.ContinueForward();
		}
	}

	// Token: 0x0600333D RID: 13117 RVA: 0x000D7EE4 File Offset: 0x000D60E4
	public void OnNearSeinExit()
	{
		foreach (LegacyAnimator legacyAnimator in this.Animators)
		{
			legacyAnimator.ContinueBackward();
		}
	}

	// Token: 0x0600333E RID: 13118 RVA: 0x000D7F40 File Offset: 0x000D6140
	public void OnSeinNearStay()
	{
	}

	// Token: 0x04002E2E RID: 11822
	public List<LegacyAnimator> Animators;
}
