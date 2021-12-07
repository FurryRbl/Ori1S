using System;
using Core;

// Token: 0x0200066B RID: 1643
public class ButtonPressedCondition : Condition
{
	// Token: 0x06002804 RID: 10244 RVA: 0x000ADD50 File Offset: 0x000ABF50
	public override bool Validate(IContext context)
	{
		if (this.Button == Input.Button.Any)
		{
			switch (this.State)
			{
			case ButtonPressedCondition.ButtonStates.Pressed:
				return Input.AnyButtonPressed;
			case ButtonPressedCondition.ButtonStates.Released:
				return Input.AnyButtonReleased;
			case ButtonPressedCondition.ButtonStates.OnPressed:
				return Input.OnAnyButtonPressed;
			case ButtonPressedCondition.ButtonStates.OnReleased:
				return Input.OnAnyButtonReleased;
			}
		}
		else
		{
			switch (this.State)
			{
			case ButtonPressedCondition.ButtonStates.Pressed:
				return Input.GetButton(this.Button).Pressed;
			case ButtonPressedCondition.ButtonStates.Released:
				return Input.GetButton(this.Button).Released;
			case ButtonPressedCondition.ButtonStates.OnPressed:
				return Input.GetButton(this.Button).OnPressedNotUsed;
			case ButtonPressedCondition.ButtonStates.OnReleased:
				return Input.GetButton(this.Button).OnReleased;
			}
		}
		return false;
	}

	// Token: 0x04002295 RID: 8853
	public Input.Button Button;

	// Token: 0x04002296 RID: 8854
	public ButtonPressedCondition.ButtonStates State;

	// Token: 0x0200066C RID: 1644
	public enum ButtonStates
	{
		// Token: 0x04002298 RID: 8856
		Pressed,
		// Token: 0x04002299 RID: 8857
		Released,
		// Token: 0x0400229A RID: 8858
		OnPressed,
		// Token: 0x0400229B RID: 8859
		OnReleased
	}
}
