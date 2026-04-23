using FFMediaToolkit.Decoding;
using FFMediaToolkit.Graphics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using FFMediaToolkit;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

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

    [HttpGet("getframe/{name}-{time}")]
    public unsafe IResult GetVideoFrame(string name, double time)
    {
        MediaFile video = MediaFile.Open($"D:\\WatchTogether3_files\\{name}");

        ImageData frame = video.Video.GetFrame(TimeSpan.FromSeconds(time));
        MemoryStream ms = new MemoryStream();
        fixed (byte* ptr = frame.Data)
        {
            Bitmap bitmap = new Bitmap(
                frame.ImageSize.Width, 
                frame.ImageSize.Height, 
                frame.Stride, 
                PixelFormat.Format24bppRgb, 
                (IntPtr)ptr);

            bitmap.Save(ms, ImageFormat.Jpeg);
        }

        ms.Position = 0;
        return Results.File(ms, "image/jpeg", $"{name}_f{time}.jpeg", enableRangeProcessing: true);
    }
}
