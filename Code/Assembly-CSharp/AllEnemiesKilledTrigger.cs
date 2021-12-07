using System;
using System.Collections.Generic;
using Game;

// Token: 0x020003BC RID: 956
public class AllEnemiesKilledTrigger : Trigger
{
	// Token: 0x06001A8C RID: 6796 RVA: 0x00072660 File Offset: 0x00070860
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_counter);
		base.Serialize(ar);
		if (this.ActionOnAwakeTrigger && this.m_counter >= this.TriggerOnCounter)
		{
			this.ActionOnAwakeTrigger.Perform(null);
		}
	}

	// Token: 0x06001A8D RID: 6797 RVA: 0x000726B0 File Offset: 0x000708B0
	public void Increment()
	{
		this.m_counter++;
		if (this.m_counter == this.TriggerOnCounter)
		{
			base.DoTrigger(true);
		}
	}

	// Token: 0x06001A8E RID: 6798 RVA: 0x000726E3 File Offset: 0x000708E3
	public new void Awake()
	{
		base.Awake();
		this.RegisterEvent();
	}

	// Token: 0x06001A8F RID: 6799 RVA: 0x000726F1 File Offset: 0x000708F1
	public new void OnDestroy()
	{
		base.OnDestroy();
		this.DeregisterEvent();
	}

	// Token: 0x06001A90 RID: 6800 RVA: 0x00072700 File Offset: 0x00070900
	public void Init()
	{
		this.RespawningPlaceholders.Clear();
		for (int i = 0; i < base.GetComponentsInChildren<RespawningPlaceholder>().Length; i++)
		{
			RespawningPlaceholder item = base.GetComponentsInChildren<RespawningPlaceholder>()[i];
			this.RespawningPlaceholders.Add(item);
		}
		this.Entities.Clear();
		for (int j = 0; j < base.GetComponentsInChildren<Entity>().Length; j++)
		{
			Entity item2 = base.GetComponentsInChildren<Entity>()[j];
			this.Entities.Add(item2);
		}
		this.TriggerOnCounter = this.RespawningPlaceholders.Count + this.Entities.Count;
	}

	// Token: 0x06001A91 RID: 6801 RVA: 0x0007279C File Offset: 0x0007099C
	private void RegisterEvent()
	{
		Action<Damage> action = new Action<Damage>(this.EntityKilled);
		for (int i = 0; i < this.RespawningPlaceholders.Count; i++)
		{
			RespawningPlaceholder respawningPlaceholder = this.RespawningPlaceholders[i];
			RespawningPlaceholder respawningPlaceholder2 = respawningPlaceholder;
			respawningPlaceholder2.OnCurrentInstanceDeath = (Action<Damage>)Delegate.Combine(respawningPlaceholder2.OnCurrentInstanceDeath, action);
		}
		for (int j = 0; j < this.Entities.Count; j++)
		{
			Entity entity = this.Entities[j];
			entity.DamageReciever.OnDeathEvent.Add(action);
		}
	}

	// Token: 0x06001A92 RID: 6802 RVA: 0x00072834 File Offset: 0x00070A34
	private void DeregisterEvent()
	{
		Action<Damage> action = new Action<Damage>(this.EntityKilled);
		for (int i = 0; i < this.RespawningPlaceholders.Count; i++)
		{
			RespawningPlaceholder respawningPlaceholder = this.RespawningPlaceholders[i];
			RespawningPlaceholder respawningPlaceholder2 = respawningPlaceholder;
			respawningPlaceholder2.OnCurrentInstanceDeath = (Action<Damage>)Delegate.Remove(respawningPlaceholder2.OnCurrentInstanceDeath, action);
		}
		for (int j = 0; j < this.Entities.Count; j++)
		{
			Entity entity = this.Entities[j];
			entity.DamageReciever.OnDeathEvent.Remove(action);
		}
	}

	// Token: 0x06001A93 RID: 6803 RVA: 0x000728CA File Offset: 0x00070ACA
	private void EntityKilled(Damage damage)
	{
		this.EnemyKilled();
	}

	// Token: 0x06001A94 RID: 6804 RVA: 0x000728D4 File Offset: 0x00070AD4
	private void EnemyKilled()
	{
		if (this.Active)
		{
			this.Increment();
			if (this.m_lastMessageBox)
			{
				this.m_lastMessageBox.HideMessageScreen();
			}
			if (this.ShowMessages)
			{
				int num = this.TriggerOnCounter - this.m_counter - 1;
				if (num >= this.Messages.Count)
				{
					num = this.Messages.Count - 1;
				}
				if (num > 0)
				{
					this.m_lastMessageBox = UI.Hints.Show(this.Messages[num], HintLayer.Gameplay, 1f);
				}
			}
		}
	}

	// Token: 0x04001709 RID: 5897
	public List<RespawningPlaceholder> RespawningPlaceholders = new List<RespawningPlaceholder>();

	// Token: 0x0400170A RID: 5898
	public List<Entity> Entities = new List<Entity>();

	// Token: 0x0400170B RID: 5899
	public List<MessageProvider> Messages = new List<MessageProvider>();

	// Token: 0x0400170C RID: 5900
	public bool ShowMessages = true;

	// Token: 0x0400170D RID: 5901
	public int TriggerOnCounter;

	// Token: 0x0400170E RID: 5902
	private int m_counter;

	// Token: 0x0400170F RID: 5903
	private MessageBox m_lastMessageBox;

	// Token: 0x04001710 RID: 5904
	public ActionMethod ActionOnAwakeTrigger;
}
