using System;
using UnityEngine;

// Token: 0x020009A1 RID: 2465
public class SolidSlopeCollision : MonoBehaviour
{
	// Token: 0x060035B8 RID: 13752 RVA: 0x000E1734 File Offset: 0x000DF934
	private void Awake()
	{
		Vector3 b = base.transform.TransformPoint(Vector2.zero);
		Vector3 a = base.transform.TransformPoint(Vector2.one);
		Vector3 v = a - b;
		float z = MoonMath.Angle.AngleFromVector(v);
		float magnitude = v.magnitude;
		GameObject gameObject = new GameObject(base.gameObject.name + "Collision");
		gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, z);
		gameObject.transform.localPosition = (a + b) / 2f;
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.parent = base.transform.parent;
		bool flag = false;
		if (base.transform.lossyScale.x < 0f)
		{
			flag = !flag;
		}
		if (base.transform.lossyScale.y < 0f)
		{
			flag = !flag;
		}
		BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
		boxCollider.size = new Vector3(magnitude, 0.5f, 1f);
		boxCollider.center = new Vector3(0f, (float)((!flag) ? 1 : -1) * 0.5f * 0.5f, 0f);
	}
}
