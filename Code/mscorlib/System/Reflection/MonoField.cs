using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020002A6 RID: 678
	[Serializable]
	internal class MonoField : FieldInfo, ISerializable
	{
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x0007C818 File Offset: 0x0007AA18
		public override FieldAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x0600227A RID: 8826 RVA: 0x0007C820 File Offset: 0x0007AA20
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				return this.fhandle;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x0600227B RID: 8827 RVA: 0x0007C828 File Offset: 0x0007AA28
		public override Type FieldType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0600227C RID: 8828
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type GetParentType(bool declaring);

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x0600227D RID: 8829 RVA: 0x0007C830 File Offset: 0x0007AA30
		public override Type ReflectedType
		{
			get
			{
				return this.GetParentType(false);
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x0007C83C File Offset: 0x0007AA3C
		public override Type DeclaringType
		{
			get
			{
				return this.GetParentType(true);
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x0007C848 File Offset: 0x0007AA48
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x0007C850 File Offset: 0x0007AA50
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x0007C85C File Offset: 0x0007AA5C
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x0007C868 File Offset: 0x0007AA68
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06002283 RID: 8835
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal override extern int GetFieldOffset();

		// Token: 0x06002284 RID: 8836
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object GetValueInternal(object obj);

		// Token: 0x06002285 RID: 8837 RVA: 0x0007C874 File Offset: 0x0007AA74
		public override object GetValue(object obj)
		{
			if (!this.IsStatic && obj == null)
			{
				throw new TargetException("Non-static field requires a target");
			}
			if (!this.IsLiteral)
			{
				this.CheckGeneric();
			}
			return this.GetValueInternal(obj);
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x0007C8B8 File Offset: 0x0007AAB8
		public override string ToString()
		{
			return string.Format("{0} {1}", this.type, this.name);
		}

		// Token: 0x06002287 RID: 8839
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetValueInternal(FieldInfo fi, object obj, object value);

		// Token: 0x06002288 RID: 8840 RVA: 0x0007C8D0 File Offset: 0x0007AAD0
		public override void SetValue(object obj, object val, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			if (!this.IsStatic && obj == null)
			{
				throw new TargetException("Non-static field requires a target");
			}
			if (this.IsLiteral)
			{
				throw new FieldAccessException("Cannot set a constant field");
			}
			if (binder == null)
			{
				binder = Binder.DefaultBinder;
			}
			this.CheckGeneric();
			if (val != null)
			{
				object obj2 = binder.ChangeType(val, this.type, culture);
				if (obj2 == null)
				{
					throw new ArgumentException(string.Concat(new object[]
					{
						"Object type ",
						val.GetType(),
						" cannot be converted to target type: ",
						this.type
					}), "val");
				}
				val = obj2;
			}
			MonoField.SetValueInternal(this, obj, val);
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x0007C984 File Offset: 0x0007AB84
		internal MonoField Clone(string newName)
		{
			return new MonoField
			{
				name = newName,
				type = this.type,
				attrs = this.attrs,
				klass = this.klass,
				fhandle = this.fhandle
			};
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x0007C9D0 File Offset: 0x0007ABD0
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			MemberInfoSerializationHolder.Serialize(info, this.Name, this.ReflectedType, this.ToString(), MemberTypes.Field);
		}

		// Token: 0x0600228B RID: 8843
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern object GetRawConstantValue();

		// Token: 0x0600228C RID: 8844 RVA: 0x0007C9F8 File Offset: 0x0007ABF8
		private void CheckGeneric()
		{
			if (this.DeclaringType.ContainsGenericParameters)
			{
				throw new InvalidOperationException("Late bound operations cannot be performed on fields with types for which Type.ContainsGenericParameters is true.");
			}
		}

		// Token: 0x04000CE3 RID: 3299
		internal IntPtr klass;

		// Token: 0x04000CE4 RID: 3300
		internal RuntimeFieldHandle fhandle;

		// Token: 0x04000CE5 RID: 3301
		private string name;

		// Token: 0x04000CE6 RID: 3302
		private Type type;

		// Token: 0x04000CE7 RID: 3303
		private FieldAttributes attrs;
	}
}
