using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000125 RID: 293
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class Collision
	{
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00014678 File Offset: 0x00012878
		public Vector3 relativeVelocity
		{
			get
			{
				return this.m_RelativeVelocity;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x00014680 File Offset: 0x00012880
		public Rigidbody rigidbody
		{
			get
			{
				return this.m_Rigidbody;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x00014688 File Offset: 0x00012888
		public Collider collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x00014690 File Offset: 0x00012890
		public Transform transform
		{
			get
			{
				return (!(this.rigidbody != null)) ? this.collider.transform : this.rigidbody.transform;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x000146CC File Offset: 0x000128CC
		public GameObject gameObject
		{
			get
			{
				return (!(this.m_Rigidbody != null)) ? this.m_Collider.gameObject : this.m_Rigidbody.gameObject;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x00014708 File Offset: 0x00012908
		public ContactPoint[] contacts
		{
			get
			{
				return this.m_Contacts;
			}
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00014710 File Offset: 0x00012910
		public virtual IEnumerator GetEnumerator()
		{
			return this.contacts.GetEnumerator();
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x00014720 File Offset: 0x00012920
		public Vector3 impulse
		{
			get
			{
				return this.m_Impulse;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x00014728 File Offset: 0x00012928
		[Obsolete("Use Collision.relativeVelocity instead.", false)]
		public Vector3 impactForceSum
		{
			get
			{
				return this.relativeVelocity;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x00014730 File Offset: 0x00012930
		[Obsolete("Will always return zero.", false)]
		public Vector3 frictionForceSum
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00014738 File Offset: 0x00012938
		[Obsolete("Please use Collision.rigidbody, Collision.transform or Collision.collider instead", false)]
		public Component other
		{
			get
			{
				return (!(this.m_Rigidbody != null)) ? this.m_Collider : this.m_Rigidbody;
			}
		}

		// Token: 0x04000378 RID: 888
		internal Vector3 m_Impulse;

		// Token: 0x04000379 RID: 889
		internal Vector3 m_RelativeVelocity;

		// Token: 0x0400037A RID: 890
		internal Rigidbody m_Rigidbody;

		// Token: 0x0400037B RID: 891
		internal Collider m_Collider;

		// Token: 0x0400037C RID: 892
		internal ContactPoint[] m_Contacts;
	}
}
