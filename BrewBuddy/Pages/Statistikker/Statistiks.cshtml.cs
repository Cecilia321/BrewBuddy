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
    }
}

