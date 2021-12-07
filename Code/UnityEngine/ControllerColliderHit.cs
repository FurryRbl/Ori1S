using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000123 RID: 291
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class ControllerColliderHit
	{
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x000145E8 File Offset: 0x000127E8
		public CharacterController controller
		{
			get
			{
				return this.m_Controller;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x000145F0 File Offset: 0x000127F0
		public Collider collider
		{
			get
			{
				return this.m_Collider;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x000145F8 File Offset: 0x000127F8
		public Rigidbody rigidbody
		{
			get
			{
				return this.m_Collider.attachedRigidbody;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x00014608 File Offset: 0x00012808
		public GameObject gameObject
		{
			get
			{
				return this.m_Collider.gameObject;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x00014618 File Offset: 0x00012818
		public Transform transform
		{
			get
			{
				return this.m_Collider.transform;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x00014628 File Offset: 0x00012828
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x00014630 File Offset: 0x00012830
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x00014638 File Offset: 0x00012838
		public Vector3 moveDirection
		{
			get
			{
				return this.m_MoveDirection;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x00014640 File Offset: 0x00012840
		public float moveLength
		{
			get
			{
				return this.m_MoveLength;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x00014648 File Offset: 0x00012848
		// (set) Token: 0x060011EB RID: 4587 RVA: 0x00014658 File Offset: 0x00012858
		private bool push
		{
			get
			{
				return this.m_Push != 0;
			}
			set
			{
				this.m_Push = ((!value) ? 0 : 1);
			}
		}

		// Token: 0x0400036C RID: 876
		internal CharacterController m_Controller;

		// Token: 0x0400036D RID: 877
		internal Collider m_Collider;

		// Token: 0x0400036E RID: 878
		internal Vector3 m_Point;

		// Token: 0x0400036F RID: 879
		internal Vector3 m_Normal;

		// Token: 0x04000370 RID: 880
		internal Vector3 m_MoveDirection;

		// Token: 0x04000371 RID: 881
		internal float m_MoveLength;

		// Token: 0x04000372 RID: 882
		internal int m_Push;
	}
}
