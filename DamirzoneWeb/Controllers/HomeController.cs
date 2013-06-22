using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using Ninject;
using DamirzoneWeb.Configuration.Helpers;

namespace DamirzoneWeb.Controllers
{
    public class HomeController : Controller
    {
        [Inject]
        public DamirzoneConfigurationHelper ConfigurationHelper {
            get;
            set;
        }

        public FileContentResult GetImage(int img = 0) {
            var files = GetFiles();
            var image = img >= files.Length ? (FileInfo)null : files[img];
            if (image == null) {
                return null;
            }
            return File(ImageContent(image), "image/" + image.Extension.Replace(".", string.Empty));
        }

        public FileContentResult GetThumbnailImage(int img = 0) {
            var files = GetFiles();
            var image = img >= files.Length ? (FileInfo)null : files[img];
            if (image == null) {
                return null;
            }

            return File(GetThumbnailImage(Image.FromStream(image.OpenRead())), "image/" + image.Extension.Replace(".", string.Empty));
        }

        byte[] ImageContent(Image image) {
            using (var stream = new MemoryStream()) {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.GetBuffer().ToArray();
            }
        }

        byte[] GetThumbnailImage(Image image) {
            var width = image.Width;
            var height = image.Height;
            var panelWidth = 256d;
            var panelHeight = 256d;
            var coef = Math.Min(panelWidth / width, panelHeight / height);
            if (coef < 1) {
                panelWidth = width * coef;
                panelHeight = height * coef;
            } else {

            }

            var resultImage = image.GetThumbnailImage(Convert.ToInt32(panelWidth), Convert.ToInt32(panelHeight), () => {
                return false;
            }, IntPtr.Zero);
            return ImageContent(resultImage);
        }

        byte[] ImageContent(FileInfo file) {
            var content = new byte[file.Length];
            file.OpenRead().Read(content, 0, content.Length);
            return content;
        }

        FileInfo[] GetFiles() {
            return new DirectoryInfo(ConfigurationHelper.ImageFolderPath).GetFiles().OrderByDescending(fi => fi.CreationTime).ToArray();
        }


        public ActionResult Index() {
            return View("Index", GetFiles().Length);
        }
    }
}
