using System;
using System.Collections;
using System.Text;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x0200006B RID: 107
	internal class GeneralNames
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x000196B8 File Offset: 0x000178B8
		public GeneralNames()
		{
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000196C0 File Offset: 0x000178C0
		public GeneralNames(string[] rfc822s, string[] dnsNames, string[] ipAddresses, string[] uris)
		{
			this.asn = new ASN1(48);
			if (rfc822s != null)
			{
				this.rfc822Name = new ArrayList();
				foreach (string s in rfc822s)
				{
					this.asn.Add(new ASN1(129, Encoding.ASCII.GetBytes(s)));
					this.rfc822Name.Add(rfc822s);
				}
			}
			if (dnsNames != null)
			{
				this.dnsName = new ArrayList();
				foreach (string text in dnsNames)
				{
					this.asn.Add(new ASN1(130, Encoding.ASCII.GetBytes(text)));
					this.dnsName.Add(text);
				}
			}
			if (ipAddresses != null)
			{
				this.ipAddr = new ArrayList();
				foreach (string text2 in ipAddresses)
				{
					string[] array = text2.Split(new char[]
					{
						'.',
						':'
					});
					byte[] array2 = new byte[array.Length];
					for (int l = 0; l < array.Length; l++)
					{
						array2[l] = byte.Parse(array[l]);
					}
					this.asn.Add(new ASN1(135, array2));
					this.ipAddr.Add(text2);
				}
			}
			if (uris != null)
			{
				this.uris = new ArrayList();
				foreach (string text3 in uris)
				{
					this.asn.Add(new ASN1(134, Encoding.ASCII.GetBytes(text3)));
					this.uris.Add(text3);
				}
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000198A8 File Offset: 0x00017AA8
		public GeneralNames(ASN1 sequence)
		{
			int i = 0;
			while (i < sequence.Count)
			{
				byte tag = sequence[i].Tag;
				switch (tag)
				{
				case 129:
					if (this.rfc822Name == null)
					{
						this.rfc822Name = new ArrayList();
					}
					this.rfc822Name.Add(Encoding.ASCII.GetString(sequence[i].Value));
					break;
				case 130:
					if (this.dnsName == null)
					{
						this.dnsName = new ArrayList();
					}
					this.dnsName.Add(Encoding.ASCII.GetString(sequence[i].Value));
					break;
				default:
					if (tag == 164)
					{
						goto IL_CF;
					}
					break;
				case 132:
					goto IL_CF;
				case 134:
					if (this.uris == null)
					{
						this.uris = new ArrayList();
					}
					this.uris.Add(Encoding.ASCII.GetString(sequence[i].Value));
					break;
				case 135:
				{
					if (this.ipAddr == null)
					{
						this.ipAddr = new ArrayList();
					}
					byte[] value = sequence[i].Value;
					string value2 = (value.Length != 4) ? ":" : ".";
					StringBuilder stringBuilder = new StringBuilder();
					for (int j = 0; j < value.Length; j++)
					{
						stringBuilder.Append(value[j].ToString());
						if (j < value.Length - 1)
						{
							stringBuilder.Append(value2);
						}
					}
					this.ipAddr.Add(stringBuilder.ToString());
					if (this.ipAddr == null)
					{
						this.ipAddr = new ArrayList();
					}
					break;
				}
				}
				IL_1F9:
				i++;
				continue;
				IL_CF:
				if (this.directoryNames == null)
				{
					this.directoryNames = new ArrayList();
				}
				this.directoryNames.Add(X501.ToString(sequence[i][0]));
				goto IL_1F9;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00019AC0 File Offset: 0x00017CC0
		public string[] RFC822
		{
			get
			{
				if (this.rfc822Name == null)
				{
					return new string[0];
				}
				return (string[])this.rfc822Name.ToArray(typeof(string));
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00019AFC File Offset: 0x00017CFC
		public string[] DirectoryNames
		{
			get
			{
				if (this.directoryNames == null)
				{
					return new string[0];
				}
				return (string[])this.directoryNames.ToArray(typeof(string));
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00019B38 File Offset: 0x00017D38
		public string[] DNSNames
		{
			get
			{
				if (this.dnsName == null)
				{
					return new string[0];
				}
				return (string[])this.dnsName.ToArray(typeof(string));
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00019B74 File Offset: 0x00017D74
		public string[] UniformResourceIdentifiers
		{
			get
			{
				if (this.uris == null)
				{
					return new string[0];
				}
				return (string[])this.uris.ToArray(typeof(string));
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00019BB0 File Offset: 0x00017DB0
		public string[] IPAddresses
		{
			get
			{
				if (this.ipAddr == null)
				{
					return new string[0];
				}
				return (string[])this.ipAddr.ToArray(typeof(string));
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00019BEC File Offset: 0x00017DEC
		public byte[] GetBytes()
		{
			return this.asn.GetBytes();
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00019BFC File Offset: 0x00017DFC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.rfc822Name != null)
			{
				foreach (object obj in this.rfc822Name)
				{
					string value = (string)obj;
					stringBuilder.Append("RFC822 Name=");
					stringBuilder.Append(value);
					stringBuilder.Append(Environment.NewLine);
				}
			}
			if (this.dnsName != null)
			{
				foreach (object obj2 in this.dnsName)
				{
					string value2 = (string)obj2;
					stringBuilder.Append("DNS Name=");
					stringBuilder.Append(value2);
					stringBuilder.Append(Environment.NewLine);
				}
			}
			if (this.directoryNames != null)
			{
				foreach (object obj3 in this.directoryNames)
				{
					string value3 = (string)obj3;
					stringBuilder.Append("Directory Address: ");
					stringBuilder.Append(value3);
					stringBuilder.Append(Environment.NewLine);
				}
			}
			if (this.uris != null)
			{
				foreach (object obj4 in this.uris)
				{
					string value4 = (string)obj4;
					stringBuilder.Append("URL=");
					stringBuilder.Append(value4);
					stringBuilder.Append(Environment.NewLine);
				}
			}
			if (this.ipAddr != null)
			{
				foreach (object obj5 in this.ipAddr)
				{
					string value5 = (string)obj5;
					stringBuilder.Append("IP Address=");
					stringBuilder.Append(value5);
					stringBuilder.Append(Environment.NewLine);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040001CE RID: 462
		private ArrayList rfc822Name;

		// Token: 0x040001CF RID: 463
		private ArrayList dnsName;

		// Token: 0x040001D0 RID: 464
		private ArrayList directoryNames;

		// Token: 0x040001D1 RID: 465
		private ArrayList uris;

		// Token: 0x040001D2 RID: 466
		private ArrayList ipAddr;

		// Token: 0x040001D3 RID: 467
		private ASN1 asn;
	}
}
