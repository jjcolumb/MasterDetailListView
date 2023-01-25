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
    public class Company : BaseObject, ICompanyMasterDetail
    {
        public Company(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        string companyCode;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string CompanyCode
        {
            get => companyCode;
            set => SetPropertyValue(nameof(CompanyCode), ref companyCode, value);
        }

        string companyName;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string CompanyName
        {
            get => companyName;
            set => SetPropertyValue(nameof(CompanyName), ref companyName, value);
        }

        [Association("Company-Branch")]
        public XPCollection<Branch> Branches
        {
            get
            {
                return GetCollection<Branch>(nameof(Branches));
            }
        }
    }
}