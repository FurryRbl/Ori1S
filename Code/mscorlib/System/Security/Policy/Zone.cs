using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Security;

namespace System.Security.Policy
{
	/// <summary>Provides the security zone of a code assembly as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x0200065A RID: 1626
	[ComVisible(true)]
	[Serializable]
	public sealed class Zone : IBuiltInEvidence, IIdentityPermissionFactory
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Zone" /> class with the zone from which a code assembly originates.</summary>
		/// <param name="zone">The zone of origin for the associated code assembly. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="zone" /> parameter is not a valid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x06003DEF RID: 15855 RVA: 0x000D5480 File Offset: 0x000D3680
		public Zone(SecurityZone zone)
		{
			if (!Enum.IsDefined(typeof(SecurityZone), zone))
			{
				string message = string.Format(Locale.GetText("Invalid zone {0}."), zone);
				throw new ArgumentException(message, "zone");
			}
			this.zone = zone;
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x000D54D8 File Offset: 0x000D36D8
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return 3;
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x000D54DC File Offset: 0x000D36DC
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			int num = (int)buffer[position++];
			num += (int)buffer[position++];
			return position;
		}

		// Token: 0x06003DF2 RID: 15858 RVA: 0x000D5500 File Offset: 0x000D3700
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			buffer[position++] = '\u0003';
			buffer[position++] = (char)(this.zone >> 16);
			buffer[position++] = (char)(this.zone & (SecurityZone)65535);
			return position;
		}

		/// <summary>Gets the zone from which the code assembly originates.</summary>
		/// <returns>The zone from which the code assembly originates.</returns>
		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06003DF3 RID: 15859 RVA: 0x000D5534 File Offset: 0x000D3734
		public SecurityZone SecurityZone
		{
			get
			{
				return this.zone;
			}
		}

		/// <summary>Creates an equivalent copy of the evidence object.</summary>
		/// <returns>A new, identical copy of the evidence object.</returns>
		// Token: 0x06003DF4 RID: 15860 RVA: 0x000D553C File Offset: 0x000D373C
		public object Copy()
		{
			return new Zone(this.zone);
		}

		/// <summary>Creates an identity permission that corresponds to the current instance of the <see cref="T:System.Security.Policy.Zone" /> evidence class.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> for the specified <see cref="T:System.Security.Policy.Zone" /> evidence.</returns>
		/// <param name="evidence">The evidence set from which to construct the identity permission. </param>
		// Token: 0x06003DF5 RID: 15861 RVA: 0x000D554C File Offset: 0x000D374C
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new ZoneIdentityPermission(this.zone);
		}

		/// <summary>Creates a new zone with the specified URL.</summary>
		/// <returns>A new zone with the specified URL.</returns>
		/// <param name="url">The URL from which to create the zone. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="url" /> parameter is null. </exception>
		// Token: 0x06003DF6 RID: 15862 RVA: 0x000D555C File Offset: 0x000D375C
		[MonoTODO("Not user configurable yet")]
		public static Zone CreateFromUrl(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			SecurityZone securityZone = SecurityZone.NoZone;
			if (url.Length == 0)
			{
				return new Zone(securityZone);
			}
			Uri uri = new Uri(url);
			if (securityZone == SecurityZone.NoZone)
			{
				if (uri.IsFile)
				{
					if (File.Exists(uri.LocalPath))
					{
						securityZone = SecurityZone.MyComputer;
					}
					else if (string.Compare("FILE://", 0, url, 0, 7, true, CultureInfo.InvariantCulture) == 0)
					{
						securityZone = SecurityZone.Intranet;
					}
					else
					{
						securityZone = SecurityZone.Internet;
					}
				}
				else if (uri.IsLoopback)
				{
					securityZone = SecurityZone.Intranet;
				}
				else
				{
					securityZone = SecurityZone.Internet;
				}
			}
			return new Zone(securityZone);
		}

		/// <summary>Compares the current <see cref="T:System.Security.Policy.Zone" /> evidence object to the specified object for equivalence.</summary>
		/// <returns>true if the two <see cref="T:System.Security.Policy.Zone" /> objects are equal; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Security.Policy.Zone" /> evidence object to test for equivalence with the current object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="o" /> parameter is not a <see cref="T:System.Security.Policy.Zone" /> object. </exception>
		// Token: 0x06003DF7 RID: 15863 RVA: 0x000D5600 File Offset: 0x000D3800
		public override bool Equals(object o)
		{
			Zone zone = o as Zone;
			return zone != null && zone.zone == this.zone;
		}

		/// <summary>Gets the hash code of the current zone.</summary>
		/// <returns>The hash code of the current zone.</returns>
		// Token: 0x06003DF8 RID: 15864 RVA: 0x000D562C File Offset: 0x000D382C
		public override int GetHashCode()
		{
			return (int)this.zone;
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Zone" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.Zone" />.</returns>
		// Token: 0x06003DF9 RID: 15865 RVA: 0x000D5634 File Offset: 0x000D3834
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Zone");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("Zone", this.zone.ToString()));
			return securityElement.ToString();
		}

		// Token: 0x04001AAC RID: 6828
		private SecurityZone zone;
	}
}
