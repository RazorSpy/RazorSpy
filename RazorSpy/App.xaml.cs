using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using ReactiveUI;

namespace RazorSpy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static CompositionContainer container;

        public static CompositionContainer Container
        {
            get { return container; }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            RxApp.GetFieldNameForPropertyNameFunc = p => "_" + Char.ToLower(p[0]) + p.Substring(1);

            AssemblyCatalog thisAsm = new AssemblyCatalog(typeof(App).Assembly);
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(thisAsm);
            if(Directory.Exists("Packages")) {
                catalog.Catalogs.Add(new DirectoryCatalog("Packages"));
            }
            container = new CompositionContainer(catalog);
            container.Compose(new CompositionBatch());
        }
    }
}
