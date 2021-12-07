using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a type.</summary>
	// Token: 0x02000070 RID: 112
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeTypeReference : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class. </summary>
		// Token: 0x060003B1 RID: 945 RVA: 0x0000D900 File Offset: 0x0000BB00
		public CodeTypeReference()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class using the specified type name.</summary>
		/// <param name="typeName">The name of the type to reference. </param>
		// Token: 0x060003B2 RID: 946 RVA: 0x0000D908 File Offset: 0x0000BB08
		[MonoTODO("We should parse basetype from right to left in 2.0 profile.")]
		public CodeTypeReference(string baseType)
		{
			this.Parse(baseType);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class using the specified type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> to reference. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type " />is null.</exception>
		// Token: 0x060003B3 RID: 947 RVA: 0x0000D918 File Offset: 0x0000BB18
		[MonoTODO("We should parse basetype from right to left in 2.0 profile.")]
		public CodeTypeReference(Type baseType)
		{
			if (baseType == null)
			{
				throw new ArgumentNullException("baseType");
			}
			if (baseType.IsGenericParameter)
			{
				this.baseType = baseType.Name;
				this.referenceOptions = CodeTypeReferenceOptions.GenericTypeParameter;
			}
			else if (baseType.IsGenericTypeDefinition)
			{
				this.baseType = baseType.FullName;
			}
			else if (baseType.IsGenericType)
			{
				this.baseType = baseType.GetGenericTypeDefinition().FullName;
				foreach (Type type in baseType.GetGenericArguments())
				{
					if (type.IsGenericParameter)
					{
						this.TypeArguments.Add(new CodeTypeReference(new CodeTypeParameter(type.Name)));
					}
					else
					{
						this.TypeArguments.Add(new CodeTypeReference(type));
					}
				}
			}
			else if (baseType.IsArray)
			{
				this.arrayRank = baseType.GetArrayRank();
				this.arrayElementType = new CodeTypeReference(baseType.GetElementType());
				this.baseType = this.arrayElementType.BaseType;
			}
			else
			{
				this.Parse(baseType.FullName);
			}
			this.isInterface = baseType.IsInterface;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class using the specified array type and rank.</summary>
		/// <param name="arrayType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the array. </param>
		/// <param name="rank">The number of dimensions in the array. </param>
		// Token: 0x060003B4 RID: 948 RVA: 0x0000DA50 File Offset: 0x0000BC50
		public CodeTypeReference(CodeTypeReference arrayElementType, int arrayRank)
		{
			this.baseType = null;
			this.arrayRank = arrayRank;
			this.arrayElementType = arrayElementType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class using the specified array type name and rank.</summary>
		/// <param name="baseType">The name of the type of the elements of the array. </param>
		/// <param name="rank">The number of dimensions of the array. </param>
		// Token: 0x060003B5 RID: 949 RVA: 0x0000DA70 File Offset: 0x0000BC70
		[MonoTODO("We should parse basetype from right to left in 2.0 profile.")]
		public CodeTypeReference(string baseType, int arrayRank) : this(new CodeTypeReference(baseType), arrayRank)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class using the specified code type parameter. </summary>
		/// <param name="typeParameter">A <see cref="T:System.CodeDom.CodeTypeParameter" /> that represents the type of the type parameter.</param>
		// Token: 0x060003B6 RID: 950 RVA: 0x0000DA80 File Offset: 0x0000BC80
		public CodeTypeReference(CodeTypeParameter typeParameter) : this(typeParameter.Name)
		{
			this.referenceOptions = CodeTypeReferenceOptions.GenericTypeParameter;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class using the specified type name and code type reference option.</summary>
		/// <param name="typeName">The name of the type to reference.</param>
		/// <param name="codeTypeReferenceOption">The code type reference option, one of the <see cref="T:System.CodeDom.CodeTypeReferenceOptions" /> values.</param>
		// Token: 0x060003B7 RID: 951 RVA: 0x0000DA98 File Offset: 0x0000BC98
		public CodeTypeReference(string typeName, CodeTypeReferenceOptions referenceOptions) : this(typeName)
		{
			this.referenceOptions = referenceOptions;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class using the specified type and code type reference.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> to reference.</param>
		/// <param name="codeTypeReferenceOption">The code type reference option, one of the <see cref="T:System.CodeDom.CodeTypeReferenceOptions" /> values. </param>
		// Token: 0x060003B8 RID: 952 RVA: 0x0000DAA8 File Offset: 0x0000BCA8
		public CodeTypeReference(Type type, CodeTypeReferenceOptions referenceOptions) : this(type)
		{
			this.referenceOptions = referenceOptions;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReference" /> class using the specified type name and type arguments.</summary>
		/// <param name="typeName">The name of the type to reference.</param>
		/// <param name="typeArguments">An array of <see cref="T:System.CodeDom.CodeTypeReference" /> values.</param>
		// Token: 0x060003B9 RID: 953 RVA: 0x0000DAB8 File Offset: 0x0000BCB8
		public CodeTypeReference(string typeName, params CodeTypeReference[] typeArguments) : this(typeName)
		{
			this.TypeArguments.AddRange(typeArguments);
			if (this.baseType.IndexOf('`') < 0)
			{
				this.baseType = this.baseType + "`" + this.TypeArguments.Count;
			}
		}

		/// <summary>Gets or sets the type of the elements in the array.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the array elements.</returns>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000DB14 File Offset: 0x0000BD14
		// (set) Token: 0x060003BB RID: 955 RVA: 0x0000DB1C File Offset: 0x0000BD1C
		public CodeTypeReference ArrayElementType
		{
			get
			{
				return this.arrayElementType;
			}
			set
			{
				this.arrayElementType = value;
			}
		}

		/// <summary>Gets or sets the array rank of the array.</summary>
		/// <returns>The number of dimensions of the array.</returns>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000DB28 File Offset: 0x0000BD28
		// (set) Token: 0x060003BD RID: 957 RVA: 0x0000DB30 File Offset: 0x0000BD30
		public int ArrayRank
		{
			get
			{
				return this.arrayRank;
			}
			set
			{
				this.arrayRank = value;
			}
		}

		/// <summary>Gets or sets the name of the type being referenced.</summary>
		/// <returns>The name of the type being referenced.</returns>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000DB3C File Offset: 0x0000BD3C
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0000DB84 File Offset: 0x0000BD84
		public string BaseType
		{
			get
			{
				if (this.arrayElementType != null && this.arrayRank > 0)
				{
					return this.arrayElementType.BaseType;
				}
				if (this.baseType == null)
				{
					return string.Empty;
				}
				return this.baseType;
			}
			set
			{
				this.baseType = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000DB90 File Offset: 0x0000BD90
		internal bool IsInterface
		{
			get
			{
				return this.isInterface;
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000DB98 File Offset: 0x0000BD98
		private void Parse(string baseType)
		{
			if (baseType == null || baseType.Length == 0)
			{
				this.baseType = typeof(void).FullName;
				return;
			}
			int num = baseType.IndexOf('[');
			if (num == -1)
			{
				this.baseType = baseType;
				return;
			}
			int num2 = baseType.LastIndexOf(']');
			if (num2 < num)
			{
				this.baseType = baseType;
				return;
			}
			int num3 = baseType.LastIndexOf('>');
			if (num3 != -1 && num3 > num2)
			{
				this.baseType = baseType;
				return;
			}
			string[] array = baseType.Substring(num + 1, num2 - num - 1).Split(new char[]
			{
				','
			});
			if (num2 - num != array.Length)
			{
				this.baseType = baseType.Substring(0, num);
				int num4 = 0;
				int i = num;
				StringBuilder stringBuilder = new StringBuilder();
				while (i < baseType.Length)
				{
					char c = baseType[i];
					char c2 = c;
					switch (c2)
					{
					case '[':
						if (num4 > 1 && stringBuilder.Length > 0)
						{
							stringBuilder.Append(c);
						}
						num4++;
						break;
					default:
						if (c2 != ',')
						{
							stringBuilder.Append(c);
						}
						else if (num4 > 1)
						{
							while (i + 1 < baseType.Length)
							{
								if (baseType[i + 1] == ']')
								{
									break;
								}
								i++;
							}
						}
						else if (stringBuilder.Length > 0)
						{
							CodeTypeReference value = new CodeTypeReference(stringBuilder.ToString());
							this.TypeArguments.Add(value);
							stringBuilder.Length = 0;
						}
						break;
					case ']':
						num4--;
						if (num4 > 1 && stringBuilder.Length > 0)
						{
							stringBuilder.Append(c);
						}
						if (stringBuilder.Length != 0 && num4 % 2 == 0)
						{
							this.TypeArguments.Add(stringBuilder.ToString());
							stringBuilder.Length = 0;
						}
						break;
					}
					i++;
				}
			}
			else
			{
				this.arrayElementType = new CodeTypeReference(baseType.Substring(0, num));
				this.arrayRank = array.Length;
			}
		}

		/// <summary>Gets or sets the code type reference option.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.CodeDom.CodeTypeReferenceOptions" /> values. </returns>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000DDD0 File Offset: 0x0000BFD0
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
		[ComVisible(false)]
		public CodeTypeReferenceOptions Options
		{
			get
			{
				return this.referenceOptions;
			}
			set
			{
				this.referenceOptions = value;
			}
		}

		/// <summary>Gets the type arguments for the current generic type reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> containing the type arguments for the current <see cref="T:System.CodeDom.CodeTypeReference" /> object.</returns>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000DDE4 File Offset: 0x0000BFE4
		[ComVisible(false)]
		public CodeTypeReferenceCollection TypeArguments
		{
			get
			{
				if (this.typeArguments == null)
				{
					this.typeArguments = new CodeTypeReferenceCollection();
				}
				return this.typeArguments;
			}
		}

		// Token: 0x04000112 RID: 274
		private string baseType;

		// Token: 0x04000113 RID: 275
		private CodeTypeReference arrayElementType;

		// Token: 0x04000114 RID: 276
		private int arrayRank;

		// Token: 0x04000115 RID: 277
		private bool isInterface;

		// Token: 0x04000116 RID: 278
		private bool needsFixup;

		// Token: 0x04000117 RID: 279
		private CodeTypeReferenceCollection typeArguments;

		// Token: 0x04000118 RID: 280
		private CodeTypeReferenceOptions referenceOptions;
	}
}
