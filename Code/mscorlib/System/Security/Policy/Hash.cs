using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;

namespace System.Security.Policy
{
	/// <summary>Provides evidence about the hash value for an assembly. This class cannot be inherited.</summary>
	// Token: 0x02000640 RID: 1600
	[ComVisible(true)]
	[Serializable]
	public sealed class Hash : ISerializable, IBuiltInEvidence
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Hash" /> class.</summary>
		/// <param name="assembly">The <see cref="T:System.Reflection.Assembly" /> for which to compute the hash value. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="assembly" /> parameter is null. </exception>
		// Token: 0x06003CF2 RID: 15602 RVA: 0x000D17CC File Offset: 0x000CF9CC
		public Hash(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			this.assembly = assembly;
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x000D17EC File Offset: 0x000CF9EC
		internal Hash()
		{
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x000D17F4 File Offset: 0x000CF9F4
		internal Hash(SerializationInfo info, StreamingContext context)
		{
			this.data = (byte[])info.GetValue("RawData", typeof(byte[]));
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x000D1828 File Offset: 0x000CFA28
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return (!verbose) ? 0 : 5;
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x000D1838 File Offset: 0x000CFA38
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x000D183C File Offset: 0x000CFA3C
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.MD5" /> hash value for the assembly.</summary>
		/// <returns>A byte array that represents the <see cref="T:System.Security.Cryptography.MD5" /> hash value for the assembly.</returns>
		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06003CF8 RID: 15608 RVA: 0x000D1840 File Offset: 0x000CFA40
		public byte[] MD5
		{
			get
			{
				if (this._md5 != null)
				{
					return this._md5;
				}
				if (this.assembly == null && this._sha1 != null)
				{
					string text = Locale.GetText("No assembly data. This instance was initialized with an MSHA1 digest value.");
					throw new SecurityException(text);
				}
				HashAlgorithm hashAlg = System.Security.Cryptography.MD5.Create();
				this._md5 = this.GenerateHash(hashAlg);
				return this._md5;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.SHA1" /> hash value for the assembly.</summary>
		/// <returns>A byte array that represents the <see cref="T:System.Security.Cryptography.SHA1" /> hash value for the assembly.</returns>
		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06003CF9 RID: 15609 RVA: 0x000D18A0 File Offset: 0x000CFAA0
		public byte[] SHA1
		{
			get
			{
				if (this._sha1 != null)
				{
					return this._sha1;
				}
				if (this.assembly == null && this._md5 != null)
				{
					string text = Locale.GetText("No assembly data. This instance was initialized with an MD5 digest value.");
					throw new SecurityException(text);
				}
				HashAlgorithm hashAlg = System.Security.Cryptography.SHA1.Create();
				this._sha1 = this.GenerateHash(hashAlg);
				return this._sha1;
			}
		}

		/// <summary>Computes the hash value for the assembly using the specified hash algorithm.</summary>
		/// <returns>A byte array that represents the hash value for the assembly.</returns>
		/// <param name="hashAlg">The hash algorithm to use to compute the hash value for the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hashAlg" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The hash value for the assembly cannot be generated.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003CFA RID: 15610 RVA: 0x000D1900 File Offset: 0x000CFB00
		public byte[] GenerateHash(HashAlgorithm hashAlg)
		{
			if (hashAlg == null)
			{
				throw new ArgumentNullException("hashAlg");
			}
			return hashAlg.ComputeHash(this.GetData());
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003CFB RID: 15611 RVA: 0x000D1920 File Offset: 0x000CFB20
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("RawData", this.GetData());
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Hash" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.Hash" />.</returns>
		// Token: 0x06003CFC RID: 15612 RVA: 0x000D1950 File Offset: 0x000CFB50
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement(base.GetType().FullName);
			securityElement.AddAttribute("version", "1");
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array = this.GetData();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("X2"));
			}
			securityElement.AddChild(new SecurityElement("RawData", stringBuilder.ToString()));
			return securityElement.ToString();
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x000D19D4 File Offset: 0x000CFBD4
		[PermissionSet(SecurityAction.Assert, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Unrestricted=\"true\"/>\n</PermissionSet>\n")]
		private byte[] GetData()
		{
			if (this.assembly == null && this.data == null)
			{
				string text = Locale.GetText("No assembly data.");
				throw new SecurityException(text);
			}
			if (this.data == null)
			{
				FileStream fileStream = new FileStream(this.assembly.Location, FileMode.Open, FileAccess.Read);
				this.data = new byte[fileStream.Length];
				fileStream.Read(this.data, 0, (int)fileStream.Length);
			}
			return this.data;
		}

		/// <summary>Creates a <see cref="T:System.Security.Policy.Hash" /> object containing an <see cref="T:System.Security.Cryptography.MD5" /> hash value.</summary>
		/// <returns>A <see cref="T:System.Security.Policy.Hash" /> object containing the hash value provided by the <paramref name="md5" /> parameter.</returns>
		/// <param name="md5">A byte array containing an <see cref="T:System.Security.Cryptography.MD5" /> hash value.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="md5" /> parameter is null. </exception>
		// Token: 0x06003CFE RID: 15614 RVA: 0x000D1A54 File Offset: 0x000CFC54
		public static Hash CreateMD5(byte[] md5)
		{
			if (md5 == null)
			{
				throw new ArgumentNullException("md5");
			}
			return new Hash
			{
				_md5 = md5
			};
		}

		/// <summary>Creates a <see cref="T:System.Security.Policy.Hash" /> object containing an <see cref="T:System.Security.Cryptography.SHA1" /> hash value.</summary>
		/// <returns>A <see cref="T:System.Security.Policy.Hash" /> object containing the hash value provided by the <paramref name="sha1" /> parameter.</returns>
		/// <param name="sha1">A byte array containing a <see cref="T:System.Security.Cryptography.SHA1" /> hash value.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sha1" /> parameter is null. </exception>
		// Token: 0x06003CFF RID: 15615 RVA: 0x000D1A80 File Offset: 0x000CFC80
		public static Hash CreateSHA1(byte[] sha1)
		{
			if (sha1 == null)
			{
				throw new ArgumentNullException("sha1");
			}
			return new Hash
			{
				_sha1 = sha1
			};
		}

		// Token: 0x04001A72 RID: 6770
		private Assembly assembly;

		// Token: 0x04001A73 RID: 6771
		private byte[] data;

		// Token: 0x04001A74 RID: 6772
		internal byte[] _md5;

		// Token: 0x04001A75 RID: 6773
		internal byte[] _sha1;
	}
}
