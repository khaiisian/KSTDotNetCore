using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KSTDotNetCore.RestApiWithNLayer.Features.Zodiac
{
    [Route("api/[controller]")]
    [ApiController]
    public class MProverbsController : ControllerBase
    {
        private MProbverbs _data;

        static int toNumber(string alphabet)
        {
            alphabet = alphabet.Replace("က", "1");
            alphabet = alphabet.Replace("ခ", "2");
            alphabet = alphabet.Replace("ဂ", "3");
            alphabet = alphabet.Replace("ဃ", "4");
            alphabet = alphabet.Replace("င", "5");
            alphabet = alphabet.Replace("စ", "6");
            alphabet = alphabet.Replace("ဆ", "7");
            alphabet = alphabet.Replace("ဇ", "8");
            alphabet = alphabet.Replace("ဈ", "9");
            alphabet = alphabet.Replace("ည", "10");
            alphabet = alphabet.Replace("ဋ", "11");
            alphabet = alphabet.Replace("ဌ", "12");
            alphabet = alphabet.Replace("ဍ", "13");
            alphabet = alphabet.Replace("ဎ", "14");
            alphabet = alphabet.Replace("ဏ", "15");
            alphabet = alphabet.Replace("တ", "16");
            alphabet = alphabet.Replace("ထ", "17");
            alphabet = alphabet.Replace("ဒ", "18");
            alphabet = alphabet.Replace("ဓ", "19");
            alphabet = alphabet.Replace("န", "20");
            alphabet = alphabet.Replace("ပ", "21");
            alphabet = alphabet.Replace("ဖ", "22");
            alphabet = alphabet.Replace("ဗ", "23");
            alphabet = alphabet.Replace("ဘ", "24");
            alphabet = alphabet.Replace("မ", "25");
            alphabet = alphabet.Replace("ယ", "26");
            alphabet = alphabet.Replace("ရ", "27");
            alphabet = alphabet.Replace("လ", "28");
            alphabet = alphabet.Replace("ဝ", "29");
            alphabet = alphabet.Replace("သ", "30");
            alphabet = alphabet.Replace("ဟ", "31");
            alphabet = alphabet.Replace("ဠ", "32");
            alphabet = alphabet.Replace("အ", "33");
            return Convert.ToInt32(alphabet);
        }

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
            int id = toNumber(alpha);
            //List<Tbl_Mmproverbs> lst = new List<Tbl_Mmproverbs>();
            return Ok(model.Tbl_MMProverbs.Where(x => x.TitleId == id));
        }


        [HttpGet("{alpha} / {id}")]
        public async Task<IActionResult> QDetail(int id)
        {
            var model = await GetData();
            return Ok(model.Tbl_MMProverbs.Where(x => x.ProverbId == id && x.TitleId == id));

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

    }
}
