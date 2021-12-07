using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System
{
	/// <summary>
	///   <see cref="T:System.RuntimeMethodHandle" /> is a handle to the internal metadata representation of a method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200016F RID: 367
	[MonoTODO("Serialization needs tests")]
	[ComVisible(true)]
	[Serializable]
	public struct RuntimeMethodHandle : ISerializable
	{
		// Token: 0x06001367 RID: 4967 RVA: 0x0004D970 File Offset: 0x0004BB70
		internal RuntimeMethodHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0004D97C File Offset: 0x0004BB7C
		private RuntimeMethodHandle(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MonoMethod monoMethod = (MonoMethod)info.GetValue("MethodObj", typeof(MonoMethod));
			this.value = monoMethod.MethodHandle.Value;
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException(Locale.GetText("Insufficient state."));
			}
		}

		/// <summary>Gets the value of this instance.</summary>
		/// <returns>A <see cref="T:System.RuntimeMethodHandle" /> that is the internal metadata representation of a method.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x0004D9F0 File Offset: 0x0004BBF0
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data necessary to deserialize the field represented by this instance.</summary>
		/// <param name="info">The object to populate with serialization information. </param>
		/// <param name="context">(Reserved) The place to store and retrieve serialized data. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">
		///   <see cref="P:System.RuntimeMethodHandle.Value" /> is invalid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600136A RID: 4970 RVA: 0x0004D9F8 File Offset: 0x0004BBF8
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException("Object fields may not be properly initialized");
			}
			info.AddValue("MethodObj", (MonoMethod)MethodBase.GetMethodFromHandle(this), typeof(MonoMethod));
		}

		// Token: 0x0600136B RID: 4971
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetFunctionPointer(IntPtr m);

		/// <summary>Obtains a pointer to the method represented by this instance.</summary>
		/// <returns>A pointer to the method represented by this instance.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the necessary permission to perform this operation.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600136C RID: 4972 RVA: 0x0004DA5C File Offset: 0x0004BC5C
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		public IntPtr GetFunctionPointer()
		{
			return RuntimeMethodHandle.GetFunctionPointer(this.value);
		}

		/// <summary>Indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.RuntimeMethodHandle" /> and equal to the value of this instance; otherwise, false.</returns>
		/// <param name="obj">A <see cref="T:System.Object" /> to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600136D RID: 4973 RVA: 0x0004DA6C File Offset: 0x0004BC6C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public override bool Equals(object obj)
		{
			return obj != null && base.GetType() == obj.GetType() && this.value == ((RuntimeMethodHandle)obj).Value;
		}

		/// <summary>Indicates whether this instance is equal to a specified <see cref="T:System.RuntimeMethodHandle" />.</summary>
		/// <returns>true if <paramref name="handle" /> is equal to the value of this instance; otherwise, false.</returns>
		/// <param name="handle">A <see cref="T:System.RuntimeMethodHandle" /> to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600136E RID: 4974 RVA: 0x0004DAB8 File Offset: 0x0004BCB8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public bool Equals(RuntimeMethodHandle handle)
		{
			return this.value == handle.Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600136F RID: 4975 RVA: 0x0004DACC File Offset: 0x0004BCCC
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		/// <summary>Indicates whether two instances of <see cref="T:System.RuntimeMethodHandle" /> are equal.</summary>
		/// <returns>true if the value of <paramref name="left" /> is equal to the value of <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">A <see cref="T:System.RuntimeMethodHandle" /> to compare to <paramref name="right" />.</param>
		/// <param name="right">A <see cref="T:System.RuntimeMethodHandle" /> to compare to <paramref name="left" />.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001370 RID: 4976 RVA: 0x0004DADC File Offset: 0x0004BCDC
		public static bool operator ==(RuntimeMethodHandle left, RuntimeMethodHandle right)
		{
			return left.Equals(right);
		}

		/// <summary>Indicates whether two instances of <see cref="T:System.RuntimeMethodHandle" /> are not equal.</summary>
		/// <returns>true if the value of <paramref name="left" /> is unequal to the value of <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">A <see cref="T:System.RuntimeMethodHandle" /> to compare to <paramref name="right" />.</param>
		/// <param name="right">A <see cref="T:System.RuntimeMethodHandle" /> to compare to <paramref name="left" />.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001371 RID: 4977 RVA: 0x0004DAE8 File Offset: 0x0004BCE8
		public static bool operator !=(RuntimeMethodHandle left, RuntimeMethodHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040005A6 RID: 1446
		private IntPtr value;
	}
}
