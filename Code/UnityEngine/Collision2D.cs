using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000151 RID: 337
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class Collision2D
	{
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x00017C38 File Offset: 0x00015E38
		public bool enabled
		{
			get
			{
				return this.m_Enabled;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x00017C40 File Offset: 0x00015E40
		public Rigidbody2D rigidbody
		{
			get
			{
				return this.m_Rigidbody;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x00017C48 File Offset: 0x00015E48
		public Collider2D collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x00017C50 File Offset: 0x00015E50
		public Transform transform
		{
			get
			{
				return (!(this.rigidbody != null)) ? this.collider.transform : this.rigidbody.transform;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x00017C8C File Offset: 0x00015E8C
		public GameObject gameObject
		{
			get
			{
				return (!(this.m_Rigidbody != null)) ? this.m_Collider.gameObject : this.m_Rigidbody.gameObject;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x00017CC8 File Offset: 0x00015EC8
		public ContactPoint2D[] contacts
		{
			get
			{
				return this.m_Contacts;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x00017CD0 File Offset: 0x00015ED0
		public Vector2 relativeVelocity
		{
			get
			{
				return this.m_RelativeVelocity;
			}
		}

		// Token: 0x040003DB RID: 987
		internal Rigidbody2D m_Rigidbody;

		// Token: 0x040003DC RID: 988
		internal Collider2D m_Collider;

		// Token: 0x040003DD RID: 989
		internal ContactPoint2D[] m_Contacts;

		// Token: 0x040003DE RID: 990
		internal Vector2 m_RelativeVelocity;

		// Token: 0x040003DF RID: 991
		internal bool m_Enabled;
	}
}
