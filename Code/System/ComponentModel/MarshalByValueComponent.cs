using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Implements <see cref="T:System.ComponentModel.IComponent" /> and provides the base implementation for remotable components that are marshaled by value (a copy of the serialized object is passed).</summary>
	// Token: 0x02000184 RID: 388
	[Designer("System.Windows.Forms.Design.ComponentDocumentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.ComponentModel.Design.IRootDesigner))]
	[DesignerCategory("Component")]
	[TypeConverter(typeof(ComponentConverter))]
	[ComVisible(true)]
	public class MarshalByValueComponent : IDisposable, IServiceProvider, IComponent
	{
		/// <summary>Adds an event handler to listen to the <see cref="E:System.ComponentModel.MarshalByValueComponent.Disposed" /> event on the component.</summary>
		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06000D46 RID: 3398 RVA: 0x000210C8 File Offset: 0x0001F2C8
		// (remove) Token: 0x06000D47 RID: 3399 RVA: 0x000210DC File Offset: 0x0001F2DC
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

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.MarshalByValueComponent" />.</summary>
		// Token: 0x06000D48 RID: 3400 RVA: 0x000210F0 File Offset: 0x0001F2F0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.MarshalByValueComponent" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000D49 RID: 3401 RVA: 0x00021100 File Offset: 0x0001F300
		[MonoTODO]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
			}
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00021108 File Offset: 0x0001F308
		~MarshalByValueComponent()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the implementer of the <see cref="T:System.IServiceProvider" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the implementer of the <see cref="T:System.IServiceProvider" />.</returns>
		/// <param name="service">A <see cref="T:System.Type" /> that represents the type of service you want. </param>
		// Token: 0x06000D4B RID: 3403 RVA: 0x00021144 File Offset: 0x0001F344
		public virtual object GetService(Type service)
		{
			if (this.mySite != null)
			{
				return this.mySite.GetService(service);
			}
			return null;
		}

		/// <summary>Gets the container for the component.</summary>
		/// <returns>An object implementing the <see cref="T:System.ComponentModel.IContainer" /> interface that represents the component's container, or null if the component does not have a site.</returns>
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00021160 File Offset: 0x0001F360
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual IContainer Container
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

		/// <summary>Gets a value indicating whether the component is currently in design mode.</summary>
		/// <returns>true if the component is in design mode; otherwise, false.</returns>
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x0002117C File Offset: 0x0001F37C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual bool DesignMode
		{
			get
			{
				return this.mySite != null && this.mySite.DesignMode;
			}
		}

		/// <summary>Gets or sets the site of the component.</summary>
		/// <returns>An object implementing the <see cref="T:System.ComponentModel.ISite" /> interface that represents the site of the component.</returns>
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x00021198 File Offset: 0x0001F398
		// (set) Token: 0x06000D4F RID: 3407 RVA: 0x000211A0 File Offset: 0x0001F3A0
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

		/// <summary>Returns a <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any. This method should not be overridden.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any.null if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		// Token: 0x06000D50 RID: 3408 RVA: 0x000211AC File Offset: 0x0001F3AC
		public override string ToString()
		{
			if (this.mySite == null)
			{
				return base.GetType().ToString();
			}
			return string.Format("{0} [{1}]", this.mySite.Name, base.GetType().ToString());
		}

		/// <summary>Gets the list of event handlers that are attached to this component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventHandlerList" /> that provides the delegates for this component.</returns>
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x000211F0 File Offset: 0x0001F3F0
		protected EventHandlerList Events
		{
			get
			{
				if (this.eventList == null)
				{
					this.eventList = new EventHandlerList();
				}
				return this.eventList;
			}
		}

		// Token: 0x040003AC RID: 940
		private EventHandlerList eventList;

		// Token: 0x040003AD RID: 941
		private ISite mySite;

		// Token: 0x040003AE RID: 942
		private object disposedEvent = new object();
	}
}
