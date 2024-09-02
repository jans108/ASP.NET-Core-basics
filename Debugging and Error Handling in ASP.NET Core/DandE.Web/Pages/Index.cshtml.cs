using DandE.DocumentHandler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        IWebHostEnvironment _environment;

        public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
            

        }

        public void OnGet()
        {

        }

        public IEnumerable<WordDocumentCard> Documents
        {
            get
            {
                string[] documents = GetDocumentFiles();

                return documents.Select(fileName => new WordDocumentCard(fileName));
            }
        }

        private string[] GetDocumentFiles()
        {
            var rootPath = this._environment.WebRootPath;

            var docPath = Path.Combine(rootPath, "../documents");

            var documents = Directory.GetFiles(docPath, "*.docx");
            return documents;
        }
    }
}