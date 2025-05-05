using StoreLab.ApplicationCore.Models;

namespace StoreLab.RetroStore.Data;

public class DataHelper
{
    // Returns a list of catalog items
    public static List<CatalogItem> GetCatalogItems()
    {
        return _catalogItems;
    }

    // / In-memory storage for catalog items as a list
    private static List<CatalogItem> _catalogItems = new List<CatalogItem>
    {
        new CatalogItem { Id = 1, Name = "Wireless Keyboard", Description = "Compact wireless keyboard with long battery life.", AvailableStock = 5, Price = 29 },
        new CatalogItem { Id = 2, Name = "USB Flash Drive", Description = "64GB USB 3.0 flash drive for fast data transfer.", AvailableStock = 25, Price = 14 },
        new CatalogItem { Id = 3, Name = "Laptop Backpack", Description = "Water-resistant laptop backpack with multiple compartments.", AvailableStock = 10, Price = 49 },
        new CatalogItem { Id = 4, Name = "Wireless Mouse", Description = "Ergonomic wireless mouse with adjustable DPI.", AvailableStock = 15, Price = 25 },
        new CatalogItem { Id = 5, Name = "Mechanical Keyboard", Description = "RGB backlit mechanical keyboard with blue switches.", AvailableStock = 8, Price = 79 },
        new CatalogItem { Id = 6, Name = "Gaming Headset", Description = "Surround sound gaming headset with noise-canceling mic.", AvailableStock = 12, Price = 49 },
        new CatalogItem { Id = 7, Name = "USB-C Hub", Description = "7-in-1 USB-C hub with HDMI, USB 3.0, and SD card reader.", AvailableStock = 25, Price = 34 },
        new CatalogItem { Id = 8, Name = "Portable SSD", Description = "1TB portable SSD with high-speed data transfer.", AvailableStock = 10, Price = 99 },
        new CatalogItem { Id = 9, Name = "Smartphone Stand", Description = "Adjustable aluminum stand for smartphones and tablets.", AvailableStock = 30, Price = 14 },
        new CatalogItem { Id = 10, Name = "Bluetooth Speaker", Description = "Compact Bluetooth speaker with deep bass and long battery life.", AvailableStock = 20, Price = 39 },
        new CatalogItem { Id = 11, Name = "Fitness Tracker", Description = "Waterproof fitness tracker with heart rate monitor.", AvailableStock = 18, Price = 59 },
        new CatalogItem { Id = 12, Name = "Smartwatch", Description = "Smartwatch with GPS and health tracking features.", AvailableStock = 10, Price = 199 },
        new CatalogItem { Id = 13, Name = "Laptop Stand", Description = "Adjustable laptop stand with cooling pad.", AvailableStock = 22, Price = 29 },
        new CatalogItem { Id = 14, Name = "Wireless Charger", Description = "Fast wireless charger compatible with most smartphones.", AvailableStock = 35, Price = 19 },
        new CatalogItem { Id = 15, Name = "Noise-Canceling Earbuds", Description = "True wireless earbuds with active noise cancellation.", AvailableStock = 15, Price = 89 },
        new CatalogItem { Id = 16, Name = "4K Monitor", Description = "27-inch 4K UHD monitor with HDR support.", AvailableStock = 7, Price = 299 },
        new CatalogItem { Id = 17, Name = "External Hard Drive", Description = "2TB external hard drive for backup and storage.", AvailableStock = 12, Price = 79 },
        new CatalogItem { Id = 18, Name = "Webcam", Description = "1080p HD webcam with built-in microphone.", AvailableStock = 20, Price = 49 },
        new CatalogItem { Id = 19, Name = "Desk Lamp", Description = "LED desk lamp with adjustable brightness and color temperature.", AvailableStock = 25, Price = 24 },
        new CatalogItem { Id = 20, Name = "Smart Plug", Description = "Wi-Fi smart plug with voice control and energy monitoring.", AvailableStock = 40, Price = 14 },
        new CatalogItem { Id = 21, Name = "Electric Kettle", Description = "1.7L electric kettle with temperature control.", AvailableStock = 18, Price = 39 },
        new CatalogItem { Id = 22, Name = "Air Purifier", Description = "HEPA air purifier for small to medium rooms.", AvailableStock = 10, Price = 129 },
        new CatalogItem { Id = 23, Name = "Robot Vacuum", Description = "Smart robot vacuum with app control and scheduling.", AvailableStock = 5, Price = 249 },
        new CatalogItem { Id = 24, Name = "Coffee Maker", Description = "Single-serve coffee maker with reusable filter.", AvailableStock = 15, Price = 59 },
        new CatalogItem { Id = 25, Name = "Electric Toothbrush", Description = "Rechargeable electric toothbrush with multiple modes.", AvailableStock = 20, Price = 49 },
        new CatalogItem { Id = 26, Name = "Hair Dryer", Description = "Compact hair dryer with ionic technology.", AvailableStock = 25, Price = 29 },
        new CatalogItem { Id = 27, Name = "Blender", Description = "High-speed blender for smoothies and shakes.", AvailableStock = 12, Price = 89 },
        new CatalogItem { Id = 28, Name = "Pressure Cooker", Description = "6-quart electric pressure cooker with multiple presets.", AvailableStock = 8, Price = 99 },
        new CatalogItem { Id = 29, Name = "Air Fryer", Description = "4-quart air fryer with digital controls.", AvailableStock = 10, Price = 79 },
        new CatalogItem { Id = 30, Name = "Smart Thermostat", Description = "Wi-Fi smart thermostat with energy-saving features.", AvailableStock = 7, Price = 199 }
    };
}