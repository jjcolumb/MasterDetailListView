using DevExpress.ExpressApp.Blazor.Components.Models;
using System;
using System.Collections.Generic;
using MasterDetailListView.Module.BusinessObjects;

namespace MasterDetailListView.Module.Blazor.Editors.CompanyMasterDetailListEditor
{
    public class CompanyMasterDetailListViewModel : ComponentModelBase
    {
        public IEnumerable<Company> Data
        {
            get => GetPropertyValue<IEnumerable<Company>>();
            set => SetPropertyValue(value);
        }

        public void Refresh() => RaiseChanged();

        public void OnSelected(IReadOnlyList<object> item) =>
            Selected?.Invoke(this, new CompanyMasterDetailListViewModelSelectedClickEventArgs(item));
        public event EventHandler<CompanyMasterDetailListViewModelSelectedClickEventArgs> Selected;

        public void OnItemClick(ICompanyMasterDetail item) =>
            ItemClick?.Invoke(this, new CompanyMasterDetailListViewModelItemClickEventArgs(item));
        public event EventHandler<CompanyMasterDetailListViewModelItemClickEventArgs> ItemClick;
    }

    public class CompanyMasterDetailListViewModelSelectedClickEventArgs : EventArgs
    {
        public CompanyMasterDetailListViewModelSelectedClickEventArgs(IReadOnlyList<object> selectedItem)
        {
            SelectedItem = selectedItem;
        }
        public IReadOnlyList<object> SelectedItem { get; }
    }


    public class CompanyMasterDetailListViewModelItemClickEventArgs : EventArgs
    {
        public CompanyMasterDetailListViewModelItemClickEventArgs(ICompanyMasterDetail item)
        {
            Item = item;
        }
        public ICompanyMasterDetail Item { get; }
    }
}
