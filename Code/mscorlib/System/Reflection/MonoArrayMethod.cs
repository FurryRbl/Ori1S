using System;
using System.Globalization;

namespace System.Reflection
{
	// Token: 0x020002ED RID: 749
	internal class MonoArrayMethod : MethodInfo
	{
		// Token: 0x06002695 RID: 9877 RVA: 0x00088574 File Offset: 0x00086774
		internal MonoArrayMethod(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			this.name = methodName;
			this.parent = arrayClass;
			this.ret = returnType;
			this.parameters = (Type[])parameterTypes.Clone();
			this.call_conv = callingConvention;
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000885AC File Offset: 0x000867AC
		[MonoTODO("Always returns this")]
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06002697 RID: 9879 RVA: 0x000885B0 File Offset: 0x000867B0
		public override Type ReturnType
		{
			get
			{
				return this.ret;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06002698 RID: 9880 RVA: 0x000885B8 File Offset: 0x000867B8
		[MonoTODO("Not implemented.  Always returns null")]
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x000885BC File Offset: 0x000867BC
		[MonoTODO("Not implemented.  Always returns zero")]
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MethodImplAttributes.IL;
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x000885C0 File Offset: 0x000867C0
		[MonoTODO("Not implemented.  Always returns an empty array")]
		public override ParameterInfo[] GetParameters()
		{
			return new ParameterInfo[0];
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x000885C8 File Offset: 0x000867C8
		[MonoTODO("Not implemented")]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x000885D0 File Offset: 0x000867D0
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.mhandle;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x0600269D RID: 9885 RVA: 0x000885D8 File Offset: 0x000867D8
		[MonoTODO("Not implemented.  Always returns zero")]
		public override MethodAttributes Attributes
		{
			get
			{
				return MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x0600269E RID: 9886 RVA: 0x000885DC File Offset: 0x000867DC
		public override Type ReflectedType
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x0600269F RID: 9887 RVA: 0x000885E4 File Offset: 0x000867E4
		public override Type DeclaringType
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060026A0 RID: 9888 RVA: 0x000885EC File Offset: 0x000867EC
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x000885F4 File Offset: 0x000867F4
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x00088600 File Offset: 0x00086800
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x0008860C File Offset: 0x0008680C
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x00088618 File Offset: 0x00086818
		public override string ToString()
		{
			string text = string.Empty;
			ParameterInfo[] array = this.GetParameters();
			for (int i = 0; i < array.Length; i++)
			{
				if (i > 0)
				{
					text += ", ";
				}
				text += array[i].ParameterType.Name;
			}
			if (this.ReturnType != null)
			{
				return string.Concat(new string[]
				{
					this.ReturnType.Name,
					" ",
					this.Name,
					"(",
					text,
					")"
				});
			}
			return string.Concat(new string[]
			{
				"void ",
				this.Name,
				"(",
				text,
				")"
			});
		}

		// Token: 0x04000E78 RID: 3704
		internal RuntimeMethodHandle mhandle;

		// Token: 0x04000E79 RID: 3705
		internal Type parent;

		// Token: 0x04000E7A RID: 3706
		internal Type ret;

		// Token: 0x04000E7B RID: 3707
		internal Type[] parameters;

		// Token: 0x04000E7C RID: 3708
		internal string name;

		// Token: 0x04000E7D RID: 3709
		internal int table_idx;

		// Token: 0x04000E7E RID: 3710
		internal CallingConventions call_conv;
	}
}
