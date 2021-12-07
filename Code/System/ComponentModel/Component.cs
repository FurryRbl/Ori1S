using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Provides the base implementation for the <see cref="T:System.ComponentModel.IComponent" /> interface and enables object sharing between applications.</summary>
	// Token: 0x020000DE RID: 222
	[DesignerCategory("Component")]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class Component : MarshalByRefObject, IDisposable, IComponent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Component" /> class. </summary>
		// Token: 0x06000967 RID: 2407 RVA: 0x0001B590 File Offset: 0x00019790
		public Component()
		{
			this.event_handlers = null;
		}

		/// <summary>Occurs when the component is disposed by a call to the <see cref="M:System.ComponentModel.Component.Dispose" /> method. </summary>
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000968 RID: 2408 RVA: 0x0001B5AC File Offset: 0x000197AC
		// (remove) Token: 0x06000969 RID: 2409 RVA: 0x0001B5C0 File Offset: 0x000197C0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event EventHandler Disposed
		{
			add
			{
				this.Events.AddHandler(this.disposedEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(this.disposedEvent, value);
			}
		}

		/// <summary>Gets a value indicating whether the component can raise an event.</summary>
		/// <returns>true if the component can raise events; otherwise, false. The default is true.</returns>
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0001B5D4 File Offset: 0x000197D4
		protected virtual bool CanRaiseEvents
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.ISite" /> of the <see cref="T:System.ComponentModel.Component" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.ComponentModel.Component" />, or null if the <see cref="T:System.ComponentModel.Component" /> is not encapsulated in an <see cref="T:System.ComponentModel.IContainer" />, the <see cref="T:System.ComponentModel.Component" /> does not have an <see cref="T:System.ComponentModel.ISite" /> associated with it, or the <see cref="T:System.ComponentModel.Component" /> is removed from its <see cref="T:System.ComponentModel.IContainer" />.</returns>
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0001B5D8 File Offset: 0x000197D8
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x0001B5E0 File Offset: 0x000197E0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual ISite Site
		{
			get
			{
				return this.mySite;
			}
			set
			{
				this.mySite = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.IContainer" /> that contains the <see cref="T:System.ComponentModel.Component" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IContainer" /> that contains the <see cref="T:System.ComponentModel.Component" />, if any, or null if the <see cref="T:System.ComponentModel.Component" /> is not encapsulated in an <see cref="T:System.ComponentModel.IContainer" />.</returns>
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x0001B5EC File Offset: 0x000197EC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IContainer Container
		{
			get
			{
				if (this.mySite == null)
				{
					return null;
				}
				return this.mySite.Container;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.ComponentModel.Component" /> is currently in design mode.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.Component" /> is in design mode; otherwise, false.</returns>
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0001B608 File Offset: 0x00019808
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		protected bool DesignMode
		{
			get
			{
				return this.mySite != null && this.mySite.DesignMode;
			}
		}

		/// <summary>Gets the list of event handlers that are attached to this <see cref="T:System.ComponentModel.Component" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventHandlerList" /> that provides the delegates for this component.</returns>
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x0001B624 File Offset: 0x00019824
		protected EventHandlerList Events
		{
			get
			{
				if (this.event_handlers == null)
				{
					this.event_handlers = new EventHandlerList();
				}
				return this.event_handlers;
			}
		}

		/// <summary>Releases unmanaged resources and performs other cleanup operations before the <see cref="T:System.ComponentModel.Component" /> is reclaimed by garbage collection.</summary>
		// Token: 0x06000970 RID: 2416 RVA: 0x0001B644 File Offset: 0x00019844
		~Component()
		{
			this.Dispose(false);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Component" />.</summary>
		// Token: 0x06000971 RID: 2417 RVA: 0x0001B680 File Offset: 0x00019880
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000972 RID: 2418 RVA: 0x0001B690 File Offset: 0x00019890
		protected virtual void Dispose(bool release_all)
		{
			if (release_all)
			{
				if (this.mySite != null && this.mySite.Container != null)
				{
					this.mySite.Container.Remove(this);
				}
				EventHandler eventHandler = (EventHandler)this.Events[this.disposedEvent];
				if (eventHandler != null)
				{
					eventHandler(this, EventArgs.Empty);
				}
			}
		}

		/// <summary>Returns an object that represents a service provided by the <see cref="T:System.ComponentModel.Component" /> or by its <see cref="T:System.ComponentModel.Container" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents a service provided by the <see cref="T:System.ComponentModel.Component" />, or null if the <see cref="T:System.ComponentModel.Component" /> does not provide the specified service.</returns>
		/// <param name="service">A service provided by the <see cref="T:System.ComponentModel.Component" />. </param>
		// Token: 0x06000973 RID: 2419 RVA: 0x0001B6F8 File Offset: 0x000198F8
		protected virtual object GetService(Type service)
		{
			if (this.mySite != null)
			{
				return this.mySite.GetService(service);
			}
			return null;
		}

		/// <summary>Returns a <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any. This method should not be overridden.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any, or null if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		// Token: 0x06000974 RID: 2420 RVA: 0x0001B714 File Offset: 0x00019914
		public override string ToString()
		{
			if (this.mySite == null)
			{
				return base.GetType().ToString();
			}
			return string.Format("{0} [{1}]", this.mySite.Name, base.GetType().ToString());
		}

		// Token: 0x04000282 RID: 642
		private EventHandlerList event_handlers;

		// Token: 0x04000283 RID: 643
		private ISite mySite;

		// Token: 0x04000284 RID: 644
		private object disposedEvent = new object();
	}
}
