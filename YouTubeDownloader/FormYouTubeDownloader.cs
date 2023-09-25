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
            try
            {
                downloadButton.Enabled = false;
                textBoxDownload.Enabled = false;
                await DownloadFileAsync(textBoxDownload.Text);
                MessageBox.Show("Movie downloaded.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "YouTube Downloader", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                downloadButton.Enabled = true;
                textBoxDownload.Enabled = true;
            }
            
            
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