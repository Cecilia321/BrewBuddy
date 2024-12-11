using BrewBuddy.Interface;
using BrewBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ScottPlot;
using System.IO;
using System.Linq;


namespace BrewBuddy.Pages.Statistikker
{
    public class StatistiksModel : PageModel
    {
        //vi statrer med at injektisere repositoriet i coffiemachinmodel
        private readonly IRepository<Statistik> _repository;

        //denne her laver vi for at holde maskinerne i en liste 
        public List<Statistik> Stat { get; set; }

        //og den her laver vi for at kunne oprette en ny maskine 
        [BindProperty]
        public Statistik NewStat { get; set; }

        //derefter laver vi en konstruktør med repositori
        public StatistiksModel(IRepository<Statistik> repository)
        {
            _repository = repository;
        }


        public void OnGet()
        {
            Stat = _repository.GetAll();
            

        }


//        private void GenerateGraph()
//{
//    // Skab en ny plotteskabelon
//    var plt = new ScottPlot.Plot();

//    // Tilføj data (her bruger vi eksempeldata)
//    double[] data = { 1, 2, 3, 4, 5 };
//    plt.AddBar(data);

//    // Tilføj labels
//    plt.Title("Statistik");
//    plt.XAxis.Label("X-Akse");
//    plt.YAxis.Label("Y-Akse");

//    // Gem grafen som et billede
//    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "statistik.png");
//    Directory.CreateDirectory(Path.GetDirectoryName(imagePath)!);
//    plt.SaveFig(imagePath);
//}




        //public IActionResult OnGetGraphImage()
        //{
        //    // Opret en OxyPlot-model
        //    var plotModel = new PlotModel { Title = "statistik" };

        //    // Tilføj datasæt
        //    var barSeries = new ColumnSeries
        //    {
        //        ItemsSource = new List<ColumnItem>
        //        {
        //            new ColumnItem(5),
        //            new ColumnItem(10),
        //            new ColumnItem(15),
        //            new ColumnItem(20)
        //        },
        //        LabelPlacement = LabelPlacement.Outside
        //    };

        //    plotModel.Series.Add(barSeries);

        //    // Generér billede som byte-array
        //    var exporter = new OxyPlot.SkiaSharp.PngExporter { Width = 600, Height = 400 };
        //    var stream = new MemoryStream();
        //    exporter.Export(plotModel, stream);
        //    stream.Seek(0, SeekOrigin.Begin);

        //    // Returnér billede
        //    return File(stream, "image/png");
        //}
    }
}
