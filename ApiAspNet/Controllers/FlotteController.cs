using ApiAspNet.Models.Flottes;
using ApiAspNet.Services;
using AutoMapper;
using FastReport;
using FastReport.Export.PdfSimple;
using Microsoft.AspNetCore.Mvc;
using FastReport;
using FastReport.Data; // ✅ NÉCESSAIRE pour RegisterBusinessObject
using FastReport.Export.PdfSimple;

using System.IO;
using System.Data;


namespace ApiAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlotteController : ControllerBase
    {
        private readonly IFlotteService _FlotteService;
        private readonly IMapper _mapper;
        private readonly ILogger<FlotteController> _logger;

        public FlotteController(
            IFlotteService flotteService,
            IMapper mapper,
            ILogger<FlotteController> logger)
        {
            _FlotteService = flotteService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var flottes = _FlotteService.GetAll();
                return Ok(flottes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur récupération flottes");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var flotte = _FlotteService.GetById(id);
                if (flotte == null)
                    return NotFound(new { message = "Flotte not found." });

                return Ok(flotte);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur récupération flotte ID {id}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create(CreateRequest model)
        {
            try
            {
                _FlotteService.Create(model);
                return Ok(new { message = "Flotte created" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur création flotte");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            try
            {
                _FlotteService.Update(id, model);
                return Ok(new { message = "Flotte updated" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur mise à jour flotte");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _FlotteService.Delete(id);
                return Ok(new { message = "Flotte deleted" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur suppression flotte");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // ✅ Rapport PDF
        [HttpGet("report")]
        public IActionResult ExportReport()
        {
            try
            {
                var flottes = _FlotteService.GetAll().ToList();

                using var report = new Report();
                var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "rapport.frx");

                if (!System.IO.File.Exists(reportPath))
                    return NotFound(new { message = "Fichier rapport.frx introuvable." });

                report.Load(reportPath);

                // Convertir la liste en DataTable
                var dt = new DataTable("Flottes");
                dt.Columns.Add("IdFlotte", typeof(string));          
                dt.Columns.Add("TypeFlotte", typeof(string));
                dt.Columns.Add("MatriculeFlotte", typeof(string));
                // Ajoutez toutes les colonnes nécessaires selon votre modèle Flotte

                foreach (var flotte in flottes)
                {
                    dt.Rows.Add(flotte.IdFlotte.ToString(), flotte.TypeFlotte, flotte.MatriculeFlotte);
                }

                // Enregistrer la DataTable comme source de données
                report.RegisterData(dt, "Flottes");
                report.GetDataSource("Flottes").Enabled = true;

                report.Prepare();

                using var stream = new MemoryStream();
                var pdfExport = new PDFSimpleExport();
                report.Export(pdfExport, stream);
                stream.Position = 0;

                return File(stream.ToArray(), "application/pdf", "rapport.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la génération du rapport.");
                return StatusCode(500, new { message = ex.Message });
            }
        }





    }
}
