using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000033 RID: 51
	public sealed class Graphics
	{
		// Token: 0x0600025E RID: 606 RVA: 0x00003258 File Offset: 0x00001458
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows)
		{
			bool receiveShadows = true;
			Graphics.DrawMesh(mesh, position, rotation, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000327C File Offset: 0x0000147C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties)
		{
			bool receiveShadows = true;
			bool castShadows = true;
			Graphics.DrawMesh(mesh, position, rotation, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000032A0 File Offset: 0x000014A0
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex)
		{
			bool receiveShadows = true;
			bool castShadows = true;
			MaterialPropertyBlock properties = null;
			Graphics.DrawMesh(mesh, position, rotation, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000032C8 File Offset: 0x000014C8
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera)
		{
			bool receiveShadows = true;
			bool castShadows = true;
			MaterialPropertyBlock properties = null;
			int submeshIndex = 0;
			Graphics.DrawMesh(mesh, position, rotation, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000032F0 File Offset: 0x000014F0
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer)
		{
			bool receiveShadows = true;
			bool castShadows = true;
			MaterialPropertyBlock properties = null;
			int submeshIndex = 0;
			Camera camera = null;
			Graphics.DrawMesh(mesh, position, rotation, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000331C File Offset: 0x0000151C
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, [DefaultValue("null")] Camera camera, [DefaultValue("0")] int submeshIndex, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("true")] bool castShadows, [DefaultValue("true")] bool receiveShadows)
		{
			Graphics.DrawMesh(mesh, position, rotation, material, layer, camera, submeshIndex, properties, (!castShadows) ? ShadowCastingMode.Off : ShadowCastingMode.On, receiveShadows);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000334C File Offset: 0x0000154C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows)
		{
			Transform probeAnchor = null;
			Graphics.DrawMesh(mesh, position, rotation, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00003374 File Offset: 0x00001574
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows)
		{
			Transform probeAnchor = null;
			bool receiveShadows = true;
			Graphics.DrawMesh(mesh, position, rotation, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000339C File Offset: 0x0000159C
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("null")] Transform probeAnchor)
		{
			Internal_DrawMeshTRArguments internal_DrawMeshTRArguments = default(Internal_DrawMeshTRArguments);
			internal_DrawMeshTRArguments.position = position;
			internal_DrawMeshTRArguments.rotation = rotation;
			internal_DrawMeshTRArguments.layer = layer;
			internal_DrawMeshTRArguments.submeshIndex = submeshIndex;
			internal_DrawMeshTRArguments.castShadows = (int)castShadows;
			internal_DrawMeshTRArguments.receiveShadows = ((!receiveShadows) ? 0 : 1);
			internal_DrawMeshTRArguments.reflectionProbeAnchorInstanceID = ((!(probeAnchor != null)) ? 0 : probeAnchor.GetInstanceID());
			Graphics.Internal_DrawMeshTR(ref internal_DrawMeshTRArguments, properties, material, mesh, camera);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00003420 File Offset: 0x00001620
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows)
		{
			bool receiveShadows = true;
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00003444 File Offset: 0x00001644
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties)
		{
			bool receiveShadows = true;
			bool castShadows = true;
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00003468 File Offset: 0x00001668
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex)
		{
			bool receiveShadows = true;
			bool castShadows = true;
			MaterialPropertyBlock properties = null;
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000348C File Offset: 0x0000168C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera)
		{
			bool receiveShadows = true;
			bool castShadows = true;
			MaterialPropertyBlock properties = null;
			int submeshIndex = 0;
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000034B0 File Offset: 0x000016B0
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer)
		{
			bool receiveShadows = true;
			bool castShadows = true;
			MaterialPropertyBlock properties = null;
			int submeshIndex = 0;
			Camera camera = null;
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000034D8 File Offset: 0x000016D8
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, [DefaultValue("null")] Camera camera, [DefaultValue("0")] int submeshIndex, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("true")] bool castShadows, [DefaultValue("true")] bool receiveShadows)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, (!castShadows) ? ShadowCastingMode.Off : ShadowCastingMode.On, receiveShadows);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00003504 File Offset: 0x00001704
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows)
		{
			Transform probeAnchor = null;
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00003528 File Offset: 0x00001728
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows)
		{
			Transform probeAnchor = null;
			bool receiveShadows = true;
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000354C File Offset: 0x0000174C
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("null")] Transform probeAnchor)
		{
			Internal_DrawMeshMatrixArguments internal_DrawMeshMatrixArguments = default(Internal_DrawMeshMatrixArguments);
			internal_DrawMeshMatrixArguments.matrix = matrix;
			internal_DrawMeshMatrixArguments.layer = layer;
			internal_DrawMeshMatrixArguments.submeshIndex = submeshIndex;
			internal_DrawMeshMatrixArguments.castShadows = (int)castShadows;
			internal_DrawMeshMatrixArguments.receiveShadows = ((!receiveShadows) ? 0 : 1);
			internal_DrawMeshMatrixArguments.reflectionProbeAnchorInstanceID = ((!(probeAnchor != null)) ? 0 : probeAnchor.GetInstanceID());
			Graphics.Internal_DrawMeshMatrix(ref internal_DrawMeshMatrixArguments, properties, material, mesh, camera);
		}

		// Token: 0x06000270 RID: 624
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshTR(ref Internal_DrawMeshTRArguments arguments, MaterialPropertyBlock properties, Material material, Mesh mesh, Camera camera);

		// Token: 0x06000271 RID: 625
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshMatrix(ref Internal_DrawMeshMatrixArguments arguments, MaterialPropertyBlock properties, Material material, Mesh mesh, Camera camera);

		// Token: 0x06000272 RID: 626 RVA: 0x000035C8 File Offset: 0x000017C8
		public static void DrawMeshNow(Mesh mesh, Vector3 position, Quaternion rotation)
		{
			Graphics.Internal_DrawMeshNow1(mesh, position, rotation, -1);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000035D4 File Offset: 0x000017D4
		public static void DrawMeshNow(Mesh mesh, Vector3 position, Quaternion rotation, int materialIndex)
		{
			Graphics.Internal_DrawMeshNow1(mesh, position, rotation, materialIndex);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000035E0 File Offset: 0x000017E0
		public static void DrawMeshNow(Mesh mesh, Matrix4x4 matrix)
		{
			Graphics.Internal_DrawMeshNow2(mesh, matrix, -1);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000035EC File Offset: 0x000017EC
		public static void DrawMeshNow(Mesh mesh, Matrix4x4 matrix, int materialIndex)
		{
			Graphics.Internal_DrawMeshNow2(mesh, matrix, materialIndex);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000035F8 File Offset: 0x000017F8
		private static void Internal_DrawMeshNow1(Mesh mesh, Vector3 position, Quaternion rotation, int materialIndex)
		{
			Graphics.INTERNAL_CALL_Internal_DrawMeshNow1(mesh, ref position, ref rotation, materialIndex);
		}

		// Token: 0x06000277 RID: 631
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_DrawMeshNow1(Mesh mesh, ref Vector3 position, ref Quaternion rotation, int materialIndex);

		// Token: 0x06000278 RID: 632 RVA: 0x00003608 File Offset: 0x00001808
		private static void Internal_DrawMeshNow2(Mesh mesh, Matrix4x4 matrix, int materialIndex)
		{
			Graphics.INTERNAL_CALL_Internal_DrawMeshNow2(mesh, ref matrix, materialIndex);
		}

		// Token: 0x06000279 RID: 633
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_DrawMeshNow2(Mesh mesh, ref Matrix4x4 matrix, int materialIndex);

		// Token: 0x0600027A RID: 634
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DrawProcedural(MeshTopology topology, int vertexCount, [DefaultValue("1")] int instanceCount);

		// Token: 0x0600027B RID: 635 RVA: 0x00003614 File Offset: 0x00001814
		[ExcludeFromDocs]
		public static void DrawProcedural(MeshTopology topology, int vertexCount)
		{
			int instanceCount = 1;
			Graphics.DrawProcedural(topology, vertexCount, instanceCount);
		}

		// Token: 0x0600027C RID: 636
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DrawProceduralIndirect(MeshTopology topology, ComputeBuffer bufferWithArgs, [DefaultValue("0")] int argsOffset);

		// Token: 0x0600027D RID: 637 RVA: 0x0000362C File Offset: 0x0000182C
		[ExcludeFromDocs]
		public static void DrawProceduralIndirect(MeshTopology topology, ComputeBuffer bufferWithArgs)
		{
			int argsOffset = 0;
			Graphics.DrawProceduralIndirect(topology, bufferWithArgs, argsOffset);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00003644 File Offset: 0x00001844
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture)
		{
			Material mat = null;
			Graphics.DrawTexture(screenRect, texture, mat);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000365C File Offset: 0x0000185C
		public static void DrawTexture(Rect screenRect, Texture texture, [DefaultValue("null")] Material mat)
		{
			Graphics.DrawTexture(screenRect, texture, 0, 0, 0, 0, mat);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000366C File Offset: 0x0000186C
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
		{
			Material mat = null;
			Graphics.DrawTexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, mat);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000368C File Offset: 0x0000188C
		public static void DrawTexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [DefaultValue("null")] Material mat)
		{
			Graphics.DrawTexture(screenRect, texture, new Rect(0f, 0f, 1f, 1f), leftBorder, rightBorder, topBorder, bottomBorder, mat);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000036C4 File Offset: 0x000018C4
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
		{
			Material mat = null;
			Graphics.DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, mat);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000036E4 File Offset: 0x000018E4
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [DefaultValue("null")] Material mat)
		{
			InternalDrawTextureArguments internalDrawTextureArguments = default(InternalDrawTextureArguments);
			internalDrawTextureArguments.screenRect = screenRect;
			internalDrawTextureArguments.texture = texture;
			internalDrawTextureArguments.sourceRect = sourceRect;
			internalDrawTextureArguments.leftBorder = leftBorder;
			internalDrawTextureArguments.rightBorder = rightBorder;
			internalDrawTextureArguments.topBorder = topBorder;
			internalDrawTextureArguments.bottomBorder = bottomBorder;
			Color32 color = default(Color32);
			color.r = (color.g = (color.b = (color.a = 128)));
			internalDrawTextureArguments.color = color;
			internalDrawTextureArguments.mat = mat;
			Graphics.DrawTexture(ref internalDrawTextureArguments);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00003780 File Offset: 0x00001980
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color)
		{
			Material mat = null;
			Graphics.DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, mat);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000037A4 File Offset: 0x000019A4
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color, [DefaultValue("null")] Material mat)
		{
			InternalDrawTextureArguments internalDrawTextureArguments = default(InternalDrawTextureArguments);
			internalDrawTextureArguments.screenRect = screenRect;
			internalDrawTextureArguments.texture = texture;
			internalDrawTextureArguments.sourceRect = sourceRect;
			internalDrawTextureArguments.leftBorder = leftBorder;
			internalDrawTextureArguments.rightBorder = rightBorder;
			internalDrawTextureArguments.topBorder = topBorder;
			internalDrawTextureArguments.bottomBorder = bottomBorder;
			internalDrawTextureArguments.color = color;
			internalDrawTextureArguments.mat = mat;
			Graphics.DrawTexture(ref internalDrawTextureArguments);
		}

		// Token: 0x06000286 RID: 646
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DrawTexture(ref InternalDrawTextureArguments arguments);

		// Token: 0x06000287 RID: 647
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ExecuteCommandBuffer(CommandBuffer buffer);

		// Token: 0x06000288 RID: 648
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Blit(Texture source, RenderTexture dest);

		// Token: 0x06000289 RID: 649 RVA: 0x00003814 File Offset: 0x00001A14
		[ExcludeFromDocs]
		public static void Blit(Texture source, RenderTexture dest, Material mat)
		{
			int pass = -1;
			Graphics.Blit(source, dest, mat, pass);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000382C File Offset: 0x00001A2C
		public static void Blit(Texture source, RenderTexture dest, Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.Internal_BlitMaterial(source, dest, mat, pass, true);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00003838 File Offset: 0x00001A38
		[ExcludeFromDocs]
		public static void Blit(Texture source, Material mat)
		{
			int pass = -1;
			Graphics.Blit(source, mat, pass);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00003850 File Offset: 0x00001A50
		public static void Blit(Texture source, Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.Internal_BlitMaterial(source, null, mat, pass, false);
		}

		// Token: 0x0600028D RID: 653
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BlitMaterial(Texture source, RenderTexture dest, Material mat, int pass, bool setRT);

		// Token: 0x0600028E RID: 654 RVA: 0x0000385C File Offset: 0x00001A5C
		public static void BlitMultiTap(Texture source, RenderTexture dest, Material mat, params Vector2[] offsets)
		{
			Graphics.Internal_BlitMultiTap(source, dest, mat, offsets);
		}

		// Token: 0x0600028F RID: 655
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BlitMultiTap(Texture source, RenderTexture dest, Material mat, Vector2[] offsets);

		// Token: 0x06000290 RID: 656
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetNullRT();

		// Token: 0x06000291 RID: 657
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRTSimple(out RenderBuffer color, out RenderBuffer depth, int mip, CubemapFace face);

		// Token: 0x06000292 RID: 658
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetMRTFullSetup(RenderBuffer[] colorSA, out RenderBuffer depth, int mip, CubemapFace face, RenderBufferLoadAction[] colorLoadSA, RenderBufferStoreAction[] colorStoreSA, RenderBufferLoadAction depthLoad, RenderBufferStoreAction depthStore);

		// Token: 0x06000293 RID: 659
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetMRTSimple(RenderBuffer[] colorSA, out RenderBuffer depth, int mip, CubemapFace face);

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00003868 File Offset: 0x00001A68
		public static RenderBuffer activeColorBuffer
		{
			get
			{
				RenderBuffer result;
				Graphics.GetActiveColorBuffer(out result);
				return result;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00003880 File Offset: 0x00001A80
		public static RenderBuffer activeDepthBuffer
		{
			get
			{
				RenderBuffer result;
				Graphics.GetActiveDepthBuffer(out result);
				return result;
			}
		}

		// Token: 0x06000296 RID: 662
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActiveColorBuffer(out RenderBuffer res);

		// Token: 0x06000297 RID: 663
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActiveDepthBuffer(out RenderBuffer res);

		// Token: 0x06000298 RID: 664 RVA: 0x00003898 File Offset: 0x00001A98
		public static void SetRandomWriteTarget(int index, RenderTexture uav)
		{
			Graphics.Internal_SetRandomWriteTargetRT(index, uav);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000038A4 File Offset: 0x00001AA4
		public static void SetRandomWriteTarget(int index, ComputeBuffer uav)
		{
			Graphics.Internal_SetRandomWriteTargetBuffer(index, uav);
		}

		// Token: 0x0600029A RID: 666
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearRandomWriteTargets();

		// Token: 0x0600029B RID: 667
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRandomWriteTargetRT(int index, RenderTexture uav);

		// Token: 0x0600029C RID: 668
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRandomWriteTargetBuffer(int index, ComputeBuffer uav);

		// Token: 0x0600029D RID: 669
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetupVertexLights(Light[] lights);

		// Token: 0x0600029E RID: 670 RVA: 0x000038B0 File Offset: 0x00001AB0
		internal static void CheckLoadActionValid(RenderBufferLoadAction load, string bufferType)
		{
			if (load != RenderBufferLoadAction.Load && load != RenderBufferLoadAction.DontCare)
			{
				throw new ArgumentException(UnityString.Format("Bad {0} LoadAction provided.", new object[]
				{
					bufferType
				}));
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000038DC File Offset: 0x00001ADC
		internal static void CheckStoreActionValid(RenderBufferStoreAction store, string bufferType)
		{
			if (store != RenderBufferStoreAction.Store && store != RenderBufferStoreAction.DontCare)
			{
				throw new ArgumentException(UnityString.Format("Bad {0} StoreAction provided.", new object[]
				{
					bufferType
				}));
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00003908 File Offset: 0x00001B08
		internal static void SetRenderTargetImpl(RenderTargetSetup setup)
		{
			if (setup.color.Length == 0)
			{
				throw new ArgumentException("Invalid color buffer count for SetRenderTarget");
			}
			if (setup.color.Length != setup.colorLoad.Length)
			{
				throw new ArgumentException("Color LoadAction and Buffer arrays have different sizes");
			}
			if (setup.color.Length != setup.colorStore.Length)
			{
				throw new ArgumentException("Color StoreAction and Buffer arrays have different sizes");
			}
			foreach (RenderBufferLoadAction load in setup.colorLoad)
			{
				Graphics.CheckLoadActionValid(load, "Color");
			}
			foreach (RenderBufferStoreAction store in setup.colorStore)
			{
				Graphics.CheckStoreActionValid(store, "Color");
			}
			Graphics.CheckLoadActionValid(setup.depthLoad, "Depth");
			Graphics.CheckStoreActionValid(setup.depthStore, "Depth");
			if (setup.cubemapFace < CubemapFace.Unknown || setup.cubemapFace > CubemapFace.NegativeZ)
			{
				throw new ArgumentException("Bad CubemapFace provided");
			}
			Graphics.Internal_SetMRTFullSetup(setup.color, out setup.depth, setup.mipLevel, setup.cubemapFace, setup.colorLoad, setup.colorStore, setup.depthLoad, setup.depthStore);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00003A54 File Offset: 0x00001C54
		internal static void SetRenderTargetImpl(RenderBuffer colorBuffer, RenderBuffer depthBuffer, int mipLevel, CubemapFace face)
		{
			RenderBuffer renderBuffer = colorBuffer;
			RenderBuffer renderBuffer2 = depthBuffer;
			Graphics.Internal_SetRTSimple(out renderBuffer, out renderBuffer2, mipLevel, face);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00003A70 File Offset: 0x00001C70
		internal static void SetRenderTargetImpl(RenderTexture rt, int mipLevel, CubemapFace face)
		{
			if (rt)
			{
				Graphics.SetRenderTargetImpl(rt.colorBuffer, rt.depthBuffer, mipLevel, face);
			}
			else
			{
				Graphics.Internal_SetNullRT();
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00003AA8 File Offset: 0x00001CA8
		internal static void SetRenderTargetImpl(RenderBuffer[] colorBuffers, RenderBuffer depthBuffer, int mipLevel, CubemapFace face)
		{
			RenderBuffer renderBuffer = depthBuffer;
			Graphics.Internal_SetMRTSimple(colorBuffers, out renderBuffer, mipLevel, face);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00003AC4 File Offset: 0x00001CC4
		public static void SetRenderTarget(RenderTexture rt)
		{
			Graphics.SetRenderTargetImpl(rt, 0, CubemapFace.Unknown);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00003AD0 File Offset: 0x00001CD0
		public static void SetRenderTarget(RenderTexture rt, int mipLevel)
		{
			Graphics.SetRenderTargetImpl(rt, mipLevel, CubemapFace.Unknown);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00003ADC File Offset: 0x00001CDC
		public static void SetRenderTarget(RenderTexture rt, int mipLevel, CubemapFace face)
		{
			Graphics.SetRenderTargetImpl(rt, mipLevel, face);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00003AE8 File Offset: 0x00001CE8
		public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer)
		{
			Graphics.SetRenderTargetImpl(colorBuffer, depthBuffer, 0, CubemapFace.Unknown);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00003AF4 File Offset: 0x00001CF4
		public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer, int mipLevel)
		{
			Graphics.SetRenderTargetImpl(colorBuffer, depthBuffer, mipLevel, CubemapFace.Unknown);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00003B00 File Offset: 0x00001D00
		public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer, int mipLevel, CubemapFace face)
		{
			Graphics.SetRenderTargetImpl(colorBuffer, depthBuffer, mipLevel, face);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00003B0C File Offset: 0x00001D0C
		public static void SetRenderTarget(RenderBuffer[] colorBuffers, RenderBuffer depthBuffer)
		{
			Graphics.SetRenderTargetImpl(colorBuffers, depthBuffer, 0, CubemapFace.Unknown);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00003B18 File Offset: 0x00001D18
		public static void SetRenderTarget(RenderTargetSetup setup)
		{
			Graphics.SetRenderTargetImpl(setup);
		}
	}
}
