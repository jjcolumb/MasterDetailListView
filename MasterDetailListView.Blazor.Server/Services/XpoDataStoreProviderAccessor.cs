using System;
using DevExpress.ExpressApp.Xpo;

namespace MasterDetailListView.Blazor.Server.Services {
    public class XpoDataStoreProviderAccessor {
        public IXpoDataStoreProvider DataStoreProvider { get; set; }
    }
}
