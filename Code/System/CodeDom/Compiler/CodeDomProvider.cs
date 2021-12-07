using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides a base class for <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementations. This class is abstract.</summary>
	// Token: 0x02000078 RID: 120
	[ComVisible(true)]
	[System.ComponentModel.ToolboxItem(false)]
	public abstract class CodeDomProvider : System.ComponentModel.Component
	{
		/// <summary>Gets the default file name extension to use for source code files in the current language.</summary>
		/// <returns>A file name extension corresponding to the extension of the source files of the current language. The base implementation always returns <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000E4D8 File Offset: 0x0000C6D8
		public virtual string FileExtension
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets a language features identifier.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.LanguageOptions" /> that indicates special features of the language.</returns>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
		public virtual LanguageOptions LanguageOptions
		{
			get
			{
				return LanguageOptions.None;
			}
		}

		/// <summary>When overridden in a derived class, creates a new code compiler. </summary>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeCompiler" /> that can be used for compilation of <see cref="N:System.CodeDom" /> based source code representations. </returns>
		// Token: 0x0600042C RID: 1068
		[Obsolete("ICodeCompiler is obsolete")]
		public abstract ICodeCompiler CreateCompiler();

		/// <summary>When overridden in a derived class, creates a new code generator.</summary>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> that can be used to generate <see cref="N:System.CodeDom" /> based source code representations.</returns>
		// Token: 0x0600042D RID: 1069
		[Obsolete("ICodeGenerator is obsolete")]
		public abstract ICodeGenerator CreateGenerator();

		/// <summary>When overridden in a derived class, creates a new code generator using the specified file name for output.</summary>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> that can be used to generate <see cref="N:System.CodeDom" /> based source code representations.</returns>
		/// <param name="fileName">The file name to output to. </param>
		// Token: 0x0600042E RID: 1070 RVA: 0x0000E4E4 File Offset: 0x0000C6E4
		public virtual ICodeGenerator CreateGenerator(string fileName)
		{
			return this.CreateGenerator();
		}

		/// <summary>When overridden in a derived class, creates a new code generator using the specified <see cref="T:System.IO.TextWriter" /> for output.</summary>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> that can be used to generate <see cref="N:System.CodeDom" /> based source code representations.</returns>
		/// <param name="output">A <see cref="T:System.IO.TextWriter" /> to use to output. </param>
		// Token: 0x0600042F RID: 1071 RVA: 0x0000E4EC File Offset: 0x0000C6EC
		public virtual ICodeGenerator CreateGenerator(TextWriter output)
		{
			return this.CreateGenerator();
		}

		/// <summary>When overridden in a derived class, creates a new code parser.</summary>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeParser" /> that can be used to parse source code. The base implementation always returns null.</returns>
		// Token: 0x06000430 RID: 1072 RVA: 0x0000E4F4 File Offset: 0x0000C6F4
		[Obsolete("ICodeParser is obsolete")]
		public virtual ICodeParser CreateParser()
		{
			return null;
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.TypeConverter" /> for the specified data type.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type, or null if a <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type cannot be found.</returns>
		/// <param name="type">The type of object to retrieve a type converter for. </param>
		// Token: 0x06000431 RID: 1073 RVA: 0x0000E4F8 File Offset: 0x0000C6F8
		public virtual System.ComponentModel.TypeConverter GetConverter(Type type)
		{
			return System.ComponentModel.TypeDescriptor.GetConverter(type);
		}

		/// <summary>Compiles an assembly based on the <see cref="N:System.CodeDom" /> trees contained in the specified array of <see cref="T:System.CodeDom.CodeCompileUnit" /> objects, using the specified compiler settings.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> object that indicates the results of the compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the settings for the compilation.</param>
		/// <param name="compilationUnits">An array of type <see cref="T:System.CodeDom.CodeCompileUnit" /> that indicates the code to compile.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateCompiler" /> method is overridden in a derived class.</exception>
		// Token: 0x06000432 RID: 1074 RVA: 0x0000E500 File Offset: 0x0000C700
		public virtual CompilerResults CompileAssemblyFromDom(CompilerParameters options, params CodeCompileUnit[] compilationUnits)
		{
			ICodeCompiler codeCompiler = this.CreateCompiler();
			if (codeCompiler == null)
			{
				throw this.GetNotImplemented();
			}
			return codeCompiler.CompileAssemblyFromDomBatch(options, compilationUnits);
		}

		/// <summary>Compiles an assembly from the source code contained in the specified files, using the specified compiler settings.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> object that indicates the results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the settings for the compilation. </param>
		/// <param name="fileNames">An array of the names of the files to compile. </param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateCompiler" /> method is overridden in a derived class.</exception>
		// Token: 0x06000433 RID: 1075 RVA: 0x0000E52C File Offset: 0x0000C72C
		public virtual CompilerResults CompileAssemblyFromFile(CompilerParameters options, params string[] fileNames)
		{
			ICodeCompiler codeCompiler = this.CreateCompiler();
			if (codeCompiler == null)
			{
				throw this.GetNotImplemented();
			}
			return codeCompiler.CompileAssemblyFromFileBatch(options, fileNames);
		}

		/// <summary>Compiles an assembly from the specified array of strings containing source code, using the specified compiler settings.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> object that indicates the results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler settings for this compilation. </param>
		/// <param name="sources">An array of source code strings to compile. </param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateCompiler" /> method is overridden in a derived class.</exception>
		// Token: 0x06000434 RID: 1076 RVA: 0x0000E558 File Offset: 0x0000C758
		public virtual CompilerResults CompileAssemblyFromSource(CompilerParameters options, params string[] fileNames)
		{
			ICodeCompiler codeCompiler = this.CreateCompiler();
			if (codeCompiler == null)
			{
				throw this.GetNotImplemented();
			}
			return codeCompiler.CompileAssemblyFromSourceBatch(options, fileNames);
		}

		/// <summary>Creates an escaped identifier for the specified value.</summary>
		/// <returns>The escaped identifier for the value.</returns>
		/// <param name="value">The string for which to create an escaped identifier.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06000435 RID: 1077 RVA: 0x0000E584 File Offset: 0x0000C784
		public virtual string CreateEscapedIdentifier(string value)
		{
			ICodeGenerator codeGenerator = this.CreateGenerator();
			if (codeGenerator == null)
			{
				throw this.GetNotImplemented();
			}
			return codeGenerator.CreateEscapedIdentifier(value);
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> instance for the specified language.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> instance that is implemented for the specified language name.</returns>
		/// <param name="language">The language name. </param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The <paramref name="language" /> does not have a configured provider on this computer. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="language" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06000436 RID: 1078 RVA: 0x0000E5AC File Offset: 0x0000C7AC
		[ComVisible(false)]
		[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
		public static CodeDomProvider CreateProvider(string language)
		{
			CompilerInfo compilerInfo = CodeDomProvider.GetCompilerInfo(language);
			return (compilerInfo != null) ? compilerInfo.CreateProvider() : null;
		}

		/// <summary>Creates a valid identifier for the specified value.</summary>
		/// <returns>A valid identifier for the specified value.</returns>
		/// <param name="value">The string for which to generate a valid identifier.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06000437 RID: 1079 RVA: 0x0000E5D4 File Offset: 0x0000C7D4
		public virtual string CreateValidIdentifier(string value)
		{
			ICodeGenerator codeGenerator = this.CreateGenerator();
			if (codeGenerator == null)
			{
				throw this.GetNotImplemented();
			}
			return codeGenerator.CreateValidIdentifier(value);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) compilation unit and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="compileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> for which to generate code. </param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which the output code is sent. </param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code. </param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06000438 RID: 1080 RVA: 0x0000E5FC File Offset: 0x0000C7FC
		public virtual void GenerateCodeFromCompileUnit(CodeCompileUnit compileUnit, TextWriter writer, CodeGeneratorOptions options)
		{
			ICodeGenerator codeGenerator = this.CreateGenerator();
			if (codeGenerator == null)
			{
				throw this.GetNotImplemented();
			}
			codeGenerator.GenerateCodeFromCompileUnit(compileUnit, writer, options);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) expression and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> object that indicates the expression for which to generate code. </param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent. </param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code. </param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06000439 RID: 1081 RVA: 0x0000E628 File Offset: 0x0000C828
		public virtual void GenerateCodeFromExpression(CodeExpression expression, TextWriter writer, CodeGeneratorOptions options)
		{
			ICodeGenerator codeGenerator = this.CreateGenerator();
			if (codeGenerator == null)
			{
				throw this.GetNotImplemented();
			}
			codeGenerator.GenerateCodeFromExpression(expression, writer, options);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) member declaration and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="member">A <see cref="T:System.CodeDom.CodeTypeMember" /> object that indicates the member for which to generate code. </param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent. </param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code. </param>
		/// <exception cref="T:System.NotImplementedException">This method is not overridden in a derived class.</exception>
		// Token: 0x0600043A RID: 1082 RVA: 0x0000E654 File Offset: 0x0000C854
		public virtual void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
		{
			throw this.GetNotImplemented();
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) namespace and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="codeNamespace">A <see cref="T:System.CodeDom.CodeNamespace" /> object that indicates the namespace for which to generate code. </param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent. </param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code. </param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x0600043B RID: 1083 RVA: 0x0000E65C File Offset: 0x0000C85C
		public virtual void GenerateCodeFromNamespace(CodeNamespace codeNamespace, TextWriter writer, CodeGeneratorOptions options)
		{
			ICodeGenerator codeGenerator = this.CreateGenerator();
			if (codeGenerator == null)
			{
				throw this.GetNotImplemented();
			}
			codeGenerator.GenerateCodeFromNamespace(codeNamespace, writer, options);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) statement and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="statement">A <see cref="T:System.CodeDom.CodeStatement" /> containing the CodeDOM elements for which to generate code. </param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent. </param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code. </param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x0600043C RID: 1084 RVA: 0x0000E688 File Offset: 0x0000C888
		public virtual void GenerateCodeFromStatement(CodeStatement statement, TextWriter writer, CodeGeneratorOptions options)
		{
			ICodeGenerator codeGenerator = this.CreateGenerator();
			if (codeGenerator == null)
			{
				throw this.GetNotImplemented();
			}
			codeGenerator.GenerateCodeFromStatement(statement, writer, options);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) type declaration and sends it to the specified text writer, using the specified options.</summary>
		/// <param name="codeType">A <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object that indicates the type for which to generate code. </param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to which output code is sent. </param>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code. </param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x0600043D RID: 1085 RVA: 0x0000E6B4 File Offset: 0x0000C8B4
		public virtual void GenerateCodeFromType(CodeTypeDeclaration codeType, TextWriter writer, CodeGeneratorOptions options)
		{
			ICodeGenerator codeGenerator = this.CreateGenerator();
			if (codeGenerator == null)
			{
				throw this.GetNotImplemented();
			}
			codeGenerator.GenerateCodeFromType(codeType, writer, options);
		}

		/// <summary>Returns the language provider and compiler configuration settings for this computer.</summary>
		/// <returns>An array of type <see cref="T:System.CodeDom.Compiler.CompilerInfo" /> representing the settings of all configured <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementations.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x0600043E RID: 1086 RVA: 0x0000E6E0 File Offset: 0x0000C8E0
		[ComVisible(false)]
		[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
		public static CompilerInfo[] GetAllCompilerInfo()
		{
			return (CodeDomProvider.Config != null) ? CodeDomProvider.Config.CompilerInfos : null;
		}

		/// <summary>Returns the language provider and compiler configuration settings for the specified language.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerInfo" /> object populated with settings of the configured <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation.</returns>
		/// <param name="language">A language name. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <paramref name="language" /> does not have a configured provider on this computer. </exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The <paramref name="language" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x0600043F RID: 1087 RVA: 0x0000E6FC File Offset: 0x0000C8FC
		[ComVisible(false)]
		[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
		public static CompilerInfo GetCompilerInfo(string language)
		{
			if (language == null)
			{
				throw new ArgumentNullException("language");
			}
			if (CodeDomProvider.Config == null)
			{
				return null;
			}
			CompilerCollection compilers = CodeDomProvider.Config.Compilers;
			return compilers[language];
		}

		/// <summary>Returns a language name associated with the specified file name extension, as configured in the <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> compiler configuration section.</summary>
		/// <returns>A language name associated with the file name extension, as configured in the <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> compiler configuration settings.</returns>
		/// <param name="extension">A file name extension. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <paramref name="extension" /> does not have a configured language provider on this computer. </exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The <paramref name="extension" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06000440 RID: 1088 RVA: 0x0000E738 File Offset: 0x0000C938
		[ComVisible(false)]
		[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
		public static string GetLanguageFromExtension(string extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			if (CodeDomProvider.Config != null)
			{
				return CodeDomProvider.Config.Compilers.GetLanguageFromExtension(extension);
			}
			return null;
		}

		/// <summary>Gets the type indicated by the specified <see cref="T:System.CodeDom.CodeTypeReference" />.</summary>
		/// <returns>A text representation of the specified type, formatted for the language in which code is generated by this code generator. In Visual Basic, for example, passing in a <see cref="T:System.CodeDom.CodeTypeReference" /> for the <see cref="T:System.Int32" /> type will return "Integer".</returns>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type to return.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06000441 RID: 1089 RVA: 0x0000E768 File Offset: 0x0000C968
		public virtual string GetTypeOutput(CodeTypeReference type)
		{
			ICodeGenerator codeGenerator = this.CreateGenerator();
			if (codeGenerator == null)
			{
				throw this.GetNotImplemented();
			}
			return codeGenerator.GetTypeOutput(type);
		}

		/// <summary>Tests whether a file name extension has an associated <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation configured on the computer.</summary>
		/// <returns>true if a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation is configured for the specified file name extension; otherwise, false.</returns>
		/// <param name="extension">A file name extension. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="extension" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06000442 RID: 1090 RVA: 0x0000E790 File Offset: 0x0000C990
		[ComVisible(false)]
		[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
		public static bool IsDefinedExtension(string extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			return CodeDomProvider.Config != null && CodeDomProvider.Config.Compilers.GetCompilerInfoForExtension(extension) != null;
		}

		/// <summary>Tests whether a language has a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation configured on the computer.</summary>
		/// <returns>true if a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation is configured for the specified language; otherwise, false.</returns>
		/// <param name="language">The language name. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="language" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06000443 RID: 1091 RVA: 0x0000E7D0 File Offset: 0x0000C9D0
		[ComVisible(false)]
		[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n")]
		public static bool IsDefinedLanguage(string language)
		{
			if (language == null)
			{
				throw new ArgumentNullException("language");
			}
			return CodeDomProvider.Config != null && CodeDomProvider.Config.Compilers.GetCompilerInfoForLanguage(language) != null;
		}

		/// <summary>Returns a value that indicates whether the specified value is a valid identifier for the current language.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is a valid identifier; otherwise, false.</returns>
		/// <param name="value">The value to verify as a valid identifier.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06000444 RID: 1092 RVA: 0x0000E810 File Offset: 0x0000CA10
		public virtual bool IsValidIdentifier(string value)
		{
			ICodeGenerator codeGenerator = this.CreateGenerator();
			if (codeGenerator == null)
			{
				throw this.GetNotImplemented();
			}
			return codeGenerator.IsValidIdentifier(value);
		}

		/// <summary>Compiles the code read from the specified text stream into a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> that contains a representation of the parsed code.</returns>
		/// <param name="codeStream">A <see cref="T:System.IO.TextReader" /> object that is used to read the code to be parsed. </param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06000445 RID: 1093 RVA: 0x0000E838 File Offset: 0x0000CA38
		public virtual CodeCompileUnit Parse(TextReader codeStream)
		{
			ICodeParser codeParser = this.CreateParser();
			if (codeParser == null)
			{
				throw this.GetNotImplemented();
			}
			return codeParser.Parse(codeStream);
		}

		/// <summary>Returns a value indicating whether the specified code generation support is provided.</summary>
		/// <returns>true if the specified code generation support is provided; otherwise, false.</returns>
		/// <param name="generatorSupport">A <see cref="T:System.CodeDom.Compiler.GeneratorSupport" /> object that indicates the type of code generation support to verify.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06000446 RID: 1094 RVA: 0x0000E860 File Offset: 0x0000CA60
		public virtual bool Supports(GeneratorSupport supports)
		{
			ICodeGenerator codeGenerator = this.CreateGenerator();
			if (codeGenerator == null)
			{
				throw this.GetNotImplemented();
			}
			return codeGenerator.Supports(supports);
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0000E888 File Offset: 0x0000CA88
		private static CodeDomConfigurationHandler Config
		{
			get
			{
				return ConfigurationManager.GetSection("system.codedom") as CodeDomConfigurationHandler;
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000E89C File Offset: 0x0000CA9C
		private Exception GetNotImplemented()
		{
			return new NotImplementedException();
		}
	}
}
