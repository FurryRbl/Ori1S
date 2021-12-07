using System;

namespace UnityEngine.UI
{
	// Token: 0x02000090 RID: 144
	[AddComponentMenu("Layout/Horizontal Layout Group", 150)]
	public class HorizontalLayoutGroup : HorizontalOrVerticalLayoutGroup
	{
		// Token: 0x06000506 RID: 1286 RVA: 0x00016FC8 File Offset: 0x000151C8
		protected HorizontalLayoutGroup()
		{
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00016FD0 File Offset: 0x000151D0
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			base.CalcAlongAxis(0, false);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00016FE0 File Offset: 0x000151E0
		public override void CalculateLayoutInputVertical()
		{
			base.CalcAlongAxis(1, false);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00016FEC File Offset: 0x000151EC
		public override void SetLayoutHorizontal()
		{
			base.SetChildrenAlongAxis(0, false);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00016FF8 File Offset: 0x000151F8
		public override void SetLayoutVertical()
		{
			base.SetChildrenAlongAxis(1, false);
		}
	}
}
