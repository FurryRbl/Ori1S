using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x020002D8 RID: 728
	internal class FieldOnTypeBuilderInst : FieldInfo
	{
		// Token: 0x06002516 RID: 9494 RVA: 0x00083460 File Offset: 0x00081660
		public FieldOnTypeBuilderInst(MonoGenericClass instantiation, FieldBuilder fb)
		{
			this.instantiation = instantiation;
			this.fb = fb;
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06002517 RID: 9495 RVA: 0x00083478 File Offset: 0x00081678
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06002518 RID: 9496 RVA: 0x00083480 File Offset: 0x00081680
		public override string Name
		{
			get
			{
				return this.fb.Name;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06002519 RID: 9497 RVA: 0x00083490 File Offset: 0x00081690
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x00083498 File Offset: 0x00081698
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x000834A0 File Offset: 0x000816A0
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x000834A8 File Offset: 0x000816A8
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x000834B0 File Offset: 0x000816B0
		public override string ToString()
		{
			if (!((ModuleBuilder)this.instantiation.generic_type.Module).assemblyb.IsCompilerContext)
			{
				return this.fb.FieldType.ToString() + " " + this.Name;
			}
			return this.FieldType.ToString() + " " + this.Name;
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x0600251E RID: 9502 RVA: 0x00083520 File Offset: 0x00081720
		public override FieldAttributes Attributes
		{
			get
			{
				return this.fb.Attributes;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x0600251F RID: 9503 RVA: 0x00083530 File Offset: 0x00081730
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06002520 RID: 9504 RVA: 0x00083538 File Offset: 0x00081738
		public override int MetadataToken
		{
			get
			{
				if (!((ModuleBuilder)this.instantiation.generic_type.Module).assemblyb.IsCompilerContext)
				{
					throw new InvalidOperationException();
				}
				return this.fb.MetadataToken;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06002521 RID: 9505 RVA: 0x00083570 File Offset: 0x00081770
		public override Type FieldType
		{
			get
			{
				if (!((ModuleBuilder)this.instantiation.generic_type.Module).assemblyb.IsCompilerContext)
				{
					throw new NotSupportedException();
				}
				return this.instantiation.InflateType(this.fb.FieldType);
			}
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x000835C0 File Offset: 0x000817C0
		public override object GetValue(object obj)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x000835C8 File Offset: 0x000817C8
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000DEF RID: 3567
		internal MonoGenericClass instantiation;

		// Token: 0x04000DF0 RID: 3568
		internal FieldBuilder fb;
	}
}
