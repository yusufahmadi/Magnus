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
   
    public class CustomSearchLookUpEdit : SearchLookUpEdit
    {
        static CustomSearchLookUpEdit()
        {
            RepositoryItemCustomSearchLookUpEdit.RegisterCustomEdit();
        }

        public CustomSearchLookUpEdit()
        {
            //this.UpdateDisplayFilter += new EventHandler(OnUpdateDisplayFilter);
        }

        public override string EditorTypeName
        {
            get
            {
                return RepositoryItemCustomSearchLookUpEdit.CustomEditName;
            }
        }

        //public delegate void OnUpdateDisplayFilter(object sender, DXSample.DisplayFilterEventArgs e)
        //{
        //    //if (e.FilterText.IndexOf(' ') == -1)
        //    //    return;
        //    //e.FilterText = '"' + e.FilterText + '"';
        //}

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemCustomSearchLookUpEdit Properties
        {
            get { return base.Properties as RepositoryItemCustomSearchLookUpEdit; }
        }

        protected override DevExpress.XtraEditors.Popup.PopupBaseForm CreatePopupForm()
        {
            return new CustomPopupSearchLookUpEditForm(this);
        }

        public event UpdateDisplayFilterHandler UpdateDisplayFilter
        {
            add { this.Properties.UpdateDisplayFilter += value; }
            remove { this.Properties.UpdateDisplayFilter -= value; }
        }
    }
}