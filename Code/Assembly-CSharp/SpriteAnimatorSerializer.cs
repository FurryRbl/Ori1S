using System;
using UnityEngine;

// Token: 0x0200038D RID: 909
public class SpriteAnimatorSerializer : SaveSerialize
{
	// Token: 0x060019C7 RID: 6599 RVA: 0x0006E5FE File Offset: 0x0006C7FE
	public void OnValidate()
	{
		this.m_spriteAnimator = base.GetComponent<SpriteAnimator>();
	}

	// Token: 0x060019C8 RID: 6600 RVA: 0x0006E60C File Offset: 0x0006C80C
	public override void Serialize(Archive ar)
	{
		if (this.m_spriteAnimator == null)
		{
			this.m_spriteAnimator = base.GetComponent<SpriteAnimator>();
		}
		this.m_spriteAnimator.DoSerialize(ar);
	}

	// Token: 0x0400161D RID: 5661
	[SerializeField]
	private SpriteAnimator m_spriteAnimator;
}
