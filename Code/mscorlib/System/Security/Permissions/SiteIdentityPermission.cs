using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Defines the identity permission for the Web site from which the code originates. This class cannot be inherited.</summary>
	// Token: 0x0200061E RID: 1566
	[ComVisible(true)]
	[Serializable]
	public sealed class SiteIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06003BAA RID: 15274 RVA: 0x000CCD78 File Offset: 0x000CAF78
		public SiteIdentityPermission(PermissionState state)
		{
			CodeAccessPermission.CheckPermissionState(state, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> class to represent the specified site identity.</summary>
		/// <param name="site">The site name or wildcard expression. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="site" /> parameter is not a valid string, or does not match a valid wildcard site name. </exception>
		// Token: 0x06003BAB RID: 15275 RVA: 0x000CCD88 File Offset: 0x000CAF88
		public SiteIdentityPermission(string site)
		{
			this.Site = site;
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x000CCDB4 File Offset: 0x000CAFB4
		int IBuiltInPermission.GetTokenIndex()
		{
			return 11;
		}

		/// <summary>Gets or sets the current site.</summary>
		/// <returns>The current site.</returns>
		/// <exception cref="T:System.NotSupportedException">The site identity cannot be retrieved because it has an ambiguous identity.</exception>
		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06003BAE RID: 15278 RVA: 0x000CCDB8 File Offset: 0x000CAFB8
		// (set) Token: 0x06003BAF RID: 15279 RVA: 0x000CCDD8 File Offset: 0x000CAFD8
		public string Site
		{
			get
			{
				if (this.IsEmpty())
				{
					throw new NullReferenceException("No site.");
				}
				return this._site;
			}
			set
			{
				if (!this.IsValid(value))
				{
					throw new ArgumentException("Invalid site.");
				}
				this._site = value;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06003BB0 RID: 15280 RVA: 0x000CCDF8 File Offset: 0x000CAFF8
		public override IPermission Copy()
		{
			if (this.IsEmpty())
			{
				return new SiteIdentityPermission(PermissionState.None);
			}
			return new SiteIdentityPermission(this._site);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not valid. </exception>
		// Token: 0x06003BB1 RID: 15281 RVA: 0x000CCE18 File Offset: 0x000CB018
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			string text = esd.Attribute("Site");
			if (text != null)
			{
				this.Site = text;
			}
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003BB2 RID: 15282 RVA: 0x000CCE4C File Offset: 0x000CB04C
		public override IPermission Intersect(IPermission target)
		{
			SiteIdentityPermission siteIdentityPermission = this.Cast(target);
			if (siteIdentityPermission == null || this.IsEmpty())
			{
				return null;
			}
			if (this.Match(siteIdentityPermission._site))
			{
				string site = (this._site.Length <= siteIdentityPermission._site.Length) ? siteIdentityPermission._site : this._site;
				return new SiteIdentityPermission(site);
			}
			return null;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003BB3 RID: 15283 RVA: 0x000CCEBC File Offset: 0x000CB0BC
		public override bool IsSubsetOf(IPermission target)
		{
			SiteIdentityPermission siteIdentityPermission = this.Cast(target);
			if (siteIdentityPermission == null)
			{
				return this.IsEmpty();
			}
			if (this._site == null && siteIdentityPermission._site == null)
			{
				return true;
			}
			if (this._site == null || siteIdentityPermission._site == null)
			{
				return false;
			}
			int num = siteIdentityPermission._site.IndexOf('*');
			if (num == -1)
			{
				return this._site == siteIdentityPermission._site;
			}
			return this._site.EndsWith(siteIdentityPermission._site.Substring(num + 1));
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06003BB4 RID: 15284 RVA: 0x000CCF50 File Offset: 0x000CB150
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._site != null)
			{
				securityElement.AddAttribute("Site", this._site);
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. -or-The permissions are not equal and one is not a subset of the other.</exception>
		// Token: 0x06003BB5 RID: 15285 RVA: 0x000CCF84 File Offset: 0x000CB184
		public override IPermission Union(IPermission target)
		{
			SiteIdentityPermission siteIdentityPermission = this.Cast(target);
			if (siteIdentityPermission == null || siteIdentityPermission.IsEmpty())
			{
				return this.Copy();
			}
			if (this.IsEmpty())
			{
				return siteIdentityPermission.Copy();
			}
			if (this.Match(siteIdentityPermission._site))
			{
				string site = (this._site.Length >= siteIdentityPermission._site.Length) ? siteIdentityPermission._site : this._site;
				return new SiteIdentityPermission(site);
			}
			throw new ArgumentException(Locale.GetText("Cannot union two different sites."), "target");
		}

		// Token: 0x06003BB6 RID: 15286 RVA: 0x000CD01C File Offset: 0x000CB21C
		private bool IsEmpty()
		{
			return this._site == null;
		}

		// Token: 0x06003BB7 RID: 15287 RVA: 0x000CD028 File Offset: 0x000CB228
		private SiteIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SiteIdentityPermission siteIdentityPermission = target as SiteIdentityPermission;
			if (siteIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(SiteIdentityPermission));
			}
			return siteIdentityPermission;
		}

		// Token: 0x06003BB8 RID: 15288 RVA: 0x000CD05C File Offset: 0x000CB25C
		private bool IsValid(string s)
		{
			if (s == null || s.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < s.Length; i++)
			{
				ushort num = (ushort)s[i];
				if (num < 33 || num > 126)
				{
					return false;
				}
				if (num == 42 && s.Length > 1 && (s[i + 1] != '.' || i > 0))
				{
					return false;
				}
				if (!SiteIdentityPermission.valid[(int)(num - 33)])
				{
					return false;
				}
			}
			return s.Length != 1 || s[0] != '.';
		}

		// Token: 0x06003BB9 RID: 15289 RVA: 0x000CD108 File Offset: 0x000CB308
		private bool Match(string target)
		{
			if (this._site == null || target == null)
			{
				return false;
			}
			int num = this._site.IndexOf('*');
			int num2 = target.IndexOf('*');
			if (num == -1 && num2 == -1)
			{
				return this._site == target;
			}
			if (num == -1)
			{
				return this._site.EndsWith(target.Substring(num2 + 1));
			}
			if (num2 == -1)
			{
				return target.EndsWith(this._site.Substring(num + 1));
			}
			string text = this._site.Substring(num + 1);
			target = target.Substring(num2 + 1);
			if (text.Length > target.Length)
			{
				return text.EndsWith(target);
			}
			return target.EndsWith(text);
		}

		// Token: 0x040019FF RID: 6655
		private const int version = 1;

		// Token: 0x04001A00 RID: 6656
		private string _site;

		// Token: 0x04001A01 RID: 6657
		private static bool[] valid = new bool[]
		{
			true,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			true,
			true,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			false,
			true,
			true,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			true,
			true
		};
	}
}
