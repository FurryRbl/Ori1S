using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200009E RID: 158
	public sealed class Sprite : Object
	{
		// Token: 0x060008FB RID: 2299 RVA: 0x0000CB2C File Offset: 0x0000AD2C
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, [DefaultValue("100.0f")] float pixelsPerUnit, [DefaultValue("0")] uint extrude, [DefaultValue("SpriteMeshType.Tight")] SpriteMeshType meshType, [DefaultValue("Vector4.zero")] Vector4 border)
		{
			return Sprite.INTERNAL_CALL_Create(texture, ref rect, ref pivot, pixelsPerUnit, extrude, meshType, ref border);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0000CB40 File Offset: 0x0000AD40
		[ExcludeFromDocs]
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType)
		{
			Vector4 zero = Vector4.zero;
			return Sprite.INTERNAL_CALL_Create(texture, ref rect, ref pivot, pixelsPerUnit, extrude, meshType, ref zero);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0000CB64 File Offset: 0x0000AD64
		[ExcludeFromDocs]
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude)
		{
			Vector4 zero = Vector4.zero;
			SpriteMeshType meshType = SpriteMeshType.Tight;
			return Sprite.INTERNAL_CALL_Create(texture, ref rect, ref pivot, pixelsPerUnit, extrude, meshType, ref zero);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0000CB8C File Offset: 0x0000AD8C
		[ExcludeFromDocs]
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit)
		{
			Vector4 zero = Vector4.zero;
			SpriteMeshType meshType = SpriteMeshType.Tight;
			uint extrude = 0U;
			return Sprite.INTERNAL_CALL_Create(texture, ref rect, ref pivot, pixelsPerUnit, extrude, meshType, ref zero);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0000CBB4 File Offset: 0x0000ADB4
		[ExcludeFromDocs]
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot)
		{
			Vector4 zero = Vector4.zero;
			SpriteMeshType meshType = SpriteMeshType.Tight;
			uint extrude = 0U;
			float pixelsPerUnit = 100f;
			return Sprite.INTERNAL_CALL_Create(texture, ref rect, ref pivot, pixelsPerUnit, extrude, meshType, ref zero);
		}

		// Token: 0x06000900 RID: 2304
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Sprite INTERNAL_CALL_Create(Texture2D texture, ref Rect rect, ref Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, ref Vector4 border);

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0000CBE0 File Offset: 0x0000ADE0
		public Bounds bounds
		{
			get
			{
				Bounds result;
				this.INTERNAL_get_bounds(out result);
				return result;
			}
		}

		// Token: 0x06000902 RID: 2306
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_bounds(out Bounds value);

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x0000CBF8 File Offset: 0x0000ADF8
		public Rect rect
		{
			get
			{
				Rect result;
				this.INTERNAL_get_rect(out result);
				return result;
			}
		}

		// Token: 0x06000904 RID: 2308
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rect(out Rect value);

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000905 RID: 2309
		public extern float pixelsPerUnit { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000906 RID: 2310
		public extern Texture2D texture { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000907 RID: 2311
		public extern Texture2D associatedAlphaSplitTexture { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x0000CC10 File Offset: 0x0000AE10
		public Rect textureRect
		{
			get
			{
				Rect result;
				this.INTERNAL_get_textureRect(out result);
				return result;
			}
		}

		// Token: 0x06000909 RID: 2313
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_textureRect(out Rect value);

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x0000CC28 File Offset: 0x0000AE28
		public Vector2 textureRectOffset
		{
			get
			{
				Vector2 result;
				Sprite.Internal_GetTextureRectOffset(this, out result);
				return result;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600090B RID: 2315
		public extern bool packed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600090C RID: 2316
		public extern SpritePackingMode packingMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600090D RID: 2317
		public extern SpritePackingRotation packingRotation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600090E RID: 2318
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTextureRectOffset(Sprite sprite, out Vector2 output);

		// Token: 0x0600090F RID: 2319
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetPivot(Sprite sprite, out Vector2 output);

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0000CC40 File Offset: 0x0000AE40
		public Vector2 pivot
		{
			get
			{
				Vector2 result;
				Sprite.Internal_GetPivot(this, out result);
				return result;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0000CC58 File Offset: 0x0000AE58
		public Vector4 border
		{
			get
			{
				Vector4 result;
				this.INTERNAL_get_border(out result);
				return result;
			}
		}

		// Token: 0x06000912 RID: 2322
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_border(out Vector4 value);

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000913 RID: 2323
		public extern Vector2[] vertices { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000914 RID: 2324
		public extern ushort[] triangles { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000915 RID: 2325
		public extern Vector2[] uv { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000916 RID: 2326
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void OverrideGeometry(Vector2[] vertices, ushort[] triangles);
	}
}
