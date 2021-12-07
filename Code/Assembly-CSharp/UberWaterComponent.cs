using System;
using UnityEngine;

// Token: 0x02000852 RID: 2130
public class UberWaterComponent : MonoBehaviour
{
	// Token: 0x170007BE RID: 1982
	// (get) Token: 0x0600305A RID: 12378 RVA: 0x000CCD54 File Offset: 0x000CAF54
	protected int ResX
	{
		get
		{
			return Mathf.CeilToInt(this.Control.VerticesPerMeterWaveline * this.Control.transform.localScale.x);
		}
	}

	// Token: 0x170007BF RID: 1983
	// (get) Token: 0x0600305B RID: 12379 RVA: 0x000CCD8A File Offset: 0x000CAF8A
	protected MeshFilter Filter
	{
		get
		{
			if (this.m_filter == null)
			{
				this.m_filter = base.GetComponent<MeshFilter>();
			}
			return this.m_filter;
		}
	}

	// Token: 0x170007C0 RID: 1984
	// (get) Token: 0x0600305C RID: 12380 RVA: 0x000CCDB0 File Offset: 0x000CAFB0
	protected UberWaterControl Control
	{
		get
		{
			if (this.m_control == null)
			{
				this.m_control = base.transform.parent.GetComponent<UberWaterControl>();
			}
			return this.m_control;
		}
	}

	// Token: 0x0600305D RID: 12381 RVA: 0x000CCDEA File Offset: 0x000CAFEA
	public virtual void GenerateMesh()
	{
	}

	// Token: 0x0600305E RID: 12382 RVA: 0x000CCDEC File Offset: 0x000CAFEC
	private Vector2 VectorStage(Vector2 a, Vector2 b, UberWaterComponent.Corner corner)
	{
		switch (corner)
		{
		case UberWaterComponent.Corner.LeftDown:
			return a;
		case UberWaterComponent.Corner.LeftUp:
			return new Vector2(a.x, b.y);
		case UberWaterComponent.Corner.RightDown:
			return new Vector2(b.x, a.y);
		case UberWaterComponent.Corner.RightUp:
			return b;
		default:
			return new Vector2(0f, 0f);
		}
	}

	// Token: 0x0600305F RID: 12383 RVA: 0x000CCE54 File Offset: 0x000CB054
	private void AppendVertex(UberWaterComponent.Corner corner)
	{
		Vector2 v = this.VectorStage(this.m_leftDownPos, this.m_rightUpPos, corner);
		Vector2 vector = this.VectorStage(this.m_leftDownUv, this.m_rightUpUv, corner);
		if (this.IsVertical)
		{
			this.Vertices[this.NumVertices] = v;
			this.Uvs[this.NumVertices] = vector;
		}
		else
		{
			this.Vertices[this.NumVertices] = new Vector3(v.x, 0f, v.y);
			this.Uvs[this.NumVertices] = new Vector2(vector.x, vector.y);
		}
		this.NumVertices++;
	}

	// Token: 0x06003060 RID: 12384 RVA: 0x000CCF34 File Offset: 0x000CB134
	protected bool StartMesh(int maxNumQuads)
	{
		this.MeshLen = maxNumQuads * 4;
		this.Vertices = new Vector3[this.MeshLen];
		this.Colours = new Color[this.MeshLen];
		this.Uvs = new Vector2[this.MeshLen];
		this.NumVertices = 0;
		this.m_numQuads = maxNumQuads;
		if (this.MeshLen > 36000)
		{
			Debug.Log("Mesh vertices density is too high!");
			return false;
		}
		return true;
	}

	// Token: 0x06003061 RID: 12385 RVA: 0x000CCFA8 File Offset: 0x000CB1A8
	protected void AppendQuad(Vector2 downLeftPosition, Vector2 upRightPosition, Vector2 downLeftUv, Vector2 upRightUv)
	{
		this.m_leftDownPos = downLeftPosition;
		this.m_rightUpPos = upRightPosition;
		this.m_leftDownUv = downLeftUv;
		this.m_rightUpUv = upRightUv;
		if ((this.m_rightUpPos - this.m_leftDownPos).magnitude < Mathf.Epsilon)
		{
			return;
		}
		this.AppendVertex(UberWaterComponent.Corner.LeftDown);
		this.AppendVertex(UberWaterComponent.Corner.LeftUp);
		this.AppendVertex(UberWaterComponent.Corner.RightUp);
		this.AppendVertex(UberWaterComponent.Corner.RightDown);
	}

	// Token: 0x06003062 RID: 12386 RVA: 0x000CD014 File Offset: 0x000CB214
	protected void AppendQuadStrip(Vector2 downLeftPosition, Vector2 upRightPosition, Vector2 downLeftUv, Vector2 upRightUv, int tesselationX, int tesselationY)
	{
		Vector2 vector = upRightPosition - downLeftPosition;
		Vector2 a = new Vector2(vector.x / (float)tesselationX, 0f);
		Vector2 a2 = new Vector2(0f, vector.y / (float)tesselationY);
		Vector2 vector2 = upRightUv - downLeftUv;
		Vector2 a3 = new Vector2(vector2.x / (float)tesselationX, 0f);
		Vector2 a4 = new Vector2(0f, vector2.y / (float)tesselationY);
		for (int i = 1; i <= tesselationX; i++)
		{
			for (int j = 1; j <= tesselationY; j++)
			{
				Vector2 downLeftUv2 = downLeftUv + a3 * (float)(i - 1) + a4 * (float)(j - 1);
				Vector2 upRightUv2 = downLeftUv + a3 * (float)i + a4 * (float)j;
				this.AppendQuad(downLeftPosition + a * (float)(i - 1) + a2 * (float)(j - 1), downLeftPosition + a * (float)i + a2 * (float)j, downLeftUv2, upRightUv2);
			}
		}
	}

	// Token: 0x06003063 RID: 12387 RVA: 0x000CD14C File Offset: 0x000CB34C
	protected Mesh CreateMesh(Mesh current, bool setShared = true)
	{
		int[] array = new int[this.m_numQuads * 6];
		for (int i = 0; i < this.m_numQuads; i++)
		{
			array[i * 6] = i * 4;
			array[i * 6 + 1] = i * 4 + 1;
			array[i * 6 + 2] = i * 4 + 2;
			array[i * 6 + 3] = i * 4;
			array[i * 6 + 4] = i * 4 + 2;
			array[i * 6 + 5] = i * 4 + 3;
		}
		MeshFilter component = base.GetComponent<MeshFilter>();
		if (component)
		{
			if (current != null)
			{
				UnityEngine.Object.DestroyImmediate(current);
			}
			Mesh mesh = new Mesh();
			mesh.Clear();
			mesh.vertices = this.Vertices;
			mesh.triangles = array;
			mesh.uv = this.Uvs;
			mesh.name = "uberWaterComponent";
			if (setShared)
			{
				component.sharedMesh = mesh;
			}
			return mesh;
		}
		return null;
	}

	// Token: 0x04002BA0 RID: 11168
	protected Vector3[] Vertices;

	// Token: 0x04002BA1 RID: 11169
	protected Color[] Colours;

	// Token: 0x04002BA2 RID: 11170
	protected Vector2[] Uvs;

	// Token: 0x04002BA3 RID: 11171
	protected int MeshLen;

	// Token: 0x04002BA4 RID: 11172
	protected int NumVertices;

	// Token: 0x04002BA5 RID: 11173
	private UberWaterControl m_control;

	// Token: 0x04002BA6 RID: 11174
	private MeshFilter m_filter;

	// Token: 0x04002BA7 RID: 11175
	protected bool IsVertical;

	// Token: 0x04002BA8 RID: 11176
	private Vector2 m_leftDownPos;

	// Token: 0x04002BA9 RID: 11177
	private Vector2 m_rightUpPos;

	// Token: 0x04002BAA RID: 11178
	private Vector2 m_leftDownUv;

	// Token: 0x04002BAB RID: 11179
	private Vector2 m_rightUpUv;

	// Token: 0x04002BAC RID: 11180
	private int m_numQuads;

	// Token: 0x02000853 RID: 2131
	private enum Corner
	{
		// Token: 0x04002BAE RID: 11182
		LeftDown,
		// Token: 0x04002BAF RID: 11183
		LeftUp,
		// Token: 0x04002BB0 RID: 11184
		RightDown,
		// Token: 0x04002BB1 RID: 11185
		RightUp
	}
}
