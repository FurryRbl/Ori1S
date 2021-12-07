using System;
using UnityEngine;

namespace Sein.World
{
	// Token: 0x020000BD RID: 189
	public static class Events
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00021F2C File Offset: 0x0002012C
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x00021F34 File Offset: 0x00020134
		public static bool GravityActivated
		{
			get
			{
				return Events.m_gravityActivated;
			}
			set
			{
				Events.m_gravityActivated = value;
				Shader.SetGlobalFloat("_NightberryActivated", (float)((!Events.m_gravityActivated) ? 0 : 1));
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00021F64 File Offset: 0x00020164
		public static WorldProgression Progression
		{
			get
			{
				if (Events.WarmthReturned)
				{
					return WorldProgression.WarmthReturned;
				}
				if (Events.WindRestored)
				{
					return WorldProgression.WindRestored;
				}
				if (Events.MistLifted)
				{
					return WorldProgression.MistLifted;
				}
				if (Events.WaterPurified)
				{
					return WorldProgression.FinishedGinsoTree;
				}
				if (Events.GinsoTreeEntered)
				{
					return WorldProgression.EnteredGinsoTree;
				}
				if (Events.SpiritTreeReached)
				{
					return WorldProgression.SpiritTreeReached;
				}
				if (GameStateMachine.Instance.CurrentState == GameStateMachine.State.Prologue)
				{
					return WorldProgression.Prologue;
				}
				return WorldProgression.StartOfGame;
			}
		}

		// Token: 0x04000657 RID: 1623
		public static bool GinsoTreeEntered;

		// Token: 0x04000658 RID: 1624
		public static bool MistLifted;

		// Token: 0x04000659 RID: 1625
		public static bool WaterPurified;

		// Token: 0x0400065A RID: 1626
		public static bool WindRestored;

		// Token: 0x0400065B RID: 1627
		public static bool GumoFree;

		// Token: 0x0400065C RID: 1628
		public static bool SpiritTreeReached;

		// Token: 0x0400065D RID: 1629
		public static bool WarmthReturned;

		// Token: 0x0400065E RID: 1630
		public static bool DarknessLifted;

		// Token: 0x0400065F RID: 1631
		private static bool m_gravityActivated;
	}
}
