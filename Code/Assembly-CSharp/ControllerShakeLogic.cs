using System;
using Core.Devices;
using UnityEngine;

// Token: 0x02000169 RID: 361
public class ControllerShakeLogic : MonoBehaviour
{
	// Token: 0x06000E63 RID: 3683 RVA: 0x0004244C File Offset: 0x0004064C
	public void Awake()
	{
		ControllerShakeLogic.Instance = this;
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x00042454 File Offset: 0x00040654
	public void OnEnable()
	{
		ControllerShakeLogic.Instance = this;
	}

	// Token: 0x06000E65 RID: 3685 RVA: 0x0004245C File Offset: 0x0004065C
	public void OnDestroy()
	{
		if (ControllerShakeLogic.Instance == this)
		{
			ControllerShakeLogic.Instance = null;
		}
	}

	// Token: 0x06000E66 RID: 3686 RVA: 0x00042474 File Offset: 0x00040674
	public void FixedUpdate()
	{
		this.UpdateShake();
	}

	// Token: 0x06000E67 RID: 3687 RVA: 0x0004247C File Offset: 0x0004067C
	public void UpdateShake()
	{
		float num = 0f;
		for (int i = 0; i < ControllerShake.All.Count; i++)
		{
			ControllerShake controllerShake = ControllerShake.All[i];
			if (!controllerShake.IsSuspended)
			{
				float modifiedStrength = controllerShake.ModifiedStrength;
				num += controllerShake.CurrentShake * modifiedStrength;
			}
		}
		ControllerShakeLogic.Vibrate(num, 0.2f);
	}

	// Token: 0x170002B5 RID: 693
	// (get) Token: 0x06000E68 RID: 3688 RVA: 0x000424E3 File Offset: 0x000406E3
	// (set) Token: 0x06000E69 RID: 3689 RVA: 0x000424EB File Offset: 0x000406EB
	public bool IsSuspended { get; set; }

	// Token: 0x06000E6A RID: 3690 RVA: 0x000424F4 File Offset: 0x000406F4
	public static void Vibrate(float strength, float time)
	{
		if (PlayerInput.Instance.WasKeyboardUsedLast)
		{
			return;
		}
		if (Recorder.IsPlaying)
		{
			return;
		}
		if (GameSettings.Instance.VibrationStrength == 0f)
		{
			return;
		}
		XboxControllerManager.CurrentController.Vibrate(Vector2.one * strength * GameSettings.Instance.VibrationStrength, time);
	}

	// Token: 0x04000B8F RID: 2959
	public static ControllerShakeLogic Instance;
}
