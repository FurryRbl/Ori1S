using System;
using System.Security.Cryptography;
using System.Text;

namespace System.Net
{
	// Token: 0x020002FA RID: 762
	internal class DigestSession
	{
		// Token: 0x06001A11 RID: 6673 RVA: 0x00048484 File Offset: 0x00046684
		public DigestSession()
		{
			this._nc = 1;
			this.lastUse = DateTime.Now;
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x000484AC File Offset: 0x000466AC
		public string Algorithm
		{
			get
			{
				return this.parser.Algorithm;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001A14 RID: 6676 RVA: 0x000484BC File Offset: 0x000466BC
		public string Realm
		{
			get
			{
				return this.parser.Realm;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001A15 RID: 6677 RVA: 0x000484CC File Offset: 0x000466CC
		public string Nonce
		{
			get
			{
				return this.parser.Nonce;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001A16 RID: 6678 RVA: 0x000484DC File Offset: 0x000466DC
		public string Opaque
		{
			get
			{
				return this.parser.Opaque;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001A17 RID: 6679 RVA: 0x000484EC File Offset: 0x000466EC
		public string QOP
		{
			get
			{
				return this.parser.QOP;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x000484FC File Offset: 0x000466FC
		public string CNonce
		{
			get
			{
				if (this._cnonce == null)
				{
					byte[] array = new byte[15];
					DigestSession.rng.GetBytes(array);
					this._cnonce = Convert.ToBase64String(array);
					Array.Clear(array, 0, array.Length);
				}
				return this._cnonce;
			}
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x00048544 File Offset: 0x00046744
		public bool Parse(string challenge)
		{
			this.parser = new DigestHeaderParser(challenge);
			if (!this.parser.Parse())
			{
				return false;
			}
			if (this.parser.Algorithm == null || this.parser.Algorithm.ToUpper().StartsWith("MD5"))
			{
				this.hash = HashAlgorithm.Create("MD5");
			}
			return true;
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x000485B0 File Offset: 0x000467B0
		private string HashToHexString(string toBeHashed)
		{
			if (this.hash == null)
			{
				return null;
			}
			this.hash.Initialize();
			byte[] array = this.hash.ComputeHash(Encoding.ASCII.GetBytes(toBeHashed));
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x0004862C File Offset: 0x0004682C
		private string HA1(string username, string password)
		{
			string toBeHashed = string.Format("{0}:{1}:{2}", username, this.Realm, password);
			if (this.Algorithm != null && this.Algorithm.ToLower() == "md5-sess")
			{
				toBeHashed = string.Format("{0}:{1}:{2}", this.HashToHexString(toBeHashed), this.Nonce, this.CNonce);
			}
			return this.HashToHexString(toBeHashed);
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x00048698 File Offset: 0x00046898
		private string HA2(HttpWebRequest webRequest)
		{
			string toBeHashed = string.Format("{0}:{1}", webRequest.Method, webRequest.RequestUri.PathAndQuery);
			if (this.QOP == "auth-int")
			{
			}
			return this.HashToHexString(toBeHashed);
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x000486E0 File Offset: 0x000468E0
		private string Response(string username, string password, HttpWebRequest webRequest)
		{
			string text = string.Format("{0}:{1}:", this.HA1(username, password), this.Nonce);
			if (this.QOP != null)
			{
				text += string.Format("{0}:{1}:{2}:", this._nc.ToString("X8"), this.CNonce, this.QOP);
			}
			text += this.HA2(webRequest);
			return this.HashToHexString(text);
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x00048754 File Offset: 0x00046954
		public Authorization Authenticate(WebRequest webRequest, ICredentials credentials)
		{
			if (this.parser == null)
			{
				throw new InvalidOperationException();
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			this.lastUse = DateTime.Now;
			NetworkCredential credential = credentials.GetCredential(httpWebRequest.RequestUri, "digest");
			if (credential == null)
			{
				return null;
			}
			string userName = credential.UserName;
			if (userName == null || userName == string.Empty)
			{
				return null;
			}
			string password = credential.Password;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Digest username=\"{0}\", ", userName);
			stringBuilder.AppendFormat("realm=\"{0}\", ", this.Realm);
			stringBuilder.AppendFormat("nonce=\"{0}\", ", this.Nonce);
			stringBuilder.AppendFormat("uri=\"{0}\", ", httpWebRequest.Address.PathAndQuery);
			if (this.Algorithm != null)
			{
				stringBuilder.AppendFormat("algorithm=\"{0}\", ", this.Algorithm);
			}
			stringBuilder.AppendFormat("response=\"{0}\", ", this.Response(userName, password, httpWebRequest));
			if (this.QOP != null)
			{
				stringBuilder.AppendFormat("qop=\"{0}\", ", this.QOP);
			}
			lock (this)
			{
				if (this.QOP != null)
				{
					stringBuilder.AppendFormat("nc={0:X8}, ", this._nc);
					this._nc++;
				}
			}
			if (this.CNonce != null)
			{
				stringBuilder.AppendFormat("cnonce=\"{0}\", ", this.CNonce);
			}
			if (this.Opaque != null)
			{
				stringBuilder.AppendFormat("opaque=\"{0}\", ", this.Opaque);
			}
			stringBuilder.Length -= 2;
			return new Authorization(stringBuilder.ToString());
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x0004892C File Offset: 0x00046B2C
		public DateTime LastUse
		{
			get
			{
				return this.lastUse;
			}
		}

		// Token: 0x0400103F RID: 4159
		private static RandomNumberGenerator rng = RandomNumberGenerator.Create();

		// Token: 0x04001040 RID: 4160
		private DateTime lastUse;

		// Token: 0x04001041 RID: 4161
		private int _nc;

		// Token: 0x04001042 RID: 4162
		private HashAlgorithm hash;

		// Token: 0x04001043 RID: 4163
		private DigestHeaderParser parser;

		// Token: 0x04001044 RID: 4164
		private string _cnonce;
	}
}
