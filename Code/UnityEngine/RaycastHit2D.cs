using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000143 RID: 323
	[UsedByNativeCode]
	public struct RaycastHit2D
	{
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x000177B0 File Offset: 0x000159B0
		// (set) Token: 0x06001599 RID: 5529 RVA: 0x000177B8 File Offset: 0x000159B8
		public Vector2 centroid
		{
			get
			{
				return this.m_Centroid;
			}
			set
			{
				this.m_Centroid = value;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x000177C4 File Offset: 0x000159C4
		// (set) Token: 0x0600159B RID: 5531 RVA: 0x000177CC File Offset: 0x000159CC
		public Vector2 point
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

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x000177D8 File Offset: 0x000159D8
		// (set) Token: 0x0600159D RID: 5533 RVA: 0x000177E0 File Offset: 0x000159E0
		public Vector2 normal
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

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x000177EC File Offset: 0x000159EC
		// (set) Token: 0x0600159F RID: 5535 RVA: 0x000177F4 File Offset: 0x000159F4
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

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x00017800 File Offset: 0x00015A00
		// (set) Token: 0x060015A1 RID: 5537 RVA: 0x00017808 File Offset: 0x00015A08
		public float fraction
		{
			get
			{
				return this.m_Fraction;
			}
			set
			{
				this.m_Fraction = value;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x00017814 File Offset: 0x00015A14
		public Collider2D collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x0001781C File Offset: 0x00015A1C
		public Rigidbody2D rigidbody
		{
			get
			{
				return (!(this.collider != null)) ? null : this.collider.attachedRigidbody;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x0001784C File Offset: 0x00015A4C
		public Transform transform
		{
			get
			{
				Rigidbody2D rigidbody = this.rigidbody;
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

		// Token: 0x060015A5 RID: 5541 RVA: 0x00017894 File Offset: 0x00015A94
		public int CompareTo(RaycastHit2D other)
		{
			if (this.collider == null)
			{
				return 1;
			}
			if (other.collider == null)
			{
				return -1;
			}
			return this.fraction.CompareTo(other.fraction);
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x000178E0 File Offset: 0x00015AE0
		public static implicit operator bool(RaycastHit2D hit)
		{
			return hit.collider != null;
		}

		// Token: 0x040003B7 RID: 951
		private Vector2 m_Centroid;

		// Token: 0x040003B8 RID: 952
		private Vector2 m_Point;

		// Token: 0x040003B9 RID: 953
		private Vector2 m_Normal;

		// Token: 0x040003BA RID: 954
		private float m_Distance;

		// Token: 0x040003BB RID: 955
		private float m_Fraction;

		// Token: 0x040003BC RID: 956
		private Collider2D m_Collider;
	}
}
