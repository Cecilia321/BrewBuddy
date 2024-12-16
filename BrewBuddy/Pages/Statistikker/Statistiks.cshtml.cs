using BrewBuddy.Interface;
using BrewBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using FusionCharts.DataEngine;
using FusionCharts.Visualization;
using System.Data;
using Microsoft.AspNetCore.Authorization;



namespace BrewBuddy.Pages.Statistikker
{
    [Authorize(Policy = "AdminOnly")]
    public class StatistiksModel : PageModel
    {
        //vi statrer med at injektisere repositoriet i coffiemachinmodel
        private readonly IRepository<Statistik> _repository;

        //denne her laver vi for at holde maskinerne i en liste 
        public List<Statistik> Stat { get; set; }

        //og den her laver vi for at kunne oprette en ny maskine 
        [BindProperty]
        public Statistik NewStat { get; set; }
        public string ChartJson { get; internal set; }

        //derefter laver vi en konstrukt�r med repositori
        public StatistiksModel(IRepository<Statistik> repository)
        {
            _repository = repository;
        }



        public void OnGet()
        {
                // Hent alle statistikker fra databasen
                Stat = _repository.GetAll();

                // Filtr�r statistikkerne, s� vi kun f�r data for B�nner og M�lkePulver
                var filteredStats = Stat.Where(s => s.Type == "B�nner" || s.Type == "M�lkePulver").ToList();

                // Grupp�r statistikkerne efter MachineId (kaffemaskine)
                var groupedStats = filteredStats
                                    .GroupBy(s => s.MachineId)
                                    .ToList();
                

                // Liste til at holde diagramdataene for hver maskine
                List<string> chartJsonList = new List<string>();

                // For hver maskine (MachineId) opretter vi et separat diagram
                foreach (var group in groupedStats)
                {
                    // Opret en DataTable til at holde data til diagrammet for den aktuelle maskine
                    DataTable ChartData = new DataTable();
                    ChartData.Columns.Add("Type", typeof(System.String)); // B�nner eller M�lkePulver
                    ChartData.Columns.Add("Amount", typeof(System.Double)); // M�ngde af b�nner eller m�lkepulver

                    // Tilf�j r�kker til ChartData for den aktuelle maskine
                    foreach (var stat in group)
                    {
                        ChartData.Rows.Add(stat.Type, stat.Amount);
                    }

                    // Opret en statisk kilde med denne data
                    StaticSource source = new StaticSource(ChartData);
                    DataModel model = new DataModel();
                    model.DataSources.Add(source);

                    // Opret et s�jlediagram for den aktuelle maskine
                    Charts.ColumnChart column = new Charts.ColumnChart($"machine_chart_{group.Key}");
                    column.Width.Pixel(700);
                    column.Height.Pixel(400);

                    // S�t DataSource til data
                    column.Data.Source = model;

                    // S�t diagramtitel og undertitel
                    column.Caption.Text = $"M�ngde af B�nner og M�lkePulver for Maskine {group.Key}";
                    column.SubCaption.Text = "Viser m�ngden af b�nner og m�lkepulver pr. maskine";

                    // Skjul legend -- legend viser forklaring af farver og linjer.
                    column.Legend.Show = false;

                    // S�t X-aksen som Type (B�nner eller M�lkePulver) og Y-aksen som Amount
                    column.XAxis.Text = "Type";
                    column.YAxis.Text = "M�ngde (i antal poser)";

                    // V�lg tema for diagrammet
                    column.ThemeName = FusionChartsTheme.ThemeName.CANDY;

                    // Generer diagrammets JSON-data og tilf�j det til listen
                    chartJsonList.Add(column.Render());
                }

                // Konverter listen af diagrammer til en enkelt streng
                ChartJson = string.Join(";", chartJsonList);
            }




            //Stat = _repository.GetAll();
            //DataTable ChartData = new DataTable();
            //ChartData.Columns.Add("MachineId", typeof(System.Int32)); // Kaffemaskinens ID
            //ChartData.Columns.Add("Type", typeof(System.String)); // B�nner eller M�lkePulver
            //ChartData.Columns.Add("Amount", typeof(System.Double)); // M�ngde af b�nner eller m�lkepulver

            //// Filtrere statistikkerne, og tilf�je data for b�nner og m�lkepulver til ChartData
            //var filteredStats = Stat.Where(s => s.Type == "B�nner" || s.Type == "M�lkePulver").ToList();

            //foreach (var stat in filteredStats)
            //{
            //    ChartData.Rows.Add(stat.MachineId, stat.Type, stat.Amount);
            //}

            //// Opret en statisk kilde med denne data
            //StaticSource source = new StaticSource(ChartData);
            //DataModel model = new DataModel();
            //model.DataSources.Add(source);

            //// Opret en s�jlediagram
            //Charts.ColumnChart column = new Charts.ColumnChart("machine_chart");
            //column.Width.Pixel(700);
            //column.Height.Pixel(400);

            //// S�t DataSource til data
            //column.Data.Source = model;

            //// S�t diagramtitel og undertitel
            //column.Caption.Text = "M�ngde af b�nner og m�lkepulver pr. Maskine";
            //column.SubCaption.Text = "Viser m�ngden af b�nner og m�lkepulver pr. maskine";

            //// Skjul legend
            //column.Legend.Show = false;

            //// S�t X-aksen som MaskineID og Y-aksen som Amount
            //column.XAxis.Text = "Kaffemaskine ID";
            //column.YAxis.Text = "M�ngde (i kg)";

            //// V�lg tema for diagrammet
            //column.ThemeName = FusionChartsTheme.ThemeName.FUSION;

            //// Generer diagrammets JSON-data
            //ChartJson = column.Render();






            ////data fra hjemmesiden
            //// create data table to store data
            //DataTable ChartData = new DataTable();
            //// Add columns to data table
            //ChartData.Columns.Add("Programming Language", typeof(System.String));
            //ChartData.Columns.Add("Users", typeof(System.Double));
            //// Add rows to data table

            //ChartData.Rows.Add("Java", 62000);
            //ChartData.Rows.Add("Python", 46000);
            //ChartData.Rows.Add("Javascript", 38000);
            //ChartData.Rows.Add("C++", 31000);
            //ChartData.Rows.Add("C#", 27000);
            //ChartData.Rows.Add("PHP", 14000);
            //ChartData.Rows.Add("Perl", 14000);

            //// Create static source with this data table
            //StaticSource source = new StaticSource(ChartData);
            //// Create instance of DataModel class
            //DataModel model = new DataModel();
            //// Add DataSource to the DataModel
            //model.DataSources.Add(source);
            //// Instantiate Column Chart
            //Charts.ColumnChart column = new Charts.ColumnChart("first_chart");
            //// Set Chart's width and height
            //column.Width.Pixel(700);
            //column.Height.Pixel(400);
            //// Set DataModel instance as the data source of the chart
            //column.Data.Source = model;
            //// Set Chart Title
            //column.Caption.Text = "Most popular programming language";
            //// Set chart sub title
            //column.SubCaption.Text = "2017-2018";
            //// hide chart Legend
            //column.Legend.Show = false;
            //// set XAxis Text
            //column.XAxis.Text = "Programming Language";
            //// Set YAxis title
            //column.YAxis.Text = "User";
            //// set chart theme
            //column.ThemeName = FusionChartsTheme.ThemeName.FUSION;
            //// set chart rendering json
            //ChartJson = column.Render();

        }


        //        private void GenerateGraph()
        //{
        //    // Skab en ny plotteskabelon
        //    var plt = new ScottPlot.Plot();

        //    // Tilf�j data (her bruger vi eksempeldata)
        //    double[] data = { 1, 2, 3, 4, 5 };
        //    plt.AddBar(data);

        //    // Tilf�j labels
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

        //    // Tilf�j datas�t
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

        //    // Gener�r billede som byte-array
        //    var exporter = new OxyPlot.SkiaSharp.PngExporter { Width = 600, Height = 400 };
        //    var stream = new MemoryStream();
        //    exporter.Export(plotModel, stream);
        //    stream.Seek(0, SeekOrigin.Begin);

        //    // Return�r billede
        //    return File(stream, "image/png");
        //}
    }

