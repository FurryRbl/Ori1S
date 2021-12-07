using System;
using Core;
using UnityEngine;

// Token: 0x020000C4 RID: 196
public class SeinInput
{
	// Token: 0x06000876 RID: 2166 RVA: 0x0002481B File Offset: 0x00022A1B
	public SeinInput(SeinCharacter sein)
	{
		this.m_sein = sein;
	}

	// Token: 0x170001DB RID: 475
	// (get) Token: 0x06000877 RID: 2167 RVA: 0x00024856 File Offset: 0x00022A56
	public int NormalizedHorizontal
	{
		get
		{
			if (this.Horizontal < -0.4f)
			{
				return -1;
			}
			if (this.Horizontal > 0.4f)
			{
				return 1;
			}
			return 0;
		}
	}

	// Token: 0x170001DC RID: 476
	// (get) Token: 0x06000878 RID: 2168 RVA: 0x0002487D File Offset: 0x00022A7D
	public int NormalizedVertical
	{
		get
		{
			if (this.Vertical < -0.6f)
			{
				return -1;
			}
			if (this.Vertical > 0.6f)
			{
				return 1;
			}
			return 0;
		}
	}

	// Token: 0x170001DD RID: 477
	// (get) Token: 0x06000879 RID: 2169 RVA: 0x000248A4 File Offset: 0x00022AA4
	public float Horizontal
	{
		get
		{
			return this.Axis.x;
		}
	}

	// Token: 0x170001DE RID: 478
	// (get) Token: 0x0600087A RID: 2170 RVA: 0x000248C0 File Offset: 0x00022AC0
	public float Vertical
	{
		get
		{
			return this.Axis.y;
		}
	}

	// Token: 0x170001DF RID: 479
	// (get) Token: 0x0600087B RID: 2171 RVA: 0x000248DB File Offset: 0x00022ADB
	public Vector2 Axis
	{
		get
		{
			return Core.Input.Axis;
		}
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x000248E2 File Offset: 0x00022AE2
	public Vector2 WorldToLocal(Vector2 speed)
	{
		return this.m_sein.PlatformBehaviour.PlatformMovement.WorldToLocal(speed);
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x000248FC File Offset: 0x00022AFC
	public void Update()
	{
		this.Down.Update(this.NormalizedVertical == -1);
		this.Up.Update(this.NormalizedVertical == 1);
		this.Left.Update(this.NormalizedHorizontal == -1);
		this.Right.Update(this.NormalizedHorizontal == 1);
	}

	// Token: 0x040006B3 RID: 1715
	public Core.Input.InputButtonProcessor Down = new Core.Input.InputButtonProcessor();

	// Token: 0x040006B4 RID: 1716
	public Core.Input.InputButtonProcessor Left = new Core.Input.InputButtonProcessor();

	// Token: 0x040006B5 RID: 1717
	public Core.Input.InputButtonProcessor Right = new Core.Input.InputButtonProcessor();

	// Token: 0x040006B6 RID: 1718
	public Core.Input.InputButtonProcessor Up = new Core.Input.InputButtonProcessor();

	// Token: 0x040006B7 RID: 1719
	public SeinCharacter m_sein;
}
