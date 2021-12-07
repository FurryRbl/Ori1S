using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000012 RID: 18
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RegisterActivationCodeResponse
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00003414 File Offset: 0x00001614
		internal static RegisterActivationCodeResponse Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<RegisterActivationCodeResponse>(data, dataSize);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000341D File Offset: 0x0000161D
		public RegisterActivationCodeResult Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003425 File Offset: 0x00001625
		public uint PackageRegistered
		{
			get
			{
				return this.packadeRegistered;
			}
		}

		// Token: 0x0400004A RID: 74
		private RegisterActivationCodeResult result;

		// Token: 0x0400004B RID: 75
		private uint packadeRegistered;
	}
}
