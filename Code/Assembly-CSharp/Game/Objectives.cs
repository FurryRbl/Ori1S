using System;

namespace Game
{
	// Token: 0x02000879 RID: 2169
	public static class Objectives
	{
		// Token: 0x060030FC RID: 12540 RVA: 0x000D0BF8 File Offset: 0x000CEDF8
		public static bool ObjectiveExists(Objective objective)
		{
			for (int i = 0; i < Objectives.All.Count; i++)
			{
				Objective x = Objectives.All[i];
				if (x == objective)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x060030FD RID: 12541 RVA: 0x000D0C3B File Offset: 0x000CEE3B
		public static AllContainer<Objective> All
		{
			get
			{
				return GameController.Instance.ActiveObjectives;
			}
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x000D0C48 File Offset: 0x000CEE48
		public static void Serialize(Archive ar)
		{
			if (ar.Reading)
			{
				Objectives.All.Clear();
				int num = ar.Serialize(1);
				for (int i = 0; i < num; i++)
				{
					Objective objectiveFromIndex = GameController.Instance.GetObjectiveFromIndex(ar.Serialize(0));
					if (objectiveFromIndex)
					{
						Objectives.All.Add(objectiveFromIndex);
					}
				}
			}
			else
			{
				ar.Serialize(Objectives.All.Count);
				for (int j = 0; j < Objectives.All.Count; j++)
				{
					Objective objective = Objectives.All[j];
					int objectiveIndex = GameController.Instance.GetObjectiveIndex(objective);
					if (objectiveIndex == -1)
					{
					}
					ar.Serialize(objectiveIndex);
				}
			}
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x000D0D0C File Offset: 0x000CEF0C
		public static void AddObjective(Objective objective)
		{
			if (!Objectives.All.Contains(objective))
			{
				Objectives.All.Add(objective);
			}
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x000D0D39 File Offset: 0x000CEF39
		public static void CompleteObjective(Objective objective)
		{
			objective.Complete();
			Objectives.All.Remove(objective);
		}
	}
}
