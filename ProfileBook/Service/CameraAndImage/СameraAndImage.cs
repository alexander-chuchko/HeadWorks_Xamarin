using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProfileBook.Service.CameraAndImage
{
    public class СameraAndImage: ICameraAndImage
    {
        public Action OpenGallery { get; set; }
        public Action TakePhoto { get; set; }
        public string PathPicture { get; set; }
        public ImageSource _pictureSource;
        public СameraAndImage()
        {
            OpenGallery = SelectImage;
            TakePhoto = TakingPictures;
        }

        public void UserDialogsL()
        {
            IUserDialogs d = UserDialogs.Instance;
            ActionSheetConfig config = new ActionSheetConfig();
            List<ActionSheetOption> Options = new List<ActionSheetOption>();
            Options.Add(new ActionSheetOption("Pick at Gallery", OpenGallery, "folder_image.png"));
            Options.Add(new ActionSheetOption("Take photo with camera", TakePhoto, "camera.png"));
            ActionSheetOption cancel = new ActionSheetOption("Cancel", null, null);
            config.Options = Options;
            config.Cancel = cancel;
            var select = d.ActionSheet(config);
        }
        public ImageSource CallImageAndCamera()
        {
            UserDialogsL();
            return PictureSource;
        }

        public ImageSource PictureSource
        {
            get { return _pictureSource; }
            set { value = _pictureSource; } 
        }
        //Method for selecting photo
        async public void SelectImage()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });
            PathPicture = result.FullPath;
            var stream = await result.OpenReadAsync();
            PictureSource = ImageSource.FromStream(() => stream);
        }
        //Method for taking pictures
        async public void TakingPictures()
        {
            var result = await MediaPicker.CapturePhotoAsync();
            var stream = await result.OpenReadAsync();
            PictureSource = ImageSource.FromStream(() => stream);
            ////string path = ((BitmapImage)image.Source).UriSource.AbsolutePath;
        }
    }
}
