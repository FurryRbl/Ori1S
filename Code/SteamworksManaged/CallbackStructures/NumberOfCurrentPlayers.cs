using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200005F RID: 95
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct NumberOfCurrentPlayers
	{
		// Token: 0x0600032D RID: 813 RVA: 0x00006C10 File Offset: 0x00004E10
		internal static NumberOfCurrentPlayers Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<NumberOfCurrentPlayers>(data, dataSize);
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00006C19 File Offset: 0x00004E19
		public byte Success
		{
			get
			{
				return this.success;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00006C21 File Offset: 0x00004E21
		public int NumberOfPlayers
		{
			get
			{
				return this.numberOfPlayers;
			}
		}

		// Token: 0x040001BC RID: 444
		private byte success;

		// Token: 0x040001BD RID: 445
		private int numberOfPlayers;
	}
}
