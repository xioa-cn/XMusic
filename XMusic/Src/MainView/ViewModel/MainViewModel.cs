using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows;
using XMusic.Src.MainView.Model;

namespace XMusic.Src.MainView.ViewModel;

public partial class MainViewModel : ObservableObject
{
    #region ContentVisibility

    [ObservableProperty] private Visibility _contentVisibility = Visibility.Hidden;
    [RelayCommand]
    private void ShowContent()
    {
        ContentVisibility = Visibility.Visible;
    }
    [RelayCommand]
    private void NoShowContent()
    {
        ContentVisibility = Visibility.Hidden;
        MainCMenuEnum.Normal.Run();
    }

    #endregion


    #region ContentMenu

    [RelayCommand]
    private void WindowMethod(string mType)
    {
        var value = Enum.Parse<MainCMenuEnum>(mType);
        value.Run();
    }

    #endregion
}