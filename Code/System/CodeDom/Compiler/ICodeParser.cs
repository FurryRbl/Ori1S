﻿using System;
using System.IO;

namespace System.CodeDom.Compiler
{
	/// <summary>Defines an interface for parsing code into a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
	// Token: 0x0200008C RID: 140
	public interface ICodeParser
	{
		/// <summary>When implemented in a derived class, compiles the specified text stream into a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> that contains a representation of the parsed code.</returns>
		/// <param name="codeStream">A <see cref="T:System.IO.TextReader" /> that can be used to read the code to be compiled. </param>
		// Token: 0x060005B3 RID: 1459
		CodeCompileUnit Parse(TextReader codeStream);
	}
}
