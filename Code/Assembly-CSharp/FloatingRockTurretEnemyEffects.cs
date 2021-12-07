using System;
using UnityEngine;

// Token: 0x020005A7 RID: 1447
[Serializable]
public class FloatingRockTurretEnemyEffects : MonoBehaviour
{
	// Token: 0x06002508 RID: 9480 RVA: 0x000A1979 File Offset: 0x0009FB79
	public void BeginCharge()
	{
		if (this.ChargingEmitter)
		{
			this.ChargingEmitter.emit = true;
		}
		if (this.ChargingAnimator)
		{
			this.ChargingAnimator.AnimatorDriver.ContinueForward();
		}
	}

	// Token: 0x06002509 RID: 9481 RVA: 0x000A19B7 File Offset: 0x0009FBB7
	public void StopCharge()
	{
		if (this.ChargingEmitter)
		{
			this.ChargingEmitter.emit = false;
		}
		if (this.ChargingAnimator)
		{
			this.ChargingAnimator.AnimatorDriver.ContinueBackwards();
		}
	}

	// Token: 0x0600250A RID: 9482 RVA: 0x000A19F5 File Offset: 0x0009FBF5
	public void OnShoot()
	{
	}

	// Token: 0x04001F7B RID: 8059
	public ParticleEmitter ChargingEmitter;

	// Token: 0x04001F7C RID: 8060
	public BaseAnimator ChargingAnimator;
}
