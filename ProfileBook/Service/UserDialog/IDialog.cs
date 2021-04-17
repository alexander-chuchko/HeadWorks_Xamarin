using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Service.UserDialog
{
    public interface IDialog
    {
        bool Result { get; set; }
        void CallDialogue();
        void CallActionSheet();

    }
}
