using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a base class for Win32 safe handle implementations in which the value of either 0 or -1 indicates an invalid handle.</summary>
	// Token: 0x02000074 RID: 116
	public abstract class SafeHandleZeroOrMinusOneIsInvalid : SafeHandle, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid" /> class, specifying whether the handle is to be reliably released. </summary>
		/// <param name="ownsHandle">true to reliably release the handle during the finalization phase; false to prevent reliable release (not recommended).</param>
		// Token: 0x06000778 RID: 1912 RVA: 0x00017C34 File Offset: 0x00015E34
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected SafeHandleZeroOrMinusOneIsInvalid(bool ownsHandle) : base((IntPtr)0, ownsHandle)
		{
		}

		/// <summary>Gets a value that indicates whether the handle is invalid.</summary>
		/// <returns>true if the handle is not valid; otherwise, false.</returns>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00017C44 File Offset: 0x00015E44
		public override bool IsInvalid
		{
			get
			{
				return this.handle == (IntPtr)(-1) || this.handle == (IntPtr)0;
			}
		}
	}
}
