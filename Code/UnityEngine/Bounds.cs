using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000062 RID: 98
	[UsedByNativeCode]
	public struct Bounds
	{
		// Token: 0x060005E8 RID: 1512 RVA: 0x0000885C File Offset: 0x00006A5C
		public Bounds(Vector3 center, Vector3 size)
		{
			this.m_Center = center;
			this.m_Extents = size * 0.5f;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00008878 File Offset: 0x00006A78
		public override int GetHashCode()
		{
			return this.center.GetHashCode() ^ this.extents.GetHashCode() << 2;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000088A4 File Offset: 0x00006AA4
		public override bool Equals(object other)
		{
			if (!(other is Bounds))
			{
				return false;
			}
			Bounds bounds = (Bounds)other;
			return this.center.Equals(bounds.center) && this.extents.Equals(bounds.extents);
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x00008904 File Offset: 0x00006B04
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x0000890C File Offset: 0x00006B0C
		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00008918 File Offset: 0x00006B18
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x0000892C File Offset: 0x00006B2C
		public Vector3 size
		{
			get
			{
				return this.m_Extents * 2f;
			}
			set
			{
				this.m_Extents = value * 0.5f;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00008940 File Offset: 0x00006B40
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x00008948 File Offset: 0x00006B48
		public Vector3 extents
		{
			get
			{
				return this.m_Extents;
			}
			set
			{
				this.m_Extents = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00008954 File Offset: 0x00006B54
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x00008968 File Offset: 0x00006B68
		public Vector3 min
		{
			get
			{
				return this.center - this.extents;
			}
			set
			{
				this.SetMinMax(value, this.max);
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00008978 File Offset: 0x00006B78
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x0000898C File Offset: 0x00006B8C
		public Vector3 max
		{
			get
			{
				return this.center + this.extents;
			}
			set
			{
				this.SetMinMax(this.min, value);
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000899C File Offset: 0x00006B9C
		public void SetMinMax(Vector3 min, Vector3 max)
		{
			this.extents = (max - min) * 0.5f;
			this.center = min + this.extents;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000089D4 File Offset: 0x00006BD4
		public void Encapsulate(Vector3 point)
		{
			this.SetMinMax(Vector3.Min(this.min, point), Vector3.Max(this.max, point));
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00008A00 File Offset: 0x00006C00
		public void Encapsulate(Bounds bounds)
		{
			this.Encapsulate(bounds.center - bounds.extents);
			this.Encapsulate(bounds.center + bounds.extents);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00008A40 File Offset: 0x00006C40
		public void Expand(float amount)
		{
			amount *= 0.5f;
			this.extents += new Vector3(amount, amount, amount);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00008A70 File Offset: 0x00006C70
		public void Expand(Vector3 amount)
		{
			this.extents += amount * 0.5f;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00008A90 File Offset: 0x00006C90
		public bool Intersects(Bounds bounds)
		{
			return this.min.x <= bounds.max.x && this.max.x >= bounds.min.x && this.min.y <= bounds.max.y && this.max.y >= bounds.min.y && this.min.z <= bounds.max.z && this.max.z >= bounds.min.z;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00008B74 File Offset: 0x00006D74
		private static bool Internal_Contains(Bounds m, Vector3 point)
		{
			return Bounds.INTERNAL_CALL_Internal_Contains(ref m, ref point);
		}

		// Token: 0x060005FC RID: 1532
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Internal_Contains(ref Bounds m, ref Vector3 point);

		// Token: 0x060005FD RID: 1533 RVA: 0x00008B80 File Offset: 0x00006D80
		public bool Contains(Vector3 point)
		{
			return Bounds.Internal_Contains(this, point);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00008B90 File Offset: 0x00006D90
		private static float Internal_SqrDistance(Bounds m, Vector3 point)
		{
			return Bounds.INTERNAL_CALL_Internal_SqrDistance(ref m, ref point);
		}

		// Token: 0x060005FF RID: 1535
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float INTERNAL_CALL_Internal_SqrDistance(ref Bounds m, ref Vector3 point);

		// Token: 0x06000600 RID: 1536 RVA: 0x00008B9C File Offset: 0x00006D9C
		public float SqrDistance(Vector3 point)
		{
			return Bounds.Internal_SqrDistance(this, point);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00008BAC File Offset: 0x00006DAC
		private static bool Internal_IntersectRay(ref Ray ray, ref Bounds bounds, out float distance)
		{
			return Bounds.INTERNAL_CALL_Internal_IntersectRay(ref ray, ref bounds, out distance);
		}

		// Token: 0x06000602 RID: 1538
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Internal_IntersectRay(ref Ray ray, ref Bounds bounds, out float distance);

		// Token: 0x06000603 RID: 1539 RVA: 0x00008BB8 File Offset: 0x00006DB8
		public bool IntersectRay(Ray ray)
		{
			float num;
			return Bounds.Internal_IntersectRay(ref ray, ref this, out num);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00008BD0 File Offset: 0x00006DD0
		public bool IntersectRay(Ray ray, out float distance)
		{
			return Bounds.Internal_IntersectRay(ref ray, ref this, out distance);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00008BDC File Offset: 0x00006DDC
		private static Vector3 Internal_GetClosestPoint(ref Bounds bounds, ref Vector3 point)
		{
			Vector3 result;
			Bounds.INTERNAL_CALL_Internal_GetClosestPoint(ref bounds, ref point, out result);
			return result;
		}

		// Token: 0x06000606 RID: 1542
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_GetClosestPoint(ref Bounds bounds, ref Vector3 point, out Vector3 value);

		// Token: 0x06000607 RID: 1543 RVA: 0x00008BF4 File Offset: 0x00006DF4
		public Vector3 ClosestPoint(Vector3 point)
		{
			return Bounds.Internal_GetClosestPoint(ref this, ref point);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00008C00 File Offset: 0x00006E00
		public override string ToString()
		{
			return UnityString.Format("Center: {0}, Extents: {1}", new object[]
			{
				this.m_Center,
				this.m_Extents
			});
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00008C3C File Offset: 0x00006E3C
		public string ToString(string format)
		{
			return UnityString.Format("Center: {0}, Extents: {1}", new object[]
			{
				this.m_Center.ToString(format),
				this.m_Extents.ToString(format)
			});
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00008C78 File Offset: 0x00006E78
		public static bool operator ==(Bounds lhs, Bounds rhs)
		{
			return lhs.center == rhs.center && lhs.extents == rhs.extents;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00008CB4 File Offset: 0x00006EB4
		public static bool operator !=(Bounds lhs, Bounds rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040000F8 RID: 248
		private Vector3 m_Center;

		// Token: 0x040000F9 RID: 249
		private Vector3 m_Extents;
	}
}
