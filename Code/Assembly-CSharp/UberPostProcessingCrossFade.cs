using System;
using Frameworks;
using UnityEngine;

// Token: 0x020003DF RID: 991
public class UberPostProcessingCrossFade : MonoBehaviour
{
	// Token: 0x1700047A RID: 1146
	// (get) Token: 0x06001B17 RID: 6935 RVA: 0x00073EAC File Offset: 0x000720AC
	public Material Material
	{
		get
		{
			if (this.m_mat == null)
			{
				this.m_mat = new Material(this.CrossFadeBlend);
				this.m_mat.name = "CrossfadeMaterial";
				this.m_mat.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_mat;
		}
	}

	// Token: 0x06001B18 RID: 6936 RVA: 0x00073F00 File Offset: 0x00072100
	private void Update()
	{
		if (Mathf.Approximately(this.TweenTime, 0f))
		{
			this.m_nextFrame = 0;
		}
		else if (Mathf.Approximately(this.TweenTime, 1f))
		{
			this.m_nextFrame = 1;
		}
		else if (this.TweenTime < 0.2f)
		{
			this.m_nextFrame = ((this.m_frameCounter % 3 <= 0) ? 1 : 0);
		}
		else if (this.TweenTime > 0.8f)
		{
			this.m_nextFrame = ((this.m_frameCounter % 3 <= 1) ? 1 : 0);
		}
		else
		{
			this.m_nextFrame = this.m_frameCounter % 2;
		}
	}

	// Token: 0x06001B19 RID: 6937 RVA: 0x00073FC0 File Offset: 0x000721C0
	public void Awake()
	{
		this.m_post = base.GetComponent<UberPostProcess>();
		UberAlphaBuffer uberAlphaBuffer = new UberAlphaBuffer();
		uberAlphaBuffer.GenerateAlphaBuffer();
		this.FromInfo.AlphaBuffer = uberAlphaBuffer;
		this.ToInfo.AlphaBuffer = this.m_post.CurrentAlphaBuffer;
	}

	// Token: 0x06001B1A RID: 6938 RVA: 0x00074008 File Offset: 0x00072208
	public void StartCrossFade()
	{
		base.enabled = true;
		this.m_nextFrame = (this.m_currentFrame = (this.m_frameCounter = 0));
		this.ToInfo.AlphaBuffer = this.FromInfo.AlphaBuffer;
		this.FromInfo.AlphaBuffer = this.m_post.CurrentAlphaBuffer;
		this.m_bufferBeenUsed = false;
		this.ApplySettings(this.FromInfo);
	}

	// Token: 0x06001B1B RID: 6939 RVA: 0x00074074 File Offset: 0x00072274
	public void StopCrossFade()
	{
		base.enabled = false;
		this.ApplySettings(this.ToInfo);
	}

	// Token: 0x06001B1C RID: 6940 RVA: 0x0007408C File Offset: 0x0007228C
	public void GenerateBuffer()
	{
		if (this.m_buf != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_buf);
		}
		this.m_buf = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
		this.m_buf.name = "buf";
	}

	// Token: 0x06001B1D RID: 6941 RVA: 0x000740DC File Offset: 0x000722DC
	private void OnRenderImage(RenderTexture from, RenderTexture to)
	{
		if (this.m_buf == null || this.m_buf.width != Screen.width || this.m_buf.height != Screen.height)
		{
			this.GenerateBuffer();
		}
		if (this.m_currentFrame == 1)
		{
			this.Material.SetFloat(UberPostCacheIds.Alpha, 1f - this.TweenTime);
		}
		else
		{
			this.Material.SetFloat(UberPostCacheIds.Alpha, this.TweenTime);
		}
		if (!this.m_bufferBeenUsed)
		{
			this.Material.SetFloat(UberPostCacheIds.Alpha, 0f);
		}
		this.Material.SetTexture(UberPostCacheIds.Buf, this.m_buf);
		Graphics.Blit(from, to, this.Material);
		if (this.m_currentFrame != this.m_nextFrame)
		{
			Graphics.Blit(from, this.m_buf);
			this.m_bufferBeenUsed = true;
		}
		this.m_currentFrame = this.m_nextFrame;
		this.m_frameCounter++;
	}

	// Token: 0x06001B1E RID: 6942 RVA: 0x000741F0 File Offset: 0x000723F0
	public void LateUpdate()
	{
		if (this.m_currentFrame == 1)
		{
			base.transform.position = this.ToInfo.Position;
			base.transform.rotation = this.ToInfo.Rotation;
			this.ApplySettings(this.ToInfo);
		}
		else
		{
			base.transform.position = this.FromInfo.Position;
			base.transform.rotation = this.FromInfo.Rotation;
			this.ApplySettings(this.FromInfo);
		}
	}

	// Token: 0x06001B1F RID: 6943 RVA: 0x00074280 File Offset: 0x00072480
	private void ApplySettings(UberPostProcessingCrossFade.CameraInformation info)
	{
		this.m_post.ApplySettings(info.Settings);
		this.m_post.ApplyAdditiveSettings(info.AdditiveSettings);
		Frameworks.Shader.Globals.FogGradientTexture = info.FogTexture;
		this.m_post.CurrentAlphaBuffer = info.AlphaBuffer;
		this.m_post.CurrentAlphaBuffer.SetCurrentAlphaGrab();
		this.m_post.GetComponent<Camera>().fieldOfView = info.FieldOfView;
		this.m_post.Speed = info.Speed;
	}

	// Token: 0x06001B20 RID: 6944 RVA: 0x00074304 File Offset: 0x00072504
	private void OnDestroy()
	{
		UnityEngine.Object.DestroyObject(this.m_buf);
		if (this.FromInfo.AlphaBuffer != null)
		{
			this.FromInfo.AlphaBuffer.Destroy();
		}
		if (this.ToInfo.AlphaBuffer != null)
		{
			this.ToInfo.AlphaBuffer.Destroy();
		}
	}

	// Token: 0x0400178B RID: 6027
	public UberPostProcessingCrossFade.CameraInformation FromInfo;

	// Token: 0x0400178C RID: 6028
	public UberPostProcessingCrossFade.CameraInformation ToInfo;

	// Token: 0x0400178D RID: 6029
	public float TweenTime;

	// Token: 0x0400178E RID: 6030
	private Material m_mat;

	// Token: 0x0400178F RID: 6031
	private RenderTexture m_buf;

	// Token: 0x04001790 RID: 6032
	private bool m_bufferBeenUsed;

	// Token: 0x04001791 RID: 6033
	private int m_frameCounter;

	// Token: 0x04001792 RID: 6034
	private int m_currentFrame;

	// Token: 0x04001793 RID: 6035
	private float m_lastTweenTime;

	// Token: 0x04001794 RID: 6036
	private int m_nextFrame;

	// Token: 0x04001795 RID: 6037
	private bool m_toggle;

	// Token: 0x04001796 RID: 6038
	private UberPostProcess m_post;

	// Token: 0x04001797 RID: 6039
	public UnityEngine.Shader CrossFadeBlend;

	// Token: 0x020003E0 RID: 992
	[Serializable]
	public class CameraInformation
	{
		// Token: 0x04001798 RID: 6040
		public Vector3 Position;

		// Token: 0x04001799 RID: 6041
		public Quaternion Rotation;

		// Token: 0x0400179A RID: 6042
		public CameraSettings Settings;

		// Token: 0x0400179B RID: 6043
		public CameraAdditiveSettings AdditiveSettings;

		// Token: 0x0400179C RID: 6044
		public Texture FogTexture;

		// Token: 0x0400179D RID: 6045
		public UberAlphaBuffer AlphaBuffer;

		// Token: 0x0400179E RID: 6046
		public Vector3 Speed;

		// Token: 0x0400179F RID: 6047
		public float FieldOfView;
	}
}
