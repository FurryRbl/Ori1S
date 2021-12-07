using System;
using Game;
using UnityEngine;

// Token: 0x020003E3 RID: 995
public class CameraInfluencePositionZone : MonoBehaviour
{
	// Token: 0x1700047B RID: 1147
	// (get) Token: 0x06001B30 RID: 6960 RVA: 0x00074F68 File Offset: 0x00073168
	public Vector3 Offset
	{
		get
		{
			Vector3 a = base.transform.position - Characters.Sein.Controller.Transform.position;
			return a * this.DistanceInfluenceCurve.Evaluate(a.magnitude);
		}
	}

	// Token: 0x040017AD RID: 6061
	public AnimationCurve DistanceInfluenceCurve;
}
