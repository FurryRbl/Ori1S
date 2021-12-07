using System;
using UnityEngine;

// Token: 0x020003D2 RID: 978
public class CameraShakeLogic : MonoBehaviour, ISuspendable
{
	// Token: 0x06001ADC RID: 6876 RVA: 0x000735BA File Offset: 0x000717BA
	public void Awake()
	{
		SuspensionManager.Register(this);
	}

	// Token: 0x06001ADD RID: 6877 RVA: 0x000735C2 File Offset: 0x000717C2
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06001ADE RID: 6878 RVA: 0x000735CC File Offset: 0x000717CC
	public void UpdateOffset()
	{
		Vector3 vector = Vector3.zero;
		Vector3 vector2 = Vector3.zero;
		for (int i = 0; i < CameraShake.All.Count; i++)
		{
			CameraShake cameraShake = CameraShake.All[i];
			float modifiedStrength = cameraShake.ModifiedStrength;
			vector += cameraShake.CurrentOffset * modifiedStrength;
			vector2 += cameraShake.CurrentRotation * modifiedStrength;
		}
		this.Target.localPosition = vector;
		this.Target.localEulerAngles = vector2;
	}

	// Token: 0x17000470 RID: 1136
	// (get) Token: 0x06001ADF RID: 6879 RVA: 0x00073654 File Offset: 0x00071854
	// (set) Token: 0x06001AE0 RID: 6880 RVA: 0x0007365C File Offset: 0x0007185C
	public bool IsSuspended { get; set; }

	// Token: 0x0400175C RID: 5980
	public Transform Target;
}
