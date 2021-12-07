using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>A synchronization primitive that can also be used for interprocess synchronization. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020006A6 RID: 1702
	[ComVisible(true)]
	public sealed class Mutex : WaitHandle
	{
		// Token: 0x060040BE RID: 16574 RVA: 0x000DF6D8 File Offset: 0x000DD8D8
		private Mutex(IntPtr handle)
		{
			this.Handle = handle;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Mutex" /> class with default properties.</summary>
		// Token: 0x060040BF RID: 16575 RVA: 0x000DF6E8 File Offset: 0x000DD8E8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public Mutex()
		{
			bool flag;
			this.Handle = Mutex.CreateMutex_internal(false, null, out flag);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Mutex" /> class with a Boolean value that indicates whether the calling thread should have initial ownership of the mutex.</summary>
		/// <param name="initiallyOwned">true to give the calling thread initial ownership of the mutex; otherwise, false. </param>
		// Token: 0x060040C0 RID: 16576 RVA: 0x000DF70C File Offset: 0x000DD90C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public Mutex(bool initiallyOwned)
		{
			bool flag;
			this.Handle = Mutex.CreateMutex_internal(initiallyOwned, null, out flag);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Mutex" /> class with a Boolean value that indicates whether the calling thread should have initial ownership of the mutex, and a string that is the name of the mutex.</summary>
		/// <param name="initiallyOwned">true to give the calling thread initial ownership of the named system mutex if the named system mutex is created as a result of this call; otherwise, false. </param>
		/// <param name="name">The name of the <see cref="T:System.Threading.Mutex" />. If the value is null, the <see cref="T:System.Threading.Mutex" /> is unnamed. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The named mutex exists and has access control security, but the user does not have <see cref="F:System.Security.AccessControl.MutexRights.FullControl" />.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.ApplicationException">The named mutex cannot be created, perhaps because a wait handle of a different type has the same name.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is longer than 260 characters.</exception>
		// Token: 0x060040C1 RID: 16577 RVA: 0x000DF730 File Offset: 0x000DD930
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		public Mutex(bool initiallyOwned, string name)
		{
			bool flag;
			this.Handle = Mutex.CreateMutex_internal(initiallyOwned, name, out flag);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Mutex" /> class with a Boolean value that indicates whether the calling thread should have initial ownership of the mutex, a string that is the name of the mutex, and a Boolean value that, when the method returns, indicates whether the calling thread was granted initial ownership of the mutex.</summary>
		/// <param name="initiallyOwned">true to give the calling thread initial ownership of the named system mutex if the named system mutex is created as a result of this call; otherwise, false. </param>
		/// <param name="name">The name of the <see cref="T:System.Threading.Mutex" />. If the value is null, the <see cref="T:System.Threading.Mutex" /> is unnamed. </param>
		/// <param name="createdNew">When this method returns, contains a Boolean that is true if a local mutex was created (that is, if <paramref name="name" /> is null or an empty string) or if the specified named system mutex was created; false if the specified named system mutex already existed. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The named mutex exists and has access control security, but the user does not have <see cref="F:System.Security.AccessControl.MutexRights.FullControl" />.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.ApplicationException">The named mutex cannot be created, perhaps because a wait handle of a different type has the same name.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is longer than 260 characters.</exception>
		// Token: 0x060040C2 RID: 16578 RVA: 0x000DF754 File Offset: 0x000DD954
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		public Mutex(bool initiallyOwned, string name, out bool createdNew)
		{
			this.Handle = Mutex.CreateMutex_internal(initiallyOwned, name, out createdNew);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Mutex" /> class with a Boolean value that indicates whether the calling thread should have initial ownership of the mutex, a string that is the name of the mutex, a Boolean variable that, when the method returns, indicates whether the calling thread was granted initial ownership of the mutex, and the access control security to be applied to the named mutex.</summary>
		/// <param name="initiallyOwned">true to give the calling thread initial ownership of the named system mutex if the named system mutex is created as a result of this call; otherwise, false. </param>
		/// <param name="name">The name of the system mutex. If the value is null, the <see cref="T:System.Threading.Mutex" /> is unnamed. </param>
		/// <param name="createdNew">When this method returns, contains a Boolean that is true if a local mutex was created (that is, if <paramref name="name" /> is null or an empty string) or if the specified named system mutex was created; false if the specified named system mutex already existed. This parameter is passed uninitialized. </param>
		/// <param name="mutexSecurity">A <see cref="T:System.Security.AccessControl.MutexSecurity" />  object that represents the access control security to be applied to the named system mutex.</param>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named mutex exists and has access control security, but the user does not have <see cref="F:System.Security.AccessControl.MutexRights.FullControl" />.</exception>
		/// <exception cref="T:System.ApplicationException">The named mutex cannot be created, perhaps because a wait handle of a different type has the same name.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is longer than 260 characters.</exception>
		// Token: 0x060040C3 RID: 16579 RVA: 0x000DF76C File Offset: 0x000DD96C
		[MonoTODO("Implement MutexSecurity")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public Mutex(bool initiallyOwned, string name, out bool createdNew, MutexSecurity mutexSecurity)
		{
			this.Handle = Mutex.CreateMutex_internal(initiallyOwned, name, out createdNew);
		}

		// Token: 0x060040C4 RID: 16580
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateMutex_internal(bool initiallyOwned, string name, out bool created);

		// Token: 0x060040C5 RID: 16581
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ReleaseMutex_internal(IntPtr handle);

		// Token: 0x060040C6 RID: 16582
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr OpenMutex_internal(string name, MutexRights rights, out MonoIOError error);

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.MutexSecurity" /> object that represents the access control security for the named mutex.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.MutexSecurity" /> object that represents the access control security for the named mutex.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The current <see cref="T:System.Threading.Mutex" /> object represents a named system mutex, but the user does not have <see cref="F:System.Security.AccessControl.MutexRights.ReadPermissions" />.-or-The current <see cref="T:System.Threading.Mutex" /> object represents a named system mutex, and was not opened with <see cref="F:System.Security.AccessControl.MutexRights.ReadPermissions" />.</exception>
		/// <exception cref="T:System.NotSupportedException">Not supported for Windows 98 or Windows Millennium Edition.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060040C7 RID: 16583 RVA: 0x000DF784 File Offset: 0x000DD984
		public MutexSecurity GetAccessControl()
		{
			throw new NotImplementedException();
		}

		/// <summary>Opens an existing named mutex.</summary>
		/// <returns>A <see cref="T:System.Threading.Mutex" /> object that represents a named system mutex.</returns>
		/// <param name="name">The name of a system-wide named mutex object.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is a zero-length string.-or-<paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named mutex does not exist.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named mutex exists, but the user does not have the security access required to use it.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060040C8 RID: 16584 RVA: 0x000DF78C File Offset: 0x000DD98C
		public static Mutex OpenExisting(string name)
		{
			return Mutex.OpenExisting(name, MutexRights.Modify | MutexRights.Synchronize);
		}

		/// <summary>Open an existing named mutex, specifying the desired security access.</summary>
		/// <returns>A <see cref="T:System.Threading.Mutex" /> object that represents the named system mutex.</returns>
		/// <param name="name">The name of a system mutex.</param>
		/// <param name="rights">A combination of <see cref="T:System.Security.AccessControl.MutexRights" />  values that represent the desired security access.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string. -or-<paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named mutex does not exist.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named mutex exists, and the user does not have the desired security access.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060040C9 RID: 16585 RVA: 0x000DF79C File Offset: 0x000DD99C
		public static Mutex OpenExisting(string name, MutexRights rights)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0 || name.Length > 260)
			{
				throw new ArgumentException("name", Locale.GetText("Invalid length [1-260]."));
			}
			MonoIOError monoIOError;
			IntPtr intPtr = Mutex.OpenMutex_internal(name, rights, out monoIOError);
			if (!(intPtr == (IntPtr)null))
			{
				return new Mutex(intPtr);
			}
			if (monoIOError == MonoIOError.ERROR_FILE_NOT_FOUND)
			{
				throw new WaitHandleCannotBeOpenedException(Locale.GetText("Named Mutex handle does not exist: ") + name);
			}
			if (monoIOError == MonoIOError.ERROR_ACCESS_DENIED)
			{
				throw new UnauthorizedAccessException();
			}
			throw new IOException(Locale.GetText("Win32 IO error: ") + monoIOError.ToString());
		}

		/// <summary>Releases the <see cref="T:System.Threading.Mutex" /> once.</summary>
		/// <exception cref="T:System.ApplicationException">The calling thread does not own the mutex. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060040CA RID: 16586 RVA: 0x000DF858 File Offset: 0x000DDA58
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void ReleaseMutex()
		{
			if (!Mutex.ReleaseMutex_internal(this.Handle))
			{
				throw new ApplicationException("Mutex is not owned");
			}
		}

		/// <summary>Sets the access control security for a named system mutex.</summary>
		/// <param name="mutexSecurity">A <see cref="T:System.Security.AccessControl.MutexSecurity" />  object that represents the access control security to be applied to the named system mutex.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mutexSecurity" /> is null.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have <see cref="F:System.Security.AccessControl.MutexRights.ChangePermissions" />.-or-The mutex was not opened with <see cref="F:System.Security.AccessControl.MutexRights.ChangePermissions" />.</exception>
		/// <exception cref="T:System.SystemException">The current <see cref="T:System.Threading.Mutex" /> object does not represent a named system mutex.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060040CB RID: 16587 RVA: 0x000DF884 File Offset: 0x000DDA84
		public void SetAccessControl(MutexSecurity mutexSecurity)
		{
			throw new NotImplementedException();
		}
	}
}
