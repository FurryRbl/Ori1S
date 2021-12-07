using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001E8 RID: 488
	public sealed class CanvasRenderer : Component
	{
		// Token: 0x06001D7F RID: 7551 RVA: 0x0001BBEC File Offset: 0x00019DEC
		public void SetColor(Color color)
		{
			CanvasRenderer.INTERNAL_CALL_SetColor(this, ref color);
		}

		// Token: 0x06001D80 RID: 7552
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetColor(CanvasRenderer self, ref Color color);

		// Token: 0x06001D81 RID: 7553 RVA: 0x0001BBF8 File Offset: 0x00019DF8
		public Color GetColor()
		{
			Color result;
			CanvasRenderer.INTERNAL_CALL_GetColor(this, out result);
			return result;
		}

		// Token: 0x06001D82 RID: 7554
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetColor(CanvasRenderer self, out Color value);

		// Token: 0x06001D83 RID: 7555
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetAlpha();

		// Token: 0x06001D84 RID: 7556
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAlpha(float alpha);

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001D85 RID: 7557
		// (set) Token: 0x06001D86 RID: 7558
		[Obsolete("isMask is no longer supported. See EnableClipping for vertex clipping configuration")]
		public extern bool isMask { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001D87 RID: 7559 RVA: 0x0001BC10 File Offset: 0x00019E10
		[Obsolete("UI System now uses meshes. Generate a mesh and use 'SetMesh' instead")]
		public void SetVertices(List<UIVertex> vertices)
		{
			this.SetVertices(vertices.ToArray(), vertices.Count);
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x0001BC24 File Offset: 0x00019E24
		[Obsolete("UI System now uses meshes. Generate a mesh and use 'SetMesh' instead")]
		public void SetVertices(UIVertex[] vertices, int size)
		{
			Mesh mesh = new Mesh();
			List<Vector3> list = new List<Vector3>();
			List<Color32> list2 = new List<Color32>();
			List<Vector2> list3 = new List<Vector2>();
			List<Vector2> list4 = new List<Vector2>();
			List<Vector3> list5 = new List<Vector3>();
			List<Vector4> list6 = new List<Vector4>();
			List<int> list7 = new List<int>();
			for (int i = 0; i < size; i += 4)
			{
				for (int j = 0; j < 4; j++)
				{
					list.Add(vertices[i + j].position);
					list2.Add(vertices[i + j].color);
					list3.Add(vertices[i + j].uv0);
					list4.Add(vertices[i + j].uv1);
					list5.Add(vertices[i + j].normal);
					list6.Add(vertices[i + j].tangent);
				}
				list7.Add(i);
				list7.Add(i + 1);
				list7.Add(i + 2);
				list7.Add(i + 2);
				list7.Add(i + 3);
				list7.Add(i);
			}
			mesh.SetVertices(list);
			mesh.SetColors(list2);
			mesh.SetNormals(list5);
			mesh.SetTangents(list6);
			mesh.SetUVs(0, list3);
			mesh.SetUVs(1, list4);
			mesh.SetIndices(list7.ToArray(), MeshTopology.Triangles, 0);
			this.SetMesh(mesh);
			Object.DestroyImmediate(mesh);
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x0001BDA4 File Offset: 0x00019FA4
		public void EnableRectClipping(Rect rect)
		{
			CanvasRenderer.INTERNAL_CALL_EnableRectClipping(this, ref rect);
		}

		// Token: 0x06001D8A RID: 7562
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_EnableRectClipping(CanvasRenderer self, ref Rect rect);

		// Token: 0x06001D8B RID: 7563
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DisableRectClipping();

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001D8C RID: 7564
		public extern bool hasRectClipping { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001D8D RID: 7565
		// (set) Token: 0x06001D8E RID: 7566
		public extern bool hasPopInstruction { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001D8F RID: 7567
		// (set) Token: 0x06001D90 RID: 7568
		public extern int materialCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001D91 RID: 7569
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetMaterial(Material material, int index);

		// Token: 0x06001D92 RID: 7570 RVA: 0x0001BDB0 File Offset: 0x00019FB0
		public void SetMaterial(Material material, Texture texture)
		{
			this.materialCount = Math.Max(1, this.materialCount);
			this.SetMaterial(material, 0);
			this.SetTexture(texture);
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x0001BDE0 File Offset: 0x00019FE0
		public Material GetMaterial()
		{
			return this.GetMaterial(0);
		}

		// Token: 0x06001D94 RID: 7572
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Material GetMaterial(int index);

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001D95 RID: 7573
		// (set) Token: 0x06001D96 RID: 7574
		public extern int popMaterialCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001D97 RID: 7575
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPopMaterial(Material material, int index);

		// Token: 0x06001D98 RID: 7576
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Material GetPopMaterial(int index);

		// Token: 0x06001D99 RID: 7577
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTexture(Texture texture);

		// Token: 0x06001D9A RID: 7578
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetMesh(Mesh mesh);

		// Token: 0x06001D9B RID: 7579
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Clear();

		// Token: 0x06001D9C RID: 7580 RVA: 0x0001BDEC File Offset: 0x00019FEC
		public static void SplitUIVertexStreams(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector2> uv0S, List<Vector2> uv1S, List<Vector3> normals, List<Vector4> tangents, List<int> indicies)
		{
			CanvasRenderer.SplitUIVertexStreamsInternal(verts, positions, colors, uv0S, uv1S, normals, tangents);
			CanvasRenderer.SplitIndiciesStreamsInternal(verts, indicies);
		}

		// Token: 0x06001D9D RID: 7581
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SplitUIVertexStreamsInternal(object verts, object positions, object colors, object uv0S, object uv1S, object normals, object tangents);

		// Token: 0x06001D9E RID: 7582
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SplitIndiciesStreamsInternal(object verts, object indicies);

		// Token: 0x06001D9F RID: 7583 RVA: 0x0001BE08 File Offset: 0x0001A008
		public static void CreateUIVertexStream(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector2> uv0S, List<Vector2> uv1S, List<Vector3> normals, List<Vector4> tangents, List<int> indicies)
		{
			CanvasRenderer.CreateUIVertexStreamInternal(verts, positions, colors, uv0S, uv1S, normals, tangents, indicies);
		}

		// Token: 0x06001DA0 RID: 7584
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateUIVertexStreamInternal(object verts, object positions, object colors, object uv0S, object uv1S, object normals, object tangents, object indicies);

		// Token: 0x06001DA1 RID: 7585 RVA: 0x0001BE28 File Offset: 0x0001A028
		public static void AddUIVertexStream(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector2> uv0S, List<Vector2> uv1S, List<Vector3> normals, List<Vector4> tangents)
		{
			CanvasRenderer.SplitUIVertexStreamsInternal(verts, positions, colors, uv0S, uv1S, normals, tangents);
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001DA2 RID: 7586
		public extern int relativeDepth { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001DA3 RID: 7587
		// (set) Token: 0x06001DA4 RID: 7588
		public extern bool cull { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001DA5 RID: 7589
		public extern int absoluteDepth { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001DA6 RID: 7590
		public extern bool hasMoved { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
