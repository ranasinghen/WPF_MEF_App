using InternalShared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Windows.Input;

namespace WPF_MEF_App
{
    public class MainWindowModel : NotifyModelBase
    {
        public ICommand ImportPluginCommand { get; protected set; }

        /// <summary>
        /// If one plugin of type IView is found then it is loaded in this property
        /// </summary>
        private IView PluginViewVar;
       // [Import(typeof(IView), AllowRecomposition = true, AllowDefault = true)]
        public IView PluginView
        {
            get { return PluginViewVar; }
            set
            {
                PluginViewVar = value;
                NotifyChangedThis();
            }
        }

        /// <summary>
        /// All plugins of type IView will be imported into this collection
        /// with lazy initialization. Plugin metadata is collected from plugins attribute
        /// </summary>
        [ImportMany(typeof(IView), AllowRecomposition = true)]
        public IEnumerable<Lazy<IView, IPluginMetadata>> Plugins;

        /// <summary>
        /// Example of method Export/Import
        /// </summary>
        [Import("Parser", typeof(Func<string, string>), AllowDefault = true)]
        public Func<string, string> ParseMethod { get; set; }

        private AggregateCatalog catalog;
        private CompositionContainer container;



        public MainWindowModel()
        {
            ImportPluginCommand = new DelegateCommand(ImportPluginExecute);

            //First create a catalog of exports
            //It can be TypeCatalog(typeof(IView), typeof(SomeOtherImportType)) 
            //to search for all exports by specified types
            //DirectoryCatalog(pluginsPath, "App*.dll") to search specified directories
            //and matching specified file name
            //An aggregate catalog that combines multiple catalogs
            catalog = new AggregateCatalog();

            //Here we add all the parts found in all assemblies in directory of executing assembly directory
            //with file name matching Plugin*.dll
            string pluginsPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            catalog.Catalogs.Add(new DirectoryCatalog(pluginsPath, "Plugin*.dll"));


            //also we add to a search path a subdirectory plugins
            pluginsPath = Path.Combine(pluginsPath, "plugins");
            if (!Directory.Exists(pluginsPath))
                Directory.CreateDirectory(pluginsPath);
            catalog.Catalogs.Add(new DirectoryCatalog(pluginsPath, "Plugin*.dll"));            

            //Create the CompositionContainer with the parts in the catalog.
            container = new CompositionContainer(catalog);
        }


        private string CurrentPluginNameVar;
        public string CurrentPluginName
        {
            get { return CurrentPluginNameVar; }
            set
            {
                CurrentPluginNameVar = value;				
                NotifyChangedThis();
            }
        }
		

        private void ImportPluginExecute()
        {
            //refresh catalog for any changes in plugins
            //catalog.Refresh();

            //Fill the imports of this object
            //finds imports and fills in all preperties decorated
            //with Import attribute in this instance
            container.ComposeParts(this);
            //another option
            //container.SatisfyImportsOnce(this);

            //if we expect more than one plugin then we can operate on its metadata
            //information before creating plugin instance

            if (string.IsNullOrEmpty(CurrentPluginName))
            {
                var pluginContainer = Plugins.FirstOrDefault();
                if (pluginContainer != null)
                {
                    PluginView = pluginContainer.Value;
                    CurrentPluginName = pluginContainer.Metadata.Name;
                }
                else
                    CurrentPluginName = "<No plugins found>";
            }
            else
            {
                var pluginContainer = Plugins.Where(pc=>pc.Metadata.Name != CurrentPluginName).FirstOrDefault();
                if (pluginContainer != null)
                {
                    PluginView = pluginContainer.Value;
                    CurrentPluginName = pluginContainer.Metadata.Name;
                }
                else
                    CurrentPluginName = "<No other plugins found>";
            }

        }
    }



}
