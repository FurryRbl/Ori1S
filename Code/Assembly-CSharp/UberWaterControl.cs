using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000815 RID: 2069
[AddComponentMenu("Uber Water/Uber water control")]
[ExecuteInEditMode]
public class UberWaterControl : MonoBehaviour, IInteractable
{
	// Token: 0x1700079E RID: 1950
	// (get) Token: 0x06002F73 RID: 12147 RVA: 0x000C89B2 File Offset: 0x000C6BB2
	// (set) Token: 0x06002F74 RID: 12148 RVA: 0x000C89BA File Offset: 0x000C6BBA
	public int Index { get; set; }

	// Token: 0x1700079F RID: 1951
	// (get) Token: 0x06002F75 RID: 12149 RVA: 0x000C89C3 File Offset: 0x000C6BC3
	// (set) Token: 0x06002F76 RID: 12150 RVA: 0x000C89CB File Offset: 0x000C6BCB
	public bool IsRegistered { get; set; }

	// Token: 0x170007A0 RID: 1952
	// (get) Token: 0x06002F77 RID: 12151 RVA: 0x000C89D4 File Offset: 0x000C6BD4
	// (set) Token: 0x06002F78 RID: 12152 RVA: 0x000C89DC File Offset: 0x000C6BDC
	public bool WantsToRegister { get; set; }

	// Token: 0x170007A1 RID: 1953
	// (get) Token: 0x06002F79 RID: 12153 RVA: 0x000C89E8 File Offset: 0x000C6BE8
	private float SimDt
	{
		get
		{
			if (!Application.isPlaying)
			{
				float num = Mathf.Clamp((Time.realtimeSinceStartup - this.m_lastTime) * Time.timeScale, 0f, 0.02f);
				num += this.m_prevDeltTime;
				num /= 2f;
				this.m_prevDeltTime = num;
				return num;
			}
			return Mathf.Clamp(Time.smoothDeltaTime, 0f, 0.04f);
		}
	}

	// Token: 0x170007A2 RID: 1954
	// (get) Token: 0x06002F7A RID: 12154 RVA: 0x000C8A50 File Offset: 0x000C6C50
	private Material WaterMat
	{
		get
		{
			if (this.m_waterMat == null)
			{
				if (this.SeinWaterUpdateShader == null)
				{
					this.SeinWaterUpdateShader = Shader.Find("Hidden/SeinWaterUpdate");
				}
				this.m_waterMat = new Material(this.SeinWaterUpdateShader);
			}
			return this.m_waterMat;
		}
	}

	// Token: 0x170007A3 RID: 1955
	// (get) Token: 0x06002F7B RID: 12155 RVA: 0x000C8AA8 File Offset: 0x000C6CA8
	private RenderTexture WaterHeight
	{
		get
		{
			if (this.m_waterHeight == null || this.m_waterHeight.width != this.m_resolutionX || this.m_waterHeight.height != this.m_resolutionY)
			{
				if (this.m_waterHeight != null)
				{
					RenderTexture.active = null;
					UnityEngine.Object.DestroyImmediate(this.m_waterHeight);
				}
				this.m_waterHeight = this.CreateTexture();
			}
			return this.m_waterHeight;
		}
	}

	// Token: 0x170007A4 RID: 1956
	// (get) Token: 0x06002F7C RID: 12156 RVA: 0x000C8B28 File Offset: 0x000C6D28
	private RenderTexture WaterHeightDraw
	{
		get
		{
			if (this.m_waterHeightDraw == null || this.m_waterHeightDraw.width != this.m_resolutionX || this.m_waterHeightDraw.height != this.m_resolutionY)
			{
				if (this.m_waterHeightDraw != null)
				{
					RenderTexture.active = null;
					UnityEngine.Object.DestroyImmediate(this.m_waterHeight);
				}
				this.m_waterHeightDraw = this.CreateTexture();
			}
			return this.m_waterHeightDraw;
		}
	}

	// Token: 0x170007A5 RID: 1957
	// (get) Token: 0x06002F7D RID: 12157 RVA: 0x000C8BA8 File Offset: 0x000C6DA8
	private Material BlurMaterial
	{
		get
		{
			if (this.m_blurMaterial != null)
			{
				return this.m_blurMaterial;
			}
			if (this.SeinWaterBlurShader == null)
			{
				this.SeinWaterBlurShader = Shader.Find("Hidden/SeinWaterBlur");
			}
			this.m_blurMaterial = new Material(this.SeinWaterBlurShader)
			{
				hideFlags = HideFlags.HideAndDontSave
			};
			return this.m_blurMaterial;
		}
	}

	// Token: 0x06002F7E RID: 12158 RVA: 0x000C8C10 File Offset: 0x000C6E10
	private void UpdateResolution()
	{
		float num = (!this.UseHighRes) ? 5f : 7f;
		int resolutionX = Mathf.CeilToInt(num * base.transform.localScale.x);
		int resolutionY = Mathf.CeilToInt(num * (base.transform.localScale.z + this.ExtendFront) * this.ZScale);
		this.m_resolutionX = resolutionX;
		this.m_resolutionY = resolutionY;
	}

	// Token: 0x06002F7F RID: 12159 RVA: 0x000C8C8C File Offset: 0x000C6E8C
	public void UpdateBoundary()
	{
		Vector3 position = base.transform.position;
		float x = base.transform.localScale.x;
		float num = 1f;
		this.Boundary = new Rect(position.x - x * 0.5f, position.y - num, x, num);
	}

	// Token: 0x06002F80 RID: 12160 RVA: 0x000C8CE4 File Offset: 0x000C6EE4
	private void OnDisable()
	{
		UberWaterControl.All.Remove(this);
		if (UberInteractionManager.Instance != null)
		{
			UberInteractionManager.Instance.RemoveInteractor(this);
		}
	}

	// Token: 0x06002F81 RID: 12161 RVA: 0x000C8D18 File Offset: 0x000C6F18
	private void OnEnable()
	{
		for (int i = 0; i < 3; i++)
		{
			this.m_impacts[i] = new Queue<UberWaterControl.ImpactBuf>();
		}
		if (UberInteractionManager.Instance != null)
		{
			UberInteractionManager.Instance.RegisterInteractor(this);
		}
		UberWaterControl.All.Add(this);
		this.m_simTime = 0f;
		this.UpdateResolution();
		this.m_lastTime = Time.realtimeSinceStartup - 0.01f;
		this.UpdateBoundary();
	}

	// Token: 0x06002F82 RID: 12162 RVA: 0x000C8D94 File Offset: 0x000C6F94
	private RenderTexture CreateTexture()
	{
		RenderTexture renderTexture = new RenderTexture(this.m_resolutionX, this.m_resolutionY, 0, RenderTextureFormat.ARGBHalf)
		{
			useMipMap = false,
			filterMode = FilterMode.Bilinear,
			wrapMode = TextureWrapMode.Repeat,
			name = "uberWaterControl",
			hideFlags = HideFlags.HideAndDontSave
		};
		renderTexture.Create();
		Graphics.SetRenderTarget(renderTexture);
		GL.Clear(false, true, Color.clear);
		Graphics.SetRenderTarget(null);
		return renderTexture;
	}

	// Token: 0x06002F83 RID: 12163 RVA: 0x000C8E00 File Offset: 0x000C7000
	private Vector4 WaveOffset(Vector4 speed, Vector4 scale)
	{
		double num = (double)(this.m_simTime * 1f / 20f);
		return new Vector4((float)((double)(speed.x * scale.x) * num), (float)((double)(speed.y * scale.y) * num), (float)((double)(speed.w * scale.z) * num), (float)((double)(speed.z * scale.w) * num));
	}

	// Token: 0x06002F84 RID: 12164 RVA: 0x000C8E74 File Offset: 0x000C7074
	private void InitShaderIds()
	{
		if (this.pxXId == -1)
		{
			this.pxXId = Shader.PropertyToID("_PxSizeX");
			this.pxYId = Shader.PropertyToID("_PxSizeY");
			this.pxSizesId = Shader.PropertyToID("_PxSizes");
			this.dispTex = Shader.PropertyToID("_DispTex");
			this.heightId = Shader.PropertyToID("_Height");
			this.wavePowId = Shader.PropertyToID("_WavePower");
			this.dispUvId = Shader.PropertyToID("_DispUv");
			this.maxHeightId = Shader.PropertyToID("_MaxWaveHeight");
			this.waterCoords = Shader.PropertyToID("_WaterPlaneCoords");
			this.waveScaleId = Shader.PropertyToID("_WaveScale4");
			this.waveOffsetId = Shader.PropertyToID("_WaveOffset");
			this.deltTimeId = Shader.PropertyToID("deltTime");
			this.dampingId = Shader.PropertyToID("damping");
			this.elasticityId = Shader.PropertyToID("elasticity");
			this.waveSpeedId = Shader.PropertyToID("waveSpeed");
			this.noiseTexId = Shader.PropertyToID("_NoiseTex");
			this.noiseStrId = Shader.PropertyToID("_NoiseStrength");
			this.noiseOffsetId = Shader.PropertyToID("_NoiseOffset");
			this.noiseScaleId = Shader.PropertyToID("_NoiseScale4");
			this.transformScaleId = Shader.PropertyToID("_TransformScale");
		}
	}

	// Token: 0x06002F85 RID: 12165 RVA: 0x000C8FD0 File Offset: 0x000C71D0
	private void BindShaderVariables(float simDt)
	{
		this.InitShaderIds();
		this.WaterMat.SetFloat(this.deltTimeId, simDt);
		float val = Mathf.Pow(1f - this.Damping, simDt);
		this.SetFloat(this.dampingId, val, this.WaterMat);
		float value = 4f + this.Elasticity * simDt;
		this.WaterMat.SetFloat(this.elasticityId, value);
		float num = this.WaveSpeed * 5f;
		this.WaterMat.SetFloat(this.waveSpeedId, simDt * num * num);
		if (this.WaterMat.GetTexture(this.noiseTexId) != this.NoiseTexture)
		{
			this.WaterMat.SetTexture(this.noiseTexId, this.NoiseTexture);
		}
		float num2 = 1f / (float)this.m_resolutionX;
		float num3 = 1f / (float)this.m_resolutionY;
		Vector4 val2 = new Vector4(num2, num3, 1f / num2, 1f / num3);
		this.SetVector(this.pxSizesId, val2, this.WaterMat);
		Vector3 localScale = base.transform.localScale;
		float x = localScale.x;
		float num4 = localScale.z + this.ExtendFront;
		float num5 = this.NoiseScale * 0.001f;
		Vector4 vector = new Vector4(num5 * 3f * x, num5 * 3f * num4, num5 * 5f * x, num5 * 5f * num4);
		Vector4 speed = new Vector4(this.NoiseSpeed / x, this.NoiseSpeed * 2f / num4, -this.NoiseSpeed * 0.3f / x, this.NoiseSpeed / num4);
		float num6 = this.NoiseStrength * simDt;
		this.SetFloat(this.noiseStrId, num6 * num6, this.WaterMat);
		this.WaterMat.SetVector(this.noiseOffsetId, this.WaveOffset(speed, vector));
		this.WaterMat.SetVector(this.noiseScaleId, vector);
	}

	// Token: 0x06002F86 RID: 12166 RVA: 0x000C91D0 File Offset: 0x000C73D0
	private void UpdateSim()
	{
		this.m_simTime += this.SimDt;
		this.BindShaderVariables(this.SimDt * 0.5f);
		RenderTexture temporary = RenderTexture.GetTemporary(this.m_resolutionX, this.m_resolutionY, 0, RenderTextureFormat.ARGBHalf);
		temporary.wrapMode = TextureWrapMode.Repeat;
		this.SetImpact();
		Graphics.Blit(this.WaterHeight, temporary, this.WaterMat);
		this.SetImpact();
		Graphics.Blit(temporary, this.WaterHeight, this.WaterMat);
		RenderTexture.ReleaseTemporary(temporary);
		this.BlurBlit(this.WaterHeight, this.WaterHeightDraw);
	}

	// Token: 0x06002F87 RID: 12167 RVA: 0x000C9265 File Offset: 0x000C7465
	private void BlurBlit(RenderTexture source, RenderTexture destination)
	{
		if (source == null)
		{
			return;
		}
		Graphics.BlitMultiTap(source, destination, this.BlurMaterial, UberWaterControl.s_offsets);
	}

	// Token: 0x06002F88 RID: 12168 RVA: 0x000C9288 File Offset: 0x000C7488
	private void Dequeue(string bufName, UberWaterControl.ImpactBuf buf)
	{
		this.WaterMat.SetVector(bufName, new Vector4(buf.Uv.x, buf.Uv.y, buf.Radius, buf.Power * 0.5f));
	}

	// Token: 0x06002F89 RID: 12169 RVA: 0x000C92D2 File Offset: 0x000C74D2
	private void DequeueEmpty(string bufName)
	{
		this.WaterMat.SetVector(bufName, Vector4.zero);
	}

	// Token: 0x06002F8A RID: 12170 RVA: 0x000C92E8 File Offset: 0x000C74E8
	private void SetImpact()
	{
		int i = 0;
		for (int j = 0; j < 3; j++)
		{
			while (i < UberWaterControl.s_setName.Length && this.m_impacts[j].Count > 0)
			{
				this.Dequeue(UberWaterControl.s_setName[i], this.m_impacts[j].Dequeue());
				i++;
			}
			if (i >= UberWaterControl.s_setName.Length)
			{
				break;
			}
		}
		for (int k = 0; k < 3; k++)
		{
			this.m_impacts[k].Clear();
		}
		if (this.RainPower != 0f && this.RainFrequency != 0f)
		{
			this.m_rainToDo += this.SimDt * this.RainFrequency * 0.5f;
			this.m_rainToDo += this.SimDt * this.RainFrequency * 0.5f;
			int num = Mathf.Min(Mathf.FloorToInt(this.m_rainToDo), UberWaterControl.s_setName.Length - i);
			for (int l = 0; l < num; l++)
			{
				Vector2 localUv = new Vector2(UnityEngine.Random.value - 0.5f, UnityEngine.Random.value - 0.5f);
				float power = this.RainPowerSpread.Evaluate(UnityEngine.Random.value) * this.RainPower;
				float radius = this.RainRadius * ((!this.UseHighRes) ? 1f : 0.8f) + 2f * (UnityEngine.Random.value - 0.5f) * this.RaindRadiusSpread;
				UberWaterControl.ImpactBuf bufFromSettings = this.GetBufFromSettings(localUv, power, radius);
				this.Dequeue(UberWaterControl.s_setName[i], bufFromSettings);
				i++;
			}
			this.m_rainToDo -= (float)num;
		}
		while (i < UberWaterControl.s_setName.Length)
		{
			this.DequeueEmpty(UberWaterControl.s_setName[i]);
			i++;
		}
	}

	// Token: 0x06002F8B RID: 12171 RVA: 0x000C94D2 File Offset: 0x000C76D2
	private void Update()
	{
		if (this.DoSim)
		{
			this.UpdateSim();
		}
		this.m_lastTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06002F8C RID: 12172 RVA: 0x000C94F0 File Offset: 0x000C76F0
	private void Splash(Vector3 pos, float power, UberInteractionActor actor)
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (this.m_splashes == null)
		{
			this.m_splashes = new List<UberWaterControl.SplashInfo>(15);
		}
		if (power < this.SplashSpawnAtPower)
		{
			return;
		}
		for (int i = this.m_splashes.Count - 1; i >= 0; i--)
		{
			if (Time.realtimeSinceStartup - this.m_splashes[i].Time > 0.3f)
			{
				this.m_splashes.RemoveAt(i);
			}
		}
		for (int j = 0; j < this.m_splashes.Count; j++)
		{
			if ((this.m_splashes[j].Position - pos).sqrMagnitude < 0.25f)
			{
				return;
			}
		}
		GameObject splashPrefab = this.SplashPrefab;
		if (actor != null && actor.OverrideSplash)
		{
			splashPrefab = actor.SplashPrefab;
		}
		if (splashPrefab == null)
		{
			return;
		}
		this.m_splashes.Add(new UberWaterControl.SplashInfo
		{
			Position = pos,
			Time = Time.realtimeSinceStartup
		});
		InstantiateUtility.Instantiate(splashPrefab, new Vector3(pos.x, base.transform.position.y + 0.05f, pos.z), Quaternion.identity);
	}

	// Token: 0x06002F8D RID: 12173 RVA: 0x000C965C File Offset: 0x000C785C
	public bool IsOverWater(Vector3 pos)
	{
		Vector3 vector = pos - base.transform.position;
		Vector2 vector2 = new Vector2(vector.x / base.transform.localScale.x, (vector.z + this.ExtendFront) / (base.transform.localScale.z + this.ExtendFront) - 0.5f);
		return vector2.x > -0.5f && vector2.x < 0.5f && vector2.y > -0.5f && vector2.y < 0.5f;
	}

	// Token: 0x06002F8E RID: 12174 RVA: 0x000C9710 File Offset: 0x000C7910
	public void Impact(Vector3 pos, float power, float radius, bool splash, int priority)
	{
		Vector3 vector = base.transform.InverseTransformDirection(pos - base.transform.position);
		Vector2 localUv = new Vector2(vector.x / base.transform.localScale.x, (vector.z + this.ExtendFront) / (base.transform.localScale.z + this.ExtendFront) - 0.5f);
		if (splash)
		{
			this.Splash(pos, power, null);
		}
		this.ImpactLocal(localUv, power, radius, priority);
	}

	// Token: 0x06002F8F RID: 12175 RVA: 0x000C97A8 File Offset: 0x000C79A8
	private void ImpactLocal(Vector2 localUv, float power, float radius, int priority)
	{
		if (priority >= 3)
		{
			Debug.LogError("Unsupported priority range");
			return;
		}
		UberWaterControl.ImpactBuf bufFromSettings = this.GetBufFromSettings(localUv, power, radius);
		this.m_impacts[priority].Enqueue(bufFromSettings);
	}

	// Token: 0x06002F90 RID: 12176 RVA: 0x000C97E4 File Offset: 0x000C79E4
	private UberWaterControl.ImpactBuf GetBufFromSettings(Vector2 localUv, float power, float radius)
	{
		UberWaterControl.ImpactBuf result = default(UberWaterControl.ImpactBuf);
		float radius2 = Mathf.Max(1f, radius * this.VerticesPerMeterWaveline);
		result.Uv = new Vector2(localUv.x * (float)this.m_resolutionX, localUv.y * (float)this.m_resolutionY);
		result.Radius = radius2;
		result.Power = power;
		return result;
	}

	// Token: 0x06002F91 RID: 12177 RVA: 0x000C9848 File Offset: 0x000C7A48
	private void OnDestroy()
	{
		RenderTexture.active = null;
		if (this.m_waterHeight != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_waterHeight);
			this.m_waterHeight = null;
		}
		if (this.m_waterMat != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_waterMat);
		}
		if (this.m_waterHeightDraw != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_waterHeightDraw);
			this.m_waterHeightDraw = null;
		}
		if (this.m_waterMat != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_waterMat);
			this.m_waterMat = null;
		}
		if (this.m_blurMaterial != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_blurMaterial);
			this.m_blurMaterial = null;
		}
	}

	// Token: 0x06002F92 RID: 12178 RVA: 0x000C9903 File Offset: 0x000C7B03
	private void SetFloat(int id, float val, Material mat)
	{
		mat.SetFloat(id, val);
	}

	// Token: 0x06002F93 RID: 12179 RVA: 0x000C990D File Offset: 0x000C7B0D
	private void SetVector(int id, Vector4 val, Material mat)
	{
		mat.SetVector(id, val);
	}

	// Token: 0x06002F94 RID: 12180 RVA: 0x000C9918 File Offset: 0x000C7B18
	public void BindShaderVariablesToMaterial(Material mat)
	{
		this.InitShaderIds();
		Vector3 position = base.transform.position;
		Vector3 localScale = base.transform.localScale;
		this.SetFloat(this.pxXId, 1f / (float)this.m_resolutionX, mat);
		this.SetFloat(this.pxYId, 1f / (float)this.m_resolutionX, mat);
		mat.SetTexture(this.dispTex, this.WaterHeightDraw);
		this.SetFloat(this.heightId, this.WaveHeightScale, mat);
		this.SetFloat(this.wavePowId, this.WaterlineRaisePower / 100000f, mat);
		this.SetFloat(this.dispUvId, Mathf.Clamp01((this.ExtendFront - this.WaterlineSimDepth) / localScale.z), mat);
		this.SetFloat(this.maxHeightId, this.MaxWaveHeight, mat);
		this.SetVector(this.waterCoords, new Vector4(position.x - localScale.x * 0.5f, localScale.x, position.z - this.ExtendFront, this.ExtendFront + localScale.z), mat);
		Vector4 vector = new Vector4(0.13f, 0.13f, 0.051999997f, 0.051999997f);
		Vector4 speed = new Vector4(48.3f, 14.2f, -12.2f, -6.4f);
		this.SetVector(this.waveScaleId, vector, mat);
		this.SetVector(this.waveOffsetId, this.WaveOffset(speed, vector), mat);
		Vector3 lossyScale = base.transform.lossyScale;
		this.SetVector(this.transformScaleId, new Vector4(1f / lossyScale.x, 1f / lossyScale.y, 0f, 0f), mat);
	}

	// Token: 0x06002F95 RID: 12181 RVA: 0x000C9AD8 File Offset: 0x000C7CD8
	public static UberWaterControl GetNearestWaterControl(GameObject gameObject)
	{
		float num = float.MaxValue;
		UberWaterControl uberWaterControl = null;
		Vector3 position = gameObject.transform.position;
		for (int i = 0; i < UberWaterControl.All.Count; i++)
		{
			UberWaterControl uberWaterControl2 = UberWaterControl.All[i];
			Plane plane = new Plane(Vector3.up, uberWaterControl2.transform.position);
			float distanceToPoint = plane.GetDistanceToPoint(position);
			if (uberWaterControl2.IsOverWater(position))
			{
				if (distanceToPoint < num)
				{
					uberWaterControl = uberWaterControl2;
					num = distanceToPoint;
				}
			}
		}
		if (uberWaterControl != null)
		{
			return uberWaterControl;
		}
		for (int j = 0; j < UberWaterControl.All.Count; j++)
		{
			UberWaterControl uberWaterControl3 = UberWaterControl.All[j];
			float num2 = Vector3.Distance(uberWaterControl3.transform.position, position);
			if (num2 < num)
			{
				uberWaterControl = uberWaterControl3;
				num = num2;
			}
		}
		return uberWaterControl;
	}

	// Token: 0x06002F96 RID: 12182 RVA: 0x000C9BCC File Offset: 0x000C7DCC
	public void SetInteraction(float time, Vector3 position, Vector3 historicPos, Vector4 strength, Vector3 velocity, float radius, bool explosion)
	{
		float f = velocity.magnitude * this.SimDt;
		float power = Mathf.Pow(f, this.ForceRaisePower) * strength.z * 2.5f;
		radius = Mathf.Min(radius, 10f);
		this.Impact(position, power, radius, true, 0);
	}

	// Token: 0x06002F97 RID: 12183 RVA: 0x000C9C20 File Offset: 0x000C7E20
	public bool DoesOverlap(Vector3 position, Vector3 velocity, float radius, float zScale)
	{
		Vector3 vector = position - velocity / 60f;
		float num = (vector.x >= position.x) ? position.x : vector.x;
		float num2 = (vector.x <= position.x) ? position.x : vector.x;
		float num3 = (vector.y >= position.y) ? position.y : vector.y;
		float num4 = (vector.y <= position.y) ? position.y : vector.y;
		return this.Boundary.Overlaps(new Rect
		{
			xMin = num - radius,
			xMax = num2 + radius,
			yMin = num3 - radius,
			yMax = num4 + radius
		});
	}

	// Token: 0x06002F98 RID: 12184 RVA: 0x000C9D1A File Offset: 0x000C7F1A
	public Vector3 GetPosition()
	{
		return base.transform.position;
	}

	// Token: 0x06002F99 RID: 12185 RVA: 0x000C9D28 File Offset: 0x000C7F28
	public Vector3 GetExplodePoint(Vector3 position)
	{
		Plane plane = default(Plane);
		plane.SetNormalAndPosition(Vector3.up, base.transform.position);
		float distanceToPoint = plane.GetDistanceToPoint(position);
		return position - distanceToPoint * Vector3.up;
	}

	// Token: 0x06002F9A RID: 12186 RVA: 0x000C9D6E File Offset: 0x000C7F6E
	public float MaxRadius()
	{
		return 1000f;
	}

	// Token: 0x06002F9B RID: 12187 RVA: 0x000C9D75 File Offset: 0x000C7F75
	public bool IsWater()
	{
		return true;
	}

	// Token: 0x06002F9C RID: 12188 RVA: 0x000C9D78 File Offset: 0x000C7F78
	public void OnRegistered()
	{
	}

	// Token: 0x04002A71 RID: 10865
	private const float c_splashCooldown = 0.3f;

	// Token: 0x04002A72 RID: 10866
	private const float c_texelsPerMeter = 5f;

	// Token: 0x04002A73 RID: 10867
	private const float c_texelsPerMeterHigh = 7f;

	// Token: 0x04002A74 RID: 10868
	private const int c_impactCount = 3;

	// Token: 0x04002A75 RID: 10869
	public Rect Boundary;

	// Token: 0x04002A76 RID: 10870
	[Range(0f, 0.999f)]
	public float Damping = 0.9f;

	// Token: 0x04002A77 RID: 10871
	[Range(0f, 15f)]
	public float Elasticity = 3f;

	// Token: 0x04002A78 RID: 10872
	[Range(0.25f, 6f)]
	public float WaveSpeed = 6f;

	// Token: 0x04002A79 RID: 10873
	[Range(1f, 2f)]
	public float ForceRaisePower = 1.2f;

	// Token: 0x04002A7A RID: 10874
	[Range(0.01f, 1f)]
	public float MaxWaveHeight = 0.2f;

	// Token: 0x04002A7B RID: 10875
	[Range(0f, 10f)]
	public float WaveHeightScale = 3f;

	// Token: 0x04002A7C RID: 10876
	[Range(0.05f, 4f)]
	public float WaterlineRaisePower = 2f;

	// Token: 0x04002A7D RID: 10877
	[Range(0f, 20f)]
	public float NoiseStrength = 5f;

	// Token: 0x04002A7E RID: 10878
	[Range(0f, 100f)]
	public float NoiseSpeed = 30f;

	// Token: 0x04002A7F RID: 10879
	public float NoiseScale = 1.4f;

	// Token: 0x04002A80 RID: 10880
	[Range(0.1f, 8f)]
	public float VerticesPerMeterWaveline = 5f;

	// Token: 0x04002A81 RID: 10881
	[Range(0f, 250f)]
	public float RainFrequency;

	// Token: 0x04002A82 RID: 10882
	[Range(0f, 60f)]
	public float RainPower = 6f;

	// Token: 0x04002A83 RID: 10883
	[Range(0.75f, 2f)]
	public float ZScale = 1f;

	// Token: 0x04002A84 RID: 10884
	public AnimationCurve RainPowerSpread = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x04002A85 RID: 10885
	public float RainRadius = 1f;

	// Token: 0x04002A86 RID: 10886
	public float RaindRadiusSpread = 0.1f;

	// Token: 0x04002A87 RID: 10887
	public Texture2D NoiseTexture;

	// Token: 0x04002A88 RID: 10888
	public int ReflectResolution = 256;

	// Token: 0x04002A89 RID: 10889
	public LayerMask RealtimeReflectLayers = -1;

	// Token: 0x04002A8A RID: 10890
	public GameObject SplashPrefab;

	// Token: 0x04002A8B RID: 10891
	public float SplashSpawnAtPower = 1f;

	// Token: 0x04002A8C RID: 10892
	public bool CrossSection = true;

	// Token: 0x04002A8D RID: 10893
	public float ExtendFront = 1f;

	// Token: 0x04002A8E RID: 10894
	public float ExtendBack;

	// Token: 0x04002A8F RID: 10895
	public float ExtendLeft;

	// Token: 0x04002A90 RID: 10896
	public float ExtendRight;

	// Token: 0x04002A91 RID: 10897
	public float WaterlineSimDepth;

	// Token: 0x04002A92 RID: 10898
	public bool UseHighRes;

	// Token: 0x04002A93 RID: 10899
	public Shader SeinWaterUpdateShader;

	// Token: 0x04002A94 RID: 10900
	public Shader SeinWaterBlurShader;

	// Token: 0x04002A95 RID: 10901
	public Material TopMaterial;

	// Token: 0x04002A96 RID: 10902
	public Material CrossMaterial;

	// Token: 0x04002A97 RID: 10903
	public Material EdgeMaterial;

	// Token: 0x04002A98 RID: 10904
	public bool DoSim;

	// Token: 0x04002A99 RID: 10905
	private List<UberWaterControl.SplashInfo> m_splashes;

	// Token: 0x04002A9A RID: 10906
	private Vector3 m_oldScale;

	// Token: 0x04002A9B RID: 10907
	private int m_resolutionX;

	// Token: 0x04002A9C RID: 10908
	private int m_resolutionY;

	// Token: 0x04002A9D RID: 10909
	private float m_rainToDo;

	// Token: 0x04002A9E RID: 10910
	private Material m_waterMat;

	// Token: 0x04002A9F RID: 10911
	private RenderTexture m_waterHeight;

	// Token: 0x04002AA0 RID: 10912
	private RenderTexture m_waterHeightDraw;

	// Token: 0x04002AA1 RID: 10913
	private Queue<UberWaterControl.ImpactBuf>[] m_impacts = new Queue<UberWaterControl.ImpactBuf>[3];

	// Token: 0x04002AA2 RID: 10914
	private float m_lastTime;

	// Token: 0x04002AA3 RID: 10915
	private float m_prevDeltTime;

	// Token: 0x04002AA4 RID: 10916
	public static readonly AllContainer<UberWaterControl> All = new AllContainer<UberWaterControl>();

	// Token: 0x04002AA5 RID: 10917
	private Material m_blurMaterial;

	// Token: 0x04002AA6 RID: 10918
	private float m_simTime;

	// Token: 0x04002AA7 RID: 10919
	private int pxXId = -1;

	// Token: 0x04002AA8 RID: 10920
	private int pxYId = -1;

	// Token: 0x04002AA9 RID: 10921
	private int pxSizesId = -1;

	// Token: 0x04002AAA RID: 10922
	private int dispTex = -1;

	// Token: 0x04002AAB RID: 10923
	private int heightId = -1;

	// Token: 0x04002AAC RID: 10924
	private int wavePowId = -1;

	// Token: 0x04002AAD RID: 10925
	private int dispUvId = -1;

	// Token: 0x04002AAE RID: 10926
	private int maxHeightId = -1;

	// Token: 0x04002AAF RID: 10927
	private int waterCoords = -1;

	// Token: 0x04002AB0 RID: 10928
	private int waveScaleId = -1;

	// Token: 0x04002AB1 RID: 10929
	private int waveOffsetId = -1;

	// Token: 0x04002AB2 RID: 10930
	private int deltTimeId = -1;

	// Token: 0x04002AB3 RID: 10931
	private int dampingId = -1;

	// Token: 0x04002AB4 RID: 10932
	private int elasticityId = -1;

	// Token: 0x04002AB5 RID: 10933
	private int waveSpeedId = -1;

	// Token: 0x04002AB6 RID: 10934
	private int noiseTexId = -1;

	// Token: 0x04002AB7 RID: 10935
	private int noiseStrId = -1;

	// Token: 0x04002AB8 RID: 10936
	private int noiseOffsetId = -1;

	// Token: 0x04002AB9 RID: 10937
	private int noiseScaleId = -1;

	// Token: 0x04002ABA RID: 10938
	private int transformScaleId = -1;

	// Token: 0x04002ABB RID: 10939
	private static Vector2[] s_offsets = new Vector2[]
	{
		new Vector2(-0.5f, -0.5f),
		new Vector2(0.5f, -0.5f),
		new Vector2(0.5f, 0.5f),
		new Vector2(-0.5f, 0.5f)
	};

	// Token: 0x04002ABC RID: 10940
	private static string[] s_setName = new string[]
	{
		"impactBuf0",
		"impactBuf1",
		"impactBuf2",
		"impactBuf3",
		"impactBuf4",
		"impactBuf5",
		"impactBuf6",
		"impactBuf7",
		"impactBuf8"
	};

	// Token: 0x02000855 RID: 2133
	private struct SplashInfo
	{
		// Token: 0x04002BB4 RID: 11188
		public float Time;

		// Token: 0x04002BB5 RID: 11189
		public Vector3 Position;
	}

	// Token: 0x02000856 RID: 2134
	private struct ImpactBuf
	{
		// Token: 0x04002BB6 RID: 11190
		public float Power;

		// Token: 0x04002BB7 RID: 11191
		public float Radius;

		// Token: 0x04002BB8 RID: 11192
		public Vector2 Uv;
	}
}
