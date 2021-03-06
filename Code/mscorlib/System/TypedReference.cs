using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System
{
	/// <summary>Describes objects that contain both a managed pointer to a location and a runtime representation of the type that may be stored at that location.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005D RID: 93
	[CLSCompliant(false)]
	[ComVisible(true)]
	public struct TypedReference
	{
		/// <summary>Checks if this object is equal to the specified object.</summary>
		/// <returns>true if this object is equal to the specified object; otherwise, false.</returns>
		/// <param name="o">The object with which to compare the current object. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000697 RID: 1687 RVA: 0x00014D80 File Offset: 0x00012F80
		public override bool Equals(object o)
		{
			throw new NotSupportedException(Locale.GetText("This operation is not supported for this type."));
		}

		/// <summary>Returns the hash code of this object.</summary>
		/// <returns>The hash code of this object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000698 RID: 1688 RVA: 0x00014D94 File Offset: 0x00012F94
		public override int GetHashCode()
		{
			if (this.type.Value == IntPtr.Zero)
			{
				return 0;
			}
			return Type.GetTypeFromHandle(this.type).GetHashCode();
		}

		/// <summary>Returns the type of the target of the specified TypedReference.</summary>
		/// <returns>The type of the target of the specified TypedReference.</returns>
		/// <param name="value">The value whose target's type is to be returned. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000699 RID: 1689 RVA: 0x00014DD0 File Offset: 0x00012FD0
		public static Type GetTargetType(TypedReference value)
		{
			return Type.GetTypeFromHandle(value.type);
		}

		/// <summary>Makes a TypedReference for a field identified by a specified object and list of field descriptions.</summary>
		/// <returns>A <see cref="T:System.TypedReference" /> for the field described by the last element of <paramref name="flds" />.</returns>
		/// <param name="target">An object that contains the field described by the first element of <paramref name="flds" />. </param>
		/// <param name="flds">A list of field descriptions where each element describes a field that contains the field described by the succeeding element. Each described field must be a value type. The field descriptions must be RuntimeFieldInfo objects supplied by the type system.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> or <paramref name="flds" /> is null.-or- An element of <paramref name="flds" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flds" /> array has no elements.-or- An element of <paramref name="flds" /> is not a RuntimeFieldInfo.-or- The <see cref="P:System.Reflection.FieldInfo.IsInitOnly" /> or <see cref="P:System.Reflection.FieldInfo.IsStatic" /> property of an element of <paramref name="flds" /> is true. </exception>
		/// <exception cref="T:System.MissingMemberException">Parameter <paramref name="target" /> does not contain the field described by the first element of <paramref name="flds" />, or an element of <paramref name="flds" /> describes a field that is not contained in the field described by the succeeding element of <paramref name="flds" />.-or- The field described by an element of <paramref name="flds" /> is not a value type. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x0600069A RID: 1690 RVA: 0x00014DE0 File Offset: 0x00012FE0
		[MonoTODO]
		[CLSCompliant(false)]
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"MemberAccess\"/>\n</PermissionSet>\n")]
		public static TypedReference MakeTypedReference(object target, FieldInfo[] flds)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (flds == null)
			{
				throw new ArgumentNullException("flds");
			}
			if (flds.Length == 0)
			{
				throw new ArgumentException(Locale.GetText("flds has no elements"));
			}
			throw new NotImplementedException();
		}

		/// <summary>Converts the specified value to a TypedReference. This method is not supported.</summary>
		/// <param name="target">The target of the conversion. </param>
		/// <param name="value">The value to be converted. </param>
		/// <exception cref="T:System.NotSupportedException">In all cases. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600069B RID: 1691 RVA: 0x00014E2C File Offset: 0x0001302C
		[MonoTODO]
		[CLSCompliant(false)]
		public static void SetTypedReference(TypedReference target, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			throw new NotImplementedException();
		}

		/// <summary>Returns the internal metadata type handle for the specified TypedReference.</summary>
		/// <returns>The internal metadata type handle for the specified TypedReference.</returns>
		/// <param name="value">The TypedReference for which the type handle is requested. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600069C RID: 1692 RVA: 0x00014E44 File Offset: 0x00013044
		public static RuntimeTypeHandle TargetTypeToken(TypedReference value)
		{
			return value.type;
		}

		/// <summary>Converts the specified TypedReference to an Object.</summary>
		/// <returns>An <see cref="T:System.Object" /> converted from a TypedReference.</returns>
		/// <param name="value">The TypedReference to be converted. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600069D RID: 1693
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object ToObject(TypedReference value);

		// Token: 0x040000B4 RID: 180
		private RuntimeTypeHandle type;

		// Token: 0x040000B5 RID: 181
		private IntPtr value;

		// Token: 0x040000B6 RID: 182
		private IntPtr klass;
	}
}
