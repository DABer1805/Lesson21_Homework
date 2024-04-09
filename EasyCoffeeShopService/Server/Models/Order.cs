namespace Server;

using System;
using System.ComponentModel.DataAnnotations;

public class Order
{
    [Required]
    [Range(0, 9999)]
    public double TotalPrice { get; set; }

    // тут хранятся id-шки товаров через запятую (Пример: 12,54,1,32)
    [Required]
    [StringLength(2000, MinimumLength = 3)]
    public string ItemsList { get; set; }

    [Required]
    [StringLength(2000, MinimumLength = 3)]
    public string CreateDate { get; set; }

    [StringLength(2000, MinimumLength = 3)]
    public string EndDate { get; set; }

    public Order(double totalPrice, string itemsList, string createDate, string endDate)
    {
        TotalPrice = totalPrice;
        ItemsList = itemsList;
        CreateDate = createDate;
        EndDate = endDate;
    }
}