using System;
using System.Collections;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000167 RID: 359
public class LightningAnimator : MonoBehaviour
{
	// Token: 0x06000E56 RID: 3670 RVA: 0x000421CF File Offset: 0x000403CF
	private void Start()
	{
		this.m_nextLightningTime = this.GetNextLightningTime();
	}

	// Token: 0x06000E57 RID: 3671 RVA: 0x000421DD File Offset: 0x000403DD
	private void FixedUpdate()
	{
		if (Time.time < this.m_nextLightningTime)
		{
			return;
		}
		this.AnimateLightning();
		this.m_nextLightningTime = this.GetNextLightningTime();
	}

	// Token: 0x06000E58 RID: 3672 RVA: 0x00042204 File Offset: 0x00040404
	private void AnimateLightning()
	{
		this.LightningObject.GetComponent<LegacyHierarchyTransparancyAnimatorController>().Speed = 1f / UnityEngine.Random.Range(this.MinLightningDuration, this.MaxLightningDuration);
		this.LightningObject.SetActive(true);
		this.LightningObject.GetComponent<LegacyHierarchyTransparancyAnimatorController>().RestartAnimators();
		if (this.ThunderSound && this.SoundTarget)
		{
			if (Characters.Sein)
			{
				if (this.ZoneRectanglesContain(Characters.Sein.Position))
				{
					Sound.Play(this.ThunderSound.GetSound(null), this.SoundTarget.position, null);
				}
			}
			else
			{
				Sound.Play(this.ThunderSound.GetSound(null), this.SoundTarget.position, null);
			}
		}
	}

	// Token: 0x06000E59 RID: 3673 RVA: 0x000422DE File Offset: 0x000404DE
	private float GetNextLightningTime()
	{
		return Time.time + UnityEngine.Random.Range(this.MinTimeBetweenLightnings, this.MaxTimeBetweenLightnings);
	}

	// Token: 0x06000E5A RID: 3674 RVA: 0x000422F8 File Offset: 0x000404F8
	private IEnumerator ThunderControllerShake()
	{
		ControllerShakeLogic.Vibrate(1f, 0.25f);
		yield return new WaitForSeconds(0.37f);
		yield return null;
		yield break;
	}

	// Token: 0x06000E5B RID: 3675 RVA: 0x0004230C File Offset: 0x0004050C
	public bool ZoneRectanglesContain(Vector2 position)
	{
		if (this.SoundZones.Length == 0)
		{
			return true;
		}
		foreach (Transform transform in this.SoundZones)
		{
			Rect rect = default(Rect);
			rect.width = transform.lossyScale.x;
			rect.height = transform.lossyScale.y;
			rect.center = transform.position;
			if (rect.Contains(position))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000B84 RID: 2948
	public GameObject LightningObject;

	// Token: 0x04000B85 RID: 2949
	public float MinTimeBetweenLightnings = 1f;

	// Token: 0x04000B86 RID: 2950
	public float MaxTimeBetweenLightnings = 10f;

	// Token: 0x04000B87 RID: 2951
	public float MinLightningDuration = 0.1f;

	// Token: 0x04000B88 RID: 2952
	public float MaxLightningDuration = 0.3f;

	// Token: 0x04000B89 RID: 2953
	private float m_nextLightningTime;

	// Token: 0x04000B8A RID: 2954
	public Transform SoundTarget;

	// Token: 0x04000B8B RID: 2955
	public SoundProvider ThunderSound;

	// Token: 0x04000B8C RID: 2956
	public Transform[] SoundZones = new Transform[0];
}
