using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000358 RID: 856
public class MapStone : SaveSerialize
{
	// Token: 0x0600185E RID: 6238 RVA: 0x0006876A File Offset: 0x0006696A
	public override void Awake()
	{
		base.Awake();
		this.m_transform = base.transform;
	}

	// Token: 0x0600185F RID: 6239 RVA: 0x0006877E File Offset: 0x0006697E
	public void FindWorldArea()
	{
		if (GameWorld.Instance)
		{
			this.WorldArea = GameWorld.Instance.WorldAreaAtPosition(this.m_transform.position);
		}
		if (this.WorldArea == null)
		{
		}
	}

	// Token: 0x06001860 RID: 6240 RVA: 0x000687BB File Offset: 0x000669BB
	public void Start()
	{
		if (this.WorldArea == null)
		{
			this.FindWorldArea();
		}
	}

	// Token: 0x17000447 RID: 1095
	// (get) Token: 0x06001861 RID: 6241 RVA: 0x000687D4 File Offset: 0x000669D4
	public bool OriHasTargets
	{
		get
		{
			SeinSpiritFlameTargetting spiritFlameTargetting = Characters.Sein.Abilities.SpiritFlameTargetting;
			return spiritFlameTargetting && spiritFlameTargetting.ClosestAttackables.Count > 0;
		}
	}

	// Token: 0x06001862 RID: 6242 RVA: 0x0006880C File Offset: 0x00066A0C
	public void Highlight()
	{
		if (this.OriTarget)
		{
			Characters.Ori.MoveOriToPosition(this.OriTarget.position, this.OriDuration);
		}
		if (Characters.Sein.Abilities.SpiritFlame)
		{
			Characters.Sein.Abilities.SpiritFlame.AddLock("mapStone");
		}
		Characters.Ori.GetComponent<Rigidbody>().velocity = Vector3.zero;
		Characters.Ori.EnableHoverWobbling = false;
		Characters.Ori.InsideMapstone = true;
		if (this.m_hint == null)
		{
			this.m_hint = UI.Hints.Show(this.HintMessage, HintLayer.HintZone, 3f);
		}
		if (this.OriEnterAction)
		{
			this.OriEnterAction.Perform(null);
		}
	}

	// Token: 0x06001863 RID: 6243 RVA: 0x000688E4 File Offset: 0x00066AE4
	public void Unhighlight()
	{
		Characters.Ori.ChangeState(Ori.State.Hovering);
		Characters.Ori.EnableHoverWobbling = true;
		Characters.Ori.InsideMapstone = false;
		if (Characters.Sein.Abilities.SpiritFlame)
		{
			Characters.Sein.Abilities.SpiritFlame.RemoveLock("mapStone");
		}
		if (this.OriExitAction)
		{
			this.OriExitAction.Perform(null);
		}
		if (this.m_hint)
		{
			this.m_hint.HideMessageScreen();
		}
	}

	// Token: 0x06001864 RID: 6244 RVA: 0x0006897B File Offset: 0x00066B7B
	public void OnDisable()
	{
		if (this.CurrentState == MapStone.State.Highlighted)
		{
			this.CurrentState = MapStone.State.Normal;
			this.Unhighlight();
		}
	}

	// Token: 0x17000448 RID: 1096
	// (get) Token: 0x06001865 RID: 6245 RVA: 0x00068996 File Offset: 0x00066B96
	public bool Activated
	{
		get
		{
			return this.CurrentState == MapStone.State.Activated;
		}
	}

	// Token: 0x06001866 RID: 6246 RVA: 0x000689A1 File Offset: 0x00066BA1
	public override void Serialize(Archive ar)
	{
		this.CurrentState = (MapStone.State)ar.Serialize((int)this.CurrentState);
	}

	// Token: 0x17000449 RID: 1097
	// (get) Token: 0x06001867 RID: 6247 RVA: 0x000689B5 File Offset: 0x00066BB5
	public float DistanceToSein
	{
		get
		{
			return Vector3.Distance(this.m_transform.position, Characters.Sein.Position);
		}
	}

	// Token: 0x06001868 RID: 6248 RVA: 0x000689D4 File Offset: 0x00066BD4
	public void FixedUpdate()
	{
		MapStone.State currentState = this.CurrentState;
		if (currentState != MapStone.State.Normal)
		{
			if (currentState == MapStone.State.Highlighted)
			{
				if (this.DistanceToSein > this.Radius || this.OriHasTargets || !Characters.Sein.IsOnGround)
				{
					this.Unhighlight();
					this.CurrentState = MapStone.State.Normal;
				}
				if (Characters.Sein.Controller.CanMove && !Characters.Sein.IsSuspended && Core.Input.SpiritFlame.OnPressed)
				{
					if (Characters.Sein.Inventory.MapStones > 0)
					{
						Characters.Sein.Inventory.MapStones--;
						if (this.OnOpenedAction)
						{
							this.OnOpenedAction.Perform(null);
						}
						AchievementsLogic.Instance.OnMapStoneActivated();
						this.CurrentState = MapStone.State.Activated;
					}
					else
					{
						UI.SeinUI.ShakeMapstones();
						if (this.OnFailAction)
						{
							this.OnFailAction.Perform(null);
						}
					}
				}
			}
		}
		else if (this.DistanceToSein < this.Radius && !this.OriHasTargets && Characters.Sein.IsOnGround)
		{
			this.Highlight();
			this.CurrentState = MapStone.State.Highlighted;
		}
	}

	// Token: 0x040014E8 RID: 5352
	public Transform OriTarget;

	// Token: 0x040014E9 RID: 5353
	public Color OriHoverColor;

	// Token: 0x040014EA RID: 5354
	public float Radius = 2f;

	// Token: 0x040014EB RID: 5355
	private Transform m_transform;

	// Token: 0x040014EC RID: 5356
	public GameWorldArea WorldArea;

	// Token: 0x040014ED RID: 5357
	public Texture2D HintTexture;

	// Token: 0x040014EE RID: 5358
	public MessageProvider HintMessage;

	// Token: 0x040014EF RID: 5359
	private MessageBox m_hint;

	// Token: 0x040014F0 RID: 5360
	public ActionMethod OriEnterAction;

	// Token: 0x040014F1 RID: 5361
	public ActionMethod OriExitAction;

	// Token: 0x040014F2 RID: 5362
	public ActionMethod OnOpenedAction;

	// Token: 0x040014F3 RID: 5363
	public ActionMethod OnFailAction;

	// Token: 0x040014F4 RID: 5364
	public float OriDuration = 1f;

	// Token: 0x040014F5 RID: 5365
	public MapStone.State CurrentState;

	// Token: 0x020008EE RID: 2286
	public enum State
	{
		// Token: 0x04002DF9 RID: 11769
		Normal,
		// Token: 0x04002DFA RID: 11770
		Highlighted,
		// Token: 0x04002DFB RID: 11771
		Activated
	}
}
