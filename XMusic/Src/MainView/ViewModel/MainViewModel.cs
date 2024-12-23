using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows;
using XMusic.Src.MainView.Model;

namespace XMusic.Src.MainView.ViewModel;

public partial class MainViewModel : ObservableObject
{
    #region ContentVisibility

    [ObservableProperty] private Visibility _contentVisibility = Visibility.Collapsed;
    [RelayCommand]
    private void ShowContent()
    {
        ContentVisibility = Visibility.Visible;
    }
    [RelayCommand]
    private void NoShowContent()
    {
        ContentVisibility = Visibility.Collapsed;
        MainCMenuEnum.Normal.Run();
    }

    #endregion


    #region ContentMenu

    private bool currentStatusIsMax = false;
    [RelayCommand]
    private void WindowMethod(string mType)
    {
        var value = Enum.Parse<MainCMenuEnum>(mType);
        if (value == MainCMenuEnum.Max && currentStatusIsMax)
        {
            MainCMenuEnum.Normal.Run();
            currentStatusIsMax = false;
            return;
        }
        else
        {
            currentStatusIsMax = true;
        }
        value.Run();
    }

    #endregion
}