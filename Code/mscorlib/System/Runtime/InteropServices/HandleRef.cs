using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps a managed object holding a handle to a resource that is passed to unmanaged code using platform invoke.</summary>
	// Token: 0x02000395 RID: 917
	[ComVisible(true)]
	public struct HandleRef
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.HandleRef" /> class with the object to wrap and a handle to the resource used by unmanaged code.</summary>
		/// <param name="wrapper">A managed object that should not be finalized until the platform invoke call returns. </param>
		/// <param name="handle">An <see cref="T:System.IntPtr" /> that indicates a handle to a resource. </param>
		// Token: 0x06002A71 RID: 10865 RVA: 0x000928EC File Offset: 0x00090AEC
		public HandleRef(object wrapper, IntPtr handle)
		{
			this.wrapper = wrapper;
			this.handle = handle;
		}

		/// <summary>Gets the handle to a resource.</summary>
		/// <returns>The handle to a resource.</returns>
		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06002A72 RID: 10866 RVA: 0x000928FC File Offset: 0x00090AFC
		public IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		/// <summary>Gets the object holding the handle to a resource.</summary>
		/// <returns>The object holding the handle to a resource.</returns>
		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06002A73 RID: 10867 RVA: 0x00092904 File Offset: 0x00090B04
		public object Wrapper
		{
			get
			{
				return this.wrapper;
			}
		}

		/// <summary>Returns the internal integer representation of a <see cref="T:System.Runtime.InteropServices.HandleRef" /> object.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> object that represents a <see cref="T:System.Runtime.InteropServices.HandleRef" /> object.</returns>
		/// <param name="value">A <see cref="T:System.Runtime.InteropServices.HandleRef" /> object to retrieve an internal integer representation from.</param>
		// Token: 0x06002A74 RID: 10868 RVA: 0x0009290C File Offset: 0x00090B0C
		public static IntPtr ToIntPtr(HandleRef value)
		{
			return value.Handle;
		}

		/// <summary>Returns the handle to a resource of the specified <see cref="T:System.Runtime.InteropServices.HandleRef" /> object.</summary>
		/// <returns>The handle to a resource of the specified <see cref="T:System.Runtime.InteropServices.HandleRef" /> object.</returns>
		/// <param name="value">The object that needs a handle. </param>
		// Token: 0x06002A75 RID: 10869 RVA: 0x00092918 File Offset: 0x00090B18
		public static explicit operator IntPtr(HandleRef value)
		{
			return value.Handle;
		}

		// Token: 0x04001133 RID: 4403
		private object wrapper;

		// Token: 0x04001134 RID: 4404
		private IntPtr handle;
	}
}
