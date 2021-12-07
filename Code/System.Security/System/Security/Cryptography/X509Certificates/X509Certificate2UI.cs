using System;
using System.Security.Permissions;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Displays user interface dialogs that allow you to select and view X.509 certificates. This class cannot be inherited.</summary>
	// Token: 0x02000032 RID: 50
	public sealed class X509Certificate2UI
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00006238 File Offset: 0x00004438
		private X509Certificate2UI()
		{
		}

		/// <summary>Displays a dialog box that contains the properties of an X.509 certificate and its associated certificate chain.</summary>
		/// <param name="certificate">The X.509 certificate to display.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificate" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="certificate" /> parameter is invalid. </exception>
		// Token: 0x06000124 RID: 292 RVA: 0x00006240 File Offset: 0x00004440
		[MonoTODO]
		public static void DisplayCertificate(X509Certificate2 certificate)
		{
			X509Certificate2UI.DisplayCertificate(certificate, IntPtr.Zero);
		}

		/// <summary>Displays a dialog box that contains the properties of an X.509 certificate and its associated certificate chain using a handle to a parent window.</summary>
		/// <param name="certificate">The X.509 certificate to display.</param>
		/// <param name="hwndParent">A handle to the parent window to use for the display dialog.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificate" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="certificate" /> parameter is invalid. </exception>
		// Token: 0x06000125 RID: 293 RVA: 0x00006250 File Offset: 0x00004450
		[MonoTODO]
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.UIPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nWindow=\"SafeTopLevelWindows\"/>\n</PermissionSet>\n")]
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		public static void DisplayCertificate(X509Certificate2 certificate, IntPtr hwndParent)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			certificate.GetRawCertData();
			throw new NotImplementedException();
		}

		/// <summary>Displays a dialog box for selecting an X.509 certificate from a certificate collection.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object that contains the selected certificate or certificates.</returns>
		/// <param name="certificates">A collection of X.509 certificates to select from.</param>
		/// <param name="title">The title of the dialog box.</param>
		/// <param name="message">A descriptive message to guide the user.  The message is displayed in the dialog box.</param>
		/// <param name="selectionFlag">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SelectionFlag" /> values that specifies whether single or multiple selections are allowed. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="selectionFlag" /> parameter is not a valid flag. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificates" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="certificates" /> parameter is invalid. </exception>
		// Token: 0x06000126 RID: 294 RVA: 0x00006270 File Offset: 0x00004470
		[MonoTODO]
		public static X509Certificate2Collection SelectFromCollection(X509Certificate2Collection certificates, string title, string message, X509SelectionFlag selectionFlag)
		{
			return X509Certificate2UI.SelectFromCollection(certificates, title, message, selectionFlag, IntPtr.Zero);
		}

		/// <summary>Displays a dialog box for selecting an X.509 certificate from a certificate collection using a handle to a parent window.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object that contains the selected certificate or certificates.</returns>
		/// <param name="certificates">A collection of X.509 certificates to select from.</param>
		/// <param name="title">The title of the dialog box.</param>
		/// <param name="message">A descriptive message to guide the user.  The message is displayed in the dialog box.</param>
		/// <param name="selectionFlag">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SelectionFlag" /> values that specifies whether single or multiple selections are allowed. </param>
		/// <param name="hwndParent">A handle to the parent window to use for the display dialog box.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="selectionFlag" /> parameter is not a valid flag. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificates" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="certificates" /> parameter is invalid. </exception>
		// Token: 0x06000127 RID: 295 RVA: 0x00006280 File Offset: 0x00004480
		[MonoTODO]
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.UIPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nWindow=\"SafeTopLevelWindows\"/>\n</PermissionSet>\n")]
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		public static X509Certificate2Collection SelectFromCollection(X509Certificate2Collection certificates, string title, string message, X509SelectionFlag selectionFlag, IntPtr hwndParent)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			if (selectionFlag < X509SelectionFlag.SingleSelection || selectionFlag > X509SelectionFlag.MultiSelection)
			{
				throw new ArgumentException("selectionFlag");
			}
			throw new NotImplementedException();
		}
	}
}
