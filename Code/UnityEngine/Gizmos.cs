using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000058 RID: 88
	public sealed class Gizmos
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x00004B08 File Offset: 0x00002D08
		public static void DrawRay(Ray r)
		{
			Gizmos.DrawLine(r.origin, r.origin + r.direction);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00004B34 File Offset: 0x00002D34
		public static void DrawRay(Vector3 from, Vector3 direction)
		{
			Gizmos.DrawLine(from, from + direction);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00004B44 File Offset: 0x00002D44
		public static void DrawLine(Vector3 from, Vector3 to)
		{
			Gizmos.INTERNAL_CALL_DrawLine(ref from, ref to);
		}

		// Token: 0x060004A3 RID: 1187
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DrawLine(ref Vector3 from, ref Vector3 to);

		// Token: 0x060004A4 RID: 1188 RVA: 0x00004B50 File Offset: 0x00002D50
		public static void DrawWireSphere(Vector3 center, float radius)
		{
			Gizmos.INTERNAL_CALL_DrawWireSphere(ref center, radius);
		}

		// Token: 0x060004A5 RID: 1189
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DrawWireSphere(ref Vector3 center, float radius);

		// Token: 0x060004A6 RID: 1190 RVA: 0x00004B5C File Offset: 0x00002D5C
		public static void DrawSphere(Vector3 center, float radius)
		{
			Gizmos.INTERNAL_CALL_DrawSphere(ref center, radius);
		}

		// Token: 0x060004A7 RID: 1191
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DrawSphere(ref Vector3 center, float radius);

		// Token: 0x060004A8 RID: 1192 RVA: 0x00004B68 File Offset: 0x00002D68
		public static void DrawWireCube(Vector3 center, Vector3 size)
		{
			Gizmos.INTERNAL_CALL_DrawWireCube(ref center, ref size);
		}

		// Token: 0x060004A9 RID: 1193
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DrawWireCube(ref Vector3 center, ref Vector3 size);

		// Token: 0x060004AA RID: 1194 RVA: 0x00004B74 File Offset: 0x00002D74
		public static void DrawCube(Vector3 center, Vector3 size)
		{
			Gizmos.INTERNAL_CALL_DrawCube(ref center, ref size);
		}

		// Token: 0x060004AB RID: 1195
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DrawCube(ref Vector3 center, ref Vector3 size);

		// Token: 0x060004AC RID: 1196 RVA: 0x00004B80 File Offset: 0x00002D80
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation)
		{
			Vector3 one = Vector3.one;
			Gizmos.DrawMesh(mesh, position, rotation, one);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00004B9C File Offset: 0x00002D9C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Gizmos.DrawMesh(mesh, position, identity, one);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00004BC0 File Offset: 0x00002DC0
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Vector3 zero = Vector3.zero;
			Gizmos.DrawMesh(mesh, zero, identity, one);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00004BE8 File Offset: 0x00002DE8
		public static void DrawMesh(Mesh mesh, [DefaultValue("Vector3.zero")] Vector3 position, [DefaultValue("Quaternion.identity")] Quaternion rotation, [DefaultValue("Vector3.one")] Vector3 scale)
		{
			Gizmos.DrawMesh(mesh, -1, position, rotation, scale);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00004BF4 File Offset: 0x00002DF4
		public static void DrawMesh(Mesh mesh, int submeshIndex, [DefaultValue("Vector3.zero")] Vector3 position, [DefaultValue("Quaternion.identity")] Quaternion rotation, [DefaultValue("Vector3.one")] Vector3 scale)
		{
			Gizmos.INTERNAL_CALL_DrawMesh(mesh, submeshIndex, ref position, ref rotation, ref scale);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00004C04 File Offset: 0x00002E04
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, int submeshIndex, Vector3 position, Quaternion rotation)
		{
			Vector3 one = Vector3.one;
			Gizmos.INTERNAL_CALL_DrawMesh(mesh, submeshIndex, ref position, ref rotation, ref one);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00004C24 File Offset: 0x00002E24
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, int submeshIndex, Vector3 position)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Gizmos.INTERNAL_CALL_DrawMesh(mesh, submeshIndex, ref position, ref identity, ref one);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00004C4C File Offset: 0x00002E4C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, int submeshIndex)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Vector3 zero = Vector3.zero;
			Gizmos.INTERNAL_CALL_DrawMesh(mesh, submeshIndex, ref zero, ref identity, ref one);
		}

		// Token: 0x060004B4 RID: 1204
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DrawMesh(Mesh mesh, int submeshIndex, ref Vector3 position, ref Quaternion rotation, ref Vector3 scale);

		// Token: 0x060004B5 RID: 1205 RVA: 0x00004C78 File Offset: 0x00002E78
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh, Vector3 position, Quaternion rotation)
		{
			Vector3 one = Vector3.one;
			Gizmos.DrawWireMesh(mesh, position, rotation, one);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00004C94 File Offset: 0x00002E94
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh, Vector3 position)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Gizmos.DrawWireMesh(mesh, position, identity, one);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00004CB8 File Offset: 0x00002EB8
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Vector3 zero = Vector3.zero;
			Gizmos.DrawWireMesh(mesh, zero, identity, one);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00004CE0 File Offset: 0x00002EE0
		public static void DrawWireMesh(Mesh mesh, [DefaultValue("Vector3.zero")] Vector3 position, [DefaultValue("Quaternion.identity")] Quaternion rotation, [DefaultValue("Vector3.one")] Vector3 scale)
		{
			Gizmos.DrawWireMesh(mesh, -1, position, rotation, scale);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00004CEC File Offset: 0x00002EEC
		public static void DrawWireMesh(Mesh mesh, int submeshIndex, [DefaultValue("Vector3.zero")] Vector3 position, [DefaultValue("Quaternion.identity")] Quaternion rotation, [DefaultValue("Vector3.one")] Vector3 scale)
		{
			Gizmos.INTERNAL_CALL_DrawWireMesh(mesh, submeshIndex, ref position, ref rotation, ref scale);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00004CFC File Offset: 0x00002EFC
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh, int submeshIndex, Vector3 position, Quaternion rotation)
		{
			Vector3 one = Vector3.one;
			Gizmos.INTERNAL_CALL_DrawWireMesh(mesh, submeshIndex, ref position, ref rotation, ref one);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00004D1C File Offset: 0x00002F1C
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh, int submeshIndex, Vector3 position)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Gizmos.INTERNAL_CALL_DrawWireMesh(mesh, submeshIndex, ref position, ref identity, ref one);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00004D44 File Offset: 0x00002F44
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh, int submeshIndex)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Vector3 zero = Vector3.zero;
			Gizmos.INTERNAL_CALL_DrawWireMesh(mesh, submeshIndex, ref zero, ref identity, ref one);
		}

		// Token: 0x060004BD RID: 1213
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DrawWireMesh(Mesh mesh, int submeshIndex, ref Vector3 position, ref Quaternion rotation, ref Vector3 scale);

		// Token: 0x060004BE RID: 1214 RVA: 0x00004D70 File Offset: 0x00002F70
		public static void DrawIcon(Vector3 center, string name, [DefaultValue("true")] bool allowScaling)
		{
			Gizmos.INTERNAL_CALL_DrawIcon(ref center, name, allowScaling);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00004D7C File Offset: 0x00002F7C
		[ExcludeFromDocs]
		public static void DrawIcon(Vector3 center, string name)
		{
			bool allowScaling = true;
			Gizmos.INTERNAL_CALL_DrawIcon(ref center, name, allowScaling);
		}

		// Token: 0x060004C0 RID: 1216
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DrawIcon(ref Vector3 center, string name, bool allowScaling);

		// Token: 0x060004C1 RID: 1217 RVA: 0x00004D94 File Offset: 0x00002F94
		[ExcludeFromDocs]
		public static void DrawGUITexture(Rect screenRect, Texture texture)
		{
			Material mat = null;
			Gizmos.DrawGUITexture(screenRect, texture, mat);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00004DAC File Offset: 0x00002FAC
		public static void DrawGUITexture(Rect screenRect, Texture texture, [DefaultValue("null")] Material mat)
		{
			Gizmos.DrawGUITexture(screenRect, texture, 0, 0, 0, 0, mat);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00004DBC File Offset: 0x00002FBC
		public static void DrawGUITexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [DefaultValue("null")] Material mat)
		{
			Gizmos.INTERNAL_CALL_DrawGUITexture(ref screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, mat);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00004DD0 File Offset: 0x00002FD0
		[ExcludeFromDocs]
		public static void DrawGUITexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
		{
			Material mat = null;
			Gizmos.INTERNAL_CALL_DrawGUITexture(ref screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, mat);
		}

		// Token: 0x060004C5 RID: 1221
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DrawGUITexture(ref Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Material mat);

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00004DF0 File Offset: 0x00002FF0
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x00004E08 File Offset: 0x00003008
		public static Color color
		{
			get
			{
				Color result;
				Gizmos.INTERNAL_get_color(out result);
				return result;
			}
			set
			{
				Gizmos.INTERNAL_set_color(ref value);
			}
		}

		// Token: 0x060004C8 RID: 1224
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_color(out Color value);

		// Token: 0x060004C9 RID: 1225
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_color(ref Color value);

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00004E14 File Offset: 0x00003014
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x00004E2C File Offset: 0x0000302C
		public static Matrix4x4 matrix
		{
			get
			{
				Matrix4x4 result;
				Gizmos.INTERNAL_get_matrix(out result);
				return result;
			}
			set
			{
				Gizmos.INTERNAL_set_matrix(ref value);
			}
		}

		// Token: 0x060004CC RID: 1228
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_matrix(out Matrix4x4 value);

		// Token: 0x060004CD RID: 1229
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_matrix(ref Matrix4x4 value);

		// Token: 0x060004CE RID: 1230 RVA: 0x00004E38 File Offset: 0x00003038
		public static void DrawFrustum(Vector3 center, float fov, float maxRange, float minRange, float aspect)
		{
			Gizmos.INTERNAL_CALL_DrawFrustum(ref center, fov, maxRange, minRange, aspect);
		}

		// Token: 0x060004CF RID: 1231
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DrawFrustum(ref Vector3 center, float fov, float maxRange, float minRange, float aspect);
	}
}
