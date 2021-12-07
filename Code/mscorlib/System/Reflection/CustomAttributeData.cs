using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection
{
	/// <summary>Provides access to custom attribute data for assemblies, modules, types, members and parameters that are loaded into the reflection-only context.</summary>
	// Token: 0x02000287 RID: 647
	[ComVisible(true)]
	[Serializable]
	public sealed class CustomAttributeData
	{
		// Token: 0x0600212E RID: 8494 RVA: 0x00079894 File Offset: 0x00077A94
		internal CustomAttributeData(ConstructorInfo ctorInfo, object[] ctorArgs, object[] namedArgs)
		{
			this.ctorInfo = ctorInfo;
			this.ctorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>((ctorArgs == null) ? new CustomAttributeTypedArgument[0] : CustomAttributeData.UnboxValues<CustomAttributeTypedArgument>(ctorArgs));
			this.namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>((namedArgs == null) ? new CustomAttributeNamedArgument[0] : CustomAttributeData.UnboxValues<CustomAttributeNamedArgument>(namedArgs));
		}

		/// <summary>Returns a <see cref="T:System.Reflection.ConstructorInfo" /> object representing the constructor that would have initialized the custom attribute.</summary>
		/// <returns>A <see cref="T:System.Reflection.ConstructorInfo" /> object representing the constructor that would have initialized the custom attribute represented by the current instance of the <see cref="T:System.Reflection.CustomAttributeData" /> class.</returns>
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x000798F4 File Offset: 0x00077AF4
		[ComVisible(true)]
		public ConstructorInfo Constructor
		{
			get
			{
				return this.ctorInfo;
			}
		}

		/// <summary>Gets the list of positional arguments specified for the attribute instance represented by the <see cref="T:System.Reflection.CustomAttributeData" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IList`1" /> of <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structures representing the positional arguments specified for the custom attribute instance.</returns>
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06002130 RID: 8496 RVA: 0x000798FC File Offset: 0x00077AFC
		[ComVisible(true)]
		public IList<CustomAttributeTypedArgument> ConstructorArguments
		{
			get
			{
				return this.ctorArgs;
			}
		}

		/// <summary>Gets the list of named arguments specified for the attribute instance represented by the <see cref="T:System.Reflection.CustomAttributeData" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IList`1" /> of <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> structures representing the named arguments specified for the custom attribute instance.</returns>
		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06002131 RID: 8497 RVA: 0x00079904 File Offset: 0x00077B04
		public IList<CustomAttributeNamedArgument> NamedArguments
		{
			get
			{
				return this.namedArgs;
			}
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target assembly.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IList`1" /> of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target assembly.</returns>
		/// <param name="target">The assembly whose custom attribute data is to be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is null.</exception>
		// Token: 0x06002132 RID: 8498 RVA: 0x0007990C File Offset: 0x00077B0C
		public static IList<CustomAttributeData> GetCustomAttributes(Assembly target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target);
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target member.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IList`1" /> of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target member.</returns>
		/// <param name="target">A <see cref="T:System.Reflection.MemberInfo" /> object representing the member whose attribute data is to be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is null.</exception>
		// Token: 0x06002133 RID: 8499 RVA: 0x00079914 File Offset: 0x00077B14
		public static IList<CustomAttributeData> GetCustomAttributes(MemberInfo target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target);
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target module.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IList`1" /> of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target module.</returns>
		/// <param name="target">The module whose custom attribute data is to be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is null.</exception>
		// Token: 0x06002134 RID: 8500 RVA: 0x0007991C File Offset: 0x00077B1C
		public static IList<CustomAttributeData> GetCustomAttributes(Module target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target);
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target parameter.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IList`1" /> of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target parameter.</returns>
		/// <param name="target">A <see cref="T:System.Reflection.ParameterInfo" /> object representing the parameter whose attribute data is to be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is null.</exception>
		// Token: 0x06002135 RID: 8501 RVA: 0x00079924 File Offset: 0x00077B24
		public static IList<CustomAttributeData> GetCustomAttributes(ParameterInfo target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target);
		}

		/// <summary>Returns a string representation of the custom attribute.</summary>
		/// <returns>A string value that represents the custom attribute.</returns>
		// Token: 0x06002136 RID: 8502 RVA: 0x0007992C File Offset: 0x00077B2C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[" + this.ctorInfo.DeclaringType.FullName + "(");
			for (int i = 0; i < this.ctorArgs.Count; i++)
			{
				stringBuilder.Append(this.ctorArgs[i].ToString());
				if (i + 1 < this.ctorArgs.Count)
				{
					stringBuilder.Append(", ");
				}
			}
			if (this.namedArgs.Count > 0)
			{
				stringBuilder.Append(", ");
			}
			for (int j = 0; j < this.namedArgs.Count; j++)
			{
				stringBuilder.Append(this.namedArgs[j].ToString());
				if (j + 1 < this.namedArgs.Count)
				{
					stringBuilder.Append(", ");
				}
			}
			stringBuilder.AppendFormat(")]", new object[0]);
			return stringBuilder.ToString();
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x00079A48 File Offset: 0x00077C48
		private static T[] UnboxValues<T>(object[] values)
		{
			T[] array = new T[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				array[i] = (T)((object)values[i]);
			}
			return array;
		}

		/// <param name="obj"></param>
		// Token: 0x06002138 RID: 8504 RVA: 0x00079A84 File Offset: 0x00077C84
		public override bool Equals(object obj)
		{
			CustomAttributeData customAttributeData = obj as CustomAttributeData;
			if (customAttributeData == null || customAttributeData.ctorInfo != this.ctorInfo || customAttributeData.ctorArgs.Count != this.ctorArgs.Count || customAttributeData.namedArgs.Count != this.namedArgs.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ctorArgs.Count; i++)
			{
				if (this.ctorArgs[i].Equals(customAttributeData.ctorArgs[i]))
				{
					return false;
				}
			}
			for (int j = 0; j < this.namedArgs.Count; j++)
			{
				bool flag = false;
				for (int k = 0; k < customAttributeData.namedArgs.Count; k++)
				{
					if (this.namedArgs[j].Equals(customAttributeData.namedArgs[k]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x00079BAC File Offset: 0x00077DAC
		public override int GetHashCode()
		{
			int num = this.ctorInfo.GetHashCode() << 16;
			for (int i = 0; i < this.ctorArgs.Count; i++)
			{
				num += (num ^ 7 + this.ctorArgs[i].GetHashCode() << i * 4);
			}
			for (int j = 0; j < this.namedArgs.Count; j++)
			{
				num += this.namedArgs[j].GetHashCode() << 5;
			}
			return num;
		}

		// Token: 0x04000C40 RID: 3136
		private ConstructorInfo ctorInfo;

		// Token: 0x04000C41 RID: 3137
		private IList<CustomAttributeTypedArgument> ctorArgs;

		// Token: 0x04000C42 RID: 3138
		private IList<CustomAttributeNamedArgument> namedArgs;
	}
}
