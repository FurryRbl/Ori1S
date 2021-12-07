using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Text;
using Mono.Security.Cryptography;
using Mono.Xml;

namespace Mono.Security
{
	// Token: 0x020000AB RID: 171
	internal class StrongNameManager
	{
		// Token: 0x060009AE RID: 2478 RVA: 0x000259C0 File Offset: 0x00023BC0
		public static void LoadConfig(string filename)
		{
			if (File.Exists(filename))
			{
				SecurityParser securityParser = new SecurityParser();
				using (StreamReader streamReader = new StreamReader(filename))
				{
					string xml = streamReader.ReadToEnd();
					securityParser.LoadXml(xml);
				}
				SecurityElement securityElement = securityParser.ToXml();
				if (securityElement != null && securityElement.Tag == "configuration")
				{
					SecurityElement securityElement2 = securityElement.SearchForChildByTag("strongNames");
					if (securityElement2 != null && securityElement2.Children.Count > 0)
					{
						SecurityElement securityElement3 = securityElement2.SearchForChildByTag("pubTokenMapping");
						if (securityElement3 != null && securityElement3.Children.Count > 0)
						{
							StrongNameManager.LoadMapping(securityElement3);
						}
						SecurityElement securityElement4 = securityElement2.SearchForChildByTag("verificationSettings");
						if (securityElement4 != null && securityElement4.Children.Count > 0)
						{
							StrongNameManager.LoadVerificationSettings(securityElement4);
						}
					}
				}
			}
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00025AC8 File Offset: 0x00023CC8
		private static void LoadMapping(SecurityElement mapping)
		{
			if (StrongNameManager.mappings == null)
			{
				StrongNameManager.mappings = new Hashtable();
			}
			object syncRoot = StrongNameManager.mappings.SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in mapping.Children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					if (!(securityElement.Tag != "map"))
					{
						string text = securityElement.Attribute("Token");
						if (text != null && text.Length == 16)
						{
							text = text.ToUpper(CultureInfo.InvariantCulture);
							string text2 = securityElement.Attribute("PublicKey");
							if (text2 != null)
							{
								if (StrongNameManager.mappings[text] == null)
								{
									StrongNameManager.mappings.Add(text, text2);
								}
								else
								{
									StrongNameManager.mappings[text] = text2;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00025C10 File Offset: 0x00023E10
		private static void LoadVerificationSettings(SecurityElement settings)
		{
			if (StrongNameManager.tokens == null)
			{
				StrongNameManager.tokens = new Hashtable();
			}
			object syncRoot = StrongNameManager.tokens.SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in settings.Children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					if (!(securityElement.Tag != "skip"))
					{
						string text = securityElement.Attribute("Token");
						if (text != null)
						{
							text = text.ToUpper(CultureInfo.InvariantCulture);
							string text2 = securityElement.Attribute("Assembly");
							if (text2 == null)
							{
								text2 = "*";
							}
							string text3 = securityElement.Attribute("Users");
							if (text3 == null)
							{
								text3 = "*";
							}
							StrongNameManager.Element element = (StrongNameManager.Element)StrongNameManager.tokens[text];
							if (element == null)
							{
								element = new StrongNameManager.Element(text2, text3);
								StrongNameManager.tokens.Add(text, element);
							}
							else if ((string)element.assemblies[text2] == null)
							{
								element.assemblies.Add(text2, text3);
							}
							else if (text3 == "*")
							{
								element.assemblies[text2] = "*";
							}
							else
							{
								string str = (string)element.assemblies[text2];
								string value = str + "," + text3;
								element.assemblies[text2] = value;
							}
						}
					}
				}
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00025DFC File Offset: 0x00023FFC
		public static byte[] GetMappedPublicKey(byte[] token)
		{
			if (StrongNameManager.mappings == null || token == null)
			{
				return null;
			}
			string key = CryptoConvert.ToHex(token);
			string text = (string)StrongNameManager.mappings[key];
			if (text == null)
			{
				return null;
			}
			return CryptoConvert.FromHex(text);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00025E44 File Offset: 0x00024044
		public static bool MustVerify(AssemblyName an)
		{
			if (an == null || StrongNameManager.tokens == null)
			{
				return true;
			}
			string key = CryptoConvert.ToHex(an.GetPublicKeyToken());
			StrongNameManager.Element element = (StrongNameManager.Element)StrongNameManager.tokens[key];
			if (element != null)
			{
				string users = element.GetUsers(an.Name);
				if (users == null)
				{
					users = element.GetUsers("*");
				}
				if (users != null)
				{
					return !(users == "*") && users.IndexOf(Environment.UserName) < 0;
				}
			}
			return true;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00025ED0 File Offset: 0x000240D0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Public Key Token\tAssemblies\t\tUsers");
			stringBuilder.Append(Environment.NewLine);
			foreach (object obj in StrongNameManager.tokens)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				stringBuilder.Append((string)dictionaryEntry.Key);
				StrongNameManager.Element element = (StrongNameManager.Element)dictionaryEntry.Value;
				bool flag = true;
				foreach (object obj2 in element.assemblies)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					if (flag)
					{
						stringBuilder.Append("\t");
						flag = false;
					}
					else
					{
						stringBuilder.Append("\t\t\t");
					}
					stringBuilder.Append((string)dictionaryEntry2.Key);
					stringBuilder.Append("\t");
					string text = (string)dictionaryEntry2.Value;
					if (text == "*")
					{
						text = "All users";
					}
					stringBuilder.Append(text);
					stringBuilder.Append(Environment.NewLine);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400020A RID: 522
		private static Hashtable mappings;

		// Token: 0x0400020B RID: 523
		private static Hashtable tokens;

		// Token: 0x020000AC RID: 172
		private class Element
		{
			// Token: 0x060009B4 RID: 2484 RVA: 0x00026064 File Offset: 0x00024264
			public Element()
			{
				this.assemblies = new Hashtable();
			}

			// Token: 0x060009B5 RID: 2485 RVA: 0x00026078 File Offset: 0x00024278
			public Element(string assembly, string users) : this()
			{
				this.assemblies.Add(assembly, users);
			}

			// Token: 0x060009B6 RID: 2486 RVA: 0x00026090 File Offset: 0x00024290
			public string GetUsers(string assembly)
			{
				return (string)this.assemblies[assembly];
			}

			// Token: 0x0400020C RID: 524
			internal Hashtable assemblies;
		}
	}
}
