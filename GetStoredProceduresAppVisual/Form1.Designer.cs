namespace GetStoredProceduresAppVisual;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        panel1 = new Panel();
        ProceduresComboBox = new ComboBox();
        DatabaseComboBox = new ComboBox();
        ProcedureTextBox = new RichTextBox();
        SaveButton = new Button();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // panel1
        // 
        panel1.Controls.Add(SaveButton);
        panel1.Controls.Add(ProceduresComboBox);
        panel1.Controls.Add(DatabaseComboBox);
        panel1.Dock = DockStyle.Top;
        panel1.Location = new Point(0, 0);
        panel1.Name = "panel1";
        panel1.Size = new Size(873, 65);
        panel1.TabIndex = 0;
        // 
        // ProceduresComboBox
        // 
        ProceduresComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        ProceduresComboBox.FormattingEnabled = true;
        ProceduresComboBox.Location = new Point(276, 12);
        ProceduresComboBox.Name = "ProceduresComboBox";
        ProceduresComboBox.Size = new Size(412, 28);
        ProceduresComboBox.TabIndex = 1;
        // 
        // DatabaseComboBox
        // 
        DatabaseComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        DatabaseComboBox.FormattingEnabled = true;
        DatabaseComboBox.Location = new Point(28, 12);
        DatabaseComboBox.Name = "DatabaseComboBox";
        DatabaseComboBox.Size = new Size(231, 28);
        DatabaseComboBox.TabIndex = 0;
        // 
        // ProcedureTextBox
        // 
        ProcedureTextBox.Dock = DockStyle.Fill;
        ProcedureTextBox.Location = new Point(0, 65);
        ProcedureTextBox.Name = "ProcedureTextBox";
        ProcedureTextBox.Size = new Size(873, 626);
        ProcedureTextBox.TabIndex = 2;
        ProcedureTextBox.Text = "";
        // 
        // SaveButton
        // 
        SaveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        SaveButton.Location = new Point(724, 12);
        SaveButton.Name = "SaveButton";
        SaveButton.Size = new Size(137, 29);
        SaveButton.TabIndex = 3;
        SaveButton.Text = "Save";
        SaveButton.UseVisualStyleBackColor = true;
        SaveButton.Click += SaveButton_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(873, 691);
        Controls.Add(ProcedureTextBox);
        Controls.Add(panel1);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Stored procedure viewer";
        Load += Form1_Load;
        panel1.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Panel panel1;
    private ComboBox DatabaseComboBox;
    private ComboBox ProceduresComboBox;
    private RichTextBox ProcedureTextBox;
    private Button SaveButton;
}
