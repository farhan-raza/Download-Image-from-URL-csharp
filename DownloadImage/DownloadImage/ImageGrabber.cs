using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Html;
using Aspose.Html.Dom;
using Aspose.Html.Net;
using DownloadImage;

namespace DownloadImage
{
    class ImageGrabber
    {
        private readonly IList<IElementGrabber> _elementGrabbers = new List<IElementGrabber>();

        public ImageGrabber()
        {
            _elementGrabbers.Add(new ImgElementGrabber());
            _elementGrabbers.Add(new ObjectElementGrabber());
        }

        public IEnumerable<Image> GrabFrom(Document document)
        {
            return _elementGrabbers.SelectMany(collector => collector.Collect(document));
        }

        private interface IElementGrabber
        {
            public IEnumerable<Image> Collect(Document document);
        }

        private class ImgElementGrabber : IElementGrabber
        {
            public IEnumerable<Image> Collect(Document document)
            {
                var images = document.QuerySelectorAll("img");

                foreach (Element image in images)
                {
                    if (!image.HasAttribute("src")) continue;
                    var src = image.GetAttribute("src");
                    using var response = document.Context.Network.Send(new RequestMessage(new Url(src, document.BaseURI)));

                    if (response.IsSuccess)
                    {
                        yield return new Image(response);
                    }
                }
            }
        }

        private class ObjectElementGrabber : IElementGrabber
        {
            public IEnumerable<Image> Collect(Document document)
            {
                var objects = document.QuerySelectorAll("object");

                foreach (Element obj in objects)
                {
                    if (!obj.HasAttribute("data")) continue;
                    var data = obj.GetAttribute("data");
                    using var response = document.Context.Network.Send(new RequestMessage(new Url(data, document.BaseURI)));

                    if (response.Headers.ContentType.MediaType.Type.StartsWith("image") && response.IsSuccess)
                    {
                        yield return new Image(response);
                    }
                }
            }
        }
    }
}