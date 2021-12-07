using System;
using System.Reflection;
using Game;
using UnityEngine;

// Token: 0x0200019D RID: 413
[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{
	// Token: 0x06000FE3 RID: 4067 RVA: 0x00048B10 File Offset: 0x00046D10
	public void OnEnable()
	{
		this.m_cameraTransform = this.Camera.transform;
		this.m_childTransform = this.CameraShake.Target;
		UI.Cameras.Manager.RegisterCamera(this);
		this.PuppetController.UpdatePuppet();
		this.CameraPostProcessing.Advance(float.PositiveInfinity);
		if (Application.isPlaying)
		{
			Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
		}
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x00048B8A File Offset: 0x00046D8A
	public void OnDisable()
	{
		UI.Cameras.Manager.UnregisterCamera(this);
		if (Application.isPlaying)
		{
			Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
		}
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x00048BBC File Offset: 0x00046DBC
	public void OnGameReset()
	{
		this.CameraPostProcessing.AdditiveSettings.AdditiveContrast.Brightness = 0f;
		this.CameraPostProcessing.AdditiveSettings.AdditiveContrast.Contrast = 0f;
		this.CameraPostProcessing.AdditiveSettings.AdditiveBloomIntensity = 0f;
		this.CameraPostProcessing.AdditiveSettings.AdditiveVignettingIntensity = 0f;
		this.CameraPostProcessing.AdditiveSettings.AdditiveBloomThreshhold = 0f;
		this.CameraPostProcessing.Apply();
	}

	// Token: 0x170002D0 RID: 720
	// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x00048C47 File Offset: 0x00046E47
	public Vector3 Position
	{
		get
		{
			return this.m_childTransform.position;
		}
	}

	// Token: 0x170002D1 RID: 721
	// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x00048C54 File Offset: 0x00046E54
	public Quaternion Rotation
	{
		get
		{
			return this.m_childTransform.rotation;
		}
	}

	// Token: 0x06000FE8 RID: 4072 RVA: 0x00048C61 File Offset: 0x00046E61
	public bool InsideFrustum(Bounds bounds)
	{
		return GeometryUtility.TestPlanesAABB(this.FrustrumPlanes, bounds);
	}

	// Token: 0x170002D2 RID: 722
	// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x00048C6F File Offset: 0x00046E6F
	private Plane[] FrustrumPlanes
	{
		get
		{
			this.UpdateFrustrumPlanes();
			return this.m_frustrumPlanes;
		}
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x00048C80 File Offset: 0x00046E80
	public void UpdateFrustrumPlanes()
	{
		if (this.m_frustrumPlanes == null || this.m_lastFrustumUpdate != Time.renderedFrameCount)
		{
			Vector3 position = this.m_cameraTransform.position;
			this.m_cameraTransform.position = this.Position;
			Matrix4x4 arg = this.Camera.projectionMatrix * this.Camera.worldToCameraMatrix;
			if (this.m_updateFunc == null)
			{
				MethodInfo method = typeof(GeometryUtility).GetMethod("Internal_ExtractPlanes", BindingFlags.Static | BindingFlags.NonPublic);
				this.m_updateFunc = (Action<Plane[], Matrix4x4>)Delegate.CreateDelegate(typeof(Action<Plane[], Matrix4x4>), method);
			}
			if (this.m_frustrumPlanes == null)
			{
				this.m_frustrumPlanes = new Plane[6];
			}
			this.m_updateFunc(this.m_frustrumPlanes, arg);
			this.m_cameraTransform.position = position;
			this.m_lastFrustumUpdate = Time.renderedFrameCount;
		}
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x00048D5E File Offset: 0x00046F5E
	public void FixedUpdate()
	{
		this.CameraPostProcessing.Advance(Time.deltaTime);
		this.PuppetController.UpdatePuppet();
		this.UpdateSpeed();
	}

	// Token: 0x06000FEC RID: 4076 RVA: 0x00048D84 File Offset: 0x00046F84
	public void UpdateCamera()
	{
		this.CameraShake.UpdateOffset();
		this.PuppetController.UpdatePuppet();
		this.m_cameraTransform.position = this.Position;
		this.m_cameraTransform.rotation = this.Rotation;
		this.CameraPostProcessing.UberPostProcess.Speed = this.Speed;
		this.Camera.fieldOfView = this.FieldOfView;
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x00048DF0 File Offset: 0x00046FF0
	public void UpdateSpeed()
	{
		if (this.m_firstFixed)
		{
			this.m_lastPosition = this.m_childTransform.position;
			this.m_firstFixed = false;
		}
		Vector3 a = (this.m_childTransform.position - this.m_lastPosition) / Time.fixedDeltaTime;
		this.Speed = a * 0.5f + this.m_lastSpeed * 0.5f;
		if (this.Speed.magnitude > 200f)
		{
			this.Speed = Vector2.zero;
		}
		this.m_lastPosition = this.m_childTransform.position;
		this.m_lastSpeed = this.Speed;
	}

	// Token: 0x04000D08 RID: 3336
	public Camera Camera;

	// Token: 0x04000D09 RID: 3337
	public CameraPostProcessing CameraPostProcessing;

	// Token: 0x04000D0A RID: 3338
	public CameraPuppetController PuppetController;

	// Token: 0x04000D0B RID: 3339
	public CameraShakeLogic CameraShake;

	// Token: 0x04000D0C RID: 3340
	private Transform m_childTransform;

	// Token: 0x04000D0D RID: 3341
	private Transform m_cameraTransform;

	// Token: 0x04000D0E RID: 3342
	public Vector3 Speed;

	// Token: 0x04000D0F RID: 3343
	private Vector3 m_lastSpeed;

	// Token: 0x04000D10 RID: 3344
	private bool m_firstFixed = true;

	// Token: 0x04000D11 RID: 3345
	private Plane[] m_frustrumPlanes;

	// Token: 0x04000D12 RID: 3346
	private int m_lastFrustumUpdate = -1;

	// Token: 0x04000D13 RID: 3347
	public float FieldOfView = 60f;

	// Token: 0x04000D14 RID: 3348
	private Action<Plane[], Matrix4x4> m_updateFunc;

	// Token: 0x04000D15 RID: 3349
	private Vector3 m_lastPosition;

	// Token: 0x020003DC RID: 988
	// (Invoke) Token: 0x06001B10 RID: 6928
	private delegate void UpdatePlaneFunc(Plane[] planes, ref Matrix4x4 trans);
}
