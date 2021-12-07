using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ManagedSteam.Utility;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x0200009A RID: 154
	internal class SteamParamStringArray : DisposableClass
	{
		// Token: 0x060004D4 RID: 1236 RVA: 0x00008E60 File Offset: 0x00007060
		public SteamParamStringArray(IList<string> values)
		{
			this.nativeStrings = new IntPtr[values.Count];
			for (int i = 0; i < values.Count; i++)
			{
				this.nativeStrings[i] = Marshal.StringToHGlobalAnsi(values[i]);
			}
			this.nativeDoubleStringArray = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * this.nativeStrings.Length);
			Marshal.Copy(this.nativeStrings, 0, this.nativeDoubleStringArray, this.nativeStrings.Length);
			SteamParamStringArray.NativeStruct nativeStruct = new SteamParamStringArray.NativeStruct
			{
				Strings = this.nativeDoubleStringArray,
				StringCount = this.nativeStrings.Length
			};
			this.nativeStructMemory = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SteamParamStringArray.NativeStruct)));
			Marshal.StructureToPtr(nativeStruct, this.nativeStructMemory, false);
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00008F41 File Offset: 0x00007141
		public IntPtr UnmanagedMemory
		{
			get
			{
				return this.nativeStructMemory;
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00008F4C File Offset: 0x0000714C
		protected override void CleanUpNativeResources()
		{
			Marshal.FreeHGlobal(this.nativeStructMemory);
			Marshal.FreeHGlobal(this.nativeDoubleStringArray);
			foreach (IntPtr hglobal in this.nativeStrings)
			{
				Marshal.FreeHGlobal(hglobal);
			}
			base.CleanUpNativeResources();
		}

		// Token: 0x040002C4 RID: 708
		private IntPtr nativeDoubleStringArray;

		// Token: 0x040002C5 RID: 709
		private IntPtr[] nativeStrings;

		// Token: 0x040002C6 RID: 710
		private IntPtr nativeStructMemory;

		// Token: 0x0200009B RID: 155
		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		private struct NativeStruct
		{
			// Token: 0x040002C7 RID: 711
			public IntPtr Strings;

			// Token: 0x040002C8 RID: 712
			public int StringCount;
		}
	}
}
