using System;
using Game;
using UnityEngine;

// Token: 0x02000452 RID: 1106
[ExecuteInEditMode]
public class TargetPositionFollower : MonoBehaviour
{
	// Token: 0x06001EAC RID: 7852 RVA: 0x0008714C File Offset: 0x0008534C
	private void FixedUpdate()
	{
		this.UpdateFollower();
	}

	// Token: 0x06001EAD RID: 7853 RVA: 0x00087154 File Offset: 0x00085354
	private void UpdateFollower()
	{
		if (this.TargetCamera && this.Target == null)
		{
			this.Target = UI.Cameras.Current.Camera.GetComponent<Camera>().transform;
		}
		this.m_transform = base.transform;
		if (this.Target != null)
		{
			this.m_transform.position = new Vector3(this.Target.position.x, this.Target.position.y, (!this.FollowZ) ? this.m_transform.position.z : this.Target.position.z);
		}
	}

	// Token: 0x04001A79 RID: 6777
	public Transform Target;

	// Token: 0x04001A7A RID: 6778
	public bool TargetCamera;

	// Token: 0x04001A7B RID: 6779
	public bool FollowZ = true;

	// Token: 0x04001A7C RID: 6780
	private Transform m_transform;
}
