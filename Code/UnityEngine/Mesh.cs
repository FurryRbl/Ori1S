using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000024 RID: 36
	public sealed class Mesh : Object
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00002AC0 File Offset: 0x00000CC0
		public Mesh()
		{
			Mesh.Internal_Create(this);
		}

		// Token: 0x0600014C RID: 332
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] Mesh mono);

		// Token: 0x0600014D RID: 333
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Clear([DefaultValue("true")] bool keepVertexLayout);

		// Token: 0x0600014E RID: 334 RVA: 0x00002AD0 File Offset: 0x00000CD0
		[ExcludeFromDocs]
		public void Clear()
		{
			bool keepVertexLayout = true;
			this.Clear(keepVertexLayout);
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600014F RID: 335
		public extern bool isReadable { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000150 RID: 336
		internal extern bool canAccess { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000151 RID: 337
		// (set) Token: 0x06000152 RID: 338
		public extern Vector3[] vertices { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000153 RID: 339 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public void SetVertices(List<Vector3> inVertices)
		{
			this.SetVerticesInternal(inVertices);
		}

		// Token: 0x06000154 RID: 340
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVerticesInternal(object vertices);

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000155 RID: 341
		// (set) Token: 0x06000156 RID: 342
		public extern Vector3[] normals { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000157 RID: 343 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public void SetNormals(List<Vector3> inNormals)
		{
			this.SetNormalsInternal(inNormals);
		}

		// Token: 0x06000158 RID: 344
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetNormalsInternal(object normals);

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000159 RID: 345
		// (set) Token: 0x0600015A RID: 346
		public extern Vector4[] tangents { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600015B RID: 347 RVA: 0x00002B00 File Offset: 0x00000D00
		public void SetTangents(List<Vector4> inTangents)
		{
			this.SetTangentsInternal(inTangents);
		}

		// Token: 0x0600015C RID: 348
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTangentsInternal(object tangents);

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600015D RID: 349
		// (set) Token: 0x0600015E RID: 350
		public extern Vector2[] uv { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600015F RID: 351
		// (set) Token: 0x06000160 RID: 352
		public extern Vector2[] uv2 { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000161 RID: 353
		// (set) Token: 0x06000162 RID: 354
		public extern Vector2[] uv3 { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000163 RID: 355
		// (set) Token: 0x06000164 RID: 356
		public extern Vector2[] uv4 { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000165 RID: 357
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Array ExtractListData(object list);

		// Token: 0x06000166 RID: 358
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetUVsInternal(Array uvs, int channel, int dim, int arraySize);

		// Token: 0x06000167 RID: 359
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetUVsInternal(Array uvs, int channel, int dim);

		// Token: 0x06000168 RID: 360
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool CheckCanAccessUVChannel(int channel);

		// Token: 0x06000169 RID: 361
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResizeList(object list, int size);

		// Token: 0x0600016A RID: 362 RVA: 0x00002B0C File Offset: 0x00000D0C
		private void GetUVsImpl<T>(int channel, List<T> uvs, int dim)
		{
			if (uvs == null)
			{
				throw new ArgumentException("The result uvs list cannot be null");
			}
			if (this.CheckCanAccessUVChannel(channel))
			{
				int vertexCount = this.vertexCount;
				if (vertexCount > uvs.Capacity)
				{
					uvs.Capacity = vertexCount;
				}
				Mesh.ResizeList(uvs, vertexCount);
				this.GetUVsInternal(Mesh.ExtractListData(uvs), channel, dim);
			}
			else
			{
				uvs.Clear();
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00002B70 File Offset: 0x00000D70
		public void SetUVs(int channel, List<Vector2> uvs)
		{
			this.SetUVsInternal(Mesh.ExtractListData(uvs), channel, 2, uvs.Count);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00002B94 File Offset: 0x00000D94
		public void SetUVs(int channel, List<Vector3> uvs)
		{
			this.SetUVsInternal(Mesh.ExtractListData(uvs), channel, 3, uvs.Count);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public void SetUVs(int channel, List<Vector4> uvs)
		{
			this.SetUVsInternal(Mesh.ExtractListData(uvs), channel, 4, uvs.Count);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00002BDC File Offset: 0x00000DDC
		public void GetUVs(int channel, List<Vector2> uvs)
		{
			this.GetUVsImpl<Vector2>(channel, uvs, 2);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public void GetUVs(int channel, List<Vector3> uvs)
		{
			this.GetUVsImpl<Vector3>(channel, uvs, 3);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public void GetUVs(int channel, List<Vector4> uvs)
		{
			this.GetUVsImpl<Vector4>(channel, uvs, 4);
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00002C00 File Offset: 0x00000E00
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00002C18 File Offset: 0x00000E18
		public Bounds bounds
		{
			get
			{
				Bounds result;
				this.INTERNAL_get_bounds(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_bounds(ref value);
			}
		}

		// Token: 0x06000173 RID: 371
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_bounds(out Bounds value);

		// Token: 0x06000174 RID: 372
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_bounds(ref Bounds value);

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000175 RID: 373
		// (set) Token: 0x06000176 RID: 374
		public extern Color[] colors { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000177 RID: 375 RVA: 0x00002C24 File Offset: 0x00000E24
		public void SetColors(List<Color> inColors)
		{
			this.SetColorsInternal(inColors);
		}

		// Token: 0x06000178 RID: 376
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetColorsInternal(object colors);

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000179 RID: 377
		// (set) Token: 0x0600017A RID: 378
		public extern Color32[] colors32 { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600017B RID: 379 RVA: 0x00002C30 File Offset: 0x00000E30
		public void SetColors(List<Color32> inColors)
		{
			this.SetColors32Internal(inColors);
		}

		// Token: 0x0600017C RID: 380
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetColors32Internal(object colors);

		// Token: 0x0600017D RID: 381
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RecalculateBounds();

		// Token: 0x0600017E RID: 382
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RecalculateNormals();

		// Token: 0x0600017F RID: 383
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Optimize();

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000180 RID: 384
		// (set) Token: 0x06000181 RID: 385
		public extern int[] triangles { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000182 RID: 386
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int[] GetTriangles(int submesh);

		// Token: 0x06000183 RID: 387
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTriangles(int[] triangles, int submesh);

		// Token: 0x06000184 RID: 388 RVA: 0x00002C3C File Offset: 0x00000E3C
		public void SetTriangles(List<int> inTriangles, int submesh)
		{
			this.SetTrianglesInternal(inTriangles, submesh);
		}

		// Token: 0x06000185 RID: 389
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTrianglesInternal(object triangles, int submesh);

		// Token: 0x06000186 RID: 390
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int[] GetIndices(int submesh);

		// Token: 0x06000187 RID: 391
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetIndices(int[] indices, MeshTopology topology, int submesh);

		// Token: 0x06000188 RID: 392
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern MeshTopology GetTopology(int submesh);

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000189 RID: 393
		public extern int vertexCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600018A RID: 394
		// (set) Token: 0x0600018B RID: 395
		public extern int subMeshCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600018C RID: 396
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CombineMeshes(CombineInstance[] combine, [DefaultValue("true")] bool mergeSubMeshes, [DefaultValue("true")] bool useMatrices);

		// Token: 0x0600018D RID: 397 RVA: 0x00002C48 File Offset: 0x00000E48
		[ExcludeFromDocs]
		public void CombineMeshes(CombineInstance[] combine, bool mergeSubMeshes)
		{
			bool useMatrices = true;
			this.CombineMeshes(combine, mergeSubMeshes, useMatrices);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00002C60 File Offset: 0x00000E60
		[ExcludeFromDocs]
		public void CombineMeshes(CombineInstance[] combine)
		{
			bool useMatrices = true;
			bool mergeSubMeshes = true;
			this.CombineMeshes(combine, mergeSubMeshes, useMatrices);
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600018F RID: 399
		// (set) Token: 0x06000190 RID: 400
		public extern BoneWeight[] boneWeights { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000191 RID: 401
		// (set) Token: 0x06000192 RID: 402
		public extern Matrix4x4[] bindposes { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000193 RID: 403
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void MarkDynamic();

		// Token: 0x06000194 RID: 404
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UploadMeshData(bool markNoLogerReadable);

		// Token: 0x06000195 RID: 405
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetBlendShapeIndex(string blendShapeName);

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000196 RID: 406
		public extern int blendShapeCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000197 RID: 407
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetBlendShapeName(int shapeIndex);

		// Token: 0x06000198 RID: 408
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetBlendShapeFrameCount(int shapeIndex);

		// Token: 0x06000199 RID: 409
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetBlendShapeFrameWeight(int shapeIndex, int frameIndex);

		// Token: 0x0600019A RID: 410
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetBlendShapeFrameVertices(int shapeIndex, int frameIndex, Vector3[] deltaVertices, Vector3[] deltaNormals, Vector3[] deltaTangents);

		// Token: 0x0600019B RID: 411
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearBlendShapes();

		// Token: 0x0600019C RID: 412
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddBlendShapeFrame(string shapeName, float frameWeight, Vector3[] deltaVertices, Vector3[] deltaNormals, Vector3[] deltaTangents);
	}
}
