using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a base class for Win32 critical handle implementations in which the value of either 0 or -1 indicates an invalid handle.</summary>
	// Token: 0x02000072 RID: 114
	public abstract class CriticalHandleZeroOrMinusOneIsInvalid : CriticalHandle, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.CriticalHandleZeroOrMinusOneIsInvalid" /> class. </summary>
		// Token: 0x06000774 RID: 1908 RVA: 0x00017BD4 File Offset: 0x00015DD4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalHandleZeroOrMinusOneIsInvalid() : base((IntPtr)(-1))
		{
		}

		/// <summary>Gets a value that indicates whether the handle is invalid.</summary>
		/// <returns>true if the handle is not valid; otherwise, false.</returns>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x00017BE4 File Offset: 0x00015DE4
		public override bool IsInvalid
		{
			get
			{
				return this.handle == (IntPtr)(-1) || this.handle == IntPtr.Zero;
			}
		}
	}
}
