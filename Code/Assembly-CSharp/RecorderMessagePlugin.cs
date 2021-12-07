using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x0200017A RID: 378
public class RecorderMessagePlugin : MonoBehaviour, IRecorderPlugin
{
	// Token: 0x06000EE1 RID: 3809 RVA: 0x000445F0 File Offset: 0x000427F0
	public void Awake()
	{
		Recorder.Instance.RegisterPlugin(this);
		if (Recorder.Instance.State == Recorder.RecorderState.Playing)
		{
			this.m_recorderMessageData = RecorderMessagePlugin.ExtractRecorderMessagesData(Recorder.Instance.RecorderData);
		}
		this.m_recorderMessageInuptUIPrefab = (GameObject)Resources.Load("recorderMessageInputUI", typeof(GameObject));
	}

	// Token: 0x06000EE2 RID: 3810 RVA: 0x0004464C File Offset: 0x0004284C
	public void Update()
	{
		if (Recorder.Instance.State == Recorder.RecorderState.Playing && MoonInput.GetKeyUp(KeyCode.T))
		{
			this.ToggleShowMessages();
		}
		if (Recorder.Instance.State == Recorder.RecorderState.Recording && MoonInput.GetKeyUp(KeyCode.T))
		{
			this.m_shouldOpenMessageInputUI = true;
		}
	}

	// Token: 0x06000EE3 RID: 3811 RVA: 0x0004469D File Offset: 0x0004289D
	public void ToggleShowMessages()
	{
		this.ShowMessages = !this.ShowMessages;
	}

	// Token: 0x170002C1 RID: 705
	// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x000446AE File Offset: 0x000428AE
	// (set) Token: 0x06000EE5 RID: 3813 RVA: 0x000446B6 File Offset: 0x000428B6
	public bool ShowMessages
	{
		get
		{
			return this.m_showMessages;
		}
		set
		{
			this.m_showMessages = value;
			if (this.m_showMessages)
			{
				RecorderPlaybackUI.Instance.Stop();
			}
			else
			{
				RecorderPlaybackUI.Instance.Play();
			}
		}
	}

	// Token: 0x06000EE6 RID: 3814 RVA: 0x000446E3 File Offset: 0x000428E3
	public void PlayCycle(int frame)
	{
	}

	// Token: 0x06000EE7 RID: 3815 RVA: 0x000446E8 File Offset: 0x000428E8
	public void RecordCycle(int frame)
	{
		if (this.m_recorderMessageInputUI == null && this.m_shouldOpenMessageInputUI)
		{
			this.m_recorderMessageInputUI = UnityEngine.Object.Instantiate<GameObject>(this.m_recorderMessageInuptUIPrefab).GetComponent<RecorderMessageInputUI>();
			RecorderMessageInputUI recorderMessageInputUI = this.m_recorderMessageInputUI;
			recorderMessageInputUI.OnExit = (Action)Delegate.Combine(recorderMessageInputUI.OnExit, new Action(this.OnExit));
			this.m_shouldOpenMessageInputUI = false;
		}
	}

	// Token: 0x06000EE8 RID: 3816 RVA: 0x00044755 File Offset: 0x00042955
	public void Exit()
	{
		UnityEngine.Object.DestroyObject(this);
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x00044760 File Offset: 0x00042960
	public void OnExit()
	{
		if (this.m_recorderMessageInputUI.ExitReason == RecorderMessageInputUI.ExitType.OK)
		{
			RecorderMessageData.Record(Recorder.Instance.RecorderStream, this.m_recorderMessageInputUI.Text);
			string text = this.m_recorderMessageInputUI.Text;
			if (text.IndexOf("\n") != -1)
			{
				text = text.Substring(0, text.IndexOf("\n"));
			}
			text = text.Replace("?", string.Empty).Replace("!", string.Empty).Replace("+", string.Empty).Replace("-", string.Empty).Replace("@", string.Empty).Replace("#", string.Empty).Replace("$", string.Empty).Replace("%", string.Empty).Replace("^", string.Empty).Replace("&", string.Empty).Replace("*", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty).Replace(";", string.Empty).Replace(":", string.Empty).Replace("'", string.Empty).Replace(",", string.Empty).Replace("<", string.Empty).Replace(">", string.Empty).Replace(".", string.Empty).Replace("=", string.Empty).Replace("/", string.Empty).Replace("\\", string.Empty).Replace("|", string.Empty).Replace("`", string.Empty).Replace("~", string.Empty).Replace("'", string.Empty).Replace("\"", string.Empty);
			Recorder.Instance.Reset(text);
			MessageBox messageBox = UI.Hints.Show(DebugMenuB.Instance.ReplayGotResetMessageProvider, HintLayer.GameSaved, 3f);
			if (messageBox)
			{
				UnityEngine.Object.DestroyObject(messageBox.GetComponent<DestroyOnRestoreCheckpoint>());
			}
		}
		RecorderMessageInputUI recorderMessageInputUI = this.m_recorderMessageInputUI;
		recorderMessageInputUI.OnExit = (Action)Delegate.Remove(recorderMessageInputUI.OnExit, new Action(this.OnExit));
		this.m_recorderMessageInputUI = null;
	}

	// Token: 0x06000EEA RID: 3818 RVA: 0x00044A10 File Offset: 0x00042C10
	public static Dictionary<RecorderMessageData, int> ExtractRecorderMessagesData(RecorderData recorderData)
	{
		Dictionary<RecorderMessageData, int> dictionary = new Dictionary<RecorderMessageData, int>();
		int num = 0;
		foreach (RecorderFrame recorderFrame in recorderData.Frames)
		{
			foreach (IFrameData frameData in recorderFrame.FrameData)
			{
				if (frameData is RecorderMessageData)
				{
					dictionary[frameData as RecorderMessageData] = num;
				}
			}
			num++;
		}
		return dictionary;
	}

	// Token: 0x04000BDC RID: 3036
	private RecorderMessageInputUI m_recorderMessageInputUI;

	// Token: 0x04000BDD RID: 3037
	private GameObject m_recorderMessageInuptUIPrefab;

	// Token: 0x04000BDE RID: 3038
	private Dictionary<RecorderMessageData, int> m_recorderMessageData = new Dictionary<RecorderMessageData, int>();

	// Token: 0x04000BDF RID: 3039
	private bool m_showMessages;

	// Token: 0x04000BE0 RID: 3040
	private bool m_shouldOpenMessageInputUI;

	// Token: 0x04000BE1 RID: 3041
	private Vector2 m_scrollPosition = Vector2.zero;

	// Token: 0x04000BE2 RID: 3042
	private Vector2 m_tooltipScrollPosition = Vector2.zero;

	// Token: 0x04000BE3 RID: 3043
	private string m_lastMessageText = string.Empty;

	// Token: 0x04000BE4 RID: 3044
	private float m_offset;
}
