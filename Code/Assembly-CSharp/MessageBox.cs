using System;
using System.Collections.Generic;
using System.Linq;
using CatlikeCoding.TextBox;
using Game;
using UnityEngine;

// Token: 0x020000F2 RID: 242
[ExecuteInEditMode]
public class MessageBox : MonoBehaviour
{
	// Token: 0x14000019 RID: 25
	// (add) Token: 0x06000999 RID: 2457 RVA: 0x0002A4B5 File Offset: 0x000286B5
	// (remove) Token: 0x0600099A RID: 2458 RVA: 0x0002A4CE File Offset: 0x000286CE
	public event Action OnMessageScreenHide = delegate()
	{
	};

	// Token: 0x1400001A RID: 26
	// (add) Token: 0x0600099B RID: 2459 RVA: 0x0002A4E7 File Offset: 0x000286E7
	// (remove) Token: 0x0600099C RID: 2460 RVA: 0x0002A500 File Offset: 0x00028700
	public event Action OnNextMessage = delegate()
	{
	};

	// Token: 0x0600099D RID: 2461 RVA: 0x0002A51C File Offset: 0x0002871C
	public HashSet<ISuspendable> GetSuspendables()
	{
		HashSet<ISuspendable> hashSet = new HashSet<ISuspendable>();
		foreach (ISuspendable item in base.GetComponentsInChildren(typeof(ISuspendable)))
		{
			hashSet.Add(item);
		}
		return hashSet;
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x0002A566 File Offset: 0x00028766
	public void OverrideLanuage(Language language)
	{
		this.m_language = language;
		this.m_forceLanguage = true;
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x0002A578 File Offset: 0x00028778
	public void SetAvatar(GameObject avatarPrefab)
	{
		if (this.m_avatar)
		{
			InstantiateUtility.Destroy(this.m_avatar);
			this.m_avatar = null;
		}
		if (avatarPrefab)
		{
			this.m_avatar = UnityEngine.Object.Instantiate<GameObject>(avatarPrefab);
			this.m_avatar.transform.parent = this.Avatar;
			this.m_avatar.transform.localPosition = Vector3.zero;
			this.m_avatar.transform.localRotation = avatarPrefab.transform.localRotation;
			this.m_avatar.transform.localScale = avatarPrefab.transform.localScale;
		}
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x0002A61F File Offset: 0x0002881F
	public void SetAvatarArray(GameObject[] avatarPrefabs)
	{
		this.m_avatarPrefabs = avatarPrefabs;
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x0002A628 File Offset: 0x00028828
	public void HideMessageScreen()
	{
		this.Visibility.HideMessageScreen();
		this.OnMessageScreenHide();
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x0002A640 File Offset: 0x00028840
	public void Awake()
	{
		if (Application.isPlaying)
		{
			Events.Scheduler.OnGameLanguageChange.Add(new Action(this.RefreshText));
			Events.Scheduler.OnGameControlSchemeChange.Add(new Action(this.RefreshText));
		}
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x0002A690 File Offset: 0x00028890
	public void OnDestroy()
	{
		if (Application.isPlaying)
		{
			Events.Scheduler.OnGameLanguageChange.Remove(new Action(this.RefreshText));
			Events.Scheduler.OnGameControlSchemeChange.Remove(new Action(this.RefreshText));
		}
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x0002A6DD File Offset: 0x000288DD
	public void Start()
	{
		this.RefreshText();
		if (this.WriteOutTextBox)
		{
			this.WriteOutTextBox.GoToStart();
		}
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x0002A700 File Offset: 0x00028900
	public void Update()
	{
		if (this.m_previousOverrideText != this.OverrideText)
		{
			this.m_previousOverrideText = this.OverrideText;
			this.RefreshText();
		}
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x0002A735 File Offset: 0x00028935
	public void RemoveMessageFade()
	{
		this.SetMessageFade(999999f);
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x0002A744 File Offset: 0x00028944
	public void SetMessageFade(float time)
	{
		if (this.TextBox.textRenderers != null)
		{
			foreach (TextRenderer textRenderer in this.TextBox.textRenderers)
			{
				MoonTextMeshRenderer moonTextMeshRenderer = textRenderer as MoonTextMeshRenderer;
				if (moonTextMeshRenderer != null)
				{
					Renderer component = moonTextMeshRenderer.GetComponent<Renderer>();
					if (component)
					{
						float val = time / this.FadeSpread;
						UberShaderAPI.SetFloat(component, val, "_TxtTime", true);
					}
				}
			}
		}
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x0002A7C8 File Offset: 0x000289C8
	public void SetMessage(MessageDescriptor messageDescriptor)
	{
		this.MessageProvider = null;
		this.m_messageDescriptors = null;
		this.m_currentMessage = messageDescriptor;
		if (this.FormatText)
		{
			string text = MessageParserUtility.ProcessString(this.m_currentMessage.Message);
			this.TextBox.SetText(text);
		}
		else
		{
			this.TextBox.SetText(this.m_currentMessage.Message);
		}
		this.RefreshText();
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x0002A834 File Offset: 0x00028A34
	public void RefreshText()
	{
		if (this.m_forceLanguage)
		{
			this.TextBox.SetStyleCollection(this.LanguageStyles.GetStyle(this.m_language));
		}
		else
		{
			this.TextBox.SetStyleCollection(this.LanguageStyles.Current);
		}
		if (this.MessageProvider)
		{
			this.m_messageDescriptors = this.MessageProvider.GetMessages().ToArray<MessageDescriptor>();
			this.MessageIndex = Mathf.Clamp(this.MessageIndex, 0, this.m_messageDescriptors.Length);
			this.m_currentMessage = this.m_messageDescriptors[this.MessageIndex];
			if (this.FormatText)
			{
				string text = MessageParserUtility.ProcessString(this.m_currentMessage.Message);
				this.TextBox.SetText(text);
			}
			else
			{
				this.TextBox.SetText(this.m_currentMessage.Message);
			}
		}
		else if (this.OverrideText != string.Empty)
		{
			if (this.FormatText)
			{
				this.TextBox.SetText(MessageParserUtility.ProcessString(this.OverrideText));
			}
			else
			{
				this.TextBox.SetText(this.OverrideText);
			}
		}
		this.TextBox.CreateRendersIfThereAreNone();
		foreach (TextRenderer textRenderer in this.TextBox.textRenderers)
		{
			MoonTextMeshRenderer moonTextMeshRenderer = textRenderer as MoonTextMeshRenderer;
			if (moonTextMeshRenderer)
			{
				moonTextMeshRenderer.FadeSpread = this.FadeSpread;
			}
		}
		this.TextBox.size = this.ScaleOverLetterCount.Evaluate((float)TextBoxExtended.CountLetters(this.TextBox));
		this.TextBox.RenderText();
		if (this.WriteOutTextBox)
		{
			this.WriteOutTextBox.OnTextChange();
		}
		else
		{
			this.RemoveMessageFade();
		}
		if (this.m_avatarPrefabs != null)
		{
			this.SetAvatar(this.m_avatarPrefabs[this.MessageIndex]);
		}
		if (!Application.isPlaying)
		{
			this.RemoveMessageFade();
		}
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x0002AA44 File Offset: 0x00028C44
	public void OnEnable()
	{
		if (!Application.isPlaying)
		{
			this.RemoveMessageFade();
		}
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x0002AA56 File Offset: 0x00028C56
	public void SetMessageProvider(MessageProvider messageProvider)
	{
		this.MessageProvider = messageProvider;
		this.RefreshText();
	}

	// Token: 0x1700020B RID: 523
	// (get) Token: 0x060009AC RID: 2476 RVA: 0x0002AA65 File Offset: 0x00028C65
	public int MessageCount
	{
		get
		{
			if (this.m_messageDescriptors == null)
			{
				return 1;
			}
			return this.m_messageDescriptors.Length;
		}
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x0002AA7C File Offset: 0x00028C7C
	public void SetWaitDuration(float duration)
	{
		this.Visibility.WaitDuration = duration;
	}

	// Token: 0x1700020C RID: 524
	// (get) Token: 0x060009AE RID: 2478 RVA: 0x0002AA8A File Offset: 0x00028C8A
	public EmotionType CurrentEmotion
	{
		get
		{
			return this.m_currentMessage.Emotion;
		}
	}

	// Token: 0x1700020D RID: 525
	// (get) Token: 0x060009AF RID: 2479 RVA: 0x0002AA97 File Offset: 0x00028C97
	public SoundProvider CurrentMessageSound
	{
		get
		{
			return this.m_currentMessage.Sound;
		}
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x0002AAA4 File Offset: 0x00028CA4
	public void FinishWriting()
	{
		if (this.WriteOutTextBox)
		{
			this.WriteOutTextBox.AnimatorDriver.GoToEnd();
		}
	}

	// Token: 0x1700020E RID: 526
	// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0002AAD1 File Offset: 0x00028CD1
	public bool IsLastMessage
	{
		get
		{
			return this.m_messageDescriptors == null || this.MessageIndex == this.m_messageDescriptors.Length - 1;
		}
	}

	// Token: 0x1700020F RID: 527
	// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0002AAF2 File Offset: 0x00028CF2
	public bool FinishedWriting
	{
		get
		{
			return this.WriteOutTextBox == null || this.WriteOutTextBox.AtEnd;
		}
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x0002AB14 File Offset: 0x00028D14
	public void NextMessage()
	{
		this.MessageIndex++;
		this.RefreshText();
		if (this.WriteOutTextBox)
		{
			this.WriteOutTextBox.GoToStart();
		}
		this.OnNextMessage();
		if (this.NextMessageAnimator)
		{
			this.NextMessageAnimator.AnimatorDriver.Restart();
		}
	}

	// Token: 0x040007EE RID: 2030
	public const float WaitTimeBetweenMessages = 0.3f;

	// Token: 0x040007EF RID: 2031
	public MessageBoxLanguageStyles LanguageStyles;

	// Token: 0x040007F0 RID: 2032
	public WriteOutTextBox WriteOutTextBox;

	// Token: 0x040007F1 RID: 2033
	public MessageBoxVisibility Visibility;

	// Token: 0x040007F2 RID: 2034
	public TextBox TextBox;

	// Token: 0x040007F3 RID: 2035
	public Transform Avatar;

	// Token: 0x040007F4 RID: 2036
	public int MessageIndex;

	// Token: 0x040007F5 RID: 2037
	public MessageProvider MessageProvider;

	// Token: 0x040007F6 RID: 2038
	public AnimationCurve ScaleOverLetterCount = AnimationCurve.Linear(0f, 1f, 150f, 1f);

	// Token: 0x040007F7 RID: 2039
	private float m_remainingWaitTime;

	// Token: 0x040007F8 RID: 2040
	private GameObject m_avatar;

	// Token: 0x040007F9 RID: 2041
	private GameObject[] m_avatarPrefabs;

	// Token: 0x040007FA RID: 2042
	public BaseAnimator NextMessageAnimator;

	// Token: 0x040007FB RID: 2043
	public bool FormatText = true;

	// Token: 0x040007FC RID: 2044
	private bool m_forceLanguage;

	// Token: 0x040007FD RID: 2045
	private Language m_language;

	// Token: 0x040007FE RID: 2046
	public float FadeSpread = 5f;

	// Token: 0x040007FF RID: 2047
	public string OverrideText;

	// Token: 0x04000800 RID: 2048
	private string m_previousOverrideText = string.Empty;

	// Token: 0x04000801 RID: 2049
	private MessageDescriptor[] m_messageDescriptors;

	// Token: 0x04000802 RID: 2050
	private MessageDescriptor m_currentMessage;
}
