using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Registrator;
using System.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors;
using System.ComponentModel;
using DevExpress.XtraEditors.Popup;
using System.Reflection;

namespace DXSample {

    public delegate void UpdateDisplayFilterHandler(object sender, DisplayFilterEventArgs e);
    public class DisplayFilterEventArgs : EventArgs
    {
        string filterText;
        public DisplayFilterEventArgs(string filterText)
        {
            this.filterText = filterText;
        }
        public string FilterText
        {
            get { return filterText; }
            set
            {
                if (filterText != value)
                    filterText = value;
            }
        }
    }
}