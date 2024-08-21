using NewAppStore.Views.AppsPages;

namespace NewAppStore.Views;

public sealed partial class MainPage : Page
{

    private readonly List<string> Apps = new List<string>()
    {
        "UltraTextEdit UWP Plus",
        "UTE UWP+",
        "UltraTextEdit UWP+",
        "Ivirius Text Editor",
        "Ivirius Text Editor Free",
        "IVR Text Editor Free",
        "IVR Text Editor",
        "IVR Free",
        "Certificate Installer for MSIX",
        "Certificate Installer",
        "Cert Installer"
    };

    public MainPage()
    {
        this.InitializeComponent();
        appTitleBar.Window = App.CurrentWindow;
        App.Current.JsonNavigationViewService.Initialize(NavView, NavFrame);
        App.Current.JsonNavigationViewService.ConfigJson("Assets/NavViewMenu/AppData.json");
    }

    private void appTitleBar_BackButtonClick(object sender, RoutedEventArgs e)
    {
        if (NavFrame.CanGoBack)
        {
            NavFrame.GoBack();
        }
    }

    private void appTitleBar_PaneButtonClick(object sender, RoutedEventArgs e)
    {
        NavView.IsPaneOpen = !NavView.IsPaneOpen;
    }

    private void NavFrame_Navigated(object sender, NavigationEventArgs e)
    {
        appTitleBar.IsBackButtonVisible = NavFrame.CanGoBack;
    }

    private void ThemeButton_Click(object sender, RoutedEventArgs e)
    {
        var element = App.CurrentWindow.Content as FrameworkElement;

        if (element.ActualTheme == ElementTheme.Light)
        {
            element.RequestedTheme = ElementTheme.Dark;
        }
        else if (element.ActualTheme == ElementTheme.Dark)
        {
            element.RequestedTheme = ElementTheme.Light;
        }
    }

    private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        // Since selecting an item will also change the text,
        // only listen to changes caused by user entering text.
        if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
        {
            var suitableItems = new List<string>();
            var splitText = sender.Text.ToLower().Split(" ");
            foreach (var app in Apps)
            {
                var found = splitText.All((key) =>
                {
                    return app.ToLower().Contains(key);
                });
                if (found)
                {
                    suitableItems.Add(app);
                }
            }
            if (suitableItems.Count == 0)
            {
                suitableItems.Add("No results found");
            }
            sender.ItemsSource = suitableItems;
        }
    }

    private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {
        var chosen = args.SelectedItem as string;
        if (chosen != null)
        {
            if (chosen == "Ivirius Text Editor" | chosen == "IVR Free" | chosen == "Ivirius Text Editor Free")
            {
                NavFrame.Navigate(typeof(IVRFree));
            }
            else if (chosen == "Certificate Installer for MSIX")
            {
                NavFrame.Navigate(typeof(CertificateInstallerForMSIX));
            }
            else if (chosen == "MyApp")
            {
            }
            else if (chosen == "UltraTextEdit UWP Plus")
            {
                NavFrame.Navigate(typeof(UTEUWPPlus));
            }
        }
    }

}
