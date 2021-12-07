using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000110 RID: 272
public class CursorController : MonoBehaviour
{
	// Token: 0x06000AB6 RID: 2742 RVA: 0x0002EA8B File Offset: 0x0002CC8B
	public static void ResetIdleTime()
	{
		CursorController.m_idleTime = 0f;
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x0002EA97 File Offset: 0x0002CC97
	public void Awake()
	{
		Cursor.SetCursor(this.CursorTexture, this.Offset, CursorMode.Auto);
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x0002EAAC File Offset: 0x0002CCAC
	private bool ShouldHide()
	{
		return CursorController.m_idleTime > 2f || (!UI.MainMenuVisible && ((GameStateMachine.Instance && GameStateMachine.Instance.CurrentState == GameStateMachine.State.Logos) || (GameStateMachine.Instance && GameStateMachine.Instance.CurrentState == GameStateMachine.State.WatchCutscenes) || (SkipCutsceneController.Instance && SkipCutsceneController.Instance.SkippingAvailable)));
	}

	// Token: 0x06000AB9 RID: 2745 RVA: 0x0002EB38 File Offset: 0x0002CD38
	public void Update()
	{
		if (Vector3.Distance(UnityEngine.Input.mousePosition, this.m_mousePosition) > 1f)
		{
			this.m_mousePosition = UnityEngine.Input.mousePosition;
			this.ShowCursor();
		}
		CursorController.m_idleTime += Time.deltaTime;
		if (this.ShouldHide())
		{
			Cursor.visible = false;
			CursorController.IsVisible = false;
			this.Transparency.AnimatorDriver.ContinueBackwards();
		}
		base.transform.position = Core.Input.CursorPositionUI + Vector3.forward;
	}

	// Token: 0x06000ABA RID: 2746 RVA: 0x0002EBD0 File Offset: 0x0002CDD0
	private void ShowCursor()
	{
		CursorController.m_idleTime = 0f;
		this.Transparency.AnimatorDriver.ContinueForward();
		CursorController.IsVisible = true;
		Cursor.visible = true;
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x0002EC04 File Offset: 0x0002CE04
	public void Start()
	{
		foreach (ISuspendable suspendable in base.gameObject.FindComponentsInChildren<ISuspendable>())
		{
			SuspensionManager.Unregister(suspendable);
		}
	}

	// Token: 0x040008C4 RID: 2244
	public TransparencyAnimator Transparency;

	// Token: 0x040008C5 RID: 2245
	private Vector2 m_mousePosition;

	// Token: 0x040008C6 RID: 2246
	private static float m_idleTime;

	// Token: 0x040008C7 RID: 2247
	public Texture2D CursorTexture;

	// Token: 0x040008C8 RID: 2248
	public Vector2 Offset;

	// Token: 0x040008C9 RID: 2249
	public static bool IsVisible;
}
