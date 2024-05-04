using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GetStoredProceduresAppVisual.Classes;
internal class Colorizer
{
    public static void Execute(RichTextBox ProcedureTextBox)
    {
        // getting keywords/functions
        string keywords = @"\b(CREATE|PROCEDURE|BEGIN|END|TRY|CATCH|WHERE|AND|SELECT|FROM|WHEN|ELSE|CASE|SET|INSERT|INTO|VALUES|IF|)\b";
        MatchCollection keywordMatches = Regex.Matches(ProcedureTextBox.Text, keywords);
        string types = @"\b(Console)\b";
        MatchCollection typeMatches = Regex.Matches(ProcedureTextBox.Text, types);
        string comments = @"(\/\/.+?$|\/\*.+?\*\/)";
        MatchCollection commentMatches = Regex.Matches(ProcedureTextBox.Text, comments, RegexOptions.Multiline);
        string strings = "\".+?\"";
        MatchCollection stringMatches = Regex.Matches(ProcedureTextBox.Text, strings);
        int originalIndex = ProcedureTextBox.SelectionStart;
        int originalLength = ProcedureTextBox.SelectionLength;
        Color originalColor = Color.Black;
        ProcedureTextBox.SelectionStart = 0;
        ProcedureTextBox.SelectionLength = ProcedureTextBox.Text.Length;
        ProcedureTextBox.SelectionColor = originalColor;

        foreach (Match m in keywordMatches)
        {
            ProcedureTextBox.SelectionStart = m.Index;
            ProcedureTextBox.SelectionLength = m.Length;
            ProcedureTextBox.SelectionColor = Color.Blue;
        }

        foreach (Match m in typeMatches)
        {
            ProcedureTextBox.SelectionStart = m.Index;
            ProcedureTextBox.SelectionLength = m.Length;
            ProcedureTextBox.SelectionColor = Color.DarkCyan;
        }

        foreach (Match m in commentMatches)
        {
            ProcedureTextBox.SelectionStart = m.Index;
            ProcedureTextBox.SelectionLength = m.Length;
            ProcedureTextBox.SelectionColor = Color.Green;
        }

        foreach (Match m in stringMatches)
        {
            ProcedureTextBox.SelectionStart = m.Index;
            ProcedureTextBox.SelectionLength = m.Length;
            ProcedureTextBox.SelectionColor = Color.Brown;
        }

        // restoring the original colors, for further writing
        ProcedureTextBox.SelectionStart = originalIndex;
        ProcedureTextBox.SelectionLength = originalLength;
        ProcedureTextBox.SelectionColor = originalColor;

        // giving back the focus
        ProcedureTextBox.Focus();
    }
}
