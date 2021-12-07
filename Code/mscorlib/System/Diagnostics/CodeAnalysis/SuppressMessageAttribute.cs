using System;

namespace System.Diagnostics.CodeAnalysis
{
	/// <summary>Suppresses reporting of a specific static analysis tool rule violation, allowing multiple suppressions on a single code artifact.</summary>
	// Token: 0x020001E7 RID: 487
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	[Conditional("CODE_ANALYSIS")]
	public sealed class SuppressMessageAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CodeAnalysis.SuppressMessageAttribute" /> class, specifying the category of the static analysis tool and the identifier for an analysis rule. </summary>
		/// <param name="category">The category for the attribute.</param>
		/// <param name="checkId">The identifier of the analysis tool rule the attribute applies to.</param>
		// Token: 0x060018A5 RID: 6309 RVA: 0x0005E040 File Offset: 0x0005C240
		public SuppressMessageAttribute(string category, string checkId)
		{
			this.category = category;
			this.checkId = checkId;
		}

		/// <summary>Gets the category identifying the classification of the attribute.</summary>
		/// <returns>The category identifying the attribute.</returns>
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x0005E058 File Offset: 0x0005C258
		public string Category
		{
			get
			{
				return this.category;
			}
		}

		/// <summary>Gets the identifier of the static analysis tool rule to be suppressed.</summary>
		/// <returns>The identifier of the static analysis tool rule to be suppressed.</returns>
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x0005E060 File Offset: 0x0005C260
		public string CheckId
		{
			get
			{
				return this.checkId;
			}
		}

		/// <summary>Gets or sets the justification for suppressing the code analysis message.</summary>
		/// <returns>The justification for suppressing the message.</returns>
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x0005E068 File Offset: 0x0005C268
		// (set) Token: 0x060018A9 RID: 6313 RVA: 0x0005E070 File Offset: 0x0005C270
		public string Justification
		{
			get
			{
				return this.justification;
			}
			set
			{
				this.justification = value;
			}
		}

		/// <summary>Gets or sets an optional argument expanding on exclusion criteria.</summary>
		/// <returns>A string containing the expanded exclusion criteria.</returns>
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060018AA RID: 6314 RVA: 0x0005E07C File Offset: 0x0005C27C
		// (set) Token: 0x060018AB RID: 6315 RVA: 0x0005E084 File Offset: 0x0005C284
		public string MessageId
		{
			get
			{
				return this.messageId;
			}
			set
			{
				this.messageId = value;
			}
		}

		/// <summary>Gets or sets the scope of the code that is relevant for the attribute.</summary>
		/// <returns>The scope of the code that is relevant for the attribute.</returns>
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x0005E090 File Offset: 0x0005C290
		// (set) Token: 0x060018AD RID: 6317 RVA: 0x0005E098 File Offset: 0x0005C298
		public string Scope
		{
			get
			{
				return this.scope;
			}
			set
			{
				this.scope = value;
			}
		}

		/// <summary>Gets or sets a fully qualified path that represents the target of the attribute.</summary>
		/// <returns>A fully qualified path that represents the target of the attribute.</returns>
		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x0005E0A4 File Offset: 0x0005C2A4
		// (set) Token: 0x060018AF RID: 6319 RVA: 0x0005E0AC File Offset: 0x0005C2AC
		public string Target
		{
			get
			{
				return this.target;
			}
			set
			{
				this.target = value;
			}
		}

		// Token: 0x040008FC RID: 2300
		private string category;

		// Token: 0x040008FD RID: 2301
		private string checkId;

		// Token: 0x040008FE RID: 2302
		private string justification;

		// Token: 0x040008FF RID: 2303
		private string messageId;

		// Token: 0x04000900 RID: 2304
		private string scope;

		// Token: 0x04000901 RID: 2305
		private string target;
	}
}
