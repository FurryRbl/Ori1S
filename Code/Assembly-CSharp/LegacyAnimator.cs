using System;
using UnityEngine;

// Token: 0x02000076 RID: 118
public abstract class LegacyAnimator : MonoBehaviour, IPooled, ISuspendable, IInScene, IDynamicGraphicHierarchy
{
	// Token: 0x14000013 RID: 19
	// (add) Token: 0x060004F1 RID: 1265 RVA: 0x0001409A File Offset: 0x0001229A
	// (remove) Token: 0x060004F2 RID: 1266 RVA: 0x000140B3 File Offset: 0x000122B3
	public event Action OnAnimationEndEvent;

	// Token: 0x17000136 RID: 310
	// (get) Token: 0x060004F3 RID: 1267 RVA: 0x000140CC File Offset: 0x000122CC
	// (set) Token: 0x060004F4 RID: 1268 RVA: 0x000140D4 File Offset: 0x000122D4
	public bool IsInScene
	{
		get
		{
			return this.m_isInScene;
		}
		set
		{
			this.m_isInScene = value;
		}
	}

	// Token: 0x17000137 RID: 311
	// (get) Token: 0x060004F5 RID: 1269 RVA: 0x000140DD File Offset: 0x000122DD
	// (set) Token: 0x060004F6 RID: 1270 RVA: 0x000140E5 File Offset: 0x000122E5
	public bool Stopped { get; set; }

	// Token: 0x17000138 RID: 312
	// (get) Token: 0x060004F7 RID: 1271 RVA: 0x000140EE File Offset: 0x000122EE
	// (set) Token: 0x060004F8 RID: 1272 RVA: 0x000140F6 File Offset: 0x000122F6
	public bool Reversed { get; set; }

	// Token: 0x17000139 RID: 313
	// (get) Token: 0x060004F9 RID: 1273 RVA: 0x000140FF File Offset: 0x000122FF
	// (set) Token: 0x060004FA RID: 1274 RVA: 0x00014107 File Offset: 0x00012307
	public float MaxTime { get; private set; }

	// Token: 0x1700013A RID: 314
	// (get) Token: 0x060004FB RID: 1275 RVA: 0x00014110 File Offset: 0x00012310
	// (set) Token: 0x060004FC RID: 1276 RVA: 0x00014118 File Offset: 0x00012318
	public float MinTime { get; private set; }

	// Token: 0x1700013B RID: 315
	// (get) Token: 0x060004FD RID: 1277 RVA: 0x00014124 File Offset: 0x00012324
	public float TimeOfLastCurvePoint
	{
		get
		{
			return this.AnimationCurve[this.AnimationCurve.length - 1].time;
		}
	}

	// Token: 0x1700013C RID: 316
	// (get) Token: 0x060004FE RID: 1278 RVA: 0x00014154 File Offset: 0x00012354
	public float TimeOfFirstCurvePoint
	{
		get
		{
			return this.AnimationCurve[0].time;
		}
	}

	// Token: 0x1700013D RID: 317
	// (get) Token: 0x060004FF RID: 1279 RVA: 0x00014175 File Offset: 0x00012375
	// (set) Token: 0x06000500 RID: 1280 RVA: 0x0001417D File Offset: 0x0001237D
	public float CurrentTime { get; set; }

	// Token: 0x1700013E RID: 318
	// (get) Token: 0x06000501 RID: 1281 RVA: 0x00014186 File Offset: 0x00012386
	public bool AtStart
	{
		get
		{
			return Mathf.Approximately(this.CurrentTime, this.MinTime);
		}
	}

	// Token: 0x1700013F RID: 319
	// (get) Token: 0x06000502 RID: 1282 RVA: 0x00014199 File Offset: 0x00012399
	public bool AtEnd
	{
		get
		{
			return Mathf.Approximately(this.CurrentTime, this.MaxTime);
		}
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x000141AC File Offset: 0x000123AC
	public virtual void Awake()
	{
		this.Stopped = true;
		SuspensionManager.Register(this);
		this.m_startSpeed = this.Speed;
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x000141C7 File Offset: 0x000123C7
	protected virtual void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x000141D0 File Offset: 0x000123D0
	public void UpdateMaxMinTime()
	{
		this.MaxTime = ((this.AnimationCurve.postWrapMode != WrapMode.ClampForever) ? float.MaxValue : this.TimeOfLastCurvePoint);
		this.MinTime = ((this.AnimationCurve.preWrapMode != WrapMode.ClampForever) ? float.MinValue : 0f);
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x0001422A File Offset: 0x0001242A
	public void SetAnimationCurve(AnimationCurve curve)
	{
		this.AnimationCurve = curve;
		this.UpdateMaxMinTime();
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x0001423C File Offset: 0x0001243C
	public virtual void Start()
	{
		this.m_hasStarted = true;
		this.CurrentTime = this.TimeOffset;
		this.UpdateMaxMinTime();
		if (this.PlayAutomatically)
		{
			this.Stopped = false;
		}
		if (this.SampleFirstFrameOnStart)
		{
			this.Sample(this.CurrentTime);
		}
		if (!this.Stopped)
		{
			this.AnimateIt(this.ValueInCurrentFrame());
		}
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x000142A4 File Offset: 0x000124A4
	public virtual void OnPoolSpawned()
	{
		this.m_hasStarted = false;
		this.Speed = this.m_startSpeed;
		this.Stopped = false;
		this.Reversed = false;
		this.MaxTime = 0f;
		this.MinTime = 0f;
		this.CurrentTime = 0f;
		if (this.PlayAutomatically)
		{
			this.AnimateIt(this.ValueInCurrentFrame());
		}
		else
		{
			this.RestoreToOriginalState();
		}
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x00014318 File Offset: 0x00012518
	public void FixedUpdate()
	{
		if (this.m_isSuspended || this.Stopped)
		{
			return;
		}
		this.CurrentTime += ((!this.Reversed) ? Time.deltaTime : (-Time.deltaTime)) * this.Speed;
		if (this.CurrentTime >= this.MaxTime)
		{
			this.CurrentTime = this.MaxTime;
			this.Stopped = true;
		}
		if (this.CurrentTime <= this.MinTime)
		{
			this.CurrentTime = this.MinTime;
			this.Stopped = true;
		}
		float num = this.ValueInCurrentFrame();
		this.AnimateIt(num * this.CurveMagnitude);
		if (!this.m_wasStopped && this.Stopped && this.OnAnimationEndEvent != null)
		{
			this.OnAnimationEndEvent();
		}
		this.m_wasStopped = this.Stopped;
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x00014400 File Offset: 0x00012600
	public float ValueInCurrentFrame()
	{
		return this.AnimationCurve.Evaluate(this.CurrentTime);
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x00014413 File Offset: 0x00012613
	public void Sample(float time)
	{
		if (!this.m_hasStarted)
		{
			return;
		}
		this.CurrentTime = time;
		this.AnimateIt(this.CurveMagnitude * this.ValueInCurrentFrame());
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x0001443C File Offset: 0x0001263C
	public virtual void Restart()
	{
		this.CurrentTime = this.MinTime;
		this.Reversed = false;
		this.Stopped = false;
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x00014463 File Offset: 0x00012663
	public void Continue()
	{
		this.Stopped = false;
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x0001446C File Offset: 0x0001266C
	public void ContinueForward()
	{
		this.Stopped = false;
		this.Reversed = false;
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x0001447C File Offset: 0x0001267C
	public void ContinueBackward()
	{
		this.Stopped = false;
		this.Reversed = true;
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x0001448C File Offset: 0x0001268C
	public void Reverse()
	{
		this.Stopped = false;
		this.Reversed = !this.Reversed;
		if (this.CurrentTime == this.MinTime)
		{
			this.Reversed = false;
		}
		if (this.CurrentTime == this.MaxTime)
		{
			this.Reversed = true;
		}
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x000144E0 File Offset: 0x000126E0
	public virtual void RestartReverse()
	{
		if (this.MaxTime == 0f)
		{
			this.MaxTime = this.TimeOfLastCurvePoint;
		}
		this.Reversed = true;
		this.CurrentTime = this.MaxTime;
		this.Stopped = false;
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x00014523 File Offset: 0x00012723
	public void Stop()
	{
		this.Stopped = true;
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x0001452C File Offset: 0x0001272C
	public void StopAndSampleAtStart()
	{
		this.CurrentTime = 0f;
		this.Stopped = true;
		this.Sample(this.CurrentTime);
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x00014558 File Offset: 0x00012758
	public void StopAndSampleAtEnd()
	{
		this.RestartReverse();
		this.Reversed = false;
		this.Sample(this.CurrentTime);
		this.Stop();
	}

	// Token: 0x06000515 RID: 1301
	protected abstract void AnimateIt(float value);

	// Token: 0x06000516 RID: 1302
	public abstract void RestoreToOriginalState();

	// Token: 0x17000140 RID: 320
	// (get) Token: 0x06000517 RID: 1303 RVA: 0x00014584 File Offset: 0x00012784
	// (set) Token: 0x06000518 RID: 1304 RVA: 0x0001458C File Offset: 0x0001278C
	public bool IsSuspended
	{
		get
		{
			return this.m_isSuspended;
		}
		set
		{
			this.m_isSuspended = value;
		}
	}

	// Token: 0x04000400 RID: 1024
	[HideInInspector]
	[SerializeField]
	private bool m_isInScene;

	// Token: 0x04000401 RID: 1025
	public AnimationCurve AnimationCurve;

	// Token: 0x04000402 RID: 1026
	public bool PlayAutomatically;

	// Token: 0x04000403 RID: 1027
	public bool SampleFirstFrameOnStart = true;

	// Token: 0x04000404 RID: 1028
	public float TimeOffset;

	// Token: 0x04000405 RID: 1029
	public float CurveMagnitude = 1f;

	// Token: 0x04000406 RID: 1030
	public float Speed = 1f;

	// Token: 0x04000407 RID: 1031
	private float m_startSpeed;

	// Token: 0x04000408 RID: 1032
	private bool m_hasStarted;

	// Token: 0x04000409 RID: 1033
	private bool m_isSuspended;

	// Token: 0x0400040A RID: 1034
	private bool m_wasStopped;
}
