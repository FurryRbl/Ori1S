using System;
using UnityEngine;

// Token: 0x02000934 RID: 2356
public class WaterReflectionRenderer : MonoBehaviour
{
	// Token: 0x06003419 RID: 13337 RVA: 0x000DB658 File Offset: 0x000D9858
	private void Start()
	{
		this.m_water = (UnityEngine.Object.FindObjectOfType(typeof(Water)) as Water);
	}

	// Token: 0x0600341A RID: 13338 RVA: 0x000DB674 File Offset: 0x000D9874
	private void LateUpdate()
	{
		Camera camera = base.GetComponent(typeof(Camera)) as Camera;
		base.transform.position = this.camera.transform.position;
		base.transform.rotation = this.camera.transform.rotation;
		camera.aspect = this.camera.aspect;
		float num = this.camera.transform.position.y - this.m_water.transform.position.y;
		base.transform.position = base.transform.position - new Vector3(0f, num * 2f, 0f);
		Vector3 vector = base.transform.rotation * new Vector3(0f, 0f, 1f);
	}

	// Token: 0x04002F19 RID: 12057
	public Camera camera;

	// Token: 0x04002F1A RID: 12058
	private Water m_water;
}
