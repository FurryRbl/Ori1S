using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Mono.Xml;

namespace Mono.Security.Cryptography
{
	// Token: 0x020000B7 RID: 183
	internal class KeyPairPersistence
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x0002B4B0 File Offset: 0x000296B0
		public KeyPairPersistence(CspParameters parameters) : this(parameters, null)
		{
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002B4BC File Offset: 0x000296BC
		public KeyPairPersistence(CspParameters parameters, string keyPair)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this._params = this.Copy(parameters);
			this._keyvalue = keyPair;
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0002B504 File Offset: 0x00029704
		public string Filename
		{
			get
			{
				if (this._filename == null)
				{
					this._filename = string.Format(CultureInfo.InvariantCulture, "[{0}][{1}][{2}].xml", new object[]
					{
						this._params.ProviderType,
						this.ContainerName,
						this._params.KeyNumber
					});
					if (this.UseMachineKeyStore)
					{
						this._filename = Path.Combine(KeyPairPersistence.MachinePath, this._filename);
					}
					else
					{
						this._filename = Path.Combine(KeyPairPersistence.UserPath, this._filename);
					}
				}
				return this._filename;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0002B5A8 File Offset: 0x000297A8
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x0002B5B0 File Offset: 0x000297B0
		public string KeyValue
		{
			get
			{
				return this._keyvalue;
			}
			set
			{
				if (this.CanChange)
				{
					this._keyvalue = value;
				}
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0002B5C4 File Offset: 0x000297C4
		public CspParameters Parameters
		{
			get
			{
				return this.Copy(this._params);
			}
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0002B5D4 File Offset: 0x000297D4
		public bool Load()
		{
			if (Environment.SocketSecurityEnabled)
			{
				return false;
			}
			bool flag = File.Exists(this.Filename);
			if (flag)
			{
				using (StreamReader streamReader = File.OpenText(this.Filename))
				{
					this.FromXml(streamReader.ReadToEnd());
				}
			}
			return flag;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0002B648 File Offset: 0x00029848
		public void Save()
		{
			if (Environment.SocketSecurityEnabled)
			{
				return;
			}
			using (FileStream fileStream = File.Open(this.Filename, FileMode.Create))
			{
				StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8);
				streamWriter.Write(this.ToXml());
				streamWriter.Close();
			}
			if (this.UseMachineKeyStore)
			{
				KeyPairPersistence.ProtectMachine(this.Filename);
			}
			else
			{
				KeyPairPersistence.ProtectUser(this.Filename);
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0002B6E4 File Offset: 0x000298E4
		public void Remove()
		{
			if (Environment.SocketSecurityEnabled)
			{
				return;
			}
			File.Delete(this.Filename);
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0002B6FC File Offset: 0x000298FC
		private static string UserPath
		{
			get
			{
				object obj = KeyPairPersistence.lockobj;
				lock (obj)
				{
					if (KeyPairPersistence._userPath == null || !KeyPairPersistence._userPathExists)
					{
						KeyPairPersistence._userPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mono");
						KeyPairPersistence._userPath = Path.Combine(KeyPairPersistence._userPath, "keypairs");
						KeyPairPersistence._userPathExists = Directory.Exists(KeyPairPersistence._userPath);
						if (!KeyPairPersistence._userPathExists)
						{
							try
							{
								Directory.CreateDirectory(KeyPairPersistence._userPath);
								KeyPairPersistence.ProtectUser(KeyPairPersistence._userPath);
								KeyPairPersistence._userPathExists = true;
							}
							catch (Exception inner)
							{
								string text = Locale.GetText("Could not create user key store '{0}'.");
								throw new CryptographicException(string.Format(text, KeyPairPersistence._userPath), inner);
							}
						}
					}
				}
				if (!KeyPairPersistence.IsUserProtected(KeyPairPersistence._userPath))
				{
					string text2 = Locale.GetText("Improperly protected user's key pairs in '{0}'.");
					throw new CryptographicException(string.Format(text2, KeyPairPersistence._userPath));
				}
				return KeyPairPersistence._userPath;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0002B820 File Offset: 0x00029A20
		private static string MachinePath
		{
			get
			{
				object obj = KeyPairPersistence.lockobj;
				lock (obj)
				{
					if (KeyPairPersistence._machinePath == null || !KeyPairPersistence._machinePathExists)
					{
						KeyPairPersistence._machinePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ".mono");
						KeyPairPersistence._machinePath = Path.Combine(KeyPairPersistence._machinePath, "keypairs");
						KeyPairPersistence._machinePathExists = Directory.Exists(KeyPairPersistence._machinePath);
						if (!KeyPairPersistence._machinePathExists)
						{
							try
							{
								Directory.CreateDirectory(KeyPairPersistence._machinePath);
								KeyPairPersistence.ProtectMachine(KeyPairPersistence._machinePath);
								KeyPairPersistence._machinePathExists = true;
							}
							catch (Exception inner)
							{
								string text = Locale.GetText("Could not create machine key store '{0}'.");
								throw new CryptographicException(string.Format(text, KeyPairPersistence._machinePath), inner);
							}
						}
					}
				}
				if (!KeyPairPersistence.IsMachineProtected(KeyPairPersistence._machinePath))
				{
					string text2 = Locale.GetText("Improperly protected machine's key pairs in '{0}'.");
					throw new CryptographicException(string.Format(text2, KeyPairPersistence._machinePath));
				}
				return KeyPairPersistence._machinePath;
			}
		}

		// Token: 0x06000A4E RID: 2638
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool _CanSecure(string root);

		// Token: 0x06000A4F RID: 2639
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool _ProtectUser(string path);

		// Token: 0x06000A50 RID: 2640
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool _ProtectMachine(string path);

		// Token: 0x06000A51 RID: 2641
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool _IsUserProtected(string path);

		// Token: 0x06000A52 RID: 2642
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool _IsMachineProtected(string path);

		// Token: 0x06000A53 RID: 2643 RVA: 0x0002B944 File Offset: 0x00029B44
		private static bool CanSecure(string path)
		{
			int platform = (int)Environment.OSVersion.Platform;
			return platform == 4 || platform == 128 || platform == 6 || KeyPairPersistence._CanSecure(Path.GetPathRoot(path));
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002B984 File Offset: 0x00029B84
		private static bool ProtectUser(string path)
		{
			return !KeyPairPersistence.CanSecure(path) || KeyPairPersistence._ProtectUser(path);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0002B99C File Offset: 0x00029B9C
		private static bool ProtectMachine(string path)
		{
			return !KeyPairPersistence.CanSecure(path) || KeyPairPersistence._ProtectMachine(path);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0002B9B4 File Offset: 0x00029BB4
		private static bool IsUserProtected(string path)
		{
			return !KeyPairPersistence.CanSecure(path) || KeyPairPersistence._IsUserProtected(path);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002B9CC File Offset: 0x00029BCC
		private static bool IsMachineProtected(string path)
		{
			return !KeyPairPersistence.CanSecure(path) || KeyPairPersistence._IsMachineProtected(path);
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0002B9E4 File Offset: 0x00029BE4
		private bool CanChange
		{
			get
			{
				return this._keyvalue == null;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0002B9F0 File Offset: 0x00029BF0
		private bool UseDefaultKeyContainer
		{
			get
			{
				return (this._params.Flags & CspProviderFlags.UseDefaultKeyContainer) == CspProviderFlags.UseDefaultKeyContainer;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0002BA04 File Offset: 0x00029C04
		private bool UseMachineKeyStore
		{
			get
			{
				return (this._params.Flags & CspProviderFlags.UseMachineKeyStore) == CspProviderFlags.UseMachineKeyStore;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0002BA18 File Offset: 0x00029C18
		private string ContainerName
		{
			get
			{
				if (this._container == null)
				{
					if (this.UseDefaultKeyContainer)
					{
						this._container = "default";
					}
					else if (this._params.KeyContainerName == null || this._params.KeyContainerName.Length == 0)
					{
						this._container = Guid.NewGuid().ToString();
					}
					else
					{
						byte[] bytes = Encoding.UTF8.GetBytes(this._params.KeyContainerName);
						MD5 md = MD5.Create();
						byte[] b = md.ComputeHash(bytes);
						Guid guid = new Guid(b);
						this._container = guid.ToString();
					}
				}
				return this._container;
			}
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0002BAC8 File Offset: 0x00029CC8
		private CspParameters Copy(CspParameters p)
		{
			return new CspParameters(p.ProviderType, p.ProviderName, p.KeyContainerName)
			{
				KeyNumber = p.KeyNumber,
				Flags = p.Flags
			};
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0002BB08 File Offset: 0x00029D08
		private void FromXml(string xml)
		{
			SecurityParser securityParser = new SecurityParser();
			securityParser.LoadXml(xml);
			SecurityElement securityElement = securityParser.ToXml();
			if (securityElement.Tag == "KeyPair")
			{
				SecurityElement securityElement2 = securityElement.SearchForChildByTag("KeyValue");
				if (securityElement2.Children.Count > 0)
				{
					this._keyvalue = securityElement2.Children[0].ToString();
				}
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002BB74 File Offset: 0x00029D74
		private string ToXml()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<KeyPair>{0}\t<Properties>{0}\t\t<Provider ", Environment.NewLine);
			if (this._params.ProviderName != null && this._params.ProviderName.Length != 0)
			{
				stringBuilder.AppendFormat("Name=\"{0}\" ", this._params.ProviderName);
			}
			stringBuilder.AppendFormat("Type=\"{0}\" />{1}\t\t<Container ", this._params.ProviderType, Environment.NewLine);
			stringBuilder.AppendFormat("Name=\"{0}\" />{1}\t</Properties>{1}\t<KeyValue", this.ContainerName, Environment.NewLine);
			if (this._params.KeyNumber != -1)
			{
				stringBuilder.AppendFormat(" Id=\"{0}\" ", this._params.KeyNumber);
			}
			stringBuilder.AppendFormat(">{1}\t\t{0}{1}\t</KeyValue>{1}</KeyPair>{1}", this.KeyValue, Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x04000261 RID: 609
		private static bool _userPathExists = false;

		// Token: 0x04000262 RID: 610
		private static string _userPath;

		// Token: 0x04000263 RID: 611
		private static bool _machinePathExists = false;

		// Token: 0x04000264 RID: 612
		private static string _machinePath;

		// Token: 0x04000265 RID: 613
		private CspParameters _params;

		// Token: 0x04000266 RID: 614
		private string _keyvalue;

		// Token: 0x04000267 RID: 615
		private string _filename;

		// Token: 0x04000268 RID: 616
		private string _container;

		// Token: 0x04000269 RID: 617
		private static object lockobj = new object();
	}
}
