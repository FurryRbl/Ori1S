using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200001A RID: 26
[ExecuteInEditMode]
public class GameplayCamera : MonoBehaviour, ISuspendable
{
	// Token: 0x17000064 RID: 100
	// (get) Token: 0x06000161 RID: 353 RVA: 0x00006B8C File Offset: 0x00004D8C
	public Camera Camera
	{
		get
		{
			return this.Controller.Camera;
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x06000162 RID: 354 RVA: 0x00006B99 File Offset: 0x00004D99
	public Transform GameplayPuppet
	{
		get
		{
			return this.Controller.PuppetController.GameplayPuppet;
		}
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x06000163 RID: 355 RVA: 0x00006BAB File Offset: 0x00004DAB
	public CameraPostProcessing CameraPostProcessing
	{
		get
		{
			return this.Controller.CameraPostProcessing;
		}
	}

	// Token: 0x06000164 RID: 356 RVA: 0x00006BB8 File Offset: 0x00004DB8
	public void LockSmoothScrollingForAFrame()
	{
		this.ChaseTarget.IgnoreSmoothingForAFrame = 2;
	}

	// Token: 0x06000165 RID: 357 RVA: 0x00006BC8 File Offset: 0x00004DC8
	public bool IsOnScreen(Vector3 position)
	{
		position.z = 0f;
		return this.CameraBoundingBox.Contains(position);
	}

	// Token: 0x06000166 RID: 358 RVA: 0x00006BF0 File Offset: 0x00004DF0
	public bool IsOnScreenPadded(Vector3 position, float padding)
	{
		Bounds cameraBoundingBox = this.CameraBoundingBox;
		cameraBoundingBox.Expand(padding);
		position.z = 0f;
		return cameraBoundingBox.Contains(position);
	}

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x06000167 RID: 359 RVA: 0x00006C20 File Offset: 0x00004E20
	public float CameraWidthWorldUnits
	{
		get
		{
			float aspect = this.Camera.aspect;
			float num = -this.Camera.transform.position.z;
			return 2f * num * Mathf.Tan(this.Camera.fieldOfView * 0.5f * 0.017453292f) * aspect;
		}
	}

	// Token: 0x06000168 RID: 360 RVA: 0x00006C7C File Offset: 0x00004E7C
	public void MoveToTarget(Vector3 target, float duration, bool ignoreScrollLock)
	{
		this.m_straightLineMotionTargetIsPlayer = false;
		this.Motion = GameplayCamera.MotionType.Move;
		if (!ignoreScrollLock)
		{
			Vector3 position = base.transform.position;
			base.transform.position = target;
			Vector3 b = this.ScrollLockConstraint.CalculateConstraintOffset(target);
			base.transform.position = position;
			target += b;
		}
		this.StraightLineMotion.MoveToTarget(target, duration);
	}

	// Token: 0x06000169 RID: 361 RVA: 0x00006CE4 File Offset: 0x00004EE4
	public void MoveToTargetCharacter(float duration)
	{
		if (duration == 0f)
		{
			this.Motion = GameplayCamera.MotionType.Chase;
			this.ChaseTarget.UpdateCameraLastPosition();
			this.MoveCameraToTargetInstantly(true);
		}
		else
		{
			this.MoveToTarget(Characters.Current.Position, duration, false);
			this.StraightLineMotion.OnMotionFinishedEvent += this.GoToChaseMode;
		}
		this.m_straightLineMotionTargetIsPlayer = true;
	}

	// Token: 0x0600016A RID: 362 RVA: 0x00006D4A File Offset: 0x00004F4A
	public void GoToChaseMode()
	{
		this.StraightLineMotion.OnMotionFinishedEvent -= this.GoToChaseMode;
		this.Motion = GameplayCamera.MotionType.Chase;
		this.ChaseTarget.UpdateCameraLastPosition();
	}

	// Token: 0x0600016B RID: 363 RVA: 0x00006D78 File Offset: 0x00004F78
	public void OnDestroy()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		SuspensionManager.Unregister(this);
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
		this.m_cameraGoThroughScrollLocks.Destroy();
		if (UI.Cameras.Current == this)
		{
			UI.Cameras.Current = null;
		}
	}

	// Token: 0x0600016C RID: 364 RVA: 0x00006DD0 File Offset: 0x00004FD0
	public void Awake()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		SuspensionManager.Register(this);
		this.m_cameraGoThroughScrollLocks = new CameraGoThroughScrollLocks(this);
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
		this.UpdateCameraBounds();
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00006E24 File Offset: 0x00005024
	public void OnEnable()
	{
		this.Transform = base.transform;
		this.GameObject = base.gameObject;
		this.CameraTarget = new CameraTarget(this);
		if (UI.Cameras.Current)
		{
			if (Application.isPlaying)
			{
				InstantiateUtility.Destroy(UI.Cameras.Current.Transform.parent.gameObject);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(UI.Cameras.Current.Transform.parent.gameObject);
			}
		}
		UI.Cameras.Current = this;
	}

	// Token: 0x0600016E RID: 366 RVA: 0x00006EAB File Offset: 0x000050AB
	public void OnDisable()
	{
		UI.Cameras.Current = null;
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00006EB3 File Offset: 0x000050B3
	public void OnRestoreCheckpoint()
	{
		this.Motion = GameplayCamera.MotionType.Chase;
		this.ChaseTarget.UpdateCameraLastPosition();
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00006EC7 File Offset: 0x000050C7
	public void UpdateTargetHelperPosition()
	{
		this.CameraTarget.UpdateTargetPosition();
		this.TargetHelperPosition = this.CameraTarget.TargetPosition;
	}

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x06000172 RID: 370 RVA: 0x00006EF8 File Offset: 0x000050F8
	// (set) Token: 0x06000171 RID: 369 RVA: 0x00006EE5 File Offset: 0x000050E5
	public Transform Target
	{
		get
		{
			return this.CameraTarget.BaseTargetLayer.Transform;
		}
		set
		{
			this.CameraTarget.BaseTargetLayer.Transform = value;
		}
	}

	// Token: 0x06000173 RID: 371 RVA: 0x00006F0A File Offset: 0x0000510A
	public void Start()
	{
		if (!Application.isPlaying)
		{
			this.SwayingSinMovement.Start();
			return;
		}
		this.UpdateTargetHelperPosition();
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x06000174 RID: 372 RVA: 0x00006F28 File Offset: 0x00005128
	// (set) Token: 0x06000175 RID: 373 RVA: 0x00006F30 File Offset: 0x00005130
	public float TimeDelta { get; set; }

	// Token: 0x06000176 RID: 374 RVA: 0x00006F3C File Offset: 0x0000513C
	public void UpdateTarget()
	{
		if (!this.ScrollLockIsFadingOut)
		{
			this.CameraTarget.UpdateTargetPosition();
			this.TargetHelper.position = this.CameraTarget.TargetPosition;
		}
		Vector3 position = this.Transform.position;
		this.Transform.position = this.TargetHelper.position;
		Vector3 vector = this.ScrollLockConstraint.CalculateConstraintOffset(this.TargetHelper.position);
		this.TargetHelper.position += vector;
		this.ScrollLockConstraintOffset = vector;
		this.Transform.position = position;
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x06000177 RID: 375 RVA: 0x00006FDD File Offset: 0x000051DD
	// (set) Token: 0x06000178 RID: 376 RVA: 0x00006FE5 File Offset: 0x000051E5
	public Bounds CameraBoundingBox { get; private set; }

	// Token: 0x06000179 RID: 377 RVA: 0x00006FEE File Offset: 0x000051EE
	public void UpdateCameraBounds()
	{
		this.CameraBoundingBox = CameraScrollLockConstraint.CalculateCameraBounds(this.Camera);
	}

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x0600017A RID: 378 RVA: 0x00007001 File Offset: 0x00005201
	public Vector3 CameraCenterInGameplayPlane
	{
		get
		{
			return this.m_cameraCenterInGameplayPlane;
		}
	}

	// Token: 0x0600017B RID: 379 RVA: 0x0000700C File Offset: 0x0000520C
	public void MoveCameraToTargetInstantly(bool updateTargetPosition = true)
	{
		CameraPivotZone.InstantUpdate();
		if (updateTargetPosition)
		{
			this.CameraTarget.UpdateTargetPosition();
		}
		this.MoveCameraToTargetPosition();
		this.Controller.CameraPostProcessing.Advance(float.PositiveInfinity);
		this.OffsetController.UpdateOffset(true);
		this.ForceCameraToObayScrollLockConstraints();
		this.ChaseTarget.UpdateCameraLastPosition();
		this.Controller.PuppetController.UpdatePuppet();
		this.Controller.UpdateCamera();
		this.UpdateCameraBounds();
		CameraFrustumOptimizer.ForceUpdate();
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00007090 File Offset: 0x00005290
	public void MoveCameraToTargetPosition()
	{
		Vector3 vector = this.CameraTarget.TargetPosition;
		this.TargetHelper.position = vector;
		vector = vector;
		this.Transform.position = vector;
		this.TargetHelperPosition = vector;
		this.ChaseTarget.UpdateCameraLastPosition();
	}

	// Token: 0x0600017D RID: 381 RVA: 0x000070D8 File Offset: 0x000052D8
	public void ForceCameraToObayScrollLockConstraints()
	{
		Vector3 vector = this.ScrollLockConstraint.CalculateConstraintOffset(this.TargetHelper.position);
		this.Transform.position += vector;
		this.TargetHelper.position += vector;
		this.ScrollLockConstraintOffset = vector;
		this.ChaseTarget.UpdateCameraLastPosition();
	}

	// Token: 0x0600017E RID: 382 RVA: 0x00007144 File Offset: 0x00005344
	public void ChangeTargetToCurrentCharacter()
	{
		if (Characters.Current as Component)
		{
			UI.Cameras.Current.Target = Characters.Current.Transform;
		}
	}

	// Token: 0x0600017F RID: 383 RVA: 0x00007179 File Offset: 0x00005379
	public void ChangeTarget(Transform targetTransform)
	{
		UI.Cameras.Current.Target = targetTransform;
	}

	// Token: 0x06000180 RID: 384 RVA: 0x00007188 File Offset: 0x00005388
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			if (this.m_rigidbody)
			{
				this.m_rigidbody.velocity = Vector2.zero;
			}
			return;
		}
		if (this.m_cameraGoThroughScrollLocks != null)
		{
			this.m_cameraGoThroughScrollLocks.Update();
		}
		this.TimeDelta = Time.deltaTime;
		if (!this.ScrollLockIsFadingOut && !Scenes.Manager.ScenesNotLoadedOnTime)
		{
			this.UpdateTargetHelperPosition();
			this.UpdateTarget();
			this.CameraPositionForSampling = this.Controller.Position;
		}
		if (this.Motion == GameplayCamera.MotionType.Chase)
		{
			this.ChaseTarget.UpdateChase();
		}
		if (this.Motion == GameplayCamera.MotionType.Move)
		{
			this.StraightLineMotion.UpdateMotion();
		}
		this.OffsetController.UpdateMultiplier();
		this.OffsetController.UpdateOffset(false);
		if (this.m_straightLineMotionTargetIsPlayer && this.Motion == GameplayCamera.MotionType.Move)
		{
			this.StraightLineMotion.EndPosition = this.TargetHelper.position;
		}
		Plane plane = new Plane(Vector3.forward, Vector3.zero);
		Ray ray = this.Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
		float d;
		if (plane.Raycast(ray, out d))
		{
			this.m_cameraCenterInGameplayPlane = ray.direction * d + ray.origin;
		}
		this.UpdateCameraBounds();
		if (!UI.Cameras.System.CrossFadeManager.IsCrossFading)
		{
			this.Controller.UpdateCamera();
		}
	}

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x06000181 RID: 385 RVA: 0x00007318 File Offset: 0x00005518
	// (set) Token: 0x06000182 RID: 386 RVA: 0x00007320 File Offset: 0x00005520
	public bool IsSuspended { get; set; }

	// Token: 0x04000118 RID: 280
	public Vector3 CameraPositionForSampling;

	// Token: 0x04000119 RID: 281
	public CameraController Controller;

	// Token: 0x0400011A RID: 282
	public CameraTarget CameraTarget;

	// Token: 0x0400011B RID: 283
	public CameraOffsetController OffsetController;

	// Token: 0x0400011C RID: 284
	public CameraChaseTarget ChaseTarget;

	// Token: 0x0400011D RID: 285
	public CameraScrollLockConstraint ScrollLockConstraint;

	// Token: 0x0400011E RID: 286
	public CameraSettingsAsset CurrentCameraSettings;

	// Token: 0x0400011F RID: 287
	public SinMovement SwayingSinMovement;

	// Token: 0x04000120 RID: 288
	public Transform TargetHelper;

	// Token: 0x04000121 RID: 289
	public CameraStraightLineMotion StraightLineMotion;

	// Token: 0x04000122 RID: 290
	public GameObject Fader;

	// Token: 0x04000123 RID: 291
	public Vector2 ScrollLockConstraintOffset;

	// Token: 0x04000124 RID: 292
	public bool ScrollLockIsFadingOut;

	// Token: 0x04000125 RID: 293
	public GameplayCamera.MotionType Motion;

	// Token: 0x04000126 RID: 294
	private bool m_straightLineMotionTargetIsPlayer;

	// Token: 0x04000127 RID: 295
	public Transform Transform;

	// Token: 0x04000128 RID: 296
	public GameObject GameObject;

	// Token: 0x04000129 RID: 297
	private Rigidbody m_rigidbody;

	// Token: 0x0400012A RID: 298
	public Vector3 TargetHelperPosition;

	// Token: 0x0400012B RID: 299
	private CameraGoThroughScrollLocks m_cameraGoThroughScrollLocks;

	// Token: 0x0400012C RID: 300
	private Vector3 m_cameraCenterInGameplayPlane;

	// Token: 0x0400012D RID: 301
	private Bounds m_editorBounds;

	// Token: 0x020003F4 RID: 1012
	public enum MotionType
	{
		// Token: 0x040017EF RID: 6127
		Chase,
		// Token: 0x040017F0 RID: 6128
		Move
	}
}
