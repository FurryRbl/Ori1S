using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Allows access to field names and field types of objects that support the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface.</summary>
	// Token: 0x02000512 RID: 1298
	[ComVisible(true)]
	public interface IFieldInfo
	{
		/// <summary>Gets or sets the field names of serialized objects.</summary>
		/// <returns>The field names of serialized objects.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x0600338F RID: 13199
		// (set) Token: 0x06003390 RID: 13200
		string[] FieldNames { get; set; }

		/// <summary>Gets or sets the field types of the serialized objects.</summary>
		/// <returns>The field types of the serialized objects.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06003391 RID: 13201
		// (set) Token: 0x06003392 RID: 13202
		Type[] FieldTypes { get; set; }
	}
}
