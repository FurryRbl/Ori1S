using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Security
{
	// Token: 0x02000547 RID: 1351
	internal struct SecurityFrame
	{
		// Token: 0x06003527 RID: 13607 RVA: 0x000AF9B4 File Offset: 0x000ADBB4
		internal SecurityFrame(RuntimeSecurityFrame frame)
		{
			this._domain = null;
			this._method = null;
			this._assert = null;
			this._deny = null;
			this._permitonly = null;
			this.InitFromRuntimeFrame(frame);
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x000AF9EC File Offset: 0x000ADBEC
		internal SecurityFrame(int skip)
		{
			this._domain = null;
			this._method = null;
			this._assert = null;
			this._deny = null;
			this._permitonly = null;
			this.InitFromRuntimeFrame(SecurityFrame._GetSecurityFrame(skip + 2));
		}

		// Token: 0x06003529 RID: 13609
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RuntimeSecurityFrame _GetSecurityFrame(int skip);

		// Token: 0x0600352A RID: 13610
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Array _GetSecurityStack(int skip);

		// Token: 0x0600352B RID: 13611 RVA: 0x000AFA20 File Offset: 0x000ADC20
		internal void InitFromRuntimeFrame(RuntimeSecurityFrame frame)
		{
			this._domain = frame.domain;
			this._method = frame.method;
			if (frame.assert.size > 0)
			{
				this._assert = SecurityManager.Decode(frame.assert.blob, frame.assert.size);
			}
			if (frame.deny.size > 0)
			{
				this._deny = SecurityManager.Decode(frame.deny.blob, frame.deny.size);
			}
			if (frame.permitonly.size > 0)
			{
				this._permitonly = SecurityManager.Decode(frame.permitonly.blob, frame.permitonly.size);
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x0600352C RID: 13612 RVA: 0x000AFADC File Offset: 0x000ADCDC
		public Assembly Assembly
		{
			get
			{
				return this._method.ReflectedType.Assembly;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x0600352D RID: 13613 RVA: 0x000AFAF0 File Offset: 0x000ADCF0
		public AppDomain Domain
		{
			get
			{
				return this._domain;
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x0600352E RID: 13614 RVA: 0x000AFAF8 File Offset: 0x000ADCF8
		public MethodInfo Method
		{
			get
			{
				return this._method;
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x0600352F RID: 13615 RVA: 0x000AFB00 File Offset: 0x000ADD00
		public PermissionSet Assert
		{
			get
			{
				return this._assert;
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06003530 RID: 13616 RVA: 0x000AFB08 File Offset: 0x000ADD08
		public PermissionSet Deny
		{
			get
			{
				return this._deny;
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06003531 RID: 13617 RVA: 0x000AFB10 File Offset: 0x000ADD10
		public PermissionSet PermitOnly
		{
			get
			{
				return this._permitonly;
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06003532 RID: 13618 RVA: 0x000AFB18 File Offset: 0x000ADD18
		public bool HasStackModifiers
		{
			get
			{
				return this._assert != null || this._deny != null || this._permitonly != null;
			}
		}

		// Token: 0x06003533 RID: 13619 RVA: 0x000AFB40 File Offset: 0x000ADD40
		public bool Equals(SecurityFrame sf)
		{
			return object.ReferenceEquals(this._domain, sf.Domain) && !(this.Assembly.ToString() != sf.Assembly.ToString()) && !(this.Method.ToString() != sf.Method.ToString()) && (this._assert == null || this._assert.Equals(sf.Assert)) && (this._deny == null || this._deny.Equals(sf.Deny)) && (this._permitonly == null || this._permitonly.Equals(sf.PermitOnly));
		}

		// Token: 0x06003534 RID: 13620 RVA: 0x000AFC1C File Offset: 0x000ADE1C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Frame: {0}{1}", this._method, Environment.NewLine);
			stringBuilder.AppendFormat("\tAppDomain: {0}{1}", this.Domain, Environment.NewLine);
			stringBuilder.AppendFormat("\tAssembly: {0}{1}", this.Assembly, Environment.NewLine);
			if (this._assert != null)
			{
				stringBuilder.AppendFormat("\tAssert: {0}{1}", this._assert, Environment.NewLine);
			}
			if (this._deny != null)
			{
				stringBuilder.AppendFormat("\tDeny: {0}{1}", this._deny, Environment.NewLine);
			}
			if (this._permitonly != null)
			{
				stringBuilder.AppendFormat("\tPermitOnly: {0}{1}", this._permitonly, Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003535 RID: 13621 RVA: 0x000AFCE0 File Offset: 0x000ADEE0
		public static ArrayList GetStack(int skipFrames)
		{
			Array array = SecurityFrame._GetSecurityStack(skipFrames + 2);
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < array.Length; i++)
			{
				object value = array.GetValue(i);
				if (value == null)
				{
					break;
				}
				arrayList.Add(new SecurityFrame((RuntimeSecurityFrame)value));
			}
			return arrayList;
		}

		// Token: 0x04001655 RID: 5717
		private AppDomain _domain;

		// Token: 0x04001656 RID: 5718
		private MethodInfo _method;

		// Token: 0x04001657 RID: 5719
		private PermissionSet _assert;

		// Token: 0x04001658 RID: 5720
		private PermissionSet _deny;

		// Token: 0x04001659 RID: 5721
		private PermissionSet _permitonly;
	}
}
