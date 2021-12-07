using System;
using System.Collections;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000888 RID: 2184
[Category("World Map")]
public class LegacyDiscoverWorldMapAreasAction : ActionMethod
{
	// Token: 0x0600312A RID: 12586 RVA: 0x000D17AB File Offset: 0x000CF9AB
	public override void Perform(IContext context)
	{
		base.StartCoroutine(this.ShowWorldMap());
	}

	// Token: 0x0600312B RID: 12587 RVA: 0x000D17BC File Offset: 0x000CF9BC
	public IEnumerator ShowWorldMap()
	{
		UI.Menu.ShowAreaMap();
		yield return new WaitForFixedUpdate();
		GameMapUI.Instance.SetRevealingMap();
		RuntimeGameWorldArea currentArea = World.CurrentArea;
		Vector2 playerMapPoition = AreaMapUI.Instance.Navigation.ScrollPosition;
		Vector2 targetMapPosition = currentArea.FindCenterPositionOnUndiscoveredAreas();
		if (this.RevealPosition)
		{
			targetMapPosition = this.RevealPosition.position;
		}
		if (currentArea != null)
		{
			currentArea.DiscoverAllAreas();
			AreaMapCanvas canvas = AreaMapUI.Instance.FindCanvas(currentArea.Area);
			canvas.UpdateAreaMaskTextureB();
			for (float t = 0f; t < 1f; t += Time.deltaTime / this.FadeDelay)
			{
				AreaMapUI.Instance.Navigation.ScrollPosition = playerMapPoition;
				yield return new WaitForFixedUpdate();
			}
			for (float t2 = 0f; t2 < 1f; t2 += Time.deltaTime / this.MoveDuration)
			{
				AreaMapUI.Instance.Navigation.ScrollPosition = Vector3.Lerp(playerMapPoition, targetMapPosition, Mathf.SmoothStep(0f, 1f, t2));
				yield return new WaitForFixedUpdate();
			}
			AreaMapUI.Instance.Navigation.UpdateScrollLimits();
			GameMapUI.Instance.SetNormal();
			AreaMapUI.Instance.IconManager.ShowAreaIcons();
			if (this.RevealSound)
			{
				Sound.Play(this.RevealSound.GetSound(null), Characters.Sein.Position, null);
			}
			for (float t3 = 0f; t3 < this.FadeDuration; t3 += Time.deltaTime)
			{
				canvas.SetFade(t3 / this.FadeDuration);
				yield return new WaitForFixedUpdate();
			}
			while (!Core.Input.Jump.OnPressed)
			{
				yield return new WaitForFixedUpdate();
			}
			UI.Menu.HideMenuScreen(false);
			base.StartCoroutine(this.ReleaseTexture(canvas));
		}
		if (this.OnClosedAction)
		{
			this.OnClosedAction.Perform(null);
		}
		yield break;
	}

	// Token: 0x0600312C RID: 12588 RVA: 0x000D17D8 File Offset: 0x000CF9D8
	public IEnumerator ReleaseTexture(AreaMapCanvas canvas)
	{
		yield return new WaitForSeconds(1f);
		canvas.ReleaseAreaMaskTextureB();
		yield break;
	}

	// Token: 0x04002C7B RID: 11387
	public ActionMethod OnClosedAction;

	// Token: 0x04002C7C RID: 11388
	public float FadeDelay;

	// Token: 0x04002C7D RID: 11389
	public float MoveDuration = 1f;

	// Token: 0x04002C7E RID: 11390
	public float FadeDuration;

	// Token: 0x04002C7F RID: 11391
	public SoundProvider RevealSound;

	// Token: 0x04002C80 RID: 11392
	public Transform RevealPosition;
}
