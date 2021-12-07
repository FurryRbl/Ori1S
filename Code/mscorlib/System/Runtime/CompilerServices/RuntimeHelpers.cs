using System;
using System.Runtime.ConstrainedExecution;

namespace System.Runtime.CompilerServices
{
	/// <summary>Provides a set of static methods and properties that provide support for compilers. This class cannot be inherited. </summary>
	// Token: 0x02000061 RID: 97
	public static class RuntimeHelpers
	{
		// Token: 0x060006C0 RID: 1728
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitializeArray(Array array, IntPtr fldHandle);

		/// <summary>Provides a fast way to initialize an array from data stored in a module.</summary>
		/// <param name="array">The array to be initialized. </param>
		/// <param name="fldHandle">A <see cref="T:System.RuntimeFieldHandle" /> specifying the location of the data used to initialize the array. </param>
		// Token: 0x060006C1 RID: 1729 RVA: 0x0001526C File Offset: 0x0001346C
		public static void InitializeArray(Array array, RuntimeFieldHandle fldHandle)
		{
			if (array == null || fldHandle.Value == IntPtr.Zero)
			{
				throw new ArgumentNullException();
			}
			RuntimeHelpers.InitializeArray(array, fldHandle.Value);
		}

		/// <summary>Gets the offset in bytes to the data in the given string.</summary>
		/// <returns>The byte offset, from the start of the <see cref="T:System.String" /> object to the first character in the string.</returns>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060006C2 RID: 1730
		public static extern int OffsetToStringData { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		/// <summary>Serves as a hash function for a particular type, suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the <see cref="T:System.Object" /> identified by the <paramref name="o" /> parameter.</returns>
		/// <param name="o">An <see cref="T:System.Object" /> to retrieve the hash code for. </param>
		// Token: 0x060006C3 RID: 1731 RVA: 0x000152A0 File Offset: 0x000134A0
		public static int GetHashCode(object o)
		{
			return object.InternalGetHashCode(o);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> instances are considered equal.</summary>
		/// <returns>true if the <paramref name="o1" /> parameter is the same instance as the <paramref name="o2" /> parameter or if both are null or if o1.Equals(o2) returns true; otherwise, false.</returns>
		/// <param name="o1">The first <see cref="T:System.Object" /> to compare. </param>
		/// <param name="o2">The second <see cref="T:System.Object" /> to compare. </param>
		// Token: 0x060006C4 RID: 1732 RVA: 0x000152A8 File Offset: 0x000134A8
		public new static bool Equals(object o1, object o2)
		{
			if (o1 == o2)
			{
				return true;
			}
			if (o1 == null || o2 == null)
			{
				return false;
			}
			if (o1 is ValueType)
			{
				return ValueType.DefaultEquals(o1, o2);
			}
			return object.Equals(o1, o2);
		}

		/// <summary>Boxes a value type.</summary>
		/// <returns>Returns a boxed copy of <paramref name="obj" /> if it is a value class; otherwise <paramref name="obj" /> itself is returned.</returns>
		/// <param name="obj">The value type to be boxed. </param>
		// Token: 0x060006C5 RID: 1733
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object GetObjectValue(object obj);

		// Token: 0x060006C6 RID: 1734
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RunClassConstructor(IntPtr type);

		/// <summary>Runs a specified class constructor method.</summary>
		/// <param name="type">A <see cref="T:System.RuntimeTypeHandle" /> specifying the class constructor method to run. </param>
		/// <exception cref="T:System.TypeInitializationException">The class initializer threw an exception. </exception>
		// Token: 0x060006C7 RID: 1735 RVA: 0x000152DC File Offset: 0x000134DC
		public static void RunClassConstructor(RuntimeTypeHandle type)
		{
			if (type.Value == IntPtr.Zero)
			{
				throw new ArgumentException("Handle is not initialized.", "type");
			}
			RuntimeHelpers.RunClassConstructor(type.Value);
		}

		/// <summary>Executes code using a <see cref="T:System.Delegate" /> while using another <see cref="T:System.Delegate" /> to execute additional code in case of an exception.</summary>
		/// <param name="code">A <see cref="T:System.Delegate" /> to the code to try.</param>
		/// <param name="backoutCode">A <see cref="T:System.Delegate" /> to the code to run if a exception occurs.</param>
		/// <param name="userData">The data to pass to <paramref name="code" /> and <paramref name="backoutCode" />.</param>
		// Token: 0x060006C8 RID: 1736 RVA: 0x0001531C File Offset: 0x0001351C
		[MonoTODO("Currently a no-op")]
		public static void ExecuteCodeWithGuaranteedCleanup(RuntimeHelpers.TryCode code, RuntimeHelpers.CleanupCode backoutCode, object userData)
		{
		}

		/// <summary>Designates a body of code as a constrained execution region (CER).</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060006C9 RID: 1737 RVA: 0x00015320 File Offset: 0x00013520
		[MonoTODO("Currently a no-op")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void PrepareConstrainedRegions()
		{
		}

		/// <summary>Designates a body of code as a constrained execution region (CER) without performing any probing.  </summary>
		// Token: 0x060006CA RID: 1738 RVA: 0x00015324 File Offset: 0x00013524
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MonoTODO("Currently a no-op")]
		public static void PrepareConstrainedRegionsNoOP()
		{
		}

		/// <summary>Probes for a certain amount of stack space to ensure that a stack overflow cannot happen within a subsequent block of code (assuming that your code uses only a finite and moderate amount of stack space). We recommend that you use a constrained execution region (CER) instead of this method. </summary>
		// Token: 0x060006CB RID: 1739 RVA: 0x00015328 File Offset: 0x00013528
		[MonoTODO("Currently a no-op")]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void ProbeForSufficientStack()
		{
		}

		/// <summary>Indicates that the specified delegate be prepared for inclusion in a constrained execution region (CER).</summary>
		/// <param name="d">The <see cref="T:System.Delegate" /> type to prepare.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060006CC RID: 1740 RVA: 0x0001532C File Offset: 0x0001352C
		[MonoTODO("Currently a no-op")]
		public static void PrepareDelegate(Delegate d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
		}

		/// <summary>Prepares a method for inclusion in a constrained execution region (CER).</summary>
		/// <param name="method">A <see cref="T:System.RuntimeMethodHandle" /> to the method to prepare.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060006CD RID: 1741 RVA: 0x00015340 File Offset: 0x00013540
		[MonoTODO("Currently a no-op")]
		public static void PrepareMethod(RuntimeMethodHandle method)
		{
		}

		/// <summary>Prepares a method for inclusion in a constrained execution region (CER) with the specified instantiation.</summary>
		/// <param name="method">A <see cref="T:System.RuntimeMethodHandle" /> to the method to prepare.</param>
		/// <param name="instantiation">A <see cref="T:System.RuntimeTypeHandle" /> instantiation to pass.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060006CE RID: 1742 RVA: 0x00015344 File Offset: 0x00013544
		[MonoTODO("Currently a no-op")]
		public static void PrepareMethod(RuntimeMethodHandle method, RuntimeTypeHandle[] instantiation)
		{
		}

		/// <summary>Runs a specified module constructor method.</summary>
		/// <param name="module">A <see cref="T:System.ModuleHandle" /> object specifying the module constructor method to run.</param>
		/// <exception cref="T:System.TypeInitializationException">The module constructor threw an exception.</exception>
		// Token: 0x060006CF RID: 1743 RVA: 0x00015348 File Offset: 0x00013548
		public static void RunModuleConstructor(ModuleHandle module)
		{
			if (module == ModuleHandle.EmptyHandle)
			{
				throw new ArgumentException("Handle is not initialized.", "module");
			}
			RuntimeHelpers.RunModuleConstructor(module.Value);
		}

		// Token: 0x060006D0 RID: 1744
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RunModuleConstructor(IntPtr module);

		/// <summary>Represents a delegate to code that should be run in a try block..</summary>
		/// <param name="userData">Data to pass to the delegate.</param>
		// Token: 0x02000062 RID: 98
		// (Invoke) Token: 0x060006D2 RID: 1746
		public delegate void TryCode(object userData);

		/// <summary>Represents a method to run when an exception occurs.</summary>
		/// <param name="userData">Data to pass to the delegate.</param>
		/// <param name="exceptionThrown">true to express that an exception was thrown; otherwise, false.</param>
		// Token: 0x02000063 RID: 99
		// (Invoke) Token: 0x060006D6 RID: 1750
		public delegate void CleanupCode(object userData, bool exceptionThrown);
	}
}
