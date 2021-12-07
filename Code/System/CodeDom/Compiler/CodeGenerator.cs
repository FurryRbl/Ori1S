using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides an example implementation of the <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> interface. This class is abstract.</summary>
	// Token: 0x02000079 RID: 121
	public abstract class CodeGenerator : ICodeGenerator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CodeGenerator" /> class. </summary>
		// Token: 0x06000449 RID: 1097 RVA: 0x0000E8A4 File Offset: 0x0000CAA4
		protected CodeGenerator()
		{
			this.visitor = new CodeGenerator.Visitor(this);
		}

		/// <summary>Creates an escaped identifier for the specified value.</summary>
		/// <returns>The escaped identifier for the value.</returns>
		/// <param name="value">The string to create an escaped identifier for.</param>
		// Token: 0x0600044B RID: 1099 RVA: 0x0000E948 File Offset: 0x0000CB48
		string ICodeGenerator.CreateEscapedIdentifier(string value)
		{
			return this.CreateEscapedIdentifier(value);
		}

		/// <summary>Creates a valid identifier for the specified value.</summary>
		/// <returns>A valid identifier for the specified value.</returns>
		/// <param name="value">The string to generate a valid identifier for.</param>
		// Token: 0x0600044C RID: 1100 RVA: 0x0000E954 File Offset: 0x0000CB54
		string ICodeGenerator.CreateValidIdentifier(string value)
		{
			return this.CreateValidIdentifier(value);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) compilation unit and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">A T:System.CodeDom.CodeCompileUnit to generate code for.</param>
		/// <param name="w">The T:System.IO.TextWriter to output code to.</param>
		/// <param name="o">A T:System.CodeDom.Compiler.CodeGeneratorOptions  that indicates the options to use for generating code.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="w" /> is not available. <paramref name="w" /> may have been closed before the method call was made.</exception>
		// Token: 0x0600044D RID: 1101 RVA: 0x0000E960 File Offset: 0x0000CB60
		void ICodeGenerator.GenerateCodeFromCompileUnit(CodeCompileUnit compileUnit, TextWriter output, CodeGeneratorOptions options)
		{
			this.InitOutput(output, options);
			if (compileUnit is CodeSnippetCompileUnit)
			{
				this.GenerateSnippetCompileUnit((CodeSnippetCompileUnit)compileUnit);
			}
			else
			{
				this.GenerateCompileUnit(compileUnit);
			}
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) expression and outputs it to the specified text writer.</summary>
		/// <param name="e">A T:System.CodeDom.CodeExpression  that indicates the expression to generate code for.</param>
		/// <param name="w">The T:System.IO.TextWriter to output code to.</param>
		/// <param name="o">A T:System.CodeDom.Compiler.CodeGeneratorOptions  that indicates the options to use for generating code.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="w" /> is not available. <paramref name="w" /> may have been closed before the method call was made.</exception>
		// Token: 0x0600044E RID: 1102 RVA: 0x0000E990 File Offset: 0x0000CB90
		void ICodeGenerator.GenerateCodeFromExpression(CodeExpression expression, TextWriter output, CodeGeneratorOptions options)
		{
			this.InitOutput(output, options);
			this.GenerateExpression(expression);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) namespace and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">A T:System.CodeDom.CodeNamespace that indicates the namespace to generate code for.</param>
		/// <param name="w">The T:System.IO.TextWriter to output code to.</param>
		/// <param name="o">A T:System.CodeDom.Compiler.CodeGeneratorOptions  that indicates the options to use for generating code.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="w" /> is not available. <paramref name="w" /> may have been closed before the method call was made.</exception>
		// Token: 0x0600044F RID: 1103 RVA: 0x0000E9A4 File Offset: 0x0000CBA4
		void ICodeGenerator.GenerateCodeFromNamespace(CodeNamespace ns, TextWriter output, CodeGeneratorOptions options)
		{
			this.InitOutput(output, options);
			this.GenerateNamespace(ns);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) statement and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">A T:System.CodeDom.CodeStatement  containing the CodeDOM elements to translate.</param>
		/// <param name="w">The T:System.IO.TextWriter to output code to.</param>
		/// <param name="o">A T:System.CodeDom.Compiler.CodeGeneratorOptions  that indicates the options to use for generating code.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="w" /> is not available. <paramref name="w" /> may have been closed before the method call was made.</exception>
		// Token: 0x06000450 RID: 1104 RVA: 0x0000E9B8 File Offset: 0x0000CBB8
		void ICodeGenerator.GenerateCodeFromStatement(CodeStatement statement, TextWriter output, CodeGeneratorOptions options)
		{
			this.InitOutput(output, options);
			this.GenerateStatement(statement);
		}

		/// <summary>Generates code for the specified Code Document Object Model (CodeDOM) type declaration and outputs it to the specified text writer using the specified options.</summary>
		/// <param name="e">A T:System.CodeDom.CodeTypeDeclaration  that indicates the type to generate code for.</param>
		/// <param name="w">The T:System.IO.TextWriter to output code to.</param>
		/// <param name="o">A T:System.CodeDom.Compiler.CodeGeneratorOptions  that indicates the options to use for generating code.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="w" /> is not available. <paramref name="w" /> may have been closed before the method call was made.</exception>
		// Token: 0x06000451 RID: 1105 RVA: 0x0000E9CC File Offset: 0x0000CBCC
		void ICodeGenerator.GenerateCodeFromType(CodeTypeDeclaration type, TextWriter output, CodeGeneratorOptions options)
		{
			this.InitOutput(output, options);
			this.GenerateType(type);
		}

		/// <summary>Gets the type indicated by the specified <see cref="T:System.CodeDom.CodeTypeReference" />.</summary>
		/// <returns>The name of the data type reference.</returns>
		/// <param name="type">A T:System.CodeDom.CodeTypeReference  that indicates the type to return.</param>
		// Token: 0x06000452 RID: 1106 RVA: 0x0000E9E0 File Offset: 0x0000CBE0
		string ICodeGenerator.GetTypeOutput(CodeTypeReference type)
		{
			return this.GetTypeOutput(type);
		}

		/// <summary>Gets a value that indicates whether the specified value is a valid identifier for the current language.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is a valid identifier; otherwise, false.</returns>
		/// <param name="value">The value to test for being a valid identifier.</param>
		// Token: 0x06000453 RID: 1107 RVA: 0x0000E9EC File Offset: 0x0000CBEC
		bool ICodeGenerator.IsValidIdentifier(string value)
		{
			return this.IsValidIdentifier(value);
		}

		/// <summary>Gets a value indicating whether the generator provides support for the language features represented by the specified <see cref="T:System.CodeDom.Compiler.GeneratorSupport" />  object.</summary>
		/// <returns>true if the specified capabilities are supported; otherwise, false.</returns>
		/// <param name="support">The capabilities to test the generator for.</param>
		// Token: 0x06000454 RID: 1108 RVA: 0x0000E9F8 File Offset: 0x0000CBF8
		bool ICodeGenerator.Supports(GeneratorSupport value)
		{
			return this.Supports(value);
		}

		/// <summary>Throws an exception if the specified value is not a valid identifier.</summary>
		/// <param name="value">The identifier to validate.</param>
		// Token: 0x06000455 RID: 1109 RVA: 0x0000EA04 File Offset: 0x0000CC04
		void ICodeGenerator.ValidateIdentifier(string value)
		{
			this.ValidateIdentifier(value);
		}

		/// <summary>Gets the code type declaration for the current class.</summary>
		/// <returns>The <see cref="T:System.CodeDom.CodeTypeDeclaration" /> for the current class.</returns>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0000EA10 File Offset: 0x0000CC10
		protected CodeTypeDeclaration CurrentClass
		{
			get
			{
				return this.currentType;
			}
		}

		/// <summary>Gets the current member of the class.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeMember" /> that indicates the current member of the class.</returns>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000EA18 File Offset: 0x0000CC18
		protected CodeTypeMember CurrentMember
		{
			get
			{
				return this.currentMember;
			}
		}

		/// <summary>Gets the current member name.</summary>
		/// <returns>The name of the current member.</returns>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000EA20 File Offset: 0x0000CC20
		protected string CurrentMemberName
		{
			get
			{
				if (this.currentMember == null)
				{
					return "<% unknown %>";
				}
				return this.currentMember.Name;
			}
		}

		/// <summary>Gets the current class name.</summary>
		/// <returns>The current class name.</returns>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000EA40 File Offset: 0x0000CC40
		protected string CurrentTypeName
		{
			get
			{
				if (this.currentType == null)
				{
					return "<% unknown %>";
				}
				return this.currentType.Name;
			}
		}

		/// <summary>Gets or sets the amount of spaces to indent each indentation level.</summary>
		/// <returns>The number of spaces to indent for each indentation level.</returns>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000EA60 File Offset: 0x0000CC60
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x0000EA70 File Offset: 0x0000CC70
		protected int Indent
		{
			get
			{
				return this.output.Indent;
			}
			set
			{
				this.output.Indent = value;
			}
		}

		/// <summary>Gets a value indicating whether the current object being generated is a class.</summary>
		/// <returns>true if the current object is a class; otherwise, false.</returns>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0000EA80 File Offset: 0x0000CC80
		protected bool IsCurrentClass
		{
			get
			{
				return this.currentType != null && this.currentType.IsClass && !(this.currentType is CodeTypeDelegate);
			}
		}

		/// <summary>Gets a value indicating whether the current object being generated is a delegate.</summary>
		/// <returns>true if the current object is a delegate; otherwise, false.</returns>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000EAB4 File Offset: 0x0000CCB4
		protected bool IsCurrentDelegate
		{
			get
			{
				return this.currentType is CodeTypeDelegate;
			}
		}

		/// <summary>Gets a value indicating whether the current object being generated is an enumeration.</summary>
		/// <returns>true if the current object is an enumeration; otherwise, false.</returns>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000EAC4 File Offset: 0x0000CCC4
		protected bool IsCurrentEnum
		{
			get
			{
				return this.currentType != null && this.currentType.IsEnum;
			}
		}

		/// <summary>Gets a value indicating whether the current object being generated is an interface.</summary>
		/// <returns>true if the current object is an interface; otherwise, false.</returns>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000EAE0 File Offset: 0x0000CCE0
		protected bool IsCurrentInterface
		{
			get
			{
				return this.currentType != null && this.currentType.IsInterface;
			}
		}

		/// <summary>Gets a value indicating whether the current object being generated is a value type or struct.</summary>
		/// <returns>true if the current object is a value type or struct; otherwise, false.</returns>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000EAFC File Offset: 0x0000CCFC
		protected bool IsCurrentStruct
		{
			get
			{
				return this.currentType != null && this.currentType.IsStruct;
			}
		}

		/// <summary>Gets the token that represents null.</summary>
		/// <returns>The token that represents null.</returns>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000461 RID: 1121
		protected abstract string NullToken { get; }

		/// <summary>Gets the options to be used by the code generator.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> object that indicates the options for the code generator to use.</returns>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000EB18 File Offset: 0x0000CD18
		protected CodeGeneratorOptions Options
		{
			get
			{
				return this.options;
			}
		}

		/// <summary>Gets the <see cref="T:System.IO.TextWriter" /> to use for output.</summary>
		/// <returns>The <see cref="T:System.IO.TextWriter" /> to use for output.</returns>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000EB20 File Offset: 0x0000CD20
		protected TextWriter Output
		{
			get
			{
				return this.output;
			}
		}

		/// <summary>Generates a line-continuation character and outputs the specified string on a new line.</summary>
		/// <param name="st">The string to write on the new line. </param>
		// Token: 0x06000464 RID: 1124 RVA: 0x0000EB28 File Offset: 0x0000CD28
		protected virtual void ContinueOnNewLine(string st)
		{
			this.output.WriteLine(st);
		}

		/// <summary>Generates code for the specified argument reference expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeArgumentReferenceExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x06000465 RID: 1125
		protected abstract void GenerateArgumentReferenceExpression(CodeArgumentReferenceExpression e);

		/// <summary>Generates code for the specified array creation expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x06000466 RID: 1126
		protected abstract void GenerateArrayCreateExpression(CodeArrayCreateExpression e);

		/// <summary>Generates code for the specified array indexer expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeArrayIndexerExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x06000467 RID: 1127
		protected abstract void GenerateArrayIndexerExpression(CodeArrayIndexerExpression e);

		/// <summary>Generates code for the specified assignment statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeAssignStatement" /> that indicates the statement to generate code for. </param>
		// Token: 0x06000468 RID: 1128
		protected abstract void GenerateAssignStatement(CodeAssignStatement s);

		/// <summary>Generates code for the specified attach event statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeAttachEventStatement" /> that indicates the statement to generate code for. </param>
		// Token: 0x06000469 RID: 1129
		protected abstract void GenerateAttachEventStatement(CodeAttachEventStatement s);

		/// <summary>Generates code for the specified attribute block start.</summary>
		/// <param name="attributes">A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the start of the attribute block to generate code for. </param>
		// Token: 0x0600046A RID: 1130
		protected abstract void GenerateAttributeDeclarationsStart(CodeAttributeDeclarationCollection attributes);

		/// <summary>Generates code for the specified attribute block end.</summary>
		/// <param name="attributes">A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the end of the attribute block to generate code for. </param>
		// Token: 0x0600046B RID: 1131
		protected abstract void GenerateAttributeDeclarationsEnd(CodeAttributeDeclarationCollection attributes);

		/// <summary>Generates code for the specified base reference expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeBaseReferenceExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x0600046C RID: 1132
		protected abstract void GenerateBaseReferenceExpression(CodeBaseReferenceExpression e);

		/// <summary>Generates code for the specified binary operator expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeBinaryOperatorExpression" /> that indicates the expression to generate code for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="e" /> is null.</exception>
		// Token: 0x0600046D RID: 1133 RVA: 0x0000EB38 File Offset: 0x0000CD38
		protected virtual void GenerateBinaryOperatorExpression(CodeBinaryOperatorExpression e)
		{
			this.output.Write('(');
			this.GenerateExpression(e.Left);
			this.output.Write(' ');
			this.OutputOperator(e.Operator);
			this.output.Write(' ');
			this.GenerateExpression(e.Right);
			this.output.Write(')');
		}

		/// <summary>Generates code for the specified cast expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeCastExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x0600046E RID: 1134
		protected abstract void GenerateCastExpression(CodeCastExpression e);

		/// <summary>Generates code for the specified class member using the specified text writer and code generator options.</summary>
		/// <param name="member">A <see cref="T:System.CodeDom.CodeTypeMember" /> to generate code for.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to write to.</param>
		/// <param name="options">The <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> to use when generating the code.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.CodeDom.Compiler.CodeGenerator.Output" /> property is not null.</exception>
		// Token: 0x0600046F RID: 1135 RVA: 0x0000EBA0 File Offset: 0x0000CDA0
		[MonoTODO]
		public virtual void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates code for the specified comment.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeComment" /> to generate code for. </param>
		// Token: 0x06000470 RID: 1136
		protected abstract void GenerateComment(CodeComment comment);

		/// <summary>Generates code for the specified comment statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeCommentStatement" /> that indicates the statement to generate code for. </param>
		// Token: 0x06000471 RID: 1137 RVA: 0x0000EBA8 File Offset: 0x0000CDA8
		protected virtual void GenerateCommentStatement(CodeCommentStatement statement)
		{
			this.GenerateComment(statement.Comment);
		}

		/// <summary>Generates code for the specified comment statements.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> that indicates the expression to generate code for. </param>
		// Token: 0x06000472 RID: 1138 RVA: 0x0000EBB8 File Offset: 0x0000CDB8
		protected virtual void GenerateCommentStatements(CodeCommentStatementCollection statements)
		{
			foreach (object obj in statements)
			{
				CodeCommentStatement statement = (CodeCommentStatement)obj;
				this.GenerateCommentStatement(statement);
			}
		}

		/// <summary>Generates code for the specified compile unit.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeCompileUnit" /> that indicates the compile unit to generate code for. </param>
		// Token: 0x06000473 RID: 1139 RVA: 0x0000EC24 File Offset: 0x0000CE24
		protected virtual void GenerateCompileUnit(CodeCompileUnit compileUnit)
		{
			this.GenerateCompileUnitStart(compileUnit);
			CodeAttributeDeclarationCollection assemblyCustomAttributes = compileUnit.AssemblyCustomAttributes;
			if (assemblyCustomAttributes.Count != 0)
			{
				foreach (object obj in assemblyCustomAttributes)
				{
					CodeAttributeDeclaration attribute = (CodeAttributeDeclaration)obj;
					this.GenerateAttributeDeclarationsStart(assemblyCustomAttributes);
					this.output.Write("assembly: ");
					this.OutputAttributeDeclaration(attribute);
					this.GenerateAttributeDeclarationsEnd(assemblyCustomAttributes);
				}
				this.output.WriteLine();
			}
			foreach (object obj2 in compileUnit.Namespaces)
			{
				CodeNamespace ns = (CodeNamespace)obj2;
				this.GenerateNamespace(ns);
			}
			this.GenerateCompileUnitEnd(compileUnit);
		}

		/// <summary>Generates code for the end of a compile unit.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeCompileUnit" /> that indicates the compile unit to generate code for. </param>
		// Token: 0x06000474 RID: 1140 RVA: 0x0000ED40 File Offset: 0x0000CF40
		protected virtual void GenerateCompileUnitEnd(CodeCompileUnit compileUnit)
		{
			if (compileUnit.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(compileUnit.EndDirectives);
			}
		}

		/// <summary>Generates code for the start of a compile unit.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeCompileUnit" /> that indicates the compile unit to generate code for. </param>
		// Token: 0x06000475 RID: 1141 RVA: 0x0000ED6C File Offset: 0x0000CF6C
		protected virtual void GenerateCompileUnitStart(CodeCompileUnit compileUnit)
		{
			if (compileUnit.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(compileUnit.StartDirectives);
				this.Output.WriteLine();
			}
		}

		/// <summary>Generates code for the specified conditional statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeConditionStatement" /> that indicates the statement to generate code for. </param>
		// Token: 0x06000476 RID: 1142
		protected abstract void GenerateConditionStatement(CodeConditionStatement s);

		/// <summary>Generates code for the specified constructor.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeConstructor" /> that indicates the constructor to generate code for. </param>
		/// <param name="c">A <see cref="T:System.CodeDom.CodeTypeDeclaration" /> that indicates the type of the object that this constructor constructs. </param>
		// Token: 0x06000477 RID: 1143
		protected abstract void GenerateConstructor(CodeConstructor x, CodeTypeDeclaration d);

		/// <summary>Generates code for the specified decimal value.</summary>
		/// <param name="d">The decimal value to generate code for. </param>
		// Token: 0x06000478 RID: 1144 RVA: 0x0000EDA4 File Offset: 0x0000CFA4
		protected virtual void GenerateDecimalValue(decimal d)
		{
			this.Output.Write(d.ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>Generates code for the specified code default value expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeDefaultValueExpression" /> to generate code for.</param>
		// Token: 0x06000479 RID: 1145 RVA: 0x0000EDC0 File Offset: 0x0000CFC0
		[MonoTODO]
		protected virtual void GenerateDefaultValueExpression(CodeDefaultValueExpression e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates code for the specified delegate creation expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeDelegateCreateExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x0600047A RID: 1146
		protected abstract void GenerateDelegateCreateExpression(CodeDelegateCreateExpression e);

		/// <summary>Generates code for the specified delegate invoke expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeDelegateInvokeExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x0600047B RID: 1147
		protected abstract void GenerateDelegateInvokeExpression(CodeDelegateInvokeExpression e);

		/// <summary>Generates code for the specified direction expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeDirectionExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x0600047C RID: 1148 RVA: 0x0000EDC8 File Offset: 0x0000CFC8
		protected virtual void GenerateDirectionExpression(CodeDirectionExpression e)
		{
			this.OutputDirection(e.Direction);
			this.output.Write(' ');
			this.GenerateExpression(e.Expression);
		}

		/// <summary>Generates code for a double-precision floating point number.</summary>
		/// <param name="d">The <see cref="T:System.Double" /> value to generate code for. </param>
		// Token: 0x0600047D RID: 1149 RVA: 0x0000EDFC File Offset: 0x0000CFFC
		protected virtual void GenerateDoubleValue(double d)
		{
			this.Output.Write(d.ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>Generates code for the specified entry point method.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeEntryPointMethod" /> that indicates the entry point for the code. </param>
		/// <param name="c">A <see cref="T:System.CodeDom.CodeTypeDeclaration" /> that indicates the code that declares the type. </param>
		// Token: 0x0600047E RID: 1150
		protected abstract void GenerateEntryPointMethod(CodeEntryPointMethod m, CodeTypeDeclaration d);

		/// <summary>Generates code for the specified event.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeMemberEvent" /> that indicates the member event to generate code for. </param>
		/// <param name="c">A <see cref="T:System.CodeDom.CodeTypeDeclaration" /> that indicates the type of the object that this event occurs on. </param>
		// Token: 0x0600047F RID: 1151
		protected abstract void GenerateEvent(CodeMemberEvent ev, CodeTypeDeclaration d);

		/// <summary>Generates code for the specified event reference expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x06000480 RID: 1152
		protected abstract void GenerateEventReferenceExpression(CodeEventReferenceExpression e);

		/// <summary>Generates code for the specified code expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the code expression to generate code for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="e" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="e" /> is not a valid <see cref="T:System.CodeDom.CodeStatement" />.</exception>
		// Token: 0x06000481 RID: 1153 RVA: 0x0000EE18 File Offset: 0x0000D018
		protected void GenerateExpression(CodeExpression e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			try
			{
				e.Accept(this.visitor);
			}
			catch (NotImplementedException)
			{
				throw new ArgumentException("Element type " + e.GetType() + " is not supported.", "e");
			}
		}

		/// <summary>Generates code for the specified expression statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeExpressionStatement" /> that indicates the statement to generate code for. </param>
		// Token: 0x06000482 RID: 1154
		protected abstract void GenerateExpressionStatement(CodeExpressionStatement statement);

		/// <summary>Generates code for the specified member field.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeMemberField" /> that indicates the field to generate code for. </param>
		// Token: 0x06000483 RID: 1155
		protected abstract void GenerateField(CodeMemberField f);

		/// <summary>Generates code for the specified field reference expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeFieldReferenceExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x06000484 RID: 1156
		protected abstract void GenerateFieldReferenceExpression(CodeFieldReferenceExpression e);

		/// <summary>Generates code for the specified goto statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeGotoStatement" /> that indicates the expression to generate code for. </param>
		// Token: 0x06000485 RID: 1157
		protected abstract void GenerateGotoStatement(CodeGotoStatement statement);

		/// <summary>Generates code for the specified indexer expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeIndexerExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x06000486 RID: 1158
		protected abstract void GenerateIndexerExpression(CodeIndexerExpression e);

		/// <summary>Generates code for the specified iteration statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeIterationStatement" /> that indicates the statement to generate code for. </param>
		// Token: 0x06000487 RID: 1159
		protected abstract void GenerateIterationStatement(CodeIterationStatement s);

		/// <summary>Generates code for the specified labeled statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeLabeledStatement" /> that indicates the statement to generate code for. </param>
		// Token: 0x06000488 RID: 1160
		protected abstract void GenerateLabeledStatement(CodeLabeledStatement statement);

		/// <summary>Generates code for the specified line pragma start.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeLinePragma" /> that indicates the start of the line pragma to generate code for. </param>
		// Token: 0x06000489 RID: 1161
		protected abstract void GenerateLinePragmaStart(CodeLinePragma p);

		/// <summary>Generates code for the specified line pragma end.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeLinePragma" /> that indicates the end of the line pragma to generate code for. </param>
		// Token: 0x0600048A RID: 1162
		protected abstract void GenerateLinePragmaEnd(CodeLinePragma p);

		/// <summary>Generates code for the specified method.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeMemberMethod" /> that indicates the member method to generate code for. </param>
		/// <param name="c">A <see cref="T:System.CodeDom.CodeTypeDeclaration" /> that indicates the type of the object that this method occurs on. </param>
		// Token: 0x0600048B RID: 1163
		protected abstract void GenerateMethod(CodeMemberMethod m, CodeTypeDeclaration d);

		/// <summary>Generates code for the specified method invoke expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeMethodInvokeExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x0600048C RID: 1164
		protected abstract void GenerateMethodInvokeExpression(CodeMethodInvokeExpression e);

		/// <summary>Generates code for the specified method reference expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x0600048D RID: 1165
		protected abstract void GenerateMethodReferenceExpression(CodeMethodReferenceExpression e);

		/// <summary>Generates code for the specified method return statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeMethodReturnStatement" /> that indicates the statement to generate code for. </param>
		// Token: 0x0600048E RID: 1166
		protected abstract void GenerateMethodReturnStatement(CodeMethodReturnStatement e);

		/// <summary>Generates code for the specified namespace.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeNamespace" /> that indicates the namespace to generate code for. </param>
		// Token: 0x0600048F RID: 1167 RVA: 0x0000EE8C File Offset: 0x0000D08C
		protected virtual void GenerateNamespace(CodeNamespace ns)
		{
			foreach (object obj in ns.Comments)
			{
				CodeCommentStatement statement = (CodeCommentStatement)obj;
				this.GenerateCommentStatement(statement);
			}
			this.GenerateNamespaceStart(ns);
			foreach (object obj2 in ns.Imports)
			{
				CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)obj2;
				if (codeNamespaceImport.LinePragma != null)
				{
					this.GenerateLinePragmaStart(codeNamespaceImport.LinePragma);
				}
				this.GenerateNamespaceImport(codeNamespaceImport);
				if (codeNamespaceImport.LinePragma != null)
				{
					this.GenerateLinePragmaEnd(codeNamespaceImport.LinePragma);
				}
			}
			this.output.WriteLine();
			this.GenerateTypes(ns);
			this.GenerateNamespaceEnd(ns);
		}

		/// <summary>Generates code for the start of a namespace.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeNamespace" /> that indicates the namespace to generate code for. </param>
		// Token: 0x06000490 RID: 1168
		protected abstract void GenerateNamespaceStart(CodeNamespace ns);

		/// <summary>Generates code for the end of a namespace.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeNamespace" /> that indicates the namespace to generate code for. </param>
		// Token: 0x06000491 RID: 1169
		protected abstract void GenerateNamespaceEnd(CodeNamespace ns);

		/// <summary>Generates code for the specified namespace import.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeNamespaceImport" /> that indicates the namespace import to generate code for. </param>
		// Token: 0x06000492 RID: 1170
		protected abstract void GenerateNamespaceImport(CodeNamespaceImport i);

		/// <summary>Generates code for the specified namespace import.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeNamespace" /> that indicates the namespace import to generate code for. </param>
		// Token: 0x06000493 RID: 1171 RVA: 0x0000EFB0 File Offset: 0x0000D1B0
		protected void GenerateNamespaceImports(CodeNamespace e)
		{
			foreach (object obj in e.Imports)
			{
				CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)obj;
				if (codeNamespaceImport.LinePragma != null)
				{
					this.GenerateLinePragmaStart(codeNamespaceImport.LinePragma);
				}
				this.GenerateNamespaceImport(codeNamespaceImport);
				if (codeNamespaceImport.LinePragma != null)
				{
					this.GenerateLinePragmaEnd(codeNamespaceImport.LinePragma);
				}
			}
		}

		/// <summary>Generates code for the namespaces in the specified compile unit.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeCompileUnit" /> that indicates the compile unit to generate namespaces for. </param>
		// Token: 0x06000494 RID: 1172 RVA: 0x0000F050 File Offset: 0x0000D250
		protected void GenerateNamespaces(CodeCompileUnit e)
		{
			foreach (object obj in e.Namespaces)
			{
				CodeNamespace ns = (CodeNamespace)obj;
				this.GenerateNamespace(ns);
			}
		}

		/// <summary>Generates code for the specified object creation expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x06000495 RID: 1173
		protected abstract void GenerateObjectCreateExpression(CodeObjectCreateExpression e);

		/// <summary>Generates code for the specified parameter declaration expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x06000496 RID: 1174 RVA: 0x0000F0C0 File Offset: 0x0000D2C0
		protected virtual void GenerateParameterDeclarationExpression(CodeParameterDeclarationExpression e)
		{
			if (e.CustomAttributes != null && e.CustomAttributes.Count > 0)
			{
				this.OutputAttributeDeclarations(e.CustomAttributes);
			}
			this.OutputDirection(e.Direction);
			this.OutputType(e.Type);
			this.output.Write(' ');
			this.output.Write(e.Name);
		}

		/// <summary>Generates code for the specified primitive expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodePrimitiveExpression" /> that indicates the expression to generate code for. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="e" /> uses an invalid data type. Only the following data types are valid:stringcharbyteInt16Int32Int64SingleDoubleDecimal</exception>
		// Token: 0x06000497 RID: 1175 RVA: 0x0000F12C File Offset: 0x0000D32C
		protected virtual void GeneratePrimitiveExpression(CodePrimitiveExpression e)
		{
			object value = e.Value;
			if (value == null)
			{
				this.output.Write(this.NullToken);
				return;
			}
			Type type = value.GetType();
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				this.output.Write(value.ToString().ToLower(CultureInfo.InvariantCulture));
				return;
			case TypeCode.Char:
				this.output.Write("'" + value.ToString() + "'");
				return;
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.Int32:
			case TypeCode.Int64:
				this.output.Write(((IFormattable)value).ToString(null, CultureInfo.InvariantCulture));
				return;
			case TypeCode.Single:
				this.GenerateSingleFloatValue((float)value);
				return;
			case TypeCode.Double:
				this.GenerateDoubleValue((double)value);
				return;
			case TypeCode.Decimal:
				this.GenerateDecimalValue((decimal)value);
				return;
			case TypeCode.String:
				this.output.Write(this.QuoteSnippetString((string)value));
				return;
			}
			throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid Primitive Type: {0}. Only CLS compliant primitive types can be used. Consider using CodeObjectCreateExpression.", new object[]
			{
				type.FullName
			}));
		}

		/// <summary>Generates code for the specified property.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeMemberProperty" /> that indicates the property to generate code for. </param>
		/// <param name="c">A <see cref="T:System.CodeDom.CodeTypeDeclaration" /> that indicates the type of the object that this property occurs on. </param>
		// Token: 0x06000498 RID: 1176
		protected abstract void GenerateProperty(CodeMemberProperty p, CodeTypeDeclaration d);

		/// <summary>Generates code for the specified property reference expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodePropertyReferenceExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x06000499 RID: 1177
		protected abstract void GeneratePropertyReferenceExpression(CodePropertyReferenceExpression e);

		/// <summary>Generates code for the specified property set value reference expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodePropertySetValueReferenceExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x0600049A RID: 1178
		protected abstract void GeneratePropertySetValueReferenceExpression(CodePropertySetValueReferenceExpression e);

		/// <summary>Generates code for the specified remove event statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeRemoveEventStatement" /> that indicates the statement to generate code for. </param>
		// Token: 0x0600049B RID: 1179
		protected abstract void GenerateRemoveEventStatement(CodeRemoveEventStatement statement);

		/// <summary>Generates code for a single-precision floating point number.</summary>
		/// <param name="s">The <see cref="T:System.Single" /> value to generate code for. </param>
		// Token: 0x0600049C RID: 1180 RVA: 0x0000F290 File Offset: 0x0000D490
		protected virtual void GenerateSingleFloatValue(float s)
		{
			this.output.Write(s.ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>Outputs the code of the specified literal code fragment compile unit.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeSnippetCompileUnit" /> that indicates the literal code fragment compile unit to generate code for. </param>
		// Token: 0x0600049D RID: 1181 RVA: 0x0000F2AC File Offset: 0x0000D4AC
		protected virtual void GenerateSnippetCompileUnit(CodeSnippetCompileUnit e)
		{
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaStart(e.LinePragma);
			}
			this.output.WriteLine(e.Value);
			if (e.LinePragma != null)
			{
				this.GenerateLinePragmaEnd(e.LinePragma);
			}
		}

		/// <summary>Outputs the code of the specified literal code fragment expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeSnippetExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x0600049E RID: 1182
		protected abstract void GenerateSnippetExpression(CodeSnippetExpression e);

		/// <summary>Outputs the code of the specified literal code fragment class member.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeSnippetTypeMember" /> that indicates the member to generate code for. </param>
		// Token: 0x0600049F RID: 1183
		protected abstract void GenerateSnippetMember(CodeSnippetTypeMember m);

		/// <summary>Outputs the code of the specified literal code fragment statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeSnippetStatement" /> that indicates the statement to generate code for. </param>
		// Token: 0x060004A0 RID: 1184 RVA: 0x0000F2F8 File Offset: 0x0000D4F8
		protected virtual void GenerateSnippetStatement(CodeSnippetStatement s)
		{
			this.output.WriteLine(s.Value);
		}

		/// <summary>Generates code for the specified statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeStatement" /> that indicates the statement to generate code for. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="e" /> is not a valid <see cref="T:System.CodeDom.CodeStatement" />.</exception>
		// Token: 0x060004A1 RID: 1185 RVA: 0x0000F30C File Offset: 0x0000D50C
		protected void GenerateStatement(CodeStatement s)
		{
			if (s.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(s.StartDirectives);
			}
			if (s.LinePragma != null)
			{
				this.GenerateLinePragmaStart(s.LinePragma);
			}
			CodeSnippetStatement codeSnippetStatement = s as CodeSnippetStatement;
			if (codeSnippetStatement != null)
			{
				int indent = this.Indent;
				try
				{
					this.Indent = 0;
					this.GenerateSnippetStatement(codeSnippetStatement);
				}
				finally
				{
					this.Indent = indent;
				}
			}
			else
			{
				try
				{
					s.Accept(this.visitor);
				}
				catch (NotImplementedException)
				{
					throw new ArgumentException("Element type " + s.GetType() + " is not supported.", "s");
				}
			}
			if (s.LinePragma != null)
			{
				this.GenerateLinePragmaEnd(s.LinePragma);
			}
			if (s.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(s.EndDirectives);
			}
		}

		/// <summary>Generates code for the specified statement collection.</summary>
		/// <param name="stms">A <see cref="T:System.CodeDom.CodeStatementCollection" /> that indicates the statements to generate code for. </param>
		// Token: 0x060004A2 RID: 1186 RVA: 0x0000F420 File Offset: 0x0000D620
		protected void GenerateStatements(CodeStatementCollection c)
		{
			foreach (object obj in c)
			{
				CodeStatement s = (CodeStatement)obj;
				this.GenerateStatement(s);
			}
		}

		/// <summary>Generates code for the specified this reference expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeThisReferenceExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x060004A3 RID: 1187
		protected abstract void GenerateThisReferenceExpression(CodeThisReferenceExpression e);

		/// <summary>Generates code for the specified throw exception statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeThrowExceptionStatement" /> that indicates the statement to generate code for. </param>
		// Token: 0x060004A4 RID: 1188
		protected abstract void GenerateThrowExceptionStatement(CodeThrowExceptionStatement s);

		/// <summary>Generates code for the specified try...catch...finally statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeTryCatchFinallyStatement" /> that indicates the statement to generate code for. </param>
		// Token: 0x060004A5 RID: 1189
		protected abstract void GenerateTryCatchFinallyStatement(CodeTryCatchFinallyStatement s);

		/// <summary>Generates code for the specified end class.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeTypeDeclaration" /> that indicates the end of the class to generate code for. </param>
		// Token: 0x060004A6 RID: 1190
		protected abstract void GenerateTypeEnd(CodeTypeDeclaration declaration);

		/// <summary>Generates code for the specified class constructor.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeTypeConstructor" /> that indicates the class constructor to generate code for. </param>
		// Token: 0x060004A7 RID: 1191
		protected abstract void GenerateTypeConstructor(CodeTypeConstructor constructor);

		/// <summary>Generates code for the specified type of expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeTypeOfExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x060004A8 RID: 1192 RVA: 0x0000F48C File Offset: 0x0000D68C
		protected virtual void GenerateTypeOfExpression(CodeTypeOfExpression e)
		{
			this.output.Write("typeof(");
			this.OutputType(e.Type);
			this.output.Write(")");
		}

		/// <summary>Generates code for the specified type reference expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x060004A9 RID: 1193 RVA: 0x0000F4C8 File Offset: 0x0000D6C8
		protected virtual void GenerateTypeReferenceExpression(CodeTypeReferenceExpression e)
		{
			this.OutputType(e.Type);
		}

		/// <summary>Generates code for the specified namespace and the classes it contains.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeNamespace" /> that indicates the namespace to generate classes for. </param>
		// Token: 0x060004AA RID: 1194 RVA: 0x0000F4D8 File Offset: 0x0000D6D8
		protected void GenerateTypes(CodeNamespace e)
		{
			foreach (object obj in e.Types)
			{
				CodeTypeDeclaration type = (CodeTypeDeclaration)obj;
				if (this.options.BlankLinesBetweenMembers)
				{
					this.output.WriteLine();
				}
				this.GenerateType(type);
			}
		}

		/// <summary>Generates code for the specified start class.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeTypeDeclaration" /> that indicates the start of the class to generate code for. </param>
		// Token: 0x060004AB RID: 1195
		protected abstract void GenerateTypeStart(CodeTypeDeclaration declaration);

		/// <summary>Generates code for the specified variable declaration statement.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> that indicates the statement to generate code for. </param>
		// Token: 0x060004AC RID: 1196
		protected abstract void GenerateVariableDeclarationStatement(CodeVariableDeclarationStatement e);

		/// <summary>Generates code for the specified variable reference expression.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeVariableReferenceExpression" /> that indicates the expression to generate code for. </param>
		// Token: 0x060004AD RID: 1197
		protected abstract void GenerateVariableReferenceExpression(CodeVariableReferenceExpression e);

		/// <summary>Outputs an argument in an attribute block.</summary>
		/// <param name="arg">A <see cref="T:System.CodeDom.CodeAttributeArgument" /> that indicates the attribute argument to generate code for. </param>
		// Token: 0x060004AE RID: 1198 RVA: 0x0000F564 File Offset: 0x0000D764
		protected virtual void OutputAttributeArgument(CodeAttributeArgument argument)
		{
			string name = argument.Name;
			if (name != null && name.Length > 0)
			{
				this.output.Write(name);
				this.output.Write('=');
			}
			this.GenerateExpression(argument.Value);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000F5B0 File Offset: 0x0000D7B0
		private void OutputAttributeDeclaration(CodeAttributeDeclaration attribute)
		{
			this.output.Write(attribute.Name.Replace('+', '.'));
			this.output.Write('(');
			IEnumerator enumerator = attribute.Arguments.GetEnumerator();
			if (enumerator.MoveNext())
			{
				CodeAttributeArgument argument = (CodeAttributeArgument)enumerator.Current;
				this.OutputAttributeArgument(argument);
				while (enumerator.MoveNext())
				{
					this.output.Write(',');
					argument = (CodeAttributeArgument)enumerator.Current;
					this.OutputAttributeArgument(argument);
				}
			}
			this.output.Write(')');
		}

		/// <summary>Generates code for the specified attribute declaration collection.</summary>
		/// <param name="attributes">A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the attributes to generate code for. </param>
		// Token: 0x060004B0 RID: 1200 RVA: 0x0000F64C File Offset: 0x0000D84C
		protected virtual void OutputAttributeDeclarations(CodeAttributeDeclarationCollection attributes)
		{
			this.GenerateAttributeDeclarationsStart(attributes);
			IEnumerator enumerator = attributes.GetEnumerator();
			if (enumerator.MoveNext())
			{
				CodeAttributeDeclaration attribute = (CodeAttributeDeclaration)enumerator.Current;
				this.OutputAttributeDeclaration(attribute);
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					attribute = (CodeAttributeDeclaration)obj;
					this.output.WriteLine(',');
					this.OutputAttributeDeclaration(attribute);
				}
			}
			this.GenerateAttributeDeclarationsEnd(attributes);
		}

		/// <summary>Generates code for the specified <see cref="T:System.CodeDom.FieldDirection" />.</summary>
		/// <param name="dir">A <see cref="T:System.CodeDom.FieldDirection" /> enumeration value indicating the attribute of the field. </param>
		// Token: 0x060004B1 RID: 1201 RVA: 0x0000F6BC File Offset: 0x0000D8BC
		protected virtual void OutputDirection(FieldDirection direction)
		{
			switch (direction)
			{
			case FieldDirection.Out:
				this.output.Write("out ");
				break;
			case FieldDirection.Ref:
				this.output.Write("ref ");
				break;
			}
		}

		/// <summary>Generates code for the specified expression list.</summary>
		/// <param name="expressions">A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the expressions to generate code for. </param>
		// Token: 0x060004B2 RID: 1202 RVA: 0x0000F714 File Offset: 0x0000D914
		protected virtual void OutputExpressionList(CodeExpressionCollection expressions)
		{
			this.OutputExpressionList(expressions, false);
		}

		/// <summary>Generates code for the specified expression list.</summary>
		/// <param name="expressions">A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the expressions to generate code for. </param>
		/// <param name="newlineBetweenItems">true to insert a new line after each item; otherwise, false. </param>
		// Token: 0x060004B3 RID: 1203 RVA: 0x0000F720 File Offset: 0x0000D920
		protected virtual void OutputExpressionList(CodeExpressionCollection expressions, bool newLineBetweenItems)
		{
			this.Indent++;
			IEnumerator enumerator = expressions.GetEnumerator();
			if (enumerator.MoveNext())
			{
				CodeExpression e = (CodeExpression)enumerator.Current;
				this.GenerateExpression(e);
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					e = (CodeExpression)obj;
					this.output.Write(',');
					if (newLineBetweenItems)
					{
						this.output.WriteLine();
					}
					else
					{
						this.output.Write(' ');
					}
					this.GenerateExpression(e);
				}
			}
			this.Indent--;
		}

		/// <summary>Outputs a field scope modifier that corresponds to the specified attributes.</summary>
		/// <param name="attributes">A <see cref="T:System.CodeDom.MemberAttributes" /> enumeration value indicating the attributes. </param>
		// Token: 0x060004B4 RID: 1204 RVA: 0x0000F7C4 File Offset: 0x0000D9C4
		protected virtual void OutputFieldScopeModifier(MemberAttributes attributes)
		{
			if ((attributes & MemberAttributes.VTableMask) == MemberAttributes.New)
			{
				this.output.Write("new ");
			}
			switch (attributes & MemberAttributes.ScopeMask)
			{
			case MemberAttributes.Static:
				this.output.Write("static ");
				break;
			case MemberAttributes.Const:
				this.output.Write("const ");
				break;
			}
		}

		/// <summary>Outputs the specified identifier.</summary>
		/// <param name="ident">The identifier to output. </param>
		// Token: 0x060004B5 RID: 1205 RVA: 0x0000F838 File Offset: 0x0000DA38
		protected virtual void OutputIdentifier(string ident)
		{
			this.output.Write(ident);
		}

		/// <summary>Generates code for the specified member access modifier.</summary>
		/// <param name="attributes">A <see cref="T:System.CodeDom.MemberAttributes" /> enumeration value indicating the member access modifier to generate code for. </param>
		// Token: 0x060004B6 RID: 1206 RVA: 0x0000F848 File Offset: 0x0000DA48
		protected virtual void OutputMemberAccessModifier(MemberAttributes attributes)
		{
			MemberAttributes memberAttributes = attributes & MemberAttributes.AccessMask;
			if (memberAttributes != MemberAttributes.Assembly)
			{
				if (memberAttributes != MemberAttributes.FamilyAndAssembly)
				{
					if (memberAttributes != MemberAttributes.Family)
					{
						if (memberAttributes != MemberAttributes.FamilyOrAssembly)
						{
							if (memberAttributes != MemberAttributes.Private)
							{
								if (memberAttributes == MemberAttributes.Public)
								{
									this.output.Write("public ");
								}
							}
							else
							{
								this.output.Write("private ");
							}
						}
						else
						{
							this.output.Write("protected internal ");
						}
					}
					else
					{
						this.output.Write("protected ");
					}
				}
				else
				{
					this.output.Write("internal ");
				}
			}
			else
			{
				this.output.Write("internal ");
			}
		}

		/// <summary>Generates code for the specified member scope modifier.</summary>
		/// <param name="attributes">A <see cref="T:System.CodeDom.MemberAttributes" /> enumeration value indicating the member scope modifier to generate code for. </param>
		// Token: 0x060004B7 RID: 1207 RVA: 0x0000F924 File Offset: 0x0000DB24
		protected virtual void OutputMemberScopeModifier(MemberAttributes attributes)
		{
			if ((attributes & MemberAttributes.VTableMask) == MemberAttributes.New)
			{
				this.output.Write("new ");
			}
			switch (attributes & MemberAttributes.ScopeMask)
			{
			case MemberAttributes.Abstract:
				this.output.Write("abstract ");
				break;
			case MemberAttributes.Final:
				break;
			case MemberAttributes.Static:
				this.output.Write("static ");
				break;
			case MemberAttributes.Override:
				this.output.Write("override ");
				break;
			default:
			{
				MemberAttributes memberAttributes = attributes & MemberAttributes.AccessMask;
				if (memberAttributes == MemberAttributes.Public || memberAttributes == MemberAttributes.Family)
				{
					this.output.Write("virtual ");
				}
				break;
			}
			}
		}

		/// <summary>Generates code for the specified operator.</summary>
		/// <param name="op">A <see cref="T:System.CodeDom.CodeBinaryOperatorType" /> that indicates the operator to generate code for. </param>
		// Token: 0x060004B8 RID: 1208 RVA: 0x0000F9E8 File Offset: 0x0000DBE8
		protected virtual void OutputOperator(CodeBinaryOperatorType op)
		{
			switch (op)
			{
			case CodeBinaryOperatorType.Add:
				this.output.Write("+");
				break;
			case CodeBinaryOperatorType.Subtract:
				this.output.Write("-");
				break;
			case CodeBinaryOperatorType.Multiply:
				this.output.Write("*");
				break;
			case CodeBinaryOperatorType.Divide:
				this.output.Write("/");
				break;
			case CodeBinaryOperatorType.Modulus:
				this.output.Write("%");
				break;
			case CodeBinaryOperatorType.Assign:
				this.output.Write("=");
				break;
			case CodeBinaryOperatorType.IdentityInequality:
				this.output.Write("!=");
				break;
			case CodeBinaryOperatorType.IdentityEquality:
				this.output.Write("==");
				break;
			case CodeBinaryOperatorType.ValueEquality:
				this.output.Write("==");
				break;
			case CodeBinaryOperatorType.BitwiseOr:
				this.output.Write("|");
				break;
			case CodeBinaryOperatorType.BitwiseAnd:
				this.output.Write("&");
				break;
			case CodeBinaryOperatorType.BooleanOr:
				this.output.Write("||");
				break;
			case CodeBinaryOperatorType.BooleanAnd:
				this.output.Write("&&");
				break;
			case CodeBinaryOperatorType.LessThan:
				this.output.Write("<");
				break;
			case CodeBinaryOperatorType.LessThanOrEqual:
				this.output.Write("<=");
				break;
			case CodeBinaryOperatorType.GreaterThan:
				this.output.Write(">");
				break;
			case CodeBinaryOperatorType.GreaterThanOrEqual:
				this.output.Write(">=");
				break;
			}
		}

		/// <summary>Generates code for the specified parameters.</summary>
		/// <param name="parameters">A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> that indicates the parameter declaration expressions to generate code for. </param>
		// Token: 0x060004B9 RID: 1209 RVA: 0x0000FBAC File Offset: 0x0000DDAC
		protected virtual void OutputParameters(CodeParameterDeclarationExpressionCollection parameters)
		{
			bool flag = true;
			foreach (object obj in parameters)
			{
				CodeParameterDeclarationExpression e = (CodeParameterDeclarationExpression)obj;
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.output.Write(", ");
				}
				this.GenerateExpression(e);
			}
		}

		/// <summary>Generates code for the specified type.</summary>
		/// <param name="typeRef">The type to generate code for. </param>
		// Token: 0x060004BA RID: 1210
		protected abstract void OutputType(CodeTypeReference t);

		/// <summary>Generates code for the specified type attributes.</summary>
		/// <param name="attributes">A <see cref="T:System.Reflection.TypeAttributes" /> enumeration value indicating the type attributes to generate code for. </param>
		/// <param name="isStruct">true if the type is a struct; otherwise, false. </param>
		/// <param name="isEnum">true if the type is an enum; otherwise, false. </param>
		// Token: 0x060004BB RID: 1211 RVA: 0x0000FC38 File Offset: 0x0000DE38
		protected virtual void OutputTypeAttributes(TypeAttributes attributes, bool isStruct, bool isEnum)
		{
			switch (attributes & TypeAttributes.VisibilityMask)
			{
			case TypeAttributes.Public:
			case TypeAttributes.NestedPublic:
				this.output.Write("public ");
				break;
			case TypeAttributes.NestedPrivate:
				this.output.Write("private ");
				break;
			}
			if (isStruct)
			{
				this.output.Write("struct ");
			}
			else if (isEnum)
			{
				this.output.Write("enum ");
			}
			else if ((attributes & TypeAttributes.ClassSemanticsMask) != TypeAttributes.NotPublic)
			{
				this.output.Write("interface ");
			}
			else if (this.currentType is CodeTypeDelegate)
			{
				this.output.Write("delegate ");
			}
			else
			{
				if ((attributes & TypeAttributes.Sealed) != TypeAttributes.NotPublic)
				{
					this.output.Write("sealed ");
				}
				if ((attributes & TypeAttributes.Abstract) != TypeAttributes.NotPublic)
				{
					this.output.Write("abstract ");
				}
				this.output.Write("class ");
			}
		}

		/// <summary>Generates code for the specified object type and name pair.</summary>
		/// <param name="typeRef">The type. </param>
		/// <param name="name">The name for the object. </param>
		// Token: 0x060004BC RID: 1212 RVA: 0x0000FD54 File Offset: 0x0000DF54
		protected virtual void OutputTypeNamePair(CodeTypeReference type, string name)
		{
			this.OutputType(type);
			this.output.Write(' ');
			this.output.Write(name);
		}

		/// <summary>Converts the specified string by formatting it with escape codes.</summary>
		/// <returns>The converted string.</returns>
		/// <param name="value">The string to convert. </param>
		// Token: 0x060004BD RID: 1213
		protected abstract string QuoteSnippetString(string value);

		/// <summary>Creates an escaped identifier for the specified value.</summary>
		/// <returns>The escaped identifier for the value.</returns>
		/// <param name="value">The string to create an escaped identifier for. </param>
		// Token: 0x060004BE RID: 1214
		protected abstract string CreateEscapedIdentifier(string value);

		/// <summary>Creates a valid identifier for the specified value.</summary>
		/// <returns>A valid identifier for the value.</returns>
		/// <param name="value">A string to create a valid identifier for. </param>
		// Token: 0x060004BF RID: 1215
		protected abstract string CreateValidIdentifier(string value);

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000FD84 File Offset: 0x0000DF84
		private void InitOutput(TextWriter output, CodeGeneratorOptions options)
		{
			if (options == null)
			{
				options = new CodeGeneratorOptions();
			}
			this.output = new IndentedTextWriter(output, options.IndentString);
			this.options = options;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000FDB8 File Offset: 0x0000DFB8
		private void GenerateType(CodeTypeDeclaration type)
		{
			this.currentType = type;
			this.currentMember = null;
			if (type.StartDirectives.Count > 0)
			{
				this.GenerateDirectives(type.StartDirectives);
			}
			foreach (object obj in type.Comments)
			{
				CodeCommentStatement statement = (CodeCommentStatement)obj;
				this.GenerateCommentStatement(statement);
			}
			if (type.LinePragma != null)
			{
				this.GenerateLinePragmaStart(type.LinePragma);
			}
			this.GenerateTypeStart(type);
			CodeTypeMember[] array = new CodeTypeMember[type.Members.Count];
			type.Members.CopyTo(array, 0);
			if (!this.Options.VerbatimOrder)
			{
				int[] array2 = new int[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = Array.IndexOf<Type>(CodeGenerator.memberTypes, array[i].GetType()) * array.Length + i;
				}
				Array.Sort<int, CodeTypeMember>(array2, array);
			}
			CodeTypeDeclaration codeTypeDeclaration = null;
			foreach (CodeTypeMember codeTypeMember in array)
			{
				CodeTypeMember codeTypeMember2 = this.currentMember;
				this.currentMember = codeTypeMember;
				if (codeTypeMember2 != null && codeTypeDeclaration == null)
				{
					if (codeTypeMember2.LinePragma != null)
					{
						this.GenerateLinePragmaEnd(codeTypeMember2.LinePragma);
					}
					if (codeTypeMember2.EndDirectives.Count > 0)
					{
						this.GenerateDirectives(codeTypeMember2.EndDirectives);
					}
				}
				if (this.options.BlankLinesBetweenMembers)
				{
					this.output.WriteLine();
				}
				codeTypeDeclaration = (codeTypeMember as CodeTypeDeclaration);
				if (codeTypeDeclaration != null)
				{
					this.GenerateType(codeTypeDeclaration);
					this.currentType = type;
				}
				else
				{
					if (this.currentMember.StartDirectives.Count > 0)
					{
						this.GenerateDirectives(this.currentMember.StartDirectives);
					}
					foreach (object obj2 in codeTypeMember.Comments)
					{
						CodeCommentStatement statement2 = (CodeCommentStatement)obj2;
						this.GenerateCommentStatement(statement2);
					}
					if (codeTypeMember.LinePragma != null)
					{
						this.GenerateLinePragmaStart(codeTypeMember.LinePragma);
					}
					try
					{
						codeTypeMember.Accept(this.visitor);
					}
					catch (NotImplementedException)
					{
						throw new ArgumentException("Element type " + codeTypeMember.GetType() + " is not supported.");
					}
				}
			}
			if (this.currentMember != null && !(this.currentMember is CodeTypeDeclaration))
			{
				if (this.currentMember.LinePragma != null)
				{
					this.GenerateLinePragmaEnd(this.currentMember.LinePragma);
				}
				if (this.currentMember.EndDirectives.Count > 0)
				{
					this.GenerateDirectives(this.currentMember.EndDirectives);
				}
			}
			this.currentType = type;
			this.GenerateTypeEnd(type);
			if (type.LinePragma != null)
			{
				this.GenerateLinePragmaEnd(type.LinePragma);
			}
			if (type.EndDirectives.Count > 0)
			{
				this.GenerateDirectives(type.EndDirectives);
			}
		}

		/// <summary>Gets the name of the specified data type.</summary>
		/// <returns>The name of the data type reference.</returns>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeTypeReference" /> of the type to return the name of. </param>
		// Token: 0x060004C2 RID: 1218
		protected abstract string GetTypeOutput(CodeTypeReference type);

		/// <summary>Gets a value indicating whether the specified value is a valid identifier.</summary>
		/// <returns>true if the value is a valid identifier; otherwise, false.</returns>
		/// <param name="value">The value to test for conflicts with valid identifiers. </param>
		// Token: 0x060004C3 RID: 1219
		protected abstract bool IsValidIdentifier(string value);

		/// <summary>Gets a value indicating whether the specified string is a valid identifier.</summary>
		/// <returns>true if the specified string is a valid identifier; otherwise, false.</returns>
		/// <param name="value">The string to test for validity. </param>
		// Token: 0x060004C4 RID: 1220 RVA: 0x0001013C File Offset: 0x0000E33C
		public static bool IsValidLanguageIndependentIdentifier(string value)
		{
			if (value == null)
			{
				return false;
			}
			if (value.Equals(string.Empty))
			{
				return false;
			}
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory(value[0]);
			switch (unicodeCategory)
			{
			case UnicodeCategory.UppercaseLetter:
			case UnicodeCategory.LowercaseLetter:
			case UnicodeCategory.TitlecaseLetter:
			case UnicodeCategory.ModifierLetter:
			case UnicodeCategory.OtherLetter:
			case UnicodeCategory.LetterNumber:
				break;
			default:
				if (unicodeCategory != UnicodeCategory.ConnectorPunctuation)
				{
					return false;
				}
				break;
			}
			int i = 1;
			while (i < value.Length)
			{
				switch (char.GetUnicodeCategory(value[i]))
				{
				case UnicodeCategory.UppercaseLetter:
				case UnicodeCategory.LowercaseLetter:
				case UnicodeCategory.TitlecaseLetter:
				case UnicodeCategory.ModifierLetter:
				case UnicodeCategory.OtherLetter:
				case UnicodeCategory.NonSpacingMark:
				case UnicodeCategory.SpacingCombiningMark:
				case UnicodeCategory.DecimalDigitNumber:
				case UnicodeCategory.LetterNumber:
				case UnicodeCategory.Format:
				case UnicodeCategory.ConnectorPunctuation:
					i++;
					continue;
				}
				return false;
			}
			return true;
		}

		/// <summary>Gets a value indicating whether the specified code generation support is provided.</summary>
		/// <returns>true if the specified code generation support is provided; otherwise, false.</returns>
		/// <param name="support">A <see cref="T:System.CodeDom.Compiler.GeneratorSupport" /> that indicates the type of code generation support to test for. </param>
		// Token: 0x060004C5 RID: 1221
		protected abstract bool Supports(GeneratorSupport supports);

		/// <summary>Throws an exception if the specified string is not a valid identifier.</summary>
		/// <param name="value">The identifier to test for validity as an identifier. </param>
		/// <exception cref="T:System.ArgumentException">If the specified identifier is invalid or conflicts with reserved or language keywords. </exception>
		// Token: 0x060004C6 RID: 1222 RVA: 0x00010238 File Offset: 0x0000E438
		protected virtual void ValidateIdentifier(string value)
		{
			if (!this.IsValidIdentifier(value))
			{
				throw new ArgumentException("Identifier is invalid", "value");
			}
		}

		/// <summary>Attempts to validate each identifier field contained in the specified <see cref="T:System.CodeDom.CodeObject" /> or <see cref="N:System.CodeDom" /> tree.</summary>
		/// <param name="e">A <see cref="T:System.CodeDom.CodeObject" /> to test for invalid identifiers. </param>
		/// <exception cref="T:System.ArgumentException">The specified <see cref="T:System.CodeDom.CodeObject" /> contains an invalid identifier. </exception>
		// Token: 0x060004C7 RID: 1223 RVA: 0x00010258 File Offset: 0x0000E458
		[MonoTODO]
		public static void ValidateIdentifiers(CodeObject e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates code for the specified code directives.</summary>
		/// <param name="directives">A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> to generate code for.</param>
		// Token: 0x060004C8 RID: 1224 RVA: 0x00010260 File Offset: 0x0000E460
		protected virtual void GenerateDirectives(CodeDirectiveCollection directives)
		{
		}

		// Token: 0x04000124 RID: 292
		private IndentedTextWriter output;

		// Token: 0x04000125 RID: 293
		private CodeGeneratorOptions options;

		// Token: 0x04000126 RID: 294
		private CodeTypeMember currentMember;

		// Token: 0x04000127 RID: 295
		private CodeTypeDeclaration currentType;

		// Token: 0x04000128 RID: 296
		private CodeGenerator.Visitor visitor;

		// Token: 0x04000129 RID: 297
		private static Type[] memberTypes = new Type[]
		{
			typeof(CodeMemberField),
			typeof(CodeSnippetTypeMember),
			typeof(CodeTypeConstructor),
			typeof(CodeConstructor),
			typeof(CodeMemberProperty),
			typeof(CodeMemberEvent),
			typeof(CodeMemberMethod),
			typeof(CodeTypeDeclaration),
			typeof(CodeEntryPointMethod)
		};

		// Token: 0x0200007A RID: 122
		internal class Visitor : ICodeDomVisitor
		{
			// Token: 0x060004C9 RID: 1225 RVA: 0x00010264 File Offset: 0x0000E464
			public Visitor(CodeGenerator generator)
			{
				this.g = generator;
			}

			// Token: 0x060004CA RID: 1226 RVA: 0x00010274 File Offset: 0x0000E474
			public void Visit(CodeArgumentReferenceExpression o)
			{
				this.g.GenerateArgumentReferenceExpression(o);
			}

			// Token: 0x060004CB RID: 1227 RVA: 0x00010284 File Offset: 0x0000E484
			public void Visit(CodeArrayCreateExpression o)
			{
				this.g.GenerateArrayCreateExpression(o);
			}

			// Token: 0x060004CC RID: 1228 RVA: 0x00010294 File Offset: 0x0000E494
			public void Visit(CodeArrayIndexerExpression o)
			{
				this.g.GenerateArrayIndexerExpression(o);
			}

			// Token: 0x060004CD RID: 1229 RVA: 0x000102A4 File Offset: 0x0000E4A4
			public void Visit(CodeBaseReferenceExpression o)
			{
				this.g.GenerateBaseReferenceExpression(o);
			}

			// Token: 0x060004CE RID: 1230 RVA: 0x000102B4 File Offset: 0x0000E4B4
			public void Visit(CodeBinaryOperatorExpression o)
			{
				this.g.GenerateBinaryOperatorExpression(o);
			}

			// Token: 0x060004CF RID: 1231 RVA: 0x000102C4 File Offset: 0x0000E4C4
			public void Visit(CodeCastExpression o)
			{
				this.g.GenerateCastExpression(o);
			}

			// Token: 0x060004D0 RID: 1232 RVA: 0x000102D4 File Offset: 0x0000E4D4
			public void Visit(CodeDefaultValueExpression o)
			{
				this.g.GenerateDefaultValueExpression(o);
			}

			// Token: 0x060004D1 RID: 1233 RVA: 0x000102E4 File Offset: 0x0000E4E4
			public void Visit(CodeDelegateCreateExpression o)
			{
				this.g.GenerateDelegateCreateExpression(o);
			}

			// Token: 0x060004D2 RID: 1234 RVA: 0x000102F4 File Offset: 0x0000E4F4
			public void Visit(CodeDelegateInvokeExpression o)
			{
				this.g.GenerateDelegateInvokeExpression(o);
			}

			// Token: 0x060004D3 RID: 1235 RVA: 0x00010304 File Offset: 0x0000E504
			public void Visit(CodeDirectionExpression o)
			{
				this.g.GenerateDirectionExpression(o);
			}

			// Token: 0x060004D4 RID: 1236 RVA: 0x00010314 File Offset: 0x0000E514
			public void Visit(CodeEventReferenceExpression o)
			{
				this.g.GenerateEventReferenceExpression(o);
			}

			// Token: 0x060004D5 RID: 1237 RVA: 0x00010324 File Offset: 0x0000E524
			public void Visit(CodeFieldReferenceExpression o)
			{
				this.g.GenerateFieldReferenceExpression(o);
			}

			// Token: 0x060004D6 RID: 1238 RVA: 0x00010334 File Offset: 0x0000E534
			public void Visit(CodeIndexerExpression o)
			{
				this.g.GenerateIndexerExpression(o);
			}

			// Token: 0x060004D7 RID: 1239 RVA: 0x00010344 File Offset: 0x0000E544
			public void Visit(CodeMethodInvokeExpression o)
			{
				this.g.GenerateMethodInvokeExpression(o);
			}

			// Token: 0x060004D8 RID: 1240 RVA: 0x00010354 File Offset: 0x0000E554
			public void Visit(CodeMethodReferenceExpression o)
			{
				this.g.GenerateMethodReferenceExpression(o);
			}

			// Token: 0x060004D9 RID: 1241 RVA: 0x00010364 File Offset: 0x0000E564
			public void Visit(CodeObjectCreateExpression o)
			{
				this.g.GenerateObjectCreateExpression(o);
			}

			// Token: 0x060004DA RID: 1242 RVA: 0x00010374 File Offset: 0x0000E574
			public void Visit(CodeParameterDeclarationExpression o)
			{
				this.g.GenerateParameterDeclarationExpression(o);
			}

			// Token: 0x060004DB RID: 1243 RVA: 0x00010384 File Offset: 0x0000E584
			public void Visit(CodePrimitiveExpression o)
			{
				this.g.GeneratePrimitiveExpression(o);
			}

			// Token: 0x060004DC RID: 1244 RVA: 0x00010394 File Offset: 0x0000E594
			public void Visit(CodePropertyReferenceExpression o)
			{
				this.g.GeneratePropertyReferenceExpression(o);
			}

			// Token: 0x060004DD RID: 1245 RVA: 0x000103A4 File Offset: 0x0000E5A4
			public void Visit(CodePropertySetValueReferenceExpression o)
			{
				this.g.GeneratePropertySetValueReferenceExpression(o);
			}

			// Token: 0x060004DE RID: 1246 RVA: 0x000103B4 File Offset: 0x0000E5B4
			public void Visit(CodeSnippetExpression o)
			{
				this.g.GenerateSnippetExpression(o);
			}

			// Token: 0x060004DF RID: 1247 RVA: 0x000103C4 File Offset: 0x0000E5C4
			public void Visit(CodeThisReferenceExpression o)
			{
				this.g.GenerateThisReferenceExpression(o);
			}

			// Token: 0x060004E0 RID: 1248 RVA: 0x000103D4 File Offset: 0x0000E5D4
			public void Visit(CodeTypeOfExpression o)
			{
				this.g.GenerateTypeOfExpression(o);
			}

			// Token: 0x060004E1 RID: 1249 RVA: 0x000103E4 File Offset: 0x0000E5E4
			public void Visit(CodeTypeReferenceExpression o)
			{
				this.g.GenerateTypeReferenceExpression(o);
			}

			// Token: 0x060004E2 RID: 1250 RVA: 0x000103F4 File Offset: 0x0000E5F4
			public void Visit(CodeVariableReferenceExpression o)
			{
				this.g.GenerateVariableReferenceExpression(o);
			}

			// Token: 0x060004E3 RID: 1251 RVA: 0x00010404 File Offset: 0x0000E604
			public void Visit(CodeAssignStatement o)
			{
				this.g.GenerateAssignStatement(o);
			}

			// Token: 0x060004E4 RID: 1252 RVA: 0x00010414 File Offset: 0x0000E614
			public void Visit(CodeAttachEventStatement o)
			{
				this.g.GenerateAttachEventStatement(o);
			}

			// Token: 0x060004E5 RID: 1253 RVA: 0x00010424 File Offset: 0x0000E624
			public void Visit(CodeCommentStatement o)
			{
				this.g.GenerateCommentStatement(o);
			}

			// Token: 0x060004E6 RID: 1254 RVA: 0x00010434 File Offset: 0x0000E634
			public void Visit(CodeConditionStatement o)
			{
				this.g.GenerateConditionStatement(o);
			}

			// Token: 0x060004E7 RID: 1255 RVA: 0x00010444 File Offset: 0x0000E644
			public void Visit(CodeExpressionStatement o)
			{
				this.g.GenerateExpressionStatement(o);
			}

			// Token: 0x060004E8 RID: 1256 RVA: 0x00010454 File Offset: 0x0000E654
			public void Visit(CodeGotoStatement o)
			{
				this.g.GenerateGotoStatement(o);
			}

			// Token: 0x060004E9 RID: 1257 RVA: 0x00010464 File Offset: 0x0000E664
			public void Visit(CodeIterationStatement o)
			{
				this.g.GenerateIterationStatement(o);
			}

			// Token: 0x060004EA RID: 1258 RVA: 0x00010474 File Offset: 0x0000E674
			public void Visit(CodeLabeledStatement o)
			{
				this.g.GenerateLabeledStatement(o);
			}

			// Token: 0x060004EB RID: 1259 RVA: 0x00010484 File Offset: 0x0000E684
			public void Visit(CodeMethodReturnStatement o)
			{
				this.g.GenerateMethodReturnStatement(o);
			}

			// Token: 0x060004EC RID: 1260 RVA: 0x00010494 File Offset: 0x0000E694
			public void Visit(CodeRemoveEventStatement o)
			{
				this.g.GenerateRemoveEventStatement(o);
			}

			// Token: 0x060004ED RID: 1261 RVA: 0x000104A4 File Offset: 0x0000E6A4
			public void Visit(CodeThrowExceptionStatement o)
			{
				this.g.GenerateThrowExceptionStatement(o);
			}

			// Token: 0x060004EE RID: 1262 RVA: 0x000104B4 File Offset: 0x0000E6B4
			public void Visit(CodeTryCatchFinallyStatement o)
			{
				this.g.GenerateTryCatchFinallyStatement(o);
			}

			// Token: 0x060004EF RID: 1263 RVA: 0x000104C4 File Offset: 0x0000E6C4
			public void Visit(CodeVariableDeclarationStatement o)
			{
				this.g.GenerateVariableDeclarationStatement(o);
			}

			// Token: 0x060004F0 RID: 1264 RVA: 0x000104D4 File Offset: 0x0000E6D4
			public void Visit(CodeConstructor o)
			{
				this.g.GenerateConstructor(o, this.g.CurrentClass);
			}

			// Token: 0x060004F1 RID: 1265 RVA: 0x000104F0 File Offset: 0x0000E6F0
			public void Visit(CodeEntryPointMethod o)
			{
				this.g.GenerateEntryPointMethod(o, this.g.CurrentClass);
			}

			// Token: 0x060004F2 RID: 1266 RVA: 0x0001050C File Offset: 0x0000E70C
			public void Visit(CodeMemberEvent o)
			{
				this.g.GenerateEvent(o, this.g.CurrentClass);
			}

			// Token: 0x060004F3 RID: 1267 RVA: 0x00010528 File Offset: 0x0000E728
			public void Visit(CodeMemberField o)
			{
				this.g.GenerateField(o);
			}

			// Token: 0x060004F4 RID: 1268 RVA: 0x00010538 File Offset: 0x0000E738
			public void Visit(CodeMemberMethod o)
			{
				this.g.GenerateMethod(o, this.g.CurrentClass);
			}

			// Token: 0x060004F5 RID: 1269 RVA: 0x00010554 File Offset: 0x0000E754
			public void Visit(CodeMemberProperty o)
			{
				this.g.GenerateProperty(o, this.g.CurrentClass);
			}

			// Token: 0x060004F6 RID: 1270 RVA: 0x00010570 File Offset: 0x0000E770
			public void Visit(CodeSnippetTypeMember o)
			{
				this.g.GenerateSnippetMember(o);
			}

			// Token: 0x060004F7 RID: 1271 RVA: 0x00010580 File Offset: 0x0000E780
			public void Visit(CodeTypeConstructor o)
			{
				this.g.GenerateTypeConstructor(o);
			}

			// Token: 0x0400012A RID: 298
			private CodeGenerator g;
		}
	}
}
