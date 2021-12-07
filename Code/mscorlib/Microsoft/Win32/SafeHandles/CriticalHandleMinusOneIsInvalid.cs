using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a base class for Win32 critical handle implementations in which the value of -1 indicates an invalid handle.</summary>
	// Token: 0x02000073 RID: 115
	public abstract class CriticalHandleMinusOneIsInvalid : CriticalHandle, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.CriticalHandleMinusOneIsInvalid" /> class.</summary>
		// Token: 0x06000776 RID: 1910 RVA: 0x00017C10 File Offset: 0x00015E10
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalHandleMinusOneIsInvalid() : base((IntPtr)(-1))
		{
		}

		/// <summary>Gets a value that indicates whether the handle is invalid.</summary>
		/// <returns>true if the handle is not valid; otherwise, false.</returns>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x00017C20 File Offset: 0x00015E20
		public override bool IsInvalid
		{
			get
			{
				return this.handle == (IntPtr)(-1);
			}
		}
	}
}
