using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000209 RID: 521
	[ExecuteInEditMode]
	[RequiredByNativeCode]
	[Serializable]
	public sealed class GUISkin : ScriptableObject
	{
		// Token: 0x06001FEA RID: 8170 RVA: 0x00024DBC File Offset: 0x00022FBC
		public GUISkin()
		{
			this.m_CustomStyles = new GUIStyle[1];
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x00024DDC File Offset: 0x00022FDC
		internal void OnEnable()
		{
			this.Apply();
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06001FEC RID: 8172 RVA: 0x00024DE4 File Offset: 0x00022FE4
		// (set) Token: 0x06001FED RID: 8173 RVA: 0x00024DEC File Offset: 0x00022FEC
		public Font font
		{
			get
			{
				return this.m_Font;
			}
			set
			{
				this.m_Font = value;
				if (GUISkin.current == this)
				{
					GUIStyle.SetDefaultFont(this.m_Font);
				}
				this.Apply();
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001FEE RID: 8174 RVA: 0x00024E24 File Offset: 0x00023024
		// (set) Token: 0x06001FEF RID: 8175 RVA: 0x00024E2C File Offset: 0x0002302C
		public GUIStyle box
		{
			get
			{
				return this.m_box;
			}
			set
			{
				this.m_box = value;
				this.Apply();
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x00024E3C File Offset: 0x0002303C
		// (set) Token: 0x06001FF1 RID: 8177 RVA: 0x00024E44 File Offset: 0x00023044
		public GUIStyle label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
				this.Apply();
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x00024E54 File Offset: 0x00023054
		// (set) Token: 0x06001FF3 RID: 8179 RVA: 0x00024E5C File Offset: 0x0002305C
		public GUIStyle textField
		{
			get
			{
				return this.m_textField;
			}
			set
			{
				this.m_textField = value;
				this.Apply();
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x00024E6C File Offset: 0x0002306C
		// (set) Token: 0x06001FF5 RID: 8181 RVA: 0x00024E74 File Offset: 0x00023074
		public GUIStyle textArea
		{
			get
			{
				return this.m_textArea;
			}
			set
			{
				this.m_textArea = value;
				this.Apply();
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x00024E84 File Offset: 0x00023084
		// (set) Token: 0x06001FF7 RID: 8183 RVA: 0x00024E8C File Offset: 0x0002308C
		public GUIStyle button
		{
			get
			{
				return this.m_button;
			}
			set
			{
				this.m_button = value;
				this.Apply();
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06001FF8 RID: 8184 RVA: 0x00024E9C File Offset: 0x0002309C
		// (set) Token: 0x06001FF9 RID: 8185 RVA: 0x00024EA4 File Offset: 0x000230A4
		public GUIStyle toggle
		{
			get
			{
				return this.m_toggle;
			}
			set
			{
				this.m_toggle = value;
				this.Apply();
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001FFA RID: 8186 RVA: 0x00024EB4 File Offset: 0x000230B4
		// (set) Token: 0x06001FFB RID: 8187 RVA: 0x00024EBC File Offset: 0x000230BC
		public GUIStyle window
		{
			get
			{
				return this.m_window;
			}
			set
			{
				this.m_window = value;
				this.Apply();
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001FFC RID: 8188 RVA: 0x00024ECC File Offset: 0x000230CC
		// (set) Token: 0x06001FFD RID: 8189 RVA: 0x00024ED4 File Offset: 0x000230D4
		public GUIStyle horizontalSlider
		{
			get
			{
				return this.m_horizontalSlider;
			}
			set
			{
				this.m_horizontalSlider = value;
				this.Apply();
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06001FFE RID: 8190 RVA: 0x00024EE4 File Offset: 0x000230E4
		// (set) Token: 0x06001FFF RID: 8191 RVA: 0x00024EEC File Offset: 0x000230EC
		public GUIStyle horizontalSliderThumb
		{
			get
			{
				return this.m_horizontalSliderThumb;
			}
			set
			{
				this.m_horizontalSliderThumb = value;
				this.Apply();
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06002000 RID: 8192 RVA: 0x00024EFC File Offset: 0x000230FC
		// (set) Token: 0x06002001 RID: 8193 RVA: 0x00024F04 File Offset: 0x00023104
		public GUIStyle verticalSlider
		{
			get
			{
				return this.m_verticalSlider;
			}
			set
			{
				this.m_verticalSlider = value;
				this.Apply();
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06002002 RID: 8194 RVA: 0x00024F14 File Offset: 0x00023114
		// (set) Token: 0x06002003 RID: 8195 RVA: 0x00024F1C File Offset: 0x0002311C
		public GUIStyle verticalSliderThumb
		{
			get
			{
				return this.m_verticalSliderThumb;
			}
			set
			{
				this.m_verticalSliderThumb = value;
				this.Apply();
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06002004 RID: 8196 RVA: 0x00024F2C File Offset: 0x0002312C
		// (set) Token: 0x06002005 RID: 8197 RVA: 0x00024F34 File Offset: 0x00023134
		public GUIStyle horizontalScrollbar
		{
			get
			{
				return this.m_horizontalScrollbar;
			}
			set
			{
				this.m_horizontalScrollbar = value;
				this.Apply();
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06002006 RID: 8198 RVA: 0x00024F44 File Offset: 0x00023144
		// (set) Token: 0x06002007 RID: 8199 RVA: 0x00024F4C File Offset: 0x0002314C
		public GUIStyle horizontalScrollbarThumb
		{
			get
			{
				return this.m_horizontalScrollbarThumb;
			}
			set
			{
				this.m_horizontalScrollbarThumb = value;
				this.Apply();
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06002008 RID: 8200 RVA: 0x00024F5C File Offset: 0x0002315C
		// (set) Token: 0x06002009 RID: 8201 RVA: 0x00024F64 File Offset: 0x00023164
		public GUIStyle horizontalScrollbarLeftButton
		{
			get
			{
				return this.m_horizontalScrollbarLeftButton;
			}
			set
			{
				this.m_horizontalScrollbarLeftButton = value;
				this.Apply();
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x0600200A RID: 8202 RVA: 0x00024F74 File Offset: 0x00023174
		// (set) Token: 0x0600200B RID: 8203 RVA: 0x00024F7C File Offset: 0x0002317C
		public GUIStyle horizontalScrollbarRightButton
		{
			get
			{
				return this.m_horizontalScrollbarRightButton;
			}
			set
			{
				this.m_horizontalScrollbarRightButton = value;
				this.Apply();
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x0600200C RID: 8204 RVA: 0x00024F8C File Offset: 0x0002318C
		// (set) Token: 0x0600200D RID: 8205 RVA: 0x00024F94 File Offset: 0x00023194
		public GUIStyle verticalScrollbar
		{
			get
			{
				return this.m_verticalScrollbar;
			}
			set
			{
				this.m_verticalScrollbar = value;
				this.Apply();
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x0600200E RID: 8206 RVA: 0x00024FA4 File Offset: 0x000231A4
		// (set) Token: 0x0600200F RID: 8207 RVA: 0x00024FAC File Offset: 0x000231AC
		public GUIStyle verticalScrollbarThumb
		{
			get
			{
				return this.m_verticalScrollbarThumb;
			}
			set
			{
				this.m_verticalScrollbarThumb = value;
				this.Apply();
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06002010 RID: 8208 RVA: 0x00024FBC File Offset: 0x000231BC
		// (set) Token: 0x06002011 RID: 8209 RVA: 0x00024FC4 File Offset: 0x000231C4
		public GUIStyle verticalScrollbarUpButton
		{
			get
			{
				return this.m_verticalScrollbarUpButton;
			}
			set
			{
				this.m_verticalScrollbarUpButton = value;
				this.Apply();
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06002012 RID: 8210 RVA: 0x00024FD4 File Offset: 0x000231D4
		// (set) Token: 0x06002013 RID: 8211 RVA: 0x00024FDC File Offset: 0x000231DC
		public GUIStyle verticalScrollbarDownButton
		{
			get
			{
				return this.m_verticalScrollbarDownButton;
			}
			set
			{
				this.m_verticalScrollbarDownButton = value;
				this.Apply();
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06002014 RID: 8212 RVA: 0x00024FEC File Offset: 0x000231EC
		// (set) Token: 0x06002015 RID: 8213 RVA: 0x00024FF4 File Offset: 0x000231F4
		public GUIStyle scrollView
		{
			get
			{
				return this.m_ScrollView;
			}
			set
			{
				this.m_ScrollView = value;
				this.Apply();
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06002016 RID: 8214 RVA: 0x00025004 File Offset: 0x00023204
		// (set) Token: 0x06002017 RID: 8215 RVA: 0x0002500C File Offset: 0x0002320C
		public GUIStyle[] customStyles
		{
			get
			{
				return this.m_CustomStyles;
			}
			set
			{
				this.m_CustomStyles = value;
				this.Apply();
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06002018 RID: 8216 RVA: 0x0002501C File Offset: 0x0002321C
		public GUISettings settings
		{
			get
			{
				return this.m_Settings;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x00025024 File Offset: 0x00023224
		internal static GUIStyle error
		{
			get
			{
				if (GUISkin.ms_Error == null)
				{
					GUISkin.ms_Error = new GUIStyle();
				}
				return GUISkin.ms_Error;
			}
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x00025040 File Offset: 0x00023240
		internal void Apply()
		{
			if (this.m_CustomStyles == null)
			{
				Debug.Log("custom styles is null");
			}
			this.BuildStyleCache();
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x00025060 File Offset: 0x00023260
		private void BuildStyleCache()
		{
			if (this.m_box == null)
			{
				this.m_box = new GUIStyle();
			}
			if (this.m_button == null)
			{
				this.m_button = new GUIStyle();
			}
			if (this.m_toggle == null)
			{
				this.m_toggle = new GUIStyle();
			}
			if (this.m_label == null)
			{
				this.m_label = new GUIStyle();
			}
			if (this.m_window == null)
			{
				this.m_window = new GUIStyle();
			}
			if (this.m_textField == null)
			{
				this.m_textField = new GUIStyle();
			}
			if (this.m_textArea == null)
			{
				this.m_textArea = new GUIStyle();
			}
			if (this.m_horizontalSlider == null)
			{
				this.m_horizontalSlider = new GUIStyle();
			}
			if (this.m_horizontalSliderThumb == null)
			{
				this.m_horizontalSliderThumb = new GUIStyle();
			}
			if (this.m_verticalSlider == null)
			{
				this.m_verticalSlider = new GUIStyle();
			}
			if (this.m_verticalSliderThumb == null)
			{
				this.m_verticalSliderThumb = new GUIStyle();
			}
			if (this.m_horizontalScrollbar == null)
			{
				this.m_horizontalScrollbar = new GUIStyle();
			}
			if (this.m_horizontalScrollbarThumb == null)
			{
				this.m_horizontalScrollbarThumb = new GUIStyle();
			}
			if (this.m_horizontalScrollbarLeftButton == null)
			{
				this.m_horizontalScrollbarLeftButton = new GUIStyle();
			}
			if (this.m_horizontalScrollbarRightButton == null)
			{
				this.m_horizontalScrollbarRightButton = new GUIStyle();
			}
			if (this.m_verticalScrollbar == null)
			{
				this.m_verticalScrollbar = new GUIStyle();
			}
			if (this.m_verticalScrollbarThumb == null)
			{
				this.m_verticalScrollbarThumb = new GUIStyle();
			}
			if (this.m_verticalScrollbarUpButton == null)
			{
				this.m_verticalScrollbarUpButton = new GUIStyle();
			}
			if (this.m_verticalScrollbarDownButton == null)
			{
				this.m_verticalScrollbarDownButton = new GUIStyle();
			}
			if (this.m_ScrollView == null)
			{
				this.m_ScrollView = new GUIStyle();
			}
			this.m_Styles = new Dictionary<string, GUIStyle>(StringComparer.OrdinalIgnoreCase);
			this.m_Styles["box"] = this.m_box;
			this.m_box.name = "box";
			this.m_Styles["button"] = this.m_button;
			this.m_button.name = "button";
			this.m_Styles["toggle"] = this.m_toggle;
			this.m_toggle.name = "toggle";
			this.m_Styles["label"] = this.m_label;
			this.m_label.name = "label";
			this.m_Styles["window"] = this.m_window;
			this.m_window.name = "window";
			this.m_Styles["textfield"] = this.m_textField;
			this.m_textField.name = "textfield";
			this.m_Styles["textarea"] = this.m_textArea;
			this.m_textArea.name = "textarea";
			this.m_Styles["horizontalslider"] = this.m_horizontalSlider;
			this.m_horizontalSlider.name = "horizontalslider";
			this.m_Styles["horizontalsliderthumb"] = this.m_horizontalSliderThumb;
			this.m_horizontalSliderThumb.name = "horizontalsliderthumb";
			this.m_Styles["verticalslider"] = this.m_verticalSlider;
			this.m_verticalSlider.name = "verticalslider";
			this.m_Styles["verticalsliderthumb"] = this.m_verticalSliderThumb;
			this.m_verticalSliderThumb.name = "verticalsliderthumb";
			this.m_Styles["horizontalscrollbar"] = this.m_horizontalScrollbar;
			this.m_horizontalScrollbar.name = "horizontalscrollbar";
			this.m_Styles["horizontalscrollbarthumb"] = this.m_horizontalScrollbarThumb;
			this.m_horizontalScrollbarThumb.name = "horizontalscrollbarthumb";
			this.m_Styles["horizontalscrollbarleftbutton"] = this.m_horizontalScrollbarLeftButton;
			this.m_horizontalScrollbarLeftButton.name = "horizontalscrollbarleftbutton";
			this.m_Styles["horizontalscrollbarrightbutton"] = this.m_horizontalScrollbarRightButton;
			this.m_horizontalScrollbarRightButton.name = "horizontalscrollbarrightbutton";
			this.m_Styles["verticalscrollbar"] = this.m_verticalScrollbar;
			this.m_verticalScrollbar.name = "verticalscrollbar";
			this.m_Styles["verticalscrollbarthumb"] = this.m_verticalScrollbarThumb;
			this.m_verticalScrollbarThumb.name = "verticalscrollbarthumb";
			this.m_Styles["verticalscrollbarupbutton"] = this.m_verticalScrollbarUpButton;
			this.m_verticalScrollbarUpButton.name = "verticalscrollbarupbutton";
			this.m_Styles["verticalscrollbardownbutton"] = this.m_verticalScrollbarDownButton;
			this.m_verticalScrollbarDownButton.name = "verticalscrollbardownbutton";
			this.m_Styles["scrollview"] = this.m_ScrollView;
			this.m_ScrollView.name = "scrollview";
			if (this.m_CustomStyles != null)
			{
				for (int i = 0; i < this.m_CustomStyles.Length; i++)
				{
					if (this.m_CustomStyles[i] != null)
					{
						this.m_Styles[this.m_CustomStyles[i].name] = this.m_CustomStyles[i];
					}
				}
			}
			GUISkin.error.stretchHeight = true;
			GUISkin.error.normal.textColor = Color.red;
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x000255A4 File Offset: 0x000237A4
		public GUIStyle GetStyle(string styleName)
		{
			GUIStyle guistyle = this.FindStyle(styleName);
			if (guistyle != null)
			{
				return guistyle;
			}
			Debug.LogWarning(string.Concat(new object[]
			{
				"Unable to find style '",
				styleName,
				"' in skin '",
				base.name,
				"' ",
				Event.current.type
			}));
			return GUISkin.error;
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x00025610 File Offset: 0x00023810
		public GUIStyle FindStyle(string styleName)
		{
			if (this == null)
			{
				Debug.LogError("GUISkin is NULL");
				return null;
			}
			if (this.m_Styles == null)
			{
				this.BuildStyleCache();
			}
			GUIStyle result;
			if (this.m_Styles.TryGetValue(styleName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x0002565C File Offset: 0x0002385C
		internal void MakeCurrent()
		{
			GUISkin.current = this;
			GUIStyle.SetDefaultFont(this.font);
			if (GUISkin.m_SkinChanged != null)
			{
				GUISkin.m_SkinChanged();
			}
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x00025684 File Offset: 0x00023884
		public IEnumerator GetEnumerator()
		{
			if (this.m_Styles == null)
			{
				this.BuildStyleCache();
			}
			return this.m_Styles.Values.GetEnumerator();
		}

		// Token: 0x040007F5 RID: 2037
		[SerializeField]
		private Font m_Font;

		// Token: 0x040007F6 RID: 2038
		[SerializeField]
		private GUIStyle m_box;

		// Token: 0x040007F7 RID: 2039
		[SerializeField]
		private GUIStyle m_button;

		// Token: 0x040007F8 RID: 2040
		[SerializeField]
		private GUIStyle m_toggle;

		// Token: 0x040007F9 RID: 2041
		[SerializeField]
		private GUIStyle m_label;

		// Token: 0x040007FA RID: 2042
		[SerializeField]
		private GUIStyle m_textField;

		// Token: 0x040007FB RID: 2043
		[SerializeField]
		private GUIStyle m_textArea;

		// Token: 0x040007FC RID: 2044
		[SerializeField]
		private GUIStyle m_window;

		// Token: 0x040007FD RID: 2045
		[SerializeField]
		private GUIStyle m_horizontalSlider;

		// Token: 0x040007FE RID: 2046
		[SerializeField]
		private GUIStyle m_horizontalSliderThumb;

		// Token: 0x040007FF RID: 2047
		[SerializeField]
		private GUIStyle m_verticalSlider;

		// Token: 0x04000800 RID: 2048
		[SerializeField]
		private GUIStyle m_verticalSliderThumb;

		// Token: 0x04000801 RID: 2049
		[SerializeField]
		private GUIStyle m_horizontalScrollbar;

		// Token: 0x04000802 RID: 2050
		[SerializeField]
		private GUIStyle m_horizontalScrollbarThumb;

		// Token: 0x04000803 RID: 2051
		[SerializeField]
		private GUIStyle m_horizontalScrollbarLeftButton;

		// Token: 0x04000804 RID: 2052
		[SerializeField]
		private GUIStyle m_horizontalScrollbarRightButton;

		// Token: 0x04000805 RID: 2053
		[SerializeField]
		private GUIStyle m_verticalScrollbar;

		// Token: 0x04000806 RID: 2054
		[SerializeField]
		private GUIStyle m_verticalScrollbarThumb;

		// Token: 0x04000807 RID: 2055
		[SerializeField]
		private GUIStyle m_verticalScrollbarUpButton;

		// Token: 0x04000808 RID: 2056
		[SerializeField]
		private GUIStyle m_verticalScrollbarDownButton;

		// Token: 0x04000809 RID: 2057
		[SerializeField]
		private GUIStyle m_ScrollView;

		// Token: 0x0400080A RID: 2058
		[SerializeField]
		internal GUIStyle[] m_CustomStyles;

		// Token: 0x0400080B RID: 2059
		[SerializeField]
		private GUISettings m_Settings = new GUISettings();

		// Token: 0x0400080C RID: 2060
		internal static GUIStyle ms_Error;

		// Token: 0x0400080D RID: 2061
		private Dictionary<string, GUIStyle> m_Styles;

		// Token: 0x0400080E RID: 2062
		internal static GUISkin.SkinChangedDelegate m_SkinChanged;

		// Token: 0x0400080F RID: 2063
		internal static GUISkin current;

		// Token: 0x0200034B RID: 843
		// (Invoke) Token: 0x06002886 RID: 10374
		internal delegate void SkinChangedDelegate();
	}
}
