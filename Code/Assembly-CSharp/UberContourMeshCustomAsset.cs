using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200082F RID: 2095
public class UberContourMeshCustomAsset : ScriptableObject
{
	// Token: 0x04002B18 RID: 11032
	public List<CageStructureTool.Vertex> CageVertices;

	// Token: 0x04002B19 RID: 11033
	public List<CageStructureTool.Edge> CageEdges;

	// Token: 0x04002B1A RID: 11034
	public List<CageStructureTool.Face> CageFaces;

	// Token: 0x04002B1B RID: 11035
	public Mesh CustomMesh;

	// Token: 0x04002B1C RID: 11036
	public string TexGUID;
}
