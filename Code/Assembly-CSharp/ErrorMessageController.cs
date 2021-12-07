using System;
using UnityEngine;

// Token: 0x02000165 RID: 357
public class ErrorMessageController : MonoBehaviour
{
	// Token: 0x06000E4F RID: 3663 RVA: 0x000420A2 File Offset: 0x000402A2
	public void Awake()
	{
		ErrorMessageController.s_instance = this;
	}

	// Token: 0x06000E50 RID: 3664 RVA: 0x000420AA File Offset: 0x000402AA
	public void OnDestroy()
	{
		ErrorMessageController.s_instance = null;
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x000420B4 File Offset: 0x000402B4
	public static void ReportSaveFailed(int errorCode)
	{
		if (ErrorMessageController.s_instance)
		{
			ErrorMessageController.s_instance.Message.SetMessage(new MessageDescriptor(ErrorMessageController.s_instance.SaveError + ":" + errorCode));
			ErrorMessageController.s_instance.AppearAnimator.AnimatorDriver.Restart();
		}
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x00042114 File Offset: 0x00040314
	public static void ReportFailedToLoad()
	{
		if (ErrorMessageController.s_instance)
		{
			ErrorMessageController.s_instance.Message.SetMessage(new MessageDescriptor(ErrorMessageController.s_instance.CorruptSavesError.ToString()));
			ErrorMessageController.s_instance.AppearAnimator.AnimatorDriver.Restart();
		}
	}

	// Token: 0x04000B7F RID: 2943
	private static ErrorMessageController s_instance;

	// Token: 0x04000B80 RID: 2944
	public MessageBox Message;

	// Token: 0x04000B81 RID: 2945
	public MessageProvider SaveError;

	// Token: 0x04000B82 RID: 2946
	public MessageProvider CorruptSavesError;

	// Token: 0x04000B83 RID: 2947
	public BaseAnimator AppearAnimator;
}
