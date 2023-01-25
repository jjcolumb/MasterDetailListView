using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MasterDetailListView.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Branch : BaseObject, ICompanyMasterDetail
    {
        public Branch(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        string branchCode;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string BranchCode
        {
            get => branchCode;
            set => SetPropertyValue(nameof(BranchCode), ref branchCode, value);
        }

        string branchName;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string BranchName
        {
            get => branchName;
            set => SetPropertyValue(nameof(BranchName), ref branchName, value);
        }

        Company company;
        [Association("Company-Branch")]
        public Company Company
        {
            get => company;
            set => SetPropertyValue(nameof(Company), ref company, value);
        }

        [Association("Branch-Departments")]
        public XPCollection<Department> Departments
        {
            get
            {
                return GetCollection<Department>(nameof(Departments));
            }
        }
    }
}