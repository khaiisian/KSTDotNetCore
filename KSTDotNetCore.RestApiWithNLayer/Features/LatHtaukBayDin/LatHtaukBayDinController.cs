using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KSTDotNetCore.RestApiWithNLayer.Features.LatHtaukBayDin
{
    [Route("api/[controller]")]
    [ApiController]
    public class LatHtaukBayDinController : ControllerBase
    {
        private LatHtaukBayDin _data;

        private async Task <LatHtaukBayDin> GetData()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("data.json");
            var model = JsonConvert.DeserializeObject<LatHtaukBayDin>(jsonStr);
            return model;
        }

        // api/ LatHtaukBaydDin/ question <- endpoint
        [HttpGet("question")]
        public async Task <IActionResult> Questions()
        {
            var model = await GetData();
            return Ok(model.questions);
        }

        [HttpGet()]
        public async Task <IActionResult> NumberList()
        {
            var model = await GetData();
            return Ok(model.numberList);
        }

        [HttpGet("{Qnum}/{no}")]
        public async Task<IActionResult> Ans(int Qnum, int no)
        {
            var model = await GetData();
            return Ok(model.answers.FirstOrDefault(x => x.questionNo == Qnum && x.answerNo == no));
        }

        public class LatHtaukBayDin
        {
            public Question[] questions { get; set; }
            public Answer[] answers { get; set; }
            public string[] numberList { get; set; }
        }

        public class Question
        {
            public int questionNo { get; set; }
            public string questionName { get; set; }
        }

        public class Answer
        {
            public int questionNo { get; set; }
            public int answerNo { get; set; }
            public string answerResult { get; set; }
        }

    }
}
