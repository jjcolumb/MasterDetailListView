using DevExpress.Blazor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterDetailListView.Module.BusinessObjects;

namespace MasterDetailListView.Module.Blazor.Editors.CompanyMasterDetailListEditor
{
    [ListEditor(typeof(Company), false)]
    public class CompanyMasterDetailListEditor : ListEditor
    {
        public CompanyMasterDetailListEditor(IModelListView model) : base(model) { }

        public class CompanyMasterDetailHolder : IComponentContentHolder
        {
            private RenderFragment componentContent;
            public CompanyMasterDetailHolder(CompanyMasterDetailListViewModel componentModel)
            {
                ComponentModel =
                    componentModel ?? throw new ArgumentNullException(nameof(componentModel));
            }
            private RenderFragment CreateComponent() =>
                ComponentModelObserver.Create(ComponentModel,
                                                CompanyMasterDetailListViewRenderer.Create(ComponentModel));
            public CompanyMasterDetailListViewModel ComponentModel { get; }
            RenderFragment IComponentContentHolder.ComponentContent =>
                componentContent ??= CreateComponent();
        }

        private ICompanyMasterDetail[] selectedObjects = Array.Empty<ICompanyMasterDetail>();

        protected override object CreateControlsCore() =>
            new CompanyMasterDetailHolder(new CompanyMasterDetailListViewModel());

        protected override void AssignDataSourceToControl(object dataSource)
        {
            if (Control is CompanyMasterDetailHolder holder)
            {
                if (holder.ComponentModel.Data is IBindingList bindingList)
                {
                    bindingList.ListChanged -= BindingList_ListChanged;
                }
                holder.ComponentModel.Data =
                    (dataSource as IEnumerable)?.OfType<Company>().OrderBy(i => i.CompanyName);
                if (dataSource is IBindingList newBindingList)
                {
                    newBindingList.ListChanged += BindingList_ListChanged;
                }
            }
        }

        protected override void OnControlsCreated()
        {
            if (Control is CompanyMasterDetailHolder holder)
            {
                holder.ComponentModel.Selected += ComponentModel_Selected;
                holder.ComponentModel.ItemClick += ComponentModel_ItemClick;
            }
            base.OnControlsCreated();
        }

        private void ComponentModel_Selected(object sender, CompanyMasterDetailListViewModelSelectedClickEventArgs e)
        {
            //var test = e.SelectedItem;
            if (e.SelectedItem is IGridSelectionChanges changes)
            {
                IReadOnlyList<object> selectedItems = changes.SelectedDataItems;
                Company[] selected = new Company[selectedItems.Count];
                for (int i = 0; i < selectedItems.Count; i++)
                {
                    selected[i] = (Company)selectedItems[i];
                }
                selectedObjects = selected;
                OnSelectionChanged();
            }
        }

        public override void BreakLinksToControls()
        {
            if (Control is CompanyMasterDetailHolder holder)
            {
                holder.ComponentModel.Selected -= ComponentModel_Selected;
                holder.ComponentModel.ItemClick -= ComponentModel_ItemClick;
            }
            AssignDataSourceToControl(null);
            base.BreakLinksToControls();
        }

        public override void Refresh()
        {
            if (Control is CompanyMasterDetailHolder holder)
            {
                holder.ComponentModel.Data = (holder.ComponentModel.Data as IEnumerable)?.OfType<Company>();
                holder.ComponentModel.Refresh();
            }
        }

        private void BindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                //HACK: New action was hidden after deletion
                selectedObjects = Array.Empty<ICompanyMasterDetail>();
                OnSelectionChanged();
            }
            Refresh();
        }

        private void ComponentModel_ItemClick(object sender,
                                                CompanyMasterDetailListViewModelItemClickEventArgs e)
        {
            selectedObjects = new ICompanyMasterDetail[] { e.Item };
            OnSelectionChanged();
            OnProcessSelectedItem();
        }

        public override SelectionType SelectionType => SelectionType.Full;
        public override IList GetSelectedObjects() => selectedObjects;
    }
}
