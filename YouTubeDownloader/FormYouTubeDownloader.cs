using Newtonsoft.Json.Linq;
using VideoLibrary;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

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
                var progress = new Progress<double>((value) =>
                {
                    progressBarDownload.Value = (int)(value * 100);
                });

                tokenSource = new CancellationTokenSource();
                downloadButton.Enabled = false;
                textBoxDownload.Enabled = false;
                cancelButton.Enabled = true;
                ;
                await DownloadFileUsingExplodeAsync(textBoxDownload.Text, progress, tokenSource.Token);
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

        private async Task DownloadFileUsingExplodeAsync(string videoUrl, IProgress<double> progress, CancellationToken token)
        {
            var youtube = new YoutubeClient();
            var videoId = VideoId.Parse(videoUrl);
            var movie = await youtube.Videos.GetAsync(videoUrl, token);

            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
            var streamInfo = streamManifest.GetMuxedStreams().TryGetWithHighestVideoQuality();
            if (streamInfo is null)
            {
                MessageBox.Show("This video has no muxed streams.");
                return;
            }
            var fileName = $"{videoId}.{streamInfo.Container.Name}";
            var filePath = Path.Combine(@"C:\Users\komp\Desktop\download\", fileName);
            await youtube.Videos.Streams.DownloadAsync(streamInfo, filePath, progress, token);
        }

        private async Task DownloadFileAsync(string movieUrl, IProgress<DownloadStatus> progress, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var youtube = YouTube.Default;
            var movie = await youtube.GetVideoAsync(movieUrl);
            var videoClient = new VideoLibrary.VideoClient();

            // Get data without token
            //var data = await movie.GetBytesAsync();

            // Get data with token
            using var stream = await videoClient.StreamAsync(movie);
            var destFilePath = @"C:\Users\komp\Desktop\download\" + movie.FullName;

            // Using RAM
            //var data = await ReadStreamToByteArrayAsync(stream, cancellationToken);
            //await File.WriteAllBytesAsync(destFilePath, data, cancellationToken);

            // Write direct to file stream
            await WriteStreamToFileAsync(stream, destFilePath, movie.ContentLength.Value, progress, cancellationToken);

        }

        private async Task WriteStreamToFileAsync(Stream stream, string destFilePath, long contentLength, IProgress<DownloadStatus> progress, CancellationToken cancellationToken)
        {
            byte[] buffer = new byte[16 * 1024];
            using FileStream fileStream = File.OpenWrite(destFilePath);
            int read;
            long totalRead = 0;
            while ((read = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();
                fileStream.Write(buffer, 0, read);
                totalRead += read;

                //Send update request to GUI
                progress.Report(new DownloadStatus
                {
                    TotalRead = totalRead,
                    TotalSize = contentLength

                });

            }

        }

        private async Task<byte[]> ReadStreamToByteArrayAsync(Stream stream, CancellationToken token)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = await stream.ReadAsync(buffer, 0, buffer.Length, token)) > 0)
                {
                    token.ThrowIfCancellationRequested();
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