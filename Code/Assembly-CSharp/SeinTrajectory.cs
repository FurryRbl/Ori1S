using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001B3 RID: 435
public class SeinTrajectory : MonoBehaviour
{
	// Token: 0x0600105E RID: 4190 RVA: 0x0004AAC8 File Offset: 0x00048CC8
	public void OnDrawGizmos()
	{
		Vector2 vector = new Vector2(0f, 0f);
		Vector2 wallJump = new Vector2(this.StartSpeed * 12.6666f, 0f);
		float num = 0.01666667f;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = true;
		List<Vector2> list = new List<Vector2>();
		float d = 0f;
		Color color = Gizmos.color;
		for (float num2 = 0f; num2 < this.TotalTime; num2 += num)
		{
			bool flag4 = num2 >= this.RunningForwardTime && num2 <= this.RunningForwardTime + this.RunningForwardDuration;
			bool flag5 = num2 >= this.RunningBackwardTime && num2 <= this.RunningBackwardTime + this.RunningBackwardDuration;
			bool flag6 = num2 >= this.JumpingTime;
			bool flag7 = num2 >= this.JumpingTime + this.JumpingDuration;
			bool flag8 = num2 >= this.DoubleJumpTime && this.DoubleJump;
			bool flag9 = num2 < this.JumpingTime && (num2 < this.WallSlideTime || !this.WallSlide);
			bool flag10 = num2 >= this.WallSlideTime && num2 <= this.WallSlideTime + this.WallSlideDuration;
			if (flag7 && !flag8 && wallJump.y > 0f)
			{
				wallJump.y -= 60f * num;
				if (wallJump.y < 0f)
				{
					wallJump.y = 0f;
				}
			}
			if (flag4)
			{
				wallJump.x += num * ((!flag9) ? 26f : 60f);
				flag3 = true;
				if (wallJump.x > 12.6666f)
				{
					wallJump.x = 12.6666f;
				}
			}
			if (flag5)
			{
				wallJump.x -= num * ((!flag9) ? 26f : 60f);
				flag3 = true;
				if (wallJump.x < -12.6666f)
				{
					wallJump.x = -12.6666f;
				}
			}
			if (flag3)
			{
				if (!flag4 && wallJump.x > 0f)
				{
					wallJump.x -= 26f * num;
					if (wallJump.x < 0f)
					{
						wallJump.x = 0f;
					}
				}
				if (!flag5 && wallJump.x < 0f)
				{
					wallJump.x += 26f * num;
					if (wallJump.x > 0f)
					{
						wallJump.x = 0f;
					}
				}
			}
			if (flag6 && !flag2)
			{
				flag2 = true;
				SeinTrajectory.JumpType jump = this.Jump;
				if (jump != SeinTrajectory.JumpType.Jump)
				{
					if (jump == SeinTrajectory.JumpType.WallJump)
					{
						wallJump = this.WallJump;
						flag3 = false;
					}
				}
				else
				{
					wallJump.y = MoonMath.Physics.SpeedFromHeightAndGravity(26f, 3f);
				}
			}
			if (flag8 && !flag && this.DoubleJump)
			{
				flag = true;
				wallJump.y = 10f;
			}
			if (!flag9)
			{
				wallJump.y -= 26f * num;
			}
			if (this.WallSlide && flag10)
			{
				wallJump.y += 26f * num * 0.5f;
				wallJump.x = 0f;
				d = vector.x;
			}
			if (wallJump.y < -32f)
			{
				wallJump.y = -32f;
			}
			if (this.Left)
			{
				vector.x -= wallJump.x * num;
				vector.y += wallJump.y * num;
			}
			else
			{
				vector += wallJump * num;
			}
			list.Add(vector);
		}
		Gizmos.DrawWireSphere(base.transform.position + Vector3.left * d + Vector3.up * 0.5f * 0.50000006f, 0.35f);
		Gizmos.DrawWireSphere(base.transform.position + Vector3.left * d, 0.35f);
		Gizmos.DrawWireSphere(base.transform.position + Vector3.left * d + Vector3.down * 0.5f * 0.50000006f, 0.35f);
		Gizmos.color = new Color(0f, 0.5f, 1f, 0.2f);
		foreach (Vector2 v in list)
		{
			Vector3 a = v + base.transform.position + Vector3.left * d;
			Gizmos.DrawWireSphere(a + Vector3.up * 0.5f * 0.50000006f, 0.35f);
			Gizmos.DrawWireSphere(a + Vector3.down * 0.5f * 0.50000006f, 0.35f);
		}
		Gizmos.color = Color.white;
		foreach (Vector2 v2 in list)
		{
			Vector3 center = v2 + base.transform.position + Vector3.left * d;
			Gizmos.DrawSphere(center, 0.05f);
		}
		Gizmos.color = color;
	}

	// Token: 0x04000D95 RID: 3477
	private const float AirAcceleration = 26f;

	// Token: 0x04000D96 RID: 3478
	private const float AirDecceleration = 26f;

	// Token: 0x04000D97 RID: 3479
	private const float MaxSpeed = 12.6666f;

	// Token: 0x04000D98 RID: 3480
	private const float GroundAcceleration = 60f;

	// Token: 0x04000D99 RID: 3481
	private const float JumpHeight = 3f;

	// Token: 0x04000D9A RID: 3482
	private const float JumpSustainDeceleration = 60f;

	// Token: 0x04000D9B RID: 3483
	private const float DoubleJumpStrength = 10f;

	// Token: 0x04000D9C RID: 3484
	private const float Gravity = 26f;

	// Token: 0x04000D9D RID: 3485
	private const float MaxFallSpeed = 32f;

	// Token: 0x04000D9E RID: 3486
	private const float CapsuleRadius = 0.35f;

	// Token: 0x04000D9F RID: 3487
	private const float CapsuleHeight = 1.2f;

	// Token: 0x04000DA0 RID: 3488
	public SeinTrajectory.JumpType Jump;

	// Token: 0x04000DA1 RID: 3489
	public float JumpingTime;

	// Token: 0x04000DA2 RID: 3490
	public float JumpingDuration = 2f;

	// Token: 0x04000DA3 RID: 3491
	public bool DoubleJump;

	// Token: 0x04000DA4 RID: 3492
	public float DoubleJumpTime = 2f;

	// Token: 0x04000DA5 RID: 3493
	public float RunningForwardTime;

	// Token: 0x04000DA6 RID: 3494
	public float RunningForwardDuration = 5f;

	// Token: 0x04000DA7 RID: 3495
	public float RunningBackwardTime;

	// Token: 0x04000DA8 RID: 3496
	public float RunningBackwardDuration;

	// Token: 0x04000DA9 RID: 3497
	public float StartSpeed = 1f;

	// Token: 0x04000DAA RID: 3498
	public float TotalTime = 1f;

	// Token: 0x04000DAB RID: 3499
	public bool Left;

	// Token: 0x04000DAC RID: 3500
	public bool WallSlide;

	// Token: 0x04000DAD RID: 3501
	public float WallSlideTime = 5f;

	// Token: 0x04000DAE RID: 3502
	public float WallSlideDuration;

	// Token: 0x04000DAF RID: 3503
	private readonly Vector2 WallJump = new Vector2(6f, 12f);

	// Token: 0x020001B4 RID: 436
	public enum JumpType
	{
		// Token: 0x04000DB1 RID: 3505
		Jump,
		// Token: 0x04000DB2 RID: 3506
		WallJump,
		// Token: 0x04000DB3 RID: 3507
		None
	}
}
