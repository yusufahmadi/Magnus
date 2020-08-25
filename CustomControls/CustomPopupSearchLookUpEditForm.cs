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

namespace DXSample {

    public class CustomPopupSearchLookUpEditForm : PopupSearchLookUpEditForm
    {
        public CustomPopupSearchLookUpEditForm(SearchLookUpEdit edit) : base(edit) { }
        
        protected override void UpdateDisplayFilter(string displayFilter)
        {
            DisplayFilterEventArgs args = new DisplayFilterEventArgs(displayFilter);
            Properties.RaiseUpdateDisplayFilter(args);
            base.UpdateDisplayFilter(args.FilterText); 
        }

        public new RepositoryItemCustomSearchLookUpEdit Properties {
            get { return base.Properties as RepositoryItemCustomSearchLookUpEdit; }
        }
    }

}