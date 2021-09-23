using System.Collections.Generic;
using System.Linq;

namespace Anshan.File
{
    public static class MimeTypes
    {
        public static class Application
        {
            static Application()
            {
                All = typeof(Application).GetProperties()
                                         .Select(p => p.GetValue(null, null))
                                         .Where(p => p is not null)
                                         .Cast<string>()
                                         .ToList();
            }

            public static IReadOnlyCollection<string> All { get; }

            public static string AtomXml => "application/atom+xml";

            public static string AtomcatXml => "application/atomcat+xml";

            public static string Ecmascript => "application/ecmascript";

            public static string JavaArchive => "application/java-archive";

            public static string Javascript => "application/javascript";

            public static string Json => "application/json";

            public static string Mp4 => "application/mp4";

            public static string OctetStream => "application/octet-stream";

            public static string Pdf => "application/pdf";

            public static string Pkcs10 => "application/pkcs10";

            public static string Pkcs7Mime => "application/pkcs7-mime";

            public static string Pkcs7Signature => "application/pkcs7-signature";

            public static string Pkcs8 => "application/pkcs8";

            public static string Postscript => "application/postscript";

            public static string RdfXml => "application/rdf+xml";

            public static string RssXml => "application/rss+xml";

            public static string Rtf => "application/rtf";

            public static string SmilXml => "application/smil+xml";

            public static string XFontOtf => "application/x-font-otf";

            public static string XFontTtf => "application/x-font-ttf";

            public static string XFontWoff => "application/x-font-woff";

            public static string XPkcs12 => "application/x-pkcs12";

            public static string XShockwaveFlash => "application/x-shockwave-flash";

            public static string XSilverlightApp => "application/x-silverlight-app";

            public static string XhtmlXml => "application/xhtml+xml";

            public static string Xml => "application/xml";

            public static string XmlDtd => "application/xml-dtd";

            public static string XsltXml => "application/xslt+xml";

            public static string Zip => "application/zip";

            public static string MicrosoftWord => "application/msword";

            public static string MicrosoftWordOpenXml =>
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        }

        public static class Audio
        {
            static Audio()
            {
                All = typeof(Audio).GetProperties()
                                   .Select(p => p.GetValue(null, null))
                                   .Where(p => p is not null)
                                   .Cast<string>()
                                   .ToList();
            }

            public static IReadOnlyCollection<string> All { get; }

            public static string Midi => "audio/midi";

            public static string Mp4 => "audio/mp4";

            public static string Mpeg => "audio/mpeg";

            public static string Mp3 => "audio/mpeg";

            public static string Ogg => "audio/ogg";

            public static string Webm => "audio/webm";

            public static string XAac => "audio/x-aac";

            public static string XAiff => "audio/x-aiff";

            public static string XMpegurl => "audio/x-mpegurl";

            public static string XMsWma => "audio/x-ms-wma";

            public static string XWav => "audio/x-wav";
        }

        public static class Image
        {
            static Image()
            {
                All = typeof(Image).GetProperties()
                                   .Select(p => p.GetValue(null, null))
                                   .Where(p => p is not null)
                                   .Cast<string>()
                                   .ToList();
            }

            public static IReadOnlyCollection<string> All { get; }

            public static string Bmp => "image/bmp";

            public static string Gif => "image/gif";

            public static string Jpeg => "image/jpeg";

            public static string Png => "image/png";

            public static string SvgXml => "image/svg+xml";

            public static string Tiff => "image/tiff";

            public static string Webp => "image/webp";
            
            public static string Tga => "Image/tga";
        }

        public static class Text
        {
            static Text()
            {
                All = typeof(Application).GetProperties()
                                         .Select(p => p.GetValue(null, null))
                                         .Where(p => p is not null)
                                         .Cast<string>()
                                         .ToList();
            }

            public static IReadOnlyCollection<string> All { get; }

            public static string Css => "text/css";

            public static string Csv => "text/csv";

            public static string Html => "text/html";

            public static string Plain => "text/plain";

            public static string RichText => "text/richtext";

            public static string Sgml => "text/sgml";

            public static string Yaml => "text/yaml";
        }

        public static class Video
        {
            static Video()
            {
                All = typeof(Video).GetProperties()
                                   .Select(p => p.GetValue(null, null))
                                   .Where(p => p is not null)
                                   .Cast<string>()
                                   .ToList();
            }

            public static IReadOnlyCollection<string> All { get; }

            public static string Threegpp => "video/3gpp";

            public static string H264 => "video/h264";

            public static string Mp4 => "video/mp4";

            public static string Mpeg => "video/mpeg";

            public static string Ogg => "video/ogg";

            public static string Quicktime => "video/quicktime";

            public static string Webm => "video/webm";
        }
    }
}