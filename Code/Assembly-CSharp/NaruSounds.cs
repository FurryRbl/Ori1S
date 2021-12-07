using System;
using Core;
using UnityEngine;

// Token: 0x0200003A RID: 58
public class NaruSounds : MonoBehaviour
{
	// Token: 0x170000AD RID: 173
	// (get) Token: 0x060002AA RID: 682 RVA: 0x0000B534 File Offset: 0x00009734
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.Naru.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x060002AB RID: 683 RVA: 0x0000B546 File Offset: 0x00009746
	private SurfaceMaterialType GroundSurfaceType
	{
		get
		{
			return SurfaceToSoundProviderMap.ColliderMaterialToSurfaceMaterialType(this.Naru.PlatformBehaviour.PlatformMovementListOfColliders.GroundCollider);
		}
	}

	// Token: 0x060002AC RID: 684 RVA: 0x0000B562 File Offset: 0x00009762
	public void Awake()
	{
		this.m_transform = base.transform;
	}

	// Token: 0x060002AD RID: 685 RVA: 0x0000B570 File Offset: 0x00009770
	public void OnJump()
	{
		Sound.Play(this.JumpSound.GetSoundForMaterial(this.GroundSurfaceType, null), this.m_transform.position, null);
		InstantiateUtility.Instantiate(this.FootstepsEffectPrefab, this.PlatformMovement.Position + 1.6f * (this.PlatformMovement.FeetPosition - this.PlatformMovement.Position), Quaternion.identity);
	}

	// Token: 0x060002AE RID: 686 RVA: 0x0000B5E8 File Offset: 0x000097E8
	public void OnLand()
	{
		InstantiateUtility.Instantiate(this.LandEffectPrefab, this.PlatformMovement.Position + 1.6f * (this.PlatformMovement.FeetPosition - this.PlatformMovement.Position), Quaternion.identity);
	}

	// Token: 0x060002AF RID: 687 RVA: 0x0000B63C File Offset: 0x0000983C
	public void HandleFootstepEvents()
	{
		if (this.PlatformMovement.IsOnGround && !this.PlatformMovement.HasWallLeft && !this.PlatformMovement.HasWallRight && !this.PlatformMovement.IsOnCeiling && this.PlatformMovement.MovingHorizontally)
		{
			this.m_nextStepTime -= Time.deltaTime * this.SoundsPerSecondOverSpeed.Evaluate(Mathf.Abs(this.PlatformMovement.LocalSpeedX));
			if (this.m_nextStepTime < 0f)
			{
				Sound.Play(this.FootstepsSounds.GetSoundForMaterial(this.GroundSurfaceType, null), this.PlatformMovement.Position, null);
				GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.FootstepsEffectPrefab, this.PlatformMovement.Position + 1.6f * (this.PlatformMovement.FeetPosition - this.PlatformMovement.Position), Quaternion.identity);
				gameObject.transform.eulerAngles = new Vector3(0f, 0f, MoonMath.Angle.AngleFromVector(this.PlatformMovement.LocalSpeed));
				this.m_nextStepTime = 1f;
			}
		}
	}

	// Token: 0x040001E7 RID: 487
	public GameObject FootstepsEffectPrefab;

	// Token: 0x040001E8 RID: 488
	public SurfaceToSoundProviderMap FootstepsSounds;

	// Token: 0x040001E9 RID: 489
	public GameObject JumpEffectPrefab;

	// Token: 0x040001EA RID: 490
	public SurfaceToSoundProviderMap JumpSound;

	// Token: 0x040001EB RID: 491
	public GameObject LandEffectPrefab;

	// Token: 0x040001EC RID: 492
	public Naru Naru;

	// Token: 0x040001ED RID: 493
	public AnimationCurve SoundsPerSecondOverSpeed;

	// Token: 0x040001EE RID: 494
	private float m_nextStepTime;

	// Token: 0x040001EF RID: 495
	private Transform m_transform;
}
