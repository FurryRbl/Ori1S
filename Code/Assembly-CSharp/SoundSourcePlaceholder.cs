using System;
using UnityEngine;

// Token: 0x020001DB RID: 475
public class SoundSourcePlaceholder : MonoBehaviour
{
	// Token: 0x060010D4 RID: 4308 RVA: 0x0004CE68 File Offset: 0x0004B068
	private void Awake()
	{
		bool flag = false;
		foreach (UnityEngine.Object @object in UnityEngine.Object.FindObjectsOfType(typeof(SoundSource)))
		{
			if ((@object as SoundSource).Sound == this.SoundSource.Sound)
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			UnityEngine.Object.Instantiate(this.SoundSource, base.transform.position, base.transform.rotation);
		}
		UnityEngine.Object.DestroyImmediate(base.gameObject);
	}

	// Token: 0x04000E7F RID: 3711
	public SoundSource SoundSource;
}
