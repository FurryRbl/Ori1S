using System;

// Token: 0x02000426 RID: 1062
public static class CharacterStateExtension
{
	// Token: 0x06001DA8 RID: 7592 RVA: 0x00083104 File Offset: 0x00081304
	public static void SetStateActive(this CharacterState state, bool active)
	{
		if (state != null)
		{
			state.Active = active;
		}
	}
}
