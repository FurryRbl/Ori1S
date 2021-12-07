using System;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents a runtime handle for a module.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000170 RID: 368
	[ComVisible(true)]
	public struct ModuleHandle
	{
		// Token: 0x06001372 RID: 4978 RVA: 0x0004DAF8 File Offset: 0x0004BCF8
		internal ModuleHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x0004DB18 File Offset: 0x0004BD18
		internal IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Gets the metadata stream version.</summary>
		/// <returns>A 32-bit integer representing the metadata stream version. The high-order two bytes represent the major version number, and the low-order two bytes represent the minor version number.</returns>
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x0004DB20 File Offset: 0x0004BD20
		public int MDStreamVersion
		{
			get
			{
				if (this.value == IntPtr.Zero)
				{
					throw new ArgumentNullException(string.Empty, "Invalid handle");
				}
				return Module.GetMDStreamVersion(this.value);
			}
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0004DB60 File Offset: 0x0004BD60
		internal void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			if (this.value == IntPtr.Zero)
			{
				throw new ArgumentNullException(string.Empty, "Invalid handle");
			}
			Module.GetPEKind(this.value, out peKind, out machine);
		}

		/// <summary>Returns a runtime handle for the field identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.RuntimeFieldHandle" /> for the field identified by <paramref name="fieldToken" />.</returns>
		/// <param name="fieldToken">A metadata token that identifies a field in the module.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.-or-<paramref name="metadataToken" /> is not a token for a field in the scope of the current module.-or-<paramref name="metadataToken" /> identifies a field whose parent TypeSpec has a signature containing element type var or mvar.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method is called on an empty field handle.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001377 RID: 4983 RVA: 0x0004DBA0 File Offset: 0x0004BDA0
		public RuntimeFieldHandle ResolveFieldHandle(int fieldToken)
		{
			return this.ResolveFieldHandle(fieldToken, null, null);
		}

		/// <summary>Returns a runtime method handle for the method or constructor identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.RuntimeMethodHandle" /> for the method or constructor identified by <paramref name="methodToken" />.</returns>
		/// <param name="methodToken">A metadata token that identifies a method or constructor in the module.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="methodToken" /> is not a valid metadata token for a method in the current module.-or-<paramref name="metadataToken" /> is not a token for a method or constructor in the scope of the current module.-or-<paramref name="metadataToken" /> is a MethodSpec whose signature contains element type var or mvar.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method is called on an empty method handle.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001378 RID: 4984 RVA: 0x0004DBAC File Offset: 0x0004BDAC
		public RuntimeMethodHandle ResolveMethodHandle(int methodToken)
		{
			return this.ResolveMethodHandle(methodToken, null, null);
		}

		/// <summary>Returns a runtime type handle for the type identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.RuntimeTypeHandle" /> for the type identified by <paramref name="typeToken" />.</returns>
		/// <param name="typeToken">A metadata token that identifies a type in the module.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="typeToken" /> is not a valid metadata token for a type in the current module.-or-<paramref name="metadataToken" /> is not a token for a type in the scope of the current module.-or-<paramref name="metadataToken" /> is a TypeSpec whose signature contains element type var or mvar.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method is called on an empty type handle.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001379 RID: 4985 RVA: 0x0004DBB8 File Offset: 0x0004BDB8
		public RuntimeTypeHandle ResolveTypeHandle(int typeToken)
		{
			return this.ResolveTypeHandle(typeToken, null, null);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0004DBC4 File Offset: 0x0004BDC4
		private IntPtr[] ptrs_from_handles(RuntimeTypeHandle[] handles)
		{
			if (handles == null)
			{
				return null;
			}
			IntPtr[] array = new IntPtr[handles.Length];
			for (int i = 0; i < handles.Length; i++)
			{
				array[i] = handles[i].Value;
			}
			return array;
		}

		/// <summary>Returns a runtime type handle for the type identified by the specified metadata token, specifying the generic type arguments of the type and method where the token is in scope.</summary>
		/// <returns>A <see cref="T:System.RuntimeTypeHandle" /> for the type identified by <paramref name="typeToken" />.</returns>
		/// <param name="typeToken">A metadata token that identifies a type in the module.</param>
		/// <param name="typeInstantiationContext">An array of <see cref="T:System.RuntimeTypeHandle" /> structures representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="methodInstantiationContext">An array of <see cref="T:System.RuntimeTypeHandle" /> structures objects representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="typeToken" /> is not a valid metadata token for a type in the current module.-or-<paramref name="metadataToken" /> is not a token for a type in the scope of the current module.-or-<paramref name="metadataToken" /> is a TypeSpec whose signature contains element type var or mvar.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method is called on an empty type handle.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="typeToken " />is not a valid token.</exception>
		// Token: 0x0600137B RID: 4987 RVA: 0x0004DC10 File Offset: 0x0004BE10
		public RuntimeTypeHandle ResolveTypeHandle(int typeToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			if (this.value == IntPtr.Zero)
			{
				throw new ArgumentNullException(string.Empty, "Invalid handle");
			}
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = Module.ResolveTypeToken(this.value, typeToken, this.ptrs_from_handles(typeInstantiationContext), this.ptrs_from_handles(methodInstantiationContext), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw new TypeLoadException(string.Format("Could not load type '0x{0:x}' from assembly '0x{1:x}'", typeToken, this.value.ToInt64()));
			}
			return new RuntimeTypeHandle(intPtr);
		}

		/// <summary>Returns a runtime method handle for the method or constructor identified by the specified metadata token, specifying the generic type arguments of the type and method where the token is in scope.</summary>
		/// <returns>A <see cref="T:System.RuntimeMethodHandle" /> for the method or constructor identified by <paramref name="methodToken" />.</returns>
		/// <param name="methodToken">A metadata token that identifies a method or constructor in the module.</param>
		/// <param name="typeInstantiationContext">An array of <see cref="T:System.RuntimeTypeHandle" /> structures representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="methodInstantiationContext">An array of <see cref="T:System.RuntimeTypeHandle" /> structures representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="methodToken" /> is not a valid metadata token for a method in the current module.-or-<paramref name="metadataToken" /> is not a token for a method or constructor in the scope of the current module.-or-<paramref name="metadataToken" /> is a MethodSpec whose signature contains element type var or mvar.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method is called on an empty method handle.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="methodToken " />is not a valid token.</exception>
		// Token: 0x0600137C RID: 4988 RVA: 0x0004DC9C File Offset: 0x0004BE9C
		public RuntimeMethodHandle ResolveMethodHandle(int methodToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			if (this.value == IntPtr.Zero)
			{
				throw new ArgumentNullException(string.Empty, "Invalid handle");
			}
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = Module.ResolveMethodToken(this.value, methodToken, this.ptrs_from_handles(typeInstantiationContext), this.ptrs_from_handles(methodInstantiationContext), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw new Exception(string.Format("Could not load method '0x{0:x}' from assembly '0x{1:x}'", methodToken, this.value.ToInt64()));
			}
			return new RuntimeMethodHandle(intPtr);
		}

		/// <summary>Returns a runtime field handle for the field identified by the specified metadata token, specifying the generic type arguments of the type and method where the token is in scope.</summary>
		/// <returns>A <see cref="T:System.RuntimeFieldHandle" /> for the field identified by <paramref name="fieldToken" />.</returns>
		/// <param name="fieldToken">A metadata token that identifies a field in the module.</param>
		/// <param name="typeInstantiationContext">An array of <see cref="T:System.RuntimeTypeHandle" /> structures representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="methodInstantiationContext">An array of <see cref="T:System.RuntimeTypeHandle" /> structures representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.-or-<paramref name="metadataToken" /> is not a token for a field in the scope of the current module.-or-<paramref name="metadataToken" /> identifies a field whose parent TypeSpec has a signature containing element type var or mvar.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method is called on an empty field handle.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="fieldToken " />is not a valid token.</exception>
		// Token: 0x0600137D RID: 4989 RVA: 0x0004DD28 File Offset: 0x0004BF28
		public RuntimeFieldHandle ResolveFieldHandle(int fieldToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			if (this.value == IntPtr.Zero)
			{
				throw new ArgumentNullException(string.Empty, "Invalid handle");
			}
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = Module.ResolveFieldToken(this.value, fieldToken, this.ptrs_from_handles(typeInstantiationContext), this.ptrs_from_handles(methodInstantiationContext), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw new Exception(string.Format("Could not load field '0x{0:x}' from assembly '0x{1:x}'", fieldToken, this.value.ToInt64()));
			}
			return new RuntimeFieldHandle(intPtr);
		}

		/// <summary>Returns a runtime handle for the field identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.RuntimeFieldHandle" /> for the field identified by <paramref name="fieldToken" />.</returns>
		/// <param name="fieldToken">A metadata token that identifies a field in the module.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600137E RID: 4990 RVA: 0x0004DDB4 File Offset: 0x0004BFB4
		public RuntimeFieldHandle GetRuntimeFieldHandleFromMetadataToken(int fieldToken)
		{
			return this.ResolveFieldHandle(fieldToken);
		}

		/// <summary>Returns a runtime method handle for the method or constructor identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.RuntimeMethodHandle" /> for the method or constructor identified by <paramref name="methodToken" />.</returns>
		/// <param name="methodToken">A metadata token that identifies a method or constructor in the module.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600137F RID: 4991 RVA: 0x0004DDC0 File Offset: 0x0004BFC0
		public RuntimeMethodHandle GetRuntimeMethodHandleFromMetadataToken(int methodToken)
		{
			return this.ResolveMethodHandle(methodToken);
		}

		/// <summary>Returns a runtime type handle for the type identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.RuntimeTypeHandle" /> for the type identified by <paramref name="typeToken" />.</returns>
		/// <param name="typeToken">A metadata token that identifies a type in the module.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001380 RID: 4992 RVA: 0x0004DDCC File Offset: 0x0004BFCC
		public RuntimeTypeHandle GetRuntimeTypeHandleFromMetadataToken(int typeToken)
		{
			return this.ResolveTypeHandle(typeToken);
		}

		/// <summary>Returns a <see cref="T:System.Boolean" /> value indicating whether the specified object is a <see cref="T:System.ModuleHandle" /> structure, and equal to the current <see cref="T:System.ModuleHandle" />.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.ModuleHandle" /> structure, and is equal to the current <see cref="T:System.ModuleHandle" /> structure; otherwise, false.</returns>
		/// <param name="obj">The object to be compared with the current <see cref="T:System.ModuleHandle" /> structure.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001381 RID: 4993 RVA: 0x0004DDD8 File Offset: 0x0004BFD8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public override bool Equals(object obj)
		{
			return obj != null && base.GetType() == obj.GetType() && this.value == ((ModuleHandle)obj).Value;
		}

		/// <summary>Returns a <see cref="T:System.Boolean" /> value indicating whether the specified <see cref="T:System.ModuleHandle" /> structure is equal to the current <see cref="T:System.ModuleHandle" />.</summary>
		/// <returns>true if <paramref name="handle" /> is equal to the current <see cref="T:System.ModuleHandle" /> structure; otherwise false.</returns>
		/// <param name="handle">The <see cref="T:System.ModuleHandle" /> structure to be compared with the current <see cref="T:System.ModuleHandle" />.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001382 RID: 4994 RVA: 0x0004DE24 File Offset: 0x0004C024
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public bool Equals(ModuleHandle handle)
		{
			return this.value == handle.Value;
		}

		/// <filterpriority>2</filterpriority>
		// Token: 0x06001383 RID: 4995 RVA: 0x0004DE38 File Offset: 0x0004C038
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		/// <summary>Tests whether two <see cref="T:System.ModuleHandle" /> structures are equal.</summary>
		/// <returns>true if the <see cref="T:System.ModuleHandle" /> structures are equal; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.ModuleHandle" /> structure to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.ModuleHandle" /> structure to the right of the equality operator.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001384 RID: 4996 RVA: 0x0004DE48 File Offset: 0x0004C048
		public static bool operator ==(ModuleHandle left, ModuleHandle right)
		{
			return object.Equals(left, right);
		}

		/// <summary>Tests whether two <see cref="T:System.ModuleHandle" /> structures are unequal.</summary>
		/// <returns>true if the <see cref="T:System.ModuleHandle" /> structures are unequal; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.ModuleHandle" /> structure to the left of the inequality operator.</param>
		/// <param name="right">The <see cref="T:System.ModuleHandle" /> structure to the right of the inequality operator.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001385 RID: 4997 RVA: 0x0004DE5C File Offset: 0x0004C05C
		public static bool operator !=(ModuleHandle left, ModuleHandle right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x040005A7 RID: 1447
		private IntPtr value;

		/// <summary>Represents an empty module handle.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040005A8 RID: 1448
		public static readonly ModuleHandle EmptyHandle = new ModuleHandle(IntPtr.Zero);
	}
}
