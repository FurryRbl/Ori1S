using System;
using UnityEngine;

// Token: 0x02000210 RID: 528
public class AVProWindowsMediaFormatConverter : IDisposable
{
	// Token: 0x1700034A RID: 842
	// (get) Token: 0x06001279 RID: 4729 RVA: 0x00053B42 File Offset: 0x00051D42
	public bool RequiresConversion
	{
		get
		{
			return this._requiresTextureCrop;
		}
	}

	// Token: 0x1700034B RID: 843
	// (get) Token: 0x0600127A RID: 4730 RVA: 0x00053B4A File Offset: 0x00051D4A
	public Texture OutputTexture
	{
		get
		{
			return this._outputTexture;
		}
	}

	// Token: 0x1700034C RID: 844
	// (get) Token: 0x0600127B RID: 4731 RVA: 0x00053B52 File Offset: 0x00051D52
	public int DisplayFrame
	{
		get
		{
			return this._lastFrameUploaded;
		}
	}

	// Token: 0x1700034D RID: 845
	// (get) Token: 0x0600127C RID: 4732 RVA: 0x00053B5A File Offset: 0x00051D5A
	// (set) Token: 0x0600127D RID: 4733 RVA: 0x00053B62 File Offset: 0x00051D62
	public bool ValidPicture { get; private set; }

	// Token: 0x0600127E RID: 4734 RVA: 0x00053B6B File Offset: 0x00051D6B
	public void Reset()
	{
		this.ValidPicture = false;
		this._lastFrameUploaded = -1;
	}

	// Token: 0x0600127F RID: 4735 RVA: 0x00053B7B File Offset: 0x00051D7B
	public Material GetConversionMaterial()
	{
		if (!this._requiresConversion)
		{
			return this._conversionMaterial;
		}
		return null;
	}

	// Token: 0x06001280 RID: 4736 RVA: 0x00053B90 File Offset: 0x00051D90
	public bool Build(int movieHandle, int width, int height, AVProWindowsMediaPlugin.VideoFrameFormat format, bool useBT709, bool flipX, bool flipY, FilterMode filterMode, TextureWrapMode wrapMode)
	{
		this.Reset();
		this._outputTexture = null;
		this._movieHandle = movieHandle;
		this._width = width;
		this._height = height;
		this._sourceVideoFormat = format;
		this._flipX = flipX;
		this._flipY = flipY;
		this._useBT709 = useBT709;
		if (AVProWindowsMediaManager.Instance._useExternalTextures)
		{
			this.CreateExternalTexture();
		}
		else
		{
			this.CreateTexture();
		}
		if (this._rawTexture != null)
		{
			this._requiresConversion = false;
			this._requiresTextureCrop = (this._usedTextureWidth != this._rawTexture.width || this._usedTextureHeight != this._rawTexture.height);
			if (this._requiresTextureCrop)
			{
				this.CreateUVs(this._flipX, this._flipY);
				this._requiresConversion = true;
			}
			if (!this._isExternalTexture)
			{
				AVProWindowsMediaPlugin.SetTexturePointer(this._movieHandle, this._rawTexture.GetNativeTexturePtr());
			}
			if (!this._requiresConversion)
			{
				bool flag = SystemInfo.graphicsDeviceVersion.StartsWith("Direct3D 11");
				if (this._flipX || this._flipY)
				{
					this._requiresConversion = true;
				}
				else if (this._sourceVideoFormat == AVProWindowsMediaPlugin.VideoFrameFormat.RAW_BGRA32 && flag)
				{
					if (!SystemInfo.SupportsTextureFormat(TextureFormat.BGRA32))
					{
						this._requiresConversion = true;
					}
				}
				else if (this._sourceVideoFormat != AVProWindowsMediaPlugin.VideoFrameFormat.Hap_RGB && this._sourceVideoFormat != AVProWindowsMediaPlugin.VideoFrameFormat.Hap_RGBA && this._sourceVideoFormat != AVProWindowsMediaPlugin.VideoFrameFormat.RAW_BGRA32)
				{
					this._requiresConversion = true;
				}
			}
			if (this._requiresConversion)
			{
				if (this.CreateMaterial())
				{
					this.CreateRenderTexture();
					this._outputTexture = this._finalTexture;
					this._conversionMaterial.mainTexture = this._rawTexture;
					if (!this._requiresTextureCrop)
					{
						if (this._flipX)
						{
							this._conversionMaterial.mainTextureScale = new Vector2(-1f, this._conversionMaterial.mainTextureScale.y);
							this._conversionMaterial.mainTextureOffset = new Vector2(1f, this._conversionMaterial.mainTextureOffset.y);
						}
						if (this._flipY)
						{
							this._conversionMaterial.mainTextureScale = new Vector2(this._conversionMaterial.mainTextureScale.x, -1f);
							this._conversionMaterial.mainTextureOffset = new Vector2(this._conversionMaterial.mainTextureOffset.x, 1f);
						}
					}
					bool flag2 = this._sourceVideoFormat != AVProWindowsMediaPlugin.VideoFrameFormat.RAW_BGRA32;
					if (flag2)
					{
						this._conversionMaterial.SetFloat("_TextureWidth", (float)this._finalTexture.width);
					}
				}
			}
			else
			{
				bool flag3 = this._sourceVideoFormat != AVProWindowsMediaPlugin.VideoFrameFormat.RAW_BGRA32 && this._sourceVideoFormat != AVProWindowsMediaPlugin.VideoFrameFormat.Hap_RGB && this._sourceVideoFormat != AVProWindowsMediaPlugin.VideoFrameFormat.Hap_RGBA;
				if (flag3)
				{
					this.CreateMaterial();
					this._conversionMaterial.SetFloat("_TextureWidth", (float)this._width);
					this._rawTexture.filterMode = FilterMode.Point;
				}
				else
				{
					this._rawTexture.filterMode = FilterMode.Bilinear;
				}
				this._outputTexture = this._rawTexture;
			}
		}
		if (this._outputTexture != null)
		{
			this._outputTexture.filterMode = filterMode;
			this._outputTexture.wrapMode = wrapMode;
		}
		return this._outputTexture != null;
	}

	// Token: 0x06001281 RID: 4737 RVA: 0x00053F04 File Offset: 0x00052104
	public bool Update()
	{
		bool flag = this.UpdateTexture();
		if (this._requiresConversion)
		{
			if (flag)
			{
				this.DoFormatConversion();
			}
			else if (this._finalTexture != null && !this._finalTexture.IsCreated())
			{
				this.Reset();
			}
		}
		else if (flag)
		{
			this.ValidPicture = true;
		}
		return flag;
	}

	// Token: 0x06001282 RID: 4738 RVA: 0x00053F70 File Offset: 0x00052170
	private bool UpdateTexture()
	{
		bool result = false;
		int lastFrameUploaded = AVProWindowsMediaPlugin.GetLastFrameUploaded(this._movieHandle);
		if (this._lastFrameUploaded != lastFrameUploaded)
		{
			this._lastFrameUploaded = lastFrameUploaded;
			result = true;
		}
		return result;
	}

	// Token: 0x06001283 RID: 4739 RVA: 0x00053FA4 File Offset: 0x000521A4
	public void Dispose()
	{
		this.ValidPicture = false;
		this._width = (this._height = 0);
		if (this._conversionMaterial != null)
		{
			this._conversionMaterial.mainTexture = null;
			UnityEngine.Object.Destroy(this._conversionMaterial);
			this._conversionMaterial = null;
		}
		this._outputTexture = null;
		if (this._finalTexture != null)
		{
			RenderTexture.ReleaseTemporary(this._finalTexture);
			this._finalTexture = null;
		}
		if (this._rawTexture != null)
		{
			if (!this._isExternalTexture)
			{
				UnityEngine.Object.Destroy(this._rawTexture);
			}
			this._rawTexture = null;
		}
	}

	// Token: 0x06001284 RID: 4740 RVA: 0x00054050 File Offset: 0x00052250
	private bool CreateMaterial()
	{
		Shader pixelConversionShader = AVProWindowsMediaManager.Instance.GetPixelConversionShader(this._sourceVideoFormat, this._useBT709);
		if (pixelConversionShader)
		{
			if (this._conversionMaterial != null && this._conversionMaterial.shader != pixelConversionShader)
			{
				UnityEngine.Object.Destroy(this._conversionMaterial);
				this._conversionMaterial = null;
			}
			if (this._conversionMaterial == null)
			{
				this._conversionMaterial = new Material(pixelConversionShader);
				this._conversionMaterial.name = "AVProWindowsMedia-Material";
			}
		}
		return this._conversionMaterial != null;
	}

	// Token: 0x06001285 RID: 4741 RVA: 0x000540F4 File Offset: 0x000522F4
	private void CreateExternalTexture()
	{
		IntPtr texturePointer = AVProWindowsMediaPlugin.GetTexturePointer(this._movieHandle);
		if (texturePointer != IntPtr.Zero)
		{
			int num = this._width;
			TextureFormat format = TextureFormat.ARGB32;
			switch (this._sourceVideoFormat)
			{
			case AVProWindowsMediaPlugin.VideoFrameFormat.YUV_422_YUY2:
			case AVProWindowsMediaPlugin.VideoFrameFormat.YUV_422_UYVY:
			case AVProWindowsMediaPlugin.VideoFrameFormat.YUV_422_YVYU:
			case AVProWindowsMediaPlugin.VideoFrameFormat.YUV_422_HDYC:
				num = this._width / 2;
				break;
			case AVProWindowsMediaPlugin.VideoFrameFormat.Hap_RGB:
				format = TextureFormat.DXT1;
				break;
			case AVProWindowsMediaPlugin.VideoFrameFormat.Hap_RGBA:
			case AVProWindowsMediaPlugin.VideoFrameFormat.Hap_RGB_HQ:
				format = TextureFormat.DXT5;
				break;
			}
			this._usedTextureWidth = num;
			this._usedTextureHeight = this._height;
			this._rawTexture = Texture2D.CreateExternalTexture(num, this._height, format, false, false, texturePointer);
			this._rawTexture.wrapMode = TextureWrapMode.Clamp;
			this._rawTexture.filterMode = FilterMode.Point;
			this._rawTexture.name = "AVProWindowsMedia-RawExternal";
			this._isExternalTexture = true;
		}
	}

	// Token: 0x06001286 RID: 4742 RVA: 0x000541DC File Offset: 0x000523DC
	private void CreateTexture()
	{
		this._usedTextureWidth = this._width;
		this._usedTextureHeight = this._height;
		int num = this._usedTextureWidth;
		int num2 = this._usedTextureHeight;
		TextureFormat textureFormat = TextureFormat.RGBA32;
		switch (this._sourceVideoFormat)
		{
		case AVProWindowsMediaPlugin.VideoFrameFormat.RAW_BGRA32:
			textureFormat = TextureFormat.RGBA32;
			if (SystemInfo.graphicsDeviceVersion.StartsWith("Direct3D 11") && SystemInfo.SupportsTextureFormat(TextureFormat.BGRA32))
			{
				textureFormat = TextureFormat.BGRA32;
			}
			break;
		case AVProWindowsMediaPlugin.VideoFrameFormat.YUV_422_YUY2:
		case AVProWindowsMediaPlugin.VideoFrameFormat.YUV_422_UYVY:
		case AVProWindowsMediaPlugin.VideoFrameFormat.YUV_422_YVYU:
		case AVProWindowsMediaPlugin.VideoFrameFormat.YUV_422_HDYC:
		case AVProWindowsMediaPlugin.VideoFrameFormat.YUV_420_NV12:
			textureFormat = TextureFormat.RGBA32;
			if (SystemInfo.graphicsDeviceVersion.StartsWith("Direct3D 11") && SystemInfo.SupportsTextureFormat(TextureFormat.BGRA32))
			{
				textureFormat = TextureFormat.BGRA32;
			}
			this._usedTextureWidth /= 2;
			num = this._usedTextureWidth;
			break;
		case AVProWindowsMediaPlugin.VideoFrameFormat.Hap_RGB:
			textureFormat = TextureFormat.DXT1;
			break;
		case AVProWindowsMediaPlugin.VideoFrameFormat.Hap_RGBA:
		case AVProWindowsMediaPlugin.VideoFrameFormat.Hap_RGB_HQ:
			textureFormat = TextureFormat.DXT5;
			break;
		}
		bool flag = SystemInfo.npotSupport == NPOTSupport.None;
		if (flag && (!Mathf.IsPowerOfTwo(this._width) || !Mathf.IsPowerOfTwo(this._height)))
		{
			num = Mathf.NextPowerOfTwo(num);
			num2 = Mathf.NextPowerOfTwo(num2);
		}
		if (this._rawTexture != null && (this._rawTexture.width != num || this._rawTexture.height != num2 || this._rawTexture.format != textureFormat))
		{
			UnityEngine.Object.Destroy(this._rawTexture);
			this._rawTexture = null;
		}
		if (this._rawTexture == null)
		{
			bool linear = true;
			if (!this._requiresConversion && (this._sourceVideoFormat == AVProWindowsMediaPlugin.VideoFrameFormat.Hap_RGBA || this._sourceVideoFormat == AVProWindowsMediaPlugin.VideoFrameFormat.Hap_RGB || this._sourceVideoFormat == AVProWindowsMediaPlugin.VideoFrameFormat.RAW_BGRA32))
			{
				linear = false;
			}
			this._rawTexture = new Texture2D(num, num2, textureFormat, false, linear);
			this._rawTexture.wrapMode = TextureWrapMode.Clamp;
			this._rawTexture.filterMode = FilterMode.Point;
			this._rawTexture.name = "AVProWindowsMedia-RawTexture";
			this._rawTexture.Apply(false, true);
			this._isExternalTexture = false;
		}
	}

	// Token: 0x06001287 RID: 4743 RVA: 0x000543F8 File Offset: 0x000525F8
	private void CreateRenderTexture()
	{
		if (this._finalTexture != null && (this._finalTexture.width != this._width || this._finalTexture.height != this._height))
		{
			RenderTexture.ReleaseTemporary(this._finalTexture);
			this._finalTexture = null;
		}
		if (this._finalTexture == null)
		{
			this.ValidPicture = false;
			this._finalTexture = RenderTexture.GetTemporary(this._width, this._height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
			this._finalTexture.wrapMode = TextureWrapMode.Clamp;
			this._finalTexture.filterMode = FilterMode.Bilinear;
			this._finalTexture.useMipMap = false;
			this._finalTexture.name = "AVProWindowsMedia-FinalTexture";
			this._finalTexture.Create();
		}
	}

	// Token: 0x06001288 RID: 4744 RVA: 0x000544C8 File Offset: 0x000526C8
	private void DoFormatConversion()
	{
		if (this._finalTexture == null)
		{
			return;
		}
		this._finalTexture.DiscardContents();
		RenderTexture active = RenderTexture.active;
		if (!this._requiresTextureCrop)
		{
			Graphics.Blit(this._rawTexture, this._finalTexture, this._conversionMaterial, 0);
		}
		else
		{
			RenderTexture.active = this._finalTexture;
			this._conversionMaterial.SetPass(0);
			GL.PushMatrix();
			GL.LoadOrtho();
			AVProWindowsMediaFormatConverter.DrawQuad(this._uv);
			GL.PopMatrix();
		}
		RenderTexture.active = active;
		this.ValidPicture = true;
	}

	// Token: 0x06001289 RID: 4745 RVA: 0x00054560 File Offset: 0x00052760
	private void CreateUVs(bool invertX, bool invertY)
	{
		float num;
		float num2;
		if (invertX)
		{
			num = 1f;
			num2 = 0f;
		}
		else
		{
			num = 0f;
			num2 = 1f;
		}
		float num3;
		float num4;
		if (invertY)
		{
			num3 = 1f;
			num4 = 0f;
		}
		else
		{
			num3 = 0f;
			num4 = 1f;
		}
		if (this._usedTextureWidth != this._rawTexture.width)
		{
			float num5 = (float)this._usedTextureWidth / (float)this._rawTexture.width;
			num *= num5;
			num2 *= num5;
		}
		if (this._usedTextureHeight != this._rawTexture.height)
		{
			float num6 = (float)this._usedTextureHeight / (float)this._rawTexture.height;
			num3 *= num6;
			num4 *= num6;
		}
		this._uv = new Vector4(num, num3, num2, num4);
	}

	// Token: 0x0600128A RID: 4746 RVA: 0x00054630 File Offset: 0x00052830
	private static void DrawQuad(Vector4 uv)
	{
		GL.Begin(7);
		GL.TexCoord2(uv.x, uv.y);
		GL.Vertex3(0f, 0f, 0.1f);
		GL.TexCoord2(uv.z, uv.y);
		GL.Vertex3(1f, 0f, 0.1f);
		GL.TexCoord2(uv.z, uv.w);
		GL.Vertex3(1f, 1f, 0.1f);
		GL.TexCoord2(uv.x, uv.w);
		GL.Vertex3(0f, 1f, 0.1f);
		GL.End();
	}

	// Token: 0x04000FB6 RID: 4022
	private int _movieHandle;

	// Token: 0x04000FB7 RID: 4023
	private Texture2D _rawTexture;

	// Token: 0x04000FB8 RID: 4024
	private bool _isExternalTexture;

	// Token: 0x04000FB9 RID: 4025
	private RenderTexture _finalTexture;

	// Token: 0x04000FBA RID: 4026
	private Texture _outputTexture;

	// Token: 0x04000FBB RID: 4027
	private Material _conversionMaterial;

	// Token: 0x04000FBC RID: 4028
	private int _usedTextureWidth;

	// Token: 0x04000FBD RID: 4029
	private int _usedTextureHeight;

	// Token: 0x04000FBE RID: 4030
	private Vector4 _uv;

	// Token: 0x04000FBF RID: 4031
	private int _lastFrameUploaded = -1;

	// Token: 0x04000FC0 RID: 4032
	private int _width;

	// Token: 0x04000FC1 RID: 4033
	private int _height;

	// Token: 0x04000FC2 RID: 4034
	private bool _flipX;

	// Token: 0x04000FC3 RID: 4035
	private bool _flipY;

	// Token: 0x04000FC4 RID: 4036
	private AVProWindowsMediaPlugin.VideoFrameFormat _sourceVideoFormat;

	// Token: 0x04000FC5 RID: 4037
	private bool _useBT709;

	// Token: 0x04000FC6 RID: 4038
	private bool _requiresTextureCrop;

	// Token: 0x04000FC7 RID: 4039
	private bool _requiresConversion;
}
