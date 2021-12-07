using System;
using UnityEngine;

// Token: 0x020006FB RID: 1787
public class ParticlesSerialize : SaveSerialize
{
	// Token: 0x06002A8D RID: 10893 RVA: 0x000B68F8 File Offset: 0x000B4AF8
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			ParticleSystem component = base.GetComponent<ParticleSystem>();
			if (component)
			{
				component.Clear();
			}
			ParticleEmitter component2 = base.GetComponent<ParticleEmitter>();
			if (component2)
			{
				component2.ClearParticles();
			}
		}
	}
}
