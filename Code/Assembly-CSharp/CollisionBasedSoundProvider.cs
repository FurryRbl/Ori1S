using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001CA RID: 458
public class CollisionBasedSoundProvider : SoundProvider
{
	// Token: 0x060010AF RID: 4271 RVA: 0x0004C520 File Offset: 0x0004A720
	public SoundDescriptor GetSoundForCollision(Collision collision, IContext context)
	{
		if (this.ForceToVolumeCurve.Evaluate(collision.relativeVelocity.magnitude) == 0f)
		{
			return null;
		}
		Vector3 vector = Vector3.zero;
		for (int i = 0; i < collision.contacts.Length; i++)
		{
			ContactPoint contactPoint = collision.contacts[i];
			vector += contactPoint.normal;
		}
		vector.Normalize();
		for (int j = 0; j < this.SoundPairs.Count; j++)
		{
			CollisionMaterialSoundPair collisionMaterialSoundPair = this.SoundPairs[j];
			if (Vector3.Dot(vector, collisionMaterialSoundPair.Normal) > Mathf.Cos(collisionMaterialSoundPair.CosAngle * 57.29578f))
			{
				SoundDescriptor sound = collisionMaterialSoundPair.IndependantSoundProvider.GetSound(context);
				sound.Volume *= this.ForceToVolumeCurve.Evaluate(collision.relativeVelocity.magnitude);
				return sound;
			}
		}
		return null;
	}

	// Token: 0x060010B0 RID: 4272 RVA: 0x0004C628 File Offset: 0x0004A828
	public override SoundDescriptor GetSound(IContext context)
	{
		CollisionContext collisionContext = context as CollisionContext;
		if (collisionContext == null)
		{
			return null;
		}
		return this.GetSoundForCollision(collisionContext.Collision, collisionContext);
	}

	// Token: 0x04000E1D RID: 3613
	public List<CollisionMaterialSoundPair> SoundPairs;

	// Token: 0x04000E1E RID: 3614
	public AnimationCurve ForceToVolumeCurve;
}
