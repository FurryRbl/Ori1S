using System;
using System.Collections;
using System.Globalization;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Abstract class for that is the base class for all particle types (e.g. <see cref="T:System.Xml.Schema.XmlSchemaAny" />).</summary>
	// Token: 0x02000233 RID: 563
	public abstract class XmlSchemaParticle : XmlSchemaAnnotated
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaParticle" /> class.</summary>
		// Token: 0x0600164D RID: 5709 RVA: 0x0006626C File Offset: 0x0006446C
		protected XmlSchemaParticle()
		{
			this.minOccurs = 1m;
			this.maxOccurs = 1m;
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x000662C4 File Offset: 0x000644C4
		internal static XmlSchemaParticle Empty
		{
			get
			{
				if (XmlSchemaParticle.empty == null)
				{
					XmlSchemaParticle.empty = new XmlSchemaParticle.EmptyParticle();
				}
				return XmlSchemaParticle.empty;
			}
		}

		/// <summary>Gets or sets the number as a string value. The minimum number of times the particle can occur.</summary>
		/// <returns>The number as a string value. String.Empty indicates that MinOccurs is equal to the default value. The default is a null reference.</returns>
		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x000662E0 File Offset: 0x000644E0
		// (set) Token: 0x06001650 RID: 5712 RVA: 0x000662E8 File Offset: 0x000644E8
		[XmlAttribute("minOccurs")]
		public string MinOccursString
		{
			get
			{
				return this.minstr;
			}
			set
			{
				if (value == null)
				{
					this.minOccurs = 1m;
					this.minstr = value;
					return;
				}
				decimal num = decimal.Parse(value, CultureInfo.InvariantCulture);
				if (num >= 0m && num == decimal.Truncate(num))
				{
					this.minOccurs = num;
					this.minstr = num.ToString(CultureInfo.InvariantCulture);
					return;
				}
				throw new XmlSchemaException("MinOccursString must be a non-negative number", null);
			}
		}

		/// <summary>Gets or sets the number as a string value. Maximum number of times the particle can occur.</summary>
		/// <returns>The number as a string value. String.Empty indicates that MaxOccurs is equal to the default value. The default is a null reference.</returns>
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x00066368 File Offset: 0x00064568
		// (set) Token: 0x06001652 RID: 5714 RVA: 0x00066370 File Offset: 0x00064570
		[XmlAttribute("maxOccurs")]
		public string MaxOccursString
		{
			get
			{
				return this.maxstr;
			}
			set
			{
				if (value == "unbounded")
				{
					this.maxstr = value;
					this.maxOccurs = decimal.MaxValue;
				}
				else
				{
					decimal num = decimal.Parse(value, CultureInfo.InvariantCulture);
					if (!(num >= 0m) || !(num == decimal.Truncate(num)))
					{
						throw new XmlSchemaException("MaxOccurs must be a non-negative integer", null);
					}
					this.maxOccurs = num;
					this.maxstr = num.ToString(CultureInfo.InvariantCulture);
					if (num == 0m && this.minstr == null)
					{
						this.minOccurs = 0m;
					}
				}
			}
		}

		/// <summary>Gets or sets the minimum number of times the particle can occur.</summary>
		/// <returns>The minimum number of times the particle can occur. The default is 1.</returns>
		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x0006642C File Offset: 0x0006462C
		// (set) Token: 0x06001654 RID: 5716 RVA: 0x00066434 File Offset: 0x00064634
		[XmlIgnore]
		public decimal MinOccurs
		{
			get
			{
				return this.minOccurs;
			}
			set
			{
				this.MinOccursString = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		/// <summary>Gets or sets the maximum number of times the particle can occur.</summary>
		/// <returns>The maximum number of times the particle can occur. The default is 1.</returns>
		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x00066448 File Offset: 0x00064648
		// (set) Token: 0x06001656 RID: 5718 RVA: 0x00066450 File Offset: 0x00064650
		[XmlIgnore]
		public decimal MaxOccurs
		{
			get
			{
				return this.maxOccurs;
			}
			set
			{
				if (value == 79228162514264337593543950335m)
				{
					this.MaxOccursString = "unbounded";
				}
				else
				{
					this.MaxOccursString = value.ToString(CultureInfo.InvariantCulture);
				}
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x00066494 File Offset: 0x00064694
		internal decimal ValidatedMinOccurs
		{
			get
			{
				return this.validatedMinOccurs;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x0006649C File Offset: 0x0006469C
		internal decimal ValidatedMaxOccurs
		{
			get
			{
				return this.validatedMaxOccurs;
			}
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x000664A4 File Offset: 0x000646A4
		internal virtual XmlSchemaParticle GetOptimizedParticle(bool isTop)
		{
			return null;
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x000664A8 File Offset: 0x000646A8
		internal XmlSchemaParticle GetShallowClone()
		{
			return (XmlSchemaParticle)base.MemberwiseClone();
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x000664B8 File Offset: 0x000646B8
		internal void CompileOccurence(ValidationEventHandler h, XmlSchema schema)
		{
			if (this.MinOccurs > this.MaxOccurs && (!(this.MaxOccurs == 0m) || this.MinOccursString != null))
			{
				base.error(h, "minOccurs must be less than or equal to maxOccurs");
			}
			else
			{
				if (this.MaxOccursString == "unbounded")
				{
					this.validatedMaxOccurs = decimal.MaxValue;
				}
				else
				{
					this.validatedMaxOccurs = this.maxOccurs;
				}
				if (this.validatedMaxOccurs == 0m)
				{
					this.validatedMinOccurs = 0m;
				}
				else
				{
					this.validatedMinOccurs = this.minOccurs;
				}
			}
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x00066578 File Offset: 0x00064778
		internal override void CopyInfo(XmlSchemaParticle obj)
		{
			base.CopyInfo(obj);
			if (this.MaxOccursString == "unbounded")
			{
				obj.maxOccurs = (obj.validatedMaxOccurs = decimal.MaxValue);
			}
			else
			{
				obj.maxOccurs = (obj.validatedMaxOccurs = this.ValidatedMaxOccurs);
			}
			if (this.MaxOccurs == 0m)
			{
				obj.minOccurs = (obj.validatedMinOccurs = 0m);
			}
			else
			{
				obj.minOccurs = (obj.validatedMinOccurs = this.ValidatedMinOccurs);
			}
			if (this.MinOccursString != null)
			{
				obj.MinOccursString = this.MinOccursString;
			}
			if (this.MaxOccursString != null)
			{
				obj.MaxOccursString = this.MaxOccursString;
			}
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x00066648 File Offset: 0x00064848
		internal virtual bool ValidateOccurenceRangeOK(XmlSchemaParticle other, ValidationEventHandler h, XmlSchema schema, bool raiseError)
		{
			if (this.ValidatedMinOccurs < other.ValidatedMinOccurs || (other.ValidatedMaxOccurs != 79228162514264337593543950335m && this.ValidatedMaxOccurs > other.ValidatedMaxOccurs))
			{
				if (raiseError)
				{
					base.error(h, "Invalid derivation occurence range was found.");
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x000666B4 File Offset: 0x000648B4
		internal virtual decimal GetMinEffectiveTotalRange()
		{
			return this.ValidatedMinOccurs;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x000666BC File Offset: 0x000648BC
		internal decimal GetMinEffectiveTotalRangeAllAndSequence()
		{
			if (this.minEffectiveTotalRange >= 0m)
			{
				return this.minEffectiveTotalRange;
			}
			decimal num = 0m;
			XmlSchemaObjectCollection items;
			if (this is XmlSchemaAll)
			{
				items = ((XmlSchemaAll)this).Items;
			}
			else
			{
				items = ((XmlSchemaSequence)this).Items;
			}
			foreach (XmlSchemaObject xmlSchemaObject in items)
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)xmlSchemaObject;
				num += xmlSchemaParticle.GetMinEffectiveTotalRange();
			}
			this.minEffectiveTotalRange = num;
			return num;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00066788 File Offset: 0x00064988
		internal virtual bool ValidateIsEmptiable()
		{
			return this.validatedMinOccurs == 0m || this.GetMinEffectiveTotalRange() == 0m;
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x000667C0 File Offset: 0x000649C0
		internal virtual bool ValidateDerivationByRestriction(XmlSchemaParticle baseParticle, ValidationEventHandler h, XmlSchema schema, bool raiseError)
		{
			return false;
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x000667C4 File Offset: 0x000649C4
		internal virtual void ValidateUniqueParticleAttribution(XmlSchemaObjectTable qnames, ArrayList nsNames, ValidationEventHandler h, XmlSchema schema)
		{
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x000667C8 File Offset: 0x000649C8
		internal virtual void ValidateUniqueTypeAttribution(XmlSchemaObjectTable labels, ValidationEventHandler h, XmlSchema schema)
		{
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x000667CC File Offset: 0x000649CC
		internal virtual void CheckRecursion(int depth, ValidationEventHandler h, XmlSchema schema)
		{
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x000667D0 File Offset: 0x000649D0
		internal virtual bool ParticleEquals(XmlSchemaParticle other)
		{
			return false;
		}

		// Token: 0x040008EE RID: 2286
		private decimal minOccurs;

		// Token: 0x040008EF RID: 2287
		private decimal maxOccurs;

		// Token: 0x040008F0 RID: 2288
		private string minstr;

		// Token: 0x040008F1 RID: 2289
		private string maxstr;

		// Token: 0x040008F2 RID: 2290
		private static XmlSchemaParticle empty;

		// Token: 0x040008F3 RID: 2291
		private decimal validatedMinOccurs = 1m;

		// Token: 0x040008F4 RID: 2292
		private decimal validatedMaxOccurs = 1m;

		// Token: 0x040008F5 RID: 2293
		internal int recursionDepth = -1;

		// Token: 0x040008F6 RID: 2294
		private decimal minEffectiveTotalRange = -1m;

		// Token: 0x040008F7 RID: 2295
		internal bool parentIsGroupDefinition;

		// Token: 0x040008F8 RID: 2296
		internal XmlSchemaParticle OptimizedParticle;

		// Token: 0x02000234 RID: 564
		internal class EmptyParticle : XmlSchemaParticle
		{
			// Token: 0x06001666 RID: 5734 RVA: 0x000667D4 File Offset: 0x000649D4
			internal EmptyParticle()
			{
			}

			// Token: 0x06001667 RID: 5735 RVA: 0x000667DC File Offset: 0x000649DC
			internal override XmlSchemaParticle GetOptimizedParticle(bool isTop)
			{
				return this;
			}

			// Token: 0x06001668 RID: 5736 RVA: 0x000667E0 File Offset: 0x000649E0
			internal override bool ParticleEquals(XmlSchemaParticle other)
			{
				return other == this || other == XmlSchemaParticle.Empty;
			}

			// Token: 0x06001669 RID: 5737 RVA: 0x000667F4 File Offset: 0x000649F4
			internal override bool ValidateDerivationByRestriction(XmlSchemaParticle baseParticle, ValidationEventHandler h, XmlSchema schema, bool raiseError)
			{
				return true;
			}

			// Token: 0x0600166A RID: 5738 RVA: 0x000667F8 File Offset: 0x000649F8
			internal override void CheckRecursion(int depth, ValidationEventHandler h, XmlSchema schema)
			{
			}

			// Token: 0x0600166B RID: 5739 RVA: 0x000667FC File Offset: 0x000649FC
			internal override void ValidateUniqueParticleAttribution(XmlSchemaObjectTable qnames, ArrayList nsNames, ValidationEventHandler h, XmlSchema schema)
			{
			}

			// Token: 0x0600166C RID: 5740 RVA: 0x00066800 File Offset: 0x00064A00
			internal override void ValidateUniqueTypeAttribution(XmlSchemaObjectTable labels, ValidationEventHandler h, XmlSchema schema)
			{
			}
		}
	}
}
