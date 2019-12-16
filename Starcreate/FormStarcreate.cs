using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Starcreate
{
	public partial class StarcreateForm : Form
	{
		private static void SetDoubleBuffered(Control control)
		{
			if (SystemInformation.TerminalServerSession)
			{
				return;
			}
			PropertyInfo aProp = typeof(Control).GetProperty(name: "DoubleBuffered", bindingAttr: BindingFlags.NonPublic | BindingFlags.Instance);
			aProp.SetValue(obj: control, value: true, index: null);
		}

		public StarcreateForm()
		{
			InitializeComponent();
		}

		private void StarcreateForm_Load(object sender, EventArgs e)
		{
			//SetDoubleBuffered(control: ToolStripContentPanel);
			toolStripContainerMain.ContentPanel.BackColor = Color.Black;
		}

		private void StarcreateForm_Shown(object sender, EventArgs e)
		{
			ToolStripManager.Renderer = new Renderers.WindowsVistaRenderer();
		}

		private void StarcreateForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Dispose();
		}

		private void ButtonCreateSystem_Click(object sender, EventArgs e)
		{
			using (CreateSystemForm formCreateSystem = new CreateSystemForm())
			{
				formCreateSystem.Show();
			}
		}
	}
}
