using System.Drawing;

namespace Renderers
{
	/// <summary>
	/// Provides colors used by WindowsVista style rendering
	/// </summary>
	/// <remarks>
	/// 2007 José Manuel Menéndez Poo
	/// Visit my blog for upgrades and other renderers - www.menendezpoo.com
	/// </remarks>
	public class WindowsVistaColorTable
	{
		#region Fields

		#endregion

		#region Ctor

		public WindowsVistaColorTable()
		{
			BackgroundNorth = Color.Black;
			BackgroundSouth = Color.Black;

			GlossyEffectNorth = Color.FromArgb(alpha: 217, red: 0x68, green: 0x7C, blue: 0xAC);
			GlossyEffectSouth = Color.FromArgb(alpha: 74, red: 0xAA, green: 0xB5, blue: 0xD0);

			BackgroundBorder = Color.FromArgb(red: 0x85, green: 0x85, blue: 0x87);
			BackgroundGlow = Color.FromArgb(red: 0x43, green: 0x53, blue: 0x7A);

			Text = Color.White;

			ButtonOuterBorder = Color.FromArgb(red: 0x75, green: 0x7D, blue: 0x95);
			ButtonInnerBorder = Color.FromArgb(red: 0xBF, green: 0xC4, blue: 0xCE);
			ButtonInnerBorderPressed = Color.FromArgb(red: 0x4b, green: 0x4b, blue: 0x4b);
			ButtonBorder = Color.FromArgb(red: 0x03, green: 0x07, blue: 0x0D);
			ButtonFillNorth = Color.FromArgb(alpha: 85, baseColor: Color.White);
			ButtonFillSouth = Color.FromArgb(alpha: 1, baseColor: Color.White);
			ButtonFillNorthPressed = Color.FromArgb(alpha: 150, baseColor: Color.Black);
			ButtonFillSouthPressed = Color.FromArgb(alpha: 100, baseColor: Color.Black);

			Glow = Color.FromArgb(red: 0x30, green: 0x73, blue: 0xCE);
			DropDownArrow = Color.White;

			MenuHighlight = Color.FromArgb(red: 0xA8, green: 0xD8, blue: 0xEB);
			MenuHighlightNorth = Color.FromArgb(alpha: 25, baseColor: MenuHighlight);
			MenuHighlightSouth = Color.FromArgb(alpha: 102, baseColor: MenuHighlight);
			MenuBackground = Color.FromArgb(red: 0xF1, green: 0xF1, blue: 0xF1);
			MenuDark = Color.FromArgb(red: 0xE2, green: 0xE3, blue: 0xE3);
			MenuLight = Color.White;

			SeparatorNorth = BackgroundSouth;
			SeparatorSouth = GlossyEffectNorth;

			MenuText = Color.Black;

			CheckedGlow = Color.FromArgb(red: 0x57, green: 0xC6, blue: 0xEF);
			CheckedGlowHot = Color.FromArgb(red: 0x70, green: 0xD4, blue: 0xFF);
			CheckedButtonFill = Color.FromArgb(red: 0x18, green: 0x38, blue: 0x9E);
			CheckedButtonFillHot = Color.FromArgb(red: 0x0F, green: 0x3A, blue: 0xBF);
		}

		#endregion

		#region Properties

		public Color CheckedGlowHot { get; set; }

		public Color CheckedButtonFillHot { get; set; }

		public Color CheckedButtonFill { get; set; }

		public Color CheckedGlow { get; set; }

		public Color MenuText { get; set; }

		public Color SeparatorNorth { get; set; }

		public Color SeparatorSouth { get; set; }

		public Color MenuLight { get; set; }

		public Color MenuDark { get; set; }

		public Color MenuBackground { get; set; }

		public Color MenuHighlightSouth { get; set; }

		public Color MenuHighlightNorth { get; set; }

		public Color MenuHighlight { get; set; }

		/// <summary>
		/// Gets or sets the color for the dropwown arrow
		/// </summary>
		public Color DropDownArrow { get; set; }

		/// <summary>
		/// Gets or sets the south color of the button fill when pressed
		/// </summary>
		public Color ButtonFillSouthPressed { get; set; }

		/// <summary>
		/// Gets or sets the south color of the button fill
		/// </summary>
		public Color ButtonFillSouth { get; set; }

		/// <summary>
		/// Gets or sets the color of the inner border when pressed
		/// </summary>
		public Color ButtonInnerBorderPressed { get; set; }

		/// <summary>
		/// Gets or sets the glow color
		/// </summary>
		public Color Glow { get; set; }

		/// <summary>
		/// Gets or sets the buttons fill color
		/// </summary>
		public Color ButtonFillNorth { get; set; }

		/// <summary>
		/// Gets or sets the buttons fill color when pressed
		/// </summary>
		public Color ButtonFillNorthPressed { get; set; }

		/// <summary>
		/// Gets or sets the buttons inner border color
		/// </summary>
		public Color ButtonInnerBorder { get; set; }

		/// <summary>
		/// Gets or sets the buttons border color
		/// </summary>
		public Color ButtonBorder { get; set; }

		/// <summary>
		/// Gets or sets the buttons outer border color
		/// </summary>
		public Color ButtonOuterBorder { get; set; }

		/// <summary>
		/// Gets or sets the color of the text
		/// </summary>
		public Color Text { get; set; }

		/// <summary>
		/// Gets or sets the background glow color
		/// </summary>
		public Color BackgroundGlow { get; set; }

		/// <summary>
		/// Gets or sets the color of the background border
		/// </summary>
		public Color BackgroundBorder { get; set; }

		/// <summary>
		/// Background north part
		/// </summary>
		public Color BackgroundNorth { get; set; }

		/// <summary>
		/// Background south color
		/// </summary>
		public Color BackgroundSouth { get; set; }

		/// <summary>
		/// Gets ors sets the glossy effect north color
		/// </summary>
		public Color GlossyEffectNorth { get; set; }

		/// <summary>
		/// Gets or sets the glossy effect south color
		/// </summary>
		public Color GlossyEffectSouth { get; set; }

		#endregion
	}
}
