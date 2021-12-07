using System;
using System.Collections.Generic;

namespace System.ComponentModel
{
	/// <summary>Encapsulates zero or more components.</summary>
	// Token: 0x020000E1 RID: 225
	public class Container : IDisposable, IContainer
	{
		/// <summary>Gets all the components in the <see cref="T:System.ComponentModel.Container" />.</summary>
		/// <returns>A collection that contains the components in the <see cref="T:System.ComponentModel.Container" />.</returns>
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x0001B9C8 File Offset: 0x00019BC8
		public virtual ComponentCollection Components
		{
			get
			{
				IComponent[] components = this.c.ToArray();
				return new ComponentCollection(components);
			}
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.Component" /> to the <see cref="T:System.ComponentModel.Container" />. The component is unnamed.</summary>
		/// <param name="component">The component to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600097F RID: 2431 RVA: 0x0001B9E8 File Offset: 0x00019BE8
		public virtual void Add(IComponent component)
		{
			this.Add(component, null);
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.Component" /> to the <see cref="T:System.ComponentModel.Container" /> and assigns it a name.</summary>
		/// <param name="component">The component to add. </param>
		/// <param name="name">The unique, case-insensitive name to assign to the component.-or- null, which leaves the component unnamed. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not unique.</exception>
		// Token: 0x06000980 RID: 2432 RVA: 0x0001B9F4 File Offset: 0x00019BF4
		public virtual void Add(IComponent component, string name)
		{
			if (component != null && (component.Site == null || component.Site.Container != this))
			{
				this.ValidateName(component, name);
				if (component.Site != null)
				{
					component.Site.Container.Remove(component);
				}
				component.Site = this.CreateSite(component, name);
				this.c.Add(component);
			}
		}

		/// <summary>Determines whether the component name is unique for this container.</summary>
		/// <param name="component">The named component.</param>
		/// <param name="name">The component name to validate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not unique.</exception>
		// Token: 0x06000981 RID: 2433 RVA: 0x0001BA64 File Offset: 0x00019C64
		protected virtual void ValidateName(IComponent component, string name)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (name == null)
			{
				return;
			}
			foreach (IComponent component2 in this.c)
			{
				if (!object.ReferenceEquals(component, component2))
				{
					if (component2.Site != null && string.Compare(component2.Site.Name, name, true) == 0)
					{
						throw new ArgumentException(string.Format("There already is a named component '{0}' in this container", name));
					}
				}
			}
		}

		/// <summary>Creates a site <see cref="T:System.ComponentModel.ISite" /> for the given <see cref="T:System.ComponentModel.IComponent" /> and assigns the given name to the site.</summary>
		/// <returns>The newly created site.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to create a site for. </param>
		/// <param name="name">The name to assign to <paramref name="component" />, or null to skip the name assignment. </param>
		// Token: 0x06000982 RID: 2434 RVA: 0x0001BB20 File Offset: 0x00019D20
		protected virtual ISite CreateSite(IComponent component, string name)
		{
			return new Container.DefaultSite(name, component, this);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Container" />.</summary>
		// Token: 0x06000983 RID: 2435 RVA: 0x0001BB2C File Offset: 0x00019D2C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Container" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000984 RID: 2436 RVA: 0x0001BB3C File Offset: 0x00019D3C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				while (this.c.Count > 0)
				{
					int index = this.c.Count - 1;
					IComponent component = this.c[index];
					this.Remove(component);
					component.Dispose();
				}
			}
		}

		/// <summary>Releases unmanaged resources and performs other cleanup operations before the <see cref="T:System.ComponentModel.Container" /> is reclaimed by garbage collection.</summary>
		// Token: 0x06000985 RID: 2437 RVA: 0x0001BB90 File Offset: 0x00019D90
		~Container()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the service object of the specified type, if it is available.</summary>
		/// <returns>An <see cref="T:System.Object" /> implementing the requested service, or null if the service cannot be resolved.</returns>
		/// <param name="service">The <see cref="T:System.Type" /> of the service to retrieve. </param>
		// Token: 0x06000986 RID: 2438 RVA: 0x0001BBCC File Offset: 0x00019DCC
		protected virtual object GetService(Type service)
		{
			if (typeof(IContainer) != service)
			{
				return null;
			}
			return this;
		}

		/// <summary>Removes a component from the <see cref="T:System.ComponentModel.Container" />.</summary>
		/// <param name="component">The component to remove. </param>
		// Token: 0x06000987 RID: 2439 RVA: 0x0001BBE4 File Offset: 0x00019DE4
		public virtual void Remove(IComponent component)
		{
			this.Remove(component, true);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0001BBF0 File Offset: 0x00019DF0
		private void Remove(IComponent component, bool unsite)
		{
			if (component != null && component.Site != null && component.Site.Container == this)
			{
				if (unsite)
				{
					component.Site = null;
				}
				this.c.Remove(component);
			}
		}

		/// <summary>Removes a component from the <see cref="T:System.ComponentModel.Container" /> without setting <see cref="P:System.ComponentModel.IComponent.Site" /> to null.</summary>
		/// <param name="component">The component to remove.</param>
		// Token: 0x06000989 RID: 2441 RVA: 0x0001BC3C File Offset: 0x00019E3C
		protected void RemoveWithoutUnsiting(IComponent component)
		{
			this.Remove(component, false);
		}

		// Token: 0x04000285 RID: 645
		private List<IComponent> c = new List<IComponent>();

		// Token: 0x020000E2 RID: 226
		private class DefaultSite : IServiceProvider, ISite
		{
			// Token: 0x0600098A RID: 2442 RVA: 0x0001BC48 File Offset: 0x00019E48
			public DefaultSite(string name, IComponent component, Container container)
			{
				this.component = component;
				this.container = container;
				this.name = name;
			}

			// Token: 0x17000227 RID: 551
			// (get) Token: 0x0600098B RID: 2443 RVA: 0x0001BC68 File Offset: 0x00019E68
			public IComponent Component
			{
				get
				{
					return this.component;
				}
			}

			// Token: 0x17000228 RID: 552
			// (get) Token: 0x0600098C RID: 2444 RVA: 0x0001BC70 File Offset: 0x00019E70
			public IContainer Container
			{
				get
				{
					return this.container;
				}
			}

			// Token: 0x17000229 RID: 553
			// (get) Token: 0x0600098D RID: 2445 RVA: 0x0001BC78 File Offset: 0x00019E78
			public bool DesignMode
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700022A RID: 554
			// (get) Token: 0x0600098E RID: 2446 RVA: 0x0001BC7C File Offset: 0x00019E7C
			// (set) Token: 0x0600098F RID: 2447 RVA: 0x0001BC84 File Offset: 0x00019E84
			public string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					this.name = value;
				}
			}

			// Token: 0x06000990 RID: 2448 RVA: 0x0001BC90 File Offset: 0x00019E90
			public virtual object GetService(Type t)
			{
				if (typeof(ISite) == t)
				{
					return this;
				}
				return this.container.GetService(t);
			}

			// Token: 0x04000286 RID: 646
			private readonly IComponent component;

			// Token: 0x04000287 RID: 647
			private readonly Container container;

			// Token: 0x04000288 RID: 648
			private string name;
		}
	}
}
