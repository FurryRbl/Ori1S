using System;
using UnityEngine;

// Token: 0x02000850 RID: 2128
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Sein UI Post Processing")]
public class UberUIPostProcess : MonoBehaviour
{
	// Token: 0x170007BD RID: 1981
	// (get) Token: 0x06003052 RID: 12370 RVA: 0x000CCB31 File Offset: 0x000CAD31
	// (set) Token: 0x06003051 RID: 12369 RVA: 0x000CCB29 File Offset: 0x000CAD29
	public static UberUIPostProcess Instance { get; private set; }

	// Token: 0x06003053 RID: 12371 RVA: 0x000CCB38 File Offset: 0x000CAD38
	private void OnEnable()
	{
		UberUIPostProcess.Instance = this;
		this.Camera = base.GetComponent<Camera>();
	}

	// Token: 0x04002B94 RID: 11156
	[NonSerialized]
	public Camera Camera;
}
