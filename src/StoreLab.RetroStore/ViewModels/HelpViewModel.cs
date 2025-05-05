using StoreLab.RetroStore.Enums;

namespace StoreLab.RetroStore.ViewModels;

public class HelpViewModel : BaseViewModel<HelpViewModelActions>
{
    public string? HelpText { get; set; }

    public HelpViewModel() { }

    public HelpViewModel(CommonActions actions, string? helpText = null)
    {
        Actions = actions;
        HelpText = helpText;
    }
}

public enum HelpViewModelActions
{
    NaN
}