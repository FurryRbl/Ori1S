using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides a set of services for registering and unregistering managed assemblies for use from COM.</summary>
	// Token: 0x020003B7 RID: 951
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[Guid("475e398f-8afa-43a7-a3be-f4ef8d6787c9")]
	public class RegistrationServices : IRegistrationServices
	{
		/// <summary>Returns the GUID of the COM category that contains the managed classes.</summary>
		/// <returns>The GUID of the COM category that contains the managed classes.</returns>
		// Token: 0x06002B5A RID: 11098 RVA: 0x000937E4 File Offset: 0x000919E4
		[MonoTODO("implement")]
		public virtual Guid GetManagedCategoryGuid()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the COM ProgID for the specified type.</summary>
		/// <returns>The ProgID for the specified type.</returns>
		/// <param name="type">The type corresponding to the ProgID that is being requested. </param>
		// Token: 0x06002B5B RID: 11099 RVA: 0x000937EC File Offset: 0x000919EC
		[MonoTODO("implement")]
		public virtual string GetProgIdForType(Type type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves a list of classes in an assembly that would be registered by a call to <see cref="M:System.Runtime.InteropServices.RegistrationServices.RegisterAssembly(System.Reflection.Assembly,System.Runtime.InteropServices.AssemblyRegistrationFlags)" />.</summary>
		/// <returns>A <see cref="T:System.Type" /> array containing a list of classes in <paramref name="assembly" />.</returns>
		/// <param name="assembly">The assembly to search for classes. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="assembly" /> parameter is null.</exception>
		// Token: 0x06002B5C RID: 11100 RVA: 0x000937F4 File Offset: 0x000919F4
		[MonoTODO("implement")]
		public virtual Type[] GetRegistrableTypesInAssembly(Assembly assembly)
		{
			throw new NotImplementedException();
		}

		/// <summary>Registers the classes in a managed assembly to enable creation from COM.</summary>
		/// <returns>true if <paramref name="assembly" /> contains types that were successfully registered; otherwise false if the assembly contains no eligible types.</returns>
		/// <param name="assembly">The assembly to be registered. </param>
		/// <param name="flags">An <see cref="T:System.Runtime.InteropServices.AssemblyRegistrationFlags" /> value indicating any special settings used when registering <paramref name="assembly" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assembly" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The full name of <paramref name="assembly" /> is null.-or- A method marked with <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> is not static.-or- There is more than one method marked with <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> at a given level of the hierarchy.-or- The signature of the method marked with <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> is not valid. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A user-defined custom registration function (marked with the <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> attribute) throws an exception.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B5D RID: 11101 RVA: 0x000937FC File Offset: 0x000919FC
		[MonoTODO("implement")]
		public virtual bool RegisterAssembly(Assembly assembly, AssemblyRegistrationFlags flags)
		{
			throw new NotImplementedException();
		}

		/// <summary>Registers the specified type with COM using the specified GUID.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> to be registered for use from COM. </param>
		/// <param name="g">The <see cref="T:System.Guid" /> used to register the specified type. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter cannot be created.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B5E RID: 11102 RVA: 0x00093804 File Offset: 0x00091A04
		[MonoTODO("implement")]
		public virtual void RegisterTypeForComClients(Type type, ref Guid g)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether a type is marked with the <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />, or derives from a type marked with the <see cref="T:System.Runtime.InteropServices.ComImportAttribute" /> and shares the same GUID as the parent.</summary>
		/// <returns>true if a type is marked with the <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />, or derives from a type marked with the <see cref="T:System.Runtime.InteropServices.ComImportAttribute" /> and shares the same GUID as the parent; otherwise false.</returns>
		/// <param name="type">The type to check for being a COM type. </param>
		// Token: 0x06002B5F RID: 11103 RVA: 0x0009380C File Offset: 0x00091A0C
		[MonoTODO("implement")]
		public virtual bool TypeRepresentsComType(Type type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the specified type requires registration.</summary>
		/// <returns>true if the type must be registered for use from COM; otherwise false.</returns>
		/// <param name="type">The type to check for COM registration requirements. </param>
		// Token: 0x06002B60 RID: 11104 RVA: 0x00093814 File Offset: 0x00091A14
		[MonoTODO("implement")]
		public virtual bool TypeRequiresRegistration(Type type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Unregisters the classes in a managed assembly.</summary>
		/// <returns>true if <paramref name="assembly" /> contains types that were successfully unregistered; otherwise false if the assembly contains no eligible types.</returns>
		/// <param name="assembly">The assembly to be unregistered. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assembly" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The full name of <paramref name="assembly" /> is null.-or- A method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not static.-or- There is more than one method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> at a given level of the hierarchy.-or- The signature of the method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not valid. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A user-defined custom unregistration function (marked with the <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" />  attribute) throws an exception.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B61 RID: 11105 RVA: 0x0009381C File Offset: 0x00091A1C
		[MonoTODO("implement")]
		public virtual bool UnregisterAssembly(Assembly assembly)
		{
			throw new NotImplementedException();
		}

		/// <summary>Registers the specified type with COM using the specified execution context and connection type.</summary>
		/// <returns>An integer that represents a cookie value.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> object to register for use from COM.</param>
		/// <param name="classContext">One of the <see cref="T:System.Runtime.InteropServices.RegistrationClassContext" /> values that indicates the context in which the executable code will be run.</param>
		/// <param name="flags">One of the <see cref="T:System.Runtime.InteropServices.RegistrationConnectionType" /> values that specifies how connections are made to the class object.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter cannot be created.</exception>
		// Token: 0x06002B62 RID: 11106 RVA: 0x00093824 File Offset: 0x00091A24
		[MonoTODO("implement")]
		[ComVisible(false)]
		public virtual int RegisterTypeForComClients(Type type, RegistrationClassContext classContext, RegistrationConnectionType flags)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes references to a type registered with the <see cref="M:System.Runtime.InteropServices.RegistrationServices.RegisterTypeForComClients(System.Type,System.Runtime.InteropServices.RegistrationClassContext,System.Runtime.InteropServices.RegistrationConnectionType)" /> method. </summary>
		/// <param name="cookie">The cookie value returned by a previous call to the <see cref="M:System.Runtime.InteropServices.RegistrationServices.RegisterTypeForComClients(System.Type,System.Runtime.InteropServices.RegistrationClassContext,System.Runtime.InteropServices.RegistrationConnectionType)" /> method.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B63 RID: 11107 RVA: 0x0009382C File Offset: 0x00091A2C
		[ComVisible(false)]
		[MonoTODO("implement")]
		public virtual void UnregisterTypeForComClients(int cookie)
		{
			throw new NotImplementedException();
		}
	}
}
