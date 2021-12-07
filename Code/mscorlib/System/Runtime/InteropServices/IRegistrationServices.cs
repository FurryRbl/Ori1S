﻿using System;
using System.Reflection;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides a set of services for registering and unregistering managed assemblies for use from COM.</summary>
	// Token: 0x0200039F RID: 927
	[ComVisible(true)]
	[Guid("CCBD682C-73A5-4568-B8B0-C7007E11ABA2")]
	public interface IRegistrationServices
	{
		/// <summary>Returns the GUID of the COM category that contains the managed classes.</summary>
		/// <returns>The GUID of the COM category that contains the managed classes.</returns>
		// Token: 0x06002A80 RID: 10880
		Guid GetManagedCategoryGuid();

		/// <summary>Retrieves the COM ProgID for a specified type.</summary>
		/// <returns>The ProgID for the specified type.</returns>
		/// <param name="type">The type whose ProgID is being requested. </param>
		// Token: 0x06002A81 RID: 10881
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		string GetProgIdForType(Type type);

		/// <summary>Retrieves a list of classes in an assembly that would be registered by a call to <see cref="M:System.Runtime.InteropServices.IRegistrationServices.RegisterAssembly(System.Reflection.Assembly,System.Runtime.InteropServices.AssemblyRegistrationFlags)" />.</summary>
		/// <returns>A <see cref="T:System.Type" /> array containing a list of classes in <paramref name="assembly" />.</returns>
		/// <param name="assembly">The assembly to search for classes. </param>
		// Token: 0x06002A82 RID: 10882
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		Type[] GetRegistrableTypesInAssembly(Assembly assembly);

		/// <summary>Registers the classes in a managed assembly to enable creation from COM.</summary>
		/// <returns>true if <paramref name="assembly" /> contains types that were successfully registered; otherwise false if the assembly contains no eligible types.</returns>
		/// <param name="assembly">The assembly to be registered. </param>
		/// <param name="flags">An <see cref="T:System.Runtime.InteropServices.AssemblyRegistrationFlags" /> value indicating any special settings needed when registering <paramref name="assembly" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assembly" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The full name of <paramref name="assembly" /> is null.-or- A method marked with <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> is not static.-or- There is more than one method marked with <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> at a given level of the hierarchy.-or- The signature of the method marked with <see cref="T:System.Runtime.InteropServices.ComRegisterFunctionAttribute" /> is not valid. </exception>
		// Token: 0x06002A83 RID: 10883
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		bool RegisterAssembly(Assembly assembly, AssemblyRegistrationFlags flags);

		/// <summary>Registers the specified type with COM using the specified GUID.</summary>
		/// <param name="type">The type to be registered for use from COM. </param>
		/// <param name="g">GUID used to register the specified type. </param>
		// Token: 0x06002A84 RID: 10884
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		void RegisterTypeForComClients(Type type, ref Guid g);

		/// <summary>Determines whether the specified type is a COM type.</summary>
		/// <returns>true if the specified type is a COM type; otherwise false.</returns>
		/// <param name="type">The type to determine if it is a COM type. </param>
		// Token: 0x06002A85 RID: 10885
		bool TypeRepresentsComType(Type type);

		/// <summary>Determines whether the specified type requires registration.</summary>
		/// <returns>true if the type must be registered for use from COM; otherwise false.</returns>
		/// <param name="type">The type to check for COM registration requirements. </param>
		// Token: 0x06002A86 RID: 10886
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		bool TypeRequiresRegistration(Type type);

		/// <summary>Unregisters the classes in a managed assembly.</summary>
		/// <returns>true if <paramref name="assembly" /> contains types that were successfully unregistered; otherwise false if the assembly contains no eligible types.</returns>
		/// <param name="assembly">The assembly to be unregistered. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assembly" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The full name of <paramref name="assembly" /> is null.-or- A method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not static.-or- There is more than one method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> at a given level of the hierarchy.-or- The signature of the method marked with <see cref="T:System.Runtime.InteropServices.ComUnregisterFunctionAttribute" /> is not valid. </exception>
		// Token: 0x06002A87 RID: 10887
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		bool UnregisterAssembly(Assembly assembly);
	}
}
