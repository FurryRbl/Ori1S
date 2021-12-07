using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents the version number for an assembly, operating system, or the common language runtime. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000197 RID: 407
	[ComVisible(true)]
	[Serializable]
	public sealed class Version : IComparable, ICloneable, IComparable<Version>, IEquatable<Version>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Version" /> class.</summary>
		// Token: 0x0600147D RID: 5245 RVA: 0x00052980 File Offset: 0x00050B80
		public Version()
		{
			this.CheckedSet(2, 0, 0, -1, -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Version" /> class using the specified string.</summary>
		/// <param name="version">A string containing the major, minor, build, and revision numbers, where each number is delimited with a period character ('.'). </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="version" /> has fewer than two components or more than four components. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="version" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A major, minor, build, or revision component is less than zero. </exception>
		/// <exception cref="T:System.FormatException">At least one component of <paramref name="version" /> does not parse to an integer. </exception>
		/// <exception cref="T:System.OverflowException">At least one component of <paramref name="version" /> represents a number greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x0600147E RID: 5246 RVA: 0x000529A0 File Offset: 0x00050BA0
		public Version(string version)
		{
			int major = -1;
			int minor = -1;
			int build = -1;
			int revision = -1;
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			string[] array = version.Split(new char[]
			{
				'.'
			});
			int num = array.Length;
			if (num < 2 || num > 4)
			{
				throw new ArgumentException(Locale.GetText("There must be 2, 3 or 4 components in the version string."));
			}
			if (num > 0)
			{
				major = int.Parse(array[0]);
			}
			if (num > 1)
			{
				minor = int.Parse(array[1]);
			}
			if (num > 2)
			{
				build = int.Parse(array[2]);
			}
			if (num > 3)
			{
				revision = int.Parse(array[3]);
			}
			this.CheckedSet(num, major, minor, build, revision);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Version" /> class using the specified major and minor values.</summary>
		/// <param name="major">The major version number. </param>
		/// <param name="minor">The minor version number. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="major" /> or <paramref name="minor" /> is less than zero. </exception>
		// Token: 0x0600147F RID: 5247 RVA: 0x00052A54 File Offset: 0x00050C54
		public Version(int major, int minor)
		{
			this.CheckedSet(2, major, minor, 0, 0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Version" /> class using the specified major, minor, and build values.</summary>
		/// <param name="major">The major version number. </param>
		/// <param name="minor">The minor version number. </param>
		/// <param name="build">The build number. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="major" />, <paramref name="minor" />, or <paramref name="build" /> is less than zero. </exception>
		// Token: 0x06001480 RID: 5248 RVA: 0x00052A74 File Offset: 0x00050C74
		public Version(int major, int minor, int build)
		{
			this.CheckedSet(3, major, minor, build, 0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Version" /> class with the specified major, minor, build, and revision numbers.</summary>
		/// <param name="major">The major version number. </param>
		/// <param name="minor">The minor version number. </param>
		/// <param name="build">The build number. </param>
		/// <param name="revision">The revision number. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="major" />, <paramref name="minor" />, <paramref name="build" />, or <paramref name="revision" /> is less than zero. </exception>
		// Token: 0x06001481 RID: 5249 RVA: 0x00052A94 File Offset: 0x00050C94
		public Version(int major, int minor, int build, int revision)
		{
			this.CheckedSet(4, major, minor, build, revision);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00052AB4 File Offset: 0x00050CB4
		private void CheckedSet(int defined, int major, int minor, int build, int revision)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major");
			}
			this._Major = major;
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor");
			}
			this._Minor = minor;
			if (defined == 2)
			{
				this._Build = -1;
				this._Revision = -1;
				return;
			}
			if (build < 0)
			{
				throw new ArgumentOutOfRangeException("build");
			}
			this._Build = build;
			if (defined == 3)
			{
				this._Revision = -1;
				return;
			}
			if (revision < 0)
			{
				throw new ArgumentOutOfRangeException("revision");
			}
			this._Revision = revision;
		}

		/// <summary>Gets the value of the build component of the version number for the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>The build number, or -1 if the build number is undefined.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x00052B50 File Offset: 0x00050D50
		public int Build
		{
			get
			{
				return this._Build;
			}
		}

		/// <summary>Gets the value of the major component of the version number for the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>The major version number.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x00052B58 File Offset: 0x00050D58
		public int Major
		{
			get
			{
				return this._Major;
			}
		}

		/// <summary>Gets the value of the minor component of the version number for the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>The minor version number.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x00052B60 File Offset: 0x00050D60
		public int Minor
		{
			get
			{
				return this._Minor;
			}
		}

		/// <summary>Gets the value of the revision component of the version number for the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>The revision number, or -1 if the revision number is undefined.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x00052B68 File Offset: 0x00050D68
		public int Revision
		{
			get
			{
				return this._Revision;
			}
		}

		/// <summary>Gets the high 16 bits of the revision number.</summary>
		/// <returns>A 16-bit signed integer.</returns>
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x00052B70 File Offset: 0x00050D70
		public short MajorRevision
		{
			get
			{
				return (short)(this._Revision >> 16);
			}
		}

		/// <summary>Gets the low 16 bits of the revision number.</summary>
		/// <returns>A 16-bit signed integer.</returns>
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x00052B7C File Offset: 0x00050D7C
		public short MinorRevision
		{
			get
			{
				return (short)this._Revision;
			}
		}

		/// <summary>Returns a new <see cref="T:System.Version" /> object whose value is the same as the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>A new <see cref="T:System.Object" /> whose values are a copy of the current <see cref="T:System.Version" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001489 RID: 5257 RVA: 0x00052B88 File Offset: 0x00050D88
		public object Clone()
		{
			if (this._Build == -1)
			{
				return new Version(this._Major, this._Minor);
			}
			if (this._Revision == -1)
			{
				return new Version(this._Major, this._Minor, this._Build);
			}
			return new Version(this._Major, this._Minor, this._Build, this._Revision);
		}

		/// <summary>Compares the current <see cref="T:System.Version" /> object to a specified object and returns an indication of their relative values.</summary>
		/// <returns>Return Value Description Less than zero The current <see cref="T:System.Version" /> object is a version before <paramref name="version" />. Zero The current <see cref="T:System.Version" /> object is the same version as <paramref name="version" />. Greater than zero The current <see cref="T:System.Version" /> object is a version subsequent to <paramref name="version" />.-or- <paramref name="version" /> is null. </returns>
		/// <param name="version">An object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="version" /> is not of type <see cref="T:System.Version" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600148A RID: 5258 RVA: 0x00052BF4 File Offset: 0x00050DF4
		public int CompareTo(object version)
		{
			if (version == null)
			{
				return 1;
			}
			if (!(version is Version))
			{
				throw new ArgumentException(Locale.GetText("Argument to Version.CompareTo must be a Version."));
			}
			return this.CompareTo((Version)version);
		}

		/// <summary>Returns a value indicating whether the current <see cref="T:System.Version" /> object is equal to a specified object.</summary>
		/// <returns>true if the current <see cref="T:System.Version" /> object and <paramref name="obj" /> are both <see cref="T:System.Version" /> objects, and every component of the current <see cref="T:System.Version" /> object matches the corresponding component of <paramref name="obj" />; otherwise, false.</returns>
		/// <param name="obj">An object to compare with the current <see cref="T:System.Version" /> object, or null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600148B RID: 5259 RVA: 0x00052C28 File Offset: 0x00050E28
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Version);
		}

		/// <summary>Compares the current <see cref="T:System.Version" /> object to a specified <see cref="T:System.Version" /> object and returns an indication of their relative values.</summary>
		/// <returns>Return Value Description Less than zero The current <see cref="T:System.Version" /> object is a version before <paramref name="value" />. Zero The current <see cref="T:System.Version" /> object is the same version as <paramref name="value" />. Greater than zero The current <see cref="T:System.Version" /> object is a version subsequent to <paramref name="value" />. -or-<paramref name="value" /> is null.</returns>
		/// <param name="value">A <see cref="T:System.Version" /> object to compare to the current <see cref="T:System.Version" /> object, or null.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600148C RID: 5260 RVA: 0x00052C38 File Offset: 0x00050E38
		public int CompareTo(Version value)
		{
			if (value == null)
			{
				return 1;
			}
			if (this._Major > value._Major)
			{
				return 1;
			}
			if (this._Major < value._Major)
			{
				return -1;
			}
			if (this._Minor > value._Minor)
			{
				return 1;
			}
			if (this._Minor < value._Minor)
			{
				return -1;
			}
			if (this._Build > value._Build)
			{
				return 1;
			}
			if (this._Build < value._Build)
			{
				return -1;
			}
			if (this._Revision > value._Revision)
			{
				return 1;
			}
			if (this._Revision < value._Revision)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Returns a value indicating whether the current <see cref="T:System.Version" /> object and a specified <see cref="T:System.Version" /> object represent the same value.</summary>
		/// <returns>true if every component of the current <see cref="T:System.Version" /> object matches the corresponding component of the <paramref name="obj" /> parameter; otherwise, false.</returns>
		/// <param name="obj">A <see cref="T:System.Version" /> object to compare to the current <see cref="T:System.Version" /> object, or null.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600148D RID: 5261 RVA: 0x00052CEC File Offset: 0x00050EEC
		public bool Equals(Version obj)
		{
			return obj != null && obj._Major == this._Major && obj._Minor == this._Minor && obj._Build == this._Build && obj._Revision == this._Revision;
		}

		/// <summary>Returns a hash code for the current <see cref="T:System.Version" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600148E RID: 5262 RVA: 0x00052D4C File Offset: 0x00050F4C
		public override int GetHashCode()
		{
			return this._Revision << 24 | this._Build << 16 | this._Minor << 8 | this._Major;
		}

		/// <summary>Converts the value of the current <see cref="T:System.Version" /> object to its equivalent <see cref="T:System.String" /> representation.</summary>
		/// <returns>The <see cref="T:System.String" /> representation of the values of the major, minor, build, and revision components of the current <see cref="T:System.Version" /> object, as depicted in the following format. Each component is separated by a period character ('.'). Square brackets ('[' and ']') indicate a component that will not appear in the return value if the component is not defined: major.minor[.build[.revision]] For example, if you create a <see cref="T:System.Version" /> object using the constructor Version(1,1), the returned string is "1.1". If you create a <see cref="T:System.Version" /> object using the constructor Version(1,3,4,2), the returned string is "1.3.4.2".</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600148F RID: 5263 RVA: 0x00052D74 File Offset: 0x00050F74
		public override string ToString()
		{
			string text = this._Major.ToString() + "." + this._Minor.ToString();
			if (this._Build != -1)
			{
				text = text + "." + this._Build.ToString();
			}
			if (this._Revision != -1)
			{
				text = text + "." + this._Revision.ToString();
			}
			return text;
		}

		/// <summary>Converts the value of the current <see cref="T:System.Version" /> object to its equivalent <see cref="T:System.String" /> representation. A specified count indicates the number of components to return.</summary>
		/// <returns>The <see cref="T:System.String" /> representation of the values of the major, minor, build, and revision components of the current <see cref="T:System.Version" /> object, each separated by a period character ('.'). The <paramref name="fieldCount" /> parameter determines how many components are returned.fieldCount Return Value 0 An empty string (""). 1 major 2 major.minor 3 major.minor.build 4 major.minor.build.revision For example, if you create <see cref="T:System.Version" /> object using the constructor Version(1,3,5), ToString(2) returns "1.3" and ToString(4) throws an exception.</returns>
		/// <param name="fieldCount">The number of components to return. The <paramref name="fieldCount" /> ranges from 0 to 4. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fieldCount" /> is less than 0, or more than 4.-or- <paramref name="fieldCount" /> is more than the number of components defined in the current <see cref="T:System.Version" /> object. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001490 RID: 5264 RVA: 0x00052DEC File Offset: 0x00050FEC
		public string ToString(int fieldCount)
		{
			if (fieldCount == 0)
			{
				return string.Empty;
			}
			if (fieldCount == 1)
			{
				return this._Major.ToString();
			}
			if (fieldCount == 2)
			{
				return this._Major.ToString() + "." + this._Minor.ToString();
			}
			if (fieldCount == 3)
			{
				if (this._Build == -1)
				{
					throw new ArgumentException(Locale.GetText("fieldCount is larger than the number of components defined in this instance."));
				}
				return string.Concat(new string[]
				{
					this._Major.ToString(),
					".",
					this._Minor.ToString(),
					".",
					this._Build.ToString()
				});
			}
			else
			{
				if (fieldCount != 4)
				{
					throw new ArgumentException(Locale.GetText("Invalid fieldCount parameter: ") + fieldCount.ToString());
				}
				if (this._Build == -1 || this._Revision == -1)
				{
					throw new ArgumentException(Locale.GetText("fieldCount is larger than the number of components defined in this instance."));
				}
				return string.Concat(new string[]
				{
					this._Major.ToString(),
					".",
					this._Minor.ToString(),
					".",
					this._Build.ToString(),
					".",
					this._Revision.ToString()
				});
			}
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x00052F50 File Offset: 0x00051150
		internal static Version CreateFromString(string info)
		{
			int major = 0;
			int minor = 0;
			int build = 0;
			int revision = 0;
			int num = 1;
			int num2 = -1;
			if (info == null)
			{
				return new Version(0, 0, 0, 0);
			}
			foreach (char c in info)
			{
				if (char.IsDigit(c))
				{
					if (num2 < 0)
					{
						num2 = (int)(c - '0');
					}
					else
					{
						num2 = num2 * 10 + (int)(c - '0');
					}
				}
				else if (num2 >= 0)
				{
					switch (num)
					{
					case 1:
						major = num2;
						break;
					case 2:
						minor = num2;
						break;
					case 3:
						build = num2;
						break;
					case 4:
						revision = num2;
						break;
					}
					num2 = -1;
					num++;
				}
				if (num == 5)
				{
					break;
				}
			}
			if (num2 >= 0)
			{
				switch (num)
				{
				case 1:
					major = num2;
					break;
				case 2:
					minor = num2;
					break;
				case 3:
					build = num2;
					break;
				case 4:
					revision = num2;
					break;
				}
			}
			return new Version(major, minor, build, revision);
		}

		/// <summary>Determines whether two specified <see cref="T:System.Version" /> objects are equal.</summary>
		/// <returns>true if <paramref name="v1" /> equals <paramref name="v2" />; otherwise, false.</returns>
		/// <param name="v1">The first <see cref="T:System.Version" /> object. </param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001492 RID: 5266 RVA: 0x00053088 File Offset: 0x00051288
		public static bool operator ==(Version v1, Version v2)
		{
			return object.Equals(v1, v2);
		}

		/// <summary>Determines whether two specified <see cref="T:System.Version" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="v1" /> does not equal <paramref name="v2" />; otherwise, false.</returns>
		/// <param name="v1">The first <see cref="T:System.Version" /> object. </param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001493 RID: 5267 RVA: 0x00053094 File Offset: 0x00051294
		public static bool operator !=(Version v1, Version v2)
		{
			return !object.Equals(v1, v2);
		}

		/// <summary>Determines whether the first specified <see cref="T:System.Version" /> object is greater than the second specified <see cref="T:System.Version" /> object.</summary>
		/// <returns>true if <paramref name="v1" /> is greater than <paramref name="v2" />; otherwise, false.</returns>
		/// <param name="v1">The first <see cref="T:System.Version" /> object. </param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001494 RID: 5268 RVA: 0x000530A0 File Offset: 0x000512A0
		public static bool operator >(Version v1, Version v2)
		{
			return v1.CompareTo(v2) > 0;
		}

		/// <summary>Determines whether the first specified <see cref="T:System.Version" /> object is greater than or equal to the second specified <see cref="T:System.Version" /> object.</summary>
		/// <returns>true if <paramref name="v1" /> is greater than or equal to <paramref name="v2" />; otherwise, false.</returns>
		/// <param name="v1">The first <see cref="T:System.Version" /> object. </param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001495 RID: 5269 RVA: 0x000530AC File Offset: 0x000512AC
		public static bool operator >=(Version v1, Version v2)
		{
			return v1.CompareTo(v2) >= 0;
		}

		/// <summary>Determines whether the first specified <see cref="T:System.Version" /> object is less than the second specified <see cref="T:System.Version" /> object.</summary>
		/// <returns>true if <paramref name="v1" /> is less than <paramref name="v2" />; otherwise, false.</returns>
		/// <param name="v1">The first <see cref="T:System.Version" /> object. </param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="v1" /> is null. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001496 RID: 5270 RVA: 0x000530BC File Offset: 0x000512BC
		public static bool operator <(Version v1, Version v2)
		{
			return v1.CompareTo(v2) < 0;
		}

		/// <summary>Determines whether the first specified <see cref="T:System.Version" /> object is less than or equal to the second <see cref="T:System.Version" /> object.</summary>
		/// <returns>true if <paramref name="v1" /> is less than or equal to <paramref name="v2" />; otherwise, false.</returns>
		/// <param name="v1">The first <see cref="T:System.Version" /> object. </param>
		/// <param name="v2">The second <see cref="T:System.Version" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="v1" /> is null. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001497 RID: 5271 RVA: 0x000530C8 File Offset: 0x000512C8
		public static bool operator <=(Version v1, Version v2)
		{
			return v1.CompareTo(v2) <= 0;
		}

		// Token: 0x04000821 RID: 2081
		private const int UNDEFINED = -1;

		// Token: 0x04000822 RID: 2082
		private int _Major;

		// Token: 0x04000823 RID: 2083
		private int _Minor;

		// Token: 0x04000824 RID: 2084
		private int _Build;

		// Token: 0x04000825 RID: 2085
		private int _Revision;
	}
}
