using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	/// <summary>Contains information used to uniquely identify a manifest-based application. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FE RID: 254
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationId
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationId" /> class.</summary>
		/// <param name="publicKeyToken">The array of bytes representing the raw public key data. </param>
		/// <param name="name">The name of the application. </param>
		/// <param name="version">A <see cref="T:System.Version" /> object that specifies the version of the application. </param>
		/// <param name="processorArchitecture">The processor architecture of the application. </param>
		/// <param name="culture">The culture of the application. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name " />is null.-or-<paramref name="version " />is null.-or-<paramref name="publicKeyToken " />is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name " />is an empty string.</exception>
		// Token: 0x06000D5C RID: 3420 RVA: 0x0003A8C0 File Offset: 0x00038AC0
		public ApplicationId(byte[] publicKeyToken, string name, Version version, string processorArchitecture, string culture)
		{
			if (publicKeyToken == null)
			{
				throw new ArgumentNullException("publicKeyToken");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this._token = (byte[])publicKeyToken.Clone();
			this._name = name;
			this._version = version;
			this._proc = processorArchitecture;
			this._culture = culture;
		}

		/// <summary>Gets a string representing the culture information for the application.</summary>
		/// <returns>The culture information for the application.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0003A93C File Offset: 0x00038B3C
		public string Culture
		{
			get
			{
				return this._culture;
			}
		}

		/// <summary>Gets the name of the application.</summary>
		/// <returns>The name of the application.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0003A944 File Offset: 0x00038B44
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Gets the target processor architecture for the application.</summary>
		/// <returns>The processor architecture of the application.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x0003A94C File Offset: 0x00038B4C
		public string ProcessorArchitecture
		{
			get
			{
				return this._proc;
			}
		}

		/// <summary>Gets the public key token for the application.</summary>
		/// <returns>A byte array containing the public key token for the application.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0003A954 File Offset: 0x00038B54
		public byte[] PublicKeyToken
		{
			get
			{
				return (byte[])this._token.Clone();
			}
		}

		/// <summary>Gets the version of the application.</summary>
		/// <returns>A <see cref="T:System.Version" /> that specifies the version of the application.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x0003A968 File Offset: 0x00038B68
		public Version Version
		{
			get
			{
				return this._version;
			}
		}

		/// <summary>Creates and returns an identical copy of the current application identity.</summary>
		/// <returns>An <see cref="T:System.ApplicationId" /> object that represents an exact copy of the original.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D62 RID: 3426 RVA: 0x0003A970 File Offset: 0x00038B70
		public ApplicationId Copy()
		{
			return new ApplicationId(this._token, this._name, this._version, this._proc, this._culture);
		}

		/// <summary>Determines whether the specified <see cref="T:System.ApplicationId" /> object is equivalent to the current <see cref="T:System.ApplicationId" />.</summary>
		/// <returns>true if the specified <see cref="T:System.ApplicationId" /> object is equivalent to the current <see cref="T:System.ApplicationId" />; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.ApplicationId" /> object to compare to the current <see cref="T:System.ApplicationId" />. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D63 RID: 3427 RVA: 0x0003A998 File Offset: 0x00038B98
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			ApplicationId applicationId = o as ApplicationId;
			if (applicationId == null)
			{
				return false;
			}
			if (this._name != applicationId._name)
			{
				return false;
			}
			if (this._proc != applicationId._proc)
			{
				return false;
			}
			if (this._culture != applicationId._culture)
			{
				return false;
			}
			if (!this._version.Equals(applicationId._version))
			{
				return false;
			}
			if (this._token.Length != applicationId._token.Length)
			{
				return false;
			}
			for (int i = 0; i < this._token.Length; i++)
			{
				if (this._token[i] != applicationId._token[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets the hash code for the current application identity.</summary>
		/// <returns>The hash code for the current application identity.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D64 RID: 3428 RVA: 0x0003AA64 File Offset: 0x00038C64
		public override int GetHashCode()
		{
			int num = this._name.GetHashCode() ^ this._version.GetHashCode();
			for (int i = 0; i < this._token.Length; i++)
			{
				num ^= (int)this._token[i];
			}
			return num;
		}

		/// <summary>Creates and returns a string representation of the application identity.</summary>
		/// <returns>A string representation of the application identity.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D65 RID: 3429 RVA: 0x0003AAB0 File Offset: 0x00038CB0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this._name);
			if (this._culture != null)
			{
				stringBuilder.AppendFormat(", culture=\"{0}\"", this._culture);
			}
			stringBuilder.AppendFormat(", version=\"{0}\", publicKeyToken=\"", this._version);
			for (int i = 0; i < this._token.Length; i++)
			{
				stringBuilder.Append(this._token[i].ToString("X2"));
			}
			if (this._proc != null)
			{
				stringBuilder.AppendFormat("\", processorArchitecture =\"{0}\"", this._proc);
			}
			else
			{
				stringBuilder.Append("\"");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000397 RID: 919
		private byte[] _token;

		// Token: 0x04000398 RID: 920
		private string _name;

		// Token: 0x04000399 RID: 921
		private Version _version;

		// Token: 0x0400039A RID: 922
		private string _proc;

		// Token: 0x0400039B RID: 923
		private string _culture;
	}
}
