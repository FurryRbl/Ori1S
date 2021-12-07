using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x020004D1 RID: 1233
[ExecuteInEditMode]
public class UberInteractionManager : MonoBehaviour
{
	// Token: 0x0600215A RID: 8538 RVA: 0x000920A0 File Offset: 0x000902A0
	public static UberInteractionManager.PropertyIDCache GetCachedPropertyID(ref UberInteractionManager.PropertyIDCache staticCache, string propertyInteractionName)
	{
		if (staticCache != null)
		{
			return staticCache;
		}
		staticCache = new UberInteractionManager.PropertyIDCache(propertyInteractionName);
		return staticCache;
	}

	// Token: 0x0600215B RID: 8539 RVA: 0x000920B8 File Offset: 0x000902B8
	public static string GetInteractionCurveString()
	{
		string[] names = Enum.GetNames(typeof(UberInteractionManager.InteractionCurveType));
		string text = string.Empty;
		foreach (string text2 in names)
		{
			string text3 = text;
			text = string.Concat(new string[]
			{
				text3,
				"\r\n\t\t\t\t\tsampler2D _InteractionCurve",
				text2,
				";\r\n\t\t\t\t\thalf4 _InteractionCurveSettings",
				text2,
				";\r\n\t\t\t\t\t"
			});
		}
		return text + "#define GetInteractionValue(uv, interName)  (((tex2Dlod(_InteractionCurve##interName, float4(uv * _InteractionCurveSettings##interName.y, 0.0f, 0.0f, 0.0f)).a * 2.0f - 1.0f)) * _InteractionCurveSettings##interName.x)";
	}

	// Token: 0x170005B0 RID: 1456
	// (get) Token: 0x0600215C RID: 8540 RVA: 0x0009213B File Offset: 0x0009033B
	public float InteractionTime
	{
		get
		{
			if (!Application.isPlaying)
			{
				return Time.realtimeSinceStartup * 1.1f;
			}
			if (this.m_timeDriver == null)
			{
				this.m_timeDriver = UnityEngine.Object.FindObjectOfType<ShaderAnimationTimeDriver>();
			}
			return this.m_timeDriver.GameTime;
		}
	}

	// Token: 0x0600215D RID: 8541 RVA: 0x0009217A File Offset: 0x0009037A
	private void OnEnable()
	{
		UberInteractionManager.Instance = this;
		this.UpdateArrays();
		this.UpdateTexture();
		this.UpdateCurves();
	}

	// Token: 0x0600215E RID: 8542 RVA: 0x00092194 File Offset: 0x00090394
	private void UpdateCurves()
	{
		for (int i = 0; i < this.Curves.Count; i++)
		{
			UberInteractionManager.InteractionCurve interactionCurve = this.Curves[i];
			Shader.SetGlobalTexture(interactionCurve.CurveName, interactionCurve.GetTexture());
			Shader.SetGlobalVector(interactionCurve.CurveSettingsName, new Vector4(interactionCurve.Scale, 1f / interactionCurve.Duration));
		}
	}

	// Token: 0x0600215F RID: 8543 RVA: 0x00092200 File Offset: 0x00090400
	public void Explode(Vector3 position, float outwardSpeed, Vector4 strength, float radius)
	{
		for (int i = 0; i < this.m_interactionCount; i++)
		{
			if (this.m_interactorActive[i])
			{
				UberInteractionManager.InteractionInfo interactionInfo = this.m_interactionInfos[i];
				float num = interactionInfo.X - position.x;
				float num2 = interactionInfo.Y - position.y;
				float num3 = num * num + num2 * num2;
				float num4 = interactionInfo.MaxRadius + radius;
				if (num3 < num4 * num4)
				{
					float num5 = interactionInfo.Z - position.z;
					if (num5 < interactionInfo.MaxRadius + radius)
					{
						IInteractable interactable = this.m_interactables[i];
						Vector3 explodePoint = interactable.GetExplodePoint(position);
						Vector3 vector = explodePoint - position;
						float num6 = Mathf.Clamp01(1f - vector.magnitude / radius);
						float num7 = vector.magnitude / outwardSpeed;
						if (num6 > 0f)
						{
							this.m_explosionApplications.Add(new UberInteractionManager.ExplosionApplication
							{
								Time = this.InteractionTime + num7,
								Apply = interactable,
								Velocity = (interactable.GetPosition() - position).normalized * outwardSpeed,
								Strength = strength * num6,
								Pos = position
							});
						}
					}
				}
			}
		}
		if (this.m_explodeSort == null)
		{
			this.m_explodeSort = new Comparison<UberInteractionManager.ExplosionApplication>(this.ExplosionApplicationSort);
		}
		this.m_explosionApplications.Sort(this.m_explodeSort);
	}

	// Token: 0x06002160 RID: 8544 RVA: 0x00092397 File Offset: 0x00090597
	private int ExplosionApplicationSort(UberInteractionManager.ExplosionApplication a, UberInteractionManager.ExplosionApplication b)
	{
		return b.Time.CompareTo(a.Time);
	}

	// Token: 0x06002161 RID: 8545 RVA: 0x000923AC File Offset: 0x000905AC
	public void Interact(UberInteractionActor actor, Vector3 velocity, Vector3 prevPos, int priority)
	{
		if (velocity.sqrMagnitude > 10000f)
		{
			return;
		}
		if (velocity.sqrMagnitude < 0.5f)
		{
			return;
		}
		Bounds bounds = new Bounds(actor.transform.position, new Vector3(actor.Radius * 2f, actor.Radius * 2f, actor.Radius * 2f * actor.ZScale));
		if (UI.Cameras.Current.Controller.InsideFrustum(bounds) || !Application.isPlaying)
		{
			UberInteractionManager.ActorInfo item = new UberInteractionManager.ActorInfo
			{
				Actor = actor,
				Velocity = velocity,
				PrevPos = prevPos
			};
			if (actor.OnlyWater)
			{
				this.m_resolveWaterQueue.Add(item);
			}
			else
			{
				this.m_actorQueue.Add(item);
			}
		}
	}

	// Token: 0x06002162 RID: 8546 RVA: 0x0009248C File Offset: 0x0009068C
	private void ResolveActorQueue()
	{
		this.m_processingQueue.Clear();
		this.m_positions.Clear();
		for (int i = 0; i < this.m_actorQueue.Count; i++)
		{
			UberInteractionManager.ActorInfo item = this.m_actorQueue[i];
			Vector3 position = item.Actor.transform.position;
			bool flag = false;
			for (int j = 0; j < this.m_positions.Count; j++)
			{
				if ((position - this.m_positions[j]).sqrMagnitude < 1f)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.m_processingQueue.Add(item);
			}
			this.m_positions.Add(position);
		}
		this.m_actorQueue.Clear();
		int num = Mathf.Min(5, this.m_processingQueue.Count);
		for (int k = 0; k < num; k++)
		{
			UberInteractionManager.ActorInfo actorInfo = this.m_processingQueue[k];
			UberInteractionActor actor = actorInfo.Actor;
			Vector3 velocity = actorInfo.Velocity;
			Vector3 prevPos = actorInfo.PrevPos;
			Vector3 position2 = actor.transform.position;
			Vector4 strength = actor.Strength;
			float zscale = actor.ZScale;
			float radius = actor.Radius;
			float num2 = actor.Radius * zscale;
			bool flag2 = false;
			for (int l = 0; l < this.m_interactionCount; l++)
			{
				if (this.m_interactorActive[l])
				{
					UberInteractionManager.InteractionInfo interactionInfo = this.m_interactionInfos[l];
					float num3 = interactionInfo.X - position2.x;
					float num4 = interactionInfo.Y - position2.y;
					float num5 = num3 * num3 + num4 * num4;
					float num6 = interactionInfo.MaxRadius + radius;
					if (num5 < num6 * num6)
					{
						float num7 = interactionInfo.Z - position2.z;
						if (num7 < interactionInfo.MaxRadius + num2)
						{
							IInteractable interactable = this.m_interactables[l];
							if (interactable != null)
							{
								bool flag3 = interactable.DoesOverlap(position2, velocity, radius, zscale);
								if (interactable.IsWater() && actor.Water == null && flag3)
								{
									actor.Water = (interactable as UberWaterControl);
									actor.OnWaterEnter();
								}
								if (flag3)
								{
									flag2 = true;
									interactable.SetInteraction(0f, position2, prevPos, strength, velocity, radius, false);
								}
							}
						}
					}
				}
			}
			if (actor.Water != null && !flag2)
			{
				actor.OnWaterExit();
				actor.Water = null;
			}
		}
		for (int m = 0; m < this.m_resolveWaterQueue.Count; m++)
		{
			bool flag4 = false;
			UberInteractionManager.ActorInfo actorInfo2 = this.m_resolveWaterQueue[m];
			for (int n = 0; n < UberWaterControl.All.Count; n++)
			{
				UberWaterControl uberWaterControl = UberWaterControl.All[n];
				bool flag5 = uberWaterControl.DoesOverlap(actorInfo2.Actor.transform.position, actorInfo2.Velocity, actorInfo2.Actor.Radius, actorInfo2.Actor.ZScale);
				if (actorInfo2.Actor.Water == null && flag5)
				{
					actorInfo2.Actor.Water = uberWaterControl;
					actorInfo2.Actor.OnWaterEnter();
				}
				if (flag5)
				{
					flag4 = true;
					uberWaterControl.SetInteraction(0f, actorInfo2.Actor.transform.position, actorInfo2.PrevPos, actorInfo2.Actor.Strength, actorInfo2.Velocity, actorInfo2.Actor.Radius, false);
				}
			}
			if (actorInfo2.Actor.Water != null && !flag4)
			{
				actorInfo2.Actor.OnWaterExit();
				actorInfo2.Actor.Water = null;
			}
		}
		this.m_resolveWaterQueue.Clear();
	}

	// Token: 0x06002163 RID: 8547 RVA: 0x000928A4 File Offset: 0x00090AA4
	public void FixedUpdate()
	{
		if (!this.DoInteractions)
		{
			return;
		}
		for (int i = 0; i < this.m_actors.Count; i++)
		{
			if (!(this.m_actors[i] == null))
			{
				UberInteractionActor uberInteractionActor = this.m_actors[i];
				uberInteractionActor.InteractionUpdate();
			}
		}
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		int num = 0;
		while (this.m_toRegister.Count > 0 && (Time.realtimeSinceStartup - realtimeSinceStartup < 0.0005f || num < 5))
		{
			int index = this.m_toRegister.Count - 1;
			IInteractable interactor = this.m_toRegister[index];
			this.m_toRegister.RemoveAt(index);
			this.DoRegisterInteractor(interactor);
			num++;
		}
		this.ResolveActorQueue();
		if (this.m_explosionApplications.Count > 0)
		{
			for (int j = this.m_explosionApplications.Count - 1; j >= 0; j--)
			{
				UberInteractionManager.ExplosionApplication explosionApplication = this.m_explosionApplications[j];
				if (this.InteractionTime < explosionApplication.Time - 0.1f)
				{
					break;
				}
				if (!explosionApplication.Apply.Equals(null))
				{
					explosionApplication.Apply.SetInteraction(explosionApplication.Time - this.InteractionTime, explosionApplication.Pos, explosionApplication.Pos, explosionApplication.Strength, explosionApplication.Velocity, 1.5f, true);
				}
				this.m_explosionApplications.RemoveAt(j);
			}
		}
	}

	// Token: 0x06002164 RID: 8548 RVA: 0x00092A38 File Offset: 0x00090C38
	private void UpdateTexture()
	{
		for (int i = 0; i < this.Curves.Count; i++)
		{
			UberInteractionManager.InteractionCurve interactionCurve = this.Curves[i];
			interactionCurve.UpdateTexture();
		}
	}

	// Token: 0x06002165 RID: 8549 RVA: 0x00092A74 File Offset: 0x00090C74
	public void RegisterActor(UberInteractionActor actor)
	{
		if (!this.m_actors.Contains(actor))
		{
			this.m_actors.Add(actor);
		}
	}

	// Token: 0x06002166 RID: 8550 RVA: 0x00092A94 File Offset: 0x00090C94
	public void RegisterInteractor(IInteractable interactor)
	{
		if (interactor.IsRegistered)
		{
			return;
		}
		interactor.Index = this.m_toRegister.Count;
		this.m_toRegister.Add(interactor);
	}

	// Token: 0x06002167 RID: 8551 RVA: 0x00092ACC File Offset: 0x00090CCC
	private void DoRegisterInteractor(IInteractable interactor)
	{
		if (interactor.IsRegistered)
		{
			return;
		}
		this.UpdateArrays();
		int num = -1;
		for (int i = this.m_minBoundIndex; i < this.m_interactables.Length; i++)
		{
			if (!this.m_interactorActive[i])
			{
				num = i;
				break;
			}
		}
		if (num == -1)
		{
			num = this.m_interactables.Length;
			Array.Resize<IInteractable>(ref this.m_interactables, this.m_interactables.Length * 2);
			Array.Resize<bool>(ref this.m_interactorActive, this.m_interactorActive.Length * 2);
			Array.Resize<UberInteractionManager.InteractionInfo>(ref this.m_interactionInfos, this.m_interactionInfos.Length * 2);
		}
		this.m_minBoundIndex = num + 1;
		interactor.Index = num;
		interactor.IsRegistered = true;
		interactor.OnRegistered();
		Vector3 position = interactor.GetPosition();
		this.m_interactionInfos[num] = new UberInteractionManager.InteractionInfo
		{
			X = position.x,
			Y = position.y,
			Z = position.z,
			MaxRadius = interactor.MaxRadius()
		};
		this.m_interactables[num] = interactor;
		this.m_interactorActive[num] = true;
		this.m_interactionCount = Mathf.Max(this.m_interactionCount, num + 1);
	}

	// Token: 0x06002168 RID: 8552 RVA: 0x00092C09 File Offset: 0x00090E09
	private void UpdateArrays()
	{
		if (this.m_interactables == null)
		{
			this.m_interactables = new IInteractable[2048];
			this.m_interactionInfos = new UberInteractionManager.InteractionInfo[2048];
			this.m_interactorActive = new bool[2048];
		}
	}

	// Token: 0x06002169 RID: 8553 RVA: 0x00092C48 File Offset: 0x00090E48
	public void RemoveInteractor(IInteractable interactor)
	{
		if (interactor.Equals(null))
		{
			return;
		}
		if (!interactor.IsRegistered && interactor.Index != -1)
		{
			this.m_toRegister.RemoveUnordered(interactor);
			return;
		}
		this.UpdateArrays();
		if (interactor.Index != -1)
		{
			this.m_interactables[interactor.Index] = null;
			this.m_interactorActive[interactor.Index] = false;
			interactor.IsRegistered = false;
			if (interactor.Index == this.m_interactionCount - 1)
			{
				for (int i = this.m_interactionCount - 1; i >= 0; i--)
				{
					if (this.m_interactorActive[i])
					{
						this.m_interactionCount = i + 1;
						break;
					}
				}
			}
		}
		this.m_minBoundIndex = Mathf.Min(this.m_minBoundIndex, interactor.Index);
		interactor.Index = -1;
	}

	// Token: 0x0600216A RID: 8554 RVA: 0x00092D26 File Offset: 0x00090F26
	public void RemoveActor(UberInteractionActor actor)
	{
		if (actor.Water != null)
		{
			actor.OnWaterExit();
		}
		this.m_actors.RemoveUnordered(actor);
	}

	// Token: 0x04001C2B RID: 7211
	public static UberInteractionManager Instance;

	// Token: 0x04001C2C RID: 7212
	[NonSerialized]
	public bool DoInteractions = true;

	// Token: 0x04001C2D RID: 7213
	public List<UberInteractionManager.InteractionCurve> Curves = new List<UberInteractionManager.InteractionCurve>();

	// Token: 0x04001C2E RID: 7214
	public float MagnitudePower = 0.5f;

	// Token: 0x04001C2F RID: 7215
	public float PlayDelayTime = 0.4f;

	// Token: 0x04001C30 RID: 7216
	private List<UberInteractionActor> m_actors = new List<UberInteractionActor>();

	// Token: 0x04001C31 RID: 7217
	private bool[] m_interactorActive;

	// Token: 0x04001C32 RID: 7218
	private IInteractable[] m_interactables;

	// Token: 0x04001C33 RID: 7219
	private UberInteractionManager.InteractionInfo[] m_interactionInfos;

	// Token: 0x04001C34 RID: 7220
	private int m_minBoundIndex;

	// Token: 0x04001C35 RID: 7221
	private int m_interactionCount;

	// Token: 0x04001C36 RID: 7222
	private List<IInteractable> m_toRegister = new List<IInteractable>();

	// Token: 0x04001C37 RID: 7223
	private List<UberInteractionManager.ExplosionApplication> m_explosionApplications = new List<UberInteractionManager.ExplosionApplication>();

	// Token: 0x04001C38 RID: 7224
	private List<UberInteractionManager.ActorInfo> m_actorQueue = new List<UberInteractionManager.ActorInfo>();

	// Token: 0x04001C39 RID: 7225
	private List<UberInteractionManager.ActorInfo> m_resolveWaterQueue = new List<UberInteractionManager.ActorInfo>();

	// Token: 0x04001C3A RID: 7226
	private List<Vector3> m_positions = new List<Vector3>();

	// Token: 0x04001C3B RID: 7227
	private List<UberInteractionManager.ActorInfo> m_processingQueue = new List<UberInteractionManager.ActorInfo>();

	// Token: 0x04001C3C RID: 7228
	private ShaderAnimationTimeDriver m_timeDriver;

	// Token: 0x04001C3D RID: 7229
	private Comparison<UberInteractionManager.ExplosionApplication> m_explodeSort;

	// Token: 0x020007F8 RID: 2040
	public class PropertyIDCache
	{
		// Token: 0x06002EF7 RID: 12023 RVA: 0x000C7054 File Offset: 0x000C5254
		public PropertyIDCache(string interactionName)
		{
			this.PosId = Shader.PropertyToID("_InteractionPos" + interactionName);
			this.VelId = Shader.PropertyToID("_InteractionVelocity" + interactionName);
			this.ParamsId = Shader.PropertyToID("_InteractionParams" + interactionName);
			this.Params2Id = Shader.PropertyToID("_InteractionParams2" + interactionName);
		}

		// Token: 0x04002A16 RID: 10774
		public int PosId;

		// Token: 0x04002A17 RID: 10775
		public int VelId;

		// Token: 0x04002A18 RID: 10776
		public int ParamsId;

		// Token: 0x04002A19 RID: 10777
		public int Params2Id;
	}

	// Token: 0x020007F9 RID: 2041
	public enum InteractionCurveType
	{
		// Token: 0x04002A1B RID: 10779
		Default,
		// Token: 0x04002A1C RID: 10780
		Light
	}

	// Token: 0x02000833 RID: 2099
	[Serializable]
	public class InteractionCurve
	{
		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06002FF3 RID: 12275 RVA: 0x000CB0EE File Offset: 0x000C92EE
		// (set) Token: 0x06002FF4 RID: 12276 RVA: 0x000CB0F6 File Offset: 0x000C92F6
		public float Scale { get; private set; }

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06002FF5 RID: 12277 RVA: 0x000CB100 File Offset: 0x000C9300
		public float Duration
		{
			get
			{
				return this.Curve[this.Curve.length - 1].time;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002FF6 RID: 12278 RVA: 0x000CB12D File Offset: 0x000C932D
		public string CurveName
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_curveName))
				{
					this.m_curveName = "_InteractionCurve" + this.Type;
				}
				return this.m_curveName;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002FF7 RID: 12279 RVA: 0x000CB160 File Offset: 0x000C9360
		public string CurveSettingsName
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_curveSettingsName))
				{
					this.m_curveSettingsName = "_InteractionCurveSettings" + this.Type;
				}
				return this.m_curveSettingsName;
			}
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x000CB194 File Offset: 0x000C9394
		public void UpdateTexture()
		{
			if (this.Curve == null)
			{
				return;
			}
			if (this.m_curveTex)
			{
				UnityEngine.Object.DestroyImmediate(this.m_curveTex);
			}
			float scale;
			float num;
			this.m_curveTex = UberShaderCurveBake.BakeAnimationCurve(this.Curve, TextureWrapMode.Clamp, 64, out scale, out num);
			this.Scale = scale;
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000CB1E7 File Offset: 0x000C93E7
		public Texture2D GetTexture()
		{
			if (this.m_curveTex == null)
			{
				this.UpdateTexture();
			}
			return this.m_curveTex;
		}

		// Token: 0x04002B2A RID: 11050
		public UberInteractionManager.InteractionCurveType Type;

		// Token: 0x04002B2B RID: 11051
		public AnimationCurve Curve = AnimationCurve.Linear(0f, 0f, 1f, 0f);

		// Token: 0x04002B2C RID: 11052
		private Texture2D m_curveTex;

		// Token: 0x04002B2D RID: 11053
		private string m_curveName;

		// Token: 0x04002B2E RID: 11054
		private string m_curveSettingsName;
	}

	// Token: 0x02000835 RID: 2101
	private struct InteractionInfo
	{
		// Token: 0x04002B32 RID: 11058
		public float X;

		// Token: 0x04002B33 RID: 11059
		public float Y;

		// Token: 0x04002B34 RID: 11060
		public float Z;

		// Token: 0x04002B35 RID: 11061
		public float MaxRadius;
	}

	// Token: 0x02000836 RID: 2102
	private struct ExplosionApplication
	{
		// Token: 0x04002B36 RID: 11062
		public float Time;

		// Token: 0x04002B37 RID: 11063
		public IInteractable Apply;

		// Token: 0x04002B38 RID: 11064
		public Vector4 Strength;

		// Token: 0x04002B39 RID: 11065
		public Vector3 Pos;

		// Token: 0x04002B3A RID: 11066
		public Vector3 Velocity;
	}

	// Token: 0x02000837 RID: 2103
	private struct ActorInfo
	{
		// Token: 0x04002B3B RID: 11067
		public UberInteractionActor Actor;

		// Token: 0x04002B3C RID: 11068
		public Vector3 Velocity;

		// Token: 0x04002B3D RID: 11069
		public Vector3 PrevPos;
	}
}
