using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000549 RID: 1353
[Serializable]
public class SpiritFlameProjectileOffsetGenerator
{
	// Token: 0x06002374 RID: 9076 RVA: 0x0009B030 File Offset: 0x00099230
	public Vector3 GenerateSpiritFlameProjectileOffset(Transform transform, Vector3 position)
	{
		SpiritFlameProjectileOffsetGenerator.SphereGroup sphereGroup = null;
		float num = float.PositiveInfinity;
		foreach (SpiritFlameProjectileOffsetGenerator.SphereGroup sphereGroup2 in this.SphereGroups)
		{
			foreach (SpiritFlameProjectileOffsetGenerator.Sphere sphere in sphereGroup2.Spheres)
			{
				float sqrMagnitude = (transform.TransformPoint(sphere.Position) - position).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num = sqrMagnitude;
					sphereGroup = sphereGroup2;
				}
			}
		}
		if (sphereGroup == null)
		{
			return Vector3.zero;
		}
		SpiritFlameProjectileOffsetGenerator.Sphere randomListItem = FixedRandom.GetRandomListItem<SpiritFlameProjectileOffsetGenerator.Sphere>(sphereGroup.Spheres, 0);
		Vector3 position2 = randomListItem.Position + Utility.Rotate(Vector2.up * randomListItem.Radius * FixedRandom.Values[0], 360f * FixedRandom.Values[1]);
		return transform.TransformPoint(position2) - transform.position;
	}

	// Token: 0x04001DBF RID: 7615
	public List<SpiritFlameProjectileOffsetGenerator.SphereGroup> SphereGroups = new List<SpiritFlameProjectileOffsetGenerator.SphereGroup>();

	// Token: 0x0200054A RID: 1354
	[Serializable]
	public class SphereGroup
	{
		// Token: 0x04001DC0 RID: 7616
		public List<SpiritFlameProjectileOffsetGenerator.Sphere> Spheres = new List<SpiritFlameProjectileOffsetGenerator.Sphere>();
	}

	// Token: 0x0200054B RID: 1355
	[Serializable]
	public class Sphere
	{
		// Token: 0x04001DC1 RID: 7617
		public Vector2 Position = Vector2.one;

		// Token: 0x04001DC2 RID: 7618
		public float Radius = 1f;
	}
}
