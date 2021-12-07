using System;
using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;

namespace SimpleJson
{
	// Token: 0x0200025F RID: 607
	[GeneratedCode("simple-json", "1.0.0")]
	internal interface IJsonSerializerStrategy
	{
		// Token: 0x06002452 RID: 9298
		[SuppressMessage("Microsoft.Design", "CA1007:UseGenericsWhereAppropriate", Justification = "Need to support .NET 2")]
		bool TrySerializeNonPrimitiveObject(object input, out object output);

		// Token: 0x06002453 RID: 9299
		object DeserializeObject(object value, Type type);
	}
}
