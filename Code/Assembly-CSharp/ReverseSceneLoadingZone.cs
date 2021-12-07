using System;
using UnityEngine;

// Token: 0x02000711 RID: 1809
[ExecuteInEditMode]
[Serializable]
public class ReverseSceneLoadingZone : MonoBehaviour
{
	// Token: 0x170006DF RID: 1759
	// (get) Token: 0x06002AFB RID: 11003 RVA: 0x000B804A File Offset: 0x000B624A
	public SceneRoot SceneRoot
	{
		get
		{
			return SceneRoot.FindFromTransform(base.transform);
		}
	}

	// Token: 0x170006E0 RID: 1760
	// (get) Token: 0x06002AFC RID: 11004 RVA: 0x000B8058 File Offset: 0x000B6258
	public Rect Rectangle
	{
		get
		{
			Vector3 position = base.transform.position;
			Vector3 vector = MoonMath.Vector.Abs(base.transform.lossyScale);
			return new Rect
			{
				width = vector.x,
				height = vector.y,
				center = position
			};
		}
	}

	// Token: 0x0400264D RID: 9805
	public SceneMetaData SceneToLoad;
}
