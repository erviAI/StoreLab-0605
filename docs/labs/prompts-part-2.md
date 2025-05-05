## Extend current Application with new functionality

[Back to outline](../outline.md)

Goal:   
- Add new features or business logic to the application, such as new services, models, or user interactions.


### Ex 1: Extend Catalog Model
* `Extent the CatalogItem with color and size.`
  ```
  Extent the CatalogItem with color and size. The color should be a mix of 6 different colors. The size should be in t-shirt sizes
  ```

  - src/StoreLab.ApplicationCore/Models/CatalogItem.cs

* `Enusre code is updated #codebase`
  ```
  Ensure the ALL part of the code is updated using #codebase
  ```

* `Show in search result - `
  ```
  Can you update the CatalogSearch to with the new properties
  ```
  - src/StoreLab.RetroStore/Views/CatalogSearchView.cs

### Ex 1.1: Extend Catalog Model - I would prompt

* `Alternative`
  ```
  I would like to extend the  CatalogItem with color and size. The color should be a mix of 6 different colors. The size should be in t-shirt sizes. Please update where need #codebase
  ```

### Ex 2: Extend Basket Model - Edit/Agent

* `Update the BasketItem to include color and size similar to CatalogItem`
  ```
  Update the BasketItem to include color and size similar to CatalogItem
  ```

  - src/StoreLab.ApplicationCore/Models/BasketItem.cs


### Ex 3: More payment types

* `Add Vipps/Swish`
  ```
  I would like to add a new payment type called Vipps. To use this type, the user should type vipps:<amount> as input
  ```
  - src/StoreLab.ApplicationCore/Models/PaymentType.cs

* `Add CreditCard`
  ```
  I would like to add a new payment type called Vipps. To use this type, the user should type card:<amount> as input
  ```

  - src/StoreLab.ApplicationCore/Models/PaymentType.cs

* `Business rule`
  ```
  When user pays for more the money/amount than what's remaining the customer should have the remaing payment back in Cash. To indicate the payback, introduce a Direction enum with [In, Out] to differentiate. Please also update the payment flow and current calculation to reflect the change #codebase
  ```
  - src/StoreLab.ApplicationCore/Models/Basket.cs

* `Business rule` - Optional
  ```
  When user pays with 'Vipps/Swish' only the exact amount is permitted
  ```
  - src/StoreLab.ApplicationCore/Models/Basket.cs

* `Add error handling and show to the user`
  ```
  Can you please handle any exception in case of payment failure gracefully and notify user on the error
  ```
  - src/StoreLab.RetroStore/StateHandlers/PayStateHandler.cs

### Ex 4: Search
* `Extend with smarter search n:<name>, d:<description>, p:<price> +/-`
  ```
  I would like to extend the search in the following pattern in "<column>:<searchString>" where column represents the first char of the property to search. If no column is provided, search across name and description
  ```
  - src\StoreLab.ApplicationCore\Infrastructure\InMemoryCatalogRepository.cs

* `Fuzzy search`
  Smart search for price +/- range
  ```
  Awsome can you extend the price search 'p' with a range search +/- lets says 10
  ```
  - src\StoreLab.ApplicationCore\Infrastructure\InMemoryCatalogRepository.cs

* `Update the GUI with info on how to perform smart search`
  ```
  When in search mode can you update option info on the ui or console app 
  ```

  - src\StoreLab.RetroStore\Utils\PrintHelper.cs

### Ex 5: Seed / Startup
* `Extend how to seed data`
  ```
  I would to seed the initial data fed into the InMemoryCatalogRepository based on a csv-file. It should be possible to pass a csv-file as argument when the program is started

  ```

* `Generate a csv file - extent current chat`
  ```
  Can you generate a csv file for me. With 10 items
  ```

---

## More prompts

- Add a new feature: implement discount codes for baskets.
- How would you add support for multiple payment types?
- Add inventory tracking to the catalog.
- Implement a feature to print receipts.
- Suggest a new feature and describe how you would implement it.
- How would you ensure new features are backward compatible?
- What changes are needed in the data model for new features?

## Summary
