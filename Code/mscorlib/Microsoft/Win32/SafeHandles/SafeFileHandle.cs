using System;
using System.IO;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Represents a wrapper class for a file handle. </summary>
	// Token: 0x02000076 RID: 118
	public sealed class SafeFileHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.SafeFileHandle" /> class. </summary>
		/// <param name="preexistingHandle">An <see cref="T:System.IntPtr" /> object that represents the pre-existing handle to use.</param>
		/// <param name="ownsHandle">true to reliably release the handle during the finalization phase; false to prevent reliable release (not recommended).</param>
		// Token: 0x0600077C RID: 1916 RVA: 0x00017CA0 File Offset: 0x00015EA0
		public SafeFileHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(preexistingHandle);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00017CB0 File Offset: 0x00015EB0
		internal SafeFileHandle() : base(true)
		{
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00017CBC File Offset: 0x00015EBC
		protected override bool ReleaseHandle()
		{
			MonoIOError monoIOError;
			MonoIO.Close(this.handle, out monoIOError);
			return monoIOError == MonoIOError.ERROR_SUCCESS;
		}
	}
}
