using System;

namespace System.Text.RegularExpressions
{
	/// <summary>Provides information about a regular expression that is used to compile a regular expression to a stand-alone assembly. </summary>
	// Token: 0x02000489 RID: 1161
	[Serializable]
	public class RegexCompilationInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexCompilationInfo" /> class that contains information about a regular expression to be included in an assembly. </summary>
		/// <param name="pattern">The regular expression to compile. </param>
		/// <param name="options">The regular expression options to use when compiling the regular expression. </param>
		/// <param name="name">The name of the type that represents the compiled regular expression. </param>
		/// <param name="fullnamespace">The namespace to which the new type belongs. </param>
		/// <param name="ispublic">true to make the compiled regular expression publicly visible; otherwise, false. </param>
		// Token: 0x06002993 RID: 10643 RVA: 0x0008B13C File Offset: 0x0008933C
		public RegexCompilationInfo(string pattern, RegexOptions options, string name, string fullnamespace, bool ispublic)
		{
			this.Pattern = pattern;
			this.Options = options;
			this.Name = name;
			this.Namespace = fullnamespace;
			this.IsPublic = ispublic;
		}

		/// <summary>Gets or sets a value that indicates whether the compiled regular expression has public visibility.</summary>
		/// <returns>true if the regular expression has public visibility; otherwise, false.</returns>
		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06002994 RID: 10644 RVA: 0x0008B174 File Offset: 0x00089374
		// (set) Token: 0x06002995 RID: 10645 RVA: 0x0008B17C File Offset: 0x0008937C
		public bool IsPublic
		{
			get
			{
				return this.isPublic;
			}
			set
			{
				this.isPublic = value;
			}
		}

		/// <summary>Gets or sets the name of the type that represents the compiled regular expression.</summary>
		/// <returns>The name of the new type.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value for this property is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value for this property is an empty string.</exception>
		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06002996 RID: 10646 RVA: 0x0008B188 File Offset: 0x00089388
		// (set) Token: 0x06002997 RID: 10647 RVA: 0x0008B190 File Offset: 0x00089390
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Name");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException("Name");
				}
				this.name = value;
			}
		}

		/// <summary>Gets or sets the namespace to which the new type belongs.</summary>
		/// <returns>The namespace of the new type.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value for this property is null.</exception>
		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06002998 RID: 10648 RVA: 0x0008B1CC File Offset: 0x000893CC
		// (set) Token: 0x06002999 RID: 10649 RVA: 0x0008B1D4 File Offset: 0x000893D4
		public string Namespace
		{
			get
			{
				return this.nspace;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Namespace");
				}
				this.nspace = value;
			}
		}

		/// <summary>Gets or sets the options to use when compiling the regular expression.</summary>
		/// <returns>A bitwise combination of the enumeration values.</returns>
		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x0600299A RID: 10650 RVA: 0x0008B1F0 File Offset: 0x000893F0
		// (set) Token: 0x0600299B RID: 10651 RVA: 0x0008B1F8 File Offset: 0x000893F8
		public RegexOptions Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		/// <summary>Gets or sets the regular expression to compile.</summary>
		/// <returns>The regular expression to compile.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value for this property is null.</exception>
		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x0600299C RID: 10652 RVA: 0x0008B204 File Offset: 0x00089404
		// (set) Token: 0x0600299D RID: 10653 RVA: 0x0008B20C File Offset: 0x0008940C
		public string Pattern
		{
			get
			{
				return this.pattern;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("pattern");
				}
				this.pattern = value;
			}
		}

		// Token: 0x04001A09 RID: 6665
		private string pattern;

		// Token: 0x04001A0A RID: 6666
		private string name;

		// Token: 0x04001A0B RID: 6667
		private string nspace;

		// Token: 0x04001A0C RID: 6668
		private RegexOptions options;

		// Token: 0x04001A0D RID: 6669
		private bool isPublic;
	}
}
