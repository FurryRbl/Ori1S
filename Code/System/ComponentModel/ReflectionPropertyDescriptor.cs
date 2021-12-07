using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x0200019F RID: 415
	internal class ReflectionPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06000EA3 RID: 3747 RVA: 0x00025AAC File Offset: 0x00023CAC
		public ReflectionPropertyDescriptor(Type componentType, PropertyDescriptor oldPropertyDescriptor, Attribute[] attributes) : base(oldPropertyDescriptor, attributes)
		{
			this._componentType = componentType;
			this._propertyType = oldPropertyDescriptor.PropertyType;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00025ACC File Offset: 0x00023CCC
		public ReflectionPropertyDescriptor(Type componentType, string name, Type type, Attribute[] attributes) : base(name, attributes)
		{
			this._componentType = componentType;
			this._propertyType = type;
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00025AE8 File Offset: 0x00023CE8
		public ReflectionPropertyDescriptor(PropertyInfo info) : base(info.Name, null)
		{
			this._member = info;
			this._componentType = this._member.DeclaringType;
			this._propertyType = info.PropertyType;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x00025B28 File Offset: 0x00023D28
		private PropertyInfo GetPropertyInfo()
		{
			if (this._member == null)
			{
				this._member = this._componentType.GetProperty(this.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
				if (this._member == null)
				{
					throw new ArgumentException("Accessor methods for the " + this.Name + " property are missing");
				}
			}
			return this._member;
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x00025B9C File Offset: 0x00023D9C
		public override Type ComponentType
		{
			get
			{
				return this._componentType;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x00025BA4 File Offset: 0x00023DA4
		public override bool IsReadOnly
		{
			get
			{
				ReadOnlyAttribute readOnlyAttribute = (ReadOnlyAttribute)this.Attributes[typeof(ReadOnlyAttribute)];
				return !this.GetPropertyInfo().CanWrite || readOnlyAttribute.IsReadOnly;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x00025BE8 File Offset: 0x00023DE8
		public override Type PropertyType
		{
			get
			{
				return this._propertyType;
			}
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00025BF0 File Offset: 0x00023DF0
		protected override void FillAttributes(IList attributeList)
		{
			base.FillAttributes(attributeList);
			if (!this.GetPropertyInfo().CanWrite)
			{
				attributeList.Add(ReadOnlyAttribute.Yes);
			}
			int num = 0;
			Type type = this.ComponentType;
			while (type != null && type != typeof(object))
			{
				num++;
				type = type.BaseType;
			}
			Attribute[][] array = new Attribute[num][];
			type = this.ComponentType;
			while (type != null && type != typeof(object))
			{
				PropertyInfo property = type.GetProperty(this.Name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
				if (property != null)
				{
					object[] customAttributes = property.GetCustomAttributes(false);
					Attribute[] array2 = new Attribute[customAttributes.Length];
					customAttributes.CopyTo(array2, 0);
					array[--num] = array2;
				}
				type = type.BaseType;
			}
			foreach (Attribute[] array4 in array)
			{
				if (array4 != null)
				{
					foreach (Attribute value in array4)
					{
						attributeList.Add(value);
					}
				}
			}
			foreach (object obj in TypeDescriptor.GetAttributes(this.PropertyType))
			{
				Attribute value2 = (Attribute)obj;
				attributeList.Add(value2);
			}
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00025D98 File Offset: 0x00023F98
		public override object GetValue(object component)
		{
			component = MemberDescriptor.GetInvokee(this._componentType, component);
			this.InitAccessors();
			return this.getter.GetValue(component, null);
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00025DBC File Offset: 0x00023FBC
		private System.ComponentModel.Design.DesignerTransaction CreateTransaction(object obj, string description)
		{
			IComponent component = obj as IComponent;
			if (component == null || component.Site == null)
			{
				return null;
			}
			System.ComponentModel.Design.IDesignerHost designerHost = (System.ComponentModel.Design.IDesignerHost)component.Site.GetService(typeof(System.ComponentModel.Design.IDesignerHost));
			if (designerHost == null)
			{
				return null;
			}
			System.ComponentModel.Design.DesignerTransaction result = designerHost.CreateTransaction(description);
			System.ComponentModel.Design.IComponentChangeService componentChangeService = (System.ComponentModel.Design.IComponentChangeService)component.Site.GetService(typeof(System.ComponentModel.Design.IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.OnComponentChanging(component, this);
			}
			return result;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00025E38 File Offset: 0x00024038
		private void EndTransaction(object obj, System.ComponentModel.Design.DesignerTransaction tran, object oldValue, object newValue, bool commit)
		{
			if (tran == null)
			{
				this.OnValueChanged(obj, new PropertyChangedEventArgs(this.Name));
				return;
			}
			if (commit)
			{
				IComponent component = obj as IComponent;
				System.ComponentModel.Design.IComponentChangeService componentChangeService = (System.ComponentModel.Design.IComponentChangeService)component.Site.GetService(typeof(System.ComponentModel.Design.IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(component, this, oldValue, newValue);
				}
				tran.Commit();
				this.OnValueChanged(obj, new PropertyChangedEventArgs(this.Name));
			}
			else
			{
				tran.Cancel();
			}
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00025EBC File Offset: 0x000240BC
		private void InitAccessors()
		{
			if (this.accessors_inited)
			{
				return;
			}
			PropertyInfo propertyInfo = this.GetPropertyInfo();
			MethodInfo setMethod = propertyInfo.GetSetMethod(true);
			MethodInfo getMethod = propertyInfo.GetGetMethod(true);
			if (getMethod != null)
			{
				this.getter = propertyInfo;
			}
			if (setMethod != null)
			{
				this.setter = propertyInfo;
			}
			if (setMethod != null && getMethod != null)
			{
				this.accessors_inited = true;
				return;
			}
			if (setMethod == null && getMethod == null)
			{
				this.accessors_inited = true;
				return;
			}
			MethodInfo methodInfo = (getMethod == null) ? setMethod : getMethod;
			if (methodInfo == null || !methodInfo.IsVirtual || (methodInfo.Attributes & MethodAttributes.VtableLayoutMask) == MethodAttributes.VtableLayoutMask)
			{
				this.accessors_inited = true;
				return;
			}
			Type baseType = this._componentType.BaseType;
			while (baseType != null && baseType != typeof(object))
			{
				propertyInfo = baseType.GetProperty(this.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
				if (propertyInfo == null)
				{
					break;
				}
				if (setMethod == null)
				{
					methodInfo = (setMethod = propertyInfo.GetSetMethod());
				}
				else
				{
					methodInfo = (getMethod = propertyInfo.GetGetMethod());
				}
				if (getMethod != null && this.getter == null)
				{
					this.getter = propertyInfo;
				}
				if (setMethod != null && this.setter == null)
				{
					this.setter = propertyInfo;
				}
				if (methodInfo != null)
				{
					break;
				}
				baseType = baseType.BaseType;
			}
			this.accessors_inited = true;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00026030 File Offset: 0x00024230
		public override void SetValue(object component, object value)
		{
			System.ComponentModel.Design.DesignerTransaction tran = this.CreateTransaction(component, "Set Property '" + this.Name + "'");
			object invokee = MemberDescriptor.GetInvokee(this._componentType, component);
			object value2 = this.GetValue(invokee);
			try
			{
				this.InitAccessors();
				this.setter.SetValue(invokee, value, null);
				this.EndTransaction(component, tran, value2, value, true);
			}
			catch
			{
				this.EndTransaction(component, tran, value2, value, false);
				throw;
			}
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x000260C4 File Offset: 0x000242C4
		private MethodInfo FindPropertyMethod(object o, string method_name)
		{
			MethodInfo result = null;
			string b = method_name + this.Name;
			foreach (MethodInfo methodInfo in o.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (methodInfo.Name == b && methodInfo.GetParameters().Length == 0)
				{
					result = methodInfo;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x00026134 File Offset: 0x00024334
		public override void ResetValue(object component)
		{
			object invokee = MemberDescriptor.GetInvokee(this._componentType, component);
			DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)this.Attributes[typeof(DefaultValueAttribute)];
			if (defaultValueAttribute != null)
			{
				this.SetValue(invokee, defaultValueAttribute.Value);
			}
			System.ComponentModel.Design.DesignerTransaction tran = this.CreateTransaction(component, "Reset Property '" + this.Name + "'");
			object value = this.GetValue(invokee);
			try
			{
				MethodInfo methodInfo = this.FindPropertyMethod(invokee, "Reset");
				if (methodInfo != null)
				{
					methodInfo.Invoke(invokee, null);
				}
				this.EndTransaction(component, tran, value, this.GetValue(invokee), true);
			}
			catch
			{
				this.EndTransaction(component, tran, value, this.GetValue(invokee), false);
				throw;
			}
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0002620C File Offset: 0x0002440C
		public override bool CanResetValue(object component)
		{
			component = MemberDescriptor.GetInvokee(this._componentType, component);
			DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)this.Attributes[typeof(DefaultValueAttribute)];
			if (defaultValueAttribute != null)
			{
				object value = this.GetValue(component);
				if (defaultValueAttribute.Value == null || value == null)
				{
					if (defaultValueAttribute.Value != value)
					{
						return true;
					}
					if (defaultValueAttribute.Value == null && value == null)
					{
						return false;
					}
				}
				return !defaultValueAttribute.Value.Equals(value);
			}
			if (!this._member.CanWrite)
			{
				return false;
			}
			MethodInfo methodInfo = this.FindPropertyMethod(component, "ShouldPersist");
			if (methodInfo != null)
			{
				return (bool)methodInfo.Invoke(component, null);
			}
			methodInfo = this.FindPropertyMethod(component, "ShouldSerialize");
			if (methodInfo != null && !(bool)methodInfo.Invoke(component, null))
			{
				return false;
			}
			methodInfo = this.FindPropertyMethod(component, "Reset");
			return methodInfo != null;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00026300 File Offset: 0x00024500
		public override bool ShouldSerializeValue(object component)
		{
			component = MemberDescriptor.GetInvokee(this._componentType, component);
			if (this.IsReadOnly)
			{
				MethodInfo methodInfo = this.FindPropertyMethod(component, "ShouldSerialize");
				if (methodInfo != null)
				{
					return (bool)methodInfo.Invoke(component, null);
				}
				return this.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);
			}
			else
			{
				DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)this.Attributes[typeof(DefaultValueAttribute)];
				if (defaultValueAttribute == null)
				{
					MethodInfo methodInfo2 = this.FindPropertyMethod(component, "ShouldSerialize");
					return methodInfo2 == null || (bool)methodInfo2.Invoke(component, null);
				}
				object value = this.GetValue(component);
				if (defaultValueAttribute.Value == null || value == null)
				{
					return defaultValueAttribute.Value != value;
				}
				return !defaultValueAttribute.Value.Equals(value);
			}
		}

		// Token: 0x0400041E RID: 1054
		private PropertyInfo _member;

		// Token: 0x0400041F RID: 1055
		private Type _componentType;

		// Token: 0x04000420 RID: 1056
		private Type _propertyType;

		// Token: 0x04000421 RID: 1057
		private PropertyInfo getter;

		// Token: 0x04000422 RID: 1058
		private PropertyInfo setter;

		// Token: 0x04000423 RID: 1059
		private bool accessors_inited;
	}
}
