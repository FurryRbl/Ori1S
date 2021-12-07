using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Threading
{
	/// <summary>Creates and controls a thread, sets its priority, and gets its status.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020006AE RID: 1710
	[ComDefaultInterface(typeof(_Thread))]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public sealed class Thread : CriticalFinalizerObject, _Thread
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Thread" /> class.</summary>
		/// <param name="start">A <see cref="T:System.Threading.ThreadStart" /> delegate that represents the methods to be invoked when this thread begins executing. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="start" /> parameter is null. </exception>
		// Token: 0x06004112 RID: 16658 RVA: 0x000E06D4 File Offset: 0x000DE8D4
		public Thread(ThreadStart start)
		{
			if (start == null)
			{
				throw new ArgumentNullException("Null ThreadStart");
			}
			this.threadstart = start;
			this.Thread_init();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Thread" /> class, specifying the maximum stack size for the thread.</summary>
		/// <param name="start">A <see cref="T:System.Threading.ThreadStart" /> delegate that represents the methods to be invoked when this thread begins executing.</param>
		/// <param name="maxStackSize">The maximum stack size to be used by the thread, or 0 to use the default maximum stack size specified in the header for the executable.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="start" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxStackSize" /> is less than zero.</exception>
		// Token: 0x06004113 RID: 16659 RVA: 0x000E0714 File Offset: 0x000DE914
		public Thread(ThreadStart start, int maxStackSize)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			if (maxStackSize < 131072)
			{
				throw new ArgumentException("< 128 kb", "maxStackSize");
			}
			this.threadstart = start;
			this.stack_size = maxStackSize;
			this.Thread_init();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Thread" /> class, specifying a delegate that allows an object to be passed to the thread when the thread is started.</summary>
		/// <param name="start">A <see cref="T:System.Threading.ParameterizedThreadStart" /> delegate that represents the methods to be invoked when this thread begins executing.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="start" /> is null. </exception>
		// Token: 0x06004114 RID: 16660 RVA: 0x000E0778 File Offset: 0x000DE978
		public Thread(ParameterizedThreadStart start)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			this.threadstart = start;
			this.Thread_init();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Thread" /> class, specifying a delegate that allows an object to be passed to the thread when the thread is started and specifying the maximum stack size for the thread.</summary>
		/// <param name="start">A <see cref="T:System.Threading.ParameterizedThreadStart" /> delegate that represents the methods to be invoked when this thread begins executing.</param>
		/// <param name="maxStackSize">The maximum stack size to be used by the thread, or 0 to use the default maximum stack size specified in the header for the executable.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="start" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxStackSize" /> is less than zero.</exception>
		// Token: 0x06004115 RID: 16661 RVA: 0x000E07B8 File Offset: 0x000DE9B8
		public Thread(ParameterizedThreadStart start, int maxStackSize)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			if (maxStackSize < 131072)
			{
				throw new ArgumentException("< 128 kb", "maxStackSize");
			}
			this.threadstart = start;
			this.stack_size = maxStackSize;
			this.Thread_init();
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004117 RID: 16663 RVA: 0x000E0834 File Offset: 0x000DEA34
		void _Thread.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004118 RID: 16664 RVA: 0x000E083C File Offset: 0x000DEA3C
		void _Thread.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004119 RID: 16665 RVA: 0x000E0844 File Offset: 0x000DEA44
		void _Thread.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600411A RID: 16666 RVA: 0x000E084C File Offset: 0x000DEA4C
		void _Thread.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the current context in which the thread is executing.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Contexts.Context" /> representing the current thread context.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x0600411B RID: 16667 RVA: 0x000E0854 File Offset: 0x000DEA54
		public static Context CurrentContext
		{
			[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"Infrastructure\"/>\n</PermissionSet>\n")]
			get
			{
				return AppDomain.InternalGetContext();
			}
		}

		/// <summary>Gets or sets the thread's current principal (for role-based security).</summary>
		/// <returns>An <see cref="T:System.Security.Principal.IPrincipal" /> value representing the security context.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the permission required to set the principal. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x0600411C RID: 16668 RVA: 0x000E085C File Offset: 0x000DEA5C
		// (set) Token: 0x0600411D RID: 16669 RVA: 0x000E08BC File Offset: 0x000DEABC
		public static IPrincipal CurrentPrincipal
		{
			get
			{
				IPrincipal principal = null;
				Thread currentThread = Thread.CurrentThread;
				Thread obj = currentThread;
				lock (obj)
				{
					principal = currentThread._principal;
					if (principal == null)
					{
						principal = Thread.GetDomain().DefaultPrincipal;
					}
				}
				return principal;
			}
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPrincipal\"/>\n</PermissionSet>\n")]
			set
			{
				Thread currentThread = Thread.CurrentThread;
				Thread obj = currentThread;
				lock (obj)
				{
					currentThread._principal = value;
				}
			}
		}

		// Token: 0x0600411E RID: 16670
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Thread CurrentThread_internal();

		/// <summary>Gets the currently running thread.</summary>
		/// <returns>A <see cref="T:System.Threading.Thread" /> that is the representation of the currently running thread.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x0600411F RID: 16671 RVA: 0x000E0908 File Offset: 0x000DEB08
		public static Thread CurrentThread
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			get
			{
				return Thread.CurrentThread_internal();
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06004120 RID: 16672 RVA: 0x000E0910 File Offset: 0x000DEB10
		internal static int CurrentThreadId
		{
			get
			{
				return (int)Thread.CurrentThread.thread_id;
			}
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x000E0920 File Offset: 0x000DEB20
		private static void InitDataStoreHash()
		{
			object obj = Thread.datastore_lock;
			lock (obj)
			{
				if (Thread.datastorehash == null)
				{
					Thread.datastorehash = Hashtable.Synchronized(new Hashtable());
				}
			}
		}

		/// <summary>Allocates a named data slot on all threads. For better performance, use fields that are marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <returns>A <see cref="T:System.LocalDataStoreSlot" />.</returns>
		/// <param name="name">The name of the data slot to be allocated. </param>
		/// <exception cref="T:System.ArgumentException">A named data slot with the specified name already exists.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004122 RID: 16674 RVA: 0x000E097C File Offset: 0x000DEB7C
		public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
		{
			object obj = Thread.datastore_lock;
			LocalDataStoreSlot result;
			lock (obj)
			{
				if (Thread.datastorehash == null)
				{
					Thread.InitDataStoreHash();
				}
				LocalDataStoreSlot localDataStoreSlot = (LocalDataStoreSlot)Thread.datastorehash[name];
				if (localDataStoreSlot != null)
				{
					throw new ArgumentException("Named data slot already added");
				}
				localDataStoreSlot = Thread.AllocateDataSlot();
				Thread.datastorehash.Add(name, localDataStoreSlot);
				result = localDataStoreSlot;
			}
			return result;
		}

		/// <summary>Eliminates the association between a name and a slot, for all threads in the process. For better performance, use fields that are marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <param name="name">The name of the data slot to be freed. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004123 RID: 16675 RVA: 0x000E0A08 File Offset: 0x000DEC08
		public static void FreeNamedDataSlot(string name)
		{
			object obj = Thread.datastore_lock;
			lock (obj)
			{
				if (Thread.datastorehash != null)
				{
					Thread.datastorehash.Remove(name);
				}
			}
		}

		/// <summary>Allocates an unnamed data slot on all the threads. For better performance, use fields that are marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <returns>A <see cref="T:System.LocalDataStoreSlot" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004124 RID: 16676 RVA: 0x000E0A60 File Offset: 0x000DEC60
		public static LocalDataStoreSlot AllocateDataSlot()
		{
			return new LocalDataStoreSlot(true);
		}

		/// <summary>Retrieves the value from the specified slot on the current thread, within the current thread's current domain. For better performance, use fields that are marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <returns>The retrieved value.</returns>
		/// <param name="slot">The <see cref="T:System.LocalDataStoreSlot" /> from which to get the value. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004125 RID: 16677 RVA: 0x000E0A68 File Offset: 0x000DEC68
		public static object GetData(LocalDataStoreSlot slot)
		{
			object[] array = Thread.local_slots;
			if (slot == null)
			{
				throw new ArgumentNullException("slot");
			}
			if (array != null && slot.slot < array.Length)
			{
				return array[slot.slot];
			}
			return null;
		}

		/// <summary>Sets the data in the specified slot on the currently running thread, for that thread's current domain. For better performance, use fields marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <param name="slot">The <see cref="T:System.LocalDataStoreSlot" /> in which to set the value. </param>
		/// <param name="data">The value to be set. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004126 RID: 16678 RVA: 0x000E0AAC File Offset: 0x000DECAC
		public static void SetData(LocalDataStoreSlot slot, object data)
		{
			object[] array = Thread.local_slots;
			if (slot == null)
			{
				throw new ArgumentNullException("slot");
			}
			if (array == null)
			{
				array = new object[slot.slot + 2];
				Thread.local_slots = array;
			}
			else if (slot.slot >= array.Length)
			{
				object[] array2 = new object[slot.slot + 2];
				array.CopyTo(array2, 0);
				array = array2;
				Thread.local_slots = array;
			}
			array[slot.slot] = data;
		}

		// Token: 0x06004127 RID: 16679
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void FreeLocalSlotValues(int slot, bool thread_local);

		/// <summary>Looks up a named data slot. For better performance, use fields that are marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <returns>A <see cref="T:System.LocalDataStoreSlot" /> allocated for this thread.</returns>
		/// <param name="name">The name of the local data slot. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004128 RID: 16680 RVA: 0x000E0B24 File Offset: 0x000DED24
		public static LocalDataStoreSlot GetNamedDataSlot(string name)
		{
			object obj = Thread.datastore_lock;
			LocalDataStoreSlot result;
			lock (obj)
			{
				if (Thread.datastorehash == null)
				{
					Thread.InitDataStoreHash();
				}
				LocalDataStoreSlot localDataStoreSlot = (LocalDataStoreSlot)Thread.datastorehash[name];
				if (localDataStoreSlot == null)
				{
					localDataStoreSlot = Thread.AllocateNamedDataSlot(name);
				}
				result = localDataStoreSlot;
			}
			return result;
		}

		/// <summary>Returns the current domain in which the current thread is running.</summary>
		/// <returns>An <see cref="T:System.AppDomain" /> representing the current application domain of the running thread.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004129 RID: 16681 RVA: 0x000E0B9C File Offset: 0x000DED9C
		public static AppDomain GetDomain()
		{
			return AppDomain.CurrentDomain;
		}

		/// <summary>Returns a unique application domain identifier.</summary>
		/// <returns>A 32-bit signed integer uniquely identifying the application domain.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600412A RID: 16682
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetDomainID();

		// Token: 0x0600412B RID: 16683
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetAbort_internal();

		/// <summary>Cancels an <see cref="M:System.Threading.Thread.Abort(System.Object)" /> requested for the current thread.</summary>
		/// <exception cref="T:System.Threading.ThreadStateException">Abort was not invoked on the current thread. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required security permission for the current thread. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x0600412C RID: 16684 RVA: 0x000E0BA4 File Offset: 0x000DEDA4
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlThread\"/>\n</PermissionSet>\n")]
		public static void ResetAbort()
		{
			Thread.ResetAbort_internal();
		}

		// Token: 0x0600412D RID: 16685
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Sleep_internal(int ms);

		/// <summary>Suspends the current thread for a specified time.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds for which the thread is blocked. Specify zero (0) to indicate that this thread should be suspended to allow other waiting threads to execute. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to block the thread indefinitely. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The time-out value is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600412E RID: 16686 RVA: 0x000E0BAC File Offset: 0x000DEDAC
		public static void Sleep(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", "Negative timeout");
			}
			Thread.Sleep_internal(millisecondsTimeout);
		}

		/// <summary>Blocks the current thread for a specified time.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> set to the amount of time for which the thread is blocked. Specify zero to indicate that this thread should be suspended to allow other waiting threads to execute. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to block the thread indefinitely. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="timeout" /> is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> in milliseconds, or is greater than <see cref="F:System.Int32.MaxValue" /> milliseconds. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600412F RID: 16687 RVA: 0x000E0BCC File Offset: 0x000DEDCC
		public static void Sleep(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", "timeout out of range");
			}
			Thread.Sleep_internal((int)num);
		}

		// Token: 0x06004130 RID: 16688
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr Thread_internal(MulticastDelegate start);

		// Token: 0x06004131 RID: 16689
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Thread_init();

		/// <summary>Gets or sets the apartment state of this thread.</summary>
		/// <returns>One of the <see cref="T:System.Threading.ApartmentState" /> values. The initial value is Unknown.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property to a state that is not a valid apartment state (a state other than single-threaded apartment (STA) or multithreaded apartment (MTA)). </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06004132 RID: 16690 RVA: 0x000E0C10 File Offset: 0x000DEE10
		// (set) Token: 0x06004133 RID: 16691 RVA: 0x000E0C34 File Offset: 0x000DEE34
		[Obsolete("Deprecated in favor of GetApartmentState, SetApartmentState and TrySetApartmentState.")]
		public ApartmentState ApartmentState
		{
			get
			{
				if ((this.ThreadState & ThreadState.Stopped) != ThreadState.Running)
				{
					throw new ThreadStateException("Thread is dead; state can not be accessed.");
				}
				return (ApartmentState)this.apartment_state;
			}
			set
			{
				this.TrySetApartmentState(value);
			}
		}

		// Token: 0x06004134 RID: 16692
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern CultureInfo GetCachedCurrentCulture();

		// Token: 0x06004135 RID: 16693
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern byte[] GetSerializedCurrentCulture();

		// Token: 0x06004136 RID: 16694
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetCachedCurrentCulture(CultureInfo culture);

		// Token: 0x06004137 RID: 16695
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetSerializedCurrentCulture(byte[] culture);

		// Token: 0x06004138 RID: 16696
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern CultureInfo GetCachedCurrentUICulture();

		// Token: 0x06004139 RID: 16697
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern byte[] GetSerializedCurrentUICulture();

		// Token: 0x0600413A RID: 16698
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetCachedCurrentUICulture(CultureInfo culture);

		// Token: 0x0600413B RID: 16699
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetSerializedCurrentUICulture(byte[] culture);

		/// <summary>Gets or sets the culture for the current thread.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> representing the culture for the current thread.</returns>
		/// <exception cref="T:System.NotSupportedException">The property is set to a neutral culture. Neutral cultures cannot be used in formatting and parsing and therefore cannot be set as the thread's current culture.</exception>
		/// <exception cref="T:System.ArgumentNullException">The property is set to null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x0600413C RID: 16700 RVA: 0x000E0C40 File Offset: 0x000DEE40
		// (set) Token: 0x0600413D RID: 16701 RVA: 0x000E0D30 File Offset: 0x000DEF30
		public CultureInfo CurrentCulture
		{
			get
			{
				if (this.in_currentculture)
				{
					return CultureInfo.InvariantCulture;
				}
				CultureInfo cultureInfo = this.GetCachedCurrentCulture();
				if (cultureInfo != null)
				{
					return cultureInfo;
				}
				byte[] serializedCurrentCulture = this.GetSerializedCurrentCulture();
				if (serializedCurrentCulture == null)
				{
					object obj = Thread.culture_lock;
					lock (obj)
					{
						this.in_currentculture = true;
						cultureInfo = CultureInfo.ConstructCurrentCulture();
						this.SetCachedCurrentCulture(cultureInfo);
						this.in_currentculture = false;
						NumberFormatter.SetThreadCurrentCulture(cultureInfo);
						return cultureInfo;
					}
				}
				this.in_currentculture = true;
				try
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					MemoryStream serializationStream = new MemoryStream(serializedCurrentCulture);
					cultureInfo = (CultureInfo)binaryFormatter.Deserialize(serializationStream);
					this.SetCachedCurrentCulture(cultureInfo);
				}
				finally
				{
					this.in_currentculture = false;
				}
				NumberFormatter.SetThreadCurrentCulture(cultureInfo);
				return cultureInfo;
			}
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlThread\"/>\n</PermissionSet>\n")]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				CultureInfo cachedCurrentCulture = this.GetCachedCurrentCulture();
				if (cachedCurrentCulture == value)
				{
					return;
				}
				value.CheckNeutral();
				this.in_currentculture = true;
				try
				{
					this.SetCachedCurrentCulture(value);
					byte[] array;
					if (value.IsReadOnly && value.cached_serialized_form != null)
					{
						array = value.cached_serialized_form;
					}
					else
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						MemoryStream memoryStream = new MemoryStream();
						binaryFormatter.Serialize(memoryStream, value);
						array = memoryStream.GetBuffer();
						if (value.IsReadOnly)
						{
							value.cached_serialized_form = array;
						}
					}
					this.SetSerializedCurrentCulture(array);
				}
				finally
				{
					this.in_currentculture = false;
				}
				NumberFormatter.SetThreadCurrentCulture(value);
			}
		}

		/// <summary>Gets or sets the current culture used by the Resource Manager to look up culture-specific resources at run time.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> representing the current culture.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is set to a culture name that cannot be used to locate a resource file. Resource filenames must include only letters, numbers, hyphens or underscores.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x0600413E RID: 16702 RVA: 0x000E0DF8 File Offset: 0x000DEFF8
		// (set) Token: 0x0600413F RID: 16703 RVA: 0x000E0EDC File Offset: 0x000DF0DC
		public CultureInfo CurrentUICulture
		{
			get
			{
				if (this.in_currentculture)
				{
					return CultureInfo.InvariantCulture;
				}
				CultureInfo cultureInfo = this.GetCachedCurrentUICulture();
				if (cultureInfo != null)
				{
					return cultureInfo;
				}
				byte[] serializedCurrentUICulture = this.GetSerializedCurrentUICulture();
				if (serializedCurrentUICulture == null)
				{
					object obj = Thread.culture_lock;
					lock (obj)
					{
						this.in_currentculture = true;
						cultureInfo = CultureInfo.ConstructCurrentUICulture();
						this.SetCachedCurrentUICulture(cultureInfo);
						this.in_currentculture = false;
						return cultureInfo;
					}
				}
				this.in_currentculture = true;
				try
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					MemoryStream serializationStream = new MemoryStream(serializedCurrentUICulture);
					cultureInfo = (CultureInfo)binaryFormatter.Deserialize(serializationStream);
					this.SetCachedCurrentUICulture(cultureInfo);
				}
				finally
				{
					this.in_currentculture = false;
				}
				return cultureInfo;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				CultureInfo cachedCurrentUICulture = this.GetCachedCurrentUICulture();
				if (cachedCurrentUICulture == value)
				{
					return;
				}
				this.in_currentculture = true;
				try
				{
					this.SetCachedCurrentUICulture(value);
					byte[] array;
					if (value.IsReadOnly && value.cached_serialized_form != null)
					{
						array = value.cached_serialized_form;
					}
					else
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						MemoryStream memoryStream = new MemoryStream();
						binaryFormatter.Serialize(memoryStream, value);
						array = memoryStream.GetBuffer();
						if (value.IsReadOnly)
						{
							value.cached_serialized_form = array;
						}
					}
					this.SetSerializedCurrentUICulture(array);
				}
				finally
				{
					this.in_currentculture = false;
				}
			}
		}

		/// <summary>Gets a value indicating whether or not a thread belongs to the managed thread pool.</summary>
		/// <returns>true if this thread belongs to the managed thread pool; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06004140 RID: 16704 RVA: 0x000E0F98 File Offset: 0x000DF198
		public bool IsThreadPoolThread
		{
			get
			{
				return this.IsThreadPoolThreadInternal;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06004141 RID: 16705 RVA: 0x000E0FA0 File Offset: 0x000DF1A0
		// (set) Token: 0x06004142 RID: 16706 RVA: 0x000E0FA8 File Offset: 0x000DF1A8
		internal bool IsThreadPoolThreadInternal
		{
			get
			{
				return this.threadpool_thread;
			}
			set
			{
				this.threadpool_thread = value;
			}
		}

		/// <summary>Gets a value indicating the execution status of the current thread.</summary>
		/// <returns>true if this thread has been started and has not terminated normally or aborted; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06004143 RID: 16707 RVA: 0x000E0FB4 File Offset: 0x000DF1B4
		public bool IsAlive
		{
			get
			{
				ThreadState threadState = this.GetState();
				return (threadState & ThreadState.Aborted) == ThreadState.Running && (threadState & ThreadState.Stopped) == ThreadState.Running && (threadState & ThreadState.Unstarted) == ThreadState.Running;
			}
		}

		/// <summary>Gets or sets a value indicating whether or not a thread is a background thread.</summary>
		/// <returns>true if this thread is or is to become a background thread; otherwise, false.</returns>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread is dead. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06004144 RID: 16708 RVA: 0x000E0FE8 File Offset: 0x000DF1E8
		// (set) Token: 0x06004145 RID: 16709 RVA: 0x000E101C File Offset: 0x000DF21C
		public bool IsBackground
		{
			get
			{
				ThreadState threadState = this.GetState();
				if ((threadState & ThreadState.Stopped) != ThreadState.Running)
				{
					throw new ThreadStateException("Thread is dead; state can not be accessed.");
				}
				return (threadState & ThreadState.Background) != ThreadState.Running;
			}
			set
			{
				if (value)
				{
					this.SetState(ThreadState.Background);
				}
				else
				{
					this.ClrState(ThreadState.Background);
				}
			}
		}

		// Token: 0x06004146 RID: 16710
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string GetName_internal();

		// Token: 0x06004147 RID: 16711
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetName_internal(string name);

		/// <summary>Gets or sets the name of the thread.</summary>
		/// <returns>A string containing the name of the thread, or null if no name was set.</returns>
		/// <exception cref="T:System.InvalidOperationException">A set operation was requested, and the Name property has already been set. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06004148 RID: 16712 RVA: 0x000E1038 File Offset: 0x000DF238
		// (set) Token: 0x06004149 RID: 16713 RVA: 0x000E1040 File Offset: 0x000DF240
		public string Name
		{
			get
			{
				return this.GetName_internal();
			}
			set
			{
				this.SetName_internal(value);
			}
		}

		/// <summary>Gets or sets a value indicating the scheduling priority of a thread.</summary>
		/// <returns>One of the <see cref="T:System.Threading.ThreadPriority" /> values. The default value is Normal.</returns>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has reached a final state, such as <see cref="F:System.Threading.ThreadState.Aborted" />. </exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is not a valid ThreadPriority value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x0600414A RID: 16714 RVA: 0x000E104C File Offset: 0x000DF24C
		// (set) Token: 0x0600414B RID: 16715 RVA: 0x000E1050 File Offset: 0x000DF250
		public ThreadPriority Priority
		{
			get
			{
				return ThreadPriority.Lowest;
			}
			set
			{
			}
		}

		/// <summary>Gets a value containing the states of the current thread.</summary>
		/// <returns>One of the <see cref="T:System.Threading.ThreadState" /> values indicating the state of the current thread. The initial value is Unstarted.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x0600414C RID: 16716 RVA: 0x000E1054 File Offset: 0x000DF254
		public ThreadState ThreadState
		{
			get
			{
				return this.GetState();
			}
		}

		// Token: 0x0600414D RID: 16717
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Abort_internal(object stateInfo);

		/// <summary>Raises a <see cref="T:System.Threading.ThreadAbortException" /> in the thread on which it is invoked, to begin the process of terminating the thread. Calling this method usually terminates the thread.</summary>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread that is being aborted is currently suspended.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x0600414E RID: 16718 RVA: 0x000E105C File Offset: 0x000DF25C
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlThread\"/>\n</PermissionSet>\n")]
		public void Abort()
		{
			this.Abort_internal(null);
		}

		/// <summary>Raises a <see cref="T:System.Threading.ThreadAbortException" /> in the thread on which it is invoked, to begin the process of terminating the thread while also providing exception information about the thread termination. Calling this method usually terminates the thread.</summary>
		/// <param name="stateInfo">An object that contains application-specific information, such as state, which can be used by the thread being aborted. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread that is being aborted is currently suspended.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x0600414F RID: 16719 RVA: 0x000E1068 File Offset: 0x000DF268
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlThread\"/>\n</PermissionSet>\n")]
		public void Abort(object stateInfo)
		{
			this.Abort_internal(stateInfo);
		}

		// Token: 0x06004150 RID: 16720
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object GetAbortExceptionState();

		// Token: 0x06004151 RID: 16721
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Interrupt_internal();

		/// <summary>Interrupts a thread that is in the WaitSleepJoin thread state.</summary>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the appropriate <see cref="T:System.Security.Permissions.SecurityPermission" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x06004152 RID: 16722 RVA: 0x000E1074 File Offset: 0x000DF274
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlThread\"/>\n</PermissionSet>\n")]
		public void Interrupt()
		{
			this.Interrupt_internal();
		}

		// Token: 0x06004153 RID: 16723
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Join_internal(int ms, IntPtr handle);

		/// <summary>Blocks the calling thread until a thread terminates, while continuing to perform standard COM and SendMessage pumping.</summary>
		/// <exception cref="T:System.Threading.ThreadStateException">The caller attempted to join a thread that is in the <see cref="F:System.Threading.ThreadState.Unstarted" /> state. </exception>
		/// <exception cref="T:System.Threading.ThreadInterruptedException">The thread is interrupted while waiting. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004154 RID: 16724 RVA: 0x000E107C File Offset: 0x000DF27C
		public void Join()
		{
			this.Join_internal(-1, this.system_thread_handle);
		}

		/// <summary>Blocks the calling thread until a thread terminates or the specified time elapses, while continuing to perform standard COM and SendMessage pumping.</summary>
		/// <returns>true if the thread has terminated; false if the thread has not terminated after the amount of time specified by the <paramref name="millisecondsTimeout" /> parameter has elapsed.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait for the thread to terminate. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="millisecondsTimeout" /> is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> in milliseconds. </exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has not been started. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004155 RID: 16725 RVA: 0x000E108C File Offset: 0x000DF28C
		public bool Join(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", "Timeout less than zero");
			}
			return this.Join_internal(millisecondsTimeout, this.system_thread_handle);
		}

		/// <summary>Blocks the calling thread until a thread terminates or the specified time elapses, while continuing to perform standard COM and SendMessage pumping.</summary>
		/// <returns>true if the thread terminated; false if the thread has not terminated after the amount of time specified by the <paramref name="timeout" /> parameter has elapsed.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> set to the amount of time to wait for the thread to terminate. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="timeout" /> is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> in milliseconds, or is greater than <see cref="F:System.Int32.MaxValue" /> milliseconds. </exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The caller attempted to join a thread that is in the <see cref="F:System.Threading.ThreadState.Unstarted" /> state. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004156 RID: 16726 RVA: 0x000E10C0 File Offset: 0x000DF2C0
		public bool Join(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", "timeout out of range");
			}
			return this.Join_internal((int)num, this.system_thread_handle);
		}

		/// <summary>Synchronizes memory access as follows: The processor executing the current thread cannot reorder instructions in such a way that memory accesses prior to the call to <see cref="M:System.Threading.Thread.MemoryBarrier" /> execute after memory accesses that follow the call to <see cref="M:System.Threading.Thread.MemoryBarrier" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004157 RID: 16727
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void MemoryBarrier();

		// Token: 0x06004158 RID: 16728
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Resume_internal();

		/// <summary>Resumes a thread that has been suspended.</summary>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has not been started, is dead, or is not in the suspended state. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the appropriate <see cref="T:System.Security.Permissions.SecurityPermission" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x06004159 RID: 16729 RVA: 0x000E1108 File Offset: 0x000DF308
		[Obsolete("")]
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlThread\"/>\n</PermissionSet>\n")]
		public void Resume()
		{
			this.Resume_internal();
		}

		// Token: 0x0600415A RID: 16730
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SpinWait_nop();

		/// <summary>Causes a thread to wait the number of times defined by the <paramref name="iterations" /> parameter.</summary>
		/// <param name="iterations">A 32-bit signed integer that defines how long a thread is to wait. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600415B RID: 16731 RVA: 0x000E1110 File Offset: 0x000DF310
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void SpinWait(int iterations)
		{
			if (iterations < 0)
			{
				return;
			}
			while (iterations-- > 0)
			{
				Thread.SpinWait_nop();
			}
		}

		/// <summary>Causes the operating system to change the state of the current instance to <see cref="F:System.Threading.ThreadState.Running" />.</summary>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has already been started. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available to start this thread. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600415C RID: 16732 RVA: 0x000E1130 File Offset: 0x000DF330
		public void Start()
		{
			if (!ExecutionContext.IsFlowSuppressed())
			{
				this.ec_to_set = ExecutionContext.Capture();
			}
			if (Thread.CurrentThread._principal != null)
			{
				this._principal = Thread.CurrentThread._principal;
			}
			if (this.Thread_internal(this.threadstart) == (IntPtr)0)
			{
				throw new SystemException("Thread creation failed.");
			}
		}

		// Token: 0x0600415D RID: 16733
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Suspend_internal();

		/// <summary>Either suspends the thread, or if the thread is already suspended, has no effect.</summary>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has not been started or is dead. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the appropriate <see cref="T:System.Security.Permissions.SecurityPermission" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x0600415E RID: 16734 RVA: 0x000E1198 File Offset: 0x000DF398
		[Obsolete("")]
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlThread\"/>\n</PermissionSet>\n")]
		public void Suspend()
		{
			this.Suspend_internal();
		}

		// Token: 0x0600415F RID: 16735
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Thread_free_internal(IntPtr handle);

		// Token: 0x06004160 RID: 16736 RVA: 0x000E11A0 File Offset: 0x000DF3A0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~Thread()
		{
			this.Thread_free_internal(this.system_thread_handle);
		}

		// Token: 0x06004161 RID: 16737
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetState(ThreadState set);

		// Token: 0x06004162 RID: 16738
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ClrState(ThreadState clr);

		// Token: 0x06004163 RID: 16739
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern ThreadState GetState();

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004164 RID: 16740
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte VolatileRead(ref byte address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004165 RID: 16741
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double VolatileRead(ref double address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004166 RID: 16742
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short VolatileRead(ref short address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004167 RID: 16743
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int VolatileRead(ref int address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004168 RID: 16744
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long VolatileRead(ref long address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004169 RID: 16745
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr VolatileRead(ref IntPtr address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600416A RID: 16746
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object VolatileRead(ref object address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600416B RID: 16747
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte VolatileRead(ref sbyte address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600416C RID: 16748
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float VolatileRead(ref float address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600416D RID: 16749
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ushort VolatileRead(ref ushort address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600416E RID: 16750
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint VolatileRead(ref uint address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600416F RID: 16751
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong VolatileRead(ref ulong address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004170 RID: 16752
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern UIntPtr VolatileRead(ref UIntPtr address);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004171 RID: 16753
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref byte address, byte value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004172 RID: 16754
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref double address, double value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004173 RID: 16755
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref short address, short value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004174 RID: 16756
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref int address, int value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004175 RID: 16757
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref long address, long value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004176 RID: 16758
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref IntPtr address, IntPtr value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004177 RID: 16759
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref object address, object value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004178 RID: 16760
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref sbyte address, sbyte value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004179 RID: 16761
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref float address, float value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600417A RID: 16762
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref ushort address, ushort value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600417B RID: 16763
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref uint address, uint value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600417C RID: 16764
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref ulong address, ulong value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600417D RID: 16765
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref UIntPtr address, UIntPtr value);

		// Token: 0x0600417E RID: 16766 RVA: 0x000E11E4 File Offset: 0x000DF3E4
		private static int GetNewManagedId()
		{
			return Thread.GetNewManagedId_internal();
		}

		// Token: 0x0600417F RID: 16767
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetNewManagedId_internal();

		/// <summary>Gets an <see cref="T:System.Threading.ExecutionContext" /> object that contains information about the various contexts of the current thread. </summary>
		/// <returns>An <see cref="T:System.Threading.ExecutionContext" /> object that consolidates context information for the current thread.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06004180 RID: 16768 RVA: 0x000E11EC File Offset: 0x000DF3EC
		[MonoTODO("limited to CompressedStack support")]
		public ExecutionContext ExecutionContext
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			get
			{
				if (Thread._ec == null)
				{
					Thread._ec = new ExecutionContext();
				}
				return Thread._ec;
			}
		}

		/// <summary>Gets a unique identifier for the current managed thread.</summary>
		/// <returns>An integer that represents a unique identifier for this managed thread.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06004181 RID: 16769 RVA: 0x000E1208 File Offset: 0x000DF408
		public int ManagedThreadId
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				if (this.managed_id == 0)
				{
					int newManagedId = Thread.GetNewManagedId();
					Interlocked.CompareExchange(ref this.managed_id, newManagedId, 0);
				}
				return this.managed_id;
			}
		}

		/// <summary>Notifies a host that execution is about to enter a region of code in which the effects of a thread abort or unhandled exception might jeopardize other tasks in the application domain.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004182 RID: 16770 RVA: 0x000E123C File Offset: 0x000DF43C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void BeginCriticalRegion()
		{
			Thread.CurrentThread.critical_region_level++;
		}

		/// <summary>Notifies a host that execution is about to enter a region of code in which the effects of a thread abort or unhandled exception are limited to the current task.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004183 RID: 16771 RVA: 0x000E1254 File Offset: 0x000DF454
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void EndCriticalRegion()
		{
			Thread.CurrentThread.critical_region_level--;
		}

		/// <summary>Notifies a host that managed code is about to execute instructions that depend on the identity of the current physical operating system thread.</summary>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x06004184 RID: 16772 RVA: 0x000E126C File Offset: 0x000DF46C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void BeginThreadAffinity()
		{
		}

		/// <summary>Notifies a host that managed code has finished executing instructions that depend on the identity of the current physical operating system thread.</summary>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x06004185 RID: 16773 RVA: 0x000E1270 File Offset: 0x000DF470
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void EndThreadAffinity()
		{
		}

		/// <summary>Returns an <see cref="T:System.Threading.ApartmentState" /> value indicating the apartment state.</summary>
		/// <returns>One of the <see cref="T:System.Threading.ApartmentState" /> values indicating the apartment state of the managed thread. The default is <see cref="F:System.Threading.ApartmentState.Unknown" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004186 RID: 16774 RVA: 0x000E1274 File Offset: 0x000DF474
		public ApartmentState GetApartmentState()
		{
			return (ApartmentState)this.apartment_state;
		}

		/// <summary>Sets the apartment state of a thread before it is started.</summary>
		/// <param name="state">The new apartment state.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid apartment state.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has already been started.</exception>
		/// <exception cref="T:System.InvalidOperationException">The apartment state has already been initialized.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004187 RID: 16775 RVA: 0x000E127C File Offset: 0x000DF47C
		public void SetApartmentState(ApartmentState state)
		{
			if (!this.TrySetApartmentState(state))
			{
				throw new InvalidOperationException("Failed to set the specified COM apartment state.");
			}
		}

		/// <summary>Sets the apartment state of a thread before it is started.</summary>
		/// <returns>true if the apartment state is set; otherwise, false.</returns>
		/// <param name="state">The new apartment state.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid apartment state.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has already been started.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004188 RID: 16776 RVA: 0x000E1298 File Offset: 0x000DF498
		public bool TrySetApartmentState(ApartmentState state)
		{
			if (this != Thread.CurrentThread && (this.ThreadState & ThreadState.Unstarted) == ThreadState.Running)
			{
				throw new ThreadStateException("Thread was in an invalid state for the operation being executed.");
			}
			if (this.apartment_state != 2)
			{
				return false;
			}
			this.apartment_state = (byte)state;
			return true;
		}

		/// <summary>Returns a hash code for the current thread.</summary>
		/// <returns>An integer hash code value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004189 RID: 16777 RVA: 0x000E12E0 File Offset: 0x000DF4E0
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return this.ManagedThreadId;
		}

		/// <summary>Causes the operating system to change the state of the current instance to <see cref="F:System.Threading.ThreadState.Running" />, and optionally supplies an object containing data to be used by the method the thread executes.</summary>
		/// <param name="parameter">An object that contains data to be used by the method the thread executes.</param>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has already been started. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available to start this thread. </exception>
		/// <exception cref="T:System.InvalidOperationException">This thread was created using a <see cref="T:System.Threading.ThreadStart" /> delegate instead of a <see cref="T:System.Threading.ParameterizedThreadStart" /> delegate.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600418A RID: 16778 RVA: 0x000E12E8 File Offset: 0x000DF4E8
		public void Start(object parameter)
		{
			this.start_obj = parameter;
			this.Start();
		}

		/// <summary>Returns a <see cref="T:System.Threading.CompressedStack" /> object that can be used to capture the stack for the current thread.</summary>
		/// <returns>A <see cref="T:System.Threading.CompressedStack" /> object that can be used to capture the stack for the current thread.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="00000000000000000400000000000000" />
		/// </PermissionSet>
		// Token: 0x0600418B RID: 16779 RVA: 0x000E12F8 File Offset: 0x000DF4F8
		[Obsolete("see CompressedStack class")]
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n   <IPermission class=\"System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                PublicKeyBlob=\"00000000000000000400000000000000\"/>\n</PermissionSet>\n")]
		public CompressedStack GetCompressedStack()
		{
			CompressedStack compressedStack = this.ExecutionContext.SecurityContext.CompressedStack;
			return (compressedStack != null && !compressedStack.IsEmpty()) ? compressedStack.CreateCopy() : null;
		}

		/// <summary>Applies a captured <see cref="T:System.Threading.CompressedStack" /> to the current thread.</summary>
		/// <param name="stack">The <see cref="T:System.Threading.CompressedStack" /> object to be applied to the current thread.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="00000000000000000400000000000000" />
		/// </PermissionSet>
		// Token: 0x0600418C RID: 16780 RVA: 0x000E1334 File Offset: 0x000DF534
		[Obsolete("see CompressedStack class")]
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n   <IPermission class=\"System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                PublicKeyBlob=\"00000000000000000400000000000000\"/>\n</PermissionSet>\n")]
		public void SetCompressedStack(CompressedStack stack)
		{
			this.ExecutionContext.SecurityContext.CompressedStack = stack;
		}

		// Token: 0x04001BD6 RID: 7126
		private int lock_thread_id;

		// Token: 0x04001BD7 RID: 7127
		private IntPtr system_thread_handle;

		// Token: 0x04001BD8 RID: 7128
		private object cached_culture_info;

		// Token: 0x04001BD9 RID: 7129
		private IntPtr unused0;

		// Token: 0x04001BDA RID: 7130
		private bool threadpool_thread;

		// Token: 0x04001BDB RID: 7131
		private IntPtr name;

		// Token: 0x04001BDC RID: 7132
		private int name_len;

		// Token: 0x04001BDD RID: 7133
		private ThreadState state = ThreadState.Unstarted;

		// Token: 0x04001BDE RID: 7134
		private object abort_exc;

		// Token: 0x04001BDF RID: 7135
		private int abort_state_handle;

		// Token: 0x04001BE0 RID: 7136
		private long thread_id;

		// Token: 0x04001BE1 RID: 7137
		private IntPtr start_notify;

		// Token: 0x04001BE2 RID: 7138
		private IntPtr stack_ptr;

		// Token: 0x04001BE3 RID: 7139
		private UIntPtr static_data;

		// Token: 0x04001BE4 RID: 7140
		private IntPtr jit_data;

		// Token: 0x04001BE5 RID: 7141
		private IntPtr lock_data;

		// Token: 0x04001BE6 RID: 7142
		private object current_appcontext;

		// Token: 0x04001BE7 RID: 7143
		private int stack_size;

		// Token: 0x04001BE8 RID: 7144
		private object start_obj;

		// Token: 0x04001BE9 RID: 7145
		private IntPtr appdomain_refs;

		// Token: 0x04001BEA RID: 7146
		private int interruption_requested;

		// Token: 0x04001BEB RID: 7147
		private IntPtr suspend_event;

		// Token: 0x04001BEC RID: 7148
		private IntPtr suspended_event;

		// Token: 0x04001BED RID: 7149
		private IntPtr resume_event;

		// Token: 0x04001BEE RID: 7150
		private IntPtr synch_cs;

		// Token: 0x04001BEF RID: 7151
		private IntPtr serialized_culture_info;

		// Token: 0x04001BF0 RID: 7152
		private int serialized_culture_info_len;

		// Token: 0x04001BF1 RID: 7153
		private IntPtr serialized_ui_culture_info;

		// Token: 0x04001BF2 RID: 7154
		private int serialized_ui_culture_info_len;

		// Token: 0x04001BF3 RID: 7155
		private bool thread_dump_requested;

		// Token: 0x04001BF4 RID: 7156
		private IntPtr end_stack;

		// Token: 0x04001BF5 RID: 7157
		private bool thread_interrupt_requested;

		// Token: 0x04001BF6 RID: 7158
		private byte apartment_state = 2;

		// Token: 0x04001BF7 RID: 7159
		private volatile int critical_region_level;

		// Token: 0x04001BF8 RID: 7160
		private int small_id;

		// Token: 0x04001BF9 RID: 7161
		private IntPtr manage_callback;

		// Token: 0x04001BFA RID: 7162
		private object pending_exception;

		// Token: 0x04001BFB RID: 7163
		private ExecutionContext ec_to_set;

		// Token: 0x04001BFC RID: 7164
		private IntPtr interrupt_on_stop;

		// Token: 0x04001BFD RID: 7165
		private IntPtr unused3;

		// Token: 0x04001BFE RID: 7166
		private IntPtr unused4;

		// Token: 0x04001BFF RID: 7167
		private IntPtr unused5;

		// Token: 0x04001C00 RID: 7168
		private IntPtr unused6;

		// Token: 0x04001C01 RID: 7169
		[ThreadStatic]
		private static object[] local_slots;

		// Token: 0x04001C02 RID: 7170
		[ThreadStatic]
		private static ExecutionContext _ec;

		// Token: 0x04001C03 RID: 7171
		private MulticastDelegate threadstart;

		// Token: 0x04001C04 RID: 7172
		private int managed_id;

		// Token: 0x04001C05 RID: 7173
		private IPrincipal _principal;

		// Token: 0x04001C06 RID: 7174
		private static Hashtable datastorehash;

		// Token: 0x04001C07 RID: 7175
		private static object datastore_lock = new object();

		// Token: 0x04001C08 RID: 7176
		private bool in_currentculture;

		// Token: 0x04001C09 RID: 7177
		private static object culture_lock = new object();
	}
}
