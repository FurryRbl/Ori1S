using System;
using Game;
using UnityEngine;

// Token: 0x020003DA RID: 986
public class CameraChaseTarget : MonoBehaviour
{
	// Token: 0x06001B04 RID: 6916 RVA: 0x00073B8B File Offset: 0x00071D8B
	public void Start()
	{
		this.m_transform = base.transform;
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		this.UpdateCameraLastPosition();
	}

	// Token: 0x06001B05 RID: 6917 RVA: 0x00073BAB File Offset: 0x00071DAB
	public void UpdateCameraLastPosition()
	{
		this.m_cameraLastPosition = UI.Cameras.Current.GameplayPuppet.transform.position;
	}

	// Token: 0x06001B06 RID: 6918 RVA: 0x00073BC8 File Offset: 0x00071DC8
	public void UpdateChase()
	{
		Vector3 position = this.Target.position;
		Vector3 a = position - this.m_transform.localPosition;
		Vector3 cameraSpeed = this.m_cameraSpeed;
		if (this.Target != null)
		{
			if (this.FollowTargetX)
			{
				this.m_cameraSpeed.x = a.x * this.SpeedRatio;
			}
			if (this.FollowTargetY)
			{
				this.m_cameraSpeed.y = a.y * this.SpeedRatio;
			}
			if (this.FollowTargetZ)
			{
				this.m_cameraSpeed.z = a.z * this.SpeedRatio;
			}
		}
		Vector3 position2 = UI.Cameras.Current.GameplayPuppet.transform.position;
		Vector3 vector = this.m_cameraLastPosition - position2;
		a = vector;
		a.z = 0f;
		a.x *= this.CameraSpeedMultiplier.x;
		a.y *= this.CameraSpeedMultiplier.y;
		this.m_rigidbody.velocity = this.m_cameraSpeed + a / Time.deltaTime;
		this.m_cameraLastPosition = UI.Cameras.Current.GameplayPuppet.transform.position + this.m_rigidbody.velocity * Time.deltaTime;
	}

	// Token: 0x06001B07 RID: 6919 RVA: 0x00073D30 File Offset: 0x00071F30
	public void GoToTarget()
	{
		Vector3 localPosition = this.m_transform.localPosition;
		if (this.FollowTargetX)
		{
			localPosition.x = this.Target.position.x;
		}
		if (this.FollowTargetY)
		{
			localPosition.x = this.Target.position.y;
		}
		if (this.FollowTargetZ)
		{
			localPosition.x = this.Target.position.z;
		}
		this.m_transform.localPosition = localPosition;
	}

	// Token: 0x17000478 RID: 1144
	// (get) Token: 0x06001B08 RID: 6920 RVA: 0x00073DC4 File Offset: 0x00071FC4
	// (set) Token: 0x06001B09 RID: 6921 RVA: 0x00073DCC File Offset: 0x00071FCC
	public int IgnoreSmoothingForAFrame { get; set; }

	// Token: 0x0400176E RID: 5998
	public Vector2 CameraSpeedMultiplier = Vector2.one;

	// Token: 0x0400176F RID: 5999
	private Vector3 m_lastTargetPosition;

	// Token: 0x04001770 RID: 6000
	public bool SilkySmooth = true;

	// Token: 0x04001771 RID: 6001
	public float DistanceRequiredForSmoothing = 4f;

	// Token: 0x04001772 RID: 6002
	public float SmoothingDuration = 1f;

	// Token: 0x04001773 RID: 6003
	public AnimationCurve SmoothingAccelerationCurveOverTime;

	// Token: 0x04001774 RID: 6004
	private Vector3 m_cameraLastPosition;

	// Token: 0x04001775 RID: 6005
	private Vector3 m_cameraSpeed;

	// Token: 0x04001776 RID: 6006
	public bool FollowTargetX = true;

	// Token: 0x04001777 RID: 6007
	public bool FollowTargetY = true;

	// Token: 0x04001778 RID: 6008
	public bool FollowTargetZ = true;

	// Token: 0x04001779 RID: 6009
	public float SpeedRatio = 6f;

	// Token: 0x0400177A RID: 6010
	public Transform Target;

	// Token: 0x0400177B RID: 6011
	private Transform m_transform;

	// Token: 0x0400177C RID: 6012
	private Rigidbody m_rigidbody;
}
