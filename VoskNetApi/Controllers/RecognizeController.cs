using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VoskNetApi.Application.Models;
using VoskNetApi.Application.Services;

namespace VoskNetApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecognizeController : Controller
    {

        private readonly ITextRecognizeService _fileService;
        public RecognizeController(ITextRecognizeService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<ActionResult<TextRecognized>> UploadFileForTheRecognizeText(IFormFile file)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var res = await Task.Run(() => {return  _fileService.Recognize(file.OpenReadStream(), file.FileName); }, cts.Token);
            return Ok(res);

        }
    }
}
