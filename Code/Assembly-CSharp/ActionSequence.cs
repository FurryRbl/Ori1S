using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class ActionSequence : PerformingAction, IPooled, ISuspendable
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000005 RID: 5 RVA: 0x000023A2 File Offset: 0x000005A2
	// (set) Token: 0x06000006 RID: 6 RVA: 0x000023AA File Offset: 0x000005AA
	public bool IsRunning
	{
		get
		{
			return this.m_isRunning;
		}
		set
		{
			this.m_isRunning = value;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000007 RID: 7 RVA: 0x000023B3 File Offset: 0x000005B3
	// (set) Token: 0x06000008 RID: 8 RVA: 0x000023BB File Offset: 0x000005BB
	public int Index
	{
		get
		{
			return this.m_index;
		}
		set
		{
			this.m_index = value;
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000023C4 File Offset: 0x000005C4
	public void OnPoolSpawned()
	{
		this.m_isRunning = false;
		this.m_index = 0;
		this.m_context = null;
		this.m_isSuspended = false;
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000023E2 File Offset: 0x000005E2
	public override void Awake()
	{
		SuspensionManager.Register(this);
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x0600000B RID: 11 RVA: 0x0000241C File Offset: 0x0000061C
	public override void OnDestroy()
	{
		SuspensionManager.Unregister(this);
		base.OnDestroy();
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002468 File Offset: 0x00000668
	private void OnGameReset()
	{
		if (this.m_isRunning)
		{
			this.m_isRunning = false;
			this.m_index = 0;
			this.m_context = null;
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002498 File Offset: 0x00000698
	public void OnRestoreCheckpoint()
	{
		ActionSequenceSerializer component = base.GetComponent<ActionSequenceSerializer>();
		if (component)
		{
			return;
		}
		this.m_isRunning = false;
		this.m_index = 0;
		this.m_context = null;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000024D0 File Offset: 0x000006D0
	public void FindActions()
	{
		this.Actions.Clear();
		for (int i = 0; i < base.transform.childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			foreach (ActionMethod item in child.GetComponents<ActionMethod>())
			{
				this.Actions.Add(item);
			}
		}
		this.Actions.Sort((ActionMethod a, ActionMethod b) => string.Compare(a.name, b.name, StringComparison.Ordinal));
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002568 File Offset: 0x00000768
	public override void Perform(IContext context)
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.Actions == null)
		{
			this.FindActions();
		}
		if (this.Actions.Count == 0)
		{
			return;
		}
		this.m_isRunning = true;
		this.m_index = 0;
		this.m_context = context;
		this.RunAction(this.Actions[this.m_index]);
		this.UpdateActions();
	}

	// Token: 0x06000010 RID: 16 RVA: 0x000025D5 File Offset: 0x000007D5
	public void RunAction(ActionMethod action)
	{
		if (action)
		{
			action.Perform(this.m_context);
		}
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000025EE File Offset: 0x000007EE
	public void FixedUpdate()
	{
		if (this.m_isSuspended)
		{
			return;
		}
		this.UpdateActions();
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00002604 File Offset: 0x00000804
	public void UpdateActions()
	{
		if (!this.m_isRunning)
		{
			return;
		}
		int count = this.Actions.Count;
		while (this.m_index < count)
		{
			ActionMethod actionMethod = this.Actions[this.m_index];
			if (actionMethod != null && actionMethod is WaitAction)
			{
				WaitAction waitAction = actionMethod as WaitAction;
				if (waitAction.IsPerforming)
				{
					return;
				}
			}
			this.m_index++;
			if (this.m_index == count)
			{
				this.m_isRunning = false;
				return;
			}
			this.RunAction(this.Actions[this.m_index]);
		}
	}

	// Token: 0x06000013 RID: 19 RVA: 0x000026B0 File Offset: 0x000008B0
	public static void Rename(List<ActionMethod> actions)
	{
		int num = 0;
		for (int i = 0; i < actions.Count; i++)
		{
			ActionMethod actionMethod = actions[i];
			num++;
			string niceName = actionMethod.GetNiceName();
			actionMethod.name = ActionSequence.FormatName(num, niceName);
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000026F6 File Offset: 0x000008F6
	public static string FormatName(int number, string name)
	{
		return string.Format("{0:00}", number) + ". " + name;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002713 File Offset: 0x00000913
	public static string UnformatName(string name)
	{
		return name.Remove(0, 4);
	}

	// Token: 0x06000016 RID: 22 RVA: 0x0000271D File Offset: 0x0000091D
	public void RefreshNames()
	{
		this.FindActions();
		ActionSequence.Rename(this.Actions);
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002730 File Offset: 0x00000930
	public override string GetNiceName()
	{
		return base.gameObject.name;
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000018 RID: 24 RVA: 0x0000273D File Offset: 0x0000093D
	// (set) Token: 0x06000019 RID: 25 RVA: 0x00002745 File Offset: 0x00000945
	public bool IsSuspended
	{
		get
		{
			return this.m_isSuspended;
		}
		set
		{
			this.m_isSuspended = value;
		}
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000274E File Offset: 0x0000094E
	public override void Stop()
	{
		this.m_isRunning = false;
		this.m_index = 0;
		this.m_context = null;
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x0600001B RID: 27 RVA: 0x00002765 File Offset: 0x00000965
	public override bool IsPerforming
	{
		get
		{
			return this.m_isRunning;
		}
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00002770 File Offset: 0x00000970
	public override void Serialize(Archive ar)
	{
		ActionSequenceSerializer component = base.GetComponent<ActionSequenceSerializer>();
		if (component)
		{
			return;
		}
		if (ar.Reading)
		{
			this.Stop();
		}
		base.Serialize(ar);
	}

	// Token: 0x04000005 RID: 5
	private bool m_isRunning;

	// Token: 0x04000006 RID: 6
	private int m_index;

	// Token: 0x04000007 RID: 7
	private IContext m_context;

	// Token: 0x04000008 RID: 8
	private bool m_isSuspended;

	// Token: 0x04000009 RID: 9
	public List<ActionMethod> Actions = new List<ActionMethod>();
}
