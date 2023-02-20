using Edis.Diagnostics;
using Edis.Entities.Common;
using Edis.Functions.Base;
using Edis.Repositories.Contexts;
using Edis.ViewModels.Common;
using Novacode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Edis.Functions.Common
{

    public class NyomtatvanySablonFunctionBase : KonasoftBVFonixFunctionsBase<NyomtatvanySablonModel, NyomtatvanySablon>, INyomtatvanySablonFunctionBase
    {
        #region Injects

        #endregion

        public DbSet<NyomtatvanySablon> Table
        {
            get { return this.KonasoftBVFonixContext.NyomtatvanySablon; }
        }

        #region Common functions
        public byte[] NyomtatvanySablonDocxById(int id)
        {
            return KonasoftBVFonixContext.NyomtatvanySablon.Where(w => w.Id == id).Select(x => x.NyomtatvanySablonDocx).First();
        }

        public byte[] NyomtatvanySablonDocxCheck()
        {
            return KonasoftBVFonixContext.NyomtatvanySablon.First().NyomtatvanySablonDocx;
        }

        public DocX NyomtatvanySablonFeltoltesDocx(int docId, List<DocTableData> lista, List<DocWordReplace> szoCsere)
        {
            Log.Info($"NyomtatványSablonFeltoltesDocx. DocId: {docId}");
            DocX sablon;
            //            byte[] data = File.ReadAllBytes(@"d:\temp\810.docx");
            //          using (MemoryStream stream = new MemoryStream(data))
            using (MemoryStream stream = new MemoryStream(NyomtatvanySablonDocxById(docId)))
            {
                sablon = DocX.Load(stream);
            }


            foreach (DocTableData docTableData in lista)
            {
                try
                {


                    Table docTable = sablon.Tables.Find(delegate (Table bk)
                    {
                        return bk.Xml.Value.Contains(docTableData.Cserelendo);
                    });

                    if (docTable == null)
                        throw new Exception($"Hiba a dokumentum generálása közben. Nem található a {docTableData.Cserelendo} táblázat");

                    List<double> columnWidths = docTable.ColumnWidths;

                    docTable.Rows[docTableData.Sor].Remove();

                    int i = docTableData.Sor == 0 ? 0 : docTableData.Sor - 1;
                    int db = 0;
                    foreach (List<string> list in docTableData.Adat)
                    {
                        try
                        {
                            docTable.InsertRow(i + 1);
                            db++;

                            int j = 0;
                            foreach (string szoveg in list)
                            {
                                //docTable.Rows[i + 1].Cells[j].Width= columnWidths[j];
                                docTable.Rows[i + 1].Cells[j].Width = docTable.Rows[i].Cells[j].Width;
                                docTable.Rows[i + 1].Cells[j].Paragraphs.First().SetLineSpacing(LineSpacingType.Line, 1);
                                docTable.Rows[i + 1].Cells[j].Paragraphs.First().SetLineSpacing(LineSpacingType.After, 0);
                                docTable.Rows[i + 1].Cells[j].Paragraphs.First().Alignment = Alignment.right;
                                docTable.Rows[i + 1].Cells[j].Paragraphs.First().Append(szoveg);
                                if (db == docTableData.Adat.Count && docTableData.VanMindosszesenSor)
                                {
                                    docTable.Rows[i + 1].Cells[j].Paragraphs.First().Bold();
                                }
                                j++;
                            }


                            i++;
                        }
                        catch (Exception exc)
                        {
                            Log.Info("NyomtatvanySablonFeltoltesDocx oszlopok száma: " + lista.Count + " értékek: " + string.Join("|", list));

                            throw exc;
                        }
                    }
                }
                catch (Exception exc)
                {
                    Log.Error("NyomtatvanySablonFeltoltesDocx táblázat: " + docTableData.Cserelendo, exc);
                    throw exc;
                }
            }


            foreach (DocWordReplace docWordReplace in szoCsere)
            {
                try
                {
                    sablon.ReplaceText(docWordReplace.RegiErtek, docWordReplace.UjErtek);
                }
                catch (Exception ex)
                {
                    Log.Error($"Hibás érték a {docWordReplace.RegiErtek}");
                    throw new Exception($"A dokumentum sablonban található {docWordReplace.RegiErtek} érték nem tölthető fel, mivel a szükséges adat nem áll rendelkezésre.", ex);
                }

            }

            return sablon;

        }

        public DocX NyomtatvanySablonFeltoltesDocxFormazassal(int docId, List<DocTableData> lista, List<DocWordReplace> szoCsere)
        {
            Log.Info($"NyomtatványSablonFeltoltesDocx. DocId: {docId}");
            DocX sablon;

            //byte[] data = File.ReadAllBytes(@"c:\temp\1002.docx");
            //using (MemoryStream stream = new MemoryStream(data))
            using (MemoryStream stream = new MemoryStream(NyomtatvanySablonDocxById(docId)))
            {
                sablon = DocX.Load(stream);
            }


            foreach (DocTableData docTableData in lista)
            {
                try
                {
                    Table docTable = sablon.Tables.Find(delegate (Table bk)
                    {
                        return bk.Xml.Value.Contains(docTableData.Cserelendo);
                    });

                    if (docTable == null)
                        throw new Exception("Hiba a dokumentum generálása közben");

                    //docTable.Rows[docTableData.Sor].Remove();

                    int i = docTableData.Sor;//== 0 ? 0 : docTableData.Sor - 1;

                    var sampleRow = docTable.Rows[i];
                    sampleRow.Cells[0].Paragraphs.First().RemoveText(0);

                    foreach (List<string> list in docTableData.Adat)
                    {
                        try
                        {
                            docTable.InsertRow(sampleRow, i + 1);


                            int j = 0;
                            foreach (string szoveg in list)
                            {
                                docTable.Rows[i + 1].Cells[j].Paragraphs.First().Append(szoveg);
                                j++;
                            }

                            i++;
                        }
                        catch (Exception exc)
                        {
                            Log.Info("NyomtatvanySablonFeltoltesDocx oszlopok száma: " + lista.Count + " értékek: " + string.Join("|", list));

                            throw exc;
                        }
                    }
                    docTable.Rows[docTableData.Sor].Remove();
                }
                catch (Exception exc)
                {
                    Log.Error("NyomtatvanySablonFeltoltesDocx táblázat: " + docTableData.Cserelendo, exc);
                    throw exc;
                }
            }


            foreach (DocWordReplace docWordReplace in szoCsere)
            {
                try
                {
                    sablon.ReplaceText(docWordReplace.RegiErtek, docWordReplace.UjErtek);
                }
                catch (Exception ex)
                {
                    Log.Error($"Hibás érték a {docWordReplace.RegiErtek}", ex);
                    throw new Exception($"A dokumentum sablonban található {docWordReplace.RegiErtek} érték nem tölthető fel, mivel a szükséges adat nem áll rendelkezésre.", ex);
                }

            }

            return sablon;
        }

        public DocX NyomtatvanySablonFeltoltesDocxBFBFormazassal(int docId, List<DocTableData> lista, List<DocWordReplace> szoCsere)
        {
            Log.Info($"NyomtatványSablonFeltoltesBFBDocx. DocId: {docId}");
            DocX sablon;

            //byte[] data = File.ReadAllBytes(@"c:\temp\1002.docx");
            //using (MemoryStream stream = new MemoryStream(data))
            using (MemoryStream stream = new MemoryStream(NyomtatvanySablonDocxById(docId)))
            {
                sablon = DocX.Load(stream);
            }


            foreach (DocTableData docTableData in lista)
            {
                try
                {
                    Table docTable = sablon.Tables.Find(delegate (Table bk)
                    {
                        return bk.Xml.Value.Contains(docTableData.Cserelendo);
                    });

                    if (docTable == null)
                        throw new Exception("Hiba a dokumentum generálása közben");

                    //docTable.Rows[docTableData.Sor].Remove();

                    int i = docTableData.Sor;//== 0 ? 0 : docTableData.Sor - 1;

                    var sampleRow = docTable.Rows[i];
                    sampleRow.Cells[0].Paragraphs.First().RemoveText(0);

                    foreach (List<string> list in docTableData.Adat)
                    {
                        try
                        {
                            docTable.InsertRow(sampleRow, i + 1);


                            int j = 0;
                            foreach (string szoveg in list)
                            {
                                docTable.Rows[i + 1].Cells[j].Paragraphs.First().Alignment = Alignment.left;
                                docTable.Rows[i + 1].Cells[j].Paragraphs.First().Append(szoveg);
                                j++;
                            }

                            i++;
                        }
                        catch (Exception exc)
                        {
                            Log.Info("NyomtatvanySablonFeltoltesDocx oszlopok száma: " + lista.Count + " értékek: " + string.Join("|", list));

                            throw exc;
                        }
                    }
                    docTable.Rows[docTableData.Sor].Remove();
                }
                catch (Exception exc)
                {
                    Log.Error("NyomtatvanySablonFeltoltesDocx táblázat: " + docTableData.Cserelendo, exc);
                    throw exc;
                }
            }


            foreach (DocWordReplace docWordReplace in szoCsere)
            {
                try
                {
                    sablon.ReplaceText(docWordReplace.RegiErtek, docWordReplace.UjErtek);
                }
                catch (Exception ex)
                {
                    Log.Error($"Hibás érték a {docWordReplace.RegiErtek}", ex);
                    throw new Exception($"A dokumentum sablonban található {docWordReplace.RegiErtek} érték nem tölthető fel, mivel a szükséges adat nem áll rendelkezésre.", ex);
                }

            }

            return sablon;
        }

        public class DocTableData
        {
            public string Cserelendo { get; set; }
            public int Sor { get; set; }
            public bool VanMindosszesenSor { get; set; }
            public List<List<string>> Adat { get; set; }
        }

        public class DocWordReplace
        {
            public string RegiErtek { get; set; }
            public string UjErtek { get; set; }
        }


        public byte[] NyomtatvanyokOsszefuzeseByteTomb(List<byte[]> nyomtatvanyok)
        {
            var list = new List<DocX>();

            foreach (byte[] item in nyomtatvanyok)
            {
                list.Add(DocX.Load(new MemoryStream(item)));
            }
            var sablon = NyomtatvanyokOsszefuzese(list);
            using (MemoryStream ms = new MemoryStream())
            {
                if (sablon != null)
                {
                    sablon.SaveAs(ms);
                    return ms.ToArray();
                }
                return null;
            }

        }

        public DocX NyomtatvanyokOsszefuzese(List<DocX> nyomtatvanyok)
        {

            DocX sablon = null;

            foreach (var item in nyomtatvanyok)
            {
                if (sablon == null)
                {
                    sablon = item;
                }
                else
                {
                    sablon.InsertSectionPageBreak();
                    sablon.InsertDocument(item);
                }

            }

            return sablon;
        }

        public int SzamKerekitesForintra(int szam)
        {
            var x = (decimal)szam;
            var k = 5;
            return (int)Math.Round(x / k, MidpointRounding.AwayFromZero) * k;
        }

        #region SzamValtasaBeture
        private string[] aOne = { "", "egy", "kettő", "három", "négy", "öt", "hat", "hét", "nyolc", "kilenc" };
        private string[] aTwo = { "", "tíz", "húsz", "harminc", "negyven", "ötven", "hatvan", "hetven", "nyolcvan", "kilencven" };
        private int[] ai = new int[7];

        private string S1()
        {
            return aOne[ai[0]];
        }

        private string S2()
        {
            if (ai[0] == 0)
            {
                return aTwo[ai[1]] + S1();
            }
            else
            {
                switch (ai[1])
                {
                    case 1:
                        return "tizen" + S1();
                    case 2:
                        return "huszon" + S1();
                    default:
                        return aTwo[ai[1]] + S1();
                }
            }
        }

        private string S3()
        {
            if (ai[2] == 0)
            {
                return S2();
            }
            else
            {
                return aOne[ai[2]] + "száz" + S2();
            }
        }

        private string S4()
        {
            if (ai[3] == 0)
            {
                return S3();
            }
            else
            {
                return aOne[ai[3]] + "ezer" + S3();
            }
        }

        private string S5()
        {
            string s;
            if (ai[3] == 0)
            {
                s = aTwo[ai[4]];
            }
            else
            {
                switch (ai[4])
                {
                    case 1:
                        s = "tizen";
                        break;
                    case 2:
                        s = "huszon";
                        break;
                    default:
                        s = aTwo[ai[4]];
                        break;
                }
            }
            if (ai[3] == 0)
            {
                s = s + "ezer";
            }
            return s + S4();
        }

        private string S6()
        {
            if (ai[5] == 0)
            {
                return aOne[ai[5]] + S5();
            }
            else
            {
                return aOne[ai[5]] + "száz" + S5();
            }
        }

        private string S7()
        {
            return aOne[ai[6]] + "millió" + S6();
        }

        public string SzamValtasaBeture(int CurrencyValue)
        {
            if (CurrencyValue == 0)
            {
                return "nulla";
            }
            else
            {
                if (Math.Abs(CurrencyValue) < 10000000)
                {
                    int i = 0;
                    string s = "";
                    if (CurrencyValue < 0)
                    {
                        CurrencyValue = Math.Abs(CurrencyValue);
                        s = "minusz ";
                    }
                    int l = CurrencyValue;
                    while (l > 0)
                    {
                        ai[i] = l % 10;
                        l = l / 10;
                        i++;
                    }
                    switch (i)
                    {
                        case 7:
                            s = s + S7();
                            break;
                        case 6:
                            s = s + S6();
                            break;
                        case 5:
                            s = s + S5();
                            break;
                        case 4:
                            s = s + S4();
                            break;
                        case 3:
                            s = s + S3();
                            break;
                        case 2:
                            s = s + S2();
                            break;
                        case 1:
                            s = s + S1();
                            break;
                    }
                    return s;
                }
                else
                {
                    throw new Exception("Maximum 7 jegyű szám lehet!");
                }
            }
        }

        #endregion SzamValtasaBeture

        #endregion Common functions

        //        #region FTTR

        public List<NyomtatvanySablonModel> GetModulNyomtatvanyok(string modelNev)
        {
            List<NyomtatvanySablonModel> models = new List<NyomtatvanySablonModel>();

            foreach (var item in Table.Where(x => x.ModulMegnevezese == modelNev).ToList())
            {
                models.Add((NyomtatvanySablonModel)item);
            }

            return models;
        }

        // Summernote HTML konvertálása DocX formátumba
        public DocX SummernoteHtmlToDocx(DocX doc, string html)
        {
            html = html.Replace("<p><br /></p>", "<p> </p>").Replace("<p><br></p>", "<p> </p>");
            var paragraphs = Regex.Split(html, @"(<p>[\s\S]+?<\/p>)").Where(l => l != string.Empty).ToList();
            int? naploParagraphIndex = doc.Paragraphs.ToList().FindIndex(w => w.Text == "%NAPLO%");
            bool sorkizart = false;

            foreach (var paragraph in paragraphs)
            {
                if (paragraph.Contains("Bizonyítási eszközök:")) sorkizart = true;
                SummernoteHtmlToDocxInstertParagraph(ref doc, paragraph, naploParagraphIndex, sorkizart);
                //naploParagraphIndex = null;
                if (naploParagraphIndex != -1) naploParagraphIndex++;
            }
            return doc;
        }

        public void SummernoteHtmlToDocxInstertParagraph(ref DocX doc, string currentParagraph, int? paragraphIndex = -1, bool sorkizart = false)
        {
            Novacode.Paragraph newParagraph;

            Formatting pFormat = new Formatting();

            currentParagraph = currentParagraph.Replace("<p>", "").Replace("<div>", "").Replace("</div>", "").Replace(" style=\"\"", "");
            if (currentParagraph.Replace("</p>", "") != "")
            {
                if (paragraphIndex != -1)
                {
                    Paragraph p = doc.Paragraphs[paragraphIndex.Value];
                    newParagraph = p.InsertParagraphBeforeSelf("", false, pFormat);
                }
                else
                {
                    newParagraph = doc.InsertParagraph("", false, pFormat);
                }

                newParagraph.LineSpacingAfter = 6;
            }
            else return;

            List list = null;
            bool isListOrElement = false;
            bool finalizeList = false;

            var blocks = Regex.Split(currentParagraph, @"(<[\s\S]+?/[\s\S]+?>)").Where(l => l != string.Empty).ToList();
            {
                foreach (var block in blocks)
                {
                    isListOrElement = false;
                    finalizeList = false;

                    Novacode.Formatting format = new Novacode.Formatting();

                    var currentBlock = block.Replace("<p>", "").Replace("</p>", ""); // \n volt

                    if (currentBlock.Contains("<center>") || currentBlock.Contains("<p align=\"center\">"))
                    {
                        newParagraph.Alignment = Alignment.center;
                    }
                    else
                    {
                        if (sorkizart)
                            newParagraph.Alignment = Alignment.both;
                        else
                            newParagraph.Alignment = Alignment.left;
                    }
                    currentBlock = currentBlock.Replace("<center>", "").Replace("</center>", "").Replace("<p align=\"center\">", "");

                    if (currentBlock.Contains("<h3>"))
                    {
                        format.Size = 16;
                    }
                    else if (currentBlock.Contains("<h5>"))
                    {
                        format.Size = 14;
                    }
                    else format.Size = 12;
                    currentBlock = currentBlock.Replace("<h3>", "").Replace("</h3>", "");
                    currentBlock = currentBlock.Replace("<h5>", "").Replace("</h5>", "");

                    if (currentBlock.Contains("<strong>") || currentBlock.Contains("<b>"))
                    {
                        format.Bold = true;
                    }
                    currentBlock = currentBlock.Replace("<strong>", "").Replace("</strong>", "").Replace("<b>", "").Replace("</b>", "");

                    if (currentBlock.Contains("<i>"))
                    {
                        format.Italic = true;
                    }
                    currentBlock = currentBlock.Replace("<i>", "").Replace("</i>", "");

                    if (currentBlock.Contains("<u>"))
                    {
                        format.UnderlineStyle = UnderlineStyle.singleLine;
                    }
                    currentBlock = currentBlock.Replace("<u>", "").Replace("</u>", "");

                    // előre vesszük

                    if (currentBlock.Contains("<div style=\"font-size:")) // span eddig
                    {
                        int fontSize;
                        if (int.TryParse(Regex.Match(currentBlock, @"\d+").Value, out fontSize))
                        {
                            format.Size = fontSize;
                        }
                        var removeSpanBlock = currentBlock
                            .Replace("<ul>", "").Replace("</ul>", "")
                            .Replace("<li>", "").Replace("</li>", "")
                            .Split(new string[] { "px;\">", "</div>" }, StringSplitOptions.None);
                        currentBlock = currentBlock.Replace(removeSpanBlock[0] + "px;\">", "");
                    }
                    currentBlock = currentBlock.Replace("</div>", ""); //span eddig

                    currentBlock = currentBlock.Replace("<br />", "\n").Replace("<br/>", "\n").Replace("<br>", "\n");


                    if (currentBlock.Contains("<ul>"))
                    {
                        isListOrElement = true;
                        list = doc.AddList(null, 0, ListItemType.Bulleted);
                    }
                    if (currentBlock.Contains("<ol>"))
                    {
                        isListOrElement = true;
                        list = doc.AddList(null, 0, ListItemType.Numbered);
                    }
                    currentBlock = currentBlock.Replace("<ul>", "").Replace("<ol>", "");

                    if (currentBlock.Contains("</ul>") || currentBlock.Contains("</ol>"))
                    {
                        isListOrElement = true;
                        finalizeList = true;
                    }
                    currentBlock = currentBlock.Replace("</ul>", "\n").Replace("</ol>", "\n");

                    if (currentBlock.Contains("<li>"))
                    {
                        isListOrElement = true;
                        doc.AddListItem(list, currentBlock.Replace("<li>", "").Replace("</li>", ""));
                    }
                    currentBlock = currentBlock.Replace("<li>", "").Replace("</li>", "");

                    if (isListOrElement)
                    {
                        if (finalizeList)
                            doc.InsertList(list);
                    }

                    if (!isListOrElement || (isListOrElement && finalizeList && currentBlock != ""))
                    {
                        if (isListOrElement) SummernoteHtmlToDocxInstertParagraph(ref doc, currentBlock);
                        else newParagraph.InsertText(currentBlock, false, format);
                    }
                }
            }
        }
        public DocX EsemenyJelentoNyomtatvanySablonDocxFormazassal(int docId, List<DocTableData> lista, List<DocWordReplace> szoCsere)
        {
            Log.Info($"NyomtatványSablonFeltoltesDocx. DocId: {docId}");
            DocX sablon;

            //byte[] data = File.ReadAllBytes(@"c:\temp\1002.docx");
            //using (MemoryStream stream = new MemoryStream(data))
            using (MemoryStream stream = new MemoryStream(NyomtatvanySablonDocxById(docId)))
            {
                sablon = DocX.Load(stream);
            }


            foreach (DocTableData docTableData in lista)
            {
                try
                {
                    Table docTable = sablon.Tables.Find(delegate (Table bk)
                    {
                        return bk.Xml.Value.Contains(docTableData.Cserelendo);
                    });

                    if (docTable == null)
                        throw new Exception("Hiba a dokumentum generálása közben");

                    //docTable.Rows[docTableData.Sor].Remove();

                    int i = docTableData.Sor;//== 0 ? 0 : docTableData.Sor - 1;


                    var sampleRow = docTable.Rows[i];
                    sampleRow.Cells[0].Paragraphs.First().RemoveText(0);

                    foreach (List<string> list in docTableData.Adat)
                    {
                        try
                        {
                            docTable.InsertRow(sampleRow, i + 1);

                            int j = 0;
                            foreach (string szoveg in list)
                            {
                                if (szoveg == "%URES%")
                                {
                                    docTable.Rows[i + 1].Cells[j].FillColor = System.Drawing.Color.FromName("Gray");

                                }
                                else
                                {

                                    docTable.Rows[i + 1].Cells[j].Paragraphs.First().Append(szoveg);
                                }
                                j++;
                            }

                            i++;
                        }
                        catch (Exception exc)
                        {
                            Log.Info("NyomtatvanySablonFeltoltesDocx oszlopok száma: " + lista.Count + " értékek: " + string.Join("|", list));

                            throw exc;
                        }
                    }

                    docTable.Rows[docTableData.Sor].Remove();
                }
                catch (Exception exc)
                {
                    Log.Error("NyomtatvanySablonFeltoltesDocx táblázat: " + docTableData.Cserelendo, exc);
                    throw exc;
                }
            }


            foreach (DocWordReplace docWordReplace in szoCsere)
            {
                try
                {
                    sablon.ReplaceText(docWordReplace.RegiErtek, docWordReplace.UjErtek);
                }
                catch (Exception ex)
                {
                    Log.Error($"Hibás érték a {docWordReplace.RegiErtek}", ex);
                    throw new Exception($"A dokumentum sablonban található {docWordReplace.RegiErtek} érték nem tölthető fel, mivel a szükséges adat nem áll rendelkezésre.", ex);
                }

            }

            return sablon;
        }
    }
}

