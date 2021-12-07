using System;
using System.IO;

namespace System.CodeDom.Compiler
{
	/// <summary>Defines an interface for generating code.</summary>
	// Token: 0x0200008B RID: 139
	public interface ICodeGenerator
	{
		/// <summary>Creates an escaped identifier for the specified value.</summary>
		/// <returns>The escaped identifier for the value.</returns>
		/// <param name="value">The string to create an escaped identifier for. </param>
		// Token: 0x060005A8 RID: 1448
		string CreateEscapedIdentifier(string value);

		/// <summary>Creates a valid identifier for the specified value.</summary>
		/// <returns>A valid identifier for the specified value.</returns>
		/// <param name="value">The string to generate a valid identifier for. </param>
		// Token: 0x060005A9 RID: 1449
		string CreateValidIdentifier(string value);

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) compilation unit and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeCompileUnit" /> to generate code for. </param>
		/// <param name="w">The <see cref="T:System.IO.TextWriter" /> to output code to. </param>
		/// <param name="o">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code. </param>
		// Token: 0x060005AA RID: 1450
		void GenerateCodeFromCompileUnit(CodeCompileUnit compileUnit, TextWriter output, CodeGeneratorOptions options);

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) expression and outputs it to the specified text writer.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to generate code for. </param>
		/// <param name="w">The <see cref="T:System.IO.TextWriter" /> to output code to. </param>
		/// <param name="o">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code. </param>
		// Token: 0x060005AB RID: 1451
		void GenerateCodeFromExpression(CodeExpression expression, TextWriter output, CodeGeneratorOptions options);

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) namespace and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeNamespace" /> that indicates the namespace to generate code for. </param>
		/// <param name="w">The <see cref="T:System.IO.TextWriter" /> to output code to. </param>
		/// <param name="o">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code. </param>
		// Token: 0x060005AC RID: 1452
		void GenerateCodeFromNamespace(CodeNamespace ns, TextWriter output, CodeGeneratorOptions options);

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) statement and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeStatement" /> containing the CodeDOM elements to translate. </param>
		/// <param name="w">The <see cref="T:System.IO.TextWriter" /> to output code to. </param>
		/// <param name="o">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code. </param>
		// Token: 0x060005AD RID: 1453
		void GenerateCodeFromStatement(CodeStatement statement, TextWriter output, CodeGeneratorOptions options);

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) type declaration and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeTypeDeclaration" /> that indicates the type to generate code for. </param>
		/// <param name="w">The <see cref="T:System.IO.TextWriter" /> to output code to. </param>
		/// <param name="o">A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> that indicates the options to use for generating code. </param>
		// Token: 0x060005AE RID: 1454
		void GenerateCodeFromType(CodeTypeDeclaration typeDeclaration, TextWriter output, CodeGeneratorOptions options);

		/// <summary>Gets the type indicated by the specified <see cref="T:System.CodeDom.CodeTypeReference" />.</summary>
		/// <returns>A text representation of the specified type for the language this code generator is designed to generate code in. For example, in Visual Basic, passing in type System.Int32 will return "Integer".</returns>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type to return. </param>
		// Token: 0x060005AF RID: 1455
		string GetTypeOutput(CodeTypeReference type);

		/// <summary>Gets a value that indicates whether the specified value is a valid identifier for the current language.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is a valid identifier; otherwise, false.</returns>
		/// <param name="value">The value to test for being a valid identifier. </param>
		// Token: 0x060005B0 RID: 1456
		bool IsValidIdentifier(string value);

		/// <summary>Gets a value indicating whether the generator provides support for the language features represented by the specified <see cref="T:System.CodeDom.Compiler.GeneratorSupport" /> object.</summary>
		/// <returns>true if the specified capabilities are supported; otherwise, false.</returns>
		/// <param name="supports">The capabilities to test the generator for. </param>
		// Token: 0x060005B1 RID: 1457
		bool Supports(GeneratorSupport supports);

		/// <summary>Throws an exception if the specified value is not a valid identifier.</summary>
		/// <param name="value">The identifier to validate. </param>
		/// <exception cref="T:System.ArgumentException">The identifier is not valid. </exception>
		// Token: 0x060005B2 RID: 1458
		void ValidateIdentifier(string value);
	}
}
