using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using  Microsoft.Office.Interop.PowerPoint;
using  Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;
using System.Windows.Forms;

namespace AutoCreatePPT
{
   public class clsPpt
{
    // Fields
    public const float Ffrom = 10f;
    private tempFont FontBiaoTi;
    private tempFont FontContent;
    private tempFont FontSubtitle;
    private tempFont FontTitle;
    public const float Fstep = 1f;
    public const float Fto = 25f;
    public const float LineSepFrom = 1f;
    public const float LineSepTo = 1.5f;
    public const float LineStep = 0.1f;
    public Microsoft.Office.Interop.PowerPoint.Shape objShape;
    public   Slide objSlide;
    private picTotal Pic_Word;
    private picTotal[] PicMedia_Pause;
    private picTotal[] PicMedia_Play;
    private picTotal[] PicMedia_Stop;
    private picTotal[] PicNav_Home;
    private picTotal[] PicNav_Next;
    private picTotal[] PicNav_Prev;
    private picTotal[] PicNav_Return;
    private picTotal[] PicNav_Up;
    private picTotal picRSJ1;
    private picTotal picRSJ2;
    private picTotal picRSJ3;
    public Microsoft.Office.Interop.PowerPoint.Application ppApp;
    public Presentation prsPres;
    private picTotal TextBoxBiaoTi;
    private picTotal TextBoxContent;
    private picTotal TextBoxContent1;
    private picTotal TextBoxSubtitle;
    private picTotal txtTitle;

    // Methods
    public void AddAnimate(string strPptName, string[] SlideIndex)
    {
        this.prsPres = this.OpenPres(strPptName, false);
        for (int i = 1; i <= this.prsPres.Slides.Count ; i++)
        {
            foreach (Microsoft.Office.Interop.PowerPoint.Shape shape in this.prsPres.Slides [i].Shapes)
            {
                if ((shape.HasTextFrame == MsoTriState.msoTrue) && (shape.TextFrame.TextRange.Text.IndexOf("Answer:") == 0))
                {
                    shape.AnimationSettings.EntryEffect =PpEntryEffect.ppEffectAppear;//.set_EntryEffect(0xf04);
                    shape.AnimationSettings.TextLevelEffect=PpTextLevelEffect.ppAnimateByAllLevels ;//.set_TextLevelEffect(0x10);
                }
            }
        }
        MessageBox.Show("ok");
    }

    private void AddContentLastLevel(Microsoft.Office.Interop.PowerPoint.Shape tempShape, string TextString, string FontName, float FontSize, int FontColor)
    {
        tempShape.TextFrame.TextRange.ParagraphFormat.Alignment =PpParagraphAlignment.ppAlignRight;//.get_ParagraphFormat().set_Alignment(3);
        tempShape.TextFrame.TextRange.Text =TextString ;
        tempShape.TextFrame.TextRange.Font.Name =FontName;
        tempShape.TextFrame.TextRange.Font.Size =FontSize ;
        tempShape.TextFrame.TextRange.Font.Bold = MsoTriState.msoTrue;
        tempShape.TextFrame.TextRange.Font.Color.RGB =FontColor ;
    }

    private void AddHylinkLastLevel(string[] LastLevel, ref Slide CurrentSlide)
    {
        int num;
        string textString = "";
        for (num = 0; num < LastLevel.Length; num++)
        {
            if (LastLevel[0] == null)
            {
                return;
            }
            if (textString == "")
            {
                textString = LastLevel[num].Trim();
            }
            else
            {
                textString = textString + "\r\n" + LastLevel[num].Trim();
            }
        }
       Microsoft.Office.Interop.PowerPoint. Shape objShape = CurrentSlide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.TextBoxContent1.Left, this.TextBoxContent1.Top, this.TextBoxContent1.Width, this.TextBoxContent1.Height);
        objShape.Name = "TempTitle2" ;
        this.ADDTextBoxContent(objShape , textString, false, this.FontContent.FontName, this.FontContent.FontSize, this.FontContent.FontColor, this.FontContent.FontBold, PpParagraphAlignment.ppAlignLeft , 0.05f);
        textString = "";
        int num2 = 1;
        for (num = 0; num < LastLevel.Length; num++)
        {
            objShape.TextFrame.TextRange.Characters(num2, LastLevel[num].Length).ActionSettings[PpMouseActivation.ppMouseClick].Hyperlink.Address="" ;
            objShape.TextFrame.TextRange.Characters(num2, LastLevel[num].Length).ActionSettings[PpMouseActivation.ppMouseClick].Hyperlink.SubAddress =LastLevel[num];
            objShape.TextFrame.TextRange.Characters(num2, LastLevel[num].Length).ActionSettings[PpMouseActivation.ppMouseClick].Hyperlink.TextToDisplay =LastLevel[num];
            num2 = (num2 + LastLevel[num].Length) + 1;
        }
    }

    public void AddMediaButton(string strUnit)
    {
        string str = ConfigurationManager.AppSettings["CommerceEnglish"];
        string[] strArray = new string[] { "dictation", "text", "exercise" };
        this.prsPres = this.OpenPres(str + "unit" + strUnit + ".ppt", false);
        this.prsPres.Slides[1].Shapes[1].Visible = MsoTriState.msoFalse;
        string str4 = str;
        this.prsPres.SlideMaster.Shapes.AddOLEObject(722.875f, 0f, 1f, 1f, "MediaPlayer.MediaPlayer.1", "", 0, "", 0, "", 0);
        for (int i = 7; i < this.prsPres.Slides.Count ; i++)
        {
            string str2 = this.prsPres.Slides [i].Shapes["SlideTitle"].TextFrame.TextRange.Text .Trim();
            string str3 = str2;
            str2 = str2.ToLower();
            if (str2 == "writing")
            {
                break;
            }
            for (int j = 0; j < strArray.Length; j++)
            {
                if (str2.IndexOf(strArray[j]) > -1)
                {
                    this.InsertPictureOperation(this.prsPres.Slides [i], str4 + @"button\stop.gif", "", "stop", 150.25f, 502.5f, 26.375f, 26.375f, true, "Mstop", "");
                    this.InsertPictureOperation(this.prsPres.Slides[i], str4 + @"button\pause.gif", "", "pause", 178.625f, 502.5f, 26.375f, 26.375f, true, "Mpause", "");
                    this.InsertPictureOperation(this.prsPres.Slides[i], str4 + @"button\play.gif", "", "play", 121.875f, 502.5f, 26.375f, 26.375f, true, "Mplay", "");
                    break;
                }
            }
        }
        MessageBox.Show("ok");
    }
        //2018-07-05,正文内容为超链接
        private void AddMuitiTextBox(string[] title, ref Slide CurrentSlide, string PicturePath="")
        {
            tempFont font;
            int length = title.Length;
            font.FontName = "Arial";
            font.FontColor = 0xff;

            font.FontBold = MsoTriState.msoTrue;// -1;
            font.FontSize = 32f;
            float num = 104.875f;//left
            float num2 = 115f;// 78.375f;//top
            float num3 = 534f;//width
            float num4 = 55.625f;//height
            float num5 =55 ;// 68f;
            float roundButtonWidth = 45.87496f;
           
             
            if (length <=4)
            {
                //num = 99.125f;
                //num2 = 173.625f;
                //num3 = 590f;// 534f;
                //num4 = 55.62496f;// 70f;
                //num5 = 55.625f;// 105.375f;
                font.FontSize = 40f;//44
            }
             
            for (int i = 0; i < length; i++)
            {
                if (PicturePath != "")
                {
                    if (!PicturePath.EndsWith("\\"))
                        PicturePath = PicturePath + "\\";
                    //圆按钮
                    //if (i % 2 == 1)
                    //    this.InsertPicture(CurrentSlide, PicturePath + "roundGreen.gif", num, num2+5, roundButtonWidth, roundButtonWidth, true,"round"+(i+1));

                    //else
                    //    this.InsertPicture(CurrentSlide, PicturePath + "roundBlue.gif", num, num2 + 5, roundButtonWidth, roundButtonWidth, true,"round" + (i + 1));
                     

                }
                //文本框
                Microsoft.Office.Interop.PowerPoint.Shape tempShape = CurrentSlide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, num+roundButtonWidth, num2, num3, num4);
                tempShape.Name = "txtTitle" + (i + 1);
                this.ADDTextBoxContent(tempShape, title[i], false, font.FontName, font.FontSize, font.FontColor, font.FontBold, PpParagraphAlignment.ppAlignLeft, 0f);
                tempShape.TextFrame.TextRange.ActionSettings[PpMouseActivation.ppMouseClick].Action = PpActionType.ppActionHyperlink;//  .get_ActionSettings().get_Item(1).get_Hyperlink().set_Address("");
                tempShape.TextFrame.TextRange.ActionSettings[PpMouseActivation.ppMouseClick].Hyperlink.SubAddress = title[i];

                num2 += num5;
            }
            //if (CurrentSlide.SlideIndex == 2)
            //{
            //    CurrentSlide.Shapes["round1"].Left = num;
            //    CurrentSlide.Shapes["round1"].Top = CurrentSlide.Shapes["txtTitle1"].Top+5;
            //}
        }

    private void AddNaviButton(ref Slide CurrentSlide, string commonImagePath, string strContentUp)
    {
        this.InsertPictureOperation(CurrentSlide, commonImagePath + @"\return.gif", "", "return", this.PicNav_Return[1].Left, this.PicNav_Return[1].Top, this.PicNav_Return[1].Width, this.PicNav_Return[1].Height, false, "", "R");
        this.InsertPictureOperation(CurrentSlide, commonImagePath + @"\prev.gif", "", "prev", this.PicNav_Prev[1].Left, this.PicNav_Prev[1].Top, this.PicNav_Prev[1].Width, this.PicNav_Prev[1].Height, false, "", "P");
        this.InsertPictureOperation(CurrentSlide, commonImagePath + @"\up.gif", strContentUp, "up", this.PicNav_Up[1].Left, this.PicNav_Up[1].Top, this.PicNav_Up[1].Width, this.PicNav_Up[1].Height, false, "", "");
        this.InsertPictureOperation(CurrentSlide, commonImagePath + @"\next.gif", "", "next", this.PicNav_Next[1].Left, this.PicNav_Next[1].Top, this.PicNav_Next[1].Width, this.PicNav_Next[1].Height, false, "", "N");
        this.InsertPictureOperation(CurrentSlide, commonImagePath + @"\home.gif", strContentUp, "home", this.PicNav_Home[1].Left, this.PicNav_Home[1].Top, this.PicNav_Home[1].Width, this.PicNav_Home[1].Height, false, "", "");
    }
    
         //new is used2018-07-05
    private void AddNaviButton(ref Slide CurrentSlide, string commonImagePath, string strContentUp, string ContentHome)
    {
        this.InsertPictureOperation(CurrentSlide, commonImagePath + @"\return.gif", "", "return", this.PicNav_Return[1].Left, this.PicNav_Return[1].Top, this.PicNav_Return[1].Width, this.PicNav_Return[1].Height, false, "", "R");
        this.InsertPictureOperation(CurrentSlide, commonImagePath + @"\prev.gif", "", "prev", this.PicNav_Prev[1].Left, this.PicNav_Prev[1].Top, this.PicNav_Prev[1].Width, this.PicNav_Prev[1].Height, false, "", "P");
        this.InsertPictureOperation(CurrentSlide, commonImagePath + @"\up.gif", strContentUp, "up", this.PicNav_Up[1].Left, this.PicNav_Up[1].Top, this.PicNav_Up[1].Width, this.PicNav_Up[1].Height, false, "", "");
        this.InsertPictureOperation(CurrentSlide, commonImagePath + @"\next.gif", "", "next", this.PicNav_Next[1].Left, this.PicNav_Next[1].Top, this.PicNav_Next[1].Width, this.PicNav_Next[1].Height, false, "", "N");
        this.InsertPictureOperation(CurrentSlide, commonImagePath + @"\home.gif", ContentHome, "home", this.PicNav_Home[1].Left, this.PicNav_Home[1].Top, this.PicNav_Home[1].Width, this.PicNav_Home[1].Height, false, "", "");
    }
        //2018-07-05
        public Slide ADDNewSlide(string SlideTitle, int SldIndex,bool hide=false)
        {
            Slide slide;
            if (SldIndex == 0)
            {
                slide = this.prsPres.Slides.Add(this.prsPres.Slides.Count + 1, PpSlideLayout.ppLayoutContentWithCaption);
            }
            else if (SldIndex > 0)
            {
                slide = this.prsPres.Slides.Add(SldIndex, PpSlideLayout.ppLayoutTitle);
            }
            else
                slide = prsPres.Slides[1];
            if (hide)
                slide.SlideShowTransition.Hidden = MsoTriState.msoTrue;
            slide.Shapes[1].Name = "SlideTitle";
            SlideTitle = this.RetSlideTitle(SlideTitle);
            slide.Shapes[1].TextFrame.TextRange.Text = SlideTitle;
            slide.Shapes[1].Visible = MsoTriState.msoFalse;
            while (slide.Shapes.Count > 1)
                slide.Shapes[slide.Shapes.Count].Delete();
            return slide;
        }

    private void AddSlideTitle(ref Slide slide, string Title, string UnitTitle, int level)
    {
        Microsoft.Office.Interop.PowerPoint.Shape objShape = slide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.TextBoxBiaoTi.Left, this.TextBoxBiaoTi.Top, this.TextBoxBiaoTi.Width, this.TextBoxBiaoTi.Height);
           objShape.Name ="Temp1" ;
        this.ADDTextBoxContent(objShape , Title, false, this.FontBiaoTi.FontName, this.FontBiaoTi.FontSize, this.FontBiaoTi.FontColor, this.FontBiaoTi.FontBold,PpParagraphAlignment.ppAlignLeft, 0.05f);
        Microsoft.Office.Interop.PowerPoint.Shape  objShape1 = slide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.txtTitle.Left, this.txtTitle.Top, this.txtTitle.Width, this.txtTitle.Height);
        objShape1.Name ="TitleYSJ" ;
        this.AddContentLastLevel(objShape1 , UnitTitle, this.FontTitle.FontName, this.FontTitle.FontSize, this.FontTitle.FontColor);
    }
//2018-07-05
    private void AddSlideTitleNewCommon(ref Slide slide, string Title, string pic)
    {
        Microsoft.Office.Interop.PowerPoint.Shape objShape =slide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.TextBoxBiaoTi.Left, this.TextBoxBiaoTi.Top, this.TextBoxBiaoTi.Width, this.TextBoxBiaoTi.Height);
        objShape.Name ="Temp1" ;

        this.ADDTextBoxContent(objShape, Title, false, this.FontBiaoTi.FontName, this.FontBiaoTi.FontSize, this.FontBiaoTi.FontColor, this.FontBiaoTi.FontBold, PpParagraphAlignment.ppAlignLeft, 0.05f);
        //去掉右上脚的小图标2017-09-06
        //this.InsertPicture(slide, pic, picRSJ1.Left, picRSJ1.Top, this.picRSJ1.Width, this.picRSJ1.Height, false);
    }
       //根据名称获取shape
  private   Microsoft.Office.Interop.PowerPoint. Shape   GetShapeByName(Slide objSlide,string shapeName)
  {
      Microsoft.Office.Interop.PowerPoint.Shape objShape=null;
      foreach (Microsoft.Office.Interop.PowerPoint.Shape tmpShape in objSlide.Shapes )
          if (tmpShape.Name ==shapeName)
          {
              objShape = tmpShape;
              break;
          }
      return objShape;

  }
    private void AddSonyPhonetics(string strPptFile, string[] strSldIndex)
    {
        this.prsPres = this.OpenPres(strPptFile, false);
        for (int i = 0; i < strSldIndex.Length; i++)
        {
            int startIndex = 0;
            int num4 = 0;
            Microsoft.Office.Interop.PowerPoint.Shape shape = GetShapeByName(prsPres.Slides[int.Parse(strSldIndex[i])], "Temp3");//this.prsPres.Slides [int.Parse(strSldIndex[i])].Shapes.get_Item("Temp3");
            string str = shape.TextFrame.TextRange .Text.Trim();
            int index = 0;
            while (index > -1)
            {
                index = str.IndexOf("/", startIndex) + 1;
                startIndex = str.IndexOf("/", index);
                shape.TextFrame.TextRange .Characters((index + 1) - num4, startIndex - index).Font.Name = "Kingsoft Phonetic Plain" ;
                index = str.IndexOf("\r", (int) (startIndex + 1));
                num4++;
                startIndex = index + 1;
            }
        }
        MessageBox.Show("ok");
    }

    public void ADDTextBoxContent(Microsoft.Office.Interop.PowerPoint.Shape tempShape, string TextString, bool FlagSign, string FontName, float FontSize, int FontColor, MsoTriState FontBold, PpParagraphAlignment align, float LineBef)
    {
        if ((TextString != "") && (TextString.Length > 1))
        {
            tempShape.TextFrame.AutoSize =PpAutoSize.ppAutoSizeNone;//.set_AutoSize(0);
            if ((((TextString.Substring(0, 2) == "A.") || (TextString.Substring(0, 2) == "B.")) || (TextString.Substring(0, 2) == "C.")) || (TextString.Substring(0, 2) == "D."))
            {
                TextString = "    " + TextString;
            }
            tempShape.TextFrame.TextRange .ParagraphFormat.LineRuleWithin=MsoTriState.msoTrue;//.set_LineRuleWithin(-1);
            tempShape.TextFrame.TextRange.ParagraphFormat.SpaceWithin=1f;//.set_SpaceWithin(1f);
            tempShape.TextFrame.TextRange.ParagraphFormat.LineRuleBefore = MsoTriState.msoTrue;//.set_LineRuleBefore(-1);
            tempShape.TextFrame.TextRange.ParagraphFormat.SpaceBefore=LineBef ;
            tempShape.TextFrame.TextRange.ParagraphFormat.LineRuleAfter = MsoTriState.msoTrue;
            tempShape.TextFrame.TextRange.ParagraphFormat.SpaceAfter=0f ;
            tempShape.TextFrame.TextRange.ParagraphFormat.Alignment=align ;
            if (FlagSign)
            {
                tempShape.TextFrame.TextRange.Paragraphs(1, 1).ParagraphFormat.Bullet.Style =PpNumberedBulletStyle.ppBulletAlphaLCPeriod;//.get_Bullet().set_Style(3);
                tempShape.TextFrame.TextRange.Paragraphs(1, 1).ParagraphFormat.Bullet.Font.Size =FontSize ;
                tempShape.TextFrame.TextRange.Paragraphs(1, 1).ParagraphFormat.Bullet.Font.Color.RGB = 0x3399ff;//.set_RGB(0x3399ff);
            }
            tempShape.TextFrame.TextRange.Text =TextString.Replace("\t", "") ;
            tempShape.TextFrame.TextRange.Font.Name =FontName ;
            tempShape.TextFrame.TextRange.Font.Size =FontSize ;
            tempShape.TextFrame.TextRange.Font.Bold =FontBold ;
            tempShape.TextFrame.TextRange.Font.Color.RGB =  FontColor;//.get_Font().get_Color().set_RGB(FontColor);
            if (FlagSign)
            {
                //tempShape.TextFrame.TextRange.ParagraphFormat .Bullet.;//.get_Bullet().set_Visible(0);
                tempShape.TextFrame.TextRange.ParagraphFormat.Bullet.Font.Size =FontSize ;
                tempShape.TextFrame.TextRange.ParagraphFormat.Bullet.Character=0x2022 ;
                //tempShape.TextFrame.TextRange.ParagraphFormat.Bullet.v.set_Visible(-1);
            }
            string str = tempShape.Name ;
            if ((str != null) && ((str == "Temp3") || (str == "TempTitle2")))
            {
                this.ChangeTextSize(tempShape, tempShape.Width , tempShape.Height );
            }
        }
    }

    private void AddTextContent(string strText, string strSubTitle, ref Slide tmpSlide)
    {
       Microsoft.Office.Interop.PowerPoint. Shape shape;
        if (strSubTitle != "")
        {
            shape = tmpSlide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.TextBoxContent.Left, this.TextBoxContent.Top, this.TextBoxContent.Width, this.TextBoxContent.Height);
        }
        else
        {
            shape = tmpSlide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.TextBoxContent1.Left, this.TextBoxContent1.Top, this.TextBoxContent1.Width, this.TextBoxContent1.Height);
        }
        shape.Name ="shpBody" ;
        this.ADDTextBoxContent(shape , strText, false, this.FontContent.FontName, this.FontContent.FontSize, this.FontContent.FontColor, this.FontContent.FontBold, PpParagraphAlignment.ppAlignLeft, 0.05f);
    }

    public void AddWav(string strUnit, string[] SlideIndex, string strPptName, string strWavPath)
    {
        string str = "";
        string str2 = "";
        strPptName = strPptName.Trim() + strUnit + ".ppt";
        this.prsPres = this.OpenPres(strPptName, false);
        for (int i = 0; i < SlideIndex.Length; i++)
        {
            Microsoft.Office.Interop.PowerPoint.Shape shape =GetShapeByName ( this.prsPres.Slides [short.Parse(SlideIndex[i])],"Temp3");
            string str3 = shape.TextFrame.TextRange .Text;
            int num4 = 0;
            int startIndex = 0;
            while (startIndex > -1)
            {
                int index = str3.IndexOf("  ", startIndex);
                if (index > 0)
                {
                    str2 = str3.Substring(startIndex + 1, index - startIndex);
                    if (str2.EndsWith("."))
                    {
                        str = str2.Substring(0, str2.Length - 3);
                    }
                    else if (str2.EndsWith("?"))
                    {
                        str = str2.Substring(0, str2.Length - 1).Trim();
                    }
                    else
                    {
                        str = str2;
                    }
                }
                if (str.IndexOf("/") > 0)
                {
                    str = str.Substring(0, str.IndexOf("/")).Trim();
                }
                str = str.Replace(" ", "-");
                shape.TextFrame.TextRange.Characters((startIndex + 1) - num4, str2.Length).ActionSettings[PpMouseActivation.ppMouseClick].Action =PpActionType.ppActionNone;//  .get_ActionSettings().get_Item(1).set_Action(0);
                shape.TextFrame.TextRange.Characters((startIndex + 1) - num4, str2.Length).ActionSettings[PpMouseActivation.ppMouseClick].SoundEffect.ImportFromFile(string.Concat(new object[] { strWavPath, short.Parse(strUnit.Substring(strUnit.Length - 2, 2)), @"\", str, ".wav" }));
                    //.a\\;//.get_SoundEffect().ImportFromFile(string.Concat(new object[] { strWavPath, short.Parse(strUnit.Substring(strUnit.Length - 2, 2)), @"\", str, ".wav" }));
                shape.TextFrame.TextRange.Characters((startIndex + 1) - num4, str2.Length).ActionSettings[PpMouseActivation.ppMouseClick].AnimateAction=MsoTriState.msoFalse;// AnimateAction(0);
                num4++;
                startIndex = str3.IndexOf("\r", (int) (index + 1));
                if (startIndex > -1)
                {
                    startIndex += 2;
                }
            }
        }
        MessageBox.Show("ok");
    }

    public void ChangeBackPicture(string strPpt, string commonImagePath, int endUnit)
    {
        for (int i = 1; i <= endUnit; i++)
        {
            string str;
            if (i < 10)
            {
                str = "0" + i.ToString();
            }
            else
            {
                str = i.ToString();
            }
            this.prsPres = this.OpenPres(strPpt + str + ".ppt", false);
            string str2 = "01" + str;
            this.InsertBackGround(this.prsPres.Slides[2] , commonImagePath + "unit" + str + ".jpg");
            this.prsPres.Save();
            this.prsPres.Close();
        }
        this.ppApp.Quit();
        MessageBox.Show("ok");
    }

    private void ChangeTextSize(Microsoft.Office.Interop.PowerPoint.Shape txtBox, float Width, float Height)
    {
        float num2 = 0f;
        float num3 = 0f;
        txtBox.TextFrame.TextRange.ParagraphFormat.LineRuleBefore =MsoTriState .msoTrue;
        txtBox.TextFrame.TextRange.ParagraphFormat.SpaceBefore=0.5f ;
        float num = 10f;
        while (num <= 25f)
        {
            txtBox.TextFrame.TextRange.Font.Size =num ;
            float num5 = txtBox.TextFrame.TextRange.Characters(1, 1).BoundHeight;
            num2 = txtBox.TextFrame.TextRange .Lines(-1, -1).Count  + (txtBox.TextFrame.TextRange .Paragraphs(-1, -1).Count / 2);
            num3 = (long) (Height / num5);
            if (num2 >= num3)
            {
                break;
            }
            num++;
        }
        if ((num2 > num3) && (num > 20f))
        {
            txtBox.TextFrame.TextRange.Font.Size =num - 1f ;
        }
        if ((num > 25f) && ((num2 * 1.5) < num3))
        {
            for (float i = 1f; i <= 1.5f; i += 0.1f)
            {
                txtBox.TextFrame.TextRange.ParagraphFormat.LineRuleWithin=MsoTriState.msoTrue ;//.set_LineRuleWithin(-1);
                txtBox.TextFrame.TextRange.ParagraphFormat.SpaceWithin=i ;
                num3 = ((float)((long)Height)) / txtBox.TextFrame.TextRange.Characters(1, 1).BoundHeight;
                if (num2 >= num3)
                {
                    txtBox.TextFrame.TextRange.ParagraphFormat.LineRuleWithin =MsoTriState.msoTrue ;//.set_LineRuleWithin(-1);
                    txtBox.TextFrame.TextRange.ParagraphFormat.SpaceWithin=i - 0.1f ;
                    break;
                }
            }
        }
    }

    public void CreateCommerceEnglish(string unitID)
    {
        int k = 0;
        int num4 = 0;
        int num5 = 0;
        int num6 = 0;
        int num7 = 0;
        int wdTableCount = 0;
        string unitTitle = "";
        string[] level = null;
        string[] title = null;
        string[] strArray3 = null;
        this.SetStyleOfCommerceEnglish();
        
        string str3 = ConfigurationManager.AppSettings["CommerceEnglish"];
        object obj2 = str3 + @"Documents\unit" + unitID + ".doc";
        string str4 = str3 + @"commonImages\";
        this.prsPres = this.OpenPres(str3 + "商务英语.pot", true);
        int num2 = 0;
        Microsoft.Office.Interop.Word.Application application = new Microsoft.Office.Interop.Word.Application();
        object missing = Type.Missing;
        Document wordOpen = application.Documents.Open(ref obj2, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
        string str6 = "";
        for (int i = 1; i <= wordOpen.Paragraphs.Count ; i++)
        {
            string slideTitle = wordOpen.Paragraphs[i].Range .Text.Trim();
            if (slideTitle != "")
            {
                switch (((Style) wordOpen.Paragraphs[i].get_Style()).NameLocal.ToUpper ())
                {
                    case "UNIT":
                        unitTitle = slideTitle;
                        this.objSlide = this.ADDNewSlide("unit" + unitID, 0);
                        this.InsertBackGround(this.objSlide, str4 + "unit" + unitID + ".jpg");
                        goto Label_04A1;

                    case "标题 1":
                        if (title != null)
                        {
                            this.objSlide = this.prsPres.Slides[num5];
                            this.AddMuitiTextBox(title, ref this.objSlide);
                        }
                        this.objSlide = this.ADDNewSlide(slideTitle, 0);
                        num5 = this.objSlide.SlideIndex;
                        this.InsertBackGround(this.objSlide, str4 + slideTitle + ".jpg");
                        this.SaveTitle(ref level, ref num2, slideTitle);
                        title = null;
                        k = 0;
                        this.AddSlideTitle(ref this.objSlide, slideTitle, unitTitle, 1);
                        this.AddNaviButton(ref this.objSlide, str4 + @"button\", "unit" + unitID, "unit" + unitID);
                        this.DealWithContent(ref i, ref wordOpen, ref wdTableCount);
                        goto Label_04A1;

                    case "标题 2":
                        if (strArray3 != null)
                        {
                            this.objSlide = this.prsPres.Slides [num6];
                            this.AddMuitiTextBox(strArray3, ref this.objSlide);
                        }
                        this.objSlide = this.ADDNewSlide(slideTitle, 0);
                        this.InsertBackGround(this.objSlide, str4 + level[num2 - 1] + ".jpg");
                        num6 = this.objSlide.SlideIndex ;
                        this.SaveTitle(ref title, ref k, slideTitle);
                        strArray3 = null;
                        num4 = 0;
                        this.AddSlideTitle(ref this.objSlide, slideTitle, unitTitle, 2);
                        this.AddNaviButton(ref this.objSlide, str4 + @"button\", level[num2 - 1], "unit" + unitID);
                        str6 = level[num2 - 1];
                        this.DealWithContent(ref i, ref wordOpen, ref wdTableCount);
                        goto Label_04A1;

                    case "标题 3":
                        this.objSlide = this.ADDNewSlide(slideTitle, 0);
                        this.InsertBackGround(this.objSlide, str4 + level[num2 - 1] + ".jpg");
                        num7 = this.objSlide.SlideIndex ;
                        this.SaveTitle(ref strArray3, ref num4, slideTitle);
                        this.AddSlideTitle(ref this.objSlide, slideTitle, unitTitle, 3);
                        this.AddNaviButton(ref this.objSlide, str4 + @"button\", title[k - 1], "unit" + unitID);
                        str6 = title[k - 1];
                        this.DealWithContent(ref i, ref wordOpen, ref wdTableCount);
                        goto Label_04A1;

                    case "标题 4":
                        this.objSlide = this.ADDNewSlide(str6 + slideTitle, 0);
                        this.InsertBackGround(this.objSlide, str4 + level[num2 - 1] + ".jpg");
                        this.AddSlideTitle(ref this.objSlide, str6, unitTitle, 3);
                        this.AddNaviButton(ref this.objSlide, str4 + @"button\", str6, "unit" + unitID);
                        this.DealWithContent(ref i, ref wordOpen, ref wdTableCount);
                        break;
                }
            Label_04A1:;
            }
        }
        if (title != null)
        {
            this.objSlide = this.prsPres.Slides [num5];
            this.AddMuitiTextBox(title, ref this.objSlide);
        }
        this.objSlide = this.prsPres.Slides[1]; 
        this.AddMuitiTextBox(level, ref this.objSlide);
        this.objSlide = this.ADDNewSlide("Thank You", 0);
        this.InsertBackGround(this.objSlide, str4 + "thank you.jpg");
        this.objSlide = this.ADDNewSlide("Credits", 0);
        this.InsertBackGround(this.objSlide, str4 + "制作人员.jpg");
        MessageBox.Show("ok");
        object obj4 = Type.Missing ;
        if (wordOpen != null)
        {
            wordOpen.Close(ref obj4, ref obj4, ref obj4);
        }
        application.Quit(ref obj4, ref obj4, ref obj4);
    }

    public void CreateComputerEnglish(string JiBie, string unitID)
    {
        string[] destinationArray = null;
        int[] numArray = null;
        string[] sourceArray = null;
        string[] lastLevel = null;
        int index = 0;
        string[] strArray4 = null;
        int[] numArray2 = null;
        int num4 = 0;
        int num5 = 0;
        int num6 = 0;
        int num7 = 0;
        string[] strArray5 = null;
        int num9 = 0;
        this.Pic_Word.Left = 513.125f;
        this.Pic_Word.Top = 247.375f;
        this.Pic_Word.Width = 187.125f;
        this.Pic_Word.Height = 187.125f;
        this.picRSJ1.Left = 388.375f;
        this.picRSJ1.Top = 0f;
        this.picRSJ1.Width = 331.625f;
        this.picRSJ1.Height = 20.5f;
        this.picRSJ2.Left = 382.625f;
        this.picRSJ2.Top = 20.5f;
        this.picRSJ2.Width = 337.375f;
        this.picRSJ2.Height = 22.75f;
        this.picRSJ3.Left = 303.25f;
        this.picRSJ3.Top = 43.25f;
        this.picRSJ3.Width = 416.75f;
        this.picRSJ3.Height = 496.75f;
        this.TextBoxBiaoTi.Left = 172.875f;
        this.TextBoxBiaoTi.Top = 48.875f;
        this.TextBoxBiaoTi.Width = 547.125f;
        this.TextBoxBiaoTi.Height = 51f;
        this.TextBoxSubtitle.Left = 172.875f;
        this.TextBoxSubtitle.Top = 98.25f;
        this.TextBoxSubtitle.Width = 547.125f;
        this.TextBoxSubtitle.Height = 45.25f;
        this.TextBoxContent.Left = 195.875f;
        this.TextBoxContent.Top = 154f;
        this.TextBoxContent.Width = 522.125f;
        this.TextBoxContent.Height = 312f;
        this.TextBoxContent1.Left = 195.875f;
        this.TextBoxContent1.Top = 97f;
        this.TextBoxContent1.Width = 522.125f;
        this.TextBoxContent1.Height = 330f;
        this.FontBiaoTi.FontName = "Arial";
        this.FontBiaoTi.FontSize = 40f;
        this.FontBiaoTi.FontColor = 0xccff33;
        this.FontBiaoTi.FontBold = MsoTriState.msoTrue ;
        this.FontBiaoTi.FontItalic = false;
        this.FontBiaoTi.FontUnderline = false;
        this.FontSubtitle.FontName = "Arial";
        this.FontSubtitle.FontSize = 23f;
        this.FontSubtitle.FontColor = 0x3399ff;
        this.FontSubtitle.FontBold = MsoTriState.msoTrue ;
        this.FontSubtitle.FontItalic = false;
        this.FontSubtitle.FontUnderline = false;
        this.FontContent.FontName = "Arial";
        this.FontContent.FontSize = 20f;
        this.FontContent.FontColor = 0xffffff;
        this.FontContent.FontBold = 0;
        this.FontContent.FontItalic = false;
        this.FontContent.FontUnderline = false;
        this.FontTitle.FontName = "Arial";
        this.FontTitle.FontSize = 14f;
        this.FontTitle.FontColor = 0xe3e0bb;
        this.FontTitle.FontBold = 0;
        this.FontTitle.FontItalic = false;
        this.FontTitle.FontUnderline = false;
        this.PicNav_Home = new picTotal[2];
        this.PicNav_Home[1].Left = 655.75f;
        this.PicNav_Home[1].Top = 477.75f;
        this.PicNav_Home[1].Width = 64.25f;
        this.PicNav_Home[1].Height = 62.25f;
        this.PicNav_Prev = new picTotal[2];
        this.PicNav_Prev[1].Left = 532.125f;
        this.PicNav_Prev[1].Top = 477.75f;
        this.PicNav_Prev[1].Width = 40.75f;
        this.PicNav_Prev[1].Height = 62.25f;
        this.PicNav_Up = new picTotal[2];
        this.PicNav_Up[1].Left = 572.875f;
        this.PicNav_Up[1].Top = 477.75f;
        this.PicNav_Up[1].Width = 39.875f;
        this.PicNav_Up[1].Height = 62.25f;
        this.PicNav_Next = new picTotal[2];
        this.PicNav_Next[1].Left = 613.875f;
        this.PicNav_Next[1].Top = 477.75f;
        this.PicNav_Next[1].Width = 41.75f;
        this.PicNav_Next[1].Height = 62.25f;
        this.PicNav_Return = new picTotal[2];
        this.PicNav_Return[1].Left = 483.875f;
        this.PicNav_Return[1].Top = 477.75f;
        this.PicNav_Return[1].Width = 52.5f;
        this.PicNav_Return[1].Height = 62.25f;
        this.PicMedia_Play = new picTotal[2];
        this.PicMedia_Play[1].Left = 248.125f;
        this.PicMedia_Play[1].Top = 477.75f;
        this.PicMedia_Play[1].Width = 46.25f;
        this.PicMedia_Play[1].Height = 62.25f;
        this.PicMedia_Stop = new picTotal[2];
        this.PicMedia_Stop[1].Left = 209.25f;
        this.PicMedia_Stop[1].Top = 477.75f;
        this.PicMedia_Stop[1].Width = 38.875f;
        this.PicMedia_Stop[1].Height = 62.25f;
        this.PicMedia_Pause = new picTotal[2];
        this.PicMedia_Pause[1].Left = 172.875f;
        this.PicMedia_Pause[1].Top = 477.75f;
        this.PicMedia_Pause[1].Width = 39.875f;
        this.PicMedia_Pause[1].Height = 62.25f;
        this.txtTitle.Left = 445f;
        this.txtTitle.Top = 22.5f;
        this.txtTitle.Width = 260.875f;
        this.txtTitle.Height = 34f;
        string str2 = ConfigurationManager.AppSettings["EnglishBookBaseVer"];
        object obj2 = str2 + @"Documents\unit" + JiBie + unitID + ".doc";
        string str = str2 + @"commonImages\";
        this.prsPres = this.OpenPres(str2 + "计算机英语.pot", true);
        int num2 = 0;
        Microsoft.Office.Interop.Word.Application class2 = new Microsoft.Office.Interop.Word.Application();
        object missing = Type.Missing;
        Document document = class2.Documents .Open(ref obj2, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
        for (int i = 1; i <= document.Paragraphs.Count ; i++)
        {
            string slideTitle = document.Paragraphs[i].Range .Text.Trim();
            if (slideTitle != "")
            {
                string str10 = ((Style)document.Paragraphs[i].get_Style()).NameLocal.ToUpper();
                if (str10 != null)
                {
                    if (!(str10 == "UNIT"))
                    {
                        if (str10 == "标题 1")
                        {
                            goto Label_08E8;
                        }
                        if (str10 == "标题 2")
                        {
                            goto Label_0E3E;
                        }
                        if (str10 == "标题 3")
                        {
                            goto Label_168C;
                        }
                    }
                    else
                    {
                        string str9 = slideTitle;
                        this.objSlide = this.ADDNewSlide("Unit" + JiBie + unitID, 0);
                        this.InsertBackGround(this.objSlide, str + "unit" + JiBie + unitID + ".jpg");
                        this.objSlide = this.ADDNewSlide("Agenda", 0);
                        num5 = this.objSlide.SlideIndex ;
                        this.InsertBackGround(this.objSlide, str + "unit" + JiBie + unitID + "agenda.jpg");
                    }
                }
            }
            continue;
        Label_08E8:
            if ((lastLevel != null) && (num6 > 0))
            {
                this.objSlide = this.prsPres.Slides [num6];
                if (lastLevel[0] != "")
                {
                    this.AddHylinkLastLevel(lastLevel, ref this.objSlide);
                }
            }
            string str4 = slideTitle;
            strArray5 = new string[3];
            strArray5[0] = slideTitle;
            this.objSlide = this.ADDNewSlide(str4, 0);
            this.InsertBackGround(this.objSlide, str + "unit" + JiBie + unitID + "一级.jpg");
            num6 = this.objSlide.SlideIndex ;
            destinationArray = new string[num2 + 1];
            if (sourceArray != null)
            {
                Array.Copy(sourceArray, 0, destinationArray, 0, sourceArray.Length);
            }
            destinationArray[num2] = str4;
            sourceArray = new string[num2 + 1];
            Array.Copy(destinationArray, 0, sourceArray, 0, destinationArray.Length);
            num2++;
            lastLevel = new string[] { "" };
            index = 0;
            this.objShape = this.objSlide.Shapes .AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.TextBoxBiaoTi.Left, this.TextBoxBiaoTi.Top, this.TextBoxBiaoTi.Width, this.TextBoxBiaoTi.Height);
            this.objShape.Name ="Temp1" ;
            this.ADDTextBoxContent(objShape , sourceArray[num2 - 1], false, this.FontBiaoTi.FontName, this.FontBiaoTi.FontSize, this.FontBiaoTi.FontColor, this.FontBiaoTi.FontBold, PpParagraphAlignment.ppAlignLeft, 0.05f);
            this.AddNaviButton(ref this.objSlide, str + @"level1\", "Agenda");
            string textString = "";
            string strText = "";
            int num8 = i + 1;
            while (num8 <= document.Paragraphs.Count )
            {
                slideTitle = document.Paragraphs[num8].Range .Text.Trim();
                if (slideTitle != "")
                {
                    if (((Style) document.Paragraphs[num8].get_Style()).NameLocal .Substring(0, 2) == "标题")
                    {
                        break;
                    }
                    if (((Style) document.Paragraphs[num8].get_Style()).NameLocal .ToLower() == "subtitle")
                    {
                        if (textString == "")
                        {
                            textString = slideTitle;
                        }
                        else
                        {
                            textString = textString + "\r\n" + slideTitle;
                        }
                    }
                    else if (((Style) document.Paragraphs[num8].get_Style()).NameLocal .ToLower() == "text")
                    {
                        if (strText == "")
                        {
                            strText = slideTitle;
                        }
                        else
                        {
                            strText = strText + "\r\n" + slideTitle;
                        }
                    }
                    else if (((Style) document.Paragraphs[num8].get_Style()).NameLocal .ToLower() == "table")
                    {
                        num9++;
                        this.PastTable(document.Tables [num9], this.objSlide, this.TextBoxContent.Left, this.TextBoxContent.Top, this.TextBoxContent.Width, this.TextBoxContent.Height);
                        num8++;
                        while ((((Style) document.Paragraphs[num8].get_Style()).NameLocal .ToLower() == "table") || (((Style) document.Paragraphs[num8].get_Style()).NameLocal .ToLower() == "正文"))
                        {
                            num8++;
                        }
                        num8--;
                    }
                }
                num8++;
            }
            if (textString != "")
            {
                this.objShape = this.objSlide.Shapes .AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.TextBoxSubtitle.Left, this.TextBoxSubtitle.Top, this.TextBoxSubtitle.Width, this.TextBoxSubtitle.Height);
                this.objShape.Name ="Temp2";
                this.ADDTextBoxContent(this.objSlide.Shapes ["Temp2"], textString, false, this.FontSubtitle.FontName, this.FontSubtitle.FontSize, this.FontSubtitle.FontColor, this.FontSubtitle.FontBold, PpParagraphAlignment.ppAlignLeft , 0.05f);
            }
            if (strText != "")
            {
                this.AddTextContent(strText, textString, ref this.objSlide);
            }
            i = num8 - 1;
            index = 0;
            continue;
        Label_0E3E:
            if ((strArray4 != null) && (num7 > 0))
            {
                this.objSlide = this.prsPres.Slides [num7];
                this.AddHylinkLastLevel(strArray4, ref this.objSlide);
            }
            string str5 = slideTitle;
            destinationArray = new string[index + 1];
            if (lastLevel != null)
            {
                Array.Copy(lastLevel, 0, destinationArray, 0, lastLevel.Length);
            }
            destinationArray[index] = this.RetSlideTitle(str5);
            lastLevel = new string[index + 1];
            Array.Copy(destinationArray, 0, lastLevel, 0, destinationArray.Length);
            index++;
            strArray5[1] = slideTitle;
            this.objSlide = this.ADDNewSlide(slideTitle, 0);
            num7 = this.objSlide.SlideIndex ;
            this.InsertBackGround(this.objSlide, str + "unit" + JiBie + unitID + "二级.jpg");
            this.objShape = this.objSlide.Shapes .AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal , this.TextBoxBiaoTi.Left, this.TextBoxBiaoTi.Top, this.TextBoxBiaoTi.Width, this.TextBoxBiaoTi.Height);
            this.objShape.Name ="Temp1" ;
            this.ADDTextBoxContent(objShape , slideTitle, false, this.FontBiaoTi.FontName, this.FontBiaoTi.FontSize, this.FontBiaoTi.FontColor, this.FontBiaoTi.FontBold, PpParagraphAlignment.ppAlignLeft , 0.05f);
            this.objShape = this.objSlide.Shapes .AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal , this.picRSJ2.Left, this.picRSJ1.Top, this.picRSJ2.Width - 21f, this.picRSJ2.Height);
            this.objShape.Name ="TitleYSJ" ;
            this.AddContentLastLevel(objShape, strArray5[0], FontBiaoTi.FontName, 20f, this.FontBiaoTi.FontColor);
            num7 = this.objSlide.SlideIndex ;
            this.AddNaviButton(ref this.objSlide, str + @"level2\", sourceArray[num2 - 1]);
            strText = "";
            textString = "";
            Slide objSlide = this.objSlide;
            num8 = i + 1;
            while (num8 <= document.Paragraphs.Count )
            {
                slideTitle = document.Paragraphs[num8].Range .Text.Trim();
                if (slideTitle != "")
                {
                    if (((Style) document.Paragraphs[num8].get_Style()).NameLocal.Substring (0, 2) == "标题")
                    {
                        break;
                    }
                    if (((Style) document.Paragraphs[num8].get_Style()).NameLocal .ToLower() == "subtitle")
                    {
                        if (textString != "")
                        {
                            objSlide = this.ADDNewSlide(slideTitle, 0);
                            this.InsertBackGround(objSlide, str + "unit" + JiBie + unitID + "二级.jpg");
                            this.objShape = objSlide.Shapes .AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal , this.TextBoxBiaoTi.Left, this.TextBoxBiaoTi.Top, this.TextBoxBiaoTi.Width, this.TextBoxBiaoTi.Height);
                            this.objShape.Name ="Temp1";
                            this.ADDTextBoxContent(objSlide.Shapes["Temp1"], lastLevel[index - 1], false, this.FontBiaoTi.FontName, this.FontBiaoTi.FontSize, this.FontBiaoTi.FontColor, this.FontBiaoTi.FontBold, PpParagraphAlignment.ppAlignLeft, 0.05f);
                            this.objShape = objSlide.Shapes .AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal , this.picRSJ2.Left, this.picRSJ1.Top, this.picRSJ2.Width - 21f, this.picRSJ2.Height);
                            this.objShape.Name ="TitleYSJ";
                            this.AddContentLastLevel(objSlide.Shapes["TitleYSJ"], strArray5[0], this.FontBiaoTi.FontName, 20f, this.FontBiaoTi.FontColor);
                            this.AddNaviButton(ref objSlide, str + @"level2\", sourceArray[num2 - 1]);
                        }
                        textString = slideTitle;
                        if (textString != "")
                        {
                            this.objShape = objSlide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.TextBoxSubtitle.Left, this.TextBoxSubtitle.Top, this.TextBoxSubtitle.Width, this.TextBoxSubtitle.Height);
                            this.objShape.Name ="Temp2";
                              
                            this.ADDTextBoxContent(objSlide.Shapes["Temp2"], textString, false, this.FontSubtitle.FontName, this.FontSubtitle.FontSize, this.FontSubtitle.FontColor, this.FontSubtitle.FontBold, PpParagraphAlignment.ppAlignLeft , 0.05f);
                        }
                    }
                    else if (((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "text")
                    {
                        strText = "";
                        while ((((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "text") || (((Style) document.Paragraphs[num8].get_Style()).NameLocal .ToLower() == "正文"))
                        {
                            if (slideTitle != "")
                            {
                                if (strText == "")
                                {
                                    strText = slideTitle;
                                }
                                else
                                {
                                    strText = strText + "\r\n" + slideTitle;
                                }
                            }
                            num8++;
                            if (num8 > document.Paragraphs.Count )
                            {
                                break;
                            }
                            slideTitle = document.Paragraphs[num8].Range.Text .Trim();
                        }
                        if (strText != "")
                        {
                            this.AddTextContent(strText, textString, ref objSlide);
                            i = num8 - 1;
                            num8--;
                        }
                    }
                    else if (((Style) document.Paragraphs[num8].get_Style()).NameLocal .ToLower() == "table")
                    {
                        num9++;
                        this.PastTable(document.Tables [num9], objSlide, this.TextBoxContent.Left, this.TextBoxContent.Top, this.TextBoxContent.Width, this.TextBoxContent.Height);
                        num8++;
                        while ((((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "table") || (((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "正文"))
                        {
                            num8++;
                            if (num8 > document.Paragraphs.Count )
                            {
                                break;
                            }
                        }
                        num8--;
                    }
                }
                num8++;
            }
            strArray4 = new string[1];
            numArray2 = new int[1];
            num4 = 0;
            i = num8 - 1;
            continue;
        Label_168C:
            strArray5[2] = slideTitle;
            string str6 = slideTitle;
            this.objSlide = this.ADDNewSlide(str6, 0);
            destinationArray = new string[num4 + 1];
            numArray = new int[num4 + 1];
            if (strArray4 != null)
            {
                Array.Copy(strArray4, 0, destinationArray, 0, strArray4.Length);
            }
            Array.Copy(numArray2, 0, numArray, 0, numArray2.Length);
            destinationArray[num4] = str6;
            numArray[num4] = this.objSlide.SlideIndex ;
            strArray4 = new string[num4 + 1];
            numArray2 = new int[num4 + 1];
            Array.Copy(destinationArray, 0, strArray4, 0, destinationArray.Length);
            Array.Copy(numArray, 0, numArray2, 0, numArray.Length);
            num4++;
            this.InsertBackGround(this.objSlide, str + "unit" + JiBie + unitID + "二级.jpg");
            this.objShape = this.objSlide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.picRSJ2.Left, this.picRSJ1.Top, this.picRSJ2.Width - 21f, this.picRSJ2.Height);
            this.objShape.Name ="TitleYSJ";
            this.AddContentLastLevel(this.objSlide.Shapes["TitleYSJ"], strArray5[1], this.FontBiaoTi.FontName, 20f, this.FontBiaoTi.FontColor);
            this.objShape = this.objSlide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.TextBoxBiaoTi.Left, this.TextBoxBiaoTi.Top, this.TextBoxBiaoTi.Width, this.TextBoxBiaoTi.Height);
            this.objShape.Name ="Temp1" ;
            this.ADDTextBoxContent(this.objSlide.Shapes["Temp1"], slideTitle, false, this.FontBiaoTi.FontName, this.FontBiaoTi.FontSize, this.FontBiaoTi.FontColor, this.FontBiaoTi.FontBold, PpParagraphAlignment.ppAlignLeft , 0.05f);
            strText = "";
            textString = "";
            num8 = i + 1;
            while (num8 <= document.Paragraphs.Count )
            {
                slideTitle = document.Paragraphs[num8].Range.Text .Trim();
                if (((Style) document.Paragraphs[num8].get_Style()).NameLocal.Substring(0, 2) == "标题")
                {
                    break;
                }
                if (((Style) document.Paragraphs[num8].get_Style()).NameLocal == "text")
                {
                    if (strText == "")
                    {
                        strText = slideTitle;
                    }
                    else
                    {
                        strText = strText + "\r\n" + slideTitle;
                    }
                }
                else if (((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "subtitle")
                {
                    if (textString == "")
                    {
                        textString = slideTitle;
                    }
                    else
                    {
                        textString = textString + "\r\n" + slideTitle;
                    }
                }
                else if (((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "table")
                {
                    num9++;
                    this.PastTable(document.Tables[num9], this.objSlide, this.TextBoxContent.Left, this.TextBoxContent.Top, this.TextBoxContent.Width, this.TextBoxContent.Height);
                    num8++;
                    while ((((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "table") || (((Style) document.Paragraphs[num8].get_Style()).NameLocal == "正文"))
                    {
                        num8++;
                    }
                    num8--;
                }
                num8++;
            }
            if (textString != "")
            {
                this.objShape = this.objSlide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.TextBoxSubtitle.Left, this.TextBoxSubtitle.Top, this.TextBoxSubtitle.Width, this.TextBoxSubtitle.Height);
                this.objShape.Name ="Temp2" ;
                this.ADDTextBoxContent(objShape, textString, false, this.FontSubtitle.FontName, this.FontSubtitle.FontSize, this.FontSubtitle.FontColor, this.FontSubtitle.FontBold, PpParagraphAlignment.ppAlignLeft, 0.05f);
            }
            if (strText != "")
            {
                this.AddTextContent(strText, textString, ref this.objSlide);
            }
            this.AddNaviButton(ref this.objSlide, str + @"level2\", sourceArray[num2 - 1] + "-" + lastLevel[index - 1]);
            i = num8 - 1;
        }
        if (sourceArray[0] != "")
        {
            this.objSlide = this.prsPres.Slides [num6];
            if (lastLevel[0] != "")
            {
                this.AddHylinkLastLevel(lastLevel, ref this.objSlide);
            }
        }
        this.objSlide = this.prsPres.Slides [num5];
        if (sourceArray[0] != "")
        {
            this.AddHylinkLastLevel(sourceArray, ref this.objSlide);
        }
        this.objSlide = this.ADDNewSlide("Thank You", 0);
        this.InsertBackGround(this.objSlide, str + "thank you.jpg");
        this.objSlide = this.ADDNewSlide("Credits", 0);
        this.InsertBackGround(this.objSlide, str + "Credits.jpg");
        object obj4 = null;
        if (document != null)
        {
            document.Close(ref obj4, ref obj4, ref obj4);
        }
        class2.Quit(ref obj4, ref obj4, ref obj4);
        MessageBox.Show("ok");
    }
        #region 2018-07-05 NewCommon
        private void ChangeSlideMaster(ref Presentation prsPres,string txtTitle)
        {
            Microsoft.Office.Interop.PowerPoint.Shape shp;
            Master slide;
            string unit="1";
            string title;
            txtTitle = txtTitle.Substring(5).Trim();//去掉前面Unit
            int iStart = txtTitle.IndexOf(" ");
            unit = txtTitle.Substring(0, iStart);
            title = txtTitle.Substring(iStart + 1).Trim ();
            slide = prsPres.Slides[2].Master;
            shp = slide.Shapes["shpUnit"];
            float fSize= shp.TextFrame.TextRange.Characters(shp.TextFrame.TextRange.Text.Length,1).Font.Size; 
            shp.TextFrame.TextRange.Text = shp.TextFrame.TextRange.Text.Replace("4", unit);
            shp.TextFrame.TextRange.Characters(shp.TextFrame.TextRange.Text.Length-unit.Length +1,unit.Length ).Font.Size = fSize;//10单元从倒数第一个字符开始处理
            shp = slide.Shapes["shpTitle"];
            shp.TextFrame.TextRange.Text = title;

        }
        //2018-07-05
        public void CreateNewCommonEnglish(string unitID)
        {
            int k = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            int num11 = 0;
            int num12 = 0;
            int wdTableCount = 0;
            string str = "";
            bool soundLevel2 = false;
            bool soundLeve3 = false;
            string[] level = null;
            string[] title = null;
            string[] strArray3 = null;
            string[] strArray4 = null;
            string[] strArray5 = null;
            string[] strArray6 = null;

            string titleLast;//上一级的标题

            this.SetStyleOfNewCommonEnglish();
            string str3 = ConfigurationManager.AppSettings["NewCommonEnglish"];
            if (!str3.EndsWith(@"\"))
            {
                str3 = str3 + @"\";
            }
            object obj2 = str3 + @"Documents\unit" + unitID + ".doc";
            string str4 = str3 + @"commonImages\";
            this.prsPres = this.OpenPres(str3 + "新通用大学英语.pptm", true);//将模板pot改为ppt
            //if (prsPres.Slides.Count == 1)//删除空白页
            //    prsPres.Slides[1].Delete();
            int num2 = 0;
            Microsoft.Office.Interop.Word.Application application = new Microsoft.Office.Interop.Word.Application();
            object missing = Type.Missing;
            Document wordOpen = application.Documents.Open(ref obj2, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
            for (int i = 1; i <= wordOpen.Paragraphs.Count; i++)
            {
                string slideTitle = wordOpen.Paragraphs[i].Range.Text.Trim();
                if (slideTitle != "")
                {
                    string wdStyle = ((Style)wordOpen.Paragraphs[i].get_Style()).NameLocal.ToUpper();
                    switch (wdStyle)
                    {
                        case "UNITS":

                        case "标题":
                            str = slideTitle;
                            ChangeSlideMaster(ref prsPres, str);
                            this.objSlide = prsPres.Slides[1];
                            objSlide.Shapes["SlideTitle"].TextFrame.TextRange.Text = "unit" + unitID;//大纲视图标题
                            objSlide.Shapes["shpUnitTitle"].TextFrame.TextRange.Text = str;
                            //this.objSlide = this.ADDNewSlide("unit" + unitID, 0);
                            //this.InsertBackGround(this.objSlide, str4 + "unit" + unitID + ".jpg");
                            this.objSlide = prsPres.Slides[2];// this.ADDNewSlide("Agenda", 0);
                                                              //this.AddSlideTitleNewCommon(ref this.objSlide, "Agenda", str4 + @"blue\" + unitID + ".jpg");//加标题
                                                              //this.AddNaviButton(ref this.objSlide, str4 + "blue", "unit" + unitID, "unit" + unitID);
                            this.DealWithContent(ref i, ref wordOpen, ref wdTableCount, 2);

                            //num5 = this.objSlide.SlideIndex;
                            break;

                        case "标题 1":
                            if (title != null)
                            {
                                this.objSlide = this.prsPres.Slides[num8];
                                this.AddMuitiTextBox(title, ref this.objSlide, str4 + "blue");
                            }
                            this.objSlide = this.ADDNewSlide(slideTitle, 0);
                            num8 = this.objSlide.SlideIndex;
                            //this.InsertBackGround(this.objSlide, str4 + "modelBlue.jpg");
                            this.SaveTitle(ref level, ref num2, slideTitle);
                            title = null;
                            k = 0;
                            this.AddSlideTitleNewCommon(ref this.objSlide, slideTitle, str4 + @"blue\" + unitID + ".jpg");
                            this.AddNaviButton(ref this.objSlide, str4 + "blue", "unit" + unitID, "unit" + unitID);
                            this.DealWithContent(ref i, ref wordOpen, ref wdTableCount);
                            break;

                        case "标题 2":
                            if (strArray3 != null)
                            {
                                this.objSlide = this.prsPres.Slides[num9];
                                this.AddMuitiTextBox(strArray3, ref this.objSlide, str4 + "blue");
                            }
                            this.objSlide = this.ADDNewSlide(slideTitle, 0);
                            //this.InsertBackGround(this.objSlide, str4 + "modelBlue.jpg");
                            num9 = this.objSlide.SlideIndex;
                            this.SaveTitle(ref title, ref k, slideTitle);
                            strArray3 = null;
                            num4 = 0;
                            this.AddSlideTitleNewCommon(ref this.objSlide, slideTitle, str4 + @"blue\" + unitID + ".jpg");
                            this.AddNaviButton(ref this.objSlide, str4 + "blue", level[num2 - 1], "unit" + unitID);
                            this.DealWithContent(ref i, ref wordOpen, ref wdTableCount);
                            if (slideTitle == "A. Listening Comprehension" || slideTitle == "Listening")//add sound
                            {
                                soundLevel2 = true;
                            }
                            else
                            {
                                soundLevel2 = false;
                            }
                            if (slideTitle == "A. Listening Comprehension")
                                InsertMediaControl(objSlide, str4 + "blue\\");
                            break;

                        case "标题 3":
                            if (strArray4 != null)
                            {
                                this.objSlide = this.prsPres.Slides[num10];
                                this.AddMuitiTextBox(strArray4, ref this.objSlide, str4 + "blue");
                            }
                            this.objSlide = this.ADDNewSlide(slideTitle, 0);
                            //this.InsertBackGround(this.objSlide, str4 + "modelBlue.jpg");
                            num10 = this.objSlide.SlideIndex;
                            this.SaveTitle(ref strArray3, ref num4, slideTitle);
                            strArray4 = null;
                            num5 = 0;
                            this.AddSlideTitleNewCommon(ref this.objSlide, slideTitle, str4 + @"blue\" + unitID + ".jpg");
                            this.AddNaviButton(ref this.objSlide, str4 + "blue", title[k - 1], "unit" + unitID);
                            this.DealWithContent(ref i, ref wordOpen, ref wdTableCount);
                            //add sound
                            if (slideTitle == "Scene 1" || slideTitle == "Scene 2" || slideTitle == "Text A" || slideTitle == "Text B" || slideTitle == "Text C")
                                soundLeve3 = true;
                            else
                                soundLeve3 = false;
                            AddSoundButton(objSlide, str4 + "blue\\", soundLevel2, soundLeve3);
                            
                            break;

                        case "标题 4":
                            if (strArray5 != null)
                            {
                                this.objSlide = this.prsPres.Slides[num11];
                                //课文下面单词
                                titleLast = objSlide.Shapes["SlideTitle"].TextFrame.TextRange.Text.Trim().ToLower();
                                //填充课文中的单词链接
                                if (string.Compare (titleLast, "text",true)==0)// (titleLast  == "new words" || titleLast == "phrases and expressions")//填写下面的单词
                                {
                                    WordLinkOfText(objSlide.Shapes["shpBody"], strArray5 );
                                }
                                else

                                    this.AddMuitiTextBox(strArray5, ref this.objSlide, str4 + "blue");
                            }
                            if (slideTitle.ToLower() != "new words" && slideTitle.ToLower() != "phrases and expressions")//填写下面的单词
                            {
                                this.objSlide = this.ADDNewSlide(slideTitle, 0);
                                num11 = this.objSlide.SlideIndex;
                                //this.InsertBackGround(this.objSlide, str4 + "modelBlue.jpg");
                                this.SaveTitle(ref strArray4, ref num5, slideTitle);
                                strArray5 = null;
                                num6 = 0;
                                this.AddSlideTitleNewCommon(ref this.objSlide, slideTitle, str4 + @"blue\" + unitID + ".jpg");
                                this.AddNaviButton(ref this.objSlide, str4 + "blue", strArray3[num4 - 1], "unit" + unitID);
                                this.DealWithContent(ref i, ref wordOpen, ref wdTableCount);

                                AddSoundButton(objSlide, str4 + "blue\\", soundLevel2, soundLeve3);
                            }
                            
                            break;

                        case "标题 5":
                            if (strArray6 != null)
                            {
                                this.objSlide = this.prsPres.Slides[num12];
                                this.AddMuitiTextBox(strArray6, ref this.objSlide, str4 + "blue");
                            }
                            string word = dealWord(slideTitle);//去掉单词的前缀
                            this.objSlide = this.ADDNewSlide(word, 0,true);
                            num12 = this.objSlide.SlideIndex;
                            this.AddSlideTitleNewCommon(ref this.objSlide, word, str4 + @"blue\" + unitID + ".jpg");

                            //this.InsertBackGround(this.objSlide, str4 + "modelBlue.jpg");
                            
                            this.SaveTitle(ref strArray5, ref num6, word);
                            strArray6 = null;
                            num7 = 0;
                           
                            this.AddNaviButton(ref this.objSlide, str4 + "blue", strArray4[num5 - 1], "unit" + unitID);
                            this.DealWithContent(ref i, ref wordOpen, ref wdTableCount);

                            AddSoundButton(objSlide, str4 + "blue\\", soundLevel2, soundLeve3);
                            break;

                        case "标题 6"://green 改为blue
                            this.objSlide = this.ADDNewSlide(slideTitle, 0);
                            //this.InsertBackGround(this.objSlide, str4 + "modelGreen.jpg");
                            this.SaveTitle(ref strArray6, ref num7, slideTitle);
                            this.AddSlideTitleNewCommon(ref this.objSlide, slideTitle, str4 + @"blue\" + unitID + ".jpg");
                            this.AddNaviButton(ref this.objSlide, str4 + "blue", strArray5[num6 - 1], "unit" + unitID);
                            this.DealWithContent(ref i, ref wordOpen, ref wdTableCount);
                            break;
                    }
                }
            }
            if (title != null)
            {
                this.objSlide = this.prsPres.Slides[num8];
                this.AddMuitiTextBox(title, ref this.objSlide, str4 + "blue");
            }
            this.objSlide = this.prsPres.Slides[2];
            this.AddMuitiTextBox(level, ref this.objSlide, str4 + "blue");
            //if (objSlide.Shapes["round1"].Top != objSlide.Shapes["txtTitle1"].Top + 5)
            //{
            //    objSlide.Shapes["round1"].Left = objSlide.Shapes["txtTitle1"].Left - objSlide.Shapes["round1"].Width;
            //    objSlide.Shapes["round1"].Top = objSlide.Shapes["txtTitle1"].Top + 5;
            //}
            this.objSlide = this.ADDNewSlide("Thank You", 0);
            //Rectangle 2left: 149.725top: 222.8width: 399.8heigth: 94.4
            this.InsertPicture(this.objSlide, str4 + "thank.png", 149.725f, 222.8f, 399.8f, 94.4f,true);
            //this.objSlide = this.ADDNewSlide("Credits", 0);
            //this.InsertBackGround(this.objSlide, str4 + "credits.jpg");
            MessageBox.Show("ok");
            object obj4 = null;
            if (wordOpen != null)
            {
                wordOpen.Close(ref obj4, ref obj4, ref obj4);
            }
            application.Quit(ref obj4, ref obj4, ref obj4);
        }
        //2018-07-05
        //去掉单词或词组的前缀◆ [C2] [B2] forexample ▲ [C2] wield

        private string dealWord(string srcWord)
        {
            string word = srcWord.Replace("◆", "").Trim();//◆
            word = word.Replace("★", "").Trim();
            word = word.Replace("▲", "").Trim();
            word = word.Substring(word.LastIndexOf("]") + 1).Trim();
            return word;
        }
    private void AddSoundButton(Slide currSlide, string btnPath, bool lev2, bool lev3)
    {
        if (lev2 == true || lev3 == true)
            InsertMediaControl(currSlide, btnPath);
    }
        //2018-07-05

        private void DealWithContent(ref int i, ref Document WordOpen, ref int WdTableCount, int slideIndex = 0)
        {
            string textString = "";
            string strText = "";
            int num = i + 1;
            while (num <= WordOpen.Paragraphs.Count)
            {
                string str3 = WordOpen.Paragraphs[num].Range.Text.Trim();
                if (str3 != "")
                {
                    if (((Style)WordOpen.Paragraphs[num].get_Style()).NameLocal.Substring(0, 2) == "标题")
                    {
                        break;
                    }
                    if (((Style)WordOpen.Paragraphs[num].get_Style()).NameLocal.ToLower() == "sub")
                    {
                        if (textString == "")
                        {
                            textString = str3;
                        }
                        else
                        {
                            textString = textString + "\r\n" + str3;
                        }
                    }
                    else if (((Style)WordOpen.Paragraphs[num].get_Style()).NameLocal.ToLower() == "正文")
                    {
                        if (strText == "")
                        {
                            strText = str3;
                        }
                        else
                        {
                            strText = strText + "\r\n" + str3;
                        }
                    }
                    else if (((Style)WordOpen.Paragraphs[num].get_Style()).NameLocal.ToLower() == "table")
                    {
                        WdTableCount++;
                        try
                        {
                            this.PastTable(WordOpen.Tables[WdTableCount], this.objSlide, this.TextBoxContent.Left, this.TextBoxContent.Top, this.TextBoxContent.Width, this.TextBoxContent.Height);
                        }
                        catch
                        {

                        }
                        num++;
                        try
                        {
                            while ((((Style)WordOpen.Paragraphs[num].get_Style()).NameLocal.ToLower() == "table") || (((Style)WordOpen.Paragraphs[num].get_Style()).NameLocal.ToLower() == "正文"))
                            {
                                num++;
                            }
                        }
                        catch
                        {

                        }

                        num--;
                    }
                }
                num++;
            }

            if (textString != "")//sub
            {
                this.objShape = this.objSlide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.TextBoxSubtitle.Left, this.TextBoxSubtitle.Top, this.TextBoxSubtitle.Width, this.TextBoxSubtitle.Height);
                this.objShape.Name = "Temp2";
                this.ADDTextBoxContent(objShape, textString, false, this.FontSubtitle.FontName, this.FontSubtitle.FontSize, this.FontSubtitle.FontColor, this.FontSubtitle.FontBold, PpParagraphAlignment.ppAlignLeft, 0.05f);
            }
            if (strText != "")//正文
            {
                if (slideIndex == 2)
                {
                    this.objShape = this.objSlide.Shapes["shpGoals"];
                    strText = strText.Replace("Unit Goals", "").Trim();
                    objShape.TextFrame.TextRange.Text = strText;
                    
                }
                else
                    this.AddTextContent(strText, textString, ref this.objSlide);
            }

            i = num - 1;
        }
        //2018-07-05
        private void SaveTitle(ref string[] level, ref int k, string Title)
        {
            string[] destinationArray = new string[k + 1];
            if (level != null)
            {
                Array.Copy(level, 0, destinationArray, 0, level.Length);
            }
            destinationArray[k] = Title;
            level = destinationArray;
            k++;
        }
        //2018-07-05
        private void SetStyleOfNewCommonEnglish()
        {
            this.TextBoxBiaoTi.Left = 14.125f;
            this.TextBoxBiaoTi.Top = 50.5f;// 11.375f;
            this.TextBoxBiaoTi.Width = 650;// 546.5f;
            this.TextBoxBiaoTi.Height = 72f;
            this.TextBoxSubtitle.Left = 25.5f;
            this.TextBoxSubtitle.Top = 98.25f;
            this.TextBoxSubtitle.Width = 547.125f;
            this.TextBoxSubtitle.Height = 45.25f;
            this.TextBoxContent.Left = 27.5f;
            this.TextBoxContent.Top = 154f;
            this.TextBoxContent.Width = 690f;
            this.TextBoxContent.Height = 312f;
            this.TextBoxContent1.Left = 27.5f;
            this.TextBoxContent1.Top = 97f;
            this.TextBoxContent1.Width = 690f;
            this.TextBoxContent1.Height = 330f;
            this.picRSJ1.Left = 289f;
            this.picRSJ1.Top = 0f;
            this.picRSJ1.Width = 431f;
            this.picRSJ1.Height = 29f;
            this.FontBiaoTi.FontName = "Arial";
            this.FontBiaoTi.FontSize = 40f;
            this.FontBiaoTi.FontBold = MsoTriState.msoTrue;
            this.FontBiaoTi.FontColor =  0x895007;// 3399ff;蓝绿红
            this.FontSubtitle.FontName = "Arial";
            this.FontSubtitle.FontSize = 23f;
            this.FontSubtitle.FontColor = 0x895007;//白色ffffff
            this.FontSubtitle.FontBold = MsoTriState.msoTrue;
            this.FontSubtitle.FontItalic = false;
            this.FontSubtitle.FontUnderline = false;
            this.FontContent.FontName = "Arial";
            this.FontContent.FontSize = 20f;
            this.FontContent.FontColor = 0x000000;//白色
            this.FontContent.FontBold = 0;
            this.FontContent.FontItalic = false;
            this.FontContent.FontUnderline = false;
            this.PicNav_Home = new picTotal[2];
            this.PicNav_Home[1].Left = 535.75f;
            this.PicNav_Home[1].Top = 491.125f;
            this.PicNav_Home[1].Width = 36.75f;
            this.PicNav_Home[1].Height = 45f;
            this.PicNav_Up = new picTotal[2];
            this.PicNav_Up[1].Left = 566.75f;
            this.PicNav_Up[1].Top = 491.125f;
            this.PicNav_Up[1].Width = 33f;
            this.PicNav_Up[1].Height = 45f;
            this.PicNav_Return = new picTotal[2];
            this.PicNav_Return[1].Left = 600.75f;
            this.PicNav_Return[1].Top = 491.125f;
            this.PicNav_Return[1].Width = 37.5f;
            this.PicNav_Return[1].Height = 45f;
            this.PicNav_Prev = new picTotal[2];
            this.PicNav_Prev[1].Left = 637.875f;
            this.PicNav_Prev[1].Top = 491.125f;
            this.PicNav_Prev[1].Width = 34.5f;
            this.PicNav_Prev[1].Height = 45f;
            this.PicNav_Next = new picTotal[2];
            this.PicNav_Next[1].Left = 666.125f;
            this.PicNav_Next[1].Top = 491.125f;
            this.PicNav_Next[1].Width = 28.5f;
            this.PicNav_Next[1].Height = 45f;
            this.PicMedia_Play = new picTotal[2];
            //前面的部分
            this.PicMedia_Play[0].Left = 372.85f;
            this.PicMedia_Play[0].Top = 491.125f;
            this.PicMedia_Play[0].Width = 22.25f;
            this.PicMedia_Play[0].Height = 45f;


            this.PicMedia_Play[1].Left = 395.125f;
            this.PicMedia_Play[1].Top = 491.125f;
            this.PicMedia_Play[1].Width = 31.75f;
            this.PicMedia_Play[1].Height = 45f;

            this.PicMedia_Pause = new picTotal[2];
            this.PicMedia_Pause[1].Left = 426.875f;
            this.PicMedia_Pause[1].Top = 491.125f;
            this.PicMedia_Pause[1].Width = 22.62496f;
            this.PicMedia_Pause[1].Height = 45f;
            this.PicMedia_Stop = new picTotal[2];
            this.PicMedia_Stop[1].Left = 449.5f;
            this.PicMedia_Stop[1].Top = 491.125f;
            this.PicMedia_Stop[1].Width = 24f;
            this.PicMedia_Stop[1].Height = 45f;
            //后面的部分
            this.PicMedia_Stop[0].Left = 473.5f;
            this.PicMedia_Stop[0].Top = 491.125f;
            this.PicMedia_Stop[0].Width = 62.25f;
            this.PicMedia_Stop[0].Height = 45f;

            NaviPosiBook2();//重设设置第二册的按钮位置


        }
        //added 2017-09-13 
        private void NaviPosiBook2()
        {
            //前面的部分
            this.PicMedia_Play[0].Left = 434.709f;
            this.PicMedia_Play[0].Top = 509.1847f;
            this.PicMedia_Play[0].Width = 28.34646f;
            this.PicMedia_Play[0].Height = 28.34646f;


            this.PicMedia_Play[1].Left = 458.136f;
            this.PicMedia_Play[1].Top = 509.1847f;
            this.PicMedia_Play[1].Width = 28.34646f;
            this.PicMedia_Play[1].Height = 28.34646f;

            this.PicMedia_Pause[1].Left = 486.734f;
            this.PicMedia_Pause[1].Top = 509.1847f;
            this.PicMedia_Pause[1].Width = 28.34646f;
            this.PicMedia_Pause[1].Height = 28.34646f;

            this.PicMedia_Stop[1].Left = 509.359f;
            this.PicMedia_Stop[1].Top = 509.1847f;
            this.PicMedia_Stop[1].Width = 28.34646f;
            this.PicMedia_Stop[1].Height = 28.34646f;
            //后面的部分
            this.PicMedia_Stop[0].Left = 533.3589f;
            this.PicMedia_Stop[0].Top = 509.1847f;
            this.PicMedia_Stop[0].Width = 28.34646f;
            this.PicMedia_Stop[0].Height = 28.34646f;
            //return
            PicNav_Return[1].Left = 615.1465F;
            PicNav_Return[1].Top = 509.1847F;
            PicNav_Return[1].Width = 28.34646F;
            PicNav_Return[1].Height = 28.34646F;
            //prev
            PicNav_Prev[1].Left = 643.496F;
            PicNav_Prev[1].Top = 509.1847F;
            PicNav_Prev[1].Width = 28.34646F;
            PicNav_Prev[1].Height = 28.34646F;

            PicNav_Up[1].Left = 588.1722F;
            PicNav_Up[1].Top = 509.1847F;
            PicNav_Up[1].Width = 28.34646F;
            PicNav_Up[1].Height = 28.34646F;

            PicNav_Next[1].Left = 671.8488F;
            PicNav_Next[1].Top = 509.1847F;
            PicNav_Next[1].Width = 28.34646F;
            PicNav_Next[1].Height = 28.34646F;

            PicNav_Home[1].Left = 559.4504F;
            PicNav_Home[1].Top = 509.1847F;
            PicNav_Home[1].Width = 28.34646F;
            PicNav_Home[1].Height = 28.34646F;
        }
        #endregion
        public void deleControl(string strPPT)
    {
        for (int i = 1; i <= 1; i++)
        {
            Presentation presentation = this.OpenPres(strPPT + "unit" + i.ToString("00") + ".ppt", false);
            for (int j = 4; j <= (presentation.Slides.Count  - 2); j++)
            {
                for (int k = presentation.Slides[j].Shapes.Count ; k > 0; k--)
                {
                    Microsoft.Office.Interop.PowerPoint.Shape shape = presentation.Slides[j].Shapes[k];
                    if (((shape.Top  + shape.Height ) == presentation.SlideMaster .Height ) && (shape.Name .IndexOf("Picture") == 0))
                    {
                        shape.Delete();
                    }
                }
            }
            presentation.Save();
            presentation.Close();
        }
        MessageBox.Show("ok");
    }
        //是否是英文字母
        public bool IsChatacter(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[A-Za-z]+$");
            return reg1.IsMatch(str);
        }
        private string GetWord(string strWord, string Word)
        {
            int index = strWord.ToLower().IndexOf(Word ,0,StringComparison.OrdinalIgnoreCase);
            if (index > -1)
            {

                while (IsChatacter((strWord.Substring(index + Word.Length, 1))))//(((strWord.Substring(index + Word.Length, 1).CompareTo("A") >= 0) && (strWord.Substring(index + Word.Length, 1).CompareTo("Z") <= 0)) || ((strWord.Substring(index + Word.Length, 1).CompareTo("a") >= 0) && (strWord.Substring(index + Word.Length, 1).CompareTo("z") <= 0)))
                {
                    Word = Word + strWord.Substring(index + Word.Length, 1);

                }
            }
            return Word;
        }

    public void InsertBackGround(Slide CurSlide, string PicturePath)
    {
        if (!common.FileExits(PicturePath))
        {
            MessageBox.Show(PicturePath + "不存在");
        }
        else
        {
            //CurSlide.FollowMasterBackground[ ;// (0);
            CurSlide.FollowMasterBackground = MsoTriState.msoFalse;
            CurSlide.DisplayMasterShapes = MsoTriState.msoTrue;
            CurSlide.Background.Fill.Visible =MsoTriState.msoTrue ;//Visible(-1);
            CurSlide.Background.Fill.UserPicture(PicturePath);
        }
    }
//插入播放图标2018-07-05
    private void InsertMediaControl(Slide objSlide, string PicturePath)
    {
        this.InsertPicture(objSlide, PicturePath + "play-1.jpg", this.PicMedia_Play[0].Left, this.PicMedia_Play[0].Top, this.PicMedia_Play[0].Width, this.PicMedia_Play[0].Height,true );
        this.InsertPictureOperation(objSlide, PicturePath + "play.jpg", "", "play", this.PicMedia_Play[1].Left, this.PicMedia_Play[1].Top, this.PicMedia_Play[1].Width, this.PicMedia_Play[1].Height, true, "Mplay", "");
        this.InsertPictureOperation(objSlide, PicturePath + "pause.jpg", "", "pause", this.PicMedia_Pause[1].Left, this.PicMedia_Pause[1].Top, this.PicMedia_Pause[1].Width, this.PicMedia_Pause[1].Height, true, "Mpause", "");
        this.InsertPictureOperation(objSlide, PicturePath + "stop.jpg", "", "stop", this.PicMedia_Stop[1].Left, this.PicMedia_Stop[1].Top, this.PicMedia_Stop[1].Width, this.PicMedia_Stop[1].Height, true, "Mstop", "");
        this.InsertPicture(objSlide, PicturePath + "stop-1.jpg", this.PicMedia_Stop[0].Left, this.PicMedia_Stop[0].Top, this.PicMedia_Stop[0].Width, this.PicMedia_Stop[0].Height, true);


    }

    public void InsertPicture(Slide prsSlide, string PicturePath, float Left, float Top, float Width, float Height, bool IsZorder,string picName="")
    {
        if (PicturePath != "")
        {
                if (!common.FileExits(PicturePath))
                {
                    MessageBox.Show(PicturePath + "不存在");
                }
                else
                {
                    Microsoft.Office.Interop.PowerPoint.Shape shp;
                    if ((Width == 0f) && (Height == 0f))
                    {
                       shp= prsSlide.Shapes.AddPicture(PicturePath, MsoTriState.msoFalse, MsoTriState.msoTrue, Left, Top, -1f, -1f);
                    }
                    else
                    {
                        shp=prsSlide.Shapes.AddPicture(PicturePath, MsoTriState.msoFalse, MsoTriState.msoTrue, Left, Top, Width, Height);
                    }
                    if (!IsZorder)
                    {
                        shp.ZOrder(MsoZOrderCmd.msoSendToBack);//1
                    }
                    else
                    {
                        shp.ZOrder(0);
                    }
                    if (picName != "")
                        shp.Name = picName;
                }
        }
    }
//2018-07-05
    public void InsertPictureOperation(Slide prsSlide, string PicturePath, string LinkName, string PictureControlName, float Left, float Top, float Width, float Height, bool FlagMacro, string MacroName, string navi)
    {
        if (!common.FileExits(PicturePath))
        {
            MessageBox.Show(PicturePath + "不存在");
        }
        else
        {
            Microsoft.Office.Interop.PowerPoint.Shape shape;
            if ((Width == 0f) && (Height == 0f))
            {
                shape = prsSlide.Shapes.AddPicture(PicturePath,MsoTriState.msoFalse,MsoTriState.msoTrue, Left, Top, -1f, -1f);
            }
            else
            {
                shape = prsSlide.Shapes.AddPicture(PicturePath, MsoTriState.msoFalse, MsoTriState.msoTrue, Left, Top, Width, Height);
            }
            shape.Name =PictureControlName ;
            shape.ZOrder(0);
                if (FlagMacro)
                {
                    shape.ActionSettings[PpMouseActivation.ppMouseClick].Action = PpActionType.ppActionRunMacro;// . set_Action(8);
                    shape.ActionSettings[PpMouseActivation.ppMouseClick].Run = MacroName;
                }
                else if (navi == "P")
                {
                    shape.ActionSettings[PpMouseActivation.ppMouseClick].Action = PpActionType.ppActionPreviousSlide;// (2);
                }
                else if (navi == "N")
                {
                    shape.ActionSettings[PpMouseActivation.ppMouseClick].Action = PpActionType.ppActionNextSlide;//.set_Action(1);
                }
                else if (navi == "R")
                {
                    shape.ActionSettings[PpMouseActivation.ppMouseClick].Action = PpActionType.ppActionLastSlideViewed;// set_Action(5);
                }
                else
                {
                    shape.ActionSettings[PpMouseActivation.ppMouseClick].Action = PpActionType.ppActionHyperlink;
                    shape.ActionSettings[PpMouseActivation.ppMouseClick].Hyperlink.SubAddress = LinkName;
                }
            shape.ActionSettings[PpMouseActivation.ppMouseClick].SoundEffect.Type =PpSoundEffectType.ppSoundNone ;//.set_Type(0);
            shape.ActionSettings[PpMouseActivation.ppMouseClick].AnimateAction=MsoTriState.msoFalse;// (0);
        }
    }

    public Presentation OpenPres(string strTemplate, bool isNew)
    {
        this.ppApp = new Microsoft.Office.Interop.PowerPoint.Application();
        this.ppApp.Visible = MsoTriState.msoTrue;// (-1);
        if (isNew)
        {
            return this.ppApp.Presentations.Open(strTemplate, MsoTriState.msoFalse, MsoTriState.msoTrue, MsoTriState.msoTrue);
        }
        return this.ppApp.Presentations.Open(strTemplate, MsoTriState.msoFalse, MsoTriState.msoFalse , MsoTriState.msoTrue);
    }

    private void PastTable(Microsoft.Office.Interop.Word.Table wbTable, Slide objPage, float lngLeft, float lngTop, float lngWidth, float lngHeight)
    {
        wbTable.Range.Copy();
        objPage.Select();
        objPage.Shapes .Paste();
        this.objShape = objPage.Shapes [objPage.Shapes.Count ];
        this.objShape.Top =lngTop ;
        this.objShape.Left =lngLeft ;
        this.objShape.Width =lngWidth ;
        this.objShape.Height =lngHeight;
    }

    private string RearrangeAnswers(string[] strQuestion, string strAnswer)
    {
        string str = "";
        string[] strArray = System.Text.RegularExpressions.Regex.Split(strAnswer, "  ");// Strings.Split(strAnswer, "  ", -1, 1);
        for (int i = 0; i < strQuestion.Length; i++)
        {
            int index = (i * 2) + 1;
            str = str + strQuestion[i].Replace("ThisIsAnswer", strArray[index]);
        }
        return str;
    }

    private string RearrangePassage(string[] strSentence, string strAnswer)
    {
        string str = "";
        string[] strArray = System.Text.RegularExpressions.Regex.Split(strAnswer, "  "); ;//Strings.Split(strAnswer, "  ", -1, 1);
        for (int i = 0; i < strArray.Length; i++)
        {
            str = str + strSentence[short.Parse(strArray[i]) - 1];
        }
        return str;
    }
//2018-07-05不做处理
    private string RetSlideTitle(string strText)
    {
        //int index = strText.IndexOf("(");
        //if (index < 0)
        //{
        //    index = strText.IndexOf(",");
        //    if (index < 0)
        //    {
        //        index = strText.IndexOf(".");
        //    }
        //}
        //if (index >= 0)
        //{
        //    return strText.Substring(0, index);
        //}
        return strText;
    }

    private void SetStyleOfCommerceEnglish()
    {
        this.TextBoxBiaoTi.Left = 14.125f;
        this.TextBoxBiaoTi.Top = -7.875f;
        this.TextBoxBiaoTi.Width = 466.5f;
        this.TextBoxBiaoTi.Height = 72f;
        this.TextBoxSubtitle.Left = 25.5f;
        this.TextBoxSubtitle.Top = 98.25f;
        this.TextBoxSubtitle.Width = 547.125f;
        this.TextBoxSubtitle.Height = 45.25f;
        this.TextBoxContent.Left = 27.5f;
        this.TextBoxContent.Top = 154f;
        this.TextBoxContent.Width = 690f;
        this.TextBoxContent.Height = 312f;
        this.TextBoxContent1.Left = 27.5f;
        this.TextBoxContent1.Top = 97f;
        this.TextBoxContent1.Width = 690f;
        this.TextBoxContent1.Height = 330f;
        this.txtTitle.Left = 445f;
        this.txtTitle.Top = 43.25f;
        this.txtTitle.Width = 269.25f;
        this.txtTitle.Height = 28.875f;
        this.FontBiaoTi.FontName = "Times New Roman";
        this.FontBiaoTi.FontSize = 54f;
        this.FontBiaoTi.FontBold =MsoTriState.msoTrue ;
        this.FontBiaoTi.FontColor = 0xffffff;
        this.FontSubtitle.FontName = "Arial";
        this.FontSubtitle.FontSize = 23f;
        this.FontSubtitle.FontColor = 0x3399ff;
        this.FontSubtitle.FontBold =MsoTriState.msoTrue ;
        this.FontSubtitle.FontItalic = false;
        this.FontSubtitle.FontUnderline = false;
        this.FontContent.FontName = "Arial";
        this.FontContent.FontSize = 20f;
        this.FontContent.FontColor = 0;
        this.FontContent.FontBold = 0;
        this.FontContent.FontItalic = false;
        this.FontContent.FontUnderline = false;
        this.FontTitle.FontName = "Times New Roman";
        this.FontTitle.FontSize = 18f;
        this.FontTitle.FontColor = 0xffffff;
        this.FontTitle.FontBold = 0;
        this.FontTitle.FontItalic = false;
        this.FontTitle.FontUnderline = false;
        this.PicNav_Prev = new picTotal[2];
        this.PicNav_Prev[1].Left = 513.125f;
        this.PicNav_Prev[1].Top = 502.5f;
        this.PicNav_Prev[1].Width = 26.25f;
        this.PicNav_Prev[1].Height = 26.25f;
        this.PicNav_Return = new picTotal[2];
        this.PicNav_Return[1].Left = 543.5f;
        this.PicNav_Return[1].Top = 502.5f;
        this.PicNav_Return[1].Width = 26.25f;
        this.PicNav_Return[1].Height = 26.25f;
        this.PicNav_Next = new picTotal[2];
        this.PicNav_Next[1].Left = 575.5f;
        this.PicNav_Next[1].Top = 502.5f;
        this.PicNav_Next[1].Width = 26.25f;
        this.PicNav_Next[1].Height = 26.25f;
        this.PicNav_Up = new picTotal[2];
        this.PicNav_Up[1].Left = 626.5f;
        this.PicNav_Up[1].Top = 502.5f;
        this.PicNav_Up[1].Width = 26.25f;
        this.PicNav_Up[1].Height = 26.25f;
        this.PicNav_Home = new picTotal[2];
        this.PicNav_Home[1].Left = 654.875f;
        this.PicNav_Home[1].Top = 502.5f;
        this.PicNav_Home[1].Width = 26.25f;
        this.PicNav_Home[1].Height = 26.25f;
    }


    public void StartUpAutoCreatePowerPointJCH(string JiBie, string unitID)
    {
        string[] destinationArray = null;
        int[] numArray = null;
        string[] sourceArray = null;
        string[] lastLevel = null;
        int index = 0;
        string[] strArray4 = null;
        int[] numArray2 = null;
        int num4 = 0;
        int num5 = 0;
        int num6 = 0;
        int num7 = 0;
        string[] strArray5 = null;
        int num9 = 0;
        this.Pic_Word.Left = 513.125f;
        this.Pic_Word.Top = 247.375f;
        this.Pic_Word.Width = 187.125f;
        this.Pic_Word.Height = 187.125f;
        this.picRSJ1.Left = 388.375f;
        this.picRSJ1.Top = 0f;
        this.picRSJ1.Width = 331.625f;
        this.picRSJ1.Height = 20.5f;
        this.picRSJ2.Left = 382.625f;
        this.picRSJ2.Top = 20.5f;
        this.picRSJ2.Width = 337.375f;
        this.picRSJ2.Height = 22.75f;
        this.picRSJ3.Left = 303.25f;
        this.picRSJ3.Top = 43.25f;
        this.picRSJ3.Width = 416.75f;
        this.picRSJ3.Height = 496.75f;
        this.TextBoxBiaoTi.Left = 172.875f;
        this.TextBoxBiaoTi.Top = 48.875f;
        this.TextBoxBiaoTi.Width = 547.125f;
        this.TextBoxBiaoTi.Height = 51f;
        this.TextBoxSubtitle.Left = 172.875f;
        this.TextBoxSubtitle.Top = 98.25f;
        this.TextBoxSubtitle.Width = 547.125f;
        this.TextBoxSubtitle.Height = 45.25f;
        this.TextBoxContent.Left = 195.875f;
        this.TextBoxContent.Top = 154f;
        this.TextBoxContent.Width = 522.125f;
        this.TextBoxContent.Height = 312f;
        this.TextBoxContent1.Left = 195.875f;
        this.TextBoxContent1.Top = 97f;
        this.TextBoxContent1.Width = 522.125f;
        this.TextBoxContent1.Height = 330f;
        this.FontBiaoTi.FontName = "Arial";
        this.FontBiaoTi.FontSize = 40f;
        this.FontBiaoTi.FontColor = 0xccff33;
        this.FontBiaoTi.FontBold = MsoTriState.msoTrue ;
        this.FontBiaoTi.FontItalic = false;
        this.FontBiaoTi.FontUnderline = false;
        this.FontSubtitle.FontName = "Arial";
        this.FontSubtitle.FontSize = 23f;
        this.FontSubtitle.FontColor = 0x3399ff;
        this.FontSubtitle.FontBold = MsoTriState.msoTrue ;
        this.FontSubtitle.FontItalic = false;
        this.FontSubtitle.FontUnderline = false;
        this.FontContent.FontName = "Arial";
        this.FontContent.FontSize = 20f;
        this.FontContent.FontColor = 0xffffff;
        this.FontContent.FontBold = 0;
        this.FontContent.FontItalic = false;
        this.FontContent.FontUnderline = false;
        this.FontTitle.FontName = "Arial";
        this.FontTitle.FontSize = 14f;
        this.FontTitle.FontColor = 0xe3e0bb;
        this.FontTitle.FontBold = 0;
        this.FontTitle.FontItalic = false;
        this.FontTitle.FontUnderline = false;
        this.PicNav_Home = new picTotal[2];
        this.PicNav_Home[1].Left = 655.75f;
        this.PicNav_Home[1].Top = 477.75f;
        this.PicNav_Home[1].Width = 64.25f;
        this.PicNav_Home[1].Height = 62.25f;
        this.PicNav_Prev = new picTotal[2];
        this.PicNav_Prev[1].Left = 532.125f;
        this.PicNav_Prev[1].Top = 477.75f;
        this.PicNav_Prev[1].Width = 40.75f;
        this.PicNav_Prev[1].Height = 62.25f;
        this.PicNav_Up = new picTotal[2];
        this.PicNav_Up[1].Left = 572.875f;
        this.PicNav_Up[1].Top = 477.75f;
        this.PicNav_Up[1].Width = 39.875f;
        this.PicNav_Up[1].Height = 62.25f;
        this.PicNav_Next = new picTotal[2];
        this.PicNav_Next[1].Left = 613.875f;
        this.PicNav_Next[1].Top = 477.75f;
        this.PicNav_Next[1].Width = 41.75f;
        this.PicNav_Next[1].Height = 62.25f;
        this.PicNav_Return = new picTotal[2];
        this.PicNav_Return[1].Left = 483.875f;
        this.PicNav_Return[1].Top = 477.75f;
        this.PicNav_Return[1].Width = 52.5f;
        this.PicNav_Return[1].Height = 62.25f;
        this.PicMedia_Play = new picTotal[2];
        this.PicMedia_Play[1].Left = 248.125f;
        this.PicMedia_Play[1].Top = 477.75f;
        this.PicMedia_Play[1].Width = 46.25f;
        this.PicMedia_Play[1].Height = 62.25f;
        this.PicMedia_Stop = new picTotal[2];
        this.PicMedia_Stop[1].Left = 209.25f;
        this.PicMedia_Stop[1].Top = 477.75f;
        this.PicMedia_Stop[1].Width = 38.875f;
        this.PicMedia_Stop[1].Height = 62.25f;
        this.PicMedia_Pause = new picTotal[2];
        this.PicMedia_Pause[1].Left = 172.875f;
        this.PicMedia_Pause[1].Top = 477.75f;
        this.PicMedia_Pause[1].Width = 39.875f;
        this.PicMedia_Pause[1].Height = 62.25f;
        this.txtTitle.Left = 445f;
        this.txtTitle.Top = 22.5f;
        this.txtTitle.Width = 260.875f;
        this.txtTitle.Height = 34f;
        string str2 =  ConfigurationManager.AppSettings["EnglishBookBaseVer"];
        object obj2 = str2 + @"Documents\unit" + JiBie + unitID + ".doc";
        string str = str2 + @"commonImages\";
        this.prsPres = this.OpenPres(str2 + "中职英语基础教程.pot", true);
        this.objSlide = this.ADDNewSlide("unit" + unitID, 0);
        this.InsertBackGround(this.objSlide, str + "book.jpg");
        int num2 = 0;
        Microsoft.Office.Interop.Word.Application class2 = new Microsoft.Office.Interop.Word.Application();
        object missing = Type.Missing;
        Document document = class2.Documents .Open(ref obj2, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
        for (int i = 1; i <= document.Paragraphs.Count; i++)
        {
            string slideTitle = document.Paragraphs[i].Range.Text.Trim();
            if (slideTitle != "")
            {
                string str10 = ((Style) document.Paragraphs[i].get_Style()).NameLocal.ToUpper();
                if (str10 != null)
                {
                    if (!(str10 == "UNITS"))
                    {
                        if (str10 == "标题 1")
                        {
                            goto Label_0918;
                        }
                        if (str10 == "标题 2")
                        {
                            goto Label_0E6E;
                        }
                        if (str10 == "标题 3")
                        {
                            goto Label_16C0;
                        }
                    }
                    else
                    {
                        string str9 = slideTitle;
                        this.objSlide = this.ADDNewSlide("Unit" + JiBie + unitID, 0);
                        this.InsertBackGround(this.objSlide, str + "unit" + JiBie + unitID + ".jpg");
                        this.objSlide = this.ADDNewSlide("Agenda", 0);
                        num5 = this.objSlide.SlideIndex;
                        this.InsertBackGround(this.objSlide, str + "unit" + JiBie + unitID + "agenda.jpg");
                    }
                }
            }
            continue;
        Label_0918:
            if ((lastLevel != null) && (num6 > 0))
            {
                this.objSlide = this.prsPres.Slides [num6];
                if (lastLevel[0] != "")
                {
                    this.AddHylinkLastLevel(lastLevel, ref this.objSlide);
                }
            }
            string str4 = slideTitle;
            strArray5 = new string[3];
            strArray5[0] = slideTitle;
            this.objSlide = this.ADDNewSlide(str4, 0);
            this.InsertBackGround(this.objSlide, str + "unit" + JiBie + unitID + "一级.jpg");
            num6 = this.objSlide.SlideIndex;
            destinationArray = new string[num2 + 1];
            if (sourceArray != null)
            {
                Array.Copy(sourceArray, 0, destinationArray, 0, sourceArray.Length);
            }
            destinationArray[num2] = str4;
            sourceArray = new string[num2 + 1];
            Array.Copy(destinationArray, 0, sourceArray, 0, destinationArray.Length);
            num2++;
            lastLevel = new string[] { "" };
            index = 0;
            this.objShape = this.objSlide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.TextBoxBiaoTi.Left, this.TextBoxBiaoTi.Top, this.TextBoxBiaoTi.Width, this.TextBoxBiaoTi.Height);
            this.objShape.Name ="Temp1" ;
            this.ADDTextBoxContent(objShape , sourceArray[num2 - 1], false, this.FontBiaoTi.FontName, this.FontBiaoTi.FontSize, this.FontBiaoTi.FontColor, this.FontBiaoTi.FontBold, PpParagraphAlignment.ppAlignLeft , 0.05f);
            this.AddNaviButton(ref this.objSlide, str + "level1", "Agenda");
            string textString = "";
            string strText = "";
            int num8 = i + 1;
            while (num8 <= document.Paragraphs.Count)
            {
                slideTitle = document.Paragraphs[num8].Range.Text.Trim();
                if (slideTitle != "")
                {
                    if (((Style) document.Paragraphs[num8].get_Style()).NameLocal.Substring(0, 2) == "标题")
                    {
                        break;
                    }
                    if (((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "sub")
                    {
                        if (textString == "")
                        {
                            textString = slideTitle;
                        }
                        else
                        {
                            textString = textString + "\r\n" + slideTitle;
                        }
                    }
                    else if (((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "正文")
                    {
                        if (strText == "")
                        {
                            strText = slideTitle;
                        }
                        else
                        {
                            strText = strText + "\r\n" + slideTitle;
                        }
                    }
                    else if (((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "table")
                    {
                        num9++;
                        this.PastTable(document.Tables[num9], this.objSlide, this.TextBoxContent.Left, this.TextBoxContent.Top, this.TextBoxContent.Width, this.TextBoxContent.Height);
                        num8++;
                        while ((((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "table") || (((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "正文"))
                        {
                            num8++;
                        }
                        num8--;
                    }
                }
                num8++;
            }
            if (textString != "")
            {
                this.objShape = this.objSlide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.TextBoxSubtitle.Left, this.TextBoxSubtitle.Top, this.TextBoxSubtitle.Width, this.TextBoxSubtitle.Height);
                this.objShape.Name ="Temp2";
                this.ADDTextBoxContent(this.objSlide.Shapes["Temp2"], textString, false, this.FontSubtitle.FontName, this.FontSubtitle.FontSize, this.FontSubtitle.FontColor, this.FontSubtitle.FontBold, PpParagraphAlignment.ppAlignLeft , 0.05f);
            }
            if (strText != "")
            {
                this.AddTextContent(strText, textString, ref this.objSlide);
            }
            i = num8 - 1;
            index = 0;
            continue;
        Label_0E6E:
            if ((strArray4 != null) && (num7 > 0))
            {
                this.objSlide = this.prsPres.Slides [num7];
                this.AddHylinkLastLevel(strArray4, ref this.objSlide);
            }
            string str5 = slideTitle;
            destinationArray = new string[index + 1];
            if (lastLevel != null)
            {
                Array.Copy(lastLevel, 0, destinationArray, 0, lastLevel.Length);
            }
            destinationArray[index] = str5;
            lastLevel = new string[index + 1];
            Array.Copy(destinationArray, 0, lastLevel, 0, destinationArray.Length);
            index++;
            strArray5[1] = slideTitle;
            this.objSlide = this.ADDNewSlide(slideTitle, 0);
            num7 = this.objSlide.SlideIndex ;
            this.InsertBackGround(this.objSlide, str + "unit" + JiBie + unitID + "二级.jpg");
            this.objShape = this.objSlide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.TextBoxBiaoTi.Left, this.TextBoxBiaoTi.Top, this.TextBoxBiaoTi.Width, this.TextBoxBiaoTi.Height);
            this.objShape.Name ="Temp1" ;
            this.ADDTextBoxContent(objShape , slideTitle, false, this.FontBiaoTi.FontName, this.FontBiaoTi.FontSize, this.FontBiaoTi.FontColor, this.FontBiaoTi.FontBold, PpParagraphAlignment.ppAlignLeft , 0.05f);
            this.objShape = this.objSlide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.picRSJ2.Left, this.picRSJ1.Top, this.picRSJ2.Width - 21f, this.picRSJ2.Height);
            this.objShape.Name ="TitleYSJ" ;
            this.AddContentLastLevel(objShape , strArray5[0], this.FontBiaoTi.FontName, 20f, this.FontBiaoTi.FontColor);
            num7 = this.objSlide.SlideIndex ;
            this.AddNaviButton(ref this.objSlide, str + "level2", sourceArray[num2 - 1], "Agenda");
            if (strArray5[0] == "Listening")
            {
                this.InsertPictureOperation(this.objSlide, str + @"level2\play.jpg", "", "play", this.PicMedia_Play[1].Left, this.PicMedia_Play[1].Top, this.PicMedia_Play[1].Width, this.PicMedia_Play[1].Height, true, "Mplay", "");
                this.InsertPictureOperation(this.objSlide, str + @"level2\stop.jpg", "", "stop", this.PicMedia_Stop[1].Left, this.PicMedia_Stop[1].Top, this.PicMedia_Stop[1].Width, this.PicMedia_Stop[1].Height, true, "Mstop", "");
                this.InsertPictureOperation(this.objSlide, str + @"level2\pause.jpg", "", "pause", this.PicMedia_Pause[1].Left, this.PicMedia_Pause[1].Top, this.PicMedia_Pause[1].Width, this.PicMedia_Pause[1].Height, true, "Mpause", "");
            }
            strText = "";
            textString = "";
            num8 = i + 1;
            while (num8 <= document.Paragraphs.Count)
            {
                slideTitle = document.Paragraphs[num8].Range.Text.Trim();
                if (slideTitle != "")
                {
                    if (((Style) document.Paragraphs[num8].get_Style()).NameLocal.Substring(0, 2) == "标题")
                    {
                        break;
                    }
                    if (((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "sub")
                    {
                        if (textString == "")
                        {
                            textString = slideTitle;
                        }
                        else
                        {
                            textString = textString + "\r\n" + slideTitle;
                        }
                        if (textString != "")
                        {
                            this.objShape = this.objSlide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.TextBoxSubtitle.Left, this.TextBoxSubtitle.Top, this.TextBoxSubtitle.Width, this.TextBoxSubtitle.Height);
                            this.objShape.Name ="Temp2" ;
                            this.ADDTextBoxContent(objShape , textString, false, this.FontSubtitle.FontName, this.FontSubtitle.FontSize, this.FontSubtitle.FontColor, this.FontSubtitle.FontBold,PpParagraphAlignment.ppAlignLeft , 0.05f);
                        }
                    }
                    else if (((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "正文")
                    {
                        strText = "";
                        while ((((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "text") || (((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "正文"))
                        {
                            if (slideTitle != "")
                            {
                                if (strText == "")
                                {
                                    strText = slideTitle;
                                }
                                else
                                {
                                    strText = strText + "\r\n" + slideTitle;
                                }
                            }
                            num8++;
                            if (num8 > document.Paragraphs.Count)
                            {
                                break;
                            }
                            slideTitle = document.Paragraphs[num8].Range.Text.Trim();
                        }
                        if ((strText != "") && (strText.Length > 1))
                        {
                            this.AddTextContent(strText, textString, ref this.objSlide);
                            i = num8 - 1;
                            num8--;
                        }
                    }
                    else if (((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "table")
                    {
                        num9++;
                        this.PastTable(document.Tables [num9], this.objSlide, this.TextBoxContent.Left, this.TextBoxContent.Top, this.TextBoxContent.Width, this.TextBoxContent.Height);
                        num8++;
                        while ((((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "table") || (((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "正文"))
                        {
                            num8++;
                            if (num8 > document.Paragraphs.Count)
                            {
                                break;
                            }
                        }
                        num8--;
                    }
                }
                num8++;
            }
            strArray4 = new string[1];
            numArray2 = new int[1];
            num4 = 0;
            i = num8 - 1;
            continue;
        Label_16C0:
            strArray5[2] = slideTitle;
            string str6 = slideTitle;
            this.objSlide = this.ADDNewSlide(str6, 0);
            destinationArray = new string[num4 + 1];
            numArray = new int[num4 + 1];
            if (strArray4 != null)
            {
                Array.Copy(strArray4, 0, destinationArray, 0, strArray4.Length);
            }
            Array.Copy(numArray2, 0, numArray, 0, numArray2.Length);
            destinationArray[num4] = str6;
            numArray[num4] = this.objSlide.SlideIndex ;
            strArray4 = new string[num4 + 1];
            numArray2 = new int[num4 + 1];
            Array.Copy(destinationArray, 0, strArray4, 0, destinationArray.Length);
            Array.Copy(numArray, 0, numArray2, 0, numArray.Length);
            num4++;
            this.InsertBackGround(this.objSlide, str + "unit" + JiBie + unitID + "二级.jpg");
            this.objShape = this.objSlide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.picRSJ2.Left, this.picRSJ1.Top, this.picRSJ2.Width - 21f, this.picRSJ2.Height);
            this.objShape.Name ="TitleYSJ";
            this.AddContentLastLevel(objShape , strArray5[1], this.FontBiaoTi.FontName, 20f, this.FontBiaoTi.FontColor);
            this.objShape = this.objSlide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.TextBoxBiaoTi.Left, this.TextBoxBiaoTi.Top, this.TextBoxBiaoTi.Width, this.TextBoxBiaoTi.Height);
            this.objShape.Name ="Temp1";
            this.ADDTextBoxContent(objShape , slideTitle, false, this.FontBiaoTi.FontName, this.FontBiaoTi.FontSize, this.FontBiaoTi.FontColor, this.FontBiaoTi.FontBold, PpParagraphAlignment.ppAlignLeft , 0.05f);
            strText = "";
            textString = "";
            num8 = i + 1;
            while (num8 <= document.Paragraphs.Count)
            {
                slideTitle = document.Paragraphs[num8].Range.Text.Trim();
                if (((Style) document.Paragraphs[num8].get_Style()).NameLocal.Substring(0, 2) == "标题")
                {
                    break;
                }
                if (((Style) document.Paragraphs[num8].get_Style()).NameLocal == "正文")
                {
                    if (strText == "")
                    {
                        strText = slideTitle;
                    }
                    else
                    {
                        strText = strText + "\r\n" + slideTitle;
                    }
                }
                else if (((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "sub")
                {
                    if (textString == "")
                    {
                        textString = slideTitle;
                    }
                    else
                    {
                        textString = textString + "\r\n" + slideTitle;
                    }
                }
                else if (((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "table")
                {
                    num9++;
                    this.PastTable(document.Tables [num9], this.objSlide, this.TextBoxContent.Left, this.TextBoxContent.Top, this.TextBoxContent.Width, this.TextBoxContent.Height);
                    num8++;
                    while ((((Style) document.Paragraphs[num8].get_Style()).NameLocal.ToLower() == "table") || (((Style) document.Paragraphs[num8].get_Style()).NameLocal == "正文"))
                    {
                        num8++;
                    }
                    num8--;
                }
                num8++;
            }
            if (textString != "")
            {
                this.objShape = this.objSlide.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, this.TextBoxSubtitle.Left, this.TextBoxSubtitle.Top, this.TextBoxSubtitle.Width, this.TextBoxSubtitle.Height);
                this.objShape.Name ="Temp2" ;
                this.ADDTextBoxContent(objShape , textString, false, this.FontSubtitle.FontName, this.FontSubtitle.FontSize, this.FontSubtitle.FontColor, this.FontSubtitle.FontBold, PpParagraphAlignment.ppAlignLeft , 0.05f);
            }
            if (strText != "")
            {
                this.AddTextContent(strText, textString, ref this.objSlide);
            }
            this.AddNaviButton(ref this.objSlide, str + "level2", lastLevel[index - 1], "Agenda");
            i = num8 - 1;
        }
        if (sourceArray[0] != "")
        {
            this.objSlide = this.prsPres.Slides [num6];
            if (lastLevel[0] != "")
            {
                this.AddHylinkLastLevel(lastLevel, ref this.objSlide);
            }
        }
        if (num7 > 0)
        {
            this.objSlide = this.prsPres.Slides [num7];
            if (strArray4[0] != "")
            {
                this.AddHylinkLastLevel(strArray4, ref this.objSlide);
            }
        }
        this.objSlide = this.prsPres.Slides[num5];
        if (sourceArray[0] != "")
        {
            this.AddHylinkLastLevel(sourceArray, ref this.objSlide);
        }
        this.objSlide = this.ADDNewSlide("Thank You", 0);
        this.InsertBackGround(this.objSlide, str + "thank you.jpg");
        this.objSlide = this.ADDNewSlide("Credits", 0);
        this.InsertBackGround(this.objSlide, str + "Credits.jpg");
        object obj4 = System.Reflection.Missing.Value  ;
        if (document != null)
        {
            document.Close(ref obj4, ref obj4, ref obj4);
        }
        class2.Quit(ref obj4, ref obj4, ref obj4);
        MessageBox.Show("ok");
    }

    public void testopenword()
    {
        object obj2 = @"D:\我的共享\中职英语基础版\Documents\unit01.doc";
        Microsoft.Office.Interop.Word.Application application = new Microsoft.Office.Interop.Word.Application();
        Document document = null;
        Microsoft.Office.Interop.PowerPoint.Application application2 = new Microsoft.Office.Interop.PowerPoint.Application();
        object missing = Type.Missing;
        try
        {
            application.Visible = true;
            application2.Visible = MsoTriState.msoTrue;
            Presentation presentation = application2.Presentations.Open(@"D:\我的共享\中职英语基础版\中职英语基础教程.ppt", MsoTriState.msoTrue, MsoTriState.msoTrue, MsoTriState.msoTrue);
            document = application.Documents .Open(ref obj2, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
        finally
        {
            if (document != null)
            {
                document.Close(ref missing, ref missing, ref missing);
            }
            application.Quit(ref missing, ref missing, ref missing);
        }
    }

        public void WordLinkOfText(Microsoft.Office.Interop.PowerPoint.Shape tempShape, string[] FindWords)
        {
            
            int num2 = 0;
           foreach (string FindWord in FindWords )
            {
                int num3 = tempShape.TextFrame.TextRange.Text.IndexOf(FindWord,0,StringComparison.OrdinalIgnoreCase);
                if (num3 >= 0)
                {
                    num2 = 0;
                    //int startIndex = 0;
                    //while (startIndex < num3)
                    //{
                    //    startIndex = tempShape.TextFrame.TextRange.Text.IndexOf("\r", startIndex);
                    //    if (startIndex < 0)
                    //    {
                    //        return;
                    //    }
                    //    if (startIndex < num3)//单词前面有回车
                    //    {
                    //        num2++;
                    //        startIndex++;
                    //    }
                    //}
                    string word = this.GetWord(tempShape.TextFrame.TextRange.Text, FindWord);
                    tempShape.TextFrame.TextRange.Characters((num3 + 1) - num2, word.Trim().Length).ActionSettings[PpMouseActivation.ppMouseClick].Action = PpActionType.ppActionHyperlink;//  .get_ActionSettings().get_Item(1).get_Hyperlink().set_Address("");
                    tempShape.TextFrame.TextRange.Characters((num3 + 1) - num2, word.Trim().Length).ActionSettings[PpMouseActivation.ppMouseClick].Hyperlink.SubAddress = FindWord;
                }
            }
        }
        public void WordColorByFindWord(Microsoft.Office.Interop.PowerPoint.Shape tempShape, string FindWord, string FontName, float FontSize, MsoTriState FontBold, int FontColor, MsoTriState FontUnderline, MsoTriState FontItalic)
        {
            int num2 = 0;
            if (FindWord.Trim() != "")
            {
                int num3 = tempShape.TextFrame.TextRange.Text.ToUpper().IndexOf(FindWord, 0, 1);
                if (num3 >= 0)
                {
                    num2 = 0;
                    int startIndex = 0;
                    while (startIndex < num3)
                    {
                        startIndex = tempShape.TextFrame.TextRange.Text.IndexOf("\r", startIndex);
                        if (startIndex < 0)
                        {
                            return;
                        }
                        if (startIndex < num3)
                        {
                            num2++;
                            startIndex++;
                        }
                    }
                    string word = this.GetWord(tempShape.TextFrame.TextRange.Text, FindWord);
                    tempShape.TextFrame.TextRange.Characters((num3 + 1) - num2, word.Trim().Length).Font.Name = FontName;
                    tempShape.TextFrame.TextRange.Characters((num3 + 1) - num2, word.Trim().Length).Font.Size = FontSize;
                    tempShape.TextFrame.TextRange.Characters((num3 + 1) - num2, word.Trim().Length).Font.Bold = FontBold;
                    tempShape.TextFrame.TextRange.Characters((num3 + 1) - num2, word.Trim().Length).Font.Color.RGB = FontColor;
                    tempShape.TextFrame.TextRange.Characters((num3 + 1) - num2, word.Trim().Length).Font.Underline = FontUnderline;
                    tempShape.TextFrame.TextRange.Characters((num3 + 1) - num2, word.Trim().Length).Font.Italic = FontItalic;
                }
            }
        }

    // Nested Types
    public struct picTotal
    {
        public float Left;
        public float Top;
        public float Width;
        public float Height;
    }

    public struct tempFont
    {
        public string FontName;
        public float FontSize;
        public int FontColor;
        public MsoTriState FontBold;
        public bool FontItalic;
        public bool FontUnderline;
    }
}

}
