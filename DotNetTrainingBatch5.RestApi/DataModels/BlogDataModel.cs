namespace DotNetTrainingBatch5.RestApi.DataModels
{
    public class BlogDataModel
    {
        public int BlogId { get; set; }

        public string BlogTital { get; set; }

        public string BlogAuthor { get; set; }

        public string BlogContent { get; set; }

        public bool DeleteFlag { get; set; }
    }
}
