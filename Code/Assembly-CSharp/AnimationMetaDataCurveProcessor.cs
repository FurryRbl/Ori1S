using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000396 RID: 918
public class AnimationMetaDataCurveProcessor : UnityModelAnimationCurveProcessor
{
	// Token: 0x060019D5 RID: 6613 RVA: 0x0006E7DB File Offset: 0x0006C9DB
	public AnimationMetaDataCurveProcessor(AnimationMetaData animationMetaData)
	{
		this.AnimationMetaData = animationMetaData;
	}

	// Token: 0x060019D6 RID: 6614 RVA: 0x0006E7EC File Offset: 0x0006C9EC
	public override void OnPreProcessModel()
	{
		this.m_cameraTarget = null;
		this.m_camera = null;
		List<Transform> list = new List<Transform>();
		foreach (Transform transform in this.GameObject.GetComponentsInChildren<Transform>())
		{
			if (transform.name == this.AnimationMetaData.CameraName)
			{
				this.m_camera = transform;
			}
			if (transform.name == "cameraTarget")
			{
				this.m_cameraTarget = transform;
			}
			if (transform.name.StartsWith("#"))
			{
				list.Add(transform);
			}
		}
		this.m_data = list.ToDictionary((Transform i) => i, (Transform i) => new AnimationMetaData.AnimationData
		{
			Name = i.name
		});
		this.AnimationMetaData.PlaneSize = Vector2.one * 2f * this.AnimationMetaData.CameraTargetDistance * Mathf.Tan(this.AnimationMetaData.CameraFieldOfView * 0.5f * 0.017453292f);
		AnimationMetaData animationMetaData = this.AnimationMetaData;
		animationMetaData.PlaneSize.x = animationMetaData.PlaneSize.x * this.AnimationMetaData.AspectRatio;
		if (this.AnimationMetaData.Animation != null)
		{
			Atlas atlas;
			AtlasSpriteTexture textureAtIndex = this.AnimationMetaData.Animation.GetTextureAtIndex(0f, out atlas);
			this.AnimationMetaData.AspectRatio = textureAtIndex.OriginalSize.x / textureAtIndex.OriginalSize.y;
		}
	}

	// Token: 0x060019D7 RID: 6615 RVA: 0x0006E994 File Offset: 0x0006CB94
	public override void OnPostProcessModel()
	{
		this.AnimationMetaData.Data = new List<AnimationMetaData.AnimationData>(this.m_data.Values);
		for (int i = 0; i < this.AnimationMetaData.Data.Count; i++)
		{
			this.AnimationMetaData.Data[i].PositionX.Duration = this.AnimationMetaData.CameraData.PositionX.Duration;
			this.AnimationMetaData.Data[i].PositionY.Duration = this.AnimationMetaData.CameraData.PositionX.Duration;
			this.AnimationMetaData.Data[i].PositionZ.Duration = this.AnimationMetaData.CameraData.PositionX.Duration;
			this.AnimationMetaData.Data[i].ScaleX.Duration = this.AnimationMetaData.CameraData.PositionX.Duration;
			this.AnimationMetaData.Data[i].ScaleY.Duration = this.AnimationMetaData.CameraData.PositionX.Duration;
			this.AnimationMetaData.Data[i].RotationZ.Duration = this.AnimationMetaData.CameraData.PositionX.Duration;
		}
	}

	// Token: 0x060019D8 RID: 6616 RVA: 0x0006EB00 File Offset: 0x0006CD00
	public override void OnSampleFrame(int frame)
	{
		if (this.AnimationMetaData.ViewMode == AnimationMetaData.ViewModes.Left)
		{
			this.GameObject.transform.eulerAngles = new Vector3(0f, 90f, 0f);
		}
		if (this.AnimationMetaData.ViewMode == AnimationMetaData.ViewModes.Right)
		{
			this.GameObject.transform.eulerAngles = new Vector3(0f, -90f, 0f);
		}
		if (this.AnimationMetaData.ViewMode == AnimationMetaData.ViewModes.Front)
		{
			this.GameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
		}
		if (this.m_camera == null)
		{
		}
		float num = Vector3.Distance(this.m_camera.position, this.m_cameraTarget.position);
		Matrix4x4 m = Matrix4x4.TRS(this.m_camera.position, this.m_camera.rotation, Vector3.one);
		Vector3 vector = m.MultiplyPoint(this.m_camera.forward * num);
		this.AnimationMetaData.CameraData.PositionX.Values.Add(vector.x / this.AnimationMetaData.PlaneSize.x);
		this.AnimationMetaData.CameraData.PositionY.Values.Add(vector.y / this.AnimationMetaData.PlaneSize.y);
		this.AnimationMetaData.CameraData.PositionZ.Values.Add(num / this.AnimationMetaData.CameraTargetDistance);
		this.AnimationMetaData.CameraData.RotationZ.Values.Add(0f);
		this.AnimationMetaData.Camera.PositionX.Values.Add(this.m_camera.position.x);
		this.AnimationMetaData.Camera.PositionY.Values.Add(this.m_camera.position.y);
		this.AnimationMetaData.Camera.PositionZ.Values.Add(-this.m_camera.position.z);
		float fov = this.AnimationMetaData.CameraFieldOfView * 2f / this.AnimationMetaData.AspectRatio;
		Matrix4x4 lhs;
		if (this.AnimationMetaData.Perspective)
		{
			lhs = Matrix4x4.Perspective(fov, this.AnimationMetaData.AspectRatio, 1f, 1000f);
		}
		else
		{
			float x = this.AnimationMetaData.PlaneSize.x;
			float y = this.AnimationMetaData.PlaneSize.y;
			lhs = Matrix4x4.Ortho(-x, x, -y, y, 1f, 1000f);
		}
		Matrix4x4 lhs2 = lhs * Matrix4x4.Inverse(m);
		foreach (Transform transform in this.m_data.Keys)
		{
			vector = (lhs2 * transform.localToWorldMatrix).MultiplyPoint(Vector3.zero);
			this.m_data[transform].PositionX.Values.Add(-vector.x);
			this.m_data[transform].PositionY.Values.Add(vector.y);
			this.m_data[transform].PositionZ.Values.Add(vector.z);
			this.m_data[transform].ScaleX.Values.Add(transform.localScale.x);
			this.m_data[transform].ScaleY.Values.Add(transform.localScale.y);
			this.m_data[transform].RotationZ.Values.Add(transform.eulerAngles.z);
		}
	}

	// Token: 0x04001630 RID: 5680
	public AnimationMetaData AnimationMetaData;

	// Token: 0x04001631 RID: 5681
	private Transform m_camera;

	// Token: 0x04001632 RID: 5682
	private Transform m_cameraTarget;

	// Token: 0x04001633 RID: 5683
	private Dictionary<Transform, AnimationMetaData.AnimationData> m_data;
}
