using System;
using UnityEngine;

// Token: 0x020008E5 RID: 2277
public class FlipPlantLogic : MonoBehaviour
{
	// Token: 0x060032CD RID: 13005 RVA: 0x000D6C25 File Offset: 0x000D4E25
	public void Awake()
	{
		this.m_spriteAnimator = base.GetComponent<SpriteAnimator>();
	}

	// Token: 0x060032CE RID: 13006 RVA: 0x000D6C34 File Offset: 0x000D4E34
	public void GoDown()
	{
		if (this.m_spriteAnimator)
		{
			this.m_spriteAnimator.SetAnimation(this.PlantDown, true);
			this.m_spriteAnimator.AnimatorDriver.Restart();
			this.m_spriteAnimator.AnimatorDriver.CurrentTime = -UnityEngine.Random.value * 0.3f;
		}
	}

	// Token: 0x060032CF RID: 13007 RVA: 0x000D6C90 File Offset: 0x000D4E90
	public void GoUp()
	{
		if (this.m_spriteAnimator)
		{
			this.m_spriteAnimator.SetAnimation(this.PlantUp, true);
			this.m_spriteAnimator.AnimatorDriver.Restart();
			this.m_spriteAnimator.AnimatorDriver.CurrentTime = -UnityEngine.Random.value * 0.3f;
		}
	}

	// Token: 0x04002DCB RID: 11723
	public TextureAnimation PlantUp;

	// Token: 0x04002DCC RID: 11724
	public TextureAnimation PlantDown;

	// Token: 0x04002DCD RID: 11725
	private SpriteAnimator m_spriteAnimator;
}
