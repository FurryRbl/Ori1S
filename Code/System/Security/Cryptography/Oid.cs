using System;

namespace System.Security.Cryptography
{
	/// <summary>Represents a cryptographic object identifier. This class cannot be inherited.</summary>
	// Token: 0x02000435 RID: 1077
	public sealed class Oid
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Oid" /> class.</summary>
		// Token: 0x060026BE RID: 9918 RVA: 0x0007807C File Offset: 0x0007627C
		public Oid()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Oid" /> class using a string value of an <see cref="T:System.Security.Cryptography.Oid" /> object.</summary>
		/// <param name="oid">An object identifier.</param>
		// Token: 0x060026BF RID: 9919 RVA: 0x00078084 File Offset: 0x00076284
		public Oid(string oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			this._value = oid;
			this._name = this.GetName(oid);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Oid" /> class using the specified value and friendly name.</summary>
		/// <param name="value">The dotted number of the identifier.</param>
		/// <param name="friendlyName">The friendly name of the identifier.</param>
		// Token: 0x060026C0 RID: 9920 RVA: 0x000780B4 File Offset: 0x000762B4
		public Oid(string value, string friendlyName)
		{
			this._value = value;
			this._name = friendlyName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Oid" /> class using the specified <see cref="T:System.Security.Cryptography.Oid" /> object.</summary>
		/// <param name="oid">The object identifier information to use to create the new object identifier.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oid " />is null.</exception>
		// Token: 0x060026C1 RID: 9921 RVA: 0x000780CC File Offset: 0x000762CC
		public Oid(Oid oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			this._value = oid.Value;
			this._name = oid.FriendlyName;
		}

		/// <summary>Gets or sets the friendly name of the identifier.</summary>
		/// <returns>The friendly name of the identifier.</returns>
		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x00078100 File Offset: 0x00076300
		// (set) Token: 0x060026C3 RID: 9923 RVA: 0x00078108 File Offset: 0x00076308
		public string FriendlyName
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
				this._value = this.GetValue(this._name);
			}
		}

		/// <summary>Gets or sets the dotted number of the identifier.</summary>
		/// <returns>The dotted number of the identifier.</returns>
		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x00078124 File Offset: 0x00076324
		// (set) Token: 0x060026C5 RID: 9925 RVA: 0x0007812C File Offset: 0x0007632C
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
				this._name = this.GetName(this._value);
			}
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x00078148 File Offset: 0x00076348
		private string GetName(string oid)
		{
			switch (oid)
			{
			case "1.2.840.113549.1.1.1":
				return "RSA";
			case "1.2.840.113549.1.7.1":
				return "PKCS 7 Data";
			case "1.2.840.113549.1.9.3":
				return "Content Type";
			case "1.2.840.113549.1.9.4":
				return "Message Digest";
			case "1.2.840.113549.1.9.5":
				return "Signing Time";
			case "1.2.840.113549.3.7":
				return "3des";
			case "2.5.29.19":
				return "Basic Constraints";
			case "2.5.29.15":
				return "Key Usage";
			case "2.5.29.37":
				return "Enhanced Key Usage";
			case "2.5.29.14":
				return "Subject Key Identifier";
			case "2.5.29.17":
				return "Subject Alternative Name";
			case "2.16.840.1.113730.1.1":
				return "Netscape Cert Type";
			case "1.2.840.113549.2.5":
				return "md5";
			case "1.3.14.3.2.26":
				return "sha1";
			}
			return this._name;
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x000782D4 File Offset: 0x000764D4
		private string GetValue(string name)
		{
			switch (name)
			{
			case "RSA":
				return "1.2.840.113549.1.1.1";
			case "PKCS 7 Data":
				return "1.2.840.113549.1.7.1";
			case "Content Type":
				return "1.2.840.113549.1.9.3";
			case "Message Digest":
				return "1.2.840.113549.1.9.4";
			case "Signing Time":
				return "1.2.840.113549.1.9.5";
			case "3des":
				return "1.2.840.113549.3.7";
			case "Basic Constraints":
				return "2.5.29.19";
			case "Key Usage":
				return "2.5.29.15";
			case "Enhanced Key Usage":
				return "2.5.29.37";
			case "Subject Key Identifier":
				return "2.5.29.14";
			case "Subject Alternative Name":
				return "2.5.29.17";
			case "Netscape Cert Type":
				return "2.16.840.1.113730.1.1";
			case "md5":
				return "1.2.840.113549.2.5";
			case "sha1":
				return "1.3.14.3.2.26";
			}
			return this._value;
		}

		// Token: 0x040017C7 RID: 6087
		internal const string oidRSA = "1.2.840.113549.1.1.1";

		// Token: 0x040017C8 RID: 6088
		internal const string nameRSA = "RSA";

		// Token: 0x040017C9 RID: 6089
		internal const string oidPkcs7Data = "1.2.840.113549.1.7.1";

		// Token: 0x040017CA RID: 6090
		internal const string namePkcs7Data = "PKCS 7 Data";

		// Token: 0x040017CB RID: 6091
		internal const string oidPkcs9ContentType = "1.2.840.113549.1.9.3";

		// Token: 0x040017CC RID: 6092
		internal const string namePkcs9ContentType = "Content Type";

		// Token: 0x040017CD RID: 6093
		internal const string oidPkcs9MessageDigest = "1.2.840.113549.1.9.4";

		// Token: 0x040017CE RID: 6094
		internal const string namePkcs9MessageDigest = "Message Digest";

		// Token: 0x040017CF RID: 6095
		internal const string oidPkcs9SigningTime = "1.2.840.113549.1.9.5";

		// Token: 0x040017D0 RID: 6096
		internal const string namePkcs9SigningTime = "Signing Time";

		// Token: 0x040017D1 RID: 6097
		internal const string oidMd5 = "1.2.840.113549.2.5";

		// Token: 0x040017D2 RID: 6098
		internal const string nameMd5 = "md5";

		// Token: 0x040017D3 RID: 6099
		internal const string oid3Des = "1.2.840.113549.3.7";

		// Token: 0x040017D4 RID: 6100
		internal const string name3Des = "3des";

		// Token: 0x040017D5 RID: 6101
		internal const string oidSha1 = "1.3.14.3.2.26";

		// Token: 0x040017D6 RID: 6102
		internal const string nameSha1 = "sha1";

		// Token: 0x040017D7 RID: 6103
		internal const string oidSubjectAltName = "2.5.29.17";

		// Token: 0x040017D8 RID: 6104
		internal const string nameSubjectAltName = "Subject Alternative Name";

		// Token: 0x040017D9 RID: 6105
		internal const string oidNetscapeCertType = "2.16.840.1.113730.1.1";

		// Token: 0x040017DA RID: 6106
		internal const string nameNetscapeCertType = "Netscape Cert Type";

		// Token: 0x040017DB RID: 6107
		private string _value;

		// Token: 0x040017DC RID: 6108
		private string _name;
	}
}
