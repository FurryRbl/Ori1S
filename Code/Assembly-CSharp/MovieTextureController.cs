using System;
using Game;
using UnityEngine;

// Token: 0x0200086B RID: 2155
public class MovieTextureController : MonoBehaviour
{
	// Token: 0x060030A5 RID: 12453 RVA: 0x000CEC10 File Offset: 0x000CCE10
	public void StartMovieSequence()
	{
		if (this.Fader)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Fader);
			this.m_fader = gameObject.GetComponent<Fader>();
			Fader fader = this.m_fader;
			fader.OnFadeInEvent = (Action)Delegate.Combine(fader.OnFadeInEvent, new Action(this.OnFadeInEvent));
			Fader fader2 = this.m_fader;
			fader2.OnFadeOutEvent = (Action)Delegate.Combine(fader2.OnFadeOutEvent, new Action(this.OnFadeOutEvent));
		}
		else
		{
			this.OnFadeInEvent();
			this.OnFadeOutEvent();
		}
	}

	// Token: 0x060030A6 RID: 12454 RVA: 0x000CECA4 File Offset: 0x000CCEA4
	private void FixedUpdate()
	{
		if (this.m_movieTexture && !this.m_movieTexture.isPlaying && this.m_fader == null)
		{
			if (this.Fader)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Fader);
				this.m_fader = gameObject.GetComponent<Fader>();
				Fader fader = this.m_fader;
				fader.OnFadeInEvent = (Action)Delegate.Combine(fader.OnFadeInEvent, new Action(this.OnFadeInAfterMovieEvent));
				Fader fader2 = this.m_fader;
				fader2.OnFadeOutEvent = (Action)Delegate.Combine(fader2.OnFadeOutEvent, new Action(this.OnFadeOutAfterMovieEvent));
			}
			else
			{
				this.OnFadeInAfterMovieEvent();
				this.OnFadeOutAfterMovieEvent();
			}
		}
	}

	// Token: 0x060030A7 RID: 12455 RVA: 0x000CED6C File Offset: 0x000CCF6C
	private void OnFadeInEvent()
	{
		if (this.m_fader)
		{
			Fader fader = this.m_fader;
			fader.OnFadeInEvent = (Action)Delegate.Remove(fader.OnFadeInEvent, new Action(this.OnFadeInEvent));
		}
		UI.Cameras.Current.MoveToTarget(this.MovieTextureGameObject.transform.position, 0.01f, false);
		UI.Cameras.Current.OffsetController.AdditiveDefaultOffset = this.MovieCameraOffset;
		UnityEngine.Object.DestroyObject(UI.Cameras.Current.GameObject.GetComponentInChildren<SinMovement>());
	}

	// Token: 0x060030A8 RID: 12456 RVA: 0x000CEDFC File Offset: 0x000CCFFC
	private void OnFadeOutEvent()
	{
		if (this.m_fader)
		{
			Fader fader = this.m_fader;
			fader.OnFadeOutEvent = (Action)Delegate.Remove(fader.OnFadeOutEvent, new Action(this.OnFadeOutEvent));
			this.m_fader = null;
		}
		this.m_movieTexture = (this.MovieTextureGameObject.GetComponent<Renderer>().material.mainTexture as MovieTexture);
		this.m_movieTexture.Play();
	}

	// Token: 0x060030A9 RID: 12457 RVA: 0x000CEE74 File Offset: 0x000CD074
	private void OnFadeInAfterMovieEvent()
	{
		if (this.m_fader)
		{
			Fader fader = this.m_fader;
			fader.OnFadeInEvent = (Action)Delegate.Remove(fader.OnFadeInEvent, new Action(this.OnFadeInAfterMovieEvent));
		}
		Application.LoadLevel(this.LevelToLoad);
	}

	// Token: 0x060030AA RID: 12458 RVA: 0x000CEEC3 File Offset: 0x000CD0C3
	private void OnFadeOutAfterMovieEvent()
	{
		if (this.m_fader)
		{
			Fader fader = this.m_fader;
			fader.OnFadeOutEvent = (Action)Delegate.Remove(fader.OnFadeOutEvent, new Action(this.OnFadeOutAfterMovieEvent));
		}
	}

	// Token: 0x04002BEB RID: 11243
	public GameObject Fader;

	// Token: 0x04002BEC RID: 11244
	private Fader m_fader;

	// Token: 0x04002BED RID: 11245
	private MovieTexture m_movieTexture;

	// Token: 0x04002BEE RID: 11246
	public GameObject MovieTextureGameObject;

	// Token: 0x04002BEF RID: 11247
	public Vector3 MovieCameraOffset = new Vector3(0f, 0f, 35f);

	// Token: 0x04002BF0 RID: 11248
	public string LevelToLoad;
}
