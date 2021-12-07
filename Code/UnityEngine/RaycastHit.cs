using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200013D RID: 317
	[UsedByNativeCode]
	public struct RaycastHit
	{
		// Token: 0x0600146C RID: 5228 RVA: 0x000163FC File Offset: 0x000145FC
		private static void CalculateRaycastTexCoord(out Vector2 output, Collider col, Vector2 uv, Vector3 point, int face, int index)
		{
			RaycastHit.INTERNAL_CALL_CalculateRaycastTexCoord(out output, col, ref uv, ref point, face, index);
		}

		// Token: 0x0600146D RID: 5229
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_CalculateRaycastTexCoord(out Vector2 output, Collider col, ref Vector2 uv, ref Vector3 point, int face, int index);

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x00016410 File Offset: 0x00014610
		// (set) Token: 0x0600146F RID: 5231 RVA: 0x00016418 File Offset: 0x00014618
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
			set
			{
				this.m_Point = value;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x00016424 File Offset: 0x00014624
		// (set) Token: 0x06001471 RID: 5233 RVA: 0x0001642C File Offset: 0x0001462C
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
			set
			{
				this.m_Normal = value;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x00016438 File Offset: 0x00014638
		// (set) Token: 0x06001473 RID: 5235 RVA: 0x00016480 File Offset: 0x00014680
		public Vector3 barycentricCoordinate
		{
			get
			{
				return new Vector3(1f - (this.m_UV.y + this.m_UV.x), this.m_UV.x, this.m_UV.y);
			}
			set
			{
				this.m_UV = value;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x00016490 File Offset: 0x00014690
		// (set) Token: 0x06001475 RID: 5237 RVA: 0x00016498 File Offset: 0x00014698
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
			set
			{
				this.m_Distance = value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x000164A4 File Offset: 0x000146A4
		public int triangleIndex
		{
			get
			{
				return this.m_FaceID;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x000164AC File Offset: 0x000146AC
		public Vector2 textureCoord
		{
			get
			{
				Vector2 result;
				RaycastHit.CalculateRaycastTexCoord(out result, this.collider, this.m_UV, this.m_Point, this.m_FaceID, 0);
				return result;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x000164DC File Offset: 0x000146DC
		public Vector2 textureCoord2
		{
			get
			{
				Vector2 result;
				RaycastHit.CalculateRaycastTexCoord(out result, this.collider, this.m_UV, this.m_Point, this.m_FaceID, 1);
				return result;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x0001650C File Offset: 0x0001470C
		[Obsolete("Use textureCoord2 instead")]
		public Vector2 textureCoord1
		{
			get
			{
				Vector2 result;
				RaycastHit.CalculateRaycastTexCoord(out result, this.collider, this.m_UV, this.m_Point, this.m_FaceID, 1);
				return result;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x0001653C File Offset: 0x0001473C
		public Vector2 lightmapCoord
		{
			get
			{
				Vector2 result;
				RaycastHit.CalculateRaycastTexCoord(out result, this.collider, this.m_UV, this.m_Point, this.m_FaceID, 1);
				if (this.collider.GetComponent<Renderer>() != null)
				{
					Vector4 lightmapScaleOffset = this.collider.GetComponent<Renderer>().lightmapScaleOffset;
					result.x = result.x * lightmapScaleOffset.x + lightmapScaleOffset.z;
					result.y = result.y * lightmapScaleOffset.y + lightmapScaleOffset.w;
				}
				return result;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x000165D0 File Offset: 0x000147D0
		public Collider collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x000165D8 File Offset: 0x000147D8
		public Rigidbody rigidbody
		{
			get
			{
				return (!(this.collider != null)) ? null : this.collider.attachedRigidbody;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x00016608 File Offset: 0x00014808
		public Transform transform
		{
			get
			{
				Rigidbody rigidbody = this.rigidbody;
				if (rigidbody != null)
				{
					return rigidbody.transform;
				}
				if (this.collider != null)
				{
					return this.collider.transform;
				}
				return null;
			}
		}

		// Token: 0x040003AB RID: 939
		private Vector3 m_Point;

		// Token: 0x040003AC RID: 940
		private Vector3 m_Normal;

		// Token: 0x040003AD RID: 941
		private int m_FaceID;

		// Token: 0x040003AE RID: 942
		private float m_Distance;

		// Token: 0x040003AF RID: 943
		private Vector2 m_UV;

		// Token: 0x040003B0 RID: 944
		private Collider m_Collider;
	}
}
