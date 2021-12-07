using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000967 RID: 2407
public class EnableParticleEmitterOnStart : MonoBehaviour
{
	// Token: 0x060034DE RID: 13534 RVA: 0x000DE052 File Offset: 0x000DC252
	public void Awake()
	{
		base.GetComponent<ParticleSystem>().enableEmission = false;
	}

	// Token: 0x060034DF RID: 13535 RVA: 0x000DE060 File Offset: 0x000DC260
	private IEnumerator Start()
	{
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		base.GetComponent<ParticleSystem>().enableEmission = true;
		yield break;
	}
}
