using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000942 RID: 2370
public class PlayRandomSound : MonoBehaviour
{
	// Token: 0x06003451 RID: 13393 RVA: 0x000DBD56 File Offset: 0x000D9F56
	private void Start()
	{
		this.DoIt();
	}

	// Token: 0x06003452 RID: 13394 RVA: 0x000DBD60 File Offset: 0x000D9F60
	private void DoIt()
	{
		if (this.Sounds.Length > 0)
		{
			if (this.PlayOnlyIfVisibleToCamera && !UI.Cameras.Current.CameraBoundingBox.Intersects(new Bounds(base.transform.position, Vector3.one * this.SoundObjectSize)))
			{
				return;
			}
			int num = (int)Mathf.Lerp(0f, (float)this.Sounds.Length, FixedRandom.Values[4]);
			Sound.Play(this.Sounds[num], base.transform.position, null, this.VolumeScale, null);
		}
	}

	// Token: 0x04002F38 RID: 12088
	public AudioClip[] Sounds;

	// Token: 0x04002F39 RID: 12089
	public float VolumeScale = 1f;

	// Token: 0x04002F3A RID: 12090
	public bool PlayOnlyIfVisibleToCamera;

	// Token: 0x04002F3B RID: 12091
	public float SoundObjectSize = 1f;
}
