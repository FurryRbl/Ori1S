using System;

namespace UnityEngine
{
	// Token: 0x02000066 RID: 102
	public struct Plane
	{
		// Token: 0x06000646 RID: 1606 RVA: 0x00009770 File Offset: 0x00007970
		public Plane(Vector3 inNormal, Vector3 inPoint)
		{
			this.m_Normal = Vector3.Normalize(inNormal);
			this.m_Distance = -Vector3.Dot(inNormal, inPoint);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0000978C File Offset: 0x0000798C
		public Plane(Vector3 inNormal, float d)
		{
			this.m_Normal = Vector3.Normalize(inNormal);
			this.m_Distance = d;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000097A4 File Offset: 0x000079A4
		public Plane(Vector3 a, Vector3 b, Vector3 c)
		{
			this.m_Normal = Vector3.Normalize(Vector3.Cross(b - a, c - a));
			this.m_Distance = -Vector3.Dot(this.m_Normal, a);
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x000097D8 File Offset: 0x000079D8
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x000097E0 File Offset: 0x000079E0
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

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x000097EC File Offset: 0x000079EC
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x000097F4 File Offset: 0x000079F4
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

		// Token: 0x0600064D RID: 1613 RVA: 0x00009800 File Offset: 0x00007A00
		public void SetNormalAndPosition(Vector3 inNormal, Vector3 inPoint)
		{
			this.normal = Vector3.Normalize(inNormal);
			this.distance = -Vector3.Dot(inNormal, inPoint);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0000981C File Offset: 0x00007A1C
		public void Set3Points(Vector3 a, Vector3 b, Vector3 c)
		{
			this.normal = Vector3.Normalize(Vector3.Cross(b - a, c - a));
			this.distance = -Vector3.Dot(this.normal, a);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0000985C File Offset: 0x00007A5C
		public float GetDistanceToPoint(Vector3 inPt)
		{
			return Vector3.Dot(this.normal, inPt) + this.distance;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00009874 File Offset: 0x00007A74
		public bool GetSide(Vector3 inPt)
		{
			return Vector3.Dot(this.normal, inPt) + this.distance > 0f;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00009890 File Offset: 0x00007A90
		public bool SameSide(Vector3 inPt0, Vector3 inPt1)
		{
			float distanceToPoint = this.GetDistanceToPoint(inPt0);
			float distanceToPoint2 = this.GetDistanceToPoint(inPt1);
			return (distanceToPoint > 0f && distanceToPoint2 > 0f) || (distanceToPoint <= 0f && distanceToPoint2 <= 0f);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x000098E0 File Offset: 0x00007AE0
		public bool Raycast(Ray ray, out float enter)
		{
			float num = Vector3.Dot(ray.direction, this.normal);
			float num2 = -Vector3.Dot(ray.origin, this.normal) - this.distance;
			if (Mathf.Approximately(num, 0f))
			{
				enter = 0f;
				return false;
			}
			enter = num2 / num;
			return enter > 0f;
		}

		// Token: 0x04000103 RID: 259
		private Vector3 m_Normal;

		// Token: 0x04000104 RID: 260
		private float m_Distance;
	}
}
