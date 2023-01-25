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
    public class Department : BaseObject, ICompanyMasterDetail
    {
        public Department(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        string departmentCode;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string DepartmentCode
        {
            get => departmentCode;
            set => SetPropertyValue(nameof(DepartmentCode), ref departmentCode, value);
        }

        string departmentName;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string DepartmentName
        {
            get => departmentName;
            set => SetPropertyValue(nameof(DepartmentName), ref departmentName, value);
        }

        Branch branch;
        [Association("Branch-Departments")]
        public Branch Branch
        {
            get => branch;
            set => SetPropertyValue(nameof(Branch), ref branch, value);
        }
    }
}