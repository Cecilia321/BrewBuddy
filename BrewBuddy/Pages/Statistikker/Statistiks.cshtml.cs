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

        //derefter laver vi en konstruktør med repositori
        public StatistiksModel(IRepository<Statistik> repository)
        {
            _repository = repository;
        }



        public void OnGet()
        {
            // Hent alle statistikker fra databasen
            Stat = _repository.GetAll();

            // Filtrér statistikkerne, så vi kun får data for Bønner og MælkePulver
            var filteredStats = Stat.Where(s => s.Type == "Bønner" || s.Type == "MælkePulver").ToList();

            // Gruppér statistikkerne efter MachineId (kaffemaskine)
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
                ChartData.Columns.Add("Type", typeof(System.String)); // Bønner eller MælkePulver
                ChartData.Columns.Add("Amount", typeof(System.Double)); // Mængde af bønner eller mælkepulver

                // Tilføj rækker til ChartData for den aktuelle maskine
                foreach (var stat in group)
                {
                    ChartData.Rows.Add(stat.Type, stat.Amount);
                }

                // Opret en statisk kilde med denne data
                StaticSource source = new StaticSource(ChartData);
                DataModel model = new DataModel();
                model.DataSources.Add(source);

                // Opret et søjlediagram for den aktuelle maskine
                Charts.ColumnChart column = new Charts.ColumnChart($"machine_chart_{group.Key}");
                column.Width.Pixel(700);
                column.Height.Pixel(400);

                // Sæt DataSource til data
                column.Data.Source = model;

                // Sæt diagramtitel og undertitel
                column.Caption.Text = $"Mængde af Bønner og MælkePulver for Maskine {group.Key}";
                column.SubCaption.Text = "Viser mængden af bønner og mælkepulver pr. maskine";

                // Skjul legend -- legend viser forklaring af farver og linjer.
                column.Legend.Show = false;

                // Sæt X-aksen som Type (Bønner eller MælkePulver) og Y-aksen som Amount
                column.XAxis.Text = "Type";
                column.YAxis.Text = "Mængde (i antal poser)";

                // Vælg tema for diagrammet
                column.ThemeName = FusionChartsTheme.ThemeName.CANDY;

                // Generer diagrammets JSON-data og tilføj det til listen
                chartJsonList.Add(column.Render());
            }

            // Konverter listen af diagrammer til en enkelt streng
            ChartJson = string.Join(";", chartJsonList);
        }
    }
}

