using System;
using System.Collections.Generic;
using Game;

// Token: 0x020002CE RID: 718
public class ChangeSoundProviderInMetaDataSoundPlayerAction : ActionMethod
{
	// Token: 0x06001645 RID: 5701 RVA: 0x000623FC File Offset: 0x000605FC
	public override void Perform(IContext context)
	{
		if (this.ShouldUseSeinsMetaDataBasedSoundPlayer)
		{
			this.MetaDataBasedSoundPlayer = Characters.Sein.gameObject.GetComponentInChildren<AnimationMetaDataBasedSoundPlayer>();
		}
		if (this.MetaDataBasedSoundPlayer != null)
		{
			this.MetaDataBasedSoundPlayer.NodeSoundPlayerPairs = this.NodeSoundPlayerPair;
		}
	}

	// Token: 0x04001347 RID: 4935
	public List<NodeSoundPlayerPair> NodeSoundPlayerPair = new List<NodeSoundPlayerPair>();

	// Token: 0x04001348 RID: 4936
	public AnimationMetaDataBasedSoundPlayer MetaDataBasedSoundPlayer;

	// Token: 0x04001349 RID: 4937
	public bool ShouldUseSeinsMetaDataBasedSoundPlayer;
}
