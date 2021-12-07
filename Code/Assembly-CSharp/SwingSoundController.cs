using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200004D RID: 77
public class SwingSoundController : MonoBehaviour
{
	// Token: 0x06000339 RID: 825 RVA: 0x0000D6AC File Offset: 0x0000B8AC
	public void Awake()
	{
		this.m_bounds = new Rect
		{
			width = base.transform.localScale.x,
			height = base.transform.localScale.y,
			center = base.transform.position
		};
	}

	// Token: 0x0600033A RID: 826 RVA: 0x0000D713 File Offset: 0x0000B913
	public void OnTriggerEnter(Collider collider)
	{
		if (collider.CompareTag("Player") && this.m_bounds.Contains(Characters.Current.Position))
		{
			this.m_characterInsideZone = true;
		}
	}

	// Token: 0x0600033B RID: 827 RVA: 0x0000D746 File Offset: 0x0000B946
	public void OnTriggerStay(Collider collider)
	{
		if (collider.CompareTag("Player") && this.m_bounds.Contains(Characters.Current.Position))
		{
			this.m_characterInsideZone = true;
		}
	}

	// Token: 0x0600033C RID: 828 RVA: 0x0000D77C File Offset: 0x0000B97C
	public void FixedUpdate()
	{
		if (Characters.Current == null)
		{
			return;
		}
		if (Characters.Current.IsOnGround)
		{
			this.m_characterJumping = false;
			if (!this.m_characterOnGround)
			{
				this.m_characterOnGround = true;
				if (this.m_characterInsideZone)
				{
					Sound.Play(this.OnLandSound.GetSound(null), Characters.Current.Position, null);
				}
			}
		}
		else
		{
			this.m_characterOnGround = false;
			if (Characters.Current.Speed.y > 0f)
			{
				if (!this.m_characterJumping)
				{
					this.m_characterJumping = true;
					if (this.m_characterInsideZone)
					{
						Sound.Play(this.OnJumpSound.GetSound(null), Characters.Current.Position, null);
					}
				}
			}
			else
			{
				this.m_characterJumping = false;
			}
		}
		this.m_characterInsideZone = false;
	}

	// Token: 0x04000260 RID: 608
	public SoundProvider OnLandSound;

	// Token: 0x04000261 RID: 609
	public SoundProvider OnJumpSound;

	// Token: 0x04000262 RID: 610
	private bool m_characterInsideZone;

	// Token: 0x04000263 RID: 611
	private bool m_characterOnGround;

	// Token: 0x04000264 RID: 612
	private bool m_characterJumping;

	// Token: 0x04000265 RID: 613
	private Rect m_bounds;
}
