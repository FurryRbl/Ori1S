using System;
using UnityEngine;

// Token: 0x02000164 RID: 356
public class WaitForSaveGameLogic : MonoBehaviour
{
	// Token: 0x06000E4C RID: 3660 RVA: 0x0004203C File Offset: 0x0004023C
	public void FixedUpdate()
	{
		if (GameController.Instance.SaveGameController.SaveGameQueried)
		{
			try
			{
				SaveSlotsManager.PrepareSlots();
			}
			catch (Exception ex)
			{
				ErrorMessageController.ReportFailedToLoad();
			}
			this.OnCompleted.Perform(null);
			WaitForSaveGameLogic.OnCompletedStatic();
		}
	}

	// Token: 0x04000B7C RID: 2940
	public ActionMethod OnCompleted;

	// Token: 0x04000B7D RID: 2941
	public static Action OnCompletedStatic = delegate()
	{
	};
}
