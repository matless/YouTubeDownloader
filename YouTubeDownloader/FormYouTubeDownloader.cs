using VideoLibrary;

namespace YouTubeDownloader
{
    public partial class FormYouTubeDownloader : Form
    {
        private CancellationTokenSource tokenSource;

        public FormYouTubeDownloader()
        {
            InitializeComponent();
        }

        private async void downloadButton_Click(object sender, EventArgs e)
        {
            //var url = "https://youtube.com/shorts/p03iq190txc?si=xkve9M-X1ZGNxzYG";
            try
            {
                tokenSource = new CancellationTokenSource();
                downloadButton.Enabled = false;
                textBoxDownload.Enabled = false;
                cancelButton.Enabled = true;
                await DownloadFileAsync(textBoxDownload.Text, tokenSource.Token);
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
                cancelButton.Enabled = false;
            }


        }
       
        private async Task DownloadFileAsync(string movieUrl, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var youtube = YouTube.Default;
            var movie = await youtube.GetVideoAsync(movieUrl);
            var videoClient = new VideoClient();

            // Get data without token
            //var data = await movie.GetBytesAsync();

            // Get data with token
            var stream = await videoClient.StreamAsync(movie);
            var data = await ReadStreamToByteArrayAsync(stream, cancellationToken);


            var destFilePath = @"C:\Users\komp\Desktop\download\" + movie.FullName;
            await File.WriteAllBytesAsync(destFilePath, data, cancellationToken);
        }

        private async Task<byte[]> ReadStreamToByteArrayAsync(Stream stream, CancellationToken token)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = await stream.ReadAsync(buffer, 0, buffer.Length, token)) > 0)
                {
                    ms.Write(buffer, 0, read);

                }
                return ms.ToArray();
            }

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
        }
    }
}