using StoreLab.ApplicationCore.Interfaces;
using StoreLab.ApplicationCore.Models;
using StoreLab.RetroStore.Enums;
using StoreLab.RetroStore.StateHandlers;
using StoreLab.RetroStore.Utils;

namespace StoreLab.RetroStore;

public class ConsoleApp
{
    internal ConsoleStateResult<object?>? LastStateResult;
    internal ConsoleState CurrentState = ConsoleState.Help;

    // Current basket
    public Basket? Basket = null;

    public ICatalogService CatalogService { get; }
    public IBasketService BasketService { get; }

    private readonly Dictionary<ConsoleState, IConsoleStateHandler> _stateHandlers;

    public ConsoleApp(ICatalogService catalogService, IBasketService basketService)
    {
        CatalogService = catalogService;
        BasketService = basketService;
        _stateHandlers = new Dictionary<ConsoleState, IConsoleStateHandler>
        {
            { ConsoleState.Help, new HelpStateHandler() },
            { ConsoleState.Pay, new PayStateHandler() },
            { ConsoleState.Sell, new SellStateHandler() },
            { ConsoleState.CatalogSearch, new CatalogSearchStateHandler() },
        };
    }

    public async Task Run()
    {
        while (true)
        {
            if (CurrentState == ConsoleState.Quit)
            {
                await QuitApplication();
                return;
            }

            if (_stateHandlers.TryGetValue(CurrentState, out var handler))
            {
                var result = await handler.HandleAsync(this);
                LastStateResult = result;
                CurrentState = result.State;
            }
        }
    }

    public async Task<Basket> AddCatalogItemToBasket(CatalogItem catalogItem)
    {
        // If the basket is null, create a new basket
        if (Basket == null || Basket.State == BasketState.Paid)
        {
            Basket = await BasketService.CreateBasket();
        }
        // Call BasketService to add the item to the basket by catalogId
        return await BasketService.AddItemToBasket(Basket.Id, catalogItem.Id);
    }

    private async Task QuitApplication()
    {
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        // Show ascii art from PrintHelper global const
        PrintHelper.AsciiArtPrinter(Constants.AsciiArt);
        // Animate countdown at current cursor position
        int countdownSeconds = 3;
        string dots = new string('.', 15); // 15 dots for visual effect
        int left = Console.CursorLeft;
        int top = Console.CursorTop;
        Console.WriteLine(dots); // Print the dotted line
        for (int i = countdownSeconds; i > 0; i--)
        {
            int dotsToShow = (int)Math.Ceiling((double)dots.Length * i / countdownSeconds);
            Console.SetCursorPosition(left, top);
            Console.Write($"Shutting down");
            Console.SetCursorPosition(left, top + 1);
            Console.Write(new string('.', dotsToShow).PadRight(dots.Length, ' '));
            Console.SetCursorPosition(0, 0);
            await Task.Delay(1000);
        }
        Console.Clear(); // Clear the screen on exit
        return;
    }
}