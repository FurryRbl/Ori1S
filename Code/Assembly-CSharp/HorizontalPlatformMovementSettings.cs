using System;

// Token: 0x020000B8 RID: 184
[Serializable]
public class HorizontalPlatformMovementSettings
{
	// Token: 0x060007D2 RID: 2002 RVA: 0x000219B8 File Offset: 0x0001FBB8
	public void CopyFrom(HorizontalPlatformMovementSettings settings)
	{
		this.Ground.CopyFrom(settings.Ground);
		this.Air.CopyFrom(settings.Air);
		this.LockInput = settings.LockInput;
	}

	// Token: 0x04000647 RID: 1607
	public HorizontalPlatformMovementSettings.SpeedSet Ground = new HorizontalPlatformMovementSettings.SpeedSet();

	// Token: 0x04000648 RID: 1608
	public HorizontalPlatformMovementSettings.SpeedSet Air = new HorizontalPlatformMovementSettings.SpeedSet();

	// Token: 0x04000649 RID: 1609
	public bool LockInput;

	// Token: 0x020000B9 RID: 185
	[Serializable]
	public class SpeedMultiplierSet
	{
		// Token: 0x0400064A RID: 1610
		public float AccelerationMultiplier = 1f;

		// Token: 0x0400064B RID: 1611
		public float DeceelerationMultiplier = 1f;

		// Token: 0x0400064C RID: 1612
		public float MaxSpeedMultiplier = 1f;
	}

	// Token: 0x020000C0 RID: 192
	[Serializable]
	public class SpeedSet
	{
		// Token: 0x06000821 RID: 2081 RVA: 0x00022F0C File Offset: 0x0002110C
		public void CopyFrom(HorizontalPlatformMovementSettings.SpeedSet speedSet)
		{
			this.MaxSpeed = speedSet.MaxSpeed;
			this.Acceleration = speedSet.Acceleration;
			this.Decceleration = speedSet.Decceleration;
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00022F3D File Offset: 0x0002113D
		public void ApplySpeedMultiplier(HorizontalPlatformMovementSettings.SpeedMultiplierSet speedMultiplierSet)
		{
			this.MaxSpeed *= speedMultiplierSet.MaxSpeedMultiplier;
			this.Acceleration *= speedMultiplierSet.AccelerationMultiplier;
			this.Decceleration *= speedMultiplierSet.DeceelerationMultiplier;
		}

		// Token: 0x0400067F RID: 1663
		public float Acceleration;

		// Token: 0x04000680 RID: 1664
		public float Decceleration;

		// Token: 0x04000681 RID: 1665
		public float MaxSpeed;
	}
}
