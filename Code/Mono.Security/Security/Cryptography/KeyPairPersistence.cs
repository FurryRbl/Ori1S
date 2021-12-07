using System;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Mono.Xml;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200002B RID: 43
	public class KeyPairPersistence
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x0000BE08 File Offset: 0x0000A008
		public KeyPairPersistence(CspParameters parameters) : this(parameters, null)
		{
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000BE14 File Offset: 0x0000A014
		public KeyPairPersistence(CspParameters parameters, string keyPair)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this._params = this.Copy(parameters);
			this._keyvalue = keyPair;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000BE5C File Offset: 0x0000A05C
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

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000BF00 File Offset: 0x0000A100
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000BF08 File Offset: 0x0000A108
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

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000BF1C File Offset: 0x0000A11C
		public CspParameters Parameters
		{
			get
			{
				return this.Copy(this._params);
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000BF2C File Offset: 0x0000A12C
		public bool Load()
		{
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

		// Token: 0x060001D1 RID: 465 RVA: 0x0000BF94 File Offset: 0x0000A194
		public void Save()
		{
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

		// Token: 0x060001D2 RID: 466 RVA: 0x0000C024 File Offset: 0x0000A224
		public void Remove()
		{
			File.Delete(this.Filename);
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000C034 File Offset: 0x0000A234
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

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000C158 File Offset: 0x0000A358
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

		// Token: 0x060001D5 RID: 469 RVA: 0x0000C27C File Offset: 0x0000A47C
		internal static bool _CanSecure(string root)
		{
			return true;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000C280 File Offset: 0x0000A480
		internal static bool _ProtectUser(string path)
		{
			return true;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000C284 File Offset: 0x0000A484
		internal static bool _ProtectMachine(string path)
		{
			return true;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000C288 File Offset: 0x0000A488
		internal static bool _IsUserProtected(string path)
		{
			return true;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000C28C File Offset: 0x0000A48C
		internal static bool _IsMachineProtected(string path)
		{
			return true;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000C290 File Offset: 0x0000A490
		private static bool CanSecure(string path)
		{
			int platform = (int)Environment.OSVersion.Platform;
			return platform == 4 || platform == 128 || platform == 6 || KeyPairPersistence._CanSecure(Path.GetPathRoot(path));
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000C2D0 File Offset: 0x0000A4D0
		private static bool ProtectUser(string path)
		{
			return !KeyPairPersistence.CanSecure(path) || KeyPairPersistence._ProtectUser(path);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000C2E8 File Offset: 0x0000A4E8
		private static bool ProtectMachine(string path)
		{
			return !KeyPairPersistence.CanSecure(path) || KeyPairPersistence._ProtectMachine(path);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000C300 File Offset: 0x0000A500
		private static bool IsUserProtected(string path)
		{
			return !KeyPairPersistence.CanSecure(path) || KeyPairPersistence._IsUserProtected(path);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000C318 File Offset: 0x0000A518
		private static bool IsMachineProtected(string path)
		{
			return !KeyPairPersistence.CanSecure(path) || KeyPairPersistence._IsMachineProtected(path);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000C330 File Offset: 0x0000A530
		private bool CanChange
		{
			get
			{
				return this._keyvalue == null;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000C33C File Offset: 0x0000A53C
		private bool UseDefaultKeyContainer
		{
			get
			{
				return (this._params.Flags & CspProviderFlags.UseDefaultKeyContainer) == CspProviderFlags.UseDefaultKeyContainer;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000C350 File Offset: 0x0000A550
		private bool UseMachineKeyStore
		{
			get
			{
				return (this._params.Flags & CspProviderFlags.UseMachineKeyStore) == CspProviderFlags.UseMachineKeyStore;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000C364 File Offset: 0x0000A564
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

		// Token: 0x060001E3 RID: 483 RVA: 0x0000C414 File Offset: 0x0000A614
		private CspParameters Copy(CspParameters p)
		{
			return new CspParameters(p.ProviderType, p.ProviderName, p.KeyContainerName)
			{
				KeyNumber = p.KeyNumber,
				Flags = p.Flags
			};
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000C454 File Offset: 0x0000A654
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

		// Token: 0x060001E5 RID: 485 RVA: 0x0000C4C0 File Offset: 0x0000A6C0
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

		// Token: 0x040000C0 RID: 192
		private static bool _userPathExists = false;

		// Token: 0x040000C1 RID: 193
		private static string _userPath;

		// Token: 0x040000C2 RID: 194
		private static bool _machinePathExists = false;

		// Token: 0x040000C3 RID: 195
		private static string _machinePath;

		// Token: 0x040000C4 RID: 196
		private CspParameters _params;

		// Token: 0x040000C5 RID: 197
		private string _keyvalue;

		// Token: 0x040000C6 RID: 198
		private string _filename;

		// Token: 0x040000C7 RID: 199
		private string _container;

		// Token: 0x040000C8 RID: 200
		private static object lockobj = new object();
	}
}
