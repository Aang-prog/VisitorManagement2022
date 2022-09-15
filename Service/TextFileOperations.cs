namespace VisitorManagement2022.Service
{
    public class TextFileOperations : ITextFileOperations
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TextFileOperations(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IEnumerable<string> LoadConditionsOfAcceptance()
        {

            string rootPath = _webHostEnvironment.WebRootPath;
            FileInfo filePath = new FileInfo(Path.Combine(rootPath, "ConditionsForAcceptance.txt"));
            string[] lines = System.IO.File.ReadAllLines(filePath.ToString());
            return lines.ToList();

        }
    }
}

