namespace DXSample {
    partial class Main {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.customersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nwindDataSet = new DXSample.nwindDataSet();
            this.customersTableAdapter = new DXSample.nwindDataSetTableAdapters.CustomersTableAdapter();
            this.customSearchLookUpEdit1 = new DXSample.CustomSearchLookUpEdit();
            this.customSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.customersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nwindDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customSearchLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customSearchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "DevExpress Dark Style";
            // 
            // customersBindingSource
            // 
            this.customersBindingSource.DataMember = "Customers";
            this.customersBindingSource.DataSource = this.nwindDataSet;
            // 
            // nwindDataSet
            // 
            this.nwindDataSet.DataSetName = "nwindDataSet";
            this.nwindDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // customersTableAdapter
            // 
            this.customersTableAdapter.ClearBeforeFill = true;
            // 
            // customSearchLookUpEdit1
            // 
            this.customSearchLookUpEdit1.Location = new System.Drawing.Point(105, 99);
            this.customSearchLookUpEdit1.Name = "customSearchLookUpEdit1";
            this.customSearchLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.customSearchLookUpEdit1.Properties.DataSource = this.customersBindingSource;
            this.customSearchLookUpEdit1.Properties.DisplayMember = "ContactName";
            this.customSearchLookUpEdit1.Properties.ValueMember = "CustomerID";
            this.customSearchLookUpEdit1.Properties.View = this.customSearchLookUpEdit1View;
            this.customSearchLookUpEdit1.Size = new System.Drawing.Size(290, 20);
            this.customSearchLookUpEdit1.TabIndex = 0;
            this.customSearchLookUpEdit1.UpdateDisplayFilter += new DXSample.UpdateDisplayFilterHandler(this.OnUpdateDisplayFilter);
            // 
            // customSearchLookUpEdit1View
            // 
            this.customSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.customSearchLookUpEdit1View.Name = "customSearchLookUpEdit1View";
            this.customSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.customSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 275);
            this.Controls.Add(this.customSearchLookUpEdit1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Main";
            this.Text = "CustomSearchLookUpEdit";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.customersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nwindDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customSearchLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customSearchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private CustomSearchLookUpEdit customSearchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView customSearchLookUpEdit1View;
        private nwindDataSet nwindDataSet;
        private System.Windows.Forms.BindingSource customersBindingSource;
        private DXSample.nwindDataSetTableAdapters.CustomersTableAdapter customersTableAdapter;
    }
}

