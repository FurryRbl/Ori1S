using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression that creates an array.</summary>
	// Token: 0x02000022 RID: 34
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeArrayCreateExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class.</summary>
		// Token: 0x06000131 RID: 305 RVA: 0x0000A368 File Offset: 0x00008568
		public CodeArrayCreateExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and code expression indicating the number of indexes for the array.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> indicating the data type of the array to create. </param>
		/// <param name="size">An expression that indicates the number of indexes of the array to create. </param>
		// Token: 0x06000132 RID: 306 RVA: 0x0000A370 File Offset: 0x00008570
		public CodeArrayCreateExpression(CodeTypeReference createType, CodeExpression size)
		{
			this.createType = createType;
			this.sizeExpression = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and initialization expressions.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the array to create. </param>
		/// <param name="initializers">An array of expressions to use to initialize the array. </param>
		// Token: 0x06000133 RID: 307 RVA: 0x0000A388 File Offset: 0x00008588
		public CodeArrayCreateExpression(CodeTypeReference createType, params CodeExpression[] initializers)
		{
			this.createType = createType;
			this.Initializers.AddRange(initializers);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and number of indexes for the array.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> indicating the data type of the array to create. </param>
		/// <param name="size">The number of indexes of the array to create. </param>
		// Token: 0x06000134 RID: 308 RVA: 0x0000A3A4 File Offset: 0x000085A4
		public CodeArrayCreateExpression(CodeTypeReference createType, int size)
		{
			this.createType = createType;
			this.size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type name and code expression indicating the number of indexes for the array.</summary>
		/// <param name="createType">The name of the data type of the array to create. </param>
		/// <param name="size">An expression that indicates the number of indexes of the array to create. </param>
		// Token: 0x06000135 RID: 309 RVA: 0x0000A3BC File Offset: 0x000085BC
		public CodeArrayCreateExpression(string createType, CodeExpression size)
		{
			this.createType = new CodeTypeReference(createType);
			this.sizeExpression = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type name and initializers.</summary>
		/// <param name="createType">The name of the data type of the array to create. </param>
		/// <param name="initializers">An array of expressions to use to initialize the array. </param>
		// Token: 0x06000136 RID: 310 RVA: 0x0000A3D8 File Offset: 0x000085D8
		public CodeArrayCreateExpression(string createType, params CodeExpression[] initializers)
		{
			this.createType = new CodeTypeReference(createType);
			this.Initializers.AddRange(initializers);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type name and number of indexes for the array.</summary>
		/// <param name="createType">The name of the data type of the array to create. </param>
		/// <param name="size">The number of indexes of the array to create. </param>
		// Token: 0x06000137 RID: 311 RVA: 0x0000A3F8 File Offset: 0x000085F8
		public CodeArrayCreateExpression(string createType, int size)
		{
			this.createType = new CodeTypeReference(createType);
			this.size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and code expression indicating the number of indexes for the array.</summary>
		/// <param name="createType">The data type of the array to create. </param>
		/// <param name="size">An expression that indicates the number of indexes of the array to create. </param>
		// Token: 0x06000138 RID: 312 RVA: 0x0000A414 File Offset: 0x00008614
		public CodeArrayCreateExpression(Type createType, CodeExpression size)
		{
			this.createType = new CodeTypeReference(createType);
			this.sizeExpression = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and initializers.</summary>
		/// <param name="createType">The data type of the array to create. </param>
		/// <param name="initializers">An array of expressions to use to initialize the array. </param>
		// Token: 0x06000139 RID: 313 RVA: 0x0000A430 File Offset: 0x00008630
		public CodeArrayCreateExpression(Type createType, params CodeExpression[] initializers)
		{
			this.createType = new CodeTypeReference(createType);
			this.Initializers.AddRange(initializers);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and number of indexes for the array.</summary>
		/// <param name="createType">The data type of the array to create. </param>
		/// <param name="size">The number of indexes of the array to create. </param>
		// Token: 0x0600013A RID: 314 RVA: 0x0000A450 File Offset: 0x00008650
		public CodeArrayCreateExpression(Type createType, int size)
		{
			this.createType = new CodeTypeReference(createType);
			this.size = size;
		}

		/// <summary>Gets or sets the type of array to create.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the array.</returns>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000A46C File Offset: 0x0000866C
		// (set) Token: 0x0600013C RID: 316 RVA: 0x0000A4A0 File Offset: 0x000086A0
		public CodeTypeReference CreateType
		{
			get
			{
				if (this.createType == null)
				{
					this.createType = new CodeTypeReference(typeof(void));
				}
				return this.createType;
			}
			set
			{
				this.createType = value;
			}
		}

		/// <summary>Gets the initializers with which to initialize the array.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the initialization values.</returns>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000A4AC File Offset: 0x000086AC
		public CodeExpressionCollection Initializers
		{
			get
			{
				if (this.initializers == null)
				{
					this.initializers = new CodeExpressionCollection();
				}
				return this.initializers;
			}
		}

		/// <summary>Gets or sets the expression that indicates the size of the array.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the size of the array.</returns>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000A4CC File Offset: 0x000086CC
		// (set) Token: 0x0600013F RID: 319 RVA: 0x0000A4D4 File Offset: 0x000086D4
		public CodeExpression SizeExpression
		{
			get
			{
				return this.sizeExpression;
			}
			set
			{
				this.sizeExpression = value;
			}
		}

		/// <summary>Gets or sets the number of indexes in the array.</summary>
		/// <returns>The number of indexes in the array.</returns>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000A4E0 File Offset: 0x000086E0
		// (set) Token: 0x06000141 RID: 321 RVA: 0x0000A4E8 File Offset: 0x000086E8
		public int Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000A4F4 File Offset: 0x000086F4
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x04000060 RID: 96
		private CodeTypeReference createType;

		// Token: 0x04000061 RID: 97
		private CodeExpressionCollection initializers;

		// Token: 0x04000062 RID: 98
		private CodeExpression sizeExpression;

		// Token: 0x04000063 RID: 99
		private int size;
	}
}
