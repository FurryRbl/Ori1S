using System;
using Game;
using UnityEngine;

// Token: 0x02000778 RID: 1912
public class FloatProviderZone : MonoBehaviour
{
	// Token: 0x06002C60 RID: 11360 RVA: 0x000BEBC4 File Offset: 0x000BCDC4
	public float GetValue()
	{
		Vector3 position = Characters.Current.Transform.position;
		return this.XValue(position.x) * this.YValue(position.y);
	}

	// Token: 0x06002C61 RID: 11361 RVA: 0x000BEBFC File Offset: 0x000BCDFC
	private float XValue(float x)
	{
		if (x < base.transform.position.x - base.transform.lossyScale.x / 2f)
		{
			return Math.Max(0f, 1f - (base.transform.position.x - base.transform.lossyScale.x / 2f - x) / this.fallOffDistance);
		}
		if (x > base.transform.position.x + base.transform.lossyScale.x / 2f)
		{
			return Math.Max(0f, 1f - (x - (base.transform.position.x + base.transform.lossyScale.x / 2f)) / this.fallOffDistance);
		}
		return 1f;
	}

	// Token: 0x06002C62 RID: 11362 RVA: 0x000BED08 File Offset: 0x000BCF08
	private float YValue(float y)
	{
		if (y < base.transform.position.y - base.transform.lossyScale.y / 2f)
		{
			return Math.Max(0f, 1f - (base.transform.position.y - base.transform.lossyScale.y / 2f - y) / this.fallOffDistance);
		}
		if (y > base.transform.position.y + base.transform.lossyScale.y / 2f)
		{
			return Math.Max(0f, 1f - (y - (base.transform.position.y + base.transform.lossyScale.y / 2f)) / this.fallOffDistance);
		}
		return 1f;
	}

	// Token: 0x0400283A RID: 10298
	public float fallOffDistance = 3f;
}
