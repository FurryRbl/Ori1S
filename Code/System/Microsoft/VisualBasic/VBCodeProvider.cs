using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Permissions;

namespace Microsoft.VisualBasic
{
	/// <summary>Provides access to instances of the Visual Basic code generator and code compiler.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000011 RID: 17
	[PermissionSet((SecurityAction)15, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
	public class VBCodeProvider : System.CodeDom.Compiler.CodeDomProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.VisualBasic.VBCodeProvider" /> class. </summary>
		// Token: 0x060000E8 RID: 232 RVA: 0x00009DC0 File Offset: 0x00007FC0
		public VBCodeProvider()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.VisualBasic.VBCodeProvider" /> class by using the specified provider options. </summary>
		/// <param name="providerOptions">A <see cref="T:System.Collections.Generic.IDictionary`2" /> object that contains the provider options from the configuration file.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerOptions" /> is null.</exception>
		// Token: 0x060000E9 RID: 233 RVA: 0x00009DC8 File Offset: 0x00007FC8
		public VBCodeProvider(IDictionary<string, string> providerOptions)
		{
		}

		/// <summary>Gets the file name extension to use when creating source code files.</summary>
		/// <returns>The file name extension to use for generated source code files.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00009DD0 File Offset: 0x00007FD0
		public override string FileExtension
		{
			get
			{
				return "vb";
			}
		}

		/// <summary>Gets a language features identifier.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.LanguageOptions" /> that indicates special features of the language.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00009DD8 File Offset: 0x00007FD8
		public override System.CodeDom.Compiler.LanguageOptions LanguageOptions
		{
			get
			{
				return System.CodeDom.Compiler.LanguageOptions.CaseInsensitive;
			}
		}

		/// <summary>Gets an instance of the Visual Basic code compiler.</summary>
		/// <returns>An instance of the Visual Basic <see cref="T:System.CodeDom.Compiler.ICodeCompiler" /> implementation.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060000EC RID: 236 RVA: 0x00009DDC File Offset: 0x00007FDC
		[Obsolete("Use CodeDomProvider class")]
		public override System.CodeDom.Compiler.ICodeCompiler CreateCompiler()
		{
			return new VBCodeCompiler();
		}

		/// <summary>Gets an instance of the Visual Basic code generator.</summary>
		/// <returns>An instance of the Visual Basic <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> implementation.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060000ED RID: 237 RVA: 0x00009DE4 File Offset: 0x00007FE4
		[Obsolete("Use CodeDomProvider class")]
		public override System.CodeDom.Compiler.ICodeGenerator CreateGenerator()
		{
			return new VBCodeGenerator();
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type of object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type.</returns>
		/// <param name="type">The type of object to retrieve a type converter for. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060000EE RID: 238 RVA: 0x00009DEC File Offset: 0x00007FEC
		public override System.ComponentModel.TypeConverter GetConverter(Type type)
		{
			return System.ComponentModel.TypeDescriptor.GetConverter(type);
		}

		/// <summary>Generates code for the specified class member using the specified text writer and code generator options.</summary>
		/// <param name="member">A <see cref="T:System.CodeDom.CodeTypeMember" /> to generate code for.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to write to.</param>
		/// <param name="options">The <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> to use when generating the code.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060000EF RID: 239 RVA: 0x00009DF4 File Offset: 0x00007FF4
		[MonoTODO]
		public override void GenerateCodeFromMember(System.CodeDom.CodeTypeMember member, TextWriter writer, System.CodeDom.Compiler.CodeGeneratorOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
