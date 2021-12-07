using System;
using UnityEngine;

// Token: 0x02000427 RID: 1063
public class CharacterUpwardsDeceleration : CharacterState
{
	// Token: 0x17000501 RID: 1281
	// (get) Token: 0x06001DAA RID: 7594 RVA: 0x00083121 File Offset: 0x00081321
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x06001DAB RID: 7595 RVA: 0x0008312E File Offset: 0x0008132E
	public void Reset()
	{
		this.Deceleration = 0f;
	}

	// Token: 0x06001DAC RID: 7596 RVA: 0x0008313C File Offset: 0x0008133C
	public override void UpdateCharacterState()
	{
		if (this.PlatformBehaviour.PlatformMovement.IsSuspended)
		{
			return;
		}
		if (this.Deceleration == 0f)
		{
			return;
		}
		Vector3 v = this.PlatformMovement.LocalSpeed;
		if (v.y < 0f)
		{
			this.Deceleration = 0f;
		}
		v.y = MoonMath.Float.ClampedSubtract(v.y, this.Deceleration * Time.deltaTime, 0f, 0f);
		this.PlatformMovement.LocalSpeed = v;
	}

	// Token: 0x06001DAD RID: 7597 RVA: 0x000831D7 File Offset: 0x000813D7
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.Deceleration);
		base.Serialize(ar);
	}

	// Token: 0x04001991 RID: 6545
	public float Deceleration;

	// Token: 0x04001992 RID: 6546
	public PlatformBehaviour PlatformBehaviour;
}
