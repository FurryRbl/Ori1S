using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007C2 RID: 1986
[ExecuteInEditMode]
public class UberCageGrid : MonoBehaviour, IStrippable
{
	// Token: 0x06002DBB RID: 11707 RVA: 0x000C322E File Offset: 0x000C142E
	private void OnEnable()
	{
		CageStructureTool component = base.GetComponent<CageStructureTool>();
		component.OnModified = (Action)Delegate.Combine(component.OnModified, new Action(this.UpdateMesh));
		this.UpdateMesh();
	}

	// Token: 0x06002DBC RID: 11708 RVA: 0x000C325D File Offset: 0x000C145D
	private void Reset()
	{
		this.GenerateCageGrid();
	}

	// Token: 0x06002DBD RID: 11709 RVA: 0x000C3268 File Offset: 0x000C1468
	public void GenerateCageGrid()
	{
		CageStructureTool component = base.GetComponent<CageStructureTool>();
		component.Clear(false);
		int num = this.XDivisions + 1;
		int num2 = this.YDivisions + 1;
		num = Mathf.Max(0, num);
		num2 = Mathf.Max(0, num2);
		for (int i = 0; i < num2; i++)
		{
			for (int j = 0; j < num; j++)
			{
				float num3 = -0.5f + (float)j / ((float)num - 1f);
				float num4 = -0.5f + (float)i / ((float)num2 - 1f);
				CageStructureTool.Vertex vertex = component.AddVertex(new Vector3(num3 * this.Size.x, num4 * this.Size.y, 0f), false);
				vertex.Metadata = new Vector4(num3 + 0.5f, num4 + 0.5f, 0f, 0f);
			}
		}
		for (int k = 0; k < num2; k++)
		{
			for (int l = 0; l < num; l++)
			{
				int num5 = k * num + l;
				if (l != num - 1)
				{
					component.AddEdge(num5, num5 + 1, true);
				}
				if (k != num2 - 1)
				{
					component.AddEdge(num5, num5 + num, false);
				}
			}
		}
		for (int m = 0; m < num2 - 1; m++)
		{
			for (int n = 0; n < num - 1; n++)
			{
				int item = m * num + n;
				int item2 = m * num + n + 1;
				int item3 = (m + 1) * num + n;
				int item4 = (m + 1) * num + n + 1;
				component.AddFace(new List<int>
				{
					item,
					item2,
					item4,
					item3
				}, true, false);
			}
		}
		component.OnModified();
	}

	// Token: 0x06002DBE RID: 11710 RVA: 0x000C344A File Offset: 0x000C164A
	public void UpdateMesh()
	{
	}

	// Token: 0x06002DBF RID: 11711 RVA: 0x000C344C File Offset: 0x000C164C
	public bool DoStrip()
	{
		return true;
	}

	// Token: 0x0400291A RID: 10522
	public int XDivisions = 10;

	// Token: 0x0400291B RID: 10523
	public int YDivisions = 10;

	// Token: 0x0400291C RID: 10524
	public Vector2 Size = Vector2.one;
}
