using System;
using System.Collections.Generic;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200002D RID: 45
	public abstract class BaseRaycaster : UIBehaviour
	{
		// Token: 0x06000123 RID: 291
		public abstract void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList);

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000124 RID: 292
		public abstract Camera eventCamera { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000053A8 File Offset: 0x000035A8
		[Obsolete("Please use sortOrderPriority and renderOrderPriority", false)]
		public virtual int priority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000053AC File Offset: 0x000035AC
		public virtual int sortOrderPriority
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000127 RID: 295 RVA: 0x000053B4 File Offset: 0x000035B4
		public virtual int renderOrderPriority
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000053BC File Offset: 0x000035BC
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Name: ",
				base.gameObject,
				"\neventCamera: ",
				this.eventCamera,
				"\nsortOrderPriority: ",
				this.sortOrderPriority,
				"\nrenderOrderPriority: ",
				this.renderOrderPriority
			});
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005424 File Offset: 0x00003624
		protected override void OnEnable()
		{
			base.OnEnable();
			RaycasterManager.AddRaycaster(this);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005434 File Offset: 0x00003634
		protected override void OnDisable()
		{
			RaycasterManager.RemoveRaycasters(this);
			base.OnDisable();
		}
	}
}
