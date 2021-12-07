using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000140 RID: 320
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct PersonaStateChange
	{
		// Token: 0x06000B71 RID: 2929 RVA: 0x0000FA06 File Offset: 0x0000DC06
		internal static PersonaStateChange Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<PersonaStateChange>(data, dataSize);
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0000FA0F File Offset: 0x0000DC0F
		public SteamID SteamID
		{
			get
			{
				return this.steamID;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0000FA17 File Offset: 0x0000DC17
		public PersonaChange ChangeFlags
		{
			get
			{
				return (PersonaChange)this.changeFlags;
			}
		}

		// Token: 0x040005C4 RID: 1476
		private SteamID steamID;

		// Token: 0x040005C5 RID: 1477
		private int changeFlags;
	}
}
