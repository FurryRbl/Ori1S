using System;

namespace UnityEngine.UI
{
	// Token: 0x0200009B RID: 155
	[AddComponentMenu("Layout/Vertical Layout Group", 151)]
	public class VerticalLayoutGroup : HorizontalOrVerticalLayoutGroup
	{
		// Token: 0x06000586 RID: 1414 RVA: 0x000182AC File Offset: 0x000164AC
		protected VerticalLayoutGroup()
		{
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x000182B4 File Offset: 0x000164B4
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			base.CalcAlongAxis(0, true);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x000182C4 File Offset: 0x000164C4
		public override void CalculateLayoutInputVertical()
		{
			base.CalcAlongAxis(1, true);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000182D0 File Offset: 0x000164D0
		public override void SetLayoutHorizontal()
		{
			base.SetChildrenAlongAxis(0, true);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x000182DC File Offset: 0x000164DC
		public override void SetLayoutVertical()
		{
			base.SetChildrenAlongAxis(1, true);
		}
	}
}
