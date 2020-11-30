using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace TextEditor
{
    static public class Class1
    {
        public static TextPointer GetTextPointerAtOffset(this RichTextBox richTextBox, int offset)
        {
            TextPointer navigator = richTextBox.Document.ContentStart;
            int count = 0;

            while (navigator.CompareTo(richTextBox.Document.ContentEnd) < 0)
            {
                switch (navigator.GetPointerContext(LogicalDirection.Forward))
                {
                    case TextPointerContext.ElementStart:
                        break;
                    case TextPointerContext.ElementEnd:
                        if (navigator.GetAdjacentElement(LogicalDirection.Forward) is Paragraph)
                            count += 2;
                        break;
                    case TextPointerContext.EmbeddedElement:
                        count++;
                        break;
                    case TextPointerContext.Text:
                        int runLength = navigator.GetTextRunLength(LogicalDirection.Forward);

                        if (runLength > 0 && runLength + count < offset)
                        {
                            count += runLength;
                            navigator = navigator.GetPositionAtOffset(runLength);
                            if (count > offset)
                                break;
                            continue;
                        }
                        count++;
                        break;
                }

                if (count > offset)
                    break;

                navigator = navigator.GetPositionAtOffset(1, LogicalDirection.Forward);

            } 

            return navigator;
        }
    }
}
