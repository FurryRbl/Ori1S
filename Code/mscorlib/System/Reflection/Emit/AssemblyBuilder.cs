using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Mono.Security;

namespace System.Reflection.Emit
{
	/// <summary>Defines and represents a dynamic assembly.</summary>
	// Token: 0x020002C4 RID: 708
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_AssemblyBuilder))]
	public sealed class AssemblyBuilder : Assembly, _AssemblyBuilder
	{
		// Token: 0x0600237F RID: 9087 RVA: 0x0007E924 File Offset: 0x0007CB24
		internal AssemblyBuilder(AssemblyName n, string directory, AssemblyBuilderAccess access, bool corlib_internal)
		{
			this.is_compiler_context = ((access & (AssemblyBuilderAccess)2048) != (AssemblyBuilderAccess)0);
			access &= (AssemblyBuilderAccess)(-2049);
			if (!Enum.IsDefined(typeof(AssemblyBuilderAccess), access))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument value {0} is not valid.", new object[]
				{
					(int)access
				}), "access");
			}
			this.name = n.Name;
			this.access = (uint)access;
			this.flags = (uint)n.Flags;
			if (this.IsSave && (directory == null || directory.Length == 0))
			{
				this.dir = Directory.GetCurrentDirectory();
			}
			else
			{
				this.dir = directory;
			}
			if (n.CultureInfo != null)
			{
				this.culture = n.CultureInfo.Name;
				this.versioninfo_culture = n.CultureInfo.Name;
			}
			Version version = n.Version;
			if (version != null)
			{
				this.version = version.ToString();
			}
			if (n.KeyPair != null)
			{
				this.sn = n.KeyPair.StrongName();
			}
			else
			{
				byte[] publicKey = n.GetPublicKey();
				if (publicKey != null && publicKey.Length > 0)
				{
					this.sn = new StrongName(publicKey);
				}
			}
			if (this.sn != null)
			{
				this.flags |= 1U;
			}
			this.corlib_internal = corlib_internal;
			if (this.sn != null)
			{
				this.pktoken = new byte[this.sn.PublicKeyToken.Length * 2];
				int num = 0;
				foreach (byte b in this.sn.PublicKeyToken)
				{
					string text = b.ToString("x2");
					this.pktoken[num++] = (byte)text[0];
					this.pktoken[num++] = (byte)text[1];
				}
			}
			AssemblyBuilder.basic_init(this);
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06002380 RID: 9088 RVA: 0x0007EB70 File Offset: 0x0007CD70
		void _AssemblyBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06002381 RID: 9089 RVA: 0x0007EB78 File Offset: 0x0007CD78
		void _AssemblyBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06002382 RID: 9090 RVA: 0x0007EB80 File Offset: 0x0007CD80
		void _AssemblyBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06002383 RID: 9091 RVA: 0x0007EB88 File Offset: 0x0007CD88
		void _AssemblyBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002384 RID: 9092
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void basic_init(AssemblyBuilder ab);

		/// <summary>Gets the location of the assembly, as specified originally (such as in an <see cref="T:System.Reflection.AssemblyName" /> object).</summary>
		/// <returns>The location of the assembly, as specified originally.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06002385 RID: 9093 RVA: 0x0007EB90 File Offset: 0x0007CD90
		public override string CodeBase
		{
			get
			{
				throw this.not_supported();
			}
		}

		/// <summary>Returns the entry point of this assembly.</summary>
		/// <returns>The entry point of this assembly.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x0007EB98 File Offset: 0x0007CD98
		public override MethodInfo EntryPoint
		{
			get
			{
				return this.entry_point;
			}
		}

		/// <summary>Gets the location, in codebase format, of the loaded file that contains the manifest if it is not shadow-copied.</summary>
		/// <returns>The location of the loaded file that contains the manifest. If the loaded file has been shadow-copied, the Location is that of the file before being shadow-copied.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x0007EBA0 File Offset: 0x0007CDA0
		public override string Location
		{
			get
			{
				throw this.not_supported();
			}
		}

		/// <summary>Gets the version of the common language runtime that will be saved in the file containing the manifest.</summary>
		/// <returns>A string representing the common language runtime version.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06002388 RID: 9096 RVA: 0x0007EBA8 File Offset: 0x0007CDA8
		public override string ImageRuntimeVersion
		{
			get
			{
				return base.ImageRuntimeVersion;
			}
		}

		/// <summary>Gets a value indicating whether the dynamic assembly is in the reflection-only context.</summary>
		/// <returns>true if the dynamic assembly is in the reflection-only context; otherwise, false.</returns>
		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06002389 RID: 9097 RVA: 0x0007EBB0 File Offset: 0x0007CDB0
		[MonoTODO]
		public override bool ReflectionOnly
		{
			get
			{
				return base.ReflectionOnly;
			}
		}

		/// <summary>Adds an existing resource file to this assembly.</summary>
		/// <param name="name">The logical name of the resource. </param>
		/// <param name="fileName">The physical file name (.resources file) to which the logical name is mapped. This should not include a path; the file must be in the same directory as the assembly to which it is added. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> has been previously defined.-or- There is another file in the assembly named <paramref name="fileName" />.-or- The length of <paramref name="name" /> is zero.-or- The length of <paramref name="fileName" /> is zero, or if <paramref name="fileName" /> includes a path. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file <paramref name="fileName" /> is not found. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600238A RID: 9098 RVA: 0x0007EBB8 File Offset: 0x0007CDB8
		public void AddResourceFile(string name, string fileName)
		{
			this.AddResourceFile(name, fileName, ResourceAttributes.Public);
		}

		/// <summary>Adds an existing resource file to this assembly.</summary>
		/// <param name="name">The logical name of the resource. </param>
		/// <param name="fileName">The physical file name (.resources file) to which the logical name is mapped. This should not include a path; the file must be in the same directory as the assembly to which it is added. </param>
		/// <param name="attribute">The resource attributes. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> has been previously defined.-or- There is another file in the assembly named <paramref name="fileName" />.-or- The length of <paramref name="name" /> is zero or if the length of <paramref name="fileName" /> is zero.-or- <paramref name="fileName" /> includes a path. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">If the file <paramref name="fileName" /> is not found. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600238B RID: 9099 RVA: 0x0007EBC4 File Offset: 0x0007CDC4
		public void AddResourceFile(string name, string fileName, ResourceAttributes attribute)
		{
			this.AddResourceFile(name, fileName, attribute, true);
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x0007EBD0 File Offset: 0x0007CDD0
		private void AddResourceFile(string name, string fileName, ResourceAttributes attribute, bool fileNeedsToExists)
		{
			this.check_name_and_filename(name, fileName, fileNeedsToExists);
			if (this.dir != null)
			{
				fileName = Path.Combine(this.dir, fileName);
			}
			if (this.resources != null)
			{
				MonoResource[] destinationArray = new MonoResource[this.resources.Length + 1];
				Array.Copy(this.resources, destinationArray, this.resources.Length);
				this.resources = destinationArray;
			}
			else
			{
				this.resources = new MonoResource[1];
			}
			int num = this.resources.Length - 1;
			this.resources[num].name = name;
			this.resources[num].filename = fileName;
			this.resources[num].attrs = attribute;
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x0007EC88 File Offset: 0x0007CE88
		internal void AddPermissionRequests(PermissionSet required, PermissionSet optional, PermissionSet refused)
		{
			if (this.created)
			{
				throw new InvalidOperationException("Assembly was already saved.");
			}
			this._minimum = required;
			this._optional = optional;
			this._refuse = refused;
			if (required != null)
			{
				this.permissions_minimum = new RefEmitPermissionSet[1];
				this.permissions_minimum[0] = new RefEmitPermissionSet(SecurityAction.RequestMinimum, required.ToXml().ToString());
			}
			if (optional != null)
			{
				this.permissions_optional = new RefEmitPermissionSet[1];
				this.permissions_optional[0] = new RefEmitPermissionSet(SecurityAction.RequestOptional, optional.ToXml().ToString());
			}
			if (refused != null)
			{
				this.permissions_refused = new RefEmitPermissionSet[1];
				this.permissions_refused[0] = new RefEmitPermissionSet(SecurityAction.RequestRefuse, refused.ToXml().ToString());
			}
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x0007ED60 File Offset: 0x0007CF60
		internal void EmbedResourceFile(string name, string fileName)
		{
			this.EmbedResourceFile(name, fileName, ResourceAttributes.Public);
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x0007ED6C File Offset: 0x0007CF6C
		internal void EmbedResourceFile(string name, string fileName, ResourceAttributes attribute)
		{
			if (this.resources != null)
			{
				MonoResource[] destinationArray = new MonoResource[this.resources.Length + 1];
				Array.Copy(this.resources, destinationArray, this.resources.Length);
				this.resources = destinationArray;
			}
			else
			{
				this.resources = new MonoResource[1];
			}
			int num = this.resources.Length - 1;
			this.resources[num].name = name;
			this.resources[num].attrs = attribute;
			try
			{
				FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				long length = fileStream.Length;
				this.resources[num].data = new byte[length];
				fileStream.Read(this.resources[num].data, 0, (int)length);
				fileStream.Close();
			}
			catch
			{
			}
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x0007EE60 File Offset: 0x0007D060
		internal void EmbedResource(string name, byte[] blob, ResourceAttributes attribute)
		{
			if (this.resources != null)
			{
				MonoResource[] destinationArray = new MonoResource[this.resources.Length + 1];
				Array.Copy(this.resources, destinationArray, this.resources.Length);
				this.resources = destinationArray;
			}
			else
			{
				this.resources = new MonoResource[1];
			}
			int num = this.resources.Length - 1;
			this.resources[num].name = name;
			this.resources[num].attrs = attribute;
			this.resources[num].data = blob;
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x0007EEF8 File Offset: 0x0007D0F8
		internal void AddTypeForwarder(Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			if (this.type_forwarders == null)
			{
				this.type_forwarders = new Type[]
				{
					t
				};
			}
			else
			{
				Type[] array = new Type[this.type_forwarders.Length + 1];
				Array.Copy(this.type_forwarders, array, this.type_forwarders.Length);
				array[this.type_forwarders.Length] = t;
				this.type_forwarders = array;
			}
		}

		/// <summary>Defines a named transient dynamic module in this assembly.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.ModuleBuilder" /> representing the defined dynamic module.</returns>
		/// <param name="name">The name of the dynamic module. Must be less than 260 characters in length. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> begins with white space.-or- The length of <paramref name="name" /> is zero.-or- The length of <paramref name="name" /> is greater than or equal to 260. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ExecutionEngineException">The assembly for default symbol writer cannot be loaded.-or- The type that implements the default symbol writer interface cannot be found. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06002392 RID: 9106 RVA: 0x0007EF6C File Offset: 0x0007D16C
		public ModuleBuilder DefineDynamicModule(string name)
		{
			return this.DefineDynamicModule(name, name, false, true);
		}

		/// <summary>Defines a named transient dynamic module in this assembly and specifies whether symbol information should be emitted.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.ModuleBuilder" /> representing the defined dynamic module.</returns>
		/// <param name="name">The name of the dynamic module. Must be less than 260 characters in length. </param>
		/// <param name="emitSymbolInfo">true if symbol information is to be emitted; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> begins with white space.-or- The length of <paramref name="name" /> is zero.-or- The length of <paramref name="name" /> is greater than or equal to 260. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ExecutionEngineException">The assembly for default symbol writer cannot be loaded.-or- The type that implements the default symbol writer interface cannot be found. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06002393 RID: 9107 RVA: 0x0007EF78 File Offset: 0x0007D178
		public ModuleBuilder DefineDynamicModule(string name, bool emitSymbolInfo)
		{
			return this.DefineDynamicModule(name, name, emitSymbolInfo, true);
		}

		/// <summary>Defines a persistable dynamic module with the given name that will be saved to the specified file. No symbol information is emitted.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.ModuleBuilder" /> object representing the defined dynamic module.</returns>
		/// <param name="name">The name of the dynamic module. Must be less than 260 characters in length. </param>
		/// <param name="fileName">The name of the file to which the dynamic module should be saved. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> or <paramref name="fileName" /> is zero.-or- The length of <paramref name="name" /> is greater than or equal to 260.-or- <paramref name="fileName" /> contains a path specification (a directory component, for example).-or- There is a conflict with the name of another file that belongs to this assembly. </exception>
		/// <exception cref="T:System.InvalidOperationException">This assembly has been previously saved. </exception>
		/// <exception cref="T:System.NotSupportedException">This assembly was called on a dynamic assembly with <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Run" /> attribute. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ExecutionEngineException">The assembly for default symbol writer cannot be loaded.-or- The type that implements the default symbol writer interface cannot be found. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06002394 RID: 9108 RVA: 0x0007EF84 File Offset: 0x0007D184
		public ModuleBuilder DefineDynamicModule(string name, string fileName)
		{
			return this.DefineDynamicModule(name, fileName, false, false);
		}

		/// <summary>Defines a persistable dynamic module, specifying the module name, the name of the file to which the module will be saved, and whether symbol information should be emitted using the default symbol writer.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.ModuleBuilder" /> object representing the defined dynamic module.</returns>
		/// <param name="name">The name of the dynamic module. Must be less than 260 characters in length. </param>
		/// <param name="fileName">The name of the file to which the dynamic module should be saved. </param>
		/// <param name="emitSymbolInfo">If true, symbolic information is written using the default symbol writer. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> or <paramref name="fileName" /> is zero.-or- The length of <paramref name="name" /> is greater than or equal to 260.-or- <paramref name="fileName" /> contains a path specification (a directory component, for example).-or- There is a conflict with the name of another file that belongs to this assembly. </exception>
		/// <exception cref="T:System.InvalidOperationException">This assembly has been previously saved. </exception>
		/// <exception cref="T:System.NotSupportedException">This assembly was called on a dynamic assembly with the <see cref="F:System.Reflection.Emit.AssemblyBuilderAccess.Run" /> attribute. </exception>
		/// <exception cref="T:System.ExecutionEngineException">The assembly for default symbol writer cannot be loaded.-or- The type that implements the default symbol writer interface cannot be found. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06002395 RID: 9109 RVA: 0x0007EF90 File Offset: 0x0007D190
		public ModuleBuilder DefineDynamicModule(string name, string fileName, bool emitSymbolInfo)
		{
			return this.DefineDynamicModule(name, fileName, emitSymbolInfo, false);
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x0007EF9C File Offset: 0x0007D19C
		private ModuleBuilder DefineDynamicModule(string name, string fileName, bool emitSymbolInfo, bool transient)
		{
			this.check_name_and_filename(name, fileName, false);
			if (!transient)
			{
				if (Path.GetExtension(fileName) == string.Empty)
				{
					throw new ArgumentException("Module file name '" + fileName + "' must have file extension.");
				}
				if (!this.IsSave)
				{
					throw new NotSupportedException("Persistable modules are not supported in a dynamic assembly created with AssemblyBuilderAccess.Run");
				}
				if (this.created)
				{
					throw new InvalidOperationException("Assembly was already saved.");
				}
			}
			ModuleBuilder moduleBuilder = new ModuleBuilder(this, name, fileName, emitSymbolInfo, transient);
			if (this.modules != null && this.is_module_only)
			{
				throw new InvalidOperationException("A module-only assembly can only contain one module.");
			}
			if (this.modules != null)
			{
				ModuleBuilder[] destinationArray = new ModuleBuilder[this.modules.Length + 1];
				Array.Copy(this.modules, destinationArray, this.modules.Length);
				this.modules = destinationArray;
			}
			else
			{
				this.modules = new ModuleBuilder[1];
			}
			this.modules[this.modules.Length - 1] = moduleBuilder;
			return moduleBuilder;
		}

		// Token: 0x06002397 RID: 9111
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Module InternalAddModule(string fileName);

		// Token: 0x06002398 RID: 9112 RVA: 0x0007F098 File Offset: 0x0007D298
		internal Module AddModule(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException(fileName);
			}
			Module module = this.InternalAddModule(fileName);
			if (this.loaded_modules != null)
			{
				Module[] destinationArray = new Module[this.loaded_modules.Length + 1];
				Array.Copy(this.loaded_modules, destinationArray, this.loaded_modules.Length);
				this.loaded_modules = destinationArray;
			}
			else
			{
				this.loaded_modules = new Module[1];
			}
			this.loaded_modules[this.loaded_modules.Length - 1] = module;
			return module;
		}

		/// <summary>Defines a standalone managed resource for this assembly with the default public resource attribute.</summary>
		/// <returns>A <see cref="T:System.Resources.ResourceWriter" /> object for the specified resource.</returns>
		/// <param name="name">The logical name of the resource. </param>
		/// <param name="description">A textual description of the resource. </param>
		/// <param name="fileName">The physical file name (.resources file) to which the logical name is mapped. This should not include a path. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> has been previously defined.-or- There is another file in the assembly named <paramref name="fileName" />.-or- The length of <paramref name="name" /> is zero.-or- The length of <paramref name="fileName" /> is zero.-or- <paramref name="fileName" /> includes a path. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002399 RID: 9113 RVA: 0x0007F114 File Offset: 0x0007D314
		public IResourceWriter DefineResource(string name, string description, string fileName)
		{
			return this.DefineResource(name, description, fileName, ResourceAttributes.Public);
		}

		/// <summary>Defines a standalone managed resource for this assembly. Attributes can be specified for the managed resource.</summary>
		/// <returns>A <see cref="T:System.Resources.ResourceWriter" /> object for the specified resource.</returns>
		/// <param name="name">The logical name of the resource. </param>
		/// <param name="description">A textual description of the resource. </param>
		/// <param name="fileName">The physical file name (.resources file) to which the logical name is mapped. This should not include a path. </param>
		/// <param name="attribute">The resource attributes. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> has been previously defined or if there is another file in the assembly named <paramref name="fileName" />.-or- The length of <paramref name="name" /> is zero.-or- The length of <paramref name="fileName" /> is zero.-or- <paramref name="fileName" /> includes a path. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="fileName" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600239A RID: 9114 RVA: 0x0007F120 File Offset: 0x0007D320
		public IResourceWriter DefineResource(string name, string description, string fileName, ResourceAttributes attribute)
		{
			this.AddResourceFile(name, fileName, attribute, false);
			IResourceWriter resourceWriter = new ResourceWriter(fileName);
			if (this.resource_writers == null)
			{
				this.resource_writers = new ArrayList();
			}
			this.resource_writers.Add(resourceWriter);
			return resourceWriter;
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x0007F164 File Offset: 0x0007D364
		private void AddUnmanagedResource(Win32Resource res)
		{
			MemoryStream memoryStream = new MemoryStream();
			res.WriteTo(memoryStream);
			if (this.win32_resources != null)
			{
				MonoWin32Resource[] destinationArray = new MonoWin32Resource[this.win32_resources.Length + 1];
				Array.Copy(this.win32_resources, destinationArray, this.win32_resources.Length);
				this.win32_resources = destinationArray;
			}
			else
			{
				this.win32_resources = new MonoWin32Resource[1];
			}
			this.win32_resources[this.win32_resources.Length - 1] = new MonoWin32Resource(res.Type.Id, res.Name.Id, res.Language, memoryStream.ToArray());
		}

		/// <summary>Defines an unmanaged resource for this assembly as an opaque blob of bytes.</summary>
		/// <param name="resource">The opaque blob of bytes representing the unmanaged resource. </param>
		/// <exception cref="T:System.ArgumentException">An unmanaged resource was previously defined. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resource" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600239C RID: 9116 RVA: 0x0007F208 File Offset: 0x0007D408
		[MonoTODO("Not currently implemenented")]
		public void DefineUnmanagedResource(byte[] resource)
		{
			if (resource == null)
			{
				throw new ArgumentNullException("resource");
			}
			if (this.native_resource != NativeResourceType.None)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.native_resource = NativeResourceType.Unmanaged;
			throw new NotImplementedException();
		}

		/// <summary>Defines an unmanaged resource file for this assembly given the name of the resource file.</summary>
		/// <param name="resourceFileName">The name of the resource file. </param>
		/// <exception cref="T:System.ArgumentException">An unmanaged resource was previously defined.-or- The file <paramref name="resourceFileName" /> is not readable.-or- <paramref name="resourceFileName" /> is the empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resourceFileName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="resourceFileName" /> is not found.-or- <paramref name="resourceFileName" /> is a directory. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600239D RID: 9117 RVA: 0x0007F240 File Offset: 0x0007D440
		public void DefineUnmanagedResource(string resourceFileName)
		{
			if (resourceFileName == null)
			{
				throw new ArgumentNullException("resourceFileName");
			}
			if (resourceFileName.Length == 0)
			{
				throw new ArgumentException("resourceFileName");
			}
			if (!File.Exists(resourceFileName) || Directory.Exists(resourceFileName))
			{
				throw new FileNotFoundException("File '" + resourceFileName + "' does not exists or is a directory.");
			}
			if (this.native_resource != NativeResourceType.None)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.native_resource = NativeResourceType.Unmanaged;
			using (FileStream fileStream = new FileStream(resourceFileName, FileMode.Open, FileAccess.Read))
			{
				Win32ResFileReader win32ResFileReader = new Win32ResFileReader(fileStream);
				foreach (object obj in win32ResFileReader.ReadResources())
				{
					Win32EncodedResource win32EncodedResource = (Win32EncodedResource)obj;
					if (win32EncodedResource.Name.IsName || win32EncodedResource.Type.IsName)
					{
						throw new InvalidOperationException("resource files with named resources or non-default resource types are not supported.");
					}
					this.AddUnmanagedResource(win32EncodedResource);
				}
			}
		}

		/// <summary>Defines an unmanaged version information resource using the information specified in the assembly's AssemblyName object and the assembly's custom attributes.</summary>
		/// <exception cref="T:System.ArgumentException">An unmanaged version information resource was previously defined.-or- The unmanaged version information is too large to persist. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x0600239E RID: 9118 RVA: 0x0007F388 File Offset: 0x0007D588
		public void DefineVersionInfoResource()
		{
			if (this.native_resource != NativeResourceType.None)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.native_resource = NativeResourceType.Assembly;
			this.version_res = new Win32VersionResource(1, 0, this.IsCompilerContext);
		}

		/// <summary>Defines an unmanaged version information resource for this assembly with the given specifications.</summary>
		/// <param name="product">The name of the product with which this assembly is distributed. </param>
		/// <param name="productVersion">The version of the product with which this assembly is distributed. </param>
		/// <param name="company">The name of the company that produced this assembly. </param>
		/// <param name="copyright">Describes all copyright notices, trademarks, and registered trademarks that apply to this assembly. This should include the full text of all notices, legal symbols, copyright dates, trademark numbers, and so on. In English, this string should be in the format "Copyright Microsoft Corp. 1990-2001". </param>
		/// <param name="trademark">Describes all trademarks and registered trademarks that apply to this assembly. This should include the full text of all notices, legal symbols, trademark numbers, and so on. In English, this string should be in the format "Windows is a trademark of Microsoft Corporation". </param>
		/// <exception cref="T:System.ArgumentException">An unmanaged version information resource was previously defined.-or- The unmanaged version information is too large to persist. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x0600239F RID: 9119 RVA: 0x0007F3C8 File Offset: 0x0007D5C8
		public void DefineVersionInfoResource(string product, string productVersion, string company, string copyright, string trademark)
		{
			if (this.native_resource != NativeResourceType.None)
			{
				throw new ArgumentException("Native resource has already been defined.");
			}
			this.native_resource = NativeResourceType.Explicit;
			this.version_res = new Win32VersionResource(1, 0, false);
			this.version_res.ProductName = ((product == null) ? " " : product);
			this.version_res.ProductVersion = ((productVersion == null) ? " " : productVersion);
			this.version_res.CompanyName = ((company == null) ? " " : company);
			this.version_res.LegalCopyright = ((copyright == null) ? " " : copyright);
			this.version_res.LegalTrademarks = ((trademark == null) ? " " : trademark);
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x0007F490 File Offset: 0x0007D690
		internal void DefineIconResource(string iconFileName)
		{
			if (iconFileName == null)
			{
				throw new ArgumentNullException("iconFileName");
			}
			if (iconFileName.Length == 0)
			{
				throw new ArgumentException("iconFileName");
			}
			if (!File.Exists(iconFileName) || Directory.Exists(iconFileName))
			{
				throw new FileNotFoundException("File '" + iconFileName + "' does not exists or is a directory.");
			}
			using (FileStream fileStream = new FileStream(iconFileName, FileMode.Open, FileAccess.Read))
			{
				Win32IconFileReader win32IconFileReader = new Win32IconFileReader(fileStream);
				ICONDIRENTRY[] array = win32IconFileReader.ReadIcons();
				Win32IconResource[] array2 = new Win32IconResource[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = new Win32IconResource(i + 1, 0, array[i]);
					this.AddUnmanagedResource(array2[i]);
				}
				Win32GroupIconResource res = new Win32GroupIconResource(1, 0, array2);
				this.AddUnmanagedResource(res);
			}
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x0007F584 File Offset: 0x0007D784
		private void DefineVersionInfoResourceImpl(string fileName)
		{
			if (this.versioninfo_culture != null)
			{
				this.version_res.FileLanguage = new CultureInfo(this.versioninfo_culture).LCID;
			}
			this.version_res.Version = ((this.version != null) ? this.version : "0.0.0.0");
			if (this.cattrs != null)
			{
				NativeResourceType nativeResourceType = this.native_resource;
				if (nativeResourceType != NativeResourceType.Assembly)
				{
					if (nativeResourceType == NativeResourceType.Explicit)
					{
						foreach (CustomAttributeBuilder customAttributeBuilder in this.cattrs)
						{
							string fullName = customAttributeBuilder.Ctor.ReflectedType.FullName;
							if (fullName == "System.Reflection.AssemblyCultureAttribute")
							{
								if (!this.IsCompilerContext)
								{
									this.version_res.FileLanguage = new CultureInfo(customAttributeBuilder.string_arg()).LCID;
								}
							}
							else if (fullName == "System.Reflection.AssemblyDescriptionAttribute")
							{
								this.version_res.Comments = customAttributeBuilder.string_arg();
							}
						}
					}
				}
				else
				{
					foreach (CustomAttributeBuilder customAttributeBuilder2 in this.cattrs)
					{
						string fullName2 = customAttributeBuilder2.Ctor.ReflectedType.FullName;
						if (fullName2 == "System.Reflection.AssemblyProductAttribute")
						{
							this.version_res.ProductName = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyCompanyAttribute")
						{
							this.version_res.CompanyName = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyCopyrightAttribute")
						{
							this.version_res.LegalCopyright = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyTrademarkAttribute")
						{
							this.version_res.LegalTrademarks = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyCultureAttribute")
						{
							if (!this.IsCompilerContext)
							{
								this.version_res.FileLanguage = new CultureInfo(customAttributeBuilder2.string_arg()).LCID;
							}
						}
						else if (fullName2 == "System.Reflection.AssemblyFileVersionAttribute")
						{
							string text = customAttributeBuilder2.string_arg();
							if (!this.IsCompilerContext || (text != null && text.Length != 0))
							{
								this.version_res.FileVersion = text;
							}
						}
						else if (fullName2 == "System.Reflection.AssemblyInformationalVersionAttribute")
						{
							this.version_res.ProductVersion = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyTitleAttribute")
						{
							this.version_res.FileDescription = customAttributeBuilder2.string_arg();
						}
						else if (fullName2 == "System.Reflection.AssemblyDescriptionAttribute")
						{
							this.version_res.Comments = customAttributeBuilder2.string_arg();
						}
					}
				}
			}
			this.version_res.OriginalFilename = fileName;
			if (this.IsCompilerContext)
			{
				this.version_res.InternalName = fileName;
				if (this.version_res.ProductVersion.Trim().Length == 0)
				{
					this.version_res.ProductVersion = this.version_res.FileVersion;
				}
			}
			else
			{
				this.version_res.InternalName = Path.GetFileNameWithoutExtension(fileName);
			}
			this.AddUnmanagedResource(this.version_res);
		}

		/// <summary>Returns the dynamic module with the specified name.</summary>
		/// <returns>A ModuleBuilder object representing the requested dynamic module.</returns>
		/// <param name="name">The name of the requested dynamic module. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060023A2 RID: 9122 RVA: 0x0007F8D8 File Offset: 0x0007DAD8
		public ModuleBuilder GetDynamicModule(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal.", "name");
			}
			if (this.modules != null)
			{
				for (int i = 0; i < this.modules.Length; i++)
				{
					if (this.modules[i].name == name)
					{
						return this.modules[i];
					}
				}
			}
			return null;
		}

		/// <summary>Gets the exported types defined in this assembly.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> containing the exported types defined in this assembly.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060023A3 RID: 9123 RVA: 0x0007F958 File Offset: 0x0007DB58
		public override Type[] GetExportedTypes()
		{
			throw this.not_supported();
		}

		/// <summary>Gets a <see cref="T:System.IO.FileStream" /> for the specified file in the file table of the manifest of this assembly.</summary>
		/// <returns>A <see cref="T:System.IO.FileStream" /> for the specified file, or null, if the file is not found.</returns>
		/// <param name="name">The name of the specified file. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060023A4 RID: 9124 RVA: 0x0007F960 File Offset: 0x0007DB60
		public override FileStream GetFile(string name)
		{
			throw this.not_supported();
		}

		/// <summary>Gets the files in the file table of an assembly manifest, specifying whether to include resource modules.</summary>
		/// <returns>An array of <see cref="T:System.IO.FileStream" /> objects.</returns>
		/// <param name="getResourceModules">true to include resource modules; otherwise, false. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060023A5 RID: 9125 RVA: 0x0007F968 File Offset: 0x0007DB68
		public override FileStream[] GetFiles(bool getResourceModules)
		{
			throw this.not_supported();
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x0007F970 File Offset: 0x0007DB70
		internal override Module[] GetModulesInternal()
		{
			if (this.modules == null)
			{
				return new Module[0];
			}
			return (Module[])this.modules.Clone();
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x0007F9A0 File Offset: 0x0007DBA0
		internal override Type[] GetTypes(bool exportedOnly)
		{
			Type[] array = null;
			if (this.modules != null)
			{
				for (int i = 0; i < this.modules.Length; i++)
				{
					Type[] types = this.modules[i].GetTypes();
					if (array == null)
					{
						array = types;
					}
					else
					{
						Type[] destinationArray = new Type[array.Length + types.Length];
						Array.Copy(array, 0, destinationArray, 0, array.Length);
						Array.Copy(types, 0, destinationArray, array.Length, types.Length);
					}
				}
			}
			if (this.loaded_modules != null)
			{
				for (int j = 0; j < this.loaded_modules.Length; j++)
				{
					Type[] types2 = this.loaded_modules[j].GetTypes();
					if (array == null)
					{
						array = types2;
					}
					else
					{
						Type[] destinationArray2 = new Type[array.Length + types2.Length];
						Array.Copy(array, 0, destinationArray2, 0, array.Length);
						Array.Copy(types2, 0, destinationArray2, array.Length, types2.Length);
					}
				}
			}
			return (array != null) ? array : Type.EmptyTypes;
		}

		/// <summary>Returns information about how the given resource has been persisted.</summary>
		/// <returns>
		///   <see cref="T:System.Reflection.ManifestResourceInfo" /> populated with information about the resource's topology, or null if the resource is not found.</returns>
		/// <param name="resourceName">The name of the resource. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060023A8 RID: 9128 RVA: 0x0007FA9C File Offset: 0x0007DC9C
		public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			throw this.not_supported();
		}

		/// <summary>Loads the specified manifest resource from this assembly.</summary>
		/// <returns>An array of type String containing the names of all the resources.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported on a dynamic assembly. To get the manifest resource names, use <see cref="M:System.Reflection.Assembly.GetManifestResourceNames" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060023A9 RID: 9129 RVA: 0x0007FAA4 File Offset: 0x0007DCA4
		public override string[] GetManifestResourceNames()
		{
			throw this.not_supported();
		}

		/// <summary>Loads the specified manifest resource from this assembly.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> representing this manifest resource.</returns>
		/// <param name="name">The name of the manifest resource being requested. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060023AA RID: 9130 RVA: 0x0007FAAC File Offset: 0x0007DCAC
		public override Stream GetManifestResourceStream(string name)
		{
			throw this.not_supported();
		}

		/// <summary>Loads the specified manifest resource, scoped by the namespace of the specified type, from this assembly.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> representing this manifest resource.</returns>
		/// <param name="type">The type whose namespace is used to scope the manifest resource name. </param>
		/// <param name="name">The name of the manifest resource being requested. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060023AB RID: 9131 RVA: 0x0007FAB4 File Offset: 0x0007DCB4
		public override Stream GetManifestResourceStream(Type type, string name)
		{
			throw this.not_supported();
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x0007FABC File Offset: 0x0007DCBC
		internal bool IsCompilerContext
		{
			get
			{
				return this.is_compiler_context;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060023AD RID: 9133 RVA: 0x0007FAC4 File Offset: 0x0007DCC4
		internal bool IsSave
		{
			get
			{
				return this.access != 1U;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060023AE RID: 9134 RVA: 0x0007FAD4 File Offset: 0x0007DCD4
		internal bool IsRun
		{
			get
			{
				return this.access == 1U || this.access == 3U;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060023AF RID: 9135 RVA: 0x0007FAF0 File Offset: 0x0007DCF0
		internal string AssemblyDir
		{
			get
			{
				return this.dir;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060023B0 RID: 9136 RVA: 0x0007FAF8 File Offset: 0x0007DCF8
		// (set) Token: 0x060023B1 RID: 9137 RVA: 0x0007FB00 File Offset: 0x0007DD00
		internal bool IsModuleOnly
		{
			get
			{
				return this.is_module_only;
			}
			set
			{
				this.is_module_only = value;
			}
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x0007FB0C File Offset: 0x0007DD0C
		internal override Module GetManifestModule()
		{
			if (this.manifest_module == null)
			{
				this.manifest_module = this.DefineDynamicModule("Default Dynamic Module");
			}
			return this.manifest_module;
		}

		/// <summary>Saves this dynamic assembly to disk, specifying the nature of code in the assembly's executables and the target platform.</summary>
		/// <param name="assemblyFileName">The file name of the assembly.</param>
		/// <param name="portableExecutableKind">A bitwise combination of the <see cref="T:System.Reflection.PortableExecutableKinds" /> values that specifies the nature of the code.</param>
		/// <param name="imageFileMachine">One of the <see cref="T:System.Reflection.ImageFileMachine" /> values that specifies the target platform.</param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="assemblyFileName" /> is 0.-or- There are two or more modules resource files in the assembly with the same name.-or- The target directory of the assembly is invalid.-or- <paramref name="assemblyFileName" /> is not a simple file name (for example, has a directory or drive component), or more than one unmanaged resource, including a version information resources, was defined in this assembly.-or- The CultureInfo string in <see cref="T:System.Reflection.AssemblyCultureAttribute" /> is not a valid string and <see cref="M:System.Reflection.Emit.AssemblyBuilder.DefineVersionInfoResource(System.String,System.String,System.String,System.String,System.String)" /> was called prior to calling this method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFileName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This assembly has been saved before.-or- This assembly has access Run<see cref="T:System.Reflection.Emit.AssemblyBuilderAccess" />. </exception>
		/// <exception cref="T:System.IO.IOException">An output error occurs during the save. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has not been called for any of the types in the modules of the assembly to be written to disk. </exception>
		// Token: 0x060023B3 RID: 9139 RVA: 0x0007FB3C File Offset: 0x0007DD3C
		[MonoLimitation("No support for PE32+ assemblies for AMD64 and IA64")]
		public void Save(string assemblyFileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
		{
			this.peKind = portableExecutableKind;
			this.machine = imageFileMachine;
			if ((this.peKind & PortableExecutableKinds.PE32Plus) != PortableExecutableKinds.NotAPortableExecutableImage || (this.peKind & PortableExecutableKinds.Unmanaged32Bit) != PortableExecutableKinds.NotAPortableExecutableImage)
			{
				throw new NotImplementedException(this.peKind.ToString());
			}
			if (this.machine == ImageFileMachine.IA64 || this.machine == ImageFileMachine.AMD64)
			{
				throw new NotImplementedException(this.machine.ToString());
			}
			if (this.resource_writers != null)
			{
				foreach (object obj in this.resource_writers)
				{
					IResourceWriter resourceWriter = (IResourceWriter)obj;
					resourceWriter.Generate();
					resourceWriter.Close();
				}
			}
			ModuleBuilder moduleBuilder = null;
			if (this.modules != null)
			{
				foreach (ModuleBuilder moduleBuilder2 in this.modules)
				{
					if (moduleBuilder2.FullyQualifiedName == assemblyFileName)
					{
						moduleBuilder = moduleBuilder2;
					}
				}
			}
			if (moduleBuilder == null)
			{
				moduleBuilder = this.DefineDynamicModule("RefEmit_OnDiskManifestModule", assemblyFileName);
			}
			if (!this.is_module_only)
			{
				moduleBuilder.IsMain = true;
			}
			if (this.entry_point != null && this.entry_point.DeclaringType.Module != moduleBuilder)
			{
				Type[] array2;
				if (this.entry_point.GetParameters().Length == 1)
				{
					array2 = new Type[]
					{
						typeof(string)
					};
				}
				else
				{
					array2 = Type.EmptyTypes;
				}
				MethodBuilder methodBuilder = moduleBuilder.DefineGlobalMethod("__EntryPoint$", MethodAttributes.Static, this.entry_point.ReturnType, array2);
				ILGenerator ilgenerator = methodBuilder.GetILGenerator();
				if (array2.Length == 1)
				{
					ilgenerator.Emit(OpCodes.Ldarg_0);
				}
				ilgenerator.Emit(OpCodes.Tailcall);
				ilgenerator.Emit(OpCodes.Call, this.entry_point);
				ilgenerator.Emit(OpCodes.Ret);
				this.entry_point = methodBuilder;
			}
			if (this.version_res != null)
			{
				this.DefineVersionInfoResourceImpl(assemblyFileName);
			}
			if (this.sn != null)
			{
				this.public_key = this.sn.PublicKey;
			}
			foreach (ModuleBuilder moduleBuilder3 in this.modules)
			{
				if (moduleBuilder3 != moduleBuilder)
				{
					moduleBuilder3.Save();
				}
			}
			moduleBuilder.Save();
			if (this.sn != null && this.sn.CanSign)
			{
				this.sn.Sign(Path.Combine(this.AssemblyDir, assemblyFileName));
			}
			this.created = true;
		}

		/// <summary>Saves this dynamic assembly to disk.</summary>
		/// <param name="assemblyFileName">The file name of the assembly. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="assemblyFileName" /> is 0.-or- There are two or more modules resource files in the assembly with the same name.-or- The target directory of the assembly is invalid.-or- <paramref name="assemblyFileName" /> is not a simple file name (for example, has a directory or drive component), or more than one unmanaged resource, including a version information resource, was defined in this assembly.-or- The CultureInfo string in <see cref="T:System.Reflection.AssemblyCultureAttribute" /> is not a valid string and <see cref="M:System.Reflection.Emit.AssemblyBuilder.DefineVersionInfoResource(System.String,System.String,System.String,System.String,System.String)" /> was called prior to calling this method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFileName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">This assembly has been saved before.-or- This assembly has access Run<see cref="T:System.Reflection.Emit.AssemblyBuilderAccess" /></exception>
		/// <exception cref="T:System.IO.IOException">An output error occurs during the save. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has not been called for any of the types in the modules of the assembly to be written to disk. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060023B4 RID: 9140 RVA: 0x0007FE08 File Offset: 0x0007E008
		public void Save(string assemblyFileName)
		{
			this.Save(assemblyFileName, PortableExecutableKinds.ILOnly, ImageFileMachine.I386);
		}

		/// <summary>Sets the entry point for this dynamic assembly, assuming that a console application is being built.</summary>
		/// <param name="entryMethod">A reference to the method that represents the entry point for this dynamic assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="entryMethod" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="entryMethod" /> is not contained within this assembly. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060023B5 RID: 9141 RVA: 0x0007FE18 File Offset: 0x0007E018
		public void SetEntryPoint(MethodInfo entryMethod)
		{
			this.SetEntryPoint(entryMethod, PEFileKinds.ConsoleApplication);
		}

		/// <summary>Sets the entry point for this assembly and defines the type of the portable executable (PE file) being built.</summary>
		/// <param name="entryMethod">A reference to the method that represents the entry point for this dynamic assembly. </param>
		/// <param name="fileKind">The type of the assembly executable being built. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="entryMethod" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="entryMethod" /> is not contained within this assembly. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060023B6 RID: 9142 RVA: 0x0007FE24 File Offset: 0x0007E024
		public void SetEntryPoint(MethodInfo entryMethod, PEFileKinds fileKind)
		{
			if (entryMethod == null)
			{
				throw new ArgumentNullException("entryMethod");
			}
			if (entryMethod.DeclaringType.Assembly != this)
			{
				throw new InvalidOperationException("Entry method is not defined in the same assembly.");
			}
			this.entry_point = entryMethod;
			this.pekind = fileKind;
		}

		/// <summary>Set a custom attribute on this assembly using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060023B7 RID: 9143 RVA: 0x0007FE64 File Offset: 0x0007E064
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			if (this.IsCompilerContext)
			{
				string fullName = customBuilder.Ctor.ReflectedType.FullName;
				if (fullName == "System.Reflection.AssemblyVersionAttribute")
				{
					this.version = this.create_assembly_version(customBuilder.string_arg());
					return;
				}
				if (fullName == "System.Reflection.AssemblyCultureAttribute")
				{
					this.culture = this.GetCultureString(customBuilder.string_arg());
				}
				else if (fullName == "System.Reflection.AssemblyAlgorithmIdAttribute")
				{
					byte[] data = customBuilder.Data;
					int num = 2;
					this.algid = (uint)data[num];
					this.algid |= (uint)((uint)data[num + 1] << 8);
					this.algid |= (uint)((uint)data[num + 2] << 16);
					this.algid |= (uint)((uint)data[num + 3] << 24);
				}
				else if (fullName == "System.Reflection.AssemblyFlagsAttribute")
				{
					byte[] data = customBuilder.Data;
					int num = 2;
					this.flags |= (uint)data[num];
					this.flags |= (uint)((uint)data[num + 1] << 8);
					this.flags |= (uint)((uint)data[num + 2] << 16);
					this.flags |= (uint)((uint)data[num + 3] << 24);
					if (this.sn == null)
					{
						this.flags &= 4294967294U;
					}
				}
			}
			if (this.cattrs != null)
			{
				CustomAttributeBuilder[] array = new CustomAttributeBuilder[this.cattrs.Length + 1];
				this.cattrs.CopyTo(array, 0);
				array[this.cattrs.Length] = customBuilder;
				this.cattrs = array;
			}
			else
			{
				this.cattrs = new CustomAttributeBuilder[1];
				this.cattrs[0] = customBuilder;
			}
		}

		/// <summary>Set a custom attribute on this assembly using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="binaryAttribute">A byte blob representing the attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="con" /> is not a RuntimeConstructorInfo.</exception>
		// Token: 0x060023B8 RID: 9144 RVA: 0x00080024 File Offset: 0x0007E224
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x00080058 File Offset: 0x0007E258
		internal void SetCorlibTypeBuilders(Type corlib_object_type, Type corlib_value_type, Type corlib_enum_type)
		{
			this.corlib_object_type = corlib_object_type;
			this.corlib_value_type = corlib_value_type;
			this.corlib_enum_type = corlib_enum_type;
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x00080070 File Offset: 0x0007E270
		internal void SetCorlibTypeBuilders(Type corlib_object_type, Type corlib_value_type, Type corlib_enum_type, Type corlib_void_type)
		{
			this.SetCorlibTypeBuilders(corlib_object_type, corlib_value_type, corlib_enum_type);
			this.corlib_void_type = corlib_void_type;
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x00080084 File Offset: 0x0007E284
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x00080090 File Offset: 0x0007E290
		private void check_name_and_filename(string name, string fileName, bool fileNeedsToExists)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal.", "name");
			}
			if (fileName.Length == 0)
			{
				throw new ArgumentException("Empty file name is not legal.", "fileName");
			}
			if (Path.GetFileName(fileName) != fileName)
			{
				throw new ArgumentException("fileName '" + fileName + "' must not include a path.", "fileName");
			}
			string text = fileName;
			if (this.dir != null)
			{
				text = Path.Combine(this.dir, fileName);
			}
			if (fileNeedsToExists && !File.Exists(text))
			{
				throw new FileNotFoundException("Could not find file '" + fileName + "'");
			}
			if (this.resources != null)
			{
				for (int i = 0; i < this.resources.Length; i++)
				{
					if (this.resources[i].filename == text)
					{
						throw new ArgumentException("Duplicate file name '" + fileName + "'");
					}
					if (this.resources[i].name == name)
					{
						throw new ArgumentException("Duplicate name '" + name + "'");
					}
				}
			}
			if (this.modules != null)
			{
				for (int j = 0; j < this.modules.Length; j++)
				{
					if (!this.modules[j].IsTransient() && this.modules[j].FileName == fileName)
					{
						throw new ArgumentException("Duplicate file name '" + fileName + "'");
					}
					if (this.modules[j].Name == name)
					{
						throw new ArgumentException("Duplicate name '" + name + "'");
					}
				}
			}
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x0008027C File Offset: 0x0007E47C
		private string create_assembly_version(string version)
		{
			string[] array = version.Split(new char[]
			{
				'.'
			});
			int[] array2 = new int[4];
			if (array.Length < 0 || array.Length > 4)
			{
				throw new ArgumentException("The version specified '" + version + "' is invalid");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == "*")
				{
					DateTime now = DateTime.Now;
					if (i == 2)
					{
						array2[2] = (now - new DateTime(2000, 1, 1)).Days;
						if (array.Length == 3)
						{
							array2[3] = (now.Second + now.Minute * 60 + now.Hour * 3600) / 2;
						}
					}
					else
					{
						if (i != 3)
						{
							throw new ArgumentException("The version specified '" + version + "' is invalid");
						}
						array2[3] = (now.Second + now.Minute * 60 + now.Hour * 3600) / 2;
					}
				}
				else
				{
					try
					{
						array2[i] = int.Parse(array[i]);
					}
					catch (FormatException)
					{
						throw new ArgumentException("The version specified '" + version + "' is invalid");
					}
				}
			}
			return string.Concat(new object[]
			{
				array2[0],
				".",
				array2[1],
				".",
				array2[2],
				".",
				array2[3]
			});
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x00080434 File Offset: 0x0007E634
		private string GetCultureString(string str)
		{
			return (!(str == "neutral")) ? str : string.Empty;
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x00080454 File Offset: 0x0007E654
		internal override AssemblyName UnprotectedGetName()
		{
			AssemblyName assemblyName = base.UnprotectedGetName();
			if (this.sn != null)
			{
				assemblyName.SetPublicKey(this.sn.PublicKey);
				assemblyName.SetPublicKeyToken(this.sn.PublicKeyToken);
			}
			return assemblyName;
		}

		// Token: 0x04000D75 RID: 3445
		private const AssemblyBuilderAccess COMPILER_ACCESS = (AssemblyBuilderAccess)2048;

		// Token: 0x04000D76 RID: 3446
		private UIntPtr dynamic_assembly;

		// Token: 0x04000D77 RID: 3447
		private MethodInfo entry_point;

		// Token: 0x04000D78 RID: 3448
		private ModuleBuilder[] modules;

		// Token: 0x04000D79 RID: 3449
		private string name;

		// Token: 0x04000D7A RID: 3450
		private string dir;

		// Token: 0x04000D7B RID: 3451
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04000D7C RID: 3452
		private MonoResource[] resources;

		// Token: 0x04000D7D RID: 3453
		private byte[] public_key;

		// Token: 0x04000D7E RID: 3454
		private string version;

		// Token: 0x04000D7F RID: 3455
		private string culture;

		// Token: 0x04000D80 RID: 3456
		private uint algid;

		// Token: 0x04000D81 RID: 3457
		private uint flags;

		// Token: 0x04000D82 RID: 3458
		private PEFileKinds pekind = PEFileKinds.Dll;

		// Token: 0x04000D83 RID: 3459
		private bool delay_sign;

		// Token: 0x04000D84 RID: 3460
		private uint access;

		// Token: 0x04000D85 RID: 3461
		private Module[] loaded_modules;

		// Token: 0x04000D86 RID: 3462
		private MonoWin32Resource[] win32_resources;

		// Token: 0x04000D87 RID: 3463
		private RefEmitPermissionSet[] permissions_minimum;

		// Token: 0x04000D88 RID: 3464
		private RefEmitPermissionSet[] permissions_optional;

		// Token: 0x04000D89 RID: 3465
		private RefEmitPermissionSet[] permissions_refused;

		// Token: 0x04000D8A RID: 3466
		private PortableExecutableKinds peKind;

		// Token: 0x04000D8B RID: 3467
		private ImageFileMachine machine;

		// Token: 0x04000D8C RID: 3468
		private bool corlib_internal;

		// Token: 0x04000D8D RID: 3469
		private Type[] type_forwarders;

		// Token: 0x04000D8E RID: 3470
		private byte[] pktoken;

		// Token: 0x04000D8F RID: 3471
		internal Type corlib_object_type = typeof(object);

		// Token: 0x04000D90 RID: 3472
		internal Type corlib_value_type = typeof(ValueType);

		// Token: 0x04000D91 RID: 3473
		internal Type corlib_enum_type = typeof(Enum);

		// Token: 0x04000D92 RID: 3474
		internal Type corlib_void_type = typeof(void);

		// Token: 0x04000D93 RID: 3475
		private ArrayList resource_writers;

		// Token: 0x04000D94 RID: 3476
		private Win32VersionResource version_res;

		// Token: 0x04000D95 RID: 3477
		private bool created;

		// Token: 0x04000D96 RID: 3478
		private bool is_module_only;

		// Token: 0x04000D97 RID: 3479
		private StrongName sn;

		// Token: 0x04000D98 RID: 3480
		private NativeResourceType native_resource;

		// Token: 0x04000D99 RID: 3481
		private readonly bool is_compiler_context;

		// Token: 0x04000D9A RID: 3482
		private string versioninfo_culture;

		// Token: 0x04000D9B RID: 3483
		private ModuleBuilder manifest_module;
	}
}
