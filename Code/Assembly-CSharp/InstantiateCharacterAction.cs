using System;
using Game;
using UnityEngine;

// Token: 0x020002F0 RID: 752
[Category("General")]
public class InstantiateCharacterAction : ActionMethod
{
	// Token: 0x060016B1 RID: 5809 RVA: 0x00063400 File Offset: 0x00061600
	public override void Perform(IContext context)
	{
		CharacterFactory.Instance.SpawnCharacter(this.Character, null, this.Position.position, new Action(this.AfterLoad));
		GameController.Instance.MainMenuCanBeOpened = true;
	}

	// Token: 0x060016B2 RID: 5810 RVA: 0x00063444 File Offset: 0x00061644
	public void AfterLoad()
	{
		Characters.Current.Position = this.Position.position;
		Characters.Current.PlaceOnGround();
		Characters.Current.Activate(true);
	}

	// Token: 0x060016B3 RID: 5811 RVA: 0x0006347C File Offset: 0x0006167C
	public override string GetNiceName()
	{
		if (this.Position)
		{
			return "Instantiate " + ActionHelper.GetName(this.Prefab) + " at " + ActionHelper.GetName(this.Position);
		}
		return "Instantiate " + ActionHelper.GetName(this.Prefab);
	}

	// Token: 0x04001391 RID: 5009
	public Transform Position;

	// Token: 0x04001392 RID: 5010
	[NotNull]
	public GameObject Prefab;

	// Token: 0x04001393 RID: 5011
	public CharacterFactory.Characters Character;
}
