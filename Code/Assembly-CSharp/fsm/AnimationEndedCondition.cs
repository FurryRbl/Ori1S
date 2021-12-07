using System;

namespace fsm
{
	// Token: 0x02000566 RID: 1382
	public class AnimationEndedCondition : ICondition
	{
		// Token: 0x060023EA RID: 9194 RVA: 0x0009CDAC File Offset: 0x0009AFAC
		public AnimationEndedCondition(SpriteAnimatorWithTransitions spriteAnimator)
		{
			this.m_spriteAnimator = spriteAnimator;
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x0009CDBB File Offset: 0x0009AFBB
		public bool Validate(IContext context)
		{
			return this.m_spriteAnimator.AnimationEnded;
		}

		// Token: 0x04001E14 RID: 7700
		private readonly SpriteAnimatorWithTransitions m_spriteAnimator;
	}
}
