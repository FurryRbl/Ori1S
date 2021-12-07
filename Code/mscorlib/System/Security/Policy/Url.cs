using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Security;

namespace System.Security.Policy
{
	/// <summary>Provides the URL from which a code assembly originates as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x02000658 RID: 1624
	[ComVisible(true)]
	[Serializable]
	public sealed class Url : IBuiltInEvidence, IIdentityPermissionFactory
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Url" /> class with the URL from which a code assembly originates.</summary>
		/// <param name="name">The URL of origin for the associated code assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06003DD5 RID: 15829 RVA: 0x000D4F70 File Offset: 0x000D3170
		public Url(string name) : this(name, false)
		{
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x000D4F7C File Offset: 0x000D317C
		internal Url(string name, bool validated)
		{
			this.origin_url = ((!validated) ? this.Prepare(name) : name);
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x000D4FA0 File Offset: 0x000D31A0
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return ((!verbose) ? 1 : 3) + this.origin_url.Length;
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x000D4FBC File Offset: 0x000D31BC
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x000D4FC0 File Offset: 0x000D31C0
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		/// <summary>Creates a new copy of the evidence object.</summary>
		/// <returns>A new, identical copy of the evidence object.</returns>
		// Token: 0x06003DDA RID: 15834 RVA: 0x000D4FC4 File Offset: 0x000D31C4
		public object Copy()
		{
			return new Url(this.origin_url, true);
		}

		/// <summary>Creates an identity permission corresponding to the current instance of the <see cref="T:System.Security.Policy.Url" /> evidence class.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> for the specified <see cref="T:System.Security.Policy.Url" /> evidence.</returns>
		/// <param name="evidence">The evidence set from which to construct the identity permission. </param>
		// Token: 0x06003DDB RID: 15835 RVA: 0x000D4FD4 File Offset: 0x000D31D4
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new UrlIdentityPermission(this.origin_url);
		}

		/// <summary>Compares the current <see cref="T:System.Security.Policy.Url" /> evidence object to the specified object for equivalence.</summary>
		/// <returns>true if the two <see cref="T:System.Security.Policy.Url" /> objects are equal; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Security.Policy.Url" /> evidence object to test for equivalence with the current object. </param>
		// Token: 0x06003DDC RID: 15836 RVA: 0x000D4FE4 File Offset: 0x000D31E4
		public override bool Equals(object o)
		{
			Url url = o as Url;
			if (url == null)
			{
				return false;
			}
			string text = url.Value;
			string text2 = this.origin_url;
			if (text.IndexOf(Uri.SchemeDelimiter) < 0)
			{
				text = "file://" + text;
			}
			if (text2.IndexOf(Uri.SchemeDelimiter) < 0)
			{
				text2 = "file://" + text2;
			}
			return string.Compare(text, text2, true, CultureInfo.InvariantCulture) == 0;
		}

		/// <summary>Gets the hash code of the current URL.</summary>
		/// <returns>The hash code of the current URL.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003DDD RID: 15837 RVA: 0x000D5058 File Offset: 0x000D3258
		public override int GetHashCode()
		{
			string text = this.origin_url;
			if (text.IndexOf(Uri.SchemeDelimiter) < 0)
			{
				text = "file://" + text;
			}
			return text.GetHashCode();
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Url" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.Url" />.</returns>
		// Token: 0x06003DDE RID: 15838 RVA: 0x000D5090 File Offset: 0x000D3290
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Url");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("Url", this.origin_url));
			return securityElement.ToString();
		}

		/// <summary>Gets the URL from which the code assembly originates.</summary>
		/// <returns>The URL from which the code assembly originates.</returns>
		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06003DDF RID: 15839 RVA: 0x000D50D4 File Offset: 0x000D32D4
		public string Value
		{
			get
			{
				return this.origin_url;
			}
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x000D50DC File Offset: 0x000D32DC
		private string Prepare(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("Url");
			}
			if (url == string.Empty)
			{
				throw new FormatException(Locale.GetText("Invalid (empty) Url"));
			}
			int num = url.IndexOf(Uri.SchemeDelimiter);
			if (num > 0)
			{
				if (url.StartsWith("file://"))
				{
					url = "file://" + url.Substring(7);
				}
				Uri uri = new Uri(url, false, false);
				url = uri.ToString();
			}
			int num2 = url.Length - 1;
			if (url[num2] == '/')
			{
				url = url.Substring(0, num2);
			}
			return url;
		}

		// Token: 0x04001AA8 RID: 6824
		private string origin_url;
	}
}
