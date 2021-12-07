using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200065D RID: 1629
public class LoadingBootstrap : MonoBehaviour
{
	// Token: 0x060027BF RID: 10175 RVA: 0x000ACCF3 File Offset: 0x000AAEF3
	public void Awake()
	{
		LoadingBootstrap.Instance = this;
	}

	// Token: 0x060027C0 RID: 10176 RVA: 0x000ACCFC File Offset: 0x000AAEFC
	public IEnumerator Start()
	{
		for (int i = 0; i < 4; i++)
		{
			yield return new WaitForFixedUpdate();
		}
		Application.backgroundLoadingPriority = ThreadPriority.High;
		UberShaderPrewarmer.Load();
		while (!UberShaderPrewarmer.IsComplete)
		{
			yield return new WaitForEndOfFrame();
		}
		AsyncOperation operation = Application.LoadLevelAdditiveAsync("introLogos");
		operation.allowSceneActivation = false;
		while (operation.progress < 0.9f)
		{
			yield return new WaitForEndOfFrame();
		}
		if (this.Fader)
		{
			this.Fader.Initialize();
			this.Fader.AnimatorDriver.RestartBackwards();
		}
		yield return new WaitForSeconds(this.FadeDuration);
		operation.allowSceneActivation = true;
		yield break;
	}

	// Token: 0x0400225A RID: 8794
	public static LoadingBootstrap Instance;

	// Token: 0x0400225B RID: 8795
	private bool m_faded;

	// Token: 0x0400225C RID: 8796
	public TransparencyAnimator Fader;

	// Token: 0x0400225D RID: 8797
	public float FadeDuration;
}
