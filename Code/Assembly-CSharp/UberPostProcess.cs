using System;
using System.Collections.Generic;
using Frameworks;
using UnityEngine;

// Token: 0x02000145 RID: 325
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Sein Post Processing")]
public class UberPostProcess : MonoBehaviour
{
	// Token: 0x17000271 RID: 625
	// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x000395FF File Offset: 0x000377FF
	// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x00039607 File Offset: 0x00037807
	public Vector3 Speed { get; set; }

	// Token: 0x17000272 RID: 626
	// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x00039610 File Offset: 0x00037810
	private float CurrentTime
	{
		get
		{
			return (!Application.isPlaying) ? Time.realtimeSinceStartup : Time.time;
		}
	}

	// Token: 0x17000273 RID: 627
	// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0003962B File Offset: 0x0003782B
	public static UberPostProcess Instance
	{
		get
		{
			return UberPostProcess.s_instance;
		}
	}

	// Token: 0x17000274 RID: 628
	// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00039632 File Offset: 0x00037832
	// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x0003963C File Offset: 0x0003783C
	public bool DoRender
	{
		get
		{
			return this.m_doRender;
		}
		set
		{
			Camera camera = this.Camera;
			if (this.m_doRender)
			{
				this.m_originalColor = camera.backgroundColor;
			}
			this.m_doRender = value;
			if (this.m_doRender)
			{
				camera.cullingMask = this.m_originalCullingMask;
				camera.backgroundColor = this.m_originalColor;
			}
			else
			{
				camera.cullingMask = 0;
				camera.backgroundColor = Color.black;
			}
		}
	}

	// Token: 0x17000275 RID: 629
	// (get) Token: 0x06000CAA RID: 3242 RVA: 0x000396A8 File Offset: 0x000378A8
	// (set) Token: 0x06000CAB RID: 3243 RVA: 0x000396EB File Offset: 0x000378EB
	public bool DoMotionBlur
	{
		get
		{
			bool result = Application.isPlaying && this.m_doMotionBlur;
			if (GameSettings.Instance != null && !GameSettings.Instance.MotionBlurEnabled)
			{
				result = false;
			}
			return result;
		}
		set
		{
			this.m_doMotionBlur = value;
		}
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x000396F4 File Offset: 0x000378F4
	private void Awake()
	{
		this.Camera = base.GetComponent<Camera>();
		this.m_originalCullingMask = this.Camera.cullingMask;
		this.DoRender = true;
		RenderTexture temporary = RenderTexture.GetTemporary(4, 4, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(4, 4, 0);
		Graphics.Blit(temporary, temporary2);
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
		RenderTexture.active = null;
		if (this.IsSubCam)
		{
			UberPostProcess.s_subCams.Add(this);
			this.Camera.enabled = false;
		}
		else
		{
			UberPostProcess.s_instance = this;
		}
	}

	// Token: 0x06000CAD RID: 3245 RVA: 0x0003977D File Offset: 0x0003797D
	public void QueueGrabPass(MeshFilter go)
	{
		if (go != null)
		{
			this.m_renderDistort.Add(go);
		}
	}

	// Token: 0x06000CAE RID: 3246 RVA: 0x00039797 File Offset: 0x00037997
	public void PushInterestZone(UberMotionBlurInterestZone zone)
	{
		this.m_renderZones.Add(zone);
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x000397A5 File Offset: 0x000379A5
	private void BindAll()
	{
		this.Rebind();
		this.RebindFrequent();
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x000397B4 File Offset: 0x000379B4
	private void OnEnable()
	{
		if (!Application.isPlaying)
		{
			UberPostProcess.s_instance = this;
		}
		this.Camera.transparencySortMode = TransparencySortMode.Orthographic;
		this.BindAll();
	}

	// Token: 0x06000CB1 RID: 3249 RVA: 0x000397E4 File Offset: 0x000379E4
	private void UpdateTargetTexture()
	{
		if (!this.PostTarget || this.PostTarget.width != Camera.current.pixelWidth || this.PostTarget.height != Camera.current.pixelHeight)
		{
			this.DestroyTargetTex(this.PostTarget);
			this.PostTarget = new RenderTexture(Camera.current.pixelWidth, Camera.current.pixelHeight, 24, RenderTextureFormat.ARGB32);
			this.m_dirty = true;
		}
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x0003986A File Offset: 0x00037A6A
	private void DestroyTargetTex(RenderTexture tex)
	{
		if (tex != null)
		{
			UnityEngine.Object.DestroyImmediate(tex);
		}
	}

	// Token: 0x06000CB3 RID: 3251 RVA: 0x00039880 File Offset: 0x00037A80
	private Material CheckShaderAndCreateMaterial(UnityEngine.Shader s, Material m2Create)
	{
		if (m2Create != null)
		{
			return m2Create;
		}
		m2Create = new Material(s)
		{
			hideFlags = HideFlags.DontSave
		};
		return m2Create;
	}

	// Token: 0x06000CB4 RID: 3252 RVA: 0x000398B0 File Offset: 0x00037AB0
	public void CreateResources()
	{
		this.m_material = this.CheckShaderAndCreateMaterial(this.Shader, this.m_material);
		this.m_lensFlareMaterial = this.CheckShaderAndCreateMaterial(this.LensFlareShader, this.m_lensFlareMaterial);
		this.m_brightPassFilterMaterial = this.CheckShaderAndCreateMaterial(this.BrightPassFilterShader, this.m_brightPassFilterMaterial);
		this.m_blendMaterial = this.CheckShaderAndCreateMaterial(this.ScreenBlend, this.m_blendMaterial);
		this.m_upsampleMat = this.CheckShaderAndCreateMaterial(this.UpsampleShader, this.m_upsampleMat);
	}

	// Token: 0x06000CB5 RID: 3253 RVA: 0x00039935 File Offset: 0x00037B35
	public void SetDirty()
	{
		this.m_dirty = true;
	}

	// Token: 0x06000CB6 RID: 3254 RVA: 0x00039940 File Offset: 0x00037B40
	private void RebindFrequent()
	{
		UberPostCacheIds.Initialize();
		this.BindBrightnessContrast();
		this.BindBrightnessContrastUI();
		float num = Mathf.Lerp(this.Desaturation.Amount - this.AdditiveDesaturation, 1f, this.FaderBrightnessContrastSettings.Contrast);
		if (this.m_prevDesaturation != num)
		{
			if (num > 0f)
			{
				this.m_material.SetFloat(UberPostCacheIds.Desat, num);
				this.m_material.DisableKeyword("NO_DESAT");
				this.m_material.EnableKeyword("DESAT");
			}
			else
			{
				this.m_material.DisableKeyword("DESAT");
				this.m_material.EnableKeyword("NO_DESAT");
			}
			this.m_prevDesaturation = num;
		}
		this.m_material.SetFloat(UberPostCacheIds.Threshold, this.BloomThreshhold + this.AdditiveBloomThreshhold);
		float num2 = this.Vignetting.Intensity + this.AdditiveVignettingIntensity;
		this.m_material.SetFloat(UberPostCacheIds.Intensity, -num2 * 0.1f / 7f);
		this.m_material.SetFloat(UberPostCacheIds.BloomIntensity, this.BloomIntensity + this.AdditiveBloomIntensity);
		this.DistortBind();
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x00039A6C File Offset: 0x00037C6C
	private void BindBrightnessContrastUI()
	{
		float num = 1f;
		float num2 = 0f;
		GameSettings instance = GameSettings.Instance;
		if (instance != null)
		{
			num2 += instance.Brightness * 0.2f;
			num += instance.Contrast * 0.2f;
		}
		num2 += 0.5f - 0.5f * num;
		this.m_material.SetVector(UberPostCacheIds.ContrastBrightUI, new Vector4(num, num2, 0f, 0f));
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x00039AE8 File Offset: 0x00037CE8
	private void BindBrightnessContrast()
	{
		float num = this.Contrast.Contrast + this.AdditiveContrast.Contrast;
		float num2 = this.Contrast.Brightness + this.AdditiveContrast.Brightness;
		num2 = Mathf.Lerp(num2, this.FaderBrightnessContrastSettings.Brightness, this.FaderBrightnessContrastSettings.Weight);
		num2 += 0.5f - 0.5f * num;
		this.m_material.SetVector(UberPostCacheIds.ContrastBright, new Vector4(num, num2, 0f, 0f));
	}

	// Token: 0x06000CB9 RID: 3257 RVA: 0x00039B74 File Offset: 0x00037D74
	private void Rebind()
	{
		this.CreateResources();
		this.m_material.SetFloat(UberPostCacheIds.Desat, this.Desaturation.Amount);
		float num;
		this.SetAnimationCurveForChannel(this.ColorCorrection.Red, 0, out num);
		float num2;
		this.SetAnimationCurveForChannel(this.ColorCorrection.Green, 1, out num2);
		float num3;
		this.SetAnimationCurveForChannel(this.ColorCorrection.Blue, 2, out num3);
		this.m_material.SetVector(UberPostCacheIds.BezierLengths, new Vector4(1f / num, 1f / num2, 1f / num3, 1f));
		this.m_dirty = false;
	}

	// Token: 0x06000CBA RID: 3258 RVA: 0x00039C14 File Offset: 0x00037E14
	private float RandSmooth(float freq, float y)
	{
		return Mathf.PerlinNoise(this.m_twirlTime * freq, y) * 2f - 1f;
	}

	// Token: 0x06000CBB RID: 3259 RVA: 0x00039C30 File Offset: 0x00037E30
	private void DistortBind()
	{
		if (this.TwirlSettings.Strength != this.m_prevTwirl)
		{
			if (Mathf.Abs(this.TwirlSettings.Strength) > 0f)
			{
				this.m_twirlTime += Mathf.Min(this.CurrentTime - this.m_lastTwirlCurrentTime, Time.deltaTime);
				this.m_lastTwirlCurrentTime = this.CurrentTime;
				float strength = this.TwirlSettings.Strength;
				Vector2 b = new Vector2(0.5f + this.RandSmooth(0.3f, 0f) * this.TwirlSettings.PosVariation * 16f / 9f * 2f, 0.5f + this.RandSmooth(0.3f, 0.5f) * this.TwirlSettings.PosVariation * 9f / 16f * 2f);
				b = Vector2.Lerp(Vector2.one * 0.5f, b, Mathf.Clamp01(strength * 10f));
				this.m_material.SetVector(UberPostCacheIds.CenterRadius, new Vector4(b.x, b.y, 1f, 1f));
				this.m_material.SetFloat(UberPostCacheIds.TwirlAngle, -strength / 3f * 0.017453292f);
				this.m_material.EnableKeyword("TWIRL");
			}
			else
			{
				this.m_material.DisableKeyword("TWIRL");
			}
			this.m_prevTwirl = this.TwirlSettings.Strength;
		}
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x00039DBC File Offset: 0x00037FBC
	private void SetAnimationCurveForChannel(AnimationCurve curve, int channel, out float len)
	{
		Vector2 v = new Vector2(curve[0].time, curve[0].value);
		Vector2 v2 = new Vector2(curve[1].time, curve[1].value);
		UberPostProcess.Bezier bezier = new UberPostProcess.Bezier(v, v2, curve[0].outTangent, curve[1].inTangent);
		Vector4 vector = new Vector4(bezier.P0.y, bezier.P1.y * 3f, bezier.P2.y * 3f, bezier.P3.y);
		len = bezier.P3.x;
		int nameID = -1;
		switch (channel)
		{
		case 0:
			nameID = UberPostCacheIds.BezierR;
			break;
		case 1:
			nameID = UberPostCacheIds.BezierG;
			break;
		case 2:
			nameID = UberPostCacheIds.BezierB;
			break;
		}
		this.m_material.SetVector(nameID, vector);
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x00039EEB File Offset: 0x000380EB
	private void OnPreCull()
	{
		UnityEngine.Shader.SetGlobalMatrix(UberPostCacheIds.Camera2World, this.Camera.cameraToWorldMatrix);
	}

	// Token: 0x06000CBE RID: 3262 RVA: 0x00039F04 File Offset: 0x00038104
	private void Update()
	{
		if (!Application.isPlaying)
		{
			Vector3 a = base.transform.position - this.m_lastPosition;
			this.Speed = a / Time.fixedDeltaTime;
			this.m_lastPosition = base.transform.position;
		}
		UnityEngine.Shader.SetGlobalFloat(UberPostCacheIds.TxtAlphaCenter, 0.5f);
	}

	// Token: 0x06000CBF RID: 3263 RVA: 0x00039F64 File Offset: 0x00038164
	private void UpdateDirectionalBlur()
	{
		if (!this.DoMotionBlur)
		{
			this.m_material.DisableKeyword("MOTION_BLUR");
			this.m_material.EnableKeyword("NO_MOTION_BLUR");
			this.m_material.SetVector(UberPostCacheIds.SpeedVec, Vector3.zero);
		}
		else
		{
			this.m_material.EnableKeyword("MOTION_BLUR");
			this.m_material.DisableKeyword("NO_MOTION_BLUR");
			float num = this.Speed.magnitude;
			num = Mathf.Clamp(num - Mathf.Clamp(this.MotionBlurThreshold, 0f, num), 0f, 22f);
			num *= this.MotionBlurMultiplier;
			if (num == 0f)
			{
				this.m_material.SetVector(UberPostCacheIds.SpeedVec, Vector4.zero);
				return;
			}
			this.m_material.SetVector(UberPostCacheIds.SpeedVec, this.Speed.normalized * num * this.MotionBlurSpread * 0.0005f);
		}
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x0003A078 File Offset: 0x00038278
	private void OnPreRender()
	{
		Vector3 vector = Camera.current.ViewportToWorldPoint(new Vector3(0f, 0f, -Camera.current.transform.position.z));
		Vector3 vector2 = Camera.current.ViewportToWorldPoint(new Vector3(1f, 1f, -Camera.current.transform.position.z));
		if (Camera.main)
		{
			Vector3 v = Camera.main.transform.position - this.m_lastPos;
			this.m_lastPos = Camera.main.transform.position;
			if (v.magnitude > 10f)
			{
				v = Vector3.zero;
			}
			v = new Vector4(v.x / (vector2.x - vector.x), v.y / (vector2.y - vector.y), 0f, 0f);
			UnityEngine.Shader.SetGlobalVector(UberPostCacheIds.CameraVelocity, v);
		}
		else
		{
			UnityEngine.Shader.SetGlobalVector(UberPostCacheIds.CameraVelocity, Vector2.zero);
		}
		this.UpdateTargetTexture();
	}

	// Token: 0x06000CC1 RID: 3265 RVA: 0x0003A1B4 File Offset: 0x000383B4
	public void CreateScreenshot(RenderTexture renderTexture)
	{
		RenderTexture.active = renderTexture;
		if (this.m_screenshot == null || this.m_screenshot.width != renderTexture.width || this.m_screenshot.height != renderTexture.height)
		{
			if (this.m_screenshot)
			{
				UnityEngine.Object.DestroyObject(this.m_screenshot);
			}
			this.m_screenshot = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
		}
		this.m_screenshot.ReadPixels(new Rect(0f, 0f, (float)renderTexture.width, (float)renderTexture.height), 0, 0);
		this.m_screenshot.EncodeToPNG();
	}

	// Token: 0x06000CC2 RID: 3266 RVA: 0x0003A270 File Offset: 0x00038470
	private void DoPost(RenderTexture source, RenderTexture destination)
	{
		if (this.m_dirty || this.m_material == null)
		{
			this.Rebind();
		}
		if (this.m_material == null)
		{
			Debug.LogError("Couldn't load post material!");
			Graphics.Blit(source, destination);
			return;
		}
		this.RebindFrequent();
		if (!Application.isPlaying)
		{
			this.Rebind();
		}
		this.CurrentAlphaBuffer.OnRenderImage(source, this.m_material);
		this.CurrentAlphaBuffer.SetCurrentAlphaGrab();
		RenderTexture temporary = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, RenderTextureFormat.ARGB32);
		RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, RenderTextureFormat.ARGB32);
		Graphics.Blit(source, temporary, this.m_material, 1);
		UberGaussianBlur.BlurPingPong(temporary, temporary2, 2, this.SepBlurSpread);
		temporary2.DiscardContents();
		if (this.m_renderDistort == null)
		{
			this.m_renderDistort = new List<MeshFilter>();
		}
		RenderTexture temporary3 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, RenderTextureFormat.RGHalf);
		this.RenderDistortions(temporary3);
		this.SetInterestZones();
		this.m_material.SetTexture(UberPostCacheIds.ColorBuffer, temporary);
		this.UpdateDirectionalBlur();
		Graphics.Blit(source, this.PostTarget, this.m_material, 0);
		if (this.m_doBlur)
		{
			RenderTexture temporary4 = RenderTexture.GetTemporary(this.PostTarget.width, this.PostTarget.height, 0, RenderTextureFormat.ARGB32);
			UberGaussianBlur.BlurPingPong(this.PostTarget, temporary4, 5, 0.4f);
			RenderTexture.ReleaseTemporary(temporary4);
		}
		if (!this.IsSubCam)
		{
			Rect rect;
			for (int i = 0; i < UberPostProcess.s_subCams.Count; i++)
			{
				if (UberPostProcess.s_subCams[i].gameObject.activeInHierarchy)
				{
					Camera camera = UberPostProcess.s_subCams[i].Camera;
					camera.targetTexture = this.PostTarget;
					rect = camera.rect;
					camera.rect = new Rect(0f, 0f, 1f, 1f);
					camera.Render();
					camera.rect = rect;
					camera.targetTexture = null;
				}
			}
			Camera camera2 = UberUIPostProcess.Instance.Camera;
			camera2.targetTexture = this.PostTarget;
			rect = camera2.rect;
			camera2.rect = new Rect(0f, 0f, 1f, 1f);
			camera2.Render();
			camera2.rect = rect;
			camera2.targetTexture = null;
			Graphics.Blit(this.PostTarget, destination, this.m_material, 3);
		}
		else
		{
			Graphics.Blit(this.PostTarget, destination);
		}
		if (temporary3 != null)
		{
			RenderTexture.ReleaseTemporary(temporary3);
		}
		if (temporary2 != null)
		{
			RenderTexture.ReleaseTemporary(temporary2);
		}
		if (temporary != null)
		{
			RenderTexture.ReleaseTemporary(temporary);
		}
	}

	// Token: 0x06000CC3 RID: 3267 RVA: 0x0003A54E File Offset: 0x0003874E
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.DoPost(source, destination);
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x0003A558 File Offset: 0x00038758
	private void SetInterestZones()
	{
		if (this.m_renderZones.Count > 3)
		{
			this.m_renderZones.Sort(UberPostProcess.s_compare);
		}
		int count = this.m_renderZones.Count;
		for (int i = 0; i < 3; i++)
		{
			if (i < count)
			{
				if (!(this.m_renderZones[i] == null))
				{
					this.m_renderZones[i].DoBind(this.m_material, UberPostProcess.s_zoneNames[i]);
				}
			}
			else
			{
				this.m_material.SetVector(UberPostProcess.s_zoneNames[i], new Vector4(1000f, 1000f, 1000f, 1000f));
			}
		}
		this.m_renderZones.Clear();
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x0003A620 File Offset: 0x00038820
	private static int ZoneSort(UberMotionBlurInterestZone x, UberMotionBlurInterestZone y)
	{
		int type = (int)x.Type;
		int type2 = (int)y.Type;
		if (type == type2)
		{
			return y.Radius.CompareTo(x.Radius);
		}
		return type2.CompareTo(type);
	}

	// Token: 0x06000CC6 RID: 3270 RVA: 0x0003A65C File Offset: 0x0003885C
	private void RenderDistortions(RenderTexture distortBuffer)
	{
		if (distortBuffer == null)
		{
			return;
		}
		Color backgroundColor = new Color(0.49803922f, 0.49803922f, 0.49803922f);
		if (this.m_renderDistort.Count > 0)
		{
			Graphics.SetRenderTarget(distortBuffer);
			GL.Clear(false, true, backgroundColor);
			for (int i = 0; i < this.m_renderDistort.Count; i++)
			{
				MeshFilter meshFilter = this.m_renderDistort[i];
				if (meshFilter == null)
				{
					this.m_renderDistort.RemoveAt(i);
					i--;
				}
				else
				{
					if (meshFilter.sharedMesh == null)
					{
						return;
					}
					Renderer component = meshFilter.GetComponent<Renderer>();
					Material sharedMaterial = component.sharedMaterial;
					sharedMaterial.SetPass(0);
					Graphics.DrawMeshNow(meshFilter.sharedMesh, component.localToWorldMatrix, 0);
				}
			}
			this.m_renderDistort.Clear();
			this.m_material.SetTexture("_DistortTex", distortBuffer);
		}
		else
		{
			this.m_material.SetTexture("_DistortTex", null);
		}
	}

	// Token: 0x06000CC7 RID: 3271 RVA: 0x0003A764 File Offset: 0x00038964
	private void OnDestroy()
	{
		this.DestroyTargetTex(this.PostTarget);
		this.CurrentAlphaBuffer.Destroy();
		if (this.IsSubCam)
		{
			UberPostProcess.s_subCams.Remove(this);
		}
	}

	// Token: 0x06000CC8 RID: 3272 RVA: 0x0003A79F File Offset: 0x0003899F
	private void SetCh(float value, ref float set)
	{
		if (value == set)
		{
			return;
		}
		set = value;
		this.m_dirty = true;
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x0003A7B4 File Offset: 0x000389B4
	private void SetCh(Vector4 value, ref Vector4 set)
	{
		if (value == set)
		{
			return;
		}
		set = value;
		this.m_dirty = true;
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x0003A7E4 File Offset: 0x000389E4
	private void SetCh(Color value, ref Color set)
	{
		if (value == set)
		{
			return;
		}
		set = value;
		this.m_dirty = true;
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x0003A814 File Offset: 0x00038A14
	private bool KeysAreDifferent(AnimationCurve curveA, AnimationCurve curveB)
	{
		if (curveA.length != curveB.length)
		{
			return true;
		}
		int length = curveA.length;
		for (int i = 0; i < length; i++)
		{
			Keyframe keyframe = curveA[i];
			Keyframe keyframe2 = curveB[i];
			if (keyframe.time != keyframe2.time || curveA[i].value != keyframe2.value || keyframe.inTangent != keyframe2.inTangent || keyframe.outTangent != keyframe2.outTangent)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x0003A8B5 File Offset: 0x00038AB5
	public void SetDoBlur(bool blur)
	{
		this.m_doBlur = blur;
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x0003A8BE File Offset: 0x00038ABE
	public void ApplySettings(CameraSettingsAsset settingsAsset)
	{
		if (settingsAsset == null)
		{
			return;
		}
		this.ApplySettings(new CameraSettings(settingsAsset, null));
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x0003A8DC File Offset: 0x00038ADC
	public void ApplyAdditiveSettings(CameraAdditiveSettings additiveSettings)
	{
		this.AdditiveContrast.Brightness = additiveSettings.AdditiveContrast.Brightness;
		this.AdditiveContrast.Contrast = additiveSettings.AdditiveContrast.Contrast;
		this.AdditiveBloomIntensity = additiveSettings.AdditiveBloomIntensity;
		this.AdditiveBloomThreshhold = additiveSettings.AdditiveBloomThreshhold;
		this.AdditiveDesaturation = additiveSettings.AdditiveDesaturation;
		this.AdditiveVignettingIntensity = additiveSettings.AdditiveVignettingIntensity;
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x0003A948 File Offset: 0x00038B48
	private void SetCurves(AnimationCurve b, ref AnimationCurve a)
	{
		if (this.KeysAreDifferent(a, b))
		{
			a.MoveKey(0, b[0]);
			a.MoveKey(1, b[1]);
			this.m_dirty = true;
		}
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x0003A98C File Offset: 0x00038B8C
	public void ApplySettings(CameraSettings settings)
	{
		if (settings == null || settings.Vignetting == null)
		{
			return;
		}
		this.SetCh(settings.Vignetting.Intensity, ref this.Vignetting.Intensity);
		this.SetCh(settings.Contrast.Brightness, ref this.Contrast.Brightness);
		this.SetCh(settings.Contrast.Contrast, ref this.Contrast.Contrast);
		this.SetCh(settings.Desaturation.Amount, ref this.Desaturation.Amount);
		this.SetCh(settings.BloomAndFlaresSettings.Intensity, ref this.BloomIntensity);
		this.SetCh(settings.BloomAndFlaresSettings.Threshhold, ref this.BloomThreshhold);
		this.SetCh(settings.BloomAndFlaresSettings.BlurSpread, ref this.SepBlurSpread);
		this.SetCh(settings.BloomAndFlaresSettings.LocalIntensity, ref this.LensflareIntensity);
		this.SetCh(settings.BloomAndFlaresSettings.LocalThreshhold, ref this.LensflareThreshhold);
		this.SetCh(settings.BloomAndFlaresSettings.FlareColorA, ref this.FlareColorA);
		this.SetCh(settings.BloomAndFlaresSettings.FlareColorB, ref this.FlareColorB);
		this.SetCh(settings.BloomAndFlaresSettings.FlareColorC, ref this.FlareColorC);
		this.SetCh(settings.BloomAndFlaresSettings.FlareColorD, ref this.FlareColorD);
		this.SetCh(settings.TwirlSettings.Strength, ref this.TwirlSettings.Strength);
		this.SetCh(settings.TwirlSettings.PosVariation, ref this.TwirlSettings.PosVariation);
		this.SetCurves(settings.ColorCorrection.Red, ref this.ColorCorrection.Red);
		this.SetCurves(settings.ColorCorrection.Green, ref this.ColorCorrection.Green);
		this.SetCurves(settings.ColorCorrection.Blue, ref this.ColorCorrection.Blue);
		RenderSettings.ambientLight = settings.Fog;
		Frameworks.Shader.Globals.FogGradientRange = settings.FogRange;
	}

	// Token: 0x06000CD1 RID: 3281 RVA: 0x0003AB8C File Offset: 0x00038D8C
	public void SaveSettings(CameraSettingsAsset settings)
	{
		settings.Vignetting.Intensity = this.Vignetting.Intensity;
		settings.Contrast.Brightness = this.Contrast.Brightness;
		settings.Contrast.Contrast = this.Contrast.Contrast;
		settings.Desaturation.Amount = this.Desaturation.Amount;
		settings.ColorCorrection.Red.keys = this.ColorCorrection.Red.keys;
		settings.ColorCorrection.Green.keys = this.ColorCorrection.Green.keys;
		settings.ColorCorrection.Blue.keys = this.ColorCorrection.Blue.keys;
		settings.Fog = RenderSettings.ambientLight;
		settings.BloomAndFlaresSettings.Intensity = this.BloomIntensity;
		settings.BloomAndFlaresSettings.Threshhold = this.BloomThreshhold;
		settings.BloomAndFlaresSettings.BlurSpread = this.SepBlurSpread;
		settings.BloomAndFlaresSettings.LocalIntensity = this.LensflareIntensity;
		settings.BloomAndFlaresSettings.LocalThreshhold = this.LensflareThreshhold;
		settings.BloomAndFlaresSettings.FlareColorA = this.FlareColorA;
		settings.BloomAndFlaresSettings.FlareColorB = this.FlareColorB;
		settings.BloomAndFlaresSettings.FlareColorC = this.FlareColorC;
		settings.BloomAndFlaresSettings.FlareColorD = this.FlareColorD;
		settings.TwirlSettings.Strength = this.TwirlSettings.Strength;
		settings.TwirlSettings.PosVariation = this.TwirlSettings.PosVariation;
	}

	// Token: 0x04000A6C RID: 2668
	public VignettingSettings Vignetting = new VignettingSettings();

	// Token: 0x04000A6D RID: 2669
	public ContrastSettings Contrast = new ContrastSettings();

	// Token: 0x04000A6E RID: 2670
	public DesaturationSettings Desaturation = new DesaturationSettings();

	// Token: 0x04000A6F RID: 2671
	public ColorCorrectionSettings ColorCorrection = new ColorCorrectionSettings();

	// Token: 0x04000A70 RID: 2672
	public TwirlSettings TwirlSettings = new TwirlSettings();

	// Token: 0x04000A71 RID: 2673
	public float SepBlurSpread = 1.5f;

	// Token: 0x04000A72 RID: 2674
	public float BloomIntensity = 1f;

	// Token: 0x04000A73 RID: 2675
	public float BloomThreshhold = 0.5f;

	// Token: 0x04000A74 RID: 2676
	public float LensflareIntensity = 1f;

	// Token: 0x04000A75 RID: 2677
	public float LensflareThreshhold = 0.3f;

	// Token: 0x04000A76 RID: 2678
	public Color FlareColorA = new Color(0.4f, 0.4f, 0.8f, 0.75f);

	// Token: 0x04000A77 RID: 2679
	public Color FlareColorB = new Color(0.4f, 0.8f, 0.8f, 0.75f);

	// Token: 0x04000A78 RID: 2680
	public Color FlareColorC = new Color(0.8f, 0.4f, 0.8f, 0.75f);

	// Token: 0x04000A79 RID: 2681
	public Color FlareColorD = new Color(0.8f, 0.4f, 0f, 0.75f);

	// Token: 0x04000A7A RID: 2682
	public bool DoLensFlares = true;

	// Token: 0x04000A7B RID: 2683
	[HideInInspector]
	public ContrastSettings AdditiveContrast = new ContrastSettings();

	// Token: 0x04000A7C RID: 2684
	[HideInInspector]
	public float AdditiveBloomIntensity;

	// Token: 0x04000A7D RID: 2685
	[HideInInspector]
	public float AdditiveBloomThreshhold;

	// Token: 0x04000A7E RID: 2686
	[HideInInspector]
	public float AdditiveDesaturation;

	// Token: 0x04000A7F RID: 2687
	[HideInInspector]
	public float AdditiveVignettingIntensity;

	// Token: 0x04000A80 RID: 2688
	public FaderBrightnessContrastSettings FaderBrightnessContrastSettings = new FaderBrightnessContrastSettings();

	// Token: 0x04000A81 RID: 2689
	public UnityEngine.Shader Shader;

	// Token: 0x04000A82 RID: 2690
	public float MotionBlurSpread = 0.3f;

	// Token: 0x04000A83 RID: 2691
	public float MotionBlurThreshold = 0.4f;

	// Token: 0x04000A84 RID: 2692
	public float MotionBlurMultiplier = 1f;

	// Token: 0x04000A85 RID: 2693
	public UberAlphaBuffer CurrentAlphaBuffer = new UberAlphaBuffer();

	// Token: 0x04000A86 RID: 2694
	public bool IsSubCam;

	// Token: 0x04000A87 RID: 2695
	private static List<UberPostProcess> s_subCams = new List<UberPostProcess>();

	// Token: 0x04000A88 RID: 2696
	private List<MeshFilter> m_renderDistort = new List<MeshFilter>();

	// Token: 0x04000A89 RID: 2697
	private Material m_material;

	// Token: 0x04000A8A RID: 2698
	public UnityEngine.Shader BrightPassFilterShader;

	// Token: 0x04000A8B RID: 2699
	private Material m_brightPassFilterMaterial;

	// Token: 0x04000A8C RID: 2700
	public UnityEngine.Shader LensFlareShader;

	// Token: 0x04000A8D RID: 2701
	private Material m_lensFlareMaterial;

	// Token: 0x04000A8E RID: 2702
	public UnityEngine.Shader ScreenBlend;

	// Token: 0x04000A8F RID: 2703
	private Material m_blendMaterial;

	// Token: 0x04000A90 RID: 2704
	public UnityEngine.Shader UpsampleShader;

	// Token: 0x04000A91 RID: 2705
	private Material m_upsampleMat;

	// Token: 0x04000A92 RID: 2706
	public RenderTexture PostTarget;

	// Token: 0x04000A93 RID: 2707
	private RenderTexture m_targetTexture;

	// Token: 0x04000A94 RID: 2708
	private bool m_dirty;

	// Token: 0x04000A95 RID: 2709
	private Vector3 m_lastPosition;

	// Token: 0x04000A96 RID: 2710
	private Vector3 m_lastPos;

	// Token: 0x04000A97 RID: 2711
	private List<UberMotionBlurInterestZone> m_renderZones = new List<UberMotionBlurInterestZone>();

	// Token: 0x04000A98 RID: 2712
	private static string[] s_zoneNames = new string[]
	{
		"_MotionblurZone0",
		"_MotionblurZone1",
		"_MotionblurZone2"
	};

	// Token: 0x04000A99 RID: 2713
	private Texture2D m_screenshot;

	// Token: 0x04000A9A RID: 2714
	private bool m_doBlur;

	// Token: 0x04000A9B RID: 2715
	private float m_prevDesaturation = -1f;

	// Token: 0x04000A9C RID: 2716
	private static UberPostProcess s_instance;

	// Token: 0x04000A9D RID: 2717
	private bool m_doRender = true;

	// Token: 0x04000A9E RID: 2718
	private Color m_originalColor;

	// Token: 0x04000A9F RID: 2719
	private bool m_doMotionBlur = true;

	// Token: 0x04000AA0 RID: 2720
	private int m_originalCullingMask;

	// Token: 0x04000AA1 RID: 2721
	public Camera Camera;

	// Token: 0x04000AA2 RID: 2722
	private float m_prevTwirl;

	// Token: 0x04000AA3 RID: 2723
	private float m_twirlTime;

	// Token: 0x04000AA4 RID: 2724
	private float m_lastTwirlCurrentTime;

	// Token: 0x04000AA5 RID: 2725
	private static int[] s_bezierStrings;

	// Token: 0x04000AA6 RID: 2726
	private static Comparison<UberMotionBlurInterestZone> s_compare = new Comparison<UberMotionBlurInterestZone>(UberPostProcess.ZoneSort);

	// Token: 0x0200084E RID: 2126
	public struct Bezier
	{
		// Token: 0x0600304D RID: 12365 RVA: 0x000CC880 File Offset: 0x000CAA80
		public Bezier(Vector3 start, Vector3 end, float tangent1, float tangent2)
		{
			this.P0 = start;
			this.P3 = end;
			float num = Mathf.Atan(tangent1);
			this.P1 = new Vector2(Mathf.Cos(num), Mathf.Sin(num));
			this.P1 *= 0.5f * Mathf.Pow(tangent1 * tangent1, 0.375f) * (this.P3.x - this.P0.x);
			this.P1 += this.P0;
			num = Mathf.Atan(tangent2);
			this.P2 = new Vector2(Mathf.Cos(3.1415927f + num), Mathf.Sin(3.1415927f + num));
			this.P2 *= 0.5f * Mathf.Pow(tangent2 * tangent2, 0.375f) * (this.P3.x - this.P0.x);
			this.P2 += this.P3;
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x000CC998 File Offset: 0x000CAB98
		public float BezierSimple(float tLen)
		{
			float num = tLen / this.P3.x;
			if (num > 1f)
			{
				return this.P3.y;
			}
			float num2 = 1f - num;
			return Mathf.Clamp01((num2 * num2 * num2 * this.P0 + 3f * num2 * num2 * num * this.P1 + 3f * num2 * num * num * this.P2 + num * num * num * this.P3).y);
		}

		// Token: 0x04002B8F RID: 11151
		public Vector2 P0;

		// Token: 0x04002B90 RID: 11152
		public Vector2 P1;

		// Token: 0x04002B91 RID: 11153
		public Vector2 P2;

		// Token: 0x04002B92 RID: 11154
		public Vector2 P3;
	}
}
