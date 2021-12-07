using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.IO.IsolatedStorage
{
	/// <summary>Represents the abstract base class from which all isolated storage implementations must derive.</summary>
	// Token: 0x02000265 RID: 613
	[ComVisible(true)]
	public abstract class IsolatedStorage : MarshalByRefObject
	{
		/// <summary>Gets an application identity that scopes isolated storage.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" /> identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">The code lacks the required <see cref="T:System.Security.Permissions.SecurityPermission" /> to access this object. These permissions are granted by the runtime based on security policy. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object is not isolated by the application <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x00075B98 File Offset: 0x00073D98
		[MonoTODO("requires manifest support")]
		[ComVisible(false)]
		public object ApplicationIdentity
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPolicy\"/>\n</PermissionSet>\n")]
			get
			{
				if ((this.storage_scope & IsolatedStorageScope.Application) == IsolatedStorageScope.None)
				{
					throw new InvalidOperationException(Locale.GetText("Invalid Isolation Scope."));
				}
				if (this._applicationIdentity == null)
				{
					throw new InvalidOperationException(Locale.GetText("Identity unavailable."));
				}
				throw new NotImplementedException(Locale.GetText("CAS related"));
			}
		}

		/// <summary>Gets an assembly identity used to scope isolated storage.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the <see cref="T:System.Reflection.Assembly" /> identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">The code lacks the required <see cref="T:System.Security.Permissions.SecurityPermission" /> to access this object. </exception>
		/// <exception cref="T:System.InvalidOperationException">The assembly is not defined.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x00075BF0 File Offset: 0x00073DF0
		public object AssemblyIdentity
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPolicy\"/>\n</PermissionSet>\n")]
			get
			{
				if ((this.storage_scope & IsolatedStorageScope.Assembly) == IsolatedStorageScope.None)
				{
					throw new InvalidOperationException(Locale.GetText("Invalid Isolation Scope."));
				}
				if (this._assemblyIdentity == null)
				{
					throw new InvalidOperationException(Locale.GetText("Identity unavailable."));
				}
				return this._assemblyIdentity;
			}
		}

		/// <summary>Gets a value representing the current size of isolated storage.</summary>
		/// <returns>The number of storage units currently used within the isolated storage scope.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current size of the isolated store is undefined. </exception>
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x00075C3C File Offset: 0x00073E3C
		[CLSCompliant(false)]
		public virtual ulong CurrentSize
		{
			get
			{
				throw new InvalidOperationException(Locale.GetText("IsolatedStorage does not have a preset CurrentSize."));
			}
		}

		/// <summary>Gets a domain identity that scopes isolated storage.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" /> identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">The code lacks the required <see cref="T:System.Security.Permissions.SecurityPermission" /> to access this object. These permissions are granted by the runtime based on security policy. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object is not isolated by the domain <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x00075C50 File Offset: 0x00073E50
		public object DomainIdentity
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPolicy\"/>\n</PermissionSet>\n")]
			get
			{
				if ((this.storage_scope & IsolatedStorageScope.Domain) == IsolatedStorageScope.None)
				{
					throw new InvalidOperationException(Locale.GetText("Invalid Isolation Scope."));
				}
				if (this._domainIdentity == null)
				{
					throw new InvalidOperationException(Locale.GetText("Identity unavailable."));
				}
				return this._domainIdentity;
			}
		}

		/// <summary>Gets a value representing the maximum amount of space available for isolated storage. When overridden in a derived class, this value can take different units of measure.</summary>
		/// <returns>The maximum amount of isolated storage space in bytes. Derived classes can return different units of value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The quota has not been defined. </exception>
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x00075C9C File Offset: 0x00073E9C
		[CLSCompliant(false)]
		public virtual ulong MaximumSize
		{
			get
			{
				throw new InvalidOperationException(Locale.GetText("IsolatedStorage does not have a preset MaximumSize."));
			}
		}

		/// <summary>Gets an <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> enumeration value specifying the scope used to isolate the store.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" />  values specifying the scope used to isolate the store.</returns>
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x00075CB0 File Offset: 0x00073EB0
		public IsolatedStorageScope Scope
		{
			get
			{
				return this.storage_scope;
			}
		}

		/// <summary>Gets a backslash character that can be used in a directory string. When overridden in a derived class, another character might be returned.</summary>
		/// <returns>The default implementation returns the '\' (backslash) character.</returns>
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x00075CB8 File Offset: 0x00073EB8
		protected virtual char SeparatorExternal
		{
			get
			{
				return Path.DirectorySeparatorChar;
			}
		}

		/// <summary>Gets a period character that can be used in a directory string. When overridden in a derived class, another character might be returned.</summary>
		/// <returns>The default implementation returns the '.' (period) character.</returns>
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x00075CC0 File Offset: 0x00073EC0
		protected virtual char SeparatorInternal
		{
			get
			{
				return '.';
			}
		}

		/// <summary>When implemented by a derived class, returns a permission that represents access to isolated storage from within a permission set.</summary>
		/// <returns>An <see cref="T:System.Security.Permissions.IsolatedStoragePermission" /> object.</returns>
		/// <param name="ps">The <see cref="T:System.Security.PermissionSet" /> object that contains the set of permissions granted to code attempting to use isolated storage. </param>
		// Token: 0x06001FF7 RID: 8183
		protected abstract IsolatedStoragePermission GetPermission(PermissionSet ps);

		/// <summary>Initializes a new <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object.</summary>
		/// <param name="scope">A bitwise combination of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> values. </param>
		/// <param name="domainEvidenceType">The type of <see cref="T:System.Security.Policy.Evidence" /> that you can choose from the list of <see cref="T:System.Security.Policy.Evidence" /> present in the domain of the calling application. null lets the <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object choose the evidence. </param>
		/// <param name="assemblyEvidenceType">The type of <see cref="T:System.Security.Policy.Evidence" /> that you can choose from the list of <see cref="T:System.Security.Policy.Evidence" /> present in the assembly of the calling application. null lets the <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object choose the evidence. </param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The assembly specified has insufficient permissions to create isolated stores. </exception>
		// Token: 0x06001FF8 RID: 8184 RVA: 0x00075CC4 File Offset: 0x00073EC4
		protected void InitStore(IsolatedStorageScope scope, Type domainEvidenceType, Type assemblyEvidenceType)
		{
			switch (scope)
			{
			case IsolatedStorageScope.User | IsolatedStorageScope.Assembly:
			case IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly:
				throw new NotImplementedException(scope.ToString());
			}
			throw new ArgumentException(scope.ToString());
		}

		/// <summary>Initializes a new <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object.</summary>
		/// <param name="scope">A bitwise combination of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> values. </param>
		/// <param name="appEvidenceType">The type of <see cref="T:System.Security.Policy.Evidence" /> that you can choose from the list of <see cref="T:System.Security.Policy.Evidence" /> for the calling application. null lets the <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object choose the evidence. </param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The assembly specified has insufficient permissions to create isolated stores. </exception>
		// Token: 0x06001FF9 RID: 8185 RVA: 0x00075D10 File Offset: 0x00073F10
		[MonoTODO("requires manifest support")]
		protected void InitStore(IsolatedStorageScope scope, Type appEvidenceType)
		{
			if (AppDomain.CurrentDomain.ApplicationIdentity == null)
			{
				throw new IsolatedStorageException(Locale.GetText("No ApplicationIdentity available for AppDomain."));
			}
			if (appEvidenceType == null)
			{
			}
			this.storage_scope = scope;
		}

		/// <summary>When overridden in a derived class, removes the individual isolated store and all contained data.</summary>
		// Token: 0x06001FFA RID: 8186
		public abstract void Remove();

		// Token: 0x04000BDB RID: 3035
		internal IsolatedStorageScope storage_scope;

		// Token: 0x04000BDC RID: 3036
		internal object _assemblyIdentity;

		// Token: 0x04000BDD RID: 3037
		internal object _domainIdentity;

		// Token: 0x04000BDE RID: 3038
		internal object _applicationIdentity;
	}
}
