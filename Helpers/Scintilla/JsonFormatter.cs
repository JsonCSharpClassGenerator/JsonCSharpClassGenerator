using System.Drawing;
using EasyScintilla.Stylers;
using ScintillaNET;

namespace Xamasoft.JsonClassGenerator.UI.Helpers
{
    internal class JsonStyler : ScintillaStyler
    {
        public JsonStyler() : base(Lexer.Json) { }

        public override void ApplyStyle(Scintilla scintilla)
        {
            scintilla.Styles[Style.Json.Default].ForeColor = Color.Black;
            scintilla.Styles[Style.Json.Default].BackColor = Color.White;

            scintilla.Styles[Style.Json.Number].ForeColor = Color.Blue;
            scintilla.Styles[Style.Json.Number].BackColor = Color.White;

            scintilla.Styles[Style.Json.BlockComment].ForeColor = Color.DarkGreen;
            scintilla.Styles[Style.Json.BlockComment].BackColor = Color.White;

            scintilla.Styles[Style.Json.EscapeSequence].ForeColor = Color.Green;
            scintilla.Styles[Style.Json.EscapeSequence].BackColor = Color.White;

            scintilla.Styles[Style.Json.Error].ForeColor = Color.OrangeRed;
            scintilla.Styles[Style.Json.Error].BackColor = Color.Beige;

            scintilla.Styles[Style.Json.Operator].ForeColor = Color.Red;
            scintilla.Styles[Style.Json.Operator].BackColor = Color.White;

            scintilla.Styles[Style.Json.String].ForeColor = Color.Green;
            scintilla.Styles[Style.Json.String].BackColor = Color.White;

            scintilla.Styles[Style.Json.PropertyName].ForeColor = Color.Blue;
            scintilla.Styles[Style.Json.PropertyName].BackColor = Color.White;

            scintilla.Styles[Style.Json.Keyword].ForeColor = Color.Chocolate;
            scintilla.Styles[Style.Json.Keyword].BackColor = Color.White;

            scintilla.Styles[21].ForeColor = Color.Black;
            scintilla.Styles[21].BackColor = ColorTranslator.FromHtml("#A6CAF0");
        }

        public override void RemoveStyle(Scintilla scintilla) { }

        public override void SetKeywords(Scintilla scintilla) { }

    }
}
