using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Starcreate
{
  public partial class StarcreateForm : Form
  {
    public StarcreateForm()
    {
      InitializeComponent();
    }

    private void StarcreateForm_Load(object sender, EventArgs e)
    {
      toolStripContainerMain.ContentPanel.BackColor = Color.Black;
    }

    private void StarcreateForm_Shown(object sender, EventArgs e)
    {
      ToolStripManager.Renderer = new Renderers.WindowsVistaRenderer();
    }

    private void StarcreateForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      this.Dispose();
    }

    private void buttonCreateSystem_Click(object sender, EventArgs e)
    {
      CreateSystemForm formCreateSystem = new CreateSystemForm();
      formCreateSystem.Show();
    }
  }
}
