using System;
using UnityEngine;

// Token: 0x0200020C RID: 524
[AddComponentMenu("AVPro Windows Media/Mesh Apply")]
public class AVProWindowsMediaMeshApply : MonoBehaviour
{
	// Token: 0x0600126D RID: 4717 RVA: 0x000538B8 File Offset: 0x00051AB8
	private void Start()
	{
		if (this._movie != null && this._movie.OutputTexture != null)
		{
			this.ApplyMapping(this._movie.OutputTexture);
		}
	}

	// Token: 0x0600126E RID: 4718 RVA: 0x00053900 File Offset: 0x00051B00
	private void Update()
	{
		if (this._movie != null && this._movie.OutputTexture != null)
		{
			this.ApplyMapping(this._movie.OutputTexture);
		}
	}

	// Token: 0x0600126F RID: 4719 RVA: 0x00053948 File Offset: 0x00051B48
	private void ApplyMapping(Texture texture)
	{
		if (this._mesh != null)
		{
			foreach (Material material in this._mesh.materials)
			{
				material.mainTexture = texture;
			}
		}
	}

	// Token: 0x06001270 RID: 4720 RVA: 0x00053991 File Offset: 0x00051B91
	public void OnDisable()
	{
		this.ApplyMapping(null);
	}

	// Token: 0x04000FAB RID: 4011
	public MeshRenderer _mesh;

	// Token: 0x04000FAC RID: 4012
	public AVProWindowsMediaMovie _movie;
}
