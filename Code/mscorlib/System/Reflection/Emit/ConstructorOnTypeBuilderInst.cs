using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x020002C7 RID: 711
	internal class ConstructorOnTypeBuilderInst : ConstructorInfo
	{
		// Token: 0x060023ED RID: 9197 RVA: 0x00080C0C File Offset: 0x0007EE0C
		public ConstructorOnTypeBuilderInst(MonoGenericClass instantiation, ConstructorBuilder cb)
		{
			this.instantiation = instantiation;
			this.cb = cb;
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x00080C24 File Offset: 0x0007EE24
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x00080C2C File Offset: 0x0007EE2C
		public override string Name
		{
			get
			{
				return this.cb.Name;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x00080C3C File Offset: 0x0007EE3C
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x00080C44 File Offset: 0x0007EE44
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.cb.IsDefined(attributeType, inherit);
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x00080C54 File Offset: 0x0007EE54
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.cb.GetCustomAttributes(inherit);
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x00080C64 File Offset: 0x0007EE64
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.cb.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x00080C74 File Offset: 0x0007EE74
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.cb.GetMethodImplementationFlags();
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x00080C84 File Offset: 0x0007EE84
		public override ParameterInfo[] GetParameters()
		{
			if (!((ModuleBuilder)this.cb.Module).assemblyb.IsCompilerContext && !this.instantiation.generic_type.is_created)
			{
				throw new NotSupportedException();
			}
			ParameterInfo[] array = new ParameterInfo[this.cb.parameters.Length];
			for (int i = 0; i < this.cb.parameters.Length; i++)
			{
				Type type = this.instantiation.InflateType(this.cb.parameters[i]);
				array[i] = new ParameterInfo((this.cb.pinfo != null) ? this.cb.pinfo[i] : null, type, this, i + 1);
			}
			return array;
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060023F6 RID: 9206 RVA: 0x00080D48 File Offset: 0x0007EF48
		public override int MetadataToken
		{
			get
			{
				if (!((ModuleBuilder)this.cb.Module).assemblyb.IsCompilerContext)
				{
					return base.MetadataToken;
				}
				return this.cb.MetadataToken;
			}
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x00080D88 File Offset: 0x0007EF88
		internal override int GetParameterCount()
		{
			return this.cb.GetParameterCount();
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x00080D98 File Offset: 0x0007EF98
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this.cb.Invoke(obj, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060023F9 RID: 9209 RVA: 0x00080DAC File Offset: 0x0007EFAC
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.cb.MethodHandle;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x00080DBC File Offset: 0x0007EFBC
		public override MethodAttributes Attributes
		{
			get
			{
				return this.cb.Attributes;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060023FB RID: 9211 RVA: 0x00080DCC File Offset: 0x0007EFCC
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.cb.CallingConvention;
			}
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x00080DDC File Offset: 0x0007EFDC
		public override Type[] GetGenericArguments()
		{
			return this.cb.GetGenericArguments();
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060023FD RID: 9213 RVA: 0x00080DEC File Offset: 0x0007EFEC
		public override bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x00080DF0 File Offset: 0x0007EFF0
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060023FF RID: 9215 RVA: 0x00080DF4 File Offset: 0x0007EFF4
		public override bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x00080DF8 File Offset: 0x0007EFF8
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x04000DAF RID: 3503
		private MonoGenericClass instantiation;

		// Token: 0x04000DB0 RID: 3504
		private ConstructorBuilder cb;
	}
}
