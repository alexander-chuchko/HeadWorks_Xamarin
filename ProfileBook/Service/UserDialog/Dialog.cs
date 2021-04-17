using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Service.UserDialog
{
    public class Dialog: IDialog
    {
        private bool result;

        public bool Result 
        {
            set { result = value; }
            get { return result; }
        }

        public async void CallDialogue()
        {
            var confirmConfig = new ConfirmConfig()
            {
                Message = "Invalid login or password!",
                OkText = "OK"
            };
            Result = await UserDialogs.Instance.ConfirmAsync(confirmConfig);
        }
        public async void ReportAboutCharacters()
        {
            var confirmConfig = new ConfirmConfig()
            {
                Message = "The login must have at least 4 and no more than 16 characters!",
                OkText = "OK"
            };
            Result = await UserDialogs.Instance.ConfirmAsync(confirmConfig);
        }
        public async void ReportAboutCharacters1()
        {
            var confirmConfig = new ConfirmConfig()
            {
                Message = "The login must have at least 4 and no more than 16 characters!",
                OkText = "OK"
            };
            Result = await UserDialogs.Instance.ConfirmAsync(confirmConfig);
        }
        public void CallActionSheet()
        {

        }
    }
}
