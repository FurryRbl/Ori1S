using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x020002BD RID: 701
	internal class XmlTypeMapMember
	{
		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x0009BA24 File Offset: 0x00099C24
		// (set) Token: 0x06001D86 RID: 7558 RVA: 0x0009BA2C File Offset: 0x00099C2C
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001D87 RID: 7559 RVA: 0x0009BA38 File Offset: 0x00099C38
		// (set) Token: 0x06001D88 RID: 7560 RVA: 0x0009BA40 File Offset: 0x00099C40
		public object DefaultValue
		{
			get
			{
				return this._defaultValue;
			}
			set
			{
				this._defaultValue = value;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001D8A RID: 7562 RVA: 0x0009BA58 File Offset: 0x00099C58
		// (set) Token: 0x06001D89 RID: 7561 RVA: 0x0009BA4C File Offset: 0x00099C4C
		public string Documentation
		{
			get
			{
				return this.documentation;
			}
			set
			{
				this.documentation = value;
			}
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x0009BA60 File Offset: 0x00099C60
		public bool IsReadOnly(Type type)
		{
			if (this._member == null)
			{
				this.InitMember(type);
			}
			return this._member is PropertyInfo && !((PropertyInfo)this._member).CanWrite;
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x0009BAA8 File Offset: 0x00099CA8
		public static object GetValue(object ob, string name)
		{
			MemberInfo[] member = ob.GetType().GetMember(name, BindingFlags.Instance | BindingFlags.Public);
			if (member[0] is PropertyInfo)
			{
				return ((PropertyInfo)member[0]).GetValue(ob, null);
			}
			return ((FieldInfo)member[0]).GetValue(ob);
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x0009BAF0 File Offset: 0x00099CF0
		public object GetValue(object ob)
		{
			if (this._member == null)
			{
				this.InitMember(ob.GetType());
			}
			if (this._member is PropertyInfo)
			{
				return ((PropertyInfo)this._member).GetValue(ob, null);
			}
			return ((FieldInfo)this._member).GetValue(ob);
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x0009BB48 File Offset: 0x00099D48
		public void SetValue(object ob, object value)
		{
			if (this._member == null)
			{
				this.InitMember(ob.GetType());
			}
			if (this._member is PropertyInfo)
			{
				((PropertyInfo)this._member).SetValue(ob, value, null);
			}
			else
			{
				((FieldInfo)this._member).SetValue(ob, value);
			}
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x0009BBA8 File Offset: 0x00099DA8
		public static void SetValue(object ob, string name, object value)
		{
			MemberInfo[] member = ob.GetType().GetMember(name, BindingFlags.Instance | BindingFlags.Public);
			if (member[0] is PropertyInfo)
			{
				((PropertyInfo)member[0]).SetValue(ob, value, null);
			}
			else
			{
				((FieldInfo)member[0]).SetValue(ob, value);
			}
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x0009BBF8 File Offset: 0x00099DF8
		private void InitMember(Type type)
		{
			MemberInfo[] member = type.GetMember(this._name, BindingFlags.Instance | BindingFlags.Public);
			this._member = member[0];
			member = type.GetMember(this._name + "Specified", BindingFlags.Instance | BindingFlags.Public);
			if (member.Length > 0)
			{
				this._specifiedMember = member[0];
			}
			if (this._specifiedMember is PropertyInfo && !((PropertyInfo)this._specifiedMember).CanWrite)
			{
				this._specifiedMember = null;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x0009BC74 File Offset: 0x00099E74
		// (set) Token: 0x06001D92 RID: 7570 RVA: 0x0009BC7C File Offset: 0x00099E7C
		public TypeData TypeData
		{
			get
			{
				return this._typeData;
			}
			set
			{
				this._typeData = value;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x0009BC88 File Offset: 0x00099E88
		// (set) Token: 0x06001D94 RID: 7572 RVA: 0x0009BC90 File Offset: 0x00099E90
		public int Index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x0009BC9C File Offset: 0x00099E9C
		// (set) Token: 0x06001D96 RID: 7574 RVA: 0x0009BCA4 File Offset: 0x00099EA4
		public int GlobalIndex
		{
			get
			{
				return this._globalIndex;
			}
			set
			{
				this._globalIndex = value;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x0009BCB0 File Offset: 0x00099EB0
		// (set) Token: 0x06001D98 RID: 7576 RVA: 0x0009BCC0 File Offset: 0x00099EC0
		public bool IsOptionalValueType
		{
			get
			{
				return (this._flags & 1) != 0;
			}
			set
			{
				this._flags = ((!value) ? (this._flags & -2) : (this._flags | 1));
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001D99 RID: 7577 RVA: 0x0009BCF0 File Offset: 0x00099EF0
		// (set) Token: 0x06001D9A RID: 7578 RVA: 0x0009BD00 File Offset: 0x00099F00
		public bool IsReturnValue
		{
			get
			{
				return (this._flags & 2) != 0;
			}
			set
			{
				this._flags = ((!value) ? (this._flags & -3) : (this._flags | 2));
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001D9B RID: 7579 RVA: 0x0009BD30 File Offset: 0x00099F30
		// (set) Token: 0x06001D9C RID: 7580 RVA: 0x0009BD40 File Offset: 0x00099F40
		public bool Ignore
		{
			get
			{
				return (this._flags & 4) != 0;
			}
			set
			{
				this._flags = ((!value) ? (this._flags & -5) : (this._flags | 4));
			}
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x0009BD70 File Offset: 0x00099F70
		public void CheckOptionalValueType(Type type)
		{
			if (this._member == null)
			{
				this.InitMember(type);
			}
			this.IsOptionalValueType = (this._specifiedMember != null);
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x0009BDA4 File Offset: 0x00099FA4
		public bool GetValueSpecified(object ob)
		{
			if (this._specifiedMember is PropertyInfo)
			{
				return (bool)((PropertyInfo)this._specifiedMember).GetValue(ob, null);
			}
			return (bool)((FieldInfo)this._specifiedMember).GetValue(ob);
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x0009BDF0 File Offset: 0x00099FF0
		public void SetValueSpecified(object ob, bool value)
		{
			if (this._specifiedMember is PropertyInfo)
			{
				((PropertyInfo)this._specifiedMember).SetValue(ob, value, null);
			}
			else
			{
				((FieldInfo)this._specifiedMember).SetValue(ob, value);
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x0009BE44 File Offset: 0x0009A044
		public virtual bool RequiresNullable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000BB2 RID: 2994
		private const int OPTIONAL = 1;

		// Token: 0x04000BB3 RID: 2995
		private const int RETURN_VALUE = 2;

		// Token: 0x04000BB4 RID: 2996
		private const int IGNORE = 4;

		// Token: 0x04000BB5 RID: 2997
		private string _name;

		// Token: 0x04000BB6 RID: 2998
		private int _index;

		// Token: 0x04000BB7 RID: 2999
		private int _globalIndex;

		// Token: 0x04000BB8 RID: 3000
		private TypeData _typeData;

		// Token: 0x04000BB9 RID: 3001
		private MemberInfo _member;

		// Token: 0x04000BBA RID: 3002
		private MemberInfo _specifiedMember;

		// Token: 0x04000BBB RID: 3003
		private object _defaultValue = DBNull.Value;

		// Token: 0x04000BBC RID: 3004
		private string documentation;

		// Token: 0x04000BBD RID: 3005
		private int _flags;
	}
}
