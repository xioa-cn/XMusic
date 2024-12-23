using System;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using XMusic.Src.NotifyView.Model;

namespace XMusic.Src.NotifyView.ViewModel;

/// <summary>
/// @author Xioa
/// @date  2024年12月23日
/// </summary>
public partial class NotifyViewModel : ObservableObject
{
    #region Title

    [ObservableProperty] private string _title = "Over My Head - Echosmith";

    #endregion

    #region

    [RelayCommand]
    private void Next(string nextType)
    {
        NextType type = Enum.Parse<NextType>(nextType);
        this.Title = nextType;
    }

    #endregion

    #region IsLike

    private bool _isLike = false;

    public bool IsLike
    {
        get => _isLike;
        set
        {
            OnIsLikeChanged(value: value);
            SetProperty(ref _isLike, value);
        }
    }

    private void OnIsLikeChanged(bool value)
    {
        Debug.WriteLine(value);
    }


    [RelayCommand]
    private void LikeStatus()
    {
        IsLike = !IsLike;
    }

    #endregion
}