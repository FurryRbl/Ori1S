using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core;
using Game;
using UnityEngine;
using UWPCompat;

// Token: 0x020009AC RID: 2476
public class ArtBuildController : MonoBehaviour
{
	// Token: 0x060035E9 RID: 13801 RVA: 0x000E24F4 File Offset: 0x000E06F4
	private void Awake()
	{
		base.StartCoroutine(this.LoadScreenshots());
		UnityEngine.Object.DontDestroyOnLoad(this);
		if (this.PauseDimmer)
		{
			if (this.m_dimmerAnimators == null)
			{
				this.m_dimmerAnimators = this.PauseDimmer.GetComponentsInChildren<LegacyAnimator>();
			}
			this.DisableDimmer();
		}
	}

	// Token: 0x060035EA RID: 13802 RVA: 0x000E2548 File Offset: 0x000E0748
	public IEnumerator LoadScreenshots()
	{
		string path = Path.Combine(UWPCompat.Environment.GetCommandLineArgs()[0].Replace(".exe", "_Data"), "sceneScreenshots");
		foreach (string sceneScreenshotPath in from f in Directory.GetFiles(path)
		where f.EndsWith(".png")
		select f)
		{
			WWW www = new WWW("file://" + sceneScreenshotPath);
			yield return www;
			this.SceneScreenshots.Add(www.texture);
			this.SceneScreenshots[this.SceneScreenshots.Count - 1].name = sceneScreenshotPath.Replace(path, string.Empty).Replace("\\", string.Empty);
		}
		this.m_loadingFinished = true;
		yield break;
	}

	// Token: 0x060035EB RID: 13803 RVA: 0x000E2564 File Offset: 0x000E0764
	private void FixedUpdate()
	{
		if (this.m_loadingFinished)
		{
			Application.LoadLevel(Application.loadedLevel + 1);
			this.m_loadingFinished = false;
		}
		if (Core.Input.Select.OnPressed)
		{
			if (this.m_menuVisible)
			{
				if (Characters.Sein)
				{
					GameController.Instance.LockInput = false;
					this.DisableDimmer();
				}
			}
			else if (Characters.Sein)
			{
				GameController.Instance.LockInput = true;
				this.EnableDimmer();
			}
			this.m_menuVisible = !this.m_menuVisible;
			Core.Input.Select.Used = true;
		}
		if (this.m_menuVisible)
		{
			if (Core.Input.Jump.OnPressed || Core.Input.SpiritFlame.OnPressed)
			{
				this.LoadSceneAtIndex(this.CurrentScene);
				this.DisableDimmer();
				this.m_menuVisible = false;
				Letterbox.ShowLetterboxes = false;
			}
			if (Core.Input.Left.OnPressed)
			{
				this.CurrentScene--;
			}
			if (Core.Input.Right.OnPressed)
			{
				this.CurrentScene++;
			}
			if (this.CurrentScene == -1)
			{
				this.CurrentScene = this.SceneScreenshots.Count - 1;
			}
			if (this.CurrentScene == this.SceneScreenshots.Count)
			{
				this.CurrentScene = 0;
			}
		}
	}

	// Token: 0x060035EC RID: 13804 RVA: 0x000E26C8 File Offset: 0x000E08C8
	private void LoadSceneAtIndex(int index)
	{
		string name = this.SceneScreenshots[index].name.Replace("sceneScreenshots\\", string.Empty).Replace(".png", string.Empty);
		Application.LoadLevel(name);
	}

	// Token: 0x060035ED RID: 13805 RVA: 0x000E270C File Offset: 0x000E090C
	private void EnableDimmer()
	{
		if (this.PauseDimmer)
		{
			foreach (LegacyAnimator legacyAnimator in this.m_dimmerAnimators)
			{
				legacyAnimator.ContinueForward();
			}
		}
	}

	// Token: 0x060035EE RID: 13806 RVA: 0x000E2750 File Offset: 0x000E0950
	private void DisableDimmer()
	{
		if (this.PauseDimmer)
		{
			foreach (LegacyAnimator legacyAnimator in this.m_dimmerAnimators)
			{
				legacyAnimator.ContinueBackward();
			}
		}
	}

	// Token: 0x04003082 RID: 12418
	public List<Texture2D> SceneScreenshots = new List<Texture2D>();

	// Token: 0x04003083 RID: 12419
	public int CurrentScene;

	// Token: 0x04003084 RID: 12420
	public GameObject PauseDimmer;

	// Token: 0x04003085 RID: 12421
	private LegacyAnimator[] m_dimmerAnimators;

	// Token: 0x04003086 RID: 12422
	public int MaxScreenshotWidth = 512;

	// Token: 0x04003087 RID: 12423
	public int MaxScreenshotHeight = 512;

	// Token: 0x04003088 RID: 12424
	private bool m_menuVisible;

	// Token: 0x04003089 RID: 12425
	private bool m_loadingFinished;
}
