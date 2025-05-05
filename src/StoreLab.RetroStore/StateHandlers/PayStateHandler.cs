using StoreLab.ApplicationCore.Models;
using StoreLab.RetroStore.Enums;
using StoreLab.RetroStore.Utils;
using StoreLab.RetroStore.Views;

namespace StoreLab.RetroStore.StateHandlers;

public class PayStateHandler : IConsoleStateHandler
{
    private readonly PayView PayView = new PayView();

    public async Task<ConsoleStateResult<object?>> HandleAsync(ConsoleApp context)
    {
        CommonActions options = CommonActions.Help | CommonActions.ToggleSellOrPay;
        
        var viewModel = new ViewModels.PayViewModel(context.Basket, options);

        PayView.Render(viewModel);
        
        string? searchInput = null;
        while (string.IsNullOrWhiteSpace(searchInput))
        {
            var inputResult = Utils.UserInputHelper.ReadUserInputAction(options);
            if (inputResult.EscPressed)
            {
                return new ConsoleStateResult<object?>(ConsoleState.Sell);
            }
            string? userInput = inputResult.TypedInput;
            if (int.TryParse(userInput, out int amount))
            {
                if (context.Basket != null)
                {
                    context.Basket = await context.BasketService.AddPaymentToBasket(context.Basket.Id, amount, PaymentType.Cash);
                    PayView.ShowPaymentDone(viewModel, PaymentType.Cash, amount);
                    return new ConsoleStateResult<object?>(ConsoleState.Pay, amount);
                }
                else
                {
                    viewModel.SetViewData(ViewModels.PayViewModelActions.ShowNoBasket, ViewModels.ViewDataMode.Warning);
                }
            }
            else if (inputResult.TabPressed || string.IsNullOrWhiteSpace(userInput))
            {
                return new ConsoleStateResult<object?>(ConsoleState.Sell);
            }
            else
            {
                viewModel.SetViewData(ViewModels.PayViewModelActions.ShowInvalidInput, ViewModels.ViewDataMode.Warning);
                
            }
            PayView.Render(viewModel);
        }
        
        return new ConsoleStateResult<object?>(ConsoleState.Pay);
    }
}
