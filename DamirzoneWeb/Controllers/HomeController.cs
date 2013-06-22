using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;

namespace DamirzoneWeb.Controllers
{
    public class HomeController : Controller
    {
        //[OutputCache(CacheProfile = "Cache1Mouth")]
        public FileContentResult GetImage(int img = 0)
        {
            var files = GetFiles();
            var image = img >= files.Length ? (FileInfo)null : files[img];
            if (image == null)
            {
                return null;
            }
            //Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            //Response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);
            //Response.Cache.SetMaxAge(TimeSpan.FromDays(1));
            return File(ImageContent(image), "image/" + image.Extension.Replace(".", string.Empty));
        }

        //[OutputCache(CacheProfile = "Cache1Mouth")]
        public FileContentResult GetThumbnailImage(int img = 0)
        {
            var files = GetFiles();
            var image = img >= files.Length ? (FileInfo)null : files[img];
            if (image == null)
            {
                return null;
            }
            //Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            //Response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);
            //Response.Cache.SetMaxAge(TimeSpan.FromDays(1));

            return File(GetThumbnailImage(Image.FromStream(image.OpenRead())), "image/" + image.Extension.Replace(".", string.Empty));
        }

        byte[] ImageContent(Image image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.GetBuffer().ToArray();
            }
        }

        byte[] GetThumbnailImage(Image image)
        {
            var width = image.Width; var height = image.Height;
            var panelWidth = 256d; var panelHeight = 256d;
            var coef = Math.Min(panelWidth / width, panelHeight / height);
            if (coef < 1)
            {
                panelWidth = width * coef;
                panelHeight = height * coef;
            }
            else
            {

            }

            var resultImage = image.GetThumbnailImage(Convert.ToInt32(panelWidth), Convert.ToInt32(panelHeight), () =>
            {
                return false;
            }, IntPtr.Zero);
            return ImageContent(resultImage);
        }

        byte[] ImageContent(FileInfo file)
        {
            var content = new byte[file.Length];
            file.OpenRead().Read(content, 0, content.Length);
            return content;
        }

        FileInfo[] GetFiles()
        {
            return new DirectoryInfo("C:/inetpub/wwwroot/damirzone/images").GetFiles().OrderByDescending(fi => fi.CreationTime).ToArray();
        }


        public ActionResult Index()
        {
            return View("Index", GetFiles().Length);
        }
    }
}
