using System;

namespace System.ComponentModel.Design
{
	/// <summary>Defines identifiers for the standard set of commands that are available to most applications.</summary>
	// Token: 0x0200013B RID: 315
	public class StandardCommands
	{
		// Token: 0x06000BC3 RID: 3011 RVA: 0x0001EB20 File Offset: 0x0001CD20
		static StandardCommands()
		{
			Guid menuGroup = new Guid("5efc7975-14bc-11cf-9b2b-00aa00573819");
			Guid menuGroup2 = new Guid("74d21313-2aee-11d1-8bfb-00a0c90f26f7");
			StandardCommands.AlignBottom = new CommandID(menuGroup, 1);
			StandardCommands.AlignHorizontalCenters = new CommandID(menuGroup, 2);
			StandardCommands.AlignLeft = new CommandID(menuGroup, 3);
			StandardCommands.AlignRight = new CommandID(menuGroup, 4);
			StandardCommands.AlignToGrid = new CommandID(menuGroup, 5);
			StandardCommands.AlignTop = new CommandID(menuGroup, 6);
			StandardCommands.AlignVerticalCenters = new CommandID(menuGroup, 7);
			StandardCommands.ArrangeBottom = new CommandID(menuGroup, 8);
			StandardCommands.ArrangeIcons = new CommandID(menuGroup2, 12298);
			StandardCommands.ArrangeRight = new CommandID(menuGroup, 9);
			StandardCommands.BringForward = new CommandID(menuGroup, 10);
			StandardCommands.BringToFront = new CommandID(menuGroup, 11);
			StandardCommands.CenterHorizontally = new CommandID(menuGroup, 12);
			StandardCommands.CenterVertically = new CommandID(menuGroup, 13);
			StandardCommands.Copy = new CommandID(menuGroup, 15);
			StandardCommands.Cut = new CommandID(menuGroup, 16);
			StandardCommands.Delete = new CommandID(menuGroup, 17);
			StandardCommands.F1Help = new CommandID(menuGroup, 377);
			StandardCommands.Group = new CommandID(menuGroup, 20);
			StandardCommands.HorizSpaceConcatenate = new CommandID(menuGroup, 21);
			StandardCommands.HorizSpaceDecrease = new CommandID(menuGroup, 22);
			StandardCommands.HorizSpaceIncrease = new CommandID(menuGroup, 23);
			StandardCommands.HorizSpaceMakeEqual = new CommandID(menuGroup, 24);
			StandardCommands.LineupIcons = new CommandID(menuGroup2, 12299);
			StandardCommands.LockControls = new CommandID(menuGroup, 369);
			StandardCommands.MultiLevelRedo = new CommandID(menuGroup, 30);
			StandardCommands.MultiLevelUndo = new CommandID(menuGroup, 44);
			StandardCommands.Paste = new CommandID(menuGroup, 26);
			StandardCommands.Properties = new CommandID(menuGroup, 28);
			StandardCommands.PropertiesWindow = new CommandID(menuGroup, 235);
			StandardCommands.Redo = new CommandID(menuGroup, 29);
			StandardCommands.Replace = new CommandID(menuGroup, 230);
			StandardCommands.SelectAll = new CommandID(menuGroup, 31);
			StandardCommands.SendBackward = new CommandID(menuGroup, 32);
			StandardCommands.SendToBack = new CommandID(menuGroup, 33);
			StandardCommands.ShowGrid = new CommandID(menuGroup, 103);
			StandardCommands.ShowLargeIcons = new CommandID(menuGroup2, 12300);
			StandardCommands.SizeToControl = new CommandID(menuGroup, 35);
			StandardCommands.SizeToControlHeight = new CommandID(menuGroup, 36);
			StandardCommands.SizeToControlWidth = new CommandID(menuGroup, 37);
			StandardCommands.SizeToFit = new CommandID(menuGroup, 38);
			StandardCommands.SizeToGrid = new CommandID(menuGroup, 39);
			StandardCommands.SnapToGrid = new CommandID(menuGroup, 40);
			StandardCommands.TabOrder = new CommandID(menuGroup, 41);
			StandardCommands.Undo = new CommandID(menuGroup, 43);
			StandardCommands.Ungroup = new CommandID(menuGroup, 45);
			StandardCommands.VerbFirst = new CommandID(menuGroup2, 8192);
			StandardCommands.VerbLast = new CommandID(menuGroup2, 8448);
			StandardCommands.VertSpaceConcatenate = new CommandID(menuGroup, 46);
			StandardCommands.VertSpaceDecrease = new CommandID(menuGroup, 47);
			StandardCommands.VertSpaceIncrease = new CommandID(menuGroup, 48);
			StandardCommands.VertSpaceMakeEqual = new CommandID(menuGroup, 49);
			StandardCommands.ViewGrid = new CommandID(menuGroup, 125);
			StandardCommands.DocumentOutline = new CommandID(menuGroup, 239);
			StandardCommands.ViewCode = new CommandID(menuGroup, 333);
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignBottom command. This field is read-only.</summary>
		// Token: 0x04000313 RID: 787
		public static readonly CommandID AlignBottom;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignHorizontalCenters command. This field is read-only.</summary>
		// Token: 0x04000314 RID: 788
		public static readonly CommandID AlignHorizontalCenters;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignLeft command. This field is read-only.</summary>
		// Token: 0x04000315 RID: 789
		public static readonly CommandID AlignLeft;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignRight command. This field is read-only.</summary>
		// Token: 0x04000316 RID: 790
		public static readonly CommandID AlignRight;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignToGrid command. This field is read-only.</summary>
		// Token: 0x04000317 RID: 791
		public static readonly CommandID AlignToGrid;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignTop command. This field is read-only.</summary>
		// Token: 0x04000318 RID: 792
		public static readonly CommandID AlignTop;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignVerticalCenters command. This field is read-only.</summary>
		// Token: 0x04000319 RID: 793
		public static readonly CommandID AlignVerticalCenters;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ArrangeBottom command. This field is read-only.</summary>
		// Token: 0x0400031A RID: 794
		public static readonly CommandID ArrangeBottom;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ArrangeIcons command. This field is read-only.</summary>
		// Token: 0x0400031B RID: 795
		public static readonly CommandID ArrangeIcons;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ArrangeRight command. This field is read-only.</summary>
		// Token: 0x0400031C RID: 796
		public static readonly CommandID ArrangeRight;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the BringForward command. This field is read-only.</summary>
		// Token: 0x0400031D RID: 797
		public static readonly CommandID BringForward;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the BringToFront command. This field is read-only.</summary>
		// Token: 0x0400031E RID: 798
		public static readonly CommandID BringToFront;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the CenterHorizontally command. This field is read-only.</summary>
		// Token: 0x0400031F RID: 799
		public static readonly CommandID CenterHorizontally;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the CenterVertically command. This field is read-only.</summary>
		// Token: 0x04000320 RID: 800
		public static readonly CommandID CenterVertically;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Copy command. This field is read-only.</summary>
		// Token: 0x04000321 RID: 801
		public static readonly CommandID Copy;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Cut command. This field is read-only.</summary>
		// Token: 0x04000322 RID: 802
		public static readonly CommandID Cut;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Delete command. This field is read-only.</summary>
		// Token: 0x04000323 RID: 803
		public static readonly CommandID Delete;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the F1Help command. This field is read-only.</summary>
		// Token: 0x04000324 RID: 804
		public static readonly CommandID F1Help;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Group command. This field is read-only.</summary>
		// Token: 0x04000325 RID: 805
		public static readonly CommandID Group;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceConcatenate command. This field is read-only.</summary>
		// Token: 0x04000326 RID: 806
		public static readonly CommandID HorizSpaceConcatenate;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceDecrease command. This field is read-only.</summary>
		// Token: 0x04000327 RID: 807
		public static readonly CommandID HorizSpaceDecrease;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceIncrease command. This field is read-only.</summary>
		// Token: 0x04000328 RID: 808
		public static readonly CommandID HorizSpaceIncrease;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceMakeEqual command. This field is read-only.</summary>
		// Token: 0x04000329 RID: 809
		public static readonly CommandID HorizSpaceMakeEqual;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the LineupIcons command. This field is read-only.</summary>
		// Token: 0x0400032A RID: 810
		public static readonly CommandID LineupIcons;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the LockControls command. This field is read-only.</summary>
		// Token: 0x0400032B RID: 811
		public static readonly CommandID LockControls;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the MultiLevelRedo command. This field is read-only.</summary>
		// Token: 0x0400032C RID: 812
		public static readonly CommandID MultiLevelRedo;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the MultiLevelUndo command. This field is read-only.</summary>
		// Token: 0x0400032D RID: 813
		public static readonly CommandID MultiLevelUndo;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Paste command. This field is read-only.</summary>
		// Token: 0x0400032E RID: 814
		public static readonly CommandID Paste;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Properties command. This field is read-only.</summary>
		// Token: 0x0400032F RID: 815
		public static readonly CommandID Properties;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the PropertiesWindow command. This field is read-only.</summary>
		// Token: 0x04000330 RID: 816
		public static readonly CommandID PropertiesWindow;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Redo command. This field is read-only.</summary>
		// Token: 0x04000331 RID: 817
		public static readonly CommandID Redo;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Replace command. This field is read-only.</summary>
		// Token: 0x04000332 RID: 818
		public static readonly CommandID Replace;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SelectAll command. This field is read-only.</summary>
		// Token: 0x04000333 RID: 819
		public static readonly CommandID SelectAll;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SendBackward command. This field is read-only.</summary>
		// Token: 0x04000334 RID: 820
		public static readonly CommandID SendBackward;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SendToBack command. This field is read-only.</summary>
		// Token: 0x04000335 RID: 821
		public static readonly CommandID SendToBack;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ShowGrid command. This field is read-only.</summary>
		// Token: 0x04000336 RID: 822
		public static readonly CommandID ShowGrid;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ShowLargeIcons command. This field is read-only.</summary>
		// Token: 0x04000337 RID: 823
		public static readonly CommandID ShowLargeIcons;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToControl command. This field is read-only.</summary>
		// Token: 0x04000338 RID: 824
		public static readonly CommandID SizeToControl;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToControlHeight command. This field is read-only.</summary>
		// Token: 0x04000339 RID: 825
		public static readonly CommandID SizeToControlHeight;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToControlWidth command. This field is read-only.</summary>
		// Token: 0x0400033A RID: 826
		public static readonly CommandID SizeToControlWidth;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToFit command. This field is read-only.</summary>
		// Token: 0x0400033B RID: 827
		public static readonly CommandID SizeToFit;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToGrid command. This field is read-only.</summary>
		// Token: 0x0400033C RID: 828
		public static readonly CommandID SizeToGrid;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SnapToGrid command. This field is read-only.</summary>
		// Token: 0x0400033D RID: 829
		public static readonly CommandID SnapToGrid;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the TabOrder command. This field is read-only.</summary>
		// Token: 0x0400033E RID: 830
		public static readonly CommandID TabOrder;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Undo command. This field is read-only.</summary>
		// Token: 0x0400033F RID: 831
		public static readonly CommandID Undo;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Ungroup command. This field is read-only.</summary>
		// Token: 0x04000340 RID: 832
		public static readonly CommandID Ungroup;

		/// <summary>Gets the first of a set of verbs. This field is read-only.</summary>
		// Token: 0x04000341 RID: 833
		public static readonly CommandID VerbFirst;

		/// <summary>Gets the last of a set of verbs. This field is read-only.</summary>
		// Token: 0x04000342 RID: 834
		public static readonly CommandID VerbLast;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceConcatenate command. This field is read-only.</summary>
		// Token: 0x04000343 RID: 835
		public static readonly CommandID VertSpaceConcatenate;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceDecrease command. This field is read-only.</summary>
		// Token: 0x04000344 RID: 836
		public static readonly CommandID VertSpaceDecrease;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceIncrease command. This field is read-only.</summary>
		// Token: 0x04000345 RID: 837
		public static readonly CommandID VertSpaceIncrease;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceMakeEqual command. This field is read-only.</summary>
		// Token: 0x04000346 RID: 838
		public static readonly CommandID VertSpaceMakeEqual;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ViewGrid command. This field is read-only.</summary>
		// Token: 0x04000347 RID: 839
		public static readonly CommandID ViewGrid;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Document Outline command. This field is read-only.</summary>
		// Token: 0x04000348 RID: 840
		public static readonly CommandID DocumentOutline;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ViewCode command. This field is read-only.</summary>
		// Token: 0x04000349 RID: 841
		public static readonly CommandID ViewCode;
	}
}
