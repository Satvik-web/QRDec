using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PigeonDecoder
{
    public partial class LanguagePage : ContentPage
    {
        public LanguagePage()
        {
            InitializeComponent();

            langlabel.Text = Util.Language.Contains("T") ? "Click on the above button..." : "மேலே உள்ள பொத்தானை அழுத்தவும்...";

            tamillabel.IsVisible = adlabel.IsVisible = adlabel.IsEnabled = Util.Language.Contains("T");

            LangImage.Source = ImageSource.FromFile(Util.Language.Contains("T") ? "tamil.png" : "english.png");
        }

        async void ChangeLang_Tapped(System.Object sender, System.EventArgs e)
        {
            if (Util.Language.Contains("E") || Util.Language.Contains("e"))
            {
                Util.Language = "T";
                Util.ChangeLanguage("T");
                adlabel.IsEnabled = false;
                adlabel.IsVisible = false;
                tamillabel.IsVisible = true;
                LangImage.Source = ImageSource.FromFile("tamil.png");
                langlabel.Text = "Click on the above button...";
                await DisplayAlert("மொழி மாற்றப்பட்டுள்ளது", "மொழி தமிழாக மாறிவிட்டது", "சரி");
                await DisplayAlert("தமிழ் தாய்யைக் காபாற்றவும்!!!", "நம் தாய்மொழி ஆகிய தமிழ்மொழியை, நாம் அனைவரும் போற்றி புகழ்ந்து காப்பாற்றவேண்டும்!", "தமிழ் மொழி வாழ்க! தமிழ் மொழி வாழ்க! என்றேன்றும் தமிழ் மொழி வாழ்க!");
            }
            else
            {
                Util.Language = "E";
                Util.ChangeLanguage("E");
                adlabel.IsEnabled = true;
                adlabel.IsVisible = true;
                tamillabel.IsVisible = false;
                langlabel.Text = "மேலே உள்ள பொத்தானை அழுத்தவும்...";
                LangImage.Source = ImageSource.FromFile("english.png");
                await DisplayAlert("The Language has been changed", "The Language has been changed to English", "Ok");
            }
            MainPage.LangCommand.Execute(null);
            Create.LangCommand.Execute(null);
            AdsPage.LangCommand.Execute(null);
        }
    }
}
