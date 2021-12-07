using System;
using System.Runtime.Serialization;

namespace System.Security.AccessControl
{
	/// <summary>The exception that is thrown when a method in the <see cref="N:System.Security.AccessControl" /> namespace attempts to enable a privilege that it does not have.</summary>
	// Token: 0x02000588 RID: 1416
	[Serializable]
	public sealed class PrivilegeNotHeldException : UnauthorizedAccessException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.PrivilegeNotHeldException" /> class.</summary>
		// Token: 0x060036E9 RID: 14057 RVA: 0x000B2604 File Offset: 0x000B0804
		public PrivilegeNotHeldException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.PrivilegeNotHeldException" /> class by using the specified privilege.</summary>
		/// <param name="privilege">The privilege that is not enabled.</param>
		// Token: 0x060036EA RID: 14058 RVA: 0x000B260C File Offset: 0x000B080C
		public PrivilegeNotHeldException(string privilege) : base(privilege)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.PrivilegeNotHeldException" /> class by using the specified exception.</summary>
		/// <param name="privilege">The privilege that is not enabled.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x060036EB RID: 14059 RVA: 0x000B2618 File Offset: 0x000B0818
		public PrivilegeNotHeldException(string privilege, Exception inner) : base(privilege, inner)
		{
		}

		/// <summary>Gets the name of the privilege that is not enabled.</summary>
		/// <returns>The name of the privilege that the method failed to enable.</returns>
		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x060036EC RID: 14060 RVA: 0x000B2624 File Offset: 0x000B0824
		public string PrivilegeName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Sets the <paramref name="info" /> parameter with information about the exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060036ED RID: 14061 RVA: 0x000B262C File Offset: 0x000B082C
		[MonoTODO]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}
	}
}
