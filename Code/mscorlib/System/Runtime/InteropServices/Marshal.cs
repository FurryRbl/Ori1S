using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Threading;
using Mono.Interop;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides a collection of methods for allocating unmanaged memory, copying unmanaged memory blocks, and converting managed to unmanaged types, as well as other miscellaneous methods used when interacting with unmanaged code.</summary>
	// Token: 0x020003AC RID: 940
	[SuppressUnmanagedCodeSecurity]
	public static class Marshal
	{
		// Token: 0x06002AA1 RID: 10913
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddRefInternal(IntPtr pUnk);

		/// <summary>Increments the reference count on the specified interface.</summary>
		/// <returns>The new value of the reference count on the <paramref name="pUnk" /> parameter.</returns>
		/// <param name="pUnk">The interface reference count to increment. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AA2 RID: 10914 RVA: 0x00092A84 File Offset: 0x00090C84
		public static int AddRef(IntPtr pUnk)
		{
			if (pUnk == IntPtr.Zero)
			{
				throw new ArgumentException("Value cannot be null.", "pUnk");
			}
			return Marshal.AddRefInternal(pUnk);
		}

		/// <summary>Allocates a block of memory of specified size from the COM task memory allocator.</summary>
		/// <returns>An integer representing the address of the block of memory allocated. This memory must be released with <see cref="M:System.Runtime.InteropServices.Marshal.FreeCoTaskMem(System.IntPtr)" />.</returns>
		/// <param name="cb">The size of the block of memory to be allocated. </param>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to satisfy the request. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AA3 RID: 10915
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr AllocCoTaskMem(int cb);

		/// <summary>Allocates memory from the process's unmanaged memory.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> to the newly allocated memory. This memory must be released using the <see cref="M:System.Runtime.InteropServices.Marshal.FreeHGlobal(System.IntPtr)" /> method.</returns>
		/// <param name="cb">The number of bytes in memory required. </param>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to satisfy the request. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AA4 RID: 10916
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr AllocHGlobal(IntPtr cb);

		/// <summary>Allocates memory from the unmanaged memory of the process.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> to the newly allocated memory. This memory must be released using the <see cref="M:System.Runtime.InteropServices.Marshal.FreeHGlobal(System.IntPtr)" /> method.</returns>
		/// <param name="cb">The number of bytes in memory required. </param>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to satisfy the request. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AA5 RID: 10917 RVA: 0x00092AB8 File Offset: 0x00090CB8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static IntPtr AllocHGlobal(int cb)
		{
			return Marshal.AllocHGlobal((IntPtr)cb);
		}

		/// <summary>Gets an interface pointer identified by the specified moniker.</summary>
		/// <returns>An object containing a reference to the interface pointer identified by the <paramref name="monikerName" /> parameter. A moniker is a name, and in this case, the moniker is defined by an interface.</returns>
		/// <param name="monikerName">The moniker corresponding to the desired interface pointer. </param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">An unrecognized HRESULT was returned by the unmanaged BindToMoniker method. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AA6 RID: 10918 RVA: 0x00092AC8 File Offset: 0x00090CC8
		[MonoTODO]
		public static object BindToMoniker(string monikerName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Changes the strength of a COM callable wrapper's (CCW) handle on the object it contains.</summary>
		/// <param name="otp">The object whose COM callable wrapper (CCW) holds a reference counted handle. The handle is strong if the reference count on the CCW is greater than zero; otherwise it is weak. </param>
		/// <param name="fIsWeak">true to change the strength of the handle on the <paramref name="otp" /> parameter to weak, regardless of its reference count; false to reset the handle strength on <paramref name="otp" /> to be reference counted. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AA7 RID: 10919 RVA: 0x00092AD0 File Offset: 0x00090CD0
		[MonoTODO]
		public static void ChangeWrapperHandleStrength(object otp, bool fIsWeak)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002AA8 RID: 10920
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void copy_to_unmanaged(Array source, int startIndex, IntPtr destination, int length);

		// Token: 0x06002AA9 RID: 10921
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void copy_from_unmanaged(IntPtr source, int startIndex, Array destination, int length);

		/// <summary>Copies data from a one-dimensional, managed 8-bit unsigned integer array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from. </param>
		/// <param name="startIndex">The zero-based index into the array where Copy should start. </param>
		/// <param name="destination">The memory pointer to copy to. </param>
		/// <param name="length">The number of array elements to copy. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="startIndex" />, <paramref name="destination" />, or <paramref name="length" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AAA RID: 10922 RVA: 0x00092AD8 File Offset: 0x00090CD8
		public static void Copy(byte[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from a one-dimensional, managed character array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from. </param>
		/// <param name="startIndex">The zero-based index into the array where Copy should start. </param>
		/// <param name="destination">The memory pointer to copy to. </param>
		/// <param name="length">The number of array elements to copy. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="startIndex" />, <paramref name="destination" />, or <paramref name="length" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AAB RID: 10923 RVA: 0x00092AE4 File Offset: 0x00090CE4
		public static void Copy(char[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from a one-dimensional, managed 16-bit signed integer array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from. </param>
		/// <param name="startIndex">The zero-based index into the array where Copy should start. </param>
		/// <param name="destination">The memory pointer to copy to. </param>
		/// <param name="length">The number of array elements to copy. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="startIndex" />, <paramref name="destination" />, or <paramref name="length" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AAC RID: 10924 RVA: 0x00092AF0 File Offset: 0x00090CF0
		public static void Copy(short[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from a one-dimensional, managed 32-bit signed integer array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from. </param>
		/// <param name="startIndex">The zero-based index into the array where Copy should start. </param>
		/// <param name="destination">The memory pointer to copy to. </param>
		/// <param name="length">The number of array elements to copy. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AAD RID: 10925 RVA: 0x00092AFC File Offset: 0x00090CFC
		public static void Copy(int[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from a one-dimensional, managed 64-bit signed integer array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from. </param>
		/// <param name="startIndex">The zero-based index into the array where Copy should start. </param>
		/// <param name="destination">The memory pointer to copy to. </param>
		/// <param name="length">The number of array elements to copy. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="startIndex" />, <paramref name="destination" />, or <paramref name="length" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AAE RID: 10926 RVA: 0x00092B08 File Offset: 0x00090D08
		public static void Copy(long[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from a one-dimensional, managed single-precision floating-point number array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from. </param>
		/// <param name="startIndex">The zero-based index into the array where Copy should start. </param>
		/// <param name="destination">The memory pointer to copy to. </param>
		/// <param name="length">The number of array elements to copy. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="startIndex" />, <paramref name="destination" />, or <paramref name="length" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AAF RID: 10927 RVA: 0x00092B14 File Offset: 0x00090D14
		public static void Copy(float[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from a one-dimensional, managed double-precision floating-point number array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from. </param>
		/// <param name="startIndex">The zero-based index into the array where Copy should start. </param>
		/// <param name="destination">The memory pointer to copy to. </param>
		/// <param name="length">The number of array elements to copy. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="startIndex" />, <paramref name="destination" />, or <paramref name="length" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AB0 RID: 10928 RVA: 0x00092B20 File Offset: 0x00090D20
		public static void Copy(double[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from a one-dimensional, managed <see cref="T:System.IntPtr" /> array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from. </param>
		/// <param name="startIndex">The zero-based index into the array where Copy should start. </param>
		/// <param name="destination">The memory pointer to copy to. </param>
		/// <param name="length">The number of array elements to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is null. </exception>
		// Token: 0x06002AB1 RID: 10929 RVA: 0x00092B2C File Offset: 0x00090D2C
		public static void Copy(IntPtr[] source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed 8-bit unsigned integer array.</summary>
		/// <param name="source">The memory pointer to copy from. </param>
		/// <param name="destination">The array to copy to. </param>
		/// <param name="startIndex">The zero-based index into the array where Copy should start. </param>
		/// <param name="length">The number of array elements to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AB2 RID: 10930 RVA: 0x00092B38 File Offset: 0x00090D38
		public static void Copy(IntPtr source, byte[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed character array.</summary>
		/// <param name="source">The memory pointer to copy from. </param>
		/// <param name="destination">The array to copy to. </param>
		/// <param name="startIndex">The zero-based index into the array where Copy should start. </param>
		/// <param name="length">The number of array elements to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AB3 RID: 10931 RVA: 0x00092B44 File Offset: 0x00090D44
		public static void Copy(IntPtr source, char[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed 16-bit signed integer array.</summary>
		/// <param name="source">The memory pointer to copy from. </param>
		/// <param name="destination">The array to copy to. </param>
		/// <param name="startIndex">The zero-based index into the array where Copy should start. </param>
		/// <param name="length">The number of array elements to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AB4 RID: 10932 RVA: 0x00092B50 File Offset: 0x00090D50
		public static void Copy(IntPtr source, short[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed 32-bit signed integer array.</summary>
		/// <param name="source">The memory pointer to copy from. </param>
		/// <param name="destination">The array to copy to. </param>
		/// <param name="startIndex">The zero-based index into the array where Copy should start. </param>
		/// <param name="length">The number of array elements to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AB5 RID: 10933 RVA: 0x00092B5C File Offset: 0x00090D5C
		public static void Copy(IntPtr source, int[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed 64-bit signed integer array.</summary>
		/// <param name="source">The memory pointer to copy from. </param>
		/// <param name="destination">The array to copy to. </param>
		/// <param name="startIndex">The zero-based index into the array where Copy should start. </param>
		/// <param name="length">The number of array elements to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AB6 RID: 10934 RVA: 0x00092B68 File Offset: 0x00090D68
		public static void Copy(IntPtr source, long[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed single-precision floating-point number array.</summary>
		/// <param name="source">The memory pointer to copy from. </param>
		/// <param name="destination">The array to copy to. </param>
		/// <param name="startIndex">The zero-based index into the array where Copy should start. </param>
		/// <param name="length">The number of array elements to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AB7 RID: 10935 RVA: 0x00092B74 File Offset: 0x00090D74
		public static void Copy(IntPtr source, float[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed double-precision floating-point number array.</summary>
		/// <param name="source">The memory pointer to copy from. </param>
		/// <param name="destination">The array to copy to. </param>
		/// <param name="startIndex">The zero-based index into the array where Copy should start. </param>
		/// <param name="length">The number of array elements to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AB8 RID: 10936 RVA: 0x00092B80 File Offset: 0x00090D80
		public static void Copy(IntPtr source, double[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed <see cref="T:System.IntPtr" /> array.</summary>
		/// <param name="source">The memory pointer to copy from. </param>
		/// <param name="destination">The array to copy to. </param>
		/// <param name="startIndex">The zero-based index into the array where Copy should start. </param>
		/// <param name="length">The number of array elements to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is null. </exception>
		// Token: 0x06002AB9 RID: 10937 RVA: 0x00092B8C File Offset: 0x00090D8C
		public static void Copy(IntPtr source, IntPtr[] destination, int startIndex, int length)
		{
			Marshal.copy_from_unmanaged(source, startIndex, destination, length);
		}

		/// <summary>Aggregates a managed object with the specified COM object.</summary>
		/// <returns>The inner IUnknown pointer of the managed object.</returns>
		/// <param name="pOuter">The outer IUnknown pointer.</param>
		/// <param name="o">An object to aggregate.</param>
		// Token: 0x06002ABA RID: 10938 RVA: 0x00092B98 File Offset: 0x00090D98
		public static IntPtr CreateAggregatedObject(IntPtr pOuter, object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>Wraps the specified COM object in an object of the specified type.</summary>
		/// <returns>The newly wrapped object that is an instance of the desired type.</returns>
		/// <param name="o">The object to be wrapped. </param>
		/// <param name="t">The <see cref="T:System.Type" /> of wrapper to create. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="t" /> must derive from __ComObject. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="t" /> parameter is null.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="o" /> cannot be converted to the destination type since it does not support all required interfaces. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002ABB RID: 10939 RVA: 0x00092BA0 File Offset: 0x00090DA0
		public static object CreateWrapperOfType(object o, Type t)
		{
			__ComObject _ComObject = o as __ComObject;
			if (_ComObject == null)
			{
				throw new ArgumentException("o must derive from __ComObject", "o");
			}
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			Type[] interfaces = o.GetType().GetInterfaces();
			foreach (Type type in interfaces)
			{
				if (type.IsImport && _ComObject.GetInterface(type) == IntPtr.Zero)
				{
					throw new InvalidCastException();
				}
			}
			return ComInteropProxy.GetProxy(_ComObject.IUnknown, t).GetTransparentProxy();
		}

		/// <summary>Frees all substructures pointed to by the specified unmanaged memory block.</summary>
		/// <param name="ptr">A pointer to an unmanaged block of memory. </param>
		/// <param name="structuretype">Type of a formatted class. This provides the layout information necessary to delete the buffer in the <paramref name="ptr" /> parameter. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="structureType" /> has an automatic layout. Use sequential or explicit instead. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002ABC RID: 10940
		[ComVisible(true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DestroyStructure(IntPtr ptr, Type structuretype);

		/// <summary>Frees a BSTR using SysFreeString.</summary>
		/// <param name="ptr">The address of the BSTR to be freed. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002ABD RID: 10941
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FreeBSTR(IntPtr ptr);

		/// <summary>Frees a block of memory allocated by the unmanaged COM task memory allocator with <see cref="M:System.Runtime.InteropServices.Marshal.AllocCoTaskMem(System.Int32)" />.</summary>
		/// <param name="ptr">The address of the memory to be freed. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002ABE RID: 10942
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FreeCoTaskMem(IntPtr ptr);

		/// <summary>Frees memory previously allocated from the unmanaged memory of the process with <see cref="M:System.Runtime.InteropServices.Marshal.AllocHGlobal(System.IntPtr)" />.</summary>
		/// <param name="hglobal">The handle returned by the original matching call to <see cref="M:System.Runtime.InteropServices.Marshal.AllocHGlobal(System.IntPtr)" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002ABF RID: 10943
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FreeHGlobal(IntPtr hglobal);

		// Token: 0x06002AC0 RID: 10944 RVA: 0x00092C40 File Offset: 0x00090E40
		private static void ClearBSTR(IntPtr ptr)
		{
			int num = Marshal.ReadInt32(ptr, -4);
			for (int i = 0; i < num; i++)
			{
				Marshal.WriteByte(ptr, i, 0);
			}
		}

		/// <summary>Frees a BSTR pointer that was allocated using the <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToBSTR(System.Security.SecureString)" /> method.</summary>
		/// <param name="s">The address of the BSTR to free.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AC1 RID: 10945 RVA: 0x00092C70 File Offset: 0x00090E70
		public static void ZeroFreeBSTR(IntPtr s)
		{
			Marshal.ClearBSTR(s);
			Marshal.FreeBSTR(s);
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x00092C80 File Offset: 0x00090E80
		private static void ClearAnsi(IntPtr ptr)
		{
			int num = 0;
			while (Marshal.ReadByte(ptr, num) != 0)
			{
				Marshal.WriteByte(ptr, num, 0);
				num++;
			}
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x00092CAC File Offset: 0x00090EAC
		private static void ClearUnicode(IntPtr ptr)
		{
			int num = 0;
			while (Marshal.ReadInt16(ptr, num) != 0)
			{
				Marshal.WriteInt16(ptr, num, 0);
				num += 2;
			}
		}

		/// <summary>Frees an unmanaged string pointer that was allocated using the <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToCoTaskMemAnsi(System.Security.SecureString)" /> method.</summary>
		/// <param name="s">The address of the unmanaged string to free.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AC4 RID: 10948 RVA: 0x00092CD8 File Offset: 0x00090ED8
		public static void ZeroFreeCoTaskMemAnsi(IntPtr s)
		{
			Marshal.ClearAnsi(s);
			Marshal.FreeCoTaskMem(s);
		}

		/// <summary>Frees an unmanaged string pointer that was allocated using the <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToCoTaskMemUnicode(System.Security.SecureString)" /> method.</summary>
		/// <param name="s">The address of the unmanaged string to free.</param>
		// Token: 0x06002AC5 RID: 10949 RVA: 0x00092CE8 File Offset: 0x00090EE8
		public static void ZeroFreeCoTaskMemUnicode(IntPtr s)
		{
			Marshal.ClearUnicode(s);
			Marshal.FreeCoTaskMem(s);
		}

		/// <summary>Frees an unmanaged string pointer that was allocated using the <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocAnsi(System.Security.SecureString)" /> method.</summary>
		/// <param name="s">The address of the unmanaged string to free.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AC6 RID: 10950 RVA: 0x00092CF8 File Offset: 0x00090EF8
		public static void ZeroFreeGlobalAllocAnsi(IntPtr s)
		{
			Marshal.ClearAnsi(s);
			Marshal.FreeHGlobal(s);
		}

		/// <summary>Frees an unmanaged string pointer that was allocated using the <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(System.Security.SecureString)" /> method.</summary>
		/// <param name="s">The address of the unmanaged string to free.</param>
		// Token: 0x06002AC7 RID: 10951 RVA: 0x00092D08 File Offset: 0x00090F08
		public static void ZeroFreeGlobalAllocUnicode(IntPtr s)
		{
			Marshal.ClearUnicode(s);
			Marshal.FreeHGlobal(s);
		}

		/// <summary>Returns the globally unique identifier (GUID) for the specified type, or generates a GUID using the algorithm used by the Type Library Exporter (Tlbexp.exe).</summary>
		/// <returns>A <see cref="T:System.Guid" /> for the specified type.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> to generate a GUID for. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AC8 RID: 10952 RVA: 0x00092D18 File Offset: 0x00090F18
		public static Guid GenerateGuidForType(Type type)
		{
			return type.GUID;
		}

		/// <summary>Returns a programmatic identifier (ProgID) for the specified type.</summary>
		/// <returns>The ProgID of the specified type.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> to get a ProgID for. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not a class that can be create by COM. The class must be public, have a public default constructor, and be COM visible. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AC9 RID: 10953 RVA: 0x00092D20 File Offset: 0x00090F20
		[MonoTODO]
		public static string GenerateProgIdForType(Type type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Obtains a running instance of the specified object from the Running Object Table (ROT).</summary>
		/// <returns>The object requested. You can cast this object to any COM interface that it supports.</returns>
		/// <param name="progID">The ProgID of the object being requested. </param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The object was not found.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002ACA RID: 10954 RVA: 0x00092D28 File Offset: 0x00090F28
		[MonoTODO]
		public static object GetActiveObject(string progID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002ACB RID: 10955
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetCCW(object o, Type T);

		// Token: 0x06002ACC RID: 10956 RVA: 0x00092D30 File Offset: 0x00090F30
		private static IntPtr GetComInterfaceForObjectInternal(object o, Type T)
		{
			if (Marshal.IsComObject(o))
			{
				return ((__ComObject)o).GetInterface(T);
			}
			return Marshal.GetCCW(o, T);
		}

		/// <summary>Returns an interface pointer that represents the specified interface for an object.</summary>
		/// <returns>The interface pointer representing the interface for the object.</returns>
		/// <param name="o">The object providing the interface. </param>
		/// <param name="T">The <see cref="T:System.Type" /> of interface that is requested. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="T" /> parameter is not an interface.-or- The type is not visible to COM. -or-The <paramref name="T" /> parameter is a generic type.</exception>
		/// <exception cref="T:System.InvalidCastException">The <paramref name="o" /> parameter does not support the requested interface. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="o" /> parameter is null-or- The <paramref name="T" /> parameter is null</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002ACD RID: 10957 RVA: 0x00092D54 File Offset: 0x00090F54
		public static IntPtr GetComInterfaceForObject(object o, Type T)
		{
			IntPtr comInterfaceForObjectInternal = Marshal.GetComInterfaceForObjectInternal(o, T);
			Marshal.AddRef(comInterfaceForObjectInternal);
			return comInterfaceForObjectInternal;
		}

		/// <summary>Returns an interface pointer that represents the specified interface for an object, if the caller is in the same context as that object.</summary>
		/// <returns>The interface pointer specified by <paramref name="t" /> that represents the interface for the specified object, or null if the caller is not in the same context as the object.</returns>
		/// <param name="o">The object that provides the interface.</param>
		/// <param name="t">The <see cref="T:System.Type" /> of interface that is requested.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="t" /> is not an interface.-or- The type is not visible to COM. </exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="o" /> does not support the requested interface.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="o" /> is null.-or- <paramref name="t" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002ACE RID: 10958 RVA: 0x00092D74 File Offset: 0x00090F74
		[MonoTODO]
		public static IntPtr GetComInterfaceForObjectInContext(object o, Type t)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets data referenced by the specified key from the specified COM object.</summary>
		/// <returns>The data represented by the <paramref name="key" /> parameter in the internal hash table of the <paramref name="obj" /> parameter.</returns>
		/// <param name="obj">The COM object containing the desired data. </param>
		/// <param name="key">The key in the internal hash table of <paramref name="obj" /> to retrieve the data from. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is null.-or- <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a COM object. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002ACF RID: 10959 RVA: 0x00092D7C File Offset: 0x00090F7C
		[MonoNotSupported("MSDN states user code should never need to call this method.")]
		public static object GetComObjectData(object obj, object key)
		{
			throw new NotSupportedException("MSDN states user code should never need to call this method.");
		}

		// Token: 0x06002AD0 RID: 10960
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetComSlotForMethodInfoInternal(MemberInfo m);

		/// <summary>Gets the virtual function table (VTBL) slot for a specified <see cref="T:System.Reflection.MemberInfo" /> when exposed to COM.</summary>
		/// <returns>The VTBL (also called v-table) slot <paramref name="m" /> identifier when it is exposed to COM.</returns>
		/// <param name="m">A <see cref="T:System.Reflection.MemberInfo" /> that represents an interface method. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="m" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="m" /> parameter is not a <see cref="T:System.Reflection.MethodInfo" /> object.-or-The <paramref name="m" /> parameter is not an interface method.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AD1 RID: 10961 RVA: 0x00092D88 File Offset: 0x00090F88
		public static int GetComSlotForMethodInfo(MemberInfo m)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			if (!(m is MethodInfo))
			{
				throw new ArgumentException("The MemberInfo must be an interface method.", "m");
			}
			if (!m.DeclaringType.IsInterface)
			{
				throw new ArgumentException("The MemberInfo must be an interface method.", "m");
			}
			return Marshal.GetComSlotForMethodInfoInternal(m);
		}

		/// <summary>Gets the last slot in the virtual function table (VTBL) of a type when exposed to COM.</summary>
		/// <returns>The last VTBL (also called v-table) slot of the interface when exposed to COM. If the <paramref name="t" /> parameter is a class, the returned VTBL slot is the last slot in the interface that is generated from the class.</returns>
		/// <param name="t">A <see cref="T:System.Type" /> representing an interface or class. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AD2 RID: 10962 RVA: 0x00092DE8 File Offset: 0x00090FE8
		[MonoTODO]
		public static int GetEndComSlot(Type t)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves a code that identifies the type of the exception that occurred.</summary>
		/// <returns>The type of the exception.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AD3 RID: 10963 RVA: 0x00092DF0 File Offset: 0x00090FF0
		[MonoTODO]
		public static int GetExceptionCode()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves a computer-independent description of an exception, and information about the state that existed for the thread when the exception occurred.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> to an EXCEPTION_POINTERS structure.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AD4 RID: 10964 RVA: 0x00092DF8 File Offset: 0x00090FF8
		[ComVisible(true)]
		[MonoTODO]
		public static IntPtr GetExceptionPointers()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the instance handle (HINSTANCE) for the specified module.</summary>
		/// <returns>The HINSTANCE for <paramref name="m" />; -1 if the module does not have an HINSTANCE.</returns>
		/// <param name="m">The <see cref="T:System.Reflection.Module" /> whose HINSTANCE is desired. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="m" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AD5 RID: 10965 RVA: 0x00092E00 File Offset: 0x00091000
		public static IntPtr GetHINSTANCE(Module m)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			return m.GetHINSTANCE();
		}

		/// <summary>Converts the specified exception to an HRESULT.</summary>
		/// <returns>The HRESULT mapped to the supplied exception.</returns>
		/// <param name="e">The <see cref="T:System.Exception" /> to convert to an HRESULT. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AD6 RID: 10966 RVA: 0x00092E1C File Offset: 0x0009101C
		[MonoTODO("SetErrorInfo")]
		public static int GetHRForException(Exception e)
		{
			return e.hresult;
		}

		/// <summary>Returns the HRESULT corresponding to the last error incurred by Win32 code executed using <see cref="T:System.Runtime.InteropServices.Marshal" />.</summary>
		/// <returns>The HRESULT corresponding to the last Win32 error code.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AD7 RID: 10967 RVA: 0x00092E24 File Offset: 0x00091024
		[MonoTODO]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int GetHRForLastWin32Error()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002AD8 RID: 10968
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetIDispatchForObjectInternal(object o);

		/// <summary>Returns an IDispatch interface from a managed object.</summary>
		/// <returns>The IDispatch pointer for the <paramref name="o" /> parameter.</returns>
		/// <param name="o">The object whose IDispatch interface is requested. </param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="o" /> does not support the requested interface. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AD9 RID: 10969 RVA: 0x00092E2C File Offset: 0x0009102C
		public static IntPtr GetIDispatchForObject(object o)
		{
			IntPtr idispatchForObjectInternal = Marshal.GetIDispatchForObjectInternal(o);
			Marshal.AddRef(idispatchForObjectInternal);
			return idispatchForObjectInternal;
		}

		/// <summary>Returns an IDispatch interface pointer from a managed object, if the caller is in the same context as that object.</summary>
		/// <returns>The IDispatch interface pointer for the <paramref name="o" /> parameter, or null if the caller is not in the same context as the specified object.</returns>
		/// <param name="o">The object whose IDispatch interface is requested. </param>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="o" /> does not support the requested interface. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="o" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002ADA RID: 10970 RVA: 0x00092E48 File Offset: 0x00091048
		[MonoTODO]
		public static IntPtr GetIDispatchForObjectInContext(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an ITypeInfo interface from a managed type.</summary>
		/// <returns>The ITypeInfo pointer for the <paramref name="t" /> parameter.</returns>
		/// <param name="t">The <see cref="T:System.Type" /> whose ITypeInfo interface is being requested. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="t" /> is not a visible type to COM. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">A type library is registered for the assembly that contains the type, but the type definition cannot be found. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002ADB RID: 10971 RVA: 0x00092E50 File Offset: 0x00091050
		[MonoTODO]
		public static IntPtr GetITypeInfoForType(Type t)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002ADC RID: 10972
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetIUnknownForObjectInternal(object o);

		/// <summary>Returns an IUnknown interface from a managed object.</summary>
		/// <returns>The IUnknown pointer for the <paramref name="o" /> parameter.</returns>
		/// <param name="o">The object whose IUnknown interface is requested. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002ADD RID: 10973 RVA: 0x00092E58 File Offset: 0x00091058
		public static IntPtr GetIUnknownForObject(object o)
		{
			IntPtr iunknownForObjectInternal = Marshal.GetIUnknownForObjectInternal(o);
			Marshal.AddRef(iunknownForObjectInternal);
			return iunknownForObjectInternal;
		}

		/// <summary>Returns an IUnknown interface from a managed object, if the caller is in the same context as that object.</summary>
		/// <returns>The IUnknown pointer for the <paramref name="o" /> parameter, or null if the caller is not in the same context as the specified object.</returns>
		/// <param name="o">The object whose IUnknown interface is requested.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002ADE RID: 10974 RVA: 0x00092E74 File Offset: 0x00091074
		[MonoTODO]
		public static IntPtr GetIUnknownForObjectInContext(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a pointer to a thunk that marshals a call from managed to unmanaged code.</summary>
		/// <returns>A pointer to the thunk that will marshal a call from the <paramref name="pfnMethodToWrap" /> parameter.</returns>
		/// <param name="pfnMethodToWrap">A pointer to the method to marshal. </param>
		/// <param name="pbSignature">A pointer to the method signature. </param>
		/// <param name="cbSignature">The number of bytes in <paramref name="pbSignature" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002ADF RID: 10975 RVA: 0x00092E7C File Offset: 0x0009107C
		[MonoTODO]
		[Obsolete("This method has been deprecated")]
		public static IntPtr GetManagedThunkForUnmanagedMethodPtr(IntPtr pfnMethodToWrap, IntPtr pbSignature, int cbSignature)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves <see cref="T:System.Reflection.MethodInfo" /> for the specified virtual function table (VTBL) slot.</summary>
		/// <returns>The MemberInfo that represents the member at the specified VTBL (also called v-table) slot.</returns>
		/// <param name="t">The type for which the MethodInfo is to be retrieved. </param>
		/// <param name="slot">The VTBL slot. </param>
		/// <param name="memberType">On successful return, the type of the member. This is one of the <see cref="T:System.Runtime.InteropServices.ComMemberType" /> enumeration members. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="t" /> is not visible from COM. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AE0 RID: 10976 RVA: 0x00092E84 File Offset: 0x00091084
		[MonoTODO]
		public static MemberInfo GetMethodInfoForComSlot(Type t, int slot, ref ComMemberType memberType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts an object to a COM VARIANT.</summary>
		/// <param name="obj">The object for which to get a COM VARIANT. </param>
		/// <param name="pDstNativeVariant">An <see cref="T:System.IntPtr" /> to receive the VARIANT corresponding to the <paramref name="obj" /> parameter. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="obj" /> parameter is a generic type.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AE1 RID: 10977 RVA: 0x00092E8C File Offset: 0x0009108C
		public static void GetNativeVariantForObject(object obj, IntPtr pDstNativeVariant)
		{
			Variant variant = default(Variant);
			variant.SetValue(obj);
			Marshal.StructureToPtr(variant, pDstNativeVariant, false);
		}

		// Token: 0x06002AE2 RID: 10978
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetObjectForCCW(IntPtr pUnk);

		/// <summary>Returns an instance of a type that represents a COM object by a pointer to its IUnknown interface.</summary>
		/// <returns>An object representing the specified unmanaged COM object.</returns>
		/// <param name="pUnk">A pointer to the IUnknown interface. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AE3 RID: 10979 RVA: 0x00092EB8 File Offset: 0x000910B8
		public static object GetObjectForIUnknown(IntPtr pUnk)
		{
			object obj = Marshal.GetObjectForCCW(pUnk);
			if (obj == null)
			{
				ComInteropProxy proxy = ComInteropProxy.GetProxy(pUnk, typeof(__ComObject));
				obj = proxy.GetTransparentProxy();
			}
			return obj;
		}

		/// <summary>Converts a COM VARIANT to an object.</summary>
		/// <returns>An object corresponding to the <paramref name="pSrcNativeVariant" /> parameter.</returns>
		/// <param name="pSrcNativeVariant">An <see cref="T:System.IntPtr" /> containing a COM VARIANT. </param>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidOleVariantTypeException">
		///   <paramref name="pSrcNativeVariant" /> is not a valid VARIANT type. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="pSrcNativeVariant" /> has an unsupported type. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AE4 RID: 10980 RVA: 0x00092EEC File Offset: 0x000910EC
		public static object GetObjectForNativeVariant(IntPtr pSrcNativeVariant)
		{
			return ((Variant)Marshal.PtrToStructure(pSrcNativeVariant, typeof(Variant))).GetValue();
		}

		/// <summary>Converts an array of COM VARIANTs to an array of objects.</summary>
		/// <returns>An object array corresponding to <paramref name="aSrcNativeVariant" />.</returns>
		/// <param name="aSrcNativeVariant">An <see cref="T:System.IntPtr" /> containing the first element of an array of COM VARIANTs. </param>
		/// <param name="cVars">The count of COM VARIANTs in <paramref name="aSrcNativeVariant" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="cVars" /> cannot be a negative number. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AE5 RID: 10981 RVA: 0x00092F18 File Offset: 0x00091118
		public static object[] GetObjectsForNativeVariants(IntPtr aSrcNativeVariant, int cVars)
		{
			if (cVars < 0)
			{
				throw new ArgumentOutOfRangeException("cVars", "cVars cannot be a negative number.");
			}
			object[] array = new object[cVars];
			for (int i = 0; i < cVars; i++)
			{
				array[i] = Marshal.GetObjectForNativeVariant((IntPtr)(aSrcNativeVariant.ToInt64() + (long)(i * Marshal.SizeOf(typeof(Variant)))));
			}
			return array;
		}

		/// <summary>Gets the first slot in the virtual function table (VTBL) that contains user defined methods.</summary>
		/// <returns>The first VTBL (also called v-table) slot that contains user defined methods. The first slot is 3 if the interface is IUnknown based, and 7 if the interface is IDispatch based.</returns>
		/// <param name="t">A <see cref="T:System.Type" /> representing an interface. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="t" /> is not visible from COM. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AE6 RID: 10982 RVA: 0x00092F80 File Offset: 0x00091180
		[MonoTODO]
		public static int GetStartComSlot(Type t)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts a fiber cookie into the corresponding <see cref="T:System.Threading.Thread" /> instance.</summary>
		/// <returns>A <see cref="T:System.Threading.Thread" /> corresponding to the <paramref name="cookie" /> parameter.</returns>
		/// <param name="cookie">An integer representing a fiber cookie. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="cookie" /> parameter is 0.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AE7 RID: 10983 RVA: 0x00092F88 File Offset: 0x00091188
		[Obsolete("This method has been deprecated")]
		[MonoTODO]
		public static Thread GetThreadFromFiberCookie(int cookie)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a managed object of a specified type that represents a COM object.</summary>
		/// <returns>An instance of the class corresponding to the <see cref="T:System.Type" /> object that represents the requested unmanaged COM object.</returns>
		/// <param name="pUnk">A pointer to the IUnknown interface of the unmanaged object. </param>
		/// <param name="t">The <see cref="T:System.Type" /> of the requested managed class. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="t" /> is not attributed with <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AE8 RID: 10984 RVA: 0x00092F90 File Offset: 0x00091190
		public static object GetTypedObjectForIUnknown(IntPtr pUnk, Type t)
		{
			ComInteropProxy comInteropProxy = new ComInteropProxy(pUnk, t);
			__ComObject _ComObject = (__ComObject)comInteropProxy.GetTransparentProxy();
			foreach (Type type in t.GetInterfaces())
			{
				if ((type.Attributes & TypeAttributes.Import) == TypeAttributes.Import && _ComObject.GetInterface(type) == IntPtr.Zero)
				{
					return null;
				}
			}
			return _ComObject;
		}

		/// <summary>Converts an ITypeInfo into a managed <see cref="T:System.Type" /> object.</summary>
		/// <returns>A managed <see cref="T:System.Type" /> that represents the unmanaged ITypeInfo.</returns>
		/// <param name="piTypeInfo">The ITypeInfo interface to marshal. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002AE9 RID: 10985 RVA: 0x00093004 File Offset: 0x00091204
		[MonoTODO]
		public static Type GetTypeForITypeInfo(IntPtr piTypeInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the name of the type represented by an ITypeInfo.</summary>
		/// <returns>The name of the type pointed to by the <paramref name="pTI" /> parameter.</returns>
		/// <param name="pTI">A <see cref="T:System.Runtime.InteropServices.UCOMITypeInfo" /> that represents an ITypeInfo pointer. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AEA RID: 10986 RVA: 0x0009300C File Offset: 0x0009120C
		[Obsolete]
		[MonoTODO]
		public static string GetTypeInfoName(UCOMITypeInfo pTI)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the name of the type represented by an ITypeInfo.</summary>
		/// <returns>The name of the type pointed to by the <paramref name="typeInfo" /> parameter.</returns>
		/// <param name="typeInfo">A <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeInfo" /> object that represents an ITypeInfo pointer. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="typeInfo" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AEB RID: 10987 RVA: 0x00093014 File Offset: 0x00091214
		public static string GetTypeInfoName(ITypeInfo typeInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the library identifier (LIBID) of a type library.</summary>
		/// <returns>The LIBID (that is, the <see cref="T:System.Guid" />) of the type library pointed to by the <paramref name="pTLB" /> parameter.</returns>
		/// <param name="pTLB">A <see cref="T:System.Runtime.InteropServices.UCOMITypeLib" /> that represents an ITypeLib pointer. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AEC RID: 10988 RVA: 0x0009301C File Offset: 0x0009121C
		[Obsolete]
		[MonoTODO]
		public static Guid GetTypeLibGuid(UCOMITypeLib pTLB)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the library identifier (LIBID) of a type library.</summary>
		/// <returns>The LIBID (that is, the <see cref="T:System.Guid" />) of the type library pointed to by the <paramref name="typelib" /> parameter.</returns>
		/// <param name="typelib">An <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeLib" /> object that represents an ITypeLib pointer. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AED RID: 10989 RVA: 0x00093024 File Offset: 0x00091224
		[MonoTODO]
		public static Guid GetTypeLibGuid(ITypeLib typelib)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the library identifier (LIBID) that is assigned to a type library when it was exported from the specified assembly.</summary>
		/// <returns>The LIBID (that is, the <see cref="T:System.Guid" />) that is assigned to a type library when it is exported from the <paramref name="asm" /> parameter.</returns>
		/// <param name="asm">A managed <see cref="T:System.Reflection.Assembly" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AEE RID: 10990 RVA: 0x0009302C File Offset: 0x0009122C
		[MonoTODO]
		public static Guid GetTypeLibGuidForAssembly(Assembly asm)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the LCID of a type library.</summary>
		/// <returns>The LCID of the type library pointed to by the <paramref name="pTLB" /> parameter.</returns>
		/// <param name="pTLB">A <see cref="T:System.Runtime.InteropServices.UCOMITypeLib" /> that represents an ITypeLib pointer. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AEF RID: 10991 RVA: 0x00093034 File Offset: 0x00091234
		[MonoTODO]
		[Obsolete]
		public static int GetTypeLibLcid(UCOMITypeLib pTLB)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the LCID of a type library.</summary>
		/// <returns>The LCID of the type library pointed to by the <paramref name="typelib" /> parameter.</returns>
		/// <param name="typelib">A <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeLib" /> object that represents an ITypeLib pointer. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AF0 RID: 10992 RVA: 0x0009303C File Offset: 0x0009123C
		[MonoTODO]
		public static int GetTypeLibLcid(ITypeLib typelib)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the name of a type library.</summary>
		/// <returns>The name of the type library pointed to by the <paramref name="pTLB" /> parameter.</returns>
		/// <param name="pTLB">A <see cref="T:System.Runtime.InteropServices.UCOMITypeLib" /> that represents an ITypeLib pointer. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AF1 RID: 10993 RVA: 0x00093044 File Offset: 0x00091244
		[MonoTODO]
		[Obsolete]
		public static string GetTypeLibName(UCOMITypeLib pTLB)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the name of a type library.</summary>
		/// <returns>The name of the type library pointed to by the <paramref name="typelib" /> parameter.</returns>
		/// <param name="typelib">An <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeLib" /> object that represents an ITypeLib pointer. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="typelib" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AF2 RID: 10994 RVA: 0x0009304C File Offset: 0x0009124C
		[MonoTODO]
		public static string GetTypeLibName(ITypeLib typelib)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the version number of a type library that will be exported from the specified assembly.</summary>
		/// <param name="inputAssembly">A managed <see cref="T:System.Reflection.Assembly" /> object.</param>
		/// <param name="majorVersion">The major version number.</param>
		/// <param name="minorVersion">The minor version number.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AF3 RID: 10995 RVA: 0x00093054 File Offset: 0x00091254
		[MonoTODO]
		public static void GetTypeLibVersionForAssembly(Assembly inputAssembly, out int majorVersion, out int minorVersion)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a unique runtime callable wrapper (RCW) object for a given IUnknown.</summary>
		/// <returns>A unique runtime callable wrapper (RCW) for a given IUnknown.</returns>
		/// <param name="unknown">A managed pointer to an IUnknown.</param>
		// Token: 0x06002AF4 RID: 10996 RVA: 0x0009305C File Offset: 0x0009125C
		public static object GetUniqueObjectForIUnknown(IntPtr unknown)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a pointer to a thunk that marshals a call from unmanaged to managed code.</summary>
		/// <returns>A pointer to the thunk that will marshal a call from <paramref name="pfnMethodToWrap" />.</returns>
		/// <param name="pfnMethodToWrap">A pointer to the method to marshal. </param>
		/// <param name="pbSignature">A pointer to the method signature. </param>
		/// <param name="cbSignature">The number of bytes in <paramref name="pbSignature" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AF5 RID: 10997 RVA: 0x00093064 File Offset: 0x00091264
		[Obsolete("This method has been deprecated")]
		[MonoTODO]
		public static IntPtr GetUnmanagedThunkForManagedMethodPtr(IntPtr pfnMethodToWrap, IntPtr pbSignature, int cbSignature)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether a specified object represents a COM object.</summary>
		/// <returns>true if the <paramref name="o" /> parameter is a COM type; otherwise, false.</returns>
		/// <param name="o">The object to check. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AF6 RID: 10998
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsComObject(object o);

		/// <summary>Indicates whether a type is visible to COM clients.</summary>
		/// <returns>true if the type is visible to COM; otherwise, false.</returns>
		/// <param name="t">The <see cref="T:System.Type" /> to check for COM visibility. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AF7 RID: 10999 RVA: 0x0009306C File Offset: 0x0009126C
		[MonoTODO]
		public static bool IsTypeVisibleFromCom(Type t)
		{
			throw new NotImplementedException();
		}

		/// <summary>Calculates the number of bytes in unmanaged memory that are required to hold the parameters for the specified method.</summary>
		/// <returns>The number of bytes required to represent the method parameters in unmanaged memory.</returns>
		/// <param name="m">A <see cref="T:System.Reflection.MethodInfo" /> that identifies the method to be checked. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="m" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="m" /> parameter is not a <see cref="T:System.Reflection.MethodInfo" /> object.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AF8 RID: 11000 RVA: 0x00093074 File Offset: 0x00091274
		[MonoTODO]
		public static int NumParamBytes(MethodInfo m)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the error code returned by the last unmanaged function called using platform invoke that has the <see cref="F:System.Runtime.InteropServices.DllImportAttribute.SetLastError" /> flag set.</summary>
		/// <returns>The last error code set by a call to the Win32 SetLastError API method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AF9 RID: 11001
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetLastWin32Error();

		/// <summary>Returns the field offset of the unmanaged form of the managed class.</summary>
		/// <returns>The offset, in bytes, for the <paramref name="fieldName" /> parameter within the platform invoke declared class <paramref name="t" />.</returns>
		/// <param name="t">A <see cref="T:System.Type" />, specifying the specified class. You must apply the <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> to the class. </param>
		/// <param name="fieldName">The field within the <paramref name="t" /> parameter. </param>
		/// <exception cref="T:System.ArgumentException">The class cannot be exported as a structure or the field is nonpublic. Beginning with the .NET Framework version 2.0, the field may be private.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="t" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AFA RID: 11002
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr OffsetOf(Type t, string fieldName);

		/// <summary>Executes one-time method setup tasks without calling the method.</summary>
		/// <param name="m">A <see cref="T:System.Reflection.MethodInfo" /> that identifies the method to be checked. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="m" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="m" /> parameter is not a <see cref="T:System.Reflection.MethodInfo" /> object.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AFB RID: 11003
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Prelink(MethodInfo m);

		/// <summary>Performs a pre-link check for all methods on a class.</summary>
		/// <param name="c">A <see cref="T:System.Type" /> that identifies the class whose methods are to be checked. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="c" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AFC RID: 11004
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PrelinkAll(Type c);

		/// <summary>Copies all characters up to the first null from an unmanaged ANSI string to a managed <see cref="T:System.String" />. Widens each ANSI character to Unicode.</summary>
		/// <returns>A managed <see cref="T:System.String" /> object that holds a copy of the unmanaged ANSI string. If <paramref name="ptr" /> is null, the method returns a null string.</returns>
		/// <param name="ptr">The address of the first character of the unmanaged string. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AFD RID: 11005
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PtrToStringAnsi(IntPtr ptr);

		/// <summary>Allocates a managed <see cref="T:System.String" />, copies a specified number of characters from an unmanaged ANSI string into it, and widens each ANSI character to Unicode.</summary>
		/// <returns>A managed <see cref="T:System.String" /> that holds a copy of the native ANSI string if the value of the <paramref name="ptr" /> parameter is not null; otherwise, this method returns null.</returns>
		/// <param name="ptr">The address of the first character of the unmanaged string. </param>
		/// <param name="len">The byte count of the input string to copy. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="len" /> is less than zero. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AFE RID: 11006
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PtrToStringAnsi(IntPtr ptr, int len);

		/// <summary>Allocates a managed <see cref="T:System.String" /> and copies all characters up to the first null character from a string stored in unmanaged memory into it.</summary>
		/// <returns>A managed string that holds a copy of the unmanaged string if the value of the <paramref name="ptr" /> parameter is not null; otherwise, this method returns null.</returns>
		/// <param name="ptr">For Unicode platforms, the address of the first Unicode character.-or- For ANSI plaforms, the address of the first ANSI character. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002AFF RID: 11007 RVA: 0x0009307C File Offset: 0x0009127C
		public static string PtrToStringAuto(IntPtr ptr)
		{
			return (Marshal.SystemDefaultCharSize != 2) ? Marshal.PtrToStringAnsi(ptr) : Marshal.PtrToStringUni(ptr);
		}

		/// <summary>Copies a specified number of characters from a string stored in unmanaged memory to a managed <see cref="T:System.String" />.</summary>
		/// <returns>A managed string that holds a copy of the native string if the value of the <paramref name="ptr" /> parameter is not null; otherwise, this method returns null.</returns>
		/// <param name="ptr">For Unicode platforms, the address of the first Unicode character.-or- For ANSI plaforms, the address of the first ANSI character. </param>
		/// <param name="len">The number of characters to copy. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="len" /> is less than zero. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B00 RID: 11008 RVA: 0x0009309C File Offset: 0x0009129C
		public static string PtrToStringAuto(IntPtr ptr, int len)
		{
			return (Marshal.SystemDefaultCharSize != 2) ? Marshal.PtrToStringAnsi(ptr, len) : Marshal.PtrToStringUni(ptr, len);
		}

		/// <summary>Allocates a managed <see cref="T:System.String" /> and copies all characters up to the first null character from an unmanaged Unicode string into it.</summary>
		/// <returns>A managed string holding a copy of the native string if the value of the <paramref name="ptr" /> parameter is not null; otherwise, this method returns null.</returns>
		/// <param name="ptr">The address of the first character of the unmanaged string. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B01 RID: 11009
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PtrToStringUni(IntPtr ptr);

		/// <summary>Copies a specified number of characters from a Unicode string stored in native heap to a managed <see cref="T:System.String" />.</summary>
		/// <returns>A managed string that holds a copy of the native string if the value of the <paramref name="ptr" /> parameter is not null; otherwise, this method returns null.</returns>
		/// <param name="ptr">The address of the first character of the unmanaged string. </param>
		/// <param name="len">The number of Unicode characters to copy. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B02 RID: 11010
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PtrToStringUni(IntPtr ptr, int len);

		/// <summary>Allocates a managed <see cref="T:System.String" /> and copies a BSTR string stored in unmanaged memory into it.</summary>
		/// <returns>A managed string that holds a copy of the native string if the value of the <paramref name="ptr" /> parameter is not null; otherwise, this method returns null.</returns>
		/// <param name="ptr">The address of the first character of the unmanaged string. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B03 RID: 11011
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PtrToStringBSTR(IntPtr ptr);

		/// <summary>Marshals data from an unmanaged block of memory to a managed object.</summary>
		/// <param name="ptr">A pointer to an unmanaged block of memory. </param>
		/// <param name="structure">The object to which the data is to be copied. This must be an instance of a formatted class. </param>
		/// <exception cref="T:System.ArgumentException">Structure layout is not sequential or explicit.-or- Structure is a boxed value type. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B04 RID: 11012
		[ComVisible(true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PtrToStructure(IntPtr ptr, object structure);

		/// <summary>Marshals data from an unmanaged block of memory to a newly allocated managed object of the specified type.</summary>
		/// <returns>A managed object containing the data pointed to by the <paramref name="ptr" /> parameter.</returns>
		/// <param name="ptr">A pointer to an unmanaged block of memory. </param>
		/// <param name="structureType">The <see cref="T:System.Type" /> of object to be created. This type object must represent a formatted class or a structure. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="structureType" /> parameter layout is not sequential or explicit. -or-The <paramref name="structureType" /> parameter is a generic type.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="structureType" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B05 RID: 11013
		[ComVisible(true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object PtrToStructure(IntPtr ptr, Type structureType);

		// Token: 0x06002B06 RID: 11014
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int QueryInterfaceInternal(IntPtr pUnk, ref Guid iid, out IntPtr ppv);

		/// <summary>Requests a pointer to a specified interface from a COM object.</summary>
		/// <returns>An HRESULT that indicates the success or failure of the call.</returns>
		/// <param name="pUnk">The interface to be queried. </param>
		/// <param name="iid">A <see cref="T:System.Guid" />, passed by reference, that is the interface identifier (IID) of the requested interface. </param>
		/// <param name="ppv">When this method returns, contains a reference to the returned interface. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B07 RID: 11015 RVA: 0x000930BC File Offset: 0x000912BC
		public static int QueryInterface(IntPtr pUnk, ref Guid iid, out IntPtr ppv)
		{
			if (pUnk == IntPtr.Zero)
			{
				throw new ArgumentException("Value cannot be null.", "pUnk");
			}
			return Marshal.QueryInterfaceInternal(pUnk, ref iid, out ppv);
		}

		/// <summary>Reads a single byte from an unmanaged pointer.</summary>
		/// <returns>The byte read from the <paramref name="ptr" /> parameter.</returns>
		/// <param name="ptr">The address in unmanaged memory from which to read. </param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format. -or-<paramref name="ptr" /> is null. -or-<paramref name="ptr" /> is invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B08 RID: 11016 RVA: 0x000930F4 File Offset: 0x000912F4
		public static byte ReadByte(IntPtr ptr)
		{
			return Marshal.ReadByte(ptr, 0);
		}

		/// <summary>Reads a single byte at a given offset (or index) from an unmanaged pointer.</summary>
		/// <returns>The byte read from the <paramref name="ptr" /> parameter.</returns>
		/// <param name="ptr">The base address in unmanaged memory from which to read. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before reading. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B09 RID: 11017
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte ReadByte(IntPtr ptr, int ofs);

		/// <summary>Reads a single byte from an unmanaged pointer.</summary>
		/// <returns>The byte read from the <paramref name="ptr" /> parameter.</returns>
		/// <param name="ptr">The base address in unmanaged memory of the source object. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before reading. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B0A RID: 11018 RVA: 0x00093100 File Offset: 0x00091300
		[MonoTODO]
		public static byte ReadByte([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reads a 16-bit signed integer from the unmanaged memory.</summary>
		/// <returns>The 16-bit signed integer read from the <paramref name="ptr" /> parameter.</returns>
		/// <param name="ptr">The address in unmanaged memory from which to read. </param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format. -or-<paramref name="ptr" /> is null.-or-<paramref name="ptr" /> is invalid.  </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B0B RID: 11019 RVA: 0x00093108 File Offset: 0x00091308
		public static short ReadInt16(IntPtr ptr)
		{
			return Marshal.ReadInt16(ptr, 0);
		}

		/// <summary>Reads a 16-bit signed integer from unmanaged memory.</summary>
		/// <returns>The 16-bit signed integer read from <paramref name="ptr" />.</returns>
		/// <param name="ptr">The base address in unmanaged memory from which to read.</param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B0C RID: 11020
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short ReadInt16(IntPtr ptr, int ofs);

		/// <summary>Reads a 16-bit signed integer from unmanaged memory.</summary>
		/// <returns>The 16-bit signed integer read from the <paramref name="ptr" /> parameter.</returns>
		/// <param name="ptr">The base address in unmanaged memory of the source object. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before reading. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B0D RID: 11021 RVA: 0x00093114 File Offset: 0x00091314
		[MonoTODO]
		public static short ReadInt16([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reads a 32-bit signed integer from unmanaged memory.</summary>
		/// <returns>The 32-bit signed integer read from the <paramref name="ptr" /> parameter.</returns>
		/// <param name="ptr">The address in unmanaged from which to read. </param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format. -or-<paramref name="ptr" /> is null.  -or-<paramref name="ptr" /> is invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B0E RID: 11022 RVA: 0x0009311C File Offset: 0x0009131C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int ReadInt32(IntPtr ptr)
		{
			return Marshal.ReadInt32(ptr, 0);
		}

		/// <summary>Reads a 32-bit signed integer from unmanaged memory.</summary>
		/// <returns>The 32-bit signed integer read from the <paramref name="ptr" /> parameter.</returns>
		/// <param name="ptr">The base address in unmanaged memory from which to read. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before reading. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B0F RID: 11023
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int ReadInt32(IntPtr ptr, int ofs);

		/// <summary>Reads a 32-bit signed integer from unmanaged memory.</summary>
		/// <returns>The 32-bit signed integer read from the <paramref name="ptr" /> parameter.</returns>
		/// <param name="ptr">The base address in unmanaged memory of the source object. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before reading. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B10 RID: 11024 RVA: 0x00093128 File Offset: 0x00091328
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MonoTODO]
		public static int ReadInt32([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reads a 64-bit signed integer from unmanaged memory.</summary>
		/// <returns>The 64-bit signed integer read from the <paramref name="ptr" /> parameter.</returns>
		/// <param name="ptr">The address in unmanaged memory from which to read. </param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format. -or-<paramref name="ptr" /> is null.  -or-<paramref name="ptr" /> is invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B11 RID: 11025 RVA: 0x00093130 File Offset: 0x00091330
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static long ReadInt64(IntPtr ptr)
		{
			return Marshal.ReadInt64(ptr, 0);
		}

		/// <summary>Reads a 64-bit signed integer from unmanaged memory.</summary>
		/// <returns>The 64-bit signed integer read from the <paramref name="ptr" /> parameter.</returns>
		/// <param name="ptr">The base address in unmanaged memory from which to read. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before reading. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B12 RID: 11026
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long ReadInt64(IntPtr ptr, int ofs);

		/// <summary>Reads a 64-bit signed integer from unmanaged memory.</summary>
		/// <returns>The 64-bit signed integer read from the <paramref name="ptr" /> parameter.</returns>
		/// <param name="ptr">The base address in unmanaged memory of the source object. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before reading. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B13 RID: 11027 RVA: 0x0009313C File Offset: 0x0009133C
		[MonoTODO]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static long ReadInt64([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reads a processor native sized integer from unmanaged memory.</summary>
		/// <returns>The IntPtr read from the <paramref name="ptr" /> parameter.</returns>
		/// <param name="ptr">The address in unmanaged memory from which to read. </param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format. -or-<paramref name="ptr" /> is null. -or-<paramref name="ptr" /> is invalid. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B14 RID: 11028 RVA: 0x00093144 File Offset: 0x00091344
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static IntPtr ReadIntPtr(IntPtr ptr)
		{
			return Marshal.ReadIntPtr(ptr, 0);
		}

		/// <summary>Reads a processor native sized integer from unmanaged memory.</summary>
		/// <returns>The IntPtr read from the <paramref name="ptr" /> parameter.</returns>
		/// <param name="ptr">The base address in unmanaged memory from which to read. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before reading. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B15 RID: 11029
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ReadIntPtr(IntPtr ptr, int ofs);

		/// <summary>Reads a processor native sized integer from unmanaged memory.</summary>
		/// <returns>The IntPtr read from the <paramref name="ptr" /> parameter.</returns>
		/// <param name="ptr">The base address in unmanaged memory of the source object. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before reading. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B16 RID: 11030 RVA: 0x00093150 File Offset: 0x00091350
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MonoTODO]
		public static IntPtr ReadIntPtr([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		/// <summary>Resizes a block of memory previously allocated with <see cref="M:System.Runtime.InteropServices.Marshal.AllocCoTaskMem(System.Int32)" />.</summary>
		/// <returns>An integer representing the address of the block of memory reallocated. This memory must be released with <see cref="M:System.Runtime.InteropServices.Marshal.FreeCoTaskMem(System.IntPtr)" />.</returns>
		/// <param name="pv">A pointer to memory allocated with <see cref="M:System.Runtime.InteropServices.Marshal.AllocCoTaskMem(System.Int32)" />. </param>
		/// <param name="cb">The new size of the allocated block. </param>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to satisfy the request. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B17 RID: 11031
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ReAllocCoTaskMem(IntPtr pv, int cb);

		/// <summary>Resizes a block of memory previously allocated with <see cref="M:System.Runtime.InteropServices.Marshal.AllocHGlobal(System.IntPtr)" />.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> to the reallocated memory. This memory must be released using <see cref="M:System.Runtime.InteropServices.Marshal.FreeHGlobal(System.IntPtr)" />.</returns>
		/// <param name="pv">A pointer to memory allocated with <see cref="M:System.Runtime.InteropServices.Marshal.AllocHGlobal(System.IntPtr)" />. </param>
		/// <param name="cb">The new size of the allocated block. This is not a pointer; it is the byte count you are requesting, cast to type <see cref="T:System.IntPtr" />. If you pass a pointer, it is treated as a size.</param>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to satisfy the request. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B18 RID: 11032
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ReAllocHGlobal(IntPtr pv, IntPtr cb);

		// Token: 0x06002B19 RID: 11033
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ReleaseInternal(IntPtr pUnk);

		/// <summary>Decrements the reference count on the specified interface.</summary>
		/// <returns>The new value of the reference count on the interface specified by the <paramref name="pUnk" /> parameter.</returns>
		/// <param name="pUnk">The interface to release. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B1A RID: 11034 RVA: 0x00093158 File Offset: 0x00091358
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int Release(IntPtr pUnk)
		{
			if (pUnk == IntPtr.Zero)
			{
				throw new ArgumentException("Value cannot be null.", "pUnk");
			}
			return Marshal.ReleaseInternal(pUnk);
		}

		// Token: 0x06002B1B RID: 11035
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ReleaseComObjectInternal(object co);

		/// <summary>Decrements the reference count of the supplied runtime callable wrapper.</summary>
		/// <returns>The new value of the reference count of the runtime callable wrapper associated with <paramref name="o" />. This value is typically zero since the runtime callable wrapper keeps just one reference to the wrapped COM object regardless of the number of managed clients calling it.</returns>
		/// <param name="o">The COM object to release. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="o" /> is not a valid COM object.</exception>
		/// <exception cref="T:System.NullReferenceException">
		///   <paramref name="o" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B1C RID: 11036 RVA: 0x0009318C File Offset: 0x0009138C
		public static int ReleaseComObject(object o)
		{
			if (o == null)
			{
				throw new ArgumentException("Value cannot be null.", "o");
			}
			if (!Marshal.IsComObject(o))
			{
				throw new ArgumentException("Value must be a Com object.", "o");
			}
			return Marshal.ReleaseComObjectInternal(o);
		}

		/// <summary>Releases the thread cache.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B1D RID: 11037 RVA: 0x000931C8 File Offset: 0x000913C8
		[Obsolete]
		[MonoTODO]
		public static void ReleaseThreadCache()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets data referenced by the specified key in the specified COM object.</summary>
		/// <returns>true if the data was set successfully; otherwise, false.</returns>
		/// <param name="obj">The COM object in which to store the data. </param>
		/// <param name="key">The key in the internal hash table of the COM object in which to store the data. </param>
		/// <param name="data">The data to set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is null.-or- <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a COM object. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B1E RID: 11038 RVA: 0x000931D0 File Offset: 0x000913D0
		[MonoNotSupported("MSDN states user code should never need to call this method.")]
		public static bool SetComObjectData(object obj, object key, object data)
		{
			throw new NotSupportedException("MSDN states user code should never need to call this method.");
		}

		/// <summary>Returns the unmanaged size of an object in bytes.</summary>
		/// <returns>The size of the <paramref name="structure" /> parameter in unmanaged code.</returns>
		/// <param name="structure">The object whose size is to be returned. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="structure" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B1F RID: 11039 RVA: 0x000931DC File Offset: 0x000913DC
		[ComVisible(true)]
		public static int SizeOf(object structure)
		{
			return Marshal.SizeOf(structure.GetType());
		}

		/// <summary>Returns the size of an unmanaged type in bytes.</summary>
		/// <returns>The size of the <paramref name="structure" /> parameter in unmanaged code.</returns>
		/// <param name="t">The <see cref="T:System.Type" /> whose size is to be returned. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="t" /> parameter is a generic type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="t" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B20 RID: 11040
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int SizeOf(Type t);

		/// <summary>Allocates a BSTR and copies the contents of a managed <see cref="T:System.String" /> into it.</summary>
		/// <returns>An unmanaged pointer to the BSTR, or 0 if a null string was supplied.</returns>
		/// <param name="s">The managed string to be copied. </param>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length for <paramref name="s" /> is out of range.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B21 RID: 11041
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr StringToBSTR(string s);

		/// <summary>Copies the contents of a managed <see cref="T:System.String" /> to a block of memory allocated from the unmanaged COM task allocator.</summary>
		/// <returns>An integer representing a pointer to the block of memory allocated for the string, or 0 if a null string was supplied.</returns>
		/// <param name="s">A managed string to be copied. </param>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="s" /> parameter exceeds the maximum length allowed by the operating system.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B22 RID: 11042 RVA: 0x000931EC File Offset: 0x000913EC
		public static IntPtr StringToCoTaskMemAnsi(string s)
		{
			int num = s.Length + 1;
			IntPtr intPtr = Marshal.AllocCoTaskMem(num);
			byte[] array = new byte[num];
			for (int i = 0; i < s.Length; i++)
			{
				array[i] = (byte)s[i];
			}
			array[s.Length] = 0;
			Marshal.copy_to_unmanaged(array, 0, intPtr, num);
			return intPtr;
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.String" /> to a block of memory allocated from the unmanaged COM task allocator.</summary>
		/// <returns>The allocated memory block, or 0 if a null string was supplied.</returns>
		/// <param name="s">A managed string to be copied. </param>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length for <paramref name="s" /> is out of range.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B23 RID: 11043 RVA: 0x00093248 File Offset: 0x00091448
		public static IntPtr StringToCoTaskMemAuto(string s)
		{
			return (Marshal.SystemDefaultCharSize != 2) ? Marshal.StringToCoTaskMemAnsi(s) : Marshal.StringToCoTaskMemUni(s);
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.String" /> to a block of memory allocated from the unmanaged COM task allocator.</summary>
		/// <returns>An integer representing a pointer to the block of memory allocated for the string, or 0 if a null string was supplied.</returns>
		/// <param name="s">A managed string to be copied. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="s" /> parameter exceeds the maximum length allowed by the operating system.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B24 RID: 11044 RVA: 0x00093268 File Offset: 0x00091468
		public static IntPtr StringToCoTaskMemUni(string s)
		{
			int num = s.Length + 1;
			IntPtr intPtr = Marshal.AllocCoTaskMem(num * 2);
			char[] array = new char[num];
			s.CopyTo(0, array, 0, s.Length);
			array[s.Length] = '\0';
			Marshal.copy_to_unmanaged(array, 0, intPtr, num);
			return intPtr;
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.String" /> into unmanaged memory, converting into ANSI format as it copies.</summary>
		/// <returns>The address, in unmanaged memory, to where <paramref name="s" /> was copied, or 0 if a null string was supplied.</returns>
		/// <param name="s">A managed string to be copied. </param>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="s" /> parameter exceeds the maximum length allowed by the operating system.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B25 RID: 11045
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr StringToHGlobalAnsi(string s);

		/// <summary>Copies the contents of a managed <see cref="T:System.String" /> into unmanaged memory, converting into ANSI format if required.</summary>
		/// <returns>The address, in unmanaged memory, to where the string was copied, or 0 if a null string was supplied.</returns>
		/// <param name="s">A managed string to be copied. </param>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B26 RID: 11046 RVA: 0x000932B0 File Offset: 0x000914B0
		public static IntPtr StringToHGlobalAuto(string s)
		{
			return (Marshal.SystemDefaultCharSize != 2) ? Marshal.StringToHGlobalAnsi(s) : Marshal.StringToHGlobalUni(s);
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.String" /> into unmanaged memory.</summary>
		/// <returns>The address, in unmanaged memory, to where the <paramref name="s" /> was copied, or 0 if a null string was supplied.</returns>
		/// <param name="s">A managed string to be copied. </param>
		/// <exception cref="T:System.OutOfMemoryException">The method could not allocate enough native heap memory. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="s" /> parameter exceeds the maximum length allowed by the operating system.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B27 RID: 11047
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr StringToHGlobalUni(string s);

		/// <summary>Allocates a BSTR and copies the contents of a managed <see cref="T:System.Security.SecureString" /> object into it.</summary>
		/// <returns>The address, in unmanaged memory, where the <paramref name="s" /> parameter was copied to, or 0 if a null <see cref="T:System.Security.SecureString" /> object was supplied.</returns>
		/// <param name="s">The managed <see cref="T:System.Security.SecureString" /> object to be copied.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The current computer is not running Windows 2000 Service Pack 3 or later.  </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B28 RID: 11048 RVA: 0x000932D0 File Offset: 0x000914D0
		public static IntPtr SecureStringToBSTR(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			int length = s.Length;
			IntPtr intPtr = Marshal.AllocCoTaskMem((length + 1) * 2 + 4);
			byte[] array = null;
			Marshal.WriteInt32(intPtr, 0, length * 2);
			try
			{
				array = s.GetBuffer();
				for (int i = 0; i < length; i++)
				{
					Marshal.WriteInt16(intPtr, 4 + i * 2, (short)((int)array[i * 2] << 8 | (int)array[i * 2 + 1]));
				}
				Marshal.WriteInt16(intPtr, 4 + array.Length, 0);
			}
			finally
			{
				if (array != null)
				{
					int j = array.Length;
					while (j > 0)
					{
						j--;
						array[j] = 0;
					}
				}
			}
			return (IntPtr)((long)intPtr + 4L);
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.Security.SecureString" /> object to a block of memory allocated from the unmanaged COM task allocator.</summary>
		/// <returns>The address, in unmanaged memory, where the <paramref name="s" /> parameter was copied to, or 0 if a null <see cref="T:System.Security.SecureString" /> object was supplied.</returns>
		/// <param name="s">The managed <see cref="T:System.Security.SecureString" /> object to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The current computer is not running Windows 2000 Service Pack 3 or later.  </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B29 RID: 11049 RVA: 0x000933A4 File Offset: 0x000915A4
		public static IntPtr SecureStringToCoTaskMemAnsi(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			int length = s.Length;
			IntPtr intPtr = Marshal.AllocCoTaskMem(length + 1);
			byte[] array = new byte[length + 1];
			try
			{
				byte[] buffer = s.GetBuffer();
				int i = 0;
				int num = 0;
				while (i < length)
				{
					array[i] = buffer[num + 1];
					buffer[num] = 0;
					buffer[num + 1] = 0;
					i++;
					num += 2;
				}
				array[i] = 0;
				Marshal.copy_to_unmanaged(array, 0, intPtr, length + 1);
			}
			finally
			{
				int j = length;
				while (j > 0)
				{
					j--;
					array[j] = 0;
				}
			}
			return intPtr;
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.Security.SecureString" /> object to a block of memory allocated from the unmanaged COM task allocator.</summary>
		/// <returns>The address, in unmanaged memory, where the <paramref name="s" /> parameter was copied to, or 0 if a null <see cref="T:System.Security.SecureString" /> object was supplied.</returns>
		/// <param name="s">The managed <see cref="T:System.Security.SecureString" /> object to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The current computer is not running Windows 2000 Service Pack 3 or later.  </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available. </exception>
		// Token: 0x06002B2A RID: 11050 RVA: 0x00093468 File Offset: 0x00091668
		public static IntPtr SecureStringToCoTaskMemUnicode(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			int length = s.Length;
			IntPtr intPtr = Marshal.AllocCoTaskMem(length * 2 + 2);
			byte[] array = null;
			try
			{
				array = s.GetBuffer();
				for (int i = 0; i < length; i++)
				{
					Marshal.WriteInt16(intPtr, i * 2, (short)((int)array[i * 2] << 8 | (int)array[i * 2 + 1]));
				}
				Marshal.WriteInt16(intPtr, array.Length, 0);
			}
			finally
			{
				if (array != null)
				{
					int j = array.Length;
					while (j > 0)
					{
						j--;
						array[j] = 0;
					}
				}
			}
			return intPtr;
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.Security.SecureString" /> into unmanaged memory, converting into ANSI format as it copies.</summary>
		/// <returns>The address, in unmanaged memory, to where the <paramref name="s" /> parameter was copied, or 0 if a null <see cref="T:System.Security.SecureString" /> was supplied.</returns>
		/// <param name="s">The managed <see cref="T:System.Security.SecureString" /> to be copied.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The current computer is not running Windows 2000 Service Pack 3 or later.  </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B2B RID: 11051 RVA: 0x0009351C File Offset: 0x0009171C
		public static IntPtr SecureStringToGlobalAllocAnsi(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return Marshal.SecureStringToCoTaskMemAnsi(s);
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.Security.SecureString" /> object into unmanaged memory.</summary>
		/// <returns>The address, in unmanaged memory, where <paramref name="s" /> was copied, or 0 if <paramref name="s" /> is a <see cref="T:System.Security.SecureString" /> object whose length is 0.</returns>
		/// <param name="s">The managed object to be copied.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The current computer is not running Windows 2000 Service Pack 3 or later.  </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available. </exception>
		// Token: 0x06002B2C RID: 11052 RVA: 0x00093538 File Offset: 0x00091738
		public static IntPtr SecureStringToGlobalAllocUnicode(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return Marshal.SecureStringToCoTaskMemUnicode(s);
		}

		/// <summary>Marshals data from a managed object to an unmanaged block of memory.</summary>
		/// <param name="structure">A managed object holding the data to be marshaled. This object must be an instance of a formatted class. </param>
		/// <param name="ptr">A pointer to an unmanaged block of memory, which must be allocated before this method is called. </param>
		/// <param name="fDeleteOld">true to have the <see cref="M:System.Runtime.InteropServices.Marshal.DestroyStructure(System.IntPtr,System.Type)" /> method called on the <paramref name="ptr" /> parameter before this method executes. Note that passing false can lead to a memory leak. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="structure" /> parameter is a generic type.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B2D RID: 11053
		[ComVisible(true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StructureToPtr(object structure, IntPtr ptr, bool fDeleteOld);

		/// <summary>Throws an exception with a specific failure HRESULT value.</summary>
		/// <param name="errorCode">The HRESULT corresponding to the desired exception. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B2E RID: 11054 RVA: 0x00093554 File Offset: 0x00091754
		public static void ThrowExceptionForHR(int errorCode)
		{
			Exception exceptionForHR = Marshal.GetExceptionForHR(errorCode);
			if (exceptionForHR != null)
			{
				throw exceptionForHR;
			}
		}

		/// <summary>Throws an exception with a specific failure HRESULT.</summary>
		/// <param name="errorCode">The HRESULT corresponding to the desired exception. </param>
		/// <param name="errorInfo">A pointer to the IErrorInfo interface that provides more information about the error. You can specify IntPtr(0) to use the current IErrorInfo interface, or IntPtr(-1) to ignore the current IErrorInfo interface and construct the exception just from the error code.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B2F RID: 11055 RVA: 0x00093570 File Offset: 0x00091770
		public static void ThrowExceptionForHR(int errorCode, IntPtr errorInfo)
		{
			Exception exceptionForHR = Marshal.GetExceptionForHR(errorCode, errorInfo);
			if (exceptionForHR != null)
			{
				throw exceptionForHR;
			}
		}

		/// <summary>Gets the address of the element at the specified index inside the specified array.</summary>
		/// <returns>The address of <paramref name="index" /> inside <paramref name="arr" />.</returns>
		/// <param name="arr">The <see cref="T:System.Array" /> containing the desired element. </param>
		/// <param name="index">The index in the <paramref name="arr" /> parameter of the desired element. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B30 RID: 11056
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr UnsafeAddrOfPinnedArrayElement(Array arr, int index);

		/// <summary>Writes a single byte value to unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged to write. </param>
		/// <param name="val">The value to write. </param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format. -or-<paramref name="ptr" /> is null.-or-<paramref name="ptr" /> is invalid.  </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B31 RID: 11057 RVA: 0x00093590 File Offset: 0x00091790
		public static void WriteByte(IntPtr ptr, byte val)
		{
			Marshal.WriteByte(ptr, 0, val);
		}

		/// <summary>Writes a single byte value to unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory to write. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before writing. </param>
		/// <param name="val">The value to write. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B32 RID: 11058
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WriteByte(IntPtr ptr, int ofs, byte val);

		/// <summary>Writes a single byte value to unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the target object. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before writing. </param>
		/// <param name="val">The value to write. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B33 RID: 11059 RVA: 0x0009359C File Offset: 0x0009179C
		[MonoTODO]
		public static void WriteByte([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, byte val)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes a 16-bit integer value to unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory to write. </param>
		/// <param name="val">The value to write. </param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format. -or-<paramref name="ptr" /> is null. -or-<paramref name="ptr" /> is invalid. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B34 RID: 11060 RVA: 0x000935A4 File Offset: 0x000917A4
		public static void WriteInt16(IntPtr ptr, short val)
		{
			Marshal.WriteInt16(ptr, 0, val);
		}

		/// <summary>Writes a 16-bit signed integer value into unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory to write. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before writing. </param>
		/// <param name="val">The value to write. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B35 RID: 11061
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WriteInt16(IntPtr ptr, int ofs, short val);

		/// <summary>Writes a 16-bit signed integer value to unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the target object. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before writing. </param>
		/// <param name="val">The value to write. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B36 RID: 11062 RVA: 0x000935B0 File Offset: 0x000917B0
		[MonoTODO]
		public static void WriteInt16([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, short val)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes a 16-bit signed integer value to unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory to write. </param>
		/// <param name="val">The value to write. </param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format. -or-<paramref name="ptr" /> is null.-or-<paramref name="ptr" /> is invalid.  </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B37 RID: 11063 RVA: 0x000935B8 File Offset: 0x000917B8
		public static void WriteInt16(IntPtr ptr, char val)
		{
			Marshal.WriteInt16(ptr, 0, val);
		}

		/// <summary>Writes a 16-bit signed integer value to unmanaged memory.</summary>
		/// <param name="ptr">The base address in the native heap to write. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before writing. </param>
		/// <param name="val">The value to write. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B38 RID: 11064
		[MonoTODO]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WriteInt16(IntPtr ptr, int ofs, char val);

		/// <summary>Writes a 16-bit signed integer value to unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the target object. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before writing. </param>
		/// <param name="val">The value to write. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B39 RID: 11065 RVA: 0x000935C4 File Offset: 0x000917C4
		[MonoTODO]
		public static void WriteInt16([In] [Out] object ptr, int ofs, char val)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes a 32-bit signed integer value to unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory to write. </param>
		/// <param name="val">The value to write. </param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format. -or-<paramref name="ptr" /> is null. -or-<paramref name="ptr" /> is invalid. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B3A RID: 11066 RVA: 0x000935CC File Offset: 0x000917CC
		public static void WriteInt32(IntPtr ptr, int val)
		{
			Marshal.WriteInt32(ptr, 0, val);
		}

		/// <summary>Writes a 32-bit signed integer value into unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory to write. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before writing. </param>
		/// <param name="val">The value to write. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B3B RID: 11067
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WriteInt32(IntPtr ptr, int ofs, int val);

		/// <summary>Writes a 32-bit signed integer value to unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the target object. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before writing. </param>
		/// <param name="val">The value to write. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B3C RID: 11068 RVA: 0x000935D8 File Offset: 0x000917D8
		[MonoTODO]
		public static void WriteInt32([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, int val)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes a 64-bit signed integer value to unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory to write. </param>
		/// <param name="val">The value to write. </param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format. -or-<paramref name="ptr" /> is null.  -or-<paramref name="ptr" /> is invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B3D RID: 11069 RVA: 0x000935E0 File Offset: 0x000917E0
		public static void WriteInt64(IntPtr ptr, long val)
		{
			Marshal.WriteInt64(ptr, 0, val);
		}

		/// <summary>Writes a 64-bit signed integer value to unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory to write. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before writing. </param>
		/// <param name="val">The value to write. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B3E RID: 11070
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WriteInt64(IntPtr ptr, int ofs, long val);

		/// <summary>Writes a 64-bit signed integer value to unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the target object. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before writing. </param>
		/// <param name="val">The value to write. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B3F RID: 11071 RVA: 0x000935EC File Offset: 0x000917EC
		[MonoTODO]
		public static void WriteInt64([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, long val)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes a processor native sized integer value into unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory to write. </param>
		/// <param name="val">The value to write. </param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format. -or-<paramref name="ptr" /> is null.  -or-<paramref name="ptr" /> is invalid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B40 RID: 11072 RVA: 0x000935F4 File Offset: 0x000917F4
		public static void WriteIntPtr(IntPtr ptr, IntPtr val)
		{
			Marshal.WriteIntPtr(ptr, 0, val);
		}

		/// <summary>Writes a processor native sized integer value to unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory to write. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before writing. </param>
		/// <param name="val">The value to write. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B41 RID: 11073
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WriteIntPtr(IntPtr ptr, int ofs, IntPtr val);

		/// <summary>Writes a processor native sized integer value to unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the target object. </param>
		/// <param name="ofs">An additional byte offset, added to the <paramref name="ptr" /> parameter before writing. </param>
		/// <param name="val">The value to write. </param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B42 RID: 11074 RVA: 0x00093600 File Offset: 0x00091800
		[MonoTODO]
		public static void WriteIntPtr([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, IntPtr val)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts the specified HRESULT error code to a corresponding <see cref="T:System.Exception" /> object.</summary>
		/// <returns>An <see cref="T:System.Exception" /> object representing the converted HRESULT.</returns>
		/// <param name="errorCode">The HRESULT to be converted.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B43 RID: 11075 RVA: 0x00093608 File Offset: 0x00091808
		public static Exception GetExceptionForHR(int errorCode)
		{
			return Marshal.GetExceptionForHR(errorCode, IntPtr.Zero);
		}

		/// <summary>Converts the specified HRESULT error code to a corresponding <see cref="T:System.Exception" /> object, with additional error information passed in an IErrorInfo interface for the exception object.</summary>
		/// <returns>An <see cref="T:System.Exception" /> object representing the converted HRESULT and information obtained from <paramref name="errorInfo" />.</returns>
		/// <param name="errorCode">The HRESULT to be converted.</param>
		/// <param name="errorInfo">A pointer to the IErrorInfo interface that provides more information about the error. You can specify IntPtr(0) to use the current IErrorInfo interface, or IntPtr(-1) to ignore the current IErrorInfo interface and construct the exception just from the error code. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B44 RID: 11076 RVA: 0x00093618 File Offset: 0x00091818
		public static Exception GetExceptionForHR(int errorCode, IntPtr errorInfo)
		{
			if (errorCode == -2147024882)
			{
				return new OutOfMemoryException();
			}
			if (errorCode == -2147024809)
			{
				return new ArgumentException();
			}
			if (errorCode < 0)
			{
				return new COMException(string.Empty, errorCode);
			}
			return null;
		}

		/// <summary>Releases all references to a runtime callable wrapper (RCW) by setting the reference count of the supplied RCW to 0.</summary>
		/// <returns>The new value of the reference count of the RCW associated with the <paramref name="o" />parameter, which is zero if the release is successful.</returns>
		/// <param name="o">The RCW to be released.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="o" /> is not a valid COM object.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="o" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B45 RID: 11077 RVA: 0x00093664 File Offset: 0x00091864
		public static int FinalReleaseComObject(object o)
		{
			while (Marshal.ReleaseComObject(o) != 0)
			{
			}
			return 0;
		}

		// Token: 0x06002B46 RID: 11078
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Delegate GetDelegateForFunctionPointerInternal(IntPtr ptr, Type t);

		/// <summary>Converts an unmanaged function pointer to a delegate.</summary>
		/// <returns>A delegate instance that can be cast to the appropriate delegate type.</returns>
		/// <param name="ptr">An <see cref="T:System.IntPtr" /> type that is the unmanaged function pointer to be converted. </param>
		/// <param name="t">The type of the delegate to be returned. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="t" /> parameter is not a delegate or is generic.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="ptr" /> parameter is null.-or-The <paramref name="t" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B47 RID: 11079 RVA: 0x00093678 File Offset: 0x00091878
		public static Delegate GetDelegateForFunctionPointer(IntPtr ptr, Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			if (!t.IsSubclassOf(typeof(MulticastDelegate)) || t == typeof(MulticastDelegate))
			{
				throw new ArgumentException("Type is not a delegate", "t");
			}
			if (ptr == IntPtr.Zero)
			{
				throw new ArgumentNullException("ptr");
			}
			return Marshal.GetDelegateForFunctionPointerInternal(ptr, t);
		}

		// Token: 0x06002B48 RID: 11080
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetFunctionPointerForDelegateInternal(Delegate d);

		/// <summary>Converts a delegate into a function pointer callable from unmanaged code.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> value that can be passed to unmanaged code, which in turn can use it to call the underlying managed delegate.</returns>
		/// <param name="d">The delegate to be passed to unmanaged code. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="d" /> parameter is a generic type</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="d" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B49 RID: 11081 RVA: 0x000936F0 File Offset: 0x000918F0
		public static IntPtr GetFunctionPointerForDelegate(Delegate d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			return Marshal.GetFunctionPointerForDelegateInternal(d);
		}

		/// <summary>Represents the maximum size of a double byte character set (DBCS) size, in bytes, for the current operating system. This field is read-only.</summary>
		// Token: 0x0400115E RID: 4446
		public static readonly int SystemMaxDBCSCharSize = 2;

		/// <summary>Represents the default character size on the system; the default is 2 for Unicode systems and 1 for ANSI systems. This field is read-only.</summary>
		// Token: 0x0400115F RID: 4447
		public static readonly int SystemDefaultCharSize = (Environment.OSVersion.Platform != PlatformID.Win32NT) ? 1 : 2;
	}
}
