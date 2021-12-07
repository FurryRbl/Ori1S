using System;
using System.Collections;

namespace System.Reflection.Emit
{
	// Token: 0x020002D5 RID: 725
	internal class EventOnTypeBuilderInst : EventInfo
	{
		// Token: 0x060024E6 RID: 9446 RVA: 0x00082EB8 File Offset: 0x000810B8
		internal EventOnTypeBuilderInst(MonoGenericClass instantiation, EventBuilder evt)
		{
			this.instantiation = instantiation;
			this.evt = evt;
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060024E7 RID: 9447 RVA: 0x00082ED0 File Offset: 0x000810D0
		public override EventAttributes Attributes
		{
			get
			{
				return this.evt.attrs;
			}
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x00082EE0 File Offset: 0x000810E0
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			if (this.evt.add_method == null || (!nonPublic && !this.evt.add_method.IsPublic))
			{
				return null;
			}
			return TypeBuilder.GetMethod(this.instantiation, this.evt.add_method);
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x00082F30 File Offset: 0x00081130
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			if (this.evt.raise_method == null || (!nonPublic && !this.evt.raise_method.IsPublic))
			{
				return null;
			}
			return TypeBuilder.GetMethod(this.instantiation, this.evt.raise_method);
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x00082F80 File Offset: 0x00081180
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			if (this.evt.remove_method == null || (!nonPublic && !this.evt.remove_method.IsPublic))
			{
				return null;
			}
			return TypeBuilder.GetMethod(this.instantiation, this.evt.remove_method);
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x00082FD0 File Offset: 0x000811D0
		public override MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			if (this.evt.other_methods == null)
			{
				return new MethodInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (MethodBuilder methodInfo in this.evt.other_methods)
			{
				if (nonPublic || methodInfo.IsPublic)
				{
					arrayList.Add(TypeBuilder.GetMethod(this.instantiation, methodInfo));
				}
			}
			MethodInfo[] array = new MethodInfo[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060024EC RID: 9452 RVA: 0x0008305C File Offset: 0x0008125C
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060024ED RID: 9453 RVA: 0x00083064 File Offset: 0x00081264
		public override string Name
		{
			get
			{
				return this.evt.name;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060024EE RID: 9454 RVA: 0x00083074 File Offset: 0x00081274
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x0008307C File Offset: 0x0008127C
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x00083084 File Offset: 0x00081284
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x0008308C File Offset: 0x0008128C
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000DDE RID: 3550
		private MonoGenericClass instantiation;

		// Token: 0x04000DDF RID: 3551
		private EventBuilder evt;
	}
}
