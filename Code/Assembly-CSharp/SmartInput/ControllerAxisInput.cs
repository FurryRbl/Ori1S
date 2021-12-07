using System;

namespace SmartInput
{
	// Token: 0x02000401 RID: 1025
	public class ControllerAxisInput : IAxisInput
	{
		// Token: 0x06001BE3 RID: 7139 RVA: 0x00077FAA File Offset: 0x000761AA
		public ControllerAxisInput(XboxControllerInput.Axis axis)
		{
			this.m_axis = axis;
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x00077FB9 File Offset: 0x000761B9
		public float AxisValue()
		{
			return XboxControllerInput.GetAxis(this.m_axis);
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x00077FC6 File Offset: 0x000761C6
		public XboxControllerInput.Axis Axis
		{
			get
			{
				return this.m_axis;
			}
		}

		// Token: 0x04001827 RID: 6183
		private XboxControllerInput.Axis m_axis;
	}
}
