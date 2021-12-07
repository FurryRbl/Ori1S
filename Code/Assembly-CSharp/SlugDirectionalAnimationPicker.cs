using System;
using UnityEngine;

// Token: 0x020005C3 RID: 1475
public class SlugDirectionalAnimationPicker
{
	// Token: 0x06002552 RID: 9554 RVA: 0x000A2D3D File Offset: 0x000A0F3D
	public SlugDirectionalAnimationPicker(SlugDirectionalAnimation animation)
	{
		this.Animation = animation;
	}

	// Token: 0x06002553 RID: 9555 RVA: 0x000A2D4C File Offset: 0x000A0F4C
	public TextureAnimationWithTransitions PickAnimation(Vector3 up, bool faceLeft)
	{
		float num = 0.76604444f;
		float num2 = 0.6427876f;
		if (Vector3.Dot(up, this.m_lastDirection) > num2)
		{
			if (this.m_lastDirection == Vector3.up)
			{
				return this.Animation.Upright;
			}
			if (this.m_lastDirection == Vector3.down)
			{
				return this.Animation.UpsideDown;
			}
			if (this.m_lastDirection == Vector3.right)
			{
				return (!faceLeft) ? this.Animation.VerticalDown : this.Animation.VerticalUp;
			}
			if (this.m_lastDirection == Vector3.left)
			{
				return (!faceLeft) ? this.Animation.VerticalUp : this.Animation.VerticalDown;
			}
		}
		if (Vector3.Dot(up, Vector3.up) > num)
		{
			this.m_lastDirection = Vector3.up;
			return this.Animation.Upright;
		}
		if (Vector3.Dot(up, Vector3.down) > num)
		{
			this.m_lastDirection = Vector3.down;
			return this.Animation.UpsideDown;
		}
		if (Vector3.Dot(up, Vector3.right) > num)
		{
			this.m_lastDirection = Vector3.right;
			return (!faceLeft) ? this.Animation.VerticalDown : this.Animation.VerticalUp;
		}
		if (Vector3.Dot(up, Vector3.left) > num)
		{
			this.m_lastDirection = Vector3.left;
			return (!faceLeft) ? this.Animation.VerticalUp : this.Animation.VerticalDown;
		}
		return this.Animation.Upright;
	}

	// Token: 0x04001FE4 RID: 8164
	public SlugDirectionalAnimation Animation;

	// Token: 0x04001FE5 RID: 8165
	private Vector3 m_lastDirection;
}
