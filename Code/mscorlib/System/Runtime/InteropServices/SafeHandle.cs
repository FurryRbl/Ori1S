using System;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace System.Runtime.InteropServices
{
	/// <summary>Represents a wrapper class for operating system handles. This class must be inherited.</summary>
	// Token: 0x020003BB RID: 955
	public abstract class SafeHandle : CriticalFinalizerObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.SafeHandle" /> class with the specified invalid handle value.</summary>
		/// <param name="invalidHandleValue">The value of an invalid handle (usually 0 or -1).  Your implementation of <see cref="P:System.Runtime.InteropServices.SafeHandle.IsInvalid" /> should return true for this value.</param>
		/// <param name="ownsHandle">true to reliably let <see cref="T:System.Runtime.InteropServices.SafeHandle" /> release the handle during the finalization phase; otherwise, false (not recommended). </param>
		/// <exception cref="T:System.TypeLoadException">The derived class resides in an assembly without unmanaged code access permission. </exception>
		// Token: 0x06002B71 RID: 11121 RVA: 0x000939A4 File Offset: 0x00091BA4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected SafeHandle(IntPtr invalidHandleValue, bool ownsHandle)
		{
			this.invalid_handle_value = invalidHandleValue;
			this.owns_handle = ownsHandle;
			this.refcount = 1;
		}

		/// <summary>Marks the handle for releasing and freeing resources.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B72 RID: 11122 RVA: 0x000939C4 File Offset: 0x00091BC4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Close()
		{
			if (this.refcount == 0)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			int num;
			int num2;
			do
			{
				num = this.refcount;
				num2 = num - 1;
			}
			while (Interlocked.CompareExchange(ref this.refcount, num2, num) != num);
			if (num2 == 0 && this.owns_handle && !this.IsInvalid)
			{
				this.ReleaseHandle();
				this.handle = this.invalid_handle_value;
				this.refcount = -1;
			}
		}

		/// <summary>Manually increments the reference counter on <see cref="T:System.Runtime.InteropServices.SafeHandle" /> instances.</summary>
		/// <param name="success">true if the reference counter was successfully incremented; otherwise, false.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B73 RID: 11123 RVA: 0x00093A44 File Offset: 0x00091C44
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void DangerousAddRef(ref bool success)
		{
			if (this.refcount <= 0)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			for (;;)
			{
				int num = this.refcount;
				int value = num + 1;
				if (num <= 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.refcount, value, num) == num)
				{
					goto Block_3;
				}
			}
			throw new ObjectDisposedException(base.GetType().FullName);
			Block_3:
			success = true;
		}

		/// <summary>Returns the value of the <see cref="F:System.Runtime.InteropServices.SafeHandle.handle" /> field.</summary>
		/// <returns>An IntPtr representing the value of the <see cref="F:System.Runtime.InteropServices.SafeHandle.handle" /> field. If the handle has been marked invalid with <see cref="M:System.Runtime.InteropServices.SafeHandle.SetHandleAsInvalid" />, this method still returns the original handle value, which can be a stale value.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B74 RID: 11124 RVA: 0x00093AA8 File Offset: 0x00091CA8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public IntPtr DangerousGetHandle()
		{
			if (this.refcount <= 0)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			return this.handle;
		}

		/// <summary>Manually decrements the reference counter on a <see cref="T:System.Runtime.InteropServices.SafeHandle" /> instance.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B75 RID: 11125 RVA: 0x00093AD0 File Offset: 0x00091CD0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void DangerousRelease()
		{
			if (this.refcount <= 0)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			int num;
			int num2;
			do
			{
				num = this.refcount;
				num2 = num - 1;
			}
			while (Interlocked.CompareExchange(ref this.refcount, num2, num) != num);
			if (num2 == 0 && this.owns_handle && !this.IsInvalid)
			{
				this.ReleaseHandle();
				this.handle = this.invalid_handle_value;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Runtime.InteropServices.SafeHandle" /> class.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B76 RID: 11126 RVA: 0x00093B48 File Offset: 0x00091D48
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Marks a handle as no longer used.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B77 RID: 11127 RVA: 0x00093B58 File Offset: 0x00091D58
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void SetHandleAsInvalid()
		{
			this.handle = this.invalid_handle_value;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Runtime.InteropServices.SafeHandle" /> class specifying whether to perform a normal dispose operation.</summary>
		/// <param name="disposing">true for a normal dispose operation; false to finalize the handle.</param>
		// Token: 0x06002B78 RID: 11128 RVA: 0x00093B68 File Offset: 0x00091D68
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		/// <summary>When overridden in a derived class, executes the code required to free the handle.</summary>
		/// <returns>true if the handle is released successfully; otherwise, in the event of a catastrophic failure, false. In this case, it generates a releaseHandleFailed MDA Managed Debugging Assistant.</returns>
		// Token: 0x06002B79 RID: 11129
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected abstract bool ReleaseHandle();

		/// <summary>Sets the handle to the specified pre-existing handle.</summary>
		/// <param name="handle">The pre-existing handle to use. </param>
		// Token: 0x06002B7A RID: 11130 RVA: 0x00093B7C File Offset: 0x00091D7C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected void SetHandle(IntPtr handle)
		{
			this.handle = handle;
		}

		/// <summary>Gets a value indicating whether the handle is closed.</summary>
		/// <returns>true if the handle is closed; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06002B7B RID: 11131 RVA: 0x00093B88 File Offset: 0x00091D88
		public bool IsClosed
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.refcount <= 0;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the handle value is invalid.</summary>
		/// <returns>true if the handle value is invalid; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06002B7C RID: 11132
		public abstract bool IsInvalid { [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get; }

		/// <summary>Frees all resources associated with the handle.</summary>
		// Token: 0x06002B7D RID: 11133 RVA: 0x00093B98 File Offset: 0x00091D98
		~SafeHandle()
		{
			if (this.owns_handle && !this.IsInvalid)
			{
				this.ReleaseHandle();
				this.handle = this.invalid_handle_value;
			}
		}

		/// <summary>Specifies the handle to be wrapped.</summary>
		// Token: 0x04001195 RID: 4501
		protected IntPtr handle;

		// Token: 0x04001196 RID: 4502
		private IntPtr invalid_handle_value;

		// Token: 0x04001197 RID: 4503
		private int refcount;

		// Token: 0x04001198 RID: 4504
		private bool owns_handle;
	}
}
