using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the W3C XML Schema Definition Language (XSD) schema types.</summary>
	// Token: 0x02000251 RID: 593
	public enum XmlTypeCode
	{
		/// <summary>No type information.</summary>
		// Token: 0x040009C7 RID: 2503
		None,
		/// <summary>An item such as a node or atomic value.</summary>
		// Token: 0x040009C8 RID: 2504
		Item,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x040009C9 RID: 2505
		Node,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x040009CA RID: 2506
		Document,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x040009CB RID: 2507
		Element,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x040009CC RID: 2508
		Attribute,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x040009CD RID: 2509
		Namespace,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x040009CE RID: 2510
		ProcessingInstruction,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x040009CF RID: 2511
		Comment,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x040009D0 RID: 2512
		Text,
		/// <summary>Any atomic value of a union.</summary>
		// Token: 0x040009D1 RID: 2513
		AnyAtomicType,
		/// <summary>An untyped atomic value.</summary>
		// Token: 0x040009D2 RID: 2514
		UntypedAtomic,
		/// <summary>A W3C XML Schema xs:string type.</summary>
		// Token: 0x040009D3 RID: 2515
		String,
		/// <summary>A W3C XML Schema xs:boolean type.</summary>
		// Token: 0x040009D4 RID: 2516
		Boolean,
		/// <summary>A W3C XML Schema xs:decimal type.</summary>
		// Token: 0x040009D5 RID: 2517
		Decimal,
		/// <summary>A W3C XML Schema xs:float type.</summary>
		// Token: 0x040009D6 RID: 2518
		Float,
		/// <summary>A W3C XML Schema xs:double type.</summary>
		// Token: 0x040009D7 RID: 2519
		Double,
		/// <summary>A W3C XML Schema xs:Duration type.</summary>
		// Token: 0x040009D8 RID: 2520
		Duration,
		/// <summary>A W3C XML Schema xs:dateTime type.</summary>
		// Token: 0x040009D9 RID: 2521
		DateTime,
		/// <summary>A W3C XML Schema xs:time type.</summary>
		// Token: 0x040009DA RID: 2522
		Time,
		/// <summary>A W3C XML Schema xs:date type.</summary>
		// Token: 0x040009DB RID: 2523
		Date,
		/// <summary>A W3C XML Schema xs:gYearMonth type.</summary>
		// Token: 0x040009DC RID: 2524
		GYearMonth,
		/// <summary>A W3C XML Schema xs:gYear type.</summary>
		// Token: 0x040009DD RID: 2525
		GYear,
		/// <summary>A W3C XML Schema xs:gMonthDay type.</summary>
		// Token: 0x040009DE RID: 2526
		GMonthDay,
		/// <summary>A W3C XML Schema xs:gDay type.</summary>
		// Token: 0x040009DF RID: 2527
		GDay,
		/// <summary>A W3C XML Schema xs:gMonth type.</summary>
		// Token: 0x040009E0 RID: 2528
		GMonth,
		/// <summary>A W3C XML Schema xs:hexBinary type.</summary>
		// Token: 0x040009E1 RID: 2529
		HexBinary,
		/// <summary>A W3C XML Schema xs:base64Binary type.</summary>
		// Token: 0x040009E2 RID: 2530
		Base64Binary,
		/// <summary>A W3C XML Schema xs:anyURI type.</summary>
		// Token: 0x040009E3 RID: 2531
		AnyUri,
		/// <summary>A W3C XML Schema xs:QName type.</summary>
		// Token: 0x040009E4 RID: 2532
		QName,
		/// <summary>A W3C XML Schema xs:NOTATION type.</summary>
		// Token: 0x040009E5 RID: 2533
		Notation,
		/// <summary>A W3C XML Schema xs:normalizedString type.</summary>
		// Token: 0x040009E6 RID: 2534
		NormalizedString,
		/// <summary>A W3C XML Schema xs:token type.</summary>
		// Token: 0x040009E7 RID: 2535
		Token,
		/// <summary>A W3C XML Schema xs:language type.</summary>
		// Token: 0x040009E8 RID: 2536
		Language,
		/// <summary>A W3C XML Schema xs:NMTOKEN type.</summary>
		// Token: 0x040009E9 RID: 2537
		NmToken,
		/// <summary>A W3C XML Schema xs:Name type.</summary>
		// Token: 0x040009EA RID: 2538
		Name,
		/// <summary>A W3C XML Schema xs:NCName type.</summary>
		// Token: 0x040009EB RID: 2539
		NCName,
		/// <summary>A W3C XML Schema xs:ID type.</summary>
		// Token: 0x040009EC RID: 2540
		Id,
		/// <summary>A W3C XML Schema xs:IDREF type.</summary>
		// Token: 0x040009ED RID: 2541
		Idref,
		/// <summary>A W3C XML Schema xs:ENTITY type.</summary>
		// Token: 0x040009EE RID: 2542
		Entity,
		/// <summary>A W3C XML Schema xs:integer type.</summary>
		// Token: 0x040009EF RID: 2543
		Integer,
		/// <summary>A W3C XML Schema xs:nonPositiveInteger type.</summary>
		// Token: 0x040009F0 RID: 2544
		NonPositiveInteger,
		/// <summary>A W3C XML Schema xs:negativeInteger type.</summary>
		// Token: 0x040009F1 RID: 2545
		NegativeInteger,
		/// <summary>A W3C XML Schema xs:long type.</summary>
		// Token: 0x040009F2 RID: 2546
		Long,
		/// <summary>A W3C XML Schema xs:int type.</summary>
		// Token: 0x040009F3 RID: 2547
		Int,
		/// <summary>A W3C XML Schema xs:short type.</summary>
		// Token: 0x040009F4 RID: 2548
		Short,
		/// <summary>A W3C XML Schema xs:byte type.</summary>
		// Token: 0x040009F5 RID: 2549
		Byte,
		/// <summary>A W3C XML Schema xs:nonNegativeInteger type.</summary>
		// Token: 0x040009F6 RID: 2550
		NonNegativeInteger,
		/// <summary>A W3C XML Schema xs:unsignedLong type.</summary>
		// Token: 0x040009F7 RID: 2551
		UnsignedLong,
		/// <summary>A W3C XML Schema xs:unsignedInt type.</summary>
		// Token: 0x040009F8 RID: 2552
		UnsignedInt,
		/// <summary>A W3C XML Schema xs:unsignedShort type.</summary>
		// Token: 0x040009F9 RID: 2553
		UnsignedShort,
		/// <summary>A W3C XML Schema xs:unsignedByte type.</summary>
		// Token: 0x040009FA RID: 2554
		UnsignedByte,
		/// <summary>A W3C XML Schema xs:positiveInteger type.</summary>
		// Token: 0x040009FB RID: 2555
		PositiveInteger,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x040009FC RID: 2556
		YearMonthDuration,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x040009FD RID: 2557
		DayTimeDuration
	}
}
