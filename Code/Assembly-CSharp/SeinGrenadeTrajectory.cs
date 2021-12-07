using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000065 RID: 101
public class SeinGrenadeTrajectory : MonoBehaviour
{
	// Token: 0x0600041E RID: 1054 RVA: 0x00011187 File Offset: 0x0000F387
	public void Awake()
	{
		this.HideTrajectory();
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x00011190 File Offset: 0x0000F390
	public void Start()
	{
		Material material = this.LineRenderer.material;
		Vector2 textureOffset = material.GetTextureOffset("_MainTex");
		textureOffset.x = 0.17f;
		material.SetTextureOffset("_MainTex", textureOffset);
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x000111CD File Offset: 0x0000F3CD
	public void HideTrajectory()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x000111DC File Offset: 0x0000F3DC
	public void ShowTrajectory()
	{
		base.gameObject.SetActive(true);
		this.FadeIn.AnimatorDriver.Restart();
		this.Update();
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x0001120B File Offset: 0x0000F40B
	public void Update()
	{
		this.CalculateTrajectory();
		this.UpdateLineRendererPoints();
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x0001121C File Offset: 0x0000F41C
	private void CalculateTrajectory()
	{
		this.m_trajectoryPoints.Clear();
		Vector3 vector = this.StartPosition;
		Vector3 a = this.InitialVelocity;
		Vector3 vector2 = vector;
		for (int i = 0; i < this.LinePoints; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				vector += a * 0.01666667f;
				a += Vector3.down * this.Gravity * 0.01666667f;
			}
			if (a.y < 0f && i > 5)
			{
				break;
			}
			Vector3 vector3 = vector - vector2;
			RaycastHit raycastHit;
			if (Physics.SphereCast(vector2, 0.5f, vector3.normalized, out raycastHit, vector3.magnitude, this.LayerMask))
			{
				break;
			}
			this.m_trajectoryPoints.Add(vector);
			vector2 = vector;
		}
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x00011318 File Offset: 0x0000F518
	private void UpdateLineRendererPoints()
	{
		this.LineRenderer.SetVertexCount(this.m_trajectoryPoints.Count);
		for (int i = 0; i < this.m_trajectoryPoints.Count; i++)
		{
			this.LineRenderer.SetPosition(i, this.m_trajectoryPoints[i]);
		}
	}

	// Token: 0x04000374 RID: 884
	[HideInInspector]
	public Vector2 StartPosition;

	// Token: 0x04000375 RID: 885
	[HideInInspector]
	public Vector2 InitialVelocity;

	// Token: 0x04000376 RID: 886
	public float Gravity;

	// Token: 0x04000377 RID: 887
	public LineRenderer LineRenderer;

	// Token: 0x04000378 RID: 888
	public int LinePoints = 40;

	// Token: 0x04000379 RID: 889
	public TransparencyAnimator FadeIn;

	// Token: 0x0400037A RID: 890
	public LayerMask LayerMask;

	// Token: 0x0400037B RID: 891
	private List<Vector3> m_trajectoryPoints = new List<Vector3>();
}
