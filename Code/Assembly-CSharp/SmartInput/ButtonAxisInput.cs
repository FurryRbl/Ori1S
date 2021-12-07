using System;

namespace SmartInput
{
	// Token: 0x02000407 RID: 1031
	public class ButtonAxisInput : IAxisInput
	{
		// Token: 0x06001BFC RID: 7164 RVA: 0x000795CF File Offset: 0x000777CF
		public ButtonAxisInput(IButtonInput buttonInput, ButtonAxisInput.Mode mode)
		{
			this.m_button = buttonInput;
			this.m_mode = mode;
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x000795E8 File Offset: 0x000777E8
		public float AxisValue()
		{
			ButtonAxisInput.Mode mode = this.m_mode;
			if (mode == ButtonAxisInput.Mode.Positive)
			{
				return (float)((!this.m_button.GetButton()) ? 0 : 1);
			}
			if (mode != ButtonAxisInput.Mode.Negative)
			{
				return 0f;
			}
			return (float)((!this.m_button.GetButton()) ? 0 : -1);
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00079645 File Offset: 0x00077845
		public IButtonInput GetButtonInput()
		{
			return this.m_button;
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x0007964D File Offset: 0x0007784D
		public bool Positive
		{
			get
			{
				return this.m_mode == ButtonAxisInput.Mode.Positive;
			}
		}

		// Token: 0x0400185D RID: 6237
		private readonly IButtonInput m_button;

		// Token: 0x0400185E RID: 6238
		private readonly ButtonAxisInput.Mode m_mode;

		// Token: 0x02000408 RID: 1032
		public enum Mode
		{
			// Token: 0x04001860 RID: 6240
			Positive,
			// Token: 0x04001861 RID: 6241
			Negative
		}
	}
}
