using System;
using System.Collections;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	/// <summary>Represents the typed value of a validated XML element or attribute. The <see cref="T:System.Xml.Schema.XmlAtomicValue" /> class cannot be inherited.</summary>
	// Token: 0x020001F4 RID: 500
	[MonoTODO]
	public sealed class XmlAtomicValue : XPathItem, ICloneable
	{
		// Token: 0x06001391 RID: 5009 RVA: 0x00053534 File Offset: 0x00051734
		internal XmlAtomicValue(bool value, XmlSchemaType xmlType)
		{
			this.Init(value, xmlType);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00053544 File Offset: 0x00051744
		internal XmlAtomicValue(DateTime value, XmlSchemaType xmlType)
		{
			this.Init(value, xmlType);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x00053554 File Offset: 0x00051754
		internal XmlAtomicValue(decimal value, XmlSchemaType xmlType)
		{
			this.Init(value, xmlType);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x00053564 File Offset: 0x00051764
		internal XmlAtomicValue(double value, XmlSchemaType xmlType)
		{
			this.Init(value, xmlType);
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00053574 File Offset: 0x00051774
		internal XmlAtomicValue(int value, XmlSchemaType xmlType)
		{
			this.Init(value, xmlType);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00053584 File Offset: 0x00051784
		internal XmlAtomicValue(long value, XmlSchemaType xmlType)
		{
			this.Init(value, xmlType);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00053594 File Offset: 0x00051794
		internal XmlAtomicValue(float value, XmlSchemaType xmlType)
		{
			this.Init(value, xmlType);
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x000535A4 File Offset: 0x000517A4
		internal XmlAtomicValue(string value, XmlSchemaType xmlType)
		{
			this.Init(value, xmlType);
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x000535B4 File Offset: 0x000517B4
		internal XmlAtomicValue(object value, XmlSchemaType xmlType)
		{
			this.Init(value, xmlType);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlAtomicValue.Clone" />.</summary>
		// Token: 0x0600139A RID: 5018 RVA: 0x000535C4 File Offset: 0x000517C4
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x000535CC File Offset: 0x000517CC
		private void Init(bool value, XmlSchemaType xmlType)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlTypeCode = XmlTypeCode.Boolean;
			this.booleanValue = value;
			this.schemaType = xmlType;
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x000535F8 File Offset: 0x000517F8
		private void Init(DateTime value, XmlSchemaType xmlType)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlTypeCode = XmlTypeCode.DateTime;
			this.dateTimeValue = value;
			this.schemaType = xmlType;
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00053624 File Offset: 0x00051824
		private void Init(decimal value, XmlSchemaType xmlType)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlTypeCode = XmlTypeCode.Decimal;
			this.decimalValue = value;
			this.schemaType = xmlType;
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x00053650 File Offset: 0x00051850
		private void Init(double value, XmlSchemaType xmlType)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlTypeCode = XmlTypeCode.Double;
			this.doubleValue = value;
			this.schemaType = xmlType;
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0005367C File Offset: 0x0005187C
		private void Init(int value, XmlSchemaType xmlType)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlTypeCode = XmlTypeCode.Int;
			this.intValue = value;
			this.schemaType = xmlType;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x000536A8 File Offset: 0x000518A8
		private void Init(long value, XmlSchemaType xmlType)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlTypeCode = XmlTypeCode.Long;
			this.longValue = value;
			this.schemaType = xmlType;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x000536D4 File Offset: 0x000518D4
		private void Init(float value, XmlSchemaType xmlType)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlTypeCode = XmlTypeCode.Float;
			this.floatValue = value;
			this.schemaType = xmlType;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x00053700 File Offset: 0x00051900
		private void Init(string value, XmlSchemaType xmlType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlTypeCode = XmlTypeCode.String;
			this.stringValue = value;
			this.schemaType = xmlType;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x00053748 File Offset: 0x00051948
		private void Init(object value, XmlSchemaType xmlType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			switch (Type.GetTypeCode(value.GetType()))
			{
			case TypeCode.Boolean:
				this.Init((bool)value, xmlType);
				return;
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
				this.Init((int)value, xmlType);
				return;
			case TypeCode.UInt32:
			case TypeCode.Int64:
				this.Init((long)value, xmlType);
				return;
			case TypeCode.Single:
				this.Init((float)value, xmlType);
				return;
			case TypeCode.Double:
				this.Init((double)value, xmlType);
				return;
			case TypeCode.Decimal:
				this.Init((decimal)value, xmlType);
				return;
			case TypeCode.DateTime:
				this.Init((DateTime)value, xmlType);
				return;
			case TypeCode.String:
				this.Init((string)value, xmlType);
				return;
			}
			ICollection collection = value as ICollection;
			if (collection != null && collection.Count == 1)
			{
				if (collection is IList)
				{
					this.Init(((IList)collection)[0], xmlType);
				}
				else
				{
					IEnumerator enumerator = collection.GetEnumerator();
					if (!enumerator.MoveNext())
					{
						return;
					}
					if (enumerator.Current is DictionaryEntry)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
						this.Init(dictionaryEntry.Value, xmlType);
					}
					else
					{
						this.Init(enumerator.Current, xmlType);
					}
				}
				return;
			}
			XmlAtomicValue xmlAtomicValue = value as XmlAtomicValue;
			if (xmlAtomicValue != null)
			{
				XmlTypeCode xmlTypeCode = xmlAtomicValue.xmlTypeCode;
				switch (xmlTypeCode)
				{
				case XmlTypeCode.String:
					this.Init(xmlAtomicValue.stringValue, xmlType);
					return;
				case XmlTypeCode.Boolean:
					this.Init(xmlAtomicValue.booleanValue, xmlType);
					return;
				case XmlTypeCode.Decimal:
					this.Init(xmlAtomicValue.decimalValue, xmlType);
					return;
				case XmlTypeCode.Float:
					this.Init(xmlAtomicValue.floatValue, xmlType);
					return;
				case XmlTypeCode.Double:
					this.Init(xmlAtomicValue.doubleValue, xmlType);
					return;
				default:
					if (xmlTypeCode == XmlTypeCode.Long)
					{
						this.Init(xmlAtomicValue.longValue, xmlType);
						return;
					}
					if (xmlTypeCode == XmlTypeCode.Int)
					{
						this.Init(xmlAtomicValue.intValue, xmlType);
						return;
					}
					this.objectValue = xmlAtomicValue.objectValue;
					break;
				case XmlTypeCode.DateTime:
					this.Init(xmlAtomicValue.dateTimeValue, xmlType);
					return;
				}
			}
			this.objectValue = value;
			this.schemaType = xmlType;
		}

		/// <summary>Returns a copy of this <see cref="T:System.Xml.Schema.XmlAtomicValue" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlAtomicValue" /> object copy of this <see cref="T:System.Xml.Schema.XmlAtomicValue" /> object.</returns>
		// Token: 0x060013A4 RID: 5028 RVA: 0x000539B0 File Offset: 0x00051BB0
		public XmlAtomicValue Clone()
		{
			return new XmlAtomicValue(this, this.schemaType);
		}

		/// <summary>Returns the validated XML element or attribute's value as the type specified using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes.</summary>
		/// <returns>The value of the validated XML element or attribute as the type requested.</returns>
		/// <param name="type">The type to return the validated XML element or attribute's value as.</param>
		/// <param name="nsResolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes.</param>
		/// <exception cref="T:System.FormatException">The validated XML element or attribute's value is not in the correct format for the target type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x060013A5 RID: 5029 RVA: 0x000539C0 File Offset: 0x00051BC0
		public override object ValueAs(Type type, IXmlNamespaceResolver nsResolver)
		{
			XmlTypeCode xmlTypeCode = XmlAtomicValue.XmlTypeCodeFromRuntimeType(type, false);
			switch (xmlTypeCode)
			{
			case XmlTypeCode.Long:
			case XmlTypeCode.UnsignedInt:
				return this.ValueAsLong;
			case XmlTypeCode.Int:
			case XmlTypeCode.Short:
			case XmlTypeCode.UnsignedShort:
				return this.ValueAsInt;
			default:
				switch (xmlTypeCode)
				{
				case XmlTypeCode.String:
					return this.Value;
				case XmlTypeCode.Boolean:
					return this.ValueAsBoolean;
				default:
					if (xmlTypeCode == XmlTypeCode.Item)
					{
						return this.TypedValue;
					}
					if (xmlTypeCode != XmlTypeCode.QName)
					{
						throw new NotImplementedException();
					}
					return XmlQualifiedName.Parse(this.Value, nsResolver, true);
				case XmlTypeCode.Float:
				case XmlTypeCode.Double:
					return this.ValueAsDouble;
				case XmlTypeCode.DateTime:
					return this.ValueAsDateTime;
				}
				break;
			}
		}

		/// <summary>Gets the string value of the validated XML element or attribute.</summary>
		/// <returns>The string value of the validated XML element or attribute.</returns>
		// Token: 0x060013A6 RID: 5030 RVA: 0x00053A94 File Offset: 0x00051C94
		public override string ToString()
		{
			return this.Value;
		}

		/// <summary>Gets a value indicating whether the validated XML element or attribute is an XPath node or an atomic value.</summary>
		/// <returns>true if the validated XML element or attribute is an XPath node; false if the validated XML element or attribute is an atomic value.</returns>
		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x00053A9C File Offset: 0x00051C9C
		public override bool IsNode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x00053AA0 File Offset: 0x00051CA0
		internal XmlTypeCode ResolvedTypeCode
		{
			get
			{
				if (this.schemaType != XmlSchemaComplexType.AnyType)
				{
					return this.schemaType.TypeCode;
				}
				return this.xmlTypeCode;
			}
		}

		/// <summary>Gets the current validated XML element or attribute as a boxed object of the most appropriate Microsoft .NET Framework type according to its schema type.</summary>
		/// <returns>The current validated XML element or attribute as a boxed object of the most appropriate .NET Framework type.</returns>
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x00053AD0 File Offset: 0x00051CD0
		public override object TypedValue
		{
			get
			{
				XmlTypeCode resolvedTypeCode = this.ResolvedTypeCode;
				switch (resolvedTypeCode)
				{
				case XmlTypeCode.String:
					return this.Value;
				case XmlTypeCode.Boolean:
					return this.ValueAsBoolean;
				default:
					if (resolvedTypeCode == XmlTypeCode.Long)
					{
						return this.ValueAsLong;
					}
					if (resolvedTypeCode != XmlTypeCode.Int)
					{
						return this.objectValue;
					}
					return this.ValueAsInt;
				case XmlTypeCode.Float:
				case XmlTypeCode.Double:
					return this.ValueAsDouble;
				case XmlTypeCode.DateTime:
					return this.ValueAsDateTime;
				}
			}
		}

		/// <summary>Gets the string value of the validated XML element or attribute.</summary>
		/// <returns>The string value of the validated XML element or attribute.</returns>
		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x00053B68 File Offset: 0x00051D68
		public override string Value
		{
			get
			{
				XmlTypeCode resolvedTypeCode = this.ResolvedTypeCode;
				switch (resolvedTypeCode)
				{
				case XmlTypeCode.NonPositiveInteger:
				case XmlTypeCode.NegativeInteger:
				case XmlTypeCode.Long:
				case XmlTypeCode.NonNegativeInteger:
				case XmlTypeCode.UnsignedLong:
				case XmlTypeCode.PositiveInteger:
					this.stringValue = XQueryConvert.IntegerToString(this.ValueAsLong);
					break;
				case XmlTypeCode.Int:
				case XmlTypeCode.Short:
				case XmlTypeCode.Byte:
				case XmlTypeCode.UnsignedInt:
				case XmlTypeCode.UnsignedShort:
				case XmlTypeCode.UnsignedByte:
					this.stringValue = XQueryConvert.IntToString(this.ValueAsInt);
					break;
				default:
				{
					switch (resolvedTypeCode)
					{
					case XmlTypeCode.AnyAtomicType:
						break;
					default:
						if (resolvedTypeCode != XmlTypeCode.None && resolvedTypeCode != XmlTypeCode.Item)
						{
							goto IL_218;
						}
						break;
					case XmlTypeCode.String:
						return this.stringValue;
					case XmlTypeCode.Boolean:
						this.stringValue = XQueryConvert.BooleanToString(this.ValueAsBoolean);
						goto IL_218;
					case XmlTypeCode.Float:
					case XmlTypeCode.Double:
						this.stringValue = XQueryConvert.DoubleToString(this.ValueAsDouble);
						goto IL_218;
					case XmlTypeCode.DateTime:
						this.stringValue = XQueryConvert.DateTimeToString(this.ValueAsDateTime);
						goto IL_218;
					}
					XmlTypeCode xmlTypeCode = XmlAtomicValue.XmlTypeCodeFromRuntimeType(this.objectValue.GetType(), false);
					switch (xmlTypeCode)
					{
					case XmlTypeCode.String:
						this.stringValue = (string)this.objectValue;
						break;
					case XmlTypeCode.Boolean:
						this.stringValue = XQueryConvert.BooleanToString((bool)this.objectValue);
						break;
					case XmlTypeCode.Decimal:
						this.stringValue = XQueryConvert.DecimalToString((decimal)this.objectValue);
						break;
					case XmlTypeCode.Float:
						this.stringValue = XQueryConvert.FloatToString((float)this.objectValue);
						break;
					case XmlTypeCode.Double:
						this.stringValue = XQueryConvert.DoubleToString((double)this.objectValue);
						break;
					default:
						if (xmlTypeCode != XmlTypeCode.Long)
						{
							if (xmlTypeCode == XmlTypeCode.Int)
							{
								this.stringValue = XQueryConvert.IntToString((int)this.objectValue);
							}
						}
						else
						{
							this.stringValue = XQueryConvert.IntegerToString((long)this.objectValue);
						}
						break;
					case XmlTypeCode.DateTime:
						this.stringValue = XQueryConvert.DateTimeToString((DateTime)this.objectValue);
						break;
					}
					break;
				}
				}
				IL_218:
				if (this.stringValue != null)
				{
					return this.stringValue;
				}
				if (this.objectValue != null)
				{
					throw new InvalidCastException(string.Format("Conversion from runtime type {0} to {1} is not supported", this.objectValue.GetType(), XmlTypeCode.String));
				}
				throw new InvalidCastException(string.Format("Conversion from schema type {0} (type code {1}, resolved type code {2}) to {3} is not supported.", new object[]
				{
					this.schemaType.QualifiedName,
					this.xmlTypeCode,
					this.ResolvedTypeCode,
					XmlTypeCode.String
				}));
			}
		}

		/// <summary>Gets the validated XML element or attribute's value as a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The validated XML element or attribute's value as a <see cref="T:System.Boolean" />.</returns>
		/// <exception cref="T:System.FormatException">The validated XML element or attribute's value is not in the correct format for the <see cref="T:System.Boolean" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Boolean" /> is not valid.</exception>
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x00053E18 File Offset: 0x00052018
		public override bool ValueAsBoolean
		{
			get
			{
				XmlTypeCode xmlTypeCode = this.xmlTypeCode;
				switch (xmlTypeCode)
				{
				case XmlTypeCode.AnyAtomicType:
					break;
				default:
					if (xmlTypeCode != XmlTypeCode.None && xmlTypeCode != XmlTypeCode.Item)
					{
						if (xmlTypeCode == XmlTypeCode.Long)
						{
							return XQueryConvert.IntegerToBoolean(this.longValue);
						}
						if (xmlTypeCode != XmlTypeCode.Int)
						{
							goto IL_BE;
						}
						return XQueryConvert.IntToBoolean(this.intValue);
					}
					break;
				case XmlTypeCode.String:
					return XQueryConvert.StringToBoolean(this.stringValue);
				case XmlTypeCode.Boolean:
					return this.booleanValue;
				case XmlTypeCode.Decimal:
					return XQueryConvert.DecimalToBoolean(this.decimalValue);
				case XmlTypeCode.Float:
					return XQueryConvert.FloatToBoolean(this.floatValue);
				case XmlTypeCode.Double:
					return XQueryConvert.DoubleToBoolean(this.doubleValue);
				}
				if (this.objectValue is bool)
				{
					return (bool)this.objectValue;
				}
				IL_BE:
				throw new InvalidCastException(string.Format("Conversion from {0} to {1} is not supported", this.schemaType.QualifiedName, XmlSchemaSimpleType.XsBoolean.QualifiedName));
			}
		}

		/// <summary>Gets the validated XML element or attribute's value as a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The validated XML element or attribute's value as a <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.FormatException">The validated XML element or attribute's value is not in the correct format for the <see cref="T:System.DateTime" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.DateTime" /> is not valid.</exception>
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x00053F08 File Offset: 0x00052108
		public override DateTime ValueAsDateTime
		{
			get
			{
				XmlTypeCode xmlTypeCode = this.xmlTypeCode;
				switch (xmlTypeCode)
				{
				case XmlTypeCode.AnyAtomicType:
					break;
				default:
					if (xmlTypeCode != XmlTypeCode.None && xmlTypeCode != XmlTypeCode.Item)
					{
						if (xmlTypeCode != XmlTypeCode.DateTime)
						{
							goto IL_6A;
						}
						return this.dateTimeValue;
					}
					break;
				case XmlTypeCode.String:
					return XQueryConvert.StringToDateTime(this.stringValue);
				}
				if (this.objectValue is DateTime)
				{
					return (DateTime)this.objectValue;
				}
				IL_6A:
				throw new InvalidCastException(string.Format("Conversion from {0} to {1} is not supported", this.schemaType.QualifiedName, XmlSchemaSimpleType.XsDateTime.QualifiedName));
			}
		}

		/// <summary>Gets the validated XML element or attribute's value as a <see cref="T:System.Double" />.</summary>
		/// <returns>The validated XML element or attribute's value as a <see cref="T:System.Double" />.</returns>
		/// <exception cref="T:System.FormatException">The validated XML element or attribute's value is not in the correct format for the <see cref="T:System.Double" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Double" /> is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x00053FA4 File Offset: 0x000521A4
		public override double ValueAsDouble
		{
			get
			{
				XmlTypeCode xmlTypeCode = this.xmlTypeCode;
				switch (xmlTypeCode)
				{
				case XmlTypeCode.AnyAtomicType:
					break;
				default:
					if (xmlTypeCode != XmlTypeCode.None && xmlTypeCode != XmlTypeCode.Item)
					{
						if (xmlTypeCode == XmlTypeCode.Long)
						{
							return XQueryConvert.IntegerToDouble(this.longValue);
						}
						if (xmlTypeCode != XmlTypeCode.Int)
						{
							goto IL_BE;
						}
						return XQueryConvert.IntToDouble(this.intValue);
					}
					break;
				case XmlTypeCode.String:
					return XQueryConvert.StringToDouble(this.stringValue);
				case XmlTypeCode.Boolean:
					return XQueryConvert.BooleanToDouble(this.booleanValue);
				case XmlTypeCode.Decimal:
					return XQueryConvert.DecimalToDouble(this.decimalValue);
				case XmlTypeCode.Float:
					return XQueryConvert.FloatToDouble(this.floatValue);
				case XmlTypeCode.Double:
					return this.doubleValue;
				}
				if (this.objectValue is double)
				{
					return (double)this.objectValue;
				}
				IL_BE:
				throw new InvalidCastException(string.Format("Conversion from {0} to {1} is not supported", this.schemaType.QualifiedName, XmlSchemaSimpleType.XsDouble.QualifiedName));
			}
		}

		/// <summary>Gets the validated XML element or attribute's value as an <see cref="T:System.Int32" />.</summary>
		/// <returns>The validated XML element or attribute's value as an <see cref="T:System.Int32" />.</returns>
		/// <exception cref="T:System.FormatException">The validated XML element or attribute's value is not in the correct format for the <see cref="T:System.Int32" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int32" /> is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x00054094 File Offset: 0x00052294
		public override int ValueAsInt
		{
			get
			{
				XmlTypeCode xmlTypeCode = this.xmlTypeCode;
				switch (xmlTypeCode)
				{
				case XmlTypeCode.AnyAtomicType:
					break;
				default:
					if (xmlTypeCode != XmlTypeCode.None && xmlTypeCode != XmlTypeCode.Item)
					{
						if (xmlTypeCode == XmlTypeCode.Long)
						{
							return XQueryConvert.IntegerToInt(this.longValue);
						}
						if (xmlTypeCode != XmlTypeCode.Int)
						{
							goto IL_BE;
						}
						return this.intValue;
					}
					break;
				case XmlTypeCode.String:
					return XQueryConvert.StringToInt(this.stringValue);
				case XmlTypeCode.Boolean:
					return XQueryConvert.BooleanToInt(this.booleanValue);
				case XmlTypeCode.Decimal:
					return XQueryConvert.DecimalToInt(this.decimalValue);
				case XmlTypeCode.Float:
					return XQueryConvert.FloatToInt(this.floatValue);
				case XmlTypeCode.Double:
					return XQueryConvert.DoubleToInt(this.doubleValue);
				}
				if (this.objectValue is int)
				{
					return (int)this.objectValue;
				}
				IL_BE:
				throw new InvalidCastException(string.Format("Conversion from {0} to {1} is not supported", this.schemaType.QualifiedName, XmlSchemaSimpleType.XsInt.QualifiedName));
			}
		}

		/// <summary>Gets the validated XML element or attribute's value as an <see cref="T:System.Int64" />.</summary>
		/// <returns>The validated XML element or attribute's value as an <see cref="T:System.Int64" />.</returns>
		/// <exception cref="T:System.FormatException">The validated XML element or attribute's value is not in the correct format for the <see cref="T:System.Int64" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast to <see cref="T:System.Int64" /> is not valid.</exception>
		/// <exception cref="T:System.OverflowException">The attempted cast resulted in an overflow.</exception>
		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x00054184 File Offset: 0x00052384
		public override long ValueAsLong
		{
			get
			{
				XmlTypeCode xmlTypeCode = this.xmlTypeCode;
				switch (xmlTypeCode)
				{
				case XmlTypeCode.AnyAtomicType:
					break;
				default:
					if (xmlTypeCode != XmlTypeCode.None && xmlTypeCode != XmlTypeCode.Item)
					{
						if (xmlTypeCode == XmlTypeCode.Long)
						{
							return this.longValue;
						}
						if (xmlTypeCode != XmlTypeCode.Int)
						{
							goto IL_C0;
						}
						return (long)XQueryConvert.IntegerToInt((long)this.intValue);
					}
					break;
				case XmlTypeCode.String:
					return XQueryConvert.StringToInteger(this.stringValue);
				case XmlTypeCode.Boolean:
					return XQueryConvert.BooleanToInteger(this.booleanValue);
				case XmlTypeCode.Decimal:
					return XQueryConvert.DecimalToInteger(this.decimalValue);
				case XmlTypeCode.Float:
					return XQueryConvert.FloatToInteger(this.floatValue);
				case XmlTypeCode.Double:
					return XQueryConvert.DoubleToInteger(this.doubleValue);
				}
				if (this.objectValue is long)
				{
					return (long)this.objectValue;
				}
				IL_C0:
				throw new InvalidCastException(string.Format("Conversion from {0} to {1} is not supported", this.schemaType.QualifiedName, XmlSchemaSimpleType.XsLong.QualifiedName));
			}
		}

		/// <summary>Gets the Microsoft .NET Framework type of the validated XML element or attribute.</summary>
		/// <returns>The .NET Framework type of the validated XML element or attribute. The default value is <see cref="T:System.String" />.</returns>
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x00054278 File Offset: 0x00052478
		public override Type ValueType
		{
			get
			{
				return this.schemaType.Datatype.ValueType;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaType" /> for the validated XML element or attribute.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaType" /> for the validated XML element or attribute.</returns>
		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x0005428C File Offset: 0x0005248C
		public override XmlSchemaType XmlType
		{
			get
			{
				return this.schemaType;
			}
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00054294 File Offset: 0x00052494
		internal static Type RuntimeTypeFromXmlTypeCode(XmlTypeCode typeCode)
		{
			switch (typeCode)
			{
			case XmlTypeCode.Long:
				return typeof(long);
			case XmlTypeCode.Int:
				return typeof(int);
			case XmlTypeCode.Short:
				return typeof(short);
			default:
				switch (typeCode)
				{
				case XmlTypeCode.String:
					return typeof(string);
				case XmlTypeCode.Boolean:
					return typeof(bool);
				case XmlTypeCode.Decimal:
					return typeof(decimal);
				case XmlTypeCode.Float:
					return typeof(float);
				case XmlTypeCode.Double:
					return typeof(double);
				default:
					if (typeCode != XmlTypeCode.Item)
					{
						throw new NotSupportedException(string.Format("XQuery internal error: Cannot infer Runtime Type from XmlTypeCode {0}.", typeCode));
					}
					return typeof(object);
				case XmlTypeCode.DateTime:
					return typeof(DateTime);
				}
				break;
			case XmlTypeCode.UnsignedInt:
				return typeof(uint);
			case XmlTypeCode.UnsignedShort:
				return typeof(ushort);
			}
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x00054398 File Offset: 0x00052598
		internal static XmlTypeCode XmlTypeCodeFromRuntimeType(Type cliType, bool raiseError)
		{
			switch (Type.GetTypeCode(cliType))
			{
			case TypeCode.Object:
				return XmlTypeCode.Item;
			case TypeCode.Boolean:
				return XmlTypeCode.Boolean;
			case TypeCode.Int16:
				return XmlTypeCode.Short;
			case TypeCode.UInt16:
				return XmlTypeCode.UnsignedShort;
			case TypeCode.Int32:
				return XmlTypeCode.Int;
			case TypeCode.UInt32:
				return XmlTypeCode.UnsignedInt;
			case TypeCode.Int64:
				return XmlTypeCode.Long;
			case TypeCode.Single:
				return XmlTypeCode.Float;
			case TypeCode.Double:
				return XmlTypeCode.Double;
			case TypeCode.Decimal:
				return XmlTypeCode.Decimal;
			case TypeCode.DateTime:
				return XmlTypeCode.DateTime;
			case TypeCode.String:
				return XmlTypeCode.String;
			}
			if (raiseError)
			{
				throw new NotSupportedException(string.Format("XQuery internal error: Cannot infer XmlTypeCode from Runtime Type {0}", cliType));
			}
			return XmlTypeCode.None;
		}

		// Token: 0x04000781 RID: 1921
		private bool booleanValue;

		// Token: 0x04000782 RID: 1922
		private DateTime dateTimeValue;

		// Token: 0x04000783 RID: 1923
		private decimal decimalValue;

		// Token: 0x04000784 RID: 1924
		private double doubleValue;

		// Token: 0x04000785 RID: 1925
		private int intValue;

		// Token: 0x04000786 RID: 1926
		private long longValue;

		// Token: 0x04000787 RID: 1927
		private object objectValue;

		// Token: 0x04000788 RID: 1928
		private float floatValue;

		// Token: 0x04000789 RID: 1929
		private string stringValue;

		// Token: 0x0400078A RID: 1930
		private XmlSchemaType schemaType;

		// Token: 0x0400078B RID: 1931
		private XmlTypeCode xmlTypeCode;
	}
}
