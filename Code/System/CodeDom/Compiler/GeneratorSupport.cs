using System;

namespace System.CodeDom.Compiler
{
	/// <summary>Defines identifiers used to determine whether a code generator supports certain types of code elements.</summary>
	// Token: 0x02000089 RID: 137
	[Flags]
	[Serializable]
	public enum GeneratorSupport
	{
		/// <summary>Indicates the generator supports arrays of arrays.</summary>
		// Token: 0x04000164 RID: 356
		ArraysOfArrays = 1,
		/// <summary>Indicates the generator supports a program entry point method designation. This is used when building executables.</summary>
		// Token: 0x04000165 RID: 357
		EntryPointMethod = 2,
		/// <summary>Indicates the generator supports goto statements.</summary>
		// Token: 0x04000166 RID: 358
		GotoStatements = 4,
		/// <summary>Indicates the generator supports referencing multidimensional arrays. Currently, the CodeDom cannot be used to instantiate multidimensional arrays.</summary>
		// Token: 0x04000167 RID: 359
		MultidimensionalArrays = 8,
		/// <summary>Indicates the generator supports static constructors.</summary>
		// Token: 0x04000168 RID: 360
		StaticConstructors = 16,
		/// <summary>Indicates the generator supports try...catch statements.</summary>
		// Token: 0x04000169 RID: 361
		TryCatchStatements = 32,
		/// <summary>Indicates the generator supports return type attribute declarations.</summary>
		// Token: 0x0400016A RID: 362
		ReturnTypeAttributes = 64,
		/// <summary>Indicates the generator supports value type declarations.</summary>
		// Token: 0x0400016B RID: 363
		DeclareValueTypes = 128,
		/// <summary>Indicates the generator supports enumeration declarations.</summary>
		// Token: 0x0400016C RID: 364
		DeclareEnums = 256,
		/// <summary>Indicates the generator supports delegate declarations.</summary>
		// Token: 0x0400016D RID: 365
		DeclareDelegates = 512,
		/// <summary>Indicates the generator supports interface declarations.</summary>
		// Token: 0x0400016E RID: 366
		DeclareInterfaces = 1024,
		/// <summary>Indicates the generator supports event declarations.</summary>
		// Token: 0x0400016F RID: 367
		DeclareEvents = 2048,
		/// <summary>Indicates the generator supports assembly attributes.</summary>
		// Token: 0x04000170 RID: 368
		AssemblyAttributes = 4096,
		/// <summary>Indicates the generator supports parameter attributes.</summary>
		// Token: 0x04000171 RID: 369
		ParameterAttributes = 8192,
		/// <summary>Indicates the generator supports reference and out parameters.</summary>
		// Token: 0x04000172 RID: 370
		ReferenceParameters = 16384,
		/// <summary>Indicates the generator supports chained constructor arguments.</summary>
		// Token: 0x04000173 RID: 371
		ChainedConstructorArguments = 32768,
		/// <summary>Indicates the generator supports the declaration of nested types.</summary>
		// Token: 0x04000174 RID: 372
		NestedTypes = 65536,
		/// <summary>Indicates the generator supports the declaration of members that implement multiple interfaces.</summary>
		// Token: 0x04000175 RID: 373
		MultipleInterfaceMembers = 131072,
		/// <summary>Indicates the generator supports public static members.</summary>
		// Token: 0x04000176 RID: 374
		PublicStaticMembers = 262144,
		/// <summary>Indicates the generator supports complex expressions.</summary>
		// Token: 0x04000177 RID: 375
		ComplexExpressions = 524288,
		/// <summary>Indicates the generator supports compilation with Win32 resources.</summary>
		// Token: 0x04000178 RID: 376
		Win32Resources = 1048576,
		/// <summary>Indicates the generator supports compilation with .NET Framework resources. These can be default resources compiled directly into an assembly, or resources referenced in a satellite assembly.</summary>
		// Token: 0x04000179 RID: 377
		Resources = 2097152,
		/// <summary>Indicates the generator supports partial type declarations.</summary>
		// Token: 0x0400017A RID: 378
		PartialTypes = 4194304,
		/// <summary>Indicates the generator supports generic type references.</summary>
		// Token: 0x0400017B RID: 379
		GenericTypeReference = 8388608,
		/// <summary>Indicates the generator supports generic type declarations.</summary>
		// Token: 0x0400017C RID: 380
		GenericTypeDeclaration = 16777216,
		/// <summary>Indicates the generator supports the declaration of indexer properties.</summary>
		// Token: 0x0400017D RID: 381
		DeclareIndexerProperties = 33554432
	}
}
