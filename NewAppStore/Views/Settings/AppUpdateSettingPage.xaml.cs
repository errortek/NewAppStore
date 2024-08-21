﻿using Windows.System;

namespace NewAppStore.Views;
public sealed partial class AppUpdateSettingPage : Page
{
    public string CurrentVersion { get; set; }
    public string ChangeLog { get; set; }
    public string BreadCrumbBarItemText { get; set; }

    public AppUpdateSettingPage()
    {
        this.InitializeComponent();
        CurrentVersion = $"Current version: v{App.Current.AppVersion}";

        TxtLastUpdateCheck.Text = Settings.LastUpdateCheck;

        BtnReleaseNote.Visibility = Visibility.Collapsed;
        BtnDownloadUpdate.Visibility = Visibility.Collapsed;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        BreadCrumbBarItemText = e.Parameter as string;
    }

    private async void CheckForUpdateAsync()
    {
        PrgLoading.IsActive = true;
        BtnCheckUpdate.IsEnabled = false;
        BtnReleaseNote.Visibility = Visibility.Collapsed;
        BtnDownloadUpdate.Visibility = Visibility.Collapsed;
        StatusCard.Header = "Checking for new version";
        if (NetworkHelper.IsNetworkAvailable())
        {
            try
            {
                //Todo: Fix UserName and Repo
                string username = "";
                string repo = "";
                TxtLastUpdateCheck.Text = DateTime.Now.ToShortDateString();
                Settings.LastUpdateCheck = DateTime.Now.ToShortDateString();
                var update = await UpdateHelper.CheckUpdateAsync(username, repo, new Version(App.Current.AppVersion));
                if (update.IsExistNewVersion)
                {
                    BtnReleaseNote.Visibility = Visibility.Visible;
                    BtnDownloadUpdate.Visibility = Visibility.Visible;
                    ChangeLog = update.Changelog;
                    StatusCard.Header = $"A new update of NewAppStore has been found! v{update.TagName}, created at {update.CreatedAt} and published at {update.PublishedAt}";
                }
                else
                {
                    StatusCard.Header = "You are using the latest version";
                }
            }
            catch (Exception ex)
            {
                StatusCard.Header = ex.Message;
                PrgLoading.IsActive = false;
                BtnCheckUpdate.IsEnabled = true;
            }
        }
        else
        {
            StatusCard.Header = "Could not check for updates. Please make sure you have a stable Internet connection, then try again.";
        }
        PrgLoading.IsActive = false;
        BtnCheckUpdate.IsEnabled = true;
    }

    private async void GoToUpdateAsync()
    {
        //Todo: Change Uri
        await Launcher.LaunchUriAsync(new Uri("https://github.com/WinUICommunity/WinUICommunity/releases"));
    }

    private async void GetReleaseNotesAsync()
    {
        ContentDialog dialog = new ContentDialog()
        {
            Title = "Release notes",
            CloseButtonText = "Close",
            Content = new ScrollViewer
            {
                Content = new TextBlock
                {
                    Text = ChangeLog,
                    Margin = new Thickness(10)
                },
                Margin = new Thickness(10)
            },
            Margin = new Thickness(10),
            DefaultButton = ContentDialogButton.Close,
            XamlRoot = App.CurrentWindow.Content.XamlRoot
        };

        await dialog.ShowAsyncQueue();
    }
}


