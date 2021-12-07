using System;

// Token: 0x02000247 RID: 583
public class ReportLocationAction : ActionMethod
{
	// Token: 0x060013D3 RID: 5075 RVA: 0x0005ADE4 File Offset: 0x00058FE4
	public override void Perform(IContext context)
	{
		switch (this.Location)
		{
		case ReportLocationAction.GameLocations.Act1End:
			ReportLocationAction.OnAct1End();
			break;
		case ReportLocationAction.GameLocations.Act2End:
			ReportLocationAction.OnAct2End();
			break;
		case ReportLocationAction.GameLocations.Act3End:
			ReportLocationAction.OnAct3End();
			break;
		}
	}

	// Token: 0x04001171 RID: 4465
	public ReportLocationAction.GameLocations Location;

	// Token: 0x04001172 RID: 4466
	public static Action OnAct1End = delegate()
	{
	};

	// Token: 0x04001173 RID: 4467
	public static Action OnAct2End = delegate()
	{
	};

	// Token: 0x04001174 RID: 4468
	public static Action OnAct3End = delegate()
	{
	};

	// Token: 0x0200031F RID: 799
	public enum GameLocations
	{
		// Token: 0x0400141B RID: 5147
		Act1End,
		// Token: 0x0400141C RID: 5148
		Act2End,
		// Token: 0x0400141D RID: 5149
		Act3End
	}
}
