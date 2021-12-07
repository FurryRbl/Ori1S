using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class UberShaderPrewarmer : MonoBehaviour
{
	// Token: 0x17000005 RID: 5
	// (get) Token: 0x0600001F RID: 31 RVA: 0x000027C4 File Offset: 0x000009C4
	public static bool IsLoaded
	{
		get
		{
			return UberShaderPrewarmer.s_syncOperation != null && UberShaderPrewarmer.s_syncOperation.isDone;
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000020 RID: 32 RVA: 0x000027DC File Offset: 0x000009DC
	public static float LoadProgress
	{
		get
		{
			if (UberShaderPrewarmer.s_syncOperation == null)
			{
				return 0f;
			}
			return UberShaderPrewarmer.s_syncOperation.progress;
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x06000021 RID: 33 RVA: 0x000027F8 File Offset: 0x000009F8
	public static float WarmProgress
	{
		get
		{
			if (UberShaderPrewarmer.Instance == null)
			{
				return 0f;
			}
			if (UberShaderPrewarmer.Instance.LoadedShaders.Count == 0)
			{
				return 1f;
			}
			return (float)UberShaderPrewarmer.Instance.m_warmIndex / (float)UberShaderPrewarmer.Instance.LoadedShaders.Count;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000022 RID: 34 RVA: 0x00002851 File Offset: 0x00000A51
	public static float TotalProgress
	{
		get
		{
			return UberShaderPrewarmer.LoadProgress * 0.5f + UberShaderPrewarmer.WarmProgress * 0.5f;
		}
	}

	// Token: 0x06000023 RID: 35 RVA: 0x0000286A File Offset: 0x00000A6A
	public static void Load()
	{
		if (UberShaderPrewarmer.Instance == null && UberShaderPrewarmer.s_syncOperation == null)
		{
			UberShaderPrewarmer.s_syncOperation = Application.LoadLevelAdditiveAsync("shaderPrewarm");
		}
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00002898 File Offset: 0x00000A98
	private void Awake()
	{
		UberShaderPrewarmer.Instance = this;
		Application.backgroundLoadingPriority = ThreadPriority.BelowNormal;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		this.BaseShaders.Add(Shader.Find("Particles/Additive (Soft)"));
		this.BaseShaders.Add(Shader.Find("Particles/Additive"));
		this.BaseShaders.Add(Shader.Find("GUI/Text Shader"));
		this.BaseShaders.Add(Shader.Find("Hidden/Internal-GUITextureClipText"));
		this.BaseShaders.Add(Shader.Find("Hidden/Internal-GUITextureClip"));
		this.BaseShaders.Add(Shader.Find("Unlit/Transparent"));
		this.BaseShaders.Add(Shader.Find("Unlit/Transparent"));
		this.BaseShaders.Add(Shader.Find("Hidden/UberShader/35D137725E89300F016E9E2E92AE45C6_FB"));
		this.BaseShaders.Add(Shader.Find("Hidden/UberShader/35D137725E89300F016E9E2E92AE45C6"));
		this.BaseShaders.Add(Shader.Find("Hidden/UberShader/F53A9602EA8E79F6500336B327BA8233_FB"));
		this.BaseShaders.Add(Shader.Find("Hidden/UberShader/F53A9602EA8E79F6500336B327BA8233"));
		this.BaseShaders.Add(Shader.Find("Hidden/UberShader/3C0918BAFB1DBB8E1F4046D5681894E0_FB"));
		this.BaseShaders.Add(Shader.Find("Hidden/UberShader/3C0918BAFB1DBB8E1F4046D5681894E0"));
		this.BaseShaders.Add(Shader.Find("Hidden/UberShader/F1A2C98806DE6FE840000B51BF921C8F_FB"));
		this.BaseShaders.Add(Shader.Find("Hidden/UberShader/48C40B66EA7F58869B46BA173C834ED5_FB"));
		this.BaseShaders.Add(Shader.Find("Hidden/UberShader/29C250DB1535C1097A23B937B5A9A9F0"));
		this.BaseShaders.Add(Shader.Find("Hidden/UberShader/FE77D2EFF65BCCD79AD2001DCF9BC490"));
		this.BaseShaders.Add(Shader.Find("Hidden/UberShader/8CA4E3FE9B98D086DDD61E591416A1FC"));
		this.BaseShaders.Add(Shader.Find("Hidden/UberShader/6FAB4BE0149DD5F1D07A885A19E956D4"));
		this.BaseShaders.Add(Shader.Find("Hidden/UberShader/F349CCD1C88D2984D718716F5CEA00D1"));
		this.BaseShaders.Add(Shader.Find("Hidden/UberShader/C42015191911E3D533200CE2A4270435"));
		this.BaseShaders.Add(Shader.Find("Hidden/UberShader/F1A2C98806DE6FE840000B51BF921C8F"));
		this.BaseShaders.Add(Shader.Find("Hidden/UberShader/48C40B66EA7F58869B46BA173C834ED5"));
		this.StartWarmingStream();
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00002AA5 File Offset: 0x00000CA5
	public void SetFastStream()
	{
		this.m_canStreamFast = true;
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002AB0 File Offset: 0x00000CB0
	private void OnStreamStart()
	{
		if (this.LoadedShaders.Count == 0 || this.LoadedShaders[0] == null)
		{
			UberShaderPrewarmer.IsComplete = true;
			return;
		}
		this.CreateResources();
		for (int i = 0; i < this.BaseShaders.Count; i++)
		{
			Shader warmShader = this.BaseShaders[i];
			this.WarmShader(warmShader);
		}
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002B24 File Offset: 0x00000D24
	private void CreateResources()
	{
		if (this.m_prewarmTex == null)
		{
			this.m_prewarmTex = new RenderTexture(16, 16, 0, RenderTextureFormat.ARGB32);
			this.m_prewarmTex.name = "prewarmTex";
		}
		if (this.m_dummyMat == null && this.LoadedShaders.Count > 0)
		{
			this.m_dummyMat = new Material(this.LoadedShaders[0]);
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002B9C File Offset: 0x00000D9C
	private void OnStreamDone()
	{
		RenderTexture.active = null;
		UberShaderPrewarmer.IsComplete = true;
		UnityEngine.Object.DestroyObject(this.m_prewarmTex);
		UnityEngine.Object.DestroyObject(this.m_dummyMat);
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002BCB File Offset: 0x00000DCB
	private void StartWarmingStream()
	{
		this.m_warmIndex = 0;
		this.m_isStreaming = true;
		this.OnStreamStart();
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002BE1 File Offset: 0x00000DE1
	private void Update()
	{
		this.UpdateStream();
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002BEC File Offset: 0x00000DEC
	private void UpdateStream()
	{
		if (!this.m_isStreaming)
		{
			return;
		}
		if (this.m_warmIndex >= this.LoadedShaders.Count)
		{
			return;
		}
		this.CreateResources();
		if (this.m_sw == null)
		{
			this.m_sw = new Stopwatch();
		}
		this.m_sw.Reset();
		this.m_sw.Start();
		Graphics.SetRenderTarget(this.m_prewarmTex);
		if (this.m_canStreamFast)
		{
			for (int i = 0; i < 50; i++)
			{
				if (!this.WarmNextShader())
				{
					break;
				}
			}
		}
		else
		{
			while (this.m_sw.Elapsed.TotalMilliseconds < 4.0)
			{
				if (!this.WarmNextShader())
				{
					break;
				}
			}
		}
		Graphics.SetRenderTarget(null);
		this.m_sw.Stop();
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002CD4 File Offset: 0x00000ED4
	private bool WarmNextShader()
	{
		Shader warmShader = null;
		if (this.m_warmIndex < this.LoadedShaders.Count)
		{
			warmShader = this.LoadedShaders[this.m_warmIndex];
			this.m_warmIndex++;
		}
		if (this.m_warmIndex >= this.LoadedShaders.Count)
		{
			this.m_isStreaming = false;
			this.OnStreamDone();
			return false;
		}
		this.WarmShader(warmShader);
		return true;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002D48 File Offset: 0x00000F48
	private void WarmShader(Shader warmShader)
	{
		if (warmShader == null)
		{
			UnityEngine.Debug.LogError("Something went wrong with shader warming!");
			return;
		}
		this.m_dummyMat.shader = warmShader;
		for (int i = 0; i < this.m_dummyMat.passCount; i++)
		{
			this.m_dummyMat.SetPass(i);
			GL.Begin(4);
			GL.PushMatrix();
			GL.LoadOrtho();
			GL.Vertex(new Vector3(0f, 0f, 0f));
			GL.Vertex(new Vector3(1f, 0f, 0f));
			GL.Vertex(new Vector3(1f, 1f, 0f));
			GL.Vertex(new Vector3(1f, 1f, 0f));
			GL.Vertex(new Vector3(1f, 0f, 0f));
			GL.Vertex(new Vector3(0f, 0f, 0f));
			GL.PopMatrix();
			GL.End();
		}
	}

	// Token: 0x0400000B RID: 11
	public static UberShaderPrewarmer Instance;

	// Token: 0x0400000C RID: 12
	public List<Shader> BaseShaders;

	// Token: 0x0400000D RID: 13
	public List<Shader> LoadedShaders;

	// Token: 0x0400000E RID: 14
	private static AsyncOperation s_syncOperation;

	// Token: 0x0400000F RID: 15
	public static bool IsComplete;

	// Token: 0x04000010 RID: 16
	private bool m_canStreamFast;

	// Token: 0x04000011 RID: 17
	private RenderTexture m_prewarmTex;

	// Token: 0x04000012 RID: 18
	private int m_warmIndex;

	// Token: 0x04000013 RID: 19
	private Material m_dummyMat;

	// Token: 0x04000014 RID: 20
	private bool m_isStreaming;

	// Token: 0x04000015 RID: 21
	private Stopwatch m_sw;
}
