using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Threading
{
	/// <summary>Represents a thread synchronization event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200069D RID: 1693
	[ComVisible(true)]
	public class EventWaitHandle : WaitHandle
	{
		// Token: 0x06004068 RID: 16488 RVA: 0x000DEE54 File Offset: 0x000DD054
		private EventWaitHandle(IntPtr handle)
		{
			this.Handle = handle;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.EventWaitHandle" /> class, specifying whether the wait handle is initially signaled, and whether it resets automatically or manually.</summary>
		/// <param name="initialState">true to set the initial state to signaled; false to set it to nonsignaled.</param>
		/// <param name="mode">One of the <see cref="T:System.Threading.EventResetMode" /> values that determines whether the event resets automatically or manually.</param>
		// Token: 0x06004069 RID: 16489 RVA: 0x000DEE64 File Offset: 0x000DD064
		public EventWaitHandle(bool initialState, EventResetMode mode)
		{
			bool manual = this.IsManualReset(mode);
			bool flag;
			this.Handle = NativeEventCalls.CreateEvent_internal(manual, initialState, null, out flag);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.EventWaitHandle" /> class, specifying whether the wait handle is initially signaled if created as a result of this call, whether it resets automatically or manually, and the name of a system synchronization event.</summary>
		/// <param name="initialState">true to set the initial state to signaled if the named event is created as a result of this call; false to set it to nonsignaled.</param>
		/// <param name="mode">One of the <see cref="T:System.Threading.EventResetMode" /> values that determines whether the event resets automatically or manually.</param>
		/// <param name="name">The name of a system-wide synchronization event.</param>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named event exists and has access control security, but the user does not have <see cref="F:System.Security.AccessControl.EventWaitHandleRights.FullControl" />.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named event cannot be created, perhaps because a wait handle of a different type has the same name.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is longer than 260 characters.</exception>
		// Token: 0x0600406A RID: 16490 RVA: 0x000DEE90 File Offset: 0x000DD090
		public EventWaitHandle(bool initialState, EventResetMode mode, string name)
		{
			bool manual = this.IsManualReset(mode);
			bool flag;
			this.Handle = NativeEventCalls.CreateEvent_internal(manual, initialState, name, out flag);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.EventWaitHandle" /> class, specifying whether the wait handle is initially signaled if created as a result of this call, whether it resets automatically or manually, the name of a system synchronization event, and a Boolean variable whose value after the call indicates whether the named system event was created.</summary>
		/// <param name="initialState">true to set the initial state to signaled if the named event is created as a result of this call; false to set it to nonsignaled.</param>
		/// <param name="mode">One of the <see cref="T:System.Threading.EventResetMode" /> values that determines whether the event resets automatically or manually.</param>
		/// <param name="name">The name of a system-wide synchronization event.</param>
		/// <param name="createdNew">When this method returns, contains true if a local event was created (that is, if <paramref name="name" /> is null or an empty string) or if the specified named system event was created; false if the specified named system event already existed. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named event exists and has access control security, but the user does not have <see cref="F:System.Security.AccessControl.EventWaitHandleRights.FullControl" />.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named event cannot be created, perhaps because a wait handle of a different type has the same name.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is longer than 260 characters.</exception>
		// Token: 0x0600406B RID: 16491 RVA: 0x000DEEBC File Offset: 0x000DD0BC
		public EventWaitHandle(bool initialState, EventResetMode mode, string name, out bool createdNew)
		{
			bool manual = this.IsManualReset(mode);
			this.Handle = NativeEventCalls.CreateEvent_internal(manual, initialState, name, out createdNew);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.EventWaitHandle" /> class, specifying whether the wait handle is initially signaled if created as a result of this call, whether it resets automatically or manually, the name of a system synchronization event, a Boolean variable whose value after the call indicates whether the named system event was created, and the access control security to be applied to the named event if it is created.</summary>
		/// <param name="initialState">true to set the initial state to signaled if the named event is created as a result of this call; false to set it to nonsignaled.</param>
		/// <param name="mode">One of the <see cref="T:System.Threading.EventResetMode" /> values that determines whether the event resets automatically or manually.</param>
		/// <param name="name">The name of a system-wide synchronization event.</param>
		/// <param name="createdNew">When this method returns, contains true if a local event was created (that is, if <paramref name="name" /> is null or an empty string) or if the specified named system event was created; false if the specified named system event already existed. This parameter is passed uninitialized.</param>
		/// <param name="eventSecurity">An <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" /> object that represents the access control security to be applied to the named system event.</param>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named event exists and has access control security, but the user does not have <see cref="F:System.Security.AccessControl.EventWaitHandleRights.FullControl" />.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named event cannot be created, perhaps because a wait handle of a different type has the same name.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is longer than 260 characters.</exception>
		// Token: 0x0600406C RID: 16492 RVA: 0x000DEEE8 File Offset: 0x000DD0E8
		[MonoTODO("Implement access control")]
		public EventWaitHandle(bool initialState, EventResetMode mode, string name, out bool createdNew, EventWaitHandleSecurity eventSecurity)
		{
			bool manual = this.IsManualReset(mode);
			this.Handle = NativeEventCalls.CreateEvent_internal(manual, initialState, name, out createdNew);
		}

		// Token: 0x0600406D RID: 16493 RVA: 0x000DEF14 File Offset: 0x000DD114
		private bool IsManualReset(EventResetMode mode)
		{
			if (mode < EventResetMode.AutoReset || mode > EventResetMode.ManualReset)
			{
				throw new ArgumentException("mode");
			}
			return mode == EventResetMode.ManualReset;
		}

		/// <summary>Gets an <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" /> object that represents the access control security for the named system event represented by the current <see cref="T:System.Threading.EventWaitHandle" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" /> object that represents the access control security for the named system event.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The current <see cref="T:System.Threading.EventWaitHandle" /> object represents a named system event, and the user does not have <see cref="F:System.Security.AccessControl.EventWaitHandleRights.ReadPermissions" />.-or-The current <see cref="T:System.Threading.EventWaitHandle" /> object represents a named system event, and was not opened with <see cref="F:System.Security.AccessControl.EventWaitHandleRights.ReadPermissions" />.</exception>
		/// <exception cref="T:System.NotSupportedException">Not supported for Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="M:System.Threading.EventWaitHandle.Close" /> method was previously called on this <see cref="T:System.Threading.EventWaitHandle" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600406E RID: 16494 RVA: 0x000DEF34 File Offset: 0x000DD134
		[MonoTODO]
		public EventWaitHandleSecurity GetAccessControl()
		{
			throw new NotImplementedException();
		}

		/// <summary>Opens an existing named synchronization event.</summary>
		/// <returns>A <see cref="T:System.Threading.EventWaitHandle" /> object that represents the named system event.</returns>
		/// <param name="name">The name of a system synchronization event.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string. -or-<paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named system event does not exist.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named event exists, but the user does not have the security access required to use it.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600406F RID: 16495 RVA: 0x000DEF3C File Offset: 0x000DD13C
		public static EventWaitHandle OpenExisting(string name)
		{
			return EventWaitHandle.OpenExisting(name, EventWaitHandleRights.Modify | EventWaitHandleRights.Synchronize);
		}

		/// <summary>Opens an existing named synchronization event, specifying the desired security access.</summary>
		/// <returns>A <see cref="T:System.Threading.EventWaitHandle" /> object that represents the named system event.</returns>
		/// <param name="name">The name of a system-wide synchronization event.</param>
		/// <param name="rights">A combination of <see cref="T:System.Security.AccessControl.EventWaitHandleRights" />  values that represent the desired security access.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.-or-<paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named system event does not exist.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named event exists, but the user does not have the desired security access.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06004070 RID: 16496 RVA: 0x000DEF4C File Offset: 0x000DD14C
		public static EventWaitHandle OpenExisting(string name, EventWaitHandleRights rights)
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
			IntPtr intPtr = NativeEventCalls.OpenEvent_internal(name, rights, out monoIOError);
			if (!(intPtr == (IntPtr)null))
			{
				return new EventWaitHandle(intPtr);
			}
			if (monoIOError == MonoIOError.ERROR_FILE_NOT_FOUND)
			{
				throw new WaitHandleCannotBeOpenedException(Locale.GetText("Named Event handle does not exist: ") + name);
			}
			if (monoIOError == MonoIOError.ERROR_ACCESS_DENIED)
			{
				throw new UnauthorizedAccessException();
			}
			throw new IOException(Locale.GetText("Win32 IO error: ") + monoIOError.ToString());
		}

		/// <summary>Sets the state of the event to nonsignaled, causing threads to block.</summary>
		/// <returns>true if the operation succeeds; otherwise, false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="M:System.Threading.EventWaitHandle.Close" /> method was previously called on this <see cref="T:System.Threading.EventWaitHandle" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004071 RID: 16497 RVA: 0x000DF008 File Offset: 0x000DD208
		public bool Reset()
		{
			base.CheckDisposed();
			return NativeEventCalls.ResetEvent_internal(this.Handle);
		}

		/// <summary>Sets the state of the event to signaled, allowing one or more waiting threads to proceed.</summary>
		/// <returns>true if the operation succeeds; otherwise, false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="M:System.Threading.EventWaitHandle.Close" /> method was previously called on this <see cref="T:System.Threading.EventWaitHandle" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004072 RID: 16498 RVA: 0x000DF01C File Offset: 0x000DD21C
		public bool Set()
		{
			base.CheckDisposed();
			return NativeEventCalls.SetEvent_internal(this.Handle);
		}

		/// <summary>Sets the access control security for a named system event.</summary>
		/// <param name="eventSecurity">An <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" />  object that represents the access control security to be applied to the named system event.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="eventSecurity" /> is null.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have <see cref="F:System.Security.AccessControl.EventWaitHandleRights.ChangePermissions" />.-or-The event was not opened with <see cref="F:System.Security.AccessControl.EventWaitHandleRights.ChangePermissions" />.</exception>
		/// <exception cref="T:System.SystemException">The current <see cref="T:System.Threading.EventWaitHandle" /> object does not represent a named system event.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06004073 RID: 16499 RVA: 0x000DF030 File Offset: 0x000DD230
		[MonoTODO]
		public void SetAccessControl(EventWaitHandleSecurity eventSecurity)
		{
			throw new NotImplementedException();
		}
	}
}
