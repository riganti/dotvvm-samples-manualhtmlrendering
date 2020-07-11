using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Controls;
using System.IO;

namespace ManualHtmlRendering.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {
		public List<DataEntry> Items { get; set; }

        public string OutputHtml { get; set; }

        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                Items = new List<DataEntry>()
                {
                    new DataEntry() { Id = 1, Name = "BMW 5", EngineHorsePower = 300, ManufacturedYear = 2010, MaxSpeed = 260 },
                    new DataEntry() { Id = 2, Name = "Citroen C5", EngineHorsePower = 120, ManufacturedYear = 2009, MaxSpeed = 220 },
                    new DataEntry() { Id = 3, Name = "Alfa Romeo Giulia", EngineHorsePower = 180, ManufacturedYear = 2013, MaxSpeed = 250 }
                }
                .ToList();
            }

            return base.PreRender();
        }

        public void ExportHtml()
        {
            var grid = Context.View.FindControlByClientId<GridView>("CarsGrid");
            grid.SetValue(RenderSettings.ModeProperty, RenderMode.Server);

            using (var stringWriter = new StringWriter())
            {
                var writer = new HtmlWriter(stringWriter, Context);
                grid.Render(writer, Context);

                OutputHtml = stringWriter.ToString();
            }
        }
    }

    public class DataEntry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EngineHorsePower { get; set; }
        public int ManufacturedYear { get; set; }
        public int MaxSpeed { get; set; }
    }
}
