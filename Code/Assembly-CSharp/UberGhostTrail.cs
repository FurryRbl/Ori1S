using System;
using UnityEngine;

// Token: 0x020000EB RID: 235
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class UberGhostTrail : MonoBehaviour, ISuspendable, IUberAtlasExternal
{
	// Token: 0x170001FD RID: 509
	// (get) Token: 0x06000955 RID: 2389 RVA: 0x00028218 File Offset: 0x00026418
	private Renderer TargetRenderer
	{
		get
		{
			if (this.m_targetRenderer == null && this.AnimatorTarget != null)
			{
				this.m_targetRenderer = this.AnimatorTarget.GetComponent<Renderer>();
			}
			return this.m_targetRenderer;
		}
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x00028253 File Offset: 0x00026453
	private void Reset()
	{
		if (!base.GetComponent<UberShaderComponent>())
		{
			base.gameObject.AddComponent<UberShaderComponent>();
		}
	}

	// Token: 0x170001FE RID: 510
	// (get) Token: 0x06000957 RID: 2391 RVA: 0x00028271 File Offset: 0x00026471
	public Renderer Renderer
	{
		get
		{
			if (this.m_renderer == null)
			{
				this.m_renderer = base.GetComponent<Renderer>();
			}
			return this.m_renderer;
		}
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x00028296 File Offset: 0x00026496
	public static void WarmUpResource()
	{
		if (UberGhostTrail.s_trailParentPrefab == null)
		{
			UberGhostTrail.s_trailParentPrefab = Resources.Load<GameObject>("trailParent");
		}
	}

	// Token: 0x06000959 RID: 2393 RVA: 0x000282B7 File Offset: 0x000264B7
	private void Awake()
	{
		SuspensionManager.Register(this);
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x000282BF File Offset: 0x000264BF
	private void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x000282C8 File Offset: 0x000264C8
	private void OnEnable()
	{
		if (this.AnimatorTarget != null)
		{
			this.UpdateComponents();
		}
		this.m_previousPosition = base.transform.position;
		this.m_lastSpawnPoint = base.transform.position;
		if (this.m_trailParent == null)
		{
			UberGhostTrail.WarmUpResource();
			this.m_trailParent = (UnityEngine.Object.Instantiate(UberGhostTrail.s_trailParentPrefab, Vector3.zero, Quaternion.identity) as GameObject);
			this.m_parentRenderer = this.m_trailParent.GetComponent<MeshRenderer>();
			this.m_meshUpdater = this.m_trailParent.GetComponent<UberGhostTrailMeshUpdate>();
			this.m_meshUpdater.SetSettings(this);
			this.m_parentRenderer.sortingOrder = this.Renderer.sortingOrder;
			this.m_parentRenderer.sortingLayerID = this.Renderer.sortingLayerID;
		}
		this.Renderer.enabled = false;
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x000283A9 File Offset: 0x000265A9
	private void UpdateComponents()
	{
		this.m_animator = this.AnimatorTarget.GetComponent<SpriteAnimator>();
		this.m_transAnimator = this.AnimatorTarget.GetComponent<SpriteAnimatorWithTransitions>();
		this.m_comboAnimator = this.AnimatorTarget.GetComponent<SeinNaruComboAnimator>();
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x000283E0 File Offset: 0x000265E0
	public void Update()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (this.m_trailParent != null)
		{
			this.m_meshUpdater.SetPos(base.transform.position);
			if (this.DoUpdate)
			{
				this.UpdateTrail();
			}
		}
	}

	// Token: 0x170001FF RID: 511
	// (get) Token: 0x0600095E RID: 2398 RVA: 0x00028434 File Offset: 0x00026634
	private bool DoUpdate
	{
		get
		{
			return !Application.isPlaying || (this.m_meshUpdater != null && this.m_meshUpdater.Visible);
		}
	}

	// Token: 0x0600095F RID: 2399 RVA: 0x00028470 File Offset: 0x00026670
	private void UpdateTrail()
	{
		if (this.m_meshUpdater == null)
		{
			return;
		}
		this.m_deltaTime = Time.deltaTime;
		if (this.m_meshUpdater.TargetMat == null)
		{
			return;
		}
		if (this.AnimatorTarget != null)
		{
			this.EmitQuads();
		}
		this.m_meshUpdater.UpdateTrailMesh();
	}

	// Token: 0x06000960 RID: 2400 RVA: 0x000284D4 File Offset: 0x000266D4
	private void EmitQuads()
	{
		Renderer targetRenderer = this.TargetRenderer;
		if (targetRenderer == null || !targetRenderer.enabled)
		{
			return;
		}
		this.m_previousPosition -= this.m_deltaTime * (this.Startspeed + base.transform.TransformDirection(this.LocalStartSpeed) + this.ConstantForce);
		Vector3 position = base.transform.position;
		Vector3 vector = position;
		Vector3 vector2 = Vector3.zero;
		if (this.UseCenterOfCroppedSprite && this.m_animator && this.m_animator.TextureAnimator != null)
		{
			vector2 = this.m_animator.TextureAnimator.Texture.CenterOffset;
			vector = base.transform.TransformPoint(vector2);
			vector2 = vector - position;
		}
		float magnitude = (vector - this.m_previousPosition).magnitude;
		this.m_previousPosition = vector;
		if (magnitude < this.SpawnDistance * 25f)
		{
			this.m_travelDistance += magnitude;
		}
		if (this.m_travelDistance <= this.SpawnDistance)
		{
			return;
		}
		Quaternion rotate = base.transform.rotation;
		Vector3 lossyScale = base.transform.lossyScale;
		int num = Mathf.RoundToInt(this.m_travelDistance / this.SpawnDistance);
		Vector4 vector3 = this.m_meshUpdater.TargetMat.GetVector("_MainTex_US_ATLAS_ST");
		vector3.z += vector3.x;
		vector3.w += vector3.y;
		Vector3 vector4 = this.m_lastSpawnPoint + this.m_previousCenterOffset - vector2;
		Vector3 eulerAngles = rotate.eulerAngles;
		rotate = Quaternion.Euler((eulerAngles.x <= 90f) ? 0f : 180f, (eulerAngles.y <= 90f) ? 0f : 180f, eulerAngles.z);
		if (Mathf.Abs(Mathf.DeltaAngle(0f, eulerAngles.x)) > 90f)
		{
			lossyScale.y *= -1f;
			eulerAngles.z *= -1f;
		}
		if (Mathf.Abs(Mathf.DeltaAngle(0f, eulerAngles.y)) > 90f)
		{
			lossyScale.x *= -1f;
			eulerAngles.z *= -1f;
		}
		for (int i = 0; i < num; i++)
		{
			float t = (num > 1) ? ((float)i / (float)(num - 1)) : 1f;
			float posx = Mathf.Lerp(vector4.x, position.x, t);
			float posy = Mathf.Lerp(vector4.y, position.y, t);
			this.m_meshUpdater.SpawnSingleTrailSprite(posx, posy, vector3, eulerAngles, rotate, lossyScale);
		}
		this.m_previousCenterOffset = vector2;
		this.m_lastSpawnPoint = position;
		this.m_travelDistance = 0f;
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x0002881C File Offset: 0x00026A1C
	public UberScreenMode GetExternalUberScreenMode()
	{
		UberScreenMode result = UberScreenMode.None;
		if (this.AnimatorTarget != null)
		{
			this.UpdateComponents();
			if (this.m_animator != null)
			{
				result = this.m_animator.GetExternalUberScreenMode();
			}
			else if (this.m_transAnimator != null)
			{
				result = this.m_transAnimator.GetExternalUberScreenMode();
			}
			else if (this.m_comboAnimator != null)
			{
				result = this.m_comboAnimator.GetExternalUberScreenMode();
			}
		}
		return result;
	}

	// Token: 0x06000962 RID: 2402 RVA: 0x000288A4 File Offset: 0x00026AA4
	public float GetUberTweakValue()
	{
		float result = 0f;
		if (this.AnimatorTarget != null)
		{
			this.UpdateComponents();
			if (this.m_animator != null)
			{
				result = this.m_animator.GetUberTweakValue();
			}
			else if (this.m_transAnimator != null)
			{
				result = this.m_transAnimator.GetUberTweakValue();
			}
			else if (this.m_comboAnimator != null)
			{
				result = this.m_comboAnimator.GetUberTweakValue();
			}
		}
		return result;
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x00028930 File Offset: 0x00026B30
	public bool DoesProvideAtlas()
	{
		return !(this.Renderer.sharedMaterial == null) && this.Renderer.sharedMaterial.HasProperty("_MainTex_US_ATLAS");
	}

	// Token: 0x17000200 RID: 512
	// (get) Token: 0x06000964 RID: 2404 RVA: 0x0002896A File Offset: 0x00026B6A
	// (set) Token: 0x06000965 RID: 2405 RVA: 0x00028972 File Offset: 0x00026B72
	public bool IsSuspended { get; set; }

	// Token: 0x04000787 RID: 1927
	private const float c_maxTravelDistance = 25f;

	// Token: 0x04000788 RID: 1928
	public float SpawnDistance = 0.1f;

	// Token: 0x04000789 RID: 1929
	public AnimationCurve FadeoutCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);

	// Token: 0x0400078A RID: 1930
	public AnimationCurve ScaleCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);

	// Token: 0x0400078B RID: 1931
	public GameObject AnimatorTarget;

	// Token: 0x0400078C RID: 1932
	private Renderer m_targetRenderer;

	// Token: 0x0400078D RID: 1933
	public Vector2 ConstantForce;

	// Token: 0x0400078E RID: 1934
	public Vector2 LocalConstantForce;

	// Token: 0x0400078F RID: 1935
	public Vector2 Startspeed;

	// Token: 0x04000790 RID: 1936
	public Vector2 RandomStartSpeed;

	// Token: 0x04000791 RID: 1937
	public Vector2 LocalStartSpeed;

	// Token: 0x04000792 RID: 1938
	public Vector2 LocalRandomStartSpeed;

	// Token: 0x04000793 RID: 1939
	public bool UseCenterOfCroppedSprite;

	// Token: 0x04000794 RID: 1940
	private SpriteAnimator m_animator;

	// Token: 0x04000795 RID: 1941
	private SpriteAnimatorWithTransitions m_transAnimator;

	// Token: 0x04000796 RID: 1942
	private SeinNaruComboAnimator m_comboAnimator;

	// Token: 0x04000797 RID: 1943
	private Vector3 m_lastSpawnPoint;

	// Token: 0x04000798 RID: 1944
	private Vector3 m_previousPosition;

	// Token: 0x04000799 RID: 1945
	private float m_travelDistance;

	// Token: 0x0400079A RID: 1946
	private GameObject m_trailParent;

	// Token: 0x0400079B RID: 1947
	private Renderer m_renderer;

	// Token: 0x0400079C RID: 1948
	private Renderer m_parentRenderer;

	// Token: 0x0400079D RID: 1949
	private float m_lastTime;

	// Token: 0x0400079E RID: 1950
	private UberGhostTrailMeshUpdate m_meshUpdater;

	// Token: 0x0400079F RID: 1951
	private float m_deltaTime;

	// Token: 0x040007A0 RID: 1952
	private Vector2 m_previousCenterOffset;

	// Token: 0x040007A1 RID: 1953
	private static GameObject s_trailParentPrefab;
}
