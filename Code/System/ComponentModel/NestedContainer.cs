using System;

namespace System.ComponentModel
{
	/// <summary>Provides the base implementation for the <see cref="T:System.ComponentModel.INestedContainer" /> interface, which enables containers to have an owning component.</summary>
	// Token: 0x0200018E RID: 398
	public class NestedContainer : Container, IDisposable, IContainer, INestedContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.NestedContainer" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.ComponentModel.IComponent" /> that owns this nested container.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is null.</exception>
		// Token: 0x06000DE8 RID: 3560 RVA: 0x00023EC0 File Offset: 0x000220C0
		public NestedContainer(IComponent owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this._owner = owner;
			this._owner.Disposed += this.OnOwnerDisposed;
		}

		/// <summary>Gets the owning component for this nested container.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> that owns this nested container.</returns>
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00023EF8 File Offset: 0x000220F8
		public IComponent Owner
		{
			get
			{
				return this._owner;
			}
		}

		/// <summary>Gets the name of the owning component.</summary>
		/// <returns>The name of the owning component.</returns>
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x00023F00 File Offset: 0x00022100
		protected virtual string OwnerName
		{
			get
			{
				if (this._owner.Site is INestedSite)
				{
					return ((INestedSite)this._owner.Site).FullName;
				}
				if (this._owner == null || this._owner.Site == null)
				{
					return null;
				}
				return this._owner.Site.Name;
			}
		}

		/// <summary>Creates a site for the component within the container.</summary>
		/// <returns>The newly created <see cref="T:System.ComponentModel.ISite" />.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to create a site for.</param>
		/// <param name="name">The name to assign to <paramref name="component" />, or null to skip the name assignment.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		// Token: 0x06000DEB RID: 3563 RVA: 0x00023F68 File Offset: 0x00022168
		protected override ISite CreateSite(IComponent component, string name)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return new NestedContainer.Site(component, this, name);
		}

		/// <summary>Gets the service object of the specified type, if it is available.</summary>
		/// <returns>An <see cref="T:System.Object" /> that implements the requested service, or null if the service cannot be resolved.</returns>
		/// <param name="service">The <see cref="T:System.Type" /> of the service to retrieve.</param>
		// Token: 0x06000DEC RID: 3564 RVA: 0x00023F84 File Offset: 0x00022184
		protected override object GetService(Type service)
		{
			if (service == typeof(INestedContainer))
			{
				return this;
			}
			return base.GetService(service);
		}

		/// <summary>Releases the resources used by the nested container.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06000DED RID: 3565 RVA: 0x00023FA0 File Offset: 0x000221A0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._owner.Disposed -= this.OnOwnerDisposed;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00023FD4 File Offset: 0x000221D4
		private void OnOwnerDisposed(object sender, EventArgs e)
		{
			this.Dispose();
		}

		// Token: 0x040003EF RID: 1007
		private IComponent _owner;

		// Token: 0x0200018F RID: 399
		private class Site : IServiceProvider, INestedSite, ISite
		{
			// Token: 0x06000DEF RID: 3567 RVA: 0x00023FDC File Offset: 0x000221DC
			public Site(IComponent component, NestedContainer container, string name)
			{
				this._component = component;
				this._nestedContainer = container;
				this._siteName = name;
			}

			// Token: 0x17000335 RID: 821
			// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x00023FFC File Offset: 0x000221FC
			public IComponent Component
			{
				get
				{
					return this._component;
				}
			}

			// Token: 0x17000336 RID: 822
			// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00024004 File Offset: 0x00022204
			public IContainer Container
			{
				get
				{
					return this._nestedContainer;
				}
			}

			// Token: 0x17000337 RID: 823
			// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0002400C File Offset: 0x0002220C
			public bool DesignMode
			{
				get
				{
					return this._nestedContainer.Owner != null && this._nestedContainer.Owner.Site != null && this._nestedContainer.Owner.Site.DesignMode;
				}
			}

			// Token: 0x17000338 RID: 824
			// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x00024058 File Offset: 0x00022258
			// (set) Token: 0x06000DF4 RID: 3572 RVA: 0x00024060 File Offset: 0x00022260
			public string Name
			{
				get
				{
					return this._siteName;
				}
				set
				{
					this._siteName = value;
				}
			}

			// Token: 0x17000339 RID: 825
			// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0002406C File Offset: 0x0002226C
			public string FullName
			{
				get
				{
					if (this._siteName == null)
					{
						return null;
					}
					if (this._nestedContainer.OwnerName == null)
					{
						return this._siteName;
					}
					return this._nestedContainer.OwnerName + "." + this._siteName;
				}
			}

			// Token: 0x06000DF6 RID: 3574 RVA: 0x000240B8 File Offset: 0x000222B8
			public virtual object GetService(Type service)
			{
				if (service == typeof(ISite))
				{
					return this;
				}
				return this._nestedContainer.GetService(service);
			}

			// Token: 0x040003F0 RID: 1008
			private IComponent _component;

			// Token: 0x040003F1 RID: 1009
			private NestedContainer _nestedContainer;

			// Token: 0x040003F2 RID: 1010
			private string _siteName;
		}
	}
}
