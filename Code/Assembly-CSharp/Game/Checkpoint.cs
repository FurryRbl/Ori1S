using System;

namespace Game
{
	// Token: 0x0200007B RID: 123
	public static class Checkpoint
	{
		// Token: 0x04000417 RID: 1047
		public static SaveGameData SaveGameData = new SaveGameData();

		// Token: 0x0200007C RID: 124
		public static class Events
		{
			// Token: 0x04000418 RID: 1048
			public static UberDelegate OnPostRestore = new UberDelegate();

			// Token: 0x04000419 RID: 1049
			public static UberDelegate OnScrollLockPassed = new UberDelegate();

			// Token: 0x0400041A RID: 1050
			public static UberDelegate OnPostCreate = new UberDelegate();
		}
	}
}
