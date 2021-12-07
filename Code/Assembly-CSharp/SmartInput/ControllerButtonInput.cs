using System;

namespace SmartInput
{
	// Token: 0x02000192 RID: 402
	public class ControllerButtonInput : IButtonInput
	{
		// Token: 0x06000FAF RID: 4015 RVA: 0x00047EEC File Offset: 0x000460EC
		public ControllerButtonInput(XboxControllerInput.Button button)
		{
			this.Button = button;
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x00047EFB File Offset: 0x000460FB
		public bool GetButton()
		{
			return XboxControllerInput.GetButton(this.Button, -1);
		}

		// Token: 0x04000C60 RID: 3168
		public XboxControllerInput.Button Button;
	}
}
