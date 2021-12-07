using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using fsm;
using UnityEngine;

// Token: 0x020001EF RID: 495
public class TextBoxMessage : MonoBehaviour, ISuspendable
{
	// Token: 0x14000025 RID: 37
	// (add) Token: 0x060010F6 RID: 4342 RVA: 0x0004D658 File Offset: 0x0004B858
	// (remove) Token: 0x060010F7 RID: 4343 RVA: 0x0004D671 File Offset: 0x0004B871
	public event Action OnCompleteEvent = delegate()
	{
	};

	// Token: 0x060010F8 RID: 4344 RVA: 0x0004D68A File Offset: 0x0004B88A
	public void AddLine(string text)
	{
		this.Messages.Enqueue(text);
	}

	// Token: 0x060010F9 RID: 4345 RVA: 0x0004D698 File Offset: 0x0004B898
	public void Awake()
	{
		SuspensionManager.Register(this);
		this.State.Inactive = new State
		{
			OnEnterEvent = new Action(this.EnterInactive)
		};
		this.State.Writing = new State
		{
			UpdateStateEvent = new Action(this.UpdateWriting),
			OnEnterEvent = new Action(this.OnEnterWriting),
			OnExitEvent = new Action(this.OnExitWriting)
		};
		this.State.Completed = new State
		{
			UpdateStateEvent = new Action(this.UpdateCompleted),
			OnEnterEvent = new Action(this.OnEnterComplete),
			OnExitEvent = new Action(this.OnExitComplete)
		};
		this.Logic.ChangeState(this.State.Inactive);
	}

	// Token: 0x060010FA RID: 4346 RVA: 0x0004D775 File Offset: 0x0004B975
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x060010FB RID: 4347 RVA: 0x0004D780 File Offset: 0x0004B980
	public void Initialize()
	{
		if (this.Background)
		{
			this.m_backgroundAnimators = new List<LegacyAnimator>(this.Background.GetComponentsInChildren<LegacyAnimator>());
		}
		if (this.Button)
		{
			this.m_buttonAnimators = new List<LegacyAnimator>(this.Button.GetComponentsInChildren<LegacyAnimator>());
		}
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x0004D7D9 File Offset: 0x0004B9D9
	public void EnterInactive()
	{
		this.Text.Text = string.Empty;
		GameController.Instance.LockInput = false;
	}

	// Token: 0x060010FD RID: 4349 RVA: 0x0004D7F8 File Offset: 0x0004B9F8
	public static byte[] StringToByteArray(string hex)
	{
		return (from x in Enumerable.Range(0, hex.Length)
		where x % 2 == 0
		select Convert.ToByte(hex.Substring(x, 2), 16)).ToArray<byte>();
	}

	// Token: 0x060010FE RID: 4350 RVA: 0x0004D85C File Offset: 0x0004BA5C
	public void OnEnterWriting()
	{
		this.m_remainingWaitTime = 0.3f;
		GameController.Instance.LockInput = true;
		string currentMessage = this.CurrentMessage;
		this.PlayOriSpeech(currentMessage.Length);
		StringBuilder stringBuilder = new StringBuilder();
		List<Color> list = new List<Color>();
		Color color = this.Text.Color;
		bool flag = false;
		Color item = color;
		string text = string.Empty;
		bool flag2 = false;
		foreach (char c in currentMessage)
		{
			if (flag)
			{
				if (c == '/')
				{
					flag2 = true;
				}
				else if (c == '>')
				{
					flag = false;
					if (text.StartsWith("color"))
					{
						if (flag2)
						{
							item = color;
						}
						else
						{
							string text2 = text.Split(new char[]
							{
								'='
							})[1];
							if (text2.StartsWith("#"))
							{
								string hex = text2.Remove(0, 1);
								byte[] array = TextBoxMessage.StringToByteArray(hex);
								if (array.Length == 3)
								{
									item = new Color((float)array[0], (float)array[1], (float)array[2], 255f) / 255f;
								}
								if (array.Length == 4)
								{
									item = new Color((float)array[0], (float)array[1], (float)array[2], (float)array[3]) / 255f;
								}
							}
						}
					}
					text = string.Empty;
					flag2 = false;
				}
				else
				{
					text += c;
				}
			}
			else if (c == '<')
			{
				flag = true;
			}
			else
			{
				list.Add(item);
				stringBuilder.Append(c);
			}
		}
		this.TextColorer.Colors = list.ToArray();
		this.TextColorer.Count = 0f;
		this.Text.Text = stringBuilder.ToString();
	}

	// Token: 0x060010FF RID: 4351 RVA: 0x0004DA34 File Offset: 0x0004BC34
	private void PlayOriSpeech(int textLength)
	{
		this.StopOriSpeech();
		if (textLength <= this.MaxTextLengthShortSpeech)
		{
			if (this.OriSpeechShortSound)
			{
				this.OriSpeechShortSound.Play();
			}
		}
		else if (textLength <= this.MaxTextLengthMedSpeech)
		{
			if (this.OriSpeechMedSound)
			{
				this.OriSpeechMedSound.Play();
			}
		}
		else if (this.OriSpeechLongSound)
		{
			this.OriSpeechLongSound.Play();
		}
	}

	// Token: 0x06001100 RID: 4352 RVA: 0x0004DABC File Offset: 0x0004BCBC
	private void StopOriSpeech()
	{
		if (this.OriSpeechShortSound)
		{
			this.OriSpeechShortSound.StopAndFadeOut(0.5f);
		}
		if (this.OriSpeechMedSound)
		{
			this.OriSpeechMedSound.StopAndFadeOut(0.5f);
		}
		if (this.OriSpeechLongSound)
		{
			this.OriSpeechLongSound.StopAndFadeOut(0.5f);
		}
	}

	// Token: 0x06001101 RID: 4353 RVA: 0x0004DB2C File Offset: 0x0004BD2C
	public void OnEnterComplete()
	{
		foreach (LegacyAnimator legacyAnimator in this.m_buttonAnimators)
		{
			legacyAnimator.ContinueForward();
		}
	}

	// Token: 0x06001102 RID: 4354 RVA: 0x0004DB88 File Offset: 0x0004BD88
	public void OnExitComplete()
	{
		foreach (LegacyAnimator legacyAnimator in this.m_buttonAnimators)
		{
			legacyAnimator.ContinueBackward();
		}
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x0004DBE4 File Offset: 0x0004BDE4
	public void OnExitWriting()
	{
	}

	// Token: 0x06001104 RID: 4356 RVA: 0x0004DBE8 File Offset: 0x0004BDE8
	public void UpdateWriting()
	{
		float num = Mathf.Min(this.Logic.CurrentStateTime * this.LettersPerSecond, (float)this.Text.Text.Length);
		this.TextColorer.Count = num;
		this.Text.UpdateText();
		if (num == (float)this.Text.Text.Length || this.OnButtonPressed)
		{
			Core.Input.SpiritFlame.Used = true;
			Core.Input.Jump.Used = true;
			this.TextColorer.Count = (float)this.CurrentMessage.Length;
			this.Text.UpdateText();
			if (this.MessageFinishedSound)
			{
				this.MessageFinishedSound.Play();
			}
			this.Logic.ChangeState(this.State.Completed);
		}
	}

	// Token: 0x06001105 RID: 4357 RVA: 0x0004DCC0 File Offset: 0x0004BEC0
	public void UpdateCompleted()
	{
		if (this.OnButtonPressed && this.m_remainingWaitTime <= 0f)
		{
			Core.Input.SpiritFlame.Used = true;
			Core.Input.Jump.Used = true;
			if (this.Messages.Count == 0)
			{
				this.StopWriting();
			}
			else
			{
				this.ReadNextLine();
			}
		}
	}

	// Token: 0x06001106 RID: 4358 RVA: 0x0004DD20 File Offset: 0x0004BF20
	public void StopWriting()
	{
		foreach (LegacyAnimator legacyAnimator in this.m_backgroundAnimators)
		{
			legacyAnimator.ContinueBackward();
		}
		this.StopOriSpeech();
		this.Logic.ChangeState(this.State.Inactive);
		this.OnCompleteEvent();
	}

	// Token: 0x06001107 RID: 4359 RVA: 0x0004DDA0 File Offset: 0x0004BFA0
	public void ReadNextLine()
	{
		this.CurrentMessage = this.Messages.Dequeue();
		this.Logic.ChangeState(this.State.Writing);
	}

	// Token: 0x06001108 RID: 4360 RVA: 0x0004DDCC File Offset: 0x0004BFCC
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.m_remainingWaitTime -= Time.deltaTime;
		this.Logic.UpdateState(Time.deltaTime);
	}

	// Token: 0x170002FD RID: 765
	// (get) Token: 0x06001109 RID: 4361 RVA: 0x0004DE07 File Offset: 0x0004C007
	public bool OnButtonPressed
	{
		get
		{
			return (Core.Input.SpiritFlame.OnPressed && !Core.Input.SpiritFlame.Used) || Core.Input.Jump.OnPressed;
		}
	}

	// Token: 0x170002FE RID: 766
	// (get) Token: 0x0600110A RID: 4362 RVA: 0x0004DE34 File Offset: 0x0004C034
	public bool Active
	{
		get
		{
			return this.Logic.CurrentState == this.State.Inactive;
		}
	}

	// Token: 0x0600110B RID: 4363 RVA: 0x0004DE50 File Offset: 0x0004C050
	public void StartWriting()
	{
		foreach (LegacyAnimator legacyAnimator in this.m_backgroundAnimators)
		{
			legacyAnimator.ContinueForward();
		}
		this.Logic.ChangeState(this.State.Writing);
	}

	// Token: 0x170002FF RID: 767
	// (get) Token: 0x0600110C RID: 4364 RVA: 0x0004DEC0 File Offset: 0x0004C0C0
	public bool IsInactive
	{
		get
		{
			return this.Logic.CurrentState == this.State.Inactive;
		}
	}

	// Token: 0x17000300 RID: 768
	// (get) Token: 0x0600110D RID: 4365 RVA: 0x0004DEDA File Offset: 0x0004C0DA
	// (set) Token: 0x0600110E RID: 4366 RVA: 0x0004DEE2 File Offset: 0x0004C0E2
	public bool IsSuspended { get; set; }

	// Token: 0x04000EB3 RID: 3763
	public const float WaitTimeBetweenMessages = 0.3f;

	// Token: 0x04000EB4 RID: 3764
	public CCText Text;

	// Token: 0x04000EB5 RID: 3765
	public CCMoonTextColorer TextColorer;

	// Token: 0x04000EB6 RID: 3766
	public SoundSource MessageFinishedSound;

	// Token: 0x04000EB7 RID: 3767
	[NotNull]
	public SoundSource OriSpeechShortSound;

	// Token: 0x04000EB8 RID: 3768
	[NotNull]
	public SoundSource OriSpeechMedSound;

	// Token: 0x04000EB9 RID: 3769
	[NotNull]
	public SoundSource OriSpeechLongSound;

	// Token: 0x04000EBA RID: 3770
	public int MaxTextLengthShortSpeech;

	// Token: 0x04000EBB RID: 3771
	public int MaxTextLengthMedSpeech;

	// Token: 0x04000EBC RID: 3772
	public GameObject Background;

	// Token: 0x04000EBD RID: 3773
	public GameObject Button;

	// Token: 0x04000EBE RID: 3774
	private List<LegacyAnimator> m_backgroundAnimators = new List<LegacyAnimator>();

	// Token: 0x04000EBF RID: 3775
	private List<LegacyAnimator> m_buttonAnimators = new List<LegacyAnimator>();

	// Token: 0x04000EC0 RID: 3776
	public TextBoxMessage.States State = new TextBoxMessage.States();

	// Token: 0x04000EC1 RID: 3777
	public float LettersPerSecond = 10f;

	// Token: 0x04000EC2 RID: 3778
	public Queue<string> Messages = new Queue<string>();

	// Token: 0x04000EC3 RID: 3779
	private float m_remainingWaitTime;

	// Token: 0x04000EC4 RID: 3780
	public StateMachine Logic = new StateMachine();

	// Token: 0x04000EC5 RID: 3781
	public string CurrentMessage;

	// Token: 0x020001F0 RID: 496
	public class States
	{
		// Token: 0x04000ECA RID: 3786
		public State Writing;

		// Token: 0x04000ECB RID: 3787
		public State Completed;

		// Token: 0x04000ECC RID: 3788
		public State Inactive;
	}
}
