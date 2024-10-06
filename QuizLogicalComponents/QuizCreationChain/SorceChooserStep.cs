using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Text.Json;
using Refit;
using QuizLogicalComponents.AbstractChain;

namespace QuizLogicalComponents.QuizCreationChain
{
    /// <summary>
    /// an <see cref="TopicCreationStep"/>, which is responsible for fetching the source of the Final Topic.
    /// </summary>
    /// <param name="FileStreamFetcher">The method that let's user choose the File, it is UI dependent.</param>
    public abstract record class ChooseNotesSource : TopicCreationStep
    {
        /// <summary>
        /// Path to a local file with the source notes.
        /// </summary>
        public string? source = null;

        /// <summary>
        /// Product of the last step.
        /// </summary>
        public override TopicProduct? BetweenStep { get; set; } = null;

        public Action finalize;

        /// <summary>
        /// Temporary Files used for the source. Present in current directory.
        /// </summary>
        protected TempFileCollection _tempFiles;

        public ChooseNotesSource(Action finalize)
        {
            this.finalize = finalize;
            string directoryPath = Path.Combine(Environment.CurrentDirectory, "temps");
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

            // Initialize TempFileCollection with the specified directory
            _tempFiles = new TempFileCollection(directoryPath, false); // Set to delete files on Dispose
        }

        /// <summary>
        /// Creates the temporary file.
        /// </summary>
        /// <param name="path">Path produced by the <see cref="TempFileCollection.AddExtension"/></param>
        protected void createTemporaryFile(string path)
        {
            File.Create(path).Dispose();
        }

        public override void Dispose()
        {
            base.Dispose();
            _tempFiles.Delete();
        }

        internal override TopicProduct Step()
        {
            BetweenStep.finalize = this.finalize;
            return BetweenStep;
        }
    }


    /// <summary>
    /// Step that lets user Choose a html file as the notes source.
    /// </summary>
    public record class ChooseLocalHtmlFileOption : ChooseNotesSource
    {
        /// <summary>
        /// The Stream of the open File reader, which locks the file for our process.
        /// </summary>
        private FileStream openedFile;

        public new static string GetLabel() => "Choose local file";

        /// <summary>
        /// The method that let's user choose the File, it is UI dependent.
        /// </summary>
        public Func<FileStream?> FileStreamFetcher;

        public ChooseLocalHtmlFileOption(Func<FileStream> fileStreamFetcher, Action finalize) : base(finalize)
        {
            this.FileStreamFetcher = fileStreamFetcher;

            // open the file
            FileStream? fileRes = null;
            while (fileRes == null)
            {
                fileRes = FileStreamFetcher.Invoke();
            }

            this.openedFile = fileRes;
        }

        internal override TopicProduct Step()
        {
            _ = base.Step();

            // add file to temporaries with the .html extension
            source = _tempFiles.AddExtension("html");
            createTemporaryFile(source);
            // copy data from the source to created temporary
            transferData();

            // give the notes to the Product
            BetweenStep.pathToSource = source;
            return BetweenStep;
        }

        public override void Dispose()
        {
            base.Dispose();
            if (openedFile != null) openedFile.Dispose();
        }

        /// <summary>
        /// Transfers the source file data to a created temporary using buffer.
        /// </summary>
        /// <returns>Success of the copying.</returns>
        private bool transferData()
        {
            if (openedFile == null) return false;
            if (source == null) return false;

            var temporaryNotesFile = new FileStream(source, FileMode.Open, FileAccess.Write);
            using (openedFile)
            {
                openedFile.CopyToAsync(temporaryNotesFile).Wait();
            }
            openedFile.Dispose();
            temporaryNotesFile.Dispose();

            return true;
        }
    }


    /// <summary>
    /// Step that lets user Choose notion page as the notes source. This Page is downloaded of Step and is not synced with the actual notion page.
    /// <para>
    /// This Class uses <see langword="notion page to html api " href="https://github.com/asnunes/notion-page-to-html-api?tab=readme-ov-file"/> For fetching the pages from URL.
    /// </para>
    /// </summary>
    /// <param name="NotionPageUrl">A URL link to Notion page. IMPORTANT: the page must be in the Fetch faze set as PUBLIC for acess</param>
    public record class ChooseNotionNotes : ChooseNotesSource
    {
        /// <summary>
        /// Url to the Notion page user want to use as Quiz source.
        /// </summary>
        public Uri NotionPageUrl;

        /// <summary>
        /// Url to the Api used to fetch the page using the <see cref="Id"/>
        /// </summary>
        public static string NotionApiUrl = "https://notion-page-to-html-api.vercel.app";

        /// <summary>
        /// The Id of page used by Api to fetch the data
        /// </summary>
        private string Id;

        public new static string GetLabel() => "Choose Notion page";

        public ChooseNotionNotes(Uri notionPageUrl, Action finalize) : base(finalize)
        {
            NotionPageUrl = notionPageUrl;

            // We will parse the @notionPageUrl to get the Id
            Id = this.GetIdFromUrl(notionPageUrl);
        }

        internal override TopicProduct Step()
        {
            _ = base.Step();

            var notionAPI = RestService.For<INotionPageAPI>(NotionApiUrl);
            var dataResponse = notionAPI.GetPage(Id);

            // add file to temporaries with the .html extension
            source = _tempFiles.AddExtension("html");
            createTemporaryFile(source);

            var dataResponceResult = dataResponse.Result;
            if (dataResponceResult == null) 
            {
                throw new Exception("Something went wrong with request!");
            } 
            if (!dataResponceResult.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"The request for the Notion notes exited with code ({(int)dataResponceResult.StatusCode}: {dataResponceResult.StatusCode})! Please make sure that YOUR NOTION PAGE IS PUBLIC! {dataResponceResult.Error.Message}!");
            }

            var data = dataResponceResult.Content;

            // Copy data from API output stream to the temporary file
            using (var fileStream = new FileStream(source, FileMode.Open, FileAccess.Write))
            {
                data.CopyToAsync(fileStream).Wait();
            }

            // give the notes temporary to the Product
            BetweenStep.pathToSource = source;

            return BetweenStep;
        }

        public interface INotionPageAPI
        {
            [Get("/html?id={block_id}")]
            Task<ApiResponse<Stream>> GetPage([AliasAs("block_id")] string notionPageBlockId);
        }

        private string GetIdFromUrl(Uri urlObject)
        {
            int idLength = 32;

            int idStartIndex = urlObject.AbsolutePath.Length - idLength;
            if (idStartIndex <= 0) throw new InvalidDataException("Not a valid Notion Link!");

            return urlObject.AbsolutePath.Substring(idStartIndex);
        }
    }
}
