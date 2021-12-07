using System;
using System.Collections;

namespace Mono.Security.X509
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	public class X509CertificateCollection : CollectionBase, IEnumerable
	{
		// Token: 0x06000302 RID: 770 RVA: 0x00016114 File Offset: 0x00014314
		public X509CertificateCollection()
		{
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0001611C File Offset: 0x0001431C
		public X509CertificateCollection(X509Certificate[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0001612C File Offset: 0x0001432C
		public X509CertificateCollection(X509CertificateCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0001613C File Offset: 0x0001433C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return base.InnerList.GetEnumerator();
		}

		// Token: 0x1700009E RID: 158
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

		// Token: 0x06000308 RID: 776 RVA: 0x00016170 File Offset: 0x00014370
		public int Add(X509Certificate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return base.InnerList.Add(value);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00016190 File Offset: 0x00014390
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

		// Token: 0x0600030A RID: 778 RVA: 0x000161D4 File Offset: 0x000143D4
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

		// Token: 0x0600030B RID: 779 RVA: 0x00016224 File Offset: 0x00014424
		public bool Contains(X509Certificate value)
		{
			return this.IndexOf(value) != -1;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00016234 File Offset: 0x00014434
		public void CopyTo(X509Certificate[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00016244 File Offset: 0x00014444
		public new X509CertificateCollection.X509CertificateEnumerator GetEnumerator()
		{
			return new X509CertificateCollection.X509CertificateEnumerator(this);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0001624C File Offset: 0x0001444C
		public override int GetHashCode()
		{
			return base.InnerList.GetHashCode();
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0001625C File Offset: 0x0001445C
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

		// Token: 0x06000310 RID: 784 RVA: 0x000162C4 File Offset: 0x000144C4
		public void Insert(int index, X509Certificate value)
		{
			base.InnerList.Insert(index, value);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000162D4 File Offset: 0x000144D4
		public void Remove(X509Certificate value)
		{
			base.InnerList.Remove(value);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000162E4 File Offset: 0x000144E4
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

		// Token: 0x02000045 RID: 69
		public class X509CertificateEnumerator : IEnumerator
		{
			// Token: 0x06000313 RID: 787 RVA: 0x0001633C File Offset: 0x0001453C
			public X509CertificateEnumerator(X509CertificateCollection mappings)
			{
				this.enumerator = ((IEnumerable)mappings).GetEnumerator();
			}

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x06000314 RID: 788 RVA: 0x00016350 File Offset: 0x00014550
			object IEnumerator.Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x06000315 RID: 789 RVA: 0x00016360 File Offset: 0x00014560
			bool IEnumerator.MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x06000316 RID: 790 RVA: 0x00016370 File Offset: 0x00014570
			void IEnumerator.Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x06000317 RID: 791 RVA: 0x00016380 File Offset: 0x00014580
			public X509Certificate Current
			{
				get
				{
					return (X509Certificate)this.enumerator.Current;
				}
			}

			// Token: 0x06000318 RID: 792 RVA: 0x00016394 File Offset: 0x00014594
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x06000319 RID: 793 RVA: 0x000163A4 File Offset: 0x000145A4
			public void Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x04000178 RID: 376
			private IEnumerator enumerator;
		}
	}
}
