using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Security.Permissions;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents the configuration settings of a language provider. This class cannot be inherited.</summary>
	// Token: 0x02000081 RID: 129
	[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	public sealed class CompilerInfo
	{
		// Token: 0x0600054B RID: 1355 RVA: 0x0001112C File Offset: 0x0000F32C
		internal CompilerInfo()
		{
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00011134 File Offset: 0x0000F334
		internal void Init()
		{
			if (this.inited)
			{
				return;
			}
			this.inited = true;
			this.type = Type.GetType(this.TypeName);
			if (this.type == null)
			{
				return;
			}
			if (!typeof(CodeDomProvider).IsAssignableFrom(this.type))
			{
				this.type = null;
			}
		}

		/// <summary>Gets the type of the configured <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation.</summary>
		/// <returns>A read-only <see cref="T:System.Type" /> instance that represents the configured language provider type.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationException">The language provider is not configured on this computer. </exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Cannot locate the type because it is a null or empty string.-or-Cannot locate the type because the name for the <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> cannot be found in the configuration file.</exception>
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00011194 File Offset: 0x0000F394
		public Type CodeDomProviderType
		{
			get
			{
				if (this.type == null)
				{
					this.type = Type.GetType(this.TypeName, false);
					if (this.type == null)
					{
						throw new ConfigurationErrorsException("Unable to locate compiler type '" + this.TypeName + "'");
					}
				}
				return this.type;
			}
		}

		/// <summary>Returns a value indicating whether the language provider implementation is configured on the computer.</summary>
		/// <returns>true if the language provider implementation type is configured on the computer; otherwise, false.</returns>
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x000111EC File Offset: 0x0000F3EC
		public bool IsCodeDomProviderTypeValid
		{
			get
			{
				return this.type != null;
			}
		}

		/// <summary>Gets the configured compiler settings for the language provider implementation.</summary>
		/// <returns>A read-only <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> instance that contains the compiler options and settings configured for the language provider. </returns>
		// Token: 0x0600054F RID: 1359 RVA: 0x000111FC File Offset: 0x0000F3FC
		public CompilerParameters CreateDefaultCompilerParameters()
		{
			CompilerParameters compilerParameters = new CompilerParameters();
			if (this.CompilerOptions == null)
			{
				compilerParameters.CompilerOptions = string.Empty;
			}
			else
			{
				compilerParameters.CompilerOptions = this.CompilerOptions;
			}
			compilerParameters.WarningLevel = this.WarningLevel;
			return compilerParameters;
		}

		/// <summary>Returns a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> instance for the current language provider settings.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> instance associated with the language provider configuration. </returns>
		// Token: 0x06000550 RID: 1360 RVA: 0x00011244 File Offset: 0x0000F444
		public CodeDomProvider CreateProvider()
		{
			Type codeDomProviderType = this.CodeDomProviderType;
			if (this.ProviderOptions != null && this.ProviderOptions.Count > 0)
			{
				ConstructorInfo constructor = codeDomProviderType.GetConstructor(new Type[]
				{
					typeof(Dictionary<string, string>)
				});
				if (constructor != null)
				{
					return (CodeDomProvider)constructor.Invoke(new object[]
					{
						this.ProviderOptions
					});
				}
			}
			return (CodeDomProvider)Activator.CreateInstance(codeDomProviderType);
		}

		/// <summary>Determines whether the specified object represents the same language provider and compiler settings as the current <see cref="T:System.CodeDom.Compiler.CompilerInfo" />.</summary>
		/// <returns>true if <paramref name="o" /> is a <see cref="T:System.CodeDom.Compiler.CompilerInfo" /> object and its value is the same as this instance; otherwise, false.</returns>
		/// <param name="o">The object to compare with the current <see cref="T:System.CodeDom.Compiler.CompilerInfo" />. </param>
		// Token: 0x06000551 RID: 1361 RVA: 0x000112BC File Offset: 0x0000F4BC
		public override bool Equals(object o)
		{
			if (!(o is CompilerInfo))
			{
				return false;
			}
			CompilerInfo compilerInfo = (CompilerInfo)o;
			return compilerInfo.TypeName == this.TypeName;
		}

		/// <summary>Returns the hash code for the current instance.</summary>
		/// <returns>A 32-bit signed integer hash code for the current <see cref="T:System.CodeDom.Compiler.CompilerInfo" /> instance, suitable for use in hashing algorithms and data structures such as a hash table. </returns>
		// Token: 0x06000552 RID: 1362 RVA: 0x000112F0 File Offset: 0x0000F4F0
		public override int GetHashCode()
		{
			return this.TypeName.GetHashCode();
		}

		/// <summary>Returns the file name extensions supported by the language provider.</summary>
		/// <returns>An array of file name extensions supported by the language provider.</returns>
		// Token: 0x06000553 RID: 1363 RVA: 0x00011300 File Offset: 0x0000F500
		public string[] GetExtensions()
		{
			return this.Extensions.Split(new char[]
			{
				';'
			});
		}

		/// <summary>Gets the language names supported by the language provider.</summary>
		/// <returns>An array of language names supported by the language provider.</returns>
		// Token: 0x06000554 RID: 1364 RVA: 0x00011318 File Offset: 0x0000F518
		public string[] GetLanguages()
		{
			return this.Languages.Split(new char[]
			{
				';'
			});
		}

		// Token: 0x0400013D RID: 317
		internal string Languages;

		// Token: 0x0400013E RID: 318
		internal string Extensions;

		// Token: 0x0400013F RID: 319
		internal string TypeName;

		// Token: 0x04000140 RID: 320
		internal int WarningLevel;

		// Token: 0x04000141 RID: 321
		internal string CompilerOptions;

		// Token: 0x04000142 RID: 322
		internal Dictionary<string, string> ProviderOptions;

		// Token: 0x04000143 RID: 323
		private bool inited;

		// Token: 0x04000144 RID: 324
		private Type type;
	}
}
