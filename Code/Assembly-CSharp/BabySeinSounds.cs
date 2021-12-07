using System;
using Core;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class BabySeinSounds : MonoBehaviour
{
	// Token: 0x17000023 RID: 35
	// (get) Token: 0x06000091 RID: 145 RVA: 0x000044BC File Offset: 0x000026BC
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.BabySein.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x06000092 RID: 146 RVA: 0x000044CE File Offset: 0x000026CE
	private SurfaceMaterialType GroundSurfaceType
	{
		get
		{
			return SurfaceToSoundProviderMap.ColliderMaterialToSurfaceMaterialType(this.BabySein.PlatformBehaviour.PlatformMovementListOfColliders.GroundCollider);
		}
	}

	// Token: 0x06000093 RID: 147 RVA: 0x000044EA File Offset: 0x000026EA
	public void Awake()
	{
		this.m_transform = base.transform;
	}

	// Token: 0x06000094 RID: 148 RVA: 0x000044F8 File Offset: 0x000026F8
	public void OnJump()
	{
		Sound.Play(this.JumpSound.GetSoundForMaterial(this.GroundSurfaceType, null), this.m_transform.position, null);
		InstantiateUtility.Instantiate(this.FootstepsEffectPrefab, this.PlatformMovement.FeetPosition, Quaternion.identity);
	}

	// Token: 0x06000095 RID: 149 RVA: 0x00004548 File Offset: 0x00002748
	public void OnLand()
	{
		Sound.Play(this.LandSound.GetSoundForMaterial(this.GroundSurfaceType, null), this.m_transform.position, null);
		InstantiateUtility.Instantiate(this.LandEffectPrefab, this.PlatformMovement.FeetPosition, Quaternion.identity);
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00004598 File Offset: 0x00002798
	public void HandleFootstepEvents()
	{
		if (this.PlatformMovement.IsOnGround && !this.PlatformMovement.HasWallLeft && !this.PlatformMovement.HasWallRight && !this.PlatformMovement.IsOnCeiling && this.PlatformMovement.MovingHorizontally)
		{
			this.m_nextStepTime -= Time.deltaTime * this.SoundsPerSecondOverSpeed.Evaluate(Mathf.Abs(this.PlatformMovement.LocalSpeedX));
			if (this.m_nextStepTime < 0f)
			{
				Sound.Play(this.FootstepsSounds.GetSoundForMaterial(this.GroundSurfaceType, null), this.PlatformMovement.Position, null);
				GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.FootstepsEffectPrefab, this.PlatformMovement.FeetPosition, Quaternion.identity);
				gameObject.transform.eulerAngles = new Vector3(0f, 0f, MoonMath.Angle.AngleFromVector(this.PlatformMovement.LocalSpeed));
				this.m_nextStepTime = 1f;
			}
		}
	}

	// Token: 0x04000098 RID: 152
	public BabySein BabySein;

	// Token: 0x04000099 RID: 153
	public GameObject FootstepsEffectPrefab;

	// Token: 0x0400009A RID: 154
	public SurfaceToSoundProviderMap FootstepsSounds;

	// Token: 0x0400009B RID: 155
	public GameObject JumpEffectPrefab;

	// Token: 0x0400009C RID: 156
	public SurfaceToSoundProviderMap JumpSound;

	// Token: 0x0400009D RID: 157
	public GameObject LandEffectPrefab;

	// Token: 0x0400009E RID: 158
	public SurfaceToSoundProviderMap LandSound;

	// Token: 0x0400009F RID: 159
	public AnimationCurve SoundsPerSecondOverSpeed;

	// Token: 0x040000A0 RID: 160
	private float m_nextStepTime;

	// Token: 0x040000A1 RID: 161
	private Transform m_transform;
}
