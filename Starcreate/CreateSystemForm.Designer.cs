namespace Starcreate
{
  partial class CreateSystemForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateSystemForm));
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.labelEccentricityCoeff = new System.Windows.Forms.Label();
      this.numericUpDownEccentricityCoeff = new System.Windows.Forms.NumericUpDown();
      this.buttonCreate = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEccentricityCoeff)).BeginInit();
      this.SuspendLayout();
      // 
      // labelEccentricityCoeff
      // 
      this.labelEccentricityCoeff.AutoSize = true;
      this.labelEccentricityCoeff.Location = new System.Drawing.Point(13, 13);
      this.labelEccentricityCoeff.Name = "labelEccentricityCoeff";
      this.labelEccentricityCoeff.Size = new System.Drawing.Size(125, 13);
      this.labelEccentricityCoeff.TabIndex = 0;
      this.labelEccentricityCoeff.Text = "ECCENTRICITY_COEFF";
      // 
      // numericUpDownEccentricityCoeff
      // 
      this.numericUpDownEccentricityCoeff.DecimalPlaces = 8;
      this.numericUpDownEccentricityCoeff.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
      this.numericUpDownEccentricityCoeff.Location = new System.Drawing.Point(144, 11);
      this.numericUpDownEccentricityCoeff.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.numericUpDownEccentricityCoeff.Name = "numericUpDownEccentricityCoeff";
      this.numericUpDownEccentricityCoeff.Size = new System.Drawing.Size(89, 20);
      this.numericUpDownEccentricityCoeff.TabIndex = 1;
      this.numericUpDownEccentricityCoeff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.numericUpDownEccentricityCoeff.Value = new decimal(new int[] {
            77,
            0,
            0,
            196608});
      // 
      // buttonCreate
      // 
      this.buttonCreate.Location = new System.Drawing.Point(249, 8);
      this.buttonCreate.Name = "buttonCreate";
      this.buttonCreate.Size = new System.Drawing.Size(75, 23);
      this.buttonCreate.TabIndex = 2;
      this.buttonCreate.Text = "Erstellen";
      this.buttonCreate.UseVisualStyleBackColor = true;
      this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
      // 
      // CreateSystemForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.ClientSize = new System.Drawing.Size(508, 430);
      this.Controls.Add(this.buttonCreate);
      this.Controls.Add(this.numericUpDownEccentricityCoeff);
      this.Controls.Add(this.labelEccentricityCoeff);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "CreateSystemForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "System erstellen";
      this.Load += new System.EventHandler(this.CreateSystemForm_Load);
      this.Shown += new System.EventHandler(this.CreateSystemForm_Shown);
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEccentricityCoeff)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolTip toolTip;
    private System.Windows.Forms.Label labelEccentricityCoeff;
    private System.Windows.Forms.NumericUpDown numericUpDownEccentricityCoeff;
    private System.Windows.Forms.Button buttonCreate;
  }
}