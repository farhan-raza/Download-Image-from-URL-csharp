using System;
using Aspose.Html;
using Aspose.Html.Net;

namespace DownloadImage
{
    public class Image
    {
        public Image(ResponseMessage response)
        {
            Content = response.Content.ReadAsByteArray();
            Url = response.Request.RequestUri;
            MimeType = response.Headers.ContentType.MediaType;
        }

        public Url Url { get; }
        public MimeType MimeType { get; }
        public byte[] Content { get; }
    }
}