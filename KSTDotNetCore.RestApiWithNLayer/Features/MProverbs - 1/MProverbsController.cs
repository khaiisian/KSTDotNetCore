using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KSTDotNetCore.RestApiWithNLayer.Features.Zodiac
{
    [Route("api/[controller]")]
    [ApiController]
    public class MProverbs1Controller : ControllerBase
    {
        private MProbverbs _data;

        private async Task<MProbverbs> GetData()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("MyanmarProverbs.json");
            var model = JsonConvert.DeserializeObject<MProbverbs>(jsonStr);
            return model;
        }

        [HttpGet("Myanmar Alphabets")]
        public async Task<IActionResult> MMAlphabet()
        {
            var model = await GetData();
            return Ok(model.Tbl_MMProverbsTitle);
        }

        //[HttpGet("Myanmar Quotes")]
        //public async Task<IActionResult> MMQuotes()
        //{
        //    var model = await GetData();
        //    return Ok(model.Tbl_MMProverbs);
        //}

        [HttpGet("{alpha}")]

        public async Task<IActionResult> MMQuotes(string alpha)
        {
            var model = await GetData();
            var titleid = model.Tbl_MMProverbsTitle.FirstOrDefault(x=>x.TitleName==alpha)!.TitleId;
            var result = model.Tbl_MMProverbs.Where(x => x.TitleId == titleid);

            List<Tbl_Mmproverb_OA> lst = result.Select(x => new Tbl_Mmproverb_OA{
                TitleId = x.TitleId,
                ProverbId = x.ProverbId,
                ProverbName = x.ProverbName,
            }).ToList();
            return Ok(lst);
        }


        [HttpGet("{alphaid} / {id}")]
        public async Task<IActionResult> QDetail(int alphaid,int id)
        {
            var model = await GetData();
            return Ok(model.Tbl_MMProverbs.Where(x => x.ProverbId == id && x.TitleId == alphaid));

        }




        public class MProbverbs
        {
            public Tbl_Mmproverbstitle[] Tbl_MMProverbsTitle { get; set; }
            public Tbl_Mmproverbs[] Tbl_MMProverbs { get; set; }

        }

        public class Tbl_Mmproverbstitle
        {
            public int TitleId { get; set; }
            public string TitleName { get; set; }
        }

        public class Tbl_Mmproverbs
        {
            public int TitleId { get; set; }
            public int ProverbId { get; set; }
            public string ProverbName { get; set; }
            public string ProverbDesp { get; set; }
        }

        public class Tbl_Mmproverb_OA
        {
            public int TitleId { get; set; }
            public int ProverbId { get; set; }
            public string ProverbName { get; set; }
        }

    }
}
