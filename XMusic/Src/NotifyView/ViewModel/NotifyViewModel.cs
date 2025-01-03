using System;
using System.Diagnostics;
using System.Windows.Documents;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using XMusic.Src.MainView.Model;
using XMusic.Src.NotifyView.Model;
using XMusic.Src.Utils;

namespace XMusic.Src.NotifyView.ViewModel;

/// <summary>
/// @author Xioa
/// @date  2024年12月23日
/// </summary>
public partial class NotifyViewModel : ObservableObject
{

    public NotifyViewModel()
    {
        WeakReferenceMessenger.Default.Register<Messenger<ChangeTitle>>(this, ChangeTitleMethod);
    }

    private void ChangeTitleMethod(object recipient, Messenger<ChangeTitle> message)
    {
        this.Title = message.data.Title;
    }

    #region Title

    [ObservableProperty] private string _title = "NULL";
    [ObservableProperty] private bool _isCheck = true;
    #endregion

    #region

    [RelayCommand]
    private void Next(string nextType)
    {
        if (nextType == "After")
            WeakReferenceMessenger.Default.Send(new Messenger<String> { data = "NEXT" });
        else if (nextType == "Before")
            WeakReferenceMessenger.Default.Send(new Messenger<String> { data = "BEFORE" });
    }

    [RelayCommand]
    public void PlayStatus()
    {
        if (!_isCheck)
            WeakReferenceMessenger.Default.Send(new Messenger<String> { data = "STOP" });
        else
            WeakReferenceMessenger.Default.Send(new Messenger<String> { data = "START" });
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

    #region Close

    [RelayCommand]
    private void CloseApplication()
    {
        GlobalHelper.Close();
    }

    #endregion

    #region Open

    [RelayCommand]
    private void OpenApplication()
    {
        GlobalHelper.Open();
    }

    #endregion


}