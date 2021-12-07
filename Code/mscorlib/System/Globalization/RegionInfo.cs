using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Contains information about the country/region.</summary>
	// Token: 0x02000225 RID: 549
	[ComVisible(true)]
	[Serializable]
	public class RegionInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.RegionInfo" /> class based on the country/region associated with the specified culture identifier.</summary>
		/// <param name="culture">A culture identifier. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="culture" /> specifies either an invariant, custom, or neutral culture.</exception>
		// Token: 0x06001C1D RID: 7197 RVA: 0x00067FC4 File Offset: 0x000661C4
		public RegionInfo(int culture)
		{
			if (!this.GetByTerritory(CultureInfo.GetCultureInfo(culture)))
			{
				throw new ArgumentException(string.Format("Region ID {0} (0x{0:X4}) is not a supported region.", culture), "culture");
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.RegionInfo" /> class based on the country/region or specific culture, specified by name.</summary>
		/// <param name="name">A string containing one of the two-letter codes defined in ISO 3166 for country/region.-or-Beginning in .NET Framework version 2.0, a string containing the culture name for a specific culture, custom culture, or Windows-only culture. If the culture name is not in RFC 4646 format, your application should specify the entire culture name, not just the country/region. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid country/region name or specific culture name.</exception>
		// Token: 0x06001C1E RID: 7198 RVA: 0x00068004 File Offset: 0x00066204
		public RegionInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException();
			}
			if (this.construct_internal_region_from_name(name.ToUpperInvariant()))
			{
				this.lcid = name.GetHashCode();
				return;
			}
			if (!this.GetByTerritory(CultureInfo.GetCultureInfo(name)))
			{
				throw new ArgumentException(string.Format("Region name {0} is not supported.", name), "name");
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.RegionInfo" /> that represents the country/region used by the current thread.</summary>
		/// <returns>The <see cref="T:System.Globalization.RegionInfo" /> that represents the country/region used by the current thread.</returns>
		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x00068068 File Offset: 0x00066268
		public static RegionInfo CurrentRegion
		{
			get
			{
				if (RegionInfo.currentRegion == null)
				{
					CultureInfo currentCulture = CultureInfo.CurrentCulture;
					if (currentCulture == null || CultureInfo.BootstrapCultureID == 127)
					{
						return null;
					}
					RegionInfo.currentRegion = new RegionInfo(CultureInfo.BootstrapCultureID);
				}
				return RegionInfo.currentRegion;
			}
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x000680B0 File Offset: 0x000662B0
		private bool GetByTerritory(CultureInfo ci)
		{
			if (ci == null)
			{
				throw new Exception("INTERNAL ERROR: should not happen.");
			}
			if (ci.IsNeutralCulture || ci.Territory == null)
			{
				return false;
			}
			this.lcid = ci.LCID;
			return this.construct_internal_region_from_name(ci.Territory.ToUpperInvariant());
		}

		// Token: 0x06001C21 RID: 7201
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool construct_internal_region_from_name(string name);

		/// <summary>Gets the name, in English, of the currency used in the country/region.</summary>
		/// <returns>The name, in English, of the currency used in the country/region.</returns>
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x00068104 File Offset: 0x00066304
		[ComVisible(false)]
		public virtual string CurrencyEnglishName
		{
			get
			{
				return this.currencyEnglishName;
			}
		}

		/// <summary>Gets the currency symbol associated with the country/region.</summary>
		/// <returns>The currency symbol associated with the country/region.</returns>
		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x0006810C File Offset: 0x0006630C
		public virtual string CurrencySymbol
		{
			get
			{
				return this.currencySymbol;
			}
		}

		/// <summary>Gets the full name of the country/region in the language of the localized version of .NET Framework.</summary>
		/// <returns>The full name of the country/region in the language of the localized version of .NET Framework.</returns>
		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x00068114 File Offset: 0x00066314
		[MonoTODO("DisplayName currently only returns the EnglishName")]
		public virtual string DisplayName
		{
			get
			{
				return this.englishName;
			}
		}

		/// <summary>Gets the full name of the country/region in English.</summary>
		/// <returns>The full name of the country/region in English.</returns>
		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x0006811C File Offset: 0x0006631C
		public virtual string EnglishName
		{
			get
			{
				return this.englishName;
			}
		}

		/// <summary>Gets a unique identification number for a geographical region, country, city, or location.</summary>
		/// <returns>A 32-bit signed number that uniquely identifies a geographical location.</returns>
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x00068124 File Offset: 0x00066324
		[ComVisible(false)]
		public virtual int GeoId
		{
			get
			{
				return this.regionId;
			}
		}

		/// <summary>Gets a value indicating whether the country/region uses the metric system for measurements.</summary>
		/// <returns>true if the country/region uses the metric system for measurements; otherwise, false.</returns>
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001C27 RID: 7207 RVA: 0x0006812C File Offset: 0x0006632C
		public virtual bool IsMetric
		{
			get
			{
				string text = this.iso2Name;
				if (text != null)
				{
					if (RegionInfo.<>f__switch$map1D == null)
					{
						RegionInfo.<>f__switch$map1D = new Dictionary<string, int>(2)
						{
							{
								"US",
								0
							},
							{
								"UK",
								0
							}
						};
					}
					int num;
					if (RegionInfo.<>f__switch$map1D.TryGetValue(text, out num))
					{
						if (num == 0)
						{
							return false;
						}
					}
				}
				return true;
			}
		}

		/// <summary>Gets the three-character ISO 4217 currency symbol associated with the country/region.</summary>
		/// <returns>The three-character ISO 4217 currency symbol associated with the country/region.</returns>
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001C28 RID: 7208 RVA: 0x00068198 File Offset: 0x00066398
		public virtual string ISOCurrencySymbol
		{
			get
			{
				return this.isoCurrencySymbol;
			}
		}

		/// <summary>Gets the name of a country/region formatted in the native language of the country/region.</summary>
		/// <returns>The native name of the country/region formatted in the language associated with the ISO 3166 country/region code. </returns>
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x000681A0 File Offset: 0x000663A0
		[ComVisible(false)]
		public virtual string NativeName
		{
			get
			{
				return this.DisplayName;
			}
		}

		/// <summary>Gets the name of the currency used in the country/region, formatted in the native language of the country/region. </summary>
		/// <returns>The native name of the currency used in the country/region, formatted in the language associated with the ISO 3166 country/region code. </returns>
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001C2A RID: 7210 RVA: 0x000681A8 File Offset: 0x000663A8
		[MonoTODO("Not implemented")]
		[ComVisible(false)]
		public virtual string CurrencyNativeName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name or ISO 3166 two-letter country/region code for the current <see cref="T:System.Globalization.RegionInfo" /> object.</summary>
		/// <returns>The value specified by the <paramref name="name" /> parameter of the <see cref="M:System.Globalization.RegionInfo.#ctor(System.String)" /> constructor. The return value is in uppercase.-or-The two-letter code defined in ISO 3166 for the country/region specified by the <paramref name="culture" /> parameter of the <see cref="M:System.Globalization.RegionInfo.#ctor(System.Int32)" /> constructor. The return value is in uppercase.</returns>
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x000681B0 File Offset: 0x000663B0
		public virtual string Name
		{
			get
			{
				return this.iso2Name;
			}
		}

		/// <summary>Gets the three-letter code defined in ISO 3166 for the country/region.</summary>
		/// <returns>The three-letter code defined in ISO 3166 for the country/region.</returns>
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001C2C RID: 7212 RVA: 0x000681B8 File Offset: 0x000663B8
		public virtual string ThreeLetterISORegionName
		{
			get
			{
				return this.iso3Name;
			}
		}

		/// <summary>Gets the three-letter code assigned by Windows to the country/region represented by this <see cref="T:System.Globalization.RegionInfo" />.</summary>
		/// <returns>The three-letter code assigned by Windows to the country/region represented by this <see cref="T:System.Globalization.RegionInfo" />.</returns>
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x000681C0 File Offset: 0x000663C0
		public virtual string ThreeLetterWindowsRegionName
		{
			get
			{
				return this.win3Name;
			}
		}

		/// <summary>Gets the two-letter code defined in ISO 3166 for the country/region.</summary>
		/// <returns>The two-letter code defined in ISO 3166 for the country/region.</returns>
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001C2E RID: 7214 RVA: 0x000681C8 File Offset: 0x000663C8
		public virtual string TwoLetterISORegionName
		{
			get
			{
				return this.iso2Name;
			}
		}

		/// <summary>Determines whether the specified object is the same instance as the current <see cref="T:System.Globalization.RegionInfo" />.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is a <see cref="T:System.Globalization.RegionInfo" /> object and its <see cref="P:System.Globalization.RegionInfo.Name" /> property is the same as the <see cref="P:System.Globalization.RegionInfo.Name" /> property of the current <see cref="T:System.Globalization.RegionInfo" /> object; otherwise, false.</returns>
		/// <param name="value">The object to compare with the current <see cref="T:System.Globalization.RegionInfo" />. </param>
		// Token: 0x06001C2F RID: 7215 RVA: 0x000681D0 File Offset: 0x000663D0
		public override bool Equals(object value)
		{
			RegionInfo regionInfo = value as RegionInfo;
			return regionInfo != null && this.lcid == regionInfo.lcid;
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.RegionInfo" />, suitable for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.RegionInfo" />.</returns>
		// Token: 0x06001C30 RID: 7216 RVA: 0x000681FC File Offset: 0x000663FC
		public override int GetHashCode()
		{
			return (int)((ulong)int.MinValue + (ulong)((long)((long)this.regionId << 3)) + (ulong)((long)this.regionId));
		}

		/// <summary>Returns a string containing the culture name or ISO 3166 two-letter country/region codes specified for the current <see cref="T:System.Globalization.RegionInfo" />.</summary>
		/// <returns>A string containing the culture name or ISO 3166 two-letter country/region codes defined for the current <see cref="T:System.Globalization.RegionInfo" />.</returns>
		// Token: 0x06001C31 RID: 7217 RVA: 0x00068218 File Offset: 0x00066418
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04000A8B RID: 2699
		private static RegionInfo currentRegion;

		// Token: 0x04000A8C RID: 2700
		private int lcid;

		// Token: 0x04000A8D RID: 2701
		private int regionId;

		// Token: 0x04000A8E RID: 2702
		private string iso2Name;

		// Token: 0x04000A8F RID: 2703
		private string iso3Name;

		// Token: 0x04000A90 RID: 2704
		private string win3Name;

		// Token: 0x04000A91 RID: 2705
		private string englishName;

		// Token: 0x04000A92 RID: 2706
		private string currencySymbol;

		// Token: 0x04000A93 RID: 2707
		private string isoCurrencySymbol;

		// Token: 0x04000A94 RID: 2708
		private string currencyEnglishName;
	}
}
