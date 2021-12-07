using System;
using UnityEngine;

// Token: 0x0200086F RID: 2159
public class PlayMovieTextureActionC : PerformingAction
{
	// Token: 0x060030C9 RID: 12489 RVA: 0x000CF5F7 File Offset: 0x000CD7F7
	public override void OnDestroy()
	{
		this.Stop();
		base.OnDestroy();
	}

	// Token: 0x060030CA RID: 12490 RVA: 0x000CF605 File Offset: 0x000CD805
	public override void Stop()
	{
		if (this.m_movieTextureController)
		{
			this.m_movieTextureController.Stop();
		}
	}

	// Token: 0x170007C7 RID: 1991
	// (get) Token: 0x060030CB RID: 12491 RVA: 0x000CF624 File Offset: 0x000CD824
	public override bool IsPerforming
	{
		get
		{
			return this.m_movieTextureController && !this.m_movieTextureController.IsFinished();
		}
	}

	// Token: 0x060030CC RID: 12492 RVA: 0x000CF654 File Offset: 0x000CD854
	public override void Perform(IContext context)
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.MovieTextureController);
		this.m_movieTextureController = gameObject.GetComponent<MovieTextureControllerB>();
		this.m_movieTextureController.VideoDescriptor = this.VideoDescriptor;
		this.m_movieTextureController.CanBePaused = this.CanBePaused;
		this.m_movieTextureController.CanBeSkipped = this.CanBeSkipped;
		this.m_movieTextureController.SkippedWithButtonPress = this.SkippedWithButtonPress;
		if (this.OnFinishedAction)
		{
			this.m_movieTextureController.OnFinishedAction = this.OnFinishedAction;
		}
		this.m_movieTextureController.Play();
	}

	// Token: 0x04002C0E RID: 11278
	public VideoDescriptor VideoDescriptor;

	// Token: 0x04002C0F RID: 11279
	public GameObject MovieTextureController;

	// Token: 0x04002C10 RID: 11280
	public ActionMethod OnFinishedAction;

	// Token: 0x04002C11 RID: 11281
	public bool CanBePaused = true;

	// Token: 0x04002C12 RID: 11282
	public bool CanBeSkipped = true;

	// Token: 0x04002C13 RID: 11283
	public bool SkippedWithButtonPress;

	// Token: 0x04002C14 RID: 11284
	private MovieTextureControllerB m_movieTextureController;
}
