using System;

namespace SmartInput
{
	// Token: 0x02000195 RID: 405
	public class CompoundButtonInput : IButtonInput
	{
		// Token: 0x06000FB9 RID: 4025 RVA: 0x000487EC File Offset: 0x000469EC
		public CompoundButtonInput(params IButtonInput[] inputs)
		{
			this.Buttons = inputs;
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x000487FB File Offset: 0x000469FB
		public CompoundButtonInput()
		{
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00048804 File Offset: 0x00046A04
		public bool GetButton()
		{
			if (this.Buttons != null)
			{
				for (int i = 0; i < this.Buttons.Length; i++)
				{
					if (this.Buttons[i].GetButton())
					{
						this.m_lastPressedIndex = i;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00048851 File Offset: 0x00046A51
		public IButtonInput GetLastPressed()
		{
			return this.Buttons[this.m_lastPressedIndex];
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x00048860 File Offset: 0x00046A60
		public void Add(IButtonInput button)
		{
			if (this.Buttons == null)
			{
				this.Buttons = new IButtonInput[]
				{
					button
				};
				return;
			}
			Array.Resize<IButtonInput>(ref this.Buttons, this.Buttons.Length + 1);
			this.Buttons[this.Buttons.Length - 1] = button;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x000488B0 File Offset: 0x00046AB0
		public void Clear()
		{
			this.Buttons = null;
		}

		// Token: 0x04000CE8 RID: 3304
		public IButtonInput[] Buttons;

		// Token: 0x04000CE9 RID: 3305
		private int m_lastPressedIndex;
	}
}
