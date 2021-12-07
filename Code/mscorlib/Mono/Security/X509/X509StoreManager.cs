using System;
using System.Collections;
using System.IO;

namespace Mono.Security.X509
{
	// Token: 0x020000D4 RID: 212
	internal sealed class X509StoreManager
	{
		// Token: 0x06000BD6 RID: 3030 RVA: 0x00036898 File Offset: 0x00034A98
		private X509StoreManager()
		{
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x000368A0 File Offset: 0x00034AA0
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

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x000368E8 File Offset: 0x00034AE8
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

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x00036930 File Offset: 0x00034B30
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

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x00036970 File Offset: 0x00034B70
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

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x000369B0 File Offset: 0x00034BB0
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

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x000369F0 File Offset: 0x00034BF0
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

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x00036A30 File Offset: 0x00034C30
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

		// Token: 0x04000332 RID: 818
		private static X509Stores _userStore;

		// Token: 0x04000333 RID: 819
		private static X509Stores _machineStore;
	}
}
