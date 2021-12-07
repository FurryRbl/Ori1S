using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents a set of options used by a code generator.</summary>
	// Token: 0x0200007B RID: 123
	[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	public class CodeGeneratorOptions
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> class.</summary>
		// Token: 0x060004F8 RID: 1272 RVA: 0x00010590 File Offset: 0x0000E790
		public CodeGeneratorOptions()
		{
			this.properties = new System.Collections.Specialized.ListDictionary();
		}

		/// <summary>Gets or sets a value indicating whether to insert blank lines between members.</summary>
		/// <returns>true if blank lines should be inserted; otherwise, false. By default, the value of this property is true.</returns>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x000105A4 File Offset: 0x0000E7A4
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x000105D4 File Offset: 0x0000E7D4
		public bool BlankLinesBetweenMembers
		{
			get
			{
				object obj = this.properties["BlankLinesBetweenMembers"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.properties["BlankLinesBetweenMembers"] = value;
			}
		}

		/// <summary>Gets or sets the style to use for bracing.</summary>
		/// <returns>A string containing the bracing style to use.</returns>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x000105EC File Offset: 0x0000E7EC
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x00010620 File Offset: 0x0000E820
		public string BracingStyle
		{
			get
			{
				object obj = this.properties["BracingStyle"];
				return (obj != null) ? ((string)obj) : "Block";
			}
			set
			{
				this.properties["BracingStyle"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to append an else, catch, or finally block, including brackets, at the closing line of each previous if or try block.</summary>
		/// <returns>true if an else should be appended; otherwise, false. The default value of this property is false.</returns>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00010634 File Offset: 0x0000E834
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x00010664 File Offset: 0x0000E864
		public bool ElseOnClosing
		{
			get
			{
				object obj = this.properties["ElseOnClosing"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.properties["ElseOnClosing"] = value;
			}
		}

		/// <summary>Gets or sets the string to use for indentations.</summary>
		/// <returns>A string containing the characters to use for indentations.</returns>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0001067C File Offset: 0x0000E87C
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x000106B0 File Offset: 0x0000E8B0
		public string IndentString
		{
			get
			{
				object obj = this.properties["IndentString"];
				return (obj != null) ? ((string)obj) : "    ";
			}
			set
			{
				this.properties["IndentString"] = value;
			}
		}

		/// <summary>Gets or sets the object at the specified index.</summary>
		/// <returns>The object associated with the specified name. If no object associated with the specified name exists in the collection, null.</returns>
		/// <param name="index">The name associated with the object to retrieve. </param>
		// Token: 0x170000DD RID: 221
		public object this[string index]
		{
			get
			{
				return this.properties[index];
			}
			set
			{
				this.properties[index] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to generate members in the order in which they occur in member collections.</summary>
		/// <returns>true to generate the members in the order in which they occur in the member collection; otherwise, false. The default value of this property is false.</returns>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x000106E4 File Offset: 0x0000E8E4
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x00010714 File Offset: 0x0000E914
		[ComVisible(false)]
		public bool VerbatimOrder
		{
			get
			{
				object obj = this.properties["VerbatimOrder"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.properties["VerbatimOrder"] = value;
			}
		}

		// Token: 0x0400012B RID: 299
		private IDictionary properties;
	}
}
