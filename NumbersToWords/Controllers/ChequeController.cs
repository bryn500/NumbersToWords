using NumbersToWords.Core.Entities;
using NumbersToWords.Core.Services.ChequeWriter;
using NumbersToWords.Core.Services.NumbersToWords;
using NumbersToWords.Core.Services.Utility;
using NumbersToWords.Models.Cheque;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;

namespace NumbersToWords.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.Disabled)]
    public class ChequeController : BaseController
    {
        [HttpGet]
        [Route("")]
        public ActionResult Form()
        {
            var model = new Cheque();

            return View(model);
        }

        [HttpPost]
        [Route("")]
        [ValidateAntiForgeryToken]
        public ActionResult Form(Cheque model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var utilityService = new UtilityService();
            var numbersToWordsService = new NumbersToWordsService();
            var chequeWriterService = new ChequeWriterService(numbersToWordsService, utilityService);

            var result = chequeWriterService.WriteCheque(model, new ChequeImage()
            {
                NameLocation = new PointF(70f, 125f),

                AmountInWordsLocation = new PointF(135f, 170f),
                AmountInWordsOverflowLocation = new PointF(60f, 210f),
                WrapCharacter = 40,

                DateLocation = new PointF(620f, 92f),

                AmountLocation = new PointF(610f, 158f),

                ImageFormat = ImageFormat.Jpeg,
                ImagePath = Server.MapPath("~/Content/img/cheques/example.jpg")
            });

            return new FileStreamResult(new MemoryStream(result.Item), "image/jpeg");
        }
    }
}