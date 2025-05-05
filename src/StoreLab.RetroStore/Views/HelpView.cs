using StoreLab.RetroStore.Enums;
using StoreLab.RetroStore.Utils;
using StoreLab.RetroStore.ViewModels;

namespace StoreLab.RetroStore.Views;

public class HelpView : ViewBase<HelpViewModel, HelpViewModelActions>
{
    public override string GetPromptText(HelpViewModel viewModel)
    {
        return "(Help) ";
    }

    public override string GetHeaderText(HelpViewModel viewModel)
    {
        return "Help";
    }

    public override void RenderInternal(HelpViewModel viewModel)
    {
        var optionList = InputOptionRows(viewModel);

        // Use TableColumn and PrintTable for output
        var columns = new List<TableColumn<InputOptionRow>>
        {
            new TableColumn<InputOptionRow>("", 32, (row, idx) => ""),
            new TableColumn<InputOptionRow>("Key", 8, (row, idx) => row.Key),
            new TableColumn<InputOptionRow>("Description", 0, (row, idx) => row.Description)
        };
        TableColumnHelper.PrintTable(optionList, columns);

        PrintHelper.AsciiArtPrinter(Constants.AsciiArt);

    }

    private static List<InputOptionRow> InputOptionRows(HelpViewModel viewModel)
    {
        var options = viewModel.Actions;
        // Prepare options as key-description pairs
        var optionList = new List<InputOptionRow>();
        if (options.HasFlag(CommonActions.Help))
            optionList.Add(new InputOptionRow { Key = "?", Description = "Show this help" });
        if (options.HasFlag(CommonActions.SearchCatalog))
            optionList.Add(new InputOptionRow { Key = "s", Description = "New search" });
        if (options.HasFlag(CommonActions.ToggleSellOrPay))
            optionList.Add(new InputOptionRow { Key = "Tab", Description = "Switch mode (sell/buy)" });
        if (options.HasFlag(CommonActions.Quit))
            optionList.Add(new InputOptionRow { Key = "q", Description = "Quit" });
        if (options.HasFlag(CommonActions.Select))
            optionList.Add(new InputOptionRow { Key = "#", Description = "Select a list item" });
        return optionList;
    }
}
