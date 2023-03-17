using COMP1640.ViewModels.Attachment.Responses;
using Domain;
using System.IO.Compression;
using System.Text;
using Utilities.Helpers;

namespace COMP1640.Services
{
    public partial class AcademicYearService
    {
        public async Task<FileDownLoadResponse> ExportIdeasAsCSVFileAsync(int academicYearId)
        {
            var ideas = await _ideaRepo.GetListAsync(academicYearId);
            if (ideas.Count <= 0)
                return null;

            StringBuilder csvData = new StringBuilder();
            string[] columnNames = new string[] {"No"
            , "Title"
            , "Content"
            , "Author"
            , "Category"
            , "Department"
            , "Like"
            , "Dislike"
            , "Comment"
            , "Created On"
            };

            foreach (var columnName in columnNames)
            {
                csvData.Append(columnName).Append(',');
            }

            csvData.Append("\r\n");

            var index = 1;
            foreach (var idea in ideas)
            {
                csvData.Append(index.ToString().Replace(",", ";")).Append(",");
                csvData.Append(idea.Title.Replace(",", ";")).Append(",");
                csvData.Append(idea.Content.Replace(",", ";")).Append(",");
                csvData.Append(idea.IsAnonymous ? "null" : idea.CreatedByNavigation.UserName.Replace(",", ";")).Append(",");
                csvData.Append(idea.Category.Name.Replace(",", ";")).Append(",");
                csvData.Append(idea.Department.Name.Replace(",", ";")).Append(",");
                csvData.Append(idea.Reactions.Where(_ => _.Status == ReactionStatusEnum.Like).Count().ToString().Replace(",", ";")).Append(",");
                csvData.Append(idea.Reactions.Where(_ => _.Status == ReactionStatusEnum.DisLike).Count().ToString().Replace(",", ";")).Append(",");
                csvData.Append(idea.Comments.Count().ToString().Replace(",", ";")).Append(",");
                csvData.Append(idea.CreatedOn.ToString().Replace(",", ";")).Append(",");

                csvData.Append("\r\n");
                index++;
            }

            byte[] bytes = Encoding.ASCII.GetBytes(csvData.ToString());
            return new FileDownLoadResponse(bytes
                , "text/csv"
                , "Ideas" + ".csv");
        }


        public async Task<FileDownLoadResponse> ExportAttachmentAsZipFileAsync(int academicYearId)
        {
            var ideas = await _ideaRepo.GetListAsync(academicYearId);
            if (ideas.Count <= 0)
                return null;

            var fileKeys = ideas.SelectMany(_ => _.IdeaAttachments.Select(_ => _.Attachment.KeyName)).ToList();
            if (fileKeys.Count <= 0)
                return null;

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var fileKey in fileKeys)
                    {
                        var file = await _attachmentService.GetAsync(fileKey);
                        var s3Object = await _attachmentService.GetS3Object(fileKey);
                        var documentData = s3Object.ResponseStream.ReadAllBytes();
                        var entry = archive.CreateEntry(file.Name);
                        using (var entryStream = entry.Open())
                        {
                            entryStream.Write(documentData
                                , 0
                                , documentData.Length);
                        }
                    }
                }

                return new FileDownLoadResponse(memoryStream.ToArray()
                    , "application/zip"
                    , "Attachments" + ".zip");
            }
        }

        public async Task<FileDownLoadResponse> ExportAcademicYearDataAsync(int academicYearId)
        {
            var academicYear = await _academicYearRepo.GetAsync(academicYearId);
            if (academicYear == null)
                return null;

            var ideasFile = await ExportIdeasAsCSVFileAsync(academicYearId);
            var attachmentsFile = await ExportAttachmentAsZipFileAsync(academicYearId);
            if (ideasFile == null && attachmentsFile == null)
                return null;

            using (var memoryStream = new MemoryStream())
            {
                // create a ZipArchive object
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    if(ideasFile != null)
                    {
                        var entry = archive.CreateEntry(ideasFile.Name);
                        using (var entryStream = entry.Open())
                        {
                            entryStream.Write(ideasFile.Data
                                , 0
                                , ideasFile.Data.Length);
                        }
                    }

                    if (attachmentsFile != null)
                    {
                        var entry = archive.CreateEntry(attachmentsFile.Name);
                        using (var entryStream = entry.Open())
                        {
                            entryStream.Write(attachmentsFile.Data
                                , 0
                                , attachmentsFile.Data.Length);
                        }
                    }
                }

                return new FileDownLoadResponse(memoryStream.ToArray()
                    , "application/zip"
                    , academicYear.Name + ".zip");
            }
        }
    }
}
