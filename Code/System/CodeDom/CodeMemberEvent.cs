using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for an event of a type.</summary>
	// Token: 0x02000048 RID: 72
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeMemberEvent : CodeTypeMember
	{
		/// <summary>Gets or sets the data type that the member event implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> that indicates the data type or types that the member event implements.</returns>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000B90C File Offset: 0x00009B0C
		public CodeTypeReferenceCollection ImplementationTypes
		{
			get
			{
				if (this.implementationTypes == null)
				{
					this.implementationTypes = new CodeTypeReferenceCollection();
				}
				return this.implementationTypes;
			}
		}

		/// <summary>Gets or sets the privately implemented data type, if any.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type that the event privately implements.</returns>
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000B92C File Offset: 0x00009B2C
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000B934 File Offset: 0x00009B34
		public CodeTypeReference PrivateImplementationType
		{
			get
			{
				return this.privateImplementationType;
			}
			set
			{
				this.privateImplementationType = value;
			}
		}

		/// <summary>Gets or sets the data type of the delegate type that handles the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the delegate type that handles the event.</returns>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000B940 File Offset: 0x00009B40
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000B964 File Offset: 0x00009B64
		public CodeTypeReference Type
		{
			get
			{
				if (this.type == null)
				{
					this.type = new CodeTypeReference(string.Empty);
				}
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B970 File Offset: 0x00009B70
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040000B1 RID: 177
		private CodeTypeReferenceCollection implementationTypes;

		// Token: 0x040000B2 RID: 178
		private CodeTypeReference privateImplementationType;

		// Token: 0x040000B3 RID: 179
		private CodeTypeReference type;
	}
}
