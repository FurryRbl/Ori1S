using System;
using UnityEngine;

// Token: 0x0200099F RID: 2463
public class SmokeTrail : MonoBehaviour
{
	// Token: 0x060035AE RID: 13742 RVA: 0x000E12F4 File Offset: 0x000DF4F4
	public void Start()
	{
		this.m_transform = base.transform;
		this.m_renderer = base.GetComponent<Renderer>();
		this.m_lineMesh = base.GetComponent<LineMesh>();
		this.m_lineMaterial = this.m_renderer.material;
		this.m_lineSegment = 1f / (float)this.NumberOfPoints;
		this.m_positions = new Vector3[this.NumberOfPoints];
		this.m_directions = new Vector3[this.NumberOfPoints];
	}

	// Token: 0x060035AF RID: 13743 RVA: 0x000E136C File Offset: 0x000DF56C
	public void Update()
	{
		this.m_timeSinceUpdate += Time.deltaTime;
		if (this.m_timeSinceUpdate > this.UpdateSpeed)
		{
			this.m_timeSinceUpdate -= this.UpdateSpeed;
			if (!this.m_allPointsAdded)
			{
				this.m_currentNumberOfPoints++;
				this.m_lineMesh.Position.Add(Vector3.zero);
				this.m_directions[0] = this.GetSmokeVector();
				this.m_positions[0] = this.m_transform.position;
				this.m_lineMesh.Position[0] = this.m_positions[0];
			}
			if (!this.m_allPointsAdded && this.m_currentNumberOfPoints == this.NumberOfPoints)
			{
				this.m_allPointsAdded = true;
			}
			for (int i = this.m_currentNumberOfPoints - 1; i > 0; i--)
			{
				this.m_positions[i] = this.m_positions[i - 1];
				this.m_directions[i] = this.m_directions[i - 1];
			}
			this.m_directions[0] = this.GetSmokeVector();
		}
		this.m_positions[0] = this.m_transform.position;
		this.m_lineMesh.Position[0] = this.m_positions[0];
		for (int j = 1; j < this.m_currentNumberOfPoints; j++)
		{
			this.m_positions[j] = this.m_positions[j] + this.m_directions[j] * Time.deltaTime;
			this.m_lineMesh.Position[j] = this.m_positions[j];
		}
		this.m_lineMesh.UpdateMesh();
		if (this.m_allPointsAdded)
		{
			this.m_lineMaterial.mainTextureOffset = new Vector2(this.m_lineSegment * (this.m_timeSinceUpdate / this.UpdateSpeed), 0f);
		}
	}

	// Token: 0x060035B0 RID: 13744 RVA: 0x000E15C8 File Offset: 0x000DF7C8
	public Vector3 GetSmokeVector()
	{
		Vector3 vector;
		vector.x = UnityEngine.Random.Range(-1f, 1f);
		vector.y = UnityEngine.Random.Range(0f, 1f);
		vector.z = UnityEngine.Random.Range(-1f, 1f);
		vector.Normalize();
		vector.z = 0f;
		vector *= this.Spread;
		vector.y += this.RiseSpeed;
		return vector;
	}

	// Token: 0x04003046 RID: 12358
	private LineMesh m_lineMesh;

	// Token: 0x04003047 RID: 12359
	private Renderer m_renderer;

	// Token: 0x04003048 RID: 12360
	private Transform m_transform;

	// Token: 0x04003049 RID: 12361
	private Vector3[] m_positions;

	// Token: 0x0400304A RID: 12362
	private Vector3[] m_directions;

	// Token: 0x0400304B RID: 12363
	private float m_timeSinceUpdate;

	// Token: 0x0400304C RID: 12364
	private Material m_lineMaterial;

	// Token: 0x0400304D RID: 12365
	private float m_lineSegment;

	// Token: 0x0400304E RID: 12366
	private int m_currentNumberOfPoints;

	// Token: 0x0400304F RID: 12367
	private bool m_allPointsAdded;

	// Token: 0x04003050 RID: 12368
	public int NumberOfPoints = 20;

	// Token: 0x04003051 RID: 12369
	public float UpdateSpeed = 0.05f;

	// Token: 0x04003052 RID: 12370
	public float RiseSpeed;

	// Token: 0x04003053 RID: 12371
	public float Spread = 1.5f;
}
