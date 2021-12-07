using System;
using System.Collections;

namespace Mono.Security.X509
{
	// Token: 0x020000D0 RID: 208
	internal sealed class X509ExtensionCollection : CollectionBase, IEnumerable
	{
		// Token: 0x06000BA9 RID: 2985 RVA: 0x00035CD4 File Offset: 0x00033ED4
		public X509ExtensionCollection()
		{
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00035CDC File Offset: 0x00033EDC
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

		// Token: 0x06000BAB RID: 2987 RVA: 0x00035D48 File Offset: 0x00033F48
		IEnumerator IEnumerable.GetEnumerator()
		{
			return base.InnerList.GetEnumerator();
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00035D58 File Offset: 0x00033F58
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

		// Token: 0x06000BAD RID: 2989 RVA: 0x00035D90 File Offset: 0x00033F90
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

		// Token: 0x06000BAE RID: 2990 RVA: 0x00035DE8 File Offset: 0x00033FE8
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

		// Token: 0x06000BAF RID: 2991 RVA: 0x00035E4C File Offset: 0x0003404C
		public bool Contains(X509Extension extension)
		{
			return this.IndexOf(extension) != -1;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00035E5C File Offset: 0x0003405C
		public bool Contains(string oid)
		{
			return this.IndexOf(oid) != -1;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00035E6C File Offset: 0x0003406C
		public void CopyTo(X509Extension[] extensions, int index)
		{
			if (extensions == null)
			{
				throw new ArgumentNullException("extensions");
			}
			base.InnerList.CopyTo(extensions, index);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00035E8C File Offset: 0x0003408C
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

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00035EE8 File Offset: 0x000340E8
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

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00035F48 File Offset: 0x00034148
		public void Insert(int index, X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			base.InnerList.Insert(index, extension);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00035F68 File Offset: 0x00034168
		public void Remove(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			base.InnerList.Remove(extension);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00035F88 File Offset: 0x00034188
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

		// Token: 0x170001A0 RID: 416
		public X509Extension this[int index]
		{
			get
			{
				return (X509Extension)base.InnerList[index];
			}
		}

		// Token: 0x170001A1 RID: 417
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

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00036008 File Offset: 0x00034208
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

		// Token: 0x04000321 RID: 801
		private bool readOnly;
	}
}
