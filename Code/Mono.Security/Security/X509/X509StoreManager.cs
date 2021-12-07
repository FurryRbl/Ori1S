using System;
using System.Collections;
using System.IO;

namespace Mono.Security.X509
{
	// Token: 0x0200004E RID: 78
	public sealed class X509StoreManager
	{
		// Token: 0x06000397 RID: 919 RVA: 0x000184D0 File Offset: 0x000166D0
		private X509StoreManager()
		{
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000398 RID: 920 RVA: 0x000184D8 File Offset: 0x000166D8
		public static X509Stores CurrentUser
		{
			get
			{
				if (X509StoreManager._userStore == null)
				{
					string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mono");
					text = Path.Combine(text, "certs");
					X509StoreManager._userStore = new X509Stores(text);
				}
				return X509StoreManager._userStore;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00018520 File Offset: 0x00016720
		public static X509Stores LocalMachine
		{
			get
			{
				if (X509StoreManager._machineStore == null)
				{
					string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ".mono");
					text = Path.Combine(text, "certs");
					X509StoreManager._machineStore = new X509Stores(text);
				}
				return X509StoreManager._machineStore;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00018568 File Offset: 0x00016768
		public static X509CertificateCollection IntermediateCACertificates
		{
			get
			{
				X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
				x509CertificateCollection.AddRange(X509StoreManager.CurrentUser.IntermediateCA.Certificates);
				x509CertificateCollection.AddRange(X509StoreManager.LocalMachine.IntermediateCA.Certificates);
				return x509CertificateCollection;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600039B RID: 923 RVA: 0x000185A8 File Offset: 0x000167A8
		public static ArrayList IntermediateCACrls
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				arrayList.AddRange(X509StoreManager.CurrentUser.IntermediateCA.Crls);
				arrayList.AddRange(X509StoreManager.LocalMachine.IntermediateCA.Crls);
				return arrayList;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600039C RID: 924 RVA: 0x000185E8 File Offset: 0x000167E8
		public static X509CertificateCollection TrustedRootCertificates
		{
			get
			{
				X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
				x509CertificateCollection.AddRange(X509StoreManager.CurrentUser.TrustedRoot.Certificates);
				x509CertificateCollection.AddRange(X509StoreManager.LocalMachine.TrustedRoot.Certificates);
				return x509CertificateCollection;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00018628 File Offset: 0x00016828
		public static ArrayList TrustedRootCACrls
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				arrayList.AddRange(X509StoreManager.CurrentUser.TrustedRoot.Crls);
				arrayList.AddRange(X509StoreManager.LocalMachine.TrustedRoot.Crls);
				return arrayList;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00018668 File Offset: 0x00016868
		public static X509CertificateCollection UntrustedCertificates
		{
			get
			{
				X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
				x509CertificateCollection.AddRange(X509StoreManager.CurrentUser.Untrusted.Certificates);
				x509CertificateCollection.AddRange(X509StoreManager.LocalMachine.Untrusted.Certificates);
				return x509CertificateCollection;
			}
		}

		// Token: 0x040001A8 RID: 424
		private static X509Stores _userStore;

		// Token: 0x040001A9 RID: 425
		private static X509Stores _machineStore;
	}
}
