using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Globalization
{
	/// <summary>Provides information about a specific culture (called a "locale" for unmanaged code development). The information includes the names for the culture, the writing system, the calendar used, and formatting for dates and sort strings.</summary>
	// Token: 0x0200020E RID: 526
	[ComVisible(true)]
	[Serializable]
	public class CultureInfo : ICloneable, IFormatProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class based on the culture specified by the culture identifier.</summary>
		/// <param name="culture">A predefined <see cref="T:System.Globalization.CultureInfo" /> identifier, <see cref="P:System.Globalization.CultureInfo.LCID" /> property of an existing <see cref="T:System.Globalization.CultureInfo" /> object, or Windows-only culture identifier. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="culture" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="culture" /> is not a valid culture identifier. -or-In .NET Compact Framework applications, <paramref name="culture" /> is not supported by the operating system of the device. </exception>
		// Token: 0x06001A0B RID: 6667 RVA: 0x00061394 File Offset: 0x0005F594
		public CultureInfo(int culture) : this(culture, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class based on the culture specified by the culture identifier and on the Boolean that specifies whether to use the user-selected culture settings from the system.</summary>
		/// <param name="culture">A predefined <see cref="T:System.Globalization.CultureInfo" /> identifier, <see cref="P:System.Globalization.CultureInfo.LCID" /> property of an existing <see cref="T:System.Globalization.CultureInfo" /> object, or Windows-only culture identifier. </param>
		/// <param name="useUserOverride">A Boolean that denotes whether to use the user-selected culture settings (true) or the default culture settings (false). </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="culture" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="culture" /> is not a valid culture identifier.-or-In .NET Compact Framework applications, <paramref name="culture" /> is not supported by the operating system of the device.  </exception>
		// Token: 0x06001A0C RID: 6668 RVA: 0x000613A0 File Offset: 0x0005F5A0
		public CultureInfo(int culture, bool useUserOverride) : this(culture, useUserOverride, false)
		{
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x000613AC File Offset: 0x0005F5AC
		private CultureInfo(int culture, bool useUserOverride, bool read_only)
		{
			if (culture < 0)
			{
				throw new ArgumentOutOfRangeException("culture", "Positive number required.");
			}
			this.constructed = true;
			this.m_isReadOnly = read_only;
			this.m_useUserOverride = useUserOverride;
			if (culture == 127)
			{
				this.ConstructInvariant(read_only);
				return;
			}
			if (!this.ConstructInternalLocaleFromLcid(culture))
			{
				throw new ArgumentException(string.Format("Culture ID {0} (0x{0:X4}) is not a supported culture.", culture), "culture");
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class based on the culture specified by name.</summary>
		/// <param name="name">A predefined <see cref="T:System.Globalization.CultureInfo" /> name, <see cref="P:System.Globalization.CultureInfo.Name" /> of an existing <see cref="T:System.Globalization.CultureInfo" />, or Windows-only culture name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid culture name. -or-In .NET Compact Framework applications, <paramref name="culture" /> is not supported by the operating system of the device.</exception>
		// Token: 0x06001A0E RID: 6670 RVA: 0x00061424 File Offset: 0x0005F624
		public CultureInfo(string name) : this(name, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class based on the culture specified by name and on the Boolean that specifies whether to use the user-selected culture settings from the system.</summary>
		/// <param name="name">A predefined <see cref="T:System.Globalization.CultureInfo" /> name, <see cref="P:System.Globalization.CultureInfo.Name" /> of an existing <see cref="T:System.Globalization.CultureInfo" />, or Windows-only culture name. </param>
		/// <param name="useUserOverride">A Boolean that denotes whether to use the user-selected culture settings (true) or the default culture settings (false). </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid culture name. -or-In .NET Compact Framework applications, <paramref name="culture" /> is not supported by the operating system of the device.</exception>
		// Token: 0x06001A0F RID: 6671 RVA: 0x00061430 File Offset: 0x0005F630
		public CultureInfo(string name, bool useUserOverride) : this(name, useUserOverride, false)
		{
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x0006143C File Offset: 0x0005F63C
		private CultureInfo(string name, bool useUserOverride, bool read_only)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.constructed = true;
			this.m_isReadOnly = read_only;
			this.m_useUserOverride = useUserOverride;
			if (name.Length == 0)
			{
				this.ConstructInvariant(read_only);
				return;
			}
			if (!this.ConstructInternalLocaleFromName(name.ToLowerInvariant()))
			{
				throw new ArgumentException("Culture name " + name + " is not supported.", "name");
			}
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x000614B4 File Offset: 0x0005F6B4
		private CultureInfo()
		{
			this.constructed = true;
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x000614C4 File Offset: 0x0005F6C4
		static CultureInfo()
		{
			CultureInfo.invariant_culture_info = new CultureInfo(127, false, true);
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> that is culture-independent (invariant).</summary>
		/// <returns>The <see cref="T:System.Globalization.CultureInfo" /> that is culture-independent (invariant).</returns>
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x000614F8 File Offset: 0x0005F6F8
		public static CultureInfo InvariantCulture
		{
			get
			{
				return CultureInfo.invariant_culture_info;
			}
		}

		/// <summary>Creates a <see cref="T:System.Globalization.CultureInfo" /> object that represents the specific culture that is associated with the specified name.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> object that represents:The invariant culture, if <paramref name="name" /> is an empty string ("").-or- The specific culture associated with <paramref name="name" />, if <paramref name="name" /> is a neutral culture.-or- The culture specified by <paramref name="name" />, if <paramref name="name" /> is already a specific culture.</returns>
		/// <param name="name">A predefined <see cref="T:System.Globalization.CultureInfo" /> name or the name of an existing <see cref="T:System.Globalization.CultureInfo" /> object. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid culture name.-or- The culture specified by <paramref name="name" /> does not have a specific culture associated with it. </exception>
		/// <exception cref="T:System.NullReferenceException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x06001A14 RID: 6676 RVA: 0x00061504 File Offset: 0x0005F704
		public static CultureInfo CreateSpecificCulture(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				return CultureInfo.InvariantCulture;
			}
			CultureInfo cultureInfo = new CultureInfo();
			if (!CultureInfo.ConstructInternalLocaleFromSpecificName(cultureInfo, name.ToLowerInvariant()))
			{
				throw new ArgumentException("Culture name " + name + " is not supported.", name);
			}
			return cultureInfo;
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> that represents the culture used by the current thread.</summary>
		/// <returns>The <see cref="T:System.Globalization.CultureInfo" /> that represents the culture used by the current thread.</returns>
		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001A15 RID: 6677 RVA: 0x00061568 File Offset: 0x0005F768
		public static CultureInfo CurrentCulture
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> that represents the current culture used by the Resource Manager to look up culture-specific resources at run time.</summary>
		/// <returns>The <see cref="T:System.Globalization.CultureInfo" /> that represents the current culture used by the Resource Manager to look up culture-specific resources at run time.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001A16 RID: 6678 RVA: 0x00061574 File Offset: 0x0005F774
		public static CultureInfo CurrentUICulture
		{
			get
			{
				return Thread.CurrentThread.CurrentUICulture;
			}
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x00061580 File Offset: 0x0005F780
		internal static CultureInfo ConstructCurrentCulture()
		{
			CultureInfo cultureInfo = new CultureInfo();
			if (!CultureInfo.ConstructInternalLocaleFromCurrentLocale(cultureInfo))
			{
				cultureInfo = CultureInfo.InvariantCulture;
			}
			CultureInfo.BootstrapCultureID = cultureInfo.cultureID;
			return cultureInfo;
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x000615B0 File Offset: 0x0005F7B0
		internal static CultureInfo ConstructCurrentUICulture()
		{
			return CultureInfo.ConstructCurrentCulture();
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x000615B8 File Offset: 0x0005F7B8
		internal string Territory
		{
			get
			{
				return this.territory;
			}
		}

		/// <summary>Gets the culture types that pertain to the current <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
		/// <returns>A bitwise combination of one or more <see cref="T:System.Globalization.CultureTypes" /> values. There is no default value.</returns>
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x000615C0 File Offset: 0x0005F7C0
		[ComVisible(false)]
		public CultureTypes CultureTypes
		{
			get
			{
				CultureTypes cultureTypes = (CultureTypes)0;
				foreach (object obj in Enum.GetValues(typeof(CultureTypes)))
				{
					CultureTypes cultureTypes2 = (CultureTypes)((int)obj);
					if (Array.IndexOf<CultureInfo>(CultureInfo.GetCultures(cultureTypes2), this) >= 0)
					{
						cultureTypes |= cultureTypes2;
					}
				}
				return cultureTypes;
			}
		}

		/// <summary>Gets an alternate user interface culture suitable for console applications when the default graphic user interface culture is unsuitable.</summary>
		/// <returns>An alternate neutral culture that is used to read and display text on the console.</returns>
		// Token: 0x06001A1B RID: 6683 RVA: 0x0006164C File Offset: 0x0005F84C
		[ComVisible(false)]
		public CultureInfo GetConsoleFallbackUICulture()
		{
			string name = this.Name;
			if (name != null)
			{
				if (CultureInfo.<>f__switch$map1A == null)
				{
					CultureInfo.<>f__switch$map1A = new Dictionary<string, int>(48)
					{
						{
							"ar",
							0
						},
						{
							"ar-BH",
							0
						},
						{
							"ar-EG",
							0
						},
						{
							"ar-IQ",
							0
						},
						{
							"ar-JO",
							0
						},
						{
							"ar-KW",
							0
						},
						{
							"ar-LB",
							0
						},
						{
							"ar-LY",
							0
						},
						{
							"ar-QA",
							0
						},
						{
							"ar-SA",
							0
						},
						{
							"ar-SY",
							0
						},
						{
							"ar-AE",
							0
						},
						{
							"ar-YE",
							0
						},
						{
							"dv",
							0
						},
						{
							"dv-MV",
							0
						},
						{
							"fa",
							0
						},
						{
							"fa-IR",
							0
						},
						{
							"gu",
							0
						},
						{
							"gu-IN",
							0
						},
						{
							"he",
							0
						},
						{
							"he-IL",
							0
						},
						{
							"hi",
							0
						},
						{
							"hi-IN",
							0
						},
						{
							"kn",
							0
						},
						{
							"kn-IN",
							0
						},
						{
							"kok",
							0
						},
						{
							"kok-IN",
							0
						},
						{
							"mr",
							0
						},
						{
							"mr-IN",
							0
						},
						{
							"pa",
							0
						},
						{
							"pa-IN",
							0
						},
						{
							"sa",
							0
						},
						{
							"sa-IN",
							0
						},
						{
							"syr",
							0
						},
						{
							"syr-SY",
							0
						},
						{
							"ta",
							0
						},
						{
							"ta-IN",
							0
						},
						{
							"te",
							0
						},
						{
							"te-IN",
							0
						},
						{
							"th",
							0
						},
						{
							"th-TH",
							0
						},
						{
							"ur",
							0
						},
						{
							"ur-PK",
							0
						},
						{
							"vi",
							0
						},
						{
							"vi-VN",
							0
						},
						{
							"ar-DZ",
							1
						},
						{
							"ar-MA",
							1
						},
						{
							"ar-TN",
							1
						}
					};
				}
				int num;
				if (CultureInfo.<>f__switch$map1A.TryGetValue(name, out num))
				{
					if (num == 0)
					{
						return CultureInfo.GetCultureInfo("en");
					}
					if (num == 1)
					{
						return CultureInfo.GetCultureInfo("fr");
					}
				}
			}
			return ((this.CultureTypes & CultureTypes.WindowsOnlyCultures) == (CultureTypes)0) ? this : CultureInfo.InvariantCulture;
		}

		/// <summary>Deprecated. Gets the RFC 4646 standard identification for a language. </summary>
		/// <returns>A string that is the RFC 4646 standard identification for a language.</returns>
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x00061914 File Offset: 0x0005FB14
		[ComVisible(false)]
		public string IetfLanguageTag
		{
			get
			{
				string name = this.Name;
				if (name != null)
				{
					if (CultureInfo.<>f__switch$map19 == null)
					{
						CultureInfo.<>f__switch$map19 = new Dictionary<string, int>(2)
						{
							{
								"zh-CHS",
								0
							},
							{
								"zh-CHT",
								1
							}
						};
					}
					int num;
					if (CultureInfo.<>f__switch$map19.TryGetValue(name, out num))
					{
						if (num == 0)
						{
							return "zh-Hans";
						}
						if (num == 1)
						{
							return "zh-Hant";
						}
					}
				}
				return this.Name;
			}
		}

		/// <summary>Gets the active input locale identifier.</summary>
		/// <returns>A 32-bit signed number that specifies an input locale identifier.</returns>
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x00061994 File Offset: 0x0005FB94
		[ComVisible(false)]
		public virtual int KeyboardLayoutId
		{
			get
			{
				int lcid = this.LCID;
				if (lcid == 4)
				{
					return 2052;
				}
				if (lcid == 1034)
				{
					return 3082;
				}
				if (lcid == 31748)
				{
					return 1028;
				}
				if (lcid != 31770)
				{
					return (this.LCID >= 1024) ? this.LCID : (this.LCID + 1024);
				}
				return 2074;
			}
		}

		/// <summary>Gets the culture identifier for the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The culture identifier for the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001A1E RID: 6686 RVA: 0x00061A14 File Offset: 0x0005FC14
		public virtual int LCID
		{
			get
			{
				return this.cultureID;
			}
		}

		/// <summary>Gets the culture name in the format "&lt;languagecode2&gt;-&lt;country/regioncode2&gt;".</summary>
		/// <returns>The culture name in the format "&lt;languagecode2&gt;-&lt;country/regioncode2&gt;", where &lt;languagecode2&gt; is a lowercase two-letter code derived from ISO 639-1 and &lt;country/regioncode2&gt; is an uppercase two-letter code derived from ISO 3166.</returns>
		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x00061A1C File Offset: 0x0005FC1C
		public virtual string Name
		{
			get
			{
				return this.m_name;
			}
		}

		/// <summary>Gets the culture name, consisting of the language, the country/region, and the optional script, that the culture is set to display.</summary>
		/// <returns>The culture name. consisting of the full name of the language, the full name of the country/region, and the optional script. The format is discussed in the description of the <see cref="T:System.Globalization.CultureInfo" /> class.</returns>
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001A20 RID: 6688 RVA: 0x00061A24 File Offset: 0x0005FC24
		public virtual string NativeName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.nativename;
			}
		}

		/// <summary>Gets the default calendar used by the culture.</summary>
		/// <returns>A <see cref="T:System.Globalization.Calendar" /> that represents the default calendar used by the culture.</returns>
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x00061A40 File Offset: 0x0005FC40
		public virtual Calendar Calendar
		{
			get
			{
				return this.DateTimeFormat.Calendar;
			}
		}

		/// <summary>Gets the list of calendars that can be used by the culture.</summary>
		/// <returns>An array of type <see cref="T:System.Globalization.Calendar" /> that represents the calendars that can be used by the culture represented by the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x00061A50 File Offset: 0x0005FC50
		public virtual Calendar[] OptionalCalendars
		{
			get
			{
				if (this.optional_calendars == null)
				{
					lock (this)
					{
						if (this.optional_calendars == null)
						{
							this.ConstructCalendars();
						}
					}
				}
				return this.optional_calendars;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> that represents the parent culture of the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The <see cref="T:System.Globalization.CultureInfo" /> that represents the parent culture of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001A23 RID: 6691 RVA: 0x00061AB0 File Offset: 0x0005FCB0
		public virtual CultureInfo Parent
		{
			get
			{
				if (this.parent_culture == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					if (this.parent_lcid == this.cultureID)
					{
						return null;
					}
					if (this.parent_lcid == 127)
					{
						this.parent_culture = CultureInfo.InvariantCulture;
					}
					else if (this.cultureID == 127)
					{
						this.parent_culture = this;
					}
					else
					{
						this.parent_culture = new CultureInfo(this.parent_lcid);
					}
				}
				return this.parent_culture;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.TextInfo" /> that defines the writing system associated with the culture.</summary>
		/// <returns>The <see cref="T:System.Globalization.TextInfo" /> that defines the writing system associated with the culture.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001A24 RID: 6692 RVA: 0x00061B3C File Offset: 0x0005FD3C
		public virtual TextInfo TextInfo
		{
			get
			{
				if (this.textInfo == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					lock (this)
					{
						if (this.textInfo == null)
						{
							this.textInfo = this.CreateTextInfo(this.m_isReadOnly);
						}
					}
				}
				return this.textInfo;
			}
		}

		/// <summary>Gets the ISO 639-2 three-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The ISO 639-2 three-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x00061BC0 File Offset: 0x0005FDC0
		public virtual string ThreeLetterISOLanguageName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.iso3lang;
			}
		}

		/// <summary>Gets the three-letter code for the language as defined in the Windows API.</summary>
		/// <returns>The three-letter code for the language as defined in the Windows API.</returns>
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x00061BDC File Offset: 0x0005FDDC
		public virtual string ThreeLetterWindowsLanguageName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.win3lang;
			}
		}

		/// <summary>Gets the ISO 639-1 two-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The ISO 639-1 two-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001A27 RID: 6695 RVA: 0x00061BF8 File Offset: 0x0005FDF8
		public virtual string TwoLetterISOLanguageName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.iso2lang;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.CultureInfo" /> uses the user-selected culture settings.</summary>
		/// <returns>true if the current <see cref="T:System.Globalization.CultureInfo" /> uses the user-selected culture settings; otherwise, false.</returns>
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x00061C14 File Offset: 0x0005FE14
		public bool UseUserOverride
		{
			get
			{
				return this.m_useUserOverride;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001A29 RID: 6697 RVA: 0x00061C1C File Offset: 0x0005FE1C
		internal string IcuName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.icu_name;
			}
		}

		/// <summary>Refreshes cached culture-related information.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001A2A RID: 6698 RVA: 0x00061C38 File Offset: 0x0005FE38
		public void ClearCachedData()
		{
			Thread.CurrentThread.CurrentCulture = null;
			Thread.CurrentThread.CurrentUICulture = null;
		}

		/// <summary>Creates a copy of the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>A copy of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x06001A2B RID: 6699 RVA: 0x00061C50 File Offset: 0x0005FE50
		public virtual object Clone()
		{
			if (!this.constructed)
			{
				this.Construct();
			}
			CultureInfo cultureInfo = (CultureInfo)base.MemberwiseClone();
			cultureInfo.m_isReadOnly = false;
			cultureInfo.cached_serialized_form = null;
			if (!this.IsNeutralCulture)
			{
				cultureInfo.NumberFormat = (NumberFormatInfo)this.NumberFormat.Clone();
				cultureInfo.DateTimeFormat = (DateTimeFormatInfo)this.DateTimeFormat.Clone();
			}
			return cultureInfo;
		}

		/// <summary>Determines whether the specified object is the same culture as the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>true if <paramref name="value" /> is the same culture as the current <see cref="T:System.Globalization.CultureInfo" />; otherwise, false.</returns>
		/// <param name="value">The object to compare with the current <see cref="T:System.Globalization.CultureInfo" />. </param>
		// Token: 0x06001A2C RID: 6700 RVA: 0x00061CC0 File Offset: 0x0005FEC0
		public override bool Equals(object value)
		{
			CultureInfo cultureInfo = value as CultureInfo;
			return cultureInfo != null && cultureInfo.cultureID == this.cultureID;
		}

		/// <summary>Gets the list of supported cultures filtered by the specified <see cref="T:System.Globalization.CultureTypes" /> parameter.</summary>
		/// <returns>An array of type <see cref="T:System.Globalization.CultureInfo" /> that contains the cultures specified by the <paramref name="types" /> parameter. The array of cultures is unsorted.</returns>
		/// <param name="types">A bitwise combination of <see cref="T:System.Globalization.CultureTypes" /> values that filter the cultures to retrieve. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="types" /> specifies an invalid combination of <see cref="T:System.Globalization.CultureTypes" /> values.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001A2D RID: 6701 RVA: 0x00061CEC File Offset: 0x0005FEEC
		public static CultureInfo[] GetCultures(CultureTypes types)
		{
			bool flag = (types & CultureTypes.NeutralCultures) != (CultureTypes)0;
			bool specific = (types & CultureTypes.SpecificCultures) != (CultureTypes)0;
			bool installed = (types & CultureTypes.InstalledWin32Cultures) != (CultureTypes)0;
			CultureInfo[] array = CultureInfo.internal_get_cultures(flag, specific, installed);
			if (flag && array.Length > 0 && array[0] == null)
			{
				array[0] = (CultureInfo)CultureInfo.InvariantCulture.Clone();
			}
			return array;
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.CultureInfo" />, suitable for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x06001A2E RID: 6702 RVA: 0x00061D4C File Offset: 0x0005FF4C
		public override int GetHashCode()
		{
			return this.cultureID;
		}

		/// <summary>Returns a read-only wrapper around the specified <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> wrapper around <paramref name="ci" />.</returns>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> to wrap. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ci" /> is null. </exception>
		// Token: 0x06001A2F RID: 6703 RVA: 0x00061D54 File Offset: 0x0005FF54
		public static CultureInfo ReadOnly(CultureInfo ci)
		{
			if (ci == null)
			{
				throw new ArgumentNullException("ci");
			}
			if (ci.m_isReadOnly)
			{
				return ci;
			}
			CultureInfo cultureInfo = (CultureInfo)ci.Clone();
			cultureInfo.m_isReadOnly = true;
			if (cultureInfo.numInfo != null)
			{
				cultureInfo.numInfo = NumberFormatInfo.ReadOnly(cultureInfo.numInfo);
			}
			if (cultureInfo.dateTimeInfo != null)
			{
				cultureInfo.dateTimeInfo = DateTimeFormatInfo.ReadOnly(cultureInfo.dateTimeInfo);
			}
			if (cultureInfo.textInfo != null)
			{
				cultureInfo.textInfo = TextInfo.ReadOnly(cultureInfo.textInfo);
			}
			return cultureInfo;
		}

		/// <summary>Returns a string containing the name of the current <see cref="T:System.Globalization.CultureInfo" /> in the format "&lt;languagecode2&gt;-&lt;country/regioncode2&gt;".</summary>
		/// <returns>A string containing the name of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x06001A30 RID: 6704 RVA: 0x00061DFC File Offset: 0x0005FFFC
		public override string ToString()
		{
			return this.m_name;
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CompareInfo" /> that defines how to compare strings for the culture.</summary>
		/// <returns>The <see cref="T:System.Globalization.CompareInfo" /> that defines how to compare strings for the culture.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001A31 RID: 6705 RVA: 0x00061E04 File Offset: 0x00060004
		public virtual CompareInfo CompareInfo
		{
			get
			{
				if (this.compareInfo == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					lock (this)
					{
						if (this.compareInfo == null)
						{
							this.compareInfo = new CompareInfo(this);
						}
					}
				}
				return this.compareInfo;
			}
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00061E84 File Offset: 0x00060084
		internal static bool IsIDNeutralCulture(int lcid)
		{
			bool result;
			if (!CultureInfo.internal_is_lcid_neutral(lcid, out result))
			{
				throw new ArgumentException(string.Format("Culture id 0x{:x4} is not supported.", lcid));
			}
			return result;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.CultureInfo" /> represents a neutral culture.</summary>
		/// <returns>true if the current <see cref="T:System.Globalization.CultureInfo" /> represents a neutral culture; otherwise, false.</returns>
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x00061EB8 File Offset: 0x000600B8
		public virtual bool IsNeutralCulture
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.cultureID != 127 && ((this.cultureID & 65280) == 0 || this.specific_lcid == 0);
			}
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00061EF8 File Offset: 0x000600F8
		internal void CheckNeutral()
		{
			if (this.IsNeutralCulture)
			{
				throw new NotSupportedException("Culture \"" + this.m_name + "\" is a neutral culture. It can not be used in formatting and parsing and therefore cannot be set as the thread's current culture.");
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Globalization.NumberFormatInfo" /> that defines the culturally appropriate format of displaying numbers, currency, and percentage.</summary>
		/// <returns>A <see cref="T:System.Globalization.NumberFormatInfo" /> that defines the culturally appropriate format of displaying numbers, currency, and percentage.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Globalization.CultureInfo" /> is for a neutral culture. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Globalization.CultureInfo.NumberFormat" /> property or any of the <see cref="T:System.Globalization.NumberFormatInfo" /> properties is set, and the <see cref="T:System.Globalization.CultureInfo" /> is read-only. </exception>
		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001A35 RID: 6709 RVA: 0x00061F2C File Offset: 0x0006012C
		// (set) Token: 0x06001A36 RID: 6710 RVA: 0x00061FBC File Offset: 0x000601BC
		public virtual NumberFormatInfo NumberFormat
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				this.CheckNeutral();
				if (this.numInfo == null)
				{
					lock (this)
					{
						if (this.numInfo == null)
						{
							this.numInfo = new NumberFormatInfo(this.m_isReadOnly);
							this.construct_number_format();
						}
					}
				}
				return this.numInfo;
			}
			set
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				if (this.m_isReadOnly)
				{
					throw new InvalidOperationException(CultureInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException("NumberFormat");
				}
				this.numInfo = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Globalization.DateTimeFormatInfo" /> that defines the culturally appropriate format of displaying dates and times.</summary>
		/// <returns>A <see cref="T:System.Globalization.DateTimeFormatInfo" /> that defines the culturally appropriate format of displaying dates and times.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Globalization.CultureInfo" /> is for a neutral culture. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Globalization.CultureInfo.DateTimeFormat" /> property or any of the <see cref="T:System.Globalization.DateTimeFormatInfo" /> properties is set, and the <see cref="T:System.Globalization.CultureInfo" /> is read-only. </exception>
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x0006200C File Offset: 0x0006020C
		// (set) Token: 0x06001A38 RID: 6712 RVA: 0x000620BC File Offset: 0x000602BC
		public virtual DateTimeFormatInfo DateTimeFormat
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				this.CheckNeutral();
				if (this.dateTimeInfo == null)
				{
					lock (this)
					{
						if (this.dateTimeInfo == null)
						{
							this.dateTimeInfo = new DateTimeFormatInfo(this.m_isReadOnly);
							this.construct_datetime_format();
							if (this.optional_calendars != null)
							{
								this.dateTimeInfo.Calendar = this.optional_calendars[0];
							}
						}
					}
				}
				return this.dateTimeInfo;
			}
			set
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				if (this.m_isReadOnly)
				{
					throw new InvalidOperationException(CultureInfo.MSG_READONLY);
				}
				if (value == null)
				{
					throw new ArgumentNullException("DateTimeFormat");
				}
				this.dateTimeInfo = value;
			}
		}

		/// <summary>Gets the culture name in the format "&lt;languagefull&gt; (&lt;country/regionfull&gt;)" in the language of the localized version of .NET Framework.</summary>
		/// <returns>The culture name in the format "&lt;languagefull&gt; (&lt;country/regionfull&gt;)" in the language of the localized version of .NET Framework, where &lt;languagefull&gt; is the full name of the language and &lt;country/regionfull&gt; is the full name of the country/region.</returns>
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x0006210C File Offset: 0x0006030C
		public virtual string DisplayName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.displayname;
			}
		}

		/// <summary>Gets the culture name in the format "&lt;languagefull&gt; (&lt;country/regionfull&gt;)" in English.</summary>
		/// <returns>The culture name in the format "&lt;languagefull&gt; (&lt;country/regionfull&gt;)" in English, where &lt;languagefull&gt; is the full name of the language and &lt;country/regionfull&gt; is the full name of the country/region.</returns>
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x00062128 File Offset: 0x00060328
		public virtual string EnglishName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.englishname;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> that represents the culture installed with the operating system.</summary>
		/// <returns>The <see cref="T:System.Globalization.CultureInfo" /> that represents the culture installed with the operating system.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x00062144 File Offset: 0x00060344
		public static CultureInfo InstalledUICulture
		{
			get
			{
				return CultureInfo.GetCultureInfo(CultureInfo.BootstrapCultureID);
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.CultureInfo" /> is read-only.</summary>
		/// <returns>true if the current <see cref="T:System.Globalization.CultureInfo" /> is read-only; otherwise, false. The default is false.</returns>
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x00062150 File Offset: 0x00060350
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		/// <summary>Gets an object that defines how to format the specified type.</summary>
		/// <returns>The value of the <see cref="P:System.Globalization.CultureInfo.NumberFormat" /> property, which is a <see cref="T:System.Globalization.NumberFormatInfo" /> containing the default number format information for the current <see cref="T:System.Globalization.CultureInfo" />, if <paramref name="formatType" /> is the <see cref="T:System.Type" /> object for the <see cref="T:System.Globalization.NumberFormatInfo" /> class.-or- The value of the <see cref="P:System.Globalization.CultureInfo.DateTimeFormat" /> property, which is a <see cref="T:System.Globalization.DateTimeFormatInfo" /> containing the default date and time format information for the current <see cref="T:System.Globalization.CultureInfo" />, if <paramref name="formatType" /> is the <see cref="T:System.Type" /> object for the <see cref="T:System.Globalization.DateTimeFormatInfo" /> class.-or- null, if <paramref name="formatType" /> is any other object.</returns>
		/// <param name="formatType">The <see cref="T:System.Type" /> for which to get a formatting object. This method only supports the <see cref="T:System.Globalization.NumberFormatInfo" /> and <see cref="T:System.Globalization.DateTimeFormatInfo" /> types. </param>
		// Token: 0x06001A3D RID: 6717 RVA: 0x00062158 File Offset: 0x00060358
		public virtual object GetFormat(Type formatType)
		{
			object result = null;
			if (formatType == typeof(NumberFormatInfo))
			{
				result = this.NumberFormat;
			}
			else if (formatType == typeof(DateTimeFormatInfo))
			{
				result = this.DateTimeFormat;
			}
			return result;
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x0006219C File Offset: 0x0006039C
		private void Construct()
		{
			this.construct_internal_locale_from_lcid(this.cultureID);
			this.constructed = true;
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x000621B4 File Offset: 0x000603B4
		private bool ConstructInternalLocaleFromName(string locale)
		{
			string text = locale;
			if (text != null)
			{
				if (CultureInfo.<>f__switch$map1B == null)
				{
					CultureInfo.<>f__switch$map1B = new Dictionary<string, int>(2)
					{
						{
							"zh-hans",
							0
						},
						{
							"zh-hant",
							1
						}
					};
				}
				int num;
				if (CultureInfo.<>f__switch$map1B.TryGetValue(text, out num))
				{
					if (num != 0)
					{
						if (num == 1)
						{
							locale = "zh-cht";
						}
					}
					else
					{
						locale = "zh-chs";
					}
				}
			}
			return this.construct_internal_locale_from_name(locale);
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x00062244 File Offset: 0x00060444
		private bool ConstructInternalLocaleFromLcid(int lcid)
		{
			return this.construct_internal_locale_from_lcid(lcid);
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x00062258 File Offset: 0x00060458
		private static bool ConstructInternalLocaleFromSpecificName(CultureInfo ci, string name)
		{
			return CultureInfo.construct_internal_locale_from_specific_name(ci, name);
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x0006226C File Offset: 0x0006046C
		private static bool ConstructInternalLocaleFromCurrentLocale(CultureInfo ci)
		{
			return CultureInfo.construct_internal_locale_from_current_locale(ci);
		}

		// Token: 0x06001A43 RID: 6723
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool construct_internal_locale_from_lcid(int lcid);

		// Token: 0x06001A44 RID: 6724
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool construct_internal_locale_from_name(string name);

		// Token: 0x06001A45 RID: 6725
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool construct_internal_locale_from_specific_name(CultureInfo ci, string name);

		// Token: 0x06001A46 RID: 6726
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool construct_internal_locale_from_current_locale(CultureInfo ci);

		// Token: 0x06001A47 RID: 6727
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CultureInfo[] internal_get_cultures(bool neutral, bool specific, bool installed);

		// Token: 0x06001A48 RID: 6728
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void construct_datetime_format();

		// Token: 0x06001A49 RID: 6729
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void construct_number_format();

		// Token: 0x06001A4A RID: 6730
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool internal_is_lcid_neutral(int lcid, out bool is_neutral);

		// Token: 0x06001A4B RID: 6731 RVA: 0x0006227C File Offset: 0x0006047C
		private void ConstructInvariant(bool read_only)
		{
			this.cultureID = 127;
			this.numInfo = NumberFormatInfo.InvariantInfo;
			this.dateTimeInfo = DateTimeFormatInfo.InvariantInfo;
			if (!read_only)
			{
				this.numInfo = (NumberFormatInfo)this.numInfo.Clone();
				this.dateTimeInfo = (DateTimeFormatInfo)this.dateTimeInfo.Clone();
			}
			this.textInfo = this.CreateTextInfo(read_only);
			this.m_name = string.Empty;
			this.displayname = (this.englishname = (this.nativename = "Invariant Language (Invariant Country)"));
			this.iso3lang = "IVL";
			this.iso2lang = "iv";
			this.icu_name = "en_US_POSIX";
			this.win3lang = "IVL";
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x00062348 File Offset: 0x00060548
		private TextInfo CreateTextInfo(bool readOnly)
		{
			return new TextInfo(this, this.cultureID, this.textinfo_data, readOnly);
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x00062360 File Offset: 0x00060560
		private static void insert_into_shared_tables(CultureInfo c)
		{
			if (CultureInfo.shared_by_number == null)
			{
				CultureInfo.shared_by_number = new Hashtable();
				CultureInfo.shared_by_name = new Hashtable();
			}
			CultureInfo.shared_by_number[c.cultureID] = c;
			CultureInfo.shared_by_name[c.m_name] = c;
		}

		/// <summary>Retrieves a cached, read-only instance of a culture using the specified culture identifier.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <param name="culture">A culture identifier.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="culture" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="culture" /> specifies a culture that is not supported.</exception>
		// Token: 0x06001A4E RID: 6734 RVA: 0x000623B4 File Offset: 0x000605B4
		public static CultureInfo GetCultureInfo(int culture)
		{
			object obj = CultureInfo.shared_table_lock;
			CultureInfo result;
			lock (obj)
			{
				CultureInfo cultureInfo;
				if (CultureInfo.shared_by_number != null)
				{
					cultureInfo = (CultureInfo.shared_by_number[culture] as CultureInfo);
					if (cultureInfo != null)
					{
						return cultureInfo;
					}
				}
				cultureInfo = new CultureInfo(culture, false, true);
				CultureInfo.insert_into_shared_tables(cultureInfo);
				result = cultureInfo;
			}
			return result;
		}

		/// <summary>Retrieves a cached, read-only instance of a culture using the specified culture name. </summary>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <param name="name">The name of a culture.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> specifies a culture that is not supported.</exception>
		// Token: 0x06001A4F RID: 6735 RVA: 0x0006243C File Offset: 0x0006063C
		public static CultureInfo GetCultureInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			object obj = CultureInfo.shared_table_lock;
			CultureInfo result;
			lock (obj)
			{
				CultureInfo cultureInfo;
				if (CultureInfo.shared_by_name != null)
				{
					cultureInfo = (CultureInfo.shared_by_name[name] as CultureInfo);
					if (cultureInfo != null)
					{
						return cultureInfo;
					}
				}
				cultureInfo = new CultureInfo(name, false, true);
				CultureInfo.insert_into_shared_tables(cultureInfo);
				result = cultureInfo;
			}
			return result;
		}

		/// <summary>Retrieves a cached, read-only instance of a culture. Parameters specify a culture that is initialized with the <see cref="T:System.Globalization.TextInfo" /> and <see cref="T:System.Globalization.CompareInfo" /> objects specified by another culture.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <param name="name">The name of a culture.</param>
		/// <param name="altName">The name of a culture that supplies the <see cref="T:System.Globalization.TextInfo" /> and <see cref="T:System.Globalization.CompareInfo" /> objects used to initialize <paramref name="name" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="altName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> or <paramref name="altName" /> specifies a culture that is not supported.</exception>
		// Token: 0x06001A50 RID: 6736 RVA: 0x000624D0 File Offset: 0x000606D0
		[MonoTODO("Currently it ignores the altName parameter")]
		public static CultureInfo GetCultureInfo(string name, string altName)
		{
			if (name == null)
			{
				throw new ArgumentNullException("null");
			}
			if (altName == null)
			{
				throw new ArgumentNullException("null");
			}
			return CultureInfo.GetCultureInfo(name);
		}

		/// <summary>Deprecated. Retrieves a read-only <see cref="T:System.Globalization.CultureInfo" /> object having linguistic characteristics that are identified by the specified RFC 4646 language tag. </summary>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <param name="name">The name of a language as specified by the RFC 4646 standard.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> does not correspond to a supported culture.</exception>
		// Token: 0x06001A51 RID: 6737 RVA: 0x00062508 File Offset: 0x00060708
		public static CultureInfo GetCultureInfoByIetfLanguageTag(string name)
		{
			if (name != null)
			{
				if (CultureInfo.<>f__switch$map1C == null)
				{
					CultureInfo.<>f__switch$map1C = new Dictionary<string, int>(2)
					{
						{
							"zh-Hans",
							0
						},
						{
							"zh-Hant",
							1
						}
					};
				}
				int num;
				if (CultureInfo.<>f__switch$map1C.TryGetValue(name, out num))
				{
					if (num == 0)
					{
						return CultureInfo.GetCultureInfo("zh-CHS");
					}
					if (num == 1)
					{
						return CultureInfo.GetCultureInfo("zh-CHT");
					}
				}
			}
			return CultureInfo.GetCultureInfo(name);
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x0006258C File Offset: 0x0006078C
		internal static CultureInfo CreateCulture(string name, bool reference)
		{
			bool flag = name.Length == 0;
			bool useUserOverride;
			bool read_only;
			if (reference)
			{
				useUserOverride = !flag;
				read_only = false;
			}
			else
			{
				read_only = false;
				useUserOverride = !flag;
			}
			return new CultureInfo(name, useUserOverride, read_only);
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x000625D8 File Offset: 0x000607D8
		internal unsafe void ConstructCalendars()
		{
			if (this.calendar_data == null)
			{
				this.optional_calendars = new Calendar[]
				{
					new GregorianCalendar(GregorianCalendarTypes.Localized)
				};
				return;
			}
			this.optional_calendars = new Calendar[5];
			for (int i = 0; i < 5; i++)
			{
				int num = this.calendar_data[i];
				Calendar calendar;
				switch (num >> 24)
				{
				case 0:
				{
					GregorianCalendarTypes type = (GregorianCalendarTypes)(num & 16777215);
					calendar = new GregorianCalendar(type);
					break;
				}
				case 1:
					calendar = new HijriCalendar();
					break;
				case 2:
					calendar = new ThaiBuddhistCalendar();
					break;
				default:
					throw new Exception("invalid calendar type:  " + num);
				}
				this.optional_calendars[i] = calendar;
			}
		}

		// Token: 0x04000992 RID: 2450
		private const int NumOptionalCalendars = 5;

		// Token: 0x04000993 RID: 2451
		private const int GregorianTypeMask = 16777215;

		// Token: 0x04000994 RID: 2452
		private const int CalendarTypeBits = 24;

		// Token: 0x04000995 RID: 2453
		private const int InvariantCultureId = 127;

		// Token: 0x04000996 RID: 2454
		private static volatile CultureInfo invariant_culture_info;

		// Token: 0x04000997 RID: 2455
		private static object shared_table_lock = new object();

		// Token: 0x04000998 RID: 2456
		internal static int BootstrapCultureID;

		// Token: 0x04000999 RID: 2457
		private bool m_isReadOnly;

		// Token: 0x0400099A RID: 2458
		private int cultureID;

		// Token: 0x0400099B RID: 2459
		[NonSerialized]
		private int parent_lcid;

		// Token: 0x0400099C RID: 2460
		[NonSerialized]
		private int specific_lcid;

		// Token: 0x0400099D RID: 2461
		[NonSerialized]
		private int datetime_index;

		// Token: 0x0400099E RID: 2462
		[NonSerialized]
		private int number_index;

		// Token: 0x0400099F RID: 2463
		private bool m_useUserOverride;

		// Token: 0x040009A0 RID: 2464
		[NonSerialized]
		private volatile NumberFormatInfo numInfo;

		// Token: 0x040009A1 RID: 2465
		private volatile DateTimeFormatInfo dateTimeInfo;

		// Token: 0x040009A2 RID: 2466
		private volatile TextInfo textInfo;

		// Token: 0x040009A3 RID: 2467
		private string m_name;

		// Token: 0x040009A4 RID: 2468
		[NonSerialized]
		private string displayname;

		// Token: 0x040009A5 RID: 2469
		[NonSerialized]
		private string englishname;

		// Token: 0x040009A6 RID: 2470
		[NonSerialized]
		private string nativename;

		// Token: 0x040009A7 RID: 2471
		[NonSerialized]
		private string iso3lang;

		// Token: 0x040009A8 RID: 2472
		[NonSerialized]
		private string iso2lang;

		// Token: 0x040009A9 RID: 2473
		[NonSerialized]
		private string icu_name;

		// Token: 0x040009AA RID: 2474
		[NonSerialized]
		private string win3lang;

		// Token: 0x040009AB RID: 2475
		[NonSerialized]
		private string territory;

		// Token: 0x040009AC RID: 2476
		private volatile CompareInfo compareInfo;

		// Token: 0x040009AD RID: 2477
		[NonSerialized]
		private unsafe readonly int* calendar_data;

		// Token: 0x040009AE RID: 2478
		[NonSerialized]
		private unsafe readonly void* textinfo_data;

		// Token: 0x040009AF RID: 2479
		[NonSerialized]
		private Calendar[] optional_calendars;

		// Token: 0x040009B0 RID: 2480
		[NonSerialized]
		private CultureInfo parent_culture;

		// Token: 0x040009B1 RID: 2481
		private int m_dataItem;

		// Token: 0x040009B2 RID: 2482
		private Calendar calendar;

		// Token: 0x040009B3 RID: 2483
		[NonSerialized]
		private bool constructed;

		// Token: 0x040009B4 RID: 2484
		[NonSerialized]
		internal byte[] cached_serialized_form;

		// Token: 0x040009B5 RID: 2485
		private static readonly string MSG_READONLY = "This instance is read only";

		// Token: 0x040009B6 RID: 2486
		private static Hashtable shared_by_number;

		// Token: 0x040009B7 RID: 2487
		private static Hashtable shared_by_name;
	}
}
