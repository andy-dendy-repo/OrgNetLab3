using Microsoft.AspNetCore.Mvc;
using OrgNetLab3.Controllers;
using OrgNetLab3.Data.Entity;
using OrgNetLab3.Data.Services;
using System.Drawing;
using System.Drawing.Imaging;

namespace OrgNetLab3.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : BaseController<Lesson>
    {
        private readonly LessonRepository _lessonRepository;
        public LessonController(LessonRepository lessonRepository, IHttpContextAccessor httpContextAccessor) : base(lessonRepository, httpContextAccessor)
        {
            _lessonRepository = lessonRepository;
        }

        [HttpGet("{lessonId}/file")]
        public async Task<IActionResult> GetFile(Guid lessonId)
        {
            var file = await _lessonRepository.GetFile(lessonId);

            if (file == null)
                return BadRequest("No file is attached to  this lesson.");
            else
                return File(file, "text/plain", $"{lessonId}.txt");
        }

        [HttpPost("{lessonId}/file")]
        public async Task<IActionResult> SetFile(Guid lessonId, IFormFile formFile)
        {
            if (!formFile.FileName.EndsWith(".txt"))
                return BadRequest();

            using MemoryStream memoryStream = new MemoryStream();

            await formFile.CopyToAsync(memoryStream);

            await _lessonRepository.SetFile(lessonId, memoryStream.ToArray());

            return Ok();
        }

        [HttpGet("graph/{teacherId}")]
        public async Task<IActionResult> GetGraph(Guid teacherId)
        {
            var list = await _lessonRepository.Get();

            list = list.Where(x => x.TeacherId == teacherId).ToList();

            var byMonth = list.GroupBy(x => x.Start.Month);

            var x = byMonth.Select(x => x.Key);

            var y = byMonth.Select(y => y.Count());



            Dictionary<int, int> points = new Dictionary<int, int>();

            foreach (var point in byMonth)
                points.Add(point.Key, point.Count());


            return File(GetImage(points), "image/jpg");
            
        }

        private byte[] GetImage(Dictionary<int, int> arr)
        {
            int xLen = 600;
            int ylen = 400;

            Bitmap bitmap = new Bitmap(xLen, ylen);

            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, xLen, ylen));

            graphics.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(0, 50, xLen, ylen-50));

            var months =new int [] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var mDel = 50;

            foreach(var month in months)
            {
                graphics.DrawString(month.ToString(), new Font(FontFamily.GenericSansSerif, 20), new SolidBrush(Color.Black), new Point(mDel * month - mDel, 10));
                
                if(arr.ContainsKey(month))
                {
                    graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(mDel * month - mDel, mDel, 40, arr[month] * 20));

                    graphics.DrawString(arr[month].ToString(), new Font(FontFamily.GenericSansSerif, 20), new SolidBrush(Color.Black), new Point(mDel * month - mDel, arr[month] * 20 + 20));
                }
            }

            graphics.Save();

            MemoryStream memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, ImageFormat.Jpeg);

            return memoryStream.ToArray();
        }
    }
}
