using System;
using Game;
using UnityEngine;

// Token: 0x02000825 RID: 2085
[ExecuteInEditMode]
public class UberEnvirionmentParticle : MonoBehaviour
{
	// Token: 0x06002FC5 RID: 12229 RVA: 0x000CAA54 File Offset: 0x000C8C54
	public void OnDrawGizmosSelected()
	{
		Rect bounds = this.Bounds;
		Gizmos.DrawWireCube(bounds.center, new Vector3(bounds.width, bounds.height, 0f));
	}

	// Token: 0x06002FC6 RID: 12230 RVA: 0x000CAA94 File Offset: 0x000C8C94
	private void Update()
	{
		if (UI.Cameras.Current != null)
		{
			Vector2 v = UI.Cameras.Current.TargetHelperPosition + this.Offset;
			v.x = Mathf.Max(v.x, this.Bounds.xMin);
			v.y = Mathf.Max(v.y, this.Bounds.yMin);
			v.x = Mathf.Min(v.x, this.Bounds.xMax);
			v.y = Mathf.Min(v.y, this.Bounds.yMax);
			Vector3 position = v;
			position.z = base.transform.position.z;
			base.transform.position = position;
		}
	}

	// Token: 0x04002AFB RID: 11003
	public Vector2 Offset;

	// Token: 0x04002AFC RID: 11004
	public Rect Bounds;
}
