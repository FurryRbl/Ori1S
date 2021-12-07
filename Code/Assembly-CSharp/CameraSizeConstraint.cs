using System;
using UnityEngine;

// Token: 0x020003F2 RID: 1010
public class CameraSizeConstraint : MonoBehaviour
{
	// Token: 0x06001B77 RID: 7031 RVA: 0x000764DC File Offset: 0x000746DC
	private void Start()
	{
		float fieldOfView = this.Camera.fieldOfView;
		float num = this.FixedWidth / 2f / Mathf.Tan(fieldOfView / 2f * 0.017453292f) / this.Camera.aspect;
		Vector3 localPosition = this.CameraOffset.localPosition;
		localPosition.z = -num;
		this.CameraOffset.localPosition = localPosition;
	}

	// Token: 0x040017E3 RID: 6115
	public float FixedWidth;

	// Token: 0x040017E4 RID: 6116
	public Transform CameraOffset;

	// Token: 0x040017E5 RID: 6117
	public Camera Camera;
}
