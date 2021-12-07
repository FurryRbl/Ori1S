using System;
using System.Collections;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;

namespace System.Reflection
{
	/// <summary>Represents an assembly, which is a reusable, versionable, and self-describing building block of a common language runtime application.</summary>
	// Token: 0x0200026D RID: 621
	[ComDefaultInterface(typeof(_Assembly))]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[Serializable]
	public class Assembly : ISerializable, ICustomAttributeProvider, _Assembly, IEvidenceFactory
	{
		// Token: 0x0600204E RID: 8270 RVA: 0x00076FEC File Offset: 0x000751EC
		internal Assembly()
		{
			this.resolve_event_holder = new Assembly.ResolveEventHolder();
		}

		/// <summary>Occurs when the common language runtime class loader cannot resolve a reference to an internal module of an assembly through normal means.</summary>
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600204F RID: 8271 RVA: 0x00077000 File Offset: 0x00075200
		// (remove) Token: 0x06002050 RID: 8272 RVA: 0x00077010 File Offset: 0x00075210
		public event ModuleResolveEventHandler ModuleResolve
		{
			[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlAppDomain\"/>\n</PermissionSet>\n")]
			add
			{
				this.resolve_event_holder.ModuleResolve += value;
			}
			[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlAppDomain\"/>\n</PermissionSet>\n")]
			remove
			{
				this.resolve_event_holder.ModuleResolve -= value;
			}
		}

		// Token: 0x06002051 RID: 8273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string get_code_base(bool escaped);

		// Token: 0x06002052 RID: 8274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string get_fullname();

		// Token: 0x06002053 RID: 8275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string get_location();

		// Token: 0x06002054 RID: 8276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string InternalImageRuntimeVersion();

		// Token: 0x06002055 RID: 8277 RVA: 0x00077020 File Offset: 0x00075220
		private string GetCodeBase(bool escaped)
		{
			string text = this.get_code_base(escaped);
			if (SecurityManager.SecurityEnabled && string.Compare("FILE://", 0, text, 0, 7, true, CultureInfo.InvariantCulture) == 0)
			{
				string path = text.Substring(7);
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, path).Demand();
			}
			return text;
		}

		/// <summary>Gets the location of the assembly as specified originally, for example, in an <see cref="T:System.Reflection.AssemblyName" /> object.</summary>
		/// <returns>The location of the assembly as specified originally.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06002056 RID: 8278 RVA: 0x00077070 File Offset: 0x00075270
		public virtual string CodeBase
		{
			get
			{
				return this.GetCodeBase(false);
			}
		}

		/// <summary>Gets the URI, including escape characters, that represents the codebase.</summary>
		/// <returns>A URI with escape characters.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x0007707C File Offset: 0x0007527C
		public virtual string EscapedCodeBase
		{
			get
			{
				return this.GetCodeBase(true);
			}
		}

		/// <summary>Gets the display name of the assembly.</summary>
		/// <returns>The display name of the assembly.</returns>
		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06002058 RID: 8280 RVA: 0x00077088 File Offset: 0x00075288
		public virtual string FullName
		{
			get
			{
				return this.ToString();
			}
		}

		/// <summary>Gets the entry point of this assembly.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object that represents the entry point of this assembly. If no entry point is found (for example, the assembly is a DLL), null is returned.</returns>
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06002059 RID: 8281
		public virtual extern MethodInfo EntryPoint { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		/// <summary>Gets the evidence for this assembly.</summary>
		/// <returns>An Evidence object for this assembly.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600205A RID: 8282 RVA: 0x00077090 File Offset: 0x00075290
		public virtual Evidence Evidence
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence\"/>\n</PermissionSet>\n")]
			get
			{
				return this.UnprotectedGetEvidence();
			}
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x00077098 File Offset: 0x00075298
		internal Evidence UnprotectedGetEvidence()
		{
			if (this._evidence == null)
			{
				lock (this)
				{
					this._evidence = Evidence.GetDefaultHostEvidence(this);
				}
			}
			return this._evidence;
		}

		// Token: 0x0600205C RID: 8284
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool get_global_assembly_cache();

		/// <summary>Gets a value indicating whether the assembly was loaded from the global assembly cache.</summary>
		/// <returns>true if the assembly was loaded from the global assembly cache; otherwise, false.</returns>
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x000770F4 File Offset: 0x000752F4
		public bool GlobalAssemblyCache
		{
			get
			{
				return this.get_global_assembly_cache();
			}
		}

		// Token: 0x17000569 RID: 1385
		// (set) Token: 0x0600205E RID: 8286 RVA: 0x000770FC File Offset: 0x000752FC
		internal bool FromByteArray
		{
			set
			{
				this.fromByteArray = value;
			}
		}

		/// <summary>Gets the path or UNC location of the loaded file that contains the manifest.</summary>
		/// <returns>The location of the loaded file that contains the manifest. If the loaded file was shadow-copied, the location is that of the file after being shadow-copied. If the assembly is loaded from a byte array, such as when using the <see cref="M:System.Reflection.Assembly.Load(System.Byte[])" /> method overload, the value returned is an empty string ("").</returns>
		/// <exception cref="T:System.NotSupportedException">The current assembly is a dynamic assembly, represented by an <see cref="T:System.Reflection.Emit.AssemblyBuilder" /> object. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x00077108 File Offset: 0x00075308
		public virtual string Location
		{
			get
			{
				if (this.fromByteArray)
				{
					return string.Empty;
				}
				string location = this.get_location();
				if (location != string.Empty && SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, location).Demand();
				}
				return location;
			}
		}

		/// <summary>Gets a string representing the version of the common language runtime (CLR) saved in the file containing the manifest.</summary>
		/// <returns>A string representing the CLR version folder name. This is not a full path.</returns>
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06002060 RID: 8288 RVA: 0x00077154 File Offset: 0x00075354
		[ComVisible(false)]
		public virtual string ImageRuntimeVersion
		{
			get
			{
				return this.InternalImageRuntimeVersion();
			}
		}

		/// <summary>Gets serialization information with all of the data needed to reinstantiate this assembly.</summary>
		/// <param name="info">The object to be populated with serialization information. </param>
		/// <param name="context">The destination context of the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06002061 RID: 8289 RVA: 0x0007715C File Offset: 0x0007535C
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetAssemblyData(this, info, context);
		}

		/// <summary>Indicates whether or not a specified attribute has been applied to the assembly.</summary>
		/// <returns>true if the attribute has been applied to the assembly; otherwise, false.</returns>
		/// <param name="attributeType">The <see cref="T:System.Type" /> of the attribute to be checked for this assembly. </param>
		/// <param name="inherit">This argument is ignored for objects of this type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> uses an invalid type.</exception>
		// Token: 0x06002062 RID: 8290 RVA: 0x00077178 File Offset: 0x00075378
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		/// <summary>Gets all the custom attributes for this assembly.</summary>
		/// <returns>An array of type Object containing the custom attributes for this assembly.</returns>
		/// <param name="inherit">This argument is ignored for objects of type <see cref="T:System.Reflection.Assembly" />. </param>
		// Token: 0x06002063 RID: 8291 RVA: 0x00077184 File Offset: 0x00075384
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		/// <summary>Gets the custom attributes for this assembly as specified by type.</summary>
		/// <returns>An array of type Object containing the custom attributes for this assembly as specified by <paramref name="attributeType" />.</returns>
		/// <param name="attributeType">The <see cref="T:System.Type" /> for which the custom attributes are to be returned. </param>
		/// <param name="inherit">This argument is ignored for objects of type <see cref="T:System.Reflection.Assembly" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not a runtime type. </exception>
		// Token: 0x06002064 RID: 8292 RVA: 0x00077190 File Offset: 0x00075390
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06002065 RID: 8293
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object GetFilesInternal(string name, bool getResourceModules);

		/// <summary>Gets the files in the file table of an assembly manifest.</summary>
		/// <returns>An array of <see cref="T:System.IO.FileStream" /> objects.</returns>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">A file was not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">A file was not a valid assembly. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002066 RID: 8294 RVA: 0x0007719C File Offset: 0x0007539C
		public virtual FileStream[] GetFiles()
		{
			return this.GetFiles(false);
		}

		/// <summary>Gets the files in the file table of an assembly manifest, specifying whether to include resource modules.</summary>
		/// <returns>An array of <see cref="T:System.IO.FileStream" /> objects.</returns>
		/// <param name="getResourceModules">true to include resource modules; otherwise, false. </param>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">A file was not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">A file was not a valid assembly. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002067 RID: 8295 RVA: 0x000771A8 File Offset: 0x000753A8
		public virtual FileStream[] GetFiles(bool getResourceModules)
		{
			string[] array = (string[])this.GetFilesInternal(null, getResourceModules);
			if (array == null)
			{
				return new FileStream[0];
			}
			string location = this.Location;
			FileStream[] array2;
			if (location != string.Empty)
			{
				array2 = new FileStream[array.Length + 1];
				array2[0] = new FileStream(location, FileMode.Open, FileAccess.Read);
				for (int i = 0; i < array.Length; i++)
				{
					array2[i + 1] = new FileStream(array[i], FileMode.Open, FileAccess.Read);
				}
			}
			else
			{
				array2 = new FileStream[array.Length];
				for (int j = 0; j < array.Length; j++)
				{
					array2[j] = new FileStream(array[j], FileMode.Open, FileAccess.Read);
				}
			}
			return array2;
		}

		/// <summary>Gets a <see cref="T:System.IO.FileStream" /> for the specified file in the file table of the manifest of this assembly.</summary>
		/// <returns>A <see cref="T:System.IO.FileStream" /> for the specified file, or null if the file is not found.</returns>
		/// <param name="name">The name of the specified file. Do not include the path to the file.</param>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> was not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> is not a valid assembly. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002068 RID: 8296 RVA: 0x00077258 File Offset: 0x00075458
		public virtual FileStream GetFile(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(null, "Name cannot be null.");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not valid");
			}
			string text = (string)this.GetFilesInternal(name, true);
			if (text != null)
			{
				return new FileStream(text, FileMode.Open, FileAccess.Read);
			}
			return null;
		}

		// Token: 0x06002069 RID: 8297
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetManifestResourceInternal(string name, out int size, out Module module);

		/// <summary>Loads the specified manifest resource from this assembly.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> representing the manifest resource; null if no resources were specified during compilation, or if the resource is not visible to the caller.</returns>
		/// <param name="name">The case-sensitive name of the manifest resource being requested. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> was not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> is not a valid assembly. </exception>
		// Token: 0x0600206A RID: 8298 RVA: 0x000772AC File Offset: 0x000754AC
		public unsafe virtual Stream GetManifestResourceStream(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("String cannot have zero length.", "name");
			}
			ManifestResourceInfo manifestResourceInfo = this.GetManifestResourceInfo(name);
			if (manifestResourceInfo == null)
			{
				return null;
			}
			if (manifestResourceInfo.ReferencedAssembly != null)
			{
				return manifestResourceInfo.ReferencedAssembly.GetManifestResourceStream(name);
			}
			if (manifestResourceInfo.FileName != null && manifestResourceInfo.ResourceLocation == (ResourceLocation)0)
			{
				if (this.fromByteArray)
				{
					throw new FileNotFoundException(manifestResourceInfo.FileName);
				}
				string directoryName = Path.GetDirectoryName(this.Location);
				string path = Path.Combine(directoryName, manifestResourceInfo.FileName);
				return new FileStream(path, FileMode.Open, FileAccess.Read);
			}
			else
			{
				int num;
				Module module;
				IntPtr manifestResourceInternal = this.GetManifestResourceInternal(name, out num, out module);
				if (manifestResourceInternal == (IntPtr)0)
				{
					return null;
				}
				UnmanagedMemoryStream unmanagedMemoryStream = new UnmanagedMemoryStream((byte*)((void*)manifestResourceInternal), (long)num);
				unmanagedMemoryStream.Closed += new Assembly.ResourceCloseHandler(module).OnClose;
				return unmanagedMemoryStream;
			}
		}

		/// <summary>Loads the specified manifest resource, scoped by the namespace of the specified type, from this assembly.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> representing the manifest resource; null if no resources were specified during compilation or if the resource is not visible to the caller.</returns>
		/// <param name="type">The type whose namespace is used to scope the manifest resource name. </param>
		/// <param name="name">The case-sensitive name of the manifest resource being requested. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> was not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> is not a valid assembly. </exception>
		// Token: 0x0600206B RID: 8299 RVA: 0x000773A8 File Offset: 0x000755A8
		public virtual Stream GetManifestResourceStream(Type type, string name)
		{
			string text;
			if (type != null)
			{
				text = type.Namespace;
			}
			else
			{
				if (name == null)
				{
					throw new ArgumentNullException("type");
				}
				text = null;
			}
			if (text == null || text.Length == 0)
			{
				return this.GetManifestResourceStream(name);
			}
			return this.GetManifestResourceStream(text + "." + name);
		}

		// Token: 0x0600206C RID: 8300
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal virtual extern Type[] GetTypes(bool exportedOnly);

		/// <summary>Gets the types defined in this assembly.</summary>
		/// <returns>An array of type <see cref="T:System.Type" /> containing objects for all the types defined in this assembly.</returns>
		/// <exception cref="T:System.Reflection.ReflectionTypeLoadException">The assembly contains one or more types that cannot be loaded. The array returned by the <see cref="P:System.Reflection.ReflectionTypeLoadException.Types" /> property of this exception contains a <see cref="T:System.Type" /> object for each type that was loaded and null for each type that could not be loaded, while the <see cref="P:System.Reflection.ReflectionTypeLoadException.LoaderExceptions" /> property contains an exception for each type that could not be loaded.</exception>
		// Token: 0x0600206D RID: 8301 RVA: 0x00077408 File Offset: 0x00075608
		public virtual Type[] GetTypes()
		{
			return this.GetTypes(false);
		}

		/// <summary>Gets the public types defined in this assembly that are visible outside the assembly.</summary>
		/// <returns>An array of Type objects that represent the types defined in this assembly that are visible outside the assembly.</returns>
		// Token: 0x0600206E RID: 8302 RVA: 0x00077414 File Offset: 0x00075614
		public virtual Type[] GetExportedTypes()
		{
			return this.GetTypes(true);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object with the specified name in the assembly instance and optionally throws an exception if the type is not found.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the specified class.</returns>
		/// <param name="name">The full name of the type. </param>
		/// <param name="throwOnError">true to throw an exception if the type is not found; false to return null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is invalid.-or- The length of <paramref name="name" /> exceeds 1024 characters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is true, and the type cannot be found.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="name" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="name" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="name" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		// Token: 0x0600206F RID: 8303 RVA: 0x00077420 File Offset: 0x00075620
		public virtual Type GetType(string name, bool throwOnError)
		{
			return this.GetType(name, throwOnError, false);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object with the specified name in the assembly instance.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the specified class, or null if the class is not found.</returns>
		/// <param name="name">The full name of the type. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is invalid. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="name" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="name" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="name" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		// Token: 0x06002070 RID: 8304 RVA: 0x0007742C File Offset: 0x0007562C
		public virtual Type GetType(string name)
		{
			return this.GetType(name, false, false);
		}

		// Token: 0x06002071 RID: 8305
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Type InternalGetType(Module module, string name, bool throwOnError, bool ignoreCase);

		/// <summary>Gets the <see cref="T:System.Type" /> object with the specified name in the assembly instance, with the options of ignoring the case, and of throwing an exception if the type is not found.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the specified class.</returns>
		/// <param name="name">The full name of the type. </param>
		/// <param name="throwOnError">true to throw an exception if the type is not found; false to return null. </param>
		/// <param name="ignoreCase">true to ignore the case of the type name; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is invalid.-or- The length of <paramref name="name" /> exceeds 1024 characters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is true, and the type cannot be found.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="name" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="name" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="name" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		// Token: 0x06002072 RID: 8306 RVA: 0x00077438 File Offset: 0x00075638
		public Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			if (name == null)
			{
				throw new ArgumentNullException(name);
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("name", "Name cannot be empty");
			}
			return this.InternalGetType(null, name, throwOnError, ignoreCase);
		}

		// Token: 0x06002073 RID: 8307
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalGetAssemblyName(string assemblyFile, AssemblyName aname);

		// Token: 0x06002074 RID: 8308
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FillName(Assembly ass, AssemblyName aname);

		/// <summary>Gets an <see cref="T:System.Reflection.AssemblyName" /> for this assembly, setting the codebase as specified by <paramref name="copiedName" />.</summary>
		/// <returns>An <see cref="T:System.Reflection.AssemblyName" /> for this assembly.</returns>
		/// <param name="copiedName">true to set the <see cref="P:System.Reflection.Assembly.CodeBase" /> to the location of the assembly after it was shadow copied; false to set <see cref="P:System.Reflection.Assembly.CodeBase" /> to the original location. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002075 RID: 8309 RVA: 0x00077478 File Offset: 0x00075678
		[MonoTODO("copiedName == true is not supported")]
		public virtual AssemblyName GetName(bool copiedName)
		{
			if (SecurityManager.SecurityEnabled)
			{
				this.GetCodeBase(true);
			}
			return this.UnprotectedGetName();
		}

		/// <summary>Gets an <see cref="T:System.Reflection.AssemblyName" /> for this assembly.</summary>
		/// <returns>An <see cref="T:System.Reflection.AssemblyName" /> for this assembly.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002076 RID: 8310 RVA: 0x00077494 File Offset: 0x00075694
		public virtual AssemblyName GetName()
		{
			return this.GetName(false);
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x000774A0 File Offset: 0x000756A0
		internal virtual AssemblyName UnprotectedGetName()
		{
			AssemblyName assemblyName = new AssemblyName();
			Assembly.FillName(this, assemblyName);
			return assemblyName;
		}

		/// <summary>Returns the full name of the assembly, also known as the display name.</summary>
		/// <returns>The full name of the assembly, or the class name if the full name of the assembly cannot be determined.</returns>
		// Token: 0x06002078 RID: 8312 RVA: 0x000774BC File Offset: 0x000756BC
		public override string ToString()
		{
			if (this.assemblyName != null)
			{
				return this.assemblyName;
			}
			this.assemblyName = this.get_fullname();
			return this.assemblyName;
		}

		/// <summary>Creates the name of a type qualified by the display name of its assembly.</summary>
		/// <returns>A String that is the full name of the type qualified by the display name of the assembly.</returns>
		/// <param name="assemblyName">The display name of an assembly. </param>
		/// <param name="typeName">The full name of a type. </param>
		// Token: 0x06002079 RID: 8313 RVA: 0x000774F0 File Offset: 0x000756F0
		public static string CreateQualifiedName(string assemblyName, string typeName)
		{
			return typeName + ", " + assemblyName;
		}

		/// <summary>Gets the currently loaded assembly in which the specified class is defined.</summary>
		/// <returns>The assembly in which the specified class is defined.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> object representing a class in the assembly that will be returned. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		// Token: 0x0600207A RID: 8314 RVA: 0x00077500 File Offset: 0x00075700
		public static Assembly GetAssembly(Type type)
		{
			if (type != null)
			{
				return type.Assembly;
			}
			throw new ArgumentNullException("type");
		}

		/// <summary>Gets the process executable in the default application domain. In other application domains, this is the first executable that was executed by <see cref="M:System.AppDomain.ExecuteAssembly(System.String)" />.</summary>
		/// <returns>The Assembly that is the process executable in the default application domain, or the first executable that was executed by <see cref="M:System.AppDomain.ExecuteAssembly(System.String)" />. Can return null when called from unmanaged code.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600207B RID: 8315
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Assembly GetEntryAssembly();

		/// <summary>Gets the satellite assembly for the specified culture.</summary>
		/// <returns>The specified satellite assembly.</returns>
		/// <param name="culture">The specified culture. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly cannot be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The satellite assembly with a matching file name was found, but the CultureInfo did not match the one specified. </exception>
		/// <exception cref="T:System.BadImageFormatException">The satellite assembly is not a valid assembly. </exception>
		// Token: 0x0600207C RID: 8316 RVA: 0x0007751C File Offset: 0x0007571C
		public Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			return this.GetSatelliteAssembly(culture, null, true);
		}

		/// <summary>Gets the specified version of the satellite assembly for the specified culture.</summary>
		/// <returns>The specified satellite assembly.</returns>
		/// <param name="culture">The specified culture. </param>
		/// <param name="version">The version of the satellite assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The satellite assembly with a matching file name was found, but the CultureInfo or the version did not match the one specified. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly cannot be found. </exception>
		/// <exception cref="T:System.BadImageFormatException">The satellite assembly is not a valid assembly. </exception>
		// Token: 0x0600207D RID: 8317 RVA: 0x00077528 File Offset: 0x00075728
		public Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
		{
			return this.GetSatelliteAssembly(culture, version, true);
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x00077534 File Offset: 0x00075734
		internal Assembly GetSatelliteAssemblyNoThrow(CultureInfo culture, Version version)
		{
			return this.GetSatelliteAssembly(culture, version, false);
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x00077540 File Offset: 0x00075740
		private Assembly GetSatelliteAssembly(CultureInfo culture, Version version, bool throwOnError)
		{
			if (culture == null)
			{
				throw new ArgumentException("culture");
			}
			AssemblyName name = this.GetName(true);
			if (version != null)
			{
				name.Version = version;
			}
			name.CultureInfo = culture;
			name.Name += ".resources";
			try
			{
				Assembly assembly = AppDomain.CurrentDomain.LoadSatellite(name, false);
				if (assembly != null)
				{
					return assembly;
				}
			}
			catch (FileNotFoundException)
			{
			}
			string directoryName = Path.GetDirectoryName(this.Location);
			string text = Path.Combine(directoryName, Path.Combine(culture.Name, name.Name + ".dll"));
			if (!throwOnError && !File.Exists(text))
			{
				return null;
			}
			return Assembly.LoadFrom(text);
		}

		// Token: 0x06002080 RID: 8320
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Assembly LoadFrom(string assemblyFile, bool refonly);

		/// <summary>Loads an assembly given its file name or path.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a filename extension. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="assemblyFile" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name is longer than MAX_PATH characters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002081 RID: 8321 RVA: 0x00077624 File Offset: 0x00075824
		public static Assembly LoadFrom(string assemblyFile)
		{
			return Assembly.LoadFrom(assemblyFile, false);
		}

		/// <summary>Loads an assembly given its file name or path and supplying security evidence.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly. </param>
		/// <param name="securityEvidence">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a filename extension. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.-or-The <paramref name="securityEvidence" /> is not ambiguous and is determined to be invalid.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="assemblyFile" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name is longer than MAX_PATH characters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002082 RID: 8322 RVA: 0x00077630 File Offset: 0x00075830
		public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFile, false);
			if (assembly != null && securityEvidence != null)
			{
				assembly.Evidence.Merge(securityEvidence);
			}
			return assembly;
		}

		/// <summary>Loads an assembly given its file name or path, security evidence hash value, and hash algorithm.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly. </param>
		/// <param name="securityEvidence">Evidence for loading the assembly. </param>
		/// <param name="hashValue">The value of the computed hash code. </param>
		/// <param name="hashAlgorithm">The hash algorithm used for hashing files and for generating the strong name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a filename extension. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.-or-The <paramref name="securityEvidence" /> is not ambiguous and is determined to be invalid. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="assemblyFile" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name is longer than MAX_PATH characters.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002083 RID: 8323 RVA: 0x00077660 File Offset: 0x00075860
		[MonoTODO("This overload is not currently implemented")]
		public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			if (assemblyFile == string.Empty)
			{
				throw new ArgumentException("Name can't be the empty string", "assemblyFile");
			}
			throw new NotImplementedException();
		}

		/// <summary>Loads an assembly given its path, loading the assembly into the domain of the caller using the supplied evidence.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="path">The path of the assembly file. </param>
		/// <param name="securityEvidence">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The <paramref name="path" /> parameter is an empty string ("") or does not exist. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="path" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="path" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002084 RID: 8324 RVA: 0x000776A4 File Offset: 0x000758A4
		public static Assembly LoadFile(string path, Evidence securityEvidence)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path == string.Empty)
			{
				throw new ArgumentException("Path can't be empty", "path");
			}
			return Assembly.LoadFrom(path, securityEvidence);
		}

		/// <summary>Loads the contents of an assembly file on the specified path.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="path">The path of the file to load. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The <paramref name="path" /> parameter is an empty string ("") or does not exist. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="path" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="path" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002085 RID: 8325 RVA: 0x000776EC File Offset: 0x000758EC
		public static Assembly LoadFile(string path)
		{
			return Assembly.LoadFile(path, null);
		}

		/// <summary>Loads an assembly given the long form of its name.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyString">The long form of the assembly name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyString" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyString" /> is a zero-length string. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyString" /> is not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyString" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyString" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002086 RID: 8326 RVA: 0x000776F8 File Offset: 0x000758F8
		public static Assembly Load(string assemblyString)
		{
			return AppDomain.CurrentDomain.Load(assemblyString);
		}

		/// <summary>Loads an assembly given its display name, loading the assembly into the domain of the caller using the supplied evidence.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyString">The display name of the assembly. </param>
		/// <param name="assemblySecurity">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyString" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyString" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyString" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyString" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded.-or-An assembly or module was loaded twice with two different evidences. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002087 RID: 8327 RVA: 0x00077708 File Offset: 0x00075908
		public static Assembly Load(string assemblyString, Evidence assemblySecurity)
		{
			return AppDomain.CurrentDomain.Load(assemblyString, assemblySecurity);
		}

		/// <summary>Loads an assembly given its <see cref="T:System.Reflection.AssemblyName" />.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyRef">The <see cref="T:System.Reflection.AssemblyName" /> object that describes the assembly to be loaded. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyRef" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyRef" /> is not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyRef" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyRef" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002088 RID: 8328 RVA: 0x00077718 File Offset: 0x00075918
		public static Assembly Load(AssemblyName assemblyRef)
		{
			return AppDomain.CurrentDomain.Load(assemblyRef);
		}

		/// <summary>Loads an assembly given its <see cref="T:System.Reflection.AssemblyName" />. The assembly is loaded into the domain of the caller using the supplied evidence.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyRef">The <see cref="T:System.Reflection.AssemblyName" /> object that describes the assembly to be loaded. </param>
		/// <param name="assemblySecurity">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyRef" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyRef" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyRef" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyRef" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002089 RID: 8329 RVA: 0x00077728 File Offset: 0x00075928
		public static Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity)
		{
			return AppDomain.CurrentDomain.Load(assemblyRef, assemblySecurity);
		}

		/// <summary>Loads the assembly with a common object file format (COFF)-based image containing an emitted assembly. The assembly is loaded into the domain of the caller.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="rawAssembly">An array of type byte that is a COFF-based image containing an emitted assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600208A RID: 8330 RVA: 0x00077738 File Offset: 0x00075938
		public static Assembly Load(byte[] rawAssembly)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly);
		}

		/// <summary>Loads the assembly with a common object file format (COFF)-based image containing an emitted assembly.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="rawAssembly">An array of type byte that is a COFF-based image containing an emitted assembly. </param>
		/// <param name="rawSymbolStore">An array of type byte containing the raw bytes representing the symbols for the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600208B RID: 8331 RVA: 0x00077748 File Offset: 0x00075948
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, rawSymbolStore);
		}

		/// <summary>Loads the assembly with a common object file format (COFF)-based image containing an emitted assembly.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="rawAssembly">An array of type byte that is a COFF-based image containing an emitted assembly. </param>
		/// <param name="rawSymbolStore">An array of type byte containing the raw bytes representing the symbols for the assembly. </param>
		/// <param name="securityEvidence">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600208C RID: 8332 RVA: 0x00077758 File Offset: 0x00075958
		public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, rawSymbolStore, securityEvidence);
		}

		/// <summary>Loads the assembly from a common object file format (COFF)-based image containing an emitted assembly. The assembly is loaded into the reflection-only context of the caller's application domain.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> object that represents the loaded assembly.</returns>
		/// <param name="rawAssembly">An array of type byte that is a COFF-based image containing an emitted assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="rawAssembly" /> cannot be loaded. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600208D RID: 8333 RVA: 0x00077768 File Offset: 0x00075968
		public static Assembly ReflectionOnlyLoad(byte[] rawAssembly)
		{
			return AppDomain.CurrentDomain.Load(rawAssembly, null, null, true);
		}

		/// <summary>Loads an assembly into the reflection-only context, given its display name.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> object that represents the loaded assembly.</returns>
		/// <param name="assemblyString">The display name of the assembly, as returned by the <see cref="P:System.Reflection.AssemblyName.FullName" /> property.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyString" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyString" /> is an empty string (""). </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyString" /> is not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="assemblyString" /> is found, but cannot be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyString" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyString" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600208E RID: 8334 RVA: 0x00077778 File Offset: 0x00075978
		public static Assembly ReflectionOnlyLoad(string assemblyString)
		{
			return AppDomain.CurrentDomain.Load(assemblyString, null, true);
		}

		/// <summary>Loads an assembly into the reflection-only context, given its path.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> object that represents the loaded assembly.</returns>
		/// <param name="assemblyFile">The path of the file that contains the manifest of the assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found, or the module you are trying to load does not specify a file name extension. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="assemblyFile" /> is found, but could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.Security.SecurityException">A codebase that does not start with "file://" was specified without the required <see cref="T:System.Net.WebPermission" />. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The assembly name is longer than MAX_PATH characters. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyFile" /> is an empty string (""). </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600208F RID: 8335 RVA: 0x00077788 File Offset: 0x00075988
		public static Assembly ReflectionOnlyLoadFrom(string assemblyFile)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			return Assembly.LoadFrom(assemblyFile, true);
		}

		/// <summary>Loads an assembly from the application directory or from the global assembly cache using a partial name.</summary>
		/// <returns>The loaded assembly. If <paramref name="partialName" /> is not found, this method returns null.</returns>
		/// <param name="partialName">The display name of the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="partialName" /> parameter is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="partialName" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002090 RID: 8336 RVA: 0x000777A4 File Offset: 0x000759A4
		[Obsolete("")]
		public static Assembly LoadWithPartialName(string partialName)
		{
			return Assembly.LoadWithPartialName(partialName, null);
		}

		/// <summary>Loads the module, internal to this assembly, with a common object file format (COFF)-based image containing an emitted module, or a resource file.</summary>
		/// <returns>The loaded Module.</returns>
		/// <param name="moduleName">Name of the module. Must correspond to a file name in this assembly's manifest. </param>
		/// <param name="rawModule">A byte array that is a COFF-based image containing an emitted module, or a resource. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="moduleName" /> or <paramref name="rawModule" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="moduleName" /> does not match a file entry in this assembly's manifest. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawModule" /> is not a valid module. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002091 RID: 8337 RVA: 0x000777B0 File Offset: 0x000759B0
		[MonoTODO("Not implemented")]
		public Module LoadModule(string moduleName, byte[] rawModule)
		{
			throw new NotImplementedException();
		}

		/// <summary>Loads the module, internal to this assembly, with a common object file format (COFF)-based image containing an emitted module, or a resource file. The raw bytes representing the symbols for the module are also loaded.</summary>
		/// <returns>The loaded module.</returns>
		/// <param name="moduleName">Name of the module. Must correspond to a file name in this assembly's manifest. </param>
		/// <param name="rawModule">A byte array that is a COFF-based image containing an emitted module, or a resource. </param>
		/// <param name="rawSymbolStore">A byte array containing the raw bytes representing the symbols for the module. Must be null if this is a resource file. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="moduleName" /> or <paramref name="rawModule" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="moduleName" /> does not match a file entry in this assembly's manifest. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawModule" /> is not a valid module. </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002092 RID: 8338 RVA: 0x000777B8 File Offset: 0x000759B8
		[MonoTODO("Not implemented")]
		public Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002093 RID: 8339
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Assembly load_with_partial_name(string name, Evidence e);

		/// <summary>Loads an assembly from the application directory or from the global assembly cache using a partial name. The assembly is loaded into the domain of the caller using the supplied evidence.</summary>
		/// <returns>The loaded assembly. If <paramref name="partialName" /> is not found, this method returns null.</returns>
		/// <param name="partialName">The display name of the assembly. </param>
		/// <param name="securityEvidence">
		///   <see cref="T:System.Security.Policy.Evidence" /> for loading the assembly. </param>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different sets of evidence. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="partialName" /> parameter is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="partialName" /> was compiled with a later version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002094 RID: 8340 RVA: 0x000777C0 File Offset: 0x000759C0
		[Obsolete("")]
		public static Assembly LoadWithPartialName(string partialName, Evidence securityEvidence)
		{
			return Assembly.LoadWithPartialName(partialName, securityEvidence, true);
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x000777CC File Offset: 0x000759CC
		internal static Assembly LoadWithPartialName(string partialName, Evidence securityEvidence, bool oldBehavior)
		{
			if (!oldBehavior)
			{
				throw new NotImplementedException();
			}
			if (partialName == null)
			{
				throw new NullReferenceException();
			}
			return Assembly.load_with_partial_name(partialName, securityEvidence);
		}

		/// <summary>Locates the specified type from this assembly and creates an instance of it using the system activator, using case-sensitive search.</summary>
		/// <returns>An instance of <see cref="T:System.Object" /> representing the type, with culture, arguments, binder, and activation attributes set to null, and <see cref="T:System.Reflection.BindingFlags" /> set to Public or Instance, or null if <paramref name="typeName" /> is not found.</returns>
		/// <param name="typeName">The <see cref="P:System.Type.FullName" /> of the type to locate. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeName" /> is an empty string ("") or a string beginning with a null character.-or-The current assembly was loaded into the reflection-only context.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="typeName" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="typeName" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="typeName" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="typeName" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="typeName" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06002096 RID: 8342 RVA: 0x000777F0 File Offset: 0x000759F0
		public object CreateInstance(string typeName)
		{
			return this.CreateInstance(typeName, false);
		}

		/// <summary>Locates the specified type from this assembly and creates an instance of it using the system activator, with optional case-sensitive search.</summary>
		/// <returns>An instance of <see cref="T:System.Object" /> representing the type, with culture, arguments, binder, and activation attributes set to null, and <see cref="T:System.Reflection.BindingFlags" /> set to Public or Instance, or null if <paramref name="typeName" /> is not found.</returns>
		/// <param name="typeName">The <see cref="P:System.Type.FullName" /> of the type to locate. </param>
		/// <param name="ignoreCase">true to ignore the case of the type name; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeName" /> is an empty string ("") or a string beginning with a null character. -or-The current assembly was loaded into the reflection-only context.</exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="typeName" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="typeName" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="typeName" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="typeName" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="typeName" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06002097 RID: 8343 RVA: 0x000777FC File Offset: 0x000759FC
		public object CreateInstance(string typeName, bool ignoreCase)
		{
			Type type = this.GetType(typeName, false, ignoreCase);
			if (type == null)
			{
				return null;
			}
			object result;
			try
			{
				result = Activator.CreateInstance(type);
			}
			catch (InvalidOperationException)
			{
				throw new ArgumentException("It is illegal to invoke a method on a Type loaded via ReflectionOnly methods.");
			}
			return result;
		}

		/// <summary>Locates the specified type from this assembly and creates an instance of it using the system activator, with optional case-sensitive search and having the specified culture, arguments, and binding and activation attributes.</summary>
		/// <returns>An instance of Object representing the type and matching the specified criteria, or null if <paramref name="typeName" /> is not found.</returns>
		/// <param name="typeName">The <see cref="P:System.Type.FullName" /> of the type to locate. </param>
		/// <param name="ignoreCase">true to ignore the case of the type name; otherwise, false. </param>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of bit flags from <see cref="T:System.Reflection.BindingFlags" />. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects via reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">An array of type Object containing the arguments to be passed to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to be invoked. If the default constructor is desired, <paramref name="args" /> must be an empty array or null. </param>
		/// <param name="culture">An instance of CultureInfo used to govern the coercion of types. If this is null, the CultureInfo for the current thread is used. (This is necessary to convert a String that represents 1000 to a Double value, for example, since 1000 is represented differently by different cultures.) </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeName" /> is an empty string ("") or a string beginning with a null character. -or-The current assembly was loaded into the reflection-only context.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.NotSupportedException">A non-empty activation attributes array is passed to a type that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="typeName" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="typeName" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="typeName" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="typeName" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="typeName" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06002098 RID: 8344 RVA: 0x0007785C File Offset: 0x00075A5C
		public object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			Type type = this.GetType(typeName, false, ignoreCase);
			if (type == null)
			{
				return null;
			}
			object result;
			try
			{
				result = Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
			}
			catch (InvalidOperationException)
			{
				throw new ArgumentException("It is illegal to invoke a method on a Type loaded via ReflectionOnly methods.");
			}
			return result;
		}

		/// <summary>Gets all the loaded modules that are part of this assembly.</summary>
		/// <returns>An array of modules.</returns>
		// Token: 0x06002099 RID: 8345 RVA: 0x000778C4 File Offset: 0x00075AC4
		public Module[] GetLoadedModules()
		{
			return this.GetLoadedModules(false);
		}

		/// <summary>Gets all the loaded modules that are part of this assembly, specifying whether to include resource modules.</summary>
		/// <returns>An array of modules.</returns>
		/// <param name="getResourceModules">true to include resource modules; otherwise, false. </param>
		// Token: 0x0600209A RID: 8346 RVA: 0x000778D0 File Offset: 0x00075AD0
		public Module[] GetLoadedModules(bool getResourceModules)
		{
			return this.GetModules(getResourceModules);
		}

		/// <summary>Gets all the modules that are part of this assembly.</summary>
		/// <returns>An array of modules.</returns>
		/// <exception cref="T:System.IO.FileNotFoundException">The module to be loaded does not specify a file name extension. </exception>
		// Token: 0x0600209B RID: 8347 RVA: 0x000778DC File Offset: 0x00075ADC
		public Module[] GetModules()
		{
			return this.GetModules(false);
		}

		/// <summary>Gets the specified module in this assembly.</summary>
		/// <returns>The module being requested, or null if the module is not found.</returns>
		/// <param name="name">The name of the module being requested. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string (""). </exception>
		/// <exception cref="T:System.IO.FileLoadException">A file that was found could not be loaded. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="name" /> was not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="name" /> is not a valid assembly. </exception>
		// Token: 0x0600209C RID: 8348 RVA: 0x000778E8 File Offset: 0x00075AE8
		public Module GetModule(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Name can't be empty");
			}
			Module[] modules = this.GetModules(true);
			foreach (Module module in modules)
			{
				if (module.ScopeName == name)
				{
					return module;
				}
			}
			return null;
		}

		// Token: 0x0600209D RID: 8349
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal virtual extern Module[] GetModulesInternal();

		/// <summary>Gets all the modules that are part of this assembly, specifying whether to include resource modules.</summary>
		/// <returns>An array of modules.</returns>
		/// <param name="getResourceModules">true to include resource modules; otherwise, false. </param>
		// Token: 0x0600209E RID: 8350 RVA: 0x00077954 File Offset: 0x00075B54
		public Module[] GetModules(bool getResourceModules)
		{
			Module[] modulesInternal = this.GetModulesInternal();
			if (!getResourceModules)
			{
				ArrayList arrayList = new ArrayList(modulesInternal.Length);
				foreach (Module module in modulesInternal)
				{
					if (!module.IsResource())
					{
						arrayList.Add(module);
					}
				}
				return (Module[])arrayList.ToArray(typeof(Module));
			}
			return modulesInternal;
		}

		// Token: 0x0600209F RID: 8351
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string[] GetNamespaces();

		/// <summary>Returns the names of all the resources in this assembly.</summary>
		/// <returns>An array of type String containing the names of all the resources.</returns>
		// Token: 0x060020A0 RID: 8352
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern string[] GetManifestResourceNames();

		/// <summary>Gets the assembly that contains the code that is currently executing.</summary>
		/// <returns>A <see cref="T:System.Reflection.Assembly" /> representing the assembly that contains the code that is currently executing. </returns>
		// Token: 0x060020A1 RID: 8353
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Assembly GetExecutingAssembly();

		/// <summary>Returns the <see cref="T:System.Reflection.Assembly" /> of the method that invoked the currently executing method.</summary>
		/// <returns>The Assembly object of the method that invoked the currently executing method.</returns>
		// Token: 0x060020A2 RID: 8354
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Assembly GetCallingAssembly();

		/// <summary>Gets the <see cref="T:System.Reflection.AssemblyName" /> objects for all the assemblies referenced by this assembly.</summary>
		/// <returns>An array of type <see cref="T:System.Reflection.AssemblyName" /> containing all the assemblies referenced by this assembly.</returns>
		// Token: 0x060020A3 RID: 8355
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AssemblyName[] GetReferencedAssemblies();

		// Token: 0x060020A4 RID: 8356
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetManifestResourceInfoInternal(string name, ManifestResourceInfo info);

		/// <summary>Returns information about how the given resource has been persisted.</summary>
		/// <returns>
		///   <see cref="T:System.Reflection.ManifestResourceInfo" /> populated with information about the resource's topology, or null if the resource is not found.</returns>
		/// <param name="resourceName">The case-sensitive name of the resource. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resourceName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="resourceName" /> parameter is an empty string (""). </exception>
		// Token: 0x060020A5 RID: 8357 RVA: 0x000779C0 File Offset: 0x00075BC0
		public virtual ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			if (resourceName == null)
			{
				throw new ArgumentNullException("resourceName");
			}
			if (resourceName.Length == 0)
			{
				throw new ArgumentException("String cannot have zero length.");
			}
			ManifestResourceInfo manifestResourceInfo = new ManifestResourceInfo();
			bool manifestResourceInfoInternal = this.GetManifestResourceInfoInternal(resourceName, manifestResourceInfo);
			if (manifestResourceInfoInternal)
			{
				return manifestResourceInfo;
			}
			return null;
		}

		// Token: 0x060020A6 RID: 8358
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int MonoDebugger_GetMethodToken(MethodBase method);

		/// <summary>Gets the host context with which the assembly was loaded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the host context with which the assembly was loaded, if any.</returns>
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060020A7 RID: 8359 RVA: 0x00077A0C File Offset: 0x00075C0C
		[ComVisible(false)]
		[MonoTODO("Always returns zero")]
		public long HostContext
		{
			get
			{
				return 0L;
			}
		}

		/// <summary>Gets the module that contains the manifest for the current assembly. </summary>
		/// <returns>A <see cref="T:System.Reflection.Module" /> object representing the module that contains the manifest for the assembly. </returns>
		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060020A8 RID: 8360 RVA: 0x00077A10 File Offset: 0x00075C10
		[ComVisible(false)]
		public Module ManifestModule
		{
			get
			{
				return this.GetManifestModule();
			}
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x00077A18 File Offset: 0x00075C18
		internal virtual Module GetManifestModule()
		{
			return this.GetManifestModuleInternal();
		}

		// Token: 0x060020AA RID: 8362
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Module GetManifestModuleInternal();

		/// <summary>Gets a <see cref="T:System.Boolean" /> value indicating whether this assembly was loaded into the reflection-only context.</summary>
		/// <returns>true if the assembly was loaded into the reflection-only context, rather than the execution context; otherwise, false.</returns>
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060020AB RID: 8363
		[ComVisible(false)]
		public virtual extern bool ReflectionOnly { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060020AC RID: 8364 RVA: 0x00077A20 File Offset: 0x00075C20
		internal void Resolve()
		{
			lock (this)
			{
				this.LoadAssemblyPermissions();
				Evidence evidence = new Evidence(this.UnprotectedGetEvidence());
				evidence.AddHost(new PermissionRequestEvidence(this._minimum, this._optional, this._refuse));
				this._granted = SecurityManager.ResolvePolicy(evidence, this._minimum, this._optional, this._refuse, out this._denied);
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060020AD RID: 8365 RVA: 0x00077AB0 File Offset: 0x00075CB0
		internal PermissionSet GrantedPermissionSet
		{
			get
			{
				if (this._granted == null)
				{
					if (SecurityManager.ResolvingPolicyLevel != null)
					{
						if (SecurityManager.ResolvingPolicyLevel.IsFullTrustAssembly(this))
						{
							return DefaultPolicies.FullTrust;
						}
						return null;
					}
					else
					{
						this.Resolve();
					}
				}
				return this._granted;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060020AE RID: 8366 RVA: 0x00077AF8 File Offset: 0x00075CF8
		internal PermissionSet DeniedPermissionSet
		{
			get
			{
				if (this._granted == null)
				{
					if (SecurityManager.ResolvingPolicyLevel != null)
					{
						if (SecurityManager.ResolvingPolicyLevel.IsFullTrustAssembly(this))
						{
							return null;
						}
						return DefaultPolicies.FullTrust;
					}
					else
					{
						this.Resolve();
					}
				}
				return this._denied;
			}
		}

		// Token: 0x060020AF RID: 8367
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool LoadPermissions(Assembly a, ref IntPtr minimum, ref int minLength, ref IntPtr optional, ref int optLength, ref IntPtr refused, ref int refLength);

		// Token: 0x060020B0 RID: 8368 RVA: 0x00077B40 File Offset: 0x00075D40
		private void LoadAssemblyPermissions()
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			IntPtr zero3 = IntPtr.Zero;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (Assembly.LoadPermissions(this, ref zero, ref num, ref zero2, ref num2, ref zero3, ref num3))
			{
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(zero, array, 0, num);
					this._minimum = SecurityManager.Decode(array);
				}
				if (num2 > 0)
				{
					byte[] array2 = new byte[num2];
					Marshal.Copy(zero2, array2, 0, num2);
					this._optional = SecurityManager.Decode(array2);
				}
				if (num3 > 0)
				{
					byte[] array3 = new byte[num3];
					Marshal.Copy(zero3, array3, 0, num3);
					this._refuse = SecurityManager.Decode(array3);
				}
			}
		}

		/// <summary>Returns the type of the current instance.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Reflection.Assembly" /> type.</returns>
		// Token: 0x060020B1 RID: 8369 RVA: 0x00077BF8 File Offset: 0x00075DF8
		virtual Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000BF2 RID: 3058
		private IntPtr _mono_assembly;

		// Token: 0x04000BF3 RID: 3059
		private Assembly.ResolveEventHolder resolve_event_holder;

		// Token: 0x04000BF4 RID: 3060
		private Evidence _evidence;

		// Token: 0x04000BF5 RID: 3061
		internal PermissionSet _minimum;

		// Token: 0x04000BF6 RID: 3062
		internal PermissionSet _optional;

		// Token: 0x04000BF7 RID: 3063
		internal PermissionSet _refuse;

		// Token: 0x04000BF8 RID: 3064
		private PermissionSet _granted;

		// Token: 0x04000BF9 RID: 3065
		private PermissionSet _denied;

		// Token: 0x04000BFA RID: 3066
		private bool fromByteArray;

		// Token: 0x04000BFB RID: 3067
		private string assemblyName;

		// Token: 0x0200026E RID: 622
		internal class ResolveEventHolder
		{
			// Token: 0x14000015 RID: 21
			// (add) Token: 0x060020B3 RID: 8371 RVA: 0x00077C08 File Offset: 0x00075E08
			// (remove) Token: 0x060020B4 RID: 8372 RVA: 0x00077C24 File Offset: 0x00075E24
			public event ModuleResolveEventHandler ModuleResolve;
		}

		// Token: 0x0200026F RID: 623
		private class ResourceCloseHandler
		{
			// Token: 0x060020B5 RID: 8373 RVA: 0x00077C40 File Offset: 0x00075E40
			public ResourceCloseHandler(Module module)
			{
				this.module = module;
			}

			// Token: 0x060020B6 RID: 8374 RVA: 0x00077C50 File Offset: 0x00075E50
			public void OnClose(object sender, EventArgs e)
			{
				this.module = null;
			}

			// Token: 0x04000BFD RID: 3069
			private Module module;
		}
	}
}
