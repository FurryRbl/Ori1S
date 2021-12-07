using System;

namespace SmartInput
{
	// Token: 0x02000403 RID: 1027
	public class AxisButtonInput : IButtonInput
	{
		// Token: 0x06001BE6 RID: 7142 RVA: 0x00077FD0 File Offset: 0x000761D0
		public AxisButtonInput(IAxisInput axis, AxisButtonInput.AxisMode axisMode, float value = 0f)
		{
			this.m_axis = axis;
			this.m_axisMode = axisMode;
			this.m_comparisonValue = value;
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x00078004 File Offset: 0x00076204
		public bool GetButton()
		{
			AxisButtonInput.AxisMode axisMode = this.m_axisMode;
			if (axisMode != AxisButtonInput.AxisMode.LessThan)
			{
				return axisMode == AxisButtonInput.AxisMode.GreaterThan && this.m_axis.AxisValue() > this.m_comparisonValue;
			}
			return this.m_axis.AxisValue() < this.m_comparisonValue;
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x00078053 File Offset: 0x00076253
		public IAxisInput GetAxisInput()
		{
			return this.m_axis;
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x0007805B File Offset: 0x0007625B
		public AxisButtonInput.AxisMode GetAxisMode()
		{
			return this.m_axisMode;
		}

		// Token: 0x0400182F RID: 6191
		private readonly IAxisInput m_axis;

		// Token: 0x04001830 RID: 6192
		private AxisButtonInput.AxisMode m_axisMode;

		// Token: 0x04001831 RID: 6193
		private float m_comparisonValue = 0.5f;

		// Token: 0x02000404 RID: 1028
		public enum AxisMode
		{
			// Token: 0x04001833 RID: 6195
			LessThan,
			// Token: 0x04001834 RID: 6196
			GreaterThan
		}
	}
}
