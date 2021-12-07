using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents an attribute declaration.</summary>
	// Token: 0x02000029 RID: 41
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeAttributeDeclaration
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class.</summary>
		// Token: 0x06000179 RID: 377 RVA: 0x0000A90C File Offset: 0x00008B0C
		public CodeAttributeDeclaration()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified name.</summary>
		/// <param name="name">The name of the attribute. </param>
		// Token: 0x0600017A RID: 378 RVA: 0x0000A914 File Offset: 0x00008B14
		public CodeAttributeDeclaration(string name)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified name and arguments.</summary>
		/// <param name="name">The name of the attribute. </param>
		/// <param name="arguments">An array of type <see cref="T:System.CodeDom.CodeAttributeArgument" />  that contains the arguments for the attribute. </param>
		// Token: 0x0600017B RID: 379 RVA: 0x0000A924 File Offset: 0x00008B24
		public CodeAttributeDeclaration(string name, params CodeAttributeArgument[] arguments)
		{
			this.Name = name;
			this.Arguments.AddRange(arguments);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified code type reference.</summary>
		/// <param name="attributeType">The <see cref="T:System.CodeDom.CodeTypeReference" /> that identifies the attribute.</param>
		// Token: 0x0600017C RID: 380 RVA: 0x0000A94C File Offset: 0x00008B4C
		public CodeAttributeDeclaration(CodeTypeReference attributeType)
		{
			this.attribute = attributeType;
			if (attributeType != null)
			{
				this.name = attributeType.BaseType;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified code type reference and arguments.</summary>
		/// <param name="attributeType">The <see cref="T:System.CodeDom.CodeTypeReference" /> that identifies the attribute.</param>
		/// <param name="arguments">An array of type <see cref="T:System.CodeDom.CodeAttributeArgument" /> that contains the arguments for the attribute.</param>
		// Token: 0x0600017D RID: 381 RVA: 0x0000A970 File Offset: 0x00008B70
		public CodeAttributeDeclaration(CodeTypeReference attributeType, params CodeAttributeArgument[] arguments)
		{
			this.attribute = attributeType;
			if (attributeType != null)
			{
				this.name = attributeType.BaseType;
			}
			this.Arguments.AddRange(arguments);
		}

		/// <summary>Gets the arguments for the attribute.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeArgumentCollection" /> that contains the arguments for the attribute.</returns>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000A9A8 File Offset: 0x00008BA8
		public CodeAttributeArgumentCollection Arguments
		{
			get
			{
				if (this.arguments == null)
				{
					this.arguments = new CodeAttributeArgumentCollection();
				}
				return this.arguments;
			}
		}

		/// <summary>Gets or sets the name of the attribute being declared.</summary>
		/// <returns>The name of the attribute.</returns>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000A9C8 File Offset: 0x00008BC8
		// (set) Token: 0x06000180 RID: 384 RVA: 0x0000A9E4 File Offset: 0x00008BE4
		public string Name
		{
			get
			{
				if (this.name == null)
				{
					return string.Empty;
				}
				return this.name;
			}
			set
			{
				this.name = value;
				this.attribute = new CodeTypeReference(this.name);
			}
		}

		/// <summary>Gets the code type reference for the code attribute declaration.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that identifies the <see cref="T:System.CodeDom.CodeAttributeDeclaration" />.</returns>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000AA00 File Offset: 0x00008C00
		public CodeTypeReference AttributeType
		{
			get
			{
				return this.attribute;
			}
		}

		// Token: 0x0400006C RID: 108
		private string name;

		// Token: 0x0400006D RID: 109
		private CodeAttributeArgumentCollection arguments;

		// Token: 0x0400006E RID: 110
		private CodeTypeReference attribute;
	}
}
