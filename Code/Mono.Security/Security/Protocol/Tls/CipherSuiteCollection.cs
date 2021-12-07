using System;
using System.Collections;
using System.Globalization;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000081 RID: 129
	internal sealed class CipherSuiteCollection : IEnumerable, ICollection, IList
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x0001CBBC File Offset: 0x0001ADBC
		public CipherSuiteCollection(SecurityProtocolType protocol)
		{
			this.protocol = protocol;
			this.cipherSuites = new ArrayList();
		}

		// Token: 0x1700011D RID: 285
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (CipherSuite)value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0001CBF4 File Offset: 0x0001ADF4
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.cipherSuites.IsSynchronized;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0001CC04 File Offset: 0x0001AE04
		object ICollection.SyncRoot
		{
			get
			{
				return this.cipherSuites.SyncRoot;
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001CC14 File Offset: 0x0001AE14
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.cipherSuites.GetEnumerator();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001CC24 File Offset: 0x0001AE24
		bool IList.Contains(object value)
		{
			return this.cipherSuites.Contains(value as CipherSuite);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001CC38 File Offset: 0x0001AE38
		int IList.IndexOf(object value)
		{
			return this.cipherSuites.IndexOf(value as CipherSuite);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001CC4C File Offset: 0x0001AE4C
		void IList.Insert(int index, object value)
		{
			this.cipherSuites.Insert(index, value as CipherSuite);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001CC60 File Offset: 0x0001AE60
		void IList.Remove(object value)
		{
			this.cipherSuites.Remove(value as CipherSuite);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001CC74 File Offset: 0x0001AE74
		void IList.RemoveAt(int index)
		{
			this.cipherSuites.RemoveAt(index);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001CC84 File Offset: 0x0001AE84
		int IList.Add(object value)
		{
			return this.cipherSuites.Add(value as CipherSuite);
		}

		// Token: 0x17000120 RID: 288
		public CipherSuite this[string name]
		{
			get
			{
				return (CipherSuite)this.cipherSuites[this.IndexOf(name)];
			}
			set
			{
				this.cipherSuites[this.IndexOf(name)] = value;
			}
		}

		// Token: 0x17000121 RID: 289
		public CipherSuite this[int index]
		{
			get
			{
				return (CipherSuite)this.cipherSuites[index];
			}
			set
			{
				this.cipherSuites[index] = value;
			}
		}

		// Token: 0x17000122 RID: 290
		public CipherSuite this[short code]
		{
			get
			{
				return (CipherSuite)this.cipherSuites[this.IndexOf(code)];
			}
			set
			{
				this.cipherSuites[this.IndexOf(code)] = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0001CD24 File Offset: 0x0001AF24
		public int Count
		{
			get
			{
				return this.cipherSuites.Count;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0001CD34 File Offset: 0x0001AF34
		public bool IsFixedSize
		{
			get
			{
				return this.cipherSuites.IsFixedSize;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0001CD44 File Offset: 0x0001AF44
		public bool IsReadOnly
		{
			get
			{
				return this.cipherSuites.IsReadOnly;
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001CD54 File Offset: 0x0001AF54
		public void CopyTo(Array array, int index)
		{
			this.cipherSuites.CopyTo(array, index);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001CD64 File Offset: 0x0001AF64
		public void Clear()
		{
			this.cipherSuites.Clear();
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001CD74 File Offset: 0x0001AF74
		public int IndexOf(string name)
		{
			int num = 0;
			foreach (object obj in this.cipherSuites)
			{
				CipherSuite cipherSuite = (CipherSuite)obj;
				if (this.cultureAwareCompare(cipherSuite.Name, name))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001CE04 File Offset: 0x0001B004
		public int IndexOf(short code)
		{
			int num = 0;
			foreach (object obj in this.cipherSuites)
			{
				CipherSuite cipherSuite = (CipherSuite)obj;
				if (cipherSuite.Code == code)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001CE8C File Offset: 0x0001B08C
		public CipherSuite Add(short code, string name, CipherAlgorithmType cipherType, HashAlgorithmType hashType, ExchangeAlgorithmType exchangeType, bool exportable, bool blockMode, byte keyMaterialSize, byte expandedKeyMaterialSize, short effectiveKeyBytes, byte ivSize, byte blockSize)
		{
			SecurityProtocolType securityProtocolType = this.protocol;
			if (securityProtocolType != SecurityProtocolType.Default)
			{
				if (securityProtocolType != SecurityProtocolType.Ssl2)
				{
					if (securityProtocolType == SecurityProtocolType.Ssl3)
					{
						return this.add(new SslCipherSuite(code, name, cipherType, hashType, exchangeType, exportable, blockMode, keyMaterialSize, expandedKeyMaterialSize, effectiveKeyBytes, ivSize, blockSize));
					}
					if (securityProtocolType == SecurityProtocolType.Tls)
					{
						goto IL_32;
					}
				}
				throw new NotSupportedException("Unsupported security protocol type.");
			}
			IL_32:
			return this.add(new TlsCipherSuite(code, name, cipherType, hashType, exchangeType, exportable, blockMode, keyMaterialSize, expandedKeyMaterialSize, effectiveKeyBytes, ivSize, blockSize));
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001CF18 File Offset: 0x0001B118
		private TlsCipherSuite add(TlsCipherSuite cipherSuite)
		{
			this.cipherSuites.Add(cipherSuite);
			return cipherSuite;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001CF28 File Offset: 0x0001B128
		private SslCipherSuite add(SslCipherSuite cipherSuite)
		{
			this.cipherSuites.Add(cipherSuite);
			return cipherSuite;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001CF38 File Offset: 0x0001B138
		private bool cultureAwareCompare(string strA, string strB)
		{
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) == 0;
		}

		// Token: 0x04000251 RID: 593
		private ArrayList cipherSuites;

		// Token: 0x04000252 RID: 594
		private SecurityProtocolType protocol;
	}
}
