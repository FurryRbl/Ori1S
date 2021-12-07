using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002BF RID: 703
	internal class XmlTypeMapMemberElement : XmlTypeMapMember
	{
		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x0009BED4 File Offset: 0x0009A0D4
		// (set) Token: 0x06001DAD RID: 7597 RVA: 0x0009BEF4 File Offset: 0x0009A0F4
		public XmlTypeMapElementInfoList ElementInfo
		{
			get
			{
				if (this._elementInfo == null)
				{
					this._elementInfo = new XmlTypeMapElementInfoList();
				}
				return this._elementInfo;
			}
			set
			{
				this._elementInfo = value;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x0009BF00 File Offset: 0x0009A100
		// (set) Token: 0x06001DAF RID: 7599 RVA: 0x0009BF08 File Offset: 0x0009A108
		public string ChoiceMember
		{
			get
			{
				return this._choiceMember;
			}
			set
			{
				this._choiceMember = value;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x0009BF14 File Offset: 0x0009A114
		// (set) Token: 0x06001DB1 RID: 7601 RVA: 0x0009BF1C File Offset: 0x0009A11C
		public TypeData ChoiceTypeData
		{
			get
			{
				return this._choiceTypeData;
			}
			set
			{
				this._choiceTypeData = value;
			}
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x0009BF28 File Offset: 0x0009A128
		public XmlTypeMapElementInfo FindElement(object ob, object memberValue)
		{
			if (this._elementInfo.Count == 1)
			{
				return (XmlTypeMapElementInfo)this._elementInfo[0];
			}
			if (this._choiceMember != null)
			{
				object value = XmlTypeMapMember.GetValue(ob, this._choiceMember);
				foreach (object obj in this._elementInfo)
				{
					XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)obj;
					if (xmlTypeMapElementInfo.ChoiceValue != null && xmlTypeMapElementInfo.ChoiceValue.Equals(value))
					{
						return xmlTypeMapElementInfo;
					}
				}
			}
			else
			{
				if (memberValue == null)
				{
					return (XmlTypeMapElementInfo)this._elementInfo[0];
				}
				foreach (object obj2 in this._elementInfo)
				{
					XmlTypeMapElementInfo xmlTypeMapElementInfo2 = (XmlTypeMapElementInfo)obj2;
					if (xmlTypeMapElementInfo2.TypeData.Type.IsInstanceOfType(memberValue))
					{
						return xmlTypeMapElementInfo2;
					}
				}
			}
			return null;
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x0009C090 File Offset: 0x0009A290
		public void SetChoice(object ob, object choice)
		{
			XmlTypeMapMember.SetValue(ob, this._choiceMember, choice);
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x0009C0A0 File Offset: 0x0009A2A0
		// (set) Token: 0x06001DB5 RID: 7605 RVA: 0x0009C0A8 File Offset: 0x0009A2A8
		public bool IsXmlTextCollector
		{
			get
			{
				return this._isTextCollector;
			}
			set
			{
				this._isTextCollector = value;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x0009C0B4 File Offset: 0x0009A2B4
		public override bool RequiresNullable
		{
			get
			{
				foreach (object obj in this.ElementInfo)
				{
					XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)obj;
					if (xmlTypeMapElementInfo.IsNullable)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x04000BC2 RID: 3010
		private XmlTypeMapElementInfoList _elementInfo;

		// Token: 0x04000BC3 RID: 3011
		private string _choiceMember;

		// Token: 0x04000BC4 RID: 3012
		private bool _isTextCollector;

		// Token: 0x04000BC5 RID: 3013
		private TypeData _choiceTypeData;
	}
}
