using System;
using UnityEngine;

// Token: 0x0200041C RID: 1052
public abstract class CharacterAnimationStateBase : MonoBehaviour
{
	// Token: 0x170004F7 RID: 1271
	// (get) Token: 0x06001D76 RID: 7542
	public abstract bool CanEnter { get; }

	// Token: 0x06001D77 RID: 7543 RVA: 0x00081A0D File Offset: 0x0007FC0D
	public virtual void OnEnter()
	{
	}

	// Token: 0x06001D78 RID: 7544 RVA: 0x00081A0F File Offset: 0x0007FC0F
	public virtual void OnExit()
	{
	}

	// Token: 0x06001D79 RID: 7545 RVA: 0x00081A11 File Offset: 0x0007FC11
	public virtual void OnAnimationEnd(TextureAnimation animation)
	{
	}

	// Token: 0x170004F8 RID: 1272
	// (get) Token: 0x06001D7A RID: 7546
	public abstract TextureAnimationWithTransitions AnimationToPlay { get; }
}
