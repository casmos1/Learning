<%@ WebHandler Language="C#" Class="Captcha" %>

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Web;
using System.Web.SessionState;

public class Captcha : IHttpHandler, IReadOnlySessionState
{
    public void ProcessRequest(HttpContext context)
    {
        var iHeight = 80;
        var iWidth = 190;
        var oRandom = new Random();

        int[] aBackgroundNoiseColor = {150, 150, 150};
        int[] aTextColor = {0, 0, 0};
        int[] aFontEmSizes = {15, 20, 25, 30, 35};

        string[] aFontNames =
        {
            "Comic Sans MS",
            "Arial",
            "Times New Roman",
            "Georgia",
            "Verdana",
            "Geneva"
        };
        FontStyle[] aFontStyles =
        {
            FontStyle.Bold,
            FontStyle.Italic,
            FontStyle.Regular,
            FontStyle.Strikeout,
            FontStyle.Underline
        };
        HatchStyle[] aHatchStyles =
        {
            HatchStyle.BackwardDiagonal, HatchStyle.Cross,
            HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal,
            HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
            HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross,
            HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid,
            HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
            HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard,
            HatchStyle.LargeConfetti, HatchStyle.LargeGrid,
            HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal,
            HatchStyle.LightUpwardDiagonal, HatchStyle.LightVertical,
            HatchStyle.Max, HatchStyle.Min, HatchStyle.NarrowHorizontal,
            HatchStyle.NarrowVertical, HatchStyle.OutlinedDiamond,
            HatchStyle.Plaid, HatchStyle.Shingle, HatchStyle.SmallCheckerBoard,
            HatchStyle.SmallConfetti, HatchStyle.SmallGrid,
            HatchStyle.SolidDiamond, HatchStyle.Sphere, HatchStyle.Trellis,
            HatchStyle.Vertical, HatchStyle.Wave, HatchStyle.Weave,
            HatchStyle.WideDownwardDiagonal, HatchStyle.WideUpwardDiagonal, HatchStyle.ZigZag
        };

//Get Captcha in Session
        var sCaptchaText = context.Session["Captcha"].ToString();

//Creates an output Bitmap
        var oOutputBitmap = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);
        var oGraphics = Graphics.FromImage(oOutputBitmap);
        oGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

//Create a Drawing area
        var oRectangleF = new RectangleF(0, 0, iWidth, iHeight);
        var oBrush = default(Brush);

//Draw background (Lighter colors RGB 100 to 255)
        oBrush = new HatchBrush(aHatchStyles[oRandom.Next
            (aHatchStyles.Length - 1)], Color.FromArgb((oRandom.Next(100, 255)),
                (oRandom.Next(100, 255)), (oRandom.Next(100, 255))), Color.White);
        oGraphics.FillRectangle(oBrush, oRectangleF);

        var oMatrix = new Matrix();
        var i = 0;
        for (i = 0; i <= sCaptchaText.Length - 1; i++)
        {
            oMatrix.Reset();
            var iChars = sCaptchaText.Length;
            var x = iWidth/(iChars + 1)*i;
            var y = iHeight/2;

            //Rotate text Random
            oMatrix.RotateAt(oRandom.Next(-40, 40), new PointF(x, y));
            oGraphics.Transform = oMatrix;

            //Draw the letters with Random Font Type, Size and Color
            oGraphics.DrawString
                (
                    //Text
                    sCaptchaText.Substring(i, 1),
                    //Random Font Name and Style
                    new Font(aFontNames[oRandom.Next(aFontNames.Length - 1)],
                        aFontEmSizes[oRandom.Next(aFontEmSizes.Length - 1)],
                        aFontStyles[oRandom.Next(aFontStyles.Length - 1)]),
                    //Random Color (Darker colors RGB 0 to 100)
                    new SolidBrush(Color.FromArgb(oRandom.Next(0, 100),
                        oRandom.Next(0, 100), oRandom.Next(0, 100))),
                    x,
                    oRandom.Next(10, 40)
                );
            oGraphics.ResetTransform();
        }

        var oMemoryStream = new MemoryStream();
        oOutputBitmap.Save(oMemoryStream, ImageFormat.Png);
        var oBytes = oMemoryStream.GetBuffer();

        oOutputBitmap.Dispose();
        oMemoryStream.Close();

        context.Response.BinaryWrite(oBytes);
        context.Response.End();
    }

    public bool IsReusable
    {
        get { return false; }
    }
}