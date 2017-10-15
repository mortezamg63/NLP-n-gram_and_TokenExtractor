using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using HtmlAgilityPack;

namespace TokenExtraction1
{
    public partial class Form1 : Form
    {        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MemoryClass.Instance.ClearAll();            

            openFileDialog1.Filter = "HtmFile (*.htm)|*.htm|Htmlfile (*.html)|*.html|MhtFile(*.mht)|*.mht";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                StreamReader file = new StreamReader(openFileDialog1.FileName);
                richTextBox1.Text = file.ReadToEnd();
                file.Close();
                button3.Enabled = true;
                button2.Enabled = false;
                btnReport.Enabled = false;
                richTextBox2.Text = string.Empty;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Regex_Patterns.Instance.ClearDigitList();
            Regex_Patterns.Instance.ClearEndMarkList();
            Regex_Patterns.Instance.ClearSignList();
            Regex_Patterns.Instance.ClearSentenceList();
            Regex_Patterns.Instance.ClearWordList();
            Regex_Patterns.Instance.ClearCurrentSentence();
            Regex_Patterns.Instance.Tape = richTextBox2.Text;
            Regex_Patterns.Instance.HeadTape = -1;

            MemoryClass.Instance.ClearAll();

            while(Regex_Patterns.Instance.HeadTape<Regex_Patterns.Instance.Tape.Length-1)
                new S0().Compute();

            btnReport.Enabled = true;                      
            
           }

        private void btnReport_Click(object sender, EventArgs e)
        {
            new ShowData().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = RemoveTags(richTextBox1.Text);
            button2.Enabled = true;          
        }
        
        #region methods for remove tags with DLL
        private static Regex _removeRepeatedWhitespaceRegex = new Regex(@"(\s|\n|\r){2,}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        public static string ExtractViewableTextCleaned(HtmlNode node)
        {
            string textWithLotsOfWhiteSpaces = ExtractViewableText(node);
            return _removeRepeatedWhitespaceRegex.Replace(textWithLotsOfWhiteSpaces, " ");
        }

        public static string ExtractViewableText(HtmlNode node)
        {
            StringBuilder sb = new StringBuilder();
            ExtractViewableTextHelper(sb, node);
            return sb.ToString();
        }
        private static void ExtractViewableTextHelper(StringBuilder sb, HtmlNode node)
        {
            if (node.Name != "script" && node.Name != "style")
            {
                if (node.NodeType == HtmlNodeType.Text)
                {
                    AppendNodeText(sb, node);
                }

                foreach (HtmlNode child in node.ChildNodes)
                {
                    ExtractViewableTextHelper(sb, child);
                }
            }
        }

        private static void AppendNodeText(StringBuilder sb, HtmlNode node)
        {
            string text = ((HtmlTextNode)node).Text;
            if (string.IsNullOrWhiteSpace(text) == false)
            {
                sb.Append(text);

                // If the last char isn't a white-space, add a white space
                // otherwise words will be added ontop of each other when they're only separated by
                // tags
                if (text.EndsWith("\t") || text.EndsWith("\n") || text.EndsWith(" ") || text.EndsWith("\r"))
                {
                    // We're good!
                }
                else
                {
                    sb.Append(" ");
                }
            }
        }
        #endregion

        public string RemoveTags(string Corpus)
        {
            string patBody = @"(((<body)|(<BODY))[^\+]*)>[^§]*((</body>)|(</BODY>))";

            string text = Corpus;
            //-------------------- step 1 --------------------------
            // Instantiate the regular expression object.
            Regex r = new Regex(patBody);

            // Match the regular expression pattern against a textstring.
            Match m = r.Match(text);
            string text2 = m.ToString();//m.Groups.SyncRoot.ToString();

            //string patRemoveBody = @"(<body)[^\+]*>|(<BODY)[^\+]*>|(</body>)|(</BODY>)";
            string PatRemoveBody = @"(<body|<BODY)[^\+]*?>|(</body>|</BODY>)";
            string text3 = Regex.Replace(text2, PatRemoveBody, " ");
            //-----remove script tag-----          
            string removeScrip = @"<(title|TITLE|script|SCRIPT|style|STYLE|iframe|IFRAME|option|OPTION|H[1-9]|h[1-9]|!--|li)[^♀]*?>[^δ]*?</(title|TITLE||--!|li|script|SCRIPT|style|STYLE|option|iframe|IFRAME|OPTION|H[1-9]|h[1-9])>";
            text3 = Regex.Replace(text3, removeScrip, " ");
            
            string patOtherTage = @"<[^>]+>";
            text3 = Regex.Replace(text3, patOtherTage, " ");
            text3 = Regex.Replace(text3, @"(\s{3,}|(&quot;)|(AddToAny)|(&nbsp;)|(&lt)|(&gt)|(_)|(&gt;))", " ");
            text3 = Regex.Replace(text3, @"(\n|‎|•|●|‏|‌)", " ");
            return text3;           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.Load(new MemoryStream(File.ReadAllBytes(openFileDialog1.FileName)), UnicodeEncoding.UTF8);
            richTextBox2.Text = ExtractViewableTextCleaned(document.DocumentNode).Replace("&gt;", "").Replace("&nbsp;", "").Replace("&quot;", "");
            button2.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new AutoChange().Show();
            
        }           
    }
}