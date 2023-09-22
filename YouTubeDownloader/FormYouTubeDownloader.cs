using VideoLibrary;

namespace YouTubeDownloader
{
    public partial class FormYouTubeDownloader : Form
    {
        public FormYouTubeDownloader()
        {
            InitializeComponent();
        }

        private async void downloadButton_Click(object sender, EventArgs e)
        {
            //var url = "https://youtube.com/shorts/p03iq190txc?si=xkve9M-X1ZGNxzYG";

            await DownloadFileAsync(textBoxDownload.Text);
        }

        private async Task DownloadFileAsync(string movieUrl)
        {
            var youtube = YouTube.Default;
            var movie = await youtube.GetVideoAsync(movieUrl);
            var data = await movie.GetBytesAsync();
            var destFilePath = @"C:\Users\komp\Desktop\download\" + movie.FullName;
            await File.WriteAllBytesAsync(destFilePath, data);
        }
    }
}