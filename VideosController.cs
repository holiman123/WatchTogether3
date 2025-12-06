using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WatchTogether3;

[Route("api/[controller]")]
[ApiController]
public class VideosController : ControllerBase
{
    public VideosController()
    {
        _ = 0;
    }

    [HttpGet("get/{name}")]
    public IResult GetVideoStream(string name)
    {
        var stream = new FileStream($"D:\\WatchTogether3_files\\{name}", FileMode.Open, FileAccess.Read);

        return Results.Stream(stream, "video/mp4", $"{name}", enableRangeProcessing: true);
    }
}
