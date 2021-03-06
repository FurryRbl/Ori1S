using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>An abstract class for <see cref="T:System.Xml.Schema.XmlSchemaAll" />, <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" />.</summary>
	// Token: 0x02000219 RID: 537
	public abstract class XmlSchemaGroupBase : XmlSchemaParticle
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaGroupBase" /> class.</summary>
		// Token: 0x0600156E RID: 5486 RVA: 0x00061040 File Offset: 0x0005F240
		protected XmlSchemaGroupBase()
		{
			this.compiledItems = new XmlSchemaObjectCollection();
		}

		/// <summary>This collection is used to add new elements to the compositor.</summary>
		/// <returns>An XmlSchemaObjectCollection.</returns>
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x0600156F RID: 5487
		[XmlIgnore]
		public abstract XmlSchemaObjectCollection Items { get; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x00061054 File Offset: 0x0005F254
		internal XmlSchemaObjectCollection CompiledItems
		{
			get
			{
				return this.compiledItems;
			}
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0006105C File Offset: 0x0005F25C
		internal void CopyOptimizedItems(XmlSchemaGroupBase gb)
		{
			for (int i = 0; i < this.Items.Count; i++)
			{
				XmlSchemaParticle xmlSchemaParticle = this.Items[i] as XmlSchemaParticle;
				xmlSchemaParticle = xmlSchemaParticle.GetOptimizedParticle(false);
				if (xmlSchemaParticle != XmlSchemaParticle.Empty)
				{
					gb.Items.Add(xmlSchemaParticle);
					gb.CompiledItems.Add(xmlSchemaParticle);
				}
			}
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x000610CC File Offset: 0x0005F2CC
		internal override bool ParticleEquals(XmlSchemaParticle other)
		{
			XmlSchemaGroupBase xmlSchemaGroupBase = other as XmlSchemaGroupBase;
			if (xmlSchemaGroupBase == null)
			{
				return false;
			}
			if (base.GetType() != xmlSchemaGroupBase.GetType())
			{
				return false;
			}
			if (base.ValidatedMaxOccurs != xmlSchemaGroupBase.ValidatedMaxOccurs || base.ValidatedMinOccurs != xmlSchemaGroupBase.ValidatedMinOccurs)
			{
				return false;
			}
			if (this.CompiledItems.Count != xmlSchemaGroupBase.CompiledItems.Count)
			{
				return false;
			}
			for (int i = 0; i < this.CompiledItems.Count; i++)
			{
				XmlSchemaParticle xmlSchemaParticle = this.CompiledItems[i] as XmlSchemaParticle;
				XmlSchemaParticle other2 = xmlSchemaGroupBase.CompiledItems[i] as XmlSchemaParticle;
				if (!xmlSchemaParticle.ParticleEquals(other2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00061198 File Offset: 0x0005F398
		internal override void CheckRecursion(int depth, ValidationEventHandler h, XmlSchema schema)
		{
			foreach (XmlSchemaObject xmlSchemaObject in this.Items)
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)xmlSchemaObject;
				xmlSchemaParticle.CheckRecursion(depth, h, schema);
			}
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0006120C File Offset: 0x0005F40C
		internal bool ValidateNSRecurseCheckCardinality(XmlSchemaAny any, ValidationEventHandler h, XmlSchema schema, bool raiseError)
		{
			foreach (XmlSchemaObject xmlSchemaObject in this.Items)
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)xmlSchemaObject;
				if (!xmlSchemaParticle.ValidateDerivationByRestriction(any, h, schema, raiseError))
				{
					return false;
				}
			}
			return this.ValidateOccurenceRangeOK(any, h, schema, raiseError);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x00061298 File Offset: 0x0005F498
		internal bool ValidateRecurse(XmlSchemaGroupBase baseGroup, ValidationEventHandler h, XmlSchema schema, bool raiseError)
		{
			return this.ValidateSeqRecurseMapSumCommon(baseGroup, h, schema, false, false, raiseError);
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x000612A8 File Offset: 0x0005F4A8
		internal bool ValidateSeqRecurseMapSumCommon(XmlSchemaGroupBase baseGroup, ValidationEventHandler h, XmlSchema schema, bool isLax, bool isMapAndSum, bool raiseError)
		{
			int num = 0;
			int num2 = 0;
			decimal num3 = 0m;
			if (baseGroup.CompiledItems.Count == 0 && this.CompiledItems.Count > 0)
			{
				if (raiseError)
				{
					base.error(h, "Invalid particle derivation by restriction was found. base particle does not contain particles.");
				}
				return false;
			}
			for (int i = 0; i < this.CompiledItems.Count; i++)
			{
				XmlSchemaParticle xmlSchemaParticle = null;
				while (this.CompiledItems.Count > num)
				{
					xmlSchemaParticle = (XmlSchemaParticle)this.CompiledItems[num];
					if (xmlSchemaParticle != XmlSchemaParticle.Empty)
					{
						break;
					}
					num++;
				}
				if (num >= this.CompiledItems.Count)
				{
					if (raiseError)
					{
						base.error(h, "Invalid particle derivation by restriction was found. Cannot be mapped to base particle.");
					}
					return false;
				}
				while (baseGroup.CompiledItems.Count > num2)
				{
					XmlSchemaParticle xmlSchemaParticle2 = (XmlSchemaParticle)baseGroup.CompiledItems[num2];
					if (xmlSchemaParticle2 != XmlSchemaParticle.Empty || !(xmlSchemaParticle2.ValidatedMaxOccurs > 0m))
					{
						if (xmlSchemaParticle.ValidateDerivationByRestriction(xmlSchemaParticle2, h, schema, false))
						{
							num3 += xmlSchemaParticle2.ValidatedMinOccurs;
							if (num3 >= baseGroup.ValidatedMaxOccurs)
							{
								num3 = 0m;
								num2++;
							}
							num++;
							break;
						}
						if (!isLax && !isMapAndSum && xmlSchemaParticle2.MinOccurs > num3 && !xmlSchemaParticle2.ValidateIsEmptiable())
						{
							if (raiseError)
							{
								base.error(h, "Invalid particle derivation by restriction was found. Invalid sub-particle derivation was found.");
							}
							return false;
						}
						num3 = 0m;
						num2++;
					}
				}
			}
			if (this.CompiledItems.Count > 0 && num != this.CompiledItems.Count)
			{
				if (raiseError)
				{
					base.error(h, "Invalid particle derivation by restriction was found. Extraneous derived particle was found.");
				}
				return false;
			}
			if (!isLax && !isMapAndSum)
			{
				if (num3 > 0m)
				{
					num2++;
				}
				for (int j = num2; j < baseGroup.CompiledItems.Count; j++)
				{
					XmlSchemaParticle xmlSchemaParticle3 = baseGroup.CompiledItems[j] as XmlSchemaParticle;
					if (!xmlSchemaParticle3.ValidateIsEmptiable())
					{
						if (raiseError)
						{
							base.error(h, "Invalid particle derivation by restriction was found. There is a base particle which does not have mapped derived particle and is not emptiable.");
						}
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04000891 RID: 2193
		private XmlSchemaObjectCollection compiledItems;
	}
}
