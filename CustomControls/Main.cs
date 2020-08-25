using System;
using DevExpress.XtraEditors;


namespace DXSample {
    public partial class Main : XtraForm
    {
        public Main()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'nwindDataSet.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.nwindDataSet.Customers);
        }

        private void OnUpdateDisplayFilter(object sender, DisplayFilterEventArgs e) {
            if(e.FilterText.IndexOf(' ') == -1)
                return;

            //for a single column search:
            //e.FilterText = "Contact:" + '"' + e.FilterText + '"';
            //multi-column search
            e.FilterText = '"' + e.FilterText + '"';
        }
    }
}
