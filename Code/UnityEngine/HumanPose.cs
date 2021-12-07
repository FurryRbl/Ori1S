using System;

namespace UnityEngine
{
	// Token: 0x020001C5 RID: 453
	public struct HumanPose
	{
		// Token: 0x06001B40 RID: 6976 RVA: 0x00019D88 File Offset: 0x00017F88
		internal void Init()
		{
			if (this.muscles != null && this.muscles.Length != HumanTrait.MuscleCount)
			{
				throw new ArgumentException("Bad array size for HumanPose.muscles. Size must equal HumanTrait.MuscleCount");
			}
			if (this.muscles == null)
			{
				this.muscles = new float[HumanTrait.MuscleCount];
				if (this.bodyRotation.x == 0f && this.bodyRotation.y == 0f && this.bodyRotation.z == 0f && this.bodyRotation.w == 0f)
				{
					this.bodyRotation.w = 1f;
				}
			}
		}

		// Token: 0x04000587 RID: 1415
		public Vector3 bodyPosition;

		// Token: 0x04000588 RID: 1416
		public Quaternion bodyRotation;

		// Token: 0x04000589 RID: 1417
		public float[] muscles;
	}
}
