using System;
using UnityEngine;

namespace SmartInput
{
	// Token: 0x020003FF RID: 1023
	public class CompoundAxisInput : IAxisInput
	{
		// Token: 0x06001BDC RID: 7132 RVA: 0x00077EA1 File Offset: 0x000760A1
		public CompoundAxisInput(params IAxisInput[] inputs)
		{
			this.Axis = inputs;
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x00077EB0 File Offset: 0x000760B0
		public CompoundAxisInput()
		{
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x00077EB8 File Offset: 0x000760B8
		public float AxisValue()
		{
			float num = 0f;
			float num2 = 0f;
			if (this.Axis != null)
			{
				for (int i = 0; i < this.Axis.Length; i++)
				{
					float num3 = this.Axis[i].AxisValue();
					if (Mathf.Abs(num3) > 0.2f)
					{
						this.m_lastPressedIndex = i;
					}
					if (num3 < 0f)
					{
						num2 = Mathf.Min(num2, num3);
					}
					else
					{
						num = Mathf.Max(num, num3);
					}
				}
			}
			return num + num2;
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x00077F3D File Offset: 0x0007613D
		public IAxisInput GetLastPressed()
		{
			return this.Axis[this.m_lastPressedIndex];
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x00077F4C File Offset: 0x0007614C
		public void Add(IAxisInput axis)
		{
			if (this.Axis == null)
			{
				this.Axis = new IAxisInput[1];
				this.Axis[0] = axis;
				return;
			}
			Array.Resize<IAxisInput>(ref this.Axis, this.Axis.Length + 1);
			this.Axis[this.Axis.Length - 1] = axis;
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x00077FA1 File Offset: 0x000761A1
		public void Clear()
		{
			this.Axis = null;
		}

		// Token: 0x04001825 RID: 6181
		public IAxisInput[] Axis;

		// Token: 0x04001826 RID: 6182
		private int m_lastPressedIndex;
	}
}
