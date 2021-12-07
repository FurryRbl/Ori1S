using System;
using Game;

// Token: 0x020004B9 RID: 1209
public class DebugMenuWorldEventActionMenuItem : ActionDebugMenuItem
{
	// Token: 0x060020C2 RID: 8386 RVA: 0x0008F882 File Offset: 0x0008DA82
	public DebugMenuWorldEventActionMenuItem(WorldEvents worldEvent)
	{
		this.m_worldEvent = worldEvent;
		this.Func = new Func<bool>(this.UpdateWorldEventValue);
		this.UpdateText();
	}

	// Token: 0x060020C3 RID: 8387 RVA: 0x0008F8AC File Offset: 0x0008DAAC
	private bool UpdateWorldEventValue()
	{
		int value = World.Events.Find(this.m_worldEvent).Value;
		int num = 0;
		for (int i = 0; i < this.m_worldEvent.Items.Count; i++)
		{
			if (this.m_worldEvent.Items[i].ID == value)
			{
				num = i;
				break;
			}
		}
		if (num + 1 == this.m_worldEvent.Items.Count)
		{
			num = 0;
		}
		else
		{
			num++;
		}
		World.Events.Find(this.m_worldEvent).Value = this.m_worldEvent.Items[num].ID;
		this.UpdateText();
		return false;
	}

	// Token: 0x060020C4 RID: 8388 RVA: 0x0008F960 File Offset: 0x0008DB60
	private void UpdateText()
	{
		string titleText = string.Empty;
		if (World.Events.Find(this.m_worldEvent) != null)
		{
			titleText = this.m_worldEvent.name;
			this.HelpText = this.m_worldEvent.name + ": " + this.m_worldEvent.GetNameFromID(World.Events.Find(this.m_worldEvent).Value);
		}
		else
		{
			this.HelpText = string.Empty;
		}
		this.TitleText = titleText;
	}

	// Token: 0x04001BC4 RID: 7108
	private WorldEvents m_worldEvent;
}
