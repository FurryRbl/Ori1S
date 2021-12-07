using System;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using Mono.Security;
using Mono.Security.Cryptography;

namespace System.Reflection
{
	/// <summary>Describes an assembly's unique identity in full.</summary>
	// Token: 0x0200027C RID: 636
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_AssemblyName))]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	public sealed class AssemblyName : ICloneable, ISerializable, _AssemblyName, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyName" /> class.</summary>
		// Token: 0x060020D3 RID: 8403 RVA: 0x00077DC4 File Offset: 0x00075FC4
		public AssemblyName()
		{
			this.versioncompat = AssemblyVersionCompatibility.SameMachine;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyName" /> class with the specified display name.</summary>
		/// <param name="assemblyName">The display name of the assembly, as returned by the <see cref="P:System.Reflection.AssemblyName.FullName" /> property.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyName" /> is a zero length string.</exception>
		// Token: 0x060020D4 RID: 8404 RVA: 0x00077DD4 File Offset: 0x00075FD4
		public AssemblyName(string assemblyName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (assemblyName.Length < 1)
			{
				throw new ArgumentException("assemblyName cannot have zero length.");
			}
			if (!AssemblyName.ParseName(this, assemblyName))
			{
				throw new FileLoadException("The assembly name is invalid.");
			}
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x00077E28 File Offset: 0x00076028
		internal AssemblyName(SerializationInfo si, StreamingContext sc)
		{
			this.name = si.GetString("_Name");
			this.codebase = si.GetString("_CodeBase");
			this.version = (Version)si.GetValue("_Version", typeof(Version));
			this.publicKey = (byte[])si.GetValue("_PublicKey", typeof(byte[]));
			this.keyToken = (byte[])si.GetValue("_PublicKeyToken", typeof(byte[]));
			this.hashalg = (AssemblyHashAlgorithm)((int)si.GetValue("_HashAlgorithm", typeof(AssemblyHashAlgorithm)));
			this.keypair = (StrongNameKeyPair)si.GetValue("_StrongNameKeyPair", typeof(StrongNameKeyPair));
			this.versioncompat = (AssemblyVersionCompatibility)((int)si.GetValue("_VersionCompatibility", typeof(AssemblyVersionCompatibility)));
			this.flags = (AssemblyNameFlags)((int)si.GetValue("_Flags", typeof(AssemblyNameFlags)));
			int @int = si.GetInt32("_CultureInfo");
			if (@int != -1)
			{
				this.cultureinfo = new CultureInfo(@int);
			}
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array that receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060020D6 RID: 8406 RVA: 0x00077F5C File Offset: 0x0007615C
		void _AssemblyName.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060020D7 RID: 8407 RVA: 0x00077F64 File Offset: 0x00076164
		void _AssemblyName.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060020D8 RID: 8408 RVA: 0x00077F6C File Offset: 0x0007616C
		void _AssemblyName.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DispIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060020D9 RID: 8409 RVA: 0x00077F74 File Offset: 0x00076174
		void _AssemblyName.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060020DA RID: 8410
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ParseName(AssemblyName aname, string assemblyName);

		/// <summary>Gets or sets a value that identifies the processor and bits-per-word of the platform targeted by an executable.</summary>
		/// <returns>One of the <see cref="T:System.Reflection.ProcessorArchitecture" /> values that identifies the processor and bits-per-word of the platform targeted by an executable.</returns>
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x00077F7C File Offset: 0x0007617C
		// (set) Token: 0x060020DC RID: 8412 RVA: 0x00077F84 File Offset: 0x00076184
		[MonoTODO("Not used, as the values are too limited;  Mono supports more")]
		public ProcessorArchitecture ProcessorArchitecture
		{
			get
			{
				return this.processor_architecture;
			}
			set
			{
				this.processor_architecture = value;
			}
		}

		/// <summary>Gets or sets the simple name of the assembly. This is usually, but not necessarily, the file name of the manifest file of the assembly, minus its extension.</summary>
		/// <returns>A String that is the simple name of the assembly.</returns>
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x00077F90 File Offset: 0x00076190
		// (set) Token: 0x060020DE RID: 8414 RVA: 0x00077F98 File Offset: 0x00076198
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the location of the assembly as a URL.</summary>
		/// <returns>A string that is the URL location of the assembly. </returns>
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060020DF RID: 8415 RVA: 0x00077FA4 File Offset: 0x000761A4
		// (set) Token: 0x060020E0 RID: 8416 RVA: 0x00077FAC File Offset: 0x000761AC
		public string CodeBase
		{
			get
			{
				return this.codebase;
			}
			set
			{
				this.codebase = value;
			}
		}

		/// <summary>Gets the URI, including escape characters, that represents the codebase.</summary>
		/// <returns>A URI with escape characters.</returns>
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060020E1 RID: 8417 RVA: 0x00077FB8 File Offset: 0x000761B8
		public string EscapedCodeBase
		{
			get
			{
				if (this.codebase == null)
				{
					return null;
				}
				return Uri.EscapeString(this.codebase, false, true, true);
			}
		}

		/// <summary>Gets or sets the culture supported by the assembly.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> object representing the culture supported by the assembly.</returns>
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060020E2 RID: 8418 RVA: 0x00077FD8 File Offset: 0x000761D8
		// (set) Token: 0x060020E3 RID: 8419 RVA: 0x00077FE0 File Offset: 0x000761E0
		public CultureInfo CultureInfo
		{
			get
			{
				return this.cultureinfo;
			}
			set
			{
				this.cultureinfo = value;
			}
		}

		/// <summary>Gets or sets the attributes of the assembly.</summary>
		/// <returns>An <see cref="T:System.Reflection.AssemblyNameFlags" /> object representing the attributes of the assembly.</returns>
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060020E4 RID: 8420 RVA: 0x00077FEC File Offset: 0x000761EC
		// (set) Token: 0x060020E5 RID: 8421 RVA: 0x00077FF4 File Offset: 0x000761F4
		public AssemblyNameFlags Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		/// <summary>Gets the full name of the assembly, also known as the display name.</summary>
		/// <returns>A string that is the full name of the assembly, also known as the display name.</returns>
		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x00078000 File Offset: 0x00076200
		public string FullName
		{
			get
			{
				if (this.name == null)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.name);
				if (this.Version != null)
				{
					stringBuilder.Append(", Version=");
					stringBuilder.Append(this.Version.ToString());
				}
				if (this.cultureinfo != null)
				{
					stringBuilder.Append(", Culture=");
					if (this.cultureinfo.LCID == CultureInfo.InvariantCulture.LCID)
					{
						stringBuilder.Append("neutral");
					}
					else
					{
						stringBuilder.Append(this.cultureinfo.Name);
					}
				}
				byte[] array = this.InternalGetPublicKeyToken();
				if (array != null)
				{
					if (array.Length == 0)
					{
						stringBuilder.Append(", PublicKeyToken=null");
					}
					else
					{
						stringBuilder.Append(", PublicKeyToken=");
						for (int i = 0; i < array.Length; i++)
						{
							stringBuilder.Append(array[i].ToString("x2"));
						}
					}
				}
				if ((this.Flags & AssemblyNameFlags.Retargetable) != AssemblyNameFlags.None)
				{
					stringBuilder.Append(", Retargetable=Yes");
				}
				return stringBuilder.ToString();
			}
		}

		/// <summary>Gets or sets the hash algorithm used by the assembly manifest.</summary>
		/// <returns>An AssemblyHashAlgorithm object representing the hash algorithm used by the assembly manifest.</returns>
		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060020E7 RID: 8423 RVA: 0x00078138 File Offset: 0x00076338
		// (set) Token: 0x060020E8 RID: 8424 RVA: 0x00078140 File Offset: 0x00076340
		public AssemblyHashAlgorithm HashAlgorithm
		{
			get
			{
				return this.hashalg;
			}
			set
			{
				this.hashalg = value;
			}
		}

		/// <summary>Gets or sets the public and private cryptographic key pair that is used to create a strong name signature for the assembly.</summary>
		/// <returns>A <see cref="T:System.Reflection.StrongNameKeyPair" /> object containing the public and private cryptographic key pair to be used to create a strong name for the assembly.</returns>
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060020E9 RID: 8425 RVA: 0x0007814C File Offset: 0x0007634C
		// (set) Token: 0x060020EA RID: 8426 RVA: 0x00078154 File Offset: 0x00076354
		public StrongNameKeyPair KeyPair
		{
			get
			{
				return this.keypair;
			}
			set
			{
				this.keypair = value;
			}
		}

		/// <summary>Gets or sets the major, minor, build, and revision numbers of the assembly.</summary>
		/// <returns>A <see cref="T:System.Version" /> object representing the major, minor, build, and revision numbers of the assembly.</returns>
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060020EB RID: 8427 RVA: 0x00078160 File Offset: 0x00076360
		// (set) Token: 0x060020EC RID: 8428 RVA: 0x00078168 File Offset: 0x00076368
		public Version Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
				if (value == null)
				{
					this.major = (this.minor = (this.build = (this.revision = 0)));
				}
				else
				{
					this.major = value.Major;
					this.minor = value.Minor;
					this.build = value.Build;
					this.revision = value.Revision;
				}
			}
		}

		/// <summary>Gets or sets the information related to the assembly's compatibility with other assemblies.</summary>
		/// <returns>An AssemblyVersionCompatibility object representing information about the assembly's compatibility with other assemblies.</returns>
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060020ED RID: 8429 RVA: 0x000781E0 File Offset: 0x000763E0
		// (set) Token: 0x060020EE RID: 8430 RVA: 0x000781E8 File Offset: 0x000763E8
		public AssemblyVersionCompatibility VersionCompatibility
		{
			get
			{
				return this.versioncompat;
			}
			set
			{
				this.versioncompat = value;
			}
		}

		/// <summary>Returns the full name of the assembly, also known as the display name.</summary>
		/// <returns>A String that is the full name of the assembly, or the class name if the full name of the assembly cannot be determined.</returns>
		// Token: 0x060020EF RID: 8431 RVA: 0x000781F4 File Offset: 0x000763F4
		public override string ToString()
		{
			string fullName = this.FullName;
			return (fullName == null) ? base.ToString() : fullName;
		}

		/// <summary>Gets the public key of the assembly.</summary>
		/// <returns>An array of type byte containing the public key of the assembly.</returns>
		/// <exception cref="T:System.Security.SecurityException">A public key was provided (for example, by using the <see cref="M:System.Reflection.AssemblyName.SetPublicKey(System.Byte[])" /> method), but no public key token was provided. </exception>
		// Token: 0x060020F0 RID: 8432 RVA: 0x0007821C File Offset: 0x0007641C
		public byte[] GetPublicKey()
		{
			return this.publicKey;
		}

		/// <summary>Gets the public key token, which is the last 8 bytes of the SHA-1 hash of the public key under which the application or assembly is signed.</summary>
		/// <returns>An array of type byte containing the public key token.</returns>
		// Token: 0x060020F1 RID: 8433 RVA: 0x00078224 File Offset: 0x00076424
		public byte[] GetPublicKeyToken()
		{
			if (this.keyToken != null)
			{
				return this.keyToken;
			}
			if (this.publicKey == null)
			{
				return null;
			}
			if (this.publicKey.Length == 0)
			{
				return new byte[0];
			}
			if (!this.IsPublicKeyValid)
			{
				throw new SecurityException("The public key is not valid.");
			}
			this.keyToken = this.ComputePublicKeyToken();
			return this.keyToken;
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060020F2 RID: 8434 RVA: 0x0007828C File Offset: 0x0007648C
		private bool IsPublicKeyValid
		{
			get
			{
				if (this.publicKey.Length == 16)
				{
					int i = 0;
					int num = 0;
					while (i < this.publicKey.Length)
					{
						num += (int)this.publicKey[i++];
					}
					if (num == 4)
					{
						return true;
					}
				}
				byte b = this.publicKey[0];
				if (b != 6)
				{
					if (b != 7)
					{
						if (b == 0)
						{
							if (this.publicKey.Length > 12 && this.publicKey[12] == 6)
							{
								try
								{
									CryptoConvert.FromCapiPublicKeyBlob(this.publicKey, 12);
									return true;
								}
								catch (CryptographicException)
								{
								}
							}
						}
					}
				}
				else
				{
					try
					{
						CryptoConvert.FromCapiPublicKeyBlob(this.publicKey);
						return true;
					}
					catch (CryptographicException)
					{
					}
				}
				return false;
			}
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x0007839C File Offset: 0x0007659C
		private byte[] InternalGetPublicKeyToken()
		{
			if (this.keyToken != null)
			{
				return this.keyToken;
			}
			if (this.publicKey == null)
			{
				return null;
			}
			if (this.publicKey.Length == 0)
			{
				return new byte[0];
			}
			if (!this.IsPublicKeyValid)
			{
				throw new SecurityException("The public key is not valid.");
			}
			return this.ComputePublicKeyToken();
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x000783F8 File Offset: 0x000765F8
		private byte[] ComputePublicKeyToken()
		{
			HashAlgorithm hashAlgorithm = SHA1.Create();
			byte[] array = hashAlgorithm.ComputeHash(this.publicKey);
			byte[] array2 = new byte[8];
			Array.Copy(array, array.Length - 8, array2, 0, 8);
			Array.Reverse(array2, 0, 8);
			return array2;
		}

		/// <summary>Returns a value indicating whether the loader resolves two assembly names to the same assembly.</summary>
		/// <returns>true if the loader resolves <paramref name="definition" /> to the same assembly as <paramref name="reference" />; otherwise, false.</returns>
		/// <param name="reference">The reference assembly name.</param>
		/// <param name="definition">The assembly name that is compared to the reference assembly.</param>
		// Token: 0x060020F5 RID: 8437 RVA: 0x00078438 File Offset: 0x00076638
		[MonoTODO]
		public static bool ReferenceMatchesDefinition(AssemblyName reference, AssemblyName definition)
		{
			if (reference == null)
			{
				throw new ArgumentNullException("reference");
			}
			if (definition == null)
			{
				throw new ArgumentNullException("definition");
			}
			if (reference.Name != definition.Name)
			{
				return false;
			}
			throw new NotImplementedException();
		}

		/// <summary>Sets the public key identifying the assembly.</summary>
		/// <param name="publicKey">A byte array containing the public key of the assembly. </param>
		// Token: 0x060020F6 RID: 8438 RVA: 0x00078484 File Offset: 0x00076684
		public void SetPublicKey(byte[] publicKey)
		{
			if (publicKey == null)
			{
				this.flags ^= AssemblyNameFlags.PublicKey;
			}
			else
			{
				this.flags |= AssemblyNameFlags.PublicKey;
			}
			this.publicKey = publicKey;
		}

		/// <summary>Sets the public key token, which is the last 8 bytes of the SHA-1 hash of the public key under which the application or assembly is signed.</summary>
		/// <param name="publicKeyToken">A byte array containing the public key token of the assembly. </param>
		// Token: 0x060020F7 RID: 8439 RVA: 0x000784C0 File Offset: 0x000766C0
		public void SetPublicKeyToken(byte[] publicKeyToken)
		{
			this.keyToken = publicKeyToken;
		}

		/// <summary>Gets serialization information with all of the data needed to recreate an instance of this AssemblyName.</summary>
		/// <param name="info">The object to be populated with serialization information. </param>
		/// <param name="context">The destination context of the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x060020F8 RID: 8440 RVA: 0x000784CC File Offset: 0x000766CC
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("_Name", this.name);
			info.AddValue("_PublicKey", this.publicKey);
			info.AddValue("_PublicKeyToken", this.keyToken);
			info.AddValue("_CultureInfo", (this.cultureinfo == null) ? -1 : this.cultureinfo.LCID);
			info.AddValue("_CodeBase", this.codebase);
			info.AddValue("_Version", this.Version);
			info.AddValue("_HashAlgorithm", this.hashalg);
			info.AddValue("_HashAlgorithmForControl", AssemblyHashAlgorithm.None);
			info.AddValue("_StrongNameKeyPair", this.keypair);
			info.AddValue("_VersionCompatibility", this.versioncompat);
			info.AddValue("_Flags", this.flags);
			info.AddValue("_HashForControl", null);
		}

		/// <summary>Makes a copy of this AssemblyName object.</summary>
		/// <returns>An object that is a copy of this AssemblyName object.</returns>
		// Token: 0x060020F9 RID: 8441 RVA: 0x000785D8 File Offset: 0x000767D8
		public object Clone()
		{
			return new AssemblyName
			{
				name = this.name,
				codebase = this.codebase,
				major = this.major,
				minor = this.minor,
				build = this.build,
				revision = this.revision,
				version = this.version,
				cultureinfo = this.cultureinfo,
				flags = this.flags,
				hashalg = this.hashalg,
				keypair = this.keypair,
				publicKey = this.publicKey,
				keyToken = this.keyToken,
				versioncompat = this.versioncompat
			};
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event. </param>
		// Token: 0x060020FA RID: 8442 RVA: 0x00078694 File Offset: 0x00076894
		public void OnDeserialization(object sender)
		{
			this.Version = this.version;
		}

		/// <summary>Gets the AssemblyName for a given file.</summary>
		/// <returns>An AssemblyName object representing the given file.</returns>
		/// <param name="assemblyFile">The path for the assembly whose AssemblyName is to be returned. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyFile" /> is invalid, such as an assembly with an invalid culture. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have path discovery permission. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different sets of evidence. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060020FB RID: 8443 RVA: 0x000786A4 File Offset: 0x000768A4
		public static AssemblyName GetAssemblyName(string assemblyFile)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			AssemblyName assemblyName = new AssemblyName();
			Assembly.InternalGetAssemblyName(Path.GetFullPath(assemblyFile), assemblyName);
			return assemblyName;
		}

		// Token: 0x04000C0A RID: 3082
		private string name;

		// Token: 0x04000C0B RID: 3083
		private string codebase;

		// Token: 0x04000C0C RID: 3084
		private int major;

		// Token: 0x04000C0D RID: 3085
		private int minor;

		// Token: 0x04000C0E RID: 3086
		private int build;

		// Token: 0x04000C0F RID: 3087
		private int revision;

		// Token: 0x04000C10 RID: 3088
		private CultureInfo cultureinfo;

		// Token: 0x04000C11 RID: 3089
		private AssemblyNameFlags flags;

		// Token: 0x04000C12 RID: 3090
		private AssemblyHashAlgorithm hashalg;

		// Token: 0x04000C13 RID: 3091
		private StrongNameKeyPair keypair;

		// Token: 0x04000C14 RID: 3092
		private byte[] publicKey;

		// Token: 0x04000C15 RID: 3093
		private byte[] keyToken;

		// Token: 0x04000C16 RID: 3094
		private AssemblyVersionCompatibility versioncompat;

		// Token: 0x04000C17 RID: 3095
		private Version version;

		// Token: 0x04000C18 RID: 3096
		private ProcessorArchitecture processor_architecture;
	}
}
