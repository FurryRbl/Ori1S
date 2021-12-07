using System;
using System.Collections;

namespace Mono.Security.X509
{
	// Token: 0x0200004C RID: 76
	public sealed class X509ExtensionCollection : CollectionBase, IEnumerable
	{
		// Token: 0x06000373 RID: 883 RVA: 0x00017B38 File Offset: 0x00015D38
		public X509ExtensionCollection()
		{
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00017B40 File Offset: 0x00015D40
		public X509ExtensionCollection(ASN1 asn1) : this()
		{
			this.readOnly = true;
			if (asn1 == null)
			{
				return;
			}
			if (asn1.Tag != 48)
			{
				throw new Exception("Invalid extensions format");
			}
			for (int i = 0; i < asn1.Count; i++)
			{
				X509Extension value = new X509Extension(asn1[i]);
				base.InnerList.Add(value);
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00017BAC File Offset: 0x00015DAC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return base.InnerList.GetEnumerator();
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00017BBC File Offset: 0x00015DBC
		public int Add(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			if (this.readOnly)
			{
				throw new NotSupportedException("Extensions are read only");
			}
			return base.InnerList.Add(extension);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00017BF4 File Offset: 0x00015DF4
		public void AddRange(X509Extension[] extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			if (this.readOnly)
			{
				throw new NotSupportedException("Extensions are read only");
			}
			for (int i = 0; i < extension.Length; i++)
			{
				base.InnerList.Add(extension[i]);
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00017C4C File Offset: 0x00015E4C
		public void AddRange(X509ExtensionCollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (this.readOnly)
			{
				throw new NotSupportedException("Extensions are read only");
			}
			for (int i = 0; i < collection.InnerList.Count; i++)
			{
				base.InnerList.Add(collection[i]);
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00017CB0 File Offset: 0x00015EB0
		public bool Contains(X509Extension extension)
		{
			return this.IndexOf(extension) != -1;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00017CC0 File Offset: 0x00015EC0
		public bool Contains(string oid)
		{
			return this.IndexOf(oid) != -1;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00017CD0 File Offset: 0x00015ED0
		public void CopyTo(X509Extension[] extensions, int index)
		{
			if (extensions == null)
			{
				throw new ArgumentNullException("extensions");
			}
			base.InnerList.CopyTo(extensions, index);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00017CF0 File Offset: 0x00015EF0
		public int IndexOf(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				X509Extension x509Extension = (X509Extension)base.InnerList[i];
				if (x509Extension.Equals(extension))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00017D4C File Offset: 0x00015F4C
		public int IndexOf(string oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				X509Extension x509Extension = (X509Extension)base.InnerList[i];
				if (x509Extension.Oid == oid)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00017DAC File Offset: 0x00015FAC
		public void Insert(int index, X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			base.InnerList.Insert(index, extension);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00017DCC File Offset: 0x00015FCC
		public void Remove(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			base.InnerList.Remove(extension);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00017DEC File Offset: 0x00015FEC
		public void Remove(string oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			int num = this.IndexOf(oid);
			if (num != -1)
			{
				base.InnerList.RemoveAt(num);
			}
		}

		// Token: 0x170000C4 RID: 196
		public X509Extension this[int index]
		{
			get
			{
				return (X509Extension)base.InnerList[index];
			}
		}

		// Token: 0x170000C5 RID: 197
		public X509Extension this[string oid]
		{
			get
			{
				int num = this.IndexOf(oid);
				if (num == -1)
				{
					return null;
				}
				return (X509Extension)base.InnerList[num];
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00017E6C File Offset: 0x0001606C
		public byte[] GetBytes()
		{
			if (base.InnerList.Count < 1)
			{
				return null;
			}
			ASN1 asn = new ASN1(48);
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				X509Extension x509Extension = (X509Extension)base.InnerList[i];
				asn.Add(x509Extension.ASN1);
			}
			return asn.GetBytes();
		}

		// Token: 0x040001A2 RID: 418
		private bool readOnly;
	}
}
