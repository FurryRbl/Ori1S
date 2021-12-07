using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the name of the category in which to group the property or event when displayed in a <see cref="T:System.Windows.Forms.PropertyGrid" /> control set to Categorized mode.</summary>
	// Token: 0x020000D6 RID: 214
	[AttributeUsage(AttributeTargets.All)]
	public class CategoryAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CategoryAttribute" /> class using the category name Default.</summary>
		// Token: 0x06000937 RID: 2359 RVA: 0x0001AAE4 File Offset: 0x00018CE4
		public CategoryAttribute()
		{
			this.category = "Misc";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CategoryAttribute" /> class using the specified category name.</summary>
		/// <param name="category">The name of the category. </param>
		// Token: 0x06000938 RID: 2360 RVA: 0x0001AAF8 File Offset: 0x00018CF8
		public CategoryAttribute(string category)
		{
			this.category = category;
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Action category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the action category.</returns>
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x0001AB14 File Offset: 0x00018D14
		public static CategoryAttribute Action
		{
			get
			{
				if (CategoryAttribute.action != null)
				{
					return CategoryAttribute.action;
				}
				object obj = CategoryAttribute.lockobj;
				lock (obj)
				{
					if (CategoryAttribute.action == null)
					{
						CategoryAttribute.action = new CategoryAttribute("Action");
					}
				}
				return CategoryAttribute.action;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Appearance category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the appearance category.</returns>
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0001AB90 File Offset: 0x00018D90
		public static CategoryAttribute Appearance
		{
			get
			{
				if (CategoryAttribute.appearance != null)
				{
					return CategoryAttribute.appearance;
				}
				object obj = CategoryAttribute.lockobj;
				lock (obj)
				{
					if (CategoryAttribute.appearance == null)
					{
						CategoryAttribute.appearance = new CategoryAttribute("Appearance");
					}
				}
				return CategoryAttribute.appearance;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Asynchronous category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the asynchronous category.</returns>
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0001AC0C File Offset: 0x00018E0C
		public static CategoryAttribute Asynchronous
		{
			get
			{
				if (CategoryAttribute.behaviour != null)
				{
					return CategoryAttribute.behaviour;
				}
				object obj = CategoryAttribute.lockobj;
				lock (obj)
				{
					if (CategoryAttribute.async == null)
					{
						CategoryAttribute.async = new CategoryAttribute("Asynchronous");
					}
				}
				return CategoryAttribute.async;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Behavior category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the behavior category.</returns>
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0001AC88 File Offset: 0x00018E88
		public static CategoryAttribute Behavior
		{
			get
			{
				if (CategoryAttribute.behaviour != null)
				{
					return CategoryAttribute.behaviour;
				}
				object obj = CategoryAttribute.lockobj;
				lock (obj)
				{
					if (CategoryAttribute.behaviour == null)
					{
						CategoryAttribute.behaviour = new CategoryAttribute("Behavior");
					}
				}
				return CategoryAttribute.behaviour;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Data category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the data category.</returns>
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0001AD04 File Offset: 0x00018F04
		public static CategoryAttribute Data
		{
			get
			{
				if (CategoryAttribute.data != null)
				{
					return CategoryAttribute.data;
				}
				object obj = CategoryAttribute.lockobj;
				lock (obj)
				{
					if (CategoryAttribute.data == null)
					{
						CategoryAttribute.data = new CategoryAttribute("Data");
					}
				}
				return CategoryAttribute.data;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Default category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the default category.</returns>
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0001AD80 File Offset: 0x00018F80
		public static CategoryAttribute Default
		{
			get
			{
				if (CategoryAttribute.def != null)
				{
					return CategoryAttribute.def;
				}
				object obj = CategoryAttribute.lockobj;
				lock (obj)
				{
					if (CategoryAttribute.def == null)
					{
						CategoryAttribute.def = new CategoryAttribute();
					}
				}
				return CategoryAttribute.def;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Design category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the design category.</returns>
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x0001ADF4 File Offset: 0x00018FF4
		public static CategoryAttribute Design
		{
			get
			{
				if (CategoryAttribute.design != null)
				{
					return CategoryAttribute.design;
				}
				object obj = CategoryAttribute.lockobj;
				lock (obj)
				{
					if (CategoryAttribute.design == null)
					{
						CategoryAttribute.design = new CategoryAttribute("Design");
					}
				}
				return CategoryAttribute.design;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the DragDrop category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the drag-and-drop category.</returns>
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0001AE70 File Offset: 0x00019070
		public static CategoryAttribute DragDrop
		{
			get
			{
				if (CategoryAttribute.drag_drop != null)
				{
					return CategoryAttribute.drag_drop;
				}
				object obj = CategoryAttribute.lockobj;
				lock (obj)
				{
					if (CategoryAttribute.drag_drop == null)
					{
						CategoryAttribute.drag_drop = new CategoryAttribute("Drag Drop");
					}
				}
				return CategoryAttribute.drag_drop;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Focus category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the focus category.</returns>
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0001AEEC File Offset: 0x000190EC
		public static CategoryAttribute Focus
		{
			get
			{
				if (CategoryAttribute.focus != null)
				{
					return CategoryAttribute.focus;
				}
				object obj = CategoryAttribute.lockobj;
				lock (obj)
				{
					if (CategoryAttribute.focus == null)
					{
						CategoryAttribute.focus = new CategoryAttribute("Focus");
					}
				}
				return CategoryAttribute.focus;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Format category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the format category.</returns>
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x0001AF68 File Offset: 0x00019168
		public static CategoryAttribute Format
		{
			get
			{
				if (CategoryAttribute.format != null)
				{
					return CategoryAttribute.format;
				}
				object obj = CategoryAttribute.lockobj;
				lock (obj)
				{
					if (CategoryAttribute.format == null)
					{
						CategoryAttribute.format = new CategoryAttribute("Format");
					}
				}
				return CategoryAttribute.format;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Key category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the key category.</returns>
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x0001AFE4 File Offset: 0x000191E4
		public static CategoryAttribute Key
		{
			get
			{
				if (CategoryAttribute.key != null)
				{
					return CategoryAttribute.key;
				}
				object obj = CategoryAttribute.lockobj;
				lock (obj)
				{
					if (CategoryAttribute.key == null)
					{
						CategoryAttribute.key = new CategoryAttribute("Key");
					}
				}
				return CategoryAttribute.key;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Layout category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the layout category.</returns>
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x0001B060 File Offset: 0x00019260
		public static CategoryAttribute Layout
		{
			get
			{
				if (CategoryAttribute.layout != null)
				{
					return CategoryAttribute.layout;
				}
				object obj = CategoryAttribute.lockobj;
				lock (obj)
				{
					if (CategoryAttribute.layout == null)
					{
						CategoryAttribute.layout = new CategoryAttribute("Layout");
					}
				}
				return CategoryAttribute.layout;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Mouse category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the mouse category.</returns>
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x0001B0DC File Offset: 0x000192DC
		public static CategoryAttribute Mouse
		{
			get
			{
				if (CategoryAttribute.mouse != null)
				{
					return CategoryAttribute.mouse;
				}
				object obj = CategoryAttribute.lockobj;
				lock (obj)
				{
					if (CategoryAttribute.mouse == null)
					{
						CategoryAttribute.mouse = new CategoryAttribute("Mouse");
					}
				}
				return CategoryAttribute.mouse;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the WindowStyle category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the window style category.</returns>
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0001B158 File Offset: 0x00019358
		public static CategoryAttribute WindowStyle
		{
			get
			{
				if (CategoryAttribute.window_style != null)
				{
					return CategoryAttribute.window_style;
				}
				object obj = CategoryAttribute.lockobj;
				lock (obj)
				{
					if (CategoryAttribute.window_style == null)
					{
						CategoryAttribute.window_style = new CategoryAttribute("Window Style");
					}
				}
				return CategoryAttribute.window_style;
			}
		}

		/// <summary>Looks up the localized name of the specified category.</summary>
		/// <returns>The localized name of the category, or null if a localized name does not exist.</returns>
		/// <param name="value">The identifer for the category to look up. </param>
		// Token: 0x06000948 RID: 2376 RVA: 0x0001B1D4 File Offset: 0x000193D4
		protected virtual string GetLocalizedString(string value)
		{
			return Locale.GetText(value);
		}

		/// <summary>Gets the name of the category for the property or event that this attribute is applied to.</summary>
		/// <returns>The name of the category for the property or event that this attribute is applied to.</returns>
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x0001B1DC File Offset: 0x000193DC
		public string Category
		{
			get
			{
				if (!this.IsLocalized)
				{
					this.IsLocalized = true;
					string localizedString = this.GetLocalizedString(this.category);
					if (localizedString != null)
					{
						this.category = localizedString;
					}
				}
				return this.category;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.CategoryAttribute" />..</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x0600094A RID: 2378 RVA: 0x0001B21C File Offset: 0x0001941C
		public override bool Equals(object obj)
		{
			return obj is CategoryAttribute && (obj == this || ((CategoryAttribute)obj).Category == this.category);
		}

		/// <summary>Returns the hash code for this attribute.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600094B RID: 2379 RVA: 0x0001B258 File Offset: 0x00019458
		public override int GetHashCode()
		{
			return this.category.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600094C RID: 2380 RVA: 0x0001B268 File Offset: 0x00019468
		public override bool IsDefaultAttribute()
		{
			return this.category == CategoryAttribute.Default.Category;
		}

		// Token: 0x04000268 RID: 616
		private string category;

		// Token: 0x04000269 RID: 617
		private bool IsLocalized;

		// Token: 0x0400026A RID: 618
		private static volatile CategoryAttribute action;

		// Token: 0x0400026B RID: 619
		private static volatile CategoryAttribute appearance;

		// Token: 0x0400026C RID: 620
		private static volatile CategoryAttribute behaviour;

		// Token: 0x0400026D RID: 621
		private static volatile CategoryAttribute data;

		// Token: 0x0400026E RID: 622
		private static volatile CategoryAttribute def;

		// Token: 0x0400026F RID: 623
		private static volatile CategoryAttribute design;

		// Token: 0x04000270 RID: 624
		private static volatile CategoryAttribute drag_drop;

		// Token: 0x04000271 RID: 625
		private static volatile CategoryAttribute focus;

		// Token: 0x04000272 RID: 626
		private static volatile CategoryAttribute format;

		// Token: 0x04000273 RID: 627
		private static volatile CategoryAttribute key;

		// Token: 0x04000274 RID: 628
		private static volatile CategoryAttribute layout;

		// Token: 0x04000275 RID: 629
		private static volatile CategoryAttribute mouse;

		// Token: 0x04000276 RID: 630
		private static volatile CategoryAttribute window_style;

		// Token: 0x04000277 RID: 631
		private static volatile CategoryAttribute async;

		// Token: 0x04000278 RID: 632
		private static object lockobj = new object();
	}
}
