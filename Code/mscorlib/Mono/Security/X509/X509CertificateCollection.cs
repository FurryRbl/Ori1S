using System;
using System.Collections;

namespace Mono.Security.X509
{
	// Token: 0x020000C9 RID: 201
	[Serializable]
	internal class X509CertificateCollection : CollectionBase, IEnumerable
	{
		// Token: 0x06000B50 RID: 2896 RVA: 0x000346B0 File Offset: 0x000328B0
		public X509CertificateCollection()
		{
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x000346B8 File Offset: 0x000328B8
		public X509CertificateCollection(X509Certificate[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x000346C8 File Offset: 0x000328C8
		public X509CertificateCollection(X509CertificateCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x000346D8 File Offset: 0x000328D8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return base.InnerList.GetEnumerator();
		}

		// Token: 0x17000184 RID: 388
		public X509Certificate this[int index]
		{
			get
			{
				return (X509Certificate)base.InnerList[index];
			}
			set
			{
				base.InnerList[index] = value;
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0003470C File Offset: 0x0003290C
		public int Add(X509Certificate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return base.InnerList.Add(value);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0003472C File Offset: 0x0003292C
		public void AddRange(X509Certificate[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				base.InnerList.Add(value[i]);
			}
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00034770 File Offset: 0x00032970
		public void AddRange(X509CertificateCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.InnerList.Count; i++)
			{
				base.InnerList.Add(value[i]);
			}
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x000347C0 File Offset: 0x000329C0
		public bool Contains(X509Certificate value)
		{
			return this.IndexOf(value) != -1;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x000347D0 File Offset: 0x000329D0
		public void CopyTo(X509Certificate[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x000347E0 File Offset: 0x000329E0
		public new X509CertificateCollection.X509CertificateEnumerator GetEnumerator()
		{
			return new X509CertificateCollection.X509CertificateEnumerator(this);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x000347E8 File Offset: 0x000329E8
		public override int GetHashCode()
		{
			return base.InnerList.GetHashCode();
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x000347F8 File Offset: 0x000329F8
		public int IndexOf(X509Certificate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			byte[] hash = value.Hash;
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				X509Certificate x509Certificate = (X509Certificate)base.InnerList[i];
				if (this.Compare(x509Certificate.Hash, hash))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00034860 File Offset: 0x00032A60
		public void Insert(int index, X509Certificate value)
		{
			base.InnerList.Insert(index, value);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00034870 File Offset: 0x00032A70
		public void Remove(X509Certificate value)
		{
			base.InnerList.Remove(value);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00034880 File Offset: 0x00032A80
		private bool Compare(byte[] array1, byte[] array2)
		{
			if (array1 == null && array2 == null)
			{
				return true;
			}
			if (array1 == null || array2 == null)
			{
				return false;
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (array1[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x020000CA RID: 202
		public class X509CertificateEnumerator : IEnumerator
		{
			// Token: 0x06000B61 RID: 2913 RVA: 0x000348D8 File Offset: 0x00032AD8
			public X509CertificateEnumerator(X509CertificateCollection mappings)
			{
				this.enumerator = ((IEnumerable)mappings).GetEnumerator();
			}

			// Token: 0x17000185 RID: 389
			// (get) Token: 0x06000B62 RID: 2914 RVA: 0x000348EC File Offset: 0x00032AEC
			object IEnumerator.Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x06000B63 RID: 2915 RVA: 0x000348FC File Offset: 0x00032AFC
			bool IEnumerator.MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x06000B64 RID: 2916 RVA: 0x0003490C File Offset: 0x00032B0C
			void IEnumerator.Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x17000186 RID: 390
			// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0003491C File Offset: 0x00032B1C
			public X509Certificate Current
			{
				get
				{
					return (X509Certificate)this.enumerator.Current;
				}
			}

			// Token: 0x06000B66 RID: 2918 RVA: 0x00034930 File Offset: 0x00032B30
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x06000B67 RID: 2919 RVA: 0x00034940 File Offset: 0x00032B40
			public void Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x04000301 RID: 769
			private IEnumerator enumerator;
		}
	}
}
