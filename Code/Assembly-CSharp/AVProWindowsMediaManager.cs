using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000207 RID: 519
[AddComponentMenu("AVPro Windows Media/Manager (required)")]
[ExecuteInEditMode]
public class AVProWindowsMediaManager : MonoBehaviour
{
	// Token: 0x17000347 RID: 839
	// (get) Token: 0x06001220 RID: 4640 RVA: 0x00053348 File Offset: 0x00051548
	public static AVProWindowsMediaManager Instance
	{
		get
		{
			if (AVProWindowsMediaManager._instance == null)
			{
				AVProWindowsMediaManager._instance = (AVProWindowsMediaManager)UnityEngine.Object.FindObjectOfType(typeof(AVProWindowsMediaManager));
				if (AVProWindowsMediaManager._instance == null)
				{
					Debug.LogError("AVProWindowsMediaManager component required");
					return null;
				}
				if (!AVProWindowsMediaManager._instance._isInitialised)
				{
					AVProWindowsMediaManager._instance.Init();
				}
			}
			return AVProWindowsMediaManager._instance;
		}
	}

	// Token: 0x06001221 RID: 4641 RVA: 0x000533B9 File Offset: 0x000515B9
	private void Awake()
	{
		if (!this._isInitialised)
		{
			AVProWindowsMediaManager._instance = this;
			this.Init();
		}
	}

	// Token: 0x06001222 RID: 4642 RVA: 0x000533D3 File Offset: 0x000515D3
	private void OnDestroy()
	{
		this.Deinit();
	}

	// Token: 0x06001223 RID: 4643 RVA: 0x000533DC File Offset: 0x000515DC
	protected bool Init()
	{
		try
		{
			if (!AVProWindowsMediaPlugin.Init())
			{
				Debug.LogError("[AVProWindowsMedia] failed to initialise.");
				base.enabled = false;
				this.Deinit();
				return false;
			}
			Debug.Log("[AVProWindowsMedia] version " + AVProWindowsMediaPlugin.GetPluginVersion().ToString("F2") + " initialised");
		}
		catch (DllNotFoundException ex)
		{
			Debug.Log("[AVProWindowsMedia] Unity couldn't find the DLL, did you move the 'Plugins' folder to the root of your project?");
			throw ex;
		}
		this.GetConversionMethod();
		this.SetUnityFeatures();
		this._isInitialised = true;
		return this._isInitialised;
	}

	// Token: 0x06001224 RID: 4644 RVA: 0x00053480 File Offset: 0x00051680
	private void SetUnityFeatures()
	{
		AVProWindowsMediaPlugin.SetUnityFeatures(this._useExternalTextures);
	}

	// Token: 0x06001225 RID: 4645 RVA: 0x00053490 File Offset: 0x00051690
	private void GetConversionMethod()
	{
		bool flag = false;
		if (SystemInfo.graphicsDeviceVersion.StartsWith("Direct3D 11") && !SystemInfo.SupportsTextureFormat(TextureFormat.BGRA32))
		{
			flag = true;
		}
		if (flag)
		{
			Shader.DisableKeyword("SWAP_RED_BLUE_OFF");
			Shader.EnableKeyword("SWAP_RED_BLUE_ON");
		}
		else
		{
			Shader.DisableKeyword("SWAP_RED_BLUE_ON");
			Shader.EnableKeyword("SWAP_RED_BLUE_OFF");
		}
		Shader.DisableKeyword("AVPRO_GAMMACORRECTION");
		Shader.EnableKeyword("AVPRO_GAMMACORRECTION_OFF");
		if (QualitySettings.activeColorSpace == ColorSpace.Linear)
		{
			Shader.DisableKeyword("AVPRO_GAMMACORRECTION_OFF");
			Shader.EnableKeyword("AVPRO_GAMMACORRECTION");
		}
	}

	// Token: 0x06001226 RID: 4646 RVA: 0x00053528 File Offset: 0x00051728
	private IEnumerator FinalRenderCapture()
	{
		while (Application.isPlaying)
		{
			GL.IssuePluginEvent(262209536);
			yield return new WaitForEndOfFrame();
		}
		yield break;
	}

	// Token: 0x06001227 RID: 4647 RVA: 0x0005353C File Offset: 0x0005173C
	public void Update()
	{
		GL.IssuePluginEvent(262209536);
	}

	// Token: 0x06001228 RID: 4648 RVA: 0x00053548 File Offset: 0x00051748
	public void Deinit()
	{
		AVProWindowsMediaMovie[] array = (AVProWindowsMediaMovie[])UnityEngine.Object.FindObjectsOfType(typeof(AVProWindowsMediaMovie));
		if (array != null && array.Length > 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i].UnloadMovie();
			}
		}
		AVProWindowsMediaManager._instance = null;
		this._isInitialised = false;
		AVProWindowsMediaPlugin.Deinit();
	}

	// Token: 0x06001229 RID: 4649 RVA: 0x000535A8 File Offset: 0x000517A8
	public Shader GetPixelConversionShader(AVProWindowsMediaPlugin.VideoFrameFormat format, bool useBT709)
	{
		Shader result = null;
		switch (format)
		{
		case AVProWindowsMediaPlugin.VideoFrameFormat.RAW_BGRA32:
			return this._shaderBGRA32;
		case AVProWindowsMediaPlugin.VideoFrameFormat.YUV_422_YUY2:
			result = this._shaderYUY2;
			if (useBT709)
			{
				result = this._shaderYUY2_709;
			}
			return result;
		case AVProWindowsMediaPlugin.VideoFrameFormat.YUV_422_UYVY:
			result = this._shaderUYVY;
			if (useBT709)
			{
				result = this._shaderHDYC;
			}
			return result;
		case AVProWindowsMediaPlugin.VideoFrameFormat.YUV_422_YVYU:
			return this._shaderYVYU;
		case AVProWindowsMediaPlugin.VideoFrameFormat.YUV_422_HDYC:
			return this._shaderHDYC;
		case AVProWindowsMediaPlugin.VideoFrameFormat.YUV_420_NV12:
			return this._shaderNV12;
		case AVProWindowsMediaPlugin.VideoFrameFormat.Hap_RGB:
			return this._shaderCopy;
		case AVProWindowsMediaPlugin.VideoFrameFormat.Hap_RGBA:
			return this._shaderCopy;
		case AVProWindowsMediaPlugin.VideoFrameFormat.Hap_RGB_HQ:
			return this._shaderHap_YCoCg;
		}
		Debug.LogError("[AVProWindowsMedia] Unknown pixel format '" + format);
		return result;
	}

	// Token: 0x04000F8C RID: 3980
	private static AVProWindowsMediaManager _instance;

	// Token: 0x04000F8D RID: 3981
	public bool _logVideoLoads = true;

	// Token: 0x04000F8E RID: 3982
	public Shader _shaderBGRA32;

	// Token: 0x04000F8F RID: 3983
	public Shader _shaderYUY2;

	// Token: 0x04000F90 RID: 3984
	public Shader _shaderYUY2_709;

	// Token: 0x04000F91 RID: 3985
	public Shader _shaderUYVY;

	// Token: 0x04000F92 RID: 3986
	public Shader _shaderYVYU;

	// Token: 0x04000F93 RID: 3987
	public Shader _shaderHDYC;

	// Token: 0x04000F94 RID: 3988
	public Shader _shaderNV12;

	// Token: 0x04000F95 RID: 3989
	public Shader _shaderCopy;

	// Token: 0x04000F96 RID: 3990
	public Shader _shaderHap_YCoCg;

	// Token: 0x04000F97 RID: 3991
	private bool _isInitialised;

	// Token: 0x04000F98 RID: 3992
	[HideInInspector]
	public bool _useExternalTextures;
}
